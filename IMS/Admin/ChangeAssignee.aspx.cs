using Sars.Systems.Data;
using Sars.Systems.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ChangeAssignee : IncidentTrackingPage
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
                        var user = string.Format("{0}\\{1}", Sars.Systems.Data.SARSDataSettings.Settings.DomainName,
                  UserSelector1.SID.Trim());
                        if (!string.IsNullOrEmpty(user))
                        {
                            try
                            {
                                var _userRole = Roles.GetUserRoles(user);
                                if (_userRole == null)
                                {
                                    Roles.AddUserToRole("Process Administrator", user);
                                }
                            }
                            catch (Sars.Systems.Security.SecureException exception)
                            {
                            }
                            try
                            {
                                var userSid = UserSelector1.SID.Trim();
                                SarsUser.SaveUser(userSid);
                                var recordsAffected = IncidentTrackingManager.AddUserToAProcess(userSid, ProcessID, "2");
                            }
                            catch (Exception)
                            {
                            }
                        }
                        Response.Redirect(string.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));
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
    private List<string> GetPeopleToBeEmailed(string incId)
    {
        List<string> ccsUsers = new List<string>();
        RecordSet list = IncidentTrackingManager.GetAllPeopleToBeEmailed(incId);
        RecordSet ccPerson = IncidentTrackingManager.GetCosCCSPerson(incId);
        if (ccPerson != null)
        {
            if (ccPerson.HasRows)
            {
                foreach (DataRow row in ccPerson.Rows)
                {
                    var getCcs = SarsUser.GetADUser(row["Answer"].ToString());
                    ccsUsers.Add(getCcs.Mail);
                }
            }
        }

        if (list.HasRows)
        {
            foreach (DataRow row in list.Rows)
            {
                if (!string.IsNullOrEmpty(row["CanSendEmail"].ToString()))
                {
                    if ((bool)(row["CanSendEmail"]))
                    {
                        string[] sid = row["Description"].ToString().Split('|');
                        if (sid.Length > 1)
                        {
                            var getCcs = SarsUser.GetADUser(sid[1]);
                            ccsUsers.Add(getCcs.Mail);
                        }
                    }
                }
            }
        }

        return ccsUsers;
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
            var userAssigned = SarsUser.GetADUser(UserSelector2.SelectedAdUserDetails.SID);

            var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.IncidentNumber);
            var createdby = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID);
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
                                                string.Format("{0} | {1}", createdby.FullName, createdby.SID)

                                                );
                    using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                    {
                        List<string> ccsUsers = GetPeopleToBeEmailed(CurrentIncident.IncidentID.ToString());
                        var oMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = "IncidentTracking@sars.gov.za",
                            Body = body,
                            IsBodyHtml = true,
                            Subject = subject,
                            To = new[] { userAssigned.Mail },
                            CC = ccsUsers.ToArray(),

                        };
                        client.Send2(oMessage);
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
        catch (Exception)
        {

            
        }
    }


}