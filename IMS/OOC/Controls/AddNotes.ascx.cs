using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_AddNotes : System.Web.UI.UserControl
{
    protected Incident CurrentIncidentDetails;
    protected IncidentProcess CurrentProcessDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["incId"]))
            {
                LoadInfo(Request["incId"]);
            }
        }
    }

    private void LoadInfo(string incidentId)
    {
        var data = IncidentTrackingManager.GetWorkInfoByIncidentID(incidentId);
        if (data != null && data.Any())
        {
            gvWorkInfo.Bind(data);
        }
    }
    protected void NewNote(object sender, EventArgs e)
    {
        btnAddNote.Enabled = true;
        txtNotes.Enabled = true;
        btnNew.Enabled = false;
    }
   
    protected void AddNote(object sender, EventArgs e)
    {
        var incidentId = Request["incId"];
        var processId = Request["procId"];
        var workInfo = new WorkInfoDetails
        {
            AddedBySID = SarsUser.SID,
            IncidentId = Convert.ToDecimal(incidentId),
            ProcessId = Convert.ToInt32(processId),
            Notes = txtNotes.Text
        };
        IncidentTrackingManager.AddWorkInfo(workInfo);
            LoadInfo(incidentId);
            txtNotes.Clear();
            MessageBox.Show("Work Info addedd successfully.");
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