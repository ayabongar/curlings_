using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Sars.Systems.Logger;
using Sars.Systems.Utilities;
using Sars.Systems.Extensions;
public partial class Logging_AddContacts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Sars.Systems.Logger.Contacts oContacts = new Sars.Systems.Logger.Contacts();
            //var contacts = oContacts.ReadContacts();
            //if (contacts.Count > 0)
            //{
            //    contacts.ForEach(contact => lbDetails.Items.Add(new ListItem(contact.FullName, contact.EmailAddress)));
            //}
        }
    }
    protected void btnAddContact_Click(object sender, EventArgs e)
    {
        lblError.SetValue("");
        if (string.IsNullOrEmpty(txtName.Text))
        {
            lblError.SetValue("Name is required");
            return;
        }
        if (string.IsNullOrEmpty(txtEmailAddress.Text.Trim()))
        {
            lblError.SetValue("Email address is required");
            return;
        }
        if (!txtEmailAddress.Text.IsValid(StringValidationType.Email))
        {
            lblError.SetValue("Email Address is not a valid internet email address");
            return;
        }

        //var item = new ListItem(txtName.Text, txtEmailAddress.Text);
        //if (!lbDetails.Items.Contains(item))
        //{
        //    lbDetails.Items.Add(item);
        //    Sars.Systems.Logger.Contacts oContacts = new Sars.Systems.Logger.Contacts();
        //    oContacts.AddContact(item.Text, item.Value);
        //    lblError.SetValue("Contact added.");
        //    txtEmailAddress.Clear();
        //    txtName.Clear();
        //}
        //else
        //{
        //    lblError.SetValue("Duplicate contacts are not allowed.");
        //}
    }
    protected void RemoveContact(object sender, EventArgs e)
    {
        //lblError.SetValue("");
        //if (lbDetails.SelectedIndex != -1)
        //{
        //    var newContacts = new List<Contacts>();
        //    lbDetails.Items.RemoveAt(lbDetails.SelectedIndex);
        //    lbDetails.Items.Cast<ListItem>().ToList().ForEach(itm => newContacts.Add(new Contacts(itm.Text, itm.Value)));


        //    if (newContacts.Count > 0)
        //    {
        //        Sars.Systems.Logger.Contacts oContacts = new Sars.Systems.lo.Contacts();
        //        oContacts.AddAll(newContacts);
        //    }           
        //    lblError.SetValue("Contact removed.");
        //}
        //else
        //{
        //    lblError.SetValue("Please select a name to remove.");
        //}
    }
}

