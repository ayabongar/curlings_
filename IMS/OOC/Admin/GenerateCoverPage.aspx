<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUserNoUpdatePanel.master" AutoEventWireup="true" CodeFile="GenerateCoverPage.aspx.cs" Inherits="Admin_GenerateCoverPage" %>

<%@ Register Src="~/Admin/UserSelector.ascx" TagPrefix="uc1" TagName="UserSelector" %>
<%@ Register Src="~/SurveyWizard/HierarchicalLookup.ascx" TagPrefix="uc1" TagName="HierarchicalLookup" %>
<%@ Register Src="~/SurveyWizard/MatrixQuestion.ascx" TagPrefix="uc1" TagName="MatrixQuestion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="asp" Src="~/SurveyWizard/DisplaySurvey.ascx" TagName="DisplaySurvey" %>
<%@ Register Src="../../SurveyWizard/DisplayCoverPage.ascx" TagName="DisplayCoverPage" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../Scripts/boxover.js"></script>
    <script type="text/javascript" src="../Scripts/_validation.js"></script>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.3.js" type="text/javascript"></script>
    <script src="../Scripts/webservices.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.min.js"> </script>
    <style type="text/css">
        .inc-details {
            width: 100%;
            padding: 15px;
        }

            .inc-details input[type="text"] {
                width: 300px;
                height: 30px;
                padding-left: 2px;
            }

        .inc-details-label {
            width: 50%;
            vertical-align: top;
            margin: 5px;
        }
    </style>
    <style>
        table {
            color: black;
        }

        td {
            min-height: 35px;
        }

        input {
            border: 1px solid black;
        }

        .checkbox {
            border: 1px solid black;
            height: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <%= CurrentIncidentDetails.ReferenceNumber %>-
            <%= CurrentProcess != null ? string.Format("{0} v {1} ","OOC Office", CurrentProcess.Version)   : string.Empty %>
        </div>
        <div class="panel-body">
            <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked" EnableClientApi="False" CssClass="toolbar" Width="99.3%">
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Close" Text="Back" Visible="True" />
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Print" Text="Download" />

                </Items>
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
            </SCS:Toolbar>
            <table width="100%" border="0" bgcolor="white">
                <tr>
                    <th>Document Type 
            <asp:DropDownList runat="server" ID="drpDocumentType">
                <asp:ListItem>Select One..</asp:ListItem>
                <asp:ListItem Value="LightBlue">Information </asp:ListItem>
                <asp:ListItem Value="LightGreen">Discussion </asp:ListItem>
                <asp:ListItem Value="Red">Urgent  </asp:ListItem>
                <asp:ListItem Value="Yellow">Signature  </asp:ListItem>
                <asp:ListItem Value="Orange">Correspondence  </asp:ListItem>    </asp:DropDownList></th>
                </tr>

                <tr>
                    <td colspan="2" style="background-color: white">
                        <div id="dvMain" runat="server">
                            <div id="dvFirst" runat="server">
                                <div class="pageBody" style="border: 0px solid black; border-spacing: 10px; page-break-after: always;">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">

                                                <div id="dvheader" runat="server" style="width: 100%; background-color: #4B6C9E; height: 100px">
                                                    <div style="float: left">
                                                        <div class="panel-heading">
                                                            <strong style="font-size: large; color: white"><%= CurrentIncidentDetails.ReferenceNumber %>-
            <%= CurrentProcess != null ? string.Format("{0} v {1} ","Cover Page", CurrentProcess.Version)   : string.Empty %>
                                                            </strong>
                                                        </div>
                                                    </div>
                                                    <div style="float: right">
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">
                                                            <asp:Image runat="server" Width="250px" ID="logo" ImageUrl="~/Images/logo-sars.png" BorderStyle="None"
                                                                ImageAlign="Left" />
                                                        </asp:HyperLink>
                                                    </div>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:DisplayCoverPage ID="DisplayCoverPage1" runat="server" />
                                            </td>
                                        </tr>
                                    </table>



                                </div>

                            </div>
                        </div>
                    </td>
                </tr>


            </table>
        </div>
    </div>
</asp:Content>

