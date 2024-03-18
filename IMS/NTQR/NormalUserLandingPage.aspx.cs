using Sars.Systems.Data;
using Sars.Systems.Security;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class NTQR_NormalUserLandingPage : IncidentTrackingPage
{
    protected IncidentProcess CurrentProc;
    protected Incident CurrentIncidentDetails;
    public string objectiveId
    {
        get { return ViewState["incId"] as string; }
        set { ViewState["incId"] = value; }
    }
    public string CurrentUser
    {
        get { return ViewState["CurrentUser"] as string; }
        set { ViewState["CurrentUser"] = value; }
    }

    public bool IsUserReadOnlySignedOffReports 
    {
        get { return Convert.ToBoolean(ViewState["IsUserReadOnlySignedOffReports"]); }
        set { ViewState["IsUserReadOnlySignedOffReports"] = value; }
    }

    public bool IsEdmTeam
    {
        get { return Convert.ToBoolean(ViewState["IsEdmTeam"]); }
        set { ViewState["IsEdmTeam"] = value; }
    }

    public bool isUserInTheProcess(string sid)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT u.[ID],[UserCode],[UserFullName] FROM NTQR_User  u inner join [dbo].[NTQR_UserRoles] r on u.id = r.UserId where UserCode = '" + sid + "'");


        using (var data = new RecordSet(sb.ToString(), QueryType.TransectSQL, null, db.ConnectionString))
        {
            if (data.HasRows)
            {
                return true;
            }
            return false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        CurrentProc = CurrentProcess;
        if (!isUserInTheProcess(SarsUser.SID.ToLower()))
        {
            MessageBox.ShowAndRedirect("You do not have access to the Quarterly report Process", "../Default.aspx");
            return;
        }
        if (!IsPostBack)
        {
            CurrentUser = string.Empty;
            var adUser = SarsUser.GetADUser(SarsUser.SID);
            objectiveId = "0";
            IsUserReadOnlySignedOffReports = false;
            IsEdmTeam = false;
            var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
            if (User != null)
            {
                var userRole = IncidentTrackingManager.GetNTQR_UserRoleById(User[0].ID);
                if (userRole != null)
                {
                    GetKRQuaters(User[0].ID);
                    foreach (var role in userRole)
                    {
                        if (role.RoleId.Equals(4))
                        {
                            IsEdmTeam = true;
                        }
                        if (role.RoleId.Equals(5))
                        {
                            IsUserReadOnlySignedOffReports = true;
                        }
                        if (role.RoleId.Equals(1))
                        {
                            Toolbar1.Items[0].Visible = true;
                            break;
                        }
                        else
                        {
                            Toolbar1.Items[0].Visible = false;
                        }
                    }
                    foreach (var role in userRole)
                    {
                        if (role.RoleId.Equals(4))
                        {
                            IsEdmTeam = true;
                            break;
                        }
                    }

                }
            }
            BindDropDownlist();
            //BindGridView();
            ApplyRoleControl();
            
        }
    }

    private void EnableReadOnlyReport()
    {
        Toolbar1.Visible = false;
    }

    private void SearchDropDownList(List<NTQ_Data> incidents)
    {
        if (incidents != null)
        {

            foreach (var incident in incidents)
            {
                var search = string.Format("{0} | Q{1}", incident.CFY, incident.fk_Quarter_ID);
                if (drpSearch.Items.FindByText(search) == null)
                {

                    drpSearch.Items.Add(search);

                }
            }
            if (drpSearch.Items.FindByText("Search by..") == null)
            {
                drpSearch.Items.Insert(0, new ListItem("Search by..", "-1"));
            }
        }
    }
    public void GetKRQuaters(int userId)
    {
        try
        {


            var oParams = new DBParamCollection
                          {
                              {"@userId", userId}

                          };
            string sql = @"SELECT fk_Quarter_ID,Case when fk_Quarter_ID = 5  THEN CONVERT(nvarchar(10),CFY)  + ' | Annual Target' ELSE 
                        Convert(nvarchar(10),CFY) + ' | Q' + Convert(nvarchar(1),fk_Quarter_ID) END  as cfy FROM (
                       SELECT distinct vw_NTQ_Data.CFY,vw_NTQ_Data.fk_Quarter_ID
                      FROM     NTQ_Lookup_StrategicObjective INNER JOIN
                      NTQ_Report_Objectives ON NTQ_Lookup_StrategicObjective.Id = NTQ_Report_Objectives.fk_Report_Objectives_ID RIGHT OUTER JOIN
                      vw_NTQ_Data ON NTQ_Report_Objectives.Id = vw_NTQ_Data.fk_NTQ_StrategicObjective_ID
				      inner Join [NTQ_UserKeyResults] uk on vw_NTQ_Data.fk_Lookup_KeyResult_ID = uk.fk_KeyResultId
					  inner Join [NTQ_User_Units] u on uk.fk_NTQ_User_Unit_Id = u.id
					  inner join [NTQ_User_UnitsMappings] us on us.fk_User_UnitId = u.id
					  WHERE  (us.fk_UserId= @userId)
				  )x
				  Order by  fk_Quarter_ID,CFY";


            using (var data = new RecordSet(sql, QueryType.TransectSQL, oParams, db.ConnectionString))
            {
                if (data.HasRows)
                {
                    drpSearch.Bind(data, "cfy", "cfy");
                }
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }
    private void ApplyRoleControl(string fy = null)
    {
        if (fy == null)
        {
            var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
            if (User != null)
            {
                var userRole = IncidentTrackingManager.GetNTQR_UserRoleById(User[0].ID);
                if (userRole != null)
                {
                    var report = IncidentTrackingManager.GetNTQRIncidents(User[0].ID);
                    foreach (var role in userRole)
                    {
                        bool userHasCompileRole = false;
                        foreach (var c in userRole)
                        {
                            if (c.RoleId.Equals(1) || c.RoleId.Equals(4))
                            {
                                userHasCompileRole = true;
                                break;
                            }
                        }
                        if (userHasCompileRole)
                        {
                            if (role.RoleId.Equals(1) || role.RoleId.Equals(4))
                            {

                                string cfy;
                                if((DateTime.Today >= Convert.ToDateTime(("01/May/" + DateTime.Today.Year.ToString()) ))
                                    && DateTime.Today <= Convert.ToDateTime(("31/March/" + DateTime.Today.AddYears(1).Year.ToString())))
                                {
                                    cfy = DateTime.Today.Year.ToString() ;
                                }
                                else
                                {
                                    cfy = (int.Parse(DateTime.Today.Year.ToString()) - 1).ToString();//DateTime.Today.AddYears(-1).ToString();
                                }
                                var incidents = IncidentTrackingManager.GetNTQRIncidents(User[0].ID).Where(c => c.CFY.Contains(cfy)).ToList();
                                // incidents = IncidentTrackingManager.GetNTQRIncidents(User[0].ID);
                                if (incidents != null)
                                {
                                    if (incidents.Any())
                                        gvIncidents.Bind(incidents);
                                   // SearchDropDownList(incidents);
                                    GetKRQuaters(User[0].ID);
                                    break;
                                }
                                else
                                {
                                    gvIncidents.Bind(null);
                                    break;
                                }
                            }
                        }
                        else if (role.RoleId.Equals(5))
                        {
                            var incidents = IncidentTrackingManager.GetNTQRIncidents(User[0].ID);
                            if (incidents != null)
                            {
                                if (incidents.Any())
                                    gvIncidents.Bind(incidents);
                                // SearchDropDownList(incidents);
                                GetKRQuaters(User[0].ID);
                                break;
                            }
                            else
                            {
                                gvIncidents.Bind(null);
                                break;
                            }
                        }
                        else if (role.RoleId.Equals(2) || role.RoleId.Equals(3))
                        {
                            if (report != null)
                            {
                                var incidents = report.FindAll(n => n.KeyResultOwner.ToLower().Contains(SarsUser.SID.ToLower()) || n.Anchor.ToLower().Contains(SarsUser.SID.ToLower()));
                                if (incidents != null && incidents.Count() > 0)
                                {
                                    gvIncidents.Bind(incidents);
                                   // SearchDropDownList(incidents);
                                    GetKRQuaters(User[0].ID);
                                    break;
                                }
                                else
                                    gvIncidents.Bind(null);
                                break;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            string[] searchFY = fy.Split('|');
            string cfy = searchFY[0].Trim();
            var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
            if (User != null)
            {
                
                var userRole = IncidentTrackingManager.GetNTQR_UserRoleById(User[0].ID);
                if (userRole != null)
                {
                    foreach (var role in userRole)
                    {
                        bool userHasCompileRole = false;
                        foreach (var c in userRole)
                        {
                            if (role.RoleId.Equals(1) || role.RoleId.Equals(4))
                            {
                                userHasCompileRole = true;
                                break;
                            }
                        }
                        if (userHasCompileRole)
                        {
                            if (role.RoleId.Equals(1) || role.RoleId.Equals(4))
                            {
                                var incidents = IncidentTrackingManager.GetNTQRIncidents(User[0].ID).Where(n => n.CFY.Trim() == cfy).ToList(); ;
                                if (incidents != null && incidents.Count() > 0)
                                {
                                    gvIncidents.Bind(incidents);
                                   // SearchDropDownList(incidents);
                                   // GetKRQuaters(User[0].ID);
                                    break;
                                }
                                else
                                    gvIncidents.Bind(null);
                                break;
                            }
                        }
                        else if (role.RoleId.Equals(5) )
                        {
                            var incidents = IncidentTrackingManager.GetNTQRIncidents(User[0].ID).Where(n => n.CFY.Trim() == cfy).ToList(); ;
                            if (incidents != null && incidents.Count() > 0)
                            {
                                gvIncidents.Bind(incidents);
                                // SearchDropDownList(incidents);
                                // GetKRQuaters(User[0].ID);
                                break;
                            }
                            else
                                gvIncidents.Bind(null);
                            break;
                        }
                        else if (role.RoleId.Equals(2) || role.RoleId.Equals(3))
                        {
                            var incidents = IncidentTrackingManager.GetNTQRIncidents(User[0].ID).Where(n => (n.Anchor.ToLower().Contains(SarsUser.SID.ToLower()) || n.KeyResultOwner.ToLower().Contains(SarsUser.SID.ToLower())) && n.CFY.Trim() == cfy).ToList(); ;
                            if (incidents != null && incidents.Count() > 0)
                            {
                                gvIncidents.Bind(incidents);
                               // SearchDropDownList(incidents);
                                //GetKRQuaters(User[0].ID);
                                break;
                            }
                            else
                            {
                                gvIncidents.Bind(null);

                            }
                            break;
                        }
                    }
                }
            }
        }
    }
    private void BindDropDownlist()
    {
        var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
        if (User != null)
        {
            var results = IncidentTrackingManager.GetLookupObjectives(User[0].ID);
            drpStrategicObjective.Bind(results, "Name", "Id");
        }

        var users = IncidentTrackingManager.NTQ_SELECT_LookupAnchorNames();
        drpAnchor.Bind(users, "Name", "Id");

        users = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_KeyResultOwner();
        drpKeyResultOwner.Bind(users, "Name", "Id");

    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "AddNewObjective":
                {
                    drpStrategicObjective.SelectedIndex = 0;
                    BindDropDownOnSeleted();
                    drpKeyResult.SelectedIndex = -1;
                    drpKeyResultIndicator.SelectedIndex = 0;
                    drpAnnualTarget.SelectedIndex = 0;
                    drpQuarterOneTarget.SelectedIndex = 0;
                    drpQuarter2Target.SelectedIndex = 0;
                    drpQuarter3Target.SelectedIndex = 0;
                    drpQuarter4Target.SelectedIndex = 0;
                    drpTID.SelectedIndex = 0;
                    txtCFY.Text = string.Empty;
                    drpKeyResultOwner.SelectedIndex = -1;
                    drpAnchor.SelectedIndex = -1;
                    mdlObjetives.Show();
                    break;
                }

            case "Reports":
                {
                    Response.Redirect(String.Format("~/ntqr/selectnormaluserprocess.aspx?procId={0}", ProcessID));
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
        //var KeyResultId = DataBinder.Eval(e.Row.DataItem, "KeyResultId");
        HiddenField Id = (HiddenField)e.Row.FindControl("hdnID");
        var gw = (GridView)e.Row.FindControl("gvReport");

        //new Role
        if (IsUserReadOnlySignedOffReports)
        {
            LinkButton lnkCreateReport = (LinkButton)e.Row.FindControl("lnkCreateReport");
            LinkButton lnkEditObjectives = (LinkButton)e.Row.FindControl("lnkEditObjectives");

            if (lnkCreateReport != null && lnkEditObjectives != null)
            {
                Toolbar1.Visible = false;
                lnkCreateReport.Visible = false;
                lnkEditObjectives.Visible = false;
            }
            if (gw != null)
            {
                if (drpSearch.SelectedIndex > 0)
                {
                    string[] searchFY = drpSearch.SelectedItem.Text.Split('|');
                    string q = searchFY[1].Replace("Q", string.Empty).Trim();
                    q = q == "Annual Target" ? "5" : q;

                    var reports = IncidentTrackingManager.NTQ_Report_SelectApprovedKeyResultReport(int.Parse(Id.Value), int.Parse(q));
                    gw.Bind(reports);
                }
                else
                {
                    var reports = IncidentTrackingManager.NTQ_Report_SelectApprovedKeyResultReport(int.Parse(Id.Value));
                    gw.Bind(reports);
                }
            }

        }
        else
        {
            if (gw != null)
            {
                LinkButton lnkEditObjectives = (LinkButton)e.Row.FindControl("lnkEditObjectives");


                if (drpSearch.SelectedIndex > 0)
                {

                    string[] searchFY = drpSearch.SelectedItem.Text.Split('|');
                    string q = searchFY[1].Replace("Q", string.Empty).Trim();
                    q = q == "Annual Target" ? "5" : q;

                    var reports = IncidentTrackingManager.NTQ_Report_SelectByKeyResultId(int.Parse(Id.Value), int.Parse(q));
                    gw.Bind(reports);
                    if (!IsEdmTeam)
                    {
                        if (reports != null)
                        {
                            foreach (DataRow item in reports.Tables[0].Rows)
                            {
                                if (item["IncidentStatusId"].ToString() == "12")
                                {
                                    //lnkEditObjectives.Visible = false;
                                }
                            }
                        }
                    }

                }
                else
                {
                    var reports = IncidentTrackingManager.NTQ_Report_SelectByKeyResultId(int.Parse(Id.Value));
                    gw.Bind(reports);
                    if (!IsEdmTeam)
                    {
                        if (reports != null)
                        {
                            foreach (DataRow item in reports.Tables[0].Rows)
                            {
                                if (item["IncidentStatusId"].ToString() == "12")
                                {
                                   // lnkEditObjectives.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected void NextedRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        //new Role
        if (IsUserReadOnlySignedOffReports)
        {
            LinkButton lnkViewReport = (LinkButton)e.Row.FindControl("lnkViewReport");

            if (lnkViewReport != null)
            {
                lnkViewReport.Visible = false;
            }
        }
    }
    protected void PageChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncidents.PageIndex = e.NewPageIndex;
        if (drpSearch.SelectedIndex > 0)
        {
            ApplyRoleControl(drpSearch.SelectedItem.Text);
        }
        else
        {
            ApplyRoleControl();
        }
    }



    protected void gvIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string pageName;
        var incidentId = e.CommandArgument.ToString();
        switch (e.CommandName)
        {

            case "Create_Report":
                {

                    Response.Redirect(string.Format("InitIncident.aspx?procId={0}&keyResult={1}&Id={2}", ProcessID, incidentId, string.Empty));
                    break;
                }
            case "View_Report":
                {
                    pageName = "NTQ_Reports";
                    Response.Redirect(String.Format("{0}.aspx?ProcId={1}&IncId={2}", pageName, ProcessID, incidentId));
                    break;
                }
            case "Edit_Object":
                {

                    var obj = IncidentTrackingManager.NTQ_Report_KeyResults_SelectByID(int.Parse(incidentId));
                    if (obj != null)
                    {
                        objectiveId = obj[0].fk_NTQ_StrategicObjective_ID.ToString();
                        hdnId.Value = incidentId;
                        drpStrategicObjective.SelectedValue = obj[0].fk_Report_Objectives_ID.ToString();
                        BindDropDownOnSeleted();
                        drpKeyResult.SelectedValue = obj[0].fk_Lookup_KeyResult_ID.ToString();
                        drpKeyResultIndicator.SelectedValue = obj[0].fk_KeyResultIndicator_ID.ToString();
                        drpAnnualTarget.SelectedValue = obj[0].fk_LookupAnualTarget_ID.ToString();
                        drpQuarterOneTarget.SelectedValue = obj[0].fk_lookup_Q1_ID.ToString();
                        drpQuarter2Target.SelectedValue = obj[0].fk_lookup_Q2_ID.ToString();
                        drpQuarter3Target.SelectedValue = obj[0].fk_lookup_Q3_ID.ToString();
                        drpQuarter4Target.SelectedValue = obj[0].fk_lookup_Q4_ID.ToString();
                        drpTID.SelectedValue = obj[0].fk_lookupTID.ToString();
                        txtCFY.Text = obj[0].CFY;
                        string[] anchor = obj[0].Anchor.Split(',');

                        foreach (var a in anchor)
                        {
                            for (int i = 0; i < drpAnchor.Items.Count; i++)
                            {
                                if (drpAnchor.Items[i].Text == a.Trim())
                                {
                                    drpAnchor.Items[i].Selected = true;
                                }
                            }
                        }


                        string[] owner = obj[0].KeyResultOwner.Split(',');
                        foreach (var a in owner)
                        {
                            for (int i = 0; i < drpKeyResultOwner.Items.Count; i++)
                            {
                                if (drpKeyResultOwner.Items[i].Text == a.Trim())
                                {
                                    drpKeyResultOwner.Items[i].Selected = true;
                                }
                            }
                        }
                    }

                    CurrentUser = string.Empty;
                    var adUser = SarsUser.GetADUser(SarsUser.SID);
                    if (adUser != null)
                    {
                        IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} viewed the Key result {1} Objective, Financial Year {3} on {2}", adUser.SID + " | " + adUser.FullName, drpKeyResult.SelectedItem.Text, DateTime.Now, txtCFY.Text), obj[0].ToXml<NTQ_Report_KeyResults>(), SarsUser.SID, DateTime.Now);
                    }

                    drpStrategicObjective.Enabled = false;
                   // drpKeyResult.Enabled = false;
                    mdlObjetives.Show();

                    break;
                }
           
                break;

            default:
                {
            // MessageBox.Show("Cant execute option selected.");
            return;
        }

    }


}

protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
{
    string pageName;
    var Id = e.CommandArgument.ToString();
    switch (e.CommandName)
    {

        case "View_Report":
            {
                string[] key = Id.Split('|');

                var report = IncidentTrackingManager.NTQ_Report_SelectByReportId(key[0].ToString());
                if (report != null)
                {
                    var p = IncidentTrackingManager.GetIncidentById(report[0].fk_IncidentId.ToString());
                        if (p.IncidentStatusId == 12)
                        {                           
                            pageName = "html2pdfReport";
                            Response.Redirect(String.Format("{0}.aspx?ProcId={1}&Id={2}&keyResult={3}&incId={4}", pageName, ProcessID, key[0].Trim(), key[1].Trim(), p.IncidentID));

                        }
                        else
                        {
                            pageName = "RegisterUserIncident";
                            Response.Redirect(String.Format("{0}.aspx?ProcId={1}&Id={2}&keyResult={3}&incId={4}", pageName, ProcessID, key[0].Trim(), key[1].Trim(), p.IncidentID));
                        }
                }
                break;
            }
            case "Print":
                {
                    string[] key = Id.Split('|');
                    var report = IncidentTrackingManager.NTQ_Report_SelectByReportId(key[0].ToString());
                    if (report != null)
                    {
                        var p = IncidentTrackingManager.GetIncidentById(report[0].fk_IncidentId.ToString());
                        
                        // Response.Redirect("html2pdfReport.aspx?id=" + incidentId);
                        pageName = "html2pdfReport";
                        Response.Redirect(String.Format("{0}.aspx?ProcId={1}&Id={2}&keyResult={3}&incId={4}", pageName, ProcessID, key[0].Trim(), key[1].Trim(), p.IncidentID));
                    }
                }
                break;
        }
}
protected void btnTeamSearch_Click(object sender, EventArgs e)
{


}

protected void gvSearchIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
{

}

private void BindDropDownOnSeleted()
{
    if (drpStrategicObjective.SelectedIndex > 0)
    {
        var results = IncidentTrackingManager.GetKeyResultByObjectivesID(Convert.ToInt32(drpStrategicObjective.SelectedValue));
        drpKeyResult.Bind(results, "Name", "Id");

        results = IncidentTrackingManager.GetKeyResultIndicatorByObjectivesID(Convert.ToInt32(drpStrategicObjective.SelectedValue));
        drpKeyResultIndicator.Bind(results, "Name", "Id");


        var tid = IncidentTrackingManager.GetTID(Convert.ToInt32(drpStrategicObjective.SelectedValue));
        drpTID.Bind(tid, "Description", "Id");

        results = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_AnnualTarget(Convert.ToInt32(drpStrategicObjective.SelectedValue));
        drpAnnualTarget.Bind(results, "Name", "Id");

        results = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_Q1(Convert.ToInt32(drpStrategicObjective.SelectedValue));
        drpQuarterOneTarget.Bind(results, "Name", "Id");

        results = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_Q2(Convert.ToInt32(drpStrategicObjective.SelectedValue));
        drpQuarter2Target.Bind(results, "Name", "Id");

        results = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_Q3(Convert.ToInt32(drpStrategicObjective.SelectedValue));
        drpQuarter3Target.Bind(results, "Name", "Id");

        results = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_Q4(Convert.ToInt32(drpStrategicObjective.SelectedValue));
        drpQuarter4Target.Bind(results, "Name", "Id");
        mdlObjetives.Show();

    }
}
protected void drpStrategicObjective_SelectedIndexChanged(object sender, EventArgs e)
{
    BindDropDownOnSeleted();
}
public static string GetListBoxSelectedText(ListBox Listbox1)
{
    string selectedItem = "";
    if (Listbox1.Items.Count > 0)
    {
        for (int i = 0; i < Listbox1.Items.Count; i++)
        {
            if (Listbox1.Items[i].Selected)
            {
                selectedItem = selectedItem == "" ? Listbox1.Items[i].Text : selectedItem + "," + Listbox1.Items[i].Text;

            }
        }
    }
    return selectedItem;
}
protected void Toolbar2_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
{
    string cfy = txtCFY.Text;
    
    var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);


    switch (e.CommandName)
    {
        case "SaveObjective":
            {
                if (txtCFY.Text.Equals("____/__"))
                {
                    MessageBox.Show("CFY is a required field");
                    mdlObjetives.Show();
                    return;
                }
                if (drpStrategicObjective.SelectedValue.Equals("-99999"))
                {
                    MessageBox.Show("Objective is a required field");
                    mdlObjetives.Show();
                    return;
                }
                if (drpKeyResult.SelectedValue.Equals("-99999"))
                {
                    MessageBox.Show("Key Result is a required field");
                    mdlObjetives.Show();
                    return;
                }
                if (drpKeyResultIndicator.SelectedValue.Equals("-99999"))
                {
                    MessageBox.Show("KeyResultIndicator is a required field");
                    mdlObjetives.Show();
                    return;
                }

                if (drpAnchor.SelectedIndex <= 0)
                {
                    MessageBox.Show("Anchor Name is a required field");
                    mdlObjetives.Show();
                    return;
                }
                if (drpKeyResultOwner.SelectedIndex <= 0)
                {
                    MessageBox.Show("KeyResultOwner Name is a required field");
                    mdlObjetives.Show();
                    return;
                }
                if (drpAnnualTarget.SelectedValue.Equals("-99999"))
                {
                    MessageBox.Show("AnnualTarget Name is a required field");
                    mdlObjetives.Show();
                    return;
                }
                if (drpQuarterOneTarget.SelectedValue.Equals("-99999"))
                {
                    MessageBox.Show("QuarterOneTarget Name is a required field");
                    mdlObjetives.Show();
                    return;
                }
                if (drpQuarter2Target.SelectedValue.Equals("-99999"))
                {
                    MessageBox.Show("Quarter2Target Name is a required field");
                    mdlObjetives.Show();
                    return;
                }
                if (drpQuarter3Target.SelectedValue.Equals("-99999"))
                {
                    MessageBox.Show("Quarter3Target Name is a required field");
                    mdlObjetives.Show();
                    return;
                }
                if (drpQuarter4Target.SelectedValue.Equals("-99999"))
                {
                    MessageBox.Show("Quarter4Target Name is a required field");
                    mdlObjetives.Show();
                    return;
                }
                if (drpTID.SelectedValue.Equals("-99999"))
                {
                    MessageBox.Show("drpTID Name is a required field");
                    mdlObjetives.Show();
                    return;
                }

                var objective = new NTQ_Report_Objectives()
                {
                    Id = int.Parse(objectiveId),
                    fk_IncidentId = 0,
                    fk_Report_Objectives_ID = int.Parse(drpStrategicObjective.SelectedValue),
                    CFY = cfy,
                    CreatedBy = User[0].ID,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = User[0].ID,
                    ModifiedDate = DateTime.Now
                };

                int id = IncidentTrackingManager.NTQ_Report_Objectives_InsertOrUpdate(objective);
                var keyResultId = (hdnId.Value != string.Empty) ? int.Parse(hdnId.Value) : 0;
                if (keyResultId <= 0)
                {
                    var incidents = IncidentTrackingManager.NTQ_CheckIfObjectivesExists(id,
                       int.Parse(drpKeyResult.SelectedValue), txtCFY.Text);
                    if (incidents != null)
                    {
                        drpKeyResult.SelectedIndex = 0;
                        MessageBox.Show(String.Format("The selected Key result for {0} year already exist and you cannot add multiple Key results for {0} year, please update the existing one", txtCFY.Text));
                        mdlObjetives.Show();
                        return;
                    }
                }
                if (id > 0)
                {

                    var keyResult = new NTQ_Report_KeyResults()
                    {
                        Id = keyResultId,
                        CFY = cfy,
                        fk_NTQ_StrategicObjective_ID = id,
                        fk_Lookup_KeyResult_ID = Convert.ToInt32(drpKeyResult.SelectedValue),
                        fk_KeyResultIndicator_ID = Convert.ToInt32(drpKeyResultIndicator.SelectedValue),
                        Anchor = GetListBoxSelectedText(drpAnchor),
                        KeyResultOwner = GetListBoxSelectedText(drpKeyResultOwner),
                        fk_LookupAnualTarget_ID = Convert.ToInt32(drpAnnualTarget.SelectedValue),
                        fk_lookup_Q1_ID = Convert.ToInt32(drpQuarterOneTarget.SelectedValue),
                        fk_lookup_Q2_ID = Convert.ToInt32(drpQuarter2Target.SelectedValue),
                        fk_lookup_Q3_ID = Convert.ToInt32(drpQuarter3Target.SelectedValue),
                        fk_lookup_Q4_ID = Convert.ToInt32(drpQuarter4Target.SelectedValue),
                        fk_lookupTID = Convert.ToInt32(drpTID.SelectedValue),
                        CreatedBy = User[0].ID,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = User[0].ID,
                        ModifiedDate = DateTime.Now
                    };
                    int keyId = IncidentTrackingManager.NTQ_Report_KeyResults_InsertOrUpdate(keyResult);
                    var adUser = SarsUser.GetADUser(SarsUser.SID);
                    if (adUser != null)
                    {
                        IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} Insert/Updated Key result Objective {1} and Financial Year {3} on {2}", adUser.SID + " | " + adUser.FullName, drpKeyResult.SelectedItem.Text, DateTime.Now, txtCFY.Text), keyResult.ToXml<NTQ_Report_KeyResults>(), SarsUser.SID, DateTime.Now);
                    }
                }
                hdnId.Value = string.Empty;
                MessageBox.Show("The Record has been saved successfully.");
                mdlObjetives.Hide();
                ApplyRoleControl();
                break;

            }
        case "Close":
            {
                mdlObjetives.Hide();
                break;
            }
    }
}
protected void gvIncidents_RowEditing(object sender, GridViewEditEventArgs e)
{

}
protected void drpSearch_SelectedIndexChanged(object sender, EventArgs e)
{
    if (drpSearch.SelectedIndex > 0)
    {
        ApplyRoleControl(drpSearch.SelectedItem.Text);
    }
    else
    {
        ApplyRoleControl();
    }
}

protected void drpKeyResult_SelectedIndexChanged(object sender, EventArgs e)
{
    if (drpKeyResult.SelectedIndex > 0)
    {
        var incidents = IncidentTrackingManager.NTQ_CheckIfObjectivesExists(int.Parse(drpStrategicObjective.SelectedValue),
            int.Parse(drpKeyResult.SelectedValue), txtCFY.Text);
        if (incidents != null)
        {
            drpKeyResult.SelectedIndex = 0;
            MessageBox.Show(String.Format("The selected Key result for {0} year already exist and you cannot add multiple Key results for {0} year, please update the existing one", txtCFY.Text));
            mdlObjetives.Show();
        }
    }
    mdlObjetives.Show();
}
}