using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NTQR_InitIncident : IncidentTrackingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string incidentNumber;
        string incidentId;
       

        var recordsAffected = IncidentTrackingManager.InitIncident(ProcessID, out incidentId, out incidentNumber);
        if (recordsAffected > 0)
        {
            
            Response.Redirect(String.Format("RegisterUserIncident.aspx?procId={0}&incId={1}&KeyResult={2}", ProcessID,
                                            incidentId,Request["KeyResult"]));
        }
    }
}