using Sars.Systems.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Menu_Processes : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string role = new SessionObjects().GetUserRole();
            menu.InnerHtml = GetXml(role);
        }
    }
 
   
 

   
    public static RecordSet GetFunctionsWithGroupsPerRole()
    {       


        using (
            var data = new RecordSet("[dbo].[uspRead_UserProcesses]", QueryType.StoredProcedure, new DBParamCollection { { "@UserSID", SarsUser.SID } }, db.ConnectionString
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
                var data = GetFunctionsWithGroupsPerRole();
                if (data != null)
                {


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


                    menuBuilder.Append("<ul class=\"menu\">");
                    menuBuilder.Append("<li>");
                    menuBuilder.Append("<a href=\"#\" runat=\"server\">");
                    menuBuilder.Append("<i>");
                    menuBuilder.Append("</i>");
                    menuBuilder.Append("<span >Dashboard </span>");
                    menuBuilder.Append("</a>");
                    menuBuilder.Append("<b></b>");
                    menuBuilder.Append("</li>");
                    int cIcons = 0;

                    menuBuilder.Append("<li>");


                    menuBuilder.Append("<a href=\"#\">");
                    menuBuilder.Append("  <i class=\"menu-icon fa " + icons[cIcons].ToString() + "\"></i>");
                    menuBuilder.Append(" <span class=\"menu-text\">Maintain Report");
                    menuBuilder.Append("</span>");
                    menuBuilder.Append("<b class=\"arrow fa fa-angle-down\"></b>");
                    menuBuilder.Append(" </a>");
                    menuBuilder.Append(" <b class=\"arrow\"></b>");
                    cIcons++;
                    menuBuilder.Append("  <ul style=\"display: block;\">");

                    foreach (DataRow rGroup in data.Tables[0].Rows)
                    {
                        var title = rGroup["Description"].ToString();
                       // if (title.Contains("Report"))
                       // {


                            // if (rGroup["ProcessId"].ToString().Equals("140"))
                            // {
                            var urls = "/ims/NTQR/NormalUserLandingPage.aspx?procId=" + rGroup["ProcessId"].ToString();// rGroup["functionurl"].ToString();
                            menuBuilder.Append("<li>");

                            string groupUrl = "#";//(url.ToString().Contains("?")) ? url + "&group=" + groupName + "&function=" + title : url + "?group=" + groupName + "&function=" + title;
                            menuBuilder.Append("   <a href=\"" + urls + "\" >");
                            menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                            menuBuilder.Append(title);

                            menuBuilder.Append("  </a>");
                            menuBuilder.Append("   </li>");
                            //  }
                       // }
                    }
                    var url = "/ims/NTQR/UserProfile.aspx?procId=" + System.Configuration.ConfigurationManager.AppSettings["ntqProcess"];
                    menuBuilder.Append("<li class=\"\">");


                    menuBuilder.Append("   <a href=\"" + url + "\" >");
                    menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    menuBuilder.Append("Manage Users");

                    menuBuilder.Append("  </a>");
                    menuBuilder.Append("   </li>");





                    menuBuilder.Append("</li>");
                    menuBuilder.Append("</ul>");

                    //var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
                    //if (User != null)
                    //{
                    //    var userRole = IncidentTrackingManager.GetNTQR_UserRoleById(User[0].ID);
                    //    if (userRole != null)
                    //    {
                    //        foreach (var item in userRole)
                    //        {
                    //            if (item.RoleId.Equals(4))
                    //            {
                    //                //-------------------------
                    //                // GROUP
                    //                menuBuilder.Append("<li class=\"\">");
                    //                menuBuilder.Append("<a href=\"#\" runat=\"server\">");
                    //                menuBuilder.Append("<i class=\"menu-icon fa fa-tachometer\">");
                    //                menuBuilder.Append("</i>");
                    //                menuBuilder.Append("<span class=\"menu-text\">Report Admin </span>");
                    //                menuBuilder.Append("</a>");
                    //                menuBuilder.Append("<b class=\"arrow\"></b>");
                    //                //-------------sub-menu
                    //                menuBuilder.Append("  <ul class=\"submenu nav-show\" style=\"display: block;\">");
                    //                url = "/IMS/ntqr/selectnormaluserprocess.aspx?procId=140";
                    //                menuBuilder.Append("<li class=\"\">");
                    //                //----------------
                    //                menuBuilder.Append("   <a href=\"" + url + "\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Data Extract");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                    //                url = "/IMS/ntqr/NTQ_Audit.aspx";
                    //                menuBuilder.Append("<li class=\"\">");
                    //                //----------------
                    //                menuBuilder.Append("   <a href=\"" + url + "\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("System Audit");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                    //                url = "/IMS/ntqr/TrackServiceEMails.aspx";
                    //                menuBuilder.Append("<li class=\"\">");
                    //                //----------------
                    //                menuBuilder.Append("   <a href=\"" + url + "\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Auto Sent Emails");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");


                    //                menuBuilder.Append("</ul>");

                    //                menuBuilder.Append("</li>");

                    //                //--------------Group Menu-----------------
                    //                menuBuilder.Append("<li class=\"\">");
                    //                menuBuilder.Append("<a href=\"#\" runat=\"server\">");
                    //                menuBuilder.Append("<i class=\"menu-icon fa fa-book\">");
                    //                menuBuilder.Append("</i>");
                    //                menuBuilder.Append("<span class=\"menu-text\">Lookups Admin </span>");
                    //                menuBuilder.Append("</a>");
                    //                menuBuilder.Append("<b class=\"arrow\"></b>");

                    //                menuBuilder.Append("  <ul class=\"submenu nav-show\" style=\"display: block;\">");
                    //                //-------------sub-menu
                    //                menuBuilder.Append("<li class=\"\">");

                    //                menuBuilder.Append("   <a href=\"/ims/NTQR/LookupKeyResult.aspx\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Key Results");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                    //                //-------------------------------------
                                 
                                   
                    //                menuBuilder.Append("<li class=\"\">");

                    //                menuBuilder.Append("   <a href=\"/ims/NTQR/LookupKeyResultIndicator.aspx\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Key Results Indicator");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                    //                //-------------------------------------
                    //                menuBuilder.Append("<li class=\"\">");
                    //                menuBuilder.Append("   <a href=\"/ims/NTQR/uploadKR.aspx\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Key Results SO Image");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                    //                //-------------------------------------
                    //                //-------------------------------------
                    //                menuBuilder.Append("<li class=\"\">");
                    //                menuBuilder.Append("   <a href=\"/ims/NTQR/UserUnits.aspx\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Key Results Units");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                    //                //-------------------------------------
                    //                menuBuilder.Append("<li class=\"\">");

                    //                menuBuilder.Append("   <a href=\"/ims/NTQR/AnnualTarget.aspx\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Annual Target");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                    //                //-------------------------------------
                    //                menuBuilder.Append("<li class=\"\">");

                    //                menuBuilder.Append("   <a href=\"/ims/NTQR/Q1.aspx\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Quarter 1");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                    //                //-------------------------------------
                    //                menuBuilder.Append("<li class=\"\">");

                    //                menuBuilder.Append("   <a href=\"/ims/NTQR/Q2.aspx\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Quarter 2");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                    //                //-------------------------------------
                    //                menuBuilder.Append("<li class=\"\">");
                    //                menuBuilder.Append("   <a href=\"/ims/NTQR/Q3.aspx\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Quarter 3");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                    //                //-------------------------------------
                    //                menuBuilder.Append("<li class=\"\">");
                    //                menuBuilder.Append("   <a href=\"/ims/NTQR/Q4.aspx\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Quarter 4");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                    //                //-------------------------------------
                    //                menuBuilder.Append("<li class=\"\">");
                    //                menuBuilder.Append("   <a href=\"/ims/NTQR/Anchor.aspx\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Anchors");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                    //                //-------------------------------------
                    //                menuBuilder.Append("<li class=\"\">");
                    //                menuBuilder.Append("   <a href=\"/ims/NTQR/KeyResultOwner.aspx\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Key Result Owners");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");

                    //                //-------------------------------------
                    //                menuBuilder.Append("<li class=\"\">");
                    //                menuBuilder.Append("   <a href=\"/ims/NTQR/NotificationDates.aspx\" >");
                    //                menuBuilder.Append("   <i class=\"menu-icon fa fa-caret-right\"></i>");
                    //                menuBuilder.Append("Notification Dates");

                    //                menuBuilder.Append("  </a>");
                    //                menuBuilder.Append("   </li>");
                                    
                    //                //-------------End submenu-----------------
                    //                menuBuilder.Append("</ul>");
                    //                menuBuilder.Append("</li>");
                    //                break;
                    //            }
                    //        }

                    //    }
                    //}
                    //--------------End Group Menu-----------
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