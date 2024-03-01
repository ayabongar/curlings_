using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;

[Table]
public class LookupItem : Sars.Systems.Data.DataAccessObject
{
    public LookupItem(string procedure, Dictionary<string, object> parameters) : base(procedure, parameters)
    {
    }
    public LookupItem()
	{
	}

	[Column(Name="LookupItemId")]
	public decimal LookupItemId {get; set;}

	[Column(Name="Description")]
	public string Description {get; set;}

	[Column(Name="IsActive")]
	public bool IsActive {get; set;}

	[Column(Name="Timestamp")]
	public DateTime Timestamp {get; set;}

	[Column(Name="CreatedBy")]
	public string CreatedBy {get; set;}

	[Column(Name="LookupDataId")]
	public decimal LookupDataId {get; set;}

}

[Table]
public class HierarchyLookupItem : Sars.Systems.Data.DataAccessObject
{
    public HierarchyLookupItem(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
    public HierarchyLookupItem()
    {
    }

    [Column(Name = "LookupItemId")]
    public decimal LookupItemId { get; set; }

    [Column(Name = "Description")]
    public string Description { get; set; }

    [Column(Name = "ParentId")]
    public decimal ParentId { get; set; }

    [Column(Name = "IsActive")]
    public bool IsActive { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }

    [Column(Name = "CreatedBy")]
    public string CreatedBy { get; set; }

    [Column(Name = "LookupDataId")]
    public decimal LookupDataId { get; set; }

}