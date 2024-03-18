<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RenderReport.aspx.cs" Inherits="Reports_RenderReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="panel panel-primary">
        <div class="panel-heading">
            
        </div>
        <div class="panel-body">
          
            
       
        <table width="100%" runat="server" id="tblReport" Visible="true">
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server"
                               
                            ProcessingMode="Local" ShowBackButton="False" ShowCredentialPrompts="False"
                            ShowFindControls="False" ShowPageNavigationControls="true"
                            ShowParameterPrompts="true" Width="100%" Height="1200px" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowExportControls="true" ShowPrintButton="true" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowToolBar="true" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
    </div>
          </div>
</asp:Content>

