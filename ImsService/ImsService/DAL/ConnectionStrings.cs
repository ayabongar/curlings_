
using Sars.Systems.Data;
using Sars.Systems.Security;
namespace ImsService.DAL
{
    public static class ConnectionStrings
    {
       


        public static DBConnection Connection
        {
            get
            {
                return new DBConnection(ConnectionString);
            }
        }

        public static DBConnection OocConnection
        {
            get
            {
                return new DBConnection(OocConnectionString);
            }
        }
        public static string AssetsConnectionString
        {
            get
            {
               return System.Configuration.ConfigurationManager.ConnectionStrings["assets"].ToString();
               // return SecureConfig.DecryptString(System.Configuration.ConfigurationManager.ConnectionStrings["assets"].ToString(), "P@ssw0rd");
            }
        }
        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["ims"].ConnectionString;
                //return SecureConfig.DecryptString(System.Configuration.ConfigurationManager.ConnectionStrings["ims"].ConnectionString, "P@ssw0rd");
            }
        }

        public static string OocConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["imsooc"].ConnectionString;
                //return SecureConfig.DecryptString(System.Configuration.ConfigurationManager.ConnectionStrings["ims"].ConnectionString, "P@ssw0rd");
            }
        }
    }
}
