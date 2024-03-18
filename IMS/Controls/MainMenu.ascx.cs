using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;
using Sars.Systems.Security;
using System.Data;



public partial class Controls_MainMenu : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            var roles = this.Page.User.GetRole();
            navigation.InnerHtml = GetXml(roles);

        }
    }

    public static RecordSet GetFunctionsWithGroupsPerRole(string roleName)
    {    

        var oParams = new DBParamCollection
                          {

                               {"@RoleName", roleName },
                                {"@SystemName", "IMS" },
                           };
        using (var data = new RecordSet("[secure].[spGetRoleFunctionsWithGroups]", QueryType.StoredProcedure, oParams, db.ConnectionString))
        {
            if (data.HasRows)
            {
                return data;
            }
            return null;
        }

    }
    private string GetXml(string role)
    {
       
        if (null != ViewState["_xm_data"])
            return ViewState["_xm_data"].ToString();
        try
        {
            if (!string.IsNullOrEmpty(role))
            {
                var menuBuilder = new StringBuilder();
                var data = GetFunctionsWithGroupsPerRole(role);
                if (data != null)
                {
                    var paretnColumn = data.Tables[1].Columns["GroupId"];
                    var childColumn = data.Tables[0].Columns["GroupId"];

                    var relation = data.Relations.Add("group_function", paretnColumn, childColumn);
                 

                    menuBuilder.Append("<ul>");
              
                    int cIcons = 0;
                    foreach (DataRow rGroup in data.Tables[1].Rows)
                    {
                        var groupName = rGroup["Description"].ToString();
                        var activeMenu = Request.QueryString["group"];

                        if (activeMenu != null)
                        {
                            new SessionObjects().GroupState = activeMenu;
                            if (groupName.Contains(activeMenu))
                            {
                                menuBuilder.Append("<li>");
                            }
                            else
                            {
                                menuBuilder.Append("  <li>");
                            }
                        }
                        else
                        {
                            if (new SessionObjects().GroupState != null)
                            {
                                if (groupName.Contains(new SessionObjects().GroupState))
                                {
                                    menuBuilder.Append("<li>");
                                }
                                else
                                {
                                    menuBuilder.Append(" <li>");
                                }
                            }
                            else
                                menuBuilder.Append("  <li>");
                        }
                        menuBuilder.Append("<a href=\"#\" class=\"aheading\">");
                      
                        menuBuilder.Append( groupName);                      
                       
                        menuBuilder.Append(" </a>");
                       
                        cIcons++;
                        menuBuilder.Append("  <ul class=\"submenu_block\" >");
                        foreach (var rFunction in rGroup.GetChildRows(relation))
                        {
                            var title = rFunction["Description"].ToString();
                            var url = rFunction["functionurl"].ToString();
                            var functionMenu = Request.QueryString["function"];

                            if (functionMenu != null)
                            {
                                new SessionObjects().FunctionState = functionMenu;
                                if (title.Contains(functionMenu))
                                {
                                    menuBuilder.Append("<li>");
                                }
                                else
                                {
                                    menuBuilder.Append("  <li>");
                                }
                            }
                            else
                            {
                                if (new SessionObjects().FunctionState != null)
                                {
                                    if (title.Contains(new SessionObjects().FunctionState))
                                    {
                                        menuBuilder.Append("<li>");
                                    }
                                    else
                                    {
                                        menuBuilder.Append(" <li>");
                                    }
                                }
                                else
                                    menuBuilder.Append(" <li>");
                            }
                            string groupUrl = (url.ToString().Contains("?")) ? url + "&group=" + groupName + "&function=" + title : url + "?group=" + groupName + "&function=" + title;
                            menuBuilder.Append("   <a href=\"" + groupUrl + "\" class=\"asubmenu\" >");
                         ;
                            menuBuilder.Append(title);

                            menuBuilder.Append("  </a>");
                            menuBuilder.Append("   </li>");
                        }
                        menuBuilder.Append("</li>");
                        menuBuilder.Append("</ul>");
                    }
                    menuBuilder.Append("</ul>");
                }
                if (!string.IsNullOrEmpty(menuBuilder.ToString()))
                {
                    ViewState["_xm_data"] = menuBuilder.ToString();

                    return ViewState["_xm_data"].ToString();
                }
                return null;
            }
            return null;
        }
        catch (Exception ex)
        {
            return string.Format("<div id=\"error\"> <ul> <li>Please make sure that your</li>  <li> Roles </li> <li>  Groups </li> <li>  Functions </li><li>are already added to the database! </li> <li> Error :  <br />{0}</li></ul> </div> ", ex.Message);
        }

    }

}