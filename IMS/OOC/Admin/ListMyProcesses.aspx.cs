using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ListMyProcesses : Page
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
            lblMessage.Visible = false;
            gvProcesses.Bind(processes);
        }
        else
        {
            lblMessage.Visible = true;
        }
    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        var processes =
            new IncidentProcess("[dbo].[uspRead_UserProcesses]",
                                new Dictionary<string, object>
                                    {
                                        {"@UserSID", SarsUser.SID}
                                    }).GetRecords<IncidentProcess>();
        if (processes.Any())
        {
            gvProcesses.NextPage(processes, e.NewPageIndex);
        }
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        e.Row.Attributes.Add("onclick",
                             Page.ClientScript.GetPostBackEventReference((Control) sender, "Select$" + e.Row.RowIndex));
        var description = DataBinder.Eval(e.Row.DataItem, "Description").ToString();

        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        e.Row.Attributes.Add("title",
                             "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Process Description</font></b>] body=[<font color='red'><b>" +
                             description + "</b></font>]");
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        if (gvProcesses.SelectedIndex == -1)
        {
            if (gvProcesses.Rows.Count == 1)
            {
                gvProcesses.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Please click on a process to select it before you can continue.");
                return;
            }
        }
        switch (e.CommandName)
        {
            case "AddNewIncident":
                {
                    if (gvProcesses.SelectedDataKey != null)
                    {
                        var prodessId = gvProcesses.SelectedDataKey["ProcessId"].ToString();
                        if(!string.IsNullOrEmpty(prodessId))
                        {
                            Response.Redirect(String.Format("InitIncident.aspx?procId={0}", prodessId));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not create new incident.");
                    }
                    break;
                }
            case "ViewIncidents":
                { if (gvProcesses.SelectedDataKey != null)
                    {
                        var prodessId = gvProcesses.SelectedDataKey["ProcessId"].ToString();
                        if(!string.IsNullOrEmpty(prodessId))
                        {
                            Response.Redirect(String.Format("ListProcessInsidents.aspx?procId={0}", prodessId));
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
}