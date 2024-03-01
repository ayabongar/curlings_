using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;

public partial class Admin_AddCoverPage : IncidentTrackingPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProcesses();
        }
    }
    private void LoadProcesses()
    {
        var processes =
            new IncidentProcess("[dbo].[uspRead_UserProcesses]",
                                new Dictionary<string, object>
                                    {
                                        {"@UserSID", SarsUser.SID}
                                    }).GetRecords<IncidentProcess>();



        if (processes != null && processes.Any())
        {
            if (processes.Count > 1)
            {
                var incidents = IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID, 0);
                if (incidents != null && incidents.Any())
                {
                    gvIncidents.Bind(incidents.FindAll(inc => inc.IncidentStatusId == 2 || inc.IncidentStatusId == 3));
                }
               // gvProcesses.Bind(processes);
                return;
            }
            if (processes.Count == 1)
            {
                var proc = processes[0];
                Response.Redirect(string.Format("NormalUserLandingPage.aspx?procId={0}", proc.ProcessId));
            }
            Response.Redirect("~/NormalUserDefault.aspx");
        }
        else
        {
            Response.Redirect("~/NormalUserDefault.aspx");
        }
    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //gvProcesses.PageIndex = e.NewPageIndex;
        LoadProcesses();
    }
    protected void IncidentPageChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncidents.PageIndex = e.NewPageIndex;
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        e.Row.Attributes.Add("onclick",
                             Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        var description = DataBinder.Eval(e.Row.DataItem, "Description").ToString();

        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        e.Row.Attributes.Add("title",
                             "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Process Description (Click to open this system)</font></b>] body=[<font color='red'><b>" +
                             description + "</b></font>]");
    }

    protected void IncidentRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }


        var summary = DataBinder.Eval(e.Row.DataItem, "Summary");
        if (summary != null)
        {

            e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
            e.Row.Attributes.Add("title",
                                 "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Incident Summary</font></b>] body=[<font color='red'><b>" +
                                 summary + "</b></font>]");
        }


        //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        var dueDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "DueDate"));
        var now = DateTime.Now;
        if ((dueDate.Year == now.Year && dueDate.Month == now.Month && dueDate.Day == now.Day) && dueDate.Ticks < now.Ticks)
        {
            e.Row.Attributes["class"] = "slaabouttobeviolated";
        }
        else if (dueDate < now)
        {
            e.Row.Attributes["class"] = "slaviolated";
        }
        else
        {
            e.Row.Attributes["class"] = "slakept";
        }

    }

   
  
 
    protected void gvIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var btn = e.CommandSource as Button;

        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null) gvIncidents.SelectedIndex = row.RowIndex;
        }
        if (e.CommandName.Equals("btnCoverPage"))
        {
            if (gvIncidents.SelectedDataKey != null)
                Response.Redirect(string.Format("/{2}/Admin/CoverPage.aspx?incid={0}&procId={1}", e.CommandArgument, gvIncidents.SelectedDataKey.Values[2], SARSDataSettings.Settings.ApplicationName));
        }
        if (e.CommandName.Equals("btnAcknowledgement"))
        {
            Response.Redirect(string.Format("/{2}/Admin/Acknowledgement.aspx?incid={0}&procId={1}", e.CommandArgument, gvIncidents.SelectedDataKey.Values[2], SARSDataSettings.Settings.ApplicationName));
        }
    }
}