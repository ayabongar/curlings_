using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_UserProfile : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUser();
        }
    }

    private void LoadUser()
    {
        if (SarsUser.CurrentUser != null)
        {
            var user = SarsUser.GetADUser(SarsUser.CurrentUser.SID);
            if(user != null)
            {
                string role = new SessionObjects().GetUserRole();
                //ViewState["thisUser"] = thisUser;
                txtSID.SetValue(user.SID);
                txtFirstName.SetValue(user.FullName);
                txtEmailAddress.SetValue(user.Mail);
                txtTelephone.SetValue(user.Telephone);
                tblUser.Visible = true;
            }
           
            //ddlRoles.Text = role;
            var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
            if (User != null)
            {
                var userRole = IncidentTrackingManager.GetNTQR_UserRoleById(User[0].ID);
                if (userRole != null)
                {
                    for (int i = 0; i < userRole.Count; i++)
                    {
                        switch (userRole[i].RoleId)
                        {
                            case 1:
                              
                                ddlRoles.Text += "Compiler | ";
                                break;
                            case 2:
                                
                                ddlRoles.Text += "Key Result Owner | ";
                                break;
                            case 3:
                                
                                ddlRoles.Text += "Anchor | ";
                                break;
                            case 4:
                               
                                ddlRoles.Text += "EDM EBR Key Result | ";
                                break;
                            default:
                                break;
                        }
                    }                    
                }
            }
        }
    }
}