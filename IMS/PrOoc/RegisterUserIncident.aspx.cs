using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP;

public partial class PrOoc_RegisterUserIncident : IncidentTrackingPage
{
    protected Incident CurrentIncidentDetails;
    protected IncidentProcess CurrentProcessDetails;
    protected IncidentProcess CurrentProc;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdownList(Request.QueryString["procId"]);
            if (CurrentIncident != null)
            {
                PesistObjectToPage();
            }
            else
            {
                SystemReferenceNo.Enabled = false;
                var currUser = SarsUser.GetADUser(SarsUser.SID);
                ResponsibleAdministrator.SelectedAdUserDetails = new SelectedUserDetails
                {
                    SID = currUser.SID,
                    FoundUserName =
                        string.Format("{0} | {1}", currUser.FullName, currUser.SID),
                    FullName = currUser.FullName
                };
                ResponsibleAdministrator.Disable();
                if (!string.IsNullOrEmpty(Request["name"]))
                {
                    CreateProcessField(ProcessID);
                }
            }
        }
    }

    private void PesistObjectToPage()
    {
       
        if (string.IsNullOrEmpty(ProcessID))
        {
            MessageBox.Show("Process ID is not available");
            return;
        }
        //if (!IsAMember)
        //{
        //    Response.Redirect("../IncidentNotYours.aspx");
        //}
        CurrentIncidentDetails = CurrentIncident;
        if (CurrentIncidentDetails == null)
        {
            Response.Redirect("../InvalidProcessOrIncident.aspx");
            return;
        }
        if (!IsPostBack)
        {
            SystemReferenceNo.Text = CurrentIncidentDetails.ReferenceNumber;
            lblReferenceNumber.Text = CurrentIncidentDetails.ReferenceNumber;
            SystemReferenceNo.Enabled = false;
            if (CurrentIncidentDetails.IncidentStatusId == 1)
                SystemReferenceNo.Visible = false;
            if (CurrentIncidentDetails != null && CurrentIncidentDetails.DueDate != null)
            {
                DueDate.SetValue(CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd"));
            }

            if (CurrentIncidentDetails != null && !string.IsNullOrEmpty(CurrentIncidentDetails.AssignedToSID))
            {
                //if (!CurrentIncidentDetails.AssignedToSID.Equals(SarsUser.SID, StringComparison.CurrentCultureIgnoreCase))
                //{
                //    return;
                //}
                if (CurrentIncidentDetails.IncidentStatusId != 2 && CurrentIncidentDetails.IncidentStatusId != 3)
                {
                    //Response.Redirect(String.Format("~/PrOoc/IncidentRealOnly.aspx?procId={0}&incId={1}&msgId=10", ProcessID,
                    //                                IncidentID));
                    //return;
                    tbContainer.Enabled = false;
                }
            }

        
            if (CurrentIncidentDetails != null && !string.IsNullOrEmpty(CurrentIncidentDetails.IncidentStatus))
            {
                if (CurrentIncidentDetails.IncidentStatusId > 1)
                {
                    var adUser = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID);
                    if (adUser != null)
                    {
                        ActionPerson.SelectedAdUserDetails = new SelectedUserDetails
                        {
                            SID = adUser.SID,
                            FoundUserName =
                                string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                            FullName = adUser.FullName
                        };
                    }
                    //ActionPerson.Disable();
                    LoadDocuments();
                    Toolbar1.Items[3].Visible = true;
                    Toolbar1.Items[4].Visible = true;
                    Toolbar1.Items[5].Visible = true;
                    Toolbar1.Items[6].Visible = true;
                    Toolbar1.Items[7].Visible = true;
                    lblMessage.Text = CurrentIncident.IncidentStatus;
                }
                if (CurrentIncidentDetails.IncidentStatusId == 4)
                {
                    Toolbar1.Items[2].Visible = false;
                    Toolbar1.Items[5].Visible = false;
                    Toolbar1.Items[6].Visible = false;
                    //Toolbar1.Items[7].Visible = false;                   
                }
                if (CurrentIncidentDetails.IncidentStatusId == 5)
                {
                    Toolbar1.Items[2].Visible = false;
                    Toolbar1.Items[5].Visible = false;
                    Toolbar1.Items[6].Visible = false;
                    Toolbar1.Items[7].Visible = false;
                }
                var currUser = SarsUser.GetADUser(SarsUser.SID);
                if (currUser != null)
                {
                    ResponsibleAdministrator.SelectedAdUserDetails = new SelectedUserDetails
                    {
                        SID = currUser.SID,
                        FoundUserName =
                            string.Format("{0} | {1}", currUser.FullName, currUser.SID),
                        FullName = currUser.FullName
                    };
                }
                ResponsibleAdministrator.Disable();
                LoadInfo();
                txtNotes.Clear();
            }
            try
            {
                TypeOfSubmissionOther.Visible = TypeOfSubmission.SelectedItem.Text.Equals("Other") ? true : false;
                tdSubmissionOther.Visible = TypeOfSubmission.SelectedItem.Text.Equals("Other") ? true : false;
            }
            catch (Exception)
            {
                // throw;
            }
        }
    }

    protected void CreateProcessField(string processId)
    {
        var data = IncidentTrackingManager.GetIncidentProcess(processId);
        if (data == null)
        {
            MessageBox.Show("The OOC Process does not exist, please contact the system adminitrator.");
            return;
        }
        var processFields = IncidentTrackingManager.GetProcessField(processId);
        var ntrl = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("tbDetails").FindControl("UpdatePanel1") ;
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
                else if (cntrl is CheckBox)
                {
                    var chkBox = (CheckBox)cntrl;
                    var fieldName = processFields != null ? processFields.Find(x => x.FieldName == chkBox.ID) : null;
                    if (fieldName == null)
                    {
                        IncidentTrackingManager.CreateProcessFields(chkBox.ID, chkBox.ID, processId, 5);
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

    private Incident CreateNewIncident()
    {
        if (CurrentIncident == null)
        {
            string incidentId;
            string incidentNumber;
            string type = Request.QueryString["type"];
            var recordsAffected = IncidentTrackingManager.InitOocIncident(ProcessID, type, out incidentId, out incidentNumber);
            CurrentProc = CurrentProcess;
            CurrentIncidentDetails = IncidentTrackingManager.GetIncidentById(incidentId);
        }
        else
        {
            CurrentIncidentDetails = CurrentIncident;
        }
        return CurrentIncidentDetails;
    }

    private void SaveAnswers()
    {
        var processId = Request.QueryString["procId"];
        if (processId == null) return;
        var controls = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("tbDetails").FindControl("UpdatePanel1") as Control;
        Incident inc = CreateNewIncident();

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
                                IncidentTrackingManager.SaveAnswers(inc.IncidentID.ToString(), processId, field.FieldId,
                                    field.FieldTypeId, chkbox.Checked ? "Yes" : "No");
                            }
                            break;
                        }
                    case 6:

                        var textEntry = controls.FindControl(field.FieldName) as TextBox;
                        if (textEntry != null)
                        {
                            IncidentTrackingManager.SaveAnswers(inc.IncidentID.ToString(), processId, field.FieldId,
                                field.FieldTypeId, textEntry.Text);
                        }
                        break;
                    case 17:
                        var drpSelected = (DropDownList)controls.FindControl(field.FieldName);
                        if (drpSelected != null)
                        {
                            IncidentTrackingManager.SaveAnswers(inc.IncidentID.ToString(), processId, field.FieldId,
                                field.FieldTypeId, drpSelected.SelectedValue);
                        }
                        break;
                    case 22:
                        var userSelector = (admin_userselector_ascx)controls.FindControl(field.FieldName);
                        if (userSelector != null)
                        {
                            if (userSelector.SID != null && !string.IsNullOrEmpty(userSelector.SID))
                            {
                                var user = SarsUser.GetADUser(userSelector.SID);
                                if (user != null)
                                {
                                    if(string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Surname) || string.IsNullOrEmpty(user.Mail)
                                        || string.IsNullOrEmpty(user.Telephone) || string.IsNullOrEmpty(user.SID))
                                    {
                                        MessageBox.Show("Some user information is missing, please check if the user is selected from the dropdownlist");
                                        break;
                                    }

                                }
                                SarsUser.SaveUser(userSelector.SID);
                                IncidentTrackingManager.SaveAnswers(inc.IncidentID.ToString(), processId, field.FieldId,
                                    field.FieldTypeId, userSelector.SID);
                            }
                            else
                            {
                                IncidentTrackingManager.SaveAnswers(inc.IncidentID.ToString(), processId, field.FieldId,
                                     field.FieldTypeId, string.Empty);
                            }
                        }
                        break;
                }
            }
        }

        if (CurrentIncidentDetails.IncidentStatusId == 1)
        {
            SarsUser.SaveUser(ActionPerson.SID);
            var numrecords =
                IncidentTrackingManager.UpdateInternalOocIncidentDetails
                    (
                        Convert.ToDateTime(DueDate.Text),
                        ActionPerson.SID.Trim(),
                        inc.IncidentID.ToString(),
                       "Incident Has been Assigned"
                    );
            CurrentIncidentDetails = IncidentTrackingManager.GetIncidentById(inc.IncidentID.ToString());
            if (numrecords > 0)
            {
               // CurrentIncidentDetails = CurrentIncident;
                SendFirstNotification(CurrentIncidentDetails);
                SendFirstNotificationToProcOwners(CurrentIncidentDetails);
            }
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        var processId = Request.QueryString["procId"];
        if (processId == null) return;
        var controls =this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("tbDetails").FindControl("UpdatePanel1") as Control ;
        switch (e.CommandName)
        {
            case "Submit":
                {
                    var actionPerson = (admin_userselector_ascx)controls.FindControl("ActionPerson");
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
                    if (string.IsNullOrEmpty(AuthorName.SID))
                    {
                        MessageBox.Show("Author Name is required.");
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

                    //if (Convert.ToDateTime(DueDate.Text) <= DateTime.Now)
                    //{
                    //    DueDate.Focus();
                    //    MessageBox.Show(string.Format("Incident Due Date must be greater than Incident Registration Date"));
                    //    return;
                    //}
                    SaveAnswers();
                    //  MessageBox.Show("An Incident has been created succesfully.");

                    Response.Redirect(String.Format("~/PrOoc/RegisterUserIncident.aspx?procId={0}&incId={1}&type={2}",
                              CurrentIncidentDetails.ProcessId, CurrentIncidentDetails.IncidentID, "Internal"));                  
                    break;
                }
            case "Cancel":
                {
                    Response.Redirect(String.Format("~/PrOoc/NormalUserLandingPage.aspx?procId={0}&type={1}", ProcessID, Request["type"]));
                    break;
                }
            case "Add":
                {

                    break;
                }
            case "Print":
                {
                    var page = "~/PrOoc/RegisterUserIncident";
                    Response.Redirect(String.Format("~/Admin/CoverPage.aspx?procId={0}&incId={1}&msgId=1&pg={2}", ProcessID, IncidentID, page));
                    break;
                }
            case "AcknowledgementLetter":
                {
                    var page = "~/PrOoc/RegisterUserIncident";
                    Response.Redirect(String.Format("~/Admin/Acknowledgement.aspx?procId={0}&incId={1}&msgId=1&pg={2}", ProcessID, IncidentID, page));
                    break;
                }
            case "ReAssign":
                {
                    var actionPerson = (admin_userselector_ascx)controls.FindControl("ActionPerson");
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
                    if (string.IsNullOrEmpty(AuthorName.SID))
                    {
                        MessageBox.Show("Author Name is required.");
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

                    //if (Convert.ToDateTime(DueDate.Text) <= DateTime.Now)
                    //{
                    //    DueDate.Focus();
                    //    MessageBox.Show(string.Format("Incident Due Date must be greater than Incident Registration Date"));
                    //    return;
                    //}
                    SaveAnswers();
                    Response.Redirect(String.Format("~/PrOoc/ChangeAssignee.aspx?procId={0}&incId={1}&type={2}", ProcessID, IncidentID,Request["type"]));                   
                    break;
                }
            case "Complete":
                Response.Redirect(String.Format("~/PrOoc/CompleteIncident.aspx?ProcId={0}&IncId={1}&type={2}", ProcessID, IncidentID, Request["type"]));
                break;
            case "Close":
                Response.Redirect(String.Format("~/PrOoc/CloseIncident.aspx?ProcId={0}&IncId={1}&type={2}", ProcessID, IncidentID, Request["type"]));
                break;
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
    protected void UploadFile(object sender, EventArgs e)
    {
        var controls = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("tbDetails").FindControl("UpdatePanel1") as Control;
        var actionPerson = (admin_userselector_ascx)controls.FindControl("ActionPerson");
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
        if (string.IsNullOrEmpty(AuthorName.SID))
        {
            MessageBox.Show("Author Name is required.");
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
        //if (Convert.ToDateTime(DueDate.Text) <= DateTime.Now)
        //{
        //    DueDate.Focus();
        //    MessageBox.Show(
        //        string.Format(
        //            "Incident Due Date must be greater than Incident Registration Date [{0}]",
        //            CurrentIncidentDetails.Timestamp.ToString("yyyy-MM-dd HH:mm")));
        //    return;
        //}
        var maxFileSize = CurrentProcess.MaxFileSize;
        //const int maxSize = 255000;

        if (!flDoc.HasFile)
        {
            flDoc.Focus();
            MessageBox.Show("Please select a file to upload");
            return;
        }
        var uploadedFile = flDoc.PostedFile;

        if (uploadedFile.ContentLength > maxFileSize)
        {
            flDoc.Focus();
            MessageBox.Show("This file is bigger than the accepted file size");
            return;
        }
        SaveAnswers();
        if (uploadedFile.ContentLength > 0)
        {
            var buffer = new byte[uploadedFile.ContentLength];
            uploadedFile.InputStream.Read(buffer, 0, buffer.Length);
            var noteId = Request["noteId"];
            var fileName = flDoc.FileName;

            var saved = IncidentTrackingManager.UploadFile(CurrentIncidentDetails.IncidentID.ToString(), noteId, buffer, fileName);

            if (saved > 0)
            {
                Response.Redirect(String.Format("~/PrOoc/RegisterUserIncident.aspx?procId={0}&incId={1}&type={2}",
                          CurrentIncidentDetails.ProcessId, CurrentIncidentDetails.IncidentID, "Internal"));
               // LoadDocuments();
              //  MessageBox.Show("File uploaded successfully.");
            }
        }
    }
    protected void gvDocs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDocs.PageIndex = e.NewPageIndex;
        LoadDocuments();
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
                LoadDocuments();
                MessageBox.Show("File deleted successfully");
            }
        }
    }
    protected void TypeOfSubmission_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TypeOfSubmission.SelectedItem.Text.Equals("Other"))
        {
            TypeOfSubmissionOther.Visible = true;
            tdSubmissionOther.Visible = true;
        }
        else
        {
            TypeOfSubmissionOther.Visible = false;
            TypeOfSubmissionOther.Text = string.Empty;
            tdSubmissionOther.Visible = false;
        }
    }
    public void LoadDocuments()
    {
        var data = IncidentTrackingManager.GetDocumentsByIncidentId(IncidentID);
        if (data != null && data.Any())
        {
            gvDocs.Bind(data);
        }
        else
        {
            gvDocs.Bind(null);
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
                                dropBox.SelectItemByText(answer["Answer"].ToString());
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
                        else if (cntrl is admin_userselector_ascx)
                        {
                            var userAd = cntrl as admin_userselector_ascx;
                            if (answer["FieldName"].ToString() == userAd.ID)
                            {
                                if (!string.IsNullOrEmpty(answer["Answer"].ToString()))
                                {
                                    var adUser = SarsUser.GetADUser(answer["Answer"].ToString());
                                    if (adUser != null)
                                    {
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
        }
        SystemReferenceNo.Text = CurrentIncidentDetails.ReferenceNumber;
        var notes = IncidentTrackingManager.GetWorkInfoByIncidentID(IncidentID);
        if (notes != null && notes.Any())
        {
            gvWorkInfo.Bind(notes);
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
   
    protected void AddNote(object sender, EventArgs e)
    {
      
        var controls = this.Master.FindControl("MainContent").FindControl("tbContainer").FindControl("tbDetails").FindControl("UpdatePanel1") as Control;
        var actionPerson = (admin_userselector_ascx)controls.FindControl("ActionPerson");
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
        if (string.IsNullOrEmpty(AuthorName.SID))
        {
            MessageBox.Show("Author Name is required.");
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
        if (string.IsNullOrEmpty(txtNotes.Text))
        {
            txtNotes.Focus();
            MessageBox.Show("Notes is required.");
            return;
        }
       
        SaveAnswers();
        var workInfo = new WorkInfoDetails
        {
            AddedBySID = SarsUser.SID,
            IncidentId = Convert.ToDecimal(CurrentIncidentDetails.IncidentID),
            ProcessId = Convert.ToInt32(CurrentIncidentDetails.ProcessId),
            Notes = txtNotes.Text
        };
        if (CurrentIncidentDetails != null)
        {
            if (IncidentTrackingManager.AddWorkInfo(workInfo) > 0)
            {
                //var details = CurrentIncident;
                if (CurrentIncidentDetails.IncidentStatusId == 2)
                {
                    const int statusId = 3; //WIP
                    //  CurrentIncidentDetails = CurrentIncident;
                    IncidentTrackingManager.UpdateIncidentStatus(statusId, IncidentID);
                }
                if (CurrentIncident == null)
                {
                    txtNotes.Clear();
                    Response.Redirect(
                        String.Format("~/PrOoc/RegisterUserIncident.aspx?procId={0}&incId={1}&type={2}",
                            CurrentIncidentDetails.ProcessId, CurrentIncidentDetails.IncidentID, "Internal"));
                }
                else
                {
                    txtNotes.Clear();
                    LoadInfo();
                    MessageBox.Show("Notes has been added.");
                }
                
            }

        }
    }

    private void SendFirstNotification(Incident cIncident)
    {
        try
        {
            var incidentURL = string.Format("http://{0}/ims/PrOoc/RegisterUserIncident.aspx?{1}", Request.ServerVariables["HTTP_HOST"],
                                            String.Format("procId={0}&incId={1}", cIncident.ProcessId, cIncident.IncidentID));
            var process = CurrentProcess;
            var userAssigned = SarsUser.GetADUser(cIncident.AssignedToSID.Trim());

            var subject = string.Format("{0} Ref : {1}", process.Description, cIncident.ReferenceNumber);

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
                                             string.Format("{0} | {1}", SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).FullName, SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).SID)
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
        catch (Exception)
        {

            
        }
    }
    private void SendFirstNotificationToProcOwners(Incident cIncident)
    {
        try
        {


            var incidentURL = string.Format("http://{0}/ims/PrOoc/RegisterUserIncident.aspx?{1}",
                                            Request.ServerVariables["HTTP_HOST"],
                                            String.Format("procId={0}&incId={1}", cIncident.ProcessId, cIncident.IncidentID));
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
                                                     userAssigned.FullName);


                            client.Send1("IncidentTracking@sars.gov.za", owner.Mail, subject, body);
                            IncidentTrackingManager.SaveIncidentEmailLog(body, subject, owner.SID, owner.Mail,
                                                                     this.IncidentID, this.ProcessID);
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