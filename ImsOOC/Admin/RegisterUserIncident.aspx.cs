using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Sars.Systems.Data;
using Sars.Systems.Security;

public partial class Admin_RegisterUserIncident : IncidentTrackingPage
{
    protected Incident CurrentIncidentDetails;
    protected IncidentProcess CurrentProcessDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ProcessID))
        {
            MessageBox.Show("Process ID is not available");
            return;
        }

        //if(!IsAMember)
        //{
        //    Response.Redirect("../IncidentNotYours.aspx");
        //}
        CurrentIncidentDetails = CurrentIncident;
        CurrentProcessDetails = CurrentProcess;
        if (CurrentIncidentDetails == null)
        {
            Response.Redirect("../InvalidProcessOrIncident.aspx");
            return;
        }
        if (!IsPostBack)
        {
            if (CurrentProcessDetails.AddCoverPage)
            {
                Toolbar1.Items[2].Visible = true;
            }

            var status = IncidentTrackingManager.GetIncidentsStatuses();
            BindUserRolesAccess();

            Toolbar1.Items[6].Visible = true;
            if (CurrentIncidentDetails != null && CurrentIncidentDetails.DueDate != null)
            {
                txtIncidentDueDate.SetValue(CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd"));
            }
            else
            {
                txtIncidentDueDate.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            }
            txtCrossReferenceNo.SetValue(CurrentIncidentDetails.CrossRefNo);
            if (CurrentIncidentDetails != null && !string.IsNullOrEmpty(CurrentIncidentDetails.AssignedToSID))
            {
                if (
                    !CurrentIncidentDetails.AssignedToSID.Equals(SarsUser.SID, StringComparison.CurrentCultureIgnoreCase))
                {
                    Response.Redirect(String.Format("IncidentRealOnly.aspx?procId={0}&incId={1}&msgId=10", ProcessID,
                                                    IncidentID));
                    return;
                }

                if (CurrentIncidentDetails.IncidentStatusId != 2 && CurrentIncidentDetails.IncidentStatusId != 3)
                {
                    Response.Redirect(String.Format("IncidentRealOnly.aspx?procId={0}&incId={1}&msgId=10", ProcessID,
                                                    IncidentID));
                    return;
                }
                var adUser = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID);
                AssignedToSID.SelectedAdUserDetails = new SelectedUserDetails
                {
                    SID = adUser.SID,
                    FoundUserName =
                                                                  string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                    FullName = adUser.FullName
                };
                AssignedToSID.Disable();

                BindSecondAssignedUser();
            }
            LoadInfo();
            txtCreatedBy.Value = string.Format("{0} | {1}", SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).FullName, SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).SID);

            UCAttachDocuments.LoadDocuments();
            if (!String.IsNullOrEmpty(Request["cpd"]) && ViewState["emailSent"] == null)
            {
                ViewState["emailSent"] = true;
                SendFirstNotification(CurrentIncidentDetails);
            }
            if (CurrentIncidentDetails.IncidentStatusId >= 2)
            {
                drpStatuses.Visible = true;
                drpStatuses.SelectedValue = CurrentIncidentDetails.IncidentStatus;
            }
        }
    }

    private void BindUserRolesAccess()
    {
        var userRole = this.Page.User.GetRole();
        var roleId = new Guid();
        switch (userRole)
        {
            case "Administrator Head - Top Secret":
                roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                break;
            case "Administrator Manager - Secret":
                roleId = new Guid(ConfigurationManager.AppSettings["AdministratorManagerSecret"]);
                break;
            case "Administrator - confidential":
                roleId = new Guid(ConfigurationManager.AppSettings["AdministratorConfidential"]);
                break;
            case "System User":
                roleId = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
                break;
            case "Developer":
                roleId = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
                break;
        }
        var roles = IncidentTrackingManager.GetIncidentsRolesByUserRoleId(roleId);       
        drpRoles.Bind(roles, "Description", "RoleId");
        drpRoles.SelectedValue = roleId.ToString();
    }

    private void BindSecondAssignedUser()
    {
        List<IncidentAllocation> data = IncidentTrackingManager.GetSecondAssignedUser(IncidentID);
        if (data != null)
        {
            //  SecAssignedToSID.Value = string.Format("{0} | {1}", SarsUser.GetADUser(data[0].AssisgnedToSID).FullName, SarsUser.GetADUser(data[0].AssisgnedToSID).SID);
            SecAssignedToSID.SelectedAdUserDetails = new SelectedUserDetails
            {
                SID = data[0].AssisgnedToSID,
                FoundUserName = string.Format("{0} | {1}", SarsUser.GetADUser(data[0].AssisgnedToSID).FullName, data[0].AssisgnedToSID),
                FullName = SarsUser.GetADUser(data[0].AssisgnedToSID).FullName
            };
        }

    }
    private void LoadInfo()
    {
        List<WorkInfoDetails> data = IncidentTrackingManager.GetWorkInfoByIncidentID(IncidentID);
        //if (data != null && data.Any())
        //{
        //    gvWorkInfo.Bind(data);
        //}
        treeNotes.Nodes.Clear();
        var text = string.Empty;
        if (data != null)
        {
            for (int i = 0; i < data.Count; i++)
            {
                text = data[i].Timestamp.ToString() + " : " + data[i].CreatedBy;
                TreeNode parent = new TreeNode
                {
                    Text = text,
                    Value = data[i].WorkInfoId.ToString(),
                };
                TreeNode child = new TreeNode
                {
                    Text = data[i].Notes,
                    Value = data[i].WorkInfoId.ToString(),
                };
                parent.ChildNodes.Add(child);
                treeNotes.Nodes.Add(parent);
            }
        }
    }

    private void SaveIncident()
    {

        try
        {
            if (CurrentIncidentDetails == null)
            {
                CurrentIncidentDetails = CurrentIncident;
            }

            var userRole = this.Page.User.GetRole();
            Guid roleId = new Guid(drpRoles.SelectedValue);           
            if (CurrentIncidentDetails.IncidentStatusId == 1)
            {
                SarsUser.SaveUser(AssignedToSID.SID);
                var numrecords =
                    IncidentTrackingManager.UpdateIncidentDetails
                        (
                            Convert.ToDateTime(txtIncidentDueDate.Text),
                            AssignedToSID.SID.Trim(),
                            IncidentID,
                            roleId
                );

                var user = string.Format("{0}\\{1}", Sars.Systems.Data.SARSDataSettings.Settings.DomainName,
                    AssignedToSID.SID.Trim());
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
                        var userSid = AssignedToSID.SID.Trim();
                        SarsUser.SaveUser(userSid);
                        var recordsAffected = IncidentTrackingManager.AddUserToAProcess(userSid, ProcessID, "2");
                    }
                    catch (Exception)
                    {
                    }
                }
                if (numrecords > 0)
                {
                    CurrentIncidentDetails = CurrentIncident;
                    List<string> ccsUsers = GetPeopleToBeEmailed();
                    SendFirstNotification(CurrentIncidentDetails, ccsUsers.ToArray());
                    SendFirstNotificationToProcOwners(CurrentIncidentDetails);
                }
            }
            else
            {
                if (IsAMember)
                {
                    if (CurrentIncidentDetails.IncidentStatusId > 1 && CurrentIncidentDetails.IncidentStatusId < 3)
                    {
                        var updateDueDate =
                            IncidentTrackingManager.UpdateIncidentDetails
                                (
                                    Convert.ToDateTime(txtIncidentDueDate.Text),
                                    AssignedToSID.SID.Trim(),
                                    IncidentID,
                                    roleId
                                );
                        const int statusId = 3;
                        IncidentTrackingManager.UpdateIncidentStatus(statusId, IncidentID);

                        List<string> ccsUsers = GetPeopleToBeEmailed();
                        SendAcceptanceEmail(CurrentIncidentDetails, ccsUsers.ToArray());
                    }
                }
                if (drpStatuses.SelectedItem.Text.Equals("Finalised") || drpStatuses.SelectedItem.Text.Equals("Return to originator"))
                {
                    UpdateIncidentStatuses(drpStatuses.SelectedItem.Text);
                }
                else if (int.Parse(drpStatuses.SelectedValue) > 3)
                {
                    UpdateIncidentStatuses(drpStatuses.SelectedValue);
                }
            }

            bool secondAssignee = !string.IsNullOrEmpty(SecAssignedToSID.SID) ? true : false;
            var records = IncidentTrackingManager.AllocateSecondAssignee(SecAssignedToSID.SID.Trim(), IncidentID, secondAssignee);

        }
        catch (Exception exception)
        {
            throw; //MessageBox.Show(exception.Message);
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
                            if (getCcs != null)
                            {
                                ccsUsers.Add(getCcs.Mail);
                            }
                        }
                    }
                }
            }
        }

        return ccsUsers;
    }

    private void PrintDocument()
    {
        var script = @"<script>$('textarea').each( function( i, el ) {
    $(el).height( el.scrollHeight );});</script>";
        var sb = new System.Text.StringBuilder();
        sb.Append(" var mywindow = window.open('', 'Incident Tracking - Cover Page', 'width=800,height=800,left=100,top=100,resizable=yes,scrollbars=1');");
        sb.Append("mywindow.document.write('<html><head><title>Incident Tracking - Cover Page</title>');");
        // sb.Append("mywindow.document.write('" + color + "');");
        sb.Append(" mywindow.document.write(' <link href=../Styles/survey.css rel=stylesheet />');");
        sb.Append(" mywindow.document.write(' <link href=../Styles/Site.css rel=stylesheet />');");
        sb.Append(" mywindow.document.write(' <link href=../Styles/bootstrap.css rel=stylesheet />');");
        //sb.Append(" mywindow.document.write(' <link href=../Styles/toolBars.css rel=stylesheet />');");
        sb.Append(" mywindow.document.write(' <link href=../Styles/toolBars.css rel=stylesheet />');");
        sb.Append(" mywindow.document.write(' <link href=../Styles/PrintIncident.css rel=stylesheet />');");
        //  sb.Append("mywindow.document.write('<script src=../Scripts/jquery-1.9.1.js type=text/javascript></script> ');");
        //  sb.Append("mywindow.document.write('" + script+"');"); 
        sb.Append(" mywindow.document.write('</head><body >');");
        sb.Append(" mywindow.document.write($('#" + divContent.ClientID + "').html());");
        sb.Append(" mywindow.document.write('</body></html>');");
        sb.Append("mywindow.document.close(); ");
        sb.Append(" mywindow.focus(); ");
        sb.Append(" mywindow.print();");
        sb.Append(" mywindow.close();");
        ClientScript.RegisterStartupScript(this.GetType(), "Print", sb.ToString(), true);
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Submit":
                {
                    if (string.IsNullOrEmpty(txtIncidentDueDate.Text))
                    {

                        MessageBox.Show("Due Date is required.");
                        return;
                    }
                    DateTime testDate;
                    if (!DateTime.TryParse(txtIncidentDueDate.Text, out testDate))
                    {
                        MessageBox.Show("Due Date formart is invalid, mut be (yyyy-mm-dd hh:ss).");
                        return;
                    }
                    if (drpRoles.SelectedIndex <= 0)
                    {
                        MessageBox.Show("Incident Type To is required.");
                        return;
                    }
                    if (string.IsNullOrEmpty(AssignedToSID.SID))
                    {
                        MessageBox.Show("Assigned To is required.");
                        return;
                    }
                    if (!DisplaySurvey2.SaveQuestions())
                    {

                        return;
                    }

                    if (!string.IsNullOrEmpty(txtNotes.Text.Trim()))
                    {
                        var workInfo = new WorkInfoDetails
                        {
                            AddedBySID = SarsUser.SID,
                            IncidentId = Convert.ToDecimal(IncidentID),
                            ProcessId = Convert.ToInt32(ProcessID),
                            Notes = txtNotes.Text
                        };
                        IncidentTrackingManager.AddWorkInfo(workInfo);
                    }
                    SaveIncident();
                    Response.Redirect(String.Format("IncidentRealOnly.aspx?procId={0}&incId={1}&msgId=1", ProcessID,
                        IncidentID));
                    break;
                }
            case "SaveAndClose":
                {
                    if (string.IsNullOrEmpty(txtIncidentDueDate.Text))
                    {

                        MessageBox.Show("Due Date is required.");
                        return;
                    }
                    DateTime testDate;
                    if (!DateTime.TryParse(txtIncidentDueDate.Text, out testDate))
                    {
                        MessageBox.Show("Due Date formart is invalid, mut be (yyyy-mm-dd hh:ss).");
                        return;
                    }
                    if (string.IsNullOrEmpty(AssignedToSID.SID))
                    {
                        MessageBox.Show("Assigned To is required.");
                        return;
                    }

                    if (!DisplaySurvey2.SaveQuestions())
                    {

                        return;
                    }

                    if (!string.IsNullOrEmpty(txtNotes.Text.Trim()))
                    {
                        var workInfo = new WorkInfoDetails
                        {
                            AddedBySID = SarsUser.SID,
                            IncidentId = Convert.ToDecimal(IncidentID),
                            ProcessId = Convert.ToInt32(ProcessID),
                            Notes = txtNotes.Text
                        };
                        IncidentTrackingManager.AddWorkInfo(workInfo);
                    }
                    SaveIncident();
                    Response.Redirect(String.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));
                    break;
                }
            case "Cancel":
                {
                    Response.Redirect(String.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));
                    break;
                }
            case "Add":
                {
                    if (string.IsNullOrEmpty(txtNotes.Text))
                    {
                        txtNotes.Focus();
                        MessageBox.Show("Description is required.");
                        return;
                    }
                    var workInfo = new WorkInfoDetails
                    {
                        AddedBySID = SarsUser.SID,
                        IncidentId = Convert.ToDecimal(IncidentID),
                        ProcessId = Convert.ToInt32(ProcessID),
                        Notes = txtNotes.Text
                    };
                    if (IncidentTrackingManager.AddWorkInfo(workInfo) > 0)
                    {
                        LoadInfo();
                        txtNotes.Clear();
                        txtNotes.Enabled = false;
                        // MessageBox.Show("Work Info added successfully.");
                    }
                    break;
                }
            case "Print":
                {
                    //Response.Redirect(String.Format("GenerateCoverPage.aspx?procId={0}&incId={1}&msgId=1", ProcessID,
                    //    IncidentID));
                    var page = "~/Admin/RegisterUserIncident";
                    Response.Redirect(String.Format("~/Admin/CoverPage.aspx?procId={0}&incId={1}&msgId=1&pg={2}", ProcessID, IncidentID, page));
                    break;
                }
            case "AcknowledgementLetter":
                {
                    Response.Redirect(String.Format("Acknowledgement.aspx?procId={0}&incId={1}&msgId=1", ProcessID,
                        IncidentID));
                    break;
                }
            case "ReAssign":
                {
                    if (string.IsNullOrEmpty(txtIncidentDueDate.Text))
                    {
                        //  tbContainer.ActiveTabIndex = 0;
                        MessageBox.Show("Due Date is required.");
                        return;
                    }
                    DateTime testDate;
                    if (!DateTime.TryParse(txtIncidentDueDate.Text, out testDate))
                    {
                        //tbContainer.ActiveTabIndex = 0;
                        MessageBox.Show("Due Date formart is invalid, mut be (yyyy-mm-dd hh:ss).");
                        return;
                    }
                    if (string.IsNullOrEmpty(AssignedToSID.SID))
                    {
                        MessageBox.Show("Assigned To is required.");
                        return;
                    }
                    if (CurrentIncidentDetails.DueDate == null)
                    {
                        if (Convert.ToDateTime(txtIncidentDueDate.Text) < DateTime.Today)
                        {
                            // tbContainer.ActiveTabIndex = 0;
                            txtIncidentDueDate.Focus();
                            MessageBox.Show(
                                string.Format(
                                    "Incident Due Date must be greater or equal to Incident Registration Date [{0}]",
                                    CurrentIncidentDetails.Timestamp.ToString("yyyy-MM-dd HH:mm")));
                            return;
                        }
                    }
                    if (!DisplaySurvey2.SaveQuestions())
                    {
                        //tbContainer.ActiveTabIndex = 0;
                        return;
                    }
                    if (!string.IsNullOrEmpty(txtNotes.Text.Trim()))
                    {
                        txtNotes.Focus();
                        //tbContainer.ActiveTabIndex = 2;
                        MessageBox.Show("Please click the Save Work Info button!");
                        return;
                    }
                    SaveIncident();
                    Response.Redirect(String.Format("ChangeAssignee.aspx?procId={0}&incId={1}", ProcessID, IncidentID));
                    break;
                }
            case "PrintScreen":
                {
                    PrintDocument();
                    //ClientScript.RegisterStartupScript(this.GetType(),"Print","print()",true);
                    break;
                }
        }
    }

    private void UpdateIncidentStatuses(string incId)
    {
        switch (incId)
        {
            case "2":
                break;
            case "3":
                break;
            case "4":
                var saved = IncidentTrackingManager.CompleteIncident(IncidentID);
                if (saved > 0)
                {
                    SendCompletedNotification();
                }
                break;
            case "5":
                saved = IncidentTrackingManager.CloseIncident(IncidentID);
                if (saved > 0)
                {
                    SendCompletedNotification();
                }
                break;
            case "Finalised":
                saved = IncidentTrackingManager.FinaliseIncident(IncidentID);
                if (saved > 0)
                {
                    SendCompletedNotification();
                }
                break;
            case "Return to originator":
                saved = IncidentTrackingManager.ReturnToOriginator(IncidentID);
                if (saved > 0)
                {
                    SendCompletedNotification();
                }
                break;
        }
    }

    private void SendCompletedNotification()
    {
        try
        {

            CurrentIncidentDetails = CurrentIncident;
            List<string> ccsUsers = GetPeopleToBeEmailed();

            var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}", ProcessID, IncidentID));

            var process = CurrentProcess;
            var userAssigned = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID.Trim());

            var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.ReferenceNumber);

            if (Request.PhysicalApplicationPath != null)
            {
                var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-completed.htm");
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
                                             txtCreatedBy.Value);

                    using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                    {
                        var sendToUser = SarsUser.GetADUser(AssignedToSID.SelectedAdUserDetails.SID);


                        var oMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = "IncidentTracking@sars.gov.za",
                            Body = body,
                            IsBodyHtml = true,
                            Subject = subject,
                            To = new[] { sendToUser.Mail },
                            CC = ccsUsers.ToArray(),

                        };
                        client.Send2(oMessage);
                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, sendToUser.SID, sendToUser.Mail,
                                                                       this.IncidentID, this.ProcessID);

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
    private void SendFirstNotification(Incident cIncident, string[] emailCss = null)
    {
        try
        {


            var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                            Request.ServerVariables["HTTP_HOST"],
                                            String.Format("procId={0}&incId={1}", ProcessID, IncidentID));
            var process = CurrentProcess;
            var userAssigned = SarsUser.GetADUser(cIncident.AssignedToSID.Trim());

            var subjectH = string.Format("{0} Ref : {1}", process.Description, cIncident.ReferenceNumber);

            if (Request.PhysicalApplicationPath != null)
            {
                var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-assigned.htm");
                var tempate = File.ReadAllText(templateDir);

                if (cIncident.DueDate != null)
                {
                    var body = string.Format(tempate,
                                             userAssigned.FullName,
                                             cIncident.Summary,
                                             cIncident.ReferenceNumber,
                                             cIncident.DueDate.Value.ToString("yyyy-MM-dd"),
                                             cIncident.IncidentStatus,
                                             incidentURL,
                                             txtCreatedBy.Value
                        );

                    using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                    {
                        var sendToUser = SarsUser.GetADUser(AssignedToSID.SelectedAdUserDetails.SID);


                        var oMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = "IncidentTracking@sars.gov.za",
                            Body = body,
                            IsBodyHtml = true,
                            Subject = subjectH,
                            To = new[] { sendToUser.Mail },
                            CC = emailCss,

                        };
                        client.Send2(oMessage);
                        IncidentTrackingManager.SaveIncidentEmailLog(body, subjectH, sendToUser.SID, sendToUser.Mail,
                                                                       this.IncidentID, this.ProcessID);
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }
    private void SendFirstNotificationToProcOwners(Incident cIncident)
    {
        try
        {


            var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                            Request.ServerVariables["HTTP_HOST"],
                                            String.Format("procId={0}&incId={1}", this.ProcessID, this.IncidentID));
            var process = CurrentProcess;
            var userAssigned = SarsUser.GetADUser(cIncident.AssignedToSID.Trim());

            var subject = string.Format("{0} Ref : {1}", process.Description, cIncident.ReferenceNumber);

            if (Request.PhysicalApplicationPath != null)
            {
                var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-assigned-procowner.htm");
                var tempate = File.ReadAllText(templateDir);

                if (cIncident.DueDate != null)
                {


                    using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                    {

                        foreach (var processOwner in CurrentProcess.Owners)
                        {
                            var owner = SarsUser.GetADUser(processOwner.OwnerSID);
                            var body = string.Format(tempate,
                                                     owner.FullName,
                                                     cIncident.Summary,
                                                     cIncident.ReferenceNumber,
                                                     cIncident.DueDate.Value.ToString("yyyy-MM-dd"),
                                                     cIncident.IncidentStatus,
                                                     incidentURL,
                                                     userAssigned.FullName,
                                                     txtCreatedBy.Value);


                            client.Send1("IncidentTracking@sars.gov.za", owner.Mail, subject, body);
                            IncidentTrackingManager.SaveIncidentEmailLog(body, subject, owner.SID, owner.Mail,
                                                                     this.IncidentID, this.ProcessID);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void NotifyGroupOfPeople(Incident cIncident, string sid, string emailTo, string content)
    {
        try
        {
            var process = CurrentProcess;
            var subject = string.Format("{0}", process.Description);

            if (Request.PhysicalApplicationPath != null)
            {
                var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails",
                    "incident-notifyGroupOfPeople.htm");
                var tempate = File.ReadAllText(templateDir);

                using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                {
                    var body = string.Format(tempate, emailTo, content);
                    var sendToUser = SarsUser.GetADUser(sid);
                    if (sendToUser != null)
                    {
                        client.Send1("IncidentTracking@sars.gov.za", sendToUser.Mail, subject, body);
                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, sendToUser.SID, sendToUser.Mail,
                            this.IncidentID, this.ProcessID);
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void SendAcceptanceEmail(Incident cIncident, string[] emailCss = null)
    {
        try
        {


            var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                            Request.ServerVariables["HTTP_HOST"],
                                            String.Format("procId={0}&incId={1}", ProcessID, IncidentID));
            var process = CurrentProcess;
            var userAssigned = SarsUser.GetADUser(cIncident.AssignedToSID.Trim());

            var subjectH = string.Format("{0} Ref : {1}", process.Description, cIncident.ReferenceNumber);

            if (Request.PhysicalApplicationPath != null)
            {
                var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-assigned-accepted.htm");
                var tempate = File.ReadAllText(templateDir);
                var sendToUser = SarsUser.GetADUser(cIncident.CreatedBySID);

                if (cIncident.DueDate != null)
                {
                    var body = string.Format(tempate,
                                             sendToUser.FullName,
                                             cIncident.Summary,
                                             cIncident.ReferenceNumber,
                                             cIncident.DueDate.Value.ToString("yyyy-MM-dd"),
                                             cIncident.IncidentStatus,
                                             incidentURL,
                                             txtCreatedBy.Value
                        );

                    using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                    {


                        var oMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = "IncidentTracking@sars.gov.za",
                            Body = body,
                            IsBodyHtml = true,
                            Subject = subjectH,
                            To = new[] { sendToUser.Mail },
                            CC = emailCss,


                        };

                        client.Send2(oMessage);
                        IncidentTrackingManager.SaveIncidentEmailLog(body, subjectH, sendToUser.SID, sendToUser.Mail,
                                                                       this.IncidentID, this.ProcessID);
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }
    private void SendToCreaterWithCommentAdded()
    {
        try
        {

            CurrentIncidentDetails = CurrentIncident;
            List<string> ccsUsers = GetPeopleToBeEmailed();

            var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}", ProcessID, IncidentID));

            var process = CurrentProcess;
            var userAssigned = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID.Trim());
            var creator = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID.Trim());
            ccsUsers.Add(userAssigned.Mail);
            var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.ReferenceNumber);

            if (Request.PhysicalApplicationPath != null)
            {
                var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-assigned-accepted-notes.htm");
                var tempate = File.ReadAllText(templateDir);

                if (CurrentIncidentDetails.DueDate != null)
                {
                    var body = string.Format(tempate,
                                             userAssigned.FullName,
                                              CurrentIncidentDetails.Summary,
                                             CurrentIncidentDetails.ReferenceNumber,
                                             CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd"),
                                             CurrentIncidentDetails.IncidentStatus,
                                             incidentURL,
                                             txtNotes.Text,
                                             creator.FullName);

                    using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                    {
                        var sendToUser = SarsUser.GetADUser(CurrentIncident.CreatedBySID);


                        var oMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = "IncidentTracking@sars.gov.za",
                            Body = body,
                            IsBodyHtml = true,
                            Subject = subject,
                            To = new[] { sendToUser.Mail },
                            CC = ccsUsers.ToArray(),

                        };
                        client.Send2(oMessage);
                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, sendToUser.SID, sendToUser.Mail,
                                                                       this.IncidentID, this.ProcessID);

                        //foreach (var processOwner in CurrentProcess.Owners)
                        //{
                        //    var owner = SarsUser.GetADUser(processOwner.OwnerSID);
                        //    client.Send1("IncidentTracking@sars.gov.za", owner.Mail, subject, body);
                        //    IncidentTrackingManager.SaveIncidentEmailLog(body, subject, owner.SID, owner.Mail, IncidentID, ProcessID);

                        //}
                    }
                }
            }
        }
        catch (Exception)
        {


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
    protected void NewNote(object sender, EventArgs e)
    {
        btnAddNote.Enabled = true;
        txtNotes.Enabled = true;
        btnNew.Enabled = false;
    }
    protected void ViewDocuments(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(CurrentIncident.AssignedToSID))
        {
            //tbContainer.ActiveTabIndex = 0;
            MessageBox.Show("Save your incident details before adding your documents ");
            return;
        }
        var btn = sender as LinkButton;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvWorkInfo.SelectRow(row.RowIndex);
                if (gvWorkInfo.SelectedDataKey != null)
                {
                    var workInfoId = gvWorkInfo.SelectedDataKey["WorkInfoId"];
                    Response.Redirect(String.Format("~/SurveyWizard/AttachDocuments.aspx?procId={0}&incId={1}&noteId={2}", ProcessID, IncidentID, workInfoId));
                }
            }
        }
    }
    protected void AddNote(object sender, EventArgs e)
    {
        CurrentIncidentDetails = CurrentIncident;
        // treeNotes.SelectedNode.Expanded = false;
        var workInfo = new WorkInfoDetails
        {
            AddedBySID = SarsUser.SID,
            IncidentId = Convert.ToDecimal(CurrentIncidentDetails.IncidentID),
            ProcessId = Convert.ToInt32(CurrentIncidentDetails.ProcessId),
            Notes = txtNotes.Text
        };
        if (IncidentTrackingManager.AddWorkInfo(workInfo) > 0)
        {
            var details = CurrentIncident;
            if (details.IncidentStatusId == 2)
            {
                const int statusId = 3; //WIP

                if (CurrentIncidentDetails.AssignedToSID != null && CurrentIncidentDetails.AssignedToSID.ToLower().Equals(SarsUser.SID.ToLower()))
                {
                    IncidentTrackingManager.UpdateIncidentStatus(statusId, IncidentID);
                    List<string> ccsUsers = GetPeopleToBeEmailed();
                    SendAcceptanceEmail(CurrentIncidentDetails, ccsUsers.ToArray());
                }
            }
            LoadInfo();
            SendToCreaterWithCommentAdded();

            txtNotes.Clear();

            // MessageBox.Show("Work Info addedd successfully.");

        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {

        treeNotes.SelectedNode.Expand();
        //if (treeNotes.SelectedNode.Expanded != true)
        //{

        //    treeNotes.SelectedNode.Expanded = true;
        //}
        //else
        //{
        //    treeNotes.SelectedNode.Collapse();
        //    treeNotes.SelectedNode.Expanded = false;
        //}

    }
}