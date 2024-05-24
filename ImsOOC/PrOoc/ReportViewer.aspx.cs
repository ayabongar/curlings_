using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Sars.Systems.Security;

public partial class PrOoc_ReportViewer : System.Web.UI.Page
{
    public string _DateFrom = null;
    public string _DateTo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDashBoard(Request["reportname"]);
        }
    }

    private void LoadDashBoard(string reportName)
    {

        try
        {

            var userRole = this.Page.User.GetRole();
            var roleId = new Guid();
            switch (userRole)
            {
                case "Administrator Head - Top Secret":
                    roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                    break;
                case "Administrator Manager - Secret":
                    roleId = new Guid(ConfigurationManager.AppSettings["AdministratorManagerSecret"]);
                    break;
                case "Administrator - confidential":
                    roleId = new Guid(ConfigurationManager.AppSettings["AdministratorConfidential"]);
                    break;
                case "System User":
                    roleId = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
                    break;
                case "Developer":
                    roleId = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
                    break;
            }

             string path = "/IMS/" + reportName;
            
            ReportViewer1.ServerReport.ReportServerUrl =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            ReportViewer1.ServerReport.ReportPath = path;
            DateTime date = DateTime.Today;
            _DateFrom = new DateTime(date.Year - 1, 04, 01).ToString("dd-MMM-yyyy");
            _DateTo = new DateTime(date.Year, 03, 31).ToString("dd-MMM-yyyy");
            if (date.Month > 4)
            {
                _DateFrom = new DateTime(date.Year, 04, 01).ToString("dd-MMM-yyyy");
                _DateTo = new DateTime(date.Year + 1, 03, 31).ToString("dd-MMM-yyyy");
            }

            if (reportName.Equals("oocstatstaxescmng"))
            {
                ReportParameter[] reportParam = new ReportParameter[3];
                reportParam[0] = new ReportParameter("processId", "120");
                reportParam[1] = new ReportParameter("startDate", _DateFrom.ToString());
                reportParam[2] = new ReportParameter("EndDate", _DateTo.ToString());                
                ReportViewer1.ServerReport.SetParameters(reportParam);
            }
            else if (reportName.Equals("OocNewDashboard")) 
            {
                ReportParameter[] reportParam = new ReportParameter[3];
                reportParam[0] = new ReportParameter("processId", "96");
                reportParam[1] = new ReportParameter("startDate", _DateFrom.ToString());
                reportParam[2] = new ReportParameter("EndDate", _DateTo.ToString());
                ReportViewer1.ServerReport.SetParameters(reportParam);
            }
            else
            {
                ReportParameter[] reportParam = new ReportParameter[4];
                //reportParam[0] = new ReportParameter("ProcessId", "96");
                reportParam[0] = new ReportParameter("startDate", _DateFrom.ToString());
                reportParam[1] = new ReportParameter("EndDate", _DateTo.ToString());
                reportParam[2] = new ReportParameter("roleId", roleId.ToString());
                reportParam[3] = new ReportParameter("SID", SarsUser.SID);
                ReportViewer1.ServerReport.SetParameters(reportParam);
            }
            ReportViewer1.ServerReport.Refresh();
            ReportViewer1.Visible = true;
        }
        catch (Exception)
        {

            // throw;
        }
    }


    protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<string> day = new List<string>();
        if (drpYear.SelectedIndex > 0 && drpMonth.SelectedIndex > 0)
        {
            int daysInMonth = System.DateTime.DaysInMonth(int.Parse(drpYear.SelectedItem.Text), int.Parse(drpMonth.SelectedItem.Text));
            int count = 0;
            count = DateTime.Today.Month == int.Parse(drpMonth.SelectedItem.Text) ? DateTime.Today.Day : daysInMonth;

            day.Add("Select One..");
            for (int i = 1; i <= count; i++)
            {
                day.Add(i.ToString());
            }
            drDate.DataSource = day;
            drDate.DataBind();
            drDate.Enabled = true;
        }
    }


    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Submit":
                //try
                //{
                //    var type = Request["type"];
                //    if (type.ToLower() != "inflow")
                //    {

                //        if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
                //        {
                //            dvReportViewer.Visible = true;
                //            DateTime firstDate = DateTime.Parse(txtStartDate.Text);
                //            DateTime lastDate = DateTime.Parse(txtEndDate.Text);
                //            var processId = Request["processid"];
                //            string path = "/IMS/" + Request["reportName"];
                //            ReportViewer1.ServerReport.ReportServerUrl =
                //                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
                //            ReportViewer1.ServerReport.ReportPath = path;

                //            ReportParameter[] parameters = new ReportParameter[3];
                //            parameters[0] = new ReportParameter("startDate", firstDate.ToShortDateString());
                //            parameters[1] = new ReportParameter("ProcessId", processId);
                //            parameters[2] = new ReportParameter("Date", lastDate.ToShortDateString());
                //            ReportViewer1.ServerReport.SetParameters(parameters);
                //            ReportViewer1.ServerReport.Refresh();
                //        }
                //        else
                //        {
                //            MessageBox.Show("Please select your filter.");
                //        }
                //    }
                //    else
                //    {
                //        if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
                //        {
                //            dvReportViewer.Visible = true;
                //            DateTime firstDate = DateTime.Parse(txtStartDate.Text);
                //            DateTime lastDate = DateTime.Parse(txtEndDate.Text);
                //            var processId = Request["processid"];
                //            string path = "/IMS/" + Request["reportName"];
                //            ReportViewer1.ServerReport.ReportServerUrl =
                //                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
                //            ReportViewer1.ServerReport.ReportPath = path;

                //            ReportParameter[] parameters = new ReportParameter[3];
                //            parameters[0] = new ReportParameter("StartDate", firstDate.ToShortDateString());
                //            parameters[1] = new ReportParameter("ProcessId", processId);
                //            parameters[2] = new ReportParameter("EndDate", lastDate.ToShortDateString());
                //            ReportViewer1.ServerReport.SetParameters(parameters);
                //            ReportViewer1.ServerReport.Refresh();
                //        }
                //        else
                //        {
                //            MessageBox.Show("Please select your filter.");
                //        }
                //    }

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                break;
            case "Export":
            {
                //try
                //{
                //    var type = Request["type"];
                //    if (type.ToLower() != "inflow")
                //    {
                //        if (drpMonth.SelectedIndex > 0 && drpYear.SelectedIndex > 0 && drDate.SelectedIndex > 0)
                //        {
                //            dvReportViewer.Visible = true;
                //            DateTime firstDate =
                //                DateTime.Parse(string.Format("{0}/{1}/1", drpYear.SelectedItem.Text,
                //                    drpMonth.SelectedItem.Text));
                //            DateTime lastDate =
                //                DateTime.Parse(string.Format("{0}/{1}/{2}", drpYear.SelectedItem.Text,
                //                    drpMonth.SelectedItem.Text, drDate.SelectedItem.Text));
                //            var processId = Request["processid"];
                //            string path = "/IMS/" + Request["reportName"];
                //            ReportViewer1.ServerReport.ReportServerUrl =
                //                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
                //            ReportViewer1.ServerReport.ReportPath = path;

                //            ReportParameter[] parameters = new ReportParameter[3];
                //            parameters[0] = new ReportParameter("startDate", firstDate.ToShortDateString());
                //            parameters[1] = new ReportParameter("ProcessId", processId);
                //            parameters[2] = new ReportParameter("Date", lastDate.ToShortDateString());
                //            ReportViewer1.ServerReport.SetParameters(parameters);
                //            ReportViewer1.ServerReport.Refresh();
                //                saveRptAs(ReportViewer1, "pdf");
                //        }
                //        else
                //        {
                //            MessageBox.Show("Please select your filter.");
                //        }
                //    }
                //    else
                //    {
                //        if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
                //        {
                //            dvReportViewer.Visible = true;
                //            DateTime firstDate = DateTime.Parse(txtStartDate.Text);
                //            DateTime lastDate = DateTime.Parse(txtEndDate.Text);
                //            var processId = Request["processid"];
                //            string path = "/IMS/" + Request["reportName"];
                //            ReportViewer1.ServerReport.ReportServerUrl =
                //                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
                //            ReportViewer1.ServerReport.ReportPath = path;

                //            ReportParameter[] parameters = new ReportParameter[3];
                //            parameters[0] = new ReportParameter("StartDate", firstDate.ToShortDateString());
                //            parameters[1] = new ReportParameter("ProcessId", processId);
                //            parameters[2] = new ReportParameter("EndDate", lastDate.ToShortDateString());
                //            ReportViewer1.ServerReport.SetParameters(parameters);
                //            ReportViewer1.ServerReport.Refresh();
                //            saveRptAs(ReportViewer1, "pdf");
                //        }
                //        else
                //        {
                //            MessageBox.Show("Please select your filter.");
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                break;
            }
        }
       
    }


    private void saveRptAs(ReportViewer ReportViewer1, String s_rptType)
    {
        try
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            // string deviceInfo;
            byte[] bytes = ReportViewer1.ServerReport.Render(
            s_rptType, null, out mimeType, out encoding, out extension,
            out streamids, out warnings);
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=Report." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
        }
        catch (Exception)
        {
            MessageBox.Show("No report has been found.");
        }
    }
    protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpYear.SelectedIndex > 0)
        {
            List<string> month = new List<string>();
            month.Add("Select One..");
            int count = 0;
            count = DateTime.Today.Year == int.Parse(drpYear.SelectedItem.Text) ? DateTime.Today.Month : 12;
            for (int i = 1; i <= count; i++)
            {
                month.Add(i.ToString());
            }
            drpMonth.DataSource = month;
            drpMonth.DataBind();
            drpMonth.Enabled = true;
        }
        else
        {
            drpMonth.SelectedIndex = 0;
            drDate.SelectedIndex = 0;
            drpMonth.Enabled = false;
            drDate.Enabled = false;
        }
    }
}