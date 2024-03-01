using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemAdmins_ManageMimeTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("~/admin/SelectNormalUserProcess.aspx");
        }
        if(!IsPostBack)
        {
            LoadMimeTypes();
        }
    }

    protected void SaveFileType(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(txtExtension.Text.Trim()))
        {
            txtExtension.Focus();
            MessageBox.Show("File Extension is required");
            return;
        }
        if(txtExtension.Text.IndexOf('.') != 0)
        {
            txtExtension.Focus();
            MessageBox.Show("All file extensions must start with a dot (.)");
            return;
        }
        if(string.IsNullOrEmpty(txtMimeType.Text.Trim()))
        {
            txtMimeType.Focus();
            MessageBox.Show("File type is required");
            return;
        }
        if(txtMimeType.Text.IndexOf('/') == -1)
        {
            txtMimeType.Focus();
            MessageBox.Show("File type must be in this format (application/type) e.g. image/bmp");
            return;
        }

        var extension = txtExtension.Text;
        var fileType = txtMimeType.Text;
        var saved = IncidentTrackingManager.SaveFileType(extension, fileType);
        MessageBox.Show(saved > 0 ? "File type saved successfully" : "There was a problem saving this file type.");
        LoadMimeTypes();
        txtExtension.Clear();
        txtMimeType.Clear();
    }

    private void LoadMimeTypes()
    {
        gvFileTypes.Bind(IncidentTrackingManager.GetFileTypes());
    }
    protected void PageChnaged(object sender, GridViewPageEventArgs e)
    {
        gvFileTypes.PageIndex = e.NewPageIndex;
        LoadMimeTypes();
    }
}