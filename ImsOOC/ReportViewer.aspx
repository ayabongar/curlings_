<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUser.master" AutoEventWireup="true" CodeFile="ReportViewer.aspx.cs" Inherits="ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .slaabouttobeviolated {
            color: orange;
        }

        .slaviolated {
            color: red;
        }

        .slakept {
            color: green;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;">
        <tr>
         
            <td>

                <div class="panel panel-primary" style="min-height: 600px">
                    <div class="panel-heading">
                        <asp:Label Text="Reportviewer" ID="lblReport" runat="server" />
                    </div>
                    <div class="panel-body">
                        <div class="pageBody" style="min-height: 600px">

  <rsweb:ReportViewer ID="ReportViewer1" runat="server"
                                            ProcessingMode="Remote" ShowBackButton="False" ShowCredentialPrompts="False"
                                            ShowFindControls="false" ShowPageNavigationControls="False"
                                            ShowParameterPrompts="true" Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowDocumentMapButton="False" ShowExportControls="true" ShowPrintButton="False" ShowPromptAreaButton="False" ShowRefreshButton="true" ShowToolBar="true" ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                            <ServerReport ReportPath="/IMS/Dashboard" />
                                        </rsweb:ReportViewer>
                        </div>

                    </div>
                </div>

            </td>
        </tr>
    </table>
</asp:Content>

