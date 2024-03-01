using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NTQR_LookupKeyResultIndicator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        try
        {
            if (e.Command.CommandType == System.Data.CommandType.Text)
            {
                if (e.Command.Parameters.Count > 0)
                {
                    var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);

                    SqlDataSource1.UpdateParameters["ModifiedBy"].DefaultValue = User[0].ID.ToString();
                    SqlDataSource1.UpdateParameters["ModifiedDate"].DefaultValue = DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource1.Update();
                }
            }

        }
        catch
        { }
    }

    protected void ListView1_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        //SqlDataSource1.Update();
    }
}