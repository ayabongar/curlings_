using Sars.Systems.Data;
using Sars.Systems.Security;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class NTQR_NTQ_Reports : IncidentTrackingPage
{
    protected IncidentProcess CurrentProc;
    protected Incident CurrentIncidentDetails;
    public string IncId
    {
        get { return ViewState["incId"] as string; }
        set { ViewState["incId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentProc = CurrentProcess;
        if (CurrentProc == null)
        {
            Response.Redirect("../InvalidProcessOrIncident.aspx");
            return;
        }
        if (!IsPostBack)
        {
           
            BindGridView();
            var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
            if (User != null)
            {
                var userRole = IncidentTrackingManager.GetNTQR_UserRoleById(User[0].ID);
                if (userRole != null)
                {

                    switch (userRole[0].RoleId.ToString().ToUpper())
                    {
                        //Compiler
                        case "1":
                            Toolbar1.Items[0].Visible = true;
                            break;
                        default:
                            Toolbar1.Items[0].Visible = false;
                            break;
                            //ddlStatuses.Bind(IncidentTrackingManager.GetIncidentsStatuses(), "Description", "IncidentStatusId");
                    }
                }
            }
        }

    }

    private void BindDropDownOnSeleted()
    {
        if (drpStrategicObjective.SelectedIndex > 0)
        {
            var results = IncidentTrackingManager.GetKeyResultByObjectivesID(Convert.ToInt32(drpStrategicObjective.SelectedValue));
            drpKeyResult.Bind(results, "Name", "Id");

            results = IncidentTrackingManager.GetKeyResultIndicatorByObjectivesID(Convert.ToInt32(drpStrategicObjective.SelectedValue));
            drpKeyResultIndicator.Bind(results, "Name", "Id");


          var  tid = IncidentTrackingManager.GetTID(Convert.ToInt32(drpStrategicObjective.SelectedValue));
            drpTID.Bind(tid, "Description", "Id");
           
        }
    }

    private void BindGridView()
    {
        var results = IncidentTrackingManager.GetLookupObjectives();
        drpStrategicObjective.Bind(results, "Name", "Id");

        if (!string.IsNullOrEmpty(Request["IncId"])){
            var obj = IncidentTrackingManager.NTQ_Report_KeyResults_SelectByID(int.Parse(Request["IncId"]));
            if (obj != null)
            {
                drpStrategicObjective.SelectedValue = obj[0].fk_Report_Objectives_ID.ToString();
                BindDropDownOnSeleted();
                drpKeyResult.SelectedValue = obj[0].fk_Lookup_KeyResult_ID.ToString();
                drpKeyResultIndicator.SelectedValue = obj[0].fk_KeyResultIndicator_ID.ToString();
                drpTID.SelectedValue = obj[0].fk_lookupTID.ToString();
            }


            var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
            if (User != null)
            {
                var userRole = IncidentTrackingManager.GetNTQR_UserRoleById(User[0].ID);
                if (userRole != null)
                {
                    var incidents = IncidentTrackingManager.NTQ_Report_SelectByKeyResultId(int.Parse(Request["IncId"]));
                    gvIncidents.Bind(incidents);

                    //switch (userRole[0].RoleId.ToString().ToUpper())
                    //{
                    //    //Compiler
                    //    case "1":

                    //        var incidents = IncidentTrackingManager.GetNTQRIncidents(CurrentProc.ProcessId);
                    //        gvIncidents.Bind(incidents);
                    //        //if (incidents != null && incidents.Any())
                    //        //{
                    //        //    if (string.IsNullOrEmpty(txtMyIncidents.Text))
                    //        //    {
                    //        //        gvIncidents.Bind(
                    //        //            incidents.FindAll(
                    //        //                inc =>
                    //        //                    inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId == 12 ||
                    //        //                    inc.IncidentStatusId == 9));
                    //        //    }
                    //        //    else
                    //        //    {
                    //        //        var incident = incidents.FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId == 12 &&
                    //        //                    inc.IncidentStatusId == 9);


                    //        //        gvIncidents.Bind(incident);
                    //        //    }
                    //        //}
                    //        //else
                    //        //{
                    //        //    gvIncidents.Bind(null);
                    //        //}
                    //        break;
                    //    //Key Result Owner
                    //    case "2":
                    //        incidents = IncidentTrackingManager.GetNTQRIncidents(CurrentProc.ProcessId);
                    //        if (incidents != null && incidents.Any())
                    //        {
                    //            if (string.IsNullOrEmpty(txtMyIncidents.Text))
                    //            {
                    //                gvIncidents.Bind(
                    //                    incidents.FindAll(
                    //                        inc =>
                    //                            inc.ProcessId == Convert.ToInt64(ProcessID) &&
                    //                            inc.IncidentStatusId == 10));
                    //            }
                    //            else
                    //            {
                    //                var incident = incidents.FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId == 12 &&
                    //                            inc.IncidentStatusId == 10);


                    //                gvIncidents.Bind(incident);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            gvIncidents.Bind(null);
                    //        }
                    //        break;
                    //    //Anchor
                    //    case "3":
                    //        incidents = IncidentTrackingManager.GetNTQRIncidents(CurrentProc.ProcessId);
                    //        if (incidents != null && incidents.Any())
                    //        {
                    //            if (string.IsNullOrEmpty(txtMyIncidents.Text))
                    //            {
                    //                gvIncidents.Bind(
                    //                    incidents.FindAll(
                    //                        inc =>
                    //                            inc.ProcessId == Convert.ToInt64(ProcessID) &&
                    //                            inc.IncidentStatusId == 11));
                    //            }
                    //            else
                    //            {
                    //                var incident = incidents.FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId == 12 &&
                    //                            inc.IncidentStatusId == 11);


                    //                gvIncidents.Bind(incident);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            gvIncidents.Bind(null);
                    //        }
                    //        break;
                    //    //Anchor
                    //    case "4":
                    //        incidents = IncidentTrackingManager.GetNTQRIncidents(CurrentProc.ProcessId);
                    //        if (incidents != null && incidents.Any())
                    //        {
                    //            if (string.IsNullOrEmpty(txtMyIncidents.Text))
                    //            {
                    //                gvIncidents.Bind(
                    //                    incidents.FindAll(
                    //                        inc =>
                    //                            inc.ProcessId == Convert.ToInt64(ProcessID) &&
                    //                            inc.IncidentStatusId == 12));
                    //            }
                    //            else
                    //            {
                    //                var incident = incidents.FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId == 12 &&
                    //                            inc.IncidentStatusId == 12);


                    //                gvIncidents.Bind(incident);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            gvIncidents.Bind(null);
                    //        }
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
            }

        }

    }
    private void BindGridView(string search)
    {
        var incidents = IncidentTrackingManager.GetNTQRIncidents(CurrentProc.ProcessId);
        

    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "AddNewObjective":
                {
                   
                    break;
                }
            case "AddNewIncident":
                {
                    Response.Redirect(string.Format("InitIncident.aspx?procId={0}&keyResult={1}", ProcessID, Request["IncId"]));
                    break;
                }
           
            
            
        }
    }
 
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        





       


    }
    protected void PageChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncidents.PageIndex = e.NewPageIndex;

        var incidents = IncidentTrackingManager.GetUserAssignedIncidents(SarsUser.SID, 0);
        if (incidents != null && incidents.Any())
        {
            gvIncidents.Bind(incidents.FindAll(inc => inc.ProcessId == Convert.ToInt64(ProcessID) && inc.IncidentStatusId > 1 && inc.IncidentStatusId < 6));
        }
        else
        {
            gvIncidents.Bind(null);
        }


    }


   
    protected void gvIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string pageName;
        var incidentId = e.CommandArgument.ToString();
        switch (e.CommandName)
        {

            case "View_Report":
                {
                    pageName = "RegisterUserIncident";
                    Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}", pageName, ProcessID, incidentId));
                    break;
                }
        }
    }     
}