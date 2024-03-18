using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;

[Table(Name = "FieldTypes")]
public class FieldType : DataAccessObject
{
    public FieldType(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
    public FieldType()
    {
    }

    [Column(Name = "FieldTypeId")]
    public int FieldTypeId { get; set; }

    [Column(Name = "Description")]
    public string Description { get; set; }

    [Column(Name = "IsActive")]
    public bool IsActive { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }

}