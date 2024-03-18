using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SurveyWizard_ViewMyIncidents : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var incidents = IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID, 0);
            if (incidents != null && incidents.Any())
            {
                lblMessage.Visible = false;
                gvIncidents.Bind(incidents);
            }
            else
            {
                lblMessage.Visible = true;
            }
            //ddlStatuses.Bind(IncidentTrackingManager.GetIncidentsStatuses(), "Description", "IncidentStatusId");
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
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
        switch (e.CommandName)
        {
            case "ViewDetails":
                {
                    if (gvIncidents.SelectedDataKey != null)
                    {
                        var prodessId = gvIncidents.SelectedDataKey["ProcessId"].ToString();
                        var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                        if (!string.IsNullOrEmpty(prodessId) && !string.IsNullOrEmpty(incidentId))
                        {
                            Response.Redirect(String.Format("~/admin/RegisterUserIncident.aspx?procId={0}&incId={1}", prodessId, incidentId));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not create new incident.");
                    }
                    break;
                }
            case "AttachDocuments":
                {
                    if (gvIncidents.SelectedDataKey != null)
                    {
                        var prodessId = gvIncidents.SelectedDataKey["ProcessId"].ToString();
                        var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                        if (!string.IsNullOrEmpty(prodessId) && !string.IsNullOrEmpty(incidentId))
                        {
                            Response.Redirect(String.Format("AttachDocuments.aspx?procId={0}&incId={1}", prodessId, incidentId));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not create new incident.");
                    }
                    break;
                }
            case "AddComments":
                {
                    if (gvIncidents.SelectedDataKey != null)
                    {
                        var prodessId = gvIncidents.SelectedDataKey["ProcessId"].ToString();
                        var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                        if (!string.IsNullOrEmpty(prodessId) && !string.IsNullOrEmpty(incidentId))
                        {
                            Response.Redirect(String.Format("AddWorkInfo.aspx?procId={0}&incId={1}", prodessId, incidentId));
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


        e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
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
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        //if (ddlStatuses.SelectedIndex <= 0)
        //{
        //    gvIncidents.Bind(IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID, 0));
        //}
        //else
        //{
        //    gvIncidents.Bind(IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID,
        //                                                            Convert.ToInt32(ddlStatuses.SelectedValue)));
        //}
    }
    protected void PageChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncidents.PageIndex = e.NewPageIndex;
        btnFilter_Click(null, EventArgs.Empty);
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
                    var processId = gvIncidents.SelectedDataKey["ProcessId"].ToString();
                    Response.Redirect(String.Format("../Admin/ChangeAssignee.aspx?ProcId={0}&IncId={1}", processId, incidentId));
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
                    var processId = gvIncidents.SelectedDataKey["ProcessId"].ToString();
                    Response.Redirect(String.Format("../Admin/CompleteIncident.aspx?ProcId={0}&IncId={1}", processId, incidentId));
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
                        Response.Redirect(String.Format("../Admin/RegisterUserIncident.aspx?procId={0}&incId={1}", prodessId, incidentId));
                    }
                }
            }
        }
    }
}