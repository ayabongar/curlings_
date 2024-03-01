
using ImsService.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ImsService.BO
{
    public  class IMSReminders
    {

        public void EscalateToManagers(Service_ProcessConfiguration proc, Incidents inc)
        {
            var employeeSid = inc.AssignedToSID;

            for (int i = 0; i < 10; i++)
            {
                var employee = Repository.Service_GetEmployeeInfoBySID(employeeSid);
                employeeSid = employee.Tables[0].Rows[0]["Man SID"].ToString();
                var empGrade = employee.Tables[0].Rows[0]["Man grade"].ToString();
                var manEmail = employee.Tables[0].Rows[0]["Man emailAddress"].ToString();
                var manName = employee.Tables[0].Rows[0]["Man ShortName"].ToString();
                // get number of sent notification per manager
                var notifiedManager = Repository.Service_GetIncidentNoOfNotificationsByManSID(inc.IncidentID, employee.Tables[0].Rows[0]["Man SID"].ToString());

                if (notifiedManager == null)
                {
                    notifiedManager = Repository.GetCountOfSentEmailsByIncidentsId(inc.IncidentID);
                    var lastNotifiedDate = notifiedManager[0].Timestamp;
                    if (lastNotifiedDate.AddDays(proc.EscalateToManagerEveryInDays).Date <= DateTime.Today.Date)
                    {
                        if (lastNotifiedDate.Date == DateTime.Today.Date)
                        {
                            break;
                        }
                        SendEmailEscalationAndBackUpRecord(proc, manEmail, manName, employeeSid, inc);
                    }
                    break;
                }
                else
                {
                    var lastNotifiedDate = notifiedManager[0].Timestamp;
                    if (lastNotifiedDate.AddDays(proc.EscalateToManagerEveryInDays).Date <= DateTime.Today.Date)
                    {
                        if (lastNotifiedDate.Date == DateTime.Today.Date)
                        {
                            break;
                        }
                        // only email managers that didn't reach max Notification Alerts
                        if (notifiedManager.Count < proc.MngNoOfNotifications)
                        {
                            SendEmailEscalationAndBackUpRecord(proc, manEmail, manName, employeeSid, inc);
                            break;
                        }
                    }
                }
            }
        }

        public int NotifyProcessOwnerCCAssignee(Service_ProcessConfiguration proc, Incidents inc, List<IncidentTracking> reminderTracker, int trackerCounter)
        {
            DateTime whenEmailSent = trackerCounter > 0 ? reminderTracker[0].Timestamp : DateTime.MinValue;
            if (whenEmailSent.Date < DateTime.Today.Date)
            {
                trackerCounter = reminderTracker.FindAll(m => m.ManagerNotified == true).Count;
                if (trackerCounter < proc.MngNoOfNotifications)
                {
                    var mangerEmail = SarsUsers.SearchADUsersBySID(proc.ManagerSID);
                    if (mangerEmail != null)
                    {
                        SendEmailAndBackUpRecord(proc, inc, true, mangerEmail[0].Mail);
                    }
                }
            }
            return trackerCounter;
        }


        private void SendEmailAndBackUpRecord(Service_ProcessConfiguration item, Incidents inc, bool ccManager, string mngEmail = null)
        {
            try
            {
                var user = SarsUsers.SearchADUsersBySID(inc.AssignedToSID);
                if (user != null)
                {
                    BO.Email.MailUsers(item, user[0].Mail, EmailBody(inc), mngEmail);
                    Repository.InsertIntoIncidentTracking(inc.AssignedToSID, item.ManagerSID, int.Parse(inc.IncidentID.ToString()), 1, EmailBody(inc), ccManager);
                }
            }
            catch (Exception ex)
            {

                new EmailReminder().Init_ServiceTracking("Service Started", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }

        }

        private void SendEmailEscalationAndBackUpRecord(Service_ProcessConfiguration config, string managerEmail, string managerName, string managerSID, Incidents inc)
        {
            try
            {
                var user = SarsUsers.SearchADUsersBySID(inc.AssignedToSID);
                if (user != null)
                {
                    BO.Email.MailUsers(config, managerEmail, EscalationEmailBody(inc, managerName), user[0].Mail);
                    Repository.InsertIntoIncidentTracking(inc.AssignedToSID, managerSID, int.Parse(inc.IncidentID.ToString()), 1, EmailBody(inc), true);
                }
            }
            catch (Exception ex)
            {

                new EmailReminder().Init_ServiceTracking("Service Started", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }

        }
        private string EmailBody(Incidents inc)
        {

            var incidentURL = System.Configuration.ConfigurationManager.AppSettings["incident-details-urlQa"] +
                String.Format("procId={0}&incId={1}", inc.ProcessId, inc.IncidentID);
            var incidentURLProd = System.Configuration.ConfigurationManager.AppSettings["incident-details-urlProd"] +
               String.Format("procId={0}&incId={1}", inc.ProcessId, inc.IncidentID);
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["isProd"]))
            {
                incidentURL = incidentURLProd;
            }

            var body = string.Format(EmailTemplates.AssignedToYou(),
                inc.AssignedToFullName,
                inc.Summary,
                inc.ReferenceNumber,
                DateTime.Parse(inc.DueDate.ToString()).ToString("dd-MM-yyyy"),
                inc.IncidentStatus,
                incidentURL
                );
            return body;
        }
        
        private string EscalationEmailBody(Incidents inc, string managerName)
        {

            var incidentURL = System.Configuration.ConfigurationManager.AppSettings["incident-details-urlQa"] +
                String.Format("procId={0}&incId={1}", inc.ProcessId, inc.IncidentID);
            var incidentURLProd = System.Configuration.ConfigurationManager.AppSettings["incident-details-urlProd"] +
               String.Format("procId={0}&incId={1}", inc.ProcessId, inc.IncidentID);
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["isProd"]))
            {
                incidentURL = incidentURLProd;
            }

            var body = string.Format(EmailTemplates.EscalateToManagers(),
                managerName,
                inc.Summary,
                inc.ReferenceNumber,
                DateTime.Parse(inc.DueDate.ToString()).ToString("dd-MM-yyyy"),
                inc.IncidentStatus,
                incidentURL,
                inc.AssignedToFullName
                );
            return body;
        }

        public void NotifyUser(Service_ProcessConfiguration proc, Incidents inc, List<IncidentTracking> reminderTracker, int trackerCounter)
        {
            DateTime whenEmailSent = trackerCounter > 0 ? reminderTracker[0].Timestamp : DateTime.MinValue;
            //make sure 1 email is sent per day
            if (whenEmailSent.Date < DateTime.Today.Date)
            {
                //  Init_ServiceTracking("Service Started ", string.Format("SendEmailAndBackUpRecord(proc, inc,false);  ProcId{0}Inc={1}", proc.ProcessId, inc.IncidentID));
                SendEmailAndBackUpRecord(proc, inc, false);
            }
        }


        #region OOC Email notification
       
        public void SendEmailToOocUser(Service_OOCProcessConfiguration item, Incidents inc,string followUpCount,int weekNo,int empNotification, bool ccManager, string mngEmail = null)
        {
            try
            {
                var user = SarsUsers.SearchADUsersBySID(inc.AssignedToSID);
                if (user != null)
                {
                    BO.Email.MailOocUsers(item, user[0].Mail, OOcUserEmailBody(inc, followUpCount), mngEmail);
                    Repository.InsertIntoOocIncidentTracking(inc.IncidentID, weekNo, inc.AssignedToSID, empNotification,
                                                    "0", 0, 0, 0, OOcUserEmailBody(inc, followUpCount),false);
                }
                if (!string.IsNullOrEmpty(inc.SecondAssignedSID))
                {
                    user = SarsUsers.SearchADUsersBySID(inc.SecondAssignedSID);
                    if (user != null)
                    {
                        BO.Email.MailOocUsers(item, user[0].Mail, OOcUserEmailBody(inc, followUpCount), mngEmail);
                        Repository.InsertIntoOocIncidentTracking(inc.IncidentID, weekNo, inc.SecondAssignedSID, empNotification,
                                                        "0", 0, 0, 0, OOcUserEmailBody(inc, followUpCount), false);
                    }
                }
            }
            catch (Exception ex)
            {

                new EmailReminder().Init_ServiceTracking("Service Started", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }

        }

        public void SendEmailToOocUserManager(Service_OOCProcessConfiguration item, Incidents inc, string escalationCount,DateTime followupDate, int weekNo, int empNotification, int manNotification, bool ccManager, string mngEmail = null)
        {
            try
            {
                var employee = Repository.Service_GetEmployeeInfoBySID(inc.AssignedToSID);
                var  manSid = employee.Tables[0].Rows[0]["Man SID"].ToString();             

                var user = SarsUsers.SearchADUsersBySID(manSid);
                if (user != null)
                {
                    var cc = SarsUsers.SearchADUsersBySID(inc.AssignedToSID)[0].Mail;
                    if (!string.IsNullOrEmpty(inc.SecondAssignedSID))
                    {
                        cc += SarsUsers.SearchADUsersBySID(inc.SecondAssignedSID)[0].Mail;
                    }
                    BO.Email.MailOocUsers(item, user[0].Mail, OOcUserManagerEmailBody(inc, manSid, escalationCount, followupDate),cc: cc, mngEmail);
                    Repository.InsertIntoOocIncidentTracking(inc.IncidentID, weekNo, manSid, empNotification,
                                                    manSid, manNotification, 0, 0, OOcUserManagerEmailBody(inc, manSid, escalationCount, followupDate), false);
                }
                
            }
            catch (Exception ex)
            {

                new EmailReminder().Init_ServiceTracking("Service Started", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }

        }

        public void SendEmailToOocProcessOwners(Service_OOCProcessConfiguration item, Incidents inc, string escalationCount, DateTime followupDate, int weekNo, int empNotification, int manNotification, int ProcessOwnerNotCount, bool ccManager, string mngEmail = null)
        {
            try
            {
                var owners = Repository.GetOocProcessOwners(inc.ProcessId.ToString(CultureInfo.InvariantCulture));
                List<string> ccs = new List<string>();
                string emailToProcessOwners = string.Empty;
                foreach (var processOwner in owners)
                {
                    var owner = SarsUsers.SearchADUsersBySID(processOwner.OwnerSID);
                    if (owner != null)
                    {
                        emailToProcessOwners = owner[0].Mail;


                        var employee = Repository.Service_GetEmployeeInfoBySID(inc.AssignedToSID);
                        var manSid = employee.Tables[0].Rows[0]["Man SID"].ToString();

                        var user = SarsUsers.SearchADUsersBySID(manSid);
                        if (user != null)
                        {
                            var cc = SarsUsers.SearchADUsersBySID(inc.AssignedToSID)[0].Mail;
                            cc += ";" + user[0].Mail;
                            if (!string.IsNullOrEmpty(inc.SecondAssignedSID))
                            {
                                cc += ";" + SarsUsers.SearchADUsersBySID(inc.SecondAssignedSID)[0].Mail;
                            }
                            BO.Email.MailOocUsers(item, emailToProcessOwners, OOcUserManagerEmailBody(inc, processOwner.OwnerSID, escalationCount, followupDate), cc: cc, mngEmail);
                            Repository.InsertIntoOocIncidentTracking(inc.IncidentID, weekNo, processOwner.OwnerSID, empNotification,
                                                            manSid, manNotification, ProcessOwnerNotCount, 0, OOcUserManagerEmailBody(inc, processOwner.OwnerSID, escalationCount, followupDate), false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new EmailReminder().Init_ServiceTracking("OOC Service Error", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }
        }

        public void SendEmailToDeputyComissioner(Service_OOCProcessConfiguration item, Incidents inc, string escalationCount, DateTime followupDate, int weekNo, int empNotification, int manNotification, int ProcessOwnerNotCount, bool ccManager, string mngEmail = null)
        {
            try
            {
                var owners = Repository.GetOocProcessOwners(inc.ProcessId.ToString(CultureInfo.InvariantCulture));  
                var employee = Repository.Service_GetEmployeeInfoBySID(inc.AssignedToSID);
                var manSid = employee.Tables[0].Rows[0]["Man SID"].ToString();

                var user = SarsUsers.SearchADUsersBySID(manSid);
                if (user != null)
                {
                    var cc = SarsUsers.SearchADUsersBySID(inc.AssignedToSID)[0].Mail;

                    foreach (var processOwner in owners)
                    {
                        var owner = SarsUsers.SearchADUsersBySID(processOwner.OwnerSID);
                        if (owner != null)
                        {                           
                            cc += ";" + owner[0].Mail;
                        }
                    }                    

                    BO.Email.MailOocUsers(item, item.DuputyComEmail, OOcDeputyCommissionerEmailBody(inc, item.DuputyComEmail, escalationCount, followupDate), cc: cc, mngEmail);
                    Repository.InsertIntoOocIncidentTracking(inc.IncidentID, weekNo, "0", empNotification,
                                                    manSid, manNotification, ProcessOwnerNotCount, 1, OOcUserManagerEmailBody(inc, "0", escalationCount, followupDate), false);
                }
            }
            catch (Exception ex)
            {
                new EmailReminder().Init_ServiceTracking("OOC Service Error", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }
        }

        private string OOcUserEmailBody(Incidents inc,string followUpNo)
        {

            var incidentURL = System.Configuration.ConfigurationManager.AppSettings["oocUrlQa"] +
                String.Format("procId={0}&incId={1}", inc.ProcessId, inc.IncidentID);
            var incidentURLProd = System.Configuration.ConfigurationManager.AppSettings["oocurlProd"] +
               String.Format("procId={0}&incId={1}", inc.ProcessId, inc.IncidentID);
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["isProd"]))
            {
                incidentURL = incidentURLProd;
            }
            
            var body = string.Format(EmailTemplates.OocAssignedToYou(),
                inc.AssignedToFullName,
                followUpNo,
                inc.IncidentNumber,
                inc.DateRegistered.ToString("dd-MM-yyyy"),
                inc.Summary,
                incidentURL,                
                inc.IncidentStatus,
                DateTime.Parse(inc.DueDate.ToString()).ToString("dd-MM-yyyy"),
                inc.RegisteredBy
                );
            return body;
        }

        private string OOcUserManagerEmailBody(Incidents inc, string managerSID, string followUpNo,DateTime followupDate)
        {

            var incidentURL = System.Configuration.ConfigurationManager.AppSettings["oocUrlQa"] +
                String.Format("procId={0}&incId={1}", inc.ProcessId, inc.IncidentID);
            var incidentURLProd = System.Configuration.ConfigurationManager.AppSettings["oocurlProd"] +
               String.Format("procId={0}&incId={1}", inc.ProcessId, inc.IncidentID);
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["isProd"]))
            {
                incidentURL = incidentURLProd;
            }
            var user = SarsUsers.SearchADUsersBySID(managerSID);            
            string body = string.Empty;
            if (user != null)
            {
                body = string.Format(EmailTemplates.OocEscalateToManager(),
                user[0].FullName,
                followUpNo,
                inc.IncidentNumber,
                inc.AssignedToFullName,
                inc.Summary,
                incidentURL,
                inc.IncidentStatus,
                DateTime.Parse(inc.DueDate.ToString()).ToString("dd-MM-yyyy"),
                inc.RegisteredBy,
                inc.DateRegistered.ToString("dd-MM-yyyy"),
                followupDate.ToString("dd-MM-yyyy"));                
            }
            return body;
        }

        private string OOcDeputyCommissionerEmailBody(Incidents inc, string managerSID, string followUpNo, DateTime followupDate)
        {

            var incidentURL = System.Configuration.ConfigurationManager.AppSettings["oocUrlQa"] +
                String.Format("procId={0}&incId={1}", inc.ProcessId, inc.IncidentID);
            var incidentURLProd = System.Configuration.ConfigurationManager.AppSettings["oocurlProd"] +
               String.Format("procId={0}&incId={1}", inc.ProcessId, inc.IncidentID);
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["isProd"]))
            {
                incidentURL = incidentURLProd;
            }
           
               var  body = string.Format(EmailTemplates.OocEscalateToManager(),
                "Deputy Commissioner ",
                followUpNo,
                inc.IncidentNumber,
                inc.AssignedToFullName,
                inc.Summary,
                incidentURL,
                inc.IncidentStatus,
                DateTime.Parse(inc.DueDate.ToString()).ToString("dd-MM-yyyy"),
                inc.RegisteredBy,
                inc.DateRegistered.ToString("dd-MM-yyyy"),
                followupDate.ToString("dd-MM-yyyy"));
            
            return body;
        }
        #endregion
    }
}
