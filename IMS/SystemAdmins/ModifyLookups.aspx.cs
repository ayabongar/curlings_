using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemAdmins_ModifyLookups : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("~/admin/SelectNormalUserProcess.aspx");
        }
        LoadLookups();
    }
   
    private void LoadLookups()
    {
        var lookups = LookUpManager.ReadLookups();
        Session["lookups"] = lookups;
        gvLookpus.Bind(lookups);
    }
    protected void Modify()
    {
        row_modify.Visible = true;
        row_addItems.Visible = false;
        row_view_items.Visible = false;
        row_modify_item.Visible = false;

        var row = gvLookpus.SelectedRow;
        if (row != null)
        {

            txtLookupdataDescription.Text = row.Cells[0].Text;
            chkIsActive.Checked = ((CheckBox)row.Cells[1].Controls[0]).Checked;
            var lblLookupDataId = row.FindControl("lblLookupDataId") as Label;
            if (lblLookupDataId != null)
            {
                ViewState["LookupDataId"] = lblLookupDataId.Text;
            }
        }
    }

    protected void AddItems()
    {
        row_modify.Visible = false;
        row_view_items.Visible = false;
        row_addItems.Visible = true;
        var row = gvLookpus.SelectedRow;
        if (row != null)
        {
            var lblLookupDataId = row.FindControl("lblLookupDataId") as Label;
            if (lblLookupDataId != null)
            {
                ViewState["LookupDataId"] = lblLookupDataId.Text;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var lookupDataId = Convert.ToDecimal(ViewState["LookupDataId"]);
        LookUpManager.AddLookupData(lookupDataId, txtLookupdataDescription.Text, chkIsActive.Checked);
        MessageBox.Show("Lookup saves succesfully.");
        LoadLookups();
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
            return;
        e.Row.Attributes.Add("onmouseover", ";style.cursor='pointer'");
        e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (Session["lookups"] != null)
        {
            gvLookpus.PageIndex = e.NewPageIndex;
            gvLookpus.DataSource = Session["lookups"];
            gvLookpus.DataBind();
        }
    }
    protected void Remove(object sender, EventArgs e)
    {
        var removeItms = lbOptions.Items.Cast<ListItem>().Where(itm => itm.Selected).ToList();
        if (lbOptions.Items.Count == 0)
        {
            MessageBox.Show("There are no items to remove");
            return;
        }
        if (removeItms.Count == 0)
        {
            MessageBox.Show("Please click on an item to remove.");
            return;
        }
        foreach (var itm in removeItms)
        {
            lbOptions.Items.Remove(itm);
        }
    }
    protected void AddOption(object sender, EventArgs e)
    {

        if (txtLKPOption.Text == string.Empty)
        {
            MessageBox.Show("You must enter an option.");
            return;
        }
        var itm = new ListItem(txtLKPOption.Text);
        if (lbOptions.Items.Contains(itm))
        {
            MessageBox.Show("You can not repeate the same answer you added.");
        }
        else
        {
            lbOptions.Items.Add(itm);
            txtLKPOption.Text = String.Empty;
        }
    }
    protected void AddItem(object sender, EventArgs e)
    {
        if (lbOptions.Items.Count == 0)
        {
            MessageBox.Show("Please make sure you add atleast one lookup item.");
            return;
        }
        var lookupDataId = Convert.ToDecimal(ViewState["LookupDataId"]);
        foreach (ListItem itm in lbOptions.Items)
        {
            LookUpManager.AddLookupItem(0, itm.Text, true, lookupDataId);
        }
        MessageBox.Show("Lookup created successfully");
        txtLKPOption.Clear();
        lbOptions.Items.Clear();
    }

    protected void ToolBarClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        if (gvLookpus.SelectedIndex == -1)
        {
            MessageBox.Show("Please click on an item to select it first.");
            return;                
        }
        switch (e.CommandName)
        {
            case "Modify":
                {
                    Modify();
                    break;
                }
            case "AddLookupItem":
                {
                    AddItems();
                    break;
                }
            case "ViewLookupItems":
                LoadItems();
                break;
                
        }
    }

    private void LoadItems()
    {
        row_addItems.Visible = false;
        row_modify.Visible = false;
        row_modify_item.Visible = false;
        row_view_items.Visible = true;
        var row = gvLookpus.SelectedRow;
        if (row != null)
        {
            var lblLookupDataId = row.FindControl("lblLookupDataId") as Label;
            if (lblLookupDataId != null)
            {
                var lookupDataId = Convert.ToDecimal(lblLookupDataId.Text);
                var lookupItmes = LookUpManager.ReadLookupDataItems(lookupDataId);
                gvItems.DataSource = lookupItmes;
                gvItems.DataBind();
            }
        }
    }
    protected void ItemsRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
            return;
        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
    }
    protected void ItemsSelectedIndexChanged(object sender, EventArgs e)
    {
        if (gvItems.SelectedIndex != -1)
        {
            var row = gvItems.SelectedRow;
            txtLookupItemDescription.Text = row.Cells[0].Text;
            chkIsActiveItem.Checked = ((CheckBox) row.Cells[1].Controls[0]).Checked;

            Toolbar1.Items[3].Visible = true;
            Toolbar1.Items[3].ToolTip = "Modify the select Lookup Item.";
            row_modify_item.Visible = true;
        }
    }
    protected void SelectedIndexChanged(object sender, EventArgs e)
    {
        Toolbar1.Items[3].Visible = false;
        row_modify.Visible = false;
        row_addItems.Visible = false;
        row_modify_item.Visible = false;
        row_view_items.Visible = false;

        txtLookupdataDescription.Clear();
        txtLKPOption.Clear();
        txtLookupItemDescription.Clear();

    }
    protected void Modify(object sender, EventArgs e)
    {
        var row = gvItems.SelectedRow;
        if (row != null)
        {
            var lblLookupItemId = row.FindControl("lblLookupItemId") as Label;
            if (lblLookupItemId != null)
            {
                var saved = LookUpManager.AddLookupItem(Convert.ToDecimal(lblLookupItemId.Text),
                                                        txtLookupItemDescription.Text, chkIsActiveItem.Checked, 0);

                LoadItems();
                MessageBox.Show("Lookup Item saved successfully!");
            }
        }
    }
}