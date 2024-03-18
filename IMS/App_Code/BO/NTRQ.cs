using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Sars.Systems.Data;


[Table]
public class NTQ_Data : DataAccessObject
{
    public NTQ_Data(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public NTQ_Data()
    {
    }

    [Column(Name = "KeyResultId")]
    public int KeyResultId { get; set; }
    [Column(Name = "fk_Report_Objectives_ID")]
    public int fk_Report_Objectives_ID { get; set; }
    
    [Column(Name = "IncidentNumber")] public string IncidentNumber { get; set; }
    [Column(Name = "IncidentStatusId")] public int IncidentStatusId { get; set; }
    [Column(Name = "StatusType")] public string StatusType { get; set; }
    [Column(Name = "ReferenceNumber")] public string ReferenceNumber { get; set; }
    [Column(Name = "IncidentDate")] public DateTime IncidentDate { get; set; }
    [Column(Name = "CreatedDate")] public DateTime CreatedDate { get; set; }
    [Column(Name = "StrategicObjective")] public string StrategicObjective { get; set; }

    [Column(Name = "CFY")] public string CFY { get; set; }
    [Column(Name = "fk_Lookup_KeyResult_ID")] public int fk_Lookup_KeyResult_ID { get; set; }
    [Column(Name = "KeyResult")] public string KeyResult { get; set; }
    [Column(Name = "fk_KeyResultIndicator_ID")] public int fk_KeyResultIndicator_ID { get; set; }
    [Column(Name = "KeyResultIndicator")] public string KeyResultIndicator { get; set; }
    [Column(Name = "Anchor")] public string Anchor { get; set; }
    [Column(Name = "KeyResultOwner")] public string KeyResultOwner { get; set; }
    [Column(Name = "AnnualTarget")] public string AnnualTarget { get; set; }
    [Column(Name = "Q1_Target")] public string Q1_Target { get; set; }
    [Column(Name = "Q2_Target")] public string Q2_Target { get; set; }
    [Column(Name = "Q3_Target")] public string Q3_Target { get; set; }
    [Column(Name = "Q4_Target")] public string Q4_Target { get; set; }
    [Column(Name = "TID")] public string TID { get; set; }
    [Column(Name = "CreatedBySID")] public string CreatedBySID { get; set; }
    [Column(Name = "AssignedToSID")] public string AssignedToSID { get; set; }
    [Column(Name = "fk_Quarter_ID")] public int fk_Quarter_ID { get; set; }
    [Column(Name = "QuarterName")] public string QuarterName { get; set; }
    
}


[Table]
public class NTQ_Report_Objectives : DataAccessObject
{
    public NTQ_Report_Objectives(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public NTQ_Report_Objectives()
    {
    }

    [Column(Name = "Id")]
    public int Id { get; set; }
    [Column(Name = "fk_IncidentId")] public decimal fk_IncidentId { get; set; }
    [Column(Name = "fk_Report_Objectives_ID")] public int fk_Report_Objectives_ID { get; set; }
    [Column(Name = "CFY")] public string CFY { get; set; }
    [Column(Name = "CreatedDate")] public DateTime CreatedDate { get; set; }
    [Column(Name = "CreatedBy")] public int CreatedBy { get; set; }
    [Column(Name = "ModifiedBy")] public int ModifiedBy { get; set; }
    [Column(Name = "ModifiedDate")] public DateTime ModifiedDate { get; set; }    
}

[Table]
public class NTQ_Report_KeyResults : DataAccessObject
{
    public NTQ_Report_KeyResults(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public NTQ_Report_KeyResults()
    {
    }

    [Column(Name = "Id")] public int Id { get; set; }
    [Column(Name = "CFY")] public string CFY { get; set; }

    [Column(Name = "fk_Report_Objectives_ID")] public int fk_Report_Objectives_ID { get; set; }
    [Column(Name = "fk_NTQ_StrategicObjective_ID")] public int fk_NTQ_StrategicObjective_ID { get; set; }
    [Column(Name = "fk_Lookup_KeyResult_ID")] public int fk_Lookup_KeyResult_ID { get; set; }
    [Column(Name = "fk_KeyResultIndicator_ID")] public int fk_KeyResultIndicator_ID { get; set; }
    [Column(Name = "AssignedToSID")] public string AssignedToSID { get; set; }
    [Column(Name = "Anchor")] public string Anchor { get; set; }
    [Column(Name = "KeyResultOwner")] public string KeyResultOwner { get; set; }
    [Column(Name = "fk_LookupAnualTarget_ID")] public int fk_LookupAnualTarget_ID { get; set; }
    [Column(Name = "fk_lookup_Q1_ID")] public int fk_lookup_Q1_ID { get; set; }
    [Column(Name = "fk_lookup_Q2_ID")] public int fk_lookup_Q2_ID { get; set; }
    [Column(Name = "fk_lookup_Q3_ID")] public int fk_lookup_Q3_ID { get; set; }
    [Column(Name = "fk_lookup_Q4_ID")] public int fk_lookup_Q4_ID { get; set; }
    [Column(Name = "fk_lookupTID")] public int fk_lookupTID { get; set; }
    [Column(Name = "CreatedDate")] public DateTime CreatedDate { get; set; }
    [Column(Name = "CreatedBy")] public int CreatedBy { get; set; }
    [Column(Name = "ModifiedBy")] public int ModifiedBy { get; set; }
    [Column(Name = "ModifiedDate")] public DateTime ModifiedDate { get; set; } 
}

[Table]
public class NTQ_Report : DataAccessObject
{
    public NTQ_Report(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public NTQ_Report()
    {
    }
    [Column(Name = "Id")] public int Id { get; set; }
    [Column(Name = "fk_Quarter_ID")] public int fk_Quarter_ID { get; set; }
    [Column(Name = "fk_ReportKeyResult_ID")] public int fk_ReportKeyResult_ID { get; set; }
    [Column(Name = "fk_IncidentId")] public decimal fk_IncidentId { get; set; }
    [Column(Name = "ActualAchievement")] public string ActualAchievement { get; set; }
    [Column(Name = "Variance")] public string Variance { get; set; }
    [Column(Name = "fk_TargetMetID")] public int fk_TargetMetID { get; set; }
    [Column(Name = "fk_DataValidAndCorrect_ID")] public int fk_DataValidAndCorrect_ID { get; set; }
    [Column(Name = "ReasonForVariance")] public string ReasonForVariance { get; set; }
    [Column(Name = "MitigationForUnderPerformance")] public string MitigationForUnderPerformance { get; set; }
    [Column(Name = "CommentOnPeformance")] public string CommentOnPeformance { get; set; }
    [Column(Name = "fk_CalculatedAccordingToTID")] public int fk_CalculatedAccordingToTID { get; set; }
    [Column(Name = "IfNotCalcAccordingToTID")] public string IfNotCalcAccordingToTID { get; set; }
    [Column(Name = "DataSoruce")] public string DataSoruce { get; set; }
    [Column(Name = "Evidence")] public string Evidence { get; set; }
    [Column(Name = "CompilerName")] public string CompilerName { get; set; }
    [Column(Name = "CompilerSigned")] public bool CompilerSigned { get; set; }
    [Column(Name = "AnchorApproved")] public bool AnchorApproved { get; set; }
    [Column(Name = "CompilerDate")] public DateTime? CompilerDate { get; set; }
    [Column(Name = "CompilerSignature")]    public byte[] CompilerSignature { get; set; }
    [Column(Name = "KeyResultOwnerName")] public string KeyResultOwnerName { get; set; }
    [Column(Name = "KeyResultOwnerApproved")] public bool KeyResultOwnerApproved { get; set; }
    [Column(Name = "KeyResultOwnerSigned")] public bool KeyResultOwnerSigned { get; set; }
    [Column(Name = "KeyResultOwnerDate")] public DateTime? KeyResultOwnerDate { get; set; }
    [Column(Name = "Kro1Signature")] public byte[] Kro1Signature { get; set; }
    [Column(Name = "AnchorName")] public string AnchorName { get; set; }
    [Column(Name = "AnchorSigned")] public bool AnchorSigned { get; set; }
    [Column(Name = "AnchorDate")] public DateTime? AnchorDate { get; set; }
    [Column(Name = "Anchor1Signature")] public byte[] Anchor1Signature { get; set; }
    [Column(Name = "KeyResultOwner2Name")] public string KeyResultOwner2Name { get; set; }
    [Column(Name = "KeyResultOwner2Signed")] public bool KeyResultOwner2Signed { get; set; }
    [Column(Name = "KeyResultOwner2Approved")] public bool KeyResultOwner2Approved { get; set; }
    [Column(Name = "KeyResultOwner2Date")] public DateTime? KeyResultOwner2Date { get; set; }
    [Column(Name = "Kro2Signature")] public byte[] Kro2Signature { get; set; }
    [Column(Name = "Anchor2Name")] public string Anchor2Name { get; set; }
    [Column(Name = "Anchor2Approved")] public bool Anchor2Approved { get; set; }
    [Column(Name = "Anchor2Signed")] public bool Anchor2Signed { get; set; }
    [Column(Name = "Anchor2Date")] public DateTime? Anchor2Date { get; set; }
    [Column(Name = "Anchor2Signature")] public byte[] Anchor2Signature { get; set; }
    [Column(Name = "TID")] public byte[] TID { get; set; }
    [Column(Name = "CreatedDate")] public DateTime CreatedDate { get; set; }
    [Column(Name = "CreatedBy")] public int CreatedBy { get; set; }
    [Column(Name = "ModifiedBy")] public int ModifiedBy { get; set; }
    [Column(Name = "ModifiedDate")] public DateTime ModifiedDate { get; set; }

}



[Table]
public class NTQ_PrintReport : DataAccessObject
{
    public NTQ_PrintReport(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public NTQ_PrintReport()
    {
    }
    [Column(Name = "IncidentStatusId")] public int IncidentStatusId { get; set; }
    [Column(Name = "ProcessId")] public Int64 ProcessId { get; set; }
    [Column(Name = "CompilerFullName")] public string CompilerFullName { get; set; }
    [Column(Name = "KeyResultFullName")] public string KeyResultFullName { get; set; }
    [Column(Name = "AnchorSignature")] public byte[] AnchorSignature { get; set; }
    [Column(Name = "KeyResultSignature")] public byte[] KeyResultSignature { get; set; }
    [Column(Name = "CompilerSignature")] public byte[] CompilerSignature { get; set; }
    [Column(Name = "Objectives")] public string Objectives { get; set; }
    [Column(Name = "KeyResult")] public string KeyResult { get; set; }
    [Column(Name = "QuarterName")] public string QuarterName { get; set; }
    [Column(Name = "KeyResultIndicator")] public string KeyResultIndicator { get; set; }
    [Column(Name = "Anchor")] public string Anchor { get; set; }
    [Column(Name = "KeyResultOwner")] public string KeyResultOwner { get; set; }
    [Column(Name = "AnnualTarget")] public string AnnualTarget { get; set; }
    [Column(Name = "Q1Target")] public string Q1Target { get; set; }
    [Column(Name = "Q2Target")] public string Q2Target { get; set; }
    [Column(Name = "Q3Target")] public string Q3Target { get; set; }
    [Column(Name = "Q4Target")] public string Q4Target { get; set; }
    [Column(Name = "TID")] public string TID { get; set; }
    [Column(Name = "fk_TargetMetID")] public string fk_TargetMetID { get; set; }
    [Column(Name = "fk_DataValidAndCorrect_ID")] public string fk_DataValidAndCorrect_ID { get; set; }
    [Column(Name = "ReasonForVariance")] public string ReasonForVariance { get; set; }
    [Column(Name = "MitigationForUnderPerformance")] public string MitigationForUnderPerformance { get; set; }
    [Column(Name = "CommentOnPeformance")] public string CommentOnPeformance { get; set; }
    [Column(Name = "fk_CalculatedAccordingToTID")] public string fk_CalculatedAccordingToTID { get; set; }
    [Column(Name = "IfNotCalcAccordingToTID")] public string IfNotCalcAccordingToTID { get; set; }
    [Column(Name = "DataSoruce")] public string DataSoruce { get; set; }
    [Column(Name = "Evidence")] public string Evidence { get; set; }
    [Column(Name = "CompilerSigned")] public string CompilerSigned { get; set; }
    [Column(Name = "CompilerDate")] public DateTime? CompilerDate { get; set; }
    [Column(Name = "KeyResultOwnerSigned")] public string KeyResultOwnerSigned { get; set; }
    [Column(Name = "KeyResultOwnerApproved")] public string KeyResultOwnerApproved { get; set; }
    [Column(Name = "KeyResultOwnerDate")] public DateTime? KeyResultOwnerDate { get; set; }
    [Column(Name = "Anchor1Name")] public string Anchor1Name { get; set; }
    [Column(Name = "AnchorApproved")] public string AnchorApproved { get; set; }
    [Column(Name = "AnchorSigned")] public string AnchorSigned { get; set; }
    [Column(Name = "AnchorDate")] public DateTime? AnchorDate { get; set; }
    [Column(Name = "CFY")] public string CFY { get; set; }
    [Column(Name = "ActualAchievement")] public string ActualAchievement { get; set; }
    [Column(Name = "Variance")] public string Variance { get; set; }
    [Column(Name = "TID_Desc")] public byte[] TID_Desc { get; set; }    
    [Column(Name = "KRO2Name")] public string KRO2Name { get; set; }
    [Column(Name = "KeyResultOwner2Signed")] public string KeyResultOwner2Signed { get; set; }
    [Column(Name = "KeyResultOwner2Approved")] public string KeyResultOwner2Approved { get; set; }
    [Column(Name = "KeyResultOwner2Date")] public DateTime? KeyResultOwner2Date { get; set; }
    [Column(Name = "Kro2Signature")] public byte[] Kro2Signature { get; set; }
    [Column(Name = "Anchor2Name")] public string Anchor2Name { get; set; }
    [Column(Name = "Anchor2Approved")] public string Anchor2Approved { get; set; }
    [Column(Name = "Anchor2Signed")] public string Anchor2Signed { get; set; }
    [Column(Name = "Anchor2Date")] public DateTime? Anchor2Date { get; set; }
    [Column(Name = "Anchor2Signature")] public byte[] Anchor2Signature { get; set; }   
    [Column(Name = "CreatedDate")] public DateTime CreatedDate { get; set; }
    [Column(Name = "CreatedBy")] public int CreatedBy { get; set; }
    [Column(Name = "ModifiedBy")] public int ModifiedBy { get; set; }
    [Column(Name = "ModifiedDate")] public DateTime ModifiedDate { get; set; }
    [Column(Name = "Q_TargetValue")] public string Q_TargetValue { get; set; }
    [Column(Name = "CurrQuarterName")] public string CurrQuarterName { get; set; }
    

}



[Table]
public class NTRQ_User : DataAccessObject
{
    public NTRQ_User(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public NTRQ_User()
    {
    }

    [Column(Name = "ID")]
    public int ID { get; set; }

    [Column(Name = "RoleId")]
    public int RoleId { get; set; }

    [Column(Name = "UserId")]
    public int UserId { get; set; }

    [Column(Name = "UserCode")]
    public string UserCode { get; set; }
    [Column(Name = "RoleName")]

    public string RoleName { get; set; }

    [Column(Name = "UserFullName")]
    public string UserFullName { get; set; }
    [Column(Name = "IsActive")]
    public bool IsActive { get; set; }
    [Column(Name = "Signature")]
    public byte[] Signature { get; set; }
    [Column(Name = "CreatedBy")]
    public int CreatedBy { get; set; }

    [Column(Name = "fk_User_UnitId")]
    public int fk_User_UnitId { get; set; }

    [Column(Name = "CreatedDate")]
    public DateTime CreatedDate { get; set; }

}

[Table]
public class NTRQ_UserRole : DataAccessObject
{
    public NTRQ_UserRole(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public NTRQ_UserRole()
    {
    }

    [Column(Name = "Id")]
    public long Id { get; set; }

    [Column(Name = "RoleId")]

    public int RoleId { get; set; }

    [Column(Name = "UserId")]
    public int UserId { get; set; }

    [Column(Name = "CreatedBy")]
    public int CreatedBy { get; set; }

    [Column(Name = "CreatedDate")]
    public DateTime CreatedDate { get; set; }
    [Column(Name = "UserCode")]
    public string UserCode { get; set; }

}

[Table]
public class NTRQ_Role : DataAccessObject
{
    public NTRQ_Role(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public NTRQ_Role()
    {
    }

    [Column(Name = "ID")]
    public int ID { get; set; }

    [Column(Name = "RoleName")]

    public string RoleName { get; set; }

    [Column(Name = "CreatedBy")]
    public string CreatedBy { get; set; }

    [Column(Name = "CreatedDate")]
    public DateTime CreatedDate { get; set; }

}

[Table]
public class Service_NTQR_ProcessConfiguration : DataAccessObject
{
    public Service_NTQR_ProcessConfiguration(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public Service_NTQR_ProcessConfiguration()
    {
    }

    [Column(Name = "Id")]
    public int Id { get; set; }

    [Column(Name = "Description")]
    public string Description { get; set; }

    [Column(Name = "Q1Date")]
    public DateTime Q1Date { get; set; }

    [Column(Name = "Q2Date")]
    public DateTime Q2Date { get; set; }

    [Column(Name = "Q3Date")]
    public DateTime Q3Date { get; set; }

    [Column(Name = "Q4Date")]
    public DateTime Q4Date { get; set; }

    [Column(Name = "AnnualDate")]
    public DateTime AnnualDate { get; set; }

    [Column(Name = "EmailMocks")]
    public string EmailMocks { get; set; }

    [Column(Name = "isActive")]
    public bool isActive { get; set; }

    [Column(Name = "testEmail")]
    public string testEmail { get; set; }

    [Column(Name = "isProd")]
    public bool isProd { get; set; }

}

[Table]
public class NTQUserInUnits : DataAccessObject
{
    public NTQUserInUnits(string procedure, Dictionary<string, object> parameters)
        : base(procedure, parameters)
    {
    }

    public NTQUserInUnits()
    {
    }

    [Column(Name = "Objective")]
    public string Objective { get; set; }

    [Column(Name = "fk_KeyResultId")]
    public int fk_KeyResultId { get; set; }
    [Column(Name = "KeyResultName")]
    public string KeyResultName { get; set; }

    [Column(Name = "fk_UserId")]
    public int fk_UserId { get; set; }

    [Column(Name = "RoleId")]
    public int RoleId { get; set; }

    [Column(Name = "RoleName")]
    public string RoleName { get; set; }

    [Column(Name = "Unit")]
    public string Unit { get; set; }


    [Column(Name = "unitId")]
    public int unitId { get; set; }

    [Column(Name = "UserCode")]
    public string UserCode { get; set; }

    [Column(Name = "UserFullName")]
    public string UserFullName { get; set; }
}
