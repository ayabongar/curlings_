using System;
using System.Configuration;
using System.Linq;
using Sars.Systems.Data;
using Sars.Systems.Security;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Convert.ToInt32("erer");
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("~/admin/SelectNormalUserProcess.aspx");
        }
        var roles = IncidentTrackingManager.GetUserRoles();
        var userProcess = roles.FindAll(r => r.ProcessId == double.Parse(ConfigurationManager.AppSettings["OocInternal"])
            || r.ProcessId == double.Parse(ConfigurationManager.AppSettings["OocExternal"])
             || r.ProcessId == double.Parse(ConfigurationManager.AppSettings["TaxEscalation"]));
        
        if (null != roles && roles.Any())
        {
            var userRole = this.Page.User.GetRole();
            var powerUsers = roles.FindAll(role => role.ProcessId == 135 || role.ProcessId == 74);
            if (powerUsers.Any())
            {
                imgWorkFlow.Visible = false;
                spnLogo.Visible = true;
            }
             if(userProcess != null && userProcess.Any())
            {
                imgWorkFlow.Visible = false;
                spnLogo.Visible = false;
            }
            else
            {
                imgWorkFlow.Visible = false;
                spnLogo.Visible = true;
            }
        }
    }

    private void GenerateRandom(int x)
    {
        if (x >= 10000)
            return;
        var r = new Random();
        var number = new System.Text.StringBuilder();

        for (var i = 0; i < 1000; i++)
        {
            number.Append(r.Next(1000));
            if (number.Length >= 6)
            {
                break;
            }
        }

        using (
            var command = new DBCommand("uspInsertTestRandomNumber", QueryType.StoredProcedure,
                                        new DBParamCollection {{"@RandomNo", number.ToString()}}, db.Connection))
        {
            command.Execute();
        }
        x++;
        GenerateRandom(x);

    }
}
