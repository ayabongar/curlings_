using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Sars.Systems.Data;

public partial class SurveyWizard_AnswerAgreeDisagree : System.Web.UI.UserControl
{
    //private SurveyManager mSurveyManager;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView = this.gvQuestions;
        //if(!IsPostBack)
            //CreateQuestions();
    }

    public void CreateQuestions()
    {
        if (this.Data != null)
        {
            if (Data.Tables.Count > 0)
            {
                if (Data.Tables["Table1"].Rows.Count > 0)
                {
                    gvQuestions.DataSource = Data.Tables["Table1"];
                    gvQuestions.DataBind();
                }
            }
        }
    }

    public RecordSet Data { get; set; }
   
    public string StrongAgree
    {
        get; set;
    }

    public string Agree { get; set; }
    public string NeitherAgreeNorDisagree { get; set; }
    public string Disagree { get; set; }
    public string StronglyDisagree { get; set; }

    public int QuestionId { get; set; }
    public int QuestionnaireId { get; set; }
    public int SectionId { get; set; }
    public int SubSectionId { get; set; }

    public int SelectedAnser
    {
        get; set;
    }

    public GridView GridView { get; set; }


    protected void gvQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[5].Text = Convert.ToString(Data.Tables["Table"].Rows[0]["Description"]);
        }
    }
}