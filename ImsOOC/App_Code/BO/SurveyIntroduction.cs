using System;
using System.Collections.Generic;
using Sars.Systems.Data;
using System.Data.Linq.Mapping;


[Table(Name = "Questionnaires")]
public class SurveyIntroduction : DataAccessObject
{
	public SurveyIntroduction(string procedure, Dictionary<string, object> parameters ):base(procedure, parameters){
    }
    public SurveyIntroduction() { }

    [Column(Name = "Id")]
    public int Identity { get; set; }

    [Column(Name = "Description")]
    public string Description { get; set; }

    [Column(Name = "TimeStamp")]
    public DateTime DateAdded { get; set; }

    [Column(Name = "Introduction")]
    public string Introduction { get; set; }

    [Column(Name = "IsActive")]
    public bool IsActive { get; set; }

    public int SaveIntroduction()
    {
        return IncidentTrackingManager.SaveSurveyIntroduction(this.Introduction, this.IsActive, this.Identity);
    }
}