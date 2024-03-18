using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;

[Table(Name = "TextValidations")]
public partial class TextValidations: DataAccessObject
{
    public TextValidations(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
	public TextValidations()
	{
	}

	[Column(Name="ValidationtypeId")]
	public int ValidationtypeId {get; set;}

	[Column(Name="Description")]
	public string Description {get; set;}

	[Column(Name="IsActive")]
	public bool IsActive {get; set;}

	[Column(Name="Timestamp")]
	public System.Nullable<DateTime> Timestamp {get; set;}

}