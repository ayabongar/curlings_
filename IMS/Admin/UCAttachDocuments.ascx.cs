using System;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

public partial class AttachDocuments : IncidentTrackingControl
{
    protected Incident cIncidentProcess;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request["noteId"]))
        {
            return;
        }
        cIncidentProcess = CurrentIncident;
        //LoadDocuments();
        if(!IsPostBack)
        {
            

            //var note = IncidentTrackingManager.GetWorkInfoByID(Request["noteId"]);

            //if (!SarsUser.SID.Equals(note.AddedBySID, StringComparison.CurrentCultureIgnoreCase))
            //{
            //    flDoc.Enabled = false;
            //}
        }
    }
    protected void UploadFile(object sender, EventArgs e)
    {
        try
        {

            var maxFileSize = CurrentProcess.MaxFileSize;

            //const int maxSize = 255000;

            if (!flDoc.HasFile)
            {
                flDoc.Focus();
                MessageBox.Show("Please select a file to upload");
                return;
            }
            var uploadedFile = flDoc.PostedFile;

            if (uploadedFile.ContentLength > maxFileSize)
            {
                flDoc.Focus();
                MessageBox.Show("This file is bigger than the accepted file size");
                return;
            }
            if (uploadedFile.ContentLength > 0)
            {
                var buffer = new byte[uploadedFile.ContentLength];
                uploadedFile.InputStream.Read(buffer, 0, buffer.Length);
                var noteId = Request["noteId"];
                var fileName = flDoc.FileName;

                var saved = IncidentTrackingManager.UploadFile(IncidentID, noteId, buffer, fileName);

                if (saved > 0)
                {
                    LoadDocuments();
                    MessageBox.Show("File uploaded successfully.");
                }
            }
        }
        catch (Exception)
        {

            MessageBox.Show("File uploaded unsuccessfully, please try again.");
        }
    }

    public void LoadDocuments()
    {
        var data = IncidentTrackingManager.GetDocumentsByIncidentId(IncidentID);
        if(data != null && data.Any())
        {
            gvDocs.Bind(data);
        }
        else
        {
            gvDocs.Bind(null);
        }
    }
    protected void gvDocs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDocs.PageIndex = e.NewPageIndex;
        LoadDocuments();
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
          switch (e.CommandName)
        {
            case "Submit":
                {
                    //UploadFile();
                    break;
                }
              case "Cancel":
            {
                Response.Redirect(String.IsNullOrEmpty(Request["pp"])
                    ? String.Format("~/Admin/RegisterUserIncident.aspx?procId={0}&incId={1}", ProcessID, IncidentID)
                    : String.Format("~/Admin/IncidentRealOnly.aspx?procId={0}&incId={1}", ProcessID, IncidentID));
                break;
            }
        }
    }
    protected void gvDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Request.PhysicalApplicationPath != null)
        {
            var directory = Path.Combine(Request.PhysicalApplicationPath, "Downloads");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var files = new DirectoryInfo(directory).GetFiles();// Directory.GetFiles(directory);
            if(files.Any())
            {
                var oldFiles = files.ToList().FindAll(fi => fi.CreationTime < DateTime.Now.AddHours(-1));
                if(oldFiles.Any())
                {
                    try
                    {
                        foreach (var t in oldFiles)
                        {
                            File.Delete(t.FullName);
                        }
                    }
                    catch (Exception)
                    {
                        ;
                    }
                }
            }
            try
            {
                if (e.CommandName.Equals("OpenFile", StringComparison.CurrentCultureIgnoreCase))
                {
                    var response = Response;
                    var docId = e.CommandArgument;
                    IncidentTrackingManager.OpenFile(directory, docId, response);
                }
                if (e.CommandName.Equals("DeleteFile", StringComparison.CurrentCultureIgnoreCase))
                {
                    var docId = e.CommandArgument.ToString();
                    var deleted = IncidentTrackingManager.DeleteFile(docId);

                    if (deleted <= 0) return;
                    LoadDocuments();
                    MessageBox.Show("File deleted successfully");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
    }
}