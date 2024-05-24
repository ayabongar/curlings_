using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Microsoft.Reporting.WebForms;
using Sars.Systems.Data;
using Sars.Systems.Extensions;

public partial class Reports_Default : IncidentTrackingPage
{
    protected string Heading;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDashBoard();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(ProcessID))
        {
            Response.Redirect("~/InvalidProcessOrIncident.aspx");
            return;
        }

        var process = CurrentProcess;
        Heading = string.Format("{0} - DASHBOARD", process.Description);
    }

    private void LoadDashBoard()
    {
        const string path = "/IMS/Dashboard";

        ReportViewer1.ServerReport.ReportServerUrl =
            new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
        ReportViewer1.ServerReport.ReportPath = path;

        var processId = new ReportParameter("ProcessId", ProcessID);

        var rptParams = new ReportParameter[1];

        rptParams[0] = processId;

        ReportViewer1.ServerReport.SetParameters(rptParams);
        ReportViewer1.ServerReport.Refresh();
        ReportViewer1.Visible = true;
    }
}

