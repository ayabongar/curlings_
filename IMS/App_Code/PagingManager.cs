using System;
using Sars.Systems.Data;

[Serializable]
public class PagingManager
{
	public int CurrentSectionId { get; set; }
    public int CurrentSectionSubsectionCount
    {
        get
        {
            return GetCurrentSectionSubsectionCount();
        } 
    }

    public int CurrentSubSectionId { get; set; }
    public int CurrentSubSectionQuestionCount
    {
        get
        {
            return GetCurrentSubSectionQuestionCount();
        }
    }

    private int GetCurrentSubSectionQuestionCount()
    {
        var oParams = new DBParamCollection {{"@SubSectionId", this.CurrentSubSectionId}};
        var oCommand = new DBCommand("SELECT COUNT(*) FROM Questions WHERE SubSectionId = @SubSectionId", QueryType.TransectSQL, oParams, db.Connection);
        var o = oCommand.ExecuteScalar();
        if (o != null)
            return Convert.ToInt32(o);
        return 0;
    }
    private int GetCurrentSectionSubsectionCount()
    {
        var oParams = new DBParamCollection { { "@SectionId", this.CurrentSubSectionId } };
        var oCommand = new DBCommand("SELECT COUNT(*) FROM SubSections WHERE SectionId = @SectionId", QueryType.TransectSQL, oParams, db.Connection);
        var o = oCommand.ExecuteScalar();
        if (o != null)
            return Convert.ToInt32(o);
        return 0;  
    }
}
