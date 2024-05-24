using System;
using System.Data.Linq.Mapping;
public class Incidents
{

    [Column(Name = "IncidentID")]
    public decimal IncidentID { get; set; }

    [Column(Name = "IncidentNumber")]
    public string IncidentNumber { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }

    [Column(Name = "CreatedBySID")]
    public string CreatedBySID { get; set; }


    [Column(Name = "RegisteredBy")]
    public string RegisteredBy { get; set; }


    [Column(Name = "LastModifiedBySID")]
    public string LastModifiedBySID { get; set; }

    [Column(Name = "LastModifiedDate")]
    public DateTime? LastModifiedDate { get; set; }

    [Column(Name = "AssignedToSID")]
    public string AssignedToSID { get; set; }
    [Column(Name = "SecondAssignedSID")]
    public string SecondAssignedSID { get; set; }

    [Column(Name = "IncidentStatusId")]
    public int? IncidentStatusId { get; set; }

    [Column(Name = "IncidentStatus")]
    public string IncidentStatus { get; set; }

    [Column(Name = "ProcessId")]
    public long ProcessId { get; set; }

    [Column(Name = "CustomerSID")]
    public string CustomerSID { get; set; }

    [Column(Name = "Resolution")]
    public string Resolution { get; set; }

    [Column(Name = "CrossRefNo")]
    public string CrossRefNo { get; set; }

    [Column(Name = "DueDate")]
    public DateTime? DueDate { get; set; }

    [Column(Name = "SLADate")]
    public DateTime? SLADate { get; set; }

    [Column(Name = "DateCompleted")]
    public DateTime? DateCompleted { get; set; }

    [Column(Name = "DateClosed")]
    public DateTime? DateClosed { get; set; }

    [Column(Name = "DateRegistered")]
    public DateTime DateRegistered { get; set; }

    [Column(Name = "DataFields")]
    public String DataFields { get; set; }

   

    [Column(Name = "AssignedToFullName")]
    public String AssignedToFullName { get; set; }

    [Column(Name = "ReferenceNumber")]
    public String ReferenceNumber { get; set; }

    [Column(Name = "Summary")]
    public string Summary { get; set; }

    [Column(Name = "ProcessName")]
    public string ProcessName { get; set; }

    [Column(Name = "Subject")]
    public string Subject { get; set; }

    [Column(Name = "SLADateAgreedExpectedTAT")]
    public string SLADateAgreedExpectedTAT { get; set; }

    [Column(Name = "CommentsReasonForUpdatingSLA")]
    public string CommentsReasonForUpdatingSLA { get; set; }

    [Column(Name = "WeekNo")]
    public int WeekNo { get; set; }


}

