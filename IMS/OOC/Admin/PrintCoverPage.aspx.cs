using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class Admin_PrintCoverPage : IncidentTrackingPage
{  
    protected Incident CurrentIncidentDetails;
    protected IncidentProcess CurrentProcessDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             CurrentIncidentDetails = CurrentIncident;
        CurrentProcessDetails = CurrentProcess;
            var procId = Request["procId"];
            var incId = Request["incId"];
            var color = Request["Color"];
            var type = Request["type"];
            var pages = Request["pages"];
            var message = Request["message"];
            //var url = "pdfHandler.ashx?procId=" + procId + "&incId=" + incId + "&Color=" + color + "&type=" + type + "&pages=" + pages;
            // emdPDF.Attributes.Add("src", url);



            if (type != "ooc")
            {
                string path = "/IMS/rptCoverPage";
                if (procId == System.Configuration.ConfigurationManager.AppSettings["BAITProcess"])
                {
                    path = "/IMS/BAITCoverPage";
                }
                //ReportViewer ReportViewer1 = new ReportViewer();

                ReportViewer1.ServerReport.ReportServerUrl =
                    new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
                ReportViewer1.ServerReport.ReportPath = path;
                ReportParameter[] reportParam = new ReportParameter[3];
                reportParam[0] = new ReportParameter("ProcessId", Request["procId"]);
                reportParam[1] = new ReportParameter("IncidentId", Request["incId"]);
                reportParam[2] = new ReportParameter("Color", Request["Color"]);
                ReportViewer1.ServerReport.SetParameters(reportParam);
                ReportViewer1.ServerReport.Refresh();
                // SaveRptAs(ReportViewer1, "pdf");
                //This is where you should be calling the appropriate APIs to get the PDF as a stream of bytes
                
            }
            else
            {
                string path = "/IMS/OocMainCoverPage";
                var reportOne = "/IMS/OocCoverPageOne";
                var reportTwo = "/IMS/OocCoverPageTwo";
                var reportThree = "/IMS/OocCoverPageThree";
                var reportFour = "/IMS/OocCoverPageFour";
                switch (Request["pages"])
                {
                    case "1":
                        path = reportOne;
                        break;
                    case "2":
                        path = reportTwo;
                        break;
                    case "3":
                        path = reportThree;
                        break;
                    case "4":
                        path = reportFour;
                        break;
                    case "0":
                        path = path;
                        break;
                }

                //ReportViewer ReportViewer1 = new ReportViewer();

                ReportViewer1.ServerReport.ReportServerUrl =
                    new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
                ReportViewer1.ServerReport.ReportPath = path;
                ReportParameter[] reportParam = new ReportParameter[4];
                reportParam[0] = new ReportParameter("ProcessId", Request["procId"]);
                reportParam[1] = new ReportParameter("IncidentId", Request["incId"]);
                reportParam[2] = new ReportParameter("Color", Request["Color"]);
                reportParam[3] = new ReportParameter("Message", Request["message"]);
                ReportViewer1.ServerReport.SetParameters(reportParam);
                ReportViewer1.ServerReport.Refresh();

               // SaveRptAs(ReportViewer1, "pdf");
                //This is where you should be calling the appropriate APIs to get the PDF as a stream of bytes
             
            }
        }
    }
    //private void SaveRptAs(ReportViewer ReportViewer1, String s_rptType)
    //{
    //    Warning[] warnings;
    //    string[] streamids;
    //    string mimeType;
    //    string encoding;
    //    string extension;
    //    // string deviceInfo;
    //    byte[] bytes = ReportViewer1.ServerReport.Render(
    //    s_rptType, null, out mimeType, out encoding, out extension,
    //   out streamids, out warnings);
    //   // var response = context.Response;
    //    Response.ClearContent();
    //    Response.ContentType = "application/pdf";
    //    Response.AddHeader("Content-Disposition", "attachment");
    //    Response.AddHeader("Content-Length", bytes.Length.ToString());
    //    Response.BinaryWrite(bytes);
    //   // Response.WriteFile(bytes); 
    //    Response.End();
    //    //return bytes;
    //}

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Print":
            {
                Response.Redirect("GenerateCoverPage.aspx?procId=" + Request["procId"] + "&incId=" + Request["incId"]);
            }
                break;
        }
    }
}