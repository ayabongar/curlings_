using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrOoc_ChangeAssignee : IncidentTrackingPage
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
                    Response.Redirect(string.Format("NormalUserLandingPage.aspx?procId={0}&type={1}", ProcessID,Request["type"]));
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
        try
        {


            string url = string.Empty;
            if (CurrentProc.Description.Contains("Internal"))
            {

                url = string.Format("http://{0}/ims/PrOoc/RegisterUserIncident.aspx?{1}", Request.ServerVariables["HTTP_HOST"],
                                            String.Format("procId={0}&incId={1}&type={2}", ProcessID, IncidentID, Request["type"]));
            }
            else if (CurrentProc.Description.Contains("External"))
            {
                url = string.Format("http://{0}/ims/PrOoc/RegisterExternalIncident.aspx?{1}", Request.ServerVariables["HTTP_HOST"],
                                            String.Format("procId={0}&incId={1}&type={2}", ProcessID, IncidentID, Request["type"]));
            }
            else
            {
                url = string.Format("http://{0}/ims/PrOoc/RegisterTaxEscalations.aspx?{1}", Request.ServerVariables["HTTP_HOST"],
                                            String.Format("procId={0}&incId={1}&type={2}", ProcessID, IncidentID, Request["type"]));
            }
            var incidentURL = url;
            CurrentIncidentDetails = CurrentIncident;
            var process = CurrentProcess;
            var userAssigned = SarsUser.GetADUser(UserSelector2.SelectedAdUserDetails.SID);

            var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.ReferenceNumber);

            if (Request.PhysicalApplicationPath != null)
            {
                var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-re-assigned.htm");
                var tempate = File.ReadAllText(templateDir);

                if (CurrentIncidentDetails.DueDate != null)
                {
                    var body = string.Format(tempate,
                                             userAssigned.FullName,
                                             CurrentIncidentDetails.ReferenceNumber,
                                             CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd hh:mm"),
                                             CurrentIncidentDetails.Summary,
                                             CurrentIncidentDetails.IncidentStatus,
                                             incidentURL,
                                             txtNotes.Text,
                                              string.Format("{0} | {1}", SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).FullName, SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).SID)
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


            string url = string.Empty;
            if (CurrentProc.Description.Contains("Internal"))
            {

                url = string.Format("http://{0}/ims/PrOoc/RegisterUserIncident.aspx?{1}", Request.ServerVariables["HTTP_HOST"],
                                            String.Format("procId={0}&incId={1}&type={2}", ProcessID, IncidentID, Request["type"]));
            }
            else if (CurrentProc.Description.Contains("External"))
            {
                url = string.Format("http://{0}/ims/PrOoc/RegisterExternalIncident.aspx?{1}", Request.ServerVariables["HTTP_HOST"],
                                            String.Format("procId={0}&incId={1}&type={2}", ProcessID, IncidentID, Request["type"]));
            }
            else
            {
                url = string.Format("http://{0}/ims/PrOoc/RegisterTaxEscalations.aspx?{1}", Request.ServerVariables["HTTP_HOST"],
                                           String.Format("procId={0}&incId={1}&type={2}", ProcessID, IncidentID, Request["type"]));
            }
            var incidentURL = url;
            var process = CurrentProcess;
            var userAssigned = SarsUser.GetADUser(UserSelector2.SelectedAdUserDetails.SID);

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
                                                     CurrentIncidentDetails.ReferenceNumber,
                                                     CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd hh:mm"),
                                                     CurrentIncidentDetails.Summary,
                                                     CurrentIncidentDetails.IncidentStatus,
                                                     incidentURL,
                                                     txtNotes.Text,
                                                     string.Format("{0} | {1}", SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).FullName, SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).SID));

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