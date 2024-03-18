using Microsoft.Reporting.WebForms;
using System;


public partial class Reports_OOCOfficeBreakdown : IncidentTrackingPage
{
    protected string Heading;
    protected void Page_Load(object sender, EventArgs e)
    {
        Heading = Request.QueryString["description"];
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["reportName"]) &&
                !string.IsNullOrEmpty(Request.QueryString["processId"]))
            {
                LoadDashBoard(Request.QueryString["reportName"], Request.QueryString["processId"]);
            }
        }
    }

    /// <summary>
    /// required parameters
    /// reportName = the name of the report rdl eg.OocOfficeExternal
    ///  description = " report header or description
    /// processid = processId 
    /// </summary>
    /// <param name="reportName"></param>
    /// <param name="processId"></param>
    private void LoadDashBoard(string reportName, string processId)
    {
        try
        {
            tblReport.Visible = true;
            string path = "/IMS/" + reportName;
            ReportViewer1.ServerReport.ReportServerUrl =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            ReportViewer1.ServerReport.ReportPath = path;
            ReportParameter[] parameters = new ReportParameter[1];;
            parameters[0] = new ReportParameter("ProcessId", processId);

            ReportViewer1.ServerReport.SetParameters(parameters);
            ReportViewer1.ServerReport.Refresh();
            ReportViewer1.Visible = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDate.Text))
        {
           // LoadDashBoard();
        }
        else
        {
            MessageBox.Show("Enter a correct Date!");
        }
    }
}