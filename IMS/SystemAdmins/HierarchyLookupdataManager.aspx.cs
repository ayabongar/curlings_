using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemAdmins_HierarchyLookupdataManager : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("~/admin/SelectNormalUserProcess.aspx");
        }
        if (!string.IsNullOrWhiteSpace(Request["lkplevel"]) && !string.IsNullOrWhiteSpace(Request["pid"]))
        {
            row_parent_lookup.Visible = true;
            row_parent_drop_downs.Visible = true;
            GetLookUpDataDetails();
            if (!IsPostBack)
            {
                var lookupItmes = LookUpManager.ReadHierarchyLookupDataItems(Convert.ToDecimal(Request["pid"]));
                ddlParentLookUps.Bind(lookupItmes, "Select Parent Option", "Description", "LookupItemId");

                var allLookUpDataItems = LookUpManager.ReadHierarchyLookups();
                var linkedItem = allLookUpDataItems.Find(lkp => lkp.ParentId == Convert.ToDecimal(Request["pid"]));
                if(linkedItem != null)
                {
                    txtLookupdataDescription.SetValue(linkedItem.Description);
                    txtLookupdataDescription.Enabled = false;
                    ViewState["pid"] = linkedItem.LookupDataId;
                }
            }
        }
    }

    private void GetLookUpDataDetails()
    {
        var lookups = LookUpManager.ReadHierarchyLookups();
        if(lookups == null)
            return;
        if(lookups.Count <0)
            return;
        var lookupdata = lookups.Find(lkp => lkp.LookupDataId == Convert.ToDecimal(Request["pid"]));
        if(lookupdata == null)
            return;

        lblParentLookUpDetails.SetValue(lookupdata.Description);
    }
    protected void btnAddOption_Click(object sender, EventArgs e)
    {
        if (txtLKPOption.Text.Trim() == string.Empty)
        {
            MessageBox.Show("You must enter an option.");
            return;
        }
        
        ListItem itm;
        if(ddlParentLookUps.Items.Count > 0 && !string.IsNullOrWhiteSpace(Request["pid"]))
        {
            //var selectedParent =
            //lbOptions.Items.Cast<ListItem>().ToList().Find(x => x.Value == ddlParentLookUps.SelectedValue);
            //if (selectedParent != null)
            //{
            //    MessageBox.Show(string.Format("The item [{0}] has already been assigned [{1}] as a parent.",
            //                                  selectedParent.Text, ddlParentLookUps.SelectedItem.Text));
            //    return;
            //}
            itm = new ListItem(txtLKPOption.Text, ddlParentLookUps.SelectedValue);
        }
        else
        {
            itm = new ListItem(txtLKPOption.Text);
        }
        if (lbOptions.Items.Contains(itm))
        {
            MessageBox.Show("You can not repeate the same answer you added.");
        }
        else
        {
            lbOptions.Items.Add(itm);
            txtLKPOption.Text = String.Empty;
            txtLKPOption.Focus();
        }
    }

    protected void btnRemove_Click(object sender, EventArgs e)
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
    protected void SubmitLookUps(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtLookupdataDescription.Text.Trim()))
        {
            MessageBox.Show("Please type in your look up name/description.");
            return;
        }
        if (lbOptions.Items.Count == 0)
        {
            MessageBox.Show("Please make sure you add atleast one lookup item.");
            return;
        }
        var parentId = string.IsNullOrWhiteSpace(Request["pid"]) ? null : Request["pid"];
        var id = ViewState["pid"] != null ? Convert.ToDecimal(ViewState["pid"]) : LookUpManager.AddHierarchyLookupData(0, txtLookupdataDescription.Text, true, parentId);
        if (id > 0)
        {
            var user = System.Web.Security.Membership.GetUser(Page.User.Identity.Name);
            if (user != null)
            {
                if (user.ProviderUserKey != null)
                    IncidentTrackingManager.SaveAuditTrail(user.ProviderUserKey.ToString(), 
                                                 string.Format(
                                                     ResourceString.GetResourceString("createdNewLookUp"),
                                                     txtLookupdataDescription.Text));
            }
            foreach (ListItem itm in lbOptions.Items)
            {
                 if (user != null)
                 {
                     if (user.ProviderUserKey != null)
                         IncidentTrackingManager.SaveAuditTrail(user.ProviderUserKey.ToString(), 
                                                      string.Format(
                                                          ResourceString.GetResourceString("addedLookUpOption"),
                                                          txtLKPOption.Text));
                 }
                 var itemParentId = row_parent_drop_downs.Visible && ddlParentLookUps.Items.Count > 0 ? itm.Value : null;
                LookUpManager.AddHierarchyLookupItem(0, itm.Text, true, id, itemParentId);
            }
        }
        btnSubmit.Enabled = false;
        btnAddOption.Enabled = false;
        btnRemove.Enabled = false;
        ddlParentLookUps.Enabled = false;
        if(ddlParentLookUps.Items.Count > 0)
        {
            ddlParentLookUps.SelectedIndex = 0;
        }
        txtLKPOption.Enabled = false;
        txtLookupdataDescription.Enabled = false;
        lbOptions.Enabled = false;
        MessageBox.Show("Lookup created successfully");
    }
}