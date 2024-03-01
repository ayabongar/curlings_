using System;
using System.Data.Linq.Mapping;

[Table(Name="Regions")]
public class Regions
{
    [Column(Name="RegionId", Storage="_RegionId")]
    public int RegionId { get; set; }

    [Column(Name = "Description", Storage = "_Descrition")]
    public string Description { get; set; }

    [Column(Name="IsActive", Storage="_IsActive")]
    public bool IsActive { get; set; }

    [Column(Name="Timestamp", Storage="_Timestamp")]
    public DateTime Timestamp { get; set; }
}