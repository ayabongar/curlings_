﻿using Sars.Systems.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sars.Systems.Security;

public partial class Controls_Menu_LeftMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            string role = new SessionObjects().GetUserRole();           
            menu.InnerHtml = GetXml(role);

        }
    }

    public static RecordSet GetFunctionsWithGroupsPerRole(string roleName)
    {

        using (
            var data = new RecordSet("[secure].[spGetRoleFunctionsWithGroups]", QueryType.StoredProcedure,new DBParamCollection { { "@RoleName", roleName }, { "@SystemName", "IMS" } }, db.ConnectionString
                                  ))
        {
            return data.HasRows ? data : null;
        }
    }
    private string GetXml(string role)
    {
        var dashboard = "/cms/Default.aspx";
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
                    string[] icons = new string[14]{
                                                      "fa-desktop",
                                                       "fa-list",
                                                       "fa-pencil-square-o",
                                                       "fa-calendar",
                                                       "fa-list-alt",
                                                       "fa-tag",
                                                       "fa-picture-o",
                                                       "fa-file-o",
                                                       "fa-list-alt",
                                                       "fa-tachometer",
                                                       "fa-picture-o",
                                                       "fa-desktop",
                                                        "fa-pencil-square-o",
                                                       "fa-tachometer"
                    };


                    menuBuilder.Append("<ul class=\"nav nav-list\">");
                    menuBuilder.Append("<li class=\"\">");
                    menuBuilder.Append("<a href=\"" + dashboard + "\" runat=\"server\">");
                    menuBuilder.Append("<i class=\"menu-icon fa fa-tachometer\">");
                    menuBuilder.Append("</i>");
                    menuBuilder.Append("<span class=\"menu-text\">Dashboard </span>");
                    menuBuilder.Append("</a>");
                    menuBuilder.Append("<b class=\"arrow\"></b>");
                    menuBuilder.Append("</li>");
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
                                menuBuilder.Append("<li class=\"active open\">");
                            }
                            else
                            {
                                menuBuilder.Append("  <li class=\"open\">");
                            }
                        }
                        else
                        {
                            if (new SessionObjects().GroupState != null)
                            {
                                if (groupName.Contains(new SessionObjects().GroupState))
                                {
                                    menuBuilder.Append("<li class=\"active open\">");
                                }
                                else
                                {
                                    menuBuilder.Append(" <li class=\"open\">");
                                }
                            }
                            else
                                menuBuilder.Append("  <li class=\"\">");
                        }
                        menuBuilder.Append("<a href=\"#\" class=\"dropdown-toggle\">");
                        menuBuilder.Append("  <i class=\"menu-icon fa " + icons[cIcons].ToString() + "\"></i>");
                        menuBuilder.Append(" <span class=\"menu-text\">" + groupName.Replace("CRIMCS", string.Empty).Replace("CI", string.Empty));
                        menuBuilder.Append("</span>");
                        menuBuilder.Append("<b class=\"arrow fa fa-angle-down\"></b>");
                        menuBuilder.Append(" </a>");
                        menuBuilder.Append(" <b class=\"arrow\"></b>");
                        cIcons++;
                        menuBuilder.Append("  <ul class=\"submenu nav-show\" style=\"display: block;\">");
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
                                    menuBuilder.Append("<li class=\"active\">");
                                }
                                else
                                {
                                    menuBuilder.Append("  <li class=\"\">");
                                }
                            }
                            else
                            {
                                if (new SessionObjects().FunctionState != null)
                                {
                                    if (title.Contains(new SessionObjects().FunctionState))
                                    {
                                        menuBuilder.Append("<li class=\"active\">");
                                    }
                                    else
                                    {
                                        menuBuilder.Append(" <li class=\"\">");
                                    }
                                }
                                else
                                    menuBuilder.Append(" <li class=\"\">");
                            }
                            string groupUrl = (url.ToString().Contains("?")) ? url + "&group=" + groupName + "&function=" + title : url + "?group=" + groupName + "&function=" + title;
                            menuBuilder.Append("   <a href=\"" + groupUrl + "\" >");
                            menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
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