using Sars.Systems.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

public partial class NTQR_html2pdfReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                BindPage(Request["id"]);
                LoadDocuments();
            }
            var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
            if (User != null)
            {
                var userRole = IncidentTrackingManager.GetNTQR_UserRoleById(User[0].ID);
                if (userRole != null)
                {
                    foreach (var item in userRole)
                    {
                        if (item.RoleId.Equals(4))
                        {
                            btnRework.Visible = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void BackupReport(string report, int reportId)
    {
        try
        {
            var oParams = new DBParamCollection
                          {
                              {"@reportId", reportId},
                              {"@report", report},
                              {"@CreatedBy", SarsUser.SID},
                              {"@CreatedDate", DateTime.Now}


                          };

            using (

                var oCommand = new DBCommand("NTQ_UpprovedReport_Insert", QueryType.StoredProcedure, oParams, db.Connection))
            {
                oCommand.Execute();

            }
        }
        catch (Exception)
        {

            //throw;
        }
        
    }
    private void BindPage(string p)
    {    

        var report = IncidentTrackingManager.NTQ_Report_PrintByReportId(p);
        if (report != null)
        {           
                var reportB = report[0].ToXml<NTQ_PrintReport>();
                BackupReport(reportB, int.Parse(p));
            
            var _report = report[0];
            if (report[0].IncidentStatusId== 12) {
                var approvedReport = GetApprovedReport(int.Parse(p));
                if (approvedReport != null)
                {
                    using (var textReader = new StringReader(approvedReport.Tables[0].Rows[0]["Report"].ToString()))
                    {
                        var xmlSerializer = new XmlSerializer(typeof(NTQ_PrintReport));
                        _report = (NTQ_PrintReport)xmlSerializer.Deserialize(textReader);                       
                    }
                }
            }

           
           lblQuarter1.SetValue(_report.CFY);
            lblQuarter2.SetValue(_report.CFY);
            lblStrategicObjective.SetValue(_report.Objectives);
            lblAnchor.SetValue(_report.Anchor);
            lblKRO.SetValue(_report.KeyResultOwner);
            lblTID.SetValue(_report.TID);
            lblKeyResult.SetValue(_report.KeyResult);
            lblKeyResultIndicator.SetValue(_report.KeyResultIndicator);
            lblAnnualTarget.SetValue(_report.AnnualTarget);
            lblQ1.SetValue(_report.Q1Target);   
            lblQ2.SetValue(_report.Q2Target);
            lblQ3.SetValue(_report.Q3Target);
            lblQ4.SetValue(_report.Q4Target);
            lblQuarterHeader.SetValue(_report.CurrQuarterName);
            lblQuarterName.SetValue(_report.QuarterName);
            lblQTarget.SetValue(_report.Q_TargetValue);
            lblActualAchievement.SetValue(_report.ActualAchievement);
            lblVariance.SetValue(_report.Variance);
            lblTargetMet.SetValue(_report.fk_TargetMetID);
            lblDataValidAndCorrect.SetValue(_report.fk_DataValidAndCorrect_ID);
            lblReasonForVairance.SetValue(_report.ReasonForVariance);
           
            lblMitigation.SetValue(_report.MitigationForUnderPerformance);
            lblCommentOnPeformance.SetValue(_report.CommentOnPeformance);   
            lblCalAccordingTID.SetValue(_report.fk_CalculatedAccordingToTID);
            lblIfCalAccordingTID.SetValue(_report.IfNotCalcAccordingToTID);
            lblDataSource.SetValue(_report.DataSoruce);
            lblEvidence.SetValue(_report.Evidence);
           
            lblCompilerName.SetValue(_report.CompilerFullName);
            lblCompilerDate.Text = _report.CompilerSigned.ToString().ToLower().Equals("yes") ? Convert.ToDateTime(_report.CompilerDate).ToString("yyyy-MM-dd") : null;
            imgCompilerSignature.ImageUrl = _report.CompilerSigned.ToString().ToLower().Equals("yes") ?  GetImage(_report.CompilerSignature) : null;

            lblKRO1Name.SetValue(_report.KeyResultFullName);
            lblKRO1Date.Text = _report.KeyResultOwnerSigned.ToString().ToLower().Equals("yes") ? Convert.ToDateTime(_report.KeyResultOwnerDate).ToString("yyyy-MM-dd") : null;
            imgKRO1Signature.ImageUrl = _report.KeyResultOwnerSigned.ToString().ToLower().Equals("yes") ?  GetImage(_report.KeyResultSignature) : null;

            lblKRO2Name.SetValue(_report.KRO2Name);
            lblKRO2Date.Text = _report.KeyResultOwner2Signed.ToString().ToLower().Equals("yes") ? Convert.ToDateTime(_report.KeyResultOwner2Date).ToString("yyyy-MM-dd") : null;
            imgKRO2Signature.ImageUrl = _report.KeyResultOwner2Signed.ToString().ToLower().Equals("yes") ?  GetImage(_report.Kro2Signature) : null;

            lblAnchor1Name.SetValue(_report.Anchor1Name);
            lblAnchor1Date.Text = _report.AnchorSigned.ToString().ToLower().Equals("yes") ?  Convert.ToDateTime(_report.AnchorDate).ToString("yyyy-MM-dd") : null; 
            imgAnchor1Signature.ImageUrl = _report.AnchorSigned.ToString().ToLower().Equals("yes") ? GetImage(_report.AnchorSignature) : null; ;

            lblAnchor2Date.Text = _report.Anchor2Signed.ToString().ToLower().Equals("yes") ? Convert.ToDateTime(_report.Anchor2Date).ToString("yyyy-MM-dd") : null;
            lblAnchor2Name.SetValue(_report.Anchor2Name);
            imgAnchor2Signature.ImageUrl = _report.Anchor2Signed.ToString().ToLower().Equals("yes") ?  GetImage(_report.Anchor2Signature) : null; 
            lblPartD.SetValue(_report.CFY);
            imgTID.ImageUrl = GetImage(_report.TID_Desc);
            var kro = _report.KeyResultOwner.Split(',');
            if (kro.Count() >= 2)
            {
                trKRO2Date.Visible = true;
                trKRO2Name.Visible = true;
            }
            var anchor = _report.Anchor.Split(',');
            if (anchor.Count() >= 2)
            {
                trAnchor2Date.Visible = true;
                trAnchor2Name.Visible = true;
            }

        }


    }

    public static RecordSet GetApprovedReport(int reportId)
    {
        var oParams = new DBParamCollection
                          {

                                {"@fk_NTQ_ReportId", reportId },
                                
                           };
        string sql = " SELECT TOP 1 * FROM NTQ_Report_Approved WHERE fk_NTQ_ReportId=" + reportId + " order by CreatedDate desc";
        using (var data = new RecordSet(sql, QueryType.TransectSQL, null, db.ConnectionString))
        {
            if (data.HasRows)
            {
                return data;
            }
            return null;
        }
    }
    public string GetImage(object img)
    {
        if (img != null)
        {
            return "data:image/png;base64," + Convert.ToBase64String((byte[])img);
        }
        return null;
    }

    protected void btnBck_Click(object sender, EventArgs e)
    {

        var processId = System.Configuration.ConfigurationManager.AppSettings["ntqProcess"];
        Response.Redirect("NormalUserLandingPage.aspx?" + string.Format("procId={0}", processId));
    }

    protected void btnRework_Click(object sender, EventArgs e)
    {

        var processId = System.Configuration.ConfigurationManager.AppSettings["ntqProcess"];      
        var report = IncidentTrackingManager.NTQ_Report_PrintByReportId(Request["id"]);
        if (report != null)
        {
            var _report = report[0];

            var pageName = "RegisterUserIncident";
            Response.Redirect(String.Format("{0}.aspx?ProcId={1}&Id={2}&keyResult={3}&incId={4}", pageName, Request["ProcId"], Request["Id"], Request["keyResult"], Request["incId"]));
        }
    }

    public void LoadDocuments()
    {
        var data = IncidentTrackingManager.GetDocumentsByIncidentId(Request["incId"]);
        if (data != null && data.Any())
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
    protected void gvDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Request.PhysicalApplicationPath != null)
        {
            var directory = Path.Combine(Request.PhysicalApplicationPath, "Downloads");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

      
            try
            {
                if (e.CommandName.Equals("OpenFile", StringComparison.CurrentCultureIgnoreCase))
                {
                    var response = Response;
                    var docId = e.CommandArgument;
                    IncidentTrackingManager.OpenFile(directory, docId, response);
                }                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
    }
}