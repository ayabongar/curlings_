using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;

[Table]
public class UserProcess: DataAccessObject
{
    public UserProcess(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }
    public UserProcess()
	{
	}

	[Column(Name="UserProcessId")]
	public int UserProcessId {get; set;}

	[Column(Name="Timestamp")]
	public DateTime Timestamp {get; set;}

	[Column(Name="UserId")]
	public int UserId {get; set;}

	[Column(Name="ProcessId")]
	public long ProcessId {get; set;}

	[Column(Name="ProcessRoleId")]
	public int ProcessRoleId {get; set;}

    [Column(Name = "SID")]
	public string SID {get; set;}

    [Column(Name = "FullName")]
    public string FullName { get; set; }

    [Column(Name = "Role")]
    public string Role { get; set; }
}