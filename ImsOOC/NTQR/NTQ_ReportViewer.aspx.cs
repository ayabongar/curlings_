using Microsoft.Reporting.WebForms;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NTQR_NTQ_ReportViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadReport(Request["Id"],Request["q"] ,Request["q"]);
        }        
    }
    private void LoadReport(string id, string quarter,string reportName)
    {

        try
        {
            ReportViewer1.ServerReport.ReportServerUrl =
                       new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            Warning warnings = null;
            string streamids = null;
            string mimeType = null;
            string encoding = null;
            string extension = null;          
            switch (id)
            {
                case "1":
                     string path = "/IMS/NTQR_SubmissionNotice";
                     ReportViewer1.ServerReport.ReportPath = path;
                    ReportViewer1.ServerReport.DisplayName = "Submission Notification Report";
                    lblHeader.InnerText = "National Treasury Quarterly Report submission notification";
                    break;
                case "3":
                    path = "/IMS/NTQR_KRO_Reminder";
                    ReportViewer1.ServerReport.ReportPath = path;
                    ReportParameter[] reportParam = new ReportParameter[2];
                    reportParam[0] = new ReportParameter("StatusId", "10");
                    reportParam[1] = new ReportParameter("QuarterId", quarter);

                    ReportViewer1.ServerReport.SetParameters(reportParam);
                    ReportViewer1.ServerReport.DisplayName= "KRO Approval Reminder Report";
                   var dc = ReportViewer1.ServerReport.GetDataSources();
                    
                    lblHeader.InnerText = "National Treasury Quarterly Report: KRO approval reminder";
                    break;
                case "4":
                    path = "/IMS/NTQR_Anchor_Reminder";
                    ReportViewer1.ServerReport.ReportPath = path;
                    reportParam = new ReportParameter[2];
                    reportParam[0] = new ReportParameter("StatusId", "11");                    
                    reportParam[1] = new ReportParameter("QuarterId", quarter);                    
                    ReportViewer1.ServerReport.SetParameters(reportParam);
                    ReportViewer1.ServerReport.DisplayName = "Anchor Approval Reminder Report";
                    lblHeader.InnerText = "National Treasury Quarterly Report: Anchor approval reminder";
                    break;
                case "5":
                    path = "/IMS/NTQR_FinalDeclarationNotice";                   
                    ReportViewer1.ServerReport.ReportPath = path;
                     reportParam = new ReportParameter[1];
                    reportParam[0] = new ReportParameter("QuarterId", quarter);
                    //reportParam[1] = new ReportParameter("End", _DateTo);
                    ReportViewer1.ServerReport.SetParameters(reportParam);
                    ReportViewer1.ServerReport.DisplayName = "Final submission reminder Report";
                    lblHeader.InnerText = "National Treasury Quarterly Report: Final submission reminder";
                    break;
                default:
                    break;
            }           
            ReportViewer1.ServerReport.Refresh();
            ReportViewer1.Visible = true;
        }
        catch (Exception)
        {

           // throw;
        }
    }
}