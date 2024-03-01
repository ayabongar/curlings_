<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUserNoUpdatePanel.master" AutoEventWireup="true"
    CodeFile="RegisterUserIncident.aspx.cs" Inherits="Admin_RegisterUserIncident" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.51116.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Src="~/Admin/UserSelector.ascx" TagPrefix="uc1" TagName="UserSelector" %>
<%@ Register Src="~/SurveyWizard/HierarchicalLookup.ascx" TagPrefix="uc1" TagName="HierarchicalLookup" %>
<%@ Register Src="~/SurveyWizard/MatrixQuestion.ascx" TagPrefix="uc1" TagName="MatrixQuestion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="asp" Src="~/SurveyWizard/DisplaySurvey.ascx" TagName="DisplaySurvey" %>
<%@ Register Src="~/Admin/UCAttachDocuments.ascx" TagPrefix="asp" TagName="UCAttachDocuments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../Scripts/boxover.js"></script>
    <script type="text/javascript" src="../Scripts/_validation.js"></script>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.3.js" type="text/javascript"></script>
    <script src="../Scripts/webservices.js" type="text/javascript"></script>
    <style type="text/css">
        .inc-details {
            width: 100%;
            padding: 15px;
        }

            .inc-details input[type="text"] {
                width: 300px;
                height: 30px;
                padding-left: 2px;
            }

        .inc-details-label {
            width: 50%;
            vertical-align: top;
            margin: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div id="divContent" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <%= CurrentProcess != null ? string.Format("{0} v{1} ",CurrentProcess.Description, CurrentProcess.Version)   : string.Empty %> 
            -
            <%= CurrentIncidentDetails.IncidentNumber %>
            </div>
            <div class="panel-body">
                <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked" EnableClientApi="False" CssClass="toolbar" Width="99.5%">
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Submit" Text="Save Incident Details" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="SaveAndClose" Text="Save & Close" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Cancel" Text="Cancel" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Print" Text="Cover Page" Visible="False" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="AcknowledgementLetter" Text="Acknowledgement Letter" Visible="False" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="ReAssign" Text="Re-Assign" Visible="False" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="PrintScreen" Text="Print" Visible="true" />

                    </Items>
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
                </SCS:Toolbar>
                <br />
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <div class="pageBody">
                            <table class="inc-details" width="100%" border="0">

                                <tr>
                                    <td class="inc-details-label">Incident Registration Date:
                                    </td>
                                    <td style="padding-left: 4px;">
                                        <input type="text" value="<%=CurrentIncidentDetails.Timestamp.ToString("yyyy-MM-dd hh:mm") %>"
                                            disabled="disabled" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="inc-details-label">Incident Reference Number:
                                    </td>
                                    <td style="padding-left: 4px;">
                                        <input type="text" value="<%=CurrentIncidentDetails.ReferenceNumber %>" disabled="disabled" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="inc-details-label">Incident Status:
                                    </td>
                                    <td style="padding-left: 4px;">

                                        <asp:DropDownList runat="server" ID="drpStatuses" Visible="False" Width="100px">
                                            <asp:ListItem Value="0">Select One..</asp:ListItem>
                                            <asp:ListItem Value="4">Complete</asp:ListItem>
                                            <asp:ListItem Value="5">Close</asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <input type="text" value="<%=CurrentIncidentDetails.IncidentStatus %>" disabled="disabled" style="width: 197px" />

                                    </td>
                                </tr>
                                <tr>
                                    <td class="inc-details-label">Incident Type:
                                    </td>
                                    <td style="padding-left: 4px;">

                                        <asp:DropDownList runat="server" ID="drpRoles" Width="100px">                                            
                                        </asp:DropDownList>                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="inc-details-label" style="font-weight: bold">Incident SLA Due Date:
                                    </td>
                                    <td style="padding-left: 4px;">
                                        <asp:TextBox type="text" runat="server" ID="txtIncidentDueDate" Width="150px" />

                                        <asp:CalendarExtender ID="txtIncidentDueDate_CalendarExtender" runat="server" ClearTime="True"
                                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtIncidentDueDate"
                                            TodaysDateFormat="yyyy-MM-dd">
                                        </asp:CalendarExtender>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="inc-details-label" runat="server" id="tdAssignedTo" style="font-weight: bold">Primary Actioned Person:
                                    </td>
                                    <td>
                                        <uc1:UserSelector ID="AssignedToSID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="inc-details-label" runat="server" id="td1">Secondary Actioned Person:
                                    </td>
                                    <td>
                                        <uc1:UserSelector ID="SecAssignedToSID" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="pageBody">
                            <table width="100%">

                                <tr>
                                    <td style="padding-left: 4px;">
                                        <asp:DisplaySurvey ID="DisplaySurvey2" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <table>

                                <tr style="visibility: hidden">
                                    <td class="inc-details-label">Cross Reference Number:
                                    </td>
                                    <td style="padding-left: 4px;">
                                        <asp:TextBox runat="server" ID="txtCrossReferenceNo" Width="300px" MaxLength="10"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="pageBody">
                    <%--  <h7> <b>Work Info Details</b></h7>--%>
                    <br />
                    <table style="width: 100%;">

                        <tr>
                            <td width="50%" style="padding-left: 20px">Comments / Special Instructions</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtNotes" Width="99%" Rows="4" Height="200px" TextMode="MultiLine" onkeyup="CountChars(this, 5000)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button runat="server" ID="btnNew" Text="New" OnClick="NewNote" Visible="False" />
                                <asp:Button runat="server" ID="btnAddNote" Text="Save" OnClick="AddNote" />
                            </td>
                        </tr>
                        <tr>
                            <th colspan="2">Comments:</th>
                        </tr>
                        <tr style="visibility: hidden">
                            <td class="inc-details-label" style="width: 50%">Compiler:
                            </td>
                            <td style="padding-left: 6px;">
                                <input type="text" id="txtCreatedBy" runat="server" disabled="disabled" />
                            </td>
                        </tr>
                        <tr style="visibility: hidden">
                            <td class="inc-details-label" style="width: 50%">System Reference Number:
                            </td>
                            <td style="padding-left: 6px;">
                                <input type="text" value="<%=CurrentIncidentDetails.IncidentNumber %>" disabled="disabled" />
                            </td>
                        </tr>


                        <tr>
                            <td colspan="2" align="left">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TreeView runat="server" ID="treeNotes" AutoGenerateDataBindings="False" BorderColor="Gray" BorderStyle="None" BorderWidth="1px"
                                            OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ExpandImageUrl="../Images/Collapse.png" CollapseImageUrl="../Images/Minus.png"
                                            ExpandDepth="0" Font-Bold="True" Font-Size="Medium" ForeColor="Black" NodeWrap="True">

                                            <LeafNodeStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="ChildTreeNode" Font-Bold="False" />
                                            <NodeStyle BorderColor="Silver" BorderStyle="None" BorderWidth="1px" Font-Bold="False" />
                                            <ParentNodeStyle BorderColor="Black" BorderStyle="Solid" Font-Bold="False" Font-Size="Medium" ForeColor="Black" />
                                            <RootNodeStyle BorderColor="Black" ChildNodesPadding="5px" CssClass="TreeNode" Font-Bold="True" Font-Overline="False" Font-Size="Small" NodeSpacing="5px" />
                                            <SelectedNodeStyle BorderColor="Black" Font-Bold="False" />
                                        </asp:TreeView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                            </td>
                        </tr>
                    </table>
                </div>


                <div class="pageBody">
                    <%-- <b>Upload Documents</b>--%>
                    <asp:UCAttachDocuments runat="server" ID="UCAttachDocuments" />
                </div>


            </div>
        </div>
    </div>

    <script>  


        function pageLoad(sender, e) {
            $("#MainContent_txtSummary").focusin(function () {
                var Myval = $("#MainContent_UserSelector1_txtSearchName").val();
                if (Myval != null) {
                    var actionPerson = $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23");
                    if (actionPerson != null) {
                        actionPerson.val(Myval);
                    }
                }


            });

            $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23").focusin(function () {
                var _actionPerson = $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23").val();
                var Myval = $("#MainContent_UserSelector1_txtSearchName").val();
                if (Myval != null) {
                    if (_actionPerson == '') {
                        $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23").val(Myval);

                    }
                }
            });


        }
        $(document).ready(function () {


            $("#MainContent_txtSummary").focusin(function () {

                var origin = window.location.origin;
                if (origin.search('localhost')) {

                    var Myval = $("#MainContent_UserSelector1_txtSearchName").val();
                    if (Myval != null) {
                        var actionPerson = $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23");
                        if (actionPerson != null) {
                            actionPerson.val(Myval);
                        }
                    }

                    $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23").focusin(function () {
                        var _actionPerson = $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23").val();
                        var Myval = $("#MainContent_UserSelector1_txtSearchName").val();
                        if (Myval != null) {
                            if (_actionPerson == '') {
                                $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23").val(Myval);

                            }
                        }
                    });
                }
                else if (origin.search('ptabriis01')) {

                    var Myval = $("#MainContent_UserSelector1_txtSearchName").val();
                    if (Myval != null) {
                        var actionPerson = $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23");
                        if (actionPerson != null) {
                            actionPerson.val(Myval);
                        }
                    }

                    $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23").focusin(function () {
                        var _actionPerson = $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23").val();
                        var Myval = $("#MainContent_UserSelector1_txtSearchName").val();
                        if (Myval != null) {
                            if (_actionPerson == '') {
                                $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23").val(Myval);

                            }
                        }
                    });
                }
                else if (origin.search('ptaqaapp01')) {

                    var Myval = $("#MainContent_UserSelector1_txtSearchName").val();
                    if (Myval != null) {
                        var actionPerson = $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23");
                        if (actionPerson != null) {
                            actionPerson.val(Myval);
                        }
                    }

                    $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23").focusin(function () {
                        var _actionPerson = $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23").val();
                        var Myval = $("#MainContent_UserSelector1_txtSearchName").val();
                        if (Myval != null) {
                            if (_actionPerson == '') {
                                $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_23_UserSelector1_23_txtSearchName_23").val(Myval);

                            }
                        }
                    });
                }
                // producation
                else if (origin.search('ptabriis011')) {
                    var Myval = $("#MainContent_UserSelector1_txtSearchName").val();
                    if (Myval != null) {
                        var actionPerson = $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_24_UserSelector1_24_txtSearchName_24");
                        if (actionPerson != null) {
                            actionPerson.val(Myval);
                        }
                    }



                    $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_24_UserSelector1_24_txtSearchName_24").focusin(function () {
                        var _actionPerson = $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_24_UserSelector1_24_txtSearchName_24").val();
                        var Myval = $("#MainContent_UserSelector1_txtSearchName").val();
                        if (Myval != null) {
                            if (_actionPerson == '') {
                                $("#MainContent_DisplaySurvey2_gvQuestions_QuestionDisplay1_24_UserSelector1_24_txtSearchName_24").val(Myval);

                            }
                        }
                    });
                }
            });
        });


    </script>
</asp:Content>
