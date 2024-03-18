<%@ WebHandler Language="C#" Class="pdfHandler" %>

using System;
using System.Web;
using Microsoft.Reporting.WebForms;

public class pdfHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        

        if (context.Request["type"] != "ooc")
        {
            string path = "/IMS/rptCoverPage";
            if (context.Request["procId"] == System.Configuration.ConfigurationManager.AppSettings["BAITProcess"])
            {
                path = "/IMS/BAITCoverPage";
            }
            ReportViewer ReportViewer1 = new ReportViewer();

            ReportViewer1.ServerReport.ReportServerUrl =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            ReportViewer1.ServerReport.ReportPath = path;
            ReportParameter[] reportParam = new ReportParameter[3];
            reportParam[0] = new ReportParameter("ProcessId", context.Request["procId"]);
            reportParam[1] = new ReportParameter("IncidentId", context.Request["incId"]);
            reportParam[2] = new ReportParameter("Color", context.Request["Color"]);
            ReportViewer1.ServerReport.SetParameters(reportParam);
            ReportViewer1.ServerReport.Refresh();
            byte[] pdfBytes = saveRptAs(ReportViewer1, "pdf");
                //This is where you should be calling the appropriate APIs to get the PDF as a stream of bytes
            var response = context.Response;
            response.ClearContent();
            response.ContentType = "application/pdf";
            response.AddHeader("Content-Disposition", "inline");
            response.AddHeader("Content-Length", pdfBytes.Length.ToString());
            response.BinaryWrite(pdfBytes);
            response.End();
        }
        else
        {
            string path = "/IMS/OocMainCoverPage";
            var reportOne = "/IMS/OocCoverPageOne";
            var reportTwo = "/IMS/OocCoverPageTwo";
            var reportThree = "/IMS/OocCoverPageThree";
            switch (context.Request["pages"])
            {
                case "1"  :
                    path = reportOne;
                    break;
                case "2":
                    path = reportTwo;
                    break;
                case "3":
                    path = reportThree;
                    break;
                case "0":
                    path = path;
                    break; 
            }
           
            ReportViewer ReportViewer1 = new ReportViewer();

            ReportViewer1.ServerReport.ReportServerUrl =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            ReportViewer1.ServerReport.ReportPath = path;
            ReportParameter[] reportParam = new ReportParameter[3];
            reportParam[0] = new ReportParameter("ProcessId", context.Request["procId"]);
            reportParam[1] = new ReportParameter("IncidentId", context.Request["incId"]);
            reportParam[2] = new ReportParameter("Color", context.Request["Color"]);
            ReportViewer1.ServerReport.SetParameters(reportParam);
            ReportViewer1.ServerReport.Refresh();

            byte[] pdfBytes = saveRptAs(ReportViewer1, "pdf");
            //This is where you should be calling the appropriate APIs to get the PDF as a stream of bytes
            var response = context.Response;
            response.ClearContent();
            response.ContentType = "application/pdf";
            response.AddHeader("Content-Disposition", "inline");
            response.AddHeader("Content-Length", pdfBytes.Length.ToString());
            response.BinaryWrite(pdfBytes);
            response.End();
        }
    }


    private byte[] saveRptAs(ReportViewer ReportViewer1, String s_rptType)
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
        return bytes;
    }

    public bool IsReusable {
        get {
            return false;
        }
    }
}