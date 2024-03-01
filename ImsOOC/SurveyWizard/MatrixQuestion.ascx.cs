using System;
using System.ComponentModel;
using Sars.Systems.Data;
using System.Web.UI.WebControls;
using Sars.Systems.Controls;
public partial class SurveyWizard_MatrixQuestion : System.Web.UI.UserControl
{
   // protected void Page_Load(object sender, EventArgs e)    {    }

    [Bindable(true)]
    public string MatrixLeftHeader
    {
        get
        {
            var obj = ViewState["mlh"];
            return (obj == null) ? string.Empty : obj.ToString();
        }
        set
        {
            ViewState["mlh"] = value; 
        }
    }

    [Bindable(true)]
    public string MatrixRightHeader
    {
        get
        {
            var obj = ViewState["mrh"];
            return (obj == null) ? string.Empty : obj.ToString();
        }
        set
        {
            ViewState["mrh"] = value;
        }
    }
    public MatrixValues? SelectedValues
    {
        get
        {
            if (rbtnLeft.SelectedIndex == -1 || rbtnRight.SelectedIndex == -1)
                return null;
            MatrixValues values;
            values.LeftValue = rbtnLeft.SelectedValue;
            values.RightValue = rbtnRight.SelectedValue;
            return values;
        }
        set
        {
            if (value != null)
            {
                var matrixValues = (MatrixValues)value;
                if (!string.IsNullOrEmpty(matrixValues.LeftValue))
                {
                    this.rbtnLeft.SelectItemByValue(matrixValues.LeftValue);
                }

                if(!string.IsNullOrEmpty(matrixValues.RightValue))
                {
                    this.rbtnRight.SelectItemByValue(matrixValues.RightValue);
                }
            }
        }
    }

    public RadioButtonList LeftColumn
    {
        get { return rbtnLeft; }
    }
    public RadioButtonList RightColumn
    {
        get { return rbtnRight; }
    }
    public void Bind(RecordSet options)
    {
        rbtnLeft.Bind(options, "LeftDimension", "MatrixOptionId");
        if (rbtnLeft.Items.Count > 0)
            rbtnLeft.Items.RemoveAt(0);
        rbtnRight.Bind(options, "RightDimension", "MatrixOptionId");

        if (rbtnRight.Items.Count > 0)
            rbtnRight.Items.RemoveAt(0);

        MatrixLeftHeader = options[0]["LeftHeader"].ToString();
        MatrixRightHeader = options[0]["RighHeader"].ToString();
    }
}