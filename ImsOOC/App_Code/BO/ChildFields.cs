using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;

[Table]
public class ChildField : DataAccessObject
{
    public ChildField(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public ChildField()
    {
    }

    [Column(Name = "ParentId")]
    public int ParentId { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }

    [Column(Name = "ProcessId")]
    public long ProcessId { get; set; }

    [Column(Name = "FieldId")]
    public decimal FieldId { get; set; }

    [Column(Name = "ParentOptionId")]
    public decimal ParentOptionId { get; set; }

    [Column(Name = "AddedBy")]
    public string AddedBy { get; set; }

    [Column(Name = "ParentFieldId")]
    public decimal ParentFieldId { get; set; }

}