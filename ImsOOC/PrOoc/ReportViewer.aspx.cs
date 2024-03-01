using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class PrOoc_ReportViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var reportName = Request["description"];
            var reportType = Request["type"];
            lblReferenceNumber.Text = reportName;
            //dvOneDate.Visible = reportType.ToLower().Equals("internal") || reportType.ToLower().Equals("external")
            //    ? true
            //    : false;
            //dvTwoDates.Visible = reportType.ToLower().Equals("inflow") 
            //  ? true
            //  : false;
           trType.Visible =  reportType.ToLower().Equals("inflow") 
            ? true
              : false;
            List<string> year = new List<string>();
            year.Add("Select One..");
            for (int i = 2016; i <= DateTime.Today.Year; i++)
            {
                year.Add(i.ToString());
            }
            drpYear.DataSource = year;
            drpYear.DataBind();
            drpMonth.Enabled = false;
            drDate.Enabled = false;
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
                try
                {
                    var type = Request["type"];
                    if (type.ToLower() != "inflow")
                    {

                        if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
                        {
                            dvReportViewer.Visible = true;
                            DateTime firstDate = DateTime.Parse(txtStartDate.Text);
                            DateTime lastDate = DateTime.Parse(txtEndDate.Text);
                            var processId = Request["processid"];
                            string path = "/IMS/" + Request["reportName"];
                            ReportViewer1.ServerReport.ReportServerUrl =
                                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
                            ReportViewer1.ServerReport.ReportPath = path;

                            ReportParameter[] parameters = new ReportParameter[3];
                            parameters[0] = new ReportParameter("startDate", firstDate.ToShortDateString());
                            parameters[1] = new ReportParameter("ProcessId", processId);
                            parameters[2] = new ReportParameter("Date", lastDate.ToShortDateString());
                            ReportViewer1.ServerReport.SetParameters(parameters);
                            ReportViewer1.ServerReport.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Please select your filter.");
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
                        {
                            dvReportViewer.Visible = true;
                            DateTime firstDate = DateTime.Parse(txtStartDate.Text);
                            DateTime lastDate = DateTime.Parse(txtEndDate.Text);
                            var processId = Request["processid"];
                            string path = "/IMS/" + Request["reportName"];
                            ReportViewer1.ServerReport.ReportServerUrl =
                                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
                            ReportViewer1.ServerReport.ReportPath = path;

                            ReportParameter[] parameters = new ReportParameter[3];
                            parameters[0] = new ReportParameter("StartDate", firstDate.ToShortDateString());
                            parameters[1] = new ReportParameter("ProcessId", processId);
                            parameters[2] = new ReportParameter("EndDate", lastDate.ToShortDateString());
                            ReportViewer1.ServerReport.SetParameters(parameters);
                            ReportViewer1.ServerReport.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Please select your filter.");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                break;
            case "Export":
            {
                try
                {
                    var type = Request["type"];
                    if (type.ToLower() != "inflow")
                    {
                        if (drpMonth.SelectedIndex > 0 && drpYear.SelectedIndex > 0 && drDate.SelectedIndex > 0)
                        {
                            dvReportViewer.Visible = true;
                            DateTime firstDate =
                                DateTime.Parse(string.Format("{0}/{1}/1", drpYear.SelectedItem.Text,
                                    drpMonth.SelectedItem.Text));
                            DateTime lastDate =
                                DateTime.Parse(string.Format("{0}/{1}/{2}", drpYear.SelectedItem.Text,
                                    drpMonth.SelectedItem.Text, drDate.SelectedItem.Text));
                            var processId = Request["processid"];
                            string path = "/IMS/" + Request["reportName"];
                            ReportViewer1.ServerReport.ReportServerUrl =
                                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
                            ReportViewer1.ServerReport.ReportPath = path;

                            ReportParameter[] parameters = new ReportParameter[3];
                            parameters[0] = new ReportParameter("startDate", firstDate.ToShortDateString());
                            parameters[1] = new ReportParameter("ProcessId", processId);
                            parameters[2] = new ReportParameter("Date", lastDate.ToShortDateString());
                            ReportViewer1.ServerReport.SetParameters(parameters);
                            ReportViewer1.ServerReport.Refresh();
                                saveRptAs(ReportViewer1, "pdf");
                        }
                        else
                        {
                            MessageBox.Show("Please select your filter.");
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
                        {
                            dvReportViewer.Visible = true;
                            DateTime firstDate = DateTime.Parse(txtStartDate.Text);
                            DateTime lastDate = DateTime.Parse(txtEndDate.Text);
                            var processId = Request["processid"];
                            string path = "/IMS/" + Request["reportName"];
                            ReportViewer1.ServerReport.ReportServerUrl =
                                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
                            ReportViewer1.ServerReport.ReportPath = path;

                            ReportParameter[] parameters = new ReportParameter[3];
                            parameters[0] = new ReportParameter("StartDate", firstDate.ToShortDateString());
                            parameters[1] = new ReportParameter("ProcessId", processId);
                            parameters[2] = new ReportParameter("EndDate", lastDate.ToShortDateString());
                            ReportViewer1.ServerReport.SetParameters(parameters);
                            ReportViewer1.ServerReport.Refresh();
                            saveRptAs(ReportViewer1, "pdf");
                        }
                        else
                        {
                            MessageBox.Show("Please select your filter.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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