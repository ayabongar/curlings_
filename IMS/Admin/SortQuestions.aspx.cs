using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;

public partial class Admin_SortQuestions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnReOrder_Click(object sender, EventArgs e)
    {
        using (var survey = new RecordSet("SELECT DISTINCT QuestionnaireId From dbo.Questions Order By QuestionnaireId", QueryType.TransectSQL, null, db.ConnectionString))
        {
            if (survey.HasRows)
            {
                foreach (DataRow dataRow in survey.Tables[0].Rows)
                {
                    var questionnireid = dataRow["QuestionnaireId"].ToString();
                    var questions = new RecordSet("SELECT QuestionsId FROM dbo.Questions WHERE QuestionnaireId = @QuestionnaireId  Order By QuestionsId ", QueryType.TransectSQL, new DBParamCollection { { "@QuestionnaireId", questionnireid } }, db.ConnectionString);
                    if (questions.HasRows)
                    {
                        var i = 0;
                        foreach (DataRow queRows in questions.Tables[0].Rows)
                        {
                            i++;
                            var qId = queRows["QuestionsId"].ToString();
                            var comm =
                                new DBCommand(
                                    "UPDATE dbo.Questions SET SortOrder = @Order WHERE QuestionsId=@qId",
                                    QueryType.TransectSQL,
                                    new DBParamCollection { { "@Order", i }, { "@qId", qId } }, db.Connection);
                            comm.Execute();
                        }
                    }
                    //var sections = new RecordSet("SELECT DISTINCT SectionId From dbo.Questions Where QuestionnaireId = @Qess", QueryType.TransectSQL, new DBParamCollection { { "@Qess", questionnireid } }, db.ConnectionString);

                    //if(sections.HasRows)
                    //{

                    //foreach (DataRow secRow in sections.Tables[0].Rows)
                    //{
                    //    var sectionId = secRow["SectionId"].ToString();
                    //    var subsections =
                    //        new RecordSet("SELECT Subsectionid From dbo.Questions Where SectionId = @SecId",
                    //                      QueryType.TransectSQL, new DBParamCollection {{"@SecId", sectionId}},
                    //                      db.ConnectionString);

                    //    if(subsections.HasRows)
                    //    {
                    //        foreach (DataRow subRow in subsections.Tables[0].Rows)
                    //        {
                    //            var subSecId = subRow["SubSectionId"].ToString();
                    //            var questions =
                    //                new RecordSet("SELECT QuestionsId FROM dbo.Questions WHERE Subsectionid = @Sub",
                    //                              QueryType.TransectSQL, new DBParamCollection {{"@Sub", subSecId}},
                    //                              db.ConnectionString);
                    //            if(questions.HasRows)
                    //            {
                    //                var i = 0;
                    //                foreach (DataRow queRows in questions.Tables[0].Rows)
                    //                {
                    //                    i++;
                    //                    var qId = queRows["QuestionsId"].ToString();
                    //                    var comm =
                    //                        new DBCommand(
                    //                            "UPDATE dbo.Questions SET SortOrder = @Order WHERE QuestionsId=@qId",
                    //                            QueryType.TransectSQL,
                    //                            new DBParamCollection {{"@Order", i}, {"@qId", qId}}, db.Connection);
                    //                    comm.Execute();
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
            }

        }
        MessageBox.Show("The Processing completed successfully");
    }
}