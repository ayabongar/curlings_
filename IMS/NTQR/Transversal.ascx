<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Transversal.ascx.cs" Inherits="PrPcr_Transversal" %>
<%@ Register TagPrefix="uc1" TagName="UserSelector" Src="~/Admin/UserSelector.ascx" %>
<table  class="inc-details">
    <tr>
        <td>Tender Number
        </td>
        <td>
            <asp:TextBox runat="server" ID="TransversalTenderNumber"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Responsible Person
        </td>
        <td>
            <uc1:UserSelector ID="TransversalResponsiblePerson" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Name of Vendor
        </td>
        <td>
            <asp:TextBox runat="server" ID="TransversalNameOfVendor"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Vendor Number
        </td>
        <td>
            <asp:TextBox runat="server" ID="TransversalVendorNumber"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Type
        </td>
        <td>
            <asp:DropDownList runat="server" ID="TransversalType">
                <asp:ListItem Value="">Select One..</asp:ListItem>
                <asp:ListItem Value="Transversal">Transversal</asp:ListItem>
                <asp:ListItem Value="Other state organ">Other state organ</asp:ListItem>

            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Awarded
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="TransversalAwarded" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Date of award
        </td>
        <td>
            <asp:TextBox type="text" runat="server" ID="TransversalDateOfAward" Width="150px" />
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="TransversalDateOfAward"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>Level of Approval
        </td>
        <td>
            <asp:DropDownList runat="server" ID="TransversalLevelOfApproval">
                <asp:ListItem Value="">Select One..</asp:ListItem>
                <asp:ListItem Value="Procurement">Procurement </asp:ListItem>
                <asp:ListItem Value="NBAC Tier 1"> NBAC Tier 1 </asp:ListItem>
                <asp:ListItem Value="NBAC Tier 2">NBAC Tier 2 </asp:ListItem>
                <asp:ListItem Value="Accounting Officer">Accounting Officer </asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Value of Award
        </td>
        <td>
            <asp:TextBox runat="server" ID="TransversalValueOfAward"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Start date of Award
        </td>
        <td>
            <asp:TextBox type="text" runat="server" ID="TransversalStartDateOfAward" Width="150px" />
            <asp:CalendarExtender ID="CalendarExtender2" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="TransversalStartDateOfAward"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>End date of Award
        </td>
        <td>
            <asp:TextBox type="text" runat="server" ID="TransversalEndDateOfAward" Width="150px" />
            <asp:CalendarExtender ID="CalendarExtender3" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="TransversalEndDateOfAward"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>Purchase Order Number
        </td>
        <td>
            <asp:TextBox runat="server" ID="TransversalPurchaseOrderNumber"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Outline agreement Number
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="TransversalOutlineAgreementNumber" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>

     <tr>
        <td>Award Reported to National Treasury
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="TransversalAwardReportedtoNationalTreasury" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Signed contract by SARS and Supplier
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="TransversalSignedContractBySARSAndSupplier" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td colspan="2">
            Comment <br/>
            <asp:TextBox runat="server" ID="TransversalNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
</table>
