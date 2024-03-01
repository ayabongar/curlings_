using System.Collections.Generic;
using System.Linq;
using System.Web.Services;



[WebService(Namespace = "http://www.sars.gov.za/ws/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

[System.Web.Script.Services.ScriptService]
public class IncidentTrackingService : WebService {

    [WebMethod(Description = "Get a user's AD details using samAccountName")]
    public ADUser GetADUser(string samAccountName)
    {
        var adUser = SarsUser.GetADUser(samAccountName);
        if (adUser != null)
        {
            return adUser;
        }
        return null;
    }

    [WebMethod(Description = "Search a user's AD details using samAccountName")]
    public List<ADUser> SearchUsers(string searchName)
    {
       var users = SarsUser.SearchADUsersByGivenName(searchName);
        if (users.Count > 0)
            return users.Take(5).ToList();

         users = SarsUser.SearchADUsersBySurname(searchName);
        if (users.Count > 0)
            return users.Take(5).ToList();

        users = SarsUser.SearchADUsersByFullName(searchName);
        if (users.Count > 0)
            return users.Take(5).ToList();

        users = SarsUser.SearchADUsersBySID(searchName);
        if (users.Count > 0)
            return users.Take(5).ToList();
        return new List<ADUser>();
    }
}
