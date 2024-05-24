using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;

[Table]
public class WorkInfoDetails: DataAccessObject
{
    public WorkInfoDetails(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
    public WorkInfoDetails()
    {
    }

    [Column(Name = "WorkInfoId")]
    public decimal WorkInfoId { get; set; }

    [Column(Name = "IncidentId")]
    public decimal IncidentId { get; set; }

    [Column(Name = "ProcessId")]
    public long ProcessId { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }

    [Column(Name = "AddedBySID")]
    public string AddedBySID { get; set; }

    [Column(Name = "Notes")]
    public string Notes { get; set; }

    [Column(Name = "CreatedBy")]
    public string CreatedBy { get; set; }
}
[Table]
public class IncidentAllocation : DataAccessObject
{
    public IncidentAllocation(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
    public IncidentAllocation()
    {
    }



    [Column(Name = "IncidentId")]
    public decimal IncidentId { get; set; }

    [Column(Name = "AssisgnedToSID")]
    public string AssisgnedToSID { get; set; }

    [Column(Name = "CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [Column(Name = "CreatedBy")]
    public string CreatedBy { get; set; }

}