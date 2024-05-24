<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchFilterTypes.ascx.cs" Inherits="Admin_SearchFilterTypes" %>
<%@ Register Src="~/Admin/UserSelector.ascx" TagPrefix="uc1" TagName="UserSelector" %>

<table width="100%" border="0">
    <tr runat="server" id="row_RegisteredDate" visible="False">
        <td style="vertical-align: top;width:40%" >Select date Range:
        </td>
        <td style="vertical-align: top;">
            <table>
                <tr>
                    <td >Start Date</td>
                    <td>End Date</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox runat="server" ID="txtFromDate" Height="30px" Width="300px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" ClearTime="True"
                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtFromDate"
                            TodaysDateFormat="yyyy-MM-dd">
                        </asp:CalendarExtender>

                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtToDate" Height="30px" Width="300px">
                                                 
                        </asp:TextBox>
                        <asp:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" ClearTime="True"
                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtToDate"
                            TodaysDateFormat="yyyy-MM-dd">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr runat="server" id="row_ReferenceNo" visible="False" >
        <td style="vertical-align: top;width:40%" id="lblDescription"  runat="server">Reference Number:</td>
        <td style="vertical-align: top;">
            <asp:TextBox runat="server" ID="txtRefNo" Height="30px" Width="300px"></asp:TextBox></td>
    </tr>
    <tr runat="server" id="row_user" visible="False">
        <td style="vertical-align: top;width:40%">Select User:</td>
        <td style="vertical-align: top;">
            <uc1:UserSelector runat="server" ID="UserSelector1" />
        </td>
    </tr>
</table>
