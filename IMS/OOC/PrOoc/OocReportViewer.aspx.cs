using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using Sars.Systems.Data;
using Sars.Systems.Extensions;
using Sars.Systems.Security;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.IO;
using System.Activities.Expressions;

public partial class PrOoc_OocReportViewer : IncidentTrackingPage
{
    protected IncidentProcess CurrentProc;
    protected Incident CurrentIncidentDetails;
    public string IncId
    {
        get { return ViewState["incId"] as string; }
        set { ViewState["incId"] = value; }
    }
    private DataTable Export
    {
        get { return ViewState["Export"] as DataTable; }
        set { ViewState["Export"] = value; }
    }

    double total = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
               
        if (!IsPostBack)
        {           
            LoadProcesses();
            LoadDashBoard();
            lblProcessDescription.Text = Request["reportname"] + " Report";
            
        }
    }


    private void LoadProcesses()
    {
        var processes =
            new IncidentProcess("[dbo].[uspRead_UserProcesses]",
                                new Dictionary<string, object>
                                    {
                                        {"@UserSID", SarsUser.SID}
                                    }).GetRecords<IncidentProcess>().Where(p => p.ProcessId == OocInternalProcessId ||
                                         p.ProcessId == OocExternalProcessId).ToList();



        if (processes != null && processes.Any())
        {
            if (processes.Count >= 1)
            {             
                drpProcess.DataSource = processes;
                drpProcess.DataTextField = "Description";
                drpProcess.DataValueField = "ProcessId";
                drpProcess.DataBind();
                drpProcess.Items.Insert(0, new ListItem("Select Process", "0"));               
                return;
            }
            if (processes.Count == 1)
            {
                var proc = processes[0];
                // Response.Redirect(string.Format("NormalUserLandingPage.aspx?procId={0}", proc.ProcessId));
            }
            // Response.Redirect("../NormalUserDefault.aspx");
        }
        else
        {
            Response.Redirect("../NormalUserDefault.aspx");
        }
    }
     

    private void LoadDashBoard()
    {

        try
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

        }
        catch (Exception)
        {

            // throw;
        }
    }


    protected void ExportData()
    {

        DateTime testDate;
        if (!DateTime.TryParse(txtDate.Text, out testDate))
        {
            MessageBox.Show("Start Date formart is invalid, mut be (yyyy-MM-dd).");
            return;
        }
        if (!DateTime.TryParse(txtEndDate.Text, out testDate))
        {
            MessageBox.Show("End Date formart is invalid, mut be (yyyy-MM-dd.");
            return;
        }
        if (drpProcess.SelectedIndex < 0)
        {
            MessageBox.Show("Please select a Process");
            return;
        }
        try
        {
            var data = IncidentTrackingManager.GetProcessReportBystatus(drpProcess.SelectedValue, string.Empty);
            DataTable tblFiltered = new DataTable();
            if (!string.IsNullOrEmpty(txtDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                var userRole = this.Page.User.GetRole();
                var secret = new Guid(ConfigurationManager.AppSettings["AdministratorManagerSecret"]);
                var topSecret = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                var systemUser = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
                var tblRow = new DataTable().AsEnumerable();
                switch (userRole)
                {
                    case "Administrator Head - Top Secret":
                        var roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                    .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                     && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text));
                        break;
                    case "Administrator Manager - Secret":
                        // no access to HeadTopScret
                        roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                    .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                     && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text) && row.Field<Guid>("RoleId") != roleId);
                        break;
                    case "Administrator - confidential":
                        roleId = new Guid(ConfigurationManager.AppSettings["AdministratorConfidential"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                    .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                     && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text)
                                       && (row.Field<Guid>("RoleId") != topSecret && row.Field<Guid>("RoleId") != secret));
                        break;
                    case "System User":
                        roleId = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                  .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                   && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text)
                                     && (row.Field<Guid>("RoleId") == roleId));
                        break;
                    case "Developer":
                        tblRow = data.Tables[0].AsEnumerable()
                                       .Where(row => row.Field<DateTime>("Date Registered") >= Convert.ToDateTime(txtDate.Text)
                                        && row.Field<DateTime>("Date Registered") <= Convert.ToDateTime(txtEndDate.Text));
                        break;
                    default:
                        break;
                }

                if (tblRow.Any())
                {
                    tblFiltered = tblRow.CopyToDataTable();
                }
                gvReports.Bind(tblFiltered);
                Export = tblFiltered;
                tblFiltered.ToExcel(null, null);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }

    protected void GetData()
    {
        gvReports.Bind(null);
        total = 0;
        DateTime testDate;
        if (drpProcess.SelectedIndex <= 0)
        {
            MessageBox.Show("Please select a process name");
            return;
        }

        if (!DateTime.TryParse(txtDate.Text, out testDate))
        {
            MessageBox.Show("Start Date formart is invalid, mut be (yyyy-MM-dd).");
            return;
        }
        //if (!DateTime.TryParse(txtEndDate.Text, out testDate))
        //{
        //    MessageBox.Show("End Date formart is invalid, mut be (yyyy-MM-dd.");
        //    return;
        //}

        if (drpProcess.SelectedIndex < 0)
        {
            MessageBox.Show("Please select a Process");
            return;
        }
        try
        {
            var data = IncidentTrackingManager.GetOococessReportBystatus(drpProcess.SelectedValue, string.Empty);
            var tblRow = new DataTable().AsEnumerable();
            DataTable tblFiltered = new DataTable();
            switch (drpReports.SelectedValue)
            {
                case "irq01":
                case "irq02":
                    tblRow = data.Tables[0].AsEnumerable()
                                  .Where(row => row.Field<DateTime>("DueDate") != null &&( row.Field<DateTime>("DueDate").ToString("yyyy-MM-dd") == Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd")));
                    if (tblRow.Any())
                    {
                        tblFiltered = tblRow.CopyToDataTable();
                        //lblReportName.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("No incidents are due on " + txtDate.Text);
                        return;
                    }
                    break;
                case "irq03":
                    tblRow = data.Tables[0].AsEnumerable()
                                  .Where(row => row.Field<DateTime?>("SLADate") != null
                                  && ((row.Field<DateTime?>("SLADate") < DateTime.Today )));
                    if (tblRow.Any())
                    {
                        tblFiltered = tblRow.CopyToDataTable();
                       // lblReportName.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("No Escalated Cases are still Open  ");
                        return;
                    }
                    break;
                case "irq04":
                    tblRow = data.Tables[0].AsEnumerable()
                                  .Where(row => row.Field<string>("Commissionercomments") != null
                                  && ((row.Field<string>("Commissionercomments").Contains(txtFreeText.Text)) ));
                    if (tblRow.Any())
                    {
                        tblFiltered = tblRow.CopyToDataTable();
                       // lblReportName.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("No Comments made by the Commissioner/ Minister/ DCs/ CoS like :   " + txtFreeText.Text);
                        return;
                    }
                    break;
                case "irq05":
                    tblRow = data.Tables[0].AsEnumerable()
                                  .Where(row => row.Field<string>("Division") != null
                                  && ((row.Field<string>("Division").Contains(txtFreeText.Text))));
                    if (tblRow.Any())
                    {
                        tblFiltered = tblRow.CopyToDataTable();
                        //lblReportName.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("No Submissions per divisions/units like :   " + txtFreeText.Text);
                        return;
                    }
                    break;
                case "irq06":
                    tblRow = data.Tables[0].AsEnumerable()
                                  .Where(row =>   row.Field<string>("Comments") != null
                                  && ((row.Field<string>("Comments").Contains(txtFreeText.Text))));
                    if (tblRow.Any())
                    {
                        tblFiltered = tblRow.CopyToDataTable();
                        //lblReportName.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("No Feedback provided to business areas / regions like :   " + txtFreeText.Text);
                        return;

                    }
                    break;
                case "irq07":
                    tblRow = data.Tables[0].AsEnumerable()
                                  .Where(row => row.Field<string>("Requesttype") != null 
                                  && ((row.Field<string>("Requesttype").Contains(txtFreeText.Text))));
                    if (tblRow.Any())
                    {
                        tblFiltered = tblRow.CopyToDataTable();
                        //lblReportName.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("No Referred requests as per business area / DCs:   " + txtFreeText.Text);
                    }
                    break;
                case "irq08":
                    tblRow = data.Tables[0].AsEnumerable()
                                  .Where(row => row.Field<string>("Correspondenceactionto") != null
                                  && ((row.Field<string>("IncidentStatus").Contains("Assigned") ||
                                  ((row.Field<string>("IncidentStatus").Contains("Work In Progress"))))));
                    if (tblRow.Any())
                    {
                        tblFiltered = tblRow.CopyToDataTable();
                        // lblReportName.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("No Referred matters i.e., enterprise committee found!  ");
                        return;
                    }
                    break;
                case "irq09":
                    tblRow = data.Tables[0].AsEnumerable()
                                  .Where(row => row.Field<string>("TaxTypes") != null 
                                  && ((row.Field<string>("TaxTypes").Contains(txtFreeText.Text))));
                    if (tblRow.Any())
                    {
                        tblFiltered = tblRow.CopyToDataTable();
                       // lblReportName.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Query and Tax types:   " + txtFreeText.Text);
                    }
                    break;
                default:
                    break;
            }            
            
            
                var userRole = this.Page.User.GetRole();
                var secret = new Guid(ConfigurationManager.AppSettings["AdministratorManagerSecret"]);
                var topSecret = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                var systemUser = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
               
                switch (userRole)
                {
                    case "Administrator Head - Top Secret":
                        var roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                        tblRow = tblFiltered.AsEnumerable();
                        break;
                    case "Administrator Manager - Secret":
                        // no access to HeadTopScret
                        roleId = new Guid(ConfigurationManager.AppSettings["AdministratorHeadTopSecret"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                    .Where(row => row.Field<Guid>("RoleId") != roleId);
                        break;
                    case "Administrator - confidential":
                        roleId = new Guid(ConfigurationManager.AppSettings["AdministratorConfidential"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                    .Where(row =>  row.Field<Guid>("RoleId") != secret);
                        break;
                    case "System User":
                        roleId = new Guid(ConfigurationManager.AppSettings["SystemUser"]);
                        tblRow = data.Tables[0].AsEnumerable()
                                  .Where(row => row.Field<Guid>("RoleId") == roleId);
                        break;
                    case "Developer":
                        tblRow = data.Tables[0].AsEnumerable();
                        break;
                    default:
                        break;
                }
                if (tblRow.Any())
                {
                    tblFiltered = tblRow.CopyToDataTable();
                    gvReports.Bind(tblFiltered);
                lblTotal.Text = "Total: " + tblFiltered.Rows.Count.ToString();
                    Export = tblFiltered;
                }                
                
            
        }
        catch (Exception ex)
        {

            MessageBox.Show(ex.Message);
        }
    }

    protected void gvReports_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (!String.IsNullOrEmpty(Request["stsId"]))
        {
            var data = Export;
            gvReports.NextPage(data, e.NewPageIndex);
        }
    }

 
    protected void Toolbar2_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
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
                                var userSid = UserSelector2.SID.Trim();
                                SarsUser.SaveUser(userSid);
                                var recordsAffected = IncidentTrackingManager.AddUserToAProcess(userSid, drpProcess.SelectedValue, "1");
                            }
                            catch (Exception)
                            {
                            }
                        }

                        CurrentIncidentDetails = IncidentTrackingManager.GetIncidentById(IncId);
                       var currentProcess =  IncidentTrackingManager.GetIncidentProcess(CurrentIncidentDetails.ProcessId.ToString());
                        AddNote();
                        SendFirstNotification();
                        SendFirstNotificationToProcOwners();
                        txtIncidentDueDate.Enabled = false;
                        txtNotes.Enabled = false;
                        const int statusId = 2;
                        IncidentTrackingManager.UpdateIncidentStatus(statusId, IncId);

                        GetData();
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
                                            String.Format("procId={0}&incId={1}", CurrentIncidentDetails.ProcessId, IncId));
            //  CurrentIncidentDetails = CurrentIncident;
            List<string> ccsUsers = GetPeopleToBeEmailed(IncId);
            var currentProcess = IncidentTrackingManager.GetIncidentProcess(CurrentIncidentDetails.ProcessId.ToString());
            var userAssigned = SarsUser.GetADUser(UserSelector2.SelectedAdUserDetails.SID);
            var createdby = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID);
            var subject = string.Format("{0} Ref : {1}", currentProcess.Description, CurrentIncidentDetails.ReferenceNumber);

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
                                                                        CurrentIncidentDetails.IncidentID.ToString(), drpProcess.SelectedValue.ToString());
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
                                            String.Format("procId={0}&incId={1}", drpProcess.SelectedValue, IncId));
            // CurrentIncidentDetails = CurrentIncident;
            var process = IncidentTrackingManager.GetIncidentProcess(CurrentIncidentDetails.ProcessId.ToString());
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
            ProcessId = Convert.ToInt32(drpProcess.SelectedValue),
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

    protected void drpReports_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(drpReports.SelectedIndex >  0)
        {
            trEndDate.Visible = false;
            trStartDate.Visible = false;
            trFreeText.Visible = false;
            switch (drpReports.SelectedValue)
            {
                case "96":
                    break;
                case "97":
                    break;
                case "120":
                    break;
                case "irq01":

                    txtDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                    txtStartDate.Text = "Due Date: ";                   
                    trStartDate.Visible = true;
                    lblProcessDescription.Text = "Cases due today Report";
                    lblReportName.Text = "Cases due today Report";
                    break;
                case "irq02":
                    txtDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtStartDate.Text = "Due Date: ";
                    trStartDate.Visible = true;
                    lblProcessDescription.Text = "Cases Due Tomorrow Report";
                   lblReportName.Text = "Cases Due Tomorrow Report";
                    break;
                case "irq03":
                    txtDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtStartDate.Text = "Due Date: ";                  
                    lblProcessDescription.Text = "Escalated Cases still Open Report";
                   lblReportName.Text = "Escalated Cases still Open Report";
                    break;
                case "irq04":
                    txtDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtStartDate.Text = "Due Date: ";                   
                    trFreeText.Visible = true;
                    lblFreeText.Text = "Comments made by the Commissioner/ Minister/ DCs/ CoS";
                    lblProcessDescription.Text = "Comments made by the Commissioner/ Minister/ DCs/ CoS";
                    lblReportName.Text = "Comments made by the Commissioner/ Minister/ DCs/ CoS";
                    break;
                case "irq05":
                    txtDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtStartDate.Text = "Due Date: ";                  
                    trFreeText.Visible = true;
                    lblFreeText.Text = "Submissions per divisions/units";
                    lblProcessDescription.Text = "Submissions per divisions/units";
                    lblReportName.Text = "Submissions per divisions/units";
                    break;
                case "irq06":
                    txtDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtStartDate.Text = "Due Date: ";                    
                    trFreeText.Visible = true;
                    lblFreeText.Text = "Feedback provided to business areas / regions";
                    lblProcessDescription.Text = "Feedback provided to business areas / regions";
                    lblReportName.Text = "Feedback provided to business areas / regions";
                    break;
                case "irq07":
                    txtDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtStartDate.Text = "Due Date: ";                    
                    trFreeText.Visible = true;
                    lblFreeText.Text = "Referred requests as per business area / DCs";
                    lblProcessDescription.Text = "Referred requests as per business area / DCs";
                    lblReportName.Text = "Referred requests as per business area / DCs";
                    break;
                case "irq08":
                    txtDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtStartDate.Text = "Due Date: ";
                    lblProcessDescription.Text = "Escalated Cases still Open Report";
                     lblReportName.Text = "Escalated Cases still Open Report";
                    break;
                case "irq09":
                    txtDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                    txtStartDate.Text = "Due Date: ";                    
                    trFreeText.Visible = true;
                    lblFreeText.Text = "Query and Tax types";
                    lblProcessDescription.Text = "Query and Tax types";
                    lblReportName.Text = "Query and Tax types";
                    break;
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
            if (adUser != null)
            {
                try
                {
                    UserSelector1.SelectedAdUserDetails = new SelectedUserDetails
                    {
                        SID = adUser.SID,
                        FoundUserName =
                                        string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                        FullName = adUser.FullName
                    };
                    UserSelector1.Disable();
                }
                catch (Exception)
                {

                    //throw;
                }
            }

           
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

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }        
        var dueDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "DueDate"));
        var incidentStatusId = DataBinder.Eval(e.Row.DataItem, "IncidentStatusId");

        var tdReAssign = e.Row.FindControl("bntReAssign") as Button;
        var tdComplete = e.Row.FindControl("btnComplete") as Button;
        var tdClose = e.Row.FindControl("btnClose") as Button;
        var tdReOpen = e.Row.FindControl("btnReOpen") as Button;
        var tdCopy = e.Row.FindControl("tdCopy") as Button;
        if (tdReAssign != null)
        {

            var IncidentStatus = DataBinder.Eval(e.Row.DataItem, "IncidentStatus");
            if (incidentStatusId.Equals(4) || incidentStatusId.Equals(5) )
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
            if (incidentStatusId.Equals(4) || incidentStatusId.Equals(5) )
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
                tdReOpen.Visible = incidentStatusId.Equals(4) || incidentStatusId.Equals(5);
                if (tdCopy != null)
                {
                    tdCopy.Visible = false;
                }
            }

        }

        if(e.Row.RowType == DataControlRowType.DataRow)
                
        {
            // Assuming the column index for the total is 6
            
            for (int i = 0; i < gvReports.Rows.Count; i++)
            {
                double value;
                if (double.TryParse(gvReports.Rows[i].Cells[0].Text, out value))
                {
                    total += value;
                }
            }            
        }
        
    }

    protected void PageChanging(object sender, GridViewPageEventArgs e)
    {
        gvReports.PageIndex = e.NewPageIndex;
        //grdTeamIncidents.PageIndex = e.NewPageIndex;
        //var incidents = IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID, 0);
        //if (incidents != null && incidents.Any())
        //{
        //    gvIncidents.Bind(incidents.FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId > 1 && inc.IncidentStatusId < 6));
        //}
        //else
        //{
        //    gvIncidents.Bind(null);
        //}
        //if (CurrentProc.CanWorkOnOneCase)
        //{
        //    tabTeamIncidents.Visible = true;
        //    var teamincidents = IncidentTrackingManager.GetProcessIncidents(ProcessID);
        //    if (teamincidents != null && teamincidents.Any())
        //    {
        //        grdTeamIncidents.Bind(teamincidents.FindAll(inc => inc.IncidentStatusId > 1 && inc.AssignedToSID != SarsUser.SID && inc.IncidentStatusId < 6));
        //    }
        //    else
        //    {
        //        grdTeamIncidents.Bind(null);
        //    }
        //}

    }

    protected void gvIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string pageName;
        var incidentId = e.CommandArgument.ToString();
        switch (e.CommandName)
        {

            case "View_Incident":
                {
                    pageName = "../Admin/RegisterUserIncident";
                    Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}", pageName, drpProcess.SelectedValue, incidentId));
                    break;
                }
            case "Reassigne_Incident":
                {
                    pageName = "ChangeAssignee";
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
                    //CopyIncident(incidentId, pageName);
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        gvReports.Bind(null);
        lblTotal.Clear();
        GetData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportData();
    }

    protected void btnReAssign_Click(object sender, EventArgs e)
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
                    var userSid = UserSelector2.SID.Trim();
                    SarsUser.SaveUser(userSid);
                    var recordsAffected = IncidentTrackingManager.AddUserToAProcess(userSid, drpProcess.SelectedValue, "1");
                }
                catch (Exception)
                {
                }
            }

            CurrentIncidentDetails = IncidentTrackingManager.GetIncidentById(IncId);
            var currentProcess = IncidentTrackingManager.GetIncidentProcess(CurrentIncidentDetails.ProcessId.ToString());
            AddNote();
            SendFirstNotification();
            SendFirstNotificationToProcOwners();
            txtIncidentDueDate.Enabled = false;
            txtNotes.Enabled = false;
            const int statusId = 2;
            IncidentTrackingManager.UpdateIncidentStatus(statusId, IncId);

            GetData();
            txtNotes.Clear();


            MessageBox.Show(string.Format("Incident was re-assigned to {0}", adUser.FullName));
        }
        else
        {
            MessageBox.Show("Incident not assigned.");
        }
    }
}