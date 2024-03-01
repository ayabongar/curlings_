using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;

[Table]
public partial class IncidentDocument: DataAccessObject
{
    public IncidentDocument(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
    public IncidentDocument()
	{
	}

	[Column(Name="Id")]
	public decimal Id {get; set;}

	[Column(Name="DocId")]
	public System.Guid DocId {get; set;}

	[Column(Name="Timestamp")]
	public DateTime Timestamp {get; set;}

	[Column(Name="IncidentId")]
	public decimal IncidentId {get; set;}

	[Column(Name="DocumentName")]
	public string DocumentName {get; set;}

	[Column(Name="AddedBySID")]
	public string AddedBySID {get; set;}

	[Column(Name="Extension")]
	public string Extension {get; set;}

	[Column(Name="WorkInfoId")]
	public decimal WorkInfoId {get; set;}

	[Column(Name="FilePath")]
	public string FilePath {get; set;}

    [Column(Name = "FullName")]
    public string FullName { get; set; }


}