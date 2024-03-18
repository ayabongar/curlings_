using Sars.Systems.Data;
using System;
using System.Xml.Linq;

public partial class Admin_CoverPage : IncidentTrackingPage
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
                        Response.Redirect(String.Format("RegisterUserIncident.aspx?procId={0}&incId={1}&msgId=1", ProcessID, IncidentID));
                    }
                }
                break;
            }
            case "Print":
            {
                int signedOff = 0 ;

                if (lblSignOff.Checked)
                {
                    signedOff = 1;
                }
                if (chkSigneOff.Checked)
                {
                    signedOff = 1;
                }
                    var subject = !string.IsNullOrEmpty(lblSubject.Text) ? lblSubject.Text: lblSecSubject.Text;
                    AddCoverPage(subject,
                    CurrentIncidentDetails.IncidentID,
                    chkDelivered.Checked ? 1 : 0,
                    txtDocContent.Text,
                    signedOff,
                    int.Parse(chkGrammer.SelectedValue),
                    int.Parse(chkConflictMessages.SelectedValue),
                    int.Parse(chkInfoRequested.SelectedValue),
                    chkDisclose.Checked ? 1 : 0,
                    int.Parse(chkDocSarsSTD.SelectedValue ),
                    txtCommissionersComment.Text,
                     int.Parse(chkApprovalFromDG.SelectedValue),
                    int.Parse(ckApprovalFromDeputyMinister.SelectedValue),
                     int.Parse(chkApprovalFromMinister.SelectedValue),
                     int.Parse(chkAdditionalInformation.SelectedValue),
                    drpNature.SelectedValue,
                    lblMethodRecieved.Text,
                    txtDocContent2.Text,
                    txtComment.Text);

                if (drpDocumentType.SelectedIndex <= 0)
                {
                    MessageBox.Show("Please select document type!");
                    return;
                }
                try
                {

                    Response.Redirect("PrintCoverPage.aspx?procId=" + Request["procId"] + "&incId=" + Request["incId"] + "&Color=" + drpDocumentType.SelectedValue + "&type=ooc" + "&pages=" + drpPageToPrint.SelectedValue +"&message=" + drpDocumentType.SelectedItem.Text);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                break;
               
            }
        }
    }

    public static int AddCoverPage(string Subject, decimal incidentId, int delivered, string documentContent, int signedOff, int docCheckedForGrammar, int docCheckedForConflict,
       int docAddressInfoRequested, int docNotDicloseFacts, int docSarsStdTemp, string commissionerComent, int dg, int deputyMinister, int minister, int addInfo, string nature,
       string methodReceived, string DocumentContent2, string FrontOfficeComments)
    {
        var oParams = new DBParamCollection
        {
            {"@IncidentId", incidentId},
            {"@Subject", Subject},
            {"@Delivered", delivered},
            {"@DocumentContent", documentContent},
            {"@SignedOff", signedOff},
            {"@DocCheckedForGrammar", docCheckedForGrammar},
            {"@DocCheckedForConflict", docCheckedForConflict},
            {"@DocAddressInfoRequested", docAddressInfoRequested},
            {"@DocNotDicloseFacts", docNotDicloseFacts},
            {"@DocSarsStdTemp", docSarsStdTemp},
            {"@Nature", nature},
            {"@CommissionerComent", commissionerComent},
            {"@DG", dg},
            {"@DeputyMinister", deputyMinister},
            {"@Minister", minister},
            {"@AddInfo ", addInfo},
            {"@CreatedBySID", SarsUser.SID},
            {"@UpdatedBySID", SarsUser.SID},
            {"@UpdatedDate", DateTime.Now},
            {"@MethodReceived", methodReceived},
            {"@DocumentContent2", DocumentContent2},
            {"@FrontOfficeComments", FrontOfficeComments}
        };

        using (var oCommand = new DBCommand("[dbo].[spINSERT_IncidentCoverPage]", QueryType.StoredProcedure, oParams, db.Connection))
        {
            return oCommand.Execute();
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
  

    private void GetOocOfficeInfo(decimal incidentId)
    {
        var results = IncidentTrackingManager.GetOocRecordSet(incidentId);
        if (results.HasRows)
        {
            for (int i = 0; i < results.Rows.Count; i++)
            {
                if (results.Tables[0].Rows[i][1].ToString().Equals("ReferenceNumber", StringComparison.OrdinalIgnoreCase))
                {
                    lblReferenceNumber.Text = results.Tables[0].Rows[i][2].ToString();
                    lblComReferenceNo.Text = results.Tables[0].Rows[i][2].ToString();
                    lblMinReferenceNo.Text = results.Tables[0].Rows[i][2].ToString();
                    lblSecReferenceNumber.Text = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("DateReceived", StringComparison.OrdinalIgnoreCase))
                {
                    lblDateRecieved.Text = results.Tables[0].Rows[i][2].ToString() + " " + CurrentIncident.Timestamp.ToString("HH:mm");
                    lblSecDateReceived.Text = results.Tables[0].Rows[i][2].ToString() + " " + CurrentIncident.Timestamp.ToString("HH:mm");
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("Subject", StringComparison.OrdinalIgnoreCase))
                {
                    lblSubject.Text = results.Tables[0].Rows[i][2].ToString();
                    lblComSubject.Text = results.Tables[0].Rows[i][2].ToString();
                    lblMinSubject.Text = results.Tables[0].Rows[i][2].ToString();
                    lblSecSubject.Text = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("DocumentContent", StringComparison.OrdinalIgnoreCase))
                {
                    txtDocContent.Text = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("LevelOfurgency", StringComparison.OrdinalIgnoreCase))
                {
                    lblPriority.Text = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("ActionPerson", StringComparison.OrdinalIgnoreCase))
                {
                    if (results.Tables[0].Rows[i][2].ToString().ToLower().StartsWith("s"))
                    {
                        var user = Sars.Systems.Security.ADUser.SearchAdUsersBySid(results.Tables[0].Rows[i][2].ToString());
                        if(user !=null)
                        {
                            lblActionPerson.Text = user[0].FullName;
                        }

                    }
                    else
                    {
                        lblActionPerson.Text = results.Tables[0].Rows[i][2].ToString();
                    }
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("Notes", StringComparison.OrdinalIgnoreCase))
                {
                    txtComment.Text = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("DocumentContent", StringComparison.OrdinalIgnoreCase))
                {
                    txtDocContent.Text = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("All SARS Numbers have been reviewed and signed off", StringComparison.OrdinalIgnoreCase))
                {
                    chkSigneOff.Checked = results.Tables[0].Rows[i][2].ToString() == "Yes" ? true : false;
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("Document has been checked for grammar and spelling errors", StringComparison.OrdinalIgnoreCase))
                {
                    chkGrammer.SelectedValue = results.Tables[0].Rows[i][2].ToString() == "Yes" ? "1" : "0";
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("Document has been checked for conflicting message", StringComparison.OrdinalIgnoreCase))
                {
                    chkConflictMessages.SelectedValue = results.Tables[0].Rows[i][2].ToString() == "Yes" ? "1" : "0";
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("Document addresses the information requested", StringComparison.OrdinalIgnoreCase))
                {
                    chkInfoRequested.SelectedValue = results.Tables[0].Rows[i][2].ToString() == "Yes" ? "1" : "0";
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("Document does not disclose inappropriate / unsubstantiated facts", StringComparison.OrdinalIgnoreCase))
                {
                    chkDisclose.Checked = results.Tables[0].Rows[i][2].ToString() == "Yes" ? true : false;
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("Document is presented in a SARS standard template being neat coherent and protraying the image SARS wants to present", StringComparison.OrdinalIgnoreCase))
                {
                    chkDocSarsSTD.SelectedValue = results.Tables[0].Rows[i][2].ToString() == "Yes" ? "1" : "0";
                }

                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("Sign-off for release", StringComparison.OrdinalIgnoreCase))
                {
                    lblSignOff.Checked = results.Tables[0].Rows[i][2].ToString() == "Yes" ? true : false;
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("Approval Required from DG: NT", StringComparison.OrdinalIgnoreCase))
                {
                    chkApprovalFromDG.SelectedValue = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("Approval Required from Deputy Minister", StringComparison.OrdinalIgnoreCase))
                {
                    ckApprovalFromDeputyMinister.SelectedValue = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                    .Equals("Approval Required from Minister", StringComparison.OrdinalIgnoreCase))
                {
                    chkApprovalFromMinister.SelectedValue = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                   .Equals("Additional Information", StringComparison.OrdinalIgnoreCase))
                {
                    chkAdditionalInformation.SelectedValue = results.Tables[0].Rows[i][2].ToString();
                }
                else if (results.Tables[0].Rows[i][1].ToString()
                  .Equals("Channel", StringComparison.OrdinalIgnoreCase))
                {
                    lblMethodRecieved.Text = results.Tables[0].Rows[i][2].ToString();
                    lblSecMethodReceived.Text = results.Tables[0].Rows[i][2].ToString();
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