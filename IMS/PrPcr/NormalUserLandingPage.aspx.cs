using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrPcr_NormalUserLandingPage : IncidentTrackingPage
{
    protected IncidentProcess CurrentProc;
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentProc = CurrentProcess;
        if (CurrentProc == null)
        {
            Response.Redirect("../InvalidProcessOrIncident.aspx");
            return;
        }
        if (!IsPostBack)
        {
            Toolbar1.Items[0].Text = (CurrentProc.Description.Contains("External")) ? "New External Registration" : "New Internal Registration";
            var incidents = IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID, 0);
            if (incidents != null && incidents.Any())
            {
                gvIncidents.Bind(incidents.FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID)));
            }
            else
            {
                gvIncidents.Bind(null);
            }
            //ddlStatuses.Bind(IncidentTrackingManager.GetIncidentsStatuses(), "Description", "IncidentStatusId");
        }
        if (CurrentProc != null)
        {
            var procAdmins = CurrentProc.Administrators;
            if (procAdmins != null && procAdmins.Any())
            {
                if (procAdmins.Find(admin => admin.SID.Equals(SarsUser.SID, StringComparison.CurrentCultureIgnoreCase)) != null)
                {
                    Toolbar1.Items[1].Visible = true;
                    Toolbar1.Items[2].Visible = true;
                }
                else
                {
                    Toolbar1.Items[1].Visible = false;
                    Toolbar1.Items[2].Visible = false;
                }
            }
        }
    }

  
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "AddNewIncident":
                {

                    string incidentNumber;
                    string incidentId;
                    string type = CurrentProc.Description.Contains("Internal") ? "Internal" : "External";
                    var recordsAffected = IncidentTrackingManager.InitOocIncident(ProcessID,type, out incidentId, out incidentNumber);
                    if (recordsAffected > 0)
                    {
                        CurrentProc = CurrentProcess;
                        if (!CurrentProc.Description.Contains("External"))
                        {
                            Response.Redirect(String.Format("~/Prpcr/RegisterUserIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
                        }
                        else
                        {
                            Response.Redirect(String.Format("~/Prpcr/RegisterExternalIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
                        }
                    }
                    break;
                }
            case "Search":
                {
                    var returnUrl = Request.Url.PathAndQuery;
                    Response.Redirect(string.Format("~/Prpcr/SearchHome.aspx?procId={0}&returnUrl={1}", ProcessID, returnUrl));
                    break;
                }
            case "Reports":
                {
                    Response.Redirect(String.Format("~/Reports/Default.aspx?procId={0}", ProcessID));
                    break;
                }
            case "Back":
                {
                    Response.Redirect(String.Format("~/Prpcr/selectnormaluserprocess.aspx?procId={0}", ProcessID));
                    break;
                }
            case "ViewIncident":
                {
                    if (gvIncidents.SelectedIndex == -1)
                    {
                        if (gvIncidents.Rows.Count == 1)
                        {
                            gvIncidents.SelectedIndex = 0;
                        }
                        else
                        {
                            MessageBox.Show("Please click on an incident to select it before you can continue.");
                            return;
                        }
                    }
                    if (gvIncidents.SelectedDataKey != null)
                    {
                        var prodessId = gvIncidents.SelectedDataKey["ProcessId"].ToString();
                        var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                        if (!string.IsNullOrEmpty(prodessId) && !string.IsNullOrEmpty(incidentId))
                        {
                            CurrentProc = CurrentProcess;
                            if (!CurrentProc.Description.Contains("External"))
                            {
                                Response.Redirect(String.Format("~/Prpcr/RegisterUserIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
                            }
                            else
                            {
                                Response.Redirect(String.Format("~/Prpcr/RegisterExternalIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not create new incident.");
                    }
                    break;
                }
        }
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
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
        var tdReAssign = e.Row.FindControl("tdReAssign") as System.Web.UI.HtmlControls.HtmlTableCell;
        var tdComplete = e.Row.FindControl("tdComplete") as System.Web.UI.HtmlControls.HtmlTableCell;
        var tdClose = e.Row.FindControl("tdClose") as System.Web.UI.HtmlControls.HtmlTableCell;
        var tdReOpen = e.Row.FindControl("tdReOpen") as System.Web.UI.HtmlControls.HtmlTableCell;
        if (tdReAssign != null)
        {
            var incidentStatusId = DataBinder.Eval(e.Row.DataItem, "IncidentStatusId");
            if (incidentStatusId.Equals(4) || incidentStatusId.Equals(5))
            {
                tdReAssign.Visible = false;
            }
            else
            {
                tdReAssign.Visible = true;
            }
        }
        if (tdComplete != null)
        {
            var incidentStatusId = DataBinder.Eval(e.Row.DataItem, "IncidentStatusId");
            if (incidentStatusId.Equals(4) || incidentStatusId.Equals(5))
            {
                tdComplete.Visible = false;
            }
            else
            {
                tdComplete.Visible = true;
            }
        }
        if (tdClose != null)
        {
            var incidentStatusId = DataBinder.Eval(e.Row.DataItem, "IncidentStatusId");
            tdClose.Visible = incidentStatusId.Equals(4);
        }
        if (tdReOpen != null)
        {
            var incidentStatusId = DataBinder.Eval(e.Row.DataItem, "IncidentStatusId");
            tdReOpen.Visible = incidentStatusId.Equals(5);
        }
    }
    protected void PageChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncidents.PageIndex = e.NewPageIndex;

        var incidents = IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID, 0);
        if (incidents != null && incidents.Any())
        {
            gvIncidents.Bind(incidents.FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID)));
        }
        else
        {
            gvIncidents.Bind(null);
        }
    }
    protected void ChangeAssignee(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvIncidents.SelectRow(row.RowIndex);

                if (gvIncidents.SelectedDataKey != null)
                {
                    var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                    Response.Redirect(String.Format("~/Prpcr/ChangeAssignee.aspx?ProcId={0}&IncId={1}", ProcessID, incidentId));
                }
            }
        }
    }
    protected void CompleteIncident(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvIncidents.SelectRow(row.RowIndex);

                if (gvIncidents.SelectedDataKey != null)
                {
                    var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                    Response.Redirect(String.Format("~/Prpcr/CompleteIncident.aspx?ProcId={0}&IncId={1}", ProcessID, incidentId));
                }
            }
        }
    }
    protected void CloseIncident(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var row = btn.Parent.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvIncidents.SelectRow(row.RowIndex);

                if (gvIncidents.SelectedDataKey != null)
                {
                    var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                    Response.Redirect(String.Format("~/Prpcr/CloseIncident.aspx?ProcId={0}&IncId={1}", ProcessID, incidentId));
                }
            }
        }
    }
    protected void OpenIncident(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvIncidents.SelectRow(row.RowIndex);

                if (gvIncidents.SelectedDataKey != null)
                {
                    var prodessId = gvIncidents.SelectedDataKey["ProcessId"].ToString();
                    var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                    if (!string.IsNullOrEmpty(prodessId) && !string.IsNullOrEmpty(incidentId))
                    {
                        CurrentProc = CurrentProcess;
                        if (!CurrentProc.Description.Contains("External"))
                        {
                            Response.Redirect(String.Format("~/Prpcr/RegisterUserIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
                        }
                        else
                        {
                            Response.Redirect(String.Format("~/Prpcr/RegisterExternalIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
                        }
                    }
                }
            }
        }
    }
    protected void ReOpenIncident(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var row = btn.Parent.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvIncidents.SelectRow(row.RowIndex);

                if (gvIncidents.SelectedDataKey != null)
                {
                    var prodessId = gvIncidents.SelectedDataKey["ProcessId"].ToString();
                    var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                    if (!string.IsNullOrEmpty(prodessId) && !string.IsNullOrEmpty(incidentId))
                    {
                        Response.Redirect(String.Format("~/Prpcr/ReOpenIncident.aspx?procId={0}&incId={1}", prodessId, incidentId));
                    }
                }
            }
        }
    }
    protected void gvIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string pageName;
        var incidentId = e.CommandArgument.ToString();
        switch (e.CommandName)
        {

            case "View_Incident":
                {
                    CurrentProc = CurrentProcess;
                    if (!CurrentProc.Description.Contains("External"))
                    {
                        pageName = "RegisterUserIncident"; 
                    }
                    else
                    {
                        pageName = "RegisterExternalIncident";
                    }
                    break;
                }
            case "Reassigne_Incident":
                {
                    pageName = "ChangeAssignee";
                    break;
                }
            case "Complete_Incident":
                {
                    pageName = "CompleteIncident";

                    break;
                }
            case "Close_Incident":
                {
                    pageName = "CloseIncident";
                    break;
                }
            case "Reopen_Incident":
                {
                    pageName = "ReOpenIncident";
                    break;
                }
            case "Copy_Incident":
                {
                    if (!CurrentProc.Description.Contains("External"))
                    {
                        pageName = "RegisterUserIncident";
                    }
                    else
                    {
                        pageName = "RegisterExternalIncident";
                    }
                    CopyIncident(incidentId, pageName);
                    break;
                }
            default:
                {
                    MessageBox.Show("Cant execute option selected.");
                    return;
                }

        }

        Response.Redirect(String.Format("~/Prpcr/{0}.aspx?ProcId={1}&IncId={2}", pageName, ProcessID, incidentId));
    }
    private void CopyIncident(string incidentId, string navigateTo)
    {
        var createdBy = SarsUser.SID;
        var incidnetToCopy = incidentId;
        string incidentNumber;
        string newIncidentId;


        IncidentTrackingManager.CopyIncident(incidnetToCopy, createdBy, ProcessID, out incidentNumber, out newIncidentId);

        if (!string.IsNullOrEmpty(incidentNumber) && !string.IsNullOrEmpty(newIncidentId))
        {
            Response.Redirect(String.Format("~/Prpcr/{0}.aspx?ProcId={1}&IncId={2}&cpd=1&oldIncId={3}", navigateTo, ProcessID, newIncidentId, incidnetToCopy));
        }
    }
}