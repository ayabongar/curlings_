using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;

public partial class Admin_ManageQuestionTypes : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("~/NoAccessForm.aspx");
        }
        if(!IsPostBack)
        {
            dgQuestionTypes.Bind(IncidentTrackingManager.GetFieldTypes());
        }
    }

    protected void Modify(object sender, EventArgs e)
    {
        var lnk = sender as Control;
        if (lnk == null)
            return;
        var row = lnk.Parent.Parent as GridViewRow;
        if (row == null)
            return;
        var lblQuestionTypeId = row.FindControl("lblQuestionTypeId") as Label;
        if (lblQuestionTypeId == null)
            return;

        var chk = row.Cells[1].Controls[0] as CheckBox;
        if (chk == null)
            return;

        ViewState["lblQuestionTypeId"] = lblQuestionTypeId.Text;
        btnAddMoreScales.Visible = lblQuestionTypeId.Text == "1";
        if (lblQuestionTypeId.Text == "1")
        {
            LoadScaleTypes();
        }
        else
        {
            row_scale_types.Visible = false;
        }
        btnAddMoreMatrices.Visible = lblQuestionTypeId.Text == "9";

        row_modify.Visible = true;
        row_addnew.Visible = false;
        row_matrix.Visible = false;
        row_scaleoptions.Visible = false;
        txtQuesTypeDescr.Text = row.Cells[0].Text;
        chkIsActive.Checked = chk.Checked;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var oParams = new DBParamCollection
                          {
                              {"@FieldTypeId", ViewState["lblQuestionTypeId"]},
                              {"@IsActive", chkIsActive.Checked},
                              {"@Description", txtQuesTypeDescr.Text}
                          };
        using (var oCommand = new DBCommand("[dbo].[uspUPSERT_FieldType]", QueryType.StoredProcedure, oParams, db.Connection))
        {
            oCommand.Execute();
           
            MessageBox.Show("Question type changed.");
            row_modify.Visible = false;
            dgQuestionTypes.Bind(IncidentTrackingManager.GetFieldTypes());
        }
    }
    protected void AddNewQuestionType(object sender, EventArgs e)
    {
        row_addnew.Visible = true;
        this.row_modify.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.row_addnew.Visible = false;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Under construction");
    }
    protected void btnAddMoreScales_Click(object sender, EventArgs e)
    {

    }
    protected void btnAddMoreScales_Click1(object sender, EventArgs e)
    {
        row_scaleoptions.Visible = true;
        row_addnew.Visible = false;
        row_matrix.Visible = false;
        btnAddMoreScales.Visible = false;
        
    }
    protected void btnAddMoreMatrices_Click(object sender, EventArgs e)
    {
        row_matrix.Visible = true;
        row_addnew.Visible = false;
        row_scaleoptions.Visible = false;
        btnAddMoreMatrices.Visible = false;
    }
    protected void btnAddOption_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.txtScaleDescription.Text))
        {
            MessageBox.Show("Please type in a scale type description.");
            return;
        }
        if (string.IsNullOrEmpty(txtScaleOption.Text))
        {
            MessageBox.Show("Please type in an option.");
            return;
        }
        var itm = new ListItem(txtScaleOption.Text, txtScaleOption.Text);
        txtScaleOption.Text = string.Empty;
        if (lbScaleOptions.Items.Contains(itm))
        {
            MessageBox.Show("This item is already added.");
            return;
        }
        if (lbScaleOptions.Items.Count >= 10)
        {
            MessageBox.Show("You can only have 10 options per scale item.");
            return;
        }
        lbScaleOptions.Items.Add(itm);


    }
    protected void btnSubmitNewScaleType_Click(object sender, EventArgs e)
    {
        if (lbScaleOptions.Items.Count == 0)
        {
            MessageBox.Show("Please add alteast one option.");
            return;
        }
        var saved = IncidentTrackingManager.SaveScaleTypes(0, txtScaleDescription.Text, true);
        if (saved <= 0) return;
        foreach (ListItem itm in this.lbScaleOptions.Items)
        {
            IncidentTrackingManager.SaveScaleOptions(saved, itm.Text, true);
        }
        LoadScaleTypes();
        MessageBox.Show("New Scale Type Added.");
        row_scaleoptions.Visible = false;
        txtScaleDescription.Text = string.Empty;
        lbScaleOptions.Items.Clear();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        this.row_scaleoptions.Visible = false;
    }
    protected void btnAddMatrixOption_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtMatrixDescription.Text))
        {
            MessageBox.Show("Please type in a matrix type description.");
            return;
        }
       
        if (string.IsNullOrEmpty(txtMatrixColumn1Heading.Text))
        {
            MessageBox.Show("Please type in column 1 heading.");
            return;
        }
        if (string.IsNullOrEmpty(txtMatrixColumn2Heading.Text))
        {
            MessageBox.Show("Please type in column 2 heading.");
            return;
        }
        if (string.IsNullOrEmpty(txtMatrixOption1.Text))
        {
            MessageBox.Show("Please type in first column.");
            return;
        }

        if (string.IsNullOrEmpty(txtMatrixOption2.Text))
        {
            MessageBox.Show("Please type in second column.");
            return;
        }

        var itemValue = string.Concat(txtMatrixOption1.Text, "|", txtMatrixOption2.Text);
        var itm = new ListItem(itemValue, itemValue);
        txtScaleOption.Text = string.Empty;
        
        if (lbMatrixOptions.Items.Contains(itm))
        {
            MessageBox.Show("This item is already added.");
            return;
        }
        if (lbMatrixOptions.Items.Count >= 10)
        {
            MessageBox.Show("You can only have 10 options per scale.");
            return;
        }
        lbMatrixOptions.Items.Add(itm);
        txtMatrixOption1.Text = string.Empty;
        txtMatrixOption2.Text = string.Empty;
    }

    protected void btnSubmitMatrix_Click(object sender, EventArgs e)
    {
        if (lbMatrixOptions.Items.Count == 0)
        {
            MessageBox.Show("Please add alteast one option.");
            return;
        }
        var saved = IncidentTrackingManager.SaveMatrixDimentions(0, txtMatrixDescription.Text, txtMatrixColumn1Heading.Text, txtMatrixColumn2Heading.Text, true);
        if (saved <= 0) return;
        foreach (ListItem itm in lbMatrixOptions.Items)
        {
            var options = itm.Text.Split("|".ToCharArray());
            if(options.Length == 2)
            {
                IncidentTrackingManager.SaveMatrixOptions(saved, options[0], options[1], true);
            }
        }
        MessageBox.Show("New Matrix Type Added.");
        row_matrix.Visible = false;
        txtMatrixDescription.Text = string.Empty;
        txtMatrixColumn1Heading.Text = string.Empty;
        txtMatrixColumn2Heading.Text = string.Empty;

        lbScaleOptions.Items.Clear();
    }
    protected void btnCancelAddingMatrix_Click(object sender, EventArgs e)
    {
        row_matrix.Visible = false;
        row_scaleoptions.Visible = false;
        row_modify.Visible = false;
    }
    protected void btnRemoveMatrixOption_Click(object sender, EventArgs e)
    {
        var toRemove = lbMatrixOptions.Items.Cast<ListItem>().Where(listItem => listItem.Selected).ToList();
        foreach (var listItem in toRemove)
        {
            lbMatrixOptions.Items.Remove(listItem);
        }
    }
    protected void btnRemoveScale_Click(object sender, EventArgs e)
    {
        var toRemove = lbScaleOptions.Items.Cast<ListItem>().Where(listItem => listItem.Selected).ToList();
        foreach (var listItem in toRemove)
        {
            lbScaleOptions.Items.Remove(listItem);
        }
    }
    protected void btnCancelMatrix_Click(object sender, EventArgs e)
    {
        row_modify.Visible = false;
        row_matrix.Visible = false;
    }
    private void LoadScaleTypes()
    {
        row_scale_types.Visible = true;

        var scaleTypes = IncidentTrackingManager.GetScaleTypes();
            gvAvailableScaleOptions.DataSource = scaleTypes;
            gvAvailableScaleOptions.DataBind();
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType != DataControlRowType.DataRow)
        //    return;
        //e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#CCCCFF';style.cursor='hand'");
        //e.Row.Attributes.Add("onmouseout", "style.backgroundColor='White'");
    }
    protected void dgQuestionTypes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgQuestionTypes.NextPage(IncidentTrackingManager.GetFieldTypes(), e.NewPageIndex);
    }
}