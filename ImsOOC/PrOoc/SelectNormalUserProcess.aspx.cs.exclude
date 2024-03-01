using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using Sars.Systems.Data;
using Sars.Systems.Extensions;

public partial class PrOoc_SelectNormalUserProcess : IncidentTrackingPage
{
    public string _DateFrom = null;
    public string _DateTo = null;
    private DataTable Export
    {
        get { return ViewState["Export"] as DataTable; }
        set { ViewState["Export"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProcesses();
            LoadDashBoard();
        }
    }
    private void LoadProcesses()
    {
        var processes =
            new IncidentProcess("[dbo].[uspRead_UserProcesses]",
                                new Dictionary<string, object>
                                    {
                                        {"@UserSID", SarsUser.SID}
                                    }).GetRecords<IncidentProcess>();



        if (processes != null && processes.Any())
        {

            if (processes.Count >= 1)
            {
                string[] oocOffice = System.Configuration.ConfigurationManager.AppSettings["oocOffice"].ToString().Split(',');
                IEnumerable<IncidentProcess> oocProcess = null;
                //switch (oocOffice.Length)
                //{
                //    case 2:
                //        oocProcess = processes.ToList().Where(c => c.ProcessId == int.Parse(oocOffice[0]) || c.ProcessId == int.Parse(oocOffice[1])).OrderBy(c=>c.Timestamp);
                //        break;
                //    case 3:
                //        oocProcess = processes.ToList().Where(c => c.ProcessId == int.Parse(oocOffice[0]) || c.ProcessId == int.Parse(oocOffice[1]) || c.ProcessId == int.Parse(oocOffice[2])).OrderBy(c => c.Timestamp);
                //        break;                  
                //}
                gvProcesses.Bind(processes.ToList());
                drpProcess.DataSource = oocProcess;
                drpProcess.DataTextField = "Description";
                drpProcess.DataValueField = "ProcessId";
                drpProcess.DataBind();
                drpProcess.Items.Insert(0, new ListItem("Select Process", "0"));               
            }
        }       
    }    
    
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        e.Row.Attributes.Add("onclick",
                             Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        var description = DataBinder.Eval(e.Row.DataItem, "Subject").ToString();

        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        e.Row.Attributes.Add("title",
                             "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Process Description (Click to open this system)</font></b>] body=[<font color='red'><b>" +
                             description + "</b></font>]");
    }

    protected void pRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        e.Row.Attributes.Add("onclick",
                             Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        var description = DataBinder.Eval(e.Row.DataItem, "Description").ToString();

        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        e.Row.Attributes.Add("title",
                             "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Process Description (Click to open this system)</font></b>] body=[<font color='red'><b>" +
                             description + "</b></font>]");
    }



    protected void gvProcesses_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gvProcesses.SelectedDataKey != null)
        {
            var pname = gvProcesses.SelectedDataKey["Description"].ToString();
            var prodessId = gvProcesses.SelectedDataKey["ProcessId"].ToString();
            if (!string.IsNullOrEmpty(prodessId))
            {
                string processType = string.Empty;
                if (pname.Contains("External"))
                    processType = "External";
                else if (pname.Contains("Internal"))
                    processType = "Internal";
                else if (pname.Contains("Escalation"))
                    processType = "Escalation";
                if (!string.IsNullOrEmpty(processType))
                {
                    Response.Redirect(string.Format("~/PrOoc/NormalUserLandingPage.aspx?procId={0}&type={1}", prodessId, processType));
                }
                else
                {
                    Response.Redirect(string.Format("~/Admin/NormalUserLandingPage.aspx?procId={0}", prodessId));
                }
            }
        }
    }

    private void LoadDashBoard()
    {
        try
        {

        
        const string path = "/IMS/imsDashboard";

        ReportViewer1.ServerReport.ReportServerUrl =
            new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
        ReportViewer1.ServerReport.ReportPath = path;
        DateTime date = DateTime.Today;
        var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        _DateFrom = firstDayOfMonth.ToString("dd-MMM-yyyy");
        _DateTo = lastDayOfMonth.ToString("dd-MMM-yyyy");
        ReportParameter[] reportParam = new ReportParameter[3];
        reportParam[0] = new ReportParameter("SID", SarsUser.SID);
        reportParam[1] = new ReportParameter("startDate", firstDayOfMonth.ToString());
        reportParam[2] = new ReportParameter("Date", lastDayOfMonth.ToString());

        ReportViewer1.ServerReport.SetParameters(reportParam);
        ReportViewer1.ServerReport.Refresh();
        ReportViewer1.Visible = true;
        }
        catch (Exception)
        {

            //throw;
        }

        List<IncidentProcess> process =   IncidentTrackingManager.GetIncidentProcesses(SarsUser.SID);
        if(process != null)
        {
            var externalProcess = process.Find(p => p.Description.ToLower().Contains("external"));
            if(externalProcess != null)
            {
                LoadExternal(externalProcess.ProcessId.ToString());
                LoadExternalInflow(externalProcess.ProcessId.ToString());

            }
            else
            {
                tabExternal.Visible = false;
                rptInflowExternal.Visible = false;
            }
            var internalProcess = process.Find(p => p.Description.ToLower().Contains("internal"));
            if (internalProcess != null)
            {
                LoadInternal(internalProcess.ProcessId.ToString());
                LoadInternalInflow(internalProcess.ProcessId.ToString());
            }
            else
            {
                rptInflowInternal.Visible = false;
                tabInternal.Visible = false;
            }
            var escalation = process.Find(p => p.Description.ToLower().Contains("escalation"));
            if (escalation != null)
            {
                LoadTaxEscalations(escalation.ProcessId.ToString());
                LoadtTaxEscalationInflow(escalation.ProcessId.ToString());
            }
            else
            {
                fstTaxEscalation.Visible = false;
                tabEscalations.Visible = false;
            }
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(DateFrom.Text) && !string.IsNullOrEmpty(DateTo.Text))
        {
            DateTime testDate;
            if (!DateTime.TryParse(DateFrom.Text, out testDate))
            {
                MessageBox.Show("Date From formart is invalid.");
                return;
            }
            if (!DateTime.TryParse(DateTo.Text, out testDate))
            {
                MessageBox.Show("Date To formart is invalid.");
                return;
            }
            const string path = "/IMS/imsDashboard";
            ReportViewer1.ServerReport.ReportServerUrl =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            ReportViewer1.ServerReport.ReportPath = path;

            _DateFrom = DateTime.Parse(DateFrom.Text).ToString("dd-MMM-yyyy");
            _DateTo = DateTime.Parse(DateTo.Text).ToString("dd-MMM-yyyy");
            ReportParameter[] reportParam = new ReportParameter[3];
            reportParam[0] = new ReportParameter("SID", SarsUser.SID);
            reportParam[1] = new ReportParameter("startDate", DateFrom.Text);
            reportParam[2] = new ReportParameter("Date", DateTo.Text);

            ReportViewer1.ServerReport.SetParameters(reportParam);
            ReportViewer1.ServerReport.Refresh();
            ReportViewer1.Visible = true;
            List<IncidentProcess> process = IncidentTrackingManager.GetIncidentProcesses(SarsUser.SID);
            if (process != null)
            {
                var externalProcess = process.Find(p => p.Description.ToLower().Contains("external"));
                if (externalProcess != null)
                {
                    LoadExternal(externalProcess.ProcessId.ToString());
                    LoadExternalInflow(externalProcess.ProcessId.ToString());

                }
                else
                {
                    tabExternal.Visible = false;
                    rptInflowExternal.Visible = false;
                }
                var internalProcess = process.Find(p => p.Description.ToLower().Contains("internal"));
                if (internalProcess != null)
                {
                    LoadInternal(internalProcess.ProcessId.ToString());
                    LoadInternalInflow(internalProcess.ProcessId.ToString());
                }
                else
                {
                    rptInflowInternal.Visible = false;
                    tabInternal.Visible = false;
                }
            }
        }
    }

    private void LoadInternal(string processId)
    {
        if (!string.IsNullOrEmpty(_DateFrom) && !string.IsNullOrEmpty(_DateTo))
        {           
            DateTime firstDate = DateTime.Parse(_DateFrom);
            DateTime lastDate = DateTime.Parse(_DateTo);           
            string path = "/IMS/OocOfficeInternal";
            rptInternal.ServerReport.ReportServerUrl =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            rptInternal.ServerReport.ReportPath = path;

            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("startDate", firstDate.ToShortDateString());
            parameters[1] = new ReportParameter("ProcessId", processId);
            parameters[2] = new ReportParameter("Date", lastDate.ToShortDateString());
            rptInternal.ServerReport.SetParameters(parameters);
            rptInternal.ServerReport.Refresh();
        }
        else
        {
            MessageBox.Show("Please select your filter.");
        }
    }

    private void LoadExternal(string processId)
    {
        if (string.IsNullOrEmpty(_DateFrom) && string.IsNullOrEmpty(_DateTo))
        {
            var firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            _DateFrom = firstDayOfMonth.ToShortDateString();
            _DateTo = lastDayOfMonth.ToShortDateString();
        }
        if (!string.IsNullOrEmpty(_DateFrom) && !string.IsNullOrEmpty(_DateTo))
        {
            DateTime firstDate = DateTime.Parse(_DateFrom);
            DateTime lastDate = DateTime.Parse(_DateTo);            
            string path = "/IMS/OocOfficeExternal";
            rptExternal.ServerReport.ReportServerUrl =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            rptExternal.ServerReport.ReportPath = path;

            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("startDate", firstDate.ToShortDateString());
            parameters[1] = new ReportParameter("ProcessId", processId);
            parameters[2] = new ReportParameter("Date", lastDate.ToShortDateString());
            rptExternal.ServerReport.SetParameters(parameters);
            rptExternal.ServerReport.Refresh();
        }
        else
        {
            MessageBox.Show("Please select your filter.");
        }
    }

    private void LoadTaxEscalations(string processId)
    {
        if(string.IsNullOrEmpty(_DateFrom) && string.IsNullOrEmpty(_DateTo))
        {
            var firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            _DateFrom = firstDayOfMonth.ToShortDateString();
            _DateTo = lastDayOfMonth.ToShortDateString();
        }
        try
        {

        
        if (!string.IsNullOrEmpty(_DateFrom) && !string.IsNullOrEmpty(_DateTo))
        {
            DateTime firstDate = DateTime.Parse(_DateFrom);
            DateTime lastDate = DateTime.Parse(_DateTo);
            string path = "/IMS/OocOfficeExternal";
            rptEscalations.ServerReport.ReportServerUrl =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            rptEscalations.ServerReport.ReportPath = path;

            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("startDate", firstDate.ToShortDateString());
            parameters[1] = new ReportParameter("ProcessId", processId);
            parameters[2] = new ReportParameter("Date", lastDate.ToShortDateString());
            rptEscalations.ServerReport.SetParameters(parameters);
            rptEscalations.ServerReport.Refresh();
        }
        else
        {
            MessageBox.Show("Please select your filter.");
        }
        }
        catch (Exception)
        {

            //throw;
        }
    }
    private void LoadInternalInflow(string processId)
    {
        if (!string.IsNullOrEmpty(_DateFrom) && !string.IsNullOrEmpty(_DateTo))
        {
            DateTime firstDate = DateTime.Parse(_DateFrom);
            DateTime lastDate = DateTime.Parse(_DateTo);           
            string path = "/IMS/OOCInflow";
            rptInflowInternal.ServerReport.ReportServerUrl =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            rptInflowInternal.ServerReport.ReportPath = path;

            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("StartDate", firstDate.ToShortDateString());
            parameters[1] = new ReportParameter("ProcessId", processId);
            parameters[2] = new ReportParameter("EndDate", lastDate.ToShortDateString());
            rptInflowInternal.ServerReport.SetParameters(parameters);
            rptInflowInternal.ServerReport.Refresh();
        }
        else
        {
            MessageBox.Show("Please select your filter.");
        }
    }
    private void LoadtTaxEscalationInflow(string processId)
    {
        try
        {


            if (!string.IsNullOrEmpty(_DateFrom) && !string.IsNullOrEmpty(_DateTo))
            {
                DateTime firstDate = DateTime.Parse(_DateFrom);
                DateTime lastDate = DateTime.Parse(_DateTo);
                string path = "/IMS/OOCInflow";
                rptTaxEscalations.ServerReport.ReportServerUrl =
                    new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
                rptTaxEscalations.ServerReport.ReportPath = path;

                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("StartDate", firstDate.ToShortDateString());
                parameters[1] = new ReportParameter("ProcessId", processId);
                parameters[2] = new ReportParameter("EndDate", lastDate.ToShortDateString());
                rptTaxEscalations.ServerReport.SetParameters(parameters);
                rptTaxEscalations.ServerReport.Refresh();
            }
            else
            {
                MessageBox.Show("Please select your filter.");
            }
        }
        catch (Exception)
        {

           // throw;
        }
    }
    private void LoadExternalInflow(string processId)
    {
        if (!string.IsNullOrEmpty(_DateFrom) && !string.IsNullOrEmpty(_DateTo))
        {
            DateTime firstDate = DateTime.Parse(_DateFrom);
            DateTime lastDate = DateTime.Parse(_DateTo);            
            string path = "/IMS/OOCInflow";
            rptInflowExternal.ServerReport.ReportServerUrl =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            rptInflowExternal.ServerReport.ReportPath = path;
            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("StartDate", firstDate.ToShortDateString());
            parameters[1] = new ReportParameter("ProcessId", processId);
            parameters[2] = new ReportParameter("EndDate", lastDate.ToShortDateString());
            rptInflowExternal.ServerReport.SetParameters(parameters);
            rptInflowExternal.ServerReport.Refresh();
        }
        else
        {
            MessageBox.Show("Please select your filter.");
        }
    }

       protected void gvReports_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (!String.IsNullOrEmpty(Request["stsId"]))
        {
            var data = Export;
            gvReports.NextPage(data, e.NewPageIndex);
        }
    }

    #region Search
    protected void FilterTypeChanged(object sender, EventArgs e)
    {       
        SearchFilterTypes1.ShowInputs(ddlFilterType.SelectedValue);
        fsSearchButton.Visible = ddlFilterType.SelectedIndex > 0;

    }

    protected void SearchIncidents(object sender, EventArgs e)
    {
        try
        {

        
        if (drpProcess.SelectedIndex <= 0)
        {
            MessageBox.Show("Please select a process.");
            return;
        }
        switch (ddlFilterType.SelectedValue)
        {
            case "1":
            case "DateRegistered":
                {
                    if (String.IsNullOrEmpty(SearchFilterTypes1.FromDate) || String.IsNullOrEmpty(SearchFilterTypes1.ToDate))
                    {
                        MessageBox.Show("Either Start Date or End Date is not provided");
                        return;
                    }
                    break;
                }
            case "RegisteredBy":
            case "AssignedTo":
                {
                    if (String.IsNullOrEmpty(SearchFilterTypes1.SID))
                    {
                        MessageBox.Show("Please search and select a user.");
                        return;
                    }
                    break;
                }
            case "5":
            case "IncidentNumber":
                {
                    if (String.IsNullOrEmpty(SearchFilterTypes1.ReferenceNumber))
                    {
                        MessageBox.Show("Reference Number/Incident number Is Required.");
                        return;
                    }
                    break;
                }

            default:
                {
                    break;
                }
        }
        var recordSet = IncidentTrackingManager.OocSearchIncidents(SearchFilterTypes1.ReferenceNumber, SearchFilterTypes1.FromDate, SearchFilterTypes1.ToDate, SearchFilterTypes1.SID, ddlFilterType.SelectedValue, drpProcess.SelectedValue);

        if (recordSet.HasRows)
        {
            Session["searchData"] = recordSet;
            gvIncidents.Bind(recordSet);           
        }
        else
        {
            MessageBox.Show("No results found");
            gvIncidents.Bind(null);

        }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
           // throw;
        }
    }
    protected void PageChanging(object sender, GridViewPageEventArgs e)
    {
        if (Session["searchData"] != null)
        {
            gvIncidents.NextPage(Session["searchData"], e.NewPageIndex);
            
        }       
    }

    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProcesses.PageIndex = e.NewPageIndex;
        LoadProcesses();
    }
    private void GetProcessName(out string processName, out string pageName)
    {
        processName = string.Empty;
        pageName = string.Empty;
        if (drpProcess.SelectedItem.Text.ToLower().Contains("internal"))
        {
            processName = "Internal";
            pageName = "RegisterUserIncident";
        }
        else if (drpProcess.SelectedItem.Text.ToLower().Contains("external"))
        {
            processName = "External";
            pageName = "RegisterExternalIncident";
        }
        else if (drpProcess.SelectedItem.Text.ToLower().Contains("escalation"))
        {
            processName = "escalation";
            pageName = "RegisterTaxEscalations";
        }

    }
    protected void gvIncidents_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (gvIncidents.SelectedDataKey != null)
        {
            var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
            string _processName , _pageName ;
            GetProcessName(out _processName, out _pageName);            
            if (!string.IsNullOrEmpty(incidentId))
            {
                Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}&type={3}", _pageName, drpProcess.SelectedValue, incidentId, _processName));
            }
        }
    }

  
    protected void gvIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string _processName, _pageName;
        GetProcessName(out _processName, out _pageName);
        if (e.CommandName.Equals("bntReAssign"))
        {
            var incidentId = e.CommandArgument.ToString();
            Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}&type={3}", _pageName, drpProcess.SelectedValue, incidentId, _processName));
        }
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        if(drpProcess.SelectedIndex <=0)
        {
            MessageBox.Show("Please select a process.");
            return;
        }
        switch (ddlFilterType.SelectedValue)
        {
            case "1":
            case "2":
                {
                    if (String.IsNullOrEmpty(SearchFilterTypes1.FromDate) || String.IsNullOrEmpty(SearchFilterTypes1.ToDate))
                    {
                        MessageBox.Show("Either Start Date or End Date is not provided");
                        return;
                    }
                    break;
                }
            case "3":
            case "4":
                {
                    if (String.IsNullOrEmpty(SearchFilterTypes1.SID))
                    {
                        MessageBox.Show("Please search and select a user.");
                        return;
                    }
                    break;
                }
            case "5":
            case "6":
                {
                    if (String.IsNullOrEmpty(SearchFilterTypes1.ReferenceNumber))
                    {
                        MessageBox.Show("Reference Number/Incident number Is Required.");
                        return;
                    }
                    break;
                }

            default:
                {
                    break;
                }
        }
        var recordSet = IncidentTrackingManager.OocSearchIncidents(SearchFilterTypes1.ReferenceNumber, SearchFilterTypes1.FromDate, SearchFilterTypes1.ToDate, SearchFilterTypes1.SID, ddlFilterType.SelectedValue, drpProcess.SelectedValue);

        if (recordSet.HasRows)
        {
            Session["searchData"] = recordSet;
            gvIncidents.Bind(recordSet);
          
            recordSet.Tables[0].ToExcel(null, null);
        }
        else
        {
            MessageBox.Show("No results found");          
        }
    }
    #endregion
}