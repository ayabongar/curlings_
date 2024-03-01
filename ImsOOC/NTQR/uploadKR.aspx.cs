
using Sars.Systems.Data;
using System;
using System.Web.UI.WebControls;

public partial class NTQR_uploadKR : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var results = IncidentTrackingManager.GetLookupObjectives();
            drpStrategicObjective.Bind(results, "Name", "Id");
        }
    }

    private void SaveFile(int fk_StrategicObjective_ID,string description,int userId)
    {
        string sql = @"insert into NTQ_Lookup_TID (fk_StrategicObjective_ID,[Description],Name,timestamp,CreatedBy,CreatedDate) VALUES(@fk_StrategicObjective_ID,@Description,@Name,@timestamp,@CreatedBy,@CreatedDate)";
        var oParams = new DBParamCollection
                          {
                              {"@fk_StrategicObjective_ID", fk_StrategicObjective_ID},
                              {"@Description", description},
                              {"@Name", UploadFile(fileUpload)},
                              {"@timestamp", DateTime.Now},
                              {"@CreatedBy", userId},
                              {"@CreatedDate", DateTime.Now},

    };
        using ( var command = new DBCommand(sql, QueryType.TransectSQL, oParams,db.Connection))
        {
             command.Execute();
        }
    }
    public static byte[] UploadFile(FileUpload file)
    {
        string fileExtension = String.Empty;
        byte[] fileString = null;
        if (file.HasFile)
        {
            var index = file.FileName.LastIndexOf(".");
            fileExtension = file.FileName.Substring(index + 1);
            fileString = file.FileBytes;
        }
        return fileString;
    }

    public string GetImage(object img)
    {
        if (img != null)
        {
            return "data:image/png;base64," + Convert.ToBase64String((byte[])img);
        }
        return null;
    }

    protected void UploadFile(object sender, EventArgs e)
    {
        var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
        if (User != null)
        {
            if (drpStrategicObjective.SelectedIndex > 0 && !string.IsNullOrEmpty(txtKRO.Text))
            {
                SaveFile(int.Parse(drpStrategicObjective.SelectedValue), txtKRO.Text, User[0].ID);

                var adUser = SarsUser.GetADUser(SarsUser.SID);
                if (adUser != null)
                {
                    // IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} added a New KRO Image  {1} on {2}", adUser.SID + " | " + adUser.FullName,  DateTime.Now), string.Empty, SarsUser.SID, DateTime.Now);
                }
            }
            MessageBox.Show("The record has been uploaded successfully");
        }
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

                }
            }

        }
        catch
        { }

    }

    protected void ListView1_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        SqlDataSource1.Update();
    }
}