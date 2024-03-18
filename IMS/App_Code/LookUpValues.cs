using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LookUpValues
/// </summary>


[Table(Name = "Lookup_Items")]
public class ResearchLookUpValue : Sars.Systems.Data.DataAccessObject
{
     public ResearchLookUpValue(string procedure, Dictionary<string, object> parameters) : base(procedure, parameters)
    {
    }
     public ResearchLookUpValue()
	{
	}
    [Column(Name = "Id")]
    public int Id { get; set; }

    [Column(Name = "TableName")]
    public string TableName { get; set; }

    [Column(Name = "Description")]
    public string Description { get; set; }

    [Column(Name = "Value")]
    public int Value { get; set; }
}

[Table(Name = "LookupQuarters")]
public class LookupQuarters : Sars.Systems.Data.DataAccessObject
{
    public LookupQuarters(string procedure, Dictionary<string, object> parameters) : base(procedure, parameters)
    {
    }
    public LookupQuarters()
    {
    }
    [Column(Name = "Id")]
    public int Id { get; set; }

    [Column(Name = "Name")]
    public string Name { get; set; }

    [Column(Name = "timestamp")]
    public DateTime timestamp { get; set; }

  
}

[Table(Name = "LookupKeyResult")]
public class LookupKeyResult : Sars.Systems.Data.DataAccessObject
{
    public LookupKeyResult(string procedure, Dictionary<string, object> parameters) : base(procedure, parameters)
    {
    }
    public LookupKeyResult()
    {
    }
    [Column(Name = "Id")]
    public int Id { get; set; }
    [Column(Name = "fk_StrategicObjective_ID")]
    public int fk_StrategicObjective_ID { get; set; }

    [Column(Name = "Name")]
    public string Name { get; set; }
    [Column(Name = "Description")]
    public string Description { get; set; }

    [Column(Name = "timestamp")]
    public DateTime timestamp { get; set; }

    [Column(Name = "CreatedBy")]
    public int CreatedBy { get; set; }

    [Column(Name = "ModifiedBy")]
    public int ModifiedBy { get; set; }
    [Column(Name = "ModifiedDate")]
    public DateTime ModifiedDate { get; set; }

    


}

[Table(Name = "DIT")]
public class DIT : Sars.Systems.Data.DataAccessObject
{
    public DIT(string procedure, Dictionary<string, object> parameters) : base(procedure, parameters)
    {
    }
    public DIT()
    {
    }
    [Column(Name = "Id")]
    public int Id { get; set; }
    [Column(Name = "fk_StrategicObjective_ID")]
    public int fk_StrategicObjective_ID { get; set; }

    [Column(Name = "Name")]
    public byte[] Name { get; set; }
    [Column(Name = "Description")]
    public string Description { get; set; }

    [Column(Name = "timestamp")]
    public DateTime timestamp { get; set; }

    [Column(Name = "CreatedBy")]
    public int CreatedBy { get; set; }

    [Column(Name = "ModifiedBy")]
    public int ModifiedBy { get; set; }
    [Column(Name = "ModifiedDate")]
    public DateTime ModifiedDate { get; set; }




}




