using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Collections.Generic;
using Sars.Systems.Data;
using System.Data;
using System.IO;

public partial class NTQR_IncidentRealOnly : IncidentTrackingPage
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
            return;
        }
        CurrentIncidentDetails = CurrentIncident;
        if (!IsPostBack)
        {
            CurrentIncidentDetails = CurrentIncident;

        if (CurrentIncidentDetails == null)
        {
            Response.Redirect("../InvalidProcessOrIncident.aspx");
            return;
        }
        if (CurrentIncidentDetails.IncidentStatusId != 2 && CurrentIncidentDetails.IncidentStatusId != 3)
        {
           // gvWorkInfo.Enabled = false;
            btnAddNote.Enabled = true;
            btnNew.Enabled = false;
            txtNotes.Enabled = true;
        }
        if (CurrentIncidentDetails.IncidentStatusId != 2 && CurrentIncidentDetails.IncidentStatusId != 3 
                && CurrentIncidentDetails.IncidentStatusId != 10 && CurrentIncidentDetails.IncidentStatusId != 7)
        {
            if (CurrentProcess.ReAssignToCreater)
            {
                var processOwners = IncidentTrackingManager.GetProcessOwners(CurrentProcess.ProcessId.ToString());
                foreach (var item in processOwners)
                {
                    if (item.OwnerSID.ToLower().Equals(SarsUser.SID.ToLower()))
                    {
                        Toolbar1.Items[5].Visible = true;
                        Toolbar1.Items[6].Visible = true;
                        break;
                    }
                }
            }
        }

        
            if (CurrentIncidentDetails != null && CurrentIncidentDetails.DueDate != null)
            {
                txtIncidentDueDate.SetValue(CurrentIncidentDetails.DueDate.Value.ToString("yyyy-MM-dd"));
            }

            if (CurrentIncidentDetails != null && !string.IsNullOrEmpty(CurrentIncidentDetails.AssignedToSID))
            {
                var adUser = SarsUser.GetADUser(CurrentIncidentDetails.AssignedToSID.ToLower());
                UserSelector1.SelectedAdUserDetails = new SelectedUserDetails
                                                          {
                                                              SID = adUser.SID,
                                                              FoundUserName =
                                                                  string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                                                              FullName = adUser.FullName
                                                          };
                UserSelector1.Disable();
                txtSummary.SetValue(CurrentIncidentDetails.Summary);
                txtCrossReferenceNo.SetValue(CurrentIncidentDetails.CrossRefNo);
                txtCreatedBy.Value = string.Format("{0} | {1}", SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID.ToLower()).FullName, SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID.ToLower()).SID);
         
                if (!string.IsNullOrEmpty(CurrentIncidentDetails.Summary))
                {
                    txtSummary.Attributes.Add("title",
                                              "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Incident Summary</font></b>] body=[<font color='red'><b>" +
                                              CurrentIncidentDetails.Summary + "</b></font>]");
                }
            }
            LoadWorkInfo();
            if (CurrentIncidentDetails.ProcessId.ToString() == GetOocOfficeKey())
            {
                Toolbar1.Items[2].Visible = true;
                Toolbar1.Items[3].Visible = true;
            }
            if (!String.IsNullOrEmpty(Request["msgId"]))
            {
                var message = Utils.GetMessage(Convert.ToInt32(Request["msgId"]));
                if ( !string.IsNullOrEmpty(message))
                {
                    MessageBox.Show(message);
                }
            }
            txtSummary.Enabled = false;
            txtIncidentDueDate.Enabled = false;
            Utils.Enable(DisplaySurvey2, false);
            UserSelector1.Disable();
            txtCrossReferenceNo.Enabled = false;

            UCAttachDocuments.LoadDocuments();
        }
    }

    private void LoadWorkInfo()
    {
        //var data = IncidentTrackingManager.GetWorkInfoByIncidentID(this.IncidentID);
        //if (data != null && data.Any())
        //{
        //    gvWorkInfo.Bind(data);
        //    Utils.Enable(DisplaySurvey2, false);
        //}

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

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        e.Row.Attributes.Add("onclick",
                             Page.ClientScript.GetPostBackEventReference((Control) sender, "Select$" + e.Row.RowIndex));
        var description = DataBinder.Eval(e.Row.DataItem, "Notes").ToString();
       

        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        e.Row.Attributes.Add("title",
                             "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Work Info Notes</font></b>] body=[<font color='red'><b>" +
                             description + "</b></font>]");
    }

    protected void NewNote(object sender, EventArgs e)
    {
        this.btnAddNote.Enabled = true;
        this.txtNotes.Enabled = true;
        this.btnNew.Enabled = true;
    }

    protected void ViewDocuments(object sender, EventArgs e)
    {

        if (String.IsNullOrEmpty(CurrentIncident.AssignedToSID))
        {
           // tbContainer.ActiveTabIndex = 0;
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
                    Response.Redirect(String.Format("~/SurveyWizard/AttachDocuments.aspx?procId={0}&incId={1}&noteId={2}&pp=1", ProcessID, IncidentID, workInfoId));
                }
            }
        }
    }
    
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Cancel":
        Response.Redirect(String.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));
                break;
            case "Notes":
                Response.Redirect(String.Format("~/SurveyWizard/AddWorkInfo.aspx?procId={0}&incId={1}", ProcessID, IncidentID));

                break;
            case "Print":
                {
                    Response.Redirect(String.Format("CoverPage.aspx?procId={0}&incId={1}&msgId=1", ProcessID, IncidentID));
                    break;
                }
            case "AcknowledgementLetter":
                {
                    Response.Redirect(String.Format("Acknowledgement.aspx?procId={0}&incId={1}&msgId=1", ProcessID, IncidentID));
                    break;
                }
            case "PrintScreen":
                {
                    PrintDocument();
                    //ClientScript.RegisterStartupScript(this.GetType(),"Print","print()",true);
                    break;
                }
            case "ReOpen":
                {
                    Response.Redirect(String.Format("ReOpenIncident.aspx?procId={0}&incId={1}", ProcessID, IncidentID));
                    break;
                }
            case "Finalise":
                {
                    var saved = IncidentTrackingManager.FinaliseIncident(IncidentID);                   
                    Response.Redirect(String.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));
                    break;
                }

        }
    }


    private void PrintDocument()
    {

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
        sb.Append(" mywindow.document.write('</head><body >');");
        sb.Append(" mywindow.document.write($('#" + divContent.ClientID + "').html());");
        sb.Append(" mywindow.document.write('</body></html>');");
        sb.Append("mywindow.document.close(); ");
        sb.Append(" mywindow.focus(); ");
        sb.Append(" mywindow.print();");
        sb.Append(" mywindow.close();");
        ClientScript.RegisterStartupScript(this.GetType(), "Print", sb.ToString(), true);
    }
    protected void btnAddNote_Click(object sender, EventArgs e)
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
            var details = CurrentIncident;
            if (details.IncidentStatusId == 2)
            {
                const int statusId = 3;
                CurrentIncidentDetails = CurrentIncident;
                if (CurrentIncidentDetails.AssignedToSID != null && CurrentIncidentDetails.AssignedToSID.Equals(SarsUser.SID))
                {
                    IncidentTrackingManager.UpdateIncidentStatus(statusId, IncidentID);
                }
            }
            LoadWorkInfo();
            SendToCreaterWithCommentAdded();
            txtNotes.Clear();
            Response.Redirect("NormalUserLandingPage.aspx?procId=" + CurrentIncidentDetails.ProcessId);
            //txtNotes.Enabled = false;
           // MessageBox.Show("Work Info addedd successfully.");
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
                    if (getCcs != null)
                    {
                        ccsUsers.Add(getCcs.Mail);
                    }
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

    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.btnAddNote.Enabled = true;
        this.txtNotes.Enabled = true;
        this.btnNew.Enabled = true;
    }


    protected string GetOocOfficeKey()
    {
        return System.Configuration.ConfigurationManager.AppSettings["oocOffice"];
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