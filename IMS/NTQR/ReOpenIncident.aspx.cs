using System;
using System.IO;
using System.Web.UI.WebControls;

public partial class NTQR_ReOpenIncident : IncidentTrackingPage
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
                    if (string.IsNullOrEmpty(txtNotes.Text))
                    {
                        txtNotes.Focus();
                        MessageBox.Show("Note is required.");
                        return;
                    }
                    var saved = IncidentTrackingManager.ReOpenIncident(IncidentID);
                    if (saved > 0)
                    {
                        AddNote();
                        SendCompletedNotification();
                        txtIncidentDueDate.Enabled = false;
                        Toolbar1.Items[0].Visible = false;
                        txtNotes.Enabled = false;
                        txtSummary.Enabled = false;
                        MessageBox.Show("Incident has been re-opened.");
                    }
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
        var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}", ProcessID, IncidentID));
        CurrentIncidentDetails = CurrentIncident;
        var process = CurrentProcess;
        var userAssigned = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID.Trim());

        var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.IncidentNumber);

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
                        client.Send1("IncidentTracking@sars.gov.za", owner.Mail, subject, body);
                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, owner.SID, owner.Mail, IncidentID, ProcessID);

                    }
                }
            }
        }
    }
}