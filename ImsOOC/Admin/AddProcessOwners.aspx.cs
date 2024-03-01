using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Admin_AddProcessOwners : IncidentTrackingPage
{
    protected IncidentProcess CurrentIncidentProcess;
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentIncidentProcess = CurrentProcess;
        if (!IsPostBack)
        {
            if (CurrentIncidentProcess != null)
            {
                gvUsers.Bind(IncidentTrackingManager.GetProcessOwners(ProcessID));
            }
            else
            {
                Response.Redirect("~/InvalidProcessOrIncident.aspx");
            }
        }
    }

    protected void RemoveUser(object sender, EventArgs e)
    {
        var btn = sender as LinkButton;
        if(btn!= null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if(row != null)
            {
                gvUsers.SelectRow(row.RowIndex);
                if (gvUsers.SelectedDataKey != null)
                {
                    var ownerId = Convert.ToInt32(gvUsers.SelectedDataKey["OwnerId"]);
                    if (ownerId != 0)
                    {
                        var deleted = IncidentTrackingManager.RemoveProcessOwner(ownerId);
                       if (deleted > 0)
                       {
                         IncidentTrackingManager.SaveAuditTrail(SarsUser.SID,
                                                  string.Format(ResourceString.GetResourceString("removedProcessOwner"), 
                                                                CurrentProcess.Description));
                           var procUsers = IncidentTrackingManager.GetProcessOwners(ProcessID);
                           gvUsers.Bind(procUsers);
                           MessageBox.Show("Process Owner Removed Successfully");
                       }
                    }
                }
            }
        }
    }

    protected void Save(object sender, EventArgs e)
    {
        var sid =UserSelector1.SelectedAdUserDetails.SID;
        SarsUser.SaveUser(sid);
        var recordsAffected = IncidentTrackingManager.AddProcessOwner(Convert.ToInt64(this.ProcessID) ,sid);
        if(recordsAffected > 0)
        {
            IncidentTrackingManager.SaveAuditTrail(SarsUser.SID,
                                                  string.Format(ResourceString.GetResourceString("addedProcessOwner"), UserSelector1.SelectedAdUserDetails.SID,
                                                                CurrentProcess.Description));
            UserSelector1.Clear();
            gvUsers.Bind(IncidentTrackingManager.GetProcessOwners(this.ProcessID));
            MessageBox.Show("Owner SID Added to the process");
        }
        else
        {
            MessageBox.Show("User already exists");
        }
    }
    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvUsers.NextPage(IncidentTrackingManager.GetProcessOwners(this.ProcessID), e.NewPageIndex);
    }
}