using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrOoc_NormalUserLandingPage : IncidentTrackingPage
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
            Toolbar1.Items[0].Text = (CurrentProc.Description.Contains("External")) ? "New External Registration" :(CurrentProc.Description.Contains("Internal")?  "New Internal Registration": "New Tax Escalation Registration");
            //var incidents = IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID, 0);
            //var incidents = IncidentTrackingManager.GetOocProcessIncidents(ProcessID);
            //if (incidents != null)
            //{
            //    gvIncidents.Bind(incidents.FindAll(inc => inc.IncidentStatusId != 6).OrderByDescending(inc => inc.Timestamp).ToList());
            //}
            //else
            //{
            //    gvIncidents.Bind(null);
            //}
            
            var incidents = IncidentTrackingManager.GetOocProcessIncidents(ProcessID);
            if (incidents != null && incidents.Any())
            {
                gvIncidents.Bind(incidents.FindAll(inc => inc.AssignedToSID != null).FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId > 1 && inc.AssignedToSID.ToLower() == SarsUser.SID.ToLower() && inc.IncidentStatusId < 6));
            }
            else
            {
                gvIncidents.Bind(null);
            }
            if (CurrentProc.CanWorkOnOneCase)
            {
                tabTeamIncidents.Visible = true;
                var teamincidents = IncidentTrackingManager.GetOocProcessIncidents(ProcessID);
                if (teamincidents != null && teamincidents.Any())
                {
                    grdTeamIncidents.Bind(teamincidents.FindAll(inc => inc.AssignedToSID!=null).FindAll(inc => inc.IncidentStatusId > 1 && inc.AssignedToSID.ToLower() != SarsUser.SID.ToLower() && inc.IncidentStatusId < 6));
                }
                else
                {
                    grdTeamIncidents.Bind(null);
                }
            }
        }
        //if (CurrentProc != null)
        //{
        //    var procAdmins = CurrentProc.Administrators;
        //    if (procAdmins != null && procAdmins.Any())
        //    {
        //        if (procAdmins.Find(admin => admin.SID.Equals(SarsUser.SID, StringComparison.CurrentCultureIgnoreCase)) != null)
        //        {
        //            Toolbar1.Items[1].Visible = true;
        //            Toolbar1.Items[2].Visible = true;
        //        }
        //        else
        //        {
        //            Toolbar1.Items[1].Visible = false;
        //            Toolbar1.Items[2].Visible = false;
        //        }
        //    }
        //}
    }

  
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "AddNewIncident":
                {

                   // string incidentNumber;
                  //  string incidentId;
                    //string type = CurrentProc.Description.Contains("Internal") ? "Internal" : "External";
                    //CurrentProc = CurrentProcess;
                    if (CurrentProc.Description.Contains("Internal"))
                    {
                        Response.Redirect(String.Format("~/PrOoc/RegisterUserIncident.aspx?procId={0}&incId={1}&type={2}", ProcessID, 0,"Internal"));
                    }
                    else if (CurrentProc.Description.Contains("External"))
                    {
                        Response.Redirect(String.Format("~/PrOoc/RegisterExternalIncident.aspx?procId={0}&incId={1}&type={2}", ProcessID, 0, "External"));
                    }
                    else if (CurrentProc.Description.Contains("Escalation"))
                    {
                        Response.Redirect(String.Format("~/PrOoc/RegisterTaxEscalations.aspx?procId={0}&incId={1}&type={2}", ProcessID, 0, "Escalation"));
                    }                      
                        break;
                }
            case "Search":
                {
                    var returnUrl = Request.Url.PathAndQuery;
                    Response.Redirect(string.Format("~/PrOoc/SearchHome.aspx?procId={0}&type={1}", ProcessID, Request["type"]));
                    break;
                }
            case "Reports":
                {
                    Response.Redirect(String.Format("~/Reports/Default.aspx?procId={0}&type={1}", ProcessID, Request["type"]));
                    break;
                }
            case "Back":
                {
                    Response.Redirect(String.Format("~/PrOoc/selectnormaluserprocess.aspx?procId={0}&type={1}", ProcessID, Request["type"]));
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
                           
                            if (CurrentProc.Description.Contains("Internal"))
                            {
                                Response.Redirect(String.Format("~/PrOoc/RegisterUserIncident.aspx?procId={0}&incId={1}&type={2}", ProcessID, incidentId, "Internal"));
                            }
                            else if (CurrentProc.Description.Contains("External"))
                            {
                                Response.Redirect(String.Format("~/PrOoc/RegisterExternalIncident.aspx?procId={0}&incId={1}&type={2}", ProcessID, incidentId, "External"));
                            }
                            else if (CurrentProc.Description.Contains("Escalation"))
                            {
                                Response.Redirect(String.Format("~/PrOoc/RegisterTaxEscalations.aspx?procId={0}&incId={1}&type={2}", ProcessID, incidentId, "Escalation"));
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
        var summary = DataBinder.Eval(e.Row.DataItem, "Subject");
        if (summary != null)
        {

            e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
            e.Row.Attributes.Add("title",
                                 "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Subject</font></b>] body=[<font color='red'><b>" +
                                 summary + "</b></font>]");
        }


        //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        var dueDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "DueDate"));
        var incidentStatusId = DataBinder.Eval(e.Row.DataItem, "IncidentStatusId");
        var now = DateTime.Now;
        e.Row.Attributes["class"] = "slakept";

        if (incidentStatusId.Equals(2) || incidentStatusId.Equals(3))
        {
            if ((dueDate.Year == now.Year && dueDate.Month == now.Month && dueDate.Day == now.Day) && dueDate.Ticks < now.Ticks)
            {
                e.Row.Attributes["class"] = "slaabouttobeviolated";
            }
            else if (dueDate < now)
            {
                e.Row.Attributes["class"] = "slaviolated";

            }
        }

        var tdReAssign = e.Row.FindControl("bntReAssign") as Button;
        var tdComplete = e.Row.FindControl("btnComplete") as Button;
        var tdClose = e.Row.FindControl("btnClose") as Button;
        var tdReOpen = e.Row.FindControl("btnReOpen") as Button;
        if (tdReAssign != null)
        {
            incidentStatusId = DataBinder.Eval(e.Row.DataItem, "IncidentStatusId");
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
            incidentStatusId = DataBinder.Eval(e.Row.DataItem, "IncidentStatusId");
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
             incidentStatusId = DataBinder.Eval(e.Row.DataItem, "IncidentStatusId");
            tdClose.Visible = incidentStatusId.Equals(4);
        }
        if (tdReOpen != null)
        {
            incidentStatusId = DataBinder.Eval(e.Row.DataItem, "IncidentStatusId");
            tdReOpen.Visible = incidentStatusId.Equals(5);
        }
    }
    protected void PageChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncidents.PageIndex = e.NewPageIndex;
        grdTeamIncidents.PageIndex = e.NewPageIndex;

        var incidents = IncidentTrackingManager.GetOocProcessIncidents(ProcessID);
        if (incidents != null && incidents.Any())
        {
            gvIncidents.Bind(incidents.FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId > 1 && inc.AssignedToSID == SarsUser.SID && inc.IncidentStatusId < 6));
        }
        else
        {
            gvIncidents.Bind(null);
        }
        if (CurrentProc.CanWorkOnOneCase)
        {
            tabTeamIncidents.Visible = true;
            var teamincidents = IncidentTrackingManager.GetOocProcessIncidents(ProcessID);
            if (teamincidents != null && teamincidents.Any())
            {
                grdTeamIncidents.Bind(teamincidents.FindAll(inc => inc.IncidentStatusId > 1 && inc.AssignedToSID != SarsUser.SID && inc.IncidentStatusId < 6));
            }
            else
            {
                grdTeamIncidents.Bind(null);
            }
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
                    Response.Redirect(String.Format("~/PrOoc/ChangeAssignee.aspx?ProcId={0}&IncId={1}", ProcessID, incidentId));
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
                    Response.Redirect(String.Format("~/PrOoc/CompleteIncident.aspx?ProcId={0}&IncId={1}", ProcessID, incidentId));
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
                    Response.Redirect(String.Format("~/PrOoc/CloseIncident.aspx?ProcId={0}&IncId={1}", ProcessID, incidentId));
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
                      

                        if (CurrentProc.Description.Contains("Internal"))
                        {
                            Response.Redirect(String.Format("~/PrOoc/RegisterUserIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
                        }
                        else if (CurrentProc.Description.Contains("External"))
                        {
                            Response.Redirect(String.Format("~/PrOoc/RegisterExternalIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
                        }
                        else if (CurrentProc.Description.Contains("Escalation"))
                        {
                            Response.Redirect(String.Format("~/PrOoc/RegisterTaxEscalations.aspx?procId={0}&incId={1}", ProcessID, incidentId));
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
                        Response.Redirect(String.Format("~/PrOoc/ReOpenIncident.aspx?procId={0}&incId={1}", prodessId, incidentId));
                    }
                }
            }
        }
    }
    protected void gvIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string pageName;
        var incidentId = e.CommandArgument.ToString();
        string type = string.Empty;
        if (CurrentProc.Description.Contains("Internal"))
        {
            pageName = "RegisterUserIncident";
            type = "Internal";
        }
        else if (CurrentProc.Description.Contains("External"))
        {
            pageName = "RegisterExternalIncident";
            type = "External";
        }
        else
        {
            pageName = "RegisterTaxEscalations";
            type = "Escalation";
        }

        switch (e.CommandName)
        {

            case "View_Incident":
                {
                    CurrentProc = CurrentProcess;
                   
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
                    if (CurrentProc.Description.Contains("Internal"))
                    {
                        pageName = "RegisterUserIncident";
                    }
                    else if (CurrentProc.Description.Contains("External"))
                    {
                        pageName = "RegisterExternalIncident";
                    }
                    else {
                        pageName = "RegisterTaxEscalations";
                    }

                    CopyIncident(incidentId, pageName);
                    break;
                }
            default:
                {
                   // MessageBox.Show("Cant execute option selected.");
                    return;
                }

        }
        Response.Redirect(String.Format("~/PrOoc/{0}.aspx?ProcId={1}&IncId={2}&type={3}", pageName, ProcessID, incidentId, type));
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
            Response.Redirect(String.Format("~/PrOoc/{0}.aspx?ProcId={1}&IncId={2}&cpd=1&oldIncId={3}", navigateTo, ProcessID, newIncidentId, incidnetToCopy));
        }
    }
}