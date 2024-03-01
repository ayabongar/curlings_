using System.Configuration;
using System;
using System.ServiceProcess;
using System.Timers;
using ImsService.BO;
using ImsService.DAL;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;

namespace ImsService
{
    public partial class EmailReminder : ServiceBase
    {
        private readonly Timer _timer = new Timer();
        // private System.Threading.Timer _timer;

        //private string _eventLogMessage;

        public EmailReminder()
        {
            InitializeComponent();
            //if (!EventLog.SourceExists("IMSServiceSource"))
            //{
            //    EventLog.CreateEventSource("IMSServiceSource", "IMSServiceLog");
            //}
        }     
        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            //   _eventLogMessage = string.Format(@"IMSService is starting :{0}", DateTime.Now);
            // EventLoggingManager.LogInformation(_eventLogMessage);
            Init_ServiceTracking("NTQ REPORT Service Auto Start", string.Format("Message {0} ", "The NTQ Service Auto Stared "));
            
           // BO.Email.NTQMailUsers("mmakhubu@sars.gov.za", ServiceEmailBody(emailBody,"Started"), subject: "Ims Service hard restart");
            double interval;

            try
            {                
                interval = Convert.ToDouble(ConfigurationManager.AppSettings["intervalInSeconds"]);
                _timer.Enabled = true;
                interval = interval * 1000;
                _timer.Interval = interval;
                _timer.Elapsed += new ElapsedEventHandler(TimerTick);
                _timer.AutoReset = true;
                _timer.Start();
            }
            catch (Exception ex)
            {
                Init_ServiceTracking("IMS  OnStart Service Error", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            //  _eventLogMessage = string.Format(@"IMS Notify service has Stoped: {0}", DateTime.Now);
            // EventLoggingManager.LogInformation(_eventLogMessage);

            try
            {
                EventLog stopLog = new EventLog("IMS Email Reminder Service");
                stopLog.Source = "IMS Email Reminder Service";

                //stopLog.WriteEntry("Demo Stopping ", EventLogEntryType.Information, 1001);
                Init_ServiceTracking("NTQ IMS REPORT Windows Service Stopped", string.Format("Message {0} ", "NTQ IMS REPORT Windows Service"));

                BO.Email.NTQMailUsers("mmakhubu@sars.gov.za", new NTQ_EmailReminders().NTQServiceEmailBody("NTQ Ims Email Reminder Service Unexpectedly Stopped, please contact the Production team to verify. <br/> " + EventLogEntryType.Information, "Stopped"), subject: "NTQ Ims Email Reminder Service Unexpectedly Stopped");

                stopLog.Dispose();
            }
            catch (Exception ex)
            {
                //Catch the exception to avoid unhandled errors and write the output to the debug window.

                Init_ServiceTracking("NTQ IMS REPORT Windows Service Stopped", string.Format("Message {0} ", "NTQ IMS REPORT Windows Service"));
                BO.Email.NTQMailUsers("mmakhubu@sars.gov.za", new NTQ_EmailReminders().NTQServiceEmailBody("NTQ Ims Email Reminder Service Unexpectedly Stopped, please contact the Production team to verify.<br/>  " + "Message: " + ex.Message + "StackTrace : " + ex.StackTrace, "Stopped"), subject: "NTQ Ims Email Reminder Service Unexpectedly Stopped");
            }

        }
        private void TimerTick(object sender, ElapsedEventArgs e)
        {
            try
            {
                IMSNotifyUsers();
                NTQ_Report_Notification();
                OOCNotifyUsers();
            }
            catch (Exception ex)
            {
                Init_ServiceTracking("IMS TimerTick Service Error", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }
        }
      
        public void OOCNotifyUsers()
        {
            try
            {
                var startTime = int.Parse(ConfigurationManager.AppSettings["NotificationStartTime"]);
                var endTime = int.Parse(ConfigurationManager.AppSettings["NotificationEndTime"]);

                new EmailReminder().Init_ServiceTracking("OOC IMS Service Started", string.Format("IMS OOC Service Started at {0}", DateTime.Now.ToString()));
                var processConfig = Repository.GetOocProcessConfiguration();

                if (processConfig != null)
                {
                    if (processConfig[0].IsProServer)
                    {
                        if (!(DateTime.Now.Hour >= startTime && DateTime.Now.Hour <= endTime))
                        {
                            return;
                        }
                        if (DateTime.Today.DayOfWeek != DayOfWeek.Monday)
                        {
                            return;
                        }
                    }
                    foreach (var proc in processConfig)
                    {
                        if (proc.NotifyUsers)
                        {
                           
                            var processIncidents = Repository.GetOocIncidentsByProcessId(proc.ProcessId);
                            if (processIncidents != null)
                            {

                                foreach (var inc in processIncidents)
                                {                             
                                    var reminderTracker = Repository.GetOocCountOfSentEmailsByIncidentsId(inc.IncidentID);                                   
                                    if (reminderTracker == null || (reminderTracker[0].Timestamp.Date != DateTime.Now.Date))
                                    {
                                        int week = (reminderTracker != null && reminderTracker[0].WeekNo > 0) ? reminderTracker[0].WeekNo :0 ;
                                        int empNotificationCount = reminderTracker != null ? reminderTracker[0].EmpNoOfNotification + 1 :1 ;
                                        week++;
                                        switch (week)
                                        {
                                            case 1:
                                            case 2:
                                            case 3:
                                                var type = week == 1 ? "FIRST FOLLOW UP" : week == 2 ? "SECOND FOLLOW UP" : "THIRD FOLLOW UP";
                                                new IMSReminders().SendEmailToOocUser(proc, inc, type, week, empNotificationCount, false);
                                                break;
                                            case 4:
                                            case 5:
                                                type = week == 4 ? "FIRST ESCALATION" : "SECOND ESCALATION";
                                                if (proc.EscalateToManagers)
                                                {
                                                    new IMSReminders().SendEmailToOocUserManager(proc, inc, type, reminderTracker[0].Timestamp,
                                                        week, reminderTracker[0].EmpNoOfNotification, reminderTracker[0].ManagerNoOfNotification + 1, false);
                                                }
                                                break;
                                            case 6:
                                                if (proc.EscalateToProcessOwners)
                                                {
                                                    inc.ProcessId = proc.ProcessId;
                                                    new IMSReminders().SendEmailToOocProcessOwners(proc, inc, "THIRD ESCALATION", reminderTracker[0].Timestamp,
                                                      week, reminderTracker[0].EmpNoOfNotification, reminderTracker[0].ManagerNoOfNotification, reminderTracker[0].ProcessOwnerNoOfNotification + 1, false);
                                                }
                                                break;
                                            case 7:
                                                if (proc.EscalateToProcessOwners)
                                                {
                                                    inc.ProcessId = proc.ProcessId;
                                                    new IMSReminders().SendEmailToDeputyComissioner(proc, inc, "FOURTH ESCALATION", reminderTracker[0].Timestamp,
                                                      week, reminderTracker[0].EmpNoOfNotification, reminderTracker[0].ManagerNoOfNotification, reminderTracker[0].ProcessOwnerNoOfNotification, false);
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                new EmailReminder().Init_ServiceTracking("IMS OOC Service Stopped", string.Format("Service Stopped at {0}", DateTime.Now.ToString()));
            }
            catch (Exception ex)
            {

                new EmailReminder().Init_ServiceTracking("IMS OOCNotifyUsers Service Error", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }
        }
        public void IMSNotifyUsers()
        {
            try
            {
                var startTime = int.Parse(ConfigurationManager.AppSettings["NotificationStartTime"]);
                var endTime = int.Parse(ConfigurationManager.AppSettings["NotificationEndTime"]);

                new EmailReminder().Init_ServiceTracking("Service Started", string.Format("Service Started at {0}", DateTime.Now.ToString()));
                var processConfig = Repository.GetProcessConfiguration();

                if (processConfig != null)
                {
                    if (processConfig[0].IsProServer)
                    {
                        if (!(DateTime.Now.Hour >= startTime && DateTime.Now.Hour <= endTime))
                        {
                            return;
                        }
                    }
                    foreach (var proc in processConfig)
                    {
                        if (proc.NotifyUsers)
                        {

                            var processIncidents = Repository.GetIncidentsByProcessId(proc.ProcessId);
                            if (processIncidents != null)
                            {
                                foreach (var inc in processIncidents)
                                {

                                    var reminderTracker = Repository.GetCountOfSentEmailsByIncidentsId(inc.IncidentID);
                                    var trackerCounter = reminderTracker == null ? 0 : reminderTracker.Count;

                                    if (trackerCounter < proc.Users_NoOfNotification)
                                    {
                                        new IMSReminders().NotifyUser(proc, inc, reminderTracker, trackerCounter);
                                    }
                                    else if (proc.EscalateToProcessOwners)
                                    {
                                        trackerCounter = new IMSReminders().NotifyProcessOwnerCCAssignee(proc, inc, reminderTracker, trackerCounter);
                                    }
                                    else if (proc.EscalateToManagers)
                                    {
                                        new IMSReminders().EscalateToManagers(proc, inc);
                                    }
                                }
                            }
                        }
                    }
                }
                new EmailReminder().Init_ServiceTracking("IMS Service Stopped", string.Format("Service Stopped at {0}", DateTime.Now.ToString()));
            }
            catch (Exception ex)
            {

                new EmailReminder().Init_ServiceTracking("IMS IMSNotifyUsers Service Error", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }
        }
        public void NTQ_Report_Notification()
        {
            try
            {
                var startTime = int.Parse(ConfigurationManager.AppSettings["NotificationStartTime"]);
                var endTime = int.Parse(ConfigurationManager.AppSettings["NotificationEndTime"]);
                var isProd = bool.Parse(ConfigurationManager.AppSettings["isProd"]);
                Init_ServiceTracking("NTQ Service notification Started Successfully", string.Format("Service Started at {0}", DateTime.Now.ToString()));
                var serviceConfigDates = Repository.GetService_NTQR_ProcessConfiguration();


                if (isProd)
                {
                    if (!(DateTime.Now.Hour >= startTime && DateTime.Now.Hour <= endTime))
                    {
                        return;
                    }
                }


                foreach (var service in serviceConfigDates)
                {

                    if (service.isActive)
                    {
                        new NTQ_EmailReminders().Quater1Notification(serviceConfigDates, service);
                        new NTQ_EmailReminders().Quater2Notification(serviceConfigDates, service);
                        new NTQ_EmailReminders().Quater3Notification(serviceConfigDates, service);
                        new NTQ_EmailReminders().Quater4Notification(serviceConfigDates, service);
                    }
                }

                Init_ServiceTracking("NTQ Service Nofication Stopped Successfully", string.Format("Service Stopped at {0}", DateTime.Now.ToString()));
            }
            catch (Exception ex)
            {

                Init_ServiceTracking("NTQ REPORT Service Error", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }
        }
       
        public void Init_ServiceTracking(string status, string comment)
        {
            try
            {
                Repository.Init_ServiceTracking(status, comment);
            }
            catch (Exception)
            {

                //throw;
            }

        }
        public void Init_OOCServiceTracking(string status, string comment)
        {
            try
            {
                Repository.Init_OOCServiceTracking(status, comment);
            }
            catch (Exception)
            {

                //throw;
            }

        }
    }
}

  
    

