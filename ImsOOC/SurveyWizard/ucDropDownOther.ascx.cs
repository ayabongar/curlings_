using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SurveyWizard_ucDropDownOther : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public string Other
    {
        get
        {
            return ViewState["other"] == null ? string.Empty : ViewState["other"].ToString();
        }
        set { ViewState["other"] = value; }
    }
    public DropDownList DropDownListControl
    {
        get { return this.ddlQuestionAnswers; }
    }
    public string Intruction
    {
        set { this.lblQuestionDescriptionDropDown.Text = value; }
    }
    protected void ddlQuestionAnswers_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}