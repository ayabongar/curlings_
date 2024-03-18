using System;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

public partial class SurveyWizard_AttachDocuments : IncidentTrackingPage
{
    protected Incident cIncidentProcess;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request["noteId"]))
        {
            return;
        }
        cIncidentProcess = CurrentIncident;
        if(!IsPostBack)
        {
            LoadDocuments();

            var note = IncidentTrackingManager.GetWorkInfoByID(Request["noteId"]);

            if (!SarsUser.SID.Equals(note.AddedBySID, StringComparison.CurrentCultureIgnoreCase))
            {
                flDoc.Enabled = false;
                Toolbar1.Items[0].Visible = false;
            }
        }
    }
    protected void UploadFile()
    {
        var maxFileSize = CurrentProcess.MaxFileSize;
        if(!flDoc.HasFile)
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

    private void LoadDocuments()
    {
        var data = IncidentTrackingManager.GetDocumentsByNoteId(Request["noteId"]);
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
                    UploadFile();
                    break;
                }
              case "Cancel":
                {
                    if(String.IsNullOrEmpty(Request["pp"]))
                    {
                        Response.Redirect(String.Format("~/Admin/RegisterUserIncident.aspx?procId={0}&incId={1}", ProcessID, IncidentID));
                    }
                    else
                    {
                        Response.Redirect(String.Format("~/Admin/IncidentRealOnly.aspx?procId={0}&incId={1}", ProcessID, IncidentID));
                    }
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
            if(e.CommandName =="OpenFile")
            {
                var response = Response;
                var docId = e.CommandArgument;
                IncidentTrackingManager.OpenFile(directory, docId, response);
            }
        }
    }
}