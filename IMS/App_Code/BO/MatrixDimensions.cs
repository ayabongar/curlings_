using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;
[Table(Name = "MatrixDimensions")]
public partial class MatrixDimensions : DataAccessObject
{
    public MatrixDimensions(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
	public MatrixDimensions()
	{
	}

	[Column(Name="MatrixDimensionId")]
	public decimal MatrixDimensionId {get; set;}

	[Column(Name="LeftHeader")]
	public string LeftHeader {get; set;}

	[Column(Name="RighHeader")]
	public string RighHeader {get; set;}

	[Column(Name="Dimensions")]
	public string Dimensions {get; set;}

	[Column(Name="IsActive")]
	public System.Nullable<bool> IsActive {get; set;}

	[Column(Name="Timestamp")]
	public System.Nullable<DateTime> Timestamp {get; set;}

}