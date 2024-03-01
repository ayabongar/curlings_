using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;

public partial class CustomPages_CoverPage : IncidentTrackingPage
{
    protected Incident CurrentIncidentDetails;
    protected IncidentProcess CurrentProcessDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentIncidentDetails = CurrentIncident;
        if (!IsPostBack)
        {
            if (CurrentIncidentDetails != null)
            {
                GetOocOfficeInfo(CurrentIncidentDetails.IncidentID);
            }
        }
    }
    
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Close":
            {
                if (!string.IsNullOrEmpty(ProcessID))
                {
                    if (!string.IsNullOrEmpty(Request["pg"]))
                    {
                        Response.Redirect(String.Format("{0}.aspx?procId={1}&incId={2}&msgId=1", Request["pg"],ProcessID, IncidentID));
                    }
                    else
                    {
                        Response.Redirect(String.Format("~/Admin/RegisterUserIncident.aspx?procId={0}&incId={1}&msgId=1", ProcessID, IncidentID));
                    }
                }
                break;
            }
            case "Print":
            {
                IncidentTrackingManager.AddCoverPage(CurrentIncidentDetails.IncidentID,
                    chkDelivered.Checked ? 1 : 0,
                    txtDocContent.Text,
                    chkSigneOff.Checked ? 1 : 0,
                    chkGrammer.Checked ? 1 : 0,
                    chkConflictMessages.Checked ? 1 : 0,
                    chkInfoRequested.Checked ? 1 : 0,
                    chkDisclose.Checked ? 1 : 0,
                    chkDocSarsSTD.Checked ? 1 : 0,
                    txtCommissionersComment.Text,
                    chkApprovalFromDG.Checked ? 1 : 0,
                    ckApprovalFromDeputyMinister.Checked ? 1 : 0,
                    chkApprovalFromMinister.Checked ? 1 : 0,
                    chkAdditionalInformation.Checked ? 1 : 0,
                    drpNature.SelectedValue);

                if (drpDocumentType.SelectedIndex <= 0)
                {
                    MessageBox.Show("Please select document type!");
                    return;
                }
                if (drpDocumentType.SelectedValue.Equals("Blue"))
                {
                    //  Page.Header.Controls.Add(new System.Web.UI.LiteralControl("<style>td {background-color:blue;border:3px solid white;}</style>"));
                    PrintDocument(GetData(drpPageToPrint.SelectedValue),
                        "<style>td {background-color:white;border: 1px solid black; min-height:  35px;} table{border: 1px solid black;border-spacing: 10px;}</style>");
                }
                else if (drpDocumentType.SelectedValue.Equals("Green"))
                {
                    PrintDocument(GetData(drpPageToPrint.SelectedValue), "<style>td {background-color:white;border: 1px solid black; min-height:  35px;} table{border: 1px solid black;border-spacing: 10px;}</style>");
                }
                else if (drpDocumentType.SelectedValue.Equals("Red"))
                {
                    PrintDocument(GetData(drpPageToPrint.SelectedValue),
                        "<style>td {background-color:white;border: 1px solid black; min-height:  35px;} table{border: 1px solid black;border-spacing: 10px;}</style>");
                }
                else if (drpDocumentType.SelectedValue.Equals("Yellow"))
                {
                    PrintDocument(GetData(drpPageToPrint.SelectedValue),
                       "<style>td {background-color:white;border: 1px solid black; min-height:  35px;} table{border: 1px solid black;border-spacing: 10px;}</style>");
                }
                else if (drpDocumentType.SelectedValue.Equals("Orange"))
                {
                    PrintDocument(GetData(drpPageToPrint.SelectedValue),
                        "<style>td {background-color:white;border: 1px solid black; min-height:  35px;} table{border: 1px solid black;border-spacing: 10px;}</style>");
                }
                break;
            }
        }
    }

    private void PrintDocument(string data, string color)
    {
        var sb = new System.Text.StringBuilder();
        sb.Append(" var mywindow = window.open('', 'Incident Tracking - Cover Page', 'width=800,height=800,left=100,top=100,resizable=yes,scrollbars=1');");
        sb.Append("mywindow.document.write('<html><head><title>Incident Tracking - Cover Page</title>');");
        sb.Append("mywindow.document.write('" + color + "');");
        sb.Append(" mywindow.document.write(' <link href=../Styles/CoverPagePrinter.css rel=stylesheet />');");
        sb.Append(" mywindow.document.write('</head><body >');");
        sb.Append(" mywindow.document.write($('" + data + "').html());");
        sb.Append(" mywindow.document.write('</body></html>');");
        sb.Append("mywindow.document.close(); ");
        sb.Append(" mywindow.focus(); ");
        sb.Append(" mywindow.print();");
        sb.Append(" mywindow.close();");
        ClientScript.RegisterStartupScript(this.GetType(), "Print", sb.ToString(), true);
    }

    private string GetData(string documentVallue)
    {
        string content = null;
        switch (documentVallue)
        {
            case "0":
                content = "#" + dvMain.ClientID;
                break;
            case "1":
                content = "#" + dvFirst.ClientID;
                break;

            case "2":
                content = "#" + dvSecond.ClientID;
                break;

            case "3":
                content = "#" + dvThird.ClientID;
                break;
        }
        return content;
    }

    private void GetOocOfficeInfo(decimal incidentId)
    {
        var results = IncidentTrackingManager.GetOocRecordSet(incidentId);
        if (results.HasRows)
        {
            for (int i = 0; i < results.Rows.Count; i++)
            {
                if (results.Tables[0].Rows[i][1].ToString().Trim().Equals("ReferenceNumber", StringComparison.OrdinalIgnoreCase))
                {
                    lblReferenceNumber.Text = results.Tables[0].Rows[i][2].ToString();
                    lblComReferenceNo.Text = results.Tables[0].Rows[i][2].ToString();
                    lblMinReferenceNo.Text = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString().Trim()
                    .Equals("DateReceived", StringComparison.OrdinalIgnoreCase))
                {
                    lblDateRecieved.Text = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString().Trim()
                    .Equals("Subject", StringComparison.OrdinalIgnoreCase))
                {
                    lblSubject.Text = results.Tables[0].Rows[i][2].ToString();
                    lblComSubject.Text = results.Tables[0].Rows[i][2].ToString();
                    lblMinSubject.Text = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString().Trim()
                    .Equals("DocumentContent", StringComparison.OrdinalIgnoreCase))
                {
                    txtDocContent.Text = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString().Trim()
                    .Equals("Priority", StringComparison.OrdinalIgnoreCase))
                {
                    lblPriority.Text = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString().Trim()
                    .Equals("ActionPerson", StringComparison.OrdinalIgnoreCase))
                {
                    lblActionPerson.Text = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString().Trim()
                    .Equals("Notes", StringComparison.OrdinalIgnoreCase))
                {
                    txtComment.Text = results.Tables[0].Rows[i][2].ToString();
                }
            }
        }
        var workInfo = IncidentTrackingManager.GetOocWorkInfoRecordSet(incidentId);
        if (workInfo.HasRows)
        {
            if (workInfo.Tables[0].Rows[0][1].ToString().Equals("Comment", StringComparison.OrdinalIgnoreCase))
                txtComment.Text = workInfo.Tables[0].Rows[0][2].ToString();
        }
    }
}