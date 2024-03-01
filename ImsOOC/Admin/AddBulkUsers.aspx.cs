using System.Data;
using Sars.Systems.Data;
using Sars.Systems.Security;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddBulkUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // LoadData();
        if (!IsPostBack)
        {
            Search();
        }
    }

    private void LoadData()
    {
        try
        {
            var roles = Roles.GetActiveRoles();
            if (roles.Count == 0)
            {
                throw new SecureException("There are no active roles in the system.");
            }
            roles.Sort();
            lstRoles.Items.Clear();
            roles.ForEach(role => lstRoles.Items.Add(role));
            //if (roles.Count() > 10 && roles.Count() < 15)
            //    lstRoles.RepeatColumns = 2;

            //if (roles.Count >= 15)
            //    lstRoles.RepeatColumns = 2;
        }
        catch (SecureException ex)
        {
           // lblError.SetValue(ex.Message);
        }

    }

    private void Search()
    {
        lstRoles.Items.Clear();

        string user;
        if (Sars.Systems.Data.SARSDataSettings.Settings.AttachDomain)
        {
            user = string.Format("{0}\\{1}", Sars.Systems.Data.SARSDataSettings.Settings.DomainName,
                                SarsUser.CurrentUser.SID);
        }
        else
        {
            user = "s2022311";
        }
        var roles = Roles.GetUserRoles(user);
        lstRoles.DataSource = roles;
        lstRoles.DataTextField = "Description";
        lstRoles.DataValueField = "RoleId";
        lstRoles.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!flDoc.HasFile)
        {
            flDoc.Focus();
            MessageBox.Show("Please select a file to upload");
            return;
        }
        var fileName = flDoc.PostedFile;
        var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; data source={0}; Extended Properties='Excel 8.0;HDR=YES'", fileName.FileName);

        var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
        var ds = new DataSet();
        adapter.Fill(ds, "anyNameHere");
        DataTable data = ds.Tables["anyNameHere"];
        List<string> list = new List<string>();
        if (data.HasRows())
        {
            foreach (DataRow sid in data.Rows)
            {
                var user = string.Format("{0}\\{1}", Sars.Systems.Data.SARSDataSettings.Settings.DomainName,
                    sid[0].ToString());
                if (!string.IsNullOrEmpty(user))
                {
                    try
                    {
                        Roles.AddUserToRole(lstRoles.SelectedItem.Text, user);
                    }
                    catch (Sars.Systems.Security.SecureException exception)
                    {                        
                    }
                    try
                    {
                        var userSid = sid[0].ToString();
                        SarsUser.SaveUser(userSid);
                        var recordsAffected = IncidentTrackingManager.AddUserToAProcess(userSid, txtProcessId.Text, "2");

                    }
                    catch (Exception)
                    {
                            
                       
                    }
                }
            }
            grdError.DataSource = list;
            grdError.DataBind();
        }
       // 


    }
}