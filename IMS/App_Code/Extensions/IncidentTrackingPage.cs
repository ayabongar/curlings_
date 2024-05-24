
using System;
using System.Configuration;

public class IncidentTrackingPage : System.Web.UI.Page
{
	public IncidentTrackingPage()
	{
	}
    public int OocInternalProcessId
    {
        get { return int.Parse(ConfigurationManager.AppSettings["OocInternal"]); }
    }

    public int OocExternalProcessId
    {
        get { return int.Parse(ConfigurationManager.AppSettings["OocExternal"]); }
    }
    public int TaxEscalationProcessId
    {
        get { return int.Parse(ConfigurationManager.AppSettings["TaxEscalation"]); }
    }
    public string ProcessID
    {
        get { return Request.Params["procId"]; }
    }
    public string IncidentID
    {
        get { return Request.Params["incId"]; }
    }

    public string FieldID { get { return Request.Params["fieldId"]; } }

    public string PreviousUrl { get; set; }
   
    public void NavigateToHistory()
    {
       Response.Redirect(PreviousUrl); 
    }

    public IncidentProcess CurrentProcess
    {
        get { return IncidentTrackingManager.GetIncidentProcess(this.ProcessID); }
    }


    public Incident CurrentIncident
    {
        get { return IncidentTrackingManager.GetIncidentById(this.IncidentID); }
    }

    public bool IsAMember
    {
        get
        {
            var users = CurrentProcess.Users;
            if (users.Find(user => user.SID.Equals( SarsUser.SID, StringComparison.CurrentCultureIgnoreCase)) != null)
            {
                return true;
            }
            return false;
        }
    }
}

public class IncidentTrackingControl : System.Web.UI.UserControl
{
    public string ProcessID
    {
        get { return Request.Params["procId"]; }
    }

    private string _incidentId;
    public string IncidentID
    {
        get { return Request.Params["incId"]; }
        set { _incidentId = value; }
    }

    public string FieldID { get { return Request.Params["fieldId"]; } }

    public string PreviousUrl { get; set; }

    public void NavigateToHistory()
    {
        Response.Redirect(PreviousUrl);
    }

    public IncidentProcess CurrentProcess
    {
        get { return IncidentTrackingManager.GetIncidentProcess(this.ProcessID); }
    }


    public Incident CurrentIncident
    {
        get { return IncidentTrackingManager.GetIncidentById(this.IncidentID); }
    }
}