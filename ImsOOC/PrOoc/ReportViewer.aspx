<%@ Page Title="" Language="C#" MasterPageFile="~/ReportsMasterPage.master" AutoEventWireup="true" CodeFile="ReportViewer.aspx.cs" Inherits="PrOoc_ReportViewer" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <asp:Label runat="server" ID="lblReferenceNumber"></asp:Label>
        </div>
        <div class="panel-body">
           
            <div id="dvOneDate" runat="server" Visible="False">


                <table>
                    <tr>
                        <td>Year:</td>
                        <td>
                            <asp:DropDownList ID="drpYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpYear_SelectedIndexChanged" /></td>
                    </tr>
                    <tr>
                        <td>Month:</td>
                        <td>
                            <asp:DropDownList ID="drpMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged" /></td>
                    </tr>
                    <tr >
                        <td>TAT:</td>
                        <td>
                            <asp:DropDownList ID="drDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                </table>

            </div>
        </div>
        <div id="dvTwoDates" runat="server" Visible="true">
            <table>
                <tr>
                    <td>Start Date:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtStartDate" Height="30px" Width="300px">
                                                 
                        </asp:TextBox>
                        <asp:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" ClearTime="True"
                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtStartDate"
                            TodaysDateFormat="yyyy-MM-dd">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>End Date:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEndDate" Height="30px" Width="300px">
                                                 
                        </asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True"
                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtEndDate"
                            TodaysDateFormat="yyyy-MM-dd">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr id="trType" runat="server" Visible="False">
                    <td>Registration Type:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="drpType">
                            <asp:ListItem Value="">Select One..</asp:ListItem>
                            <asp:ListItem Value="98">Internal Registration</asp:ListItem>
                         <asp:ListItem Value="97">External Registration</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
            </table>
        </div>
         <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked" EnableClientApi="False" CssClass="toolbar" Width="99%">
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Submit" Text="View Report" />
                     <SCS:ToolbarButton CausesValidation="True" CommandName="Export" Visible="False" Text="Export to Exel" />

                </Items>
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
            </SCS:Toolbar>
        <div id="dvReportViewer" runat="server" Visible="true">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server"  
            ProcessingMode="Remote" ShowCredentialPrompts="False"
            ShowFindControls="False"
            ShowParameterPrompts="False" Width="100%" Height="1200px" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <ServerReport ReportPath="/ims/default" ReportServerUrl="http://wks529dt/reportserver" />
        </rsweb:ReportViewer>
            </div>
    </div>

</asp:Content>

