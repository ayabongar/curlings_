using System;
using System.Configuration;


public static class Configurations
{
    //public static bool UseTam
    //{
    //    get
    //    {
    //        return ConfigurationManager.AppSettings["use_tam"] != null &&
    //               Convert.ToBoolean(ConfigurationManager.AppSettings["use_tam"]);
    //    }
    //}

    public static string AppName
    {
        get { return ConfigurationManager.AppSettings["applicationName"] ?? string.Empty; }
    }

    public static string AppVersion
    {
        get { return ConfigurationManager.AppSettings["version"] ?? string.Empty; }
    }

    //public static string ReportServer
    //{
    //    get { return ConfigurationManager.AppSettings["report_server"] ?? string.Empty; }
    //}

    public static string SurveyUrl
    {
        get { return ConfigurationManager.AppSettings["surveyURL"] ?? string.Empty; }
    }

    public static string ScriptFolder
    {
        get { return ConfigurationManager.AppSettings["scripts"] ?? "Scripts"; }
    }

    public static string StylesFolder
    {
        get { return ConfigurationManager.AppSettings["css"] ?? "Styles"; }
    }


    public static string LDAPUrl
    {
        get { return ConfigurationManager.AppSettings["ldap"]; }
    }

}