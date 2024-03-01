using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class Admin_GenerateCoverPage : IncidentTrackingPage
{
    protected Incident CurrentIncidentDetails;
    protected IncidentProcess CurrentProcessDetails;
    public byte[] pdf = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentIncidentDetails = CurrentIncident;
        CurrentProcessDetails = CurrentProcess;
        if (!IsPostBack)
        {
            LoadInfo();
        }
    }

    private void LoadInfo()
    {
        var data = IncidentTrackingManager.GetWorkInfoByIncidentID(IncidentID);
        if (data != null && data.Any())
        {
           // gvWorkInfo.Bind(data);
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Print":
            {
                try
                {
                    if (!DisplayCoverPage1.SaveQuestions())
                    {
                        return;
                    }
                    Response.Redirect("PrintCoverPage.aspx?procId=" + Request["procId"] + "&incId=" + Request["incId"] + "&Color=" + drpDocumentType.SelectedValue + "&type=other&pages=");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                break;
            }
            case "Close":
            {
                Response.Redirect("RegisterUserIncident.aspx?procId=" + Request["procId"] + "&incId=" + Request["incId"] );
            }
            break;
        }
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
           
        }
        return content;
    }

    private void PrintDocument(string data, string color)
    {
        var sb = new System.Text.StringBuilder();
        sb.Append(" var mywindow = window.open('', 'Incident Tracking - Cover Page', 'width=800,height=800,left=100,top=100,resizable=yes,scrollbars=1');");
        sb.Append("mywindow.document.write('<html><head><title>Incident Tracking - Cover Page</title>');");
        sb.Append("mywindow.document.write('" + color + "');");
        sb.Append(" mywindow.document.write(' <link href=../Styles/GenericCoverPage.css rel=stylesheet />');");
        sb.Append(" mywindow.document.write('</head><body >');");
        sb.Append(" mywindow.document.write($('" + data + "').html());");
        sb.Append(" mywindow.document.write('</body></html>');");
        sb.Append("mywindow.document.close(); ");
        sb.Append(" mywindow.focus(); ");
        sb.Append(" mywindow.print();");
        sb.Append(" mywindow.close();");
        ClientScript.RegisterStartupScript(this.GetType(), "Print", sb.ToString(), true);
    }
}