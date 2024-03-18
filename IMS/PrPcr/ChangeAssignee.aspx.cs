using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrPcr_ChangeAssignee : IncidentTrackingPage
{
    protected Incident CurrentIncidentDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ProcessID))
        {
            MessageBox.Show("Process ID is not available");
            return;
        }
        if (!IsAMember)
        {
            Response.Redirect("../IncidentNotYours.aspx");
        }
        CurrentIncidentDetails = CurrentIncident;

        if (!IsPostBack)
        {
            if (CurrentIncidentDetails != null && CurrentIncidentDetails.DueDate != null)
            {
                txtIncidentDueDate.SetValue(CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd"));
            }
            if (CurrentIncidentDetails != null && !string.IsNullOrEmpty(CurrentIncidentDetails.AssignedToSID))
            {
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
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Submit":
                {
                    if (this.UserSelector2.SelectedAdUserDetails == null)
                    {
                        UserSelector2.Focus();
                        MessageBox.Show("Please select a user to assigne to");
                        return;
                    }
                    if (string.IsNullOrEmpty(UserSelector2.SelectedAdUserDetails.SID))
                    {
                        UserSelector2.Focus();
                        MessageBox.Show("Please select a user to assigne to");
                        return;
                    }

                    var adUser = SarsUser.GetADUser(UserSelector2.SelectedAdUserDetails.SID);
                    if (adUser == null)
                    {
                        UserSelector2.Focus();
                        MessageBox.Show("Please select a user to assigne to");
                        return;
                    }


                    if (string.IsNullOrEmpty(txtNotes.Text))
                    {
                        txtNotes.Focus();
                        MessageBox.Show("Note is required.");
                        return;
                    }
                    var saved = IncidentTrackingManager.ChangeAssignee(IncidentID,
                                                                       UserSelector2.SelectedAdUserDetails.SID.Trim());
                    if (saved > 0)
                    {
                        AddNote();
                        SendFirstNotification();
                        SendFirstNotificationToProcOwners();
                        txtIncidentDueDate.Enabled = false;
                        txtNotes.Enabled = false;
                        Toolbar1.Items[0].Visible = false;
                        MessageBox.Show(string.Format("Incident was re-assigned to {0}", adUser.FullName));
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
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        e.Row.Attributes.Add("onclick",
                             Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        var description = DataBinder.Eval(e.Row.DataItem, "Notes").ToString();

        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        e.Row.Attributes.Add("title",
                             "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Work Info Notes</font></b>] body=[<font color='red'><b>" +
                             description + "</b></font>]");
    }

    private void SendFirstNotification()
    {

        var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}", ProcessID, IncidentID));
        CurrentIncidentDetails = CurrentIncident;
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
                                        String.Format("procId={0}&incId={1}", ProcessID, IncidentID));
        CurrentIncidentDetails = CurrentIncident;
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