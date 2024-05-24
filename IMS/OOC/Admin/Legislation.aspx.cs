using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;

public partial class Admin_Legislation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            var sid = SarsUser.SID;
            if (!string.IsNullOrEmpty(sid))
            {
                SarsUser.SaveUser(sid);
                var recordsAffected = IncidentTrackingManager.AddUserToAProcess(sid, "101", "2");


                Response.Redirect("http://ptabriis06/ims/admin/NormalUserLandingPage.aspx?procId=101");
            }
        }
        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{

        //    foreach (DataRow item in GetSarsUser().Rows)
        //    {
        //        var sid = item["SID"].ToString();
        //        if (!string.IsNullOrEmpty(sid))
        //        {
        //            SarsUser.SaveUser(sid);
        //            var recordsAffected = IncidentTrackingManager.AddUserToAProcess(sid, processId.Text, ddlRole.SelectedValue);
        //            if (recordsAffected > 0)
        //            {
        //                //MessageBox.Show("User Added to the process");
        //            }
        //           }

        //    }

        //}

        //public static RecordSet GetSarsUser()
        //{

        //    using (
        //        var lists = new RecordSet("Select * from AllUsers", QueryType.TransectSQL, null,
        //                                  db.SapConnectionString))
        //    {
        //        return lists;
        //    }
        //}
    }
}
