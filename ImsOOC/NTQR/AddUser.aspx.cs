using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;
using Sars.Systems.Security;

public partial class NTQR_AddUser : IncidentTrackingPage
{
    protected IncidentProcess CurrentProcessDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CurrentProcessDetails = CurrentProcess;
            SearchUsers();

            BindObjectToPage();
            if (string.IsNullOrEmpty(Request["userId"]))
            {
                drpUnits.Visible = false;
                tdUnits.Visible = false;
            }
        }
    }

    private void BindObjectToPage()
    {
        if (!string.IsNullOrEmpty(Request["userId"]))
        {
            var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
            if (User != null)
            {
                trSignature.Visible = false;
                var userRole = IncidentTrackingManager.GetNTQR_UserRoleById(int.Parse(Request["userId"]));
                if (userRole != null)
                {
                    string sid = string.Empty;
                    foreach (var u in userRole)
                    {
                        sid = u.UserCode;


                        for (int i = 0; i < chkRoles.Items.Count; i++)
                        {
                            if (chkRoles.Items[i].Value == u.RoleId.ToString())
                            {
                                chkRoles.Items[i].Selected = true;
                            }
                        }
                    }

                    var user = SarsUser.GetADUser(sid);
                    UserSelector1.SelectedAdUserDetails = new SelectedUserDetails
                    {
                        SID = user.SID,
                        FoundUserName = string.Format("{0} | {1}", user.FullName, user.SID),
                        FullName = user.FullName
                    };
                }
            }

            var userUnits = GetUserUnits(int.Parse(Request["userId"]));
            if (userUnits != null)
            {
                for (int i = 0; i < userUnits.Tables[0].Rows.Count ; i++)
                {
                    for (int j = 0; j < drpUnits.Items.Count  ; j++)
                    {
                        if(drpUnits.Items[j].Value == userUnits.Tables[0].Rows[i]["fk_User_UnitId"].ToString())
                        {
                            drpUnits.Items[j].Selected = true;  
                        }
                    }
                }
            }
        }
    }

    private void SearchUsers()
    {
        bool isProcessOwner = false;
        foreach (var processOwner in CurrentProcess.Owners)
        {

            chkRoles.Bind(IncidentTrackingManager.GetNTQR_Role(), "RoleName", "ID");

            if (processOwner.OwnerSID.ToLower().Equals(SarsUser.SID.ToLower()))
            {
                isProcessOwner = true;
            }
        }
        chkRoles.Items.RemoveAt(0);
        if (!isProcessOwner)
        {
            chkRoles.Items.FindByValue("4").Enabled = false;
            // chkRoles.Items.Remove("EDM EBR Key Result");
            //chkRoles.Items.re
        }
        drpUnits.Bind(UserUnits(), "Name", "Id");
        drpUnits.Items.RemoveAt(0);
    }

    protected void Save(object sender, EventArgs e)
    {
       

        if (!string.IsNullOrEmpty(Request["userId"]))
        {
            UpdateUserRoles();
            var processId = System.Configuration.ConfigurationManager.AppSettings["ntqProcess"];
            Response.Redirect("UserProfile.aspx?procId=" + processId);
        }

        if (!fileUpload.HasFile)
        {
            MessageBox.Show("Please upload a user Signature!");
            return;
        }

        if (fileUpload.HasFile && !fileUpload.FileName.ToLower().Contains("png"))
        {
            MessageBox.Show("The signature type must be a png image!");
            return;
        }
        
        string sid = UserSelector1.SelectedAdUserDetails.SID;
        SarsUser.SaveUser(sid);
        var User = IncidentTrackingManager.GetNTQR_UserBySID(sid);
        int userId = 0;
        if (User != null)
        {
            userId = User[0].ID;
        }
        string newUser = string.Empty;
        for (int i = 0; i < chkRoles.Items.Count; i++)
        {
            if (chkRoles.Items[i].Selected)
            {
                var user = new NTRQ_User()
                {
                    UserCode = sid,
                    UserFullName = SarsUser.SearchADUsersBySID(sid)[0].FullName,
                    CreatedBy = userId,
                    IsActive = true,
                    Signature = UploadFile(fileUpload),
                    CreatedDate = DateTime.Now,
                    RoleId = int.Parse(chkRoles.Items[i].Value),
                    UserId = userId,
                    fk_User_UnitId = -99

                };
                newUser = SarsUser.SearchADUsersBySID(sid)[0].FullName;
                var recordsAffected = IncidentTrackingManager.NQT_AddUserToRole(user);
                var adUser = SarsUser.GetADUser(SarsUser.SID);
                if (adUser != null)
                {
                    IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} Added a User {1} to a role '{2}' on {3}", adUser.SID + " | " + adUser.FullName, newUser, chkRoles.SelectedItem.Text, DateTime.Now), user.ToXml<NTRQ_User>(), SarsUser.SID, DateTime.Now);
                }
            }
        }

        try
        {
            var processId = System.Configuration.ConfigurationManager.AppSettings["ntqProcess"];
            var result = IncidentTrackingManager.AddUserToAProcess(sid, processId, "1");
            var saved = IncidentTrackingManager.SavePowerUser(sid);
            var user1 = string.Format("{0}\\{1}", Sars.Systems.Data.SARSDataSettings.Settings.DomainName,
               sid);
            if (!string.IsNullOrEmpty(user1))
            {
                try
                {
                    var getUserRole = Roles.GetUserRoles(user1);
                    if (getUserRole.Count == 0)
                    {
                        try
                        {
                            Roles.AddUserToRole("Process Administrator", user1);
                        }
                        catch
                        {


                        };
                    }


                }
                catch (Exception)
                {
                    MessageBox.Show("Could not add user!");
                }
            }
            UserSelector1.Clear();

            MessageBox.ShowAndRedirect("User Added to the process", "UserProfile.aspx?procId=" + processId);
        }
        catch (Exception ex)
        {

            MessageBox.Show(ex.Message);
        }

    }

    private void UpdateUserRoles()
    {

        StringBuilder _sb0 = new StringBuilder();
        _sb0.Append("  Delete from [dbo].[NTQ_User_UnitsMappings] where fk_UserId = @fk_UserId ");


        var _oParams = new DBParamCollection
                          {
                            {"@fk_UserId", int.Parse(Request["userId"])},

                          };
        using (
            var oCommand = new DBCommand(_sb0.ToString(), QueryType.TransectSQL, _oParams, db.Connection))
        {
            oCommand.Execute();

        }


        for (int i = 0; i < drpUnits.Items.Count; i++)
        {

            if (drpUnits.Items[i].Selected)
            {
                StringBuilder sb = new StringBuilder();
               
                sb.Append(" INSERT INTO[dbo].[NTQ_User_UnitsMappings] ");
                sb.Append("  ([fk_UserId] ");
                sb.Append("  ,[fk_User_UnitId] ");
                sb.Append("  ,[CreatedBy] ");
                sb.Append("  ,[CreatedDate]) ");
                sb.Append(" VALUES ");
                sb.Append("   (@fk_UserId, ");
                sb.Append("  @fk_User_UnitId, ");
                sb.Append("  @CreatedBy, ");
                sb.Append("  @CreatedDate) ");

                var oParams = new DBParamCollection
                          {
                            {"@fk_User_UnitId", int.Parse(drpUnits.Items[i].Value)},
                            {"@fk_UserId", int.Parse(Request["userId"])},
                            {"@CreatedBy", SarsUser.SID},
                            {"@CreatedDate",DateTime.Now},
                          };
                using (
                    var oCommand = new DBCommand(sb.ToString(), QueryType.TransectSQL, oParams, db.Connection))
                {
                    oCommand.Execute();

                }
            }
        }


        for (int i = 0; i < chkRoles.Items.Count; i++)
        {
            if (chkRoles.Items[i].Selected)
            {
                StringBuilder _sb = new StringBuilder();
                _sb.Append(" IF NOT EXISTS(SELECT * FROM[dbo].[NTQR_UserRoles] where UserId = @UserId AND RoleId = @RoleId) ");
                _sb.Append("  BEGIN ");
                _sb.Append("  INSERT INTO[dbo].[NTQR_UserRoles] ");
                _sb.Append(" ([RoleId] ");
                _sb.Append(" ,[UserId] ");
                _sb.Append(" ,[CreatedBy] ");
                _sb.Append(" ,[CreatedDate]) ");
                _sb.Append("  VALUES ");
                _sb.Append("    (@RoleId ");
                _sb.Append("  , @UserId ");
                _sb.Append("  , @CreatedBy ");
                _sb.Append("  , @CreatedDate) ");
                _sb.Append("    END ");

                var oParam1 = new DBParamCollection
                          {
                            {"@RoleId", int.Parse(chkRoles.Items[i].Value)},
                            {"@UserId", int.Parse(Request["userId"])},
                            {"@CreatedBy", int.Parse(Request["userId"])},
                            {"@CreatedDate", DateTime.Now},
                          };

                using (
                    var oCommand = new DBCommand(_sb.ToString(), QueryType.TransectSQL, oParam1, db.Connection))
                {
                    oCommand.Execute();


                }
            }
        }

        string sid = UserSelector1.SelectedAdUserDetails.SID;
        var user1 = string.Format("{0}\\{1}", Sars.Systems.Data.SARSDataSettings.Settings.DomainName,
               sid);
        var getUserRole = Roles.GetUserRoles(user1);
        if (getUserRole.Count == 0)
        {
            try
            {
                Roles.AddUserToRole("Process Administrator", user1);
            }
            catch 
            {

                
            }
            
        }
    }

    public RecordSet UserUnits()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT  * from NTQ_User_Units ");


        using (var data = new RecordSet(sb.ToString(), QueryType.TransectSQL, null, db.ConnectionString))
        {
            if (data.HasRows)
            {
                return data;
            }
            return null;
        }
    }
    public RecordSet GetUserUnits(int userId)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT  * from NTQ_User_UnitsMappings where fk_UserId =" + userId);


        using (var data = new RecordSet(sb.ToString(), QueryType.TransectSQL, null, db.ConnectionString))
        {
            if (data.HasRows)
            {
                return data;
            }
            return null;
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


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        var processId = System.Configuration.ConfigurationManager.AppSettings["ntqProcess"];
        Response.Redirect("UserProfile.aspx?procId=" + processId);
    }

}