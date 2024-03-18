using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;

[Table(Name = "MultichoiceOptions")]
public class MultichoiceOption : DataAccessObject
{
    public MultichoiceOption(string procedurename, Dictionary<string, object> parameters)
        : base(procedurename, parameters)
    {
    }

    public MultichoiceOption()
    {
    }

    [Column(Name = "MultichoiceOptionId")]
    public Decimal MultichoiceOptionId { get; set; }

    [Column(Name = "OptionDescription")]
    public string OptionDescription { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }

    [Column(Name = "FieldId")]
    public decimal FieldId { get; set; }

    [Column(Name = "ProcessId")]
    public int ProcessId { get; set; }
}