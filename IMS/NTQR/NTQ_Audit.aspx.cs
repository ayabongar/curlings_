using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NTQR_NTQ_Audit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDashBoard();
        }
    }

    private void LoadDashBoard()
    {

        try
        {


            const string path = "/IMS/NTQR_Audit";

            ReportViewer1.ServerReport.ReportServerUrl =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            ReportViewer1.ServerReport.ReportPath = path;
         
            //ReportParameter[] reportParam = new ReportParameter[2];
            //reportParam[0] = new ReportParameter("start", _DateFrom);
            //reportParam[1] = new ReportParameter("End", _DateTo);


            //ReportViewer1.ServerReport.SetParameters(reportParam);
            ReportViewer1.ServerReport.Refresh();
            ReportViewer1.Visible = true;
        }
        catch (Exception)
        {

            throw;
        }
    }
}