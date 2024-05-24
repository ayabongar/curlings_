using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Sars.Systems.Data;
using Sars.Systems.Extensions;
using Sars.Systems.Security;

public partial class Reports_IMSReports : IncidentTrackingPage
{
    protected string Heading;

    private DataTable Export
    {
        get { return ViewState["Export"] as DataTable; }
        set { ViewState["Export"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(ProcessID))
        {
            Response.Redirect("~/InvalidProcessOrIncident.aspx");
            return;
        }


        var process = CurrentProcess;
        switch (Request["stsId"])
        {
            case "2":
                {
                    Heading = string.Format("{0} - Reports : Assigned Incidents", process.Description);
                    break;
                }
            case "3":
                {
                    Heading = string.Format("{0} - Reports : Work In Progress Incidents", process.Description);
                    break;
                }
            case "4":
                {
                    Heading = string.Format("{0} - Reports : Completed Incidents", process.Description);
                    break;
                }
            case "5":
                {
                    Heading = string.Format("{0} - Reports : Closed Incidents", process.Description);
                    break;
                }
            case "6":
                {
                    Heading = string.Format("{0} - Reports : Incidents Data Extract", process.Description);
                    break;
                }

            default:
                {
                    Heading = string.Format("{0} - Reports : All Incidents", process.Description);
                    break;
                }
        }

        if (IsPostBack) return;
        if (!String.IsNullOrEmpty(Request["stsId"]))
        {
            //  var data = IncidentTrackingManager.GetProcessReportBystatus(this.ProcessID, Request["stsId"]);
            // gvReports.Bind(data);
            // Export = data.Tables[0];
            // if (!data.HasRows)
            // {
            //    btnExport.Visible = false;
            // }
            // else
            // {
            //     btnExport.Visible = true;
            //  }
        }
    }

    protected void gvReports_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (!String.IsNullOrEmpty(Request["stsId"]))
        {
            var data = Export;
            gvReports.NextPage(data, e.NewPageIndex);
        }

    }

    protected void ExportData(object o, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request["stsId"]))
        {
            DateTime testDate;
            if (!DateTime.TryParse(txtDate.Text, out testDate))
            {
                MessageBox.Show("Start Date formart is invalid, mut be (yyyy-MM-dd).");
                return;
            }
            if (!DateTime.TryParse(txtEndDate.Text, out testDate))
            {
                MessageBox.Show("End Date formart is invalid, mut be (yyyy-MM-dd.");
                return;
            }
            var data = IncidentTrackingManager.GetProcessReportBystatus(this.ProcessID, Request["stsId"]);
            DataTable tblFiltered = new DataTable();
            if (!string.IsNullOrEmpty(txtDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                var userRole = this.Page.User.GetRole();
                var secret = new Guid(ConfigurationManager.AppSettings["AdministratorManagerSecret"]);
                var topSecret = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                var systemUser = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
                var tblRow = new DataTable().AsEnumerable();
                switch (userRole)
                {
                    case "Administrator Head - Top Secret":
                        var roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                    .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                     && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text));
                        break;
                    case "Administrator Manager - Secret":
                        // no access to HeadTopScret
                        roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                    .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                     && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text) && row.Field<Guid>("RoleId") != roleId);
                        break;
                    case "Administrator - confidential":
                        roleId = new Guid(ConfigurationManager.AppSettings["AdministratorConfidential"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                    .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                     && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text)
                                       && (row.Field<Guid>("RoleId") != topSecret && row.Field<Guid>("RoleId") != secret));
                        break;
                    case "System User":
                        roleId = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                  .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                   && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text)
                                     && (row.Field<Guid>("RoleId") == roleId));
                        break;
                    case "Developer":
                        tblRow = data.Tables[0].AsEnumerable()
                                       .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                        && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text));
                        break;
                    default:
                        break;
                }

                if (tblRow.Any())
                {
                    tblFiltered = tblRow.CopyToDataTable();
                }
                gvReports.Bind(tblFiltered);
                Export = tblFiltered;               
                tblFiltered.ToExcel(null, null);
            }
        }
    }

    [WebMethod]
    public static string GetData(string processId)
    {
        var xys = new List<XY>();
        var jSearializer = new JavaScriptSerializer();
        using (
            var data =
                new RecordSet(
                    "SELECT     [Incident Status] as [IS], NumberOfIncidents FROM   vw_NumberOfIncidentsPerstatusPerProcess WHERE     (ProcessId = @ProcessId)",
                    QueryType.TransectSQL, new DBParamCollection { { "@ProcessId", processId } }, db.ConnectionString))
        {
            if (data.HasRows)
            {
                xys.AddRange(from DataRow row in data.Tables[0].Rows
                             select new XY
                             {
                                 label = row["IS"].ToString(),
                                 value = row["NumberOfIncidents"].ToString()
                             });
            }
        }
        var json = jSearializer.Serialize(xys);
        return json;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request["stsId"]))
        {
            DateTime testDate;
            if (!DateTime.TryParse(txtDate.Text, out testDate))
            {
                MessageBox.Show("Start Date formart is invalid, mut be (yyyy-MM-dd).");
                return;
            }
            if (!DateTime.TryParse(txtEndDate.Text, out testDate))
            {
                MessageBox.Show("End Date formart is invalid, mut be (yyyy-MM-dd.");
                return;
            }
            var data = IncidentTrackingManager.GetProcessReportBystatus(this.ProcessID, Request["stsId"]);
            DataTable tblFiltered = new DataTable();
            if (!string.IsNullOrEmpty(txtDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                var userRole = this.Page.User.GetRole();
                var secret = new Guid(ConfigurationManager.AppSettings["AdministratorManagerSecret"]);
                var topSecret = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                var systemUser = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
                var tblRow = new DataTable().AsEnumerable();
                switch (userRole)
                {
                    case "Administrator Head - Top Secret":
                        var roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                         tblRow = data.Tables[0].AsEnumerable()
                                     .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                      && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text));
                        break;
                    case "Administrator Manager - Secret":
                        // no access to HeadTopScret
                        roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                    .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                     && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text) && row.Field<Guid>("RoleId") != roleId);
                        break;
                    case "Administrator - confidential":
                        roleId = new Guid(ConfigurationManager.AppSettings["AdministratorConfidential"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                    .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                     && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text) 
                                       && (row.Field<Guid>("RoleId") != topSecret && row.Field<Guid>("RoleId") != secret));
                        break;
                    case "System User":
                        roleId = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                  .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                   && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text)
                                     && (row.Field<Guid>("RoleId") == roleId));
                        break;
                    case "Developer":                       
                       tblRow = data.Tables[0].AsEnumerable()
                                      .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                       && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text));
                        break;
                    default:
                        break;
                }

                if (tblRow.Any())
                {
                    tblFiltered = tblRow.CopyToDataTable();
                }
                gvReports.Bind(tblFiltered);
                Export = tblFiltered;
            }
        }
    }
}

public class XY
{
    [XmlElement("label")]
    public string label { get; set; }

    [XmlElement("value")]
    public string value { get; set; }
}