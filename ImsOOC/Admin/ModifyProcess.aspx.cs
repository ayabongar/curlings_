using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;

public partial class ModifyProcessPage : IncidentTrackingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("~/NoAccessForm.aspx");
        }
       
        if (!IsPostBack)
        {
            LoadProcesses();
        }
    }

    private void LoadProcesses()
    {
        var processes = IncidentTrackingManager.GetIncidentProcesses(SarsUser.SID);
        if (processes !=null && processes.Any())
        {
            gvProcesses.Bind(processes);
            SelectedIndexChanged(null, EventArgs.Empty);
            return;
        }
        lblMessage.Visible = true;
    }

    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        var processes = IncidentTrackingManager.GetIncidentProcesses(SarsUser.SID);
        gvProcesses.NextPage(processes, e.NewPageIndex);
        gvProcesses.SelectedIndex = -1;
        row_modfy_process.Visible = false;
        row_review_process.Visible = false;
    }

    protected void ViewFields()
    {
        object processId;
        if (gvProcesses.SelectedDataKey != null && (processId = gvProcesses.SelectedDataKey["ProcessId"]) != null)
        {
            Response.Redirect(String.Format("ViewFields.aspx?procId={0}", processId));
        }
    }

    protected void AddFields()
    {
        object processId;
        if (gvProcesses.SelectedDataKey != null && (processId = gvProcesses.SelectedDataKey["ProcessId"]) != null)
        {
            Response.Redirect(String.Format("AddFields.aspx?procId={0}", processId));
        }
    }


    protected void SelectSurvey(object sender, EventArgs e)
    {
        var lnkBtn = sender as LinkButton;
        if (lnkBtn != null)
        {
            var row = lnkBtn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvProcesses.SelectedIndex = row.RowIndex;
            }
        }
    }

    protected void ModifyProcess(object sender, EventArgs e)
    {
        using (var row = gvProcesses.SelectedRow)
        {
            if (row != null)
            {
                gvProcesses.SelectedIndex = row.RowIndex;
                if (gvProcesses.SelectedDataKey != null)
                {
                    var processDataKey = gvProcesses.SelectedDataKey["ProcessId"] ;
                    if (processDataKey != null)
                    {
                        ViewState["ProcId"] = processDataKey;
                        
                        var processData = IncidentTrackingManager.GetIncidentProcess(processDataKey.ToString());

                        if (processData != null)
                        {
                            row_modfy_process.Visible = true;
                            row_review_process.Visible = false;
                            txtProcessName.Enabled = true;

                            txtProcessName.SetValue(processData.Description);
                            chkActive.Checked = processData.IsActive;
                            chkCreater.Checked = processData.ReAssignToCreater;
                            ckAddCoverPage.Checked = processData.AddCoverPage;
                            ckCanShareIncidents.Checked = processData.CanWorkOnOneCase;
                            txtPrefix.SetValue(processData.Prefix);
                            txtFileSize.SetValue(processData.MaxFileSize/1024/1024);
                            txtWorkingFolderUrl.SetValue(processData.WorkingUrl);

                            var owners = processData.Owners;
                            if (owners != null)
                            {
                                foreach (var owner in processData.Owners)
                                {
                                    var user = SarsUser.GetADUser(owner.OwnerSID);
                                    if(user != null)    
                                      lbOwners.Items.Add(new ListItem(String.Format("{0} | {1}", user.FullName, user.SID)));
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected void ViewSections(object sender, EventArgs e)
    {
        var row = gvProcesses.SelectedRow;
        if (row != null)
        {
            var lblQuestionnaireId = row.FindControl("lblQuestionnaireId") as Label;
            if (lblQuestionnaireId != null)
            {
                Response.Redirect("ModifySections.aspx?quesnn=" + lblQuestionnaireId.Text);
            }
        }
    }
    
    protected void ReviewSurvey_Click(object sender, EventArgs e)
    {
        var numCompletedItems = 0;
        foreach (ListItem item in chkPublishCheckList.Items)
        {
            if (!item.Selected)
            {
                item.Attributes.Add("class", "requireddata");
                numCompletedItems++;
            }
        }
        if(numCompletedItems > 0)
        {
            MessageBox.Show("Please make sure that you complete all your check list (marked in red).");
            return;
        }
        var row = gvProcesses.SelectedRow;
        row_review_process.Visible = false;
        if (row != null)
        {
            if (gvProcesses.SelectedDataKey != null)
            {
                var processDataKey = gvProcesses.SelectedDataKey["ProcessId"];
                if (processDataKey != null)
                {
                    var oParams = new DBParamCollection
                                      {
                                          {"@ProcessId", processDataKey}
                                      };
                    using (var oCommand = new DBCommand("[dbo].[uspReviewProcess]", QueryType.StoredProcedure, oParams, db.Connection))
                    {
                        var saved = oCommand.Execute();
                        if (saved > 0)
                        {
                            IncidentTrackingManager.SaveAuditTrail(SarsUser.SID,
                                                   string.Format(ResourceString.GetResourceString("reviewedProcess"),
                                                                 gvProcesses.SelectedRow.Cells[0].Text));
                            row_review_process.Visible = false;
                            LoadProcesses();
                            MessageBox.Show("Status of the process has been moved to Reviewed.");
                       
                            return;
                        }
                        MessageBox.Show("Oops there was a problem, try again later.");
                    }
                }
            }
        }
    }

    protected void ReviewSurvey(object sender, EventArgs e)
    {
        var row = gvProcesses.SelectedRow;
        if (row != null)
        {
            var lblSurveyStatusId = row.FindControl("lblSurveyStatusId") as Label;
            if (lblSurveyStatusId != null)
            {
                if (lblSurveyStatusId.Text != "1")
                {
                    btnReview.Enabled = false;
                    chkPublishCheckList.Enabled = false;
                }
                else
                {
                    btnReview.Enabled = true;
                    chkPublishCheckList.Enabled = true;
                }
            }
            gvProcesses.SelectedIndex = row.RowIndex;
            row_modfy_process.Visible = false;
            row_review_process.Visible = true;
            LoadProcesses();
        }
    }
    public bool CanReviewSelectedProcess
    {
        get
        {
            var row = gvProcesses.SelectedRow;
            if (row != null)
            {
                if (gvProcesses.SelectedDataKey != null)
                {
                    var processDataKey = gvProcesses.SelectedDataKey["StatusId"];
                    var processId = gvProcesses.SelectedDataKey["ProcessId"];
                    if (processDataKey != null)
                    {
                        if (processDataKey.ToString() == "1" && IncidentTrackingManager.GetProcessFieldCount(processId.ToString()) > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }       
    }

    public bool CanPublishSelectedProcess
    {
        get
        {
            var row = gvProcesses.SelectedRow;
            if (row != null)
            {
                if (gvProcesses.SelectedDataKey != null)
                {
                    var processStatusId = gvProcesses.SelectedDataKey["StatusId"];
                    var processId = gvProcesses.SelectedDataKey["ProcessId"];
                    if (processStatusId != null && processId != null)
                    {
                        if (processStatusId.ToString() == "2")
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }

    public bool CanAddUsers
    {
        get
        {
            var row = gvProcesses.SelectedRow;
            if (row != null)
            {
                if (gvProcesses.SelectedDataKey != null)
                {
                    var processStatusId = gvProcesses.SelectedDataKey["StatusId"];
                    var processId = gvProcesses.SelectedDataKey["ProcessId"];
                    if (processStatusId != null && processId != null)
                    {
                        if (processStatusId.ToString() == "3" && IncidentTrackingManager.GetProcessFieldCount(processId.ToString()) > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
    
    protected void PublishSurvey(object sender, EventArgs e)
    {
        var row = gvProcesses.SelectedRow;
        if (row != null)
        {
            if (gvProcesses.SelectedDataKey != null)
            {
                var processStatusId = gvProcesses.SelectedDataKey["StatusId"];
                var processId = gvProcesses.SelectedDataKey["ProcessId"];
                if (processStatusId != null && processId != null)
                {
                    if (processStatusId.ToString() == "2" && IncidentTrackingManager.GetProcessFieldCount(processId.ToString()) > 0)
                    {
                        IncidentTrackingManager.SaveAuditTrail(SarsUser.SID,
                                                   string.Format(ResourceString.GetResourceString("publishedProcess"),
                                                                 gvProcesses.SelectedRow.Cells[0].Text));
                        IncidentTrackingManager.PublishProcess(processId.ToString());
                        IncidentTrackingManager.UpdateProcessVersion(SarsUser.SID, processId.ToString());
                        LoadProcesses();
                        MessageBox.Show("Process was published successfully.");
                    }
                    else
                    {
                        MessageBox.Show("You cannot publish this process.");
                    }
                }
            }
        }
    }

    protected void SubmitProcessData(object sender, EventArgs e)
    {
        if (!ValidateForm())
            return;
        var name = txtProcessName.Text;
        var processId = Convert.ToInt64(ViewState["ProcId"]);
        var maFileSize = (Convert.ToDouble(txtFileSize.Text) * 1024D) * 1024D;
        var saved = IncidentTrackingManager.SaveProcess(processId, name, chkActive.Checked, txtPrefix.Text, Convert.ToString(Math.Floor(maFileSize)),ckAddCoverPage.Checked,chkCreater.Checked,ckCanShareIncidents.Checked,txtWorkingFolderUrl.Text);
        IncidentTrackingManager.SaveAuditTrail(SarsUser.SID, 
                                     string.Format(
                                         ResourceString.GetResourceString("modifiedProcess"),
                                         txtProcessName.Text));
        if (saved > 0)
        {
            IncidentTrackingManager.UpdateProcessVersion(SarsUser.SID, processId.ToString());
        }
        txtPrefix.Clear();
        LoadProcesses();
        btnsubmitNext.Enabled = false;
        row_modfy_process.Visible = false;
        MessageBox.Show("Process saved successfully.");
    }

    private bool ValidateForm()
    {
        if (txtProcessName.Text == string.Empty)
        {
            MessageBox.Show("Process name or description is required.");
            return false;
        }
        if (string.IsNullOrEmpty(txtFileSize.Text))
        {
            txtFileSize.Focus();
            MessageBox.Show("Muximum file ulpoad size is required.");
            return false;
        }
        decimal fSize;
        if(!decimal.TryParse(txtFileSize.Text, out fSize))
        {
            txtFileSize.Focus();
            MessageBox.Show("Muximum file ulpoad size must be a number.");
            return false;
        }
        return true;
    }

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        e.Row.Attributes.Add("onclick",
                             Page.ClientScript.GetPostBackEventReference((Control) sender, "Select$" + e.Row.RowIndex));
        var description = DataBinder.Eval(e.Row.DataItem, "Description").ToString();

        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        e.Row.Attributes.Add("title",
                             "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Full Description of the question</font></b>] body=[<font color='red'><b>" +
                             description + "</b></font>]");

        //var lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
        //if (lnkDelete != null)
        //{
        //    lnkDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to permanantly delete this survey?')");
        //}
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        if (gvProcesses.SelectedIndex == -1)
        {
            if (gvProcesses.Rows.Count == 1)
            {
                gvProcesses.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Please click on a process to select it before you can continue.");
                return;
            }
        }
        switch (e.CommandName)
        {
            case "Modify":
                {
                    lbOwners.Items.Clear();
                    btnsubmitNext.Enabled = true;
                    ModifyProcess(null, EventArgs.Empty);
                    btnsubmitNext.Focus();
                    break;
                }
            case "ViewFields":
                {
                    ViewFields();
                    break;
                }
            case "AddFields":
                {
                    AddFields();
                    break;
                }
            case "Review":
                {
                    ReviewSurvey(null, EventArgs.Empty);
                    break;
                }
            case "Publish":
                {
                    PublishSurvey(null, EventArgs.Empty);
                    break;
                }
            case "AddUsers":
                {
                    var row = gvProcesses.SelectedRow;
                    if (row != null)
                    {
                        if (gvProcesses.SelectedDataKey != null)
                        {
                            var processStatusId = gvProcesses.SelectedDataKey["StatusId"];
                            var processId = gvProcesses.SelectedDataKey["ProcessId"];
                            if (processStatusId != null && processId != null)
                            {
                                if (processStatusId.ToString() == "3" && IncidentTrackingManager.GetProcessFieldCount(processId.ToString()) > 0)
                                {
                                    Response.Redirect(string.Format("AddProcessUsers.aspx?procId={0}", processId));
                                }
                                else
                                {
                                    MessageBox.Show("You cannot add users to this process.");
                                }
                            }
                        }
                    }
                    
                    break;
                }
            case "ViewOwners":
                {
                    var row = gvProcesses.SelectedRow;
                    if (row != null)
                    {
                        if (gvProcesses.SelectedDataKey != null)
                        {
                            var processId = gvProcesses.SelectedDataKey["ProcessId"];
                            if ( processId != null)
                            {
                                Response.Redirect(string.Format("AddProcessOwners.aspx?procId={0}", processId));
                            }
                        }
                    }
                    break;
                }
            case "Reminders":
                {
                    var row = gvProcesses.SelectedRow;
                    if (row != null)
                    {
                        if (gvProcesses.SelectedDataKey != null)
                        {
                            var processId = gvProcesses.SelectedDataKey["ProcessId"];
                            if (processId != null)
                            {
                                Response.Redirect(string.Format("ConfigureReminders.aspx?procId={0}", processId));
                            }
                        }
                    }
                    break;
                }
        }
    }

    protected void SetBranching()
    {
        var row = gvProcesses.SelectedRow;
        if (row != null)
        {
            var lblQuestionnaireId = row.FindControl("lblQuestionnaireId") as Label;
            if (lblQuestionnaireId != null)
            {
                Response.Redirect("MapAnswerQuestionBranching.aspx?quesnn=" + lblQuestionnaireId.Text);
            }
        }
    }
   
    protected void gvProcesses_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        row_modfy_process.Visible = false;
        row_review_process.Visible = false;
        chkPublishCheckList.SelectedIndex = -1;

    }
    protected void SelectedIndexChanged(object sender, EventArgs e)
    {
        Toolbar1.Items[3].Visible = CanReviewSelectedProcess;
        Toolbar1.Items[4].Visible = CanPublishSelectedProcess;
        Toolbar1.Items[5].Visible = CanAddUsers;
    }
   
    protected void Sorting(object sender, GridViewSortEventArgs e)
    {
        var processes = IncidentTrackingManager.GetIncidentProcesses(SarsUser.SID);
        gvProcesses.SortView(processes, ViewState, e.SortExpression);
    }
    
}