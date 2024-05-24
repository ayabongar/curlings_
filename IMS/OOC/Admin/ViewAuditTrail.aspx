<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewAuditTrail.aspx.cs" Inherits="Admin_ViewAuditTrail" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                <div class="page-header">View Reports</div>
                <fieldset>

                    <asp:TabContainer runat="server" ID="tcParams">
                        <asp:TabPanel runat="server" ID="pannelParams" HeaderText="Select Dates">

                            <ContentTemplate>

                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">

                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Start Date:</strong></td>
                                                <td>
                                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="dates" Height="28px"
                                                        meta:resourcekey="txtStartDateResource1"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtStartDate"></asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>End Date:</strong></td>
                                                <td style="margin-left: 40px">
                                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="dates" Height="28px"
                                                        meta:resourcekey="txtEndDateResource1"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtEndDate"></asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td style="margin-left: 40px">
                                                    <asp:Button ID="btnDownloadAuditTrail" runat="server" Height="30px" OnClick="btnDownloadAuditTrail_Click" Text="View Audit Trail" Width="150px" />
                                                </td>
                                            </tr>
                                        </table>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnDownloadAuditTrail" />
                                    </Triggers>
                                </asp:UpdatePanel>


                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>

                </fieldset>

            </td>
        </tr>
    </table>
</asp:Content>

