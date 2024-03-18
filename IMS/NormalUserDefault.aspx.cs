using System;

public partial class NormalUserDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         var userId = IncidentTrackingManager.GetInitUser();
         if (userId != 0)
         {
             Response.Redirect("Default.aspx");
         }
    }
}