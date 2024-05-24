using System;
using System.IO;
using System.Web.UI.WebControls;

public partial class Admin_ReOpenIncident : IncidentTrackingPage
{
    protected Incident CurrentIncidentDetails;
    protected IncidentProcess CurrentProc;

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentIncidentDetails = CurrentIncident;
        CurrentProc = CurrentProcess;

        if (!IsPostBack)
        {
            if (CurrentIncidentDetails != null && CurrentIncidentDetails.DueDate != null)
            {
                txtIncidentDueDate.SetValue(CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd hh:mm"));
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
               // UserSelector1.Disable();
               // txtSummary.SetValue(CurrentIncidentDetails.Summary);
              
            }
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Submit":
                {
                    if (string.IsNullOrEmpty(txtNotes.Text))
                    {
                        txtNotes.Focus();
                        MessageBox.Show("Note is required.");
                        return;
                    }

                    var saved = IncidentTrackingManager.ReOpenIncident(IncidentID);
                    // saved = IncidentTrackingManager.ChangeAssignee(IncidentID, UserSelector1.SelectedAdUserDetails.SID.Trim());
                    if (UserSelector_1.SelectedAdUserDetails.SID != null)
                    {
                        saved = IncidentTrackingManager.ChangeAssignee(IncidentID,
                            UserSelector_1.SelectedAdUserDetails.SID.Trim());
                    }
                    AddNote();
                    SendCompletedNotification();
                    SendFirstNotificationToProcOwners();
                    txtIncidentDueDate.Enabled = false;
                    Toolbar1.Items[0].Visible = false;
                    txtNotes.Enabled = false;

                    Response.Redirect(String.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));

                    break;
                }
            case "Cancel":
                {
                    Response.Redirect(String.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));
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

    private void SendCompletedNotification()
    {
        try
        {

       
        var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}", ProcessID, IncidentID));
        CurrentIncidentDetails = CurrentIncident;
        var process = CurrentProcess;
        var userAssigned = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID.Trim());

        var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.ReferenceNumber);

        if (Request.PhysicalApplicationPath != null)
        {
            var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-reopened.htm");
            var tempate = File.ReadAllText(templateDir);

            if (CurrentIncidentDetails.DueDate != null)
            {
                var body = string.Format(tempate,
                                         userAssigned.FullName,
                                         CurrentIncidentDetails.IncidentNumber,
                                         CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd"),
                                         CurrentIncidentDetails.Summary,
                                         CurrentIncidentDetails.IncidentStatus,
                                         incidentURL,
                                         txtNotes.Text);

                using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                {
                    client.Send1("IncidentTracking@sars.gov.za", userAssigned.Mail, subject, body);
                    IncidentTrackingManager.SaveIncidentEmailLog(body, subject, userAssigned.SID, userAssigned.Mail, IncidentID, ProcessID);

                    foreach (var processOwner in this.CurrentProcess.Owners)
                    {
                        var owner = SarsUser.GetADUser(processOwner.OwnerSID);
                            if (owner != null)
                            {
                                client.Send1("IncidentTracking@sars.gov.za", owner.Mail, subject, body);
                                IncidentTrackingManager.SaveIncidentEmailLog(body, subject, owner.SID, owner.Mail, IncidentID, ProcessID);
                            }

                    }
                }
            }
        }
        }
        catch (Exception)
        {

            
        }
    }

    private void SendFirstNotification()
    {
        try
        {       

        var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}", ProcessID, IncidentID));
        CurrentIncidentDetails = CurrentIncident;
        var process = CurrentProcess;
        var userAssigned = SarsUser.GetADUser(UserSelector1.SelectedAdUserDetails.SID);
      var createdBy = string.Format("{0} | {1}", SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).FullName, SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).SID);

      var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.ReferenceNumber);

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
                                         txtNotes.Text,
                                         createdBy
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
        catch (Exception)
        {

            
        }
    }
    private void SendFirstNotificationToProcOwners()
    {
        try
        {

        
       
        var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}", ProcessID, IncidentID));
        CurrentIncidentDetails = CurrentIncident;
        var process = CurrentProcess;
        var userAssigned = SarsUser.GetADUser(UserSelector1.SelectedAdUserDetails.SID);

        var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.ReferenceNumber);

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
        catch (Exception)
        {

            
        }
    }
}