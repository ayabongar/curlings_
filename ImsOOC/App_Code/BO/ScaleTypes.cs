using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;

[Table(Name = "ScaleTypes")]
public partial class ScaleTypes : Sars.Systems.Data.DataAccessObject
{
    public ScaleTypes(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
    public ScaleTypes()
    {
    }

    [Column(Name = "ScaleTypeId")]
    public int ScaleTypeId { get; set; }

    [Column(Name = "Description")]
    public string Description { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }

    [Column(Name = "IsActive")]
    public bool IsActive { get; set; }

}