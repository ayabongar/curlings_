using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;


    public static class SarsUsers
    {
        public static List<ADUser> SearchADUsersBySID(string searchName)
        {
            var userPrincipal = new UserPrincipal(new PrincipalContext(ContextType.Domain, "SARSGOV")) { SamAccountName = String.Format("{0}*", searchName) };

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
    }

