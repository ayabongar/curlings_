using System;
using System.Text;
using System.Web.Configuration;

public partial class Admin_Acknowledgement : IncidentTrackingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtOffice.Text = WebConfigurationManager.AppSettings["headOffice"];
            txtEnquiry.Text = WebConfigurationManager.AppSettings["StreetAddress"];
            txtTel.Text = WebConfigurationManager.AppSettings["tel"];
            txtFax.Text = WebConfigurationManager.AppSettings["fax"];
            txtRoom.Text = WebConfigurationManager.AppSettings["Block"];
            txtAddress.Text = "Pretoria Head Office\n" + "Bronkhorst Street\n" + "Nieuw Muckleneuk, 0181\n" + "Private Bag X923, Pretoria, 0001\n" + "SARS online: www.sars.gov.za\n" + "Telephone (012) 422 4000";
            txtTopic1.Text = WebConfigurationManager.AppSettings["topic"];
            txtFooter.Text = WebConfigurationManager.AppSettings["footer"];
            txtAcknowledgement.Text = WebConfigurationManager.AppSettings["acknowledgement"];
            txtGreeting.Text = WebConfigurationManager.AppSettings["greeting"];	
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Close":
                {
                    if (!string.IsNullOrEmpty(Request["pg"]))
                    {
                        Response.Redirect(String.Format("{0}.aspx?procId={1}&incId={2}&msgId=1", Request["pg"], ProcessID, IncidentID));
                    }
                    else
                    {
                        Response.Redirect(String.Format("RegisterUserIncident.aspx?procId={0}&incId={1}&msgId=1", ProcessID, IncidentID));
                    }
                    break;
                }
            case "Print":
                {
                    PrintDocument("#dvMain");
                    break;
                }
        }
    }
    private void PrintDocument(string data)
    {
        var sb = new StringBuilder();
        sb.Append(" var mywindow = window.open('', 'Acknowledgement Letter', 'width=1200,height=800,left=100,top=100,resizable=yes,scrollbars=1');");
        sb.Append("mywindow.document.write('<html><head><title>Acknowledgement Letter</title>');");
        sb.Append(" mywindow.document.write(' <link href=../Styles/Acknowledgement.css rel=stylesheet />');");
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