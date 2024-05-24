<%@ Page Title="" Language="C#" MasterPageFile="~/OOC/Site.master" AutoEventWireup="true" CodeFile="ReportViewer.aspx.cs" Inherits="PrOoc_ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <asp:Label runat="server" ID="lblReferenceNumber"></asp:Label>
        </div>
        <div class="panel-body" >

            <div id="dvOneDate" class="pageBody" runat="server" visible="False">


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
                    <tr>
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
        <div id="dvTwoDates" class="pageBody"  style="min-height:3000px" runat="server" visible="true">
            
           
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"
                ProcessingMode="Remote" ShowCredentialPrompts="False"
                ShowFindControls="False"
                ShowPageNavigationControls="false"
                ShowParameterPrompts="True" Width="100%" Height="1200px" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowPromptAreaButton="False" ShowRefreshButton="True" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <ServerReport ReportPath="/ims/default"  />
            </rsweb:ReportViewer>
        </div>

        <div id="dvReportViewer" class="pageBody" runat="server" visible="true">
        </div>
    </div>

</asp:Content>

