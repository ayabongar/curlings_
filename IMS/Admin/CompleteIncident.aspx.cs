using Sars.Systems.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

public partial class Admin_CompleteIncident : IncidentTrackingPage
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
        CurrentProc = this.CurrentProcess;

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
                   
                    if (string.IsNullOrEmpty(txtNotes.Text))
                    {
                        txtNotes.Focus();
                        MessageBox.Show("Note is required.");
                        return;
                    }
                    if (CurrentProc.ProcessId == int.Parse(System.Configuration.ConfigurationManager.AppSettings["Legislative"]))
                    {
                        var saved = IncidentTrackingManager.FinaliseIncident(IncidentID);
                        if (saved > 0)
                        {
                            AddNote();
                            SendCompletedNotification();
                            txtIncidentDueDate.Enabled = false;
                            Toolbar1.Items[0].Visible = false;
                            txtNotes.Enabled = false;
                            txtSummary.Enabled = false;
                            MessageBox.Show("Incident has been Finalised.");
                        }
                    }
                    else
                    {
                        var saved = IncidentTrackingManager.CompleteIncident(IncidentID);
                        if (saved > 0)
                        {
                            AddNote();
                            SendCompletedNotification();
                            txtIncidentDueDate.Enabled = false;
                            Toolbar1.Items[0].Visible = false;
                            txtNotes.Enabled = false;
                            txtSummary.Enabled = false;
                            Response.Redirect(String.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));
                        }
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

    private List<string> GetPeopleToBeEmailed()
    {
        List<string> ccsUsers = new List<string>();
        RecordSet list = IncidentTrackingManager.GetAllPeopleToBeEmailed(CurrentIncident.IncidentID.ToString());
        RecordSet ccPerson = IncidentTrackingManager.GetCosCCSPerson(CurrentIncident.IncidentID.ToString());
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
    private void SendCompletedNotification()
    {
        try
        {

            var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                            Request.ServerVariables["HTTP_HOST"],
                                            String.Format("procId={0}&incId={1}", ProcessID, IncidentID));
            CurrentIncidentDetails = CurrentIncident;
       
            List<string> ccsUsers = GetPeopleToBeEmailed();
            var process = CurrentProcess;
            var userAssigned = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID.Trim());
            var createdby = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID);
            var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.IncidentNumber);

            if (Request.PhysicalApplicationPath != null)
            {
                var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-completed.htm");
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
                                             txtNotes.Text, string.Format("{0} | {1}", createdby.FullName, createdby.SID));

                    using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                    {
                          ccsUsers.Add(userAssigned.Mail);
                       
                        var oMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = "IncidentTracking@sars.gov.za",
                            Body = body,
                            IsBodyHtml = true,
                            Subject = subject,
                            To = new[] { createdby.Mail },
                            CC = ccsUsers.ToArray(),

                        };
                        client.Send2(oMessage);
                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, createdby.SID, createdby.Mail, IncidentID, ProcessID);

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