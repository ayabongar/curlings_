using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ApplicationSettings : System.Web.UI.Page
{

    public class MySetting
    {
        public MySetting(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public string Key { get; set; }
        public string Value { get; set; }
    }
    protected void Page_Load(object sender, EventArgs e) 
    {
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("~/admin/SelectNormalUserProcess.aspx");
        }
        if (!IsPostBack)
        {
            var path = Request.ApplicationPath;
            Configuration rootWebConfig = null;
            try
            {
                rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            }
            catch (System.Exception)
            {
            }

            if(rootWebConfig !=null)
            {
                var mySettings = new List<MySetting>();
                if (rootWebConfig.AppSettings.Settings.Count > 0)
                {
                    mySettings.AddRange(
                        rootWebConfig.AppSettings.Settings.AllKeys.Select(
                            setting => new MySetting(setting, rootWebConfig.AppSettings.Settings[setting].Value)));
                }

                if (mySettings.Count > 0)
                {
                    GridView1.Bind(mySettings);
                }
            }
        }
    }
    protected void Update(object sender, EventArgs e)
    {
        var path = Request.ApplicationPath;
        var rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);

        
        var b = sender as Button;
        if(b != null)
        {
            var row = b.Parent.Parent as GridViewRow;

            if(row != null)
            {
                var txtValue = row.FindControl("txtValue") as TextBox;
                if(txtValue != null)
                {
                    GridView1.SelectedIndex = row.RowIndex;
                    if (GridView1.SelectedDataKey != null)
                    {
                        var key = GridView1.SelectedDataKey.Value.ToString();
                        rootWebConfig.AppSettings.Settings[key].Value = txtValue.Text;
                        rootWebConfig.Save();
                        MessageBox.Show("Item saved successfully.");
                    }
                }
            }
        }
    }
}