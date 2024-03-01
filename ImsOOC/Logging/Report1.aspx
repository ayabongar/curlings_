<%@ Page Language="C#" MasterPageFile="~/SiteNoUpdatePanel.master" AutoEventWireup="true" CodeFile="Report1.aspx.cs" Inherits="Logging_Report1" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.51116.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <div class="panel panel-primary">
        <div class="panel-heading">
            INCIDENT TRACKING SYSTEM ENCOUNTERED AN ERROR
        </div>
        <div class="panel-body">
            <table style="width: 100%; padding: 0px; border-collapse: collapse;">

                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2">
                                    <asp:Label Text="" runat="server" ID="lblError" ForeColor="Red" Font-Bold="true" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label-column">From Date:</td>
                                <td class="field-column">
                                    <asp:TextBox runat="server" ID="txtFrom" CssClass="dates" Height="30px" Width="250px" />
                                    <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" ClearTime="True"
                                        Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtFrom"
                                        TodaysDateFormat="yyyy-MM-dd">
                                    </asp:CalendarExtender>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="label-column">To Date:</td>
                                <td class="field-column">
                                    <asp:TextBox runat="server" ID="txtTo" CssClass="dates" Height="30px" Width="250px" />
                                    <asp:CalendarExtender ID="txtTo_CalendarExtender" runat="server" ClearTime="True"
                                        Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtTo"
                                        TodaysDateFormat="yyyy-MM-dd">
                                    </asp:CalendarExtender>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="label-column">&nbsp;</td>
                                <td class="field-column">
                                    <asp:Button ID="btnAddContact" runat="server" CssClass="buttons" Text="View Report" OnClick="ViewReport" Height="30px" Width="150px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>
        </div>
    </div>

</asp:Content>
