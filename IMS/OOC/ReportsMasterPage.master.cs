using Sars.Systems.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportsMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            var user = SarsUser.CurrentUser;
            if (user == null)
            {
                Response.Redirect("~/NoAccessForm.aspx");
            }
            else
            {
                var userRole = this.Page.User.GetRole();
                welcome.InnerHtml = string.Format("Welcome <b>{0} - {1}</b><br/>Role <b>{2}</b> ", user.FullName, user.SID, userRole);

            }
        }
        else
        {
            Response.Redirect("~/NoAccessForm.aspx");
        }
        //var scripts = new List<string>
        //{
        //    "_validation.js",
        //    "Common.js",
        //    "boxover.js"
        //};
        //var styles = new List<string>
        //{
        //    "Site.css",
        //    "survey.css",
        //    "toolBars.css"
        //};

        //Page.RegisterClientScriptFiles(scripts);
        //Page.RegisterCSSFiles(styles);


    }
}
