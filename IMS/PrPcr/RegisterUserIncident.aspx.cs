using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP;

public partial class PrPcr_RegisterUserIncident : IncidentTrackingPage
{
    protected Incident CurrentIncidentDetails;
    protected IncidentProcess CurrentProcessDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["name"]))
        {
            if (Request["name"].Equals("s2022311"))
            {
                CreateProcessField(ProcessID);
                CreateTenderStageOnePreapration(ProcessID);
                CreateTenderStageTwoPreapration(ProcessID);
                CreateRfqFields(ProcessID);
                CreateCondonationFields(ProcessID);
                CreateDeviationFields(ProcessID);
                CreateExpansionAndVariationFields(ProcessID);
                CreateTransversalFields(ProcessID);
            }
        }
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
        if (CurrentIncidentDetails == null)
        {
            Response.Redirect("../InvalidProcessOrIncident.aspx");
            return;
        }
        if (!IsPostBack)
        {
            SystemReferenceNo.Text = CurrentIncidentDetails.ReferenceNumber;
            SystemReferenceNo.Enabled = false;
                
                if (CurrentIncidentDetails != null && CurrentIncidentDetails.DueDate != null)
                {
                    DueDate.SetValue(CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd"));
                }

                if (CurrentIncidentDetails != null && !string.IsNullOrEmpty(CurrentIncidentDetails.AssignedToSID))
                {
                    if (!CurrentIncidentDetails.AssignedToSID.Equals(SarsUser.SID, StringComparison.CurrentCultureIgnoreCase))
                    {
                       
                        return;
                    }
                    if (CurrentIncidentDetails.IncidentStatusId != 2 && CurrentIncidentDetails.IncidentStatusId != 3)
                    {
                        //Response.Redirect(String.Format("~/PrOoc/IncidentRealOnly.aspx?procId={0}&incId={1}&msgId=10", ProcessID,
                        //                                IncidentID));
                        //return;
                        tbContainer.Enabled = false;
                    }
                }
           
          //  PopulateDropdownList(ProcessID);
                if (ProcurementCategory.SelectedIndex > 0)
                {
                    ShowHidePanels(ProcurementCategory.SelectedIndex);
                }
            if (CurrentIncidentDetails != null && !string.IsNullOrEmpty(CurrentIncidentDetails.IncidentStatus))
            {
                if (CurrentIncidentDetails.IncidentStatusId > 1)
                {
                    LoadInfo();
                    var adUser = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID);
                    ActionPerson.SelectedAdUserDetails = new SelectedUserDetails
                    {
                        SID = adUser.SID,
                        FoundUserName =
                            string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                        FullName = adUser.FullName
                    };
                    //ActionPerson.Disable();
                   
                   
                }
                var currUser = SarsUser.GetADUser(SarsUser.SID);
                SeniorManager.SelectedAdUserDetails = new SelectedUserDetails
                {
                    SID = currUser.SID,
                    FoundUserName =
                        string.Format("{0} | {1}", currUser.FullName, currUser.SID),
                    FullName = currUser.FullName
                };
            }
            
        }
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        CurrentIncidentDetails = CurrentIncident;
        var processId = Request.QueryString["procId"];
        if (processId == null) return;
        var admin =this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("tbDetails").FindControl("UpdatePanel1") as Control ;
        var TenderStage1Preparation = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("TenderStage1Preparation").FindControl("ReqInfoProposal1") as Control;
        var TenderStage2Preparation = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("TenderStage2Preparation").FindControl("ReqInfoProposalStage21") as Control;
        var Condonation = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("Condonation").FindControl("Condonation1") as Control;
        var Deviation = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("Deviation").FindControl("Deviation1") as Control;
        var ExpansionVariation = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("ExpansionVariation").FindControl("ExpansionVariation1") as Control;
        var Transversal = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("Transversal").FindControl("Transversal1") as Control;
        var RFQ = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("RFQ").FindControl("RFQ1") as Control;
       
        switch (e.CommandName)
        {
            case "Submit":
            {
                var actionPerson = (admin_userselector_ascx)admin.FindControl("ActionPerson");
                if (actionPerson != null)
                {
                    if (actionPerson.SID == null || string.IsNullOrEmpty(actionPerson.SID))
                    {
                        MessageBox.Show("Action Person can not be empty.");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(Subject.Text))
                {
                    MessageBox.Show("Subject is required.");
                    return;
                }
               
                if (string.IsNullOrEmpty(DueDate.Text))
                {
                    MessageBox.Show("Due Date is required.");
                    return;
                }
                DateTime testDate;
                if (!DateTime.TryParse(DueDate.Text, out testDate))
                {
                    tbContainer.ActiveTabIndex = 0;
                    MessageBox.Show("Due Date formart is invalid, mut be (yyyy-mm-dd hh:ss).");
                    return;
                }
                if (CurrentIncidentDetails.DueDate == null)
                {
                    if (Convert.ToDateTime(DueDate.Text) <= DateTime.Now)
                    {
                        DueDate.Focus();
                        MessageBox.Show(
                            string.Format(
                                "Incident Due Date must be greater than Incident Registration Date [{0}]",
                                CurrentIncidentDetails.Timestamp.ToString("yyyy-MM-dd HH:mm")));
                        return;
                    }
                }
                SaveAdminForm(processId, admin);
                SaveTabForms(processId, TenderStage1Preparation);
                SaveTabForms(processId, TenderStage2Preparation);
                SaveTabForms(processId, Condonation);
                SaveTabForms(processId, Deviation);
                SaveTabForms(processId, Transversal);
                SaveTabForms(processId, ExpansionVariation);
                SaveTabForms(processId, RFQ);

                if (CurrentIncidentDetails.IncidentStatusId == 1)
                    {
                        SarsUser.SaveUser(ActionPerson.SID);
                        var numrecords =
                            IncidentTrackingManager.UpdateIncidentDetails
                                (
                                    Convert.ToDateTime(DueDate.Text),
                                    ActionPerson.SID.Trim(),
                                    IncidentID,
                                   "Incident Has been Assigned"
                                );
                        if (numrecords > 0)
                        {
                            CurrentIncidentDetails = CurrentIncident;
                            SendFirstNotification(CurrentIncidentDetails);
                            SendFirstNotificationToProcOwners(CurrentIncidentDetails);
                        }
                    }
                MessageBox.Show("An Incident has been created succesfully.");
                Response.Redirect(String.Format("~/PrPcr/NormalUserLandingPage.aspx?procId={0}", ProcessID));
                break;
            }
            case "Cancel":
            {
                Response.Redirect(String.Format("~/PrPcr/NormalUserLandingPage.aspx?procId={0}", ProcessID));
                break;
            }
            case "Add":
            {
                break;
            }
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
    protected void gvDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Request.PhysicalApplicationPath != null)
        {
            var directory = Path.Combine(Request.PhysicalApplicationPath, "Downloads");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var files = new DirectoryInfo(directory).GetFiles();// Directory.GetFiles(directory);
            if (files.Any())
            {
                var oldFiles = files.ToList().FindAll(fi => fi.CreationTime < DateTime.Now.AddHours(-1));
                if (oldFiles.Any())
                {
                    try
                    {
                        foreach (var t in oldFiles)
                        {
                            File.Delete(t.FullName);
                        }
                    }
                    catch (Exception)
                    {
                     
                    }
                }
            }
            if (e.CommandName.Equals("OpenFile", StringComparison.CurrentCultureIgnoreCase))
            {
                var response = Response;
                var docId = e.CommandArgument;
                IncidentTrackingManager.OpenFile(directory, docId, response);
            }
            if (e.CommandName.Equals("DeleteFile", StringComparison.CurrentCultureIgnoreCase))
            {
                var docId = e.CommandArgument.ToString();
                var deleted = IncidentTrackingManager.DeleteFile(docId);

                if (deleted <= 0) return;
              
                MessageBox.Show("File deleted successfully");
            }
        }
    }
    private void PopulateDropdownList(string processId)
    {
        var processFields = IncidentTrackingManager.GetProcessField(processId);
        var ntrl = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("tbDetails").FindControl("UpdatePanel1");
        processFields = IncidentTrackingManager.GetProcessField(processId);
        if (processFields != null)
        {
            foreach (Control childControls in ntrl.Controls)
            {
                foreach (var cntrl in childControls.Controls)
                {
                    if (cntrl is DropDownList)
                    {
                        var drpBox = (DropDownList)cntrl;
                        var methodReceived = processFields.Find(x => x.FieldName == drpBox.ID);
                        BindDropdownList(drpBox, methodReceived.FieldTypeId, methodReceived.FieldId);
                    }
                }
            }
        }
    }
    private void LoadInfo()
    {
       
      
        var TenderStage1Preparation = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("TenderStage1Preparation").FindControl("ReqInfoProposal1") as Control;
        var TenderStage2Preparation = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("TenderStage2Preparation").FindControl("ReqInfoProposalStage21") as Control;
        var Condonation = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("Condonation").FindControl("Condonation1") as Control;
        var Deviation = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("Deviation").FindControl("Deviation1") as Control;
        var ExpansionVariation = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("ExpansionVariation").FindControl("ExpansionVariation1") as Control;
        var Transversal = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("Transversal").FindControl("Transversal1") as Control;
        var RFQ = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("RFQ").FindControl("RFQ1") as Control;


        PopulateAdminTab();
        PopulateCustomControls(TenderStage1Preparation);
        PopulateCustomControls(TenderStage2Preparation);
        PopulateCustomControls(Condonation);
        PopulateCustomControls(Deviation);
        PopulateCustomControls(ExpansionVariation);
        PopulateCustomControls(Transversal);
        PopulateCustomControls(RFQ);
    }
    private void PopulateAdminTab()
    {
        var ntrl = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("tbDetails").FindControl("UpdatePanel1");
        var data = IncidentTrackingManager.ReadOocRecordSet(IncidentID);
        if (data != null && data.HasRows)
        {
            foreach (DataRow answer in data.Tables[0].Rows)
            {
                foreach (Control childControls in ntrl.Controls)
                {
                    foreach (var cntrl in childControls.Controls)
                    {
                        if (cntrl is TextBox)
                        {
                            var textBox = cntrl as TextBox;
                            if (answer["FieldName"].ToString() == textBox.ID)
                            {
                                textBox.Text = answer["Answer"].ToString();
                            }
                        }
                        else if (cntrl is DropDownList)
                        {
                            var dropBox = cntrl as DropDownList;
                            if (answer["FieldName"].ToString() == dropBox.ID)
                            {
                                dropBox.SelectItemByValue(answer["Answer"].ToString());
                            }
                        }
                        else if (cntrl is CheckBox)
                        {
                            var checkBox = cntrl as CheckBox;
                            if (answer["FieldName"].ToString() == checkBox.ID)
                            {
                                checkBox.Checked = answer["Answer"].ToString().Equals("Yes") ? true : false;
                            }
                        }
                        else if (cntrl is RadioButtonList)
                        {
                            var checkBox = cntrl as RadioButtonList;
                            if (answer["FieldName"].ToString() == checkBox.ID)
                            {
                                checkBox.SelectItemByValue(answer["Answer"].ToString());
                            }
                        }
                        else if (cntrl is admin_userselector_ascx)
                        {
                            var userAd = cntrl as admin_userselector_ascx;
                            if (answer["FieldName"].ToString() == userAd.ID)
                            {
                                if (!string.IsNullOrEmpty(answer["Answer"].ToString()))
                                {
                                    var adUser = SarsUser.GetADUser(answer["Answer"].ToString());
                                    userAd.SelectedAdUserDetails = new SelectedUserDetails
                                    {
                                        SID = adUser.SID,
                                        FoundUserName =
                                            string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                                        FullName = adUser.FullName
                                    };
                                }
                            }
                        }
                    }
                }
            }
            if (ProcurementCategory.SelectedIndex > 0)
            {
                ShowHidePanels(ProcurementCategory.SelectedIndex);
            }
        }
    }
    private void PopulateCustomControls(Control cControl)
    {
        var ntrl = cControl;
        var data = IncidentTrackingManager.ReadOocRecordSet(IncidentID);
        if (data != null && data.HasRows)
        {
            string description = string.Empty;
            foreach (DataRow answer in data.Tables[0].Rows)
            {
                foreach (Control cntrl in ntrl.Controls)
                {
                    if (cntrl is TextBox)
                    {
                        var textBox = cntrl as TextBox;
                        if (answer["FieldName"].ToString() == textBox.ID)
                        {
                            textBox.Text = answer["Answer"].ToString();
                        }
                    }
                    else if (cntrl is DropDownList)
                    {
                        var dropBox = cntrl as DropDownList;
                        if (answer["FieldName"].ToString() == dropBox.ID)
                        {
                            dropBox.SelectItemByValue(answer["Answer"].ToString());
                        }
                    }
                    else if (cntrl is CheckBox)
                    {
                        var checkBox = cntrl as CheckBox;
                        if (answer["FieldName"].ToString() == checkBox.ID)
                        {
                            checkBox.Checked = answer["Answer"].ToString().Equals("Yes") ? true : false;
                        }
                    }
                    else if (cntrl is RadioButtonList)
                    {
                        var checkBox = cntrl as RadioButtonList;
                        if (answer["FieldName"].ToString() == checkBox.ID)
                        {
                            checkBox.SelectItemByValue(answer["Answer"].ToString());
                        }
                    }
                    else if (cntrl is admin_userselector_ascx)
                    {
                        var userAd = cntrl as admin_userselector_ascx;
                        if (answer["FieldName"].ToString() == userAd.ID)
                        {
                            if (!string.IsNullOrEmpty(answer["Answer"].ToString()))
                            {
                                var adUser = SarsUser.GetADUser(answer["Answer"].ToString());
                                userAd.SelectedAdUserDetails = new SelectedUserDetails
                                {
                                    SID = adUser.SID,
                                    FoundUserName =
                                        string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                                    FullName = adUser.FullName
                                };
                            }
                        }
                    }
                }
            }
        }
    }
    protected void BindDropdownList(DropDownList dropDownList, int fieldType, decimal fieldId)
    {
        var options = IncidentTrackingManager.GetQuestionTypeAnswers(fieldType, fieldId, 0);
        if (options != null)
        {
            if (options.HasRows)
            {
                dropDownList.Bind(options, "OptionDescription", "MultichoiceOptionId");
            }
        }
    }
    private void SendFirstNotification(Incident cIncident)
    {

        var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}", ProcessID, IncidentID));
        var process = CurrentProcess;
        var userAssigned = SarsUser.GetADUser(cIncident.AssignedToSID.Trim());

        var subject = string.Format("{0} Ref : {1}", process.Description, cIncident.IncidentNumber);

        if (Request.PhysicalApplicationPath != null)
        {
            var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-assigned.htm");
            var tempate = File.ReadAllText(templateDir);

            if (cIncident.DueDate != null)
            {
                var body = string.Format(tempate,
                                         userAssigned.FullName,
                                         cIncident.Summary,
                                         cIncident.IncidentNumber,
                                         cIncident.DueDate.Value.ToString("yyyy-MM-dd"),
                                         cIncident.IncidentStatus,
                                         incidentURL
                    );

                using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                {
                    var sendToUser = SarsUser.GetADUser(ActionPerson.SelectedAdUserDetails.SID);
                    if (sendToUser != null)
                    {
                        client.Send1("IncidentTracking@sars.gov.za", sendToUser.Mail, subject, body);
                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, sendToUser.SID, sendToUser.Mail,
                                                                     this.IncidentID, this.ProcessID);
                    }
                }
            }
        }
    }
    private void SendFirstNotificationToProcOwners(Incident cIncident)
    {

        var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}", this.ProcessID, this.IncidentID));
        var process = CurrentProcess;
        var userAssigned = SarsUser.GetADUser(cIncident.AssignedToSID.Trim());

        var subject = string.Format("{0} Ref : {1}", process.Description, cIncident.IncidentNumber);

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
                                                 cIncident.IncidentNumber,
                                                 cIncident.DueDate.Value.ToString("yyyy-MM-dd"),
                                                 cIncident.IncidentStatus,
                                                 incidentURL,
                                                 userAssigned.FullName);


                        client.Send1("IncidentTracking@sars.gov.za", owner.Mail, subject, body);
                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, owner.SID, owner.Mail,
                                                                 this.IncidentID, this.ProcessID);
                    }
                }
            }
        }
    }
    protected void ProcurementCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ProcurementCategory.SelectedIndex > 0)
        {
            ShowHidePanels(ProcurementCategory.SelectedIndex);
        }
    }
    private void ShowHidePanels(int index)
    {
        TenderStage1Preparation.Visible = false;
        TenderStage2Preparation.Visible = false;
        Deviation.Visible = false;
        ExpansionVariation.Visible = false;
        Condonation.Visible = false;
        Transversal.Visible = false;
        RFQ.Visible = false;
        switch (index)
        {
            case 1:
                RFQ.Visible = true;
                break;
            case 2:
                TenderStage1Preparation.Visible = true;
                TenderStage2Preparation.Visible = true;
                break;
            case 3:
                TenderStage1Preparation.Visible = true;
                TenderStage2Preparation.Visible = true;
                break;
            case 4:
                Deviation.Visible = true;
                break;
            case 5:
                ExpansionVariation.Visible = true;
                break;
            case 6:
                Condonation.Visible = true;
                break;
            case 7:
                Transversal.Visible = true;
                break;
        } 
    }
    private void SaveAdminForm(string processId, Control controls)
    {
        var processFields = IncidentTrackingManager.GetProcessField(processId);
        if (processFields != null)
        {
            foreach (var field in processFields)
            {
                switch (field.FieldTypeId)
                {
                    case 5:
                        {
                            var chkbox = controls.FindControl(field.FieldName) as CheckBox;
                            if (chkbox != null)
                            {
                                IncidentTrackingManager.SavePcrAnswers(IncidentID, processId, field.FieldId,
                                    field.FieldTypeId, chkbox.Checked ? "Yes" : "No");
                            }
                            break;
                        }
                    case 6:

                        var textEntry = controls.FindControl(field.FieldName) as TextBox;
                        if (textEntry != null)
                        {
                            IncidentTrackingManager.SavePcrAnswers(IncidentID, processId, field.FieldId,
                                field.FieldTypeId, textEntry.Text);
                        }
                        break;
                    case 16:
                        var lstSelected = (RadioButtonList)controls.FindControl(field.FieldName);
                        if (lstSelected != null)
                        {
                            IncidentTrackingManager.SavePcrAnswers(IncidentID, processId, field.FieldId,
                                field.FieldTypeId, lstSelected.SelectedValue);
                        }
                        break;
                    case 17:
                        var drpSelected = (DropDownList)controls.FindControl(field.FieldName);
                        if (drpSelected != null)
                        {
                            IncidentTrackingManager.SavePcrAnswers(IncidentID, processId, field.FieldId,
                                field.FieldTypeId, drpSelected.SelectedValue);
                        }
                        break;
                    case 22:
                        var userSelector = (admin_userselector_ascx)controls.FindControl(field.FieldName);
                        if (userSelector != null)
                        {
                            if (userSelector.SID != null && !string.IsNullOrEmpty(userSelector.SID))
                            {
                                SarsUser.SaveUser(userSelector.SID);
                                IncidentTrackingManager.SavePcrAnswers(IncidentID, processId, field.FieldId,
                                    field.FieldTypeId, userSelector.SID);
                            }
                            else
                            {
                                IncidentTrackingManager.SavePcrAnswers(IncidentID, processId, field.FieldId,
                                    field.FieldTypeId, string.Empty);
                            }
                        }
                        break;
                }
            }
        }
    }
    private void SaveTabForms(string processId, Control control)
    {
        var processFields = IncidentTrackingManager.GetProcessField(processId);
        if (processFields != null)
        {
            foreach (var field in processFields)
            {

                foreach (Control controls in control.Controls)
                {
                    switch (field.FieldTypeId)
                    {
                        case 5:
                            {
                                if (controls.ID == field.FieldName)
                                {
                                    var chkbox = (CheckBox)controls;
                                    IncidentTrackingManager.SavePcrAnswers(IncidentID, processId, field.FieldId,
                                            field.FieldTypeId, chkbox.Checked ? "Yes" : "No");
                                }
                                break;
                            }
                        case 6:
                            if (controls.ID == field.FieldName)
                            {
                                var textEntry = (TextBox)controls;
                                IncidentTrackingManager.SavePcrAnswers(IncidentID, processId, field.FieldId,
                                        field.FieldTypeId, textEntry.Text);
                            }
                            break;
                        case 16:
                            if (controls.ID == field.FieldName)
                            {
                                var lstSelected = (RadioButtonList)controls;
                                IncidentTrackingManager.SavePcrAnswers(IncidentID, processId, field.FieldId,
                                        field.FieldTypeId, lstSelected.SelectedValue);
                            }
                            break;
                        case 17:
                            if (controls.ID == field.FieldName)
                            {
                                var drpSelected = controls as DropDownList;
                                if (drpSelected != null)
                                {
                                    IncidentTrackingManager.SavePcrAnswers(IncidentID, processId, field.FieldId,
                                        field.FieldTypeId, drpSelected.SelectedValue);
                                }
                            }
                            break;
                        case 22:
                            if (controls.ID == field.FieldName)
                            {
                                var userSelector = (admin_userselector_ascx)controls;
                                if (userSelector.SID != null && !string.IsNullOrEmpty(userSelector.SID))
                                {
                                    SarsUser.SaveUser(userSelector.SID);
                                    IncidentTrackingManager.SavePcrAnswers(IncidentID, processId, field.FieldId,
                                        field.FieldTypeId, userSelector.SID);
                                }
                                else
                                {
                                    IncidentTrackingManager.SavePcrAnswers(IncidentID, processId, field.FieldId,
                                        field.FieldTypeId, string.Empty);
                                }
                            }
                            break;
                    }
                }

            }
        }
    }
    protected void CreateProcessField(string processId)
    {
        var data = IncidentTrackingManager.GetIncidentProcess(processId);
        if (data == null)
        {
            MessageBox.Show("The Process does not exist, please contact the system adminitrator.");
            return;
        }
        var processFields = IncidentTrackingManager.GetProcessField(processId);
        var ntrl = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("tbDetails").FindControl("UpdatePanel1");
        processFields = IncidentTrackingManager.GetProcessField(processId);

        foreach (Control childControls in ntrl.Controls)
        {
            foreach (var cntrl in childControls.Controls)
            {
                if (cntrl is TextBox)
                {
                    var textBox = (TextBox)cntrl;
                    var fieldName = processFields != null ? processFields.Find(x => x.FieldName == textBox.ID) : null;
                    if (fieldName == null)
                    {
                        IncidentTrackingManager.CreateProcessFields(textBox.ID, textBox.ID, processId, 6);
                    }
                }
                else if (cntrl is DropDownList)
                {
                    var drpBox = (DropDownList)cntrl;
                    var fieldName = processFields != null ? processFields.Find(x => x.FieldName == drpBox.ID) : null;
                    if (fieldName == null)
                    {
                        IncidentTrackingManager.CreateProcessFields(drpBox.ID, drpBox.ID, processId, 17);
                    }
                }
                else if (cntrl is RadioButtonList)
                {
                    var chkBox = (RadioButtonList)cntrl;
                    var fieldName = processFields != null ? processFields.Find(x => x.FieldName == chkBox.ID) : null;
                    if (fieldName == null)
                    {
                        IncidentTrackingManager.CreateProcessFields(chkBox.ID, chkBox.ID, processId, 16);
                    }
                }
                else if (cntrl is admin_userselector_ascx)
                {
                    var UseAD = (admin_userselector_ascx)cntrl;
                    var fieldName = processFields != null ? processFields.Find(x => x.FieldName == UseAD.ID) : null;
                    if (fieldName == null)
                    {
                        IncidentTrackingManager.CreateProcessFields(UseAD.ID, UseAD.ID, processId, 22);
                    }
                }
            }
        }

    }
    protected void CreateTenderStageOnePreapration(string processId)
    {
        var data = IncidentTrackingManager.GetIncidentProcess(processId);
        if (data == null)
        {
            MessageBox.Show("The OOC Process does not exist, please contact the system adminitrator.");
            return;
        }
        var processFields = IncidentTrackingManager.GetProcessField(processId);
        var ntrl = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("TenderStage1Preparation");

        string description = string.Empty;
        foreach (Control childControls in ntrl.Controls)
        {
            foreach (var _cntrl in childControls.Controls)
            {
                if (_cntrl is prpcr_reqinfoproposal_ascx)
                {
                    var stage1cntrl = _cntrl as Control;
                    foreach (var cntrl in stage1cntrl.Controls)
                    {
                        if (cntrl is LiteralControl)
                        {
                            var text = (LiteralControl)cntrl;
                            if (!string.IsNullOrEmpty(text.Text))
                            {
                                int endopeniLngTag = text.Text.LastIndexOf("</td>", StringComparison.CurrentCulture);
                                if (endopeniLngTag > -1)
                                {
                                    text.Text = text.Text.Remove(endopeniLngTag);
                                }
                                int startingIndex = text.Text.LastIndexOf(">", StringComparison.CurrentCulture);
                                text.Text = text.Text.Remove(0, startingIndex + 1);
                                description = HttpUtility.HtmlDecode(text.Text);
                            }
                        }
                        if (cntrl is TextBox)
                        {
                            var textBox = (TextBox)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == textBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(textBox.ID, description, processId, 6);
                            }
                        }
                        else if (cntrl is DropDownList)
                        {
                            var drpBox = (DropDownList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == drpBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(drpBox.ID, description, processId, 17);
                            }
                        }
                        else if (cntrl is RadioButtonList)
                        {
                            var chkBox = (RadioButtonList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == chkBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(chkBox.ID, description, processId, 16);
                            }
                        }
                        else if (cntrl is admin_userselector_ascx)
                        {
                            var UseAD = (admin_userselector_ascx)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == UseAD.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(UseAD.ID, UseAD.ID, processId, 22);
                            }
                        }
                    }
                }
            }
        }
    }
    protected void CreateTenderStageTwoPreapration(string processId)
    {
        var data = IncidentTrackingManager.GetIncidentProcess(processId);
        if (data == null)
        {
            MessageBox.Show("The Process does not exist, please contact the system adminitrator.");
            return;
        }
        var processFields = IncidentTrackingManager.GetProcessField(processId);
        var ntrl = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("TenderStage2Preparation");

        string description = string.Empty;
        foreach (Control childControls in ntrl.Controls)
        {
            foreach (var _cntrl in childControls.Controls)
            {
                if (_cntrl is prpcr_reqinfoproposalstage2_ascx)
                {
                    var stage1cntrl = _cntrl as Control;
                    foreach (var cntrl in stage1cntrl.Controls)
                    {
                        if (cntrl is LiteralControl)
                        {
                            var text = (LiteralControl)cntrl;
                            if (!string.IsNullOrEmpty(text.Text))
                            {
                                int endopeniLngTag = text.Text.LastIndexOf("</td>", StringComparison.CurrentCulture);
                                if (endopeniLngTag > -1)
                                {
                                    text.Text = text.Text.Remove(endopeniLngTag);
                                }
                                int startingIndex = text.Text.LastIndexOf(">", StringComparison.CurrentCulture);
                                text.Text = text.Text.Remove(0, startingIndex + 1);
                                description = HttpUtility.HtmlDecode(text.Text);
                            }
                        }
                        if (cntrl is TextBox)
                        {
                            var textBox = (TextBox)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == textBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(textBox.ID, description, processId, 6);
                            }
                        }
                        else if (cntrl is DropDownList)
                        {
                            var drpBox = (DropDownList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == drpBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(drpBox.ID, description, processId, 17);
                            }
                        }
                        else if (cntrl is RadioButtonList)
                        {
                            var chkBox = (RadioButtonList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == chkBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(chkBox.ID, description, processId, 16);
                            }
                        }
                        else if (cntrl is admin_userselector_ascx)
                        {
                            var UseAD = (admin_userselector_ascx)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == UseAD.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(UseAD.ID, UseAD.ID, processId, 22);
                            }
                        }
                    }
                }
            }
        }
    }
    protected void CreateRfqFields(string processId)
    {
        var data = IncidentTrackingManager.GetIncidentProcess(processId);
        if (data == null)
        {
            MessageBox.Show("The Process does not exist, please contact the system adminitrator.");
            return;
        }
        var processFields = IncidentTrackingManager.GetProcessField(processId);
        var ntrl = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("RFQ");

        string description = string.Empty;
        foreach (Control childControls in ntrl.Controls)
        {
            foreach (var _cntrl in childControls.Controls)
            {
                if (_cntrl is prpcr_rfq_ascx)
                {
                    var stage1cntrl = _cntrl as Control;
                    foreach (var cntrl in stage1cntrl.Controls)
                    {
                        if (cntrl is LiteralControl)
                        {
                            var text = (LiteralControl)cntrl;
                            if (!string.IsNullOrEmpty(text.Text))
                            {
                                int endopeniLngTag = text.Text.LastIndexOf("</td>", StringComparison.CurrentCulture);
                                if (endopeniLngTag > -1)
                                {
                                    text.Text = text.Text.Remove(endopeniLngTag);
                                }
                                int startingIndex = text.Text.LastIndexOf(">", StringComparison.CurrentCulture);
                                text.Text = text.Text.Remove(0, startingIndex + 1);
                                description = HttpUtility.HtmlDecode(text.Text);
                            }
                        }
                        if (cntrl is TextBox)
                        {
                            var textBox = (TextBox)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == textBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(textBox.ID, description, processId, 6);
                            }
                        }
                        else if (cntrl is DropDownList)
                        {
                            var drpBox = (DropDownList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == drpBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(drpBox.ID, description, processId, 17);
                            }
                        }
                        else if (cntrl is RadioButtonList)
                        {
                            var chkBox = (RadioButtonList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == chkBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(chkBox.ID, description, processId, 16);
                            }
                        }
                        else if (cntrl is admin_userselector_ascx)
                        {
                            var UseAD = (admin_userselector_ascx)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == UseAD.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(UseAD.ID, UseAD.ID, processId, 22);
                            }
                        }
                    }
                }
            }
        }
    }
    protected void CreateCondonationFields(string processId)
    {
        var data = IncidentTrackingManager.GetIncidentProcess(processId);
        if (data == null)
        {
            MessageBox.Show("The Process does not exist, please contact the system adminitrator.");
            return;
        }
        var processFields = IncidentTrackingManager.GetProcessField(processId);
        var ntrl = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("Condonation");

        string description = string.Empty;
        foreach (Control childControls in ntrl.Controls)
        {
            foreach (var _cntrl in childControls.Controls)
            {
                if (_cntrl is prpcr_condonation_ascx)
                {
                    var stage1cntrl = _cntrl as Control;
                    foreach (var cntrl in stage1cntrl.Controls)
                    {
                        if (cntrl is LiteralControl)
                        {
                            var text = (LiteralControl)cntrl;
                            if (!string.IsNullOrEmpty(text.Text))
                            {
                                int endopeniLngTag = text.Text.LastIndexOf("</td>", StringComparison.CurrentCulture);
                                if (endopeniLngTag > -1)
                                {
                                    text.Text = text.Text.Remove(endopeniLngTag);
                                }
                                int startingIndex = text.Text.LastIndexOf(">", StringComparison.CurrentCulture);
                                text.Text = text.Text.Remove(0, startingIndex + 1);
                                description = HttpUtility.HtmlDecode(text.Text);
                            }
                        }
                        if (cntrl is TextBox)
                        {
                            var textBox = (TextBox)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == textBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(textBox.ID, description, processId, 6);
                            }
                        }
                        else if (cntrl is DropDownList)
                        {
                            var drpBox = (DropDownList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == drpBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(drpBox.ID, description, processId, 17);
                            }
                        }
                        else if (cntrl is RadioButtonList)
                        {
                            var chkBox = (RadioButtonList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == chkBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(chkBox.ID, description, processId, 16);
                            }
                        }
                        else if (cntrl is admin_userselector_ascx)
                        {
                            var UseAD = (admin_userselector_ascx)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == UseAD.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(UseAD.ID, UseAD.ID, processId, 22);
                            }
                        }
                    }
                }
            }
        }
    }
    protected void CreateDeviationFields(string processId)
    {
        var data = IncidentTrackingManager.GetIncidentProcess(processId);
        if (data == null)
        {
            MessageBox.Show("The Process does not exist, please contact the system adminitrator.");
            return;
        }
        var processFields = IncidentTrackingManager.GetProcessField(processId);
        var ntrl = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("Deviation");

        string description = string.Empty;
        foreach (Control childControls in ntrl.Controls)
        {
            foreach (var _cntrl in childControls.Controls)
            {
                if (_cntrl is prpcr_deviation_ascx)
                {
                    var stage1cntrl = _cntrl as Control;
                    foreach (var cntrl in stage1cntrl.Controls)
                    {
                        if (cntrl is LiteralControl)
                        {
                            var text = (LiteralControl)cntrl;
                            if (!string.IsNullOrEmpty(text.Text))
                            {
                                int endopeniLngTag = text.Text.LastIndexOf("</td>", StringComparison.CurrentCulture);
                                if (endopeniLngTag > -1)
                                {
                                    text.Text = text.Text.Remove(endopeniLngTag);
                                }
                                int startingIndex = text.Text.LastIndexOf(">", StringComparison.CurrentCulture);
                                text.Text = text.Text.Remove(0, startingIndex + 1);
                                description = HttpUtility.HtmlDecode(text.Text);
                            }
                        }
                        if (cntrl is TextBox)
                        {
                            var textBox = (TextBox)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == textBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(textBox.ID, description, processId, 6);
                            }
                        }
                        else if (cntrl is DropDownList)
                        {
                            var drpBox = (DropDownList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == drpBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(drpBox.ID, description, processId, 17);
                            }
                        }
                        else if (cntrl is RadioButtonList)
                        {
                            var chkBox = (RadioButtonList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == chkBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(chkBox.ID, description, processId, 16);
                            }
                        }
                        else if (cntrl is admin_userselector_ascx)
                        {
                            var UseAD = (admin_userselector_ascx)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == UseAD.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(UseAD.ID, UseAD.ID, processId, 22);
                            }
                        }
                    }
                }
            }
        }
    }
    protected void CreateExpansionAndVariationFields(string processId)
    {
        var data = IncidentTrackingManager.GetIncidentProcess(processId);
        if (data == null)
        {
            MessageBox.Show("The Process does not exist, please contact the system adminitrator.");
            return;
        }
        var processFields = IncidentTrackingManager.GetProcessField(processId);
        var ntrl = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("ExpansionVariation");

        string description = string.Empty;
        foreach (Control childControls in ntrl.Controls)
        {
            foreach (var _cntrl in childControls.Controls)
            {
                if (_cntrl is prpcr_expansionvariation_ascx)
                {
                    var stage1cntrl = _cntrl as Control;
                    foreach (var cntrl in stage1cntrl.Controls)
                    {
                        if (cntrl is LiteralControl)
                        {
                            var text = (LiteralControl)cntrl;
                            if (!string.IsNullOrEmpty(text.Text))
                            {
                                int endopeniLngTag = text.Text.LastIndexOf("</td>", StringComparison.CurrentCulture);
                                if (endopeniLngTag > -1)
                                {
                                    text.Text = text.Text.Remove(endopeniLngTag);
                                }
                                int startingIndex = text.Text.LastIndexOf(">", StringComparison.CurrentCulture);
                                text.Text = text.Text.Remove(0, startingIndex + 1);
                                description = HttpUtility.HtmlDecode(text.Text);
                            }
                        }
                        if (cntrl is TextBox)
                        {
                            var textBox = (TextBox)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == textBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(textBox.ID, description, processId, 6);
                            }
                        }
                        else if (cntrl is DropDownList)
                        {
                            var drpBox = (DropDownList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == drpBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(drpBox.ID, description, processId, 17);
                            }
                        }
                        else if (cntrl is RadioButtonList)
                        {
                            var chkBox = (RadioButtonList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == chkBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(chkBox.ID, description, processId, 16);
                            }
                        }
                        else if (cntrl is admin_userselector_ascx)
                        {
                            var UseAD = (admin_userselector_ascx)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == UseAD.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(UseAD.ID, UseAD.ID, processId, 22);
                            }
                        }
                    }
                }
            }
        }
    }
    protected void CreateTransversalFields(string processId)
    {
        var data = IncidentTrackingManager.GetIncidentProcess(processId);
        if (data == null)
        {
            MessageBox.Show("The Process does not exist, please contact the system adminitrator.");
            return;
        }
        var processFields = IncidentTrackingManager.GetProcessField(processId);
        var ntrl = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("Transversal");

        string description = string.Empty;
        foreach (Control childControls in ntrl.Controls)
        {
            foreach (var _cntrl in childControls.Controls)
            {
                if (_cntrl is prpcr_transversal_ascx)
                {
                    var stage1cntrl = _cntrl as Control;
                    foreach (var cntrl in stage1cntrl.Controls)
                    {
                        if (cntrl is LiteralControl)
                        {
                            var text = (LiteralControl)cntrl;
                            if (!string.IsNullOrEmpty(text.Text))
                            {
                                int endopeniLngTag = text.Text.LastIndexOf("</td>", StringComparison.CurrentCulture);
                                if (endopeniLngTag > -1)
                                {
                                    text.Text = text.Text.Remove(endopeniLngTag);
                                }
                                int startingIndex = text.Text.LastIndexOf(">", StringComparison.CurrentCulture);
                                text.Text = text.Text.Remove(0, startingIndex + 1);
                                description = HttpUtility.HtmlDecode(text.Text);
                            }
                        }
                        if (cntrl is TextBox)
                        {
                            var textBox = (TextBox)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == textBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(textBox.ID, description, processId, 6);
                            }
                        }
                        else if (cntrl is DropDownList)
                        {
                            var drpBox = (DropDownList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == drpBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(drpBox.ID, description, processId, 17);
                            }
                        }
                        else if (cntrl is RadioButtonList)
                        {
                            var chkBox = (RadioButtonList)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == chkBox.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(chkBox.ID, description, processId, 16);
                            }
                        }
                        else if (cntrl is admin_userselector_ascx)
                        {
                            var UseAD = (admin_userselector_ascx)cntrl;
                            var fieldName = processFields != null ? processFields.Find(x => x.FieldName == UseAD.ID) : null;
                            if (fieldName == null)
                            {
                                IncidentTrackingManager.CreateProcessFields(UseAD.ID, UseAD.ID, processId, 22);
                            }
                        }
                    }
                }
            }
        }
    }
}