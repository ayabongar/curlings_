using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP;

public partial class PrOoc_AddNewFields : IncidentTrackingPage
{
    protected Incident CurrentIncidentDetails;
    protected IncidentProcess CurrentProcessDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["procId"]))
        {

            CreateProcessField(Request["procId"]);
        }
    }

    private List<string> AddFields(string processId)
    {

        List<string> fields = new List<string>();
        fields.Add("DocumentContent,Document Content,"+ processId +",6");
        fields.Add("All SARS Numbers have been reviewed and signed off,All SARS Numbers have been reviewed and signed off," + processId + ",5");
        fields.Add("Document has been checked for grammar and spelling errors,Document has been checked for grammar and spelling errors," + processId + ",5");
        fields.Add("Document has been checked for conflicting messages,Document has been checked for conflicting messages," + processId + ",5");
        fields.Add("Document addresses the information requested,Document addresses the information requested," + processId + ",5");
        fields.Add("Document does not disclose inappropriate / unsubstantiated facts,Document does not disclose inappropriate / unsubstantiated facts," + processId + ",5");
        fields.Add("Document is presented in a SARS standard template being neat coherent and protraying the image SARS wants to present,Document is presented in a SARS standard template being neat coherent and protraying the image SARS wants to present," + processId + ",5");
        fields.Add("Commissioner's Comments,Commissioner's Comments," + processId + ",6");
        fields.Add("Comments,Comments," + processId + ",6");
        fields.Add("Sign-off for release,Sign-off for release," + processId + ",5");
        fields.Add("Notes,Notes," + processId + ",6");
        fields.Add("Approval Required from DG: NT,Approval Required from DG: NT," + processId + ",5");
        fields.Add("Approval Required from Deputy Minister,Approval Required from Deputy Minister," + processId + ",5");
        fields.Add("Approval Required from Minister,Approval Required from Minister," + processId + ",5");
        fields.Add("Approval Required from Minister,Approval Required from Minister," + processId + ",6");
        return fields;
    }
    protected void CreateProcessField(string processId)
    {
        List<string> list = AddFields(processId);
        foreach (string cntrl in list)
        {
            string[] fields = cntrl.Split(',');
            IncidentTrackingManager.CreateProcessFields(fields[0], fields[1], fields[2], int.Parse(fields[3]));
        }

    }
}