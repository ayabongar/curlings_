using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;
[Table]
public class ProcessOwner  : DataAccessObject
{
    public ProcessOwner(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
	public ProcessOwner()
	{
	}

	[Column(Name="OwnerId")]
	public int OwnerId {get; set;}

	[Column(Name="OwnerSID")]
	public string OwnerSID {get; set;}

	[Column(Name="ProcessId")]
	public long ProcessId {get; set;}

	[Column(Name="Timestamp")]
	public DateTime Timestamp {get; set;}

    [Column]
    public string FullName { get; set; }

}