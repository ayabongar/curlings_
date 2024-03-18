using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP;
using Sars.Systems.Controls;
using Sars.Systems.Data;

public partial class SurveyWizard_DisplayCoverPage : IncidentTrackingControl
{

    public int SortOrder
    {
        get
        {
            var obj = ViewState["sOrder"];
            return (obj == null) ? 0 : Convert.ToInt32(obj);
        }
        set { ViewState["sOrder"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Remove("quesnn_data");

        if (string.IsNullOrEmpty(ProcessID))
        {
            MessageBox.Show("Please select a survey, or use a link that was sent to you.");
            return;
        }
        if (!IsPostBack)
        {
            var fields = IncidentTrackingManager.GetCoverPageIncidentFields(IncidentID);
            if (fields != null)
            {
                gvQuestions.Bind(fields);
                //ApplyDropdownSkipLogic();
            }
        }
    }

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        var questionDisplay = e.Row.FindControl("QuestionDisplay1") as ntqr_surveywizard_questiondisplay_ascx;
        if (questionDisplay != null)
        {

            const string style = "style.backgroundColor='ghostwhite';style.cursor='default'";
            e.Row.Attributes.Add("onmouseover", style);
            e.Row.Attributes.Add("onmouseout", "style.backgroundColor='White'");

            questionDisplay.FieldId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FieldId"));
            questionDisplay.FieldType = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FieldTypeId"));

            switch (questionDisplay.FieldType)
            {
                case 1:
                    questionDisplay.ScaleTypeId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ScaleTypeId"));
                    break;
                case 9:
                    questionDisplay.MatrixDimensionId =
                        Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MatrixDimensionId"));
                    break;
                case 6:
                    questionDisplay.ValidationType = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ValidationTypeId"));
                    break;
                case 13:
                    questionDisplay.LookupDataId = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LookupDataId"));
                    break;

                case 20:
                    {
                        questionDisplay.HierarchyLookupDataId =
                            Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "HierarchyLookupId"));
                        break;
                    }
            }
            questionDisplay.SortOrder = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SortOrder"));
            questionDisplay.IsParent = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsParent"));
            questionDisplay.IsChild = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsChild"));
            questionDisplay.IsRequiredField = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsRequired"));
            SortOrder = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SortOrder"));
            questionDisplay.Display = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Display"));
            questionDisplay.CurrentRowIndex = e.Row.RowIndex;

            questionDisplay.ShowQuestions();

            string incidentId;
            if (!String.IsNullOrEmpty(Request["cpd"]) && !string.IsNullOrEmpty(Request["oldIncId"]))
            {
                incidentId = String.IsNullOrEmpty(Request["oldIncId"]) ? IncidentID : Request["oldIncId"];
            }
            else
            {
                incidentId = IncidentID;
            }
            if (questionDisplay.IsChild)
            {
                e.Row.Visible = false;
            }
            if (!string.IsNullOrEmpty(incidentId))
            {
                if (questionDisplay.FieldType != 9 && questionDisplay.FieldType != 20 && questionDisplay.FieldType != 22)
                {
                    decimal selectedAns;
                    IncidentTrackingManager.SetSelectedQuestionAnswer
                        (
                            questionDisplay.ThisControl,
                            questionDisplay.FieldId,
                            questionDisplay.FieldType,
                            questionDisplay.ScaleTypeId,
                            Convert.ToInt32(incidentId),
                            out selectedAns
                        );
                    if (selectedAns != 0M)
                    {
                        OptionChanged(null, new IntelliQuestionArgs
                        {
                            FieldId = questionDisplay.FieldId,
                            IsParent = questionDisplay.IsParent,
                            RowIndex = e.Row.RowIndex,
                            SelectedOption = selectedAns
                        });
                    }

                    //if (questionDisplay.IsChild)
                    //{
                    //    if (questionDisplay.FieldType == 16)
                    //    {
                    //        questionDisplay.RunRadioButtonEvents();
                    //    }
                    //    if (questionDisplay.FieldType == 17)
                    //    {
                    //        questionDisplay.RunDropDownEvents();
                    //    }
                    //    if (questionDisplay.FieldType == 5)
                    //    {
                    //        questionDisplay.RunCheckBoxEvents();
                    //    }
                    //}
                }
                else
                {
                    if (questionDisplay.FieldType == 9)
                    {
                        LoadMatrixValues(incidentId, questionDisplay);
                    }
                    if (questionDisplay.FieldType == 20)
                    {
                        LoadHierarchyValues(incidentId, questionDisplay);
                    }
                    else
                    {
                        if (questionDisplay.FieldType == 22)
                        {
                            LoadADUserValues(incidentId, questionDisplay);
                        }
                    }
                }
            }
        }
    }

    private static void LoadMatrixValues(string incidentId, ntqr_surveywizard_questiondisplay_ascx questionDisplay)
    {
        var oParams = new DBParamCollection
                          {
                              {"@FieldId", questionDisplay.FieldId},
                              {"@FieldTypeId", questionDisplay.FieldType},
                              {"@IncidentId", Convert.ToInt32(incidentId)}
                          };
        using (
            var oCommand = new DBCommand("uspREAD_QuestionResponse", QueryType.StoredProcedure, oParams, db.Connection))
        {
            var answers = oCommand.ExecuteReader();
            if (answers.HasRows)
            {
                var matrix = questionDisplay.ThisControl as SurveyWizard_MatrixQuestion;
                if (matrix != null)
                {
                    answers.Read();
                    var matrixValues = new MatrixValues
                    {
                        LeftValue = answers["LeftDimensionAnswer"].ToString(),
                        RightValue = answers["RightDimensionAnswer"].ToString()
                    };
                    matrix.SelectedValues = matrixValues;
                }
            }
            answers.Close();
            answers.Dispose();
        }
    }

    private static void LoadHierarchyValues(string incidentId, ntqr_surveywizard_questiondisplay_ascx questionDisplay)
    {
        var oParams = new DBParamCollection
                          {
                              {"@FieldId", questionDisplay.FieldId},
                              {"@FieldTypeId", questionDisplay.FieldType},
                              {"@IncidentId", Convert.ToInt32(incidentId)}
                          };
        using (
            var data = new RecordSet("uspREAD_QuestionResponse", QueryType.StoredProcedure, oParams, db.ConnectionString)
            )
        {
            if (data.HasRows)
            {
                var hierarchicallookup = questionDisplay.ThisControl as ntqr_surveywizard_hierarchicallookup_ascx;
                if (hierarchicallookup != null)
                {
                    var row = data[0];
                    var selectedHierarchical = new SelectedHierarchicalDetails
                    {
                        FirstLevel = Convert.ToDecimal(row["Level1Answer"]),
                        SecondLevel =
                            row["Level2Answer"] != DBNull.Value
                                ? Convert.ToDecimal(row["Level2Answer"])
                                : 0,
                        ThirdLevel =
                            row["Level3Answer"] != DBNull.Value
                                ? Convert.ToDecimal(row["Level3Answer"])
                                : 0
                    };
                    hierarchicallookup.PopulateData(selectedHierarchical);
                }
            }
        }
    }

    private static void LoadADUserValues(string incidentId, ntqr_surveywizard_questiondisplay_ascx questionDisplay)
    {
        var oParams = new DBParamCollection
                          {
                              {"@FieldId", questionDisplay.FieldId},
                              {"@FieldTypeId", questionDisplay.FieldType},
                              {"@IncidentId", Convert.ToInt32(incidentId)}
                          };
        using (
            var answers = new RecordSet("uspREAD_QuestionResponse", QueryType.StoredProcedure, oParams,
                                        db.ConnectionString))
        {
            if (answers.HasRows)
            {
                var adUser = SarsUser.GetADUser(answers[0]["Answer"].ToString());
                if (adUser != null)
                {
                    var userSelector = questionDisplay.ThisControl as ASP.admin_userselector_ascx;

                    if (userSelector != null)
                    {
                        userSelector.SelectedAdUserDetails = new SelectedUserDetails
                        {
                            SID = adUser.SID,
                            FoundUserName =
                                string.Format("{0} | {1}", adUser.FullName,
                                              adUser.SID),
                            FullName = adUser.FullName
                        };
                    }
                }
            }
        }
    }

    private static void ChangeColor(Control c, int questinType, int scaleType, bool addColor)
    {
        switch (questinType)
        {
            case 16:
                {
                    var control = c as RadioButtonList;
                    if (control != null)
                    {
                        //control.CssClass = !addColor ? string.Empty : "requireddata";
                        control.BorderColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        control.BorderStyle = !addColor ? BorderStyle.NotSet : BorderStyle.Dashed;
                        control.BorderWidth = 1;
                    }
                    break;
                }

            case 1:
            case 12:
                if (scaleType != 5)
                {
                    var control = c as RadioButtonList;
                    if (control != null)
                    {
                        //control.CssClass = !addColor ? string.Empty : "requireddata";
                        control.BorderColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        control.BorderStyle = !addColor ? BorderStyle.NotSet : BorderStyle.Dashed;
                        control.BorderWidth = 1;
                    }
                }
                else
                {
                    var control = c as DropDownList;
                    if (control != null)
                    {
                        //control.BackColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        control.BorderColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        control.BorderStyle = !addColor ? BorderStyle.NotSet : BorderStyle.Dashed;
                        control.BorderWidth = 1;
                    }
                }
                break;
            case 5:
                {
                    var control = c as CheckBoxList;
                    if (control != null)
                    {
                        //control.CssClass = !addColor ? string.Empty : "requireddata";
                        control.BorderColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        control.BorderStyle = !addColor ? BorderStyle.NotSet : BorderStyle.Dashed;
                        control.BorderWidth = 1;
                    }
                    break;
                }
            case 6:
            case 10:
            case 11:
            case 14:
                {
                    var control = c as TextBox;
                    if (control != null)
                    {
                        //control.BackColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        //control.CssClass = !addColor ? string.Empty : "requireddata";
                        control.BorderColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        control.BorderStyle = !addColor ? BorderStyle.NotSet : BorderStyle.Dashed;
                        control.BorderWidth = 1;
                    }

                    break;
                }
            case 9:
                {
                    var control = c as ntqr_surveywizard_matrixquestion_ascx;

                    if (control != null)
                    {
                        control.LeftColumn.CssClass = control.LeftColumn.SelectedIndex == -1
                                                          ? "requireddata"
                                                          : string.Empty;
                        control.RightColumn.CssClass = control.RightColumn.SelectedIndex == -1
                                                           ? "requireddata"
                                                           : string.Empty;
                    }

                    break;
                }
            case 13:
            case 17:
                {
                    var control = c as DropDownList;
                    if (control != null)
                    {
                        //control.BackColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        control.BorderColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        control.BorderStyle = !addColor ? BorderStyle.NotSet : BorderStyle.Dashed;
                        control.BorderWidth = 1;
                    }
                    break;
                }
            case 18:
                {
                    var control = c as CheckBoxListWithOther;
                    if (control != null)
                    {
                        control.OtherCssClass = !addColor ? string.Empty : "requireddata";
                    }
                    break;
                }
            case 19:
                {
                    var control = c as RadioButtonListWithOther;
                    if (control != null)
                    {
                        control.OtherCssClass = !addColor ? string.Empty : "requireddata";
                    }
                    break;
                }
            case 20:
                {
                    var control = c as ntqr_surveywizard_hierarchicallookup_ascx;
                    if (control != null)
                    {
                        control.SetValidationColours();
                    }
                    break;
                }
            case 22:
                {
                    var control = c as admin_userselector_ascx;
                    if (control != null)
                    {
                        control.SetValidationColours();
                    }
                    break;
                }
        }
    }

    public bool SaveQuestions()
    {
        var numInvalidRequiredAanswers = 0;
        foreach (GridViewRow row in gvQuestions.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                var questionDisplay = row.FindControl("QuestionDisplay1") as ntqr_surveywizard_questiondisplay_ascx;
                if (questionDisplay != null)
                {
                    if (questionDisplay.IsRequiredField)
                    {
                        if (questionDisplay.SelectedAnswer == null)
                        {
                            if (questionDisplay.ThisControl != null)
                            {
                                ChangeColor(questionDisplay.ThisControl, questionDisplay.FieldType,
                                            questionDisplay.ScaleTypeId, true);
                                numInvalidRequiredAanswers++;
                            }
                        }
                        else
                        {
                            ChangeColor(questionDisplay.ThisControl, questionDisplay.FieldType,
                                        questionDisplay.ScaleTypeId, false);
                        }
                    }
                }
                if (questionDisplay != null)
                {
                    questionDisplay.Dispose();
                }
            }
        }
        if (numInvalidRequiredAanswers > 0)
        {
            //MessageBox.Show("Please answer all questions.");
            //return false;
        }

        foreach (GridViewRow row in gvQuestions.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                var questionDisplay = row.FindControl("QuestionDisplay1") as ntqr_surveywizard_questiondisplay_ascx;
                if (questionDisplay != null)
                {
                    if (questionDisplay.SelectedAnswer != null)
                    {
                        SaveAnswers(
                            IncidentID,
                            questionDisplay.ProcessID,
                            questionDisplay.FieldId,
                            questionDisplay.FieldType,
                            questionDisplay.SelectedAnswer
                            );
                    }
                }
                if (questionDisplay != null)
                {
                    questionDisplay.Dispose();
                }
            }
        }
        return true;
    }

    private static void SaveAnswers(string incidentId, string processId, decimal questionId, int questionTypeId, object answer)
    {
        if (!string.IsNullOrEmpty(incidentId))
        {
            var oSurveyGen = new SurveyGenerator(processId);

            switch (questionTypeId)
            {
                case 1:
                case 16:
                case 17:
                    {
                        oSurveyGen.SaveScaleAnswers(answer.ToString(), questionId, incidentId);
                        break;
                    }
                case 5:
                    {
                        var selectedAnswers = (List<int>)answer;
                        oSurveyGen.RemoveMultiChoiceAnswers(questionId, incidentId);
                        foreach (var i in selectedAnswers)
                        {
                            oSurveyGen.SaveMultiChoiceAnswers(i, questionId, incidentId);
                        }
                        selectedAnswers.Clear();
                        break;
                    }
                case 6:
                case 10:
                case 11:
                    {
                        oSurveyGen.SaveFreeTextAnswers(answer.ToString(), questionId, incidentId);
                        break;
                    }
                case 9:
                    {
                        var selectedAnswers = (MatrixValues)answer;
                        oSurveyGen.SaveMatrixOptionAnswers(selectedAnswers.LeftValue, selectedAnswers.RightValue,
                                                           questionId, incidentId);
                        break;
                    }
                case 13:
                    {
                        oSurveyGen.SaveLookupAnswers(answer.ToString(), questionId, incidentId);
                        break;
                    }
                case 18:
                    {
                        var options = (List<object>)answer;
                        oSurveyGen.CleanMultiAnswersWithOther(questionId, incidentId);
                        foreach (var option in options)
                        {
                            if (option is ListItem)
                            {
                                var item = (ListItem)option;
                                if (!item.Text.Equals("Other"))
                                {
                                    oSurveyGen.SaveMultiChoiceAnswers(Convert.ToInt32(item.Value), questionId,
                                                                      incidentId);
                                }
                            }
                            else
                            {
                                oSurveyGen.SaveMultiChoiceAnswers(-100, questionId, incidentId);
                                oSurveyGen.SaveFreeTextAnswers(option.ToString(), questionId, incidentId);
                            }
                        }
                        break;
                    }
                case 19:
                    {
                        int test;
                        if (!int.TryParse(answer.ToString(), out test))
                        {
                            oSurveyGen.SaveFreeTextAnswers(answer.ToString(), questionId, incidentId);
                        }
                        else
                        {
                            oSurveyGen.SaveScaleAnswers(answer.ToString(), questionId, incidentId);
                        }
                        break;
                    }
                case 20:
                    {
                        var selectedHierarchy = answer as SelectedHierarchicalDetails;
                        if (selectedHierarchy != null)
                        {
                            oSurveyGen.SaveScaleHierarchyookUpAnswers(selectedHierarchy, questionId, incidentId);
                        }
                        break;
                    }
                case 22:
                    {
                        var selected = answer as SelectedUserDetails;
                        if (selected != null)
                        {
                            if (!string.IsNullOrEmpty(selected.SID))
                            {
                                SarsUser.SaveUser(selected.SID);
                                oSurveyGen.SaveFreeTextAnswers(selected.SID, questionId, incidentId);
                            }
                        }
                        break;
                    }
            }
            oSurveyGen.Dispose();
        }
    }

    protected void OptionChanged(object sender, IntelliQuestionArgs e)
    {
        foreach (GridViewRow row in gvQuestions.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                var dataKey = gvQuestions.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    var parentId = Convert.ToDecimal(dataKey["ParentId"]);
                    var isChild = Convert.ToBoolean(dataKey["IsChild"]);
                    if (isChild && parentId == e.FieldId)
                    {
                        row.Visible = false;
                    }
                }
            }
        }
        if (e.SelectedOption == null)
        {
            return;
        }

        if (!e.IsParent)
        {
            return;
        }
        var childFields = IncidentTrackingManager.GetChildFields(e.FieldId.ToString(CultureInfo.InvariantCulture), e.SelectedOption.ToString());
        if (childFields != null && childFields.Any())
        {
            foreach (GridViewRow row in gvQuestions.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow && !row.Visible)
                {
                    var dataKey = gvQuestions.DataKeys[row.RowIndex];
                    if (dataKey != null)
                    {
                        var fieldId = Convert.ToDecimal(dataKey["FieldId"]);
                        if (childFields.Find(c => c.FieldId == fieldId) != null)
                        {
                            row.Visible = true;
                        }
                    }
                }
            }
        }
    }

    protected void ApplyDropdownSkipLogic()
    {
        foreach (GridViewRow row in gvQuestions.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow && row.Visible)
            {
                var dataKey = gvQuestions.DataKeys[row.RowIndex];
                var baby = row.FindControl("QuestionDisplay1") as ASP.ntqr_surveywizard_questiondisplay_ascx;
                var list = (DropDownList)baby.FindControl("ddlQuestionAnswers");
                if (list != null && list.SelectedIndex > 0)
                {
                    var childFields = IncidentTrackingManager.GetChildFields(dataKey.Value.ToString(),
                        list.SelectedValue);
                    if (childFields != null && childFields.Any())
                    {
                        foreach (GridViewRow nestedRow in gvQuestions.Rows)
                        {
                            if (nestedRow.RowType == DataControlRowType.DataRow && !nestedRow.Visible)
                            {
                                var dataKey1 = gvQuestions.DataKeys[nestedRow.RowIndex];
                                if (dataKey1 != null)
                                {
                                    var fieldId = Convert.ToDecimal(dataKey1["FieldId"]);
                                    if (childFields.Find(c => c.FieldId == fieldId) != null)
                                    {
                                        nestedRow.Visible = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}