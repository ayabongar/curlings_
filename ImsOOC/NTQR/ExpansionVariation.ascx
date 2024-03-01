<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExpansionVariation.ascx.cs" Inherits="PrPcr_ExpansionVariation" %>
<%@ Register TagPrefix="uc1" TagName="UserSelector" Src="~/Admin/UserSelector.ascx" %>
<table  class="inc-details">
    <tr>
        <td>Tender Number
        </td>
        <td>
            <asp:TextBox runat="server" ID="ExpansionVariationTenderNumber"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Responsible Person
        </td>
        <td>
            <uc1:UserSelector ID="ExpansionVariationResponsiblePerson" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Name of Vendor
        </td>
        <td>
            <asp:TextBox runat="server" ID="ExpansionVariationNameOfVendor"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Vendor Number
        </td>
        <td>
            <asp:TextBox runat="server" ID="ExpansionVariationVendorNumber"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Type
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ExpansionVariationType">
                <asp:ListItem Value="">Select One..</asp:ListItem>
                <asp:ListItem Value="Tender">Tender </asp:ListItem>
                <asp:ListItem Value="Deviation">Deviation</asp:ListItem>
                <asp:ListItem Value="RFQ">RFQ</asp:ListItem>
                <asp:ListItem Value="Transversal">Transversal</asp:ListItem>
                <asp:ListItem Value="Other">Other</asp:ListItem>

            </asp:DropDownList>

        </td>
    </tr>
    <tr>
        <td>Awarded
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="ExpansionVariationAwarded" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Date of award
        </td>
        <td>
            <asp:TextBox type="text" runat="server" ID="ExpansionVariationDateOfAward" Width="150px" />
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="ExpansionVariationDateOfAward"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>Level of Approval
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ExpansionVariationLevelOfApproval">
                <asp:ListItem Value="">Select One..</asp:ListItem>
                <asp:ListItem Value="Procurement">Procurement </asp:ListItem>
                <asp:ListItem Value="NBAC Tier 1"> NBAC Tier 1 </asp:ListItem>
                <asp:ListItem Value="NBAC Tier 2">NBAC Tier 2 </asp:ListItem>
                <asp:ListItem Value="Accounting Officer">Accounting Officer </asp:ListItem>
            </asp:DropDownList>

        </td>
    </tr>
    <tr>
        <td>Value of award
        </td>
        <td>
            <asp:TextBox runat="server" ID="ExpansionVariationValueOfAward"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Start date of award
        </td>
        <td>
            <asp:TextBox type="text" runat="server" ID="ExpansionVariationStartDateOfAward" Width="150px" />
            <asp:CalendarExtender ID="CalendarExtender2" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="ExpansionVariationStartDateOfAward"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>End date of Award
        </td>
        <td>
            <asp:TextBox type="text" runat="server" ID="ExpansionVariationEndDateOfAward" Width="150px" />
            <asp:CalendarExtender ID="CalendarExtender3" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="ExpansionVariationEndDateOfAward"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>Purchase Order Number
        </td>
        <td>
            <asp:TextBox runat="server" ID="ExpansionVariationPurchaseOrderNumber"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Outline agreement Number
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="ExpansionVariationOutlineAgreementNumber" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>

     <tr>
        <td>Award Reported to National Treasury
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="ExpansionVariationAwardReportedtoNationalTreasury" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Signed contract by SARS and Supplier
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="ExpansionVariationSignedContractBySARSAndSupplier" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:TextBox runat="server" ID="ExpansionVariationNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
</table>
