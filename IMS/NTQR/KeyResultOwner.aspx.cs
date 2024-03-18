using Sars.Systems.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NTQR_KeyResultOwner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindKRO();
        }
    }

    protected void RemoveUser(object sender, EventArgs e)
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
                    var id = Convert.ToInt32(gvUsers.SelectedDataKey["Id"]);
                    if (id != 0)
                    {
                        RemoveKRO(id);
                        MessageBox.Show("KRO Removed Successfully");
                        BindKRO();
                    }
                }
            }
        }
    }

  
    protected void btnTeamSearch_Click(object sender, EventArgs e)
    {
        //SearchUsers();
    }
    public void BindKRO()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT * FROM [NTQ_Lookup_KeyResultOwner] ");


        using (var data = new RecordSet(sb.ToString(), QueryType.TransectSQL, null, db.ConnectionString))
        {
            if (data.HasRows)
            {
                gvUsers.Bind(data);
            }
          
        }
    }

    public void RemoveKRO(int id)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" Delete FROM [dbo].[NTQ_Lookup_KeyResultOwner] where id = " + id);
        
        using (var command = new DBCommand(sb.ToString(), QueryType.TransectSQL, null,db.Connection))
        {
            command.Execute();
        }
    }
    protected void Save(object sender, EventArgs e)
    {
        string sid = UserSelector1.SelectedAdUserDetails.SID;            
        int _user = 0;
        var currUser = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
        if (currUser != null)
        {
            _user = currUser[0].ID;
        }

        StringBuilder sb = new StringBuilder();
       
        sb.Append("IF NOT EXISTS(SELECT * FROM NTQ_Lookup_KeyResultOwner where [Name] =@Name) ");
        sb.Append(" BEGIN");
        sb.Append(" INSERT INTO [NTQ_Lookup_KeyResultOwner] ([Name], [timestamp], [CreatedBy], [CreatedDate]) VALUES (@Name, @timestamp, @CreatedBy, @CreatedDate)");
        sb.Append(" END");
        var oParams = new DBParamCollection
                          {
                              {"@Name", sid + " | " + SarsUser.SearchADUsersBySID(sid)[0].FullName},
                              {"@timestamp", DateTime.Now},
                              {"@CreatedBy", _user},
                              {"@CreatedDate",DateTime.Now},


                          };
        using (
        var command = new DBCommand(sb.ToString(), QueryType.TransectSQL, oParams,
                                    db.Connection))
        {
            command.Execute();
            MessageBox.Show("KRO Inserted successfully");
            BindKRO();
        }
    }

    private void SearchUsers()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT * FROM [NTQ_Lookup_KeyResultOwner] where ");


        using (var data = new RecordSet(sb.ToString(), QueryType.TransectSQL, null, db.ConnectionString))
        {
            if (data.HasRows)
            {
                gvUsers.Bind(data);
            }

        }
    }

    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // this.gvUsers.NextPage(this.CurrentProcess.Users, e.NewPageIndex);
    }
}