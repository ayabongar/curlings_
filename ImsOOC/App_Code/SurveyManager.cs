using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Text;
using Sars.Systems.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Controls;
using System.Xml;
using System.IO;
using SqlConnection = System.Data.SqlClient.SqlConnection;
using System.Security.Cryptography;
using Sars.Systems.SSRS;
using Sars.Systems.Logger;
using System.Diagnostics;


public static class IncidentTrackingManager
{

    public static long SaveProcess(long processId, string processName, bool isActive, string prefix, string fileSize, bool addCoverPage, bool reAllocateBackToCreator, bool canWorkOnOneCase,string WorkingUrl)
    {
        var oparams = new DBParamCollection
                          {
                              {"@ProcessId", processId},
                              {"@Description", processName},
                              {"@IsActive", isActive},
                              {"@CreatedBySID", SarsUser.SID},
                              {"@LastModifiedBy", SarsUser.SID},
                              {"@MaxFileSize", fileSize},
                              {"@Prefix", prefix},
                              {"@AddCoverPage",addCoverPage},
                              {"@ReAssignToCreater",reAllocateBackToCreator},
                               {"@CanWorkOnOneCase",canWorkOnOneCase},
                                {"@WorkingUrl",WorkingUrl},
                              {"@Return_Value", null, ParameterDirection.ReturnValue}
                          };
        using (
            var oCommand = new DBCommand("[dbo].[uspUPSERT_Process]", QueryType.StoredProcedure, oparams,
                                         db.Connection))
        {
            Hashtable oHashTable;
            var scopeIdentity = 0L;
            oCommand.Execute(out oHashTable);

            if (oHashTable.Count > 0)
            {
                scopeIdentity = long.Parse(oHashTable["@Return_Value"].ToString());
            }
            return scopeIdentity;
        }
    }

    public static List<FieldType> GetFieldTypes()
    {
        return new FieldType("[dbo].[uspGetFieldTypes]", null).GetRecords<FieldType>();
    }

    public static List<ScaleTypes> GetScaleTypes()
    {
        return new ScaleTypes("[dbo].[uspREAD_ScaleTypes_All]", null).GetRecords<ScaleTypes>();
    }

    public static List<MatrixDimensions> GetMatrixDimensions()
    {
        return new MatrixDimensions("[dbo].[uspREAD_MatrixDimentions]", null).GetRecords<MatrixDimensions>();
    }

    public static List<TextValidations> GetTextValidations()
    {
        return new TextValidations("uspREAD_TextValidations", null).GetRecords<TextValidations>();
    }

    public static List<ProcessField> GetProcessField(string processId)
    {
        return
            new ProcessField("[dbo].[uspRead_ProcessFields]",
                             new Dictionary<string, object> { { "@ProcessId", processId } })
                .GetRecords<ProcessField>();
    }

    public static List<ProcessField> GetIncidentFields(string incidentId)
    {
        var fields = new List<ProcessField>();
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidentId}
                          };
        using (
            var command = new DBCommand("[dbo].[uspGetIncidentFields]", QueryType.StoredProcedure, oParams,
                                        db.Connection))
        {
            var data = command.ExecuteScalar();
            if (data != null && data != DBNull.Value)
            {
                var ds = new RecordSet();
                var reader = new StringReader(data.ToString());
                ds.ReadXml(reader);

                if (ds.HasRows)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //if(Convert.ToInt32(row["ShowOnScreen"]) == 0)
                        //{
                        //    continue;
                        //}
                        var field = new ProcessField
                        {
                            FieldId = Convert.ToDecimal(row["ProcessFieldId"]),
                            FieldName = row["FieldName"].ToString(),
                            Display = row["Display"].ToString(),
                            ProcessId = Convert.ToInt64(row["ProcessId"]),
                            FieldTypeId = Convert.ToInt32(row["FieldTypeId"]),
                            SortOrder = Convert.ToInt32(row["SortOrder"])
                        };

                        if (row["IsActive"] != null && row["IsActive"] != DBNull.Value)
                        {
                            field.IsActive = row["IsActive"].ToString() == "1";
                        }
                        field.Timestamp = Convert.ToDateTime(row["Timestamp"]);
                        field.IsRequired = row["IsRequired"].ToString() == "1";

                        if (row["ScaleTypeId"] != null && row["ScaleTypeId"] != DBNull.Value)
                        {
                            field.ScaleTypeId = Convert.ToInt32(row["ScaleTypeId"]);
                        }
                        if (row["MatrixDimensionId"] != null && row["MatrixDimensionId"] != DBNull.Value)
                        {
                            field.MatrixDimensionId = Convert.ToInt32(row["MatrixDimensionId"]);
                        }
                        if (row["ValidationTypeId"] != null && row["ValidationTypeId"] != DBNull.Value)
                        {
                            field.ValidationTypeId = Convert.ToInt32(row["ValidationTypeId"]);
                        }

                        if (row["LookupDataId"] != null && row["LookupDataId"] != DBNull.Value)
                        {
                            field.LookupDataId = Convert.ToDecimal(row["LookupDataId"]);
                        }
                        if (row["HierarchyLookupId"] != null && row["HierarchyLookupId"] != DBNull.Value)
                        {
                            field.HierarchyLookupId = Convert.ToDecimal(row["HierarchyLookupId"]);
                        }
                        if (row["ShowOnSearch"] != null && row["ShowOnSearch"] != DBNull.Value)
                        {
                            field.ShowOnSearch = row["ShowOnSearch"].ToString() == "1";
                        }
                        if (row["ShowOnScreen"] != null && row["ShowOnScreen"] != DBNull.Value)
                        {
                            field.ShowOnScreen = row["ShowOnScreen"].ToString() == "1";
                        }
                        if (row["ShowOnReport"] != null && row["ShowOnReport"] != DBNull.Value)
                        {
                            field.ShowOnReport = row["ShowOnReport"].ToString() == "1";
                        }

                        field.AddedBySID = Convert.ToString(row["AddedBySID"]);
                        field.LastModifiedBySID = Convert.ToString(row["LastModifiedBySID"]);

                        if (row["LastModifiedDate"] != null && row["LastModifiedDate"] != DBNull.Value)
                        {
                            field.LastModifiedDate = Convert.ToDateTime(row["LastModifiedDate"]);
                        }
                        if (row["IsParent"] != null && row["IsParent"] != DBNull.Value)
                        {
                            field.IsParent = row["IsParent"].ToString() == "1";
                        }
                        if (row["IsChild"] != null && row["IsChild"] != DBNull.Value)
                        {
                            field.IsChild = row["IsChild"].ToString() == "1";
                        }
                        if (row["ParentId"] != null && row["ParentId"] != DBNull.Value)
                        {
                            field.ParentId = Convert.ToDecimal(row["ParentId"]);
                        }
                        //field.FieldType = Convert.ToString(row["FieldType"]);

                        fields.Add(field);
                    }
                    return fields;
                }
            }
        }
        return null;
    }

    public static List<ProcessField> GetEntryIncidentFields(string incidentId)
    {
        var fields = new List<ProcessField>();
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidentId}
                          };
        using (
            var command = new DBCommand("[dbo].[uspGetIncidentFields]", QueryType.StoredProcedure, oParams,
                                        db.Connection))
        {
            var data = command.ExecuteScalar();
            if (data != null && data != DBNull.Value)
            {
                var ds = new RecordSet();
                var reader = new StringReader(data.ToString());
                ds.ReadXml(reader);

                if (ds.HasRows)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //if(Convert.ToInt32(row["ShowOnScreen"]) == 0)
                        //{
                        //    continue;
                        //}
                        var field = new ProcessField
                        {
                            FieldId = Convert.ToDecimal(row["ProcessFieldId"]),
                            FieldName = row["FieldName"].ToString(),
                            Display = row["Display"].ToString(),
                            ProcessId = Convert.ToInt64(row["ProcessId"]),
                            FieldTypeId = Convert.ToInt32(row["FieldTypeId"]),
                            SortOrder = Convert.ToInt32(row["SortOrder"])
                        };

                        if (row["IsActive"] != null && row["IsActive"] != DBNull.Value)
                        {
                            field.IsActive = row["IsActive"].ToString() == "1";
                        }
                        field.Timestamp = Convert.ToDateTime(row["Timestamp"]);
                        field.IsRequired = row["IsRequired"].ToString() == "1";

                        if (row["ScaleTypeId"] != null && row["ScaleTypeId"] != DBNull.Value)
                        {
                            field.ScaleTypeId = Convert.ToInt32(row["ScaleTypeId"]);
                        }
                        if (row["MatrixDimensionId"] != null && row["MatrixDimensionId"] != DBNull.Value)
                        {
                            field.MatrixDimensionId = Convert.ToInt32(row["MatrixDimensionId"]);
                        }
                        if (row["ValidationTypeId"] != null && row["ValidationTypeId"] != DBNull.Value)
                        {
                            field.ValidationTypeId = Convert.ToInt32(row["ValidationTypeId"]);
                        }

                        if (row["LookupDataId"] != null && row["LookupDataId"] != DBNull.Value)
                        {
                            field.LookupDataId = Convert.ToDecimal(row["LookupDataId"]);
                        }
                        if (row["HierarchyLookupId"] != null && row["HierarchyLookupId"] != DBNull.Value)
                        {
                            field.HierarchyLookupId = Convert.ToDecimal(row["HierarchyLookupId"]);
                        }
                        if (row["ShowOnSearch"] != null && row["ShowOnSearch"] != DBNull.Value)
                        {
                            field.ShowOnSearch = row["ShowOnSearch"].ToString() == "1";
                        }
                        if (row["ShowOnScreen"] != null && row["ShowOnScreen"] != DBNull.Value)
                        {
                            field.ShowOnScreen = row["ShowOnScreen"].ToString() == "1";
                        }
                        if (row["ShowOnReport"] != null && row["ShowOnReport"] != DBNull.Value)
                        {
                            field.ShowOnReport = row["ShowOnReport"].ToString() == "1";
                        }

                        field.AddedBySID = Convert.ToString(row["AddedBySID"]);
                        field.LastModifiedBySID = Convert.ToString(row["LastModifiedBySID"]);

                        if (row["LastModifiedDate"] != null && row["LastModifiedDate"] != DBNull.Value)
                        {
                            field.LastModifiedDate = Convert.ToDateTime(row["LastModifiedDate"]);
                        }
                        if (row["IsParent"] != null && row["IsParent"] != DBNull.Value)
                        {
                            field.IsParent = row["IsParent"].ToString() == "1";
                        }
                        if (row["IsChild"] != null && row["IsChild"] != DBNull.Value)
                        {
                            field.IsChild = row["IsChild"].ToString() == "1";
                        }
                        if (row["ParentId"] != null && row["ParentId"] != DBNull.Value)
                        {
                            field.ParentId = Convert.ToDecimal(row["ParentId"]);
                        }
                        //field.FieldType = Convert.ToString(row["FieldType"]);
                        try
                        {
                            if (row["ShowOnAssigned"] != null && row["ShowOnAssigned"] != DBNull.Value)
                            {
                                field.ShowOnAssigned = row["ShowOnAssigned"].ToString().Equals("1") ? true : false;
                            }
                        }
                        catch (Exception)
                        {

                        }

                        if (!field.ShowOnAssigned)
                        {
                            fields.Add(field);
                        }
                    }
                    return fields;
                }
            }
        }
        return null;
    }

    public static List<ProcessField> GetCoverPageIncidentFields(string incidentId)
    {
        var fields = new List<ProcessField>();
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidentId}
                          };
        using (
            var command = new DBCommand("[dbo].[uspGetIncidentFields]", QueryType.StoredProcedure, oParams,
                                        db.Connection))
        {
            var data = command.ExecuteScalar();
            if (data != null && data != DBNull.Value)
            {
                var ds = new RecordSet();
                var reader = new StringReader(data.ToString());
                ds.ReadXml(reader);

                if (ds.HasRows)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //if(Convert.ToInt32(row["ShowOnScreen"]) == 0)
                        //{
                        //    continue;
                        //}
                        var field = new ProcessField
                        {
                            FieldId = Convert.ToDecimal(row["ProcessFieldId"]),
                            FieldName = row["FieldName"].ToString(),
                            Display = row["Display"].ToString(),
                            ProcessId = Convert.ToInt64(row["ProcessId"]),
                            FieldTypeId = Convert.ToInt32(row["FieldTypeId"]),
                            SortOrder = Convert.ToInt32(row["SortOrder"])
                        };


                        field.Timestamp = Convert.ToDateTime(row["Timestamp"]);
                        field.IsRequired = row["IsRequired"].ToString() == "1";

                        if (row["ScaleTypeId"] != null && row["ScaleTypeId"] != DBNull.Value)
                        {
                            field.ScaleTypeId = Convert.ToInt32(row["ScaleTypeId"]);
                        }
                        if (row["MatrixDimensionId"] != null && row["MatrixDimensionId"] != DBNull.Value)
                        {
                            field.MatrixDimensionId = Convert.ToInt32(row["MatrixDimensionId"]);
                        }
                        if (row["ValidationTypeId"] != null && row["ValidationTypeId"] != DBNull.Value)
                        {
                            field.ValidationTypeId = Convert.ToInt32(row["ValidationTypeId"]);
                        }

                        if (row["LookupDataId"] != null && row["LookupDataId"] != DBNull.Value)
                        {
                            field.LookupDataId = Convert.ToDecimal(row["LookupDataId"]);
                        }
                        if (row["HierarchyLookupId"] != null && row["HierarchyLookupId"] != DBNull.Value)
                        {
                            field.HierarchyLookupId = Convert.ToDecimal(row["HierarchyLookupId"]);
                        }
                        if (row["ShowOnSearch"] != null && row["ShowOnSearch"] != DBNull.Value)
                        {
                            field.ShowOnSearch = row["ShowOnSearch"].ToString() == "1";
                        }
                        if (row["ShowOnScreen"] != null && row["ShowOnScreen"] != DBNull.Value)
                        {
                            field.ShowOnScreen = row["ShowOnScreen"].ToString() == "1";
                        }
                        if (row["ShowOnReport"] != null && row["ShowOnReport"] != DBNull.Value)
                        {
                            field.ShowOnReport = row["ShowOnReport"].ToString() == "1";
                        }


                        field.AddedBySID = Convert.ToString(row["AddedBySID"]);
                        field.LastModifiedBySID = Convert.ToString(row["LastModifiedBySID"]);

                        if (row["LastModifiedDate"] != null && row["LastModifiedDate"] != DBNull.Value)
                        {
                            field.LastModifiedDate = Convert.ToDateTime(row["LastModifiedDate"]);
                        }
                        if (row["IsParent"] != null && row["IsParent"] != DBNull.Value)
                        {
                            field.IsParent = row["IsParent"].ToString() == "1";
                        }
                        if (row["IsChild"] != null && row["IsChild"] != DBNull.Value)
                        {
                            field.IsChild = row["IsChild"].ToString() == "1";
                        }
                        if (row["ParentId"] != null && row["ParentId"] != DBNull.Value)
                        {
                            field.ParentId = Convert.ToDecimal(row["ParentId"]);
                        }
                        //field.FieldType = Convert.ToString(row["FieldType"]);
                        if ((row["IsActive"] != null && row["IsActive"] != DBNull.Value) && (row["AddToCoverPage"] != null && row["AddToCoverPage"] != DBNull.Value))
                        {
                            if ((Convert.ToInt32(row["IsActive"]) == 1) && (Convert.ToInt32(row["AddToCoverPage"]) == 1))
                            {
                                //  field.AddToCoverPage = row["AddToCoverPage"].ToString() == "1";
                                fields.Add(field);
                            }
                        }

                    }
                    return fields;
                }
            }
        }
        return null;
    }

    public static DataTable GetDivisions()
    {
        var oRecords = new RecordSet("uspREAD_Divisions_All", QueryType.StoredProcedure, null, db.ConnectionString);
        return oRecords.Tables[0];
    }

    public static DataTable GetDepartments()
    {
        var oRecords = new RecordSet("uspREAD_Departments_All", QueryType.StoredProcedure, null, db.ConnectionString);
        return oRecords.Tables[0];
    }

    public static DataTable GetDivisionDepartments(string divisionCode)
    {
        var oParams = new DBParamCollection
                          {
                              {"@DivisionCode", divisionCode}
                          };
        var oRecords = new RecordSet("uspREAD_Departments_ForDivision", QueryType.StoredProcedure, oParams,
                                     db.ConnectionString);
        return oRecords.Tables[0];
    }

    public static DataTable GetDepartmentsByDivisionId(string divisionId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@DivisionId", divisionId}
                          };
        var oRecords = new RecordSet("[dbo].[uspREAD_Departments_ForDivision_ById]", QueryType.StoredProcedure, oParams,
                                     db.ConnectionString);
        return oRecords.Tables[0];
    }

    public static RecordSet GetQuestionTypeAnswers(int fieldTypeId, int scaleTypeId)
    {
        var oParams = new DBParamCollection()
                          {
                              {"@FieldTypeId", fieldTypeId},
                              {"@ScaleTypeId", scaleTypeId}
                          };
        var recordSet = new RecordSet("uspREAD_QuestionTypeAnswers", QueryType.StoredProcedure, oParams,
                                      db.ConnectionString);
        return recordSet;
    }

    public static RecordSet GetQuestionTypeAnswers(int questionTypeId, decimal questionId, int scaleTypeId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@FieldTypeId", questionTypeId},
                              {"@FieldId", questionId},
                              {"@ScaleTypeId", scaleTypeId}
                          };
        var recordSet = new RecordSet("uspREAD_QuestionTypeAnswers", QueryType.StoredProcedure, oParams,
                                      db.ConnectionString);
        return recordSet;
    }

    public static RecordSet GetMatrixDimentions(int dimensionId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@MatrixDimensionId", dimensionId},
                          };
        var recordSet = new RecordSet("uspREAD_MatrixOptions_ByDimensionId", QueryType.StoredProcedure, oParams,
                                      db.ConnectionString);
        return recordSet;
    }

    public static void SetSelectedQuestionAnswer(Control control, decimal questionId, int questionType, int scaleTypeId,
                                                 int response, out decimal selectedAns)
    {
        selectedAns = 0M;
        var oParams = new DBParamCollection
                          {
                              {"@FieldId", questionId},
                              {"@FieldTypeId", questionType},
                              {"@IncidentId", response}
                          };
        using (
            var oCommand = new DBCommand("uspREAD_QuestionResponse", QueryType.StoredProcedure, oParams, db.Connection))
        {
            var answers = oCommand.ExecuteReader();
          
                if (answers.HasRows)
                {
                    switch (questionType)
                    {
                        case 1:
                            {
                                if (scaleTypeId != 5)
                                {
                                    var rbtnList = control as RadioButtonList;
                                    if (rbtnList != null)
                                    {
                                        answers.Read();
                                        var selectedValue = answers["Answer"];
                                        if (selectedValue != DBNull.Value)
                                        {
                                            rbtnList.SelectItemByValue(selectedValue.ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    var ddl = control as DropDownList;
                                    if (ddl != null)
                                    {
                                        answers.Read();
                                        var selectedValue = answers["Answer"];
                                        if (selectedValue != DBNull.Value)
                                        {
                                            ddl.SelectItemByValue(selectedValue.ToString());
                                        }
                                    }
                                }
                                break;
                            }
                        case 5:
                            {
                                var chkList = control as CheckBoxList;
                                if (chkList != null)
                                {
                                    while (answers.Read())
                                    {
                                        var selectedValue = answers["Answer"];
                                        if (selectedValue != DBNull.Value)
                                        {
                                            foreach (ListItem itm in chkList.Items)
                                            {
                                                if (itm.Value == selectedValue.ToString())
                                                {
                                                    itm.Selected = true;
                                                    if (selectedAns == 0M)
                                                    {
                                                        selectedAns = Convert.ToDecimal(itm.Value);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case 6:
                        case 10:
                        case 11:
                            {
                                var txt = control as TextBox;
                                if (txt != null)
                                {
                                    answers.Read();
                                    var selectedValue = answers["Answer"];
                                    if (selectedValue != DBNull.Value)
                                    {
                                        txt.Text = selectedValue.ToString();
                                    }
                                }
                                break;
                            }
                        case 13:
                            {
                                var ddl = control as DropDownList;
                                if (ddl != null)
                                {
                                    while (answers.Read())
                                    {
                                        var selectedValue = answers["Answer"];
                                        if (selectedValue != DBNull.Value)
                                        {
                                            ddl.SelectItemByValue(selectedValue.ToString());
                                        }
                                    }
                                }
                                break;
                            }
                        case 16:
                            {
                                if (control is RadioButtonList)
                                {
                                    var rbtnList = control as RadioButtonList;

                                    answers.Read();
                                    var selectedValue = answers["Answer"];
                                    if (selectedValue != DBNull.Value)
                                    {
                                        rbtnList.SelectItemByValue(selectedValue.ToString());
                                        selectedAns = Convert.ToDecimal(selectedValue);
                                    }
                                }

                                else if (control is TextBox)
                                {
                                    var txt = control as TextBox;
                                    answers.Read();
                                    var selectedValue = answers["Answer"];
                                    if (selectedValue != DBNull.Value)
                                    {
                                        txt.Text = selectedValue.ToString();
                                    }
                                }
                                break;
                            }
                        case 17:
                            {
                                var ddl = control as DropDownList;
                                if (ddl != null)
                                {
                                    answers.Read();
                                    var selectedValue = answers["Answer"];
                                    if (selectedValue != DBNull.Value)
                                    {
                                        ddl.SelectItemByValue(selectedValue.ToString());
                                        selectedAns = Convert.ToDecimal(selectedValue);
                                    }
                                }
                                break;
                            }
                        case 18:
                            {
                                var checkBoxOther = control as CheckBoxListWithOther;
                                if (checkBoxOther != null)
                                {
                                    while (answers.Read())
                                    {
                                        var selectedValue = answers["Answer"];

                                        if (selectedValue != DBNull.Value)
                                        {
                                            int result;
                                            if (int.TryParse(selectedValue.ToString(), out result))
                                            {
                                                foreach (ListItem itm in checkBoxOther.Items)
                                                {
                                                    if (itm.Value == selectedValue.ToString())
                                                    {
                                                        itm.Selected = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                checkBoxOther.OtherText = selectedValue.ToString();
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case 19:
                            {
                                var radioButtonOther = control as RadioButtonListWithOther;
                                if (radioButtonOther != null)
                                {
                                    while (answers.Read())
                                    {
                                        var selectedValue = answers["Answer"];

                                        if (selectedValue != DBNull.Value)
                                        {
                                            int result;
                                            if (int.TryParse(selectedValue.ToString(), out result))
                                            {
                                                foreach (ListItem itm in radioButtonOther.Items)
                                                {
                                                    if (itm.Value == selectedValue.ToString())
                                                    {
                                                        itm.Selected = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                radioButtonOther.OtherText = selectedValue.ToString();
                                                var other = radioButtonOther.Items.FindByText("Other");
                                                if (other != null)
                                                {
                                                    other.Selected = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            }

                    }
                }
               // answers.Close();
               // answers.Dispose();
           
        }

        #region Commented out dont remove

        //var answers = new RecordSet("uspREAD_QuestionResponse", QueryType.StoredProcedure, oParams, db.ConnectionString);
        //{
        //    if (answers.Tables.Count > 0)
        //    {
        //        if (answers.Tables[0].Rows.Count > 0)
        //        {
        //            switch (questionType)
        //            {
        //                case 1:
        //                    {
        //                        if (scaleTypeId != 5)
        //                        {
        //                            var rbtnList = control as RadioButtonList;
        //                            if (rbtnList != null)
        //                            {
        //                                var selectedValue = answers[0]["Answer"];
        //                                if (selectedValue != DBNull.Value)
        //                                {
        //                                    rbtnList.SelectItemByValue(selectedValue.ToString());
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            var ddl = control as DropDownList;
        //                            if (ddl != null)
        //                            {
        //                                foreach (DataRow dr in answers.Tables[0].Rows)
        //                                {
        //                                    var selectedValue = dr["Answer"];
        //                                    if (selectedValue != DBNull.Value)
        //                                    {
        //                                        ddl.SelectItemByValue(selectedValue.ToString());
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        break;
        //                    }
        //                case 5:
        //                    {
        //                        var chkList = control as CheckBoxList;
        //                        if (chkList != null)
        //                        {
        //                            foreach (DataRow dr in answers.Tables[0].Rows)
        //                            {
        //                                var selectedValue = dr["Answer"];
        //                                if (selectedValue != DBNull.Value)
        //                                {
        //                                    foreach (ListItem itm in chkList.Items)
        //                                    {
        //                                        if (itm.Value == selectedValue.ToString())
        //                                        {
        //                                            itm.Selected = true;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        break;
        //                    }
        //                case 6:
        //                case 10:
        //                case 11:
        //                    {
        //                        var txt = control as TextBox;
        //                        if (txt != null)
        //                        {
        //                            var selectedValue = answers[0]["Answer"];
        //                            if (selectedValue != DBNull.Value)
        //                            {
        //                                txt.Text = selectedValue.ToString();
        //                            }
        //                        }

        //                        break;
        //                    }
        //                case 13:
        //                    {
        //                        var ddl = control as DropDownList;
        //                        if (ddl != null)
        //                        {
        //                            foreach (DataRow dr in answers.Tables[0].Rows)
        //                            {
        //                                var selectedValue = dr["Answer"];
        //                                if (selectedValue != DBNull.Value)
        //                                {
        //                                    ddl.SelectItemByValue(selectedValue.ToString());
        //                                }
        //                            }
        //                        }
        //                        break;
        //                    }
        //            }
        //        }
        //    }
        //}

        #endregion
    }

    public static int SaveScaleTypes(int scaleTypeId, string description, bool isActive)
    {
        var oParams = new DBParamCollection
                          {
                              {"@ScaleTypeId", scaleTypeId},
                              {"@Description", description},
                              {"@IsActive", isActive},
                              {"@Return_Value", null, ParameterDirection.ReturnValue}
                          };
        Hashtable oHashTable;
        int scopeIdentity;
        using (
            var oCommand = new DBCommand("uspUPSERT_ScaleTypes", QueryType.StoredProcedure, oParams,
                                         db.TransactionConnection))
        {
            scopeIdentity = 0;
            oCommand.Execute(out oHashTable);
            oCommand.Commit();
        }
        if (oHashTable.Count > 0)
        {
            scopeIdentity = int.Parse(oHashTable["@Return_Value"].ToString());
        }
        return scopeIdentity;
    }

    public static int SaveScaleOptions(int scaleTypeId, string scaleTypeDescription, bool isActive)
    {
        var oParams = new DBParamCollection
                          {
                              {"@QuestionTypeAnswerId", 0},
                              {"@ScaleTypeId", scaleTypeId},
                              {"@Description", scaleTypeDescription},
                              {"@IsActive", isActive},
                              {"@Return_Value", null, ParameterDirection.ReturnValue}
                          };
        using (
            var oCommand = new DBCommand("uspUPSERT_QuestionTypeAnswers", QueryType.StoredProcedure, oParams,
                                         db.TransactionConnection))
        {
            Hashtable oHashTable;
            oCommand.Execute(out oHashTable);
            oCommand.Commit();
            if (oHashTable.Count > 0)
            {
                return int.Parse(oHashTable["@Return_Value"].ToString());
            }
            return 0;
        }
    }

    public static int SaveMatrixDimentions(int matrixDimensionId, string dimensions, string leftHeader,
                                           string righHeader, bool isActive)
    {
        var oParams = new DBParamCollection
                          {
                              {"@MatrixDimensionId", matrixDimensionId},
                              {"@LeftHeader", leftHeader},
                              {"@RighHeader", righHeader},
                              {"@Dimensions", dimensions},
                              {"@IsActive", isActive},
                              {"@Return_Value", null, ParameterDirection.ReturnValue}
                          };
        Hashtable oHashTable;
        int scopeIdentity;
        using (
            var oCommand = new DBCommand("uspUPSERT_MatrixDimensions", QueryType.StoredProcedure, oParams,
                                         db.TransactionConnection))
        {
            scopeIdentity = 0;
            oCommand.Execute(out oHashTable);
            oCommand.Commit();
        }
        if (oHashTable.Count > 0)
        {
            scopeIdentity = int.Parse(oHashTable["@Return_Value"].ToString());
        }
        return scopeIdentity;
    }

    public static int SaveMatrixOptions(int matrixDimensionId, string leftDimension, string rightDimension,
                                        bool isActive)
    {
        var oParams = new DBParamCollection
                          {
                              {"@MatrixOptionId", 0},
                              {"@LeftDimension", leftDimension},
                              {"@RightDimension", rightDimension},
                              {"@IsActive", isActive},
                              {"@MatrixDimensionId", matrixDimensionId},
                              {"@Return_Value", null, ParameterDirection.ReturnValue}
                          };
        Hashtable oHashTable;
        int scopeIdentity;
        using (
            var oCommand = new DBCommand("uspUPSERT_MatrixOptions", QueryType.StoredProcedure, oParams,
                                         db.TransactionConnection))
        {
            scopeIdentity = 0;
            oCommand.Execute(out oHashTable);
            oCommand.Commit();
        }
        if (oHashTable.Count > 0)
        {
            scopeIdentity = int.Parse(oHashTable["@Return_Value"].ToString());
        }
        return scopeIdentity;
    }

    public static List<MultichoiceOption> ReadQuestionOptions(string fieldId)
    {
        return
            new MultichoiceOption("[dbo].[uspREAD_MultichoiceOptions_ByQuestionId]",
                                  new Dictionary<string, object> { { "@FieldId", fieldId } }).GetRecords
                <MultichoiceOption>();

    }


    public static void RemoveListItem(decimal listItemId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@ListItemId", listItemId}
                          };
        using (var oCommand = new DBCommand("usp_DELETE__ListItme", QueryType.StoredProcedure, oParams, db.Connection))
        {
            oCommand.Execute();
        }
    }

    public static RecordSet ReadQuestionListItems(decimal fieldId, string incidentId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@FieldId", fieldId},
                              {"@IncidentId", incidentId}
                          };
        using (
            var lists = new RecordSet("uspREAD_ListItmes_ByQuestion", QueryType.StoredProcedure, oParams,
                                      db.ConnectionString))
        {
            return lists;
        }
    }

    public static void SaveListItem(string description, string processId, decimal fieldId, string incidentId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@Description", description},
                              {"@ProcessId", processId},
                              {"@FieldId", fieldId},
                              {"@IncidentId", incidentId}
                          };
        using (var oCommand = new DBCommand("uspINSERT_ListItmes", QueryType.StoredProcedure, oParams, db.Connection))
        {
            oCommand.Execute();
        }
    }

    public static Stream CreateXml(int questionId)
    {
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.OmitXmlDeclaration = true;
        settings.CloseOutput = false;
        settings.Indent = true;
        var xml = string.Empty;

        var stream = new MemoryStream();

        XmlWriter xmlWriter = XmlWriter.Create(stream, settings);

        var oParams = new DBParamCollection
                          {
                              {"@QuestionId", questionId}
                          };
        var rs =
            new RecordSet(
                "EXEC uspREAD_TextGridRowHeadings_ByQuestionId @QuestionId  EXEC uspREAD_TextGridColumnHeadings_ByQuestionId @QuestionId ",
                QueryType.TransectSQL, oParams, db.ConnectionString);
        if (rs.Tables.Count > 0)
        {
            var rowsTable = rs.Tables[0];
            var colsTable = rs.Tables[1];

            if (rowsTable.Rows.Count > 0)
            {
                // xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Root");
                xmlWriter.WriteStartElement("Items");

                foreach (DataRow dr in rowsTable.Rows)
                {
                    xmlWriter.WriteStartElement(string.Format("Row{0}", dr["HeadingId"]));
                    xmlWriter.WriteAttributeString("Description", dr["Description"].ToString());
                    xmlWriter.WriteAttributeString("QuestionId", dr["QuestionId"].ToString());

                    foreach (DataRow cRows in colsTable.Rows)
                    {
                        //xmlWriter.WriteElementString(string.Format("Column{0}{1}", dr["HeadingId"], cRows["HeadingId"]), " ");
                        xmlWriter.WriteElementString(cRows["Description"].ToString().Replace(" ", "_"), " ");
                    }
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                //xmlWriter.WriteEndDocument();                
                xmlWriter.Flush();
                xmlWriter.Close();
            }
        }
        if (stream.Length > 0)
        {
            stream.Seek(0, SeekOrigin.Begin);

            var length = Convert.ToInt32(stream.Length);
            var xmlByte = new byte[length];
            stream.Read(xmlByte, 0, length);
            xml = Encoding.UTF8.GetString(xmlByte);
            xml = xml.Trim();
            stream.Close();
        }
        //return xml;
        return stream;
    }

    public static RecordSet DownLoadCompletedReport(string questionnaireId, string startDate, string endDate)
    {
        var oParams = new DBParamCollection
                          {
                              {"@StartDate", startDate},
                              {"@EndDate", endDate},
                              {"@QuestionnaireId", questionnaireId}
                          };
        using (
            var data = new RecordSet("[dbo].[usp_rpt_Completed_Survey_Results]", QueryType.StoredProcedure, oParams,
                                     db.ConnectionString))
        {
            if (data.HasRows)
            {
                return data;
            }
            return null;
        }
    }

    public static RecordSet GetReport(string questionnaireId, string procedure)
    {
        var oParams = new DBParamCollection
                          {
                              {"@QuestionnaireId", questionnaireId}
                          };
        using (var data = new RecordSet(procedure, QueryType.StoredProcedure, oParams, db.ConnectionString))
        {
            if (data.HasRows)
            {
                return data;
            }
            return null;
        }
    }

    public static void SaveFirstGroup(string code, string firstGroup, string respId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@FirstGroup", firstGroup},
                              {"@ResponseId", respId}
                          };
        using (
            var oCommand = new DBCommand("dbo.uspSave_SurveyFirstGroup", QueryType.StoredProcedure, oParams,
                                         db.Connection))
        {
            oCommand.Execute();
        }
    }






    public static string GenerateUserCode(int questionnaireId)
    {
        var userCode = "0";
        var currentDay = DateTime.Now.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
        var currentMonth = DateTime.Now.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
        var currentYear = DateTime.Now.Year;
        var currentDate = string.Format("{0}{1}{2}", currentDay, currentYear, currentMonth);

        var oParams = new DBParamCollection
                          {
                              {"@QuestionnaireId", questionnaireId},
                              {"@CurrentDate", currentDate},
                              {"@UserCode", null, ParameterDirection.Output}
                          };

        using (var oCommand = new DBCommand("dbo.uspGenerateCode", QueryType.StoredProcedure, oParams, db.Connection))
        {
            Hashtable ht;
            oCommand.Execute(out ht);
            if (ht != null)
            {
                if (ht.Count > 0)
                {
                    userCode = ht["@UserCode"].ToString();
                    return userCode;
                }
            }
        }
        return userCode;
    }

    public static long SaveResponse(int questionnaireId)
    {
        var responseId = 0L;
        var oParams = new DBParamCollection
                          {
                              {"@QuestionnaireId", questionnaireId},
                              {"@ResponseId", null, ParameterDirection.ReturnValue}
                          };
        using (
            var oCommand = new DBCommand("dbo.uspINSERT_Responses", QueryType.StoredProcedure, oParams, db.Connection))
        {
            Hashtable ht;
            oCommand.Execute(out ht);
            if (ht.Count > 0)
            {
                responseId = Convert.ToInt64(ht["@ResponseId"].ToString());
            }
            return responseId;
        }
    }

    public static int CountAnsweredSurveyQuestion(int questionnaireId, int responseId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@QuestionnaireId", questionnaireId},
                              {"@ResponseId", responseId},
                          };
        using (
            var oCommand = new DBCommand("[dbo].[uspCountAnsweredQuestions]", QueryType.StoredProcedure, oParams,
                                         db.Connection))
        {
            var numberOfAnsweredQuestion = oCommand.ExecuteScalar();

            return numberOfAnsweredQuestion != null && numberOfAnsweredQuestion != DBNull.Value
                       ? Convert.ToInt32(numberOfAnsweredQuestion)
                       : 0;
        }
    }

    public static int SaveSurveyIntroduction(string introduction, bool isActive, int id)
    {
        var oParams = new DBParamCollection
                          {
                              {"@Id", id},
                              {"@IntroText", introduction},
                              {"@IsActive", isActive}
                          };
        using (var oCommand = new DBCommand("dbo.uspAddIntroduction", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            return oCommand.Execute();
        }
    }

    public static int SaveAuditTrail(string userSID, string auditDetails)
    {
        var oParams = new DBParamCollection
                          {
                              {"@UserSID", userSID},
                              {"@UserAction", auditDetails}
                          };
        using (
            var oCommand = new DBCommand("[dbo].[uspINSERT_AuditTrail]", QueryType.StoredProcedure, oParams,
                                         db.Connection))
        {
            return oCommand.Execute();
        }
    }





    public static IncidentProcess GetIncidentProcess(string processId)
    {
        return new IncidentProcess("[dbo].[uspRead_Process_byId]",
                                   new Dictionary<string, object>
                                       {
                                           {"@ProcessId", processId}
                                       }).GetRecord<IncidentProcess>();
    }

    public static Incident GetIncidentById(string incidentId)
    {
        return new Incident("[dbo].[uspGetIncidentByID]",
                            new Dictionary<string, object>
                                {
                                    {"@IncidentId", incidentId}
                                }).GetRecord<Incident>();
    }

    public static List<IncidentProcess> GetIncidentProcesses(string sid)
    {
        return new IncidentProcess("[dbo].[uspRead_AllProcesses]", new Dictionary<string, object> { { "@sid", sid } }).GetRecords<IncidentProcess>();
    }


    public static int InitIncident(string processId, out string incidentId, out string incidentNumber)
    {
        incidentId = string.Empty;
        incidentNumber = string.Empty;

        var oParams = new DBParamCollection
                          {
                              {"@CreatedBySID", SarsUser.SID},
                              {"@ProcessId", processId},
                              {"@IncidentNumber", null, ParameterDirection.Output},
                              {"@IncidentID", null, ParameterDirection.Output}
                          };
        using (
            var command = new DBCommand("uspInitialiseNewIncident", QueryType.StoredProcedure, oParams, db.Connection))
        {
            Hashtable ht;
            var numRecordsAffected = command.Execute(out ht);

            if (ht.ContainsKey("@IncidentNumber"))
            {
                incidentNumber = ht["@IncidentNumber"].ToString();
            }
            if (ht.ContainsKey("@IncidentID"))
            {
                incidentId = ht["@IncidentID"].ToString();
            }
            return numRecordsAffected;
        }
    }
    public static int InitOocIncident(string processId, string type, out string incidentId, out string incidentNumber)
    {

        incidentId = string.Empty;
        incidentNumber = string.Empty;
        var numRecordsAffected = 0;
        var oParams = new DBParamCollection
                          {
                              {"@CreatedBySID", SarsUser.SID},
                              {"@ProcessId", processId},
                              {"@IncidentNumber", null, ParameterDirection.Output},
                              {"@IncidentID", null, ParameterDirection.Output}
                          };
        if (type.Equals("Internal"))
        {
            using (
                var command = new DBCommand("uspOocInitialiseNewIncident", QueryType.StoredProcedure, oParams,
                    db.Connection))
            {
                Hashtable ht;
                numRecordsAffected = command.Execute(out ht);

                if (ht.ContainsKey("@IncidentNumber"))
                {
                    incidentNumber = ht["@IncidentNumber"].ToString();
                }
                if (ht.ContainsKey("@IncidentID"))
                {
                    incidentId = ht["@IncidentID"].ToString();
                }
            }
        }
        else if (type.Equals("External"))
        {

            using (var command = new DBCommand("uspOocInitialiseNewExtIncident", QueryType.StoredProcedure, oParams, db.Connection))
            {
                Hashtable ht;
                numRecordsAffected = command.Execute(out ht);

                if (ht.ContainsKey("@IncidentNumber"))
                {
                    incidentNumber = ht["@IncidentNumber"].ToString();
                }
                if (ht.ContainsKey("@IncidentID"))
                {
                    incidentId = ht["@IncidentID"].ToString();
                }
            }
        }
        else
        {
            using (var command = new DBCommand("uspOocInitialiseNewTaxEscalation", QueryType.StoredProcedure, oParams, db.Connection))
            {
                Hashtable ht;
                numRecordsAffected = command.Execute(out ht);

                if (ht.ContainsKey("@IncidentNumber"))
                {
                    incidentNumber = ht["@IncidentNumber"].ToString();
                }
                if (ht.ContainsKey("@IncidentID"))
                {
                    incidentId = ht["@IncidentID"].ToString();
                }
            }
        }
        return numRecordsAffected;
    }

    public static int GetProcessFieldCount(string processId)
    {
        using (
            var command = new DBCommand("[dbo].[uspGetProcessFieldCount]", QueryType.StoredProcedure,
                                        new DBParamCollection { { "@ProcessId", processId } }, db.Connection))
        {
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }

    public static int PublishProcess(string processId)
    {
        using (
            var command = new DBCommand("[dbo].[uspPublishProcess]", QueryType.StoredProcedure,
                                        new DBParamCollection { { "@ProcessId", processId } }, db.Connection))
        {
            return command.Execute();
        }
    }

    public static List<LookupQuarters> GetLookupQuarters()
    {
        return
            new LookupQuarters("NQT_Select_LookupQuarters",
                         null).GetRecords<LookupQuarters>();
    }

    public static List<LookupQuarters> GetLookupObjectives()
    {
        return
            new LookupQuarters("NQT_Select_StrategicObjective",
                         null).GetRecords<LookupQuarters>();
    }
    public static RecordSet GetLookupObjectives(int userId)
    {
        StringBuilder sb = new StringBuilder();
      
        sb.Append("   SELECT  DISTINCT NTQ_Lookup_StrategicObjective.Name, us.fk_UserId, NTQ_Lookup_StrategicObjective.Id ");
        sb.Append(" FROM     NTQ_User_Units AS u INNER JOIN ");
                   sb.Append("  NTQ_UserKeyResults AS uk ON u.Id = uk.fk_NTQ_User_Unit_Id INNER JOIN ");
                  sb.Append("   NTQ_User_UnitsMappings AS us ON us.fk_User_UnitId = u.Id INNER JOIN  ");
        sb.Append("   NTQ_Lookup_StrategicObjective ON uk.fk_ObjectiveId = NTQ_Lookup_StrategicObjective.Id ");
  sb.Append(" WHERE us.fk_UserId = " + userId  );

        using (var data = new RecordSet(sb.ToString(), QueryType.TransectSQL, null, db.ConnectionString))
        {
            if (data.HasRows)
            {
                return data;
            }           
        }
        return null;
    }
    public static List<LookupQuarters> GetNTQ_YesNo()
    {
        return
            new LookupQuarters("NQT_Select_YesNo",
                         null).GetRecords<LookupQuarters>();
    }
    public static List<LookupQuarters> NTQ_SELECT_LookupAnchorNames()
    {
        return
            new LookupQuarters("NTQ_SELECT_LookupAnchorNames",
                         null).GetRecords<LookupQuarters>();
    }
    public static List<LookupQuarters> NTQ_SELECT_NTQ_Lookup_KeyResultOwner()
    {
        return
            new LookupQuarters("NTQ_SELECT_NTQ_Lookup_KeyResultOwner",
                         null).GetRecords<LookupQuarters>();
    }
    public static List<DIT> GetTID(int id)
    {
        return
            new DIT("NQT_Select_TID",
                          new Dictionary<string, object> { { "@Id", id } }).GetRecords<DIT>();
    }

    public static List<DIT> GetTIDDescriptionById(int id)
    {
        return
            new DIT("NQT_Select_TIDByID",
                          new Dictionary<string, object> { { "@Id", id } }).GetRecords<DIT>();
    }

    public static List<LookupKeyResult> NTQ_SELECT_NTQ_Lookup_AnnualTarget(int id)
    {
        return
            new LookupKeyResult("NTQ_SELECT_NTQ_Lookup_AnnualTarget",
                          new Dictionary<string, object> { { "@Id", id } }).GetRecords<LookupKeyResult>();
    }
    public static List<LookupKeyResult> NTQ_SELECT_NTQ_Lookup_Q1(int id)
    {
        return
            new LookupKeyResult("NTQ_SELECT_NTQ_Lookup_Q1",
                          new Dictionary<string, object> { { "@Id", id } }).GetRecords<LookupKeyResult>();
    }
    public static List<LookupKeyResult> NTQ_SELECT_NTQ_Lookup_Q2(int id)
    {
        return
            new LookupKeyResult("NTQ_SELECT_NTQ_Lookup_Q2",
                          new Dictionary<string, object> { { "@Id", id } }).GetRecords<LookupKeyResult>();
    }
    public static List<LookupKeyResult> NTQ_SELECT_NTQ_Lookup_Q3(int id)
    {
        return
            new LookupKeyResult("NTQ_SELECT_NTQ_Lookup_Q3",
                          new Dictionary<string, object> { { "@Id", id } }).GetRecords<LookupKeyResult>();
    }

    public static List<LookupKeyResult> NTQ_SELECT_NTQ_Lookup_Q4(int id)
    {
        return
            new LookupKeyResult("NTQ_SELECT_NTQ_Lookup_Q4",
                          new Dictionary<string, object> { { "@Id", id } }).GetRecords<LookupKeyResult>();
    }
    public static List<LookupKeyResult> GetKeyResultByObjectivesID(int id)
    {
        return
            new LookupKeyResult("NQT_Select_KeyResultByStrategicObjectiveID",
                          new Dictionary<string, object> { { "@Id", id } }).GetRecords<LookupKeyResult>();
    }
    public static List<LookupKeyResult> GetKeyResultIndicatorByObjectivesID(int id)
    {
        return
            new LookupKeyResult("NQT_Select_KeyResultIndicatorByObjectiveID",
                          new Dictionary<string, object> { { "@Id", id } }).GetRecords<LookupKeyResult>();
    }
    public static List<NTQ_Data> GetNTQRIncidents(decimal userId)
    {
        return
            new NTQ_Data("[dbo].[ntq_GetNTQRIncidents]",
                         new Dictionary<string, object> { { "@userId", userId } }).GetRecords<NTQ_Data>();
    }

    public static List<NTQ_Report_KeyResults> NTQ_Report_KeyResults_SelectByID(int id)
    {
        return
            new NTQ_Report_KeyResults("[dbo].[NTQ_Report_KeyResults_SelectByID]",
                         new Dictionary<string, object> { { "@ID", id } }).GetRecords<NTQ_Report_KeyResults>();
    }

    public static List<NTQ_Report_KeyResults> NTQ_CheckIfObjectivesExists(int ObjectiveId,int KeyResultID, string CFY)
    {
        return
            new NTQ_Report_KeyResults("[dbo].[NTQ_CheckIfExists]",
                         new Dictionary<string, object> { { "@ObjectiveId", ObjectiveId } , { "@KeyResultID", KeyResultID }, { "@CFY", CFY } }).GetRecords<NTQ_Report_KeyResults>();
    }

    public static List<NTRQ_User> GetNTQR_UserBySID(string sid)
    {
        return
            new NTRQ_User("GetNTQR_UserBySID", new Dictionary<string, object> { { "@SID", sid } }).GetRecords<NTRQ_User>();
    }

    public static List<NTRQ_User> GetNTQR_Role()
    {
        return
            new NTRQ_User("GetNTQR_UserRole", null).GetRecords<NTRQ_User>();
    }
    public static List<NTRQ_UserRole> GetNTQR_UserRoleById(int uId)
    {
        return
            new NTRQ_UserRole("GetNTQR_UserRoleById", new Dictionary<string, object> { { "@Id", uId } }).GetRecords<NTRQ_UserRole>();
    }

    public static void GetNTQR_RemoveUserFromRole(int Id, int roleId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@Id", Id},
                              {"@roleId", roleId}

                          };
        using (
            var command = new DBCommand("GetNTQR_RemoveUserFromRole", QueryType.StoredProcedure, oParams, db.Connection))
        {
            command.Execute();
        }
    }

    public static void NTQ_User_Actions_Insert(string action,string xml, string CreatedBy, DateTime CreatedDate)
    {
        var oParams = new DBParamCollection
                          {
                              {"@action", action},
                              {"@xml", xml},
                              {"@CreatedBy", CreatedBy},
                              {"@CreatedDate", CreatedDate}
                          };
        using (
            var command = new DBCommand("NTQ_User_Actions_Insert", QueryType.StoredProcedure, oParams, db.Connection))
        {
            command.Execute();
        }
    }

    public static List<NTRQ_User> GetNTQR_UserRole()
    {
        return
            new NTRQ_User("GetNTQR_AllUsersRole", null).GetRecords<NTRQ_User>();
    }

    public static int NTQ_Report_Objectives_InsertOrUpdate(NTQ_Report_Objectives obj)
    {


        var oParams = new DBParamCollection
                          {

                           {"@ID", obj.Id },
                           {"@fk_IncidentId" ,DBNull.Value},
                           {"@fk_Report_Objectives_ID", obj.fk_Report_Objectives_ID },
                           {"@CFY", obj. CFY},
                           {"@CreatedDate" ,obj. CreatedDate},
                           {"@CreatedBy", obj.CreatedBy },
                           {"@ModifiedBy", obj. ModifiedBy},
                           {"@ModifiedDate" , obj. ModifiedDate},
                           {"@ObjID", null, ParameterDirection.Output}
                          };

        using (
                       var command = new DBCommand("NTQ_Report_Objectives_InsertOrUpdate", QueryType.StoredProcedure, oParams,
                           db.Connection))
        {
            var s = obj.Id;
            Hashtable ht;
            command.Execute(out ht);
            if (ht.Count > 0)
            {
                return Convert.ToInt32(ht["@ObjID"].ToString());
            }
            return s;

        }
    }

    public static int NTQ_Report_UpDateOrInsert(NTQ_Report obj)
    {
        var keyResultDate = obj.KeyResultOwnerDate.ToString().Equals("1/1/0001 12:00:00 AM") ? null : obj.KeyResultOwnerDate;
        var anchorDate = obj.AnchorDate.ToString().Equals("1/1/0001 12:00:00 AM") ? null : obj.AnchorDate;

        var oParams = new DBParamCollection
                          {
                                          {"@Id" , obj.Id},
                                          {"@fk_Quarter_ID" , obj.fk_Quarter_ID},
                                          {"@fk_ReportKeyResult_ID",obj.fk_ReportKeyResult_ID},
                                          {"@fk_IncidentId" , obj.fk_IncidentId},
                                          {"@ActualAchievement" , obj.ActualAchievement},
                                          {"@Variance" , obj.Variance},
                                          {"@fk_TargetMetID" , obj.fk_TargetMetID},
                                          {"@fk_DataValidAndCorrect_ID" , obj.fk_DataValidAndCorrect_ID},
                                          {"@ReasonForVariance" , obj.ReasonForVariance},
                                          {"@MitigationForUnderPerformance" , obj.MitigationForUnderPerformance},
                                          {"@CommentOnPeformance" , obj.CommentOnPeformance},
                                          {"@fk_CalculatedAccordingToTID" , obj.fk_CalculatedAccordingToTID},
                                          {"@IfNotCalcAccordingToTID" , obj.IfNotCalcAccordingToTID},
                                          {"@DataSoruce" , obj.DataSoruce},
                                          {"@Evidence" , obj.Evidence},
                                          {"@CompilerName" , obj.CompilerName},
                                          {"@CompilerSigned" , obj.CompilerSigned},
                                          {"@CompilerDate" , obj.CompilerDate},
                                          {"@KeyResultOwnerName" , obj.KeyResultOwnerName},
                                          {"@KeyResultOwnerSigned" , obj.KeyResultOwnerSigned},
                                          {"@KeyResultOwnerApproved" , obj.KeyResultOwnerApproved},
                                          {"@KeyResultOwnerDate" , keyResultDate},

                                           {"@AnchorName" , obj.AnchorName},
                                          {"@AnchorApproved" , obj.AnchorApproved},
                                          {"@AnchorSigned" , obj.AnchorSigned},
                                          {"@AnchorDate" , anchorDate},

                                           {"@KeyResultOwner2Name" , obj.KeyResultOwner2Name},
                                          {"@KeyResultOwner2Signed" , obj.KeyResultOwner2Signed},
                                          {"@KeyResultOwner2Approved" , obj.KeyResultOwner2Approved},
                                          {"@KeyResultOwner2Date" , obj.KeyResultOwner2Date},

                                         
                                          
                                          {"@Anchor2Name" , obj.Anchor2Name},
                                          {"@Anchor2Approved" , obj.Anchor2Approved},
                                          {"@Anchor2Signed" , obj.Anchor2Signed},
                                          {"@anchor2Date" , obj.Anchor2Date},

                                          {"@CreatedDate" , obj.CreatedDate},
                                          {"@CreatedBy" , obj.CreatedBy},
                                          {"@ModifiedBy" , obj.ModifiedBy},
                                          {"@ModifiedDate" , obj.ModifiedDate},
                                          
                          };

        using (
                       var command = new DBCommand("NTQ_Report_UpDateOrInsert", QueryType.StoredProcedure, oParams,
                           db.Connection))
        {            
           return command.Execute();           

        }
    }

    public static int NTQ_Report_Insert(NTQ_Report obj)
    {
        var keyResultDate = obj.KeyResultOwnerDate.ToString().Equals("1/1/0001 12:00:00 AM") ? null : obj.KeyResultOwnerDate;
        var anchorDate = obj.AnchorDate.ToString().Equals("1/1/0001 12:00:00 AM") ? null : obj.AnchorDate;

        var oParams = new DBParamCollection
                          {
                                          {"@Id" , obj.Id},
                                          {"@fk_Quarter_ID" , obj.fk_Quarter_ID},
                                          {"@fk_ReportKeyResult_ID",obj.fk_ReportKeyResult_ID},
                                          {"@fk_IncidentId" , obj.fk_IncidentId},
                                          {"@ActualAchievement" , obj.ActualAchievement},
                                          {"@Variance" , obj.Variance},
                                          {"@fk_TargetMetID" , obj.fk_TargetMetID},
                                          {"@fk_DataValidAndCorrect_ID" , obj.fk_DataValidAndCorrect_ID},
                                          {"@ReasonForVariance" , obj.ReasonForVariance},
                                          {"@MitigationForUnderPerformance" , obj.MitigationForUnderPerformance},
                                          {"@CommentOnPeformance" , obj.CommentOnPeformance},
                                          {"@fk_CalculatedAccordingToTID" , obj.fk_CalculatedAccordingToTID},
                                          {"@IfNotCalcAccordingToTID" , obj.IfNotCalcAccordingToTID},
                                          {"@DataSoruce" , obj.DataSoruce},
                                          {"@Evidence" , obj.Evidence},
                                          {"@CompilerName" , obj.CompilerName},
                                          {"@CompilerSigned" , obj.CompilerSigned},
                                          {"@CompilerDate" , obj.CompilerDate},                                          
                                          {"@CreatedDate" , obj.CreatedDate},
                                          {"@CreatedBy" , obj.CreatedBy},
                                          {"@KeyResultOwnerName" , obj.KeyResultOwnerName},
                                          {"@AnchorName" , obj.AnchorName},
                                          {"@NewID", null, ParameterDirection.Output}
                          };

        using (
                       var command = new DBCommand("NTQ_Report_Insert", QueryType.StoredProcedure, oParams,
                           db.Connection))
        {
            var s = obj.Id;
            Hashtable ht;
            command.Execute(out ht);
            if (ht.Count > 0)
            {
                return Convert.ToInt32(ht["@NewID"].ToString());
            }
            return s;

        }
    }

    public static int NTQ_Report_KeyResults_InsertOrUpdate(NTQ_Report_KeyResults obj)
    {


        var oParams = new DBParamCollection
                          {

                               {"@ID", obj.Id },
                                {"@CFY",obj.CFY},
                                {"@fk_NTQ_StrategicObjective_ID", obj.fk_NTQ_StrategicObjective_ID },
                                {"@fk_Lookup_KeyResult_ID", obj.fk_Lookup_KeyResult_ID },
                                {"@fk_KeyResultIndicator_ID", obj.fk_KeyResultIndicator_ID},
                                {"@Anchor ", obj.Anchor },
                                {"@KeyResultOwner ", obj. KeyResultOwner},
                                {"@fk_LookupAnualTarget_ID", obj.fk_LookupAnualTarget_ID },
                                {"@fk_lookup_Q1_ID", obj.fk_lookup_Q1_ID },
                                {"@fk_lookup_Q2_ID", obj. fk_lookup_Q2_ID},
                                {"@fk_lookup_Q3_ID", obj.fk_lookup_Q3_ID },
                                {"@fk_lookup_Q4_ID", obj. fk_lookup_Q4_ID},
                                {"@fk_lookupTID", obj. fk_lookupTID},
                                {"@CreatedDate ", obj. CreatedDate},
                                {"@CreatedBy", obj. CreatedBy},
                                {"@ModifiedBy", obj. ModifiedBy},
                                {"@ModifiedDate ", obj.ModifiedDate },
                                {"@ObjID", null, ParameterDirection.Output}
                          };

        using (
                       var command = new DBCommand("NTQ_Report_KeyResults_InsertOrUpdate", QueryType.StoredProcedure, oParams,
                           db.Connection))
        {
            var s = obj.Id;
            Hashtable ht;
            command.Execute(out ht);
            if (ht.Count > 0)
            {
                if (!string.IsNullOrEmpty(ht["@ObjID"].ToString()))
                {
                    return Convert.ToInt32(ht["@ObjID"].ToString());
                }
            }
            return s;

        }
    }

    public static RecordSet NTQ_Report_SelectByKeyResultId(int keyResultId,int quarterId = 0)
    {
        var oParams = new DBParamCollection
                          {

                               {"@KeyResultID", keyResultId },
                                {"@quarterId", quarterId },
                           };
        using (var data = new RecordSet("NTQ_Report_SelectByKeyResultId", QueryType.StoredProcedure, oParams, db.ConnectionString))
        {
            if (data.HasRows)
            {
                return data;
            }
            return null;
        }
    }

    public static List<NTQUserInUnits> NTQ_Report_GetUsersByUnit(int unitId, string sid)
    {      

        return new NTQUserInUnits("NQT_GetUserUnits", new Dictionary<string, object> { { "@unitId", unitId }, { "@sid", sid } }).GetRecords<NTQUserInUnits>();
    }

    public static List<NTQUserInUnits> NTQ_Report_GetUserUnitsBySid(string sid,int keyResult)
    {

        return new NTQUserInUnits("NQT_GetUserUnitsBySID", new Dictionary<string, object> { { "@keyResultId", keyResult }, { "@sid", sid } }).GetRecords<NTQUserInUnits>();   

    }


    public static RecordSet NTQ_Report_SelectApprovedKeyResultReport(int keyResultId, int quarterId = 0)
    {
        var oParams = new DBParamCollection
                          {

                               {"@KeyResultID", keyResultId },
                                {"@quarterId", quarterId },
                           };
        using (var data = new RecordSet("NTQ_Report_SelectApprovedKeyResultReport", QueryType.StoredProcedure, oParams, db.ConnectionString))
        {
            if (data.HasRows)
            {
                return data;
            }
            return null;
        }
    }

    public static List<Incident> GetUserAssignedIncidents(string sid, int statusId)
    {
        return
            new Incident("[dbo].[uspGetMyAssignedIncidents]",
                         new Dictionary<string, object> { { "@SID", sid }, { "@StatusId", statusId } }).GetRecords<Incident>();
    }
    public static List<NTQ_Report> NTQ_Report_SelectByReportId(string id)
    {
        return
            new NTQ_Report("[dbo].[NTQ_Report_SelectByReportId]",
                         new Dictionary<string, object> {  { "@Id",id } }).GetRecords<NTQ_Report>();
    }

    public static List<NTQ_PrintReport> NTQ_Report_PrintByReportId(string id)
    {
        return
            new NTQ_PrintReport("[dbo].[NTQ_Quarter_Report_SelectByReportId]",
                         new Dictionary<string, object> { { "@Id", id } }).GetRecords<NTQ_PrintReport>();
    }


    public static int NQT_AddUserToRole(NTRQ_User details)
    {
        var oParams = new DBParamCollection
                          {
                              {"@UserCode", details.UserCode},
                              {"@UserFullName", details.UserFullName},
                              {"@IsActive", 1},
                              {"@Signature", details.Signature},
                               {"@CreatedBy", details.CreatedBy},
                                {"@CreatedDate", DateTime.Now},
                                {"@UserId", details.UserId},
                                {"@RoleId", details.RoleId},
                                {"@fk_User_UnitId", details.fk_User_UnitId},

                          };
        using (
            var command = new DBCommand("[dbo].[GetNTQR_AddUserToARole]", QueryType.StoredProcedure, oParams,
                                        db.Connection))
        {
            return command.Execute();
        }
    }

    public static List<IncidentStatus> GetIncidentsStatuses()
    {
        return
            new IncidentStatus("[dbo].[uspGetIncidentStatuses]", null).GetRecords<IncidentStatus>();
    }
    public static List<UserRoles> GetIncidentsRolesByUserRoleId(Guid roleId)
    {       
        return
            new IncidentStatus("[dbo].[Incident_RoleById]",
            new Dictionary<string, object> { { "@RoleId", roleId } }).GetRecords<UserRoles>();
    }
    public static int UpdateIncidentDetails(DateTime dueDate, string assignedTo, string incidentId, Guid roleId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@DueDate", dueDate},
                              {"@AssignedTo", assignedTo},
                              {"@IncidentId", incidentId},
                              {"@RoleId", roleId},
                              {"@LastModifiedBySID", SarsUser.SID}
                          };
        using (
            var command = new DBCommand("[dbo].[uspAssignIncident]", QueryType.StoredProcedure, oParams, db.Connection))
        {
            return command.Execute();
        }
    }

    public static int AllocateSecondAssignee( string assignedTo, string incidentId, bool isSecondAssigned)
    {
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidentId},
                              {"@AssignedTo", assignedTo},                               
                              {"@CreatedBy", SarsUser.SID},
                              {"@SecSID", isSecondAssigned}
                          };
        using (
            var command = new DBCommand("[dbo].[uspAllocateSecondAssignee]", QueryType.StoredProcedure, oParams, db.Connection))
        {
            return command.Execute();
        }
    }

    public static int UpdateInternalOocIncidentDetails(DateTime dueDate, string assignedTo, string incidentId, string summary)
    {
        var oParams = new DBParamCollection
                          {
                              {"@DueDate", dueDate},
                              {"@AssignedTo", assignedTo},
                              {"@IncidentId", incidentId},
                              {"@Summary", summary},
                              {"@LastModifiedBySID", SarsUser.SID}
                          };
        using (
            var command = new DBCommand("[dbo].[uspAssignIncident_OOCInternal]", QueryType.StoredProcedure, oParams, db.Connection))
        {
            return command.Execute();
        }
    }

    public static int UpdateOtherIncidentDetails(string incidentId, string summary, string crossRefNo)
    {
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidentId},
                              {"@Summary", summary},
                              {"@LastModifiedBySID", SarsUser.SID},
                              {"@CrossRefNo", crossRefNo}
                          };
        using (
            var command = new DBCommand("[dbo].[uspUpdateOtherIncidentDetails]", QueryType.StoredProcedure, oParams,
                                        db.Connection))
        {
            return command.Execute();
        }
    }

    public static RecordSet getCoverPage(string processId, string incidentId)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT DISTINCT ooc.IncidentId, ooc.FieldName, ooc.Answer, p.Description ");
        sb.Append(" FROM vw_OOCOffice AS ooc INNER JOIN ");
        sb.Append("Incidents AS inc ON inc.IncidentID = ooc.IncidentId INNER JOIN ");
        sb.Append("ProcessFields AS pf ON pf.FieldName = ooc.FieldName INNER JOIN ");
        sb.Append("Processes AS p ON p.ProcessId = inc.ProcessId ");
        sb.Append("WHERE (pf.AddToCoverPage = 1) AND (inc.ProcessId = " + processId + ") AND (ooc.IncidentId =" + incidentId + ") ");

        using (var data = new RecordSet(sb.ToString(), QueryType.TransectSQL, null, db.ConnectionString))
        {
            if (data.HasRows)
            {
                return data;
            }
            return null;
        }
    }

    public static int AddWorkInfo(WorkInfoDetails details)
    {
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", details.IncidentId},
                              {"@ProcessId", details.ProcessId},
                              {"@AddedBySID", details.AddedBySID},
                              {"@Notes", details.Notes}
                          };
        using (
            var command = new DBCommand("[dbo].[spINSERT_WorkInfoDetails]", QueryType.StoredProcedure, oParams,
                                        db.Connection))
        {
            return command.Execute();
        }
    }

    public static List<WorkInfoDetails> GetWorkInfoByIncidentID(string incidentId)
    {
        return
            new WorkInfoDetails("[dbo].[spREAD_WorkInfoDetails]",
                                new Dictionary<string, object>
                                    {
                                        {"@IncidentId", incidentId}
                                    }).GetRecords<WorkInfoDetails>();
    }

    public static List<IncidentAllocation> GetSecondAssignedUser(string incidentId)
    {
        return
            new IncidentAllocation("[dbo].[uspReadAllocateSecondAssignee]",
                                new Dictionary<string, object>
                                    {
                                        {"@IncidentID", incidentId}
                                    }).GetRecords<IncidentAllocation>();
    }

    public static WorkInfoDetails GetWorkInfoByID(string noteId)
    {
        return
            new WorkInfoDetails("[dbo].[spREAD_WorkInfoDetailsById]",
                                new Dictionary<string, object>
                                    {
                                        {"@NoteId", noteId}
                                    }).GetRecord<WorkInfoDetails>();
    }


    public static int AddUserToAProcess(string sid, string processId, string roleId)
    {
        using (var command = new DBCommand("uspINSERT_UserProcesses", QueryType.StoredProcedure, new DBParamCollection
                                                                                                     {
                                                                                                         {"@SID", sid},
                                                                                                         {
                                                                                                             "@ProcessId",
                                                                                                             processId
                                                                                                         },
                                                                                                         {
                                                                                                             "@ProcessRoleId"
                                                                                                             , roleId
                                                                                                         }
                                                                                                     }, db.Connection))
        {
            return command.Execute();
        }
    }

    public static int UpdateIncidentStatus(int statusId, string incidentId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidentId},
                              {"@StatusId", statusId},
                              {"@SID", SarsUser.SID}
                          };
        using (
            var command = new DBCommand("[dbo].[usUpdateIncidentStatus]", QueryType.StoredProcedure, oParams,
                                        db.Connection))
        {
            return command.Execute();

        }
    }
    public static int NQT_UpdateIncidentStatus(int statusId, string incidentId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidentId},
                              {"@StatusId", statusId},
                              {"@SID", SarsUser.SID}
                          };
        using (
            var command = new DBCommand("[dbo].[usUpdateIncidentStatus_NTQ]", QueryType.StoredProcedure, oParams,
                                        db.Connection))
        {
            return command.Execute();

        }
    }

    public static int NQT_UpdateReworkSignedParties(int Id)
    {
        var oParams = new DBParamCollection
                          {
                              {"@id", Id},
                             
                          };
        using (
            var command = new DBCommand("[dbo].[ntq_Report-Rework]", QueryType.StoredProcedure, oParams,
                                        db.Connection))
        {
            return command.Execute();

        }
    }

    public static int ChangeFieldOrder(string fieldId, string sOrder, string processId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@FieldId", fieldId},
                              {"@SortOrder", sOrder},
                              {"@ProcessId", processId}
                          };
        using (
            var command = new DBCommand("[dbo].[uspChangeFieldOrder]", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            return command.Execute();
        }
    }

    public static int AddProcessOwner(long processId, string ownerSID)
    {
        var oParams = new DBParamCollection
                          {
                              {"@ProcessId", processId},
                              {"@SID", ownerSID}
                          };
        using (
            var command = new DBCommand("[dbo].[uspAddProcessOwners]", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            return command.Execute();
        }
    }

    public static List<ProcessOwner> GetProcessOwners(string processId)
    {
        return
            new ProcessOwner("uspGetProcessOwners", new Dictionary<string, object>
                                                        {
                                                            {"@ProcessId", processId}
                                                        }).GetRecords<ProcessOwner>();
    }

    public static List<UserProcess> GetUsersAssigneToThisProcess(string processId)
    {

        return
            new ProcessOwner("[dbo].[uspGetUsersAssignedToThisProcess]", new Dictionary<string, object>
                                                                             {
                                                                                 {"@ProcessId", processId}
                                                                             }).GetRecords<UserProcess>();
    }

    public static List<UserProcess> GetUserRoles()
    {

        return
            new ProcessOwner("[dbo].[uspGetUserRoles]", new Dictionary<string, object>
                                                            {
                                                                {"@SID", SarsUser.SID}
                                                            }).GetRecords<UserProcess>();
    }

    public static int GetInitUser()
    {
        using (
            var command = new DBCommand("dbo.uspIsPowerUser", QueryType.StoredProcedure,
                                        new DBParamCollection { { "@SID", SarsUser.SID } }, db.Connection))
        {
            var userId = command.ExecuteScalar();
            if (userId != DBNull.Value)
            {
                return Convert.ToInt32(userId);
            }
        }
        return 0;
    }

    public static int RemoveProcessUser(int userProcessId)
    {
        using (
            var command = new DBCommand("[dbo].[uspRemoveProcessUser]", QueryType.StoredProcedure, new DBParamCollection
                                                                                                       {
                                                                                                           {
                                                                                                               "@UserProcessId"
                                                                                                               ,
                                                                                                               userProcessId
                                                                                                           }
                                                                                                       }))
        {
            return command.Execute();
        }
    }

    public static int RemoveProcessOwner(int ownerId)
    {
        using (
            var command = new DBCommand("[dbo].[uspRemoveProcessOwner]", QueryType.StoredProcedure,
                                        new DBParamCollection
                                            {
                                                {"@OwnerId", ownerId}
                                            }))
        {
            return command.Execute();
        }
    }

    public static List<Incident> GetProcessIncidents(string processId)
    {
        return
            new Incident("[dbo].[uspGetProcessIncidents]", new Dictionary<string, object>
                                                               {
                                                                   {"@ProcessId", processId}
                                                               }).GetRecords<Incident>();
    }
    public static List<Incident> GetOocProcessIncidents(string processId)
    {
        return
            new Incident("[dbo].[uspGetOocProcessIncidents]", new Dictionary<string, object>
                                                               {
                                                                   {"@ProcessId", processId}
                                                               }).GetRecords<Incident>();
    }
    public static int DeleteProcessField(string fielId)
    {
        using (
            var command = new DBCommand("[dbo].[uspDeleteProcessField]", QueryType.StoredProcedure,
                                        new DBParamCollection { { "@ProcessFieldId", fielId } }, db.Connection))
        {
            return command.Execute();
        }
    }

    public static int ChangeAssignee(string incidentId, string assigneeSID)
    {
        var oParams = new DBParamCollection { { "@IncidentId", incidentId }, { "@SID", assigneeSID } };
        using (
            var command = new DBCommand("[dbo].[uspChangeIncidentAssignee]", QueryType.StoredProcedure, oParams,
                                        db.Connection))
        {
            return command.Execute();
        }
    }

    public static int CompleteIncident(string incidentId)
    {
        var oParams = new DBParamCollection { { "@IncidentId", incidentId } };
        using (
            var command = new DBCommand("[dbo].[uspCompleteIncident]", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            return command.Execute();
        }
    }
    public static int FinaliseIncident(string incidentId)
    {
        var oParams = new DBParamCollection { { "@IncidentId", incidentId } };
        using (
            var command = new DBCommand("[dbo].[uspFiniliseIncident]", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            return command.Execute();
        }
    }
    public static int ReturnToOriginator(string incidentId)
    {
        var oParams = new DBParamCollection { { "@IncidentId", incidentId } };
        using (
            var command = new DBCommand("[dbo].[uspReturnToOriginatorIncident]", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            return command.Execute();
        }
    }
    public static int CloseIncident(string incidentId)
    {
        var oParams = new DBParamCollection { { "@IncidentId", incidentId } };
        using (
            var command = new DBCommand("[dbo].[uspCloseIncident]", QueryType.StoredProcedure, oParams, db.Connection))
        {
            return command.Execute();
        }
    }

    public static int ReOpenIncident(string incidentId)
    {
        var oParams = new DBParamCollection { { "@IncidentId", incidentId } };
        using (
            var command = new DBCommand("[dbo].[uspReOpenIncident]", QueryType.StoredProcedure, oParams, db.Connection))
        {
            return command.Execute();
        }
    }

    public static int UploadFile(string incidentID, string noteId, byte[] buffer, string fileName)
    {
        var connection = new SqlConnection(db.ConnectionString);
        connection.Open();
        var transaction = connection.BeginTransaction();

        var command = new SqlCommand("[dbo].[uspAddIncidentFile]", connection, transaction)
        { CommandType = CommandType.StoredProcedure };

        command.Parameters.AddWithValue("@IncidentId", incidentID);
        command.Parameters.AddWithValue("@DocumentName", fileName);
        command.Parameters.AddWithValue("@AddedBySID", SarsUser.SID);
        command.Parameters.AddWithValue("@Extension", Path.GetExtension(fileName));
        command.Parameters.AddWithValue("@WorkInfoId", noteId);

        var filePathParam = new SqlParameter("@FilePath", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output };
        command.Parameters.Add(filePathParam);

        var saved = command.ExecuteNonQuery();
        var path = command.Parameters["@FilePath"].Value.ToString();
        command = new SqlCommand("SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()", connection, transaction);

        var objContext = (byte[])command.ExecuteScalar();
        try
        {
            var fileStream = new SqlFileStream(path, objContext, FileAccess.Write);
            fileStream.Write(buffer, 0, buffer.Length);
            fileStream.Close();
            transaction.Commit();

        }
        catch (Exception ex)
        {

            //MessageBox.Show(ex.Message);
        }

        return saved;
    }

    public static List<IncidentDocument> GetDocumentsByNoteId(string noteId)
    {
        return
            new IncidentDocument("[dbo].[uspGet_IncidentDocuments]",
                                 new Dictionary<string, object> { { "@NoteId", noteId } })
                .GetRecords<IncidentDocument>();
    }

    public static List<IncidentDocument> GetDocumentsByIncidentId(string incidentId)
    {
        return
            new IncidentDocument("[dbo].[uspGet_IncidentDocumentsByIncidentId]",
                                 new Dictionary<string, object> { { "@IncidentId", incidentId } })
                .GetRecords<IncidentDocument>();
    }





    public static int SaveFileType(string extension, string fileType)
    {
        var oParams = new DBParamCollection
                          {
                              {"@MimeType", fileType},
                              {"@Extension", extension}
                          };
        using (
            var command = new DBCommand("[dbo].[spINSERT_MimeTypes]", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            return command.Execute();
        }
    }

    public static List<MimeType> GetFileTypes()
    {
        return new MimeType("spREAD_MimeTypes", null).GetRecords<MimeType>();
    }

    public static List<PowerUser> GetPowerUsers()
    {
        return new PowerUser("uspGetPowerUsers", null).GetRecords<PowerUser>();
    }

    public static int SavePowerUser(string sid)
    {
        var oParams = new DBParamCollection
                          {
                              {"@SID", sid}
                          };
        using (var command = new DBCommand("[dbo].[uspAddPowerUser]", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            return command.Execute();
        }
    }

    public static void OpenFile(string directory, object docId, System.Web.HttpResponse response)
    {
        var connection = new SqlConnection(db.ConnectionString);
        connection.Open();
        var transaction = connection.BeginTransaction();

        var command = new SqlCommand("[dbo].[uspGetDocument]", connection, transaction)
        { CommandType = CommandType.StoredProcedure };


        command.Parameters.AddWithValue("@DocId", docId);
        var path = string.Empty;
        var fileType = string.Empty;
        var contentType = string.Empty;

        using (var sdr = command.ExecuteReader())
        {
            if (sdr.HasRows)
            {
                sdr.Read();
                path = sdr["PathName"].ToString();
                fileType = sdr["Extension"].ToString();
                contentType = sdr["MimeType"].ToString();
            }
        }

        if (string.IsNullOrEmpty(contentType))
        {
            contentType = "application/stream";
        }

        command = new SqlCommand("SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()", connection, transaction);

        var objContext = (byte[])command.ExecuteScalar();
        var objSqlFileStream = new SqlFileStream(path, objContext, FileAccess.Read);
        var buffer = new byte[(int)objSqlFileStream.Length];
        objSqlFileStream.Read(buffer, 0, buffer.Length);
        objSqlFileStream.Close();

        transaction.Commit();
        if (connection.State != ConnectionState.Closed)
        {
            connection.Close();
        }

        var fileName = string.Format("{0}\\{1}{2}", directory, DateTime.Now.ToFileTime(), fileType);

        response.AddHeader("Content-disposition", "attachment; filename=" + Path.GetFileName(fileName));
        response.ContentType = contentType;

        File.WriteAllBytes(fileName, buffer);
        var contentLength = buffer.Length;
        response.AddHeader("Content-Length", contentLength.ToString());
        response.OutputStream.Write(buffer, 0, contentLength);
        response.Flush();
        //response.End();
    }

    public static int RemovePowerUser(string sid)
    {
        var oParams = new DBParamCollection
                          {
                              {"@SID", sid}
                          };
        using (
            var command = new DBCommand("[dbo].[uspRemovePowerUser]", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            return command.Execute();
        }
    }

    public static RecordSet GetProcessReportBystatus(string processId, string statusId)
    {
        return new RecordSet("usp_RPT_IncidentsByStatus", QueryType.StoredProcedure, new DBParamCollection
                                                                                         {
                                                                                             {"@ProcessId", processId},
                                                                                             {"@Status", statusId}
                                                                                         }, db.ConnectionString);
    }

    public static RecordSet GetIncidentsSearchFields(string processId)
    {
        return new RecordSet("uspIncidentsSearchFields", QueryType.StoredProcedure, new DBParamCollection
                                                                                         {
                                                                                             {"@ProcessId", processId},

                                                                                         }, db.ConnectionString);
    }

    public static RecordSet SearchIncidents(string refNo,
                                            string startDate,
                                            string endDate,
                                            string sid,
                                            string filterType,
                                            string processId, string answer = null)
    {
        var oParams = new DBParamCollection { { "@FilterType", filterType }, { "@ProcessId", processId } };
        if (filterType == "DateRegistered" || filterType == "DueDate")
        {
            oParams.Add("@DateFrom", startDate);
            oParams.Add("@DateTo", endDate);
        }
        if (filterType == "RegisteredBy" || filterType == "AssignedTo")
        {
            oParams.Add("@SID", sid);
        }
        if (filterType == "IncidentNumber")
        {
            oParams.Add("@RefNo", refNo);
        }
        if (!string.IsNullOrEmpty(answer))
        {
            oParams.Add("@Answer", answer);
        }
        return new RecordSet("[dbo].[uspSearchIncidents]", QueryType.StoredProcedure, oParams, db.ConnectionString);
    }

    public static RecordSet OocSearchIncidents(string refNo,
                                          string startDate,
                                          string endDate,
                                          string sid,
                                          string filterType,
                                          string processId)
    {
        var oParams = new DBParamCollection { { "@FilterType", filterType }, { "@ProcessId", processId } };
        if (filterType == "DateRegistered" || filterType == "DueDate")
        {
            oParams.Add("@DateFrom", startDate);
            oParams.Add("@DateTo", endDate);
        }
        if (filterType == "RegisteredBy" || filterType == "AssignedTo")
        {
            oParams.Add("@SID", sid);
        }
        if (filterType == "IncidentNumber")
        {
            oParams.Add("@RefNo", refNo);
        }
        if (filterType == "Subject")
        {
            oParams.Add("@RefNo", refNo);
        }


        return new RecordSet("[dbo].[uspOocSearchIncidents]", QueryType.StoredProcedure, oParams, db.ConnectionString);
    }




    public static int UpdateProcessVersion(string sid, string processId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@SID", sid},
                              {"@ProcessId", processId}
                          };
        using (
            var command = new DBCommand("[dbo].[uspChangeProcessVersion]", QueryType.StoredProcedure, oParams,
                                        db.Connection))
        {
            return command.Execute();
        }
    }



    public static int SaveIncidentEmailLog(string body, string subject, string sentToSID, string sentToEmailAddress,
                                           string incidentId, string processId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@SentBy", SarsUser.SID},
                              {"@EmailBody", body},
                              {"@SentTo", sentToSID},
                              {"@EmailAddress", sentToEmailAddress},
                              {"@Subject", subject},
                              {"@IncidentId", incidentId},
                              {"@ProcessId", processId}
                          };
        using (
            var oCommand = new DBCommand("spINSERT_IncidentEmailLog", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            var saved = oCommand.Execute();
            return saved;
        }
    }

    public static IncidentEmailLog GetEmailLogById(string id)
    {
        return
            new IncidentEmailLog("spREAD_IncidentEmailLogById", new Dictionary<string, object> { { "@EmailLogId", id } }).
                GetRecord<IncidentEmailLog>();
    }

    public static List<IncidentEmailLog> SearchEmailLogByIncidentNumber(string incidentNumber)
    {
        return
            new IncidentEmailLog("[dbo].[spREAD_IncidentEmailLogByIncidentNo]",
                                 new Dictionary<string, object> { { "@IncidentNo", incidentNumber } }).
                GetRecords<IncidentEmailLog>();
    }

    public static void SaveSystemError(string sessionId, string message, string stacktrace, string systemUser)
    {
        var oParams = new DBParamCollection
                          {
                              {"@Message", message},
                              {"@SessionId", sessionId},
                              {"@StackTrace", stacktrace},
                              {"@UserName", systemUser}
                          };

        using (var oCommand = new DBCommand("spINSERT_SystemErrors", QueryType.StoredProcedure, oParams, db.Connection))
        {
            oCommand.Execute();
        }
    }

    public static int AddChildFields(string processId, string cheildFieldId, string parentFieldId, string parentOptionId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@ProcessId", processId},
                              {"@FieldId", cheildFieldId},
                              {"@ParentFieldId", parentFieldId},
                              {"@ParentOptionId", parentOptionId},
                              {"@AddedBy", SarsUser.SID}
                          };

        using (
            var oCommand = new DBCommand("[dbo].[spINSERT_ParentFields]", QueryType.StoredProcedure, oParams,
                                         db.Connection))
        {
            return oCommand.Execute();
        }
    }

    public static int RemoveChildFields(string processId, string cheildFieldId, string parentFieldId, string parentOptionId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@FieldId", cheildFieldId},
                              {"@ParentFieldId", parentFieldId},
                              {"@ParentOptionId", parentOptionId},
                              {"@AddedBy", SarsUser.SID}
                          };

        using (
            var oCommand = new DBCommand("[dbo].[spRemove_ParentChildFields]", QueryType.StoredProcedure, oParams,
                                         db.Connection))
        {
            return oCommand.Execute();
        }
    }
    public static RecordSet GetErrorReport(string startDate, string endDate)
    {
        var oParams = new DBParamCollection
                          {
                              {"@StartDate", startDate},
                              {"@EndDate", endDate}
                          };
        return new RecordSet("spREAD_SystemErrors", QueryType.TransectSQL, oParams, db.ConnectionString);
    }

    public static int CopyIncident(string incidnetToCopy, string createdBy, string processId, out string incidentNumber,
                                   out string incidentId)
    {
        incidentNumber = string.Empty;
        incidentId = string.Empty;
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidnetToCopy},
                              {"@CreatedBySID", createdBy},
                              {"@ProcessId", processId},
                              {"@IncidentNumber", null, ParameterDirection.Output},
                              {"@NewIncidentId", null, ParameterDirection.ReturnValue}
                          };
        using (var command = new DBCommand("[dbo].[uspCopyIncident]", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            Hashtable ht;
            var saved = command.Execute(out ht);
            if (ht.ContainsKey("@IncidentNumber"))
            {
                incidentNumber = ht["@IncidentNumber"].ToString();
            }
            if (ht.ContainsKey("@NewIncidentId"))
            {
                incidentId = ht["@NewIncidentId"].ToString();
            }
            return saved;
        }
    }

    public static ProcessField GetFieldById(string fieldId)
    {
        var fieldDetails =
            new ProcessField("[dbo].[uspREAD_ProcessFieldById]",
                             new Dictionary<string, object> { { "@FieldId", fieldId } }).
                GetRecord<ProcessField>();
        return fieldDetails;
    }

    public static List<ProcessField> GetPossibleChildFields(string fieldId)
    {
        var fieldDetails =
            new ProcessField("[dbo].[uspGetPossibleChildFields]",
                             new Dictionary<string, object> { { "@ParentFieldId", fieldId } }).
                GetRecords<ProcessField>();
        return fieldDetails;
    }

    public static List<ChildField> GetChildFields(string parentField, string parentOptionId)
    {
        var fieldDetails =
            new ProcessField("[dbo].[uspGetChildFields]",
                             new Dictionary<string, object> { { "@ParentFieldId", parentField }, { "@ParentOptionId", parentOptionId } }).
                GetRecords<ChildField>();
        return fieldDetails;
    }


    public static List<ChildField> GetChildFieldsByParentId(string parentField)
    {
        var fieldDetails =
            new ProcessField("[dbo].[uspGetChildFieldsByParentField]",
                             new Dictionary<string, object> { { "@ParentFieldId", parentField } }).
                GetRecords<ChildField>();
        return fieldDetails;
    }

    public static int DeleteFile(string docId)
    {
        using (var command = new DBCommand("dbo.uspDeleteIncidentFile", QueryType.StoredProcedure, new DBParamCollection() { { "@DocId", docId } }, db.Connection))
        {
            return command.Execute();
        }
    }

    public static RecordSet GetOocRecordSet(decimal incidentId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidentId}
                          };
        return new RecordSet("spREAD_OOCOffice", QueryType.StoredProcedure, oParams, db.ConnectionString);
    }

    public static RecordSet GetOocWorkInfoRecordSet(decimal incidentId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidentId}
                          };
        return new RecordSet("spREAD_GetooOfficeWorkDetails", QueryType.StoredProcedure, oParams, db.ConnectionString);
    }
    public static int AddCoverPage(decimal incidentId, int delivered, string documentContent, int signedOff, int docCheckedForGrammar, int docCheckedForConflict,
        int docAddressInfoRequested, int docNotDicloseFacts, int docSarsStdTemp, string commissionerComent, int dg, int deputyMinister, int minister, int addInfo, string nature)
    {
        var oParams = new DBParamCollection
        {
            {"@IncidentId", incidentId},
            {"@Delivered", delivered},
            {"@DocumentContent", documentContent},
            {"@SignedOff", signedOff},
            {"@DocCheckedForGrammar", docCheckedForGrammar},
            {"@DocCheckedForConflict", docCheckedForConflict},
            {"@DocAddressInfoRequested", docAddressInfoRequested},
            {"@DocNotDicloseFacts", docNotDicloseFacts},
            {"@DocSarsStdTemp", docSarsStdTemp},
            {"@Nature", nature},
            {"@CommissionerComent", commissionerComent},
            {"@DG", dg},
            {"@DeputyMinister", deputyMinister},
            {"@Minister", minister},
            {"@AddInfo ", addInfo},
            {"@CreatedBySID", SarsUser.SID},
            {"@UpdatedBySID", SarsUser.SID},
            {"@UpdatedDate", DateTime.Now},
        };

        using (var oCommand = new DBCommand("[dbo].[spINSERT_IncidentCoverPage]", QueryType.StoredProcedure, oParams, db.Connection))
        {
            return oCommand.Execute();
        }
    }

    public static int CreateProcessFields(string fieldName, string displayName, string processId, int fieldType)
    {
        var oParams = new DBParamCollection
                          {
                              {"@FieldName", Utils.CleanHTMLData(fieldName)},
                              {"@Display", displayName},
                              {"@ProcessId", processId},
                              {"@FieldTypeId", fieldType},
                              {"@IsActive", 1},
                              {"@IsRequired", 1},
                              {"@ShowOnSearch", 1},
                              {"@ShowOnScreen", 1},
                              {"@ShowOnReport", 1},
                              {"@AddedBySID", SarsUser.SID},
                              {"@Return_Value", null, System.Data.ParameterDirection.ReturnValue}
                          };


        Hashtable oHashTable;
        int scopeIdentity;
        using (
            var oCommand = new DBCommand("[dbo].[uspINSERT_ProcessField]", QueryType.StoredProcedure, oParams,
                                         db.TransactionConnection))
        {
            scopeIdentity = 0;
            oCommand.Execute(out oHashTable);
            oCommand.Commit();
        }
        if (oHashTable.Count > 0)
        {
            scopeIdentity = int.Parse(oHashTable["@Return_Value"].ToString());
        }
        return scopeIdentity;
    }

    public static void SaveAnswers(string incidentId, string processId, decimal questionId, int questionTypeId, object answer)
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
                        oSurveyGen.SaveFreeTextAnswers(answer.ToString(), questionId, incidentId);
                        //var selectedAnswers = (List<int>)answer;
                        //oSurveyGen.RemoveMultiChoiceAnswers(questionId, incidentId);
                        //foreach (var i in selectedAnswers)
                        //{
                        //    oSurveyGen.SaveMultiChoiceAnswers(i, questionId, incidentId);
                        //}
                        //selectedAnswers.Clear();
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
                        oSurveyGen.SaveFreeTextAnswers(answer.ToString(), questionId, incidentId);
                        break;
                    }
            }
            oSurveyGen.Dispose();
        }
    }

    public static void SavePcrAnswers(string incidentId, string processId, decimal questionId, int questionTypeId, object answer)
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
                        // oSurveyGen.SaveScaleAnswers(answer.ToString(), questionId, incidentId);
                        oSurveyGen.SaveFreeTextAnswers(answer.ToString(), questionId, incidentId);
                        break;
                    }
                case 5:
                    {
                        oSurveyGen.SaveFreeTextAnswers(answer.ToString(), questionId, incidentId);
                        //var selectedAnswers = (List<int>)answer;
                        //oSurveyGen.RemoveMultiChoiceAnswers(questionId, incidentId);
                        //foreach (var i in selectedAnswers)
                        //{
                        //    oSurveyGen.SaveMultiChoiceAnswers(i, questionId, incidentId);
                        //}
                        //selectedAnswers.Clear();
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
                        oSurveyGen.SaveFreeTextAnswers(answer.ToString(), questionId, incidentId);
                        break;
                    }
            }
            oSurveyGen.Dispose();
        }
    }

    public static List<ServiceConfig> GetOocProcessConfiguration()
    {

        using (
            var data = new RecordSet("ImsService_SELECT_ProcessConfiguration", QueryType.StoredProcedure, null,
                db.ConnectionString))
        {
            if (data.HasRows)
            {
                return data.Tables[0].ToList<ServiceConfig>();
            }
        }
        return null;
    }
    public static RecordSet ReadOocRecordSet(string incidentId)
    {
        var oParams = new DBParamCollection
                          {
                              {" @incidentId", incidentId}
                          };
        using (
            var lists = new RecordSet("SELECT   [IncidentId],[FieldName],[Answer] FROM [dbo].[vw_OOCOffice]  where IncidentId ='" + incidentId + "'", QueryType.TransectSQL, null,
                                      db.ConnectionString))
        {
            return lists;
        }
    }
    public static RecordSet GetAllPeopleToBeEmailed(string incidentId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidentId}
                          };
        using (
            var lists = new RecordSet("uspSelect_PeopleToBeEmailed", QueryType.StoredProcedure, oParams,
                                      db.ConnectionString))
        {
            return lists;
        }
    }
    public static RecordSet GetCosCCSPerson(string incidentId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidentId}
                          };
        string sql = @" SELECT IncidentID, FieldName, CONVERT(nvarchar(200), Answer) AS Answer
                         FROM  dbo.vw_FreeTextAnswers  where IncidentId= @IncidentId
				        AND FieldName = 'AlwaysCCTheCos'";
        using (
            var lists = new RecordSet(sql, QueryType.TransectSQL, oParams,
                                      db.ConnectionString))
        {
            return lists;
        }
    }
    public static int AssignLegislativeOffice(decimal IncidentId, int assigned, long processid)
    {
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", IncidentId},
                               {"@AssignToAdmn", assigned},
                                {"@ProcessId", processid},

                          };
        using (var command = new DBCommand("[dbo].[uspLegAssignIncident]", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            return command.Execute();
        }
    }

    public static int InsertOrUpdateServiceConfig(ServiceConfig conf)
    {
        var oParams = new DBParamCollection
         {
                { "@ProcessId", conf.ProcessId},                            
                { "@NotifyUsers" ,conf.NotifyUsers },           
                { "@EscalateToManagers" ,conf.EscalateToManagers },                
                { "@EscalateToProcessOwners" ,conf.EscalateToProcessOwners },           
                { "@EscalateToDeputyCom" ,conf.EscalateToDeputyCom },           
                { "@DuputyComEmail" ,conf.DuputyComEmail },
                { "@IsProServer" ,conf.IsProServer },
                { "@TestEmailsGoTo" ,conf.TestEmailsGoTo },

         };
        using (var command = new DBCommand("[dbo].[usp_Service_UpInsert_Config]", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            return command.Execute();
        }
    }
}