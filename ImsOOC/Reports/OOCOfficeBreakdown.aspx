<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OOCOfficeBreakdown.aspx.cs" Inherits="Reports_OOCOfficeBreakdown" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style>
        .report{
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <%=Heading%>
        </div>
        <div class="panel-body" style="height:3000px;background-color:white">
            <table id="trBody" runat="server" Visible="False">
                <tr>
                    <td>Date:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtDate" enableediting="false"></asp:TextBox>
                        <asp:CalendarExtender ID="txtIncidentDueDate_CalendarExtender" runat="server" ClearTime="True"
                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtDate"
                            TodaysDateFormat="yyyy-MM-dd">
                        </asp:CalendarExtender>

                    </td>
                   
                </tr>
                <tr>
                    <td>Registration Type:</td>
                    <td><asp:DropDownList runat="server" ID="drpType">
                            <asp:ListItem Value="">Select One..</asp:ListItem>
                            <asp:ListItem Value="1">Internal - SARS Departments</asp:ListItem>
                         <asp:ListItem Value="2">External - Stakeholder Documentation</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                     <td>
                        <asp:Button runat="server" ID="btnViewReport" Text="View Report" OnClick="btnViewReport_Click" />
                    </td>
                </tr>

            </table>
            <table width="100%" runat="server" id="tblReport" Visible="true" >
                <tr>
                    <td>
                        <rsweb:ReportViewer CssClass="report"  ID="ReportViewer1" runat="server"
                            ProcessingMode="Remote" ShowBackButton="False" ShowCredentialPrompts="False"
                            ShowFindControls="False" ShowPageNavigationControls="False"
                            ShowParameterPrompts="true" Width="100%" Height="1200px" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowExportControls="true" ShowPrintButton="true" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowToolBar="true" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </div>
        
    </div>
</asp:Content>

