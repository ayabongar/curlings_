using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;

[Table]
public class ServiceConfig : DataAccessObject
{
    public ServiceConfig(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public ServiceConfig()
    {
    }

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