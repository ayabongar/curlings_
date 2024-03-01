using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RadioButtonListOther : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public string SelectedAnswer
    {
        get
        {
            var numItems = rbtnQuestionAnswers.Items.Count;
            return rbtnQuestionAnswers.SelectedIndex == (numItems - 1)
                       ? string.Format("OTHER|{0}", txtOther.Text)
                       : rbtnQuestionAnswers.SelectedValue;
        }
    }
    public RadioButtonList RadioButtonList
    {
        get { return rbtnQuestionAnswers; }
    }
    public TextBox Other
    {
        get { return txtOther; }
    }
    
    protected void SelectedIndexChanged(object sender, EventArgs e)
    {
        var numItems = rbtnQuestionAnswers.Items.Count;
        txtOther.Enabled = rbtnQuestionAnswers.SelectedIndex == (numItems-1);
    }
}