using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Collections.Generic;
[Table]
public class GroupedSurveyQuestion : Sars.Systems.Data.DataAccessObject
{
    public GroupedSurveyQuestion(string procedure, Dictionary<string, object> parameters ):base(procedure, parameters)
    {
    }
	public GroupedSurveyQuestion()
	{
	}

	[Column(Name="GroupId")]
	public decimal GroupId {get; set;}

	[Column(Name="QuestionsId")]
	public decimal QuestionsId {get; set;}

	[Column(Name="QuestionDescription")]
	public string QuestionDescription {get; set;}

	[Column(Name="SectionId")]
	public int SectionId {get; set;}

	[Column(Name="SubsectionId")]
	public int SubsectionId {get; set;}

	[Column(Name="QuestionTypeId")]
	public int QuestionTypeId {get; set;}

	[Column(Name="QuestionnaireId")]
	public int? QuestionnaireId {get; set;}

	[Column(Name="SortOrder")]
	public int? SortOrder {get; set;}

	[Column(Name="IsActive")]
	public bool IsActive {get; set;}

	[Column(Name="IsRequired")]
	public bool IsRequired {get; set;}

	[Column(Name="SubsectionDescription")]
	public string SubsectionDescription {get; set;}

	[Column(Name="SectionDescription")]
	public string SectionDescription {get; set;}

	[Column(Name="SectionInstruction")]
	public string SectionInstruction {get; set;}

	[Column(Name="AlowComments")]
	public bool AlowComments {get; set;}

	[Column(Name="PageGroup")]
	public int PageGroup {get; set;}

	[Column(Name="QuestionNumber")]
	public Int64 QuestionNumber {get; set;}

	[Column(Name="ScaleTypeId")]
	public int ScaleTypeId {get; set;}

	[Column(Name="MatrixDimensionId")]
	public int MatrixDimensionId {get; set;}

	[Column(Name="ValidationTypeId")]
	public int ValidationTypeId {get; set;}

	[Column(Name="LookupDataId")]
	public decimal LookupDataId {get; set;}

    [Column(Name = "HierarchyLookupId")]
    public decimal HierarchyLookupId { get; set; }

	[Column(Name="DescriptionHtml")]
	public string DescriptionHtml {get; set;}


    [Column(Name = "HasDictionary")]
    public bool HasDictionary { get; set; }

 [Column(Name = "AllowsOtherAnswer")]
    public bool AllowsOtherAnswer { get; set; }


}