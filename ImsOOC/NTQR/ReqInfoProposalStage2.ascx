<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReqInfoProposalStage2.ascx.cs" Inherits="PrPcr_ReqInfoProposalStage2" %>
<%@ Register src="../Controls/AddNotes.ascx" tagname="AddNotes" tagprefix="uc1" %>
<style>
    fieldset {
        border: 1px solid #428BCA;
        margin: 15px;
    }

    legend {
        padding: 0.2em 0.5em;
        border: 1px solid #428BCA;
        color: #428BCA;
        font-size: 90%;
        text-align: right;
        text-transform: uppercase;
        font-weight: bold;
    }
</style>
<fieldset>
    <legend>Post NBAC</legend>
    <table class="inc-details">
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Awarded </td>
        <td>
            <asp:RadioButtonList runat="server" ID="Awarded" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Date of award</td>
        <td>
              <asp:TextBox type="text" runat="server" ID="DateOfAward" Width="150px" />
           <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="DateOfAward"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Level of Approval</td>
        <td>
             <asp:TextBox type="text" runat="server" ID="LevelOfApproval"  />
        </td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Signed minutes of approval</td>
        <td>
            <asp:RadioButtonList runat="server" ID="SignedMinutesOfApproval" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Signed submission on tender file</td>
        <td>
            <asp:RadioButtonList runat="server" ID="SignedSubmissionOnTenderFile" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Name of Vendor</td>
        <td>
             <asp:TextBox type="text" runat="server" ID="NameOfVendor"  /></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> SAP Vendor Number</td>
        <td>
             <asp:TextBox type="text" runat="server" ID="SAPVendorNumber"  /></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Purchase Order Number</td>
        <td>
            <asp:TextBox type="text" runat="server" ID="PurchaseOrderNumber"  /></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Outline agreement Number</td>
        <td>
            <asp:TextBox type="text" runat="server" ID="OutlineAgreementNumber"  /></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Value of Award</td>
        <td>
            <asp:TextBox type="text" runat="server" ID="ValueOfAward"  />
        </td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Start date of award</td>
        <td>
              <asp:TextBox type="text" runat="server" ID="StartDateOfAward" Width="150px" />
           <asp:CalendarExtender ID="CalendarExtender2" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="StartDateOfAward"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> End date of award</td>
        <td>
              <asp:TextBox type="text" runat="server" ID="EndDateOfAward" Width="150px" />
           <asp:CalendarExtender ID="CalendarExtender3" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="EndDateOfAward"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Award Reported to National Treasury</td>
        <td>
            <asp:RadioButtonList runat="server" ID="AwardReportedToNationalTreasury" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Upload the successful bidder on SARS website.</td>
        <td>
            <asp:RadioButtonList runat="server" ID="UploadTheSuccessfulBidderOnSARSWebsite" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Letter of successful bidder sent out to the winning bidder.</td>
        <td>
            <asp:RadioButtonList runat="server" ID="LetterOfSuccessfulBidderSentOutToTheWinningBidder" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Tender award was published on the Government Tender Bulletin, and the newspaper that was used to advertise a tender.</td>
        <td>
            <asp:RadioButtonList runat="server" ID="TenderAwardWasPublishedOnTheGovernmentTenderBulletinAndTheNewspaperThatWasUsedToAdvertiseATender" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Upload Information stored in sharepoint</td>
        <td>
            <asp:RadioButtonList runat="server" ID="UploadInformationStoredInSharepoint" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top" class="inc-details-label"> Signed contract by SARS and Supplier</td>
        <td>
            <asp:RadioButtonList runat="server" ID="SignedContractBySARSAndSupplier" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
       <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="TenderStage2Notes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
</table>
</fieldset>


