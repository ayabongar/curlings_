using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemAdmins_LookupdataManager : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("~/admin/SelectNormalUserProcess.aspx");
        }
    }
    protected void btnAddOption_Click(object sender, EventArgs e)
    {
        if (txtLKPOption.Text.Trim() == string.Empty)
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
            txtLKPOption.Focus();
            lbOptions.Items.Add(itm);
            txtLKPOption.Text = String.Empty;
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
    protected void btnSubmit_Click(object sender, EventArgs e)
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
        var id = LookUpManager.AddLookupData(0, txtLookupdataDescription.Text, true);
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
                LookUpManager.AddLookupItem(0, itm.Text, true, id);
            }
        }
        btnSubmit.Enabled = false;
        btnAddOption.Enabled = false;
        btnRemove.Enabled = false;
        MessageBox.Show("Lookup created successfully");
    }
}