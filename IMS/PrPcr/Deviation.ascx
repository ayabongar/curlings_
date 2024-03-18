<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Deviation.ascx.cs" Inherits="PrPcr_Deviation" %>
<%@ Register TagPrefix="uc1" TagName="UserSelector" Src="~/Admin/UserSelector.ascx" %>
<table  class="inc-details">
    <tr>
        <td class="inc-details-label">Tender Number
        </td>
        <td class="inc-details-label">
            <asp:TextBox runat="server" ID="DeviationTenderNumber"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Responsible Person
        </td>
        <td>
            <uc1:UserSelector ID="DeviationResponsiblePerson" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Name of Vendor
        </td>
        <td>
            <asp:TextBox runat="server" ID="DeviationNameOfVendor"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Vendor Number
        </td>
        <td>
            <asp:TextBox runat="server" ID="DeviationVendorNumber"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Type
        </td>
        <td>
            <asp:DropDownList runat="server" ID="DeviationType">
                <asp:ListItem Value="">Select One..</asp:ListItem>
                <asp:ListItem Value="Case of emergency">Case of emergency </asp:ListItem>
                <asp:ListItem Value="Sole Supplier">Sole Supplier</asp:ListItem>
                <asp:ListItem Value="Impractical">Impractical</asp:ListItem>

            </asp:DropDownList>

        </td>
    </tr>
    <tr>
        <td>Awarded
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="DeviationAwarded" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Date of award
        </td>
        <td>
            <asp:TextBox type="text" runat="server" ID="DeviationDateOfAward" Width="150px" />
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="DeviationDateOfAward"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>Level of Approval
        </td>
        <td>
            <asp:DropDownList runat="server" ID="DeviationLevelOfApproval">
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
            <asp:RadioButtonList runat="server" ID="DeviationSignedMinutesOfApproval" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Value of award
        </td>
        <td>
            <asp:TextBox runat="server" ID="DeviationValueOfAward"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Start date of award
        </td>
        <td>
            <asp:TextBox type="text" runat="server" ID="DeviationStartDateOfAward" Width="150px" />
            <asp:CalendarExtender ID="CalendarExtender2" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="DeviationStartDateOfAward"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>End date of Award
        </td>
        <td>
            <asp:TextBox type="text" runat="server" ID="DeviationEndDateOfAward" Width="150px" />
            <asp:CalendarExtender ID="CalendarExtender3" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="DeviationEndDateOfAward"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>Purchase Order Number
        </td>
        <td>
            <asp:TextBox runat="server" ID="DeviationPurchaseOrderNumber"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Outline agreement Number
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="DeviationOutlineAgreementNumber" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>

    <tr>
        <td>Award Reported to National Treasury
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="DeviationAwardReportedtoNationalTreasury" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Signed contract by SARS and Supplier
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="DeviationSignedContractBySARSAndSupplier" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="DeviationNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
</table>
