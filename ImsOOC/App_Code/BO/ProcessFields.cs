using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;

[Table]
public partial class ProcessField : DataAccessObject
{
    public ProcessField(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public ProcessField()
    {
    }

    [Column(Name = "ProcessFieldId")]
    public decimal FieldId { get; set; }

    [Column(Name = "FieldName")]
    public string FieldName { get; set; }

    [Column(Name = "Display")]
    public string Display { get; set; }

    [Column(Name = "ProcessId")]
    public long ProcessId { get; set; }

    [Column(Name = "FieldTypeId")]
    public int FieldTypeId { get; set; }

    [Column(Name = "SortOrder")]
    public int? SortOrder { get; set; }

    [Column(Name = "IsActive")]
    public bool IsActive { get; set; }

    [Column(Name = "AddToCoverPage")]
    public bool AddToCoverPage { get; set; }
   
    [Column(Name = "ShowOnAssigned")]
    public bool ShowOnAssigned { get; set; }

    [Column(Name = "IsParent")]
    public bool IsParent { get; set; }

    [Column(Name = "IsChild")]
    public bool IsChild { get; set; }

    [Column(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }

    [Column(Name = "IsRequired")]
    public bool IsRequired { get; set; }

    [Column(Name = "ScaleTypeId")]
    public int? ScaleTypeId { get; set; }

    [Column(Name = "MatrixDimensionId")]
    public int? MatrixDimensionId { get; set; }

    [Column(Name = "ValidationTypeId")]
    public int? ValidationTypeId { get; set; }

    [Column(Name = "LookupDataId")]
    public decimal LookupDataId { get; set; }

    [Column(Name = "HierarchyLookupId")]
    public decimal? HierarchyLookupId { get; set; }

    [Column(Name = "ShowOnSearch")]
    public bool? ShowOnSearch { get; set; }

    [Column(Name = "ShowOnScreen")]
    public bool? ShowOnScreen { get; set; }

    [Column(Name = "ShowOnReport")]
    public bool? ShowOnReport { get; set; }

    [Column(Name = "AddedBySID")]
    public string AddedBySID { get; set; }

    [Column(Name = "LastModifiedBySID")]
    public string LastModifiedBySID { get; set; }

    [Column(Name = "LastModifiedDate")]
    public DateTime? LastModifiedDate { get; set; }

    [Column(Name = "FieldType")]
    public string FieldType { get; set; }

    [Column(Name = "ParentId")]
    public decimal? ParentId { get; set; }

    [Column(Name = "CanSendEmail")]
    public bool CanSendEmail { get; set; }

    [Column(Name = "EmailContent")]
    public string EmailContent { get; set; }

    [Column(Name = "DefaultCCPersonSID")]
    public string DefaultCCPersonSID { get; set; }

}