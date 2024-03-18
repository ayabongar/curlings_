using Sars.Systems;
using Sars.Systems.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NTQR_UserUnits : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            drpStrategicObjective.Bind(IncidentTrackingManager.GetLookupObjectives(), "Name", "Id");
        }

    }

    protected void drpStrategicObjective_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDropDownOnSeleted();
    }
    private void BindDropDownOnSeleted()
    {
        if (drpStrategicObjective.SelectedIndex > 0)
        {
            var results = IncidentTrackingManager.GetKeyResultByObjectivesID(Convert.ToInt32(drpStrategicObjective.SelectedValue));
            drpKeyResult.Bind(results, "Name", "Id");
           
            mdlObjetives.Show();

        }
    }
    protected void Remove(object sender, EventArgs e)
    {
        var btn = sender as LinkButton;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvUsers.SelectRow(row.RowIndex);
                if (gvUsers.SelectedDataKey != null)
                {
                    if (!string.IsNullOrEmpty(gvUsers.SelectedDataKey["Id"].ToString()))
                    {
                        var userProcessId = Convert.ToInt32(gvUsers.SelectedDataKey["UserId"]);
                        var id = Convert.ToInt32(gvUsers.SelectedDataKey["Id"]);
                        if (id != 0)
                        {
                            DeleteUnitsKeyResult(id);
                        }
                    }
                    else
                    {
                        MessageBox.Show("This cannot be removed because the KR is not mapped to the Unit.");

                    }
                }
            }
            
        }
    }

    protected void Modify(object sender, EventArgs e)
    {
        var btn = sender as LinkButton;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvUsers.SelectRow(row.RowIndex);
                if (gvUsers.SelectedDataKey != null)
                {                  
                    var RoleId = Convert.ToInt32(gvUsers.SelectedDataKey["Id"]);
                    if (RoleId != 0)
                    {
                        mdlObjetives.Show();
                    }
                }
            }
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        
        switch (e.CommandName)
        {
            case "Open":
                mdlObjetives.Show();
                break;
            case "Save":
                {
                    if (drpStrategicObjective.SelectedIndex <= 0)
                    {
                        MessageBox.Show("Please select a Key Result");
                        mdlObjetives.Show();
                        return;
                    }
                    if (drpKeyResult.SelectedIndex <= 0)
                    {
                        MessageBox.Show("Please select a Key Result");
                        mdlObjetives.Show();
                        return;
                    }
                    InsertUnits();                   
                    break;
                }

            case "Close":
                {
                    mdlObjetives.Hide();
                    break;
                }

        }
    }

    public void InsertUnits()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" INSERT INTO[dbo].[NTQ_UserKeyResults] ");
        sb.Append("  ([fk_NTQ_User_Unit_Id]  ");
        sb.Append("   ,[fk_ObjectiveId]  ");
        sb.Append("   ,[fk_KeyResultId]  ");
        sb.Append("   ,[CreatedBy]  ");
        sb.Append("   ,[CreatedDate])  ");
        sb.Append("   VALUES  ");
        sb.Append("     ("+ int.Parse(drpUnits.SelectedValue) + ",  ");
        sb.Append("     "+ int.Parse(drpStrategicObjective.SelectedValue) + ",  ");
        sb.Append("     "+ int.Parse(drpKeyResult.SelectedValue) + ",  ");
        sb.Append("     '"+ SarsUser.SID + "', ");
        sb.Append("     '"+ DateTime.Now + "')   ");
        
    

        var oParams = new DBParamCollection
                          {
                            {"@fk_NTQ_User_Unit_Id", int.Parse(drpUnits.SelectedValue)},
                            {"@k_ObjectiveId", int.Parse(drpStrategicObjective.SelectedValue)},
                            {"@k_KeyResultId", int.Parse(drpKeyResult.SelectedValue)},
                            {"@CreatedBy", SarsUser.SID.ToString()},
                            {"@CreatedDate", DateTime.Now}
                          };
        using (
            var oCommand = new DBCommand(sb.ToString(), QueryType.TransectSQL, null,db.Connection))
        {
             oCommand.Execute();
            MessageBox.Show("Record Added.");
            gvUsers.DataBind(); 
        }
    }

    public void DeleteUnitsKeyResult(int id)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Delete from NTQ_UserKeyResults where Id = " + id);        
       
        using (
            var oCommand = new DBCommand(sb.ToString(), QueryType.TransectSQL, null, db.Connection))
        {
            oCommand.Execute();
            MessageBox.Show("Record Deleted.");
            gvUsers.DataBind();
        }
    }
}