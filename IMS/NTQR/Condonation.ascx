<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Condonation.ascx.cs" Inherits="PrPcr_Condonation" %>
<%@ Register TagPrefix="uc1" TagName="UserSelector" Src="~/Admin/UserSelector.ascx" %>
<table  class="inc-details">
    <tr>
        <td>Tender Number
        </td>
        <td>
            <asp:TextBox runat="server" ID="CondonationTenderNumber"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Responsible Person
        </td>
        <td>
            <uc1:UserSelector ID="CondonationResponsiblePerson" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Name of Vendor
        </td>
        <td>
            <asp:TextBox runat="server" ID="CondonationNameOfVendor"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Vendor Number
        </td>
        <td>
            <asp:TextBox runat="server" ID="CondonationVendorNumber"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Type
        </td>
        <td>
            <asp:DropDownList runat="server" ID="CondonationType">
                <asp:ListItem Value="">Select One..</asp:ListItem>
                <asp:ListItem Value="Case of emergency">Case of emergency  </asp:ListItem>
                <asp:ListItem Value="Best interest of SARS">  Best interest of SARS</asp:ListItem>
            </asp:DropDownList>

        </td>
    </tr>
    <tr>
        <td>Awarded
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="CondonationAwarded" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Date of award
        </td>
        <td>
            <asp:TextBox type="text" runat="server" ID="CondonationDateOfAward" Width="150px" />
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="CondonationDateOfAward"
               
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>Level of Approval
        </td>
        <td>
            <asp:DropDownList runat="server" ID="CondonationLevelOfApproval">
                <asp:ListItem Value="">Select One..</asp:ListItem>
                <asp:ListItem Value="Procurement">Procurement </asp:ListItem>
                <asp:ListItem Value="NBAC Tier 1"> NBAC Tier 1 </asp:ListItem>
                <asp:ListItem Value="NBAC Tier 2">NBAC Tier 2 </asp:ListItem>
                <asp:ListItem Value="Accounting Officer">Accounting Officer </asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Signed minutes of approval
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="CondonationSignedMinutesOfApproval" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Value of award
        </td>
        <td>
            <asp:TextBox runat="server" ID="CondonationValueOfAward"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Purchase Order Number
        </td>
        <td>
            <asp:TextBox runat="server" ID="CondonationPurchaseOrderNumber"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Award Reported to National Treasury
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="CondonationAwardReportedToNationalTreasury" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td colspan="2">
            Comment <br/>
            <asp:TextBox runat="server" ID="CondonationNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>


</table>
