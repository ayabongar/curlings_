using System.Data;
using Microsoft.Reporting.WebForms;
using System;
using Sars.Systems.Data;

public partial class Reports_RenderReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadDashBoard("1", "", "");
    }


    private void LoadDashBoard(string reportName, string processId,string reportDesc)
    {
        try
        {
            tblReport.Visible = true;
            string path = "Reports/ooc.rdlc";
            ReportViewer1.LocalReport.ReportPath = path;

            // Fetch ordered data
            var data = GetDataOrderedBySIandSO(processId);

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("ProcessId", processId);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "IMSDataSet";
            rds.Value = data;
            ReportViewer1.LocalReport.DataSources.Add(rds); 


           // ReportViewer1.ServerReport.SetParameters(parameters);
            ReportViewer1.LocalReport.SetParameters(parameters);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.Visible = true;

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private DataTable GetDataOrderedBySIandSO(string processId)
    {
        // retrieve and order data based on SI and SO
        //var data = IncidentTrackingManager.GetIncidentTracking(processId);
        var data = new DataTable();
        
        // data.Columns.Add("SI");
        // data.Columns.Add("SO");
        // data.Columns.Add("Name");
        // data.Columns.Add("Surname");
        // data.Columns.Add("Email");
        // data.Columns.Add("Phone");
        // data.Columns.Add("Status");
        // data.Columns.Add("Date");
        // data.Columns.Add("Time");
        // data.Columns.Add("Duration");
        // data.Columns.Add("ProcessId");
        // data.Columns.Add("ProcessName");
        // data.Columns.Add("ProcessDescription");
        // data.Columns.Add("ProcessStatus");
        // data.Columns.Add("ProcessDate");
        // data.Columns.Add("ProcessTime");
        // data.Columns.Add("ProcessDuration");
        // data.Columns.Add("ProcessStatus");
        // data.Columns.Add("ProcessDate");
        // data.Columns.Add("ProcessTime");
        // data.Columns.Add("ProcessDuration");
        
        return data;
    }
}