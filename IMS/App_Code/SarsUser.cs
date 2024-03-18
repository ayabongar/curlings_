using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using Sars.Systems.Data;

/// <summary>
/// Summary description for SarsUser
/// </summary>
public class SarsUser
{
    public static string FullSID
    {
        get { return HttpContext.Current.User.Identity.Name; }
    }
    public static string SID
    {
        get
        {
           return HttpContext.Current.User.Identity.Name.Split("\\".ToCharArray())[1];
            //return "s1022678";//
        }
    }
    public static string FullName
    {
        get { return HttpContext.Current.User.Identity.Name; }
    }

    public static ProcessUsers CurrentUser
    {
        get
        {
            if (null == HttpContext.Current.Session["currentUser"])
            {
                var cUser = GetUser();
                HttpContext.Current.Session.Add("currentUser", cUser);
                return cUser;
            }
           
            return HttpContext.Current.Session["currentUser"] as ProcessUsers;
        }
    }

    public static ADUser CurrentADUser
    {
        get
        {
            if (null == HttpContext.Current.Session["currentUser"])
            {
                var adUser = GetADUser(FullSID);
                HttpContext.Current.Session.Add("currentUser", adUser);
                return adUser;
            }
            return HttpContext.Current.Session["currentUser"] as ADUser;
        }
    }
    public static ProcessUsers GetUser()
    {
        var oParams = new DBParamCollection { { "@SID", SID, System.Data.ParameterDirection.Input } };

        var oRecordSet = new RecordSet("[dbo].[uspREAD_Users]", oParams, db.ConnectionString);
        if (oRecordSet.Tables.Count > 0)
        {
            if (oRecordSet.Tables[0].Rows.Count > 0)
            {
                var user = oRecordSet.GetCustomObject<ProcessUsers>();
                return user;
            }
        }
        return null;
    }
    public static List<ADUser> SearchADUsersBySID(string searchName)
    {
        var userPrincipal = new UserPrincipal(new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["domain"]))
                     {SamAccountName = String.Format("{0}*", searchName)};

        var searcher = new PrincipalSearcher(userPrincipal);
        var usersFound = searcher.FindAll();

        return usersFound.OfType<UserPrincipal>().Select(
            foundUserPrincipal => new ADUser
                                      {
                                          Mail = foundUserPrincipal.EmailAddress,
                                          Name = foundUserPrincipal.GivenName,
                                          Surname = foundUserPrincipal.Surname,
                                          SID = string.Format("S{0}", foundUserPrincipal.SamAccountName.Substring(1)),
                                          Telephone = foundUserPrincipal.VoiceTelephoneNumber,
                                          FullName = foundUserPrincipal.Name
                                      }).ToList();
    }
    public static List<ADUser> SearchADUsersByGivenName(string searchName)
    {
        var userPrincipal =
            new UserPrincipal(new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["domain"]))
                {GivenName = String.Format("{0}*", searchName)};

        var searcher = new PrincipalSearcher(userPrincipal);
        var usersFound = searcher.FindAll();

        return usersFound.OfType<UserPrincipal>().Select(
            foundUserPrincipal => new ADUser
                                      {
                                          Mail = foundUserPrincipal.EmailAddress,
                                          Name = foundUserPrincipal.GivenName,
                                          Surname = foundUserPrincipal.Surname,
                                          SID = string.Format("S{0}", foundUserPrincipal.SamAccountName.Substring(1)),
                                          Telephone = foundUserPrincipal.VoiceTelephoneNumber,
                                          FullName = foundUserPrincipal.Name
                                      }).ToList();
    }

    public static List<ADUser> SearchADUsersByFullName(string searchName)
    {
        var userPrincipal =
            new UserPrincipal(new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["domain"]))
                {DisplayName = String.Format("{0}*", searchName)};

        var searcher = new PrincipalSearcher(userPrincipal);
        var usersFound = searcher.FindAll();

        return usersFound.OfType<UserPrincipal>().Select(
            foundUserPrincipal => new ADUser
                                      {
                                          Mail = foundUserPrincipal.EmailAddress,
                                          Name = foundUserPrincipal.GivenName,
                                          Surname = foundUserPrincipal.Surname,
                                          SID = string.Format("S{0}", foundUserPrincipal.SamAccountName.Substring(1)),
                                          Telephone = foundUserPrincipal.VoiceTelephoneNumber,
                                          FullName = foundUserPrincipal.Name
                                      }).ToList();
    }

    public static List<ADUser> SearchADUsersBySurname(string searchName)
    {
        var userPrincipal =
            new UserPrincipal(new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["domain"]))
                {Surname = String.Format("{0}*", searchName)};

        var searcher = new PrincipalSearcher(userPrincipal);
        var usersFound = searcher.FindAll();

        return usersFound.OfType<UserPrincipal>().Select(
            foundUserPrincipal => new ADUser
                                      {
                                          Mail = foundUserPrincipal.EmailAddress,
                                          Name = foundUserPrincipal.GivenName,
                                          Surname = foundUserPrincipal.Surname,
                                          SID = string.Format("S{0}", foundUserPrincipal.SamAccountName.Substring(1)),
                                          Telephone = foundUserPrincipal.VoiceTelephoneNumber,
                                          FullName = foundUserPrincipal.Name
                                      }).ToList();
    }
    public static ADUser GetADUser(string samAccountName)
    {
        try
        {
            var context = new PrincipalContext(ContextType.Domain,
                                               ConfigurationManager.AppSettings["domain"]);
            var userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName,
                                                             samAccountName);
            if (userPrincipal != null)
            {
                return new ADUser
                           {
                               Mail = userPrincipal.EmailAddress,
                               Name = userPrincipal.GivenName,
                               Surname = userPrincipal.Surname,
                               SID = userPrincipal.SamAccountName,
                               Telephone = userPrincipal.VoiceTelephoneNumber,
                               FullName = userPrincipal.Name
                           };
            }
            userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.Name,
                                                         samAccountName);
            if (userPrincipal != null)
            {
                return new ADUser
                           {
                               Mail = userPrincipal.EmailAddress,
                               Name = userPrincipal.GivenName,
                               Surname = userPrincipal.Surname,
                               SID = string.Format("S{0}", userPrincipal.SamAccountName.Substring(1)),
                               Telephone = userPrincipal.VoiceTelephoneNumber,
                               FullName = userPrincipal.Name
                           };
            }
        }
        catch (Exception exception)
        {
            //throw exception;
            return new ADUser
                       {
                           Mail = "mmakhubu@sars.gov.za",
                           Name = "Mzwakhe",
                           Surname = "Makhubu",
                           Telephone = "0124226042",
                           FullName = "Mzwakhe Makhubu",
                           SID = SID
                       };
        }
        return null;
    }

    public static void SaveUser(String sid)
    {
        AddUser(GetADUser(sid));
    }

    public static void AddUser(ADUser adUser)
    {
        if( adUser == null) return;
        var oParams = new DBParamCollection
                          {
                              {"@SID", adUser.SID},
                              {"@FirstName", !string.IsNullOrEmpty(adUser.Name) ? adUser.Name: adUser.FullName},
                              {"@Surname", adUser.Surname},
                              {"@EmailAddress", adUser.Mail},
                              {"@Telephone", adUser.Telephone},
                              {"@AddedBySID", SID}
                          };
        using (var oComm = new DBCommand("[dbo].[uspINSERT_User]", QueryType.StoredProcedure, oParams, db.Connection))
        {
            oComm.Execute();
        }
    }


}


public class SelectedUserDetails
{
    public String FullName { get; set; }
    public String SID { get; set; }
    public String FoundUserName { get; set; }
}