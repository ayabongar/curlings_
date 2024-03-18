using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sars.Systems.Data;
using System.Data.Linq.Mapping;

/// <summary>
/// Summary description for PowerUser
/// </summary>
[Table]
public class PowerUser: DataAccessObject
{
    public PowerUser(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
	public PowerUser()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [Column]
    public string SID { get; set; }
    [Column]
    public string FullName { get; set; }
}

