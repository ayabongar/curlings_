<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ConfigureReminders.aspx.cs" Inherits="Admin_ConfigureReminders" %>


<%@ Register Src="UserSelector.ascx" TagName="UserSelector" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/webservices.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            NOTIFICATION REMINDER CONFIGURATION
        </div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 15px; width: 100%;">
                        <tr>

                            <td style="text-align: center; vertical-align: top;">
                                <fieldset>
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Process Name:</strong>
                                            </td>
                                            <td style="padding-left: 15px;">
                                                <input type="text" style="height: 30px; width: 300px;" readonly value="<%= CurrentProcess != null ? CurrentProcess.Description : string.Empty %>"></input>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Notify Users:</strong>
                                            </td>
                                            <td style="padding-left: 15px;">
                                                <asp:RadioButtonList ID="drpNotifyUsers" runat="server">
                                                    <asp:ListItem Text="Yes" Value="true" />
                                                    <asp:ListItem Text="No" Value="false" Selected="True" />
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Escalate To Managers:</strong>
                                            </td>
                                            <td style="padding-left: 15px;">
                                                <asp:RadioButtonList ID="radEscalateToManagers" runat="server">
                                                    <asp:ListItem Text="Yes" Value="true" />
                                                    <asp:ListItem Text="No" Value="false" Selected="True" />
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Escalate To Head/Process Owners:</strong>
                                            </td>
                                            <td style="padding-left: 15px;">
                                                <asp:RadioButtonList ID="radEscalateToProcessOwner" runat="server">
                                                    <asp:ListItem Text="Yes" Value="true" />
                                                    <asp:ListItem Text="No" Value="false" Selected="True" />
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Escalate To Deputy Commissioner:</strong>
                                            </td>
                                            <td style="padding-left: 15px;">
                                                <asp:RadioButtonList ID="radEscalateToDeputyComm" runat="server">
                                                    <asp:ListItem Text="Yes" Value="true" />
                                                    <asp:ListItem Text="No" Value="false" Selected="True" />
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Deputy Commissioner Emailbox:</strong>
                                            </td>
                                            <td style="padding-left: 15px;">
                                                <asp:TextBox runat="server" ID="txtDeputyComEmail" />
                                            </td>
                                        </tr>


                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Is Producation Server:</strong>
                                            </td>
                                            <td style="padding-left: 15px;">
                                                <asp:RadioButtonList ID="radProdServer" runat="server">
                                                    <asp:ListItem Text="Yes" Value="true" />
                                                    <asp:ListItem Text="No" Value="false" Selected="True" />
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Test Emailbox :</strong>
                                            </td>
                                            <td style="padding-left: 15px;">
                                                <asp:TextBox runat="server" ID="txtTestMails" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px; padding-top: 15px;">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="buttons" OnClick="Save" Text="Submit"
                                                    Width="141px"  Height="30px" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>


