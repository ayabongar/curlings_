using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Sars.Systems.Data;

public partial class Admin_ModifyProcessField : IncidentTrackingPage
{
    protected IncidentProcess currentProcess;
    protected void Page_Load(object sender, EventArgs e)
    {
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("~/NoAccessForm.aspx");
        }
        currentProcess = CurrentProcess;
        if (CurrentProcess.Version == 0)
        {
            CurrentProcess.Version = 1.0M;
        }
        if (!string.IsNullOrEmpty(FieldID) && !string.IsNullOrEmpty(ProcessID))
        {
            if (!IsPostBack)
            {
                ddlFieldType.Bind(IncidentTrackingManager.GetFieldTypes().OrderBy(f=>f.Description), "Description", "FieldTypeId");
                ddlScaleType.Bind(IncidentTrackingManager.GetScaleTypes().OrderBy(f => f.Description), "Description", "ScaleTypeId");
                ddlDimension.Bind(IncidentTrackingManager.GetMatrixDimensions(), "Dimensions", "MatrixDimensionId");
                ddlInputType.Bind(IncidentTrackingManager.GetTextValidations(), "Description", "ValidationtypeId");
                ddlLookup.Bind(LookUpManager.ReadLookupsActive(), "Description", "LookupDataId");
                var hl = LookUpManager.ReadHierarchyLookupsActive();
                if (hl != null)
                {
                    ddlHierarchyLookUp.Bind(hl, "Description", "LookupDataId");
                }

                LoadFieldDetails();
                SelectQuestionsType(ddlFieldType, EventArgs.Empty);
                ddlScaleType_SelectedIndexChanged(ddlScaleType, EventArgs.Empty);
                if (ddlFieldType.SelectedValue == "14")
                {
                    ShowEmailContent(int.Parse(ddlFieldType.SelectedValue));
                }
            }
        }
    }

    private void ShowEmailContent(int fieldTypeId)
    {
        trCreateEmail.Visible = fieldTypeId.Equals(14) ? true :false;
        trEmailContent.Visible = chkEmailContent.Checked ? true : false;
    }
    private void LoadFieldDetails()
    {
        var fieldDetails = IncidentTrackingManager.GetFieldById(this.FieldID);
        if (fieldDetails != null)
        {
            this.txtDisplayName.SetValue(fieldDetails.Display);
            this.txtFieldName.SetValue(fieldDetails.FieldName);
            this.ddlFieldType.SelectItemByValue(fieldDetails.FieldTypeId.ToString());

            var fieldTypeId = fieldDetails.FieldTypeId;
            var scaleTypeId = 0;

            if (fieldDetails.ScaleTypeId != null)
            {
                scaleTypeId = fieldDetails.ScaleTypeId.Value;
                ddlScaleType.SelectedValue = scaleTypeId.ToString(CultureInfo.InvariantCulture);
            }
            if (fieldDetails.ValidationTypeId != null)
            {
                ddlInputType.SelectedValue = fieldDetails.ValidationTypeId.ToString();
            }
            if (fieldDetails.MatrixDimensionId != null)
            {
                ddlDimension.SelectedValue = fieldDetails.MatrixDimensionId.ToString();
            }

            if (fieldTypeId == 5 || (fieldTypeId == 1 && scaleTypeId == 5) || fieldTypeId == 16 || fieldTypeId == 17 ||
                fieldTypeId == 18 || fieldTypeId == 19)
            {
                LoadMultiplehoiceQuestions();
            }
            ddlLookup.SelectedValue = Convert.ToString(fieldDetails.LookupDataId);

            ddlHierarchyLookUp.SelectedValue = Convert.ToString(fieldDetails.HierarchyLookupId);
            chkIsActive.Checked = fieldDetails.IsActive;
            chkIsRequired.Checked = fieldDetails.IsRequired;
            chkAddToCoverPage.Checked = fieldDetails.AddToCoverPage;
            chkShowOnAssigned.Checked = fieldDetails.ShowOnAssigned;
            chkEmailContent.Checked = fieldDetails.CanSendEmail;
            if (fieldDetails.ShowOnReport != null) chkShowOnReport.Checked = fieldDetails.ShowOnReport.Value;
            if (fieldDetails.ShowOnScreen != null) chkShowOnScreen.Checked = fieldDetails.ShowOnScreen.Value;
            if (fieldDetails.ShowOnSearch != null) chkShowOnSearch.Checked = fieldDetails.ShowOnSearch.Value;
            editorEmailContent.Text = fieldDetails.EmailContent;
            var adUser = SarsUser.GetADUser(fieldDetails.DefaultCCPersonSID);
            if (adUser != null)
            {
                UserSelector1.SelectedAdUserDetails = new SelectedUserDetails
                {
                    SID = adUser.SID,
                    FoundUserName = string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                    FullName = adUser.FullName
                };
            }
        }
    }

    private void LoadMultiplehoiceQuestions()
    {
        var options = IncidentTrackingManager.ReadQuestionOptions(this.FieldID);
        if (options != null && options.Count > 0)
        {
            gvOptions.Bind(options);
        }
    }

    protected void AddOption(object sender, EventArgs e)
    {
        if (txtMultiChoiceQuestionAnswer.Text == string.Empty)
        {
            MessageBox.Show("You must enter an option.");
            return;
        }
        var itm = new ListItem(txtMultiChoiceQuestionAnswer.Text);
        if (lbOptions.Items.Contains(itm))
        {
            MessageBox.Show("You can not repeate the same answer you added.");
        }
        else
        {
            var saved = AddMultiChoiceOptions();

            if (saved > 0)
            {

                IncidentTrackingManager.SaveAuditTrail(SarsUser.SID,
                                             string.Format(
                                                 ResourceString.GetResourceString("addedQuestionOptions"),
                                                 txtMultiChoiceQuestionAnswer.Text));

                LoadMultiplehoiceQuestions();
                MessageBox.Show("Multiple choice option added.");
                txtMultiChoiceQuestionAnswer.Clear();
                txtMultiChoiceQuestionAnswer.Focus();
            }
            else
            {
                MessageBox.Show("Option was not added, try again.");
            }
        }
    }

    private int AddMultiChoiceOptions()
    {
        return SurveyGenerator.AddMultiChoiceOptions
            (
                this.ProcessID,
                this.FieldID,
                txtMultiChoiceQuestionAnswer.Text
            );
    }

    protected void ModifyOption(object sender, EventArgs e)
    {
        if (ViewState["mcoi"] != null)
        {
            var multiChaiceOptionId = Convert.ToInt32(ViewState["mcoi"]);
            SaveOption(multiChaiceOptionId);
            txtOption.Clear();
        }
    }

    private void SaveOption(int multichoiceOptionId)
    {
        var saved =
            SurveyGenerator.ModifyMultiChoiceOption(new MultichoiceOption
            {
                MultichoiceOptionId = multichoiceOptionId,
                OptionDescription = txtOption.Text
            });

        if (saved > 0)
        {
            LoadMultiplehoiceQuestions();
            row_modifyOption.Visible = false;
            MessageBox.Show("Option Saved");
        }
    }

    protected void SaveField(object sender, EventArgs e)
    {
        if (rowMultichoiceOptions.Visible)
        {
            if (lbOptions.Items.Count == 0 && gvOptions.Rows.Count <= 0)
            {
                MessageBox.Show("You are required to add multichoice answers if the question type is Multichoice.");
                return;
            }
        }
        if (string.IsNullOrEmpty(txtFieldName.Text))
        {
            txtFieldName.Focus();
            MessageBox.Show("Please enter a Field Name description.");
            return;
        }

        if (string.IsNullOrEmpty(txtDisplayName.Text))
        {
            txtDisplayName.Focus();
            MessageBox.Show("Please enter a Label description.");
            return;
        }
        if (ddlFieldType.SelectedIndex == 0)
        {
            MessageBox.Show("Please select a field type.");
            return;
        }
        if (row_scaletypes.Visible)
        {
            if (ddlScaleType.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a scale type.");
                return;
            }
        }
        if (row_matrix_dimension.Visible)
        {
            if (ddlDimension.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a dimension.");
                return;
            }
        }

        if (row_matrix_dimension.Visible)
        {
            if (ddlDimension.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a dimension.");
                return;
            }
        }
        if (row_lookups.Visible)
        {
            if (ddlLookup.SelectedIndex == 0)
            {
                MessageBox.Show(
                    "Please select a Lookup of your choice, if it does not exist please ask administrators to add it.");
                return;
            }
        }
        if (row_hierarchy_lookups.Visible)
        {
            if (ddlHierarchyLookUp.SelectedIndex <= 0)
            {
                MessageBox.Show(
                    "Please select a Lookop of your choice, if it does not exist please ask administrators to add it.");
                return;
            }
        }

        Save();

        IncidentTrackingManager.SaveAuditTrail(SarsUser.SID,
                                     string.Format(
                                         ResourceString.GetResourceString("modifiedField"),
                                         txtFieldName.Text, CurrentProcess.Description));

        //if (lbOptions.Items.Count > 0)
        //{
        //    AddMultiChoiceOptions();
        //}
        txtFieldName.Clear();
        txtDisplayName.Clear();
        txtMultiChoiceQuestionAnswer.Clear();

        chkIsActive.Checked =
            chkIsRequired.Checked =
            chkShowOnReport.Checked =
            chkShowOnScreen.Checked =
            chkShowOnSearch.Checked = false;

        lbOptions.Items.Clear();
        Response.Redirect(string.Format("ViewFields.aspx?procId={0}", ProcessID));
    }

    public int Save()
    {
        var oParams = new DBParamCollection
                          {
                              {"@ProcessFieldId",this.FieldID},
                              {"@FieldName", Utils.CleanHTMLData(txtFieldName.Text)},
                              {"@Display", txtDisplayName.Text},
                              {"@FieldTypeId", ddlFieldType.SelectedValue},
                              {"@IsActive", chkIsActive.Checked},
                              {"@IsRequired", chkIsRequired.Checked},
                              {"@ShowOnSearch", chkShowOnSearch.Checked},
                              {"@ShowOnScreen", chkShowOnScreen.Checked},
                              {"@ShowOnReport", chkShowOnReport.Checked},
                              {"@LastModifiedBySID", SarsUser.SID},
                              {"@AddToCoverPage", chkAddToCoverPage.Checked},
                              {"@CanSendEmail", chkEmailContent.Checked},
                              {"@EmailContent", editorEmailContent.Text},
                              {"@ShowOnAssigned", chkShowOnAssigned.Checked},
                               {"@DefaultCCPersonSID", UserSelector1.SID},
                              {"@Return_Value", null, System.Data.ParameterDirection.ReturnValue}
                          };
        if (row_scaletypes.Visible)
        {
            oParams.Add("@ScaleTypeId", ddlScaleType.SelectedValue);
        }

        if (row_matrix_dimension.Visible)
        {
            oParams.Add("@MatrixDimensionId", ddlDimension.SelectedValue);
        }
        if (row_validation_types.Visible)
        {
            oParams.Add("@ValidationTypeId", ddlInputType.SelectedValue);
        }
        if (row_lookups.Visible)
        {
            oParams.Add("@LookupDataId", ddlLookup.SelectedValue);
        }
        if (row_hierarchy_lookups.Visible)
        {
            oParams.Add("@HierarchyLookupId", ddlHierarchyLookUp.SelectedValue);
        }

        Hashtable oHashTable;
        int scopeIdentity;
        using (
            var oCommand = new DBCommand("[dbo].[uspUPDATE_ProcessField]", QueryType.StoredProcedure, oParams,
                                         db.Connection))
        {
            scopeIdentity = 0;
            var saved = oCommand.Execute(out oHashTable);
            oCommand.Commit();
            if(saved >0)
            {
                IncidentTrackingManager.UpdateProcessVersion(SarsUser.SID, ProcessID);
            }
        }
        if (oHashTable.Count > 0)
        {
            scopeIdentity = int.Parse(oHashTable["@Return_Value"].ToString());
        }
        return scopeIdentity;
    }

    protected void SelectQuestionsType(object sender, EventArgs e)
    {
        rowMultichoiceOptions.Visible =
            ddlFieldType.SelectedValue == "5" ||
            ddlFieldType.SelectedValue == "16" ||
            ddlFieldType.SelectedValue == "17" ||
            ddlFieldType.SelectedValue == "18" ||
            ddlFieldType.SelectedValue == "19";
        row_User.Visible = ddlFieldType.SelectedValue == "22";
        row_scaletypes.Visible = ddlFieldType.SelectedValue == "1";
        row_matrix_dimension.Visible = ddlFieldType.SelectedValue == "9";
        row_validation_types.Visible = ddlFieldType.SelectedValue == "6";
        row_lookups.Visible = ddlFieldType.SelectedValue == "13";
        row_hierarchy_lookups.Visible = ddlFieldType.SelectedValue == "20";
        if (ddlFieldType.SelectedIndex > 0)
        {
            if (ddlFieldType.SelectedValue != "14")
            {
                chkEmailContent.Checked = false;
            }
            ShowEmailContent(int.Parse(ddlFieldType.SelectedValue));
        }
    }


    protected void ddlScaleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFieldType.SelectedValue == 1.ToString())
        {
            rowMultichoiceOptions.Visible = ddlScaleType.SelectedValue == 5.ToString();
        }
    }


    protected void lnkRemove_Click(object sender, EventArgs e)
    {
        var lnkUpdate = sender as LinkButton;
        if (lnkUpdate != null)
        {
            var row = lnkUpdate.Parent.Parent as GridViewRow;
            if (row != null)
            {
                var lblMultichoiceOptionId = row.FindControl("lblMultichoiceOptionId") as Label;

                if (lblMultichoiceOptionId != null)
                {
                    var oParams = new DBParamCollection
                                      {
                                          {"@MultichoiceOptionId", lblMultichoiceOptionId.Text}
                                      };

                    using (var oCommand = new DBCommand("uspREMOVE_MultichoiceOptions", QueryType.StoredProcedure, oParams,db.Connection))
                    {
                        var saved = oCommand.Execute();
                        if (saved > 0)
                        {
                            IncidentTrackingManager.SaveAuditTrail(SarsUser.SID,
                                                         string.Format(
                                                             ResourceString.GetResourceString("removedOption"),
                                                             lblMultichoiceOptionId.Text));
                       
                            LoadMultiplehoiceQuestions();
                            MessageBox.Show("Option removed");
                        }
                    }
                }
            }
        }
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        var lnkUpdate = sender as LinkButton;
        if (lnkUpdate != null)
        {
            var row = lnkUpdate.Parent.Parent as GridViewRow;
            if (row != null)
            {
                var lblMultichoiceOptionId = row.FindControl("lblMultichoiceOptionId") as Label;
                if (lblMultichoiceOptionId != null)
                {
                    ViewState["mcoi"] = lblMultichoiceOptionId.Text;
                    txtOption.Text = row.Cells[0].Text;
                    row_modifyOption.Visible = true;
                }
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("ViewFields.aspx?procId={0}", ProcessID));
    }
    protected void chkEmailContent_CheckedChanged(object sender, EventArgs e)
    {

        trEmailContent.Visible = chkEmailContent.Checked ? true : false;
    }
}