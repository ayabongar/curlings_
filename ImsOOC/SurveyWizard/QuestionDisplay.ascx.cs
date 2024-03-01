using System;
using System.Activities.Statements;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Controls;

public partial class SurveyWizard_QuestionDisplay : IncidentTrackingControl
{
    public delegate void IntelliSurveyOptionChangeHandler(object sender, IntelliQuestionArgs e);

    public event IntelliSurveyOptionChangeHandler OptionChanged;

    protected void Page_Init(object sender, EventArgs e)
    {
        //ddlQuestionAnswers.AutoPostBack = IsParent;
        //rbtnQuestionAnswers.AutoPostBack = IsParent;
    }

    public int CurrentQuestion
    {
        get
        {
            var obj = ViewState["currentQ"];
            return (obj == null) ? 0 : Convert.ToInt32(obj);
        }
        set { ViewState["currentQ"] = value; }
    }

    [Bindable(true)]
    public decimal FieldId
    {
        get
        {
            var obj = ViewState["fId"];
            return (obj == null) ? 0M : (decimal) obj;
        }
        set { ViewState["fId"] = value; }
    }

    [Bindable(true)]
    public int ScaleTypeId
    {
        get
        {
            var obj = ViewState["ScaleType"];
            return (obj == null) ? 0 : (int) obj;
        }
        set { ViewState["ScaleType"] = value; }
    }

    [Bindable(true)]
    public bool IsParent
    {
        get
        {
            var obj = ViewState["IsParent"];
            return (obj != null) && (bool) obj;
        }
        set { ViewState["IsParent"] = value; }
    }

    [Bindable(true)]
    public bool IsChild
    {
        get
        {
            var obj = ViewState["IsChild"];
            return (obj != null) && (bool) obj;
        }
        set { ViewState["IsChild"] = value; }
    }

    [Bindable(true)]
    public int FieldType
    {
        get
        {
            var obj = ViewState["Ftype"];
            return (obj == null) ? 0 : (int) obj;
        }
        set { ViewState["Ftype"] = value; }
    }

    [Bindable(true)]
    public decimal LookupDataId
    {
        get
        {
            var obj = ViewState["lkpId"];
            return (obj == null) ? 0m : Convert.ToDecimal(ViewState["lkpId"]);
        }
        set { ViewState["lkpId"] = value; }
    }

    [Bindable(true)]
    public decimal HierarchyLookupDataId
    {
        get
        {
            var obj = ViewState["hlkpId"];
            return (obj == null) ? 0m : Convert.ToDecimal(ViewState["hlkpId"]);
        }
        set { ViewState["hlkpId"] = value; }
    }

    [Bindable(true)]
    public string Display { get; set; }

    [Bindable(true)]
    public bool IsRequiredField
    {
        get
        {
            var obj = ViewState["requiredField"];
            return (obj != null) && Convert.ToBoolean(ViewState["requiredField"]);
        }
        set { ViewState["requiredField"] = value; }
    }

    [Bindable(true)]
    public int SortOrder
    {
        get
        {
            var obj = ViewState["SOrder"];
            return (obj == null) ? 0 : (int) ViewState["SOrder"];
        }
        set { ViewState["SOrder"] = value; }
    }

    [Bindable(true)]
    public int CurrentRowIndex
    {
        get
        {
            var obj = ViewState["rIndex"];
            return (obj == null) ? 0 : (int) ViewState["rIndex"];
        }
        set { ViewState["rIndex"] = value; }
    }

    [Bindable(true)]
    public object SelectedAnswer
    {
        get { return GetSelectedAnswer(); }
    }

    public Control Question { get; set; }

    [Bindable(true)]
    public string Instruction
    {
        get
        {
            var obj = ViewState["QuesInstruction"];
            return (obj == null) ? string.Empty : ViewState["QuesInstruction"].ToString();
        }
        set { ViewState["QuesInstruction"] = value; }
    }

    [Bindable(true)]
    public int ValidationType
    {
        get
        {
            var obj = ViewState["valtype"];
            return (obj == null) ? 0 : (int) ViewState["valtype"];
        }
        set { ViewState["valtype"] = value; }
    }

    [Bindable(true)]
    public int MatrixDimensionId
    {
        get
        {
            var obj = ViewState["MatrixDimensionId"];
            return (obj == null) ? 0 : Convert.ToInt32(ViewState["MatrixDimensionId"]);
        }
        set { ViewState["MatrixDimensionId"] = value; }
    }


    public ASP.surveywizard_matrixquestion_ascx MatrixQuestion
    {
        get { return matrix; }
    }

    public ASP.surveywizard_hierarchicallookup_ascx HierarchicalLookup
    {
        get { return HierarchicalLookup1; }
    }

    public ASP.admin_userselector_ascx UserSelector
    {
        get { return UserSelector1; }
    }

    public void ShowQuestions()
    {
        if (FieldType != 0)
        {
            LoadQuestions(FieldType);
        }
    }

    public Control ThisControl
    {
        get
        {
            switch (FieldType)
            {
                case 1:
                case 12:
                    return ScaleTypeId != 5 ? (Control) rbtnQuestionAnswers : ddlQuestionAnswers;
                case 5:
                    return chkbQuestionAnswers;
                case 6:
                    return txtQuestionAnswer;
                case 9:
                    return matrix;
                case 10:
                    return txtComments;
                case 11:
                    return txtDate;
                case 13:
                    return ddlQuestionAnswers;
                case 14:
                    return txtListOption;
                case 16:
                    return rbtnQuestionAnswers;
                case 17:
                    return ddlQuestionAnswers;
                case 18:
                    return CheckBoxListWithOther1;
                case 19:
                    return RadioButtonListWithOther1;
                case 20:
                    {
                        return HierarchicalLookup;
                    }
                case 22:
                    {
                        return UserSelector1;
                    }
            }
            return null;
        }
    }

    protected void RemoveListItem(object sender, EventArgs e)
    {
        var lnk = sender as LinkButton;
        if (lnk != null)
        {
            var row = lnk.Parent.Parent as GridViewRow;
            if (row != null)
            {
                var lblListItemId = row.FindControl("lblListItemId") as Label;
                if (lblListItemId != null)
                {
                    IncidentTrackingManager.RemoveListItem(Convert.ToDecimal(lblListItemId.Text));
                    LoadItems();
                }
            }
        }
    }

    private void LoadItems()
    {
        var lists = IncidentTrackingManager.ReadQuestionListItems(FieldId, IncidentID);
        if (lists.HasRows)
        {
            gvListItems.DataSource = lists;
            gvListItems.DataBind();
        }
        else
        {
            gvListItems.DataSource = null;
            gvListItems.DataBind();
        }
    }

    public string QuesDisplay
    {
        get
        {
            var obj = ViewState["qd"];
            return obj.ToString();
        }
        set { ViewState["qd"] = value; }
    }

    private void LoadQuestions(int questionType)
    {
        txtDate.Attributes.Add("readonly", "readonly");
        QuesDisplay = Display;
        switch (questionType)
        {
            case 1:
            case 12:
                {
                    if (ScaleTypeId != 5 && ScaleTypeId != 0)
                    {

                        var options = IncidentTrackingManager.GetQuestionTypeAnswers(0, ScaleTypeId);
                        if (options != null)
                        {
                            if (options.Tables.Count > 0)
                            {
                                if (options.Tables[0].Rows.Count > 0)
                                {
                                    DeactivateRoles();
                                    ActivateRole(row_scale);

                                    rbtnQuestionAnswers.DataSource = options;
                                    rbtnQuestionAnswers.DataValueField = "QuestionTypeAnswerId";
                                    rbtnQuestionAnswers.DataTextField = "Description";
                                    DataBind();
                                }
                            }
                        }

                    }
                    else
                    {
                        var options = IncidentTrackingManager.GetQuestionTypeAnswers(FieldType, FieldId, ScaleTypeId);
                        if (options != null)
                        {
                            if (options.Tables.Count > 0)
                            {
                                if (options.Tables[0].Rows.Count > 0)
                                {
                                    DeactivateRoles();
                                    ActivateRole(row_dropdown_choice);
                                    ddlQuestionAnswers.Bind(options, "OptionDescription", "MultichoiceOptionId");
                                }
                            }
                        }
                    }
                    break;
                }

            case 5:
                {
                    var options = IncidentTrackingManager.GetQuestionTypeAnswers(FieldType, FieldId, 0);
                    if (options != null)
                    {
                        if (options.HasRows)
                        {
                            DeactivateRoles();
                            ActivateRole(row_multi_choice);
                            chkbQuestionAnswers.Bind(options, "OptionDescription", "MultichoiceOptionId");
                            chkbQuestionAnswers.Items.RemoveAt(0);
                        }
                    }
                    break;
                }
            case 6:
                {
                    switch (ValidationType)
                    {
                        case 1:
                            txtQuestionAnswer.Attributes.Add("onblur", "isNumber(this)");
                            break;
                        case 2:
                            txtQuestionAnswer.Attributes.Add("onblur", "isAlpha(this)");
                            break;
                        case 3:
                            txtQuestionAnswer.Attributes.Add("onblur", "isAlphanum(this)");
                            break;
                        case 4:
                            txtQuestionAnswer.Attributes.Add("onblur", "isMoney(this)");
                            break;
                        case 5:
                            txtQuestionAnswer.Attributes.Add("onblur", "validateEmail(this)");
                            break;
                        case 6:
                            txtQuestionAnswer.Attributes.Add("onblur", "isPercentage(this)");
                            break;
                    }
                    DeactivateRoles();
                    ActivateRole(row_free_text);
                    break;
                }
            case 9:
                {
                    var options = IncidentTrackingManager.GetMatrixDimentions(MatrixDimensionId);

                    if (options != null)
                    {
                        if (options.Tables.Count > 0)
                        {
                            if (options.Tables[0].Rows.Count > 0)
                            {
                                DeactivateRoles();
                                ActivateRole(row_matrix);
                                matrix.Bind(options);
                            }
                        }
                    }
                    break;
                }
            case 10:
                {
                    DeactivateRoles();
                    ActivateRole(row_comment);
                    break;
                }
            case 11:
                {
                    DeactivateRoles();
                    ActivateRole(row_date);
                    if(QuesDisplay.Equals("Date Received"))
                    {
                        txtDate.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
                    }
                        
                    break;
                }
            case 13:
                {
                    var options = LookUpManager.ReadActiveLookupDataItems(LookupDataId);

                    if (options != null)
                    {
                        if (options.Count > 0)
                        {
                            DeactivateRoles();
                            ActivateRole(row_dropdown_choice);
                            ddlQuestionAnswers.Bind(options, "Description", "LookupItemId");
                        }
                    }

                    break;
                }
            case 14:
                {
                    DeactivateRoles();
                    ActivateRole(row_lists);
                    LoadItems();
                    break;
                }

            case 16:
                {
                    var options = IncidentTrackingManager.GetQuestionTypeAnswers(FieldType, FieldId, 0);
                    if (options != null)
                    {
                        if (options.HasRows)
                        {
                            DeactivateRoles();
                            ActivateRole(row_scale);

                            rbtnQuestionAnswers.Bind(options, "OptionDescription", "MultichoiceOptionId");
                            rbtnQuestionAnswers.Items.RemoveAt(0);
                        }
                    }
                    break;
                }
            case 17:
                {
                    var options = IncidentTrackingManager.GetQuestionTypeAnswers(FieldType, FieldId, 0);
                    if (options != null)
                    {
                        if (options.HasRows)
                        {
                            DeactivateRoles();
                            ActivateRole(row_dropdown_choice);
                            ddlQuestionAnswers.Bind(options, "OptionDescription", "MultichoiceOptionId");
                        }
                    }
                    break;
                }
            case 18:
                {
                    var options = IncidentTrackingManager.GetQuestionTypeAnswers(FieldType, FieldId, 0);
                    if (options != null)
                    {
                        if (options.HasRows)
                        {
                            DeactivateRoles();
                            ActivateRole(row_check_box_list_other);
                            CheckBoxListWithOther1.DataSource = options;
                            CheckBoxListWithOther1.DataTextField = "OptionDescription";
                            CheckBoxListWithOther1.DataValueField = "MultichoiceOptionId";
                            CheckBoxListWithOther1.Bind();
                        }
                    }
                    break;
                }
            case 19:
                {
                    var options = IncidentTrackingManager.GetQuestionTypeAnswers(FieldType, FieldId, 0);
                    if (options != null)
                    {
                        if (options.HasRows)
                        {
                            DeactivateRoles();
                            ActivateRole(row_radio_button_list_other);
                            RadioButtonListWithOther1.DataSource = options;
                            RadioButtonListWithOther1.DataTextField = "OptionDescription";
                            RadioButtonListWithOther1.DataValueField = "MultichoiceOptionId";
                            RadioButtonListWithOther1.Bind();
                        }
                    }
                    break;
                }
            case 21:
                {
                    break;
                }
            case 20:
                {
                    HierarchicalLookup.LookupDataId = this.HierarchyLookupDataId;
                    HierarchicalLookup.Bind();
                    DeactivateRoles();
                    ActivateRole(row_hierarchy_lookup);
                    break;
                }
            case 22:
                {
                    DeactivateRoles();
                    ActivateRole(row_AD_user);
                  var fieldDetails = IncidentTrackingManager.GetFieldById(FieldId.ToString());

                    if (!string.IsNullOrEmpty(fieldDetails.DefaultCCPersonSID))
                    {
                        var adUser = SarsUser.GetADUser(fieldDetails.DefaultCCPersonSID);
                        if (adUser != null)
                        {
                            UserSelector1.SelectedAdUserDetails = new SelectedUserDetails
                            {
                                SID = adUser.SID,
                                FoundUserName = string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                                FullName = adUser.FullName
                            };
                            if (fieldDetails.ShowOnSearch == false)
                            {
                                UserSelector1.Disable();
                            }
                        }
                    }
                    if(Display.Equals("Action Person"))
                    {
                        //HtmlTextWriter writer = new HtmlTextWriter();
                        //writer.AddAttribute("data-spy", "affix");
                        //UserSelector1.Add("class","ActionPerson");
                    }

                    break;
                }

        }
    }

    private void DeactivateRoles()
    {
        row_scale.Visible = false;
        row_radio_button_list_other.Visible = false;
        row_ddl_other.Visible = false;
        row_free_text.Visible = false;
        row_multi_choice.Visible = false;
        row_matrix.Visible = false;
        row_comment.Visible = false;
        row_date.Visible = false;
        row_lists.Visible = false;
        row_check_box_list_other.Visible = false;
        row_radio_button_list_other.Visible = false;
        row_hierarchy_lookup.Visible = false;
        row_AD_user.Visible = false;
    }

    private void ActivateRole(Control row)
    {
        row.Visible = true;
    }

    private object GetSelectedAnswer()
    {
        switch (FieldType)
        {
            case 1:
            case 12:
                {
                    return ScaleTypeId != 5 && ScaleTypeId != 0
                               ? (rbtnQuestionAnswers.SelectedIndex == -1
                                      ? null
                                      : rbtnQuestionAnswers.SelectedValue)
                               : (ddlQuestionAnswers.SelectedIndex == 0 ? null : ddlQuestionAnswers.SelectedValue);
                }
            case 5:
                {
                    var choices = (from ListItem item in chkbQuestionAnswers.Items
                                   where item.Selected
                                   select Convert.ToInt32(item.Value)).ToList();
                    return choices.Count == 0 ? null : choices;
                }
            case 6:
                {
                    return string.IsNullOrEmpty(txtQuestionAnswer.Text) ? null : txtQuestionAnswer.Text;
                }
            case 9:
                {
                    return matrix.SelectedValues;
                }
            case 10:
                {
                    return string.IsNullOrEmpty(txtComments.Text) ? null : txtComments.Text;
                }
            case 11:
                {
                    return string.IsNullOrEmpty(txtDate.Text) ? null : txtDate.Text;
                }
            case 13:
                {
                    return ddlQuestionAnswers.SelectedIndex == 0 ? null : ddlQuestionAnswers.SelectedValue;
                }
            case 14:
                {
                    return gvListItems.Rows.Count == 0
                               ? null
                               : gvListItems.Rows.Count.ToString(CultureInfo.InvariantCulture);
                }
            case 16:
                {
                    return rbtnQuestionAnswers.SelectedIndex == -1 ? null : rbtnQuestionAnswers.SelectedValue;
                }
            case 17:
                {
                    return ddlQuestionAnswers.SelectedIndex == -1 || ddlQuestionAnswers.SelectedIndex == 0
                               ? null
                               : ddlQuestionAnswers.SelectedValue;
                }
            case 18:
                {
                    if (CheckBoxListWithOther1.HasValue)
                    {
                        return CheckBoxListWithOther1.SelectedOptions;
                    }
                    break;
                }
            case 19:
                {
                    if (RadioButtonListWithOther1.HasValue)
                    {
                        return RadioButtonListWithOther1.SelectedOption;
                    }
                    break;
                }
            case 20:
                {
                    if (HierarchicalLookup.IsAnswered)
                    {
                        return HierarchicalLookup.SelectedHierarchy;
                    }
                    break;
                }
            case 22:
                {
                    return UserSelector1.SelectedAdUserDetails;
                }
        }
        return null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtListOption.Attributes["onkeyup"] = string.Format("findAdUser(this.id,{0})", userContainer.ClientID);

    }

    private void AddCCPersonOnLoad(string ccPerson)
    {
        if (!ProcessID.Equals(135))
            return;
        bool added = false;
        foreach (GridViewRow row in gvListItems.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                var itmValue = row.Cells[0].Text;
                if (!string.IsNullOrEmpty(itmValue))
                {
                    if (itmValue.Trim().ToUpper().Equals(ccPerson.Trim().ToUpper()))
                    {
                        txtListOption.Focus();                       
                        added = true;
                        break;
                    }
                }
            }
        }
        if (added)
            return;

        IncidentTrackingManager.SaveListItem(ccPerson, ProcessID, FieldId, IncidentID);
        txtListOption.Text = string.Empty;
        LoadItems();
    }

    protected void btnAddListOption_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtListOption.Text))
        {
            MessageBox.Show("Please type in your item.");
            txtListOption.Focus();
            return;
        }
        var added = false;
        foreach (GridViewRow row in gvListItems.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                var itmValue = row.Cells[0].Text;
                if (!string.IsNullOrEmpty(itmValue))
                {
                    if (itmValue.Trim().ToUpper().Equals(txtListOption.Text.Trim().ToUpper()))
                    {
                        txtListOption.Focus();
                        MessageBox.Show("You have already added this item.");
                        added = true;
                        break;
                    }
                }
            }
        }
        if (added)
            return;

        IncidentTrackingManager.SaveListItem(txtListOption.Text, ProcessID, FieldId, IncidentID);
        txtListOption.Text = string.Empty;
        LoadItems();
        txtListOption.Focus();
    }

    public void ResendSelectedItem(object sender, EventArgs e)
    {
        var ddlQuestionAnswers2 = sender as DropDownList;
        if (ddlQuestionAnswers2 != null && ddlQuestionAnswers2.SelectedIndex > 0)
        {
            IntelliOption = Convert.ToDecimal(ddlQuestionAnswers2.SelectedValue);
        }
        else
        {
            IntelliOption = null;
        }
    }

    protected void ddlQuestionAnswers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlQuestionAnswers.SelectedIndex > 0)
        {
            ddlQuestionAnswers.BackColor = Color.White;
            IntelliOption = Convert.ToDecimal(ddlQuestionAnswers.SelectedValue);        
        }
        else
        {
            IntelliOption = null;
        }
    }

    public Decimal? IntelliOption
    {
        get
        {
            if (FieldType == 17)
            {
                return ddlQuestionAnswers.SelectedIndex > 0
                           ? (int?) int.Parse(ddlQuestionAnswers.SelectedItem.Value)
                           : null;

            }
            if (FieldType == 16)
            {
                return rbtnQuestionAnswers.SelectedIndex > 0
                           ? (int?) int.Parse(rbtnQuestionAnswers.SelectedItem.Value)
                           : null;
            }
            return null;
        }
        set
        {
            try
            {
                if (OptionChanged != null)
                {
                    ViewState["iOption"] = value;
                    var e = new IntelliQuestionArgs(value, this.FieldId, IsParent, this.CurrentRowIndex);
                    if (FieldType == 17)
                    {
                        OptionChanged(ddlQuestionAnswers, e);
                    }
                    if (FieldType == 16)
                    {
                        OptionChanged(rbtnQuestionAnswers, e);
                    }
                    if (FieldType == 5)
                    {
                        OptionChanged(chkbQuestionAnswers, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    protected void rbtnQuestionAnswers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnQuestionAnswers.SelectedIndex > 0)
        {
            IntelliOption = Convert.ToDecimal(rbtnQuestionAnswers.SelectedValue);
        }
        else
        {
            IntelliOption = null;
        }
    }

    protected void chkbQuestionAnswers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkbQuestionAnswers.SelectedIndex > -1)
        {
            foreach (ListItem listItem in chkbQuestionAnswers.Items)
            {
                if (listItem.Selected)
                {
                    IntelliOption = Convert.ToDecimal(listItem.Value);
                    break;
                }
            }
        }
        else
        {
            IntelliOption = null;
        }
    }

    public void RunCheckBoxEvents()
    {
        chkbQuestionAnswers_SelectedIndexChanged(this.chkbQuestionAnswers, EventArgs.Empty);
    }

    public void RunDropDownEvents()
    {
        ddlQuestionAnswers_SelectedIndexChanged(this.ddlQuestionAnswers, EventArgs.Empty);
    }

    public void RunRadioButtonEvents()
    {
        rbtnQuestionAnswers_SelectedIndexChanged(this.rbtnQuestionAnswers, EventArgs.Empty);
    }
}