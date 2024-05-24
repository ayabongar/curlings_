using System;
using System.Data.Linq.Mapping;


    public class Service_ProcessConfiguration
    {
        [Column(Name = "Id")]
        public int Id { get; set; }

        [Column(Name = "ProcessId")]
        public int ProcessId { get; set; }

        [Column(Name = "NotifyUsers")]
        public bool NotifyUsers { get; set; }

        [Column(Name = "Users_NoOfNotification")]
        public int Users_NoOfNotification { get; set; }

        [Column(Name = "EscalateToManagers")]
        public bool EscalateToManagers { get; set; }

        [Column(Name = "MngNoOfNotifications")]
        public int MngNoOfNotifications { get; set; }

        [Column(Name = "EscalateToMngAfterXNoOfNotify")]
        public int EscalateToMngAfterXNoOfNotify { get; set; }

        [Column(Name = "ManagerSID")]
        public string ManagerSID { get; set; }

        [Column(Name = "EscalateToProcessOwners")]
        public bool EscalateToProcessOwners { get; set; }

        [Column(Name = "IsProServer")]
        public bool IsProServer { get; set; }

        [Column(Name = "EscalateToManagerEveryInDays")]
        public int EscalateToManagerEveryInDays { get; set; }

        [Column(Name = "TestEmailsGoTo")]
        public string TestEmailsGoTo { get; set; }
    }


public class Service_NTQR_ProcessConfiguration
{
    [Column(Name = "Id")]
    public int Id { get; set; }

    [Column(Name = "Description")]
    public string Description { get; set; }

    [Column(Name = "Q1Date")]
    public DateTime Q1Date { get; set; }

    [Column(Name = "Q2Date")]
    public DateTime Q2Date { get; set; }

    [Column(Name = "Q3Date")]
    public  DateTime Q3Date { get; set; }

    [Column(Name = "Q4Date")]
    public DateTime Q4Date { get; set; }

    [Column(Name = "AnnualDate")]
    public DateTime AnnualDate { get; set; }

    [Column(Name = "EmailMocks")]
    public string EmailMocks { get; set; }

    [Column(Name = "isActive")]
    public bool isActive { get; set; }

    [Column(Name = "testEmail")]
    public string testEmail { get; set; }

    [Column(Name = "isProd")]
    public bool isProd { get; set; }
}

public class Service_NTQR_ServiceTracker
{
    [Column(Name = "Id")]
    public int Id { get; set; }

    [Column(Name = "fk_NTQR_ServiceConfig_ID")]
    public int fk_NTQR_ServiceConfig_ID { get; set; }
    [Column(Name = "fk_KeyResultId")]
    public int fk_KeyResultId { get; set; }
    [Column(Name = "UserId")]
    public int UserId { get; set; }


    [Column(Name = "fk_NTQ_Report_ID")]
    public int fk_NTQ_Report_ID { get; set; }

    [Column(Name = "Comment")]
    public string Comment { get; set; }

    [Column(Name = "NotifiedDate")]
    public DateTime NotifiedDate { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }   
}

public class ImsService_NTQR_KroOrAnchorUser
{
    [Column(Name = "UserId")]
    public int UserId { get; set; }
    [Column(Name = "UserRoleID")]
    public int UserRoleID { get; set; }

    [Column(Name = "RoleName")]
    public string RoleName { get; set; }

    [Column(Name = "UserFullName")]
    public string UserFullName { get; set; }

    [Column(Name = "Unit")]
    public string Unit { get; set; }


    [Column(Name = "KeyResultID")]
    public int KeyResultID { get; set; }

    [Column(Name = "KeyResult")]
    public string KeyResult { get; set; }

}


public class ImsService_NTQR_Report_Notifier
{
    [Column(Name = "Id")]
    public int Id { get; set; }
    [Column(Name = "fk_ReportKeyResult_ID")]
    public int fk_ReportKeyResult_ID { get; set; }

    [Column(Name = "Compiler")]
    public string Compiler { get; set; }

    [Column(Name = "KeyResultOwner")]
    public string KeyResultOwner { get; set; }

    [Column(Name = "Anchor")]
    public string Anchor { get; set; }

    [Column(Name = "QuarterName")]
    public string QuarterName { get; set; }

    [Column(Name = "IncidentID")]
    public decimal IncidentID { get; set; }

    [Column(Name = "ProcessId")]
    public Int64 ProcessId { get; set; }
    [Column(Name = "AssignedToSID")]
    public string AssignedToSID { get; set; }

    [Column(Name = "IncidentStatus")]
    public string IncidentStatus { get; set; }
    [Column(Name = "ReferenceNumber")]
    public string ReferenceNumber { get; set; }
    [Column(Name = "Summary")]
    public string Summary { get; set; }

    [Column(Name = "KeyResultID")]
    public int KeyResultID { get; set; }

    [Column(Name = "KeyResult")]
    public string KeyResult { get; set; }

}


public class NTRQ_User 
{
    

    [Column(Name = "ID")]
    public int ID { get; set; }

    [Column(Name = "RoleId")]
    public int RoleId { get; set; }

    [Column(Name = "UserId")]
    public int UserId { get; set; }

    [Column(Name = "UserCode")]
    public string UserCode { get; set; }
    [Column(Name = "RoleName")]

    public string RoleName { get; set; }

    [Column(Name = "UserFullName")]
    public string UserFullName { get; set; }
    [Column(Name = "IsActive")]
    public bool IsActive { get; set; }
    [Column(Name = "Signature")]
    public byte[] Signature { get; set; }
    [Column(Name = "CreatedBy")]
    public int CreatedBy { get; set; }

    [Column(Name = "fk_User_UnitId")]
    public int fk_User_UnitId { get; set; }

    [Column(Name = "CreatedDate")]
    public DateTime CreatedDate { get; set; }

}

public class ProcessOwner 
{
   

    [Column(Name = "OwnerId")]
    public int OwnerId { get; set; }

    [Column(Name = "OwnerSID")]
    public string OwnerSID { get; set; }

    [Column(Name = "ProcessId")]
    public long ProcessId { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }

    [Column]
    public string FullName { get; set; }

}



public class Service_OOCProcessConfiguration
{
    [Column(Name = "Id")]
    public int Id { get; set; }

    [Column(Name = "ProcessId")]
    public int ProcessId { get; set; }

    [Column(Name = "NotifyUsers")]
    public bool NotifyUsers { get; set; }

    [Column(Name = "Users_NoOfNotification")]
    public int Users_NoOfNotification { get; set; }

    [Column(Name = "EscalateToManagers")]
    public bool EscalateToManagers { get; set; }

    [Column(Name = "MngNoOfNotifications")]
    public int MngNoOfNotifications { get; set; }

    [Column(Name = "EscalateToProcessOwners")]
    public bool EscalateToProcessOwners { get; set; }

    [Column(Name = "EscalateToDeputyCom")]
    public bool EscalateToDeputyCom { get; set; }

    [Column(Name = "DeputyComNoOfNotify")]
    public int DeputyComNoOfNotify { get; set; }

    [Column(Name = "DuputyComEmail")]
    public string DuputyComEmail { get; set; }

    [Column(Name = "IsProServer")]
    public bool IsProServer { get; set; }

    [Column(Name = "EscalateToManagerEveryInDays")]
    public int EscalateToManagerEveryInDays { get; set; }

    [Column(Name = "TestEmailsGoTo")]
    public string TestEmailsGoTo { get; set; }

    [Column(Name = "ReminderInterval")]
    public int ReminderInterval { get; set; }
}

