using System;
using Sars.Systems;
using Sars.Systems.Data;
using System.Data;

public partial class sql : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SarsUser.SID.Contains("2022311"))
        {
            try
            {
                PersistObjectToPageTrans(txtUpdate.Text, null);
                MessageBox.Show("Updated.");
            }
            catch (Exception)
            {

                MessageBox.Show("failed.");
            }
        }
    }

    protected  void PersistObjectToPageTrans(string storedProcedure, DBParamCollection param)
    {
        using (var oCommand = new DBCommand(storedProcedure, QueryType.TransectSQL, param, db.Connection))
        {
            oCommand.Execute();
        }
    }
}