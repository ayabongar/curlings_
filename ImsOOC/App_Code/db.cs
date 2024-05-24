using System.Data;
using Sars.Systems.Data;
using Sars.Systems.Security;

/// <summary>
/// Summary description for db
/// </summary>

public class db
{
    public static string ConnectionString
    {
        get
        {
           return System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString;
          //return SecureConfig.DecryptString(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString, "P@ssw0rd");
        }
    }
    public static string OocDbConnectionString
    {
        get
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["OocDb"].ConnectionString;
            //return SecureConfig.DecryptString(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString, "P@ssw0rd");
        }
    }
    public static string SapConnectionString
    {
        get
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["assets"].ConnectionString;
            //return SecureConfig.DecryptString(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString, "P@ssw0rd");
        }
    }
    public static DBConnection Connection
    {
        get
        {
           return new DBConnection(ConnectionString);
        }
    }
    public static DBConnection TransactionConnection
    {
        get
        {
            return new DBConnection(ConnectionString, true, IsolationLevel.ReadUncommitted);
        }
    }
}
