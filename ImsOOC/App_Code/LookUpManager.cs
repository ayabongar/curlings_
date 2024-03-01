using System;
using System.Collections;
using System.Collections.Generic;
using Sars.Systems.Data;
using Sars.Systems.Extensions;

/// <summary>
/// Summary description for LookUpManager
/// </summary>
public static class LookUpManager
{
   
    public static List<Regions> GetRegions()
    {
        var list = new RecordSet("uspREAD_Regions_All", null, db.ConnectionString);
        return list.Tables[0].ToList<Regions>();
    }
    public static RecordSet GetBusinesUnits()
    {
        var oRecordSet = new RecordSet("uspREAD_BusinessUnit_All", null, db.ConnectionString);
        return oRecordSet;
    }

    public static RecordSet GetBusinesDepartments(int busineUnit)
    {
        var oParams = new DBParamCollection
                          {
                              {"@BusinessUnitId", busineUnit}
                          };
        var oRecordSet = new RecordSet("uspREAD_BusinesUnitDepartments", oParams, db.ConnectionString);
        return oRecordSet;
    }

    public static int AddLookupItem(decimal lookupItemId, string description, bool isActive, decimal lookupDataId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@LookupItemId", lookupItemId},
                              {"@Description", description},
                              {"@IsActive", isActive},
                              {"@LookupDataId", lookupDataId},
                              {"@Return_Value", null, System.Data.ParameterDirection.ReturnValue}
                          };
        Hashtable oHashTable;
        int scopeIdentity;
        using (var oCommand = new DBCommand("uspUPSERT_LookupItems", QueryType.StoredProcedure, oParams, db.TransactionConnection))
        {
            scopeIdentity = 0;
            oCommand.Execute(out oHashTable);
            oCommand.Commit();
        }
        if (oHashTable.Count > 0)
        {
            scopeIdentity = int.Parse(oHashTable["@Return_Value"].ToString());
        }
        return scopeIdentity;
    }
    public static int AddHierarchyLookupItem(decimal lookupItemId, string description, bool isActive, decimal lookupDataId, string parentId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@LookupItemId", lookupItemId},
                              {"@Description", description},
                              {"@IsActive", isActive},
                              {"@LookupDataId", lookupDataId},
                              {"@Return_Value", null, System.Data.ParameterDirection.ReturnValue},
                              {"@ParentId", parentId}
                          };
        Hashtable oHashTable;
        int scopeIdentity;
        using (var oCommand = new DBCommand("[dbo].[uspUPSERT_HierarchyLookupItems]", QueryType.StoredProcedure, oParams, db.TransactionConnection))
        {
            scopeIdentity = 0;
            oCommand.Execute(out oHashTable);
            oCommand.Commit();
        }
        if (oHashTable.Count > 0)
        {
            scopeIdentity = int.Parse(oHashTable["@Return_Value"].ToString());
        }
        return scopeIdentity;
    }


    public static int DeleteHierarchyLookupItem(decimal lookupItemId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@LookupItemId", lookupItemId}
                              
                          };
        using (var oCommand = new DBCommand("[dbo].[uspDELETE_HierarchyLookupItems]", QueryType.StoredProcedure, oParams, db.Connection))
        {
           return oCommand.Execute();
        }
    }

    public static int DeleteHierarchyLookupData(decimal lookupDataId)
    {
        var oParams = new DBParamCollection
                          {
                              {"@LookupDataId", lookupDataId}
                              
                          };
        using (var oCommand = new DBCommand("[dbo].[uspDELETE_HierarchyLookupData]", QueryType.StoredProcedure, oParams, db.Connection))
        {
            return oCommand.Execute();
        }
    }


    public static decimal AddLookupData(decimal lookupDataId, string description, bool isActive )
    {
        var oParams = new DBParamCollection
                          {
                              {"@LookupDataId", lookupDataId},
                              {"@Description", description},
                              {"@IsActive", isActive},
                              {"@Return_Value", null, System.Data.ParameterDirection.ReturnValue}
                          };
        Hashtable oHashTable;
        int scopeIdentity;
        using (var oCommand = new DBCommand("uspUPSERT_LookupsData", QueryType.StoredProcedure, oParams, db.TransactionConnection))
        {
            scopeIdentity = 0;
            oCommand.Execute(out oHashTable);
            oCommand.Commit();            
        }
        if (oHashTable.Count > 0)
        {
            scopeIdentity = int.Parse(oHashTable["@Return_Value"].ToString());
        }
        return scopeIdentity;
    }
    public static  bool IsLookupRelated(decimal lookUpDataId)
    {
        var oParams = new DBParamCollection
            {
                {"@LookUpItemId", lookUpDataId}
            };
        using (var oCommand = new DBCommand("uspCheckIfLookUpIsParent", QueryType.StoredProcedure, oParams, db.Connection))
        {
            return Convert.ToBoolean(oCommand.ExecuteScalar());
        }
    }
   public static bool HasNoParent(decimal lookUpDataId)
    {
        var oParams = new DBParamCollection
            {
                {"@LookupDataId", lookUpDataId}
            };
        using (var oCommand = new DBCommand("uspCheckIfLookUpHasNoParent", QueryType.StoredProcedure, oParams, db.Connection))
        {
            return Convert.ToBoolean(oCommand.ExecuteScalar());
        }
    }

    public static decimal AddHierarchyLookupData(decimal lookupDataId, string description, bool isActive, string parentId)
    {
        var oParams = new DBParamCollection
            {
                {"@LookupDataId", lookupDataId},
                {"@Description", description},
                {"@IsActive", isActive},
                {"@ParentId", parentId},
                {"@Return_Value", null, System.Data.ParameterDirection.ReturnValue}
            };
        Hashtable oHashTable;
        int scopeIdentity;
        using (var oCommand = new DBCommand("[dbo].[uspUPSERT_HierarchyLookupsData]", QueryType.StoredProcedure, oParams, db.TransactionConnection))
        {
            scopeIdentity = 0;
            oCommand.Execute(out oHashTable);
            oCommand.Commit();
        }
        if (oHashTable.Count > 0)
        {
            scopeIdentity = int.Parse(oHashTable["@Return_Value"].ToString());
        }
        return scopeIdentity;
    }

    public static decimal AddHierarchyLookupDataSecondLevel(decimal lookupDataId, string description, decimal parentId, bool isActive)
    {
        var oParams = new DBParamCollection
            {
                {"@LookupDataId", lookupDataId},
                {"@Description", description},
                {"@LevelOneId", parentId},
                {"@IsActive", isActive},
                {"@Return_Value", null, System.Data.ParameterDirection.ReturnValue}
            };
        Hashtable oHashTable;
        int scopeIdentity;
        using (var oCommand = new DBCommand("[dbo].[uspUPSERT_HierarchyLookupsDataSecondLevel]", QueryType.StoredProcedure, oParams, db.TransactionConnection))
        {
            scopeIdentity = 0;
            oCommand.Execute(out oHashTable);
            oCommand.Commit();
        }
        if (oHashTable.Count > 0)
        {
            scopeIdentity = int.Parse(oHashTable["@Return_Value"].ToString());
        }
        return scopeIdentity;
    }

    public static decimal AddHierarchyLookupDataThirdLevel(decimal lookupDataId, string description, decimal parentId, bool isActive)
    {
        var oParams = new DBParamCollection
            {
                {"@LookupDataId", lookupDataId},
                {"@Description", description},
                {"@LevelOneId", parentId},
                {"@IsActive", isActive},
                {"@Return_Value", null, System.Data.ParameterDirection.ReturnValue}
            };
        Hashtable oHashTable;
        int scopeIdentity;
        using (var oCommand = new DBCommand("[dbo].[uspUPSERT_HierarchyLookupsDataThirdLevel]", QueryType.StoredProcedure, oParams, db.TransactionConnection))
        {
            scopeIdentity = 0;
            oCommand.Execute(out oHashTable);
            oCommand.Commit();
        }
        if (oHashTable.Count > 0)
        {
            scopeIdentity = int.Parse(oHashTable["@Return_Value"].ToString());
        }
        return scopeIdentity;
    }



    public static List<SurveyLookups> ReadLookups()
    {
        return new SurveyLookups("uspREAD_LookupsData_All",  null).GetRecords<SurveyLookups>();
    }
    public static List<SurveyHierarchyLookups> ReadHierarchyLookups()
    {
        return new SurveyLookups("uspREAD_HierarchyLookupsData_All", null).GetRecords<SurveyHierarchyLookups>();
    }

    public static List<SurveyLookups> ReadLookupsActive()
    {
        return new SurveyLookups("uspREAD_LookupsData_All", null).GetRecords<SurveyLookups>();
    }
    public static List<SurveyHierarchyLookups> ReadHierarchyLookupsActive()
    {
        return new SurveyHierarchyLookups("[dbo].[uspREAD_HierarchyLookupsData_All_Active]", null).GetRecords<SurveyHierarchyLookups>();
    }

    public static List<LookupItem> ReadLookupsItems()
    {
        return
          new LookupItem("uspREAD_LookupItems_All", null).GetRecords<LookupItem>();
    }
    public static List<LookupItem> ReadLookupsItemsActive()
    {
        return
           new LookupItem("uspREAD_LookupItems_All_Active", null).GetRecords<LookupItem>();

    }

    public static List<HierarchyLookupItem> ReadLookupDataItems(decimal lookupDataId)
    {
        return
            new LookupItem("uspREAD_LookupItems", new Dictionary<string, object> {{"@LookupDataId", lookupDataId}}).
                GetRecords<HierarchyLookupItem>();
    }
    public static List<HierarchyLookupItem> ReadHierarchyLookupDataItems(decimal lookupDataId)
    {
        return
            new LookupItem("[dbo].[uspREAD_HierarchyLookupItems]", new Dictionary<string, object> { { "@LookupDataId", lookupDataId } }).
                GetRecords<HierarchyLookupItem>();
    }

    public static List<HierarchyLookupItem> ReadHierarchyLookupDataItemsById(decimal parentId)
    {
        return
            new LookupItem("[dbo].[uspREAD_HierarchyLookupItemsByParent]", new Dictionary<string, object> { { "@ParentId", parentId } }).
                GetRecords<HierarchyLookupItem>();
    }





    public static List<LookupItem> ReadActiveLookupDataItems(decimal lookupDataId)
    {
        return
           new LookupItem("uspREAD_ActiveLookupItems", new Dictionary<string, object> { { "@LookupDataId", lookupDataId } }).
               GetRecords<LookupItem>();
    }

    public static List<ResearchLookUpValue> GetResearchLookUpValue(string tableName)
    {
        return
           new ResearchLookUpValue("research.uspGetLookUps", new Dictionary<string, object> { { "@TableName", tableName } }).
               GetRecords<ResearchLookUpValue>();
    }

    public static int RemoveResearchOption(int id)
    {
        var oParams = new DBParamCollection
            {
                {"@OptionId", id}
            };
        using (var oCommand = new DBCommand("research.uspRemoveOption", QueryType.StoredProcedure, oParams, db.Connection))
        {
            return oCommand.Execute();
        }
    }

    public static int AddResearchOption(string tableName, string option, int optionValue)
    {
        var oParams = new DBParamCollection
            {
                {"@Description", option},
                {"@TableName", tableName},
                {"@Value", optionValue}
            };
        using (var oCommand = new DBCommand("research.uspAddOption", QueryType.StoredProcedure, oParams, db.Connection))
        {
            return oCommand.Execute();
        }
    }
}
