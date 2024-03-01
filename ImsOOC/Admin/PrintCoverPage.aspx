<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUserNoUpdatePanel.master" AutoEventWireup="true" CodeFile="PrintCoverPage.aspx.cs" Inherits="Admin_PrintCoverPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">

        <div class="panel-heading">
            Cover Page
        </div>
        <div class="panel-body" style="background-color:white!important">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"
                ProcessingMode="Remote" ShowBackButton="True" ShowCredentialPrompts="False"
                ShowFindControls="false" ShowPageNavigationControls="False"
                ShowParameterPrompts="false" Width="100%" Height="1000px" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowExportControls="True" ShowPrintButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowToolBar="True" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <ServerReport ReportPath="/IMS/Dashboard" />
            </rsweb:ReportViewer>
            <embed id="emdPDF" type="application/pdf" runat="server" height="1200px" width="100%" />
        </div>
    </div>
</asp:Content>

