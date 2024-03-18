using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportsMaster : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated)
        {
            var user = SarsUser.CurrentUser;
            if (user == null)
            {
                Response.Redirect("~/NoAccessForm.aspx");
            }
            else
            {
                welcome.InnerHtml = string.Format("Welcome <b>{0} - {1}</b> ", user.FullName, user.SID);
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
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        var status = TreeView1.SelectedValue;
        if (status == "Home")
        {
            Response.Redirect("~/Default.aspx");
        }
        Response.Redirect(status == "100"
                              ? string.Format("~/Reports/Default.aspx?procId={0}", ProcessID)
                              : string.Format("~/Reports/Reports.aspx?procId={0}&stsId={1}", ProcessID, status));
    }

    public string ProcessID
    {
        get { return Request["procId"]; }
    }
}
