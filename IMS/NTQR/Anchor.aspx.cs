using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;
using System.Text;

public partial class NTQR_Anchor : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAnchor();
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
                        RemoveAnchor(id);
                        MessageBox.Show("Anchor Removed Successfully");
                        BindAnchor();
                    }
                }
            }
        }
    }


    protected void btnTeamSearch_Click(object sender, EventArgs e)
    {
        //SearchUsers();
    }
    public void BindAnchor()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT * FROM [NTQ_LookupAnchorNames] ");


        using (var data = new RecordSet(sb.ToString(), QueryType.TransectSQL, null, db.ConnectionString))
        {
            if (data.HasRows)
            {
                gvUsers.Bind(data);
            }

        }
    }

    public void RemoveAnchor(int id)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" Delete FROM [dbo].[NTQ_LookupAnchorNames] where id = " + id);

        using (var command = new DBCommand(sb.ToString(), QueryType.TransectSQL, null, db.Connection))
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

        sb.Append("IF NOT EXISTS(SELECT * FROM NTQ_LookupAnchorNames where [Name] =@Name) ");
        sb.Append(" BEGIN");
        sb.Append(" INSERT INTO [NTQ_LookupAnchorNames] ([Name], [timestamp], [CreatedBy], [CreatedDate]) VALUES (@Name, @timestamp, @CreatedBy, @CreatedDate)");
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
            MessageBox.Show("Anchor Inserted successfully");
            BindAnchor();
        }
    }

    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // this.gvUsers.NextPage(this.CurrentProcess.Users, e.NewPageIndex);
    }
}