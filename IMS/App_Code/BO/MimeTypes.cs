using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;
[Table]
public class MimeType : DataAccessObject
{
    public MimeType(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public MimeType(){}
	[Column(Name="MimeTypeId")]
	public int MimeTypeId {get; set;}

	[Column(Name="MimeType")]
	public string FileType {get; set;}

	[Column(Name="Extension")]
	public string Extension {get; set;}

	[Column(Name="Description")]
	public string Description {get; set;}

	[Column(Name="Timestamp")]
	public System.Nullable<DateTime> Timestamp {get; set;}

}