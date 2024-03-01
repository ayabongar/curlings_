using System;
using System.Data.Linq.Mapping;

[Table(Name = "ProcessUsers")]
public class ProcessUsers : ADUser
{
    [Column(Name = "UserId")]
    public int UserId { get; set; }

    [Column(Name = "IsActive")]
    public bool IsActive { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }
}

public class ADUser
{
    [Column(Name = "SID")]
    public string SID { get; set; }

    [Column(Name = "FirstName")]
    public string Name { get; set; }

    [Column(Name = "Surname")]
    public string Surname { get; set; }

    [Column(Name = "EmailAddress")]
    public string Mail { get; set; }

    [Column(Name = "Telephone")]
    public string Telephone { get; set; }

    [Column(Name = "FullName")]
    public string FullName { get; set; }

    public bool FoundBySID { get; set; }
}