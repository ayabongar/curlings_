<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUserNoUpdatePanel.master" AutoEventWireup="true" CodeFile="Acknowledgement.aspx.cs" Inherits="Admin_Acknowledgement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
  
    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.min.js"> </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
            <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked" EnableClientApi="False" CssClass="toolbar" Width="100%">
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Close" Text="Back" Visible="true" />
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Print" Text="Print" />

                </Items>
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
            </SCS:Toolbar>
            <br />
            <div id="dvMain">
                <div class="page">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <div id="Div2" runat="server" style="width: 100%; background-color: #4B6C9E; height: 100px">
                                    <div style="float: left">
                                        <div class="panel-heading">
                                            <strong style="font-size: x-large;color:white">Office of the Commissioner</strong>
                                        </div>
                                    </div>
                                    <div style="float: right">
                                        <asp:Image runat="server" Width="250px" ID="Image2" ImageUrl="~/Images/logo-sars.png" BorderStyle="None"
                                            ImageAlign="Left" />
                                    </div>

                                </div>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td><strong>Office</strong></td>
                            <td rowspan="8">
                                <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" Width="100%" Height="174px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" Width="300px" ID="txtOffice"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><strong>Enquiries</strong></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" Width="300px" ID="txtEnquiry"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><strong>Telephone</strong></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" Width="300px" ID="txtTel"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><strong>Facsimile</strong></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" Width="300px" ID="txtFax"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><strong>Room</strong></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" Width="300px" ID="txtRoom"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><strong>Reference</strong></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" Width="300px" ID="txtReference"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><strong>Date</strong></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" Width="300px" ID="txtDate"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox runat="server" Width="100%" ID="txtGreeting"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2"><strong>Topic</strong></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox runat="server" Width="100%" ID="txtTopic1" TextMode="MultiLine" Height="257px"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <asp:TextBox runat="server" Width="100%" ID="txtAcknowledgement" Height="47px" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td height="5"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" Width="300px" ID="txtFooter"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <asp:TextBox runat="server" Width="100%" ID="txtFooter2"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <br />
                                ---------------------------------<br />
                                Office of the Commissioner
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
</asp:Content>

