using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;

[Table(Name = "FieldTypes")]
public class IncidentEmailLog : DataAccessObject
{
    public IncidentEmailLog(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
	public IncidentEmailLog()
	{
	}

	[Column(Name="EmailLogId")]
	public decimal EmailLogId {get; set;}

	[Column(Name="Timestamp")]
	public DateTime Timestamp {get; set;}

	[Column(Name="SentBy")]
	public string SentBy {get; set;}

	[Column(Name="EmailBody")]
	public string EmailBody {get; set;}

	[Column(Name="SentTo")]
	public string SentTo {get; set;}

	[Column(Name="EmailAddress")]
	public string EmailAddress {get; set;}

	[Column(Name="Subject")]
	public string Subject {get; set;}

	[Column(Name="IncidentId")]
	public decimal IncidentId {get; set;}

	[Column(Name="ProcessId")]
	public long ProcessId {get; set;}

}