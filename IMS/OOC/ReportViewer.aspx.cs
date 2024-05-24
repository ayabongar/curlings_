using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
        

            var reportName = Request["reportName"].ToString();
            string path = @"/IMS/"+ reportName;
            lblReport.Text = Request["header"];
            ReportViewer1.ServerReport.ReportServerUrl =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            ReportViewer1.ServerReport.ReportPath = path;
            //DateTime date = DateTime.Today;
            //var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            //var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            //_DateFrom = firstDayOfMonth.ToString("dd-MMM-yyyy");
            //_DateTo = lastDayOfMonth.ToString("dd-MMM-yyyy");
            //ReportParameter[] reportParam = new ReportParameter[3];
            //reportParam[0] = new ReportParameter("SID", SarsUser.SID);
            //reportParam[1] = new ReportParameter("startDate", firstDayOfMonth.ToString());
            //reportParam[2] = new ReportParameter("Date", lastDayOfMonth.ToString());

           // ReportViewer1.ServerReport.SetParameters(reportParam);
            ReportViewer1.ServerReport.Refresh();
            ReportViewer1.Visible = true;
        }
    }

}