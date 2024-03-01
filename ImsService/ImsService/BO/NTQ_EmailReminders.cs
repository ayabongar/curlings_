using ImsService.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ImsService.BO
{
    public class NTQ_EmailReminders
    {
        #region NTQ Report

        public void Quater1Notification(List<Service_NTQR_ProcessConfiguration> serviceConfigDates, Service_NTQR_ProcessConfiguration service)
        {
            switch (service.Id)
            {
                case 1:
                    if (service.Q1Date == DateTime.Today)
                    {
                        var users = Repository.ImsService_NTQ_GetCompilersNotifier();
                        var reportPeriod = "1";
                        QuarterNotification(service, users, service.Q1Date, serviceConfigDates[4].Q1Date, reportPeriod);
                    }
                    break;
                case 3:
                    if (DateTime.Today == service.Q1Date)
                    {
                        // get Key Result Owner record to notify
                        var reports = Repository.ImsService_NTQR_Report_Notifier(10, 1);
                        QuarterKRONotification(service, reports, service.Q1Date, serviceConfigDates[4].Q1Date);
                    }
                    break;
                case 4:
                    if (DateTime.Today == service.Q1Date)
                    {
                        //Anchors
                        var reports = Repository.ImsService_NTQR_Report_Notifier(11, 1);
                        QuarterAnchorNotification(service, reports, service.Q1Date, serviceConfigDates[4].Q1Date);
                    }
                    break;
                case 5:

                    if (service.Q1Date == DateTime.Today)
                    {
                        // People Didn't Declare
                        var reportPeriod = "1";
                        var subject = "National Treasury Quarterly Report: Final submission reminder";
                        var users = Repository.ImsService_NTQR_PeopleDidntDeclare(int.Parse(reportPeriod));
                        if (users != null)
                        {
                            QuarterNotification(service, users, service.Q1Date, serviceConfigDates[4].Q1Date, reportPeriod, subject);
                        }             
                    }
                    break;
                default:
                    break;
            }
        }
        public void Quater2Notification(List<Service_NTQR_ProcessConfiguration> serviceConfigDates, Service_NTQR_ProcessConfiguration service)
        {
            switch (service.Id)
            {
                case 1:
                    if (service.Q2Date == DateTime.Today)
                    {
                        var users = Repository.ImsService_NTQ_GetCompilersNotifier();
                        var reportPeriod = "2";
                        QuarterNotification(service, users, service.Q2Date, serviceConfigDates[4].Q2Date, reportPeriod);
                    }
                    break;
                case 3:
                    if (DateTime.Today == service.Q2Date)
                    {
                        // get Key Result Owner record to notify
                        var reports = Repository.ImsService_NTQR_Report_Notifier(10, 2);
                        QuarterKRONotification(service, reports, service.Q2Date, serviceConfigDates[4].Q2Date);
                    }
                    break;
                case 4:
                    if (DateTime.Today == service.Q2Date)
                    {
                        //Anchors
                        var reports = Repository.ImsService_NTQR_Report_Notifier(11, 2);
                        QuarterAnchorNotification(service, reports, service.Q2Date, serviceConfigDates[4].Q2Date);
                    }
                    break;
                case 5:

                    if (service.Q2Date == DateTime.Today)
                    {
                        // People Didn't Declare
                        var reportPeriod = "2";
                        var subject = "National Treasury Quarterly Report: Final submission reminder";
                        var users = Repository.ImsService_NTQR_PeopleDidntDeclare(2);
                        if (users != null)
                        {
                            QuarterNotification(service, users, service.Q2Date, serviceConfigDates[4].Q2Date, reportPeriod, subject);
                        }                  
                    }
                    break;

                default:
                    break;
            }
        }
        public void Quater3Notification(List<Service_NTQR_ProcessConfiguration> serviceConfigDates, Service_NTQR_ProcessConfiguration service)
        {
            switch (service.Id)
            {
                case 1:
                    if (service.Q3Date == DateTime.Today)
                    {
                        var users = Repository.ImsService_NTQ_GetCompilersNotifier();
                        var reportPeriod = "3";
                        QuarterNotification(service, users, service.Q3Date, serviceConfigDates[4].Q3Date, reportPeriod);
                    }
                    break;
                case 3:
                    if (DateTime.Today == service.Q3Date)
                    {
                        // get Key Result Owner record to notify
                        var reports = Repository.ImsService_NTQR_Report_Notifier(10, 3);
                        QuarterKRONotification(service, reports, service.Q3Date, serviceConfigDates[4].Q3Date);
                    }
                    break;
                case 4:
                    if (service.Q3Date == DateTime.Today)
                    {
                        //Anchors
                        var reports = Repository.ImsService_NTQR_Report_Notifier(11, 3);
                        QuarterAnchorNotification(service, reports, service.Q3Date, serviceConfigDates[4].Q3Date);
                    }
                    break;
                case 5:
                    if (service.Q3Date == DateTime.Today)
                    {
                        // People Didn't Declare
                        var reportPeriod = "3";
                        var subject = "National Treasury Quarterly Report: Final submission reminder";
                        var users = Repository.ImsService_NTQR_PeopleDidntDeclare(3);
                        if (users != null)
                        {
                            QuarterNotification(service, users, service.Q3Date, serviceConfigDates[4].Q3Date, reportPeriod, subject);
                        }                   
                    }
                    break;


                default:
                    break;
            }
        }
        public void Quater4Notification(List<Service_NTQR_ProcessConfiguration> serviceConfigDates, Service_NTQR_ProcessConfiguration service)
        {
            switch (service.Id)
            {
                case 1:
                    if (service.Q4Date == DateTime.Today)
                    {
                        var users = Repository.ImsService_NTQ_GetCompilersNotifier();
                        var reportPeriod = "4";
                        QuarterNotification(service, users, service.Q4Date, serviceConfigDates[4].Q4Date, reportPeriod, "National Treasury Quarterly Report: Key Result owner submission reminder");
                    }
                    break;
                case 3:
                    if (DateTime.Today == service.Q4Date)
                    {
                        // get Key Result Owner record to notify
                        var reports = Repository.ImsService_NTQR_Report_Notifier(10, 4);
                        QuarterKRONotification(service, reports, service.Q4Date, serviceConfigDates[4].Q4Date);
                    }
                    break;
                case 4:
                    if (DateTime.Today == service.Q4Date)
                    {
                        //Anchors
                        var reports = Repository.ImsService_NTQR_Report_Notifier(11, 4);
                        QuarterAnchorNotification(service, reports, service.Q4Date, serviceConfigDates[4].Q4Date);
                    }
                    break;
                case 5:
                    if (service.Q4Date == DateTime.Today)
                    {
                        var reportPeriod = "4";
                        var subject = "National Treasury Quarterly Report: Final submission reminder";
                        // didn't declare by quorter Id
                        var users = Repository.ImsService_NTQR_PeopleDidntDeclare(4);
                        if (users != null)
                        {
                            QuarterNotification(service, users, service.Q4Date, serviceConfigDates[4].Q4Date, reportPeriod, subject);
                        }                        
                    }
                    break;

                default:
                    break;
            }
        }      
        private void QuarterKRONotification(Service_NTQR_ProcessConfiguration service, List<ImsService_NTQR_Report_Notifier> reports, DateTime quarterDate, DateTime dueDate)
        {
            if (reports != null)
            {
                foreach (var report in reports)
                {
                    //Q1 Reminder- 

                    var reminderTracker = Repository.ImsService_NTQR_SELECT_ServiceTracker(service.Id, report.Id, report.Compiler, quarterDate);
                    if (reminderTracker == null) //send a reminder then insert into NTQR_ServiceTracker table
                    {
                        var keyResultOwner = report.Compiler;
                        if (!string.IsNullOrEmpty(keyResultOwner))
                        {
                            var compiler = Repository.ImsService_NTQR_GetUserByKeyAndRoleId(1, int.Parse(report.KeyResultID.ToString()));
                            List<string> compilerSid = new List<string>();
                            if (compiler != null)
                            {
                                foreach (var c in compiler)
                                {
                                   // compilerSid.Add(c.UserFullName.Split('|')[0].Trim());
                                }
                            }
                            var subject = "National Treasury Quarterly Report: Key Result owner submission reminder";
                            NTQ_SendEmailAndBackUpRecord(service: service, report: report, dueDate: dueDate, assignUser: keyResultOwner, ccs: compilerSid, service.EmailMocks, subject: subject, quarterDate: quarterDate);
                        }
                    }
                }
            }
        }
        private void QuarterAnchorNotification(Service_NTQR_ProcessConfiguration service, List<ImsService_NTQR_Report_Notifier> reports, DateTime quarterDate, DateTime dueDate)
        {
            if (reports != null)
            {
                foreach (var report in reports)
                {
                    //Q1 Reminder- 

                    var reminderTracker = Repository.ImsService_NTQR_SELECT_ServiceTracker(service.Id, report.Id,report.Compiler, quarterDate);
                    if (reminderTracker == null) //send a reminder then insert into NTQR_ServiceTracker table
                    {

                        var anchorSID = report.Compiler;
                        if (!string.IsNullOrEmpty(anchorSID))
                        {
                            List<string> kroSid = new List<string>();
                            var kro = Repository.ImsService_NTQR_GetUserByKeyAndRoleId(2, int.Parse(report.KeyResultID.ToString()));
                            if (kro != null)
                            {
                                foreach (var k in kro)
                                {
                                   // kroSid.Add(k.UserFullName.Split('|')[0].Trim());
                                }
                            }
                            var subject = "National Treasury Quarterly Report: Anchor approval reminder";
                            NTQ_SendEmailAndBackUpRecord(service: service, report: report, dueDate: dueDate, assignUser: anchorSID, ccs: kroSid, mailBody: service.EmailMocks, subject: subject, quarterDate: quarterDate);
                        }
                    }
                }
            }
        }
        private void QuarterNotification(Service_NTQR_ProcessConfiguration service, List<ImsService_NTQR_Report_Notifier> users, DateTime quarterDate, DateTime dueDate, string reportPeriod, string subject = null)
        {
            if (users != null)
            {
                foreach (var u in users)
                {
                    //Q1 Reminder- 
                    u.Summary = reportPeriod;
                    var reminderTracker = Repository.CheckIfCompilerNotified(service.Id, u.KeyResultID, quarterDate, u.Id);
                    if (reminderTracker == null) //send a reminder then insert into NTQR_ServiceTracker table
                    {
                        var compilerSID = u.Compiler;
                        if (!string.IsNullOrEmpty(compilerSID))
                        {
                            SendNQT_QuarterEmail(service, u, dueDate, compilerSID, service.EmailMocks, quarterDate, subject);
                        }
                    }
                }
            }
        }
        private void NTQ_SendEmailAndBackUpRecord(Service_NTQR_ProcessConfiguration service, ImsService_NTQR_Report_Notifier report, DateTime dueDate, string assignUser, List<string> ccs, string mailBody, string subject, DateTime quarterDate)
        {
            try
            {

                string emailBody = string.Empty;
                string[] userSid = null;
                string emailAddress = string.Empty;
                var Subject = subject;
                int userId = 0;

                var compiler = Repository.ImsService_NTQR_GetUserByKeyAndRoleId(1, int.Parse(report.KeyResultID.ToString()));
                string compilerName = string.Empty;
                List<string> compilerSid = new List<string>();
                if (compiler != null)
                {
                    foreach (var c in compiler)
                    {
                        compilerName += c.UserFullName + "<br>";
                    }
                }

                string kroName = string.Empty;
                List<string> kroSid = new List<string>();
                var kro = Repository.ImsService_NTQR_GetUserByKeyAndRoleId(2, int.Parse(report.KeyResultID.ToString()));
                if (kro != null)
                {
                    foreach (var k in kro)
                    {
                        kroName += k.UserFullName + "<br>";
                    }
                }

                var anchor = Repository.ImsService_NTQR_GetUserByKeyAndRoleId(3, int.Parse(report.KeyResultID.ToString()));
                string anchorName = string.Empty;
                List<string> anchorSid = new List<string>();
                if (anchor != null)
                {
                    foreach (var a in anchor)
                    {
                        anchorName += a.UserFullName + "<br>";
                    }
                }

                if (assignUser.Contains(","))
                {
                    userSid = assignUser.Split(',');
                    foreach (var item in userSid)
                    {
                        var sid = item.Split('|');
                        var user = SarsUsers.SearchADUsersBySID(sid[0].Trim());
                        emailAddress = service.isProd ? user[0].Mail : service.testEmail;
                       

                        emailBody = NQT_EmailBody(report, dueDate, user[0].FullName, compilerName, kroName, anchorName, mailBody);
                        ccs = service.isProd ? ccs : null;
                        BO.Email.NTQMailUsers(emailAddress, content: emailBody, cc: ccs, subject: subject);

                        var User = Repository.GetNTQR_UserBySID(sid[0].Trim());
                        if (User != null)
                        {
                            userId = User[0].ID;
                        }


                        Repository.NTQR_Insert_ServiceTracker(service.Id, report.Id, quarterDate, "Quarter: " + report.Summary, userId, report.KeyResultID, "noreply@sars.gov.za",
                        emailBody, user[0].FullName, emailAddress, Subject);

                    }
                }
                else if (assignUser.Contains("|"))
                {
                    userSid = assignUser.Split('|');
                    var user = SarsUsers.SearchADUsersBySID(userSid[0].Trim());

                    emailBody = NQT_EmailBody(report, dueDate, user[0].FullName, compilerName, kroName, anchorName, mailBody);

                    emailAddress = service.isProd ? user[0].Mail : service.testEmail;
                    BO.Email.NTQMailUsers(emailAddress, content: emailBody, cc: ccs, subject: subject);
                    var User = Repository.GetNTQR_UserBySID(userSid[0].Trim());
                    if (User != null)
                    {
                        userId = User[0].ID;
                    }
                    Repository.NTQR_Insert_ServiceTracker(service.Id, report.Id, quarterDate, "Quarter: " + report.Summary, userId, report.KeyResultID, "noreply@sars.gov.za",
                    emailBody, user[0].FullName, emailAddress, Subject);
                }

            }
            catch (Exception ex)
            {

                new EmailReminder().Init_ServiceTracking("NTQ Service Error", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }

        }
        private string NQT_QuarterFirstEmail(ImsService_NTQR_Report_Notifier inc, DateTime dueDate, string assignUser, string compiler, string kro, string anchor, string emailBody)
        {

            var incidentURL = System.Configuration.ConfigurationManager.AppSettings["nqtCaptureQA"];
            var incidentURLProd = System.Configuration.ConfigurationManager.AppSettings["nqtCaptureProd"];
            string period = string.Empty;
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["isProd"]))
            {
                incidentURL = incidentURLProd;
            }
            switch (inc.Summary)
            {
                case "1":
                    period = "First";
                    break;
                case "2":
                    period = "Second";
                    break;
                case "3":
                    period = "Third";
                    break;
                case "4":
                    period = "Fourth";
                    break;
                case "5":
                    period = "Annual";
                    break;

                default:
                    break;
            }

            var status = string.IsNullOrEmpty(inc.IncidentStatus) ? "New" : inc.IncidentStatus;
            var body = string.Format(emailBody,
                    assignUser,
                    period,
                    DateTime.Parse(dueDate.ToString()).ToString("dd-MM-yyyy"),
                    period,
                    compiler,
                    kro,
                    anchor,
                    incidentURL,
                    inc.KeyResult,
                    status

                );

            return body;
        }
        private void SendNQT_QuarterEmail(Service_NTQR_ProcessConfiguration service, ImsService_NTQR_Report_Notifier inc, DateTime dueDate, string assignUser, string mailBody, DateTime quarterDate, string subject = null)
        {
            try
            {

                string emailBody = string.Empty;
                string[] userSid = null;
                string emailAddress = string.Empty;
                var Subject = (string.IsNullOrEmpty(subject)) ? "National Treasury Quarterly Report submission notification" : subject;
                if (assignUser.Contains("|"))
                {
                    userSid = assignUser.Split('|');
                    var user = SarsUsers.SearchADUsersBySID(userSid[0].Trim());

                    var compiler = Repository.ImsService_NTQR_GetUserByKeyAndRoleId(1, int.Parse(inc.KeyResultID.ToString()));
                    StringBuilder compilerName = new StringBuilder();
                    if (compiler != null)
                    {
                        foreach (var c in compiler)
                        {
                            compilerName.Append(c.UserFullName);
                            compilerName.Append("<br>");

                        }
                        compilerName.Append("<br>");
                    }

                    var kro = Repository.ImsService_NTQR_GetUserByKeyAndRoleId(2, int.Parse(inc.KeyResultID.ToString()));
                    StringBuilder kroName = new StringBuilder();
                    if (kro != null)
                    {
                        foreach (var k in kro)
                        {
                            kroName.AppendLine(k.UserFullName);
                            kroName.Append("<br>");

                        }
                        kroName.Append("<br>");
                    }

                    var anchor = Repository.ImsService_NTQR_GetUserByKeyAndRoleId(3, int.Parse(inc.KeyResultID.ToString()));
                    StringBuilder anchorName = new StringBuilder();
                    if (anchor != null)
                    {
                        foreach (var a in anchor)
                        {
                            anchorName.AppendLine(a.UserFullName);
                            anchorName.Append("<br>");

                        }
                        anchorName.Append("<br>");
                    }
                    emailBody = NQT_QuarterFirstEmail(inc, dueDate, user[0].FullName, compilerName.ToString(), kroName.ToString(), anchorName.ToString(), mailBody);
                    emailAddress = service.isProd ? user[0].Mail : service.testEmail;
                    BO.Email.NTQMailUsers(emailAddress, emailBody, subject: Subject);
                    // back sent Emails
                    Repository.NTQR_Insert_ServiceTracker(service.Id, 0, quarterDate, "Quarter: " + inc.Summary, inc.Id, inc.KeyResultID, "noreply@sars.gov.za",
                    emailBody, user[0].FullName, emailAddress, Subject);
                }


            }
            catch (Exception ex)
            {

                new EmailReminder().Init_ServiceTracking("NTQ Service Error", string.Format("Message {0} - Inner Exception {1} - StackTrace {2}", ex.Message, ex.InnerException, ex.StackTrace));
            }

        }
        private string NQT_EmailBody(ImsService_NTQR_Report_Notifier inc, DateTime dueDate, string assignUser, string compilerName, string kroName, string anchorName, string emailBody)
        {

            var incidentURL = System.Configuration.ConfigurationManager.AppSettings["nqtReportQA"] +
                String.Format("ProcId={0}&Id={1}&keyResult={2}&incId={3}", inc.ProcessId, inc.Id, inc.fk_ReportKeyResult_ID, inc.IncidentID);
            var incidentURLProd = System.Configuration.ConfigurationManager.AppSettings["nqtReportProd"] +
               String.Format("ProcId={0}&Id={1}&keyResult={2}&incId={3}", inc.ProcessId, inc.Id, inc.fk_ReportKeyResult_ID, inc.IncidentID);
            string period = string.Empty;
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["isProd"]))
            {
                incidentURL = incidentURLProd;
            }
            switch (inc.Summary)
            {
                case "1":
                    period = "First";
                    break;
                case "2":
                    period = "Second";
                    break;
                case "3":
                    period = "Third";
                    break;
                case "4":
                    period = "Fourth";
                    break;
                case "5":
                    period = "Annual";
                    break;

                default:
                    break;
            }

            var body = string.Format(emailBody,
                assignUser,
                period,
                incidentURL,
                inc.ReferenceNumber,
                DateTime.Parse(dueDate.ToString()).ToString("dd-MM-yyyy"),
                inc.IncidentStatus,
                compilerName,
                kroName,
                anchorName,
                inc.KeyResult
                );
            return body;
        }
        public string NQT_AssignedToYou()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   <html> ");
            sb.Append(" <body> ");
            sb.Append("  <center> ");
            sb.Append("   <table style='margin: 0px 10px; border-top-left-radius: 10px; border-top-right-radius: 10px; ");
            sb.Append("     width: 747px;'> ");
            sb.Append("    <tr> ");
            sb.Append(
                "     <td style='border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: silver; ");
            sb.Append("      width: 640px; text-align: center; height: 100px; font-size: 30pt; color: #fff; ");
            sb.Append("    font-family: Arial, Verdana, Comic Sans MS'> ");
            sb.Append("    <b>NATIONAL TREASURY REPORT SIGN-OFF SHEET Notification</b> ");
            sb.Append("  </td> ");
            sb.Append("    </tr> ");
            sb.Append("   <tr> ");
            sb.Append("   <td class='w640' bgcolor='#ffffff' height='30' width='640'> ");
            sb.Append("   </td> ");
            sb.Append("   </tr> ");
            sb.Append("   <tr id='simple-content-row'> ");
            sb.Append("   <td class='w640' bgcolor='#ffffff' width='640'> ");
            sb.Append("    <table class='w640' border='0' cellspacing='0' cellpadding='0' width='640'> ");
            sb.Append("  <tbody> ");
            sb.Append("       <tr> ");
            sb.Append("     <td class='w30' width='30'> ");
            sb.Append("    </td> ");
            sb.Append("    <td class='w580' width='580'> ");
            sb.Append("  <repeater> ");

            sb.Append("   <layout label='Text only'> ");
            sb.Append("  <table class='w580' border='0' cellspacing='0' cellpadding='0'> ");
            sb.Append("  <tbody><tr> ");
            sb.Append("  <td class='w580' width='580'> ");
            sb.Append("    <div class='article-content' align='left'> ");
            sb.Append("    <table style='font-family: Verdana;  width: 700px; color: white; padding: 10px'> ");
            sb.Append("   <tr> ");
            sb.Append("        <td class='header' align='left' style='font-size: 10pt; color: Gray;' > ");
            sb.Append("       Good day <strong>{0}</strong><br /> ");
            sb.Append("      <br /> ");
            sb.Append("    <br /> ");
            sb.Append("The report signoff sheet with the reference number <a href='{5}'>{2}</a>  is due today and you are required to submit the Quarterly National Treasury report as well as evidence by the due date: {3}<br />.");

            sb.Append("   <br /> ");
            sb.Append("     <br /> ");
            sb.Append("    </td> ");
            sb.Append("   </tr> ");
            sb.Append("  <tr> ");
            sb.Append("  <td style='font-size: 10pt; color: Gray;'> ");

            sb.Append("   <b>Incident Details</b> ");
            sb.Append("   <table width='100%' border='0' cellpadding='5px' style='font-size: 10pt; color: Gray;'> ");
            sb.Append("   <tr> ");
            sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;'> ");
            sb.Append("     Summary : ");
            sb.Append("     </td> ");
            sb.Append("    <td> ");
            sb.Append("   {1} ");
            sb.Append("   </tr> ");
            sb.Append("   <tr> ");
            sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;'> ");
            sb.Append("     Reference Number: ");
            sb.Append("     </td> ");
            sb.Append("    <td> ");
            sb.Append("    <a href='{5}'>{2}</a> ");
            sb.Append("   </td> ");

            sb.Append("   </tr> ");
            sb.Append("    <tr> ");
            sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;' > ");
            sb.Append("       Incident Status: ");
            sb.Append("    </td> ");
            sb.Append("     <td> ");
            sb.Append("      {4} ");
            sb.Append("     </td> ");

            sb.Append("   </tr> ");
            sb.Append("   <tr> ");
            sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;' > ");
            sb.Append("       Due Date: ");
            sb.Append("   </td> ");
            sb.Append("    <td> ");
            sb.Append("       {3} ");
            sb.Append("    </td> ");

            sb.Append("  </tr> ");

            sb.Append("  </table> ");

            sb.Append("   </td> ");
            sb.Append("  </tr> ");


            sb.Append("   <tr> ");
            sb.Append("  <td style='font-size: 10pt; color: Gray;'> ");
            sb.Append("   <br /> ");
            sb.Append("   <b>Kind Regards<br/>NATIONAL TREASURY REPORT  System ");

            sb.Append("    </b><br /><br /> ");
            sb.Append("    </td> ");
            sb.Append("   </tr> ");
            sb.Append("    </table> ");
            sb.Append("   </div> ");
            sb.Append(" </td> ");
            sb.Append("  </tr> ");

            sb.Append("  </tbody></table> ");
            sb.Append("    </layout> ");
            sb.Append("   </td> ");
            sb.Append("    <td class='w30' width='30'> ");
            sb.Append("    </td> ");
            sb.Append("   </tr> ");
            sb.Append("   </tbody> ");
            sb.Append("  </table> ");
            sb.Append("  </td> ");
            sb.Append("   </tr> ");
            sb.Append("   <tr> ");
            sb.Append(
                "     <td style='border-bottom-left-radius: 5px; border-bottom-right-radius: 5px; background-color: silver; ");
            sb.Append(
                "     width: 640px; text-align: left; height: 20px; color: #fff; font-family: Arial, Verdana, Comic Sans MS; ");
            sb.Append("     padding: 2px'> ");
            sb.Append("     <b>NB: This is an automated email, do not reply. </b> ");
            sb.Append("   </td> ");
            sb.Append("  </tr> ");
            sb.Append("  </table> ");
            sb.Append("  </center> ");
            sb.Append(" </body> ");
            sb.Append(" </html> ");


            return sb.ToString();

        }
        public string NTQServiceEmailBody(string emailBody, string status)
        {

            var incidentURL = System.Configuration.ConfigurationManager.AppSettings["nqtCaptureQA"];
            var incidentURLProd = System.Configuration.ConfigurationManager.AppSettings["nqtCaptureProd"];
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["isProd"]))
            {
                incidentURL = incidentURLProd;
            }

            var body = string.Format(EmailTemplates.ServiceStartStopped(),
                DateTime.Now.ToString("dd-MM-yyyy"),
                emailBody,
                status
                );
            return body;
        }

        #endregion
    }
}
