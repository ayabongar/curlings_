using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SelectedHierarchicalDetails
/// </summary>
public class SelectedHierarchicalDetails
{
	public SelectedHierarchicalDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public decimal FirstLevel { get; set; }
    public decimal? SecondLevel { get; set; }
    public decimal? ThirdLevel { get; set; }
}