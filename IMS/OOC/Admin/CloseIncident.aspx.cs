using System;
using System.IO;
using System.Web.UI.WebControls;

public partial class Admin_CloseIncident : IncidentTrackingPage
{
    protected Incident CurrentIncidentDetails;
    protected IncidentProcess CurrentProc;

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
            }
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Submit":
                {
                    if (string.IsNullOrEmpty(txtIncidentDueDate.Text))
                    {
                        txtIncidentDueDate.Focus();
                        MessageBox.Show("Date Actioned is required.");
                        return;
                    }
                    if (string.IsNullOrEmpty(txtNotes.Text))
                    {
                        txtNotes.Focus();
                        MessageBox.Show("Note is required.");
                        return;
                    }
                    var saved = IncidentTrackingManager.CloseIncident(IncidentID, DateTime.Parse(txtIncidentDueDate.Text));
                    if (saved > 0)
                    {
                        AddNote();
                        SendCompletedNotification();
                        txtIncidentDueDate.Enabled = false;
                        Toolbar1.Items[0].Visible = false;
                        txtNotes.Enabled = false;                        
                        Response.Redirect(String.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));
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
        try
        {

       

        var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}", ProcessID, IncidentID));
        CurrentIncidentDetails = CurrentIncident;
        var process = CurrentProcess;
        var userAssigned = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID.Trim());
        var createdby = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID);
        var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.ReferenceNumber);

        if (Request.PhysicalApplicationPath != null)
        {
            var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-closed.htm");
            var tempate = File.ReadAllText(templateDir);

            if (CurrentIncidentDetails.DueDate != null)
            {
                var body = string.Format(tempate,
                                         userAssigned.FullName,
                                         CurrentIncidentDetails.ReferenceNumber,
                                         CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd"),
                                         CurrentIncidentDetails.Summary,
                                         CurrentIncidentDetails.IncidentStatus,
                                         incidentURL,
                                         txtNotes.Text,
                                         string.Format("{0} | {1}", createdby.FullName, createdby.SID));

                using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                {
                    var newUser = SarsUser.GetADUser(UserSelector1.SID);
                    client.Send1("IncidentTracking@sars.gov.za", newUser.Mail, subject, body);
                    IncidentTrackingManager.SaveIncidentEmailLog(body, subject, newUser.SID, newUser.Mail, IncidentID, ProcessID);
                    foreach (var processOwner in CurrentProcess.Owners)
                    {
                        var owner = SarsUser.GetADUser(processOwner.OwnerSID);
                        client.Send1("IncidentTracking@sars.gov.za", owner.Mail, subject, body);
                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, owner.SID, owner.Mail, IncidentID, ProcessID);
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