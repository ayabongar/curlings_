using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;

public partial class PrPcr_SearchHome : IncidentTrackingPage
{
    protected IncidentProcess CurrentProc;
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentProc = CurrentProcess;
        if (CurrentProc == null)
        {
            Response.Redirect("../InvalidProcessOrIncident.aspx");
        }
    }
    protected void FilterTypeChanged(object sender, EventArgs e)
    {
        fsRecords.Visible = false;
        SearchFilterTypes1.ShowInputs(ddlFilterType.SelectedValue);
        fsSearchButton.Visible = ddlFilterType.SelectedIndex > 0;

    }

    protected void SearchIncidents(object sender, EventArgs e)
    {
        switch (ddlFilterType.SelectedValue)
        {
            case "1":
            case "2":
                {
                    if (String.IsNullOrEmpty(SearchFilterTypes1.FromDate) || String.IsNullOrEmpty(SearchFilterTypes1.ToDate) )
                    {
                        MessageBox.Show("Either Start Date or End Date is not provided");
                        return;
                    }
                    break;
                }
            case "3":
            case "4":
                {
                    if (String.IsNullOrEmpty(SearchFilterTypes1.SID))
                    {
                        MessageBox.Show("Please search and select a user.");
                        return;
                    }
                    break;
                }
            case "5":
            case "6":
                {
                    if (String.IsNullOrEmpty(SearchFilterTypes1.ReferenceNumber))
                    {
                        MessageBox.Show("Reference Number/Incident number Is Required.");
                        return;
                    }
                    break;
                }
                
            default:
                {
                    break;
                }
        }
        var recordSet = IncidentTrackingManager.SearchIncidents(SearchFilterTypes1.ReferenceNumber, SearchFilterTypes1.FromDate, SearchFilterTypes1.ToDate, SearchFilterTypes1.SID, ddlFilterType.SelectedValue, this.ProcessID);

        if(recordSet.HasRows)
        {
            Session["searchData"] = recordSet;
            gvIncidents.Bind(recordSet);
            fsRecords.Visible = true;
        }
        else
        {
            MessageBox.Show("No results found");
            fsRecords.Visible = false;
        }
    }
    protected void PageChanging(object sender, GridViewPageEventArgs e)
    {
        if (Session["searchData"] != null)
        {
            gvIncidents.NextPage(Session["searchData"], e.NewPageIndex);
            fsRecords.Visible = true;
        }
        else
        {
            fsRecords.Visible = false;
        }
    }

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        e.Row.Attributes.Add("onclick",
                             Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        var description = DataBinder.Eval(e.Row.DataItem, "Summary").ToString();

        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        e.Row.Attributes.Add("title",
                             "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Incident Summary (Click to open this incident)</font></b>] body=[<font color='red'><b>" +
                             description + "</b></font>]");

        var tdReAssign = e.Row.FindControl("tdReAssign") as System.Web.UI.HtmlControls.HtmlTableCell;
        if (tdReAssign != null)
        {
            var incidentStatusId = DataBinder.Eval(e.Row.DataItem, "Incident Status");
            if (incidentStatusId.Equals("Complete") || incidentStatusId.Equals("Closed"))
            {
                tdReAssign.Visible = false;
            }
            else
            {
                tdReAssign.Visible = true;
            }
        }
    }
    protected void gvIncidents_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (gvIncidents.SelectedDataKey != null)
        {
            var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
            if (!string.IsNullOrEmpty(incidentId))
            {
                Response.Redirect(string.Format("RegisterUserIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
            }
        }
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        if (e.CommandName.Equals("Back", StringComparison.CurrentCultureIgnoreCase))
        {
            Response.Redirect(Request["returnUrl"]);
        }
    }

    protected void gvIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string pageName = string.Empty;
        var incidentId = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "Reassigne_Incident":
                {
                    pageName = "ChangeAssignee";
                    break;
                }
            default:
                pageName = "IncidentRealOnly";
                break;
                ;
        }
        Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}", pageName, ProcessID, incidentId));
    }
}