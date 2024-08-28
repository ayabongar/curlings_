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

            var validNames = GetValidNames(processId);

            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("ProcessId", processId);
            parameters[1] = new ReportParameter("ValidNames", string.Join(",", validNames));

            // ReportViewer1.LocalReport.SetParameters(parameters);
            // parameters[0] = new ReportParameter("ProcessId", processId);



            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "IMSDataSet";
            rds.Value = validNames; // To be binding correcct / VAlid names
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

    private IEnumerable<string> GetValidNames(string processId)
    {
        // retrieve and filter valid names based on processId

        return filteredNames;
    }
}