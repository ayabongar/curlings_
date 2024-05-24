using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;
using Sars.Systems.Extensions;

public partial class Admin_ViewAuditTrail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole("Administrators"))
        {
            Response.Redirect("~/AccessDenied.aspx");
        }
      
    }
    protected void btnDownloadAuditTrail_Click(object sender, EventArgs e)
    {
        DateTime date;

        if(!string.IsNullOrWhiteSpace(txtStartDate.Text))
        {
            if(!DateTime.TryParse(txtStartDate.Text, out date))
            {
                MessageBox.Show("Start Date is not valid date.");
                return;
            }
        }

        if (!string.IsNullOrWhiteSpace(txtEndDate.Text))
        {
            if (!DateTime.TryParse(txtEndDate.Text, out date))
            {
                MessageBox.Show("End Date is not valid date.");
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrWhiteSpace(txtEndDate.Text))
        {
            var oParams = new DBParamCollection
                {
                    {"@StarDate", txtStartDate.Text}
                    ,
                    {"@EndDate", txtEndDate.Text}
                };
            using (var data = new RecordSet("[dbo].[uspGetAuditTrail_ByDate]", QueryType.StoredProcedure, oParams, db.ConnectionString))
            {
                if (data.HasRows)
                {
                    data.Tables[0].ToExcel("SURVEY AUDIT TRAIL", new Dictionary<int, CellFormartting> { { 2, CellFormartting.LongDate } });
                }
            }
        }
        else
        {
            using (var data = new RecordSet("uspGetAuditTrail", QueryType.StoredProcedure, null, db.ConnectionString))
            {
                if (data.HasRows)
                {
                    data.Tables[0].ToExcel("SURVEY AUDIT TRAIL",
                                           new Dictionary<int, CellFormartting> {{2, CellFormartting.LongDate}});
                }
            }
        }
    }
}