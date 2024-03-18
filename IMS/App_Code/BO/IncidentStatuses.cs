using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;
[Table]
public partial class IncidentStatus: DataAccessObject
{
    public IncidentStatus(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
	public IncidentStatus()
	{
	}

	[Column(Name="IncidentStatusId")]
	public int IncidentStatusId {get; set;}

	[Column(Name="Description")]
	public string Description {get; set;}

	[Column(Name="Timestamp")]
	public DateTime Timestamp {get; set;}

	[Column(Name="IsActive")]
	public bool IsActive {get; set;}

}