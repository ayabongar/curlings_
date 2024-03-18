using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class SystemAdmins_PowerUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("~/admin/SelectNormalUserProcess.aspx");
        }
        if(!IsPostBack)
        {
            gvUsers.Bind(IncidentTrackingManager.GetPowerUsers());
        }
    }
    protected void AddUser(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty( UserSelector1.SelectedAdUserDetails.SID))
        {
            UserSelector1.Focus();
            MessageBox.Show("Please select a user");
            return;
        }
        var sid = UserSelector1.SelectedAdUserDetails.SID;
        SarsUser.AddUser(SarsUser.GetADUser(sid));
        var powerUsers = IncidentTrackingManager.GetPowerUsers();
        if(powerUsers !=null && powerUsers.Any())
        {
            var powerUser = powerUsers.Find(pu => pu.SID.Equals(sid, StringComparison.CurrentCultureIgnoreCase));
            if(powerUser != null)
            {
                UserSelector1.Focus();
                MessageBox.Show(String.Format("{0} is already in the power user group.", powerUser.FullName));
                return;
            }
        }

        var saved = IncidentTrackingManager.SavePowerUser(sid);
        if(saved > 0)
        {
            gvUsers.Bind(IncidentTrackingManager.GetPowerUsers());
            UserSelector1.Clear();
            MessageBox.Show("Power user added successfully");
        }
    }

    protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            var sid = e.CommandArgument;
            IncidentTrackingManager.RemovePowerUser(sid.ToString());
            gvUsers.Bind(IncidentTrackingManager.GetPowerUsers());
        }
    }
}