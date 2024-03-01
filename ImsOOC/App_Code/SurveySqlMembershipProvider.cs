using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using Sars.Systems.Security;

/// <summary>
/// Summary description for SurveySqlMembershipProvider
/// </summary>
public class SurveySqlMembershipProvider : SqlMembershipProvider
{
    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
    {
        base.Initialize(name, config);

        var connectionString =SecureConfig.DecryptString(
                System.Configuration.ConfigurationManager.ConnectionStrings["survey"].ConnectionString, "P@ssw0rd");
        var baseType = GetType().BaseType;
        if (baseType != null)
        {
            var connectionStringField = baseType.GetField("_sqlConnectionString",
                                                          BindingFlags.Instance | BindingFlags.NonPublic);
            if (connectionStringField != null)
            {
                 connectionStringField.SetValue(this, connectionString);
            }
        }
    }
}


public class SurveySqlRoleProvider : SqlRoleProvider
{
    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
    {
        base.Initialize(name, config);
        var connectionString = SecureConfig.DecryptString(System.Configuration.ConfigurationManager.ConnectionStrings["survey"].ConnectionString, "P@ssw0rd");
        var baseType = GetType().BaseType;
        if (baseType != null)
        {
            var connectionStringField = baseType.GetField("_sqlConnectionString",
                                                                               BindingFlags.Instance | BindingFlags.NonPublic);
            if (connectionStringField != null)
            {
                connectionStringField.SetValue(this, connectionString);
            }
        }
    }
}


public class SurveySqlProfileProvider : SqlProfileProvider
{
    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
    {
        base.Initialize(name, config);
        var connectionString = SecureConfig.DecryptString(System.Configuration.ConfigurationManager.ConnectionStrings["survey"].ConnectionString, "P@ssw0rd");
        var baseType = GetType().BaseType;
        if (baseType != null)
        {
            var connectionStringField = baseType.GetField("_sqlConnectionString",
                                                                               BindingFlags.Instance | BindingFlags.NonPublic);
            if (connectionStringField != null)
            {
                 connectionStringField.SetValue(this, connectionString);
            }
        }
    }
}