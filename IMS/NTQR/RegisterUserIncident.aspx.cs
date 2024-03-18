using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel.Activities;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Sars.Systems.Data;
using Sars.Systems.Security;


public partial class NTQR_RegisterUserIncident : IncidentTrackingPage
{
    protected Incident CurrentIncidentDetails;
    protected IncidentProcess CurrentProcessDetails;

    public string KeyOwnerViewState
    {
        set { ViewState["KeyOwner"] = value; }
        get { return ViewState["KeyOwner"] as string; }
    }
    public string reportId
    {
        set { ViewState["reportId"] = value; }
        get { return ViewState["reportId"] as string ; }
    }
    public string AnchorViewState
    {
        set { ViewState["anchor"] = value; }
        get { return ViewState["anchor"] as string; }
    }
    public string CurrentUser
    {
        get { return ViewState["CurrentUser"] as string; }
        set { ViewState["CurrentUser"] = value; }
    }

    public string FY
    {
        get { return ViewState["FY"] as string; }
        set { ViewState["FY"] = value; }
    }
    public string StatusId 
    {
        set { ViewState["StatusId"] = value; }
        get { return ViewState["StatusId"] as string; }
    }
    public bool isUserInTheProcess(string sid)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT u.[ID],[UserCode],[UserFullName] FROM NTQR_User  u inner join [dbo].[NTQR_UserRoles] r on u.id = r.UserId where UserCode = '" + sid + "'");


        using (var data = new RecordSet(sb.ToString(), QueryType.TransectSQL, null, db.ConnectionString))
        {
            if (data.HasRows)
            {
                return true;
            }
            return false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ProcessID))
        {
            MessageBox.Show("Process ID is not available");
            return;
        }
        if (!isUserInTheProcess(SarsUser.SID.ToLower()))
        {
            MessageBox.ShowAndRedirect("You do not have access to the Quarterly report Process", "../Default.aspx");
            return;
        }
        KeyOwnerViewState = string.Empty;

        CurrentIncidentDetails = CurrentIncident;
        CurrentProcessDetails = CurrentProcess;


        if (!IsPostBack)
        {
            if(int.Parse(Request["incId"]) == 0)
            {
                Response.Redirect("NormalUserLandingPage.aspx?procId=140");
            }
            StatusId = "0";
            reportId = "0";
            FY = String.Empty;
            Toolbar1.Items[5].CausesValidation = false;
            Toolbar1.Items[6].CausesValidation = false;
            BindDropDownlist();
            if (!string.IsNullOrEmpty(Request["Id"]))
            {
                BindPage(Request["Id"]);
            }
            var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);
            if (User != null)
            {
                var userRole = IncidentTrackingManager.GetNTQR_UserRoleById(User[0].ID);
                if (userRole != null)
                {
                    StatusId = CurrentIncidentDetails.IncidentStatusId.ToString();
                    bool userHasAssignedRole = false;
                    switch (int.Parse(StatusId))
                    {
                        case 1:
                        case 9:
                            foreach (var item in userRole)
                            {
                                if (item.RoleId.Equals(1))
                                {
                                    userHasAssignedRole = true;
                                    break;
                                }
                            }
                            if(userHasAssignedRole) {
                                pnlCompiler.Enabled = true;
                                Toolbar1.Items[0].Visible = true;
                                Toolbar1.Items[1].Visible = true;
                                // hide other status panels
                                pnlAnchor.Visible = false;
                                pnlKeyResultApproval.Visible = false;
                                pnlAnchor2.Visible = false;
                                pnlKeyResultApproval2.Visible = false;
                            }
                          
                                    break;
                        case 10:
                            foreach (var item in userRole)
                            {
                                if (item.RoleId.Equals(2))
                                {
                                    userHasAssignedRole = true;
                                    break;
                                }
                            }
                            if (userHasAssignedRole)
                            {
                                pnlCompiler.Enabled = false;
                                pnlAnchor.Visible = false;
                                pnlAnchor2.Visible = false;
                                pnlKeyResultApproval.Enabled = drpKeyResultOwnerAprover.SID.ToLower().Contains(SarsUser.SID.ToLower()) && drpKeyResultOwnerSigned.SelectedValue.Equals("false") ? true : false;
                                pnlKeyResultApproval2.Enabled = drpKeyResultOwner2Aprover.SID.ToLower().Contains(SarsUser.SID.ToLower()) && drpKeyResultOwner2Signed.SelectedValue.Equals("false") ? true : false;
                                if (KeyOwnerViewState.ToLower().Contains(SarsUser.SID.ToLower()))
                                {
                                    Toolbar1.Items[4].Visible = true;
                                    Toolbar1.Items[3].Visible = true;
                                    pnlKeyResultApproval2.Visible = false;

                                    string[] kerResuts = txtKeyResultOwnerName.Text.Split(',');
                                    if (kerResuts.Count() >= 2)
                                    {
                                        if (drpKeyResultOwnerSigned.SelectedValue.Equals("true") || drpKeyResultOwner2Signed.SelectedValue.Equals("true"))
                                        {
                                            if (drpKeyResultOwnerSigned.SelectedValue.Equals("true") && drpKeyResultOwner2Signed.SelectedValue.Equals("true"))
                                            {
                                                Toolbar1.Items[3].Visible = false;
                                            }
                                        }
                                        else
                                        {
                                            Toolbar1.Items[1].Visible = true;
                                            Toolbar1.Items[3].Visible = false;
                                        }
                                    }

                                    if(drpKeyResultOwnerSigned.SelectedValue.Equals("true"))
                                    {
                                        pnlKeyResultApproval.Visible = true;
                                        pnlKeyResultApproval.Enabled = false;
                                    }

                                    if (drpKeyResultOwner2Signed.SelectedValue.Equals("true"))
                                    {
                                        pnlKeyResultApproval2.Visible = true;
                                        pnlKeyResultApproval2.Enabled = false;
                                    }
                                }
                            }
                            break;
                        case 11:
                            foreach (var item in userRole)
                            {
                                if (item.RoleId.Equals(3))
                                {
                                    userHasAssignedRole = true;
                                    break;
                                }
                            }
                            if (userHasAssignedRole)
                            {
                                pnlCompiler.Enabled = false;
                                pnlCompiler.Visible = true;
                                pnlKeyResultApproval.Visible = true;
                                pnlKeyResultApproval.Enabled = false;
                                pnlAnchor2.Visible = txtAnchorName.Text.Contains(",") ? true : false;
                                pnlKeyResultApproval2.Visible = txtKeyResultOwnerName.Text.Contains(",") ? true : false;
                                pnlKeyResultApproval2.Enabled = false;
                                pnlAnchor.Visible = true;

                                pnlAnchor.Visible = drpAnchorName.SID.ToLower().Contains(SarsUser.SID.ToLower()) ? true : false;
                                pnlAnchor2.Visible = drpAnchor2Name.SID.ToLower().Contains(SarsUser.SID.ToLower()) ? true : false;

                                pnlAnchor.Enabled = drpAnchorName.SID.ToLower().Contains(SarsUser.SID.ToLower()) && drpAnchorSigned.SelectedValue.Equals("false") ? true : false;
                                pnlAnchor2.Enabled = drpAnchor2Name.SID.ToLower().Contains(SarsUser.SID.ToLower()) && drpAnchor2Signed.SelectedValue.Equals("false") ? true : false;

                                

                                if (AnchorViewState.ToLower().Contains(SarsUser.SID.ToLower()))
                                {                                    
                                    Toolbar1.Items[4].Visible = true;
                                    string[] anchors = txtAnchorName.Text.Split(',');
                                    if (anchors.Count() >= 2)
                                    {
                                        Toolbar1.Items[3].Visible = true;
                                        if (drpAnchorSigned.SelectedValue.Equals("true") || drpAnchor2Signed.SelectedValue.Equals("true"))
                                        {
                                            Toolbar1.Items[5].Visible = true;
                                            Toolbar1.Items[3].Visible = false;
                                        }                                        
                                    }
                                    else
                                    {
                                        Toolbar1.Items[5].Visible = true;
                                    }

                                    if (drpAnchorSigned.SelectedValue.Equals("true"))
                                    {
                                        pnlAnchor.Visible = true;
                                        pnlAnchor.Enabled = false;
                                    }

                                    if (drpAnchor2Signed.SelectedValue.Equals("true"))
                                    {
                                        pnlAnchor2.Visible = true;
                                        pnlAnchor2.Enabled = false;
                                    }
                                }
                            }
                            break;
                        case 12:
                            pnlCompiler.Visible = true;
                            pnlKeyResultApproval.Visible = true;
                            pnlAnchor.Visible = true;
                            pnlAnchor2.Visible = true;
                            pnlCompiler.Enabled = false;
                            pnlKeyResultApproval.Enabled = false;
                            pnlAnchor.Enabled = false;
                            pnlAnchor2.Visible = (!txtAnchorName.Text.Contains(",")) ? false : true;
                            pnlAnchor2.Enabled = false;
                            pnlKeyResultApproval2.Visible = (!txtKeyResultOwnerName.Text.Contains(",")) ? false : true;
                            pnlKeyResultApproval2.Enabled = false;
                            var reportBackup = IncidentTrackingManager.NTQ_Report_PrintByReportId(Request["Id"]);
                            if (reportBackup != null)
                            {
                                var reportB = reportBackup[0].ToXml<NTQ_PrintReport>();
                                AnchorBackupReport(reportB, int.Parse(Request["Id"]));
                            }
                            break;
                        default:
                            break;
                    }

                    foreach (var item in userRole)
                    {
                        if (item.RoleId.Equals(4))
                        {
                            Toolbar1.Items[4].Visible = true;
                            break;
                        }
                    }                     
                }
             
                var year = DateTime.Now.Year + 1;
                var dueDate = DateTime.Parse(year + "-03-31");
                txtIncidentDueDate.SetValue(dueDate.ToString("yyyy-MM-dd"));
                var adUser = SarsUser.GetADUser(SarsUser.SID);
                UserSelector1.SelectedAdUserDetails = new SelectedUserDetails
                {
                    SID = adUser.SID,
                    FoundUserName =
                                                                  string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                    FullName = adUser.FullName
                };

                LoadInfo();
                txtCreatedBy.Value = string.Format("{0} | {1}", SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).FullName, SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID).SID);

                UCAttachDocuments.LoadDocuments();
                if (CurrentIncident.IncidentStatusId == 1 || CurrentIncident.IncidentStatusId == 9)
                {
                    pnlKeyResultApproval.Enabled = false;
                    pnlKeyResultApproval2.Enabled=false;
                    var compiler = SarsUser.GetADUser(SarsUser.SID);
                    drpCompilers.SelectedAdUserDetails = new SelectedUserDetails
                    {
                        SID = adUser.SID,
                        FoundUserName = string.Format("{0} | {1}", compiler.FullName, compiler.SID),
                        FullName = adUser.FullName
                    };
                }                
            }
            Toolbar1.Items[0].CausesValidation = false;
        }
    }
    private string BindPage(string p)
    {
        var report = IncidentTrackingManager.NTQ_Report_SelectByReportId(p);
        if (report != null)
        {

            drpQuarter.SelectedValue = report[0].fk_Quarter_ID.ToString();
            drpQuarter.Enabled = false;
            //drpKeyResult.SelectedValue = report[0].fk_ReportKeyResult_ID.ToString();
            txtActualAchievement.Text = report[0].ActualAchievement;
            txtVariance.Text = report[0].Variance;
            drpTargetMet.SelectedValue = report[0].fk_TargetMetID.ToString();
            drpDataValidAndCorrect.SelectedValue = report[0].fk_DataValidAndCorrect_ID.ToString();
            txtReasonForVariance.Text = report[0].ReasonForVariance;
            txtMitigation.Text = report[0].MitigationForUnderPerformance;
            txtCommentOnPerformance.Text = report[0].CommentOnPeformance;
            drpCalculatedAccordingTID.SelectedValue = report[0].fk_CalculatedAccordingToTID.ToString();
            txtIfNotCalculatedAccordingToTID.Text = report[0].IfNotCalcAccordingToTID;
            txtDataSoruce.Text = report[0].DataSoruce;
            txtEvidence.Text = report[0].Evidence;
            drpCompilerSign.SelectedValue = report[0].CompilerSigned.ToString().ToLower();
            txtCompilerDate.Text = report[0].CompilerDate != null ? Convert.ToDateTime(report[0].CompilerDate).ToString("yyyy-MM-dd") : null;
            
            drpKeyResultOwnerSigned.SelectedValue = report[0].KeyResultOwnerSigned.ToString().ToLower();
            drpKeyResultApproved.SelectedValue = report[0].KeyResultOwnerApproved.ToString().ToLower();
            txtKeyResultDate.Text = report[0].KeyResultOwnerDate != null ? Convert.ToDateTime(report[0].KeyResultOwnerDate).ToString("yyyy-MM-dd") : null;

            drpKeyResultOwner2Signed.SelectedValue = report[0].KeyResultOwner2Signed.ToString().ToLower();
            drpKeyResult2Approved.SelectedValue = report[0].KeyResultOwner2Approved.ToString().ToLower();
            txtKeyResult2Date.Text = report[0].KeyResultOwner2Approved.ToString().ToLower().Equals("true") && report[0].KeyResultOwner2Date != null ? Convert.ToDateTime(report[0].KeyResultOwner2Date).ToString("yyyy-MM-dd") : null;


            drpAnchorApproved.SelectedValue = report[0].AnchorApproved.ToString().ToLower();
            drpAnchorSigned.SelectedValue = report[0].AnchorSigned.ToString().ToLower();
            txtAnchorSignedDate.Text = report[0].AnchorDate != null ? Convert.ToDateTime(report[0].AnchorDate).ToString("yyyy-MM-dd") : null;

            drpAnchor2Approved.SelectedValue = report[0].Anchor2Approved.ToString().ToLower();
            drpAnchor2Signed.SelectedValue = report[0].Anchor2Signed.ToString().ToLower();
            txtAnchor2SignedDate.Text = (report[0].Anchor2Signed.ToString().ToLower().Equals("true") &&  report[0].Anchor2Date != null) ? Convert.ToDateTime(report[0].Anchor2Date).ToString("yyyy-MM-dd") : null;

            var adUser = SarsUser.GetADUser(report[0].CompilerName);
            if (adUser != null)
            {
                drpCompilers.SelectedAdUserDetails = new SelectedUserDetails
                {
                    SID = adUser.SID,
                    FoundUserName = string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                    FullName = adUser.FullName
                };
            }
            var kroName = !string.IsNullOrEmpty(txtKeyResultOwnerName.Text) ? txtKeyResultOwnerName.Text.Split('|')[0].Trim() : report[0].KeyResultOwnerName ;
            var _kroName = SarsUser.GetADUser(kroName);
            if (_kroName != null)
            {
                drpKeyResultOwnerAprover.SelectedAdUserDetails = new SelectedUserDetails
                {
                    SID = _kroName.SID,
                    FoundUserName = string.Format("{0} | {1}", _kroName.FullName, _kroName.SID),
                    FullName = _kroName.FullName
                };
            }
            var anchorName = !string.IsNullOrEmpty(txtAnchorName.Text) ? txtAnchorName.Text.Split('|')[0].Trim() : report[0].AnchorName;

            var _anchorName = SarsUser.GetADUser(anchorName);
            if (_anchorName != null)
            {
                drpAnchorName.SelectedAdUserDetails = new SelectedUserDetails
                {
                    SID = _anchorName.SID,
                    FoundUserName = string.Format("{0} | {1}", _anchorName.FullName, _anchorName.SID),
                    FullName = _anchorName.FullName
                };
            }


            // second Approvals
            if (txtKeyResultOwnerName.Text.Contains(","))
            {
                var kro2Name = txtKeyResultOwnerName.Text.Contains(",") ? txtKeyResultOwnerName.Text.Split(',')[1].Split('|')[0].Trim() : report[0].KeyResultOwnerName;
                var _kro2Name = SarsUser.GetADUser(kro2Name);
                if (_kro2Name != null)
                {
                    drpKeyResultOwner2Aprover.SelectedAdUserDetails = new SelectedUserDetails
                    {
                        SID = _kro2Name.SID,
                        FoundUserName = string.Format("{0} | {1}", _kro2Name.FullName, _kro2Name.SID),
                        FullName = _kro2Name.FullName
                    };
                }
            }

            if (txtAnchorName.Text.Contains(","))
            {
                var anchor2Name = txtAnchorName.Text.Contains(",") ? txtAnchorName.Text.Split(',')[1].Split('|')[0].Trim() : report[0].AnchorName;

                var _anchor2Name = SarsUser.GetADUser(anchor2Name);
                if (_anchor2Name != null)
                {
                    drpAnchor2Name.SelectedAdUserDetails = new SelectedUserDetails
                    {
                        SID = _anchor2Name.SID,
                        FoundUserName = string.Format("{0} | {1}", _anchor2Name.FullName, _anchor2Name.SID),
                        FullName = _anchor2Name.FullName
                    };
                }
            }
           
            adUser = SarsUser.GetADUser(SarsUser.SID);
            if (adUser != null)
            {
                IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} viewed the Key result  Report {1}, Financial Year {3} on {2}", adUser.SID + " | " + adUser.FullName, drpKeyResult.SelectedItem.Text, DateTime.Now, FY), report[0].ToXml<NTQ_Report>(), SarsUser.SID, DateTime.Now);
            }
        }

        return report[0].ToXml<NTQ_Report>();
    }   
    private void BindDropDownlist()
    {
        var status = IncidentTrackingManager.GetLookupQuarters();
        var reports = IncidentTrackingManager.NTQ_Report_SelectByKeyResultId(int.Parse(Request["KeyResult"]));
        if (reports != null)
        {
            if (CurrentIncidentDetails.IncidentStatus.Equals("New"))
            {
                foreach (DataRow row in reports.Tables[0].Rows)
                {
                    status.RemoveAll(r => r.Id == int.Parse(row["fk_Quarter_ID"].ToString()));                   
                }
            }
        }
        drpQuarter.Bind(status, "Name", "Id");


        var results = IncidentTrackingManager.GetLookupObjectives();
        drpStrategicObjective.Bind(results, "Name", "Id");
        var users = IncidentTrackingManager.NTQ_SELECT_LookupAnchorNames();
        drpAnchor.Bind(users, "Name", "Id");

        // users = IncidentTrackingManager.NTQ_SELECT_LookupAnchorNames();
        //  drpAnchorName.Bind(users, "Name", "Id");

        users = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_KeyResultOwner();
        drpKeyResultOwner.Bind(users, "Name", "Id");


        // users = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_KeyResultOwner();
        //  drpKeyResultOwnerAprover.Bind(users, "Name", "Id");

        if (Request["keyResult"] != null)
        {
            var obj = IncidentTrackingManager.NTQ_Report_KeyResults_SelectByID(int.Parse(Request["keyResult"]));
            if (obj != null)
            {
                txtFY.Text = obj[0].CFY;
                drpStrategicObjective.SelectedValue = obj[0].fk_Report_Objectives_ID.ToString();
                BindDropDownOnSeleted();
                drpKeyResult.SelectedValue = obj[0].fk_Lookup_KeyResult_ID.ToString();
                drpKeyResultIndicator.SelectedValue = obj[0].fk_KeyResultIndicator_ID.ToString();
                drpAnnualTarget.SelectedValue = obj[0].fk_LookupAnualTarget_ID.ToString();
                drpQuarterOneTarget.SelectedValue = obj[0].fk_lookup_Q1_ID.ToString();
                drpQuarter2Target.SelectedValue = obj[0].fk_lookup_Q2_ID.ToString();
                drpQuarter3Target.SelectedValue = obj[0].fk_lookup_Q3_ID.ToString();
                drpQuarter4Target.SelectedValue = obj[0].fk_lookup_Q4_ID.ToString();
                drpTID.SelectedValue = obj[0].fk_lookupTID.ToString();
                 var tid = IncidentTrackingManager.GetTIDDescriptionById(int.Parse(obj[0].fk_lookupTID.ToString()));
                GetImage(tid[0].Name);
                FY = obj[0].CFY;
                AnchorViewState = obj[0].Anchor;
                string[] anchor = obj[0].Anchor.Split(',');

                foreach (var a in anchor)
                {
                    for (int i = 0; i < drpAnchor.Items.Count; i++)
                    {
                        if (drpAnchor.Items[i].Text == a.Trim())
                        {
                            drpAnchor.Items[i].Selected = true;
                            txtAnchorName.Text += drpAnchor.Items[i].Text + ",";
                            string[] keyAnchor = a.Split('|');
                            var adUser = SarsUser.GetADUser(keyAnchor[0].Trim());
                            if (adUser != null)
                            {
                                drpAnchorName.SelectedAdUserDetails = new SelectedUserDetails
                                {
                                    SID = adUser.SID,
                                    FoundUserName = string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                                    FullName = adUser.FullName
                                };
                            }
                        }
                    }
                }
                if (txtAnchorName.Text.LastIndexOf(',') > 0)
                {
                    txtAnchorName.Text = txtAnchorName.Text.Remove(txtAnchorName.Text.LastIndexOf(','));
                }
                 KeyOwnerViewState = obj[0].KeyResultOwner;
                string[] owner = obj[0].KeyResultOwner.Split(',');
                foreach (var a in owner)
                {
                    for (int i = 0; i < drpKeyResultOwner.Items.Count; i++)
                    {
                        if (drpKeyResultOwner.Items[i].Text == a.Trim())
                        {
                            drpKeyResultOwner.Items[i].Selected = true;
                            txtKeyResultOwnerName.Text += drpKeyResultOwner.Items[i].Text + ",";
                            string[] keyOwner = a.Split('|');
                            var adUser = SarsUser.GetADUser(keyOwner[0].Trim());
                            if (adUser != null)
                            {
                                drpKeyResultOwnerAprover.SelectedAdUserDetails = new SelectedUserDetails
                                {
                                    SID = adUser.SID,
                                    FoundUserName = string.Format("{0} | {1}", adUser.FullName, adUser.SID),
                                    FullName = adUser.FullName
                                };
                            }
                        }
                    }
                }
                if (txtKeyResultOwnerName.Text.LastIndexOf(',') > 0)
                {
                    txtKeyResultOwnerName.Text = txtKeyResultOwnerName.Text.Remove(txtKeyResultOwnerName.Text.LastIndexOf(','));
                }
            }
        }
    }
    public void GetImage(object img)
    {
        if (img != null)
        {
            var image = "data:image/png;base64," + Convert.ToBase64String((byte[])img);
            Image3.ImageUrl = image;
        }
    }
    private void LoadInfo()
    {
        List<WorkInfoDetails> data = IncidentTrackingManager.GetWorkInfoByIncidentID(CurrentIncidentDetails.IncidentID.ToString());
        //if (data != null && data.Any())
        //{
        //    gvWorkInfo.Bind(data);
        //}
        treeNotes.Nodes.Clear();
        var text = string.Empty;
        if (data != null)
        {
            for (int i = 0; i < data.Count; i++)
            {
                text = data[i].Timestamp.ToString() + " : " + data[i].CreatedBy;
                TreeNode parent = new TreeNode
                {
                    Text = text,
                    Value = data[i].WorkInfoId.ToString(),
                };
                TreeNode child = new TreeNode
                {
                    Text = data[i].Notes,
                    Value = data[i].WorkInfoId.ToString(),
                };
                parent.ChildNodes.Add(child);
                treeNotes.Nodes.Add(parent);
            }
        }
    }
    private string SaveIncident(int statusId)
    {

        SarsUser.SaveUser(UserSelector1.SID);


        var User = IncidentTrackingManager.GetNTQR_UserBySID(SarsUser.SID);

        DateTime? CompilerDate = !string.IsNullOrEmpty(txtCompilerDate.Text) ? Convert.ToDateTime(txtCompilerDate.Text) : DateTime.Today;
        DateTime? KeyResultOwnerDate = !string.IsNullOrEmpty(txtKeyResultDate.Text) ? Convert.ToDateTime(txtKeyResultDate.Text) : DateTime.Today;
        DateTime? AnchorDate = !string.IsNullOrEmpty(txtAnchorSignedDate.Text) ? Convert.ToDateTime(txtAnchorSignedDate.Text) : DateTime.Today;

        
        DateTime? KeyResultOwner2Date = !string.IsNullOrEmpty(txtKeyResult2Date.Text) ? Convert.ToDateTime(txtKeyResult2Date.Text) : DateTime.Today;
        DateTime? Anchor2Date = !string.IsNullOrEmpty(txtAnchor2SignedDate.Text) ? Convert.ToDateTime(txtAnchor2SignedDate.Text) : DateTime.Today;

        var report = new NTQ_Report()
        {
            Id = (Request["Id"] != null) ? int.Parse(Request["Id"]) : 0,
            fk_Quarter_ID = int.Parse(drpQuarter.SelectedValue),
            fk_ReportKeyResult_ID = int.Parse(Request["KeyResult"]),
            fk_IncidentId = decimal.Parse(Request["IncId"]),
            ActualAchievement = txtActualAchievement.Text,
            Variance = txtVariance.Text,
            fk_TargetMetID = (drpTargetMet.SelectedIndex > 0) ? int.Parse(drpTargetMet.SelectedValue) : 0,
            fk_DataValidAndCorrect_ID = (drpDataValidAndCorrect.SelectedIndex > 0) ? int.Parse(drpDataValidAndCorrect.SelectedValue) : 0,
            ReasonForVariance = txtReasonForVariance.Text,
            MitigationForUnderPerformance = txtMitigation.Text,
            CommentOnPeformance = txtCommentOnPerformance.Text,
            fk_CalculatedAccordingToTID = (drpCalculatedAccordingTID.SelectedIndex > 0) ? int.Parse(drpCalculatedAccordingTID.SelectedValue) : 0,
            IfNotCalcAccordingToTID = string.IsNullOrEmpty(txtIfNotCalculatedAccordingToTID.Text) ? "n/a" : txtIfNotCalculatedAccordingToTID.Text,
            DataSoruce = txtDataSoruce.Text,
            Evidence = txtEvidence.Text,
            CompilerName = drpCompilers.SID,
            CompilerSigned = drpCompilerSign.SelectedValue.ToString().Equals("true") ? bool.Parse(drpCompilerSign.SelectedValue) : false,
            CompilerDate = CompilerDate,
            KeyResultOwnerName = drpKeyResultOwnerAprover.SID,
            KeyResultOwnerSigned = drpKeyResultOwnerSigned.SelectedValue.ToString().Equals("true") ? bool.Parse(drpKeyResultOwnerSigned.SelectedValue) : false,
            KeyResultOwnerApproved = drpKeyResultApproved.SelectedValue.ToString().Equals("true") ? bool.Parse(drpKeyResultApproved.SelectedValue) : false,
            KeyResultOwnerDate = KeyResultOwnerDate,

            KeyResultOwner2Name = drpKeyResultOwner2Aprover.SID,
            KeyResultOwner2Signed = drpKeyResultOwner2Signed.SelectedValue.ToString().Equals("true") ? bool.Parse(drpKeyResultOwner2Signed.SelectedValue) : false,
            KeyResultOwner2Approved = drpKeyResult2Approved.SelectedValue.ToString().Equals("true") ? bool.Parse(drpKeyResult2Approved.SelectedValue) : false,
            KeyResultOwner2Date = KeyResultOwner2Date,

            AnchorName = drpAnchorName.SID,
            AnchorApproved = drpAnchorApproved.SelectedValue.ToString().Equals("true") ? bool.Parse(drpAnchorApproved.SelectedValue) : false,
            AnchorSigned = drpAnchorSigned.SelectedValue.ToString().Equals("true") ? bool.Parse(drpAnchorSigned.SelectedValue) : false,
            AnchorDate = AnchorDate,

            Anchor2Name = drpAnchor2Name.SID,
            Anchor2Approved = drpAnchor2Approved.SelectedValue.ToString().Equals("true") ? bool.Parse(drpAnchor2Approved.SelectedValue) : false,
            Anchor2Signed = drpAnchor2Signed.SelectedValue.ToString().Equals("true") ? bool.Parse(drpAnchor2Signed.SelectedValue) : false,
            Anchor2Date = Anchor2Date,

            CreatedDate = DateTime.Now,
            CreatedBy = User[0].ID,
            ModifiedBy = User[0].ID,
            ModifiedDate = DateTime.Now,
        };
        //
        if (!String.IsNullOrEmpty(Request["Id"]))
        {
            if (Convert.ToInt32(Request["Id"]) > 0)
            {
                int result = IncidentTrackingManager.NTQ_Report_UpDateOrInsert(report);
                if (result > 0)
                {
                    IncidentTrackingManager.NQT_UpdateIncidentStatus(statusId, CurrentIncidentDetails.IncidentID.ToString());
                }
                var adUser = SarsUser.GetADUser(SarsUser.SID);
                if (adUser != null)
                {
                    IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} Updated Key the result Report {1} and Financial Year {3} on {2}", adUser.SID + " | " + adUser.FullName, drpKeyResult.SelectedItem.Text, DateTime.Now, FY), report.ToXml<NTQ_Report>(), SarsUser.SID, DateTime.Now);
                }
            }
        }
        else
        {
            int result = IncidentTrackingManager.NTQ_Report_Insert(report);
            if (result > 0)
            {
                reportId = result.ToString();
                report.Id = result;
                IncidentTrackingManager.NQT_UpdateIncidentStatus(statusId, CurrentIncidentDetails.IncidentID.ToString());
            }
            var adUser = SarsUser.GetADUser(SarsUser.SID);
            if (adUser != null)
            {
                IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} Inserted Key the result Report {1} and Financial Year {3} on {2}", adUser.SID + " | " + adUser.FullName, drpKeyResult.SelectedItem.Text, DateTime.Now, FY), report.ToXml<NTQ_Report>(), SarsUser.SID, DateTime.Now);
            }
        }
        return report.ToXml<NTQ_Report>();
    }   
    private void CompilerEmailKRO(string assignedUserSid, List<NTQUserInUnits> usersToBeCCS, string keyResultName, string QuarterNo, string id, string keyresultId)
    {
        CurrentIncidentDetails = CurrentIncident;
        switch (QuarterNo)
        {
            case "1":
                QuarterNo = "First";
                break;
            case "2":
                QuarterNo = "Second";
                break;
            case "3":
                QuarterNo = "Third";
                break;
            case "4":
                QuarterNo = "Fourth";
                break;
            case "5":
                QuarterNo = "Annual";
                break;

            default:
                break;
        }  

        List<string> ccs = new List<string>();
        foreach (var u in usersToBeCCS)
        {
            var getAllStakeholders = SarsUser.GetADUser(u.UserCode.Trim());
            if (getAllStakeholders != null)
            {
                if (!ccs.Contains(getAllStakeholders.Mail))
                {
                    ccs.Add(getAllStakeholders.Mail);
                }
            }
        }
        // EBR Process Owners
        foreach (var processOwner in CurrentProcess.Owners)
        {
            var owner = SarsUser.GetADUser(processOwner.OwnerSID);
            if (owner != null)
            {
                if (!ccs.Contains(owner.Mail))
                {
                    ccs.Add(owner.Mail);
                }
            }
        }

        try
        {          
            string[] users = assignedUserSid.Split(',');
            if (users != null)
            {
                foreach (var user in users)
                {
                    string[] sid = user.Split('|');
                    if (sid.Length >= 1)
                    {
                        var assignedUser = SarsUser.GetADUser(sid[0].Trim());
                        if (assignedUser != null)
                        {
                            if (ccs.Contains(assignedUser.Mail))
                            {
                                ccs.Remove(assignedUser.Mail);
                            }
                            var incidentURL = string.Format("http://{0}/ims/NTQR/RegisterUserIncident.aspx?{1}",
                            Request.ServerVariables["HTTP_HOST"],
                            String.Format("procId={0}&incId={1}&Id={2}&keyResult={3}", ProcessID, IncidentID, id, keyresultId));

                            var process = CurrentProcess;
                            var creator = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID.Trim());
                            if (creator != null)
                            {
                                var subject = string.Format("{0}", "National Treasury Quarterly Report: Status changed: Assigned to Key Result Owner for approval. ");

                                if (Request.PhysicalApplicationPath != null)
                                {
                                    var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "nqt-report-compiler-assigned-to-kro.htm");
                                    var tempate = File.ReadAllText(templateDir);

                                    var body = string.Format(tempate,
                                                                             assignedUser.FullName,
                                                                             CurrentIncidentDetails.ReferenceNumber,
                                                                             CurrentIncidentDetails.IncidentStatus,
                                                                             string.Format("{0} | {1}",creator.SID, creator.FullName),
                                                                             incidentURL,
                                                                             keyResultName,
                                                                             QuarterNo,
                                                                             txtKeyResultOwnerName.Text.Replace(",", "<br>"),
                                                                             txtAnchorName.Text.Replace(",", "<br>")
                                                                             );



                                    using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                                    {
                                        var sendToUser = SarsUser.GetADUser(CurrentIncident.CreatedBySID);


                                        var oMessage = new Sars.Systems.Mail.SmtpMessage
                                        {
                                            From = "IncidentTracking@sars.gov.za",
                                            Body = body,
                                            IsBodyHtml = true,
                                            Subject = subject,
                                            To = new[] { assignedUser.Mail },
                                            CC = ccs.ToArray(),

                                        };
                                        client.Send2(oMessage);
                                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, sendToUser.SID, sendToUser.Mail,
                                                                                       this.IncidentID, this.ProcessID);
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private void KroEmailSecondKRO(string assignedUserSid, List<NTQUserInUnits> usersToBeCCS, string keyResultName, string QuarterNo, string id, string keyresultId)
    {
        CurrentIncidentDetails = CurrentIncident;
        switch (QuarterNo)
        {
            case "1":
                QuarterNo = "First";
                break;
            case "2":
                QuarterNo = "Second";
                break;
            case "3":
                QuarterNo = "Third";
                break;
            case "4":
                QuarterNo = "Fourth";
                break;
            case "5":
                QuarterNo = "Annual";
                break;

            default:
                break;
        }

        List<string> ccs = new List<string>();
        foreach (var u in usersToBeCCS)
        {
            var getAllStakeholders = SarsUser.GetADUser(u.UserCode.Trim());
            if (getAllStakeholders != null)
            {
                if (!ccs.Contains(getAllStakeholders.Mail))
                {
                    ccs.Add(getAllStakeholders.Mail);
                }
            }
        }
        // EBR Process Owners
        foreach (var processOwner in CurrentProcess.Owners)
        {
            var owner = SarsUser.GetADUser(processOwner.OwnerSID);
            if (owner != null)
            {
                if (!ccs.Contains(owner.Mail))
                {
                    ccs.Add(owner.Mail);
                }
            }
        }


        try
        {
            string[] users = assignedUserSid.Split(',');
            if (users != null)
            {
                foreach (var user in users)
                {
                    
                    string[] sid = user.Split('|');
                    if (sid.Length >= 1)
                    {
                        var assignedUser = SarsUser.GetADUser(sid[0].Trim());
                        if (!assignedUser.SID.Equals(SarsUser.SID.ToLower()))
                        {
                            if (ccs.Contains(assignedUser.Mail))
                            {
                                ccs.Remove(assignedUser.Mail);
                            }
                            if (assignedUser != null)
                            {

                                var incidentURL = string.Format("http://{0}/ims/NTQR/RegisterUserIncident.aspx?{1}",
                                Request.ServerVariables["HTTP_HOST"],
                                String.Format("procId={0}&incId={1}&Id={2}&keyResult={3}", ProcessID, IncidentID, id, keyresultId));

                                var process = CurrentProcess;
                                var creator = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID.Trim());
                                if (creator != null)
                                {
                                    var subject = string.Format("{0}", "National Treasury Quarterly Report: Status changed: Assigned to Key Result Owner for approval. ");

                                    if (Request.PhysicalApplicationPath != null)
                                    {
                                        var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "nqt-report-compiler-assigned-to-kro.htm");
                                        var tempate = File.ReadAllText(templateDir);

                                        var body = string.Format(tempate,
                                                                                 assignedUser.FullName,
                                                                                 CurrentIncidentDetails.ReferenceNumber,
                                                                                 CurrentIncidentDetails.IncidentStatus,                                                                                 
                                                                                 string.Format("{0} | {1}", creator.SID, creator.FullName),
                                                                                 incidentURL,
                                                                                 keyResultName,
                                                                                 QuarterNo,
                                                                                 txtKeyResultOwnerName.Text.Replace(",", "<br>"),
                                                                                 txtAnchorName.Text.Replace(",", "<br>")
                                                                                 );



                                        using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                                        {
                                            var sendToUser = SarsUser.GetADUser(CurrentIncident.CreatedBySID);


                                            var oMessage = new Sars.Systems.Mail.SmtpMessage
                                            {
                                                From = "IncidentTracking@sars.gov.za",
                                                Body = body,
                                                IsBodyHtml = true,
                                                Subject = subject,
                                                To = new[] { assignedUser.Mail },
                                                CC = ccs.ToArray(),

                                            };
                                            client.Send2(oMessage);
                                            IncidentTrackingManager.SaveIncidentEmailLog(body, subject, sendToUser.SID, sendToUser.Mail,
                                                                                           this.IncidentID, this.ProcessID);
                                        }
                                    }

                                }
                            }
                        }

                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private void KroEmailAnchor(string assignedUserSid, List<NTQUserInUnits> usersToBeCCS, string keyResultName, string QuarterNo, string id, string keyresultId)
    {
        CurrentIncidentDetails = CurrentIncident;
        switch (QuarterNo)
        {
            case "1":
                QuarterNo = "First";
                break;
            case "2":
                QuarterNo = "Second";
                break;
            case "3":
                QuarterNo = "Third";
                break;
            case "4":
                QuarterNo = "Fourth";
                break;
            case "5":
                QuarterNo = "Annual";
                break;

            default:
                break;
        }

        List<string> ccs = new List<string>();
        foreach (var u in usersToBeCCS)
        {
            var getAllStakeholders = SarsUser.GetADUser(u.UserCode.Trim());
            if (getAllStakeholders != null)
            {
                if (!ccs.Contains(getAllStakeholders.Mail))
                {
                    ccs.Add(getAllStakeholders.Mail);
                }
            }
        }
       

        try
        {
            string[] users = assignedUserSid.Split(',');
            if (users != null)
            {
                foreach (var user in users)
                {
                    // EBR Process Owners
                    foreach (var processOwner in CurrentProcess.Owners)
                    {
                        var owner = SarsUser.GetADUser(processOwner.OwnerSID);
                        if (owner != null)
                        {
                            if (!ccs.Contains(owner.Mail))
                            {
                                ccs.Add(owner.Mail);
                            }
                        }
                    }
                    string[] sid = user.Split('|');
                    if (sid.Length >= 1)
                    {
                        var assignedUser = SarsUser.GetADUser(sid[0].Trim());
                        if (assignedUser != null)
                        {
                            if (ccs.Contains(assignedUser.Mail))
                            {
                                ccs.Remove(assignedUser.Mail);
                            }
                            var incidentURL = string.Format("http://{0}/ims/NTQR/RegisterUserIncident.aspx?{1}",
                            Request.ServerVariables["HTTP_HOST"],
                            String.Format("procId={0}&incId={1}&Id={2}&keyResult={3}", ProcessID, IncidentID, id, keyresultId));

                            var process = CurrentProcess;
                            var creator = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID.Trim());
                            if (creator != null)
                            {
                                var subject = string.Format("{0}", "National Treasury Quarterly Report: Status changed: Assigned to Anchor. ");

                                if (Request.PhysicalApplicationPath != null)
                                {
                                    var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "nqt-report-kro-assigned-to-anchor.htm");
                                    var tempate = File.ReadAllText(templateDir);

                                    var body = string.Format(tempate,
                                                                             assignedUser.FullName,
                                                                             CurrentIncidentDetails.ReferenceNumber,
                                                                             CurrentIncidentDetails.IncidentStatus,
                                                                             string.Format("{0} | {1}", creator.SID, creator.FullName),
                                                                             incidentURL,
                                                                             keyResultName,
                                                                             QuarterNo,
                                                                             txtKeyResultOwnerName.Text.Replace(",", "<br>"),
                                                                             txtAnchorName.Text.Replace(",", "<br>")
                                                                             );



                                    using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                                    {
                                        var sendToUser = SarsUser.GetADUser(CurrentIncident.CreatedBySID);


                                        var oMessage = new Sars.Systems.Mail.SmtpMessage
                                        {
                                            From = "IncidentTracking@sars.gov.za",
                                            Body = body,
                                            IsBodyHtml = true,
                                            Subject = subject,
                                            To = new[] { assignedUser.Mail },
                                            CC = ccs.ToArray(),

                                        };
                                        client.Send2(oMessage);
                                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, sendToUser.SID, sendToUser.Mail,
                                                                                       this.IncidentID, this.ProcessID);
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private void AnchorEmailSecondAnchor(string assignedUserSid, List<NTQUserInUnits> usersToBeCCS, string keyResultName, string QuarterNo, string id, string keyresultId)
    {
        CurrentIncidentDetails = CurrentIncident;
        switch (QuarterNo)
        {
            case "1":
                QuarterNo = "First";
                break;
            case "2":
                QuarterNo = "Second";
                break;
            case "3":
                QuarterNo = "Third";
                break;
            case "4":
                QuarterNo = "Fourth";
                break;
            case "5":
                QuarterNo = "Annual";
                break;

            default:
                break;
        }

        List<string> ccs = new List<string>();
        foreach (var u in usersToBeCCS)
        {
            var getAllStakeholders = SarsUser.GetADUser(u.UserCode.Trim());
            if (getAllStakeholders != null)
            {
                if (!ccs.Contains(getAllStakeholders.Mail))
                {
                    ccs.Add(getAllStakeholders.Mail);
                }
            }
        }
        // EBR Process Owners
        foreach (var processOwner in CurrentProcess.Owners)
        {
            var owner = SarsUser.GetADUser(processOwner.OwnerSID);
            if (owner != null)
            {
                if (!ccs.Contains(owner.Mail))
                {
                    ccs.Add(owner.Mail);
                }
            }
        }


        try
        {
            string firstAnchorName = string.Empty;
            string[] users = assignedUserSid.Split(',');
            if (users != null)
            {
                foreach (var user in users)
                {

                    string[] sid = user.Split('|');
                    if (sid.Length >= 1)
                    {
                        var assignedUser = SarsUser.GetADUser(sid[0].Trim());
                        
                        if (assignedUser.SID.ToLower().Equals(SarsUser.SID.ToLower()))
                        {
                            firstAnchorName = assignedUser.FullName;
                        }
                            if (!assignedUser.SID.Equals(SarsUser.SID.ToLower()))
                        {
                            if (ccs.Contains(assignedUser.Mail))
                            {
                                ccs.Remove(assignedUser.Mail);
                            }
                            if (assignedUser != null)
                            {

                                var incidentURL = string.Format("http://{0}/ims/NTQR/RegisterUserIncident.aspx?{1}",
                                Request.ServerVariables["HTTP_HOST"],
                                String.Format("procId={0}&incId={1}&Id={2}&keyResult={3}", ProcessID, IncidentID, id, keyresultId));

                                var process = CurrentProcess;
                                var creator = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID.Trim());
                                if (creator != null)
                                {
                                    var subject = string.Format("{0}{1}", "National Treasury Quarterly Report: Approved by Anchor: ",keyResultName);

                                    if (Request.PhysicalApplicationPath != null)
                                    {
                                        var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "nqt-report-anchor-assigned-to-achor.htm");
                                        var tempate = File.ReadAllText(templateDir);

                                        var body = string.Format(tempate,
                                                                                 assignedUser.FullName,
                                                                                 CurrentIncidentDetails.ReferenceNumber,
                                                                                 "Second Anchor’s approval required",
                                                                                 string.Format("{0} | {1}", creator.SID, creator.FullName),
                                                                                 incidentURL,
                                                                                 keyResultName,
                                                                                 QuarterNo,
                                                                                 txtKeyResultOwnerName.Text.Replace(",", "<br>"),
                                                                                 txtAnchorName.Text.Replace(",", "<br>"),
                                                                                 firstAnchorName
                                                                                 );



                                        using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                                        {
                                            var sendToUser = SarsUser.GetADUser(CurrentIncident.CreatedBySID);

                                            var oMessage = new Sars.Systems.Mail.SmtpMessage
                                            {
                                                From = "IncidentTracking@sars.gov.za",
                                                Body = body,
                                                IsBodyHtml = true,
                                                Subject = subject,
                                                To = new[] { assignedUser.Mail },
                                                CC = ccs.ToArray(),
                                            };
                                            client.Send2(oMessage);
                                            IncidentTrackingManager.SaveIncidentEmailLog(body, subject, assignedUser.SID, sendToUser.Mail,
                                                                                           this.IncidentID, this.ProcessID);
                                        }
                                    }

                                }
                            }
                        }

                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private void AnchorApprovedEMail(string assignedUserSid, List<NTQUserInUnits> usersToBeCCS, string keyResultName, string QuarterNo, string id, string keyresultId)
    {
        CurrentIncidentDetails = CurrentIncident;
        switch (QuarterNo)
        {
            case "1":
                QuarterNo = "First";
                break;
            case "2":
                QuarterNo = "Second";
                break;
            case "3":
                QuarterNo = "Third";
                break;
            case "4":
                QuarterNo = "Fourth";
                break;
            case "5":
                QuarterNo = "Annual";
                break;

            default:
                break;
        }

        List<string> ccs = new List<string>();
        foreach (var u in usersToBeCCS)
        {
            var getAllStakeholders = SarsUser.GetADUser(u.UserCode.Trim());
            if (getAllStakeholders != null)
            {
                if (!ccs.Contains(getAllStakeholders.Mail))
                {
                    ccs.Add(getAllStakeholders.Mail);
                }
            }
        }


        try
        {
            string[] users = assignedUserSid.Split(',');
            if (users != null)
            {
                foreach (var user in users)
                {
                    // EBR Process Owners
                    foreach (var processOwner in CurrentProcess.Owners)
                    {
                        var owner = SarsUser.GetADUser(processOwner.OwnerSID);
                        if (owner != null)
                        {
                            if (!ccs.Contains(owner.Mail))
                            {
                                ccs.Add(owner.Mail);
                            }
                        }
                    }
                    string[] sid = user.Split('|');
                    if (sid.Length >= 1)
                    {
                        var assignedUser = SarsUser.GetADUser(sid[0].Trim());
                        if (assignedUser != null)
                        {
                            if (ccs.Contains(assignedUser.Mail))
                            {
                                ccs.Remove(assignedUser.Mail);
                            }
                            var incidentURL = string.Format("http://{0}/ims/NTQR/RegisterUserIncident.aspx?{1}",
                            Request.ServerVariables["HTTP_HOST"],
                            String.Format("procId={0}&incId={1}&Id={2}&keyResult={3}", ProcessID, IncidentID, id, keyresultId));

                            var process = CurrentProcess;
                            var creator = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID.Trim());
                            if (creator != null)
                            {
                                var subject = string.Format("{0}{1}", "National Treasury Quarterly Report: Approved by Anchor: ",keyResultName);

                                if (Request.PhysicalApplicationPath != null)
                                {
                                    var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "nqt-completed-report.htm");
                                    var tempate = File.ReadAllText(templateDir);

                                    var body = string.Format(tempate,
                                                                             assignedUser.FullName,
                                                                             CurrentIncidentDetails.ReferenceNumber,
                                                                             CurrentIncidentDetails.IncidentStatus,
                                                                             string.Format("{0} | {1}", creator.SID, creator.FullName),
                                                                             incidentURL,
                                                                             keyResultName,
                                                                             QuarterNo,
                                                                             txtKeyResultOwnerName.Text.Replace(",", "<br>"),
                                                                             txtAnchorName.Text.Replace(",", "<br>")
                                                                             );



                                    using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                                    {
                                        var sendToUser = SarsUser.GetADUser(CurrentIncident.CreatedBySID);


                                        var oMessage = new Sars.Systems.Mail.SmtpMessage
                                        {
                                            From = "IncidentTracking@sars.gov.za",
                                            Body = body,
                                            IsBodyHtml = true,
                                            Subject = subject,
                                            To = new[] { assignedUser.Mail },
                                            CC = ccs.ToArray(),

                                        };
                                        client.Send2(oMessage);
                                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, sendToUser.SID, sendToUser.Mail,
                                                                                       this.IncidentID, this.ProcessID);
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private void AnchorsApprovedEMail(string assignedUserSid, List<NTQUserInUnits> usersToBeCCS, string keyResultName, string QuarterNo, string id, string keyresultId)
    {
        CurrentIncidentDetails = CurrentIncident;
        switch (QuarterNo)
        {
            case "1":
                QuarterNo = "First";
                break;
            case "2":
                QuarterNo = "Second";
                break;
            case "3":
                QuarterNo = "Third";
                break;
            case "4":
                QuarterNo = "Fourth";
                break;
            case "5":
                QuarterNo = "Annual";
                break;

            default:
                break;
        }

        List<string> ccs = new List<string>();
        foreach (var u in usersToBeCCS)
        {
            var getAllStakeholders = SarsUser.GetADUser(u.UserCode.Trim());
            if (getAllStakeholders != null)
            {
                if (!ccs.Contains(getAllStakeholders.Mail))
                {
                    ccs.Add(getAllStakeholders.Mail);
                }
            }
        }


        try
        {
            string[] users = assignedUserSid.Split(',');
            if (users != null)
            {
                foreach (var user in users)
                {
                    // EBR Process Owners
                    foreach (var processOwner in CurrentProcess.Owners)
                    {
                        var owner = SarsUser.GetADUser(processOwner.OwnerSID);
                        if (owner != null)
                        {
                            if (!ccs.Contains(owner.Mail))
                            {
                                ccs.Add(owner.Mail);
                            }
                        }
                    }
                    string[] sid = user.Split('|');
                    if (sid.Length >= 1)
                    {
                        var assignedUser = SarsUser.GetADUser(sid[0].Trim());
                        if (assignedUser != null)
                        {
                            if (ccs.Contains(assignedUser.Mail))
                            {
                                ccs.Remove(assignedUser.Mail);
                            }
                            var incidentURL = string.Format("http://{0}/ims/NTQR/RegisterUserIncident.aspx?{1}",
                            Request.ServerVariables["HTTP_HOST"],
                            String.Format("procId={0}&incId={1}&Id={2}&keyResult={3}", ProcessID, IncidentID, id, keyresultId));

                            var process = CurrentProcess;
                            var creator = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID.Trim());
                            if (creator != null)
                            {
                                var subject = string.Format("{0}{1}", "National Treasury Quarterly Report: Approved by Anchors: ", keyResultName);

                                if (Request.PhysicalApplicationPath != null)
                                {
                                    var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "nqt-completed-multi-anchor-report.htm");
                                    var tempate = File.ReadAllText(templateDir);

                                    var body = string.Format(tempate,
                                                                             assignedUser.FullName,
                                                                             CurrentIncidentDetails.ReferenceNumber,
                                                                             CurrentIncidentDetails.IncidentStatus,
                                                                            string.Format("{0} | {1}", creator.SID, creator.FullName),
                                                                             incidentURL,
                                                                             keyResultName,
                                                                             QuarterNo,
                                                                             txtKeyResultOwnerName.Text.Replace(",", "<br>"),
                                                                             txtAnchorName.Text.Replace(",", "<br>")
                                                                             );



                                    using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                                    {
                                        var sendToUser = SarsUser.GetADUser(CurrentIncident.CreatedBySID);


                                        var oMessage = new Sars.Systems.Mail.SmtpMessage
                                        {
                                            From = "IncidentTracking@sars.gov.za",
                                            Body = body,
                                            IsBodyHtml = true,
                                            Subject = subject,
                                            To = new[] { assignedUser.Mail },
                                            CC = ccs.ToArray(),

                                        };
                                        client.Send2(oMessage);
                                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, sendToUser.SID, sendToUser.Mail,
                                                                                       this.IncidentID, this.ProcessID);
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private void EmailReworkReport(string compilerSID, List<NTQUserInUnits> usersToBeCCS, string keyResultName, string QuarterNo, string id, string keyresultId)
    {
        CurrentIncidentDetails = CurrentIncident;
        switch (QuarterNo)
        {
            case "1":
                QuarterNo = "First";
                break;
            case "2":
                QuarterNo = "Second";
                break;
            case "3":
                QuarterNo = "Third";
                break;
            case "4":
                QuarterNo = "Fourth";
                break;
            case "5":
                QuarterNo = "Annual";
                break;

            default:
                break;
        }
        var compiler = SarsUser.GetADUser(compilerSID);
        string compilerFullName = string.Empty;
        if (compiler != null)
        {
            compilerFullName = string.Format("{0} | {1}", compiler.SID, compiler.FullName);
        }

        List<string> ccs = new List<string>();

        foreach (var u in usersToBeCCS)
        {
            var getAllStakeholders = SarsUser.GetADUser(u.UserCode.Trim());
            if (getAllStakeholders != null)
            {
                if (!ccs.Contains(getAllStakeholders.Mail))
                {
                    ccs.Add(getAllStakeholders.Mail);
                }
            }
        }

        try
        {
            // EBR Owners
            foreach (var processOwner in CurrentProcess.Owners)
            {
                var owner = SarsUser.GetADUser(processOwner.OwnerSID);
                if (owner != null)
                {
                    if (!ccs.Contains(owner.Mail))
                    {
                        ccs.Add(owner.Mail);
                    }
                }
            }
            if (compiler != null)
            {
                if (ccs.Contains(compiler.Mail))
                {
                    ccs.Remove(compiler.Mail);
                }
                var incidentURL = string.Format("http://{0}/ims/NTQR/RegisterUserIncident.aspx?{1}",
                            Request.ServerVariables["HTTP_HOST"],
                            String.Format("procId={0}&incId={1}&Id={2}&keyResult={3}", ProcessID, IncidentID, id, keyresultId));

                var process = CurrentProcess;
                var userAssigned = compiler;
                var creator = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID.Trim());

                var subject = string.Format("{0}", "National Treasury Quarterly Report: Status changed: Assigned to Compiler for re-work. ");

                if (Request.PhysicalApplicationPath != null)
                {
                    var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-assigned -ntq-report - rework.htm");
                    var tempate = File.ReadAllText(templateDir);

                    var body = string.Format(tempate,
                                                userAssigned.FullName,
                                                CurrentIncidentDetails.ReferenceNumber,
                                                CurrentIncidentDetails.IncidentStatus,
                                                compilerFullName,
                                                incidentURL,
                                                keyResultName,
                                                QuarterNo,
                                                txtKeyResultOwnerName.Text.Replace(",", "<br>"),
                                                txtAnchorName.Text.Replace(",", "<br>"),
                                                txtSummary.Text
                                                );



                    using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                    {
                        var sendToUser = SarsUser.GetADUser(CurrentIncident.CreatedBySID);


                        var oMessage = new Sars.Systems.Mail.SmtpMessage
                        {
                            From = "IncidentTracking@sars.gov.za",
                            Body = body,
                            IsBodyHtml = true,
                            Subject = subject,
                            To = new[] { userAssigned.Mail },
                            CC = ccs.ToArray(),

                        };
                        client.Send2(oMessage);
                        IncidentTrackingManager.SaveIncidentEmailLog(body, subject, sendToUser.SID, sendToUser.Mail,
                                                                       this.IncidentID, this.ProcessID);
                    }
                }

            }
        }
        catch (Exception)
        {

        }
    }
    private void EmailUsersWithCommentAdded(string sids, string keyResult, string QuarterNo, string id, string keyresult, string compilerEmailAddress,string comment)
    {
        CurrentIncidentDetails = CurrentIncident;
        switch (QuarterNo)
        {
            case "1":
                QuarterNo = "First";
                break;
            case "2":
                QuarterNo = "Second";
                break;
            case "3":
                QuarterNo = "Third";
                break;
            case "4":
                QuarterNo = "Fourth";
                break;
            case "5":
                QuarterNo = "Annual";
                break;

            default:
                break;
        }
        List<string> ccs = new List<string>();
        ccs.Add(compilerEmailAddress);
        foreach (var processOwner in CurrentProcess.Owners)
        {
            var owner = SarsUser.GetADUser(processOwner.OwnerSID);
            ccs.Add(owner.Mail);
        }
        var compiler = SarsUser.GetADUser(drpCompilers.SID);
        string complileFullName = string.Empty;
        if (compiler != null)
        {
            complileFullName = string.Format("{0} | {1}", compiler.SID, compiler.FullName);
        }
        try
        {
            string[] users = sids.Split(',');
            if (users != null)
            {
                foreach (var user in users)
                {
                    string[] sid = user.Split('|');
                    if (sid.Length >= 1)
                    {
                        var getUserInfor = SarsUser.GetADUser(sid[0].Trim());
                        if (getUserInfor != null)
                        {


                            var incidentURL = string.Format("http://{0}/ims/NTQR/RegisterUserIncident.aspx?{1}",
                                        Request.ServerVariables["HTTP_HOST"],
                                        String.Format("procId={0}&incId={1}&Id={2}&keyResult={3}", ProcessID, IncidentID, id, keyresult));

                            var process = CurrentProcess;
                            var userAssigned = SarsUser.GetADUser(getUserInfor.SID.Trim());
                            var creator = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID.Trim());

                            var subject = string.Format("{0} Ref : {1}", "National Treasury Quarterly Report", CurrentIncidentDetails.ReferenceNumber);

                            if (Request.PhysicalApplicationPath != null)
                            {
                                var templateDir = Path.Combine(Request.PhysicalApplicationPath, "emails", "incident-assigned -ntq-report - notes.htm");
                                var tempate = File.ReadAllText(templateDir);

                                var body = string.Format(tempate,
                                                            userAssigned.FullName,
                                                            CurrentIncidentDetails.ReferenceNumber,
                                                            CurrentIncidentDetails.IncidentStatus,
                                                            complileFullName,
                                                            incidentURL,
                                                            keyResult,
                                                            QuarterNo,
                                                            comment,
                                                            txtKeyResultOwnerName.Text,
                                                            txtAnchorName.Text
                                                            );



                                using (var client = new Sars.Systems.Mail.SmtpServiceClient("basicHttpEndPoint"))
                                {
                                    var sendToUser = SarsUser.GetADUser(CurrentIncident.CreatedBySID);


                                    var oMessage = new Sars.Systems.Mail.SmtpMessage
                                    {
                                        From = "IncidentTracking@sars.gov.za",
                                        Body = body,
                                        IsBodyHtml = true,
                                        Subject = subject,
                                        To = new[] { userAssigned.Mail },
                                        CC = ccs.ToArray(),

                                    };
                                    client.Send2(oMessage);
                                    IncidentTrackingManager.SaveIncidentEmailLog(body, subject, sendToUser.SID, sendToUser.Mail,
                                                                                   this.IncidentID, this.ProcessID);

                                }
                            }

                        }
                    }
                }
            }
        }
        catch (Exception)
        {

        }

    }
   
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {

        if (drpCalculatedAccordingTID.SelectedValue.Equals("1"))
        {
            if (string.IsNullOrEmpty(txtIfNotCalculatedAccordingToTID.Text))
            {
                MessageBox.Show("Comment On IfNotCalculatedAccordingToTID is a required field");
                return;
            }
        }
        if (string.IsNullOrEmpty(txtIncidentDueDate.Text))
        {

            MessageBox.Show("Due Date is required.");
            return;
        }
        DateTime testDate;
        if (!DateTime.TryParse(txtIncidentDueDate.Text, out testDate))
        {
            MessageBox.Show("Due Date formart is invalid, mut be (yyyy-mm-dd hh:ss).");
            return;
        }       
        string report = string.Empty;

        switch (e.CommandName)
        {

            case "SaveAndClose":
                {
                    if (drpQuarter.SelectedIndex == 0)
                    {
                        MessageBox.Show("The Quarter is a required field");
                        return;
                    }
                    report = SaveIncident(9);
                    if (!string.IsNullOrEmpty(txtNotes.Text.Trim()))
                    {
                        var workInfo = new WorkInfoDetails
                        {
                            AddedBySID = SarsUser.SID,
                            IncidentId = Convert.ToDecimal(IncidentID),
                            ProcessId = Convert.ToInt32(ProcessID),
                            Notes = txtNotes.Text
                        };
                        IncidentTrackingManager.AddWorkInfo(workInfo);
                    }
                    var adUser1 = SarsUser.GetADUser(SarsUser.SID);
                    if (adUser1 != null)
                    {
                        IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} Saved and Closed The Key result Report {1} and Financial Year {3} on {2}", adUser1.SID + " | " + adUser1.FullName, drpKeyResult.SelectedItem.Text, DateTime.Now, FY), report, SarsUser.SID, DateTime.Now);
                    }

                    var incidentURL = string.Format("http://{0}/ims/NTQR/NormalUserLandingPage.aspx?{1}", Request.ServerVariables["HTTP_HOST"], String.Format("procId={0}", ProcessID));
                    MessageBox.ShowAndRedirect("The record has been saved successfully.", incidentURL);


                    break;
                }
            case "SendToKeyResultOwner":
                {
                    if (drpCompilerSign.SelectedValue.ToLower().Equals("false") || string.IsNullOrEmpty(txtCompilerDate.Text))
                    {
                        MessageBox.Show("Please sign first");
                        drpCompilerSign.Focus();
                        drpCompilerSign.Attributes.Add("class", "required");
                        return;
                    }
                    if (pnlKeyResultApproval.Enabled)
                    {
                        if (drpKeyResultApproved.SelectedValue.ToLower().Equals("false") || drpKeyResultOwnerSigned.SelectedValue.ToLower().Equals("false"))
                        {
                            MessageBox.Show("Please approve and sign first");
                            drpKeyResultApproved.Focus();
                            drpKeyResultApproved.Attributes.Add("class", "required");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtKeyResultDate.Text))
                        {
                            MessageBox.Show("The KRO sign date is required");
                            txtKeyResultDate.Focus();
                            txtKeyResultDate.Attributes.Add("class", "required");
                            return;
                        }
                    }
                    else if (pnlKeyResultApproval2.Enabled)
                    {
                        if (drpKeyResult2Approved.SelectedValue.ToLower().Equals("false") || drpKeyResultOwner2Signed.SelectedValue.ToLower().Equals("false"))
                        {
                            MessageBox.Show("Please approve and sign first");
                            drpKeyResult2Approved.Focus();
                            drpKeyResult2Approved.Attributes.Add("class", "required");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtKeyResult2Date.Text))
                        {
                            MessageBox.Show("The KRO sign date is required");
                            txtKeyResult2Date.Focus();
                            txtKeyResult2Date.Attributes.Add("class", "required");
                            return;
                        }
                    }
                    report = SaveIncident(10);
                    if (!string.IsNullOrEmpty(txtNotes.Text.Trim()))
                    {
                        var workInfo = new WorkInfoDetails
                        {
                            AddedBySID = SarsUser.SID,
                            IncidentId = Convert.ToDecimal(IncidentID),
                            ProcessId = Convert.ToInt32(ProcessID),
                            Notes = txtNotes.Text
                        };
                        IncidentTrackingManager.AddWorkInfo(workInfo);
                    }
                    var obj = IncidentTrackingManager.NTQ_Report_KeyResults_SelectByID(int.Parse(Request["keyResult"]));
                    if (obj != null)
                    {
                        var userInUnits = IncidentTrackingManager.NTQ_Report_GetUserUnitsBySid(SarsUser.SID, obj[0].fk_Lookup_KeyResult_ID);
                        if (userInUnits != null)
                        {
                            var usersInUnit = IncidentTrackingManager.NTQ_Report_GetUsersByUnit(userInUnits[0].unitId, SarsUser.SID);
                            if (usersInUnit != null)
                            {
                                var rptId = !string.IsNullOrEmpty(Request["Id"]) ? Request["Id"] : reportId;
                                switch (int.Parse(StatusId))
                                {
                                    case 1:
                                    case 9:
                                        
                                        CompilerEmailKRO(obj[0].KeyResultOwner, usersInUnit, drpKeyResult.SelectedItem.Text.Replace("\n", "").Replace("\r", ""), drpQuarter.SelectedValue, rptId, Request["keyResult"]);
                                        break;
                                    case 10:
                                        
                                        KroEmailSecondKRO(obj[0].KeyResultOwner, usersInUnit, drpKeyResult.SelectedItem.Text.Replace("\n", "").Replace("\r", ""), drpQuarter.SelectedValue, rptId, Request["keyResult"]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    var adUser2 = SarsUser.GetADUser(SarsUser.SID);
                    if (adUser2 != null)
                    {
                        IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} Approved and Sent the Key result Report {1} and Financial Year {3} on {2} to the KRO for Approval.", adUser2.SID + " | " + adUser2.FullName, drpKeyResult.SelectedItem.Text, DateTime.Now, FY), report, SarsUser.SID, DateTime.Now);
                    }
                    var incidentURL = string.Format("http://{0}/ims/NTQR/NormalUserLandingPage.aspx?{1}", Request.ServerVariables["HTTP_HOST"], String.Format("procId={0}", ProcessID));
                    MessageBox.ShowAndRedirect("The Report has been sent to Key Result Owners.", incidentURL);

                    break;
                }
            case "SendToAnchor":
                {
                    string[] kerResuts = txtKeyResultOwnerName.Text.Split(',');
                    if (kerResuts.Count() >= 2)
                    {
                        if (drpKeyResultOwnerSigned.SelectedValue.Equals("false") || drpKeyResultOwner2Signed.SelectedValue.Equals("false"))
                        {
                            MessageBox.Show("All KRO(s) must sign the report first");
                            return;
                        }
                        
                    }
                    else
                    {
                        if (drpKeyResultApproved.SelectedValue.ToLower().Equals("false") || drpKeyResultOwnerSigned.SelectedValue.ToLower().Equals("false"))
                        {
                            MessageBox.Show("Please approve and sign first");
                            drpKeyResultApproved.Focus();
                            drpKeyResultApproved.Attributes.Add("class", "required");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtKeyResultDate.Text))
                        {
                            MessageBox.Show("The KRO sign date is required");
                            txtKeyResultDate.Focus();
                            txtKeyResultDate.Attributes.Add("class", "required");
                            return;
                        }
                    }

                    if (StatusId == "11")
                    {
                        if (pnlAnchor.Enabled)
                        {
                            if (drpAnchorSigned.SelectedValue.Equals("false") || drpAnchorApproved.SelectedValue.Equals("false"))
                            {
                                MessageBox.Show("All Anchor(s) must sign the report first");
                                return;
                            }
                        }
                        if (pnlAnchor2.Enabled)
                        {
                            if (drpAnchor2Signed.SelectedValue.Equals("false") || drpAnchor2Approved.SelectedValue.Equals("false"))
                            {
                                MessageBox.Show("All Anchor(s) must sign the report first");
                                return;
                            }
                        }                        
                    }
                    report = SaveIncident(11);
                    if (!string.IsNullOrEmpty(txtNotes.Text.Trim()))
                    {
                        var workInfo = new WorkInfoDetails
                        {
                            AddedBySID = SarsUser.SID,
                            IncidentId = Convert.ToDecimal(IncidentID),
                            ProcessId = Convert.ToInt32(ProcessID),
                            Notes = txtNotes.Text
                        };
                        IncidentTrackingManager.AddWorkInfo(workInfo);
                    }                  

                    var obj = IncidentTrackingManager.NTQ_Report_KeyResults_SelectByID(int.Parse(Request["keyResult"]));
                    if (obj != null)
                    {
                        var userInUnits = IncidentTrackingManager.NTQ_Report_GetUserUnitsBySid(SarsUser.SID, obj[0].fk_Lookup_KeyResult_ID);
                        if (userInUnits != null)
                        {
                            var usersInUnit = IncidentTrackingManager.NTQ_Report_GetUsersByUnit(userInUnits[0].unitId, SarsUser.SID);
                            if (usersInUnit != null)
                            {
                                var rptId = !string.IsNullOrEmpty(Request["Id"]) ? Request["Id"] : reportId;
                                switch (int.Parse(StatusId))
                                {
                                    case 10:                                        
                                        KroEmailAnchor(obj[0].Anchor, usersInUnit, drpKeyResult.SelectedItem.Text.Replace("\n", "").Replace("\r", ""), drpQuarter.SelectedValue, rptId, Request["keyResult"]);
                                        break;
                                    case 11:
                                        var secondAnchor = obj[0].Anchor.Split(',');
                                        var anchor = string.Empty;
                                        foreach (var thisAnchor in secondAnchor)
                                        {
                                            if (!thisAnchor.ToLower().Contains(SarsUser.SID.ToLower())){
                                                anchor = thisAnchor;
                                            }
                                        }

                                        AnchorEmailSecondAnchor(anchor, usersInUnit, drpKeyResult.SelectedItem.Text.Replace("\n", "").Replace("\r", ""), drpQuarter.SelectedValue, rptId, Request["keyResult"]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    var adUser3 = SarsUser.GetADUser(SarsUser.SID);
                    if (adUser3 != null)
                    {
                        IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} Approved and Sent the Key result Report {1} and Financial Year {3} on {2} to the Anchor for Approval.", adUser3.SID + " | " + adUser3.FullName, drpKeyResult.SelectedItem.Text, DateTime.Now, FY), report, SarsUser.SID, DateTime.Now);
                    }
                    var incidentURL = string.Format("http://{0}/ims/NTQR/NormalUserLandingPage.aspx?{1}", Request.ServerVariables["HTTP_HOST"], String.Format("procId={0}", ProcessID));
                    MessageBox.ShowAndRedirect("The Report has been sent to Anchors.", incidentURL);

                    break;
                }          
        
            case "Complete":
                {
                    string[] kerResuts = txtAnchorName.Text.Split(',');
                    if (kerResuts.Count() >= 2)
                    {
                        if (drpAnchorSigned.SelectedValue.Equals("false") || drpAnchor2Signed.SelectedValue.Equals("false"))
                        {
                            MessageBox.Show("All Anchor(s) must sign the report first");
                            return;
                        }
                        if (!drpAnchor2Signed.SelectedValue.ToLower().Equals("true"))
                        {
                            MessageBox.Show("Please approve or sign first");
                            drpAnchor2Signed.Focus();
                            drpAnchor2Signed.Attributes.Add("class", "required");
                            return;
                        }
                        if (!drpAnchor2Approved.SelectedValue.ToLower().Equals("true"))
                        {
                            MessageBox.Show("Please approve or sign first");
                            drpAnchor2Approved.Focus();
                            drpAnchor2Approved.Attributes.Add("class", "required");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtAnchor2SignedDate.Text))
                        {
                            MessageBox.Show("The Anchor sign date is required");
                            txtAnchor2SignedDate.Focus();
                            txtAnchor2SignedDate.Attributes.Add("class", "required");
                            return;
                        }
                    }
                    if (!drpAnchorSigned.SelectedValue.ToLower().Equals("true"))
                    {
                        MessageBox.Show("Please approve or sign first");
                        drpAnchorSigned.Focus();
                        drpAnchorSigned.Attributes.Add("class", "required");
                        return;
                    }
                    if (!drpAnchorApproved.SelectedValue.ToLower().Equals("true"))
                    {
                        MessageBox.Show("Please approve or sign first");
                        drpAnchorApproved.Focus();
                        drpAnchorApproved.Attributes.Add("class", "required");
                        return;
                    }
                    if (string.IsNullOrEmpty(txtAnchorSignedDate.Text))
                    {
                        MessageBox.Show("The Anchor sign date is required");
                        txtAnchorSignedDate.Focus();
                        txtAnchorSignedDate.Attributes.Add("class", "required");
                        return;
                    }


                    report = SaveIncident(12);
                    var reportBackup = IncidentTrackingManager.NTQ_Report_PrintByReportId(Request["Id"]);
                    if (report != null)
                    {
                       var  reportB = reportBackup[0].ToXml<NTQ_PrintReport>();
                        AnchorBackupReport(reportB, int.Parse(Request["Id"]));
                    }
                        if (!string.IsNullOrEmpty(txtNotes.Text.Trim()))
                    {
                        var workInfo = new WorkInfoDetails
                        {
                            AddedBySID = SarsUser.SID,
                            IncidentId = Convert.ToDecimal(IncidentID),
                            ProcessId = Convert.ToInt32(ProcessID),
                            Notes = txtNotes.Text
                        };
                        IncidentTrackingManager.AddWorkInfo(workInfo);
                    }

                    var obj = IncidentTrackingManager.NTQ_Report_KeyResults_SelectByID(int.Parse(Request["keyResult"]));
                    if (obj != null)
                    {
                        var userInUnits = IncidentTrackingManager.NTQ_Report_GetUserUnitsBySid(SarsUser.SID, obj[0].fk_Lookup_KeyResult_ID);
                        if (userInUnits != null)
                        {
                            var usersInUnit = IncidentTrackingManager.NTQ_Report_GetUsersByUnit(userInUnits[0].unitId, SarsUser.SID);
                            if (usersInUnit != null)
                            {
                                int numOfAnchor = 1;
                                if (txtAnchorName.Text.Contains(","))
                                {
                                    numOfAnchor = 2;
                                }
                                usersInUnit = usersInUnit.Where(u => u.RoleId != 3).ToList();
                                switch (numOfAnchor)
                                {
                                    case 1:

                                        AnchorApprovedEMail(CurrentIncidentDetails.CreatedBySID, usersInUnit, drpKeyResult.SelectedItem.Text.Replace("\n", "").Replace("\r", ""), drpQuarter.SelectedValue, Request["Id"], Request["keyResult"]);
                                        break;
                                    case 2:
                                        AnchorsApprovedEMail(CurrentIncidentDetails.CreatedBySID, usersInUnit, drpKeyResult.SelectedItem.Text.Replace("\n", "").Replace("\r", ""), drpQuarter.SelectedValue, Request["Id"], Request["keyResult"]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    var adUser4 = SarsUser.GetADUser(SarsUser.SID);
                    if (adUser4 != null)
                    {
                        IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} approved the Key result Report {1}, Financial Year {3} on {2} and no further action is required", adUser4.SID + " | " + adUser4.FullName, drpKeyResult.SelectedItem.Text, DateTime.Now, FY), report, SarsUser.SID, DateTime.Now);
                    }
                    var incidentURL = string.Format("http://{0}/ims/NTQR/NormalUserLandingPage.aspx?{1}", Request.ServerVariables["HTTP_HOST"], String.Format("procId={0}", ProcessID));
                    MessageBox.ShowAndRedirect("The Report has been Approved.", incidentURL);

                    break;
                }
            case "Cancel":
                {
                    Response.Redirect(String.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));
                    break;
                }

            case "Rework":
                {
                    mpAllocate.Show();                  

                    break;
                }
            case "PrintScreen":
               // report = BindPage(Request["Id"]);
                PrintReport(Request["Id"]);
                var adUser = SarsUser.GetADUser(SarsUser.SID);
                if (adUser != null)
                {
                    IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} Printed/Downloaded the Key result Report {1} and Financial Year {3} on {2}.", adUser.SID + " | " + adUser.FullName, drpKeyResult.SelectedItem.Text, DateTime.Now, FY), report, SarsUser.SID, DateTime.Now);
                }
                break;
            case "EDRSaveAndClose":
                {

                    report = SaveIncident(int.Parse(StatusId));
                    if (!string.IsNullOrEmpty(txtNotes.Text.Trim()))
                    {
                        var workInfo = new WorkInfoDetails
                        {
                            AddedBySID = SarsUser.SID,
                            IncidentId = Convert.ToDecimal(IncidentID),
                            ProcessId = Convert.ToInt32(ProcessID),
                            Notes = txtNotes.Text
                        };
                        IncidentTrackingManager.AddWorkInfo(workInfo);
                    }
                    var adUser1 = SarsUser.GetADUser(SarsUser.SID);
                    if (adUser1 != null)
                    {
                        IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} Saved Key result Report {1} and Financial Year {3} on {2}", adUser1.SID + " | " + adUser1.FullName, drpKeyResult.SelectedItem.Text, DateTime.Now, FY), report, SarsUser.SID, DateTime.Now);
                    }

                    var incidentURL = string.Format("http://{0}/ims/NTQR/NormalUserLandingPage.aspx?{1}", Request.ServerVariables["HTTP_HOST"], String.Format("procId={0}", ProcessID));
                    MessageBox.ShowAndRedirect("The record has been saved successfully.", incidentURL);


                    break;
                }

        }
    }

    public void AnchorBackupReport(string report, int reportId)
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

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        e.Row.Attributes.Add("onclick",
                             Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex));
        var description = DataBinder.Eval(e.Row.DataItem, "Notes").ToString();

        e.Row.Attributes.Add("onmouseover", "style.cursor='cursor'");
        e.Row.Attributes.Add("title",
                             "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Work Info Notes</font></b>] body=[<font color='red'><b>" +
                             description + "</b></font>]");
    }
    protected void ViewDocuments(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(CurrentIncident.AssignedToSID))
        {
            //tbContainer.ActiveTabIndex = 0;
            MessageBox.Show("Save your incident details before adding your documents ");
            return;
        }
        var btn = sender as LinkButton;
        if (btn != null)
        {
            var row = btn.Parent.Parent as GridViewRow;
            if (row != null)
            {
                gvWorkInfo.SelectRow(row.RowIndex);
                if (gvWorkInfo.SelectedDataKey != null)
                {
                    var workInfoId = gvWorkInfo.SelectedDataKey["WorkInfoId"];
                    Response.Redirect(String.Format("~/SurveyWizard/AttachDocuments.aspx?procId={0}&incId={1}&noteId={2}", ProcessID, IncidentID, workInfoId));
                }
            }
        }
    }
    protected void AddNote(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtNotes.Text.Trim()))
        {
            MessageBox.Show("Please add a comment");
            return ;
        }
            // treeNotes.SelectedNode.Expanded = false;
            var workInfo = new WorkInfoDetails
        {
            AddedBySID = SarsUser.SID,
            IncidentId = Convert.ToDecimal(IncidentID),
            ProcessId = Convert.ToInt32(ProcessID),
            Notes = txtNotes.Text
        };
        if (IncidentTrackingManager.AddWorkInfo(workInfo) > 0)
        {
            var compilerDetails = SarsUser.GetADUser(CurrentIncidentDetails.CreatedBySID);
            string compilerEmail = string.Empty;
            if (compilerDetails != null)
            {
                compilerEmail = compilerDetails.Mail;
            }
            var obj = IncidentTrackingManager.NTQ_Report_KeyResults_SelectByID(int.Parse(Request["keyResult"]));
            switch (CurrentIncidentDetails.IncidentStatusId)
            {
                case 9:
                    EmailUsersWithCommentAdded(compilerDetails.SID, drpKeyResult.SelectedItem.Text, drpQuarter.SelectedValue, Request["Id"], Request["keyResult"], compilerDetails.Mail, txtNotes.Text);

                    break;
                    case 10:
                    EmailUsersWithCommentAdded(obj[0].KeyResultOwner, drpKeyResult.SelectedItem.Text, drpQuarter.SelectedValue, Request["Id"], Request["keyResult"], compilerDetails.Mail, txtNotes.Text);

                    break;
                case 11:
                    EmailUsersWithCommentAdded(obj[0].Anchor, drpKeyResult.SelectedItem.Text, drpQuarter.SelectedValue, Request["Id"], Request["keyResult"], compilerDetails.Mail, txtNotes.Text);
                    break;
                case 12:
                    EmailUsersWithCommentAdded(obj[0].Anchor, drpKeyResult.SelectedItem.Text, drpQuarter.SelectedValue, Request["Id"], Request["keyResult"], compilerDetails.Mail, txtNotes.Text);

                    break;
                default:
                    break;
            }
           
            txtNotes.Clear();
            MessageBox.Show("Work Info added successfully.");
            LoadInfo();
           
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        treeNotes.SelectedNode.Expand();

    }
    protected void Toolbar2_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Submit":
                {

                    if (!string.IsNullOrEmpty(txtSummary.Text.Trim()))
                    {
                       var report = SaveIncident(9);

                        CreateReportBackup("ReworkCopyOf_" + drpQuarter.SelectedItem.Text + "Report.pdf", Request["Id"]);
                        IncidentTrackingManager.NQT_UpdateReworkSignedParties(int.Parse(Request["Id"]));
                        var obj = IncidentTrackingManager.NTQ_Report_KeyResults_SelectByID(int.Parse(Request["keyResult"]));
                        var userInUnits = IncidentTrackingManager.NTQ_Report_GetUserUnitsBySid(SarsUser.SID, obj[0].fk_Lookup_KeyResult_ID);
                        if( userInUnits != null)
                        {
                            var usersInUnit = IncidentTrackingManager.NTQ_Report_GetUsersByUnit(userInUnits[0].unitId, SarsUser.SID);
                            if (usersInUnit != null)
                            {
                                EmailReworkReport(CurrentIncidentDetails.CreatedBySID, usersInUnit, drpKeyResult.SelectedItem.Text, drpQuarter.SelectedValue, Request["Id"], Request["keyResult"]);
                                var workInfo = new WorkInfoDetails
                                {
                                    AddedBySID = SarsUser.SID,
                                    IncidentId = Convert.ToDecimal(IncidentID),
                                    ProcessId = Convert.ToInt32(ProcessID),
                                    Notes = txtSummary.Text
                                };
                                IncidentTrackingManager.AddWorkInfo(workInfo);
                            }
                        }
                        
                        var adUser5 = SarsUser.GetADUser(SarsUser.SID);
                        if (adUser5 != null)
                        {
                            IncidentTrackingManager.NTQ_User_Actions_Insert(string.Format("{0} sent the Key result Report {1} and Financial Year {3} on {2} for re-work.", adUser5.SID + " | " + adUser5.FullName, drpKeyResult.SelectedItem.Text, DateTime.Now, FY), report, SarsUser.SID, DateTime.Now);
                        }
                        MessageBox.Show("The Report has been sent for re-work.");
                        Response.Redirect(String.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));

                        
                    }
                    else
                    {
                        mpAllocate.Hide();
                        MessageBox.Show("Please add a reason for rework.");
                        mpAllocate.Show();
                    }

                    break;
                }
            case "Cancel":
                {
                    Response.Redirect(string.Format("NormalUserLandingPage.aspx?procId={0}", ProcessID));
                    break;
                }
        }
    }
    private void BindDropDownOnSeleted()
    {
        if (drpStrategicObjective.SelectedIndex > 0)
        {
            var results = IncidentTrackingManager.GetKeyResultByObjectivesID(Convert.ToInt32(drpStrategicObjective.SelectedValue));
            drpKeyResult.Bind(results, "Name", "Id");

            results = IncidentTrackingManager.GetKeyResultIndicatorByObjectivesID(Convert.ToInt32(drpStrategicObjective.SelectedValue));
            drpKeyResultIndicator.Bind(results, "Name", "Id");

            var result = IncidentTrackingManager.GetNTQ_YesNo();
            drpTargetMet.Bind(result, "Name", "Id");

            result = IncidentTrackingManager.GetNTQ_YesNo();
            drpDataValidAndCorrect.Bind(result, "Name", "Id");

            result = IncidentTrackingManager.GetNTQ_YesNo();
            drpCalculatedAccordingTID.Bind(result, "Name", "Id");

            var tid = IncidentTrackingManager.GetTID(Convert.ToInt32(drpStrategicObjective.SelectedValue));
            drpTID.Bind(tid, "Description", "Id");
            results = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_AnnualTarget(Convert.ToInt32(drpStrategicObjective.SelectedValue));
            drpAnnualTarget.Bind(results, "Name", "Id");


            results = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_Q1(Convert.ToInt32(drpStrategicObjective.SelectedValue));
            drpQuarterOneTarget.Bind(results, "Name", "Id");

            results = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_Q2(Convert.ToInt32(drpStrategicObjective.SelectedValue));
            drpQuarter2Target.Bind(results, "Name", "Id");

            results = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_Q3(Convert.ToInt32(drpStrategicObjective.SelectedValue));
            drpQuarter3Target.Bind(results, "Name", "Id");

            results = IncidentTrackingManager.NTQ_SELECT_NTQ_Lookup_Q4(Convert.ToInt32(drpStrategicObjective.SelectedValue));
            drpQuarter4Target.Bind(results, "Name", "Id");

        }
    }
    protected void drpStrategicObjective_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDropDownOnSeleted();
    }
    protected void drpCompilerSign_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(!drpCompilers.SID.ToLower().Trim().Equals(SarsUser.SID.ToLower()))
        {
            MessageBox.Show("Please sign using your sid and name.");
            return;
        }
        txtCompilerDate.Enabled = true;
        txtCompilerDate.Clear();
        if (drpCompilerSign.SelectedValue.ToLower().Equals("true"))
        {
            txtCompilerDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            txtCompilerDate.Enabled = false;
        }
    }
    protected void drpKeyResultOwnerSigned_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!drpKeyResultOwnerAprover.SID.ToLower().Trim().Equals(SarsUser.SID.ToLower()))
        {
            MessageBox.Show("Please sign with your sid and name.");
            return;
        }
        txtKeyResultDate.Enabled = true;
        txtKeyResultDate.Clear();
        if (drpKeyResultOwnerSigned.SelectedValue.ToLower().Equals("true"))
        {
            txtKeyResultDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            txtKeyResultDate.Enabled = false;
        }
    }
    protected void drpKeyResultOwner2Signed_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!drpKeyResultOwner2Aprover.SID.ToLower().Trim().Equals(SarsUser.SID.ToLower()))
        {
            MessageBox.Show("Please sign with your sid and name.");
            return;
        }
        txtKeyResult2Date.Enabled = true;
        txtKeyResult2Date.Clear();
        if (drpKeyResultOwner2Signed.SelectedValue.ToLower().Equals("true"))
        {
            txtKeyResult2Date.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            txtKeyResult2Date.Enabled = false;
        }
    }
    protected void drpAnchorSigned_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!drpAnchorName.SID.ToLower().Trim().Equals(SarsUser.SID.ToLower()))
        {
            MessageBox.Show("Please sign with your sid and name.");
            return;
        }
        txtAnchorSignedDate.Enabled = true;
        txtAnchorSignedDate.Clear();
        if (drpAnchorSigned.SelectedValue.ToLower().Equals("true"))
        {
            txtAnchorSignedDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            txtAnchorSignedDate.Enabled = false;
        }
    }
    protected void drpAnchor2Signed_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!drpAnchor2Name.SID.ToLower().Trim().Equals(SarsUser.SID.ToLower()))
        {
            MessageBox.Show("Please sign with your sid and name.");
            return;
        }
        txtAnchor2SignedDate.Enabled = true;
        txtAnchor2SignedDate.Clear();
        if (drpAnchor2Signed.SelectedValue.ToLower().Equals("true"))
        {
            txtAnchor2SignedDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            txtAnchor2SignedDate.Enabled = false;
        }
    }
    private void PrintReport(string Id)
    {

        string path = "/IMS/NTQR_Quarter_Report";
        ReportViewer ReportViewer1 = new ReportViewer();
        ReportViewer1.ProcessingMode = ProcessingMode.Remote;
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
        ReportViewer1.ServerReport.ReportPath = path;
        ReportParameter[] reportParam = new ReportParameter[1];
        reportParam[0] = new ReportParameter("ID", Id);
        ReportViewer1.ServerReport.SetParameters(reportParam);

        // ReportViewer1.ServerReport.Refresh();

        Warning[] warnings;
        string[] streamIds;
        string contentType;
        string encoding;
        string extension;

        //Export the RDLC Report to Byte Array.
        byte[] bytes = ReportViewer1.ServerReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

        //Download the RDLC Report in Word, Excel, PDF and Image formats.
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=Quarterly Report." + extension);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
    private void CreateReportBackup(string quarterFileName, string Id)
    {
        try
        {

            string path = "/IMS/NTQR_Quarter_Report";
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["reports-url"]);
            ReportViewer1.ServerReport.ReportPath = path;
            ReportParameter[] reportParam = new ReportParameter[1];
            reportParam[0] = new ReportParameter("ID", Id);
            ReportViewer1.ServerReport.SetParameters(reportParam);

            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;

            //Export the RDLC Report to Byte Array.
            byte[] bytes = ReportViewer1.ServerReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);


            if (bytes.Length > 0)
            {

                var saved = IncidentTrackingManager.UploadFile(IncidentID, null, bytes, quarterFileName);
            }
        }
        catch (Exception)
        {

            MessageBox.Show("The copy of the report is unsuccessfully created");
        }
    }

    protected void drpCalculatedAccordingTID_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}