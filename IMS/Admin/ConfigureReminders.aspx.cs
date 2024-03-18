using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ConfigureReminders : IncidentTrackingPage
{
    protected IncidentProcess CurrentIncidentProcess;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SearchUsers();
        }
    }


    private void getServiceInformation(decimal processId)
    {
        new IncidentProcess("[dbo].[uspRead_AllProcesses]", new Dictionary<string, object> { { "@ProcessId", processId } }).GetRecords<IncidentProcess>();

    }
    private void SearchUsers()
    {
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("~/NoAccessForm.aspx");
        }
        CurrentIncidentProcess = CurrentProcess;

       
        var roles = new ProcessRole("[dbo].[uspREAD_ProcessRoles]", null).GetRecords<ProcessRole>();
        if (roles != null && roles.Any())
        {
           // ddlRole.Bind(roles, "Description", "RoleID");
        }

    }

    protected void RemoveUser(object sender, EventArgs e)
    {
        var btn = sender as LinkButton;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
               // gvUsers.SelectRow(row.RowIndex);
                //if (gvUsers.SelectedDataKey != null)
                //{
                //    var userProcessId = Convert.ToInt32(gvUsers.SelectedDataKey["UserProcessId"]);
                //    if (userProcessId != 0)
                //    {
                //        var deleted = IncidentTrackingManager.RemoveProcessUser(userProcessId);
                //        if (deleted > 0)
                //        {
                //            var procUsers = CurrentProcess.Users;
                //            gvUsers.Bind(procUsers);
                //            MessageBox.Show("User Removed Successfully");
                //        }
                //    }
                //}
            }
        }
    }

    protected void Save(object sender, EventArgs e)
    {
        //var sid = UserSelector1.SelectedAdUserDetails.SID;
       // SarsUser.SaveUser(sid);
      //  var recordsAffected = IncidentTrackingManager.AddUserToAProcess(sid, this.ProcessID, ddlRole.SelectedValue);
        //if (recordsAffected > 0)
        //{

        //    UserSelector1.Clear();
        ////    gvUsers.Bind(CurrentProcess.Users);
        //    MessageBox.Show("User Added to the process");
        //}
        //else
        //{
        //    MessageBox.Show("Could not add user!");
        //}
    }
    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       // this.gvUsers.NextPage(this.CurrentProcess.Users, e.NewPageIndex);
    }
    protected void btnTeamSearch_Click(object sender, EventArgs e)
    {
        SearchUsers();
    }
}