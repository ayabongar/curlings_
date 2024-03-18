using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Collections.Generic;
using Sars.Systems.Data;

[Table]
public class SurveyLookups : DataAccessObject
{
    public SurveyLookups(string procedure, Dictionary<string, object> parameters) : base(procedure, parameters) 
    {
    }
    public SurveyLookups()
	{
	}

	[Column(Name="LookupDataId")]
	public decimal LookupDataId {get; set;}

	[Column(Name="Description")]
	public string Description {get; set;}

	[Column(Name="IsActive")]
	public bool IsActive {get; set;}

	[Column(Name="Timestamp")]
	public DateTime Timestamp {get; set;}

	[Column(Name="CrearedBy")]
	public string CrearedBy {get; set;}

	[Column(Name="LastModifiedBy")]
	public string LastModifiedBy {get; set;}

	[Column(Name="LastModifiedDate")]
	public DateTime LastModifiedDate {get; set;}

}

[Table]
public class SurveyHierarchyLookups : DataAccessObject
{
    public SurveyHierarchyLookups(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public SurveyHierarchyLookups()
    {
    }

    [Column(Name = "LookupDataId")]
    public decimal LookupDataId { get; set; }

    [Column(Name = "Description")]
    public string Description { get; set; }

    [Column(Name = "ParentId")]
    public decimal ParentId { get; set; }

    [Column(Name = "IsActive")]
    public bool IsActive { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }

    [Column(Name = "CrearedBy")]
    public string CrearedBy { get; set; }

    [Column(Name = "LastModifiedBy")]
    public string LastModifiedBy { get; set; }

    [Column(Name = "LastModifiedDate")]
    public DateTime LastModifiedDate { get; set; }

}