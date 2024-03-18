<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" CodeFile="TrackServiceEMails.aspx.cs" Inherits="NTQR_TrackServiceEMails" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-primary">
                <div class="panel-heading"><span id="lblHeader" runat="server"></span> </div>
                <div class="panel-body">
                    <div class="pageBody">
                             <rsweb:ReportViewer ID="ReportViewer1" runat="server"
                                            ProcessingMode="Remote" ShowBackButton="True" ShowCredentialPrompts="False"
                                            ShowFindControls="false" ShowPageNavigationControls="True"
                                            ShowParameterPrompts="True" Width="100%" Height="1000px" Font-Names="Calibri" Font-Size="8pt" ShowDocumentMapButton="False" ShowExportControls="True" ShowPrintButton="True" ShowPromptAreaButton="False" ShowRefreshButton="True" ShowToolBar="True" ShowZoomControl="True" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                            <ServerReport ReportPath="/IMS/Dashboard" />
                                        </rsweb:ReportViewer>
                    </div>
                    </div>
        </div>
</asp:Content>

