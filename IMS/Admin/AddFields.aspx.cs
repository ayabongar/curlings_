using System;
using System.Collections;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;

public partial class Admin_AddFields : IncidentTrackingPage
{
    protected IncidentProcess currentProcess;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ProcessID))
        {
            Response.Redirect("~/InvalidProcessOrIncident.aspx");
        }
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("SelectNormalUserProcess.aspx");
        }
        currentProcess = CurrentProcess;
        if (!IsPostBack)
        {
           

            if (currentProcess.Version == 0M)
            {
                currentProcess.Version = 1.0M;
            }
            ddlFieldTypeId.Bind(IncidentTrackingManager.GetFieldTypes().OrderBy(f=>f.Description), "Description", "FieldTypeId");
            ddlScaleType.Bind(IncidentTrackingManager.GetScaleTypes().OrderBy(f => f.Description), "Description", "ScaleTypeId");
            ddlDimension.Bind(IncidentTrackingManager.GetMatrixDimensions(), "Dimensions", "MatrixDimensionId");
            ddlInputType.Bind(IncidentTrackingManager.GetTextValidations(), "Description", "ValidationtypeId");
            var lookUps = LookUpManager.ReadLookupsActive();
            if (lookUps!= null)
            {
                ddlLookup.Bind(lookUps, "Description", "LookupDataId");
            }
            var hlookups = LookUpManager.ReadHierarchyLookupsActive();
            if (hlookups != null)
            {
                ddlHierarchyLookUp.Bind(hlookups, "Description", "LookupDataId");
            }
            //LoadValues();
            UpdateQuestions();
        }
    }

    protected void btnAddQuestion_Click(object sender, EventArgs e)
    {
        if (rowMultichoiceOptions.Visible)
        {
            if (lbOptions.Items.Count == 0)
            {
                MessageBox.Show("You are required to add multichoice answers if the question type is Multichoice.");
                return;
            }
        }
        if (string.IsNullOrEmpty(txtFieldName.Text) )
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
        if (ddlFieldTypeId.SelectedIndex == 0)
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

        var fieldId = Save();
        if (fieldId > 0)
        {
            IncidentTrackingManager.SaveAuditTrail(SarsUser.SID,
                                         string.Format(
                                             ResourceString.GetResourceString("addedField"),
                                             txtFieldName.Text, CurrentProcess.Description));
            
            //SurveyPublisher.SortSurvey(Convert.ToInt32(Request["quesnn"]));
            if (lbOptions.Items.Count > 0)
            {

                AddMultiChoiceOptions(fieldId.ToString());
            }
            txtFieldName.Clear();
            txtDisplayName.Clear();
            txtMultiChoiceQuestionAnswer.Clear();

            chkIsActive.Checked =
                chkIsRequired.Checked =
                chkShowOnReport.Checked =
                chkShowOnScreen.Checked =
                chkShowOnSearch.Checked = false;

            ddlQuestionType_SelectedIndexChanged(ddlFieldTypeId, EventArgs.Empty);
            MessageBox.Show("Field saved successfully, you can add another one!");
            UpdateQuestions();
            lbOptions.Items.Clear();
        }
    }

    public int Save()
    {
        var oParams = new DBParamCollection
                          {
                              {"@FieldName", Utils.CleanHTMLData(txtFieldName.Text)},
                              {"@Display", txtDisplayName.Text},
                              {"@ProcessId", this.ProcessID},
                              {"@FieldTypeId", ddlFieldTypeId.SelectedValue},
                              {"@IsActive", chkIsActive.Checked},
                              {"@IsRequired", chkIsRequired.Checked},
                              {"@ShowOnSearch", chkShowOnSearch.Checked},
                              {"@ShowOnScreen", chkShowOnScreen.Checked},
                              {"@ShowOnReport", chkShowOnReport.Checked},
                              {"@AddedBySID", SarsUser.SID},
                              {"@AddToCoverPage", chkAddToCoverPage.Checked},
                              {"@CanSendEmail", chkEmailContent.Checked},
                              {"@EmailContent", editorEmailContent.Text},
                              {"@ShowOnAssigned", chkShowOnAssigned.Checked},
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
            var oCommand = new DBCommand("[dbo].[uspINSERT_ProcessField]", QueryType.StoredProcedure, oParams,
                                         db.Connection))
        {
            scopeIdentity = 0;
            oCommand.Execute(out oHashTable);
            //oCommand.Commit();
        }
        if (oHashTable.Count > 0)
        {
            scopeIdentity = int.Parse(oHashTable["@Return_Value"].ToString());
        }
        return scopeIdentity;
    }
    protected void dsQuestions_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@SubsectionId"].Value = Session["subSecId"];
    }

    private void UpdateQuestions()
    {
        var data = IncidentTrackingManager.GetProcessField(ProcessID);
        if(data != null && data.Any())
        {
            fsFields.Visible = true;
            gvProcessFields.Bind(data);
        }
        else
        {
            fsFields.Visible = false;
            gvProcessFields.Bind(null);
        }
    }

    protected void gvQuestions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProcessFields.NextPage(IncidentTrackingManager.GetProcessField(ProcessID), e.NewPageIndex);
    }

    protected void ddlQuestionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        rowMultichoiceOptions.Visible =
            ddlFieldTypeId.SelectedValue == "5" ||
            ddlFieldTypeId.SelectedValue == "16" ||
            ddlFieldTypeId.SelectedValue == "17" ||
            ddlFieldTypeId.SelectedValue == "18" ||
            ddlFieldTypeId.SelectedValue == "19";

        row_scaletypes.Visible = ddlFieldTypeId.SelectedValue == "1";
        row_matrix_dimension.Visible = ddlFieldTypeId.SelectedValue == "9";
        row_validation_types.Visible = ddlFieldTypeId.SelectedValue == "6";

        //row_lookups.Visible = ddlFieldTypeId.SelectedValue == "13";
        //row_hierarchy_lookups.Visible = ddlFieldTypeId.SelectedValue == "20";

        if(ddlFieldTypeId.SelectedValue == "13")
        {
            if (ddlLookup.Items.Count == 0)
            {
                ddlFieldTypeId.SelectedIndex = -1;
                MessageBox.Show("There are no items for this field type.");
                return;
            }
            row_lookups.Visible = true;
        }
        else
        {
            row_lookups.Visible = false;
  
        }


        if (ddlFieldTypeId.SelectedValue == "20")
        {
            if (ddlHierarchyLookUp.Items.Count == 0)
            {
                ddlFieldTypeId.SelectedIndex = -1;
                MessageBox.Show("There are no items for this field type.");
                return;
            }
            row_hierarchy_lookups.Visible = true;
        }
        else
        {
            row_hierarchy_lookups.Visible = false;

        }
        if (ddlFieldTypeId.SelectedIndex > 0)
        {
            if (ddlFieldTypeId.SelectedValue != "14")
            {
                chkEmailContent.Checked = false;
            }
            ShowEmailContent(int.Parse(ddlFieldTypeId.SelectedValue));
        }
    }

    protected void btnAddOption_Click(object sender, EventArgs e)
    {
        if (txtMultiChoiceQuestionAnswer.Text == string.Empty)
        {
            MessageBox.Show("You must enter an option.");
            return;
        }
        
        var itm = new ListItem(txtMultiChoiceQuestionAnswer.Text);
        if (lbOptions.Items.Contains(itm))
        {
            MessageBox.Show("You can not repeate the same option you added.");
        }
        else
        {
            lbOptions.Items.Add(itm);
            txtMultiChoiceQuestionAnswer.Clear();
            txtMultiChoiceQuestionAnswer.Focus();
        }
    }

    private void AddMultiChoiceOptions(string fieldId)
    {
        foreach (ListItem item in lbOptions.Items)
        {
            IncidentTrackingManager.SaveAuditTrail(SarsUser.SID,
                                         string.Format(
                                             ResourceString.GetResourceString("addedQuestionOptions"),
                                             item.Text));
           
            SurveyGenerator.AddMultiChoiceOptions(this.ProcessID, fieldId, item.Text);
        }
    }

    protected void gvQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var desctiontion = DataBinder.Eval(e.Row.DataItem, "FieldName").ToString();
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='cursor'");
            e.Row.Attributes.Add("title",
                                 "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Full Field Name</font></b>] body=[<font color='red'><b>" +
                                 desctiontion + "</b></font>]");
        }
    }

    protected void ddlScaleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        rowMultichoiceOptions.Visible = ddlScaleType.SelectedValue == 5.ToString();
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        var removeItms = lbOptions.Items.Cast<ListItem>().Where(itm => itm.Selected).ToList();
        if (lbOptions.Items.Count == 0)
        {
            MessageBox.Show("There are no items to remove");
            return;
        }
        if (removeItms.Count == 0)
        {
            MessageBox.Show("Please click on an item to remove.");
            return;
        }
        foreach (var itm in removeItms)
        {
            lbOptions.Items.Remove(itm);
        }
    }

    private void ShowEmailContent(int fieldTypeId)
    {
        trCreateEmail.Visible = fieldTypeId.Equals(14) ? true : false;
        trEmailContent.Visible = chkEmailContent.Checked ? true : false;
    }
    protected void chkEmailContent_CheckedChanged(object sender, EventArgs e)
    {
        trEmailContent.Visible = chkEmailContent.Checked ? true : false;
    }
}