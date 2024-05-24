using Sars.Systems.Data;
using Sars.Systems.Security;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace ImsService.DAL
{
    public class Repository
    {
        #region IMS Notification
        public static int Init_ServiceTracking(string status, string comment)
        {
            var oParams = new DBParamCollection
                          {
                              {"@Status", status},
                              {"@Comment", comment},
                              {"@TimeStamp", DateTime.Now},
                            
                          };

            using (
                var oCommand = new DBCommand("[dbo].[ImsService_InsertInto_ServiceLog]", QueryType.StoredProcedure, oParams, ConnectionStrings.Connection))
            {
                return oCommand.Execute();
            }
        }

        public static int Init_OOCServiceTracking(string status, string comment)
        {
            var oParams = new DBParamCollection
                          {
                              {"@Status", status},
                              {"@Comment", comment},
                              {"@TimeStamp", DateTime.Now},

                          };

            using (
                var oCommand = new DBCommand("[dbo].[ImsService_InsertInto_ServiceLog]", QueryType.StoredProcedure, oParams, ConnectionStrings.Connection))
            {
                return oCommand.Execute();
            }
        }

        public static List<Service_ProcessConfiguration> GetIMSProcessConfiguration()
        {

            using (
                var data = new RecordSet("ImsService_SELECT_IMSProcessConfiguration", QueryType.StoredProcedure, null,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<Service_ProcessConfiguration>();
                }
            }
            return null;
        }
        public static List<Service_ProcessConfiguration> GetProcessConfiguration()
        {
           
            using (
                var data = new RecordSet("ImsService_SELECT_ProcessConfiguration", QueryType.StoredProcedure, null,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<Service_ProcessConfiguration>();
                }
            }
            return null;          
        }
       
        public static List<Incidents> GetIncidentsByProcessId(int processid)
        {
            var oParams = new DBParamCollection
            {
                {"@ProcessId", processid    }
            };
            using (
                var data = new RecordSet("Service_GetIncidentByProcessId", QueryType.StoredProcedure, oParams,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<Incidents>();
                }
            }
            return null;
        }

        
        public static List<IncidentTracking> GetCountOfSentEmailsByIncidentsId(decimal incidentId)
        {
            var oParams = new DBParamCollection
            {
                {"@IncidentId", incidentId    }
            };
            using (
                var data = new RecordSet("Service_GetIncidentNoOfNotifications", QueryType.StoredProcedure, oParams,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<IncidentTracking>();
                }
            }
            return null;
        }
        public static List<IncidentTracking> Service_GetIncidentNoOfNotificationsByManSID(decimal incidentId,string manSid)
        {
            var oParams = new DBParamCollection
            {
                {"@IncidentId", incidentId    },
                {"@manSid",manSid}
            };
            using (
                var data = new RecordSet("Service_GetIncidentNoOfNotificationsByManSID", QueryType.StoredProcedure, oParams,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<IncidentTracking>();
                }
            }
            return null;
        }

        public static RecordSet Service_GetEmployeeInfoBySID(string sid)
        {
            var oParams = new DBParamCollection
            {
                {"@SID", sid    },
               
            };
            using (
                var data = new RecordSet("Select_EmployeeInfo_SID", QueryType.StoredProcedure, oParams,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data;
                }
            }
            return null;
        }

        public static List<ProcessOwner> GetProcessOwners(string processId)
        {

            var oParams = new DBParamCollection
            {
               {"@ProcessId", processId}

            };
            using (
               var data = new RecordSet("uspGetProcessOwners", QueryType.StoredProcedure, oParams,
                  DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<ProcessOwner>();
                }
            }
            return null;
        }
        public static int InsertIntoIncidentTracking(string empSID, string managerSID, int incidentId, int noOfNotification, string emailContent, bool managerNotified)
        {
            var oParams = new DBParamCollection
                          {
                              {"@IncidentID", incidentId},
                              {"@NoOfNotification", noOfNotification},
                              {"@ManagerNotified", managerNotified},
                              {"@EmailContent", emailContent},
                              {"@EmpSID", empSID},
                              {"@ManagerSID", managerSID},
                              {"@Timestamp", DateTime.Now.AddDays(-1)}
                          };

            using (
                var oCommand = new DBCommand("[dbo].[spUpSERT_IncidentTracking]", QueryType.StoredProcedure, oParams, ConnectionStrings.Connection))
            {
                return oCommand.Execute();
            }
        }

        #endregion

        #region NTQ Report


        public static List<Service_NTQR_ProcessConfiguration> GetService_NTQR_ProcessConfiguration()
        {

            using (
                var data = new RecordSet("ImsService_NTQR_SELECT_ProcessConfiguration", QueryType.StoredProcedure, null,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<Service_NTQR_ProcessConfiguration>();
                }
            }
            return null;
        }
        public static List<Service_NTQR_ServiceTracker> ImsService_NTQR_SELECT_ServiceTracker(int fk_NTQR_ServiceConfig_ID, int fk_NTQ_Report_ID, string notifierName,
            DateTime Q1NotifiedDate)
        {
            var sid = notifierName.Split('|');
            var user = SarsUsers.SearchADUsersBySID(sid[0].Trim());
            var emailAddress = user != null ? user[0].Mail : string.Empty;

            var oParams = new DBParamCollection
            {
                {"@fk_NTQR_ServiceConfig_ID", fk_NTQR_ServiceConfig_ID    },
                {"@fk_NTQ_Report_ID", fk_NTQ_Report_ID    },
                {"@Q1NotifiedDate", Q1NotifiedDate    },
                {"@Email", emailAddress    }
            };
            using (
                var data = new RecordSet("ImsService_NTQR_SELECT_ServiceTracker", QueryType.StoredProcedure, oParams,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<Service_NTQR_ServiceTracker>();
                }
            }
            return null;
        }

        public static List<Service_NTQR_ServiceTracker> CheckIfCompilerNotified(int fk_NTQR_ServiceConfig_ID, int fk_KeyResult,
         DateTime Q1NotifiedDate, int userId)
        {
            var oParams = new DBParamCollection
            {
                {"@fk_NTQR_ServiceConfig_ID", fk_NTQR_ServiceConfig_ID    },
                {"@KeyResult_ID", fk_KeyResult    },
                {"@Q1NotifiedDate", Q1NotifiedDate    },
                 {"@UserId", userId    }
            };
            using (
                var data = new RecordSet("ImsService_NTQR_SELECT_CompilerServiceTracker", QueryType.StoredProcedure, oParams,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<Service_NTQR_ServiceTracker>();
                }
            }
            return null;
        }

        public static List<ImsService_NTQR_Report_Notifier> ImsService_NTQR_Report_Notifier(int StatusId, int quarterId)
        {
            var oParams = new DBParamCollection
            {
                {"@StatusId", StatusId    },
                {"@QuarterId", quarterId    },

            };
            using (
                var data = new RecordSet("ImsService_NTQR_Report_Notifier", QueryType.StoredProcedure, oParams,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<ImsService_NTQR_Report_Notifier>();
                }
            }
            return null;
        }


        public static List<ImsService_NTQR_Report_Notifier> ImsService_NTQR_PeopleDidntDeclare(int quarterId)
        {
            var oParams = new DBParamCollection
            {

                {"@QuarterId", quarterId    },

            };
            using (
                var data = new RecordSet("ImsService_NTQR_PeopleDidntDeclare", QueryType.StoredProcedure, oParams,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<ImsService_NTQR_Report_Notifier>();
                }
            }
            return null;
        }

        public static List<ImsService_NTQR_Report_Notifier> ImsService_NTQ_GetCompilersNotifier()
        {

            using (
                var data = new RecordSet("ImsService_NTQR_NotifyCompilersToCreateReports", QueryType.StoredProcedure, null,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<ImsService_NTQR_Report_Notifier>();
                }
            }
            return null;
        }


        public static List<ImsService_NTQR_KroOrAnchorUser> ImsService_NTQR_GetUserByKeyAndRoleId(int roleId, int keyResultId)
        {
            var oParams = new DBParamCollection
            {
                {"@RoleId", roleId},
                {"@KeyResultId", keyResultId},
            };

            using (
                var data = new RecordSet("ImsService_NTQR_GetUserByKeyAndRoleId", QueryType.StoredProcedure, oParams,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<ImsService_NTQR_KroOrAnchorUser>();
                }
            }
            return null;
        }

        public static int NTQR_Insert_ServiceTracker(int fk_NTQR_ServiceConfig_ID, int fk_NTQ_Report_ID, DateTime NotifiedDate, string Comment, int UserId = 0, int fk_KeyResultId = 0,
            string SentBy = null, string EmailBody = null, string SentTo = null, string EmailAddress = null, string Subject = null)
        {
            var oParams = new DBParamCollection
                          {
                              {"@fk_NTQR_ServiceConfig_ID", fk_NTQR_ServiceConfig_ID},
                              {"@fk_NTQ_Report_ID", fk_NTQ_Report_ID},
                              {"@NotifiedDate", NotifiedDate},
                              {"@Comment", Comment},
                              {"@UserId", UserId},
                              {"@fk_KeyResultId", fk_KeyResultId},
                              {"@SentBy", SentBy},
                              {"@EmailBody", EmailBody},
                              {"@SentTo", SentTo},
                              {"@EmailAddress",EmailAddress },
                              {"@Subject",Subject },

                          };

            using (
                var oCommand = new DBCommand("[dbo].[ImsService_NTQR_Insert_ServiceTracker]", QueryType.StoredProcedure, oParams, ConnectionStrings.Connection))
            {
                return oCommand.Execute();
            }
        }

        public static List<NTRQ_User> GetNTQR_UserBySID(string sid)
        {
            var oParams = new DBParamCollection
            {
                {"@SID", sid    }
            };
            using (
                var data = new RecordSet("GetNTQR_UserBySID", QueryType.StoredProcedure, oParams,
                   DAL.ConnectionStrings.ConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<NTRQ_User>();
                }
            }
            return null;
        }

        #endregion

        #region OOC notification

        public static List<ProcessOwner> GetOocProcessOwners(string processId)
        {

            var oParams = new DBParamCollection
            {
               {"@ProcessId", processId}

            };
            using (
               var data = new RecordSet("uspGetProcessOwners", QueryType.StoredProcedure, oParams,
                  DAL.ConnectionStrings.OocConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<ProcessOwner>();
                }
            }
            return null;
        }
        public static List<Service_OOCProcessConfiguration> GetOocProcessConfiguration()
        {

            using (
                var data = new RecordSet("ImsService_SELECT_ProcessConfigurationValues", QueryType.StoredProcedure, null,
                   DAL.ConnectionStrings.OocConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<Service_OOCProcessConfiguration>();
                }
            }
            return null;
        }
        public static List<Incidents> GetOocIncidentsByProcessId(int processid)
        {
            var oParams = new DBParamCollection
            {
                {"@ProcessId", processid    }
            };
            using (
                var data = new RecordSet("usp_Service_OpenIncidentsByProcessId", QueryType.StoredProcedure, oParams,
                   DAL.ConnectionStrings.OocConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<Incidents>();
                }
            }
            return null;
        }
        public static List<OocIncidentTracking> GetOocCountOfSentEmailsByIncidentsId(decimal incidentId)
        {
            var oParams = new DBParamCollection
            {
                {"@IncidentId", incidentId    }
            };
            using (
                var data = new RecordSet("usp_Service_IncidentTrackingById", QueryType.StoredProcedure, oParams,
                   DAL.ConnectionStrings.OocConnectionString))
            {
                if (data.HasRows)
                {
                    return data.Tables[0].ToList<OocIncidentTracking>();
                }
            }
            return null;
        }

        public static int InsertIntoOocIncidentTracking(decimal incidentId, int weekNo, string empSID, int noOfNotification,  string managerSID,
            int managerNoOfNotification,int processOwnerNoOfNotification, int deputyNoOfNotification, string emailContent, bool managerNotified)
        {
            var oParams = new DBParamCollection
                          {
                              {"@IncidentID", incidentId},
                              {"@EmpSID", empSID},
                              {"@WeekNo",weekNo },
                              {"@NoOfNotification", noOfNotification},
                              {"@ManagerSID", managerSID},
                              {"@ManagerNoOfNotification", managerNoOfNotification},
                              {"@ProcessOwnerNoOfNotification", processOwnerNoOfNotification},
                              {"@DeputyNoOfNotification", deputyNoOfNotification},
                              {"@EmailContent", emailContent},                           
                           
                          };

            using (
                var oCommand = new DBCommand("[dbo].[spUpSERT_IncidentTracking]", QueryType.StoredProcedure, oParams, ConnectionStrings.OocConnection))
            {
                return oCommand.Execute();
            }
        }

        #endregion
    }
}
