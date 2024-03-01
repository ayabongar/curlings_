<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUser.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="OOC_Default" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">   

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
                            OnRowDataBound="RowDataBound" PageSize="20" Width="100%"
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
                                        <asp:TextBox type="text" runat="server" ID="DateFrom"  CssClass="TextBoxes" />

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
                                        <asp:TextBox type="text" runat="server" ID="DateTo" CssClass="TextBoxes" />

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
                        MY DASHBOARD
                    </div>
                    <div class="panel-body">
                        <div class="pageBody" style="min-height: 600px">


                            <asp:TabContainer runat="server" ID="tabIncidents" ActiveTabIndex="0">
                                <asp:TabPanel runat="server" ID="tabMyIncidents" Style="min-height: 600px">
                                    <HeaderTemplate>Dashboard</HeaderTemplate>
                                    <ContentTemplate>
                                        <rsweb:ReportViewer ID="ReportViewer1" runat="server"
                                            ProcessingMode="Remote" ShowBackButton="False" ShowCredentialPrompts="False"
                                            ShowFindControls="false" ShowPageNavigationControls="False"
                                            ShowParameterPrompts="false" Width="100%" Height="1000px" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowExportControls="False" ShowPrintButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowToolBar="False" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                            <ServerReport ReportPath="/IMS/Dashboard" />
                                        </rsweb:ReportViewer>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel runat="server" ID="TabPanel1">
                                    <HeaderTemplate>Data Extract</HeaderTemplate>
                                    <ContentTemplate>

                                        <table style="width: 100%">
                                            <tr>
                                                <td colspan="2">
                                                    <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked" CssClass="toolbar" Width="99.5%">
                                                        <Items>
                                                            <SCS:ToolbarButton CausesValidation="True" CommandName="Submit" Text="Submit" />
                                                            <SCS:ToolbarButton CausesValidation="True" CommandName="Export" Text="Export To Excel" />


                                                        </Items>
                                                        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled" CssClassSelected=""></ButtonCssClasses>
                                                    </SCS:Toolbar>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>Process Name:</td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="drpProcess">
                                                     
                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Start Date:</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtDate" enableediting="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtIncidentDueDate_CalendarExtender" runat="server" ClearTime="True"
                                                        Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtDate"
                                                        TodaysDateFormat="yyyy-MM-dd">
                                                    </asp:CalendarExtender>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>End Date:</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtEndDate" enableediting="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" ClearTime="True"
                                                        Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtEndDate"
                                                        TodaysDateFormat="yyyy-MM-dd">
                                                    </asp:CalendarExtender>

                                                </td>                                               
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel runat="server" Width="1200px" ScrollBars="Both">
                                                        <asp:GridView runat="server" ID="gvReports" CssClass="documents" AllowPaging="True" OnPageIndexChanging="gvReports_PageIndexChanging" EmptyDataText="NO INCIDENTS " PageSize="30">
                                                        </asp:GridView>
                                                    </asp:Panel>

                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </div>

                    </div>
                </div>

            </td>
        </tr>
    </table>

    <asp:GridView ID="gvIncidents" runat="server" AllowPaging="True" AutoGenerateColumns="False" Visible="false"
        CssClass="documents" DataKeyNames="IncidentID,IncidentNumber,ProcessId" GridLines="Horizontal"
        OnPageIndexChanging="IncidentPageChanging"
        OnRowDataBound="IncidentRowDataBound" Width="100%" PageSize="20">
        <Columns>
            <asp:BoundField DataField="ProcessName" HeaderText="Process Name">
                <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="ReferenceNumber" HeaderText="Incident Number">
                <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Timestamp" DataFormatString="{0:yyyy-MM-dd hh:mm}" HeaderText="Registered">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>

            <asp:BoundField DataField="DueDate" DataFormatString="{0:yyyy-MM-dd hh:mm}" HeaderText="Due Date">
                <HeaderStyle VerticalAlign="Top" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="IncidentStatus" HeaderText="Status">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
        <PagerStyle HorizontalAlign="Left" />
        <SelectedRowStyle CssClass="selectedRow" />
    </asp:GridView>
</asp:Content>


