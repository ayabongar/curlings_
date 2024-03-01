using System;
using System.Web.UI.WebControls;

public partial class SurveyWizard_HierarchicalLookup : System.Web.UI.UserControl
{
   
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int NumAnswers
    {
        get
        {
            if (row_level1.Visible && ddlLevel1.SelectedIndex > 0)
            {
                ViewState["NumA"] = 1;
            }
            if (row_level2.Visible && ddlLevel2.SelectedIndex > 0)
            {
                ViewState["NumA"] = ViewState["NumA"] != null ? (Convert.ToInt32(ViewState["NumA"])) + 1 : 1;
            }
            if (row_level3.Visible && ddlLevel3.SelectedIndex > 0)
            {
                ViewState["NumA"] = ViewState["NumA"] != null ? (Convert.ToInt32(ViewState["NumA"])) + 1 : 1;
            }
            return ViewState["NumA"] == null ? 0 : Convert.ToInt32(ViewState["NumA"]);
        }
    }

    public bool IsAnswered
    {
        get { return SetValidationColours(); }
    }
    public Boolean SetValidationColours()
    {
        if(row_level1.Visible && ddlLevel1.SelectedIndex <= 0)
        {
            //ddlLevel1.BackColor = System.Drawing.Color.Red;
            ddlLevel1.BorderColor = System.Drawing.Color.Red;
            ddlLevel1.BorderStyle = BorderStyle.Dashed;
            ddlLevel1.BorderWidth = 1;
            return false;
        }
        //ddlLevel1.BackColor = System.Drawing.Color.Empty;
        ddlLevel1.BorderColor = System.Drawing.Color.Empty;
        ddlLevel1.BorderStyle = BorderStyle.NotSet;
        ddlLevel1.BorderWidth = 1;

        if(row_level2.Visible && ddlLevel2.SelectedIndex <=0)
        {
            //ddlLevel2.BackColor = System.Drawing.Color.Red;
            ddlLevel2.BorderColor = System.Drawing.Color.Red;
            ddlLevel2.BorderStyle = BorderStyle.Dashed;
            ddlLevel2.BorderWidth = 1;
            return false;
        }
        //ddlLevel2.BackColor = System.Drawing.Color.Empty;
        ddlLevel2.BorderColor = System.Drawing.Color.Empty;
        ddlLevel2.BorderStyle = BorderStyle.NotSet;
        ddlLevel2.BorderWidth = 1;
        if (row_level3.Visible && ddlLevel3.SelectedIndex <= 0)
        {
            ddlLevel3.BorderColor = System.Drawing.Color.Red;
            ddlLevel3.BorderStyle = BorderStyle.Dashed;
            ddlLevel3.BorderWidth = 1;
            //ddlLevel3.BackColor = System.Drawing.Color.Red;
            return false;
        }
        ddlLevel3.BorderColor = System.Drawing.Color.Empty;
        ddlLevel3.BorderStyle = BorderStyle.NotSet;
        ddlLevel3.BorderWidth = 1;
        //ddlLevel3.BackColor = System.Drawing.Color.Empty;
        return true;
    }

    public decimal LookupDataId
    {
        get
        {
            if(ViewState["hlkp"] != null)
            {
                return Convert.ToDecimal(ViewState["hlkp"]);
            }
            return 0;
        }
        set { ViewState["hlkp"] = value; }
    }
    public SelectedHierarchicalDetails SelectedHierarchy
    {
        get
        {
            var selected = new SelectedHierarchicalDetails();
            if(row_level1.Visible && ddlLevel1.SelectedIndex>0)
            {
                selected.FirstLevel = Convert.ToDecimal(ddlLevel1.SelectedValue) ;
            }
            if(row_level2.Visible && ddlLevel2.SelectedIndex >0)
            {
                selected.SecondLevel = Convert.ToDecimal(ddlLevel2.SelectedValue);
            }
            if(row_level3.Visible && ddlLevel3.SelectedIndex > 0)
            {
                selected.ThirdLevel = Convert.ToDecimal(ddlLevel3.SelectedValue);
            }
            return selected;
        }
    }
    public void PopulateData(SelectedHierarchicalDetails selected)
    {
        if (ddlLevel1.Items.Count > 1)
        {
            ddlLevel1.SelectItemByValue(selected.FirstLevel.ToString());
            row_level1.Visible = true;
            ddlLevel1_SelectedIndexChanged(ddlLevel1, EventArgs.Empty);
        }
        if (ddlLevel2.Items.Count > 1)
        {
            if (selected.SecondLevel != null)
            {
                ddlLevel2.SelectItemByValue(selected.SecondLevel.ToString()); 
                row_level2.Visible = true;
                ddlLevel2_SelectedIndexChanged(ddlLevel2, EventArgs.Empty);
            }
        }
        if (ddlLevel3.Items.Count > 1)
        {
            if (selected.ThirdLevel != null)
            {
                ddlLevel3.SelectItemByValue(selected.ThirdLevel.ToString());
                row_level3.Visible = true;
            }
        }
    }

    public void Bind()
    {
        ddlLevel1.Bind(LookUpManager.ReadHierarchyLookupDataItems(LookupDataId), "Description", "LookupItemId");
    }
    protected void ddlLevel1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlLevel1.SelectedIndex > 0)
        {
            var children = LookUpManager.ReadHierarchyLookupDataItemsById(Convert.ToDecimal(ddlLevel1.SelectedValue));
            if (children != null && children.Count > 0)
            {
                ddlLevel2.Bind(children, "Description", "LookupItemId");
                row_level2.Visible = true;
                ddlLevel3.Items.Clear();
                row_level3.Visible = false;
            }
        }
        else
        {
            ddlLevel2.Items.Clear();
            ddlLevel3.Items.Clear();
            row_level2.Visible = false;
            row_level3.Visible = false;
        }
    }
    protected void ddlLevel2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLevel2.SelectedIndex > 0)
        {
            var children = LookUpManager.ReadHierarchyLookupDataItemsById(Convert.ToDecimal(ddlLevel2.SelectedValue));
            if (children != null && children.Count > 0) 
            {
                ddlLevel3.Bind(children, "Description", "LookupItemId");
                row_level3.Visible = true;
            }
        }
        else
        {
            ddlLevel3.Items.Clear();
            row_level3.Visible = false;
        }
    }
}