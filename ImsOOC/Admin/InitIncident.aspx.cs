using System;
using System.Linq;

public partial class Admin_InitIncident : IncidentTrackingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string incidentNumber;
        string incidentId;     
      
         
        var recordsAffected = IncidentTrackingManager.InitIncident(ProcessID, out incidentId, out incidentNumber);
        if (recordsAffected > 0)
        {
            var admins = CurrentProcess.PowerUsers;
            if (admins != null && admins.Any())
            {
                var processAdmin =
                    admins.Find(
                        admin => admin.SID.ToUpper().Equals(SarsUser.SID.ToUpper()));
                if (processAdmin != null)
                {
                    Response.Redirect(String.Format("NewIncident.aspx?procId={0}&incId={1}", ProcessID, incidentId));
                }
            }
            Response.Redirect(String.Format("RegisterUserIncident.aspx?procId={0}&incId={1}", ProcessID,
                                            incidentId));
        }
    }
}