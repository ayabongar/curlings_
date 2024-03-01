<%@ Page Title="IMS REPORTS" Language="C#" MasterPageFile="~/ReportsMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Reports_Default" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <div class="panel panel-primary">
        <div class="panel-heading">
            <%=Heading%>
        </div>
        <div class="panel-body" style="min-height:800px">
            <div class="pageBody" style="min-height:800px">
            <table style="width: 100%">
                <tr>
                    <td>
                        <div id="chartContainer">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server"
                                ProcessingMode="Remote" ShowBackButton="False" ShowCredentialPrompts="False"
                                ShowFindControls="False" ShowPageNavigationControls="False"
                                ShowParameterPrompts="False" Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowExportControls="False" ShowPrintButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowToolBar="False" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <ServerReport ReportPath="/IMS/Dashboard" />
                            </rsweb:ReportViewer>
                        </div>
                    </td>
                </tr>

            </table>
                </div>
        </div>
    </div>
</asp:Content>

