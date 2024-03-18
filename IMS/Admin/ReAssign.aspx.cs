using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ReAssign : IncidentTrackingPage
{
    protected IncidentProcess CurrentProc;
    protected Incident CurrentIncidentDetails;

    public string IncId
    {
        get { return ViewState["incId"] as string; }
        set { ViewState["incId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentProc = CurrentProcess;
        if (CurrentProc == null)
        {
            Response.Redirect("../InvalidProcessOrIncident.aspx");
        }
        if (!IsPostBack)
        {
            var results = IncidentTrackingManager.GetIncidentsSearchFields(this.ProcessID);
            if (results.HasRows)
            {
                ddlFilterType.DataSource = results;
                ddlFilterType.DataTextField = "Display";
                ddlFilterType.DataValueField = "FieldDName";
                ddlFilterType.DataBind();
                ddlFilterType.Items.Insert(0, new ListItem("Select Filter Type..", ""));
            }
        }
       
    }

    private void BindAssignScreen(string incidentId)
    {
        IncId = incidentId;
        CurrentIncidentDetails = IncidentTrackingManager.GetIncidentById(incidentId); 
        if (CurrentIncidentDetails != null && CurrentIncidentDetails.DueDate != null)
        {
            txtIncidentDueDate.SetValue(CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd"));
        }
        if (CurrentIncidentDetails != null && !string.IsNullOrEmpty(CurrentIncidentDetails.AssignedToSID))
        {
            IncidentNumber.Text = CurrentIncidentDetails.IncidentNumber;
            IncidentStatus1.Text = CurrentIncidentDetails.IncidentStatus;
            Timestamp.Text = CurrentIncidentDetails.AssignedToFullName;
            var adUser = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID);
            UserSelector1.SelectedAdUserDetails = new SelectedUserDetails
            {
                SID = adUser.SID,
                FoundUserName =
                    string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                FullName = adUser.FullName
            };
            UserSelector1.Disable();
            txtSummary.SetValue(CurrentIncidentDetails.Summary);
            if (!string.IsNullOrEmpty(CurrentIncidentDetails.Summary))
            {
                txtSummary.Attributes.Add("title",
                                          "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Incident Summary</font></b>] body=[<font color='red'><b>" +
                                          CurrentIncidentDetails.Summary + "</b></font>]");
            }
        }
    }
    protected void FilterTypeChanged(object sender, EventArgs e)
    {
        fsRecords.Visible = false;
        SearchFilterTypes1.ShowInputs(ddlFilterType.SelectedValue, ddlFilterType.SelectedItem.Text);
        fsSearchButton.Visible = ddlFilterType.SelectedIndex > 0;

    }

    protected void SearchIncidents(object sender, EventArgs e)
    {
        BindIncidents();
    }

    private void BindIncidents()
    {
        switch (ddlFilterType.SelectedValue)
        {
            case "DateRegistered":
            {
                if (String.IsNullOrEmpty(SearchFilterTypes1.FromDate) || String.IsNullOrEmpty(SearchFilterTypes1.ToDate))
                {
                    MessageBox.Show("Either Start Date or End Date is not provided");
                    return;
                }
                break;
            }
            case "RegisteredBy":
            case "AssignedTo":
            {
                if (String.IsNullOrEmpty(SearchFilterTypes1.SID))
                {
                    MessageBox.Show("Please search and select a user.");
                    return;
                }
                break;
            }
            case "IncidentNumber":
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
        var recordSet = IncidentTrackingManager.SearchIncidents(SearchFilterTypes1.ReferenceNumber,
            SearchFilterTypes1.FromDate, SearchFilterTypes1.ToDate, SearchFilterTypes1.SID, ddlFilterType.SelectedValue,
            this.ProcessID, SearchFilterTypes1.ReferenceNumber);

        if (recordSet.HasRows)
        {
            Session["searchData"] = recordSet;
            gvSearchIncidents.Bind(recordSet);
            fsRecords.Visible = true;
        }
        else
        {
           // MessageBox.Show("No results found");
            fsRecords.Visible = false;
        }
    }

    protected void PageChanging(object sender, GridViewPageEventArgs e)
    {
        if (Session["searchData"] != null)
        {
            gvSearchIncidents.NextPage(Session["searchData"], e.NewPageIndex);
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
        //e.Row.Attributes.Add("onclick",
        //                     Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        //var description = DataBinder.Eval(e.Row.DataItem, "Summary").ToString();

        //e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        //e.Row.Attributes.Add("title",
        //                     "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Incident Summary (Click to open this incident)</font></b>] body=[<font color='red'><b>" +
        //                     description + "</b></font>]");

        //var tdReAssign = e.Row.FindControl("tdReAssign") as System.Web.UI.HtmlControls.HtmlTableCell;
        //if (tdReAssign != null)
        //{
        //    var incidentStatusId = DataBinder.Eval(e.Row.DataItem, "Incident Status");
        //    if (incidentStatusId.Equals("Complete") || incidentStatusId.Equals("Closed"))
        //    {
        //        tdReAssign.Visible = false;
        //    }
        //    else
        //    {
        //        tdReAssign.Visible = true;
        //    }
        //}
    }
    protected void gvIncidents_SelectedIndexChanged(object sender, EventArgs e)
    {

        //if (gvIncidents.SelectedDataKey != null)
        //{
        //    var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
        //    if (!string.IsNullOrEmpty(incidentId))
        //    {
        //        Response.Redirect(string.Format("RegisterUserIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
        //    }
        //}
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        if (e.CommandName.Equals("Back", StringComparison.CurrentCultureIgnoreCase))
        {
            Response.Redirect(Request["returnUrl"]);
        }
        switch (e.CommandName)
        {
            case "Submit":
            {
                if (this.UserSelector2.SelectedAdUserDetails == null)
                {
                    UserSelector2.Focus();
                    MessageBox.Show("Please select a user to assigne to");
                    mpAllocate.Show();
                    return;
                }
                if (string.IsNullOrEmpty(UserSelector2.SelectedAdUserDetails.SID))
                {
                    UserSelector2.Focus();
                    MessageBox.Show("Please select a user to assigne to");
                    mpAllocate.Show();
                    return;
                }

                var adUser = SarsUser.GetADUser(UserSelector2.SelectedAdUserDetails.SID);
                if (adUser == null)
                {
                    UserSelector2.Focus();
                    MessageBox.Show("Please select a user to assigne to");
                    mpAllocate.Show();
                    return;
                }


                if (string.IsNullOrEmpty(txtNotes.Text))
                {
                    txtNotes.Focus();
                    MessageBox.Show("Note is required.");
                    mpAllocate.Show();
                    return;
                }
                var saved = IncidentTrackingManager.ChangeAssignee(IncId,
                    UserSelector2.SelectedAdUserDetails.SID.Trim());
                if (saved > 0)
                {
                    CurrentIncidentDetails = IncidentTrackingManager.GetIncidentById(IncId);
                    AddNote();
                    SendFirstNotification();
                    SendFirstNotificationToProcOwners();
                    txtIncidentDueDate.Enabled = false;
                    txtNotes.Enabled = false;
                   // Toolbar1.Items[0].Visible = false;
                    BindIncidents();
                    BindGridViewFromRad();
                    txtNotes.Clear();
                    MessageBox.Show(string.Format("Incident was re-assigned to {0}", adUser.FullName));
                }
                else
                {
                    MessageBox.Show("Incident not assigned.");
                }
                break;
            }
            case "Cancel":
            {
                Response.Redirect(string.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));
                break;
            }
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
                    //Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}", pageName, ProcessID, incidentId));
                    BindAssignScreen(incidentId);
                    mpAllocate.Show();
                    break;
                }
            case "View_Incident":
                if (!string.IsNullOrEmpty(incidentId))
                {
                    Response.Redirect(string.Format("RegisterUserIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
                }
                break;
            default:
                pageName = "IncidentRealOnly";
                break;

        }

    }

    protected void ReAssign_Click(object sender, EventArgs e)
    {
        var objSender = ((sender as Button).CommandArgument);
        if (objSender != null)
        {
            string pageName = string.Empty;
            var incidentId = objSender.ToString();
            pageName = "ChangeAssignee";
            Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}", pageName, ProcessID, incidentId));

        }
    }
    protected void radStatuses_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridViewFromRad();
    }

    private void BindGridViewFromRad()
    {
        var recordSet = IncidentTrackingManager.SearchIncidents(SearchFilterTypes1.ReferenceNumber,
            SearchFilterTypes1.FromDate, SearchFilterTypes1.ToDate, SearchFilterTypes1.SID, radStatuses.SelectedValue,
            this.ProcessID);

        if (recordSet.HasRows)
        {
            Session["searchData"] = recordSet;
            gvSearchIncidents.Bind(recordSet);
            fsRecords.Visible = true;
        }
        else
        {
          //  MessageBox.Show("No results found");
            fsRecords.Visible = false;
        }
    }

    protected void gvSearchIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string pageName = string.Empty;
        var incidentId = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "Reassigne_Incident":
                {
                    pageName = "ChangeAssignee";
                    BindAssignScreen(incidentId);

                    mpAllocate.Show();
                   // Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}", pageName, ProcessID, incidentId));
                    break;
                }
            case "View_Incident":
                if (!string.IsNullOrEmpty(incidentId))
                {
                    Response.Redirect(string.Format("RegisterUserIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
                }
                break;
            default:
                pageName = "IncidentRealOnly";
                break;

        }
    }

    protected void AddNote()
    {
        var workInfo = new WorkInfoDetails
        {
            AddedBySID = SarsUser.SID,
            IncidentId = Convert.ToDecimal(IncidentID),
            ProcessId = Convert.ToInt32(ProcessID),
            Notes = txtNotes.Text
        };
        if (IncidentTrackingManager.AddWorkInfo(workInfo) > 0)
        {
            var details = CurrentIncident;
            if (details.IncidentStatusId == 2)
            {
                const int statusId = 3;
                IncidentTrackingManager.UpdateIncidentStatus(statusId, IncidentID);
            }
            txtNotes.Enabled = false;
        }
    }

    private void SendFirstNotification()
    {

        var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}", ProcessID, IncId));
     //   CurrentIncidentDetails = CurrentIncident;
        var process = CurrentProcess;
        var userAssigned = SarsUser.GetADUser(UserSelector2.SelectedAdUserDetails.SID);

        var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.IncidentNumber);

        if (Request.PhysicalApplicationPath != null)
        {
            var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-re-assigned.htm");
            var tempate = File.ReadAllText(templateDir);

            if (CurrentIncidentDetails.DueDate != null)
            {
                var body = string.Format(tempate,
                                         userAssigned.FullName,
                                         CurrentIncidentDetails.IncidentNumber,
                                         CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd hh:mm"),
                                         CurrentIncidentDetails.Summary,
                                         CurrentIncidentDetails.IncidentStatus,
                                         incidentURL,
                                         txtNotes.Text
                    );

                using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                {
                    client.Send1("IncidentTracking@sars.gov.za", userAssigned.Mail, subject, body);
                    IncidentTrackingManager.SaveIncidentEmailLog(body, subject, userAssigned.SID, userAssigned.Mail,
                                                                 this.IncidentID, this.ProcessID);
                }
            }
        }
    }
    private void SendFirstNotificationToProcOwners()
    {

        var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}", ProcessID, IncId));
       // CurrentIncidentDetails = CurrentIncident;
        var process = CurrentProcess;
        var userAssigned = SarsUser.GetADUser(UserSelector2.SelectedAdUserDetails.SID);

        var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.IncidentNumber);

        if (Request.PhysicalApplicationPath != null)
        {
            var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-re-assigned-procowner.htm");
            var tempate = File.ReadAllText(templateDir);

            if (CurrentIncidentDetails.DueDate != null)
            {

                using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                {
                    foreach (var processOwner in CurrentProcess.Owners)
                    {
                        var owner = SarsUser.GetADUser(processOwner.OwnerSID);
                        var body = string.Format(tempate,
                                                 owner.FullName,
                                                 CurrentIncidentDetails.IncidentNumber,
                                                 CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd hh:mm"),
                                                 CurrentIncidentDetails.Summary,
                                                 CurrentIncidentDetails.IncidentStatus,
                                                 incidentURL,
                                                 txtNotes.Text,
                                                 userAssigned.FullName);

                        client.Send1("IncidentTracking@sars.gov.za", owner.Mail, subject, body);
                    }
                }
            }
        }
    }
}