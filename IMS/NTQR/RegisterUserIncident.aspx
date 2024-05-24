<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="RegisterUserIncident.aspx.cs" Inherits="NTQR_RegisterUserIncident" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<%@ Register Src="~/Admin/UserSelector.ascx" TagPrefix="uc1" TagName="UserSelector" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/Admin/UCAttachDocuments.ascx" TagPrefix="asp" TagName="UCAttachDocuments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../Scripts/boxover.js"></script>
    <script type="text/javascript" src="../Scripts/_validation.js"></script>

    <script src="../Scripts/webservices.js" type="text/javascript"></script>

    <link href="sceditor-3.1.1/minified/themes/office.min.css" rel="stylesheet" />

    <script src="sceditor-3.1.1/minified/sceditor.min.js"></script>
    <script src="sceditor-3.1.1/minified/icons/monocons.js"></script>
    <script src="sceditor-3.1.1/minified/formats/bbcode.js"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="//resources/demos/style.css">


    <style>
        .textArea {
            height: 300px;
            width: 100%;
        }

        .required {
            border: 1px solid red !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="widget-header widget-header-blue widget-header-flat">
        <h4 class="widget-title lighter"><%= CurrentProcess != null ? string.Format("{0} v{1} ",CurrentProcess.Description, CurrentProcess.Version)   : string.Empty %> 
            -
            <%= CurrentIncidentDetails.ReferenceNumber %></h4>


    </div>





    <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked" ClientIDMode="Static" EnableClientApi="False" CssClass="toolbar" Width="100%">
        <Items>
            <SCS:ToolbarButton CausesValidation="False" CommandName="SaveAndClose" Text="Save & Close"  Visible="false" />
            <SCS:ToolbarButton CausesValidation="True" CommandName="SendToKeyResultOwner" Text="Send To Key Result Owner" Visible="false" />
            <SCS:ToolbarButton CausesValidation="True" CommandName="SendToSecKeyResultOwner" Text="Send To Next Key Result Owner" Visible="false" />      
            <SCS:ToolbarButton CausesValidation="True" CommandName="SendToAnchor" Text="Send To Anchor" Visible="false" />
             
            <SCS:ToolbarButton CausesValidation="True" CommandName="Rework" Text="Re-Work" Visible="false" />
            <SCS:ToolbarButton CausesValidation="True" CommandName="Complete" Text="Approve" Visible="false" />
            <SCS:ToolbarButton CausesValidation="False" CommandName="PrintScreen" Text="Print" Visible="true" />
            <SCS:ToolbarButton CausesValidation="False" CommandName="Cancel" Text="Cancel"  />

        </Items>
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
    </SCS:Toolbar>
    <hr />


    <div class="widget-box">
        <div class="widget-header">
            <h4 class="widget-title">Default Information</h4>

            <div class="widget-toolbar">
                <a href="#" data-action="collapse">
                    <i class="ace-icon fa fa-chevron-up"></i>
                </a>

                <a href="#" data-action="close">
                    <i class="ace-icon fa fa-times"></i>
                </a>
            </div>
        </div>

        <div class="widget-body">
            <div class="widget-main">
                <div class="form-group has-info">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Compiler Name:</label>

                    <div class="col-xs-12 col-sm-5">
                        <span class="block input-icon input-icon-right">

                            <input type="text" id="txtCreatedBy" runat="server" class="" disabled="disabled" />
                        </span>
                    </div>


                </div>
                <div class="form-group has-info" runat="server" visible="False">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Registered By:</label>

                    <div class="col-xs-12 col-sm-5">
                        <span class="block input-icon input-icon-right">

                            <uc1:UserSelector class="width-100" ID="UserSelector1" runat="server" />

                        </span>
                    </div>

                </div>

                <div class="form-group has-info" runat="server" visible="False">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Reference Number:</label>

                    <div class="col-xs-12 col-sm-5">
                        <span class="block input-icon input-icon-right">

                            <input type="text" class="width-100" disabled="disabled" value="<%=CurrentIncidentDetails.ReferenceNumber %>" />
                            <i class="ace-icon fa fa-info-circle"></i>
                        </span>
                    </div>

                </div>
                <div class="form-group has-info">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Report Status:</label>

                    <div class="col-xs-12 col-sm-5">
                        <span class="block input-icon input-icon-right">

                            <input type="text" class="width-100" disabled="disabled" value="<%=CurrentIncidentDetails.IncidentStatus %>" />
                            <i class="ace-icon fa fa-info-circle"></i>
                        </span>
                    </div>

                </div>
                  <div class="form-group has-info">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Financial Year:</label>

                    <div class="col-xs-12 col-sm-5">
                        <span class="block input-icon input-icon-right">

                            <asp:TextBox type="text" class="width-100" runat="server" ID="txtFY" Enabled="false" />
                            <i class="ace-icon fa fa-info-circle"></i>
                        </span>
                    </div>

                </div>
                <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Quarter: <span style="color: red">*</span></label>
                    <span style="color: red">*</span>
                    <div class="col-xs-12 col-sm-5">

                        <asp:DropDownList ID="drpQuarter" runat="server">
                        </asp:DropDownList>

                        <asp:CompareValidator ErrorMessage="Quarter is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpQuarter" runat="server" Operator="NotEqual" SetFocusOnError="True" />
                    </div>

                </div>
                <div class="form-group has-info" runat="server" visible="False">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Report Due Date:</label>

                    <div class="col-xs-12 col-sm-5">
                        <span class="block input-icon input-icon-right">
                            <asp:TextBox type="text" class="width-100" runat="server" ID="txtIncidentDueDate" />

                            <i class="ace-icon fa fa-info-circle"></i>
                        </span>
                    </div>

                </div>

            </div>

        </div>
    </div>

    <div class="widget-box">
        <div class="widget-header">
            <h4 class="widget-title">Objectives</h4>

            <div class="widget-toolbar">
                <a href="#" data-action="collapse">
                    <i class="ace-icon fa fa-chevron-up"></i>
                </a>

                <a href="#" data-action="close">
                    <i class="ace-icon fa fa-times"></i>
                </a>
            </div>
        </div>

        <div class="widget-body">
            <div class="widget-main">
                <div class="form-group has-info" runat="server">
                    <asp:ValidationSummary runat="server" ID="vSummary" ShowSummary="false" ShowMessageBox="true" />
                </div>

            </div>

            <div class="form-group has-info" runat="server">
                <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Strategic Objective: <span style="color: red">*</span></label>

                <div class="col-xs-12 col-sm-10">
                    

                            <asp:DropDownList ID="drpStrategicObjective" Enabled="false" class="width-100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpStrategicObjective_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CompareValidator ErrorMessage="StrategicObjective is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpStrategicObjective" runat="server" Operator="NotEqual" SetFocusOnError="True" />


                      

                </div>

            </div>

            <div class="form-group has-info" runat="server">
                <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Key Result: <span style="color: red">*</span></label>

                <div class="col-xs-12 col-sm-10">
                
                            <asp:DropDownList ID="drpKeyResult" Enabled="false" class="width-100" runat="server">
                            </asp:DropDownList>
                            <asp:CompareValidator ErrorMessage="KeyResult is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpKeyResult" runat="server" Operator="NotEqual" SetFocusOnError="True" />

                      
                </div>

            </div>
            <div class="form-group has-info" runat="server">
                <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Key Result Indicator: <span style="color: red">*</span></label>

                <div class="col-xs-12 col-sm-10">
                

                            <asp:DropDownList ID="drpKeyResultIndicator" Enabled="false" class="width-100" runat="server">
                            </asp:DropDownList>
                            <asp:CompareValidator ErrorMessage="KeyResultIndicator is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpKeyResultIndicator" runat="server" Operator="NotEqual" SetFocusOnError="True" />

                        
                </div>
            </div>

            <div class="form-group has-info" runat="server">
                <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Anchor: <span style="color: red">*</span></label>

                <div class="col-xs-12 col-sm-10">

                    <asp:ListBox runat="server" ID="drpAnchor" class="width-100" SelectionMode="Multiple" visible="false"></asp:ListBox>
                     <asp:TextBox ID="txtAnchorName" runat="server" ClientIDMode="Static" Enabled="false"  />

                </div>

            </div>
            <div class="form-group has-info" runat="server">

                <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right input-icon input-icon-right">
                    Key Result Owner: <span style="color: red">*</span>


                </label>

                <div class="col-xs-12 col-sm-10">


                    <asp:ListBox runat="server" ID="drpKeyResultOwner" class="width-100" SelectionMode="Multiple" visible="false"></asp:ListBox>
                    <asp:TextBox ID="txtKeyResultOwnerName" runat="server" ClientIDMode="Static"  Enabled="false"  />
                </div>

            </div>
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>

                    <div class="form-group has-info" runat="server">
                        <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Annual Target:</label>

                        <div class="col-xs-12 col-sm-10">
                            <asp:DropDownList ID="drpAnnualTarget" Enabled="false" runat="server">
                            </asp:DropDownList>
                            <asp:CompareValidator ErrorMessage="AnnualTarget is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpAnnualTarget" runat="server" Operator="NotEqual" SetFocusOnError="True" />

                        </div>

                    </div>
                    <div class="form-group has-info" runat="server">
                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">Quarter One Target: <span style="color: red">*</span></label>

                        <div class="col-xs-12 col-sm-10">
                            <asp:DropDownList ID="drpQuarterOneTarget" Enabled="false" runat="server">
                            </asp:DropDownList>
                            <asp:CompareValidator ErrorMessage="QuarterOneTarget is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpQuarterOneTarget" runat="server" Operator="NotEqual" SetFocusOnError="True" />

                        </div>

                    </div>
                    <div class="form-group has-info" runat="server">
                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">Quarter Two Target: <span style="color: red">*</span></label>

                        <div class="col-xs-12 col-sm-10">
                            <asp:DropDownList ID="drpQuarter2Target" Enabled="false" runat="server">
                            </asp:DropDownList>
                            <asp:CompareValidator ErrorMessage="drpQuarter2Target is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpQuarter2Target" runat="server" Operator="NotEqual" SetFocusOnError="True" />

                        </div>

                    </div>
                    <div class="form-group has-info" runat="server">
                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">Quarter Three Target: <span style="color: red">*</span></label>

                        <div class="col-xs-12 col-sm-10">
                            <asp:DropDownList ID="drpQuarter3Target" Enabled="false" runat="server">
                            </asp:DropDownList>
                            <asp:CompareValidator ErrorMessage="drpQuarter3Target is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpQuarter3Target" runat="server" Operator="NotEqual" SetFocusOnError="True" />

                        </div>

                    </div>
                    <div class="form-group has-info" runat="server">
                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">Quarter Four Target: <span style="color: red">*</span></label>

                        <div class="col-xs-12 col-sm-10">
                            <asp:DropDownList ID="drpQuarter4Target" Enabled="false" runat="server">
                            </asp:DropDownList>
                            <asp:CompareValidator ErrorMessage="drpQuarter4Target is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpQuarter4Target" runat="server" Operator="NotEqual" SetFocusOnError="True" />

                        </div>

                    </div>

                    <div class="form-group has-info" runat="server">
                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">TECHNICAL INDICATOR DESCRIPTION: <span style="color: red">*</span></label>

                        <div class="col-xs-12 col-sm-5">

                            <asp:DropDownList ID="drpTID" runat="server" Enabled="false">
                            </asp:DropDownList>
                            <asp:CompareValidator ErrorMessage="drpTID is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpTID" runat="server" Operator="NotEqual" SetFocusOnError="True" />


                        </div>
                    </div>
                     <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right"> </label>

                    <div class="col-xs-12 col-sm-10">

                        <asp:image id="Image3" runat="server"  />

                    </div>

                </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

    </div>


    <div class="widget-box">
        <div class="widget-header">
            <h4 class="widget-title" id="lblQheader" runat="server">QUARTER REPORT
            </h4>

            <div class="widget-toolbar">
                <a href="#" data-action="collapse">
                    <i class="ace-icon fa fa-chevron-up"></i>
                </a>

                <a href="#" data-action="close">
                    <i class="ace-icon fa fa-times"></i>
                </a>
            </div>
        </div>

        <div class="widget-body">
            <div class="widget-main">
                <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                        Actual Achievement: <span style="color: red">*</span></label>

                    <div class="col-xs-12 col-sm-10">
                        <asp:TextBox ID="txtActualAchievement" runat="server" ClientIDMode="Static" />
                        <asp:RequiredFieldValidator ErrorMessage="Actual Achievement Target is a required field" Text="*" ForeColor="Red" ControlToValidate="txtActualAchievement" runat="server" />
                    </div>

                </div>
                <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                        Variance(Actual less Target): <span style="color: red">*</span></label>

                    <div class="col-xs-12 col-sm-10">
                        <asp:TextBox ID="txtVariance" runat="server" ClientIDMode="Static" />
                        <asp:RequiredFieldValidator ErrorMessage="Variance Target is a required field" Text="*" ForeColor="Red" ControlToValidate="txtVariance" runat="server" />
                    </div>

                </div>
                <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                        Target Met: <span style="color: red">*</span></label>

                    <div class="col-xs-12 col-sm-5">
                        <asp:DropDownList ID="drpTargetMet" runat="server" ClientIDMode="Static">
                        </asp:DropDownList>
                        <asp:CompareValidator ErrorMessage="drpTargetMet is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpTargetMet" runat="server" Operator="NotEqual" SetFocusOnError="True" />
                    </div>

                </div>
                <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                        Data Valid And Correct: <span style="color: red">*</span></label>

                    <div class="col-xs-12 col-sm-5">
                        <asp:DropDownList ID="drpDataValidAndCorrect" runat="server" ClientIDMode="Static">
                        </asp:DropDownList>
                        <asp:CompareValidator ErrorMessage="drpDataValidAndCorrect is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpDataValidAndCorrect" runat="server" Operator="NotEqual" SetFocusOnError="True" />
                    </div>

                </div>
                <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Reason For Variance: <span style="color: red">*</span></label>

                    <div class="col-xs-12 col-sm-10">
                        <asp:TextBox ID="txtReasonForVariance" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="textArea" />
                        <asp:RequiredFieldValidator ErrorMessage="ReasonForVariance is a required field" Text="*" ForeColor="Red" ControlToValidate="txtReasonForVariance" runat="server" />

                    </div>

                </div>
                <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">
                        Mitigation For Underperformance (When variance is a
                       negative): <span style="color: red">*</span></label>

                    <div class="col-xs-12 col-sm-10">
                        <asp:TextBox ID="txtMitigation" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="textArea" />
                        <asp:RequiredFieldValidator ErrorMessage="Mitigation is a required field" Text="*" ForeColor="Red" ControlToValidate="txtMitigation" runat="server" />

                    </div>

                </div>
                <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">
                        Comment On Performance (Progress, actions and
activities completed during
the quarter) <span style="color: red">*</span></label>

                    <div class="col-xs-12 col-sm-10">
                        <asp:TextBox ID="txtCommentOnPerformance" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="textArea" />
                        <asp:RequiredFieldValidator ErrorMessage="CommentOnPerformance is a required field" Text="*" ForeColor="Red" ControlToValidate="txtCommentOnPerformance" runat="server" />

                    </div>

                </div>
                <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">Calculated according to the Technical Indicator Descriptions: <span style="color: red">*</span></label>

                    <div class="col-xs-12 col-sm-5">
                        <asp:DropDownList ID="drpCalculatedAccordingTID" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="drpCalculatedAccordingTID_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:CompareValidator ErrorMessage="drpCalculatedAccordingTID is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpCalculatedAccordingTID" runat="server" Operator="NotEqual" SetFocusOnError="True" />
                    </div>

                </div>
               

                <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">
                        If not calculated
                                                                            according to the TID,
                                                                            please state reason why
                                                                            and the calculation
                                                                            used <span style="color: red">*</span></label>

                    <div class="col-xs-12 col-sm-10">
                        <asp:TextBox ID="txtIfNotCalculatedAccordingToTID" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="textArea" />
                        <%--<asp:RequiredFieldValidator ErrorMessage="Comment On IfNotCalculatedAccordingToTID is a required field" Text="*" ForeColor="Red" ControlToValidate="txtIfNotCalculatedAccordingToTID" runat="server" />--%>
                       
                        
                    </div>

                </div>
                <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Data Source: <span style="color: red">*</span></label>

                    <div class="col-xs-12 col-sm-10">
                        <asp:TextBox ID="txtDataSoruce" runat="server" ClientIDMode="Static" />
                        <asp:RequiredFieldValidator ErrorMessage=" Data Soruce is a required field" Text="*" ForeColor="Red" ControlToValidate="txtDataSoruce" runat="server" />


                    </div>

                </div>
                <div class="form-group has-info" runat="server">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Evidence (Provide a list)<span>.
                        <br />
                        The evidence listed must be uploaded under Section: Comments &amp; Document Upload:</span> <span style="color: red">*</span></label>

                    <div class="col-xs-12 col-sm-10">
                        <asp:TextBox ID="txtEvidence" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="textArea" />
                        <asp:RequiredFieldValidator ErrorMessage="Evidence is a required field" Text="*" ForeColor="Red" ControlToValidate="txtEvidence" runat="server" />
                    </div>

                </div>
                <div class="form-group has-info" runat="server">

                    <div class="col-xs-12 col-sm-12">
                        <span style="color: red; font-weight: bold">
                            <ul>
                                <li>Please ensure that information provided is complete, accurate and valid and has been reviewed prior to
submission.</li>
                                <li>The information you provide is subject to audit by the Auditor-General and Internal Audit.</li>
                                <li>It must be emphasised that line managers remain responsible for establishing and running performance
information systems within their sections, and for using performance information to make decisions.</li>
                            </ul>
                        </span>
                    </div>

                </div>

            </div>

        </div>
    </div>

    <div class="widget-box">
        <div class="widget-header">
            <h4 class="widget-title" id="H1" runat="server">Compilers & Approvals
            </h4>

            <div class="widget-toolbar">
                <a href="#" data-action="collapse">
                    <i class="ace-icon fa fa-chevron-up"></i>
                </a>

                <a href="#" data-action="close">
                    <i class="ace-icon fa fa-times"></i>
                </a>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>


                <asp:Panel runat="server" ID="pnlCompiler" Enabled="false">
                    <div class="widget-body">
                        <div class="widget-main">

                            <div class="form-group has-info" runat="server">
                                <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                    Compiler of this Report:<span style="color: red">Type & Select Your Name</span></label>

                                <div class="col-xs-12 col-sm-5">

                                    <uc1:UserSelector ID="drpCompilers" runat="server" />

                                </div>

                            </div>
                            <div class="form-group has-info" runat="server">
                                <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                    Compiler Sign:</label>

                                <div class="col-xs-12 col-sm-5">
                                    <asp:DropDownList ID="drpCompilerSign" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCompilerSign_SelectedIndexChanged">
                                        <asp:ListItem Text="Select One .." Value="-99999" />
                                        <asp:ListItem Text="Signed" Value="true" />
                                        <asp:ListItem Text="Not Signed" Value="false" />
                                    </asp:DropDownList>
                                    <asp:CompareValidator ErrorMessage="drpCompilerSign is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpCompilerSign" runat="server" Operator="NotEqual" SetFocusOnError="True" />

                                </div>

                            </div>
                            <div class="form-group has-info" runat="server">
                                <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                    Date:</label><span style="color: red">*</span>

                                <div class="col-xs-12 col-sm-5">
                                    <asp:TextBox runat="server" ID="txtCompilerDate" />
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd"
                                        Enabled="True" TargetControlID="txtCompilerDate">
                                    </asp:CalendarExtender>
                                </div>

                            </div>
                        </div>
                    </div><hr />
                </asp:Panel>
                
                <asp:Panel runat="server" ID="pnlKeyResultApproval" Enabled="false">
                    <div class="widget-body">
                        <div class="widget-main">

                            <div class="form-group has-info" runat="server">
                                <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                    Key Result owner:<span style="color: red">Type & Select Your Name</span></label>

                                <div class="col-xs-12 col-sm-5">
                                    <uc1:UserSelector ID="drpKeyResultOwnerAprover" runat="server" />
                                </div>

                            </div>
                            <div class="form-group has-info" runat="server">
                                <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                    Approve:</label>

                                <div class="col-xs-12 col-sm-5">
                                    <asp:DropDownList ID="drpKeyResultApproved" runat="server">
                                        <asp:ListItem Text="Select One .." Value="-99999" />
                                        <asp:ListItem Text="Approved" Value="true" />
                                        <asp:ListItem Text="Not Approved" Value="false" />
                                    </asp:DropDownList>
                                    <asp:CompareValidator ErrorMessage="drpKeyResultApproved is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpKeyResultApproved" runat="server" Operator="NotEqual" SetFocusOnError="True" />


                                </div>

                            </div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="form-group has-info" runat="server">
                                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                            Sign:</label>

                                        <div class="col-xs-12 col-sm-5">
                                            <asp:DropDownList ID="drpKeyResultOwnerSigned" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpKeyResultOwnerSigned_SelectedIndexChanged">
                                                <asp:ListItem Text="Select One .." Value="-99999" />
                                                <asp:ListItem Text="Signed" Value="true" />
                                                <asp:ListItem Text="Not Signed" Value="false" />
                                            </asp:DropDownList>
                                            <asp:CompareValidator ErrorMessage="drpKeyResultOwnerSigned is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpKeyResultOwnerSigned" runat="server" Operator="NotEqual" SetFocusOnError="True" />


                                        </div>

                                    </div>
                                    <div class="form-group has-info" runat="server">
                                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                            Date:</label><span style="color: red">*</span>

                                        <div class="col-xs-12 col-sm-5">
                                            <asp:TextBox runat="server" ID="txtKeyResultDate" />
                                            <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                                Enabled="True" TargetControlID="txtKeyResultDate">
                                            </asp:CalendarExtender>
                                        </div>

                                    </div>


                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div><hr />
                </asp:Panel>    
                <asp:Panel runat="server" ID="pnlKeyResultApproval2" Enabled="false">
                    <div class="widget-body">
                        <div class="widget-main">

                            <div class="form-group has-info" runat="server">
                                <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                    Key Result owner:<span style="color: red">Type & Select Your Name</span></label>

                                <div class="col-xs-12 col-sm-5">
                                    <uc1:UserSelector ID="drpKeyResultOwner2Aprover" runat="server" />
                                </div>

                            </div>
                            <div class="form-group has-info" runat="server">
                                <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                    Approve:</label>

                                <div class="col-xs-12 col-sm-5">
                                    <asp:DropDownList ID="drpKeyResult2Approved" runat="server">
                                        <asp:ListItem Text="Select One .." Value="-99999" />
                                        <asp:ListItem Text="Approved" Value="true" />
                                        <asp:ListItem Text="Not Approved" Value="false" />
                                    </asp:DropDownList>
                                    <asp:CompareValidator ErrorMessage="drpKeyResult2Approved is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpKeyResult2Approved" runat="server" Operator="NotEqual" SetFocusOnError="True" />


                                </div>

                            </div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="form-group has-info" runat="server">
                                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                            Sign:</label>

                                        <div class="col-xs-12 col-sm-5">
                                            <asp:DropDownList ID="drpKeyResultOwner2Signed" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpKeyResultOwner2Signed_SelectedIndexChanged">
                                                <asp:ListItem Text="Select One .." Value="-99999" />
                                                <asp:ListItem Text="Signed" Value="true" />
                                                <asp:ListItem Text="Not Signed" Value="false" />
                                            </asp:DropDownList>
                                            <asp:CompareValidator ErrorMessage="drpKeyResultOwner2Signed is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpKeyResultOwner2Signed" runat="server" Operator="NotEqual" SetFocusOnError="True" />


                                        </div>

                                    </div>
                                    <div class="form-group has-info" runat="server">
                                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                            Date:</label><span style="color: red">*</span>

                                        <div class="col-xs-12 col-sm-5">
                                            <asp:TextBox runat="server" ID="txtKeyResult2Date" />
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="yyyy-MM-dd"
                                                Enabled="True" TargetControlID="txtKeyResult2Date">
                                            </asp:CalendarExtender>
                                        </div>

                                    </div>


                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div><hr />
                </asp:Panel>    
                <asp:Panel runat="server" ID="pnlAnchor" Enabled="false">
                    <div class="widget-body">
                        <div class="widget-main">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>


                                    <div class="form-group has-info" runat="server">
                                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                            Anchor Name:<span style="color: red">Type & Select Your Name</span></label>

                                        <div class="col-xs-12 col-sm-5">

                                            <uc1:UserSelector ID="drpAnchorName" runat="server" />
                                        </div>

                                    </div>
                                    <div class="form-group has-info" runat="server">
                                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                            Anchor Signed:</label>

                                        <div class="col-xs-12 col-sm-5">
                                            <asp:DropDownList ID="drpAnchorSigned" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpAnchorSigned_SelectedIndexChanged">
                                                <asp:ListItem Text="Select One .." Value="-99999" />
                                                <asp:ListItem Text="Signed" Value="true" />
                                                <asp:ListItem Text="Not Signed" Value="false" />
                                            </asp:DropDownList>
                                            <asp:CompareValidator ErrorMessage="drpAnchorSigned is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpAnchorSigned" runat="server" Operator="NotEqual" SetFocusOnError="True" />



                                        </div>

                                    </div>
                                    <div class="form-group has-info" runat="server">
                                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                            Anchor Approved:</label>

                                        <div class="col-xs-12 col-sm-5">
                                            <asp:DropDownList ID="drpAnchorApproved" runat="server">
                                                <asp:ListItem Text="Select One .." Value="-99999" />
                                                <asp:ListItem Text="Approved" Value="true" />
                                                <asp:ListItem Text="Not Approved" Value="false" />
                                            </asp:DropDownList>
                                            <asp:CompareValidator ErrorMessage="drpAnchorApproved is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpAnchorApproved" runat="server" Operator="NotEqual" SetFocusOnError="True" />



                                        </div>

                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group has-info" runat="server">
                                <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                    Date:</label><span style="color: red">*</span>

                                <div class="col-xs-12 col-sm-5">
                                    <asp:TextBox runat="server" ID="txtAnchorSignedDate" />
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd"
                                        Enabled="True" TargetControlID="txtAnchorSignedDate">
                                    </asp:CalendarExtender>
                                </div>

                            </div>
                        </div>
                    </div><hr />
                </asp:Panel> 
                <asp:Panel runat="server" ID="pnlAnchor2" Enabled="false">
                    <div class="widget-body">
                        <div class="widget-main">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>


                                    <div class="form-group has-info" runat="server">
                                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                            Anchor Name:<span style="color: red">Type & Select Your Name</span></label>

                                        <div class="col-xs-12 col-sm-5">

                                            <uc1:UserSelector ID="drpAnchor2Name" runat="server" />
                                        </div>

                                    </div>
                                    <div class="form-group has-info" runat="server">
                                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                            Anchor Signed:</label>

                                        <div class="col-xs-12 col-sm-5">
                                            <asp:DropDownList ID="drpAnchor2Signed" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpAnchor2Signed_SelectedIndexChanged">
                                                <asp:ListItem Text="Select One .." Value="-99999" />
                                                <asp:ListItem Text="Signed" Value="true" />
                                                <asp:ListItem Text="Not Signed" Value="false" />
                                            </asp:DropDownList>
                                            <asp:CompareValidator ErrorMessage="drpAnchor2Signed is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpAnchor2Signed" runat="server" Operator="NotEqual" SetFocusOnError="True" />



                                        </div>

                                    </div>
                                    <div class="form-group has-info" runat="server">
                                        <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                            Anchor Approved:</label>

                                        <div class="col-xs-12 col-sm-5">
                                            <asp:DropDownList ID="drpAnchor2Approved" runat="server">
                                                <asp:ListItem Text="Select One .." Value="-99999" />
                                                <asp:ListItem Text="Approved" Value="true" />
                                                <asp:ListItem Text="Not Approved" Value="false" />
                                            </asp:DropDownList>
                                            <asp:CompareValidator ErrorMessage="drpAnchor2Approved is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpAnchor2Approved" runat="server" Operator="NotEqual" SetFocusOnError="True" />



                                        </div>

                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group has-info" runat="server">
                                <label style="font-weight: bold" for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">
                                    Date:</label><span style="color: red">*</span>

                                <div class="col-xs-12 col-sm-5">
                                    <asp:TextBox runat="server" ID="txtAnchor2SignedDate" />
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd"
                                        Enabled="True" TargetControlID="txtAnchor2SignedDate">
                                    </asp:CalendarExtender>
                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="widget-box">
        <div class="widget-header">
            <h4 class="widget-title" id="H3" runat="server">Comments & Document Upload
            </h4>

            <div class="widget-toolbar">
                <a href="#" data-action="collapse">
                    <i class="ace-icon fa fa-chevron-up"></i>
                </a>

                <a href="#" data-action="close">
                    <i class="ace-icon fa fa-times"></i>
                </a>
            </div>
        </div>
        <div class="widget-body">
            <div class="widget-main">
                <asp:UCAttachDocuments runat="server" ID="UCAttachDocuments" />
                <hr />
                <div class="form-group has-info">
                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right">Comments / Special Instructions:</label>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="col-xs-12 col-sm-5">
                                <span class="block input-icon input-icon-right">
                                    <asp:TextBox runat="server" ID="txtNotes" Width="99%" Rows="8" Height="300px" TextMode="MultiLine" onkeyup="CountChars(this, 5000)"></asp:TextBox>


                                    <i class="ace-icon fa fa-info-circle"></i>
                                </span>


                                <asp:LinkButton ID="btnAddNote" runat="server" class="btn btn-xs btn-primary" OnClick="AddNote">
												<i class="ace-icon fa fa-save bigger-110"></i>	Save Comment
												
                                </asp:LinkButton>

                                <asp:TreeView runat="server" ID="treeNotes" AutoGenerateDataBindings="False" BorderColor="Gray" BorderStyle="None" BorderWidth="1px"
                                    OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ExpandImageUrl="../Images/Collapse.png" CollapseImageUrl="../Images/Minus.png"
                                    ExpandDepth="0" Font-Bold="True" Font-Size="Medium" ForeColor="Black" NodeWrap="True">

                                    <LeafNodeStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="ChildTreeNode" Font-Bold="False" />
                                    <NodeStyle BorderColor="Silver" BorderStyle="None" BorderWidth="1px" Font-Bold="False" />
                                    <ParentNodeStyle BorderColor="Black" BorderStyle="Solid" Font-Bold="False" Font-Size="Medium" ForeColor="Black" />
                                    <RootNodeStyle BorderColor="Black" ChildNodesPadding="5px" CssClass="TreeNode" Font-Bold="True" Font-Overline="False" Font-Size="Small" NodeSpacing="5px" />
                                    <SelectedNodeStyle BorderColor="Black" Font-Bold="False" />
                                </asp:TreeView>


                                <asp:GridView runat="server" CssClass="documents" ID="gvWorkInfo" GridLines="Horizontal"
                                    AutoGenerateColumns="False" OnRowDataBound="RowDataBound" DataKeyNames="WorkInfoId">
                                    <Columns>
                                        <asp:BoundField DataField="CreatedBy" HeaderText="Added By" />
                                        <asp:BoundField DataField="Timestamp" HeaderText="Date Created" DataFormatString="{0:yyyy-MM-dd hh:mm}" />
                                        <asp:TemplateField HeaderText="Notes" SortExpression="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNotes" runat="server" Text='<%# Utils.getshortString(Eval("Notes").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAttach" runat="server" Text="Documents" OnClick="ViewDocuments"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>


            </div>
        </div>
    </div>

    


            <asp:Panel runat="server" ID="pnlAllocate" Width="50%">
                <table>
                    <tr>
                        <td style="visibility: hidden">
                            <asp:Button runat="server" ID="btnViewCase" Text="View Case Details" />
                            <asp:Button runat="server" ID="btnExit" Text="View Case Details" />
                        </td>
                    </tr>
                </table>
                <table width="100%" style="background-color: white">
                    <tr style="background-color: grey">
                        <td style="max-height: 20px">
                            <table width="100%" border="0" class="panel-heading">
                                <tr>
                                    <td style="color: white;background-color:black">Re-work Report</td>
                                    <td align="right"  style="color: white;background-color:black">
                                        <asp:ImageButton runat="server" ImageUrl="~/Images/Close_Box_Red.png" ID="btnClose" CausesValidation="false" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>

                                        <SCS:Toolbar ID="Toolbar2" runat="server" OnButtonClicked="Toolbar2_ButtonClicked"
                                            EnableClientApi="False" CssClass="toolbar" Width="99%">
                                            <Items>
                                                <SCS:ToolbarButton CausesValidation="True" CommandName="Submit" Text="Submit" />

                                            </Items>
                                            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                                                CssClassDisabled="button_disabled"></ButtonCssClasses>
                                        </SCS:Toolbar>
                                        <br />
                                        <br />
                                        <div class="pageBody">
                                            <table class="inc-details">

                                                <tr>
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td class="inc-details-label">Comment
                                                    </td>
                                                    <td style="padding-left: 4px;">
                                                        <asp:TextBox runat="server" ID="txtSummary" Width="800px"  MaxLength="250"  ReadOnly="false"></asp:TextBox>

                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                    </td>
                                </tr>

                            </table>

                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:ModalPopupExtender ID="mpAllocate" runat="server" PopupControlID="pnlAllocate" TargetControlID="btnViewCase" RepositionMode="RepositionOnWindowResizeAndScroll"
                Drag="True"
                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>




    <script>
        $(function () {
            $("textarea").htmlarea();

        });
        setProperties();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {

                setProperties();
                ////if (sender._postBackSettings.panelsToUpdate != null) {

                ////}
            });
        };
        function setProperties() {



            $(document).ready(function () {
                // $('[data-toggle="tooltip"]').tooltip();


                //var textarea = document.getElementById('txtReasonForVariance');
                //sceditor.create(textarea, {
                //    format: 'bbcode',
                //    icons: 'monocons',
                //    style: 'sceditor-3.1.1/minified/themes/content/office.min.css'
                //});
                //var textarea = document.getElementById('txtMitigation');
                //sceditor.create(textarea, {
                //    format: 'bbcode',
                //    icons: 'monocons',
                //    style: 'sceditor-3.1.1/minified/themes/content/office.min.css'
                //});
                //var textarea = document.getElementById('txtCommentOnPerformance');
                //sceditor.create(textarea, {
                //    format: 'bbcode',
                //    icons: 'monocons',
                //    style: 'sceditor-3.1.1/minified/themes/content/office.min.css'
                //});
                //var textarea = document.getElementById('txtIfNotCalculatedAccordingToTID');
                //sceditor.create(textarea, {
                //    format: 'bbcode',
                //    icons: 'monocons',
                //    style: 'sceditor-3.1.1/minified/themes/content/office.min.css'
                //});
                //var textarea = document.getElementById('txtEvidence');
                //sceditor.create(textarea, {
                //    format: 'bbcode',
                //    icons: 'monocons',
                //    style: 'sceditor-3.1.1/minified/themes/content/office.min.css'
                //});
            });
        }


    </script>

    <script src="../assets/js/jquery-ui.custom.min.js"></script>
    <script src="../assets/js/jquery.ui.touch-punch.min.js"></script>
    <script src="../assets/js/chosen.jquery.min.js"></script>
    <script src="../assets/js/spinbox.min.js"></script>
    <script src="../assets/js/bootstrap-datepicker.min.js"></script>
    <script src="../assets/js/bootstrap-timepicker.min.js"></script>
    <script src="../assets/js/moment.min.js"></script>
    <script src="../assets/js/daterangepicker.min.js"></script>
    <script src="../assets/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/js/bootstrap-colorpicker.min.js"></script>
    <script src="../assets/js/jquery.knob.min.js"></script>
    <script src="../assets/js/autosize.min.js"></script>
    <script src="../assets/js/jquery.inputlimiter.min.js"></script>
    <script src="../assets/js/jquery.maskedinput.min.js"></script>
    <script src="../assets/js/bootstrap-tag.min.js"></script>


    <!-- ace scripts -->
    <script src="../assets/js/ace-elements.min.js"></script>
    <script src="../assets/js/ace.min.js"></script>
      <script type="text/javascript">
         

          //$('#Toolbar1_item_0').click(function () {
          //    if (Page_ClientValidate()) {
          //        $('#Toolbar1').hide();
          //    }
                                    
            
          //});
          $('#Toolbar1_item_1').click(function () {
              if (Page_ClientValidate()) {
                  $('#Toolbar1').hide();
              }
             

          });
          $('#Toolbar1_item_2').click(function () {
              if (Page_ClientValidate()) {
                  $('#Toolbar1').hide();
              }
            
          });
          $('#Toolbar1_item_3').click(function () {
              if (Page_ClientValidate()) {
                  $('#Toolbar1').hide();
              }
          });
          $('#Toolbar1_item_4').click(function () {
              if (Page_ClientValidate()) {
                  $('#Toolbar1').hide();
              }
          });
          $('#Toolbar2_item_0').click(function () {
              if (Page_ClientValidate()) {
                  $('#MainContent_Toolbar2').hide();
              }
          });
         
      </script>
    <!-- inline scripts related to this page -->
    <script type="text/javascript">
        jQuery(function ($) {
            $('#id-disable-check').on('click', function () {
                var inp = $('#form-input-readonly').get(0);
                if (inp.hasAttribute('disabled')) {
                    inp.setAttribute('readonly', 'true');
                    inp.removeAttribute('disabled');
                    inp.value = "This text field is readonly!";
                }
                else {
                    inp.setAttribute('disabled', 'disabled');
                    inp.removeAttribute('readonly');
                    inp.value = "This text field is disabled!";
                }
            });


            if (!ace.vars['touch']) {
                $('.chosen-select').chosen({ allow_single_deselect: true });
                //resize the chosen on window resize

                $(window)
                    .off('resize.chosen')
                    .on('resize.chosen', function () {
                        $('.chosen-select').each(function () {
                            var $this = $(this);
                            $this.next().css({ 'width': $this.parent().width() });
                        })
                    }).trigger('resize.chosen');
                //resize chosen on sidebar collapse/expand
                $(document).on('settings.ace.chosen', function (e, event_name, event_val) {
                    if (event_name != 'sidebar_collapsed') return;
                    $('.chosen-select').each(function () {
                        var $this = $(this);
                        $this.next().css({ 'width': $this.parent().width() });
                    })
                });


                $('#chosen-multiple-style .btn').on('click', function (e) {
                    var target = $(this).find('input[type=radio]');
                    var which = parseInt(target.val());
                    if (which == 2) $('#form-field-select-4').addClass('tag-input-style');
                    else $('#form-field-select-4').removeClass('tag-input-style');
                });
            }


            $('[data-rel=tooltip]').tooltip({ container: 'body' });
            $('[data-rel=popover]').popover({ container: 'body' });

            autosize($('textarea[class*=autosize]'));

            $('textarea.limited').inputlimiter({
                remText: '%n character%s remaining...',
                limitText: 'max allowed : %n.'
            });

            $.mask.definitions['~'] = '[+-]';
            $('.input-mask-date').mask('99/99/9999');
            $('.input-mask-phone').mask('(999) 999-9999');
            $('.input-mask-eyescript').mask('~9.99 ~9.99 999');
            $(".input-mask-product").mask("a*-999-a999", { placeholder: " ", completed: function () { alert("You typed the following: " + this.val()); } });



            $("#input-size-slider").css('width', '200px').slider({
                value: 1,
                range: "min",
                min: 1,
                max: 8,
                step: 1,
                slide: function (event, ui) {
                    var sizing = ['', 'input-sm', 'input-lg', 'input-mini', 'input-small', 'input-medium', 'input-large', 'input-xlarge', 'input-xxlarge'];
                    var val = parseInt(ui.value);
                    $('#form-field-4').attr('class', sizing[val]).attr('placeholder', '.' + sizing[val]);
                }
            });

            $("#input-span-slider").slider({
                value: 1,
                range: "min",
                min: 1,
                max: 12,
                step: 1,
                slide: function (event, ui) {
                    var val = parseInt(ui.value);
                    $('#form-field-5').attr('class', 'col-xs-' + val).val('.col-xs-' + val);
                }
            });



            //"jQuery UI Slider"
            //range slider tooltip example
            $("#slider-range").css('height', '200px').slider({
                orientation: "vertical",
                range: true,
                min: 0,
                max: 100,
                values: [17, 67],
                slide: function (event, ui) {
                    var val = ui.values[$(ui.handle).index() - 1] + "";

                    if (!ui.handle.firstChild) {
                        $("<div class='tooltip right in' style='display:none;left:16px;top:-6px;'><div class='tooltip-arrow'></div><div class='tooltip-inner'></div></div>")
                            .prependTo(ui.handle);
                    }
                    $(ui.handle.firstChild).show().children().eq(1).text(val);
                }
            }).find('span.ui-slider-handle').on('blur', function () {
                $(this.firstChild).hide();
            });


            $("#slider-range-max").slider({
                range: "max",
                min: 1,
                max: 10,
                value: 2
            });

            $("#slider-eq > span").css({ width: '90%', 'float': 'left', margin: '15px' }).each(function () {
                // read initial values from markup and remove that
                var value = parseInt($(this).text(), 10);
                $(this).empty().slider({
                    value: value,
                    range: "min",
                    animate: true

                });
            });

            $("#slider-eq > span.ui-slider-purple").slider('disable');//disable third item


            $('#id-input-file-1 , #id-input-file-2').ace_file_input({
                no_file: 'No File ...',
                btn_choose: 'Choose',
                btn_change: 'Change',
                droppable: false,
                onchange: null,
                thumbnail: false //| true | large
                //whitelist:'gif|png|jpg|jpeg'
                //blacklist:'exe|php'
                //onchange:''
                //
            });
            //pre-show a file name, for example a previously selected file
            //$('#id-input-file-1').ace_file_input('show_file_list', ['myfile.txt'])


            $('#id-input-file-3').ace_file_input({
                style: 'well',
                btn_choose: 'Drop files here or click to choose',
                btn_change: null,
                no_icon: 'ace-icon fa fa-cloud-upload',
                droppable: true,
                thumbnail: 'small'//large | fit
                //,icon_remove:null//set null, to hide remove/reset button
                /**,before_change:function(files, dropped) {
                    //Check an example below
                    //or examples/file-upload.html
                    return true;
                }*/
                /**,before_remove : function() {
                    return true;
                }*/
                ,
                preview_error: function (filename, error_code) {
                    //name of the file that failed
                    //error_code values
                    //1 = 'FILE_LOAD_FAILED',
                    //2 = 'IMAGE_LOAD_FAILED',
                    //3 = 'THUMBNAIL_FAILED'
                    //alert(error_code);
                }

            }).on('change', function () {
                //console.log($(this).data('ace_input_files'));
                //console.log($(this).data('ace_input_method'));
            });


            //$('#id-input-file-3')
            //.ace_file_input('show_file_list', [
            //{type: 'image', name: 'name of image', path: 'http://path/to/image/for/preview'},
            //{type: 'file', name: 'hello.txt'}
            //]);




            //dynamically change allowed formats by changing allowExt && allowMime function
            $('#id-file-format').removeAttr('checked').on('change', function () {
                var whitelist_ext, whitelist_mime;
                var btn_choose
                var no_icon
                if (this.checked) {
                    btn_choose = "Drop images here or click to choose";
                    no_icon = "ace-icon fa fa-picture-o";

                    whitelist_ext = ["jpeg", "jpg", "png", "gif", "bmp"];
                    whitelist_mime = ["image/jpg", "image/jpeg", "image/png", "image/gif", "image/bmp"];
                }
                else {
                    btn_choose = "Drop files here or click to choose";
                    no_icon = "ace-icon fa fa-cloud-upload";

                    whitelist_ext = null;//all extensions are acceptable
                    whitelist_mime = null;//all mimes are acceptable
                }
                var file_input = $('#id-input-file-3');
                file_input
                    .ace_file_input('update_settings',
                        {
                            'btn_choose': btn_choose,
                            'no_icon': no_icon,
                            'allowExt': whitelist_ext,
                            'allowMime': whitelist_mime
                        })
                file_input.ace_file_input('reset_input');

                file_input
                    .off('file.error.ace')
                    .on('file.error.ace', function (e, info) {
                        //console.log(info.file_count);//number of selected files
                        //console.log(info.invalid_count);//number of invalid files
                        //console.log(info.error_list);//a list of errors in the following format

                        //info.error_count['ext']
                        //info.error_count['mime']
                        //info.error_count['size']

                        //info.error_list['ext']  = [list of file names with invalid extension]
                        //info.error_list['mime'] = [list of file names with invalid mimetype]
                        //info.error_list['size'] = [list of file names with invalid size]


                        /**
                        if( !info.dropped ) {
                            //perhapse reset file field if files have been selected, and there are invalid files among them
                            //when files are dropped, only valid files will be added to our file array
                            e.preventDefault();//it will rest input
                        }
                        */


                        //if files have been selected (not dropped), you can choose to reset input
                        //because browser keeps all selected files anyway and this cannot be changed
                        //we can only reset file field to become empty again
                        //on any case you still should check files with your server side script
                        //because any arbitrary file can be uploaded by user and it's not safe to rely on browser-side measures
                    });


                /**
                file_input
                .off('file.preview.ace')
                .on('file.preview.ace', function(e, info) {
                    console.log(info.file.width);
                    console.log(info.file.height);
                    e.preventDefault();//to prevent preview
                });
                */

            });

            $('#spinner1').ace_spinner({ value: 0, min: 0, max: 200, step: 10, btn_up_class: 'btn-info', btn_down_class: 'btn-info' })
                .closest('.ace-spinner')
                .on('changed.fu.spinbox', function () {
                    //console.log($('#spinner1').val())
                });
            $('#spinner2').ace_spinner({ value: 0, min: 0, max: 10000, step: 100, touch_spinner: true, icon_up: 'ace-icon fa fa-caret-up bigger-110', icon_down: 'ace-icon fa fa-caret-down bigger-110' });
            $('#spinner3').ace_spinner({ value: 0, min: -100, max: 100, step: 10, on_sides: true, icon_up: 'ace-icon fa fa-plus bigger-110', icon_down: 'ace-icon fa fa-minus bigger-110', btn_up_class: 'btn-success', btn_down_class: 'btn-danger' });
            $('#spinner4').ace_spinner({ value: 0, min: -100, max: 100, step: 10, on_sides: true, icon_up: 'ace-icon fa fa-plus', icon_down: 'ace-icon fa fa-minus', btn_up_class: 'btn-purple', btn_down_class: 'btn-purple' });

            //$('#spinner1').ace_spinner('disable').ace_spinner('value', 11);
            //or
            //$('#spinner1').closest('.ace-spinner').spinner('disable').spinner('enable').spinner('value', 11);//disable, enable or change value
            //$('#spinner1').closest('.ace-spinner').spinner('value', 0);//reset to 0


            //datepicker plugin
            //link
            $('.date-picker').datepicker({
                autoclose: true,
                todayHighlight: true
            })
                //show datepicker when clicking on the icon
                .next().on(ace.click_event, function () {
                    $(this).prev().focus();
                });

            //or change it into a date range picker
            $('.input-daterange').datepicker({ autoclose: true });


            //to translate the daterange picker, please copy the "examples/daterange-fr.js" contents here before initialization
            $('input[name=date-range-picker]').daterangepicker({
                'applyClass': 'btn-sm btn-success',
                'cancelClass': 'btn-sm btn-default',
                locale: {
                    applyLabel: 'Apply',
                    cancelLabel: 'Cancel',
                }
            })
                .prev().on(ace.click_event, function () {
                    $(this).next().focus();
                });


            $('#timepicker1').timepicker({
                minuteStep: 1,
                showSeconds: true,
                showMeridian: false,
                disableFocus: true,
                icons: {
                    up: 'fa fa-chevron-up',
                    down: 'fa fa-chevron-down'
                }
            }).on('focus', function () {
                $('#timepicker1').timepicker('showWidget');
            }).next().on(ace.click_event, function () {
                $(this).prev().focus();
            });




            if (!ace.vars['old_ie']) $('#date-timepicker1').datetimepicker({
                //format: 'MM/DD/YYYY h:mm:ss A',//use this option to display seconds
                icons: {
                    time: 'fa fa-clock-o',
                    date: 'fa fa-calendar',
                    up: 'fa fa-chevron-up',
                    down: 'fa fa-chevron-down',
                    previous: 'fa fa-chevron-left',
                    next: 'fa fa-chevron-right',
                    today: 'fa fa-arrows ',
                    clear: 'fa fa-trash',
                    close: 'fa fa-times'
                }
            }).next().on(ace.click_event, function () {
                $(this).prev().focus();
            });


            $('#colorpicker1').colorpicker();
            //$('.colorpicker').last().css('z-index', 2000);//if colorpicker is inside a modal, its z-index should be higher than modal'safe

            $('#simple-colorpicker-1').ace_colorpicker();
            //$('#simple-colorpicker-1').ace_colorpicker('pick', 2);//select 2nd color
            //$('#simple-colorpicker-1').ace_colorpicker('pick', '#fbe983');//select #fbe983 color
            //var picker = $('#simple-colorpicker-1').data('ace_colorpicker')
            //picker.pick('red', true);//insert the color if it doesn't exist


            $(".knob").knob();


            var tag_input = $('#form-field-tags');
            try {
                tag_input.tag(
                    {
                        placeholder: tag_input.attr('placeholder'),
                        //enable typeahead by specifying the source array
                        source: ace.vars['US_STATES'],//defined in ace.js >> ace.enable_search_ahead
                        /**
                        //or fetch data from database, fetch those that match "query"
                        source: function(query, process) {
                          $.ajax({url: 'remote_source.php?q='+encodeURIComponent(query)})
                          .done(function(result_items){
                            process(result_items);
                          });
                        }
                        */
                    }
                )

                //programmatically add/remove a tag
                var $tag_obj = $('#form-field-tags').data('tag');
                $tag_obj.add('Programmatically Added');

                var index = $tag_obj.inValues('some tag');
                $tag_obj.remove(index);
            }
            catch (e) {
                //display a textarea for old IE, because it doesn't support this plugin or another one I tried!
                tag_input.after('<textarea id="' + tag_input.attr('id') + '" name="' + tag_input.attr('name') + '" rows="3">' + tag_input.val() + '</textarea>').remove();
                //autosize($('#form-field-tags'));
            }


            /////////
            $('#modal-form input[type=file]').ace_file_input({
                style: 'well',
                btn_choose: 'Drop files here or click to choose',
                btn_change: null,
                no_icon: 'ace-icon fa fa-cloud-upload',
                droppable: true,
                thumbnail: 'large'
            })

            //chosen plugin inside a modal will have a zero width because the select element is originally hidden
            //and its width cannot be determined.
            //so we set the width after modal is show
            $('#modal-form').on('shown.bs.modal', function () {
                if (!ace.vars['touch']) {
                    $(this).find('.chosen-container').each(function () {
                        $(this).find('a:first-child').css('width', '210px');
                        $(this).find('.chosen-drop').css('width', '210px');
                        $(this).find('.chosen-search input').css('width', '200px');
                    });
                }
            })
            /**
            //or you can activate the chosen plugin after modal is shown
            //this way select element becomes visible with dimensions and chosen works as expected
            $('#modal-form').on('shown', function () {
                $(this).find('.modal-chosen').chosen();
            })
            */



        });
    </script>

</asp:Content>
