using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;

[Table]
[ReadManyProcedure( "uspREAD_ProcessRoles")]
public class ProcessRole: DataAccessObject
{
    public ProcessRole(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public ProcessRole(){}

	[Column(Name="RoleID")]
	public int RoleID {get; set;}

	[Column(Name="Description")]
	public string Description {get; set;}

	[Column(Name="Timestamp")]
	public DateTime Timestamp {get; set;}

	[Column(Name="IsActive")]
	public bool IsActive {get; set;}

}