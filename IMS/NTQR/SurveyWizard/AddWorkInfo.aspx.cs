using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SurveyWizard_AddWorkInfo : IncidentTrackingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvWorkInfo.Bind(IncidentTrackingManager.GetWorkInfoByIncidentID(this.IncidentID));
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        
        switch (e.CommandName)
        {
            case "Back":
                {
                    Response.Redirect(String.Format("ViewMyIncidents.aspx?procId={0}", this.ProcessID));
                    break;
                }
            case "Add":
                {
                    
                    if (string.IsNullOrEmpty(txtNotes.Text))
                    {
                        txtNotes.Focus();
                        MessageBox.Show("Description is required.");
                        return;
                    }
                    var workInfo = new WorkInfoDetails
                                       {
                                           AddedBySID = SarsUser.SID,
                                           IncidentId = Convert.ToDecimal(IncidentID),
                                           ProcessId = Convert.ToInt32(ProcessID),
                                           Notes = txtNotes.Text
                                       };
                    if (IncidentTrackingManager.AddWorkInfo(workInfo) > 0)
                    {
                        var details = CurrentIncident;
                        if(details.IncidentStatusId == 2)
                        {
                            const int statusId = 3;
                            IncidentTrackingManager.UpdateIncidentStatus(statusId, this.IncidentID);
                        }
                        gvWorkInfo.Bind(IncidentTrackingManager.GetWorkInfoByIncidentID(IncidentID));
                        txtNotes.Clear();
                        txtNotes.Enabled = false;
                        MessageBox.Show("Work Info added successfully.");
                    }
                    break;
                }
        }
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        e.Row.Attributes.Add("onclick",
                             Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        var description = DataBinder.Eval(e.Row.DataItem, "Notes").ToString();

        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        e.Row.Attributes.Add("title",
                             "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Work Info Notes</font></b>] body=[<font color='red'><b>" +
                             description + "</b></font>]");
    }
}