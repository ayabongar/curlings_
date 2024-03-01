using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using Sars.Systems.Data;
using Sars.Systems.Extensions;

public partial class OOC_Default : IncidentTrackingPage
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
            if (processes.Count > 1)
            {
                gvProcesses.Bind(processes);
                drpProcess.DataSource = processes;
                drpProcess.DataTextField = "Description";
                drpProcess.DataValueField = "ProcessId";
                drpProcess.DataBind();
                drpProcess.Items.Insert(0, new ListItem("Select Process", "0"));
                //var incidents = IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID, 0);
                //if (incidents != null && incidents.Any())
                //{
                //    gvIncidents.Bind(incidents.FindAll(inc => inc.IncidentStatusId == 2 || inc.IncidentStatusId == 3));
                //}
                // ooc 97 and 98

                //string[] oocOffice = System.Configuration.ConfigurationManager.AppSettings["oocOffice"].ToString().Split(',');
                //if (oocOffice.Length == 3)
                //{
                //    var process = processes.ToList().Where(c => c.ProcessId != int.Parse(oocOffice[0]) && c.ProcessId != int.Parse(oocOffice[1])
                //    && c.ProcessId != int.Parse(oocOffice[2]));
                //    gvProcesses.Bind(process.ToList());
                //    drpProcess.DataSource = process;
                //    drpProcess.DataTextField = "Description";
                //    drpProcess.DataValueField = "ProcessId";
                //    drpProcess.DataBind();
                //    drpProcess.Items.Insert(0, new ListItem("Select Process", "0"));
                //}
                //else
                //{
                //    gvProcesses.Bind(processes);
                //    drpProcess.DataSource = processes;
                //    drpProcess.DataTextField = "Description";
                //    drpProcess.DataValueField = "ProcessId";
                //    drpProcess.DataBind();
                //    drpProcess.Items.Insert(0, new ListItem("Select Process", "0"));
                //}               

                return;
            }
            if (processes.Count == 1)
            {
                var proc = processes[0];
                Response.Redirect(string.Format("NormalUserLandingPage.aspx?procId={0}", proc.ProcessId));
            }
            Response.Redirect("../NormalUserDefault.aspx");
        }
        else
        {
            Response.Redirect("../NormalUserDefault.aspx");
        }
    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProcesses.PageIndex = e.NewPageIndex;
        LoadProcesses();
    }
    protected void IncidentPageChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncidents.PageIndex = e.NewPageIndex;
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void IncidentRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }


        var summary = DataBinder.Eval(e.Row.DataItem, "Summary");
        if (summary != null)
        {

            e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
            e.Row.Attributes.Add("title",
                                 "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Incident Summary</font></b>] body=[<font color='red'><b>" +
                                 summary + "</b></font>]");
        }


        //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        var dueDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "DueDate"));
        var now = DateTime.Now;
        if ((dueDate.Year == now.Year && dueDate.Month == now.Month && dueDate.Day == now.Day) && dueDate.Ticks < now.Ticks)
        {
            e.Row.Attributes["class"] = "slaabouttobeviolated";
        }
        else if (dueDate < now)
        {
            e.Row.Attributes["class"] = "slaviolated";
        }
        else
        {
            e.Row.Attributes["class"] = "slakept";
        }

    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        DateTime testDate;
        if (!DateTime.TryParse(txtDate.Text, out testDate))
        {
            MessageBox.Show("Start Date From formart is invalid.");
            return;
        }
        if (!DateTime.TryParse(txtEndDate.Text, out testDate))
        {
            MessageBox.Show("End Date To formart is invalid.");
            return;
        }
        switch (e.CommandName)
        {
            case "Submit":
                {
                    GetData();
                    break;
                }
            case "Export":
                {
                    ExportData();
                    break;
                }
        }
    }

    protected void gvIncidents_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (gvIncidents.SelectedDataKey != null)
        //{
        //    var prodessId = gvIncidents.SelectedDataKey["ProcessId"].ToString();
        //    var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
        //    if (!string.IsNullOrEmpty(prodessId) && !string.IsNullOrEmpty(incidentId))
        //    {
        //        Response.Redirect(String.Format("~/admin/RegisterUserIncident.aspx?procId={0}&incId={1}", prodessId, incidentId));
        //    }
        //}
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
                if (prodessId == "97")
                    processType = "External";
                else if (prodessId == "98")
                    processType = "Internal";
                else if (pname.Contains("Escalation"))
                    processType = "Escalation";
                if (!string.IsNullOrEmpty(processType))
                {
                    Response.Redirect(string.Format("~/PrOoc/NormalUserLandingPage.aspx?procId={0}&type={1}", prodessId, processType));
                }
                else
                {
                    Response.Redirect(string.Format("NormalUserLandingPage.aspx?procId={0}", prodessId));
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
            var firstDayOfMonth = new DateTime(2015, 01, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            _DateFrom = firstDayOfMonth.ToString("dd-MMM-yyyy");
            _DateTo = DateTime.Now.ToString("dd-MMM-yyyy");
            ReportParameter[] reportParam = new ReportParameter[3];
            reportParam[0] = new ReportParameter("SID", SarsUser.SID);
            reportParam[1] = new ReportParameter("startDate", firstDayOfMonth.ToString());
            reportParam[2] = new ReportParameter("Date", _DateTo.ToString());

            ReportViewer1.ServerReport.SetParameters(reportParam);
            ReportViewer1.ServerReport.Refresh();
            ReportViewer1.Visible = true;
        }
        catch (Exception)
        {

            // throw;
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
        }
    }

    protected void ExportData()
    {

        DateTime testDate;
        if (!DateTime.TryParse(txtDate.Text, out testDate))
        {
            MessageBox.Show("Start Date formart is invalid, mut be (yyyy-MM-dd).");
            return;
        }
        if (!DateTime.TryParse(txtEndDate.Text, out testDate))
        {
            MessageBox.Show("End Date formart is invalid, mut be (yyyy-MM-dd.");
            return;
        }
        if (drpProcess.SelectedIndex < 0)
        {
            MessageBox.Show("Please select a Process");
            return;
        }
        try
        {
            var data = IncidentTrackingManager.GetProcessReportBystatus(drpProcess.SelectedValue, string.Empty);
            DataTable tblFiltered = new DataTable();
            if (!string.IsNullOrEmpty(txtDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                var tblRow = data.Tables[0].AsEnumerable()
                     .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                  && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text) && row.Field<string>("AssignedToSID") == SarsUser.SID);
                if (tblRow.Any())
                {
                    tblFiltered = tblRow.CopyToDataTable();
                }
                gvReports.Bind(tblFiltered);
                Export = tblFiltered;
                tblFiltered.ToExcel(null, null);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }

    protected void GetData()
    {

        DateTime testDate;
        if (!DateTime.TryParse(txtDate.Text, out testDate))
        {
            MessageBox.Show("Start Date formart is invalid, mut be (yyyy-MM-dd).");
            return;
        }
        if (!DateTime.TryParse(txtEndDate.Text, out testDate))
        {
            MessageBox.Show("End Date formart is invalid, mut be (yyyy-MM-dd.");
            return;
        }

        if (drpProcess.SelectedIndex < 0)
        {
            MessageBox.Show("Please select a Process");
            return;
        }
        try
        {
            var data = IncidentTrackingManager.GetProcessReportBystatus(drpProcess.SelectedValue, string.Empty);
            DataTable tblFiltered = new DataTable();
            if (!string.IsNullOrEmpty(txtDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                var tblRow = data.Tables[0].AsEnumerable()
                     .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                  && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text) && row.Field<string>("AssignedToSID") == SarsUser.SID);
                if (tblRow.Any())
                {
                    tblFiltered = tblRow.CopyToDataTable();
                }
                gvReports.Bind(tblFiltered);
                Export = tblFiltered;
            }
        }
        catch (Exception ex)
        {

            throw;//MessageBox.Show(ex.Message);
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
}