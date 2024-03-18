using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;
/// <summary>
/// Summary description for SurveyGenerator
/// </summary>


public sealed class SurveyGenerator : IDisposable
{
    public struct IntelliValues
    {
        public string SelectedValue;
        public string SelectedText;
        public string NextQuestionId;
        public object Tag;
    }
	
    public SurveyGenerator(string processId, int numberOfQuestionsPerPage, int nextPageNumber)
	{
        ProcessID = processId;
        NumberOfPagesPerPage = numberOfQuestionsPerPage;
	    NextPageNumber = nextPageNumber;
	}
    
   

    public SurveyGenerator(string processId)
    {
        ProcessID = processId;
    }

    public int ResponseID { get; set; }

    public SurveyGenerator(string processId, int responseId)
    {
        ProcessID = processId;
        ResponseID = responseId;
    }

    public SurveyGenerator(){}

    public int NumberOfPagesPerPage { get; set; }
    public int NextPageNumber { get; set; }
    public string ProcessID { get; set; }
 
  
    public void SaveFreeTextAnswers(string ans, decimal fieldId, string incidentId)
    {
        var oParams = new DBParamCollection
            {
                {"@Answer", ans},
                {"@FieldId", fieldId},
                {"@ProcesId", ProcessID},
                {"@IncidentId", incidentId}
            };
        using (
            var oCommand = new DBCommand("uspINSERT_FreeTextAnswers", QueryType.StoredProcedure, oParams, db.Connection)
            )
        {
            oCommand.Execute();
            oCommand.Commit();
        }
        oParams.Clear();
        oParams.Dispose();
    }
    public void SaveMultiChoiceAnswers(int ans, decimal quesId, string incidentId)
    {
        var oParams = new DBParamCollection
            {
                {"@Answer", ans},
                {"@FieldId", quesId},
                {"@ProcessId", ProcessID},
                {"@IncidentId", incidentId}
            };
        using (
            var oCommand = new DBCommand("uspINSERT_MultiChoiceAnswers", QueryType.StoredProcedure, oParams,
                                         db.Connection))
        {
            oCommand.Execute();
        }
        oParams.Clear();
        oParams.Dispose();

    }
    public void SaveMatrixOptionAnswers(string ansLeft, string ansRight, decimal quesId, string incidentId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@LeftDimensionAnswer", ansLeft},
                              {"@RightDimensionAnswer", ansRight},
                              {"@FieldId", quesId},
                              {"@ProcessId", ProcessID},
                              {"@IncidentId", incidentId}
                          };
        using (
            var oCommand = new DBCommand("uspINSERT_MatrixOptionAnswers", QueryType.StoredProcedure, oParams,
                                         db.Connection))
        {
            oCommand.Execute();
        }
        if (oParams != null)
        {
            oParams.Clear();
            oParams.Dispose();
        }
    }
    public void SaveScaleAnswers(string ans, decimal quesId, string incidentId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@Answer", ans},
                              {"@ProcessId", ProcessID},
                              {"@IncidentId", incidentId},
                              {"@FieldId", quesId    }
                          };
        using (var oCommand = new DBCommand("uspINSERT_ScaleAnswers", QueryType.StoredProcedure, oParams, db.Connection))
        {
            oCommand.Execute();
        }
    }

    public void SaveScaleHierarchyookUpAnswers(SelectedHierarchicalDetails ans, decimal quesId, string incidentId)
    {
        var oParams = new DBParamCollection
            {
                {"@Level1Answer", ans.FirstLevel},
                {"@Level2Answer", ans.SecondLevel},
                {"@Level3Answer", ans.ThirdLevel},
                {"@ProcessId", ProcessID},
                {"@IncidentId", incidentId},
                {"@FieldId", quesId}
            };
        using (var oCommand = new DBCommand("[dbo].[uspINSERT_HierarchyLookupDataAnswers]", QueryType.StoredProcedure, oParams, db.Connection))
        {
            oCommand.Execute();
        }
    }
    public static int AddMultiChoiceOptions(string processId, string fieldId, string option)
    {
        var oParams = new DBParamCollection
            {
                {"@FieldId", fieldId},
                {"@ProcessId", processId},
                {"@OptionDescription", option}
            };
        using (
            var oCommand = new DBCommand("uspINSERT_MultichoiceOptions", QueryType.StoredProcedure, oParams,
                                         db.Connection))
        {
            var saved = oCommand.Execute();
            oParams.Clear();
            oParams.Dispose();
            return saved;
        }
    }

    public static int ModifyMultiChoiceOption(MultichoiceOption option)
    {
        var oParams = new DBParamCollection
        {
            {"@MultichoiceOptionId", option.MultichoiceOptionId},
            {"@OptionDescription", option.OptionDescription}
        };
        using (var oCommand = new DBCommand("uspUPDATE_MultichoiceOptions", QueryType.StoredProcedure, oParams, db.Connection))
        {
            var saved = oCommand.Execute();
            return saved;
        }
    }
    public void CleanMultiAnswersWithOther(decimal fieldId, string incidentId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@IncidentId", incidentId},
                              {"@FieldId", fieldId    }
                          };
        using (var oCommand = new DBCommand("uspRemoveMultiAnswersWithOther", QueryType.StoredProcedure, oParams, db.Connection))
        {
            oCommand.Execute();
        }
    }
    public void SaveLookupAnswers(string ans, decimal quesId, string responseId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@Answer", ans},
                              {"@ProcessId", ProcessID},
                              {"@IncidentId", responseId},
                              {"@FieldId", quesId}
                          };
        using (
            var oCommand = new DBCommand("uspINSERT_LookupDataAnswers", QueryType.StoredProcedure, oParams,
                                         db.Connection))
        {
            oCommand.Execute();
        }

        oParams.Clear();
        oParams.Dispose();

    }

    public void DeleteScaleAnswers(decimal fieldId, int incidentId)
    {
        var oParams = new DBParamCollection
                          {                            
                              {"@IncidentId", incidentId},
                              {"@FieldId", fieldId    }
                          };
        using (var oCommand = new DBCommand("uspDELETE_ScaleAnswers", QueryType.StoredProcedure, oParams, db.Connection))
        {
            oCommand.Execute();
        }
    }

    public void RemoveMultiChoiceAnswers(decimal questionId, string responseId)
    {
         var oParams = new DBParamCollection
                          {
                              {"@IncidentId",responseId},
                              {"@FieldId",questionId}
                          };
         using (var oCommand = new DBCommand("uspREMOVE_MultiChoiceAndwers", QueryType.StoredProcedure, oParams, db.Connection)) 
        {
            oCommand.Execute();
        }
    }
   

    #region IDisposable Members

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    #endregion

  
  
}

