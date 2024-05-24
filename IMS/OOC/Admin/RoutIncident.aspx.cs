using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RoutIncident : IncidentTrackingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if(!IsAMember)
        //{
        //    Response.Redirect("~/IncidentNotYours.aspx");
        //    return;
        //}
        var currentIncident = CurrentIncident;

        if(currentIncident.IncidentStatusId == 2 || currentIncident.IncidentStatusId == 3)
        {
            if(!string.IsNullOrEmpty(currentIncident.AssignedToSID) )
            {
                Response.Redirect(string.Format("RegisterUserIncident.aspx?procId={0}&incId={1}", ProcessID, IncidentID));
            }
        }
        Response.Redirect(string.Format("IncidentRealOnly.aspx?procId={0}&incId={1}", ProcessID, IncidentID));
    }
}