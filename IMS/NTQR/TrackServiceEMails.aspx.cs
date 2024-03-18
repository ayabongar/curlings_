using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;
using System.Text;
using Sars.Systems.Extensions;
using System.IO;

public partial class NTQR_TrackServiceEMails : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadReport();
        }
    }

    private void LoadReport()
    {

        try
        {
            string path = "/IMS/NTQR_SentMails";
            ReportViewer1.ServerReport.ReportServerUrl =
                       new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            ReportViewer1.ServerReport.ReportPath = path;
            ReportViewer1.ServerReport.Refresh();
            ReportViewer1.Visible = true;         
            ReportViewer1.ServerReport.Refresh();
            ReportViewer1.Visible = true;
        }
        catch (Exception)
        {

            throw;
        }
    }
}