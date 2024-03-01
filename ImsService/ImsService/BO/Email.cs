using System;
using System.ComponentModel;
using System.DirectoryServices;
using System.Configuration;
using ImsService.BLL;
using Sars.Systems;
using Sars.Systems.Mail;
using System.Collections.Generic;
using ImsService.DAL;
using System.Globalization;

namespace ImsService.BO
{
    public static class Email
    {
        static string _eventMessage = string.Empty;
        private static void ClientOnSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var mailSentTo = (string)e.UserState;


            if (e.Cancelled)
            {
                _eventMessage = "Mail sending was cancelled while sending to " + mailSentTo;
              //  EventLoggingManager.LogWarning(_eventMessage);
            }
            if (e.Error != null)
            {
                _eventMessage = "Error sending mail to " + mailSentTo;
                _eventMessage += "\n Error " + e.Error.InnerException;
               // EventLoggingManager.LogError(_eventMessage);
            }
            else
            {
                _eventMessage = "Message sent to " + mailSentTo;
               // EventLoggingManager.LogInformation(_eventMessage);
            }
        }

        public static void MailOocUsers(Service_OOCProcessConfiguration procConfig, string to, string content, string cc = null, string bcc = null, string subject = null)
        {
            string fromEmail = ConfigurationManager.AppSettings["fromEmail"];
            string isProd = ConfigurationManager.AppSettings["isProd"];
            if (!procConfig.IsProServer)
            {
                to = procConfig.TestEmailsGoTo;
                if (!string.IsNullOrEmpty(cc))
                {
                    cc = "mmakhubu@sars.gov.za";
                }
            }
            try
            {

                var client = new SmtpServiceClient("basicHttpEndPoint");
                {
                    var messageBody = content;
                    Sars.Systems.Mail.SmtpMessage mailMessage = null;
                    if (string.IsNullOrEmpty(cc))
                    {
                        mailMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = fromEmail,
                            To = new[] { to },
                            Body = messageBody,
                            IsBodyHtml = true,
                            Subject = string.IsNullOrEmpty(subject) ? "IMS Service - Incident Reminder" : subject
                        };
                    }
                    else if (!string.IsNullOrEmpty(cc))
                    {
                        mailMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = fromEmail,
                            To = new[] { to },
                            CC = new[] { cc },
                            Body = messageBody,
                            IsBodyHtml = true,
                            Subject = string.IsNullOrEmpty(subject) ? "IMS Incident Reminder" : subject
                        };
                    }
                    client.Send2(mailMessage);
                }
            }
            catch (Exception)
            {
                //_eventMessage = "Mail sending was cancelled while sending to " + x.Message;
                //  EventLoggingManager.LogWarning(_eventMessage);
            }
        }

        public static void MailUsers(Service_ProcessConfiguration procConfig, string to, string content, string cc = null, string bcc = null,string subject = null)
        {
            string fromEmail = ConfigurationManager.AppSettings["fromEmail"];
            string isProd = ConfigurationManager.AppSettings["isProd"];
            if (!procConfig.IsProServer)
            {
                to = procConfig.TestEmailsGoTo;
                //if(!string.IsNullOrEmpty(cc))
                //{
                //    cc = "mmakhubu@sars.gov.za";
                //}
            }
            try
            {

                var client = new SmtpServiceClient("basicHttpEndPoint");
                {
                    var messageBody = content;
                    Sars.Systems.Mail.SmtpMessage mailMessage = null;
                    if (string.IsNullOrEmpty(cc))
                    {
                        mailMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = fromEmail,
                            To = new[] { to },
                            Body = messageBody,
                            IsBodyHtml = true,
                            Subject = string.IsNullOrEmpty(subject) ?"IMS Service - Incident Reminder" : subject
                        };
                    }
                    else if (!string.IsNullOrEmpty(cc))
                    {
                        mailMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = fromEmail,
                            To = new[] { to },
                            CC = new[] { cc },
                            Body = messageBody,
                            IsBodyHtml = true,
                            Subject = string.IsNullOrEmpty(subject) ? "IMS Incident Reminder" : subject
                        };
                    }
                 client.Send2(mailMessage);
                }
            }
            catch (Exception)
            {
                //_eventMessage = "Mail sending was cancelled while sending to " + x.Message;
              //  EventLoggingManager.LogWarning(_eventMessage);
            }
        }

        public static void NTQMailUsers(string to, string content, List<string> cc = null, string bcc = null, string subject = null)
        {
            string fromEmail = ConfigurationManager.AppSettings["fromEmail"];
            string isProd = ConfigurationManager.AppSettings["isProd"];
            string procId = !bool.Parse(isProd)? ConfigurationManager.AppSettings["nqtQAProcId"] : ConfigurationManager.AppSettings["nqtprodProcId"];
            var owners =  Repository.GetProcessOwners( procId.ToString(CultureInfo.InvariantCulture));
            List<string> ccs = new List<string>();
            foreach (var processOwner in owners)
            {
                var owner = SarsUsers.SearchADUsersBySID(processOwner.OwnerSID);
                if (owner != null)
                {
                   ccs.Add(owner[0].Mail);
                }
                
            }
           // ccs.Add("tcoetzer@sars.gov.za");
            //ccs.Add("dvangreunen@sars.gov.za");
            if (cc != null)
            {               
                foreach (var c in cc)
                {
                    var cEmail = SarsUsers.SearchADUsersBySID(c);
                    if (cEmail != null)
                    { if(!ccs.Contains(cEmail[0].Mail))
                         ccs.Add(cEmail[0].Mail);
                    }
                }
            }

            try
            {

                var client = new SmtpServiceClient("basicHttpEndPoint");
                {
                    var messageBody = content;
                    Sars.Systems.Mail.SmtpMessage mailMessage = null;
                    if (ccs.Count <  0)
                    {
                        mailMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = fromEmail,
                            To = new[] { to },
                            //To = new[] { "mmakhubu@sars.gov.za" },
                            Body = messageBody,
                            IsBodyHtml = true,
                            Subject = string.IsNullOrEmpty(subject) ? "National Treasury Quarterly Report submission notification" : subject
                        };
                    }
                    else 
                    {
                        mailMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = fromEmail,
                            To = new[] { to },
                            //To = new[] { "mmakhubu@sars.gov.za" },
                            CC = ccs.ToArray(),
                            Body = messageBody,
                            IsBodyHtml = true,
                            Subject = string.IsNullOrEmpty(subject) ? "National Treasury Quarterly Report submission notification" : subject
                        };
                    }
                    client.Send2(mailMessage);
                }
            }
            catch (Exception ex)
            {
                //_eventMessage = "Mail sending was cancelled while sending to " + x.Message;
                //  EventLoggingManager.LogWarning(_eventMessage);
                new EmailReminder().Init_ServiceTracking("NTQ Service Error", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));

            }
        }

        public static void MailNQTUsers(string to, string content, string cc = null, string bcc = null, string subject = null)
        {
            string fromEmail = ConfigurationManager.AppSettings["fromEmail"];            
            try
            {

                var client = new SmtpServiceClient("basicHttpEndPoint");
                {
                    var messageBody = content;
                    Sars.Systems.Mail.SmtpMessage mailMessage = null;
                    if (string.IsNullOrEmpty(cc))
                    {
                        mailMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = fromEmail,
                            To = new[] { to },
                            Body = messageBody,
                            IsBodyHtml = true,
                            Subject = string.IsNullOrEmpty(subject) ? "IMS Service - Incident Reminder" : subject
                        };
                    }
                    else if (!string.IsNullOrEmpty(cc))
                    {
                        mailMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = fromEmail,
                            To = new[] { to },
                            CC = new[] { cc },
                            Body = messageBody,
                            IsBodyHtml = true,
                            Subject = string.IsNullOrEmpty(subject) ? "IMS Incident Reminder" : subject
                        };
                    }
                    client.Send2(mailMessage);
                }
            }
            catch (Exception )
            {
                //_eventMessage = "Mail sending was cancelled while sending to " + x.Message;
                //  EventLoggingManager.LogWarning(_eventMessage);
            }
        }

        public static string GetUserEmail(string strSid)
        {
            string userEmail = "";
            if (!string.IsNullOrEmpty(strSid))
            {
                var entry = new DirectoryEntry(LdapUrl.GetCurrentDomainPath());
                var search = new DirectorySearcher(entry);

                search.Filter = "(&(objectClass=user)(anr=" + strSid.Trim() + "))";
                search.PropertiesToLoad.Add("mail");
                // perform the search 
                SearchResult result = search.FindOne();

                if (result != null)
                {
                    try
                    {
                        userEmail = result.Properties["mail"][0].ToString();
                    }
                    catch (Exception)
                    {

                        userEmail = string.Empty;
                    }
                }
                else
                {
                    userEmail = System.Configuration.ConfigurationManager.AppSettings["emailNotFound"];
                }
            }

            return userEmail;
        }
        internal class LdapUrl
        {

            public static string GetCurrentDomainPath()
            {
                DirectoryEntry de =
                   new DirectoryEntry("LDAP://RootDSE");

                return "LDAP://" +
                   de.Properties["defaultNamingContext"][0].
                       ToString();
            }
        }


    }
}
