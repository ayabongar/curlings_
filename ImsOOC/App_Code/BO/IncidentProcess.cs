using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Linq;
using Sars.Systems.Data;

[Table(Name = "Processes")]
public class IncidentProcess : DataAccessObject
{
    public IncidentProcess(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public IncidentProcess()
    {
    }

    [Column(Name = "ProcessId")]
    public long ProcessId { get; set; }

    [Column(Name = "Description")]
    public string Description { get; set; }
    [Column(Name = "ProcessName")]
    public string ProcessName { get; set; }

    [Column(Name = "Publish")]
    public bool Publish { get; set; }

    [Column(Name = "CreatedBySID")]
    public string CreatedBySID { get; set; }

    [Column(Name = "IsActive")]
    public bool IsActive { get; set; }
   
    [Column(Name = "AddCoverPage")]
    public bool AddCoverPage { get; set; }
   
    [Column(Name = "ReAssignToCreater")]
    public bool ReAssignToCreater { get; set; }

    [Column(Name = "CanWorkOnOneCase")]
    public bool CanWorkOnOneCase { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }

    [Column(Name = "LastModifiedDate")]
    public DateTime? LastModifiedDate { get; set; }

    [Column(Name = "LastModifiedBy")]
    public string LastModifiedBy { get; set; }

    [Column(Name = "StatusId")]
    public int StatusId { get; set; }


    [Column(Name = "ProcessStatus")]
    public string ProcessStatus { get; set; }

    [Column(Name = "Prefix")]
    public string Prefix { get; set; }

    [Column(Name = "MaxFileSize")]
    public decimal MaxFileSize { get; set; }

    [Column(Name = "ProcessVersion")]
    public decimal Version { get; set; }

    [Column(Name = "WorkingUrl")]
    public string WorkingUrl { get; set; }

    public int FieldCount
    {
        get { return IncidentTrackingManager.GetProcessFieldCount(ProcessId.ToString(CultureInfo.InvariantCulture)); }
    }

    public List<ProcessOwner> Owners
    {
        get { return IncidentTrackingManager.GetProcessOwners(ProcessId.ToString(CultureInfo.InvariantCulture)); }
    }

    public List<UserProcess> Administrators
    {
        get
        {
            var pUsers = Users;
            if (pUsers != null && pUsers.Any())
            {
                return pUsers.FindAll(pUser => pUser.ProcessRoleId == 1);
            }
            return null;
        }
    }

    public List<UserProcess> PowerUsers
    {
        get
        {
            var pUsers = Users;
            if (pUsers != null && pUsers.Any())
            {
                return pUsers.FindAll(pUser => pUser.ProcessRoleId == 3);
            }
            return null;
        }
    }
    public List<UserProcess> NormalUsers
    {
        get
        {
            var pUsers = Users;
            if (pUsers != null && pUsers.Any())
            {
                return pUsers.FindAll(pUser => pUser.ProcessRoleId == 2);
            }
            return null;
        }
    }

    public List<UserProcess> Users
    {
        get { return IncidentTrackingManager.GetUsersAssigneToThisProcess(ProcessId.ToString(CultureInfo.InvariantCulture)); }
    }

    public List<Incident> Incidents
    {
        get { return IncidentTrackingManager.GetProcessIncidents(ProcessId.ToString(CultureInfo.InvariantCulture)); }
    }

}