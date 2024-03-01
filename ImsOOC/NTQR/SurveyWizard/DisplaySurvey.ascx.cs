using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP;
using Sars.Systems.Controls;
using Sars.Systems.Data;

public partial class ntqr_SurveyWizard_DisplaySurvey : IncidentTrackingControl
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
           
            var incidentStatus = CurrentIncident.IncidentStatusId;
            if (incidentStatus <= 1)
            {
                var fields = IncidentTrackingManager.GetEntryIncidentFields(IncidentID);
                if (fields != null)
                {
                    gvQuestions.Bind(fields);
                    //ApplyDropdownSkipLogic();
                }
            }
            else
            {
                var fields = IncidentTrackingManager.GetIncidentFields(IncidentID);
                if (fields != null)
                {
                    gvQuestions.Bind(fields);
                    ApplyDropdownSkipLogic();
                    HideFieldsBaseOnSelectedQuater();
               }
            }
        }
    }


    private void HideFieldsBaseOnSelectedQuater()
    {
        List<ChildField> childFields = null;
        int selectedQuarter = 0;

        foreach (GridViewRow row in gvQuestions.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow && row.Visible)
            {
                var dataKey = gvQuestions.DataKeys[row.RowIndex];
                if (dataKey.Value.ToString().Equals("1685"))
                {
                    var baby = row.FindControl("QuestionDisplay1") as ASP.ntqr_surveywizard_questiondisplay_ascx;
                    var list = (DropDownList)baby.FindControl("ddlQuestionAnswers");
                    if (list != null && list.SelectedIndex > 0)
                    {
                        selectedQuarter = list.SelectedIndex;
                        childFields = IncidentTrackingManager.GetChildFields(dataKey.Value.ToString(),
                         list.SelectedValue);
                        if (childFields != null && childFields.Any())
                        {
                            foreach (GridViewRow nestedRow in gvQuestions.Rows)
                            {
                                if (nestedRow.RowType == DataControlRowType.DataRow)
                                {
                                    var dataKey1 = gvQuestions.DataKeys[nestedRow.RowIndex];
                                    if (dataKey1 != null)
                                    {
                                        var fieldId = Convert.ToDecimal(dataKey1["FieldId"]);

                                        var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
                                        if (User != null)
                                        {
                                            var userRole = IncidentTrackingManager.GetNTQR_UserRoleById(User[0].ID);
                                            if (userRole != null)
                                            {
                                                // Signatures
                                                switch (userRole[0].RoleId.ToString().ToUpper())
                                                {
                                                    //Compiler
                                                    case "1":
                                                        decimal[] KeyResultAndAnchor = { 1700,1701,1702,1703,1704,1705, 1730, 1731, 1732, 1733, 1734, 1735, 1750, 1751, 1752, 1753, 1754, 1755,1770, 1771, 1772, 1773, 1774, 1775 };
                                                        var excludeSignature = KeyResultAndAnchor.Contains(fieldId);
                                                        if (excludeSignature)
                                                        {
                                                            nestedRow.Visible = false;
                                                        }
                                                        break;
                                                    //Key Result Owner
                                                    case "2":
                                                        KeyResultAndAnchor = new decimal[] { 1697, 1698, 1699, 1703, 1704, 1705, 1727, 1728, 1729, 1733, 1734, 1735, 1733, 1734, 1735, 1747, 1749, 1748, 1753, 1754, 1755, 1767, 1768, 1769, 1773, 1774, 1775 };
                                                         excludeSignature = KeyResultAndAnchor.Contains(fieldId);
                                                        if (excludeSignature)
                                                        {
                                                            nestedRow.Visible = false;
                                                        }
                                                        break;
                                                    //Anchor
                                                    case "3":
                                                        KeyResultAndAnchor = new decimal[] { 1697, 1698, 1699, 1700, 1701, 1702, 1727, 1728, 1729, 1730, 1731, 1732, 1747, 1748, 1749, 1750, 1751, 1752, 1767, 1768, 1769, 1770, 1771, 1772 };
                                                        excludeSignature = KeyResultAndAnchor.Contains(fieldId);
                                                        if (excludeSignature)
                                                        {
                                                            nestedRow.Visible = false;
                                                        }
                                                        break;
                                                    default:
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
                if (dataKey.Value.ToString() == "1612")
                {
                    var getParentBabies = row.FindControl("QuestionDisplay1") as ASP.ntqr_surveywizard_questiondisplay_ascx;
                    var Parentlist = (DropDownList)getParentBabies.FindControl("ddlQuestionAnswers");
                    if (Parentlist != null && Parentlist.SelectedIndex > 0)
                    {
                        childFields = IncidentTrackingManager.GetChildFields(dataKey.Value.ToString(),
                           Parentlist.SelectedValue);
                        if (childFields != null && childFields.Any())
                        {
                            foreach (GridViewRow nestedRow in gvQuestions.Rows)
                            {
                                if (nestedRow.RowType == DataControlRowType.DataRow)
                                {
                                    var dataKey1 = gvQuestions.DataKeys[nestedRow.RowIndex];
                                    if (dataKey1 != null)
                                    {
                                        var fieldId = Convert.ToDecimal(dataKey1["FieldId"]);
                                        switch (selectedQuarter)
                                        {
                                            case 1:
                                                decimal[] OtherQuaters = { 1655, 1656, 1657, 1658, 1659, 1660, 1661, 1662, 1663, 1664, 1665, 1666, 1667, 1668, 1669, 1670, 1671, 1672, 1673, 1674, 1675, 1676, 1677, 1678, 1679, 1680, 1681, 1682, 1683, 1684 };
                                                var excludeQuarter = OtherQuaters.Contains(fieldId);
                                                if (excludeQuarter)
                                                {
                                                    nestedRow.Visible = false;
                                                }

                                                break;
                                            case 2:
                                                OtherQuaters = new decimal[] { 1645, 1646, 1647, 1648, 1649, 1650, 1651, 1652, 1653, 1654, 1665, 1666, 1667, 1668, 1669, 1670, 1671, 1672, 1673, 1674, 1675, 1676, 1677, 1678, 1679, 1680, 1681, 1682, 1683, 1684 };
                                                excludeQuarter = OtherQuaters.Contains(fieldId);
                                                if (excludeQuarter)
                                                {
                                                    nestedRow.Visible = false;
                                                }
                                                break;
                                            case 3:
                                                OtherQuaters = new decimal[] { 1645, 1646, 1647, 1648, 1649, 1650, 1651, 1652, 1653, 1654, 1655, 1656, 1657, 1658, 1659, 1660, 1661, 1662, 1663, 1664, 1675, 1676, 1677, 1678, 1679, 1680, 1681, 1682, 1683, 1684 };
                                                excludeQuarter = OtherQuaters.Contains(fieldId);
                                                if (excludeQuarter)
                                                {
                                                    nestedRow.Visible = false;
                                                }
                                                break;
                                            case 4:
                                                OtherQuaters = new decimal[] { 1645, 1646, 1647, 1648, 1649, 1650, 1651, 1652, 1653, 1654, 1655, 1656, 1657, 1658, 1659, 1660, 1661, 1662, 1663, 1664, 1665, 1666, 1667, 1668, 1669, 1670, 1671, 1672, 1673, 1674 };
                                                excludeQuarter = OtherQuaters.Contains(fieldId);
                                                if (excludeQuarter)
                                                {
                                                    nestedRow.Visible = false;
                                                }
                                                break;
                                            default:
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

        //foreach (GridViewRow row in gvQuestions.Rows)
        //{
        //    if (row.RowType == DataControlRowType.DataRow && row.Visible)
        //    {
        //        var dataKey = gvQuestions.DataKeys[row.RowIndex];
        //        if (dataKey.Value.ToString() == "1612")
        //        {
        //            var getParentBabies = row.FindControl("QuestionDisplay1") as ASP.ntqr_surveywizard_questiondisplay_ascx;
        //            var Parentlist = (DropDownList)getParentBabies.FindControl("ddlQuestionAnswers");
        //            if (Parentlist != null && Parentlist.SelectedIndex > 0)
        //            {
        //                childFields = IncidentTrackingManager.GetChildFields(dataKey.Value.ToString(),
        //                   Parentlist.SelectedValue);
        //                if (childFields != null && childFields.Any())
        //                {
        //                    foreach (GridViewRow nestedRow in gvQuestions.Rows)
        //                    {
        //                        if (nestedRow.RowType == DataControlRowType.DataRow)
        //                        {
        //                            var dataKey1 = gvQuestions.DataKeys[nestedRow.RowIndex];
        //                            if (dataKey1 != null)
        //                            {
        //                                var fieldId = Convert.ToDecimal(dataKey1["FieldId"]);
        //                                switch (selectedQuarter)
        //                                {
        //                                    case 1:
        //                                        decimal[] OtherQuaters =  { 1655, 1656, 1657, 1658, 1659, 1660, 1661, 1662, 1663, 1664, 1665, 1666, 1667, 1668, 1669, 1670, 1671, 1672, 1673, 1674, 1675, 1676, 1677, 1678, 1679, 1680, 1681, 1682, 1683, 1684 };
        //                                        var excludeQuarter = OtherQuaters.Contains(fieldId);
        //                                        if (excludeQuarter )
        //                                        {
        //                                            nestedRow.Visible = false;
        //                                        }

        //                                        break;
        //                                    case 2:
        //                                       OtherQuaters = new decimal[]{ 1645, 1646, 1647, 1648, 1649, 1650, 1651, 1652, 1653, 1654, 1665, 1666, 1667, 1668, 1669, 1670, 1671, 1672, 1673, 1674, 1675, 1676, 1677, 1678, 1679, 1680, 1681, 1682, 1683, 1684 };
        //                                         excludeQuarter = OtherQuaters.Contains(fieldId);
        //                                        if (excludeQuarter)
        //                                        {
        //                                            nestedRow.Visible = false;
        //                                        }
        //                                        break;
        //                                    case 3:
        //                                        OtherQuaters = new decimal[] { 1645, 1646, 1647, 1648, 1649, 1650, 1651, 1652, 1653, 1654, 1655, 1656, 1657, 1658, 1659, 1660, 1661, 1662, 1663, 1664, 1675, 1676, 1677, 1678, 1679, 1680, 1681, 1682, 1683, 1684 };
        //                                        excludeQuarter = OtherQuaters.Contains(fieldId);
        //                                        if (excludeQuarter)
        //                                        {
        //                                            nestedRow.Visible = false;
        //                                        }
        //                                        break;
        //                                    case 4:
        //                                        OtherQuaters = new decimal[] { 1645, 1646, 1647, 1648, 1649, 1650, 1651, 1652, 1653, 1654, 1655, 1656, 1657, 1658, 1659, 1660, 1661, 1662, 1663, 1664, 1665, 1666, 1667, 1668, 1669, 1670, 1671, 1672, 1673, 1674 };
        //                                        excludeQuarter = OtherQuaters.Contains(fieldId);
        //                                        if (excludeQuarter)
        //                                        {
        //                                            nestedRow.Visible = false;
        //                                        }
        //                                        break;
        //                                    default:
        //                                        break;
        //                                }                                     
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}          
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
                        control.BorderStyle = !addColor ? BorderStyle.NotSet : BorderStyle.Solid;
                        control.BorderColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                       
                        control.BorderWidth = 2;
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
                        control.BorderStyle = !addColor ? BorderStyle.NotSet : BorderStyle.Solid;
                        control.BorderColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        
                        control.BorderWidth = 2;
                    }
                }
                else
                {
                    var control = c as DropDownList;
                    if (control != null)
                    {
                        //control.BackColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        control.BorderStyle = !addColor ? BorderStyle.NotSet : BorderStyle.Solid;
                        control.BorderColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                    
                        control.BorderWidth = 2;
                    }
                }
                break;
            case 5:
                {
                    var control = c as CheckBoxList;
                    if (control != null)
                    {
                        //control.CssClass = !addColor ? string.Empty : "requireddata";
                        control.BorderStyle = !addColor ? BorderStyle.NotSet : BorderStyle.Solid;
                        control.BorderColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                       
                        control.BorderWidth = 2;
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
                        control.BorderStyle = !addColor ? BorderStyle.NotSet : BorderStyle.Solid;
                        control.BorderColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        
                        control.BorderWidth = 2;
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
                        control.BorderStyle = !addColor ? BorderStyle.NotSet : BorderStyle.Solid;
                        control.BorderColor = !addColor ? System.Drawing.Color.Empty : System.Drawing.Color.Red;
                        control.BorderWidth = 2;
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
        StringBuilder sb = new StringBuilder();
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
                                if (questionDisplay.ThisControl.Visible)
                                {
                                    //sb.Append(string.Format("The {0} is a required field. </br>",
                                       // questionDisplay.ThisControl.ID));
                                    ChangeColor(questionDisplay.ThisControl, questionDisplay.FieldType,
                                        questionDisplay.ScaleTypeId, true);
                                    numInvalidRequiredAanswers++;
                                }
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

            MessageBox.Show("Please answer all questions.");
            return false;
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
                        var selectedAnswers = (List<int>) answer;
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
                        var selectedAnswers = (MatrixValues) answer;
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
                        var options = (List<object>) answer;
                        oSurveyGen.CleanMultiAnswersWithOther(questionId, incidentId);
                        foreach (var option in options)
                        {
                            if (option is ListItem)
                            {
                                var item = (ListItem) option;
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
        if (e.FieldId.ToString().Equals("1685"))
        {
            foreach (GridViewRow row in gvQuestions.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow && row.Visible)
                {
                    var dataKey = gvQuestions.DataKeys[row.RowIndex];
                    if (dataKey.Value.ToString().Equals("1612"))
                    {
                        var baby = row.FindControl("QuestionDisplay1") as ASP.ntqr_surveywizard_questiondisplay_ascx;
                        var list = (DropDownList)baby.FindControl("ddlQuestionAnswers");
                        if (list != null && list.SelectedIndex > 0)
                        {
                            list.SelectedIndex = -1;
                            break;
                        }
                    }
                }
            }
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
        HideFieldsBaseOnSelectedQuater();
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
                                        //break;
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