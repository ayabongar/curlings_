using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Admin_ViewIncidentEmailLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("SelectNormalUserProcess.aspx");
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName =="View")
        {
            var id = e.CommandArgument.ToString();
            var email = IncidentTrackingManager.GetEmailLogById(id);
            if(email != null)
            {
                dvBody.InnerHtml = email.EmailBody;
                txtEmailAddress.SetValue(email.EmailAddress);
                txtSubject.SetValue(email.Subject);
                txtEmailAddress.Focus();
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(txtIncNo.Text))
        {
            txtIncNo.Focus();
            MessageBox.Show("Incident Number is required");
            return;
        }
        var emails = IncidentTrackingManager.SearchEmailLogByIncidentNumber(txtIncNo.Text);
        if(emails !=  null && emails.Any())
        {
            tblEmail.Visible = true;
            gvEmailLog.Bind(emails);
        }
        else
        {
            MessageBox.Show("No incident found.");
            tblEmail.Visible = false;
        }
    }
    protected void gvEmailLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmailLog.PageIndex = e.NewPageIndex;
        var emails = IncidentTrackingManager.SearchEmailLogByIncidentNumber(txtIncNo.Text);
        if (emails != null && emails.Any())
        {
            tblEmail.Visible = true;
            gvEmailLog.Bind(emails);
        }
        else
        {
            MessageBox.Show("No incident found.");
            tblEmail.Visible = false;
        }
    }
}