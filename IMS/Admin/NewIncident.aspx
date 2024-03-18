<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="NewIncident.aspx.cs" Inherits="Admin_NewIncident" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.51116.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Src="~/Admin/UserSelector.ascx" TagPrefix="uc1" TagName="UserSelector" %>
<%@ Register Src="~/SurveyWizard/HierarchicalLookup.ascx" TagPrefix="uc1" TagName="HierarchicalLookup" %>
<%@ Register Src="~/SurveyWizard/MatrixQuestion.ascx" TagPrefix="uc1" TagName="MatrixQuestion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/SurveyWizard/DisplaySurvey.ascx" TagPrefix="uc1" TagName="DisplaySurvey" %>
<%@ Register tagPrefix="asp" src="~/SurveyWizard/DisplaySurvey.ascx" tagName="DisplaySurvey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../Scripts/boxover.js"></script>
    <script type="text/javascript" src="../Scripts/_validation.js"></script>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript" ></script>
    <script src="../Scripts/jquery-ui-1.10.3.js" type="text/javascript"></script>
    <script src="../Scripts/webservices.js" type="text/javascript"></script>
    <style type="text/css">
        .inc-details
        {
            width: 100%;
            padding: 15px;
        }
        
        .inc-details input[type="text"]
        {
            width: 300px;
            height: 30px;
            padding-left: 2px;
        }
        
        .inc-details-label
        {
            width: 50%;
            vertical-align: top;
            margin: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-header">
        <%= CurrentProcess != null ? CurrentProcess.Description : string.Empty %> - <%= CurrentIncidentDetails.IncidentNumber %></div>
    <div class="roundeddiv">
        <fieldset>
            <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"
                EnableClientApi="False" CssClass="toolbar" Width="99%">
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Submit" Text="Save Incident Details" />
                    <SCS:ToolbarButton CausesValidation="True" CommandName="AddNotes" Text="Add Work Info" Visible="False" />
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Cancel" Text="Cancel" />
                </Items>
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                    CssClassDisabled="button_disabled"></ButtonCssClasses>
            </SCS:Toolbar>
        </fieldset>
        <fieldset>
            <asp:TabContainer runat="server" ID="tbContainer" ActiveTabIndex="0" Width="100%">
                <asp:TabPanel runat="server" ID="tbDetails">
                    <HeaderTemplate>
                        Incident Details
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table class="inc-details">
                            <tr>
                                <td class="inc-details-label">
                                    Incident Reference Number:
                                </td>
                                <td style="padding-left: 4px;">
                                    <input type="text" value="<%=CurrentIncidentDetails.IncidentNumber %>" disabled="disabled" />
                                </td>
                            </tr>
                            <tr>
                                <td class="inc-details-label">
                                    Incident Registration Date:
                                </td>
                                <td style="padding-left: 4px;">
                                    <input type="text" value="<%=CurrentIncidentDetails.Timestamp.ToString("yyyy-MM-dd HH:mm") %>" disabled="disabled" />
                                </td>
                            </tr>
                            <tr>
                                <td class="inc-details-label">
                                    Incident Status:
                                </td>
                                <td style="padding-left: 4px;">
                                    <input type="text" value="<%=CurrentIncidentDetails.IncidentStatus %>" disabled="disabled" />
                                </td>
                            </tr>
                            <tr>
                                <td class="inc-details-label">
                                    Assigned To:
                                </td>
                                <td>
                                    <uc1:UserSelector ID="UserSelector1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="inc-details-label">Incident Summary</td>
                                <td style="padding-left: 4px;"><asp:TextBox runat="server" ID="txtSummary" Width="400px" MaxLength="250"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="inc-details-label">
                                    Incident Due Date:
                                </td>
                                <td style="padding-left: 4px;">
                                    <asp:TextBox type="text" runat="server" ID="txtIncidentDueDate" Width="150px" />
                                    <asp:CalendarExtender ID="txtIncidentDueDate_CalendarExtender" runat="server" ClearTime="True"
                                        Enabled="True" Format="yyyy-MM-dd hh:mm" TargetControlID="txtIncidentDueDate"
                                        TodaysDateFormat="yyyy-MM-dd hh:mm">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tb">
                    <HeaderTemplate>
                        Work Info Details
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNotes" Width="99%" Rows="4" 
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr><td><asp:Button runat="server" ID="btnNew" Text="New" OnClick="NewNote"/>
                                    <asp:Button runat="server" ID="btnAddNote" Text="Save Work Info" OnClick="AddNote"/>
                                </td></tr>
                            <tr>
                                <td>
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
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" style="min-height: 100%;">
                    <tr>
                        <td>
                            <asp:DisplaySurvey ID="DisplaySurvey2" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
