using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

public partial class Admin_NewIncident : IncidentTrackingPage
{
    protected Incident CurrentIncidentDetails;

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (string.IsNullOrEmpty(ProcessID))
        //{
        //    MessageBox.Show("Process ID is not available");
        //    return;
        //}

        //if (!IsAMember)
        //{
        //    //Response.Redirect(string.Format("IncidentRealOnly.aspx?procId={0}&incId={1}&msgId=3", ProcessID, IncidentID));
        //    Response.Redirect("../IncidentNotYours.aspx");
        //    return;
        //}
        //CurrentIncidentDetails = CurrentIncident;

        //if (CurrentIncidentDetails == null)
        //{
        //    Response.Redirect("../InvalidProcessOrIncident.aspx");
        //}
        //if (CurrentIncidentDetails != null && !string.IsNullOrEmpty(CurrentIncidentDetails.AssignedToSID) && !CurrentIncidentDetails.AssignedToSID.ToUpper().Equals(SarsUser.SID.ToUpper()))
        //{
        //    Response.Redirect(String.Format("../IncidentNotYours.aspx?incid={0}", IncidentID));
        //}
        //if (!IsPostBack)
        //{
        //    if (CurrentIncidentDetails != null && CurrentIncidentDetails.DueDate != null)
        //    {
        //        txtIncidentDueDate.SetValue(CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd hh:mm"));
        //    }
        //    if (CurrentIncidentDetails != null && !string.IsNullOrEmpty(CurrentIncidentDetails.AssignedToSID))
        //    {
        //        var adUser = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID);
        //        UserSelector1.SelectedAdUserDetails = new SelectedUserDetails
        //                                                  {
        //                                                      SID = adUser.SID,
        //                                                      FoundUserName =
        //                                                          string.Format("{0} | {1}", adUser.FullName, adUser.SID),
        //                                                      FullName = adUser.FullName
        //                                                  };
        //        UserSelector1.Disable(); 
        //        txtSummary.SetValue(CurrentIncidentDetails.Summary);
        //        if (!string.IsNullOrEmpty(CurrentIncidentDetails.Summary))
        //        {
        //            txtSummary.Attributes.Add("title",
        //                                      "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Incident Summary</font></b>] body=[<font color='red'><b>" +
        //                                      CurrentIncidentDetails.Summary + "</b></font>]");
        //        }
        //    }
           
        //    LoadInfo();

        //}
        //Response.Redirect(string.Format("IncidentRealOnly.aspx?procId={0}&incId={1}&msgId=1", ProcessID, IncidentID));
    }

    private void LoadInfo()
    {
    //    var data = IncidentTrackingManager.GetWorkInfoByIncidentID(IncidentID);
    //    if(data != null && data.Any())
    //    {
    //        gvWorkInfo.Bind(data);
    //    }
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        //switch (e.CommandName)
        //{
        //    case "Submit":
        //        {
        //            if(string.IsNullOrEmpty(txtIncidentDueDate.Text))
        //            {
        //                MessageBox.Show("Due Date is required.");
        //                return;
        //            }

        //            if (CurrentIncidentDetails.DueDate == null)
        //            {
        //                if (Convert.ToDateTime(txtIncidentDueDate.Text) <= DateTime.Now)
        //                {
        //                    txtIncidentDueDate.Focus();
        //                    MessageBox.Show(
        //                        string.Format(
        //                            "Incident Due Date must be greater than Incident Registration Date [{0}]",
        //                            this.CurrentIncidentDetails.Timestamp.ToString("yyyy-MM-dd HH:mm")));
        //                    return;
        //                }
        //            }
        //            if(string.IsNullOrEmpty(txtSummary.Text.Trim()))
        //            {
        //                txtSummary.Focus();
        //                MessageBox.Show("Incident Summary is required.");
        //                return;
        //            }

        //            try
        //            {
        //                if(!txtSummary.Text.Trim().Equals(CurrentIncidentDetails.Summary))
        //                {
        //                    //IncidentTrackingManager.UpdateOtherIncidentDetails(IncidentID, txtSummary.Text);
        //                }

        //                if(CurrentIncidentDetails == null)
        //                {
        //                    CurrentIncidentDetails = CurrentIncident;
        //                }

        //                if (CurrentIncidentDetails.IncidentStatusId == 1)
        //                {
        //                    SarsUser.SaveUser(UserSelector1.SID);
        //                    var numrecords =
        //                        IncidentTrackingManager.UpdateIncidentDetails(Convert.ToDateTime(txtIncidentDueDate.Text),
        //                                                            UserSelector1.SID.Trim(),
        //                                                            IncidentID,
        //                                                            txtSummary.Text);
        //                    if (numrecords > 0)
        //                    {
        //                        SendFirstNotification();
        //                    }
        //                }

                       
        //                DateTime testDate;
        //                if (String.IsNullOrEmpty(txtIncidentDueDate.Text) && !DateTime.TryParse(txtIncidentDueDate.Text, out testDate))
        //                {
        //                    return;
        //                }
        //                if (!DisplaySurvey2.SaveQuestions())
        //                {
        //                    return;
        //                }
        //                if(!string.IsNullOrEmpty(this.txtNotes.Text.Trim()) )
        //                {
        //                    txtNotes.Focus();
        //                    tbContainer.ActiveTabIndex = 1;
        //                    MessageBox.Show("Please click the Save Work Info button!");
        //                    return;
        //                }
        //                MessageBox.Show("Incident saved successfully.");
        //            }
        //            catch (Exception exception)
        //            {
        //                MessageBox.Show(exception.Message);
        //            }
                   
        //            break;
        //        }
        //    //case "AddNotes":
        //    //    {
        //    //        Response.Redirect(String.Format("~/SurveyWizard/AddWorkInfo.aspx?procId={0}&incId={1}", ProcessID,IncidentID ));
        //    //        break;
        //    //    }
        //    case "Cancel":
        //        {
        //            Response.Redirect("ListMyProcesses.aspx");
        //            break;
        //        }

        //    case "Add":
        //        {

        //            if (string.IsNullOrEmpty(txtNotes.Text))
        //            {
        //                txtNotes.Focus();
        //                MessageBox.Show("Description is required.");
        //                return;
        //            }
        //            var workInfo = new WorkInfoDetails
        //            {
        //                AddedBySID = SarsUser.SID,
        //                IncidentId = Convert.ToDecimal(IncidentID),
        //                ProcessId = Convert.ToInt32(ProcessID),
        //                Notes = txtNotes.Text
        //            };
        //            if (IncidentTrackingManager.AddWorkInfo(workInfo) > 0)
        //            {
        //                var details = CurrentIncident;
        //                if (details.IncidentStatusId == 2)
        //                {
        //                    const int statusId = 3;
        //                    IncidentTrackingManager.UpdateIncidentStatus(statusId, IncidentID);
        //                }
        //                LoadInfo();
        //                txtNotes.Clear();
        //                txtNotes.Enabled = false;
        //                MessageBox.Show("Work Info added successfully.");
        //            }
        //            break;
        //        }
        //}
    }

    private void SendFirstNotification()
    {

        //var incidentURL = string.Format(System.Configuration.ConfigurationManager.AppSettings["incident-details-url"],
        //                                Request.ServerVariables["HTTP_HOST"], 
        //                                String.Format("procId={0}&incId={1}", this.ProcessID, this.IncidentID));
        //CurrentIncidentDetails = CurrentIncident;
        //var process = CurrentProcess;
        //var userAssigned = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID.Trim()); 

        //var subject = string.Format("{0} Ref : {1}", process.Description, CurrentIncidentDetails.IncidentNumber);

        //if (Request.PhysicalApplicationPath != null)
        //{
        //    var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-assigned.htm");
        //    var tempate = File.ReadAllText(templateDir);

        //    if (CurrentIncidentDetails.DueDate != null)
        //    {
        //        var body = string.Format(tempate, userAssigned.FullName,
        //                                 CurrentIncidentDetails.Summary,
        //                                 CurrentIncidentDetails.IncidentNumber,
        //                                 CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd hh:mm"),
        //                                 CurrentIncidentDetails.IncidentStatus,
        //                                 incidentURL);

        //        using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
        //        {
        //            client.Send1("IncidentTracking@sars.gov.za", SarsUser.GetADUser(UserSelector1.SID).Mail, subject, body);
        //            foreach (var processOwner in this.CurrentProcess.Owners)
        //            {
        //                var owner = SarsUser.GetADUser(processOwner.OwnerSID);
        //                client.Send1("IncidentTracking@sars.gov.za", owner.Mail, subject, body);
        //            }
        //        }
        //    }
        //}
    }

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType != DataControlRowType.DataRow)
        //{
        //    return;
        //}
        //e.Row.Attributes.Add("onclick",
        //                     Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        //var description = DataBinder.Eval(e.Row.DataItem, "Notes").ToString();

        //e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        //e.Row.Attributes.Add("title",
        //                     "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Work Info Notes</font></b>] body=[<font color='red'><b>" +
        //                     description + "</b></font>]");
    }
    protected void NewNote(object sender, EventArgs e)
    {
        //this.btnAddNote.Enabled = true;
        //this.txtNotes.Enabled = true;
        //this.btnNew.Enabled = false;
    }


    protected void ViewDocuments(object sender, EventArgs e)
    {
        //var btn = sender as LinkButton;
        //if (btn != null)
        //{
        //    var row = btn.Parent.Parent as GridViewRow;
        //    if (row != null)
        //    {
        //        gvWorkInfo.SelectRow(row.RowIndex);
        //        if (gvWorkInfo.SelectedDataKey != null)
        //        {
        //            ViewState["WinfoId"] = gvWorkInfo.SelectedDataKey["WorkInfoId"];
        //            if (tbContainer.Tabs.Count > 2)
        //            {
        //                tbContainer.Tabs.RemoveAt(2);
        //            }
        //            tbContainer.Tabs.Add(new TabPanel() {ContentTemplate = new BindableTemplateBuilder()});
        //        }
        //    }
        //}
    }

    protected void AddNote(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(txtNotes.Text))
        //{
        //    txtNotes.Focus();
        //    MessageBox.Show("Description is required.");
        //    return;
        //}
        //var workInfo = new WorkInfoDetails
        //{
        //    AddedBySID = SarsUser.SID,
        //    IncidentId = Convert.ToDecimal(IncidentID),
        //    ProcessId = Convert.ToInt32(ProcessID),
        //    Notes = txtNotes.Text
        //};
        //if (IncidentTrackingManager.AddWorkInfo(workInfo) > 0)
        //{
        //    var details = CurrentIncident;
        //    if (details.IncidentStatusId == 2)
        //    {
        //        const int statusId = 3;
        //        IncidentTrackingManager.UpdateIncidentStatus(statusId, IncidentID);
        //    }
        //    LoadInfo();
        //    txtNotes.Clear();
        //    txtNotes.Enabled = false;
        //    btnAddNote.Enabled = false;
        //    btnNew.Enabled = true;
        //    MessageBox.Show("Work Info added successfully.");
        //}
    }
}