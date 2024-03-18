using Sars.Systems.Data;
using Sars.Systems.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NTQR_UserProfile :IncidentTrackingPage
{
    
    protected IncidentProcess CurrentProcessDetails;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CurrentProcessDetails = CurrentProcess;
            SearchUsers();
        }
    }

    private void SearchUsers()
    {
        bool userIsOwner = false;
        if (string.IsNullOrEmpty(txtTeamSearch.Text))
        {
            foreach (var processOwner in CurrentProcess.Owners)
            {
                if (processOwner.OwnerSID.ToLower().Equals(SarsUser.SID.ToLower()))
                {
                    gvUsers.Bind(IncidentTrackingManager.GetNTQR_UserRole());
                    userIsOwner = true;
                    break;
                }               
            }            
            if (!userIsOwner)
            {
                gvUsers.Bind(IncidentTrackingManager.GetNTQR_UserRole().FindAll(u => u.UserCode.ToLower().Equals(SarsUser.SID.ToLower())));
            }
        }
        else
        {
           
            foreach (var processOwner in CurrentProcess.Owners)
            {
                if (processOwner.OwnerSID.ToLower().Equals(SarsUser.SID.ToLower()))
                {
                    gvUsers.Bind(IncidentTrackingManager.GetNTQR_UserRole().FindAll(u => u.UserCode.ToLower().Equals(txtTeamSearch.Text.ToLower())));
                    userIsOwner = true;
                    break;
                }
            }
            if (!userIsOwner)
            {
                gvUsers.Bind(IncidentTrackingManager.GetNTQR_UserRole().FindAll(u => u.UserCode.ToLower().Equals(SarsUser.SID.ToLower())));
            }
        }            

    }
    public string GetImage(object img)
    {
        if (img != null)
        {
            return  "data:image/png;base64," + Convert.ToBase64String((byte[])img);           
        }
        return null;
    }

    public string GetUserUnits(object id)
    {
        string units = string.Empty;
        if (id == null) return null;

        StringBuilder sb = new StringBuilder(); 
        sb.Append("SELECT [fk_UserId],u.Name FROM NTQ_User_UnitsMappings m ");
        sb.Append("inner join[dbo].[NTQ_User_Units] u ");
        sb.Append("on m.fk_User_UnitId = u.Id ");
        sb.Append("where fk_UserId = " + int.Parse(id.ToString()));

        using (var data = new RecordSet(sb.ToString(), QueryType.TransectSQL, null, db.ConnectionString))
        {
            if (data.HasRows)
            {
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    units += " | " +data.Tables[0].Rows[i]["Name"].ToString()  ;
                }                 
            }           
        }
        return units;
    }
    
    protected void RemoveUser(object sender, EventArgs e)
    {
        var btn = sender as LinkButton;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvUsers.SelectRow(row.RowIndex);
                if (gvUsers.SelectedDataKey != null)
                {
                    var userProcessId = Convert.ToInt32(gvUsers.SelectedDataKey["UserId"]);
                    var RoleId = Convert.ToInt32(gvUsers.SelectedDataKey["ID"]);
                    if (userProcessId != 0)
                    {
                        IncidentTrackingManager.GetNTQR_RemoveUserFromRole(userProcessId, RoleId);
                        MessageBox.Show("User Removed Successfully");
                        SearchUsers();
                    }
                }
            }
        }
    }

    protected void ModifyUser(object sender, EventArgs e)
    {
        var btn = sender as LinkButton;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvUsers.SelectRow(row.RowIndex);
                if (gvUsers.SelectedDataKey != null)
                {
                    var userProcessId = Convert.ToInt32(gvUsers.SelectedDataKey["UserId"]);
                    var RoleId = Convert.ToInt32(gvUsers.SelectedDataKey["ID"]);
                    if (userProcessId != 0)
                    {
                        var processId = System.Configuration.ConfigurationManager.AppSettings["ntqProcess"];
                        Response.Redirect("AddUser.aspx?" + string.Format("procId={0}&userId={1}" ,processId, userProcessId));
                    }
                }
            }
        }
    }

    protected void Save(object sender, EventArgs e)
    {
        string sid = UserSelector1.SelectedAdUserDetails.SID;
        SarsUser.SaveUser(sid);
        var User = IncidentTrackingManager.GetNTQR_UserBySID(sid);
        int userId = 0;
        if (User != null)
        {
            userId = User[0].ID;
        }
       
        var user = new NTRQ_User() {
            UserCode = sid,
            UserFullName = SarsUser.SearchADUsersBySID(sid)[0].FullName,
            CreatedBy = userId,
            IsActive = true,
            Signature = UploadFile(fileUpload),
            CreatedDate = DateTime.Now,
            RoleId = int.Parse(ddlRole.SelectedValue),
            UserId = userId,
            

        };

        var recordsAffected = IncidentTrackingManager.NQT_AddUserToRole(user);
        if (recordsAffected > 0)
        {
            var processId = System.Configuration.ConfigurationManager.AppSettings["ntqProcess"];
            var result = IncidentTrackingManager.AddUserToAProcess(sid, processId, "1");
            var user1 = string.Format("{0}\\{1}", Sars.Systems.Data.SARSDataSettings.Settings.DomainName,
                   sid);
            if (!string.IsNullOrEmpty(user1))
            {
                try
                {
                    var getUserRole = Roles.GetUserRoles(user1);
                    if(getUserRole.Count == 0)
                    {
                        Roles.AddUserToRole("Process Administrator", user1);
                    }   
                    
                }
                catch (Exception ex)
                {

                }
            }
            UserSelector1.Clear();
            gvUsers.Bind(IncidentTrackingManager.GetNTQR_UserRole());
            MessageBox.Show("User Added to the process");
        }
        else
        {
            MessageBox.Show("Could not add user!");
        }
    }

    public static byte[] UploadFile(FileUpload file)
    {
        string fileExtension = String.Empty;
        byte[] fileString = null;
        if (file.HasFile)
        {
            var index = file.FileName.LastIndexOf(".");
            fileExtension = file.FileName.Substring(index + 1);
            fileString = file.FileBytes;
        }
        return fileString;
    }
    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       // this.gvUsers.NextPage(this.CurrentProcess.Users, e.NewPageIndex);
    }
    protected void btnTeamSearch_Click(object sender, EventArgs e)
    {
        SearchUsers();
    }

    protected void AddUser_Click(object sender, EventArgs e)
    {
        var processId = System.Configuration.ConfigurationManager.AppSettings["ntqProcess"];
        Response.Redirect("AddUser.aspx?procId=" + processId);
    }
}