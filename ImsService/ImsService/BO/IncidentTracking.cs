using System;

using System.Data.Linq.Mapping;
public class IncidentTracking
{

    [Column(Name = "Id")]
    public int Id { get; set; }
    [Column(Name = "IncidentID")]
    public decimal IncidentID { get; set; }
    
    [Column(Name = "EmpSID")]
    public string EmpSID { get; set; }
    [Column(Name = "NoOfNotification")]
    public int NoOfNotification { get; set; }
    [Column(Name = "EmailContent")]
    public string EmailContent { get; set; }
    [Column(Name = "ManagerNotified")]
    public bool ManagerNotified { get; set; }
    [Column(Name = "ManagerSID")]
    public string ManagerSID { get; set; }
    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }
}

public class OocIncidentTracking
{

    [Column(Name = "Id")]
    public int Id { get; set; }

    [Column(Name = "IncidentID")]
    public decimal IncidentID { get; set; }
    
    [Column(Name = "WeekNo")]
    public int WeekNo { get; set; }  

    [Column(Name = "EmpSID")]
    public string EmpSID { get; set; }

    [Column(Name = "EmpNoOfNotification")]
    public int EmpNoOfNotification { get; set; }

    [Column(Name = "ManagerSID")]
    public string ManagerSID { get; set; }


    [Column(Name = "ManagerNoOfNotification")]
    public int ManagerNoOfNotification { get; set; }


    [Column(Name = "ProcessOwnerNoOfNotification")]
    public int ProcessOwnerNoOfNotification { get; set; }


    [Column(Name = "DeputyNoOfNotification")]
    public int DeputyNoOfNotification { get; set; }
   
    [Column(Name = "EmailContent")]
    public string EmailContent { get; set; }
   
   
    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }
}

