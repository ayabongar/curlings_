<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUser.master" AutoEventWireup="true" CodeFile="SelectNormalUserProcess.aspx.cs" Inherits="PrOoc_SelectNormalUserProcess" %>

<%@ Register Src="~/Admin/SearchFilterTypes.ascx" TagPrefix="uc1" TagName="SearchFilterTypes" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
       <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/webservices.js"></script>
    <script src="../Scripts/boxover.js"></script>
    <style type="text/css">
        .slaabouttobeviolated {
            color: orange!important;
        }

        .slaviolated {
            color: red!important;
        }

        .slakept {
            color: green!important;
        }


        .dvhdr1 {
            background: #CCCCCC;
            font-family: verdana;
            font-size: 9px;
            font-weight: bold;
            border-top: 1px solid #000000;
            border-left: 1px solid #000000;
            border-right: 1px solid #000000;
            border-bottom: 1px solid #000000;
            padding: 2px;
            width: 400px;
        }

        .dvbdy1 {
            background: #FFFFFF;
            font-family: verdana;
            font-size: 10px;
            border-left: 1px solid #000000;
            border-right: 1px solid #000000;
            border-bottom: 1px solid #000000;
            padding: 2px;
            width: 400px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <table style="width: 100%;">
        <tr>
            <td style="vertical-align: top; width: 20%; border-right: 1px solid silver;">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        SYSTEM NAME
                    </div>
                    <div class="panel-body">
                        <asp:GridView ID="gvProcesses" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CssClass="documents" DataKeyNames="ProcessId,StatusId,Description" GridLines="None" OnPageIndexChanging="PageIndexChanging"
                            OnRowDataBound="pRowDataBound" PageSize="20" Width="100%"
                            OnSelectedIndexChanged="gvProcesses_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderText="MY SYSTEMS" SortExpression="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcessDescription" runat="server" Text='<%# String.Format("{0} v{1}", Utils.getshortString(Eval("Description").ToString()), Eval("Version")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="selectedRow" />
                        </asp:GridView>
                    </div>
                </div>


                <div class="panel panel-primary">
                    <div class="panel-heading">
                        FILTER BY
                    </div>
                    <div class="panel-body">
                        <div class="pageBody" style="width: 97%">
                            <table>
                                <tr>

                                    <td style="padding-left: 4px;">
                                        <asp:TextBox type="text" runat="server" ID="DateFrom" Width="150px" />

                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True"
                                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="DateFrom"
                                            TodaysDateFormat="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                            Enabled="true" TargetControlID="DateFrom" WatermarkCssClass="watermarked"
                                            WatermarkText="Date From">
                                        </asp:TextBoxWatermarkExtender>
                                    </td>
                                </tr>
                                <tr>

                                    <td style="padding-left: 4px;">
                                        <asp:TextBox type="text" runat="server" ID="DateTo" Width="150px" />

                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" ClearTime="True"
                                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="DateTo"
                                            TodaysDateFormat="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                            Enabled="true" TargetControlID="DateTo" WatermarkCssClass="watermarked"
                                            WatermarkText="Date To">
                                        </asp:TextBoxWatermarkExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 4px;">
                                        <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

            </td>
            <td>

                <div class="panel panel-primary" style="min-height: 600px">
                    <div class="panel-heading">
                        MY DASHBOARD DATE FROM :  (<%= _DateFrom %>   -   <%= _DateTo %>)
                    </div>
                    <div class="panel-body">
                        <div class="pageBody" style="min-height: 600px">

                            <asp:TabContainer runat="server" ID="tabIncidents" ActiveTabIndex="3">
                                <asp:TabPanel runat="server" ID="tabMyIncidents" Height="600px">
                                    <HeaderTemplate>My Dashboard</HeaderTemplate>
                                    <ContentTemplate>
                                        <rsweb:ReportViewer ID="ReportViewer1" runat="server"
                                            ProcessingMode="Remote" ShowBackButton="False" ShowCredentialPrompts="False"
                                            ShowFindControls="false" ShowPageNavigationControls="False"
                                            ShowParameterPrompts="false" Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowExportControls="False" ShowPrintButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowToolBar="False" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                            <ServerReport ReportPath="/IMS/Dashboard" />
                                        </rsweb:ReportViewer>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel runat="server" ID="tabInternal">
                                    <HeaderTemplate>Internal Registration</HeaderTemplate>
                                    <ContentTemplate>
                                        <rsweb:ReportViewer ID="rptInternal" runat="server"
                                            ProcessingMode="Remote" ShowCredentialPrompts="False"
                                            ShowFindControls="False"
                                            ShowParameterPrompts="False" Width="100%" Height="1200px" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                            <ServerReport ReportPath="/ims/default" ReportServerUrl="http://wks529dt/reportserver" />
                                        </rsweb:ReportViewer>

                                    </ContentTemplate>
                                </asp:TabPanel>

                                <asp:TabPanel runat="server" ID="tabExternal">
                                    <HeaderTemplate>External Registration</HeaderTemplate>
                                    <ContentTemplate>
                                        <rsweb:ReportViewer ID="rptExternal" runat="server"
                                            ProcessingMode="Remote" ShowCredentialPrompts="False"
                                            ShowFindControls="False"
                                            ShowParameterPrompts="False" Width="100%" Height="1200px" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                            <ServerReport ReportPath="/ims/default" ReportServerUrl="http://wks529dt/reportserver" />
                                        </rsweb:ReportViewer>

                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel runat="server" ID="tabEscalations">
                                    <HeaderTemplate>Tax Escalations</HeaderTemplate>
                                    <ContentTemplate>
                                        <rsweb:ReportViewer ID="rptEscalations" runat="server"
                                            ProcessingMode="Remote" ShowCredentialPrompts="False"
                                            ShowFindControls="False"
                                            ShowParameterPrompts="False" Width="100%" Height="1200px" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                            <ServerReport ReportPath="/ims/default" ReportServerUrl="http://wks529dt/reportserver" />
                                        </rsweb:ReportViewer>

                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel runat="server" ID="TabPanel3">
                                    <HeaderTemplate>Inflow</HeaderTemplate>
                                    <ContentTemplate>
                                        <fieldset>
                                            <legend>Internal Registration</legend>

                                            <rsweb:ReportViewer ID="rptInflowInternal" runat="server"
                                                ProcessingMode="Remote" ShowCredentialPrompts="False"
                                                ShowFindControls="False"
                                                ShowParameterPrompts="False" Width="100%" Height="500px" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                                <ServerReport ReportPath="/ims/default" ReportServerUrl="http://wks529dt/reportserver" />
                                            </rsweb:ReportViewer>
                                        </fieldset>
                                        <fieldset>
                                            <legend>External Registration</legend>
                                            <rsweb:ReportViewer ID="rptInflowExternal" runat="server"
                                                ProcessingMode="Remote" ShowCredentialPrompts="False"
                                                ShowFindControls="False"
                                                ShowParameterPrompts="False" Width="100%" Height="500px" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                                <ServerReport ReportPath="/ims/default" ReportServerUrl="http://wks529dt/reportserver" />
                                            </rsweb:ReportViewer>
                                        </fieldset>
                                            <fieldset id="fstTaxEscalation" runat="server">
                                            <legend>Tax Escalations</legend>
                                            <rsweb:ReportViewer ID="rptTaxEscalations" runat="server"
                                                ProcessingMode="Remote" ShowCredentialPrompts="False"
                                                ShowFindControls="False"
                                                ShowParameterPrompts="False" Width="100%" Height="500px" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                                <ServerReport ReportPath="/ims/default" ReportServerUrl="http://wks529dt/reportserver" />
                                            </rsweb:ReportViewer>
                                        </fieldset>

                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel runat="server" ID="TabPanel1" Visible="false">
                                    <HeaderTemplate>Data Extract</HeaderTemplate>
                                    <ContentTemplate>

                                        <table style="width: 100%">                                            
                                            <tr>
                                                <td style="width:40%">Process Name:</td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="drpProcess">
                                                    </asp:DropDownList>

                                                </td>
                                            </tr>

                                          
                                            <tr>
                                                <td>Select your filter type:
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlFilterType" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="FilterTypeChanged">
                                                        <asp:ListItem Value="0">Select Filter Type</asp:ListItem>
                                                        <asp:ListItem Value="DateRegistered">Date Registered</asp:ListItem>
                                                        <asp:ListItem Value="RegisteredBy">Registered By</asp:ListItem>
                                                        <asp:ListItem Value="AssignedTo">Assigned To</asp:ListItem>
                                                        <asp:ListItem Value="Subject">Subject</asp:ListItem>
                                                        <asp:ListItem Value="IncidentNumber">Incident Number</asp:ListItem>
                                                        <asp:ListItem Value="CompleteOrClosed">Complete Or Closed</asp:ListItem>
                                                        <asp:ListItem Value="AssignedOrWorkInProgress">Assigned Or Work In Progress</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                              <tr>
                                                <td colspan="2">
                                                    <asp:Panel runat="server" Width="100%" ScrollBars="Both">
                                                        <asp:GridView runat="server" ID="gvReports" CssClass="documents" AllowPaging="True" OnPageIndexChanging="gvReports_PageIndexChanging" EmptyDataText="NO INCIDENTS ">
                                                        </asp:GridView>
                                                    </asp:Panel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <uc1:SearchFilterTypes runat="server" ID="SearchFilterTypes1" />

                                                </td>
                                            </tr>
                                        </table>


                                        <fieldset runat="server" id="fsSearchButton" visible="False">
                                            <table style="width: 80%;">
                                                <tr>
                                                    <td style="width: 450px;"></td>
                                                    <td style="padding-left: 7px;">
                                                        <asp:Button runat="server" ID="btnSearch" Text="SEARCH" Width="200px" OnClick="SearchIncidents" />
                                                        <asp:Button runat="server" ID="btnSearchExport" Text="Export To Excel" Width="200px"  OnClick="btnExport_Click"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <asp:GridView DataKeyNames="IncidentID,Incident Status" runat="server" ID="gvIncidents"
                    CssClass="documents" AutoGenerateColumns="False"
                    OnPageIndexChanging="PageChanging" OnRowDataBound="RowDataBound"
                    OnSelectedIndexChanged="gvIncidents_SelectedIndexChanged" AllowPaging="True" PageSize="30" OnRowCommand="gvIncidents_RowCommand" >
                    <Columns>
                        <asp:BoundField DataField="System Name" HeaderText="System Name"   />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" />
                        <asp:BoundField DataField="Incident Number" HeaderText="Incident Number" />
                        <asp:BoundField DataField="Date Registered" HeaderText="Date Registered" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="Registered By" HeaderText="Registered By" />
                        <asp:BoundField DataField="Assigned To" HeaderText="Assigned To" />
                        <asp:BoundField DataField="Incident Status" HeaderText="Incident Status" />
                        <asp:BoundField DataField="Due Date" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                         <td runat="server" id="tdReAssign">
                                              <asp:Button ID="bntReAssign" runat="server" Text="Re-Assign" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="bntReAssign" Height="30px"></asp:Button>
                                             </td>
                                    </tr>
                                </table>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </div>

                    </div>
                </div>

            </td>
        </tr>
    </table>    
</asp:Content>


