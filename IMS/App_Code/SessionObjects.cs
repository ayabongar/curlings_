using Sars.Systems.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SessionObjects
/// </summary>
public class SessionObjects : System.Web.UI.Page
{
    public string CaseId
    {
        get
        {
            return HttpContext.Current.Session["CaseId"] as string;
        }
        set
        {
            HttpContext.Current.Session["CaseId"] = value;
        }
    }
    public string GetUserRole()
    {
        return User.GetRole();
    }
    public string Header
    {
        get
        {
            return HttpContext.Current.Session["Header"] as string;
        }
        set
        {
            HttpContext.Current.Session["Header"] = value;
        }
    }

    public string FunctionState
    {
        get { return HttpContext.Current.Session["functionState"] as string; }
        set { HttpContext.Current.Session["functionState"] = value; }
    }
    public string GroupState
    {
        get { return HttpContext.Current.Session["GroupState"] as string; }
        set { HttpContext.Current.Session["GroupState"] = value; }
    }

    public string CompactSideBar
    {
        get { return HttpContext.Current.Session["compactSideBar"] as string; }
        set { HttpContext.Current.Session["compactSideBar"] = value; }
    }
    public string SubmenuOnhover
    {
        get { return HttpContext.Current.Session["submenuOnhover"] as string; }
        set { HttpContext.Current.Session["submenuOnhover"] = value; }
    }

    public string Skin
    {
        get { return HttpContext.Current.Session["Skin"] as string; }
        set { HttpContext.Current.Session["Skin"] = value; }
    }
    public string SessionUser
    {
        get { return HttpContext.Current.Session["SessionUser"] as string; }
        set { HttpContext.Current.Session["SessionUser"] = value; }
    }
}