using Sars.Systems.Data;
using Sars.Systems.Security;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_NormalUserLandingPage : IncidentTrackingPage
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
            return;
        }
        if (!IsPostBack)
        {
            BindGridView();
            //ddlStatuses.Bind(IncidentTrackingManager.GetIncidentsStatuses(), "Description", "IncidentStatusId");
        }
        if (CurrentProc != null)
        {
            var procAdmins = CurrentProc.Administrators;
            if (procAdmins != null && procAdmins.Any())
            {
                if (procAdmins.Find(admin => admin.SID.Equals(SarsUser.SID, StringComparison.CurrentCultureIgnoreCase)) != null)
                {
                    Toolbar1.Items[1].Visible = true;
                    Toolbar1.Items[2].Visible = true;
                }
                else
                {
                    Toolbar1.Items[1].Visible = false;
                    Toolbar1.Items[2].Visible = false;
                }
            }
        }
    }

    private void BindGridView()
    {
        var incidents = new List<Incident>();
        Guid roleId;
        var userRole = this.Page.User.GetRole();
        incidents = IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID, 0);
        switch (userRole)
        {
            case "Administrator Head - Top Secret":
                roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);                
                BindGridView(userRole,incidents, roleId);
                break;
            case "Administrator Manager - Secret":
                // no access to HeadTopScret
                roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                BindGridView(userRole, incidents, roleId);
                break;
            case "Administrator - confidential":
                roleId = new Guid(ConfigurationManager.AppSettings["AdministratorConfidential"]);
                BindGridView(userRole, incidents, roleId);
                break;
            case "System User":
                roleId = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
                BindGridView(userRole,incidents, roleId);
                break;
            case "Developer":                
                roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                BindGridView(userRole, incidents, roleId);
                break;
            default:
                break;
        }
        BindTeamIncidents(userRole);      

    }
    private void BindTeamIncidents(string userRole)
    {
        grdTeamIncidents.Bind(null);
        if (CurrentProc.CanWorkOnOneCase)
        {
            var secret = new Guid(ConfigurationManager.AppSettings["AdministratorManagerSecret"]);
            var  topSecret = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
            var systemUser = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
            tabTeamIncidents.Visible = true;
            var teamincidents = IncidentTrackingManager.GetProcessIncidents(ProcessID);
            if (teamincidents != null && teamincidents.Any())
            {
                switch (userRole)
                {
                    case "Administrator Head - Top Secret":
                        grdTeamIncidents.Bind(
                        teamincidents.FindAll(
                         inc => inc.AssignedToSID == null ||
                              inc.AssignedToSID.ToLower() != SarsUser.SID.ToLower() &&
                             inc.IncidentStatusId != 5));
                        break;
                    case "Administrator Manager - Secret":                       
                        grdTeamIncidents.Bind(teamincidents.FindAll(
                        inc => inc.AssignedToSID == null ||
                             inc.AssignedToSID.ToLower() != SarsUser.SID.ToLower() &&
                            inc.IncidentStatusId != 5 && inc.RoleId != topSecret));
                        break;
                    case "Administrator - confidential":                       
                        grdTeamIncidents.Bind(teamincidents.FindAll(
                        inc => inc.AssignedToSID == null ||
                             inc.AssignedToSID.ToLower() != SarsUser.SID.ToLower() &&
                            inc.IncidentStatusId != 5 && inc.RoleId != topSecret && inc.RoleId != secret));
                        break;
                    case "System User":
                       
                        grdTeamIncidents.Bind(teamincidents.FindAll(
                         inc => inc.AssignedToSID == null ||
                              inc.AssignedToSID.ToLower() != SarsUser.SID.ToLower() &&
                             inc.IncidentStatusId != 5 && inc.RoleId == systemUser));
                        break;
                    case "Developer":
                        grdTeamIncidents.Bind(
                      teamincidents.FindAll(
                       inc => inc.AssignedToSID == null ||
                            inc.AssignedToSID.ToLower() != SarsUser.SID.ToLower() &&
                           inc.IncidentStatusId != 5));
                        break;
                    default:
                        break;
                }
            }           
        }
    }

    private void BindGridView(string userRole, List<Incident> inc, Guid roleId)
    {
        var topSecret = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
        var secret = new Guid(ConfigurationManager.AppSettings["AdministratorManagerSecret"]);
        
        gvIncidents.Bind(null);
        if(inc != null)
        {
           
            switch (userRole)
            {
                case "Administrator Head - Top Secret":
                    gvIncidents.Bind(inc.FindAll(i => i.ProcessId == Convert.ToInt64(ProcessID)));
                    break;
                case "Administrator Manager - Secret":
                    gvIncidents.Bind(inc.FindAll(i => i.ProcessId == Convert.ToInt64(ProcessID) && i.RoleId != roleId));
                    break;
                case "Administrator - confidential":
                    gvIncidents.Bind(inc.FindAll(i => i.ProcessId == Convert.ToInt64(ProcessID) &&  (i.RoleId != topSecret && i.RoleId != secret)));
                    break;
                case "System User":
                    gvIncidents.Bind(inc.FindAll(i => i.ProcessId == Convert.ToInt64(ProcessID) && i.RoleId == roleId));
                    break;
                case "Developer":
                    gvIncidents.Bind(inc.FindAll(i => i.ProcessId == Convert.ToInt64(ProcessID)));
                    break;
                default:
                    break;
            }
        }
    }
    private void BindGridView(string search)
    {
        var incidents = IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID, 0);
        if (incidents != null && incidents.Any())
        {
            if (string.IsNullOrEmpty(txtMyIncidents.Text))
            {
                gvIncidents.Bind(
                    incidents.FindAll(
                        inc =>
                            inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId != 1 &&
                            inc.IncidentStatusId != 6));
            }
            else
            {
                var incident = incidents.FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId != 1 &&
                            inc.IncidentStatusId != 6);
                var table = incident.FindAll(my => my.Summary.ToLower().Contains(txtMyIncidents.Text.ToLower())
                    || my.ReferenceNumber.ToLower().Contains(txtMyIncidents.Text.ToLower())
                        || my.IncidentStatus.ToLower().Contains(txtMyIncidents.Text.ToLower()));

                gvIncidents.Bind(table);
            }
        }
        else
        {
            gvIncidents.Bind(null);
        }
        if (CurrentProc.CanWorkOnOneCase)
        {
            tabTeamIncidents.Visible = true;
            var teamincidents = IncidentTrackingManager.GetProcessIncidents(ProcessID);
            if (teamincidents != null && teamincidents.Any())
            {
                if (string.IsNullOrEmpty(search))
                {
                    grdTeamIncidents.Bind(
                        teamincidents.FindAll(
                            inc =>
                                inc.IncidentStatusId > 1 && inc.AssignedToSID.ToLower() != SarsUser.SID.ToLower() &&
                                inc.IncidentStatusId != 6));
                }
                else
                {
                    var incident = teamincidents.FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId != 1 &&
                                inc.IncidentStatusId != 6 && inc.AssignedToSID.ToLower() != SarsUser.SID.ToLower());
                    var table = incident.FindAll(my => my.Summary.ToLower().Contains(search.ToLower())
                        || my.ReferenceNumber.ToLower().Contains(search.ToLower())
                            || my.IncidentStatus.ToLower().Contains(search.ToLower()));

                    grdTeamIncidents.Bind(table);
                }

            }
            else
            {
                grdTeamIncidents.Bind(null);
            }
        }
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "AddNewIncident":
                {
                    Response.Redirect(string.Format("InitIncident.aspx?procId={0}", ProcessID));
                    break;
                }
            case "Search":
                {
                    var returnUrl = Request.Url.PathAndQuery;

                    Response.Redirect(string.Format("SearchHome.aspx?procId={0}&returnUrl={1}", ProcessID, returnUrl));
                    break;
                }
            case "Reports":
                {
                    Response.Redirect(String.Format("~/Reports/Default.aspx?procId={0}", ProcessID));
                    break;
                }
            case "Back":
                {
                    Response.Redirect(String.Format("~/Default.aspx?procId={0}", ProcessID));
                    break;
                }
            case "ViewIncident":
                {
                    if (gvIncidents.SelectedIndex == -1)
                    {
                        if (gvIncidents.Rows.Count == 1)
                        {
                            gvIncidents.SelectedIndex = 0;
                        }
                        else
                        {
                            MessageBox.Show("Please click on an incident to select it before you can continue.");
                            return;
                        }
                    }
                    if (gvIncidents.SelectedDataKey != null)
                    {
                        var prodessId = gvIncidents.SelectedDataKey["ProcessId"].ToString();
                        var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                        if (!string.IsNullOrEmpty(prodessId) && !string.IsNullOrEmpty(incidentId))
                        {
                            Response.Redirect(String.Format("RegisterUserIncident.aspx?procId={0}&incId={1}", prodessId, incidentId));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not create new incident.");
                    }
                    break;
                }
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

                        CurrentIncidentDetails = IncidentTrackingManager.GetIncidentById(IncId);
                        AddNote();
                        SendFirstNotification();
                        SendFirstNotificationToProcOwners();
                        txtIncidentDueDate.Enabled = false;
                        txtNotes.Enabled = false;
                        const int statusId = 2;
                        IncidentTrackingManager.UpdateIncidentStatus(statusId, IncId);

                        BindGridView();
                        txtNotes.Clear();


                        MessageBox.Show(string.Format("Incident was re-assigned to {0}", adUser.FullName));
                    }
                    else
                    {
                        MessageBox.Show("Incident not assigned.");
                    }
                    break;
                }
        }
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
                                            String.Format("procId={0}&incId={1}", ProcessID, IncId));
            //  CurrentIncidentDetails = CurrentIncident;
            List<string> ccsUsers = GetPeopleToBeEmailed(IncId);
            var process = CurrentProcess;
            var userAssigned = SarsUser.GetADUser(UserSelector2.SelectedAdUserDetails.SID);
            var createdby = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID);
            var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.ReferenceNumber);

            if (Request.PhysicalApplicationPath != null)
            {
                var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-re-assigned.htm");
                var tempate = File.ReadAllText(templateDir);
                using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                {

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
                                               string.Format("{0} | {1}", createdby.FullName, createdby.SID)
                            );
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
                                                                        CurrentIncidentDetails.IncidentID.ToString(), CurrentIncidentDetails.ProcessId.ToString());
                    }
                }
            }
        }
        catch (Exception)
        {
            // throw;// ex.Message;
        }
    }
    private void SendFirstNotificationToProcOwners()
    {
        try
        {


            var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                            Request.ServerVariables["HTTP_HOST"],
                                            String.Format("procId={0}&incId={1}", ProcessID, IncId));
            // CurrentIncidentDetails = CurrentIncident;
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
                            if (owner != null)
                            {
                                var body = string.Format(tempate,
                                    owner.FullName,
                                    CurrentIncidentDetails.ReferenceNumber,
                                    CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd hh:mm"),
                                    CurrentIncidentDetails.Summary,
                                    CurrentIncidentDetails.IncidentStatus,
                                    incidentURL,
                                    txtNotes.Text,
                                    string.Format("{0} | {1}", SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).FullName,
                                        SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).SID));

                                client.Send1("IncidentTracking@sars.gov.za", owner.Mail, subject, body);
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
    protected void AddNote()
    {

        var workInfo = new WorkInfoDetails
        {
            AddedBySID = SarsUser.SID,
            IncidentId = Convert.ToDecimal(CurrentIncidentDetails.IncidentID),
            ProcessId = Convert.ToInt32(CurrentIncidentDetails.ProcessId),
            Notes = txtNotes.Text
        };
        if (IncidentTrackingManager.AddWorkInfo(workInfo) > 0)
        {
            var details = CurrentIncidentDetails;
            if (details.IncidentStatusId == 2)
            {
                const int statusId = 3;
                IncidentTrackingManager.UpdateIncidentStatus(statusId, CurrentIncidentDetails.IncidentID.ToString());
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
        var summary = DataBinder.Eval(e.Row.DataItem, "Summary");
        if (summary != null)
        {

            e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
            e.Row.Attributes.Add("title",
                                 "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Incident Summary</font></b>] body=[<font color='red'><b>" +
                                 summary + "</b></font>]");
        }


        //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        var dueDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "DueDate"));
        var incidentStatusId = DataBinder.Eval(e.Row.DataItem, "IncidentStatusId");
        var now = DateTime.Now;
        e.Row.Attributes["class"] = "slakept";

        if (incidentStatusId.Equals(2) || incidentStatusId.Equals(3))
        {
            if ((dueDate.Year == now.Year && dueDate.Month == now.Month && dueDate.Day == now.Day) && dueDate.Ticks < now.Ticks)
            {
                e.Row.Attributes["class"] = "slaabouttobeviolated";
            }
            else if (dueDate < now)
            {
                e.Row.Attributes["class"] = "slaviolated";

            }
        }

        var tdReAssign = e.Row.FindControl("bntReAssign") as Button;
        var tdComplete = e.Row.FindControl("btnComplete") as Button;
        var tdClose = e.Row.FindControl("btnClose") as Button;
        var tdReOpen = e.Row.FindControl("btnReOpen") as Button;
        var tdCopy = e.Row.FindControl("tdCopy") as Button;
        if (tdReAssign != null)
        {

            var IncidentStatus = DataBinder.Eval(e.Row.DataItem, "IncidentStatus");
            if (incidentStatusId.Equals(4) || incidentStatusId.Equals(5) || IncidentStatus.Equals("Finalised"))
            {
                tdReAssign.Visible = false;
            }
            else
            {
                tdReAssign.Visible = true;
            }
        }
        if (tdComplete != null)
        {

            var IncidentStatus = DataBinder.Eval(e.Row.DataItem, "IncidentStatus");
            if (incidentStatusId.Equals(4) || incidentStatusId.Equals(5) || IncidentStatus.Equals("Finalised"))
            {
                tdComplete.Visible = false;
                tdReOpen.Visible = true;
            }


            if (tdClose != null)
            {
                tdClose.Visible = incidentStatusId.Equals(4);
            }
            if (tdReOpen != null)
            {
                tdReOpen.Visible = incidentStatusId.Equals(4) || incidentStatusId.Equals(5) || IncidentStatus.Equals("Return to originator");
                if (tdCopy != null)
                {
                    tdCopy.Visible = false;
                }
            }

        }
        if (CurrentProcess.ReAssignToCreater && incidentStatusId.Equals(4))
        {
            tdClose.Visible = false;
            tdReOpen.Visible = false;
            e.Row.Cells[4].Text = "Awaiting Finalisation";

        }
    }
    protected void PageChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncidents.PageIndex = e.NewPageIndex;
        grdTeamIncidents.PageIndex = e.NewPageIndex;
        var incidents = IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID, 0);
        if (incidents != null && incidents.Any())
        {
            gvIncidents.Bind(incidents.FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId > 1 && inc.IncidentStatusId < 6));
        }
        else
        {
            gvIncidents.Bind(null);
        }
        if (CurrentProc.CanWorkOnOneCase)
        {
            tabTeamIncidents.Visible = true;
            var teamincidents = IncidentTrackingManager.GetProcessIncidents(ProcessID);
            if (teamincidents != null && teamincidents.Any())
            {
                grdTeamIncidents.Bind(teamincidents.FindAll(inc => inc.IncidentStatusId > 1 && inc.AssignedToSID != SarsUser.SID && inc.IncidentStatusId < 6));
            }
            else
            {
                grdTeamIncidents.Bind(null);
            }
        }

    }


    protected void ChangeAssignee(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvIncidents.SelectRow(row.RowIndex);

                if (gvIncidents.SelectedDataKey != null)
                {
                    var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                    Response.Redirect(String.Format("ChangeAssignee.aspx?ProcId={0}&IncId={1}", ProcessID, incidentId));
                }
            }
        }
    }
    protected void CompleteIncident(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvIncidents.SelectRow(row.RowIndex);

                if (gvIncidents.SelectedDataKey != null)
                {
                    var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                    Response.Redirect(String.Format("CompleteIncident.aspx?ProcId={0}&IncId={1}", ProcessID, incidentId));
                }
            }
        }
    }
    protected void CloseIncident(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var row = btn.Parent.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvIncidents.SelectRow(row.RowIndex);

                if (gvIncidents.SelectedDataKey != null)
                {
                    var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                    Response.Redirect(String.Format("CloseIncident.aspx?ProcId={0}&IncId={1}", ProcessID, incidentId));
                }
            }
        }
    }
    protected void OpenIncident(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvIncidents.SelectRow(row.RowIndex);

                if (gvIncidents.SelectedDataKey != null)
                {
                    var prodessId = gvIncidents.SelectedDataKey["ProcessId"].ToString();
                    var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                    if (!string.IsNullOrEmpty(prodessId) && !string.IsNullOrEmpty(incidentId))
                    {
                        Response.Redirect(String.Format("RegisterUserIncident.aspx?procId={0}&incId={1}", prodessId, incidentId));
                    }
                }
            }
        }
    }
    protected void ReOpenIncident(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var row = btn.Parent.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvIncidents.SelectRow(row.RowIndex);

                if (gvIncidents.SelectedDataKey != null)
                {
                    var prodessId = gvIncidents.SelectedDataKey["ProcessId"].ToString();
                    var incidentId = gvIncidents.SelectedDataKey["IncidentID"].ToString();
                    if (!string.IsNullOrEmpty(prodessId) && !string.IsNullOrEmpty(incidentId))
                    {
                        Response.Redirect(String.Format("ReOpenIncident.aspx?procId={0}&incId={1}", prodessId, incidentId));
                    }
                }
            }
        }
    }
    protected void gvIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string pageName;
        var incidentId = e.CommandArgument.ToString();
        switch (e.CommandName)
        {

            case "View_Incident":
                {
                    pageName = "RegisterUserIncident";
                    Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}", pageName, ProcessID, incidentId));
                    break;
                }
            case "Reassigne_Incident":
                {
                    //  pageName = "ChangeAssignee";
                    BindAssignScreen(incidentId);
                    txtNotes.Enabled = true;
                    UserSelector2.Clear();
                    mpAllocate.Show();
                    break;
                }
            case "Complete_Incident":
                {
                    pageName = "CompleteIncident";
                    Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}", pageName, ProcessID, incidentId));
                    break;
                }
            case "Close_Incident":
                {
                    pageName = "CloseIncident";
                    Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}", pageName, ProcessID, incidentId));
                    break;
                }
            case "Reopen_Incident":
                {
                    pageName = "ReOpenIncident";
                    Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}", pageName, ProcessID, incidentId));
                    break;
                }
            case "Copy_Incident":
                {
                    pageName = "RegisterUserIncident";
                    CopyIncident(incidentId, pageName);
                    Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}", pageName, ProcessID, incidentId));
                    break;

                }
            default:
                {
                    // MessageBox.Show("Cant execute option selected.");
                    return;
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
            IncidentNumber.Text = CurrentIncidentDetails.ReferenceNumber;
            // IncidentStatus1.Text = CurrentIncidentDetails.IncidentStatus;
            // Timestamp.Text = CurrentIncidentDetails.AssignedToFullName;
            var adUser = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID);
            UserSelector1.SelectedAdUserDetails = new SelectedUserDetails
            {
                SID = adUser.SID,
                FoundUserName =
                    string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                FullName = adUser.FullName
            };
            UserSelector1.Disable();
            // txtSummary.SetValue(CurrentIncidentDetails.Summary);
            //if (!string.IsNullOrEmpty(CurrentIncidentDetails.Summary))
            //{
            //   // txtSummary.Attributes.Add("title",
            //                              "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Incident Summary</font></b>] body=[<font color='red'><b>" +
            //                              CurrentIncidentDetails.Summary + "</b></font>]");
            //}
        }
    }
    private void CopyIncident(string incidentId, string navigateTo)
    {
        var createdBy = SarsUser.SID;
        var incidnetToCopy = incidentId;
        string incidentNumber;
        string newIncidentId;


        IncidentTrackingManager.CopyIncident(incidnetToCopy, createdBy, ProcessID, out incidentNumber, out newIncidentId);

        if (!string.IsNullOrEmpty(incidentNumber) && !string.IsNullOrEmpty(newIncidentId))
        {
            Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}&cpd=1&oldIncId={3}", navigateTo, ProcessID, newIncidentId, incidnetToCopy));
        }
    }


    protected void btnTeamSearch_Click(object sender, EventArgs e)
    {

        BindGridView(txtTeamSearch.Text);
    }

    protected void gvSearchIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string pageName = string.Empty;
        var incidentId = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "Reassigne_Incident":
                {
                    BindAssignScreen(incidentId);

                    mpAllocate.Show();
                    //mdlSearch.Show();
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
}