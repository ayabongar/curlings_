using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Controls;
using Sars.Systems.Extensions;
public partial class SystemAdmins_ManageRoles : Page
{
    private const string DOMAIN = "SARSGOV\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnAssignRoles.Visible = Request["action"] == "1";
        this.btnRemoveUser.Visible = Request["action"] == "2";
       
        if(!IsPostBack)
        {
           
        }
    }
    protected void AddUserToRole(object sender, EventArgs e)
    {
        if(this.chklRoles.SelectedIndex == -1)
        {
            MessageBox.Show("Please select atleast one role");
            return;
        }
        var user = txtUserSID.Text;
        foreach (ListItem itm in chklRoles.Items)
        {
            if (itm.Selected)
            {
                if (!Roles.IsUserInRole(user, itm.Text))
                {
                    Roles.AddUserToRole(user, itm.Text);
                }
            }
        }
       
        MessageBox.Show("User added to role(s)");
        this.btnAssignRoles.Enabled = false;
        return;
    }
    protected void Search(object sender, EventArgs e)
    {
        
        this.chklRoles.Items.Clear();
        if (string.IsNullOrEmpty(this.txtUserSID.Text))
        {
            MessageBox.Show("User SID is reguired");
            return;
        }
        var user = txtUserSID.Text;
        var roles = Roles.GetAllRoles();
        if (Request["action"] == "2")
        {
            var userRoles = Roles.GetRolesForUser(user);

            foreach (string role in userRoles)
            {
                var itm = new ListItem(role, role);
                this.chklRoles.Items.Add(itm);
            }
        }
        else
        {

            foreach (var role in roles)
            {
                var itm = new ListItem(role, role);
                this.chklRoles.Items.Add(itm);
            }

            foreach (ListItem listItem in chklRoles.Items)
            {
                if (Roles.IsUserInRole(user, listItem.Text))
                {
                    listItem.Enabled = false;
                }
            }
        }
        this.btnAssignRoles.Enabled = true;
        this.btnRemoveUser.Enabled = true;
    }
    protected void RemoveUserFromRole(object sender, EventArgs e)
    {
        if (this.chklRoles.SelectedIndex == -1)
        {
            MessageBox.Show("Please select atleast one role");
            return;
        }
        foreach (ListItem item in chklRoles.Items)
        {
            if (item.Selected)
            {
                Roles.RemoveUserFromRole(this.txtUserSID.Text, item.Text);
            }
        }
        MessageBox.Show("User removed from role(s)");
        this.btnRemoveUser.Enabled = false;
        return;
    }
}