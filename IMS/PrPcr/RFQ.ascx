<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RFQ.ascx.cs" Inherits="PrPcr_RFQ" %>

<table class="inc-details">
    <tr>
        <td>Type
        </td>
        <td width="50%">
            <asp:DropDownList runat="server" ID="RFQ_Type">
                <asp:ListItem Value="">Select One..</asp:ListItem>
                <asp:ListItem Value="RFQ">RFQ</asp:ListItem>
                <asp:ListItem Value="Panel">Panel</asp:ListItem>
            </asp:DropDownList>

        </td>
    </tr>
    <tr>
        <td>Signed-off Specifications/Scope of work by Business Unit </td>
        <td>
            <asp:RadioButtonList runat="server" ID="RFQ_SignedoffSpecificationsScopeofworkbyBusinessUnit" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Technical Evaluation required </td>
        <td>
            <asp:RadioButtonList runat="server" ID="RFQ_TechnicalEvaluationrequired" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Signed-off Technical Evaluatin Criteria </td>
        <td>
            <asp:RadioButtonList runat="server" ID="RFQ_SignedoffTechnicalEvaluatinCriteria" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Signed-off Pricing Template/ Bill of quatiny/ Price Analysis</td>
        <td>
            <asp:RadioButtonList runat="server" ID="RFQ_SignedoffPricingTemplateBillofquatinyPriceAnalysis" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Prepared RFQ Pack  </td>
        <td>
            <asp:RadioButtonList runat="server" ID="RFQ_PreparedRFQPack" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Briefing Session Required </td>
        <td>
            <asp:RadioButtonList runat="server" ID="RFQ_BriefingSessionRequired" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Date of briefing session </td>
        <td>
            <asp:TextBox type="text" runat="server" ID="RFQ_DateOfBriefingSession" Width="150px" />

            <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="RFQ_DateOfBriefingSession"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>
    </tr>
    <tr>
        <td>If a Briefing seession is required, prepare attendance register for sign-off by attendees, BU and Buyer </td>
        <td>
            <asp:RadioButtonList runat="server" ID="RFQ_IfaBriefingSeessionIsRequiredPrepareAttendanceRegisterForsignOffByAttendeesBUandBuyer" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Inquiries received from bidders were responded to all bidders, and printed and placed on bid file and were
        </td>
        <td>
            <asp:RadioButtonList runat="server" ID="RFQ_InquiriesReceivedFromBiddersWereRespondedToAllBiddersAndPrintedAndPlacedOnBidFileAndWere" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td>Main RFQ Documented submitted</td>
        <td>
            <asp:RadioButtonList runat="server" ID="RFQ_MainRFQDocumentedsubmitted" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>

</table>
<fieldset>
    <legend>The following SBD documents:</legend>
    <table class="inc-details" width="100%">

        <tr>
            <td>SBD 3.1 / 3.2 / 3.3/ Pricing Schedule</td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_SBD313233PricingSchedule" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>SBD 4: Declaration of Interest</td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_SBD4DeclarationofInterest" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>SBD 6.1: Preference Point Claim Form </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_SBD61PreferencePointClaimForm" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>SBD 6.2: Declaration Certificate for Local Production and Content for designated Sectors</td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_SBD62DeclarationCertificateforLocalProductionandContentfordesignatedSectors" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>SBD 8: Declaration of Bidders past Supply Chain Management Practices</td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_SBD8DeclarationofBidderspastSupplyChainManagementPractices" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>SBD 9: Certificate of independent Bid determination</td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_SBD9CertificateofindependentBiddetermination" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Government Procurement General Conditions of Contract (GCC) or Special terms and conditions</td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_GovernmentProcurementGeneralConditionsofContractGCCorSpecialtermsandconditions" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Oaths of secrecy</td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_OathsOfSecrecy" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
           <tr>
            <td colspan="2">Notes
                <br />
                <asp:TextBox runat="server" ID="RFQ_SBDDocuments" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <legend>Bid Specification/Scope of service which specify:</legend>
    <table class="inc-details" width="100%">

        <tr>
            <td>Mandatory Requirements signed by business
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_MandatoryRequirementsSignedbyBusiness" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Validity period of a RFQ clearly indicated in the SBD 3.

            </td>
            <td class="inc-details-label" valign="top">
                <asp:TextBox type="text" runat="server" ID="RFQ_ValidityPeriodOfaRfqClearlyIndicatedInTheSBD3" />

            </td>
        </tr>
          <tr>
            <td colspan="2">Notes
                <br />
                <asp:TextBox runat="server" ID="FRQ_BidSpecificationNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <legend>STAGE 1 PLANNING</legend>
    <table class="inc-details" width="100%">
        <tr>
            <td>Number of bidders invited 

            </td>
            <td class="inc-details-label" valign="top">
                <asp:TextBox type="text" runat="server" ID="RFQ_Numberofbiddersinvited" /></td>
        </tr>
        <tr>
            <td>Number of bidders invited 

            </td>
            <td class="inc-details-label" valign="top">
                <asp:TextBox type="text" runat="server" ID="RFQ_Numberofbiddersreceived" /></td>
        </tr>
        <tr>
            <td>All Quotations sourced by Buyer and received by closing date and time
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_AllQuotationssourcedbyBuyerandreceivedbyclosingdateandtime" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>RFQ extended
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_RFQextended" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Date of new closing date
            </td>
            <td class="inc-details-label" valign="top">
                <asp:TextBox type="text" runat="server" ID="RFQ_Dateofnewclosingdate" Width="150px" />

                <asp:CalendarExtender ID="CalendarExtender2" runat="server" ClearTime="True"
                    Enabled="True" Format="yyyy-MM-dd" TargetControlID="RFQ_Dateofnewclosingdate"
                    TodaysDateFormat="yyyy-MM-dd">
                </asp:CalendarExtender>

            </td>
        </tr>
        <tr>
            <td>Email for inviting bidders and receiving qoutations maintained
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_EmailRorInvitingBiddersAndReceivingQoutationsMaintained" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Signed Memo for less than three quotations
(Only if less than 3 quotes received)
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_SignedMemoRorLessThanThreeQuotationsOnlyIfLessThan3QuotesReceived" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Technical Evaluation results conducted
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RQF_TechnicalEvaluationResultsConducted" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Price Comparison Sheet (80/20)
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_PriceComparisonSheet8020" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Does the price include VAT
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_DoesThePriceIncludeVAT" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>If yes, verify VAT registration number on the TCC or VAT vendor search on SARS 
website.
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_IfYesVerifyVATRegistrationNumberOnTheTCCorVATVendorSearchonSARSWebsite" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Signed Memo for not selecting the highest scoring bidder
(Only if the first bidder is not selected)
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_SignedMemoForNotSelectingTheHighestScoringBidderOnlyFfTheFirstBidderIsNotSelected" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Valid TCC(Valid by closing date of the RFQ)
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_ValidTCCValidByClosingDateOfTheRFQ" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Valid BEE Certificate / Auditor Letter / Affidavit(Valid by closing date of the RFQ)
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_ValidBEECertificateAuditorLetterAffidavitValidbyClosingDateOfTheRFQ" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Checked against the list of restricted suppliers
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_CheckedAgainstTheListofRestrictedSuppliers" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Checked against the list of  defaulters
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_CheckedAgainstTheListOfDefaulters" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>All Barn-Owl Action plans resolved
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_AllBarnOwlActionPlansResolved" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Signed PRF
            </td>
            <td class="inc-details-label" valign="top">
                <asp:RadioButtonList runat="server" ID="RFQ_SignedPRF" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td colspan="2">Notes
                <br />
                <asp:TextBox runat="server" ID="RFQ_Stage1PlanningNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <legend>STAGE 2 RFQ PROCESS</legend>
    <table class="inc-details" width="100%">
        <tr>
            <td>Awarded
            </td>
            <td>
                <asp:RadioButtonList runat="server" ID="RFQ_lAwarded" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>Date of award
            </td>
            <td>
                <asp:TextBox type="text" runat="server" ID="RFQ_DateOfAward" Width="150px" />
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" ClearTime="True"
                    Enabled="True" Format="yyyy-MM-dd" TargetControlID="RFQ_DateOfAward"
                    TodaysDateFormat="yyyy-MM-dd">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>Level of Approval
            </td>
            <td>
                <asp:DropDownList runat="server" ID="RFQ_LevelOfApproval">
                    <asp:ListItem Value="">Select One..</asp:ListItem>
                    <asp:ListItem Value="Procurement">Procurement </asp:ListItem>
                    <asp:ListItem Value="NBAC Tier 1"> NBAC Tier 1 </asp:ListItem>
                    <asp:ListItem Value="NBAC Tier 2">NBAC Tier 2 </asp:ListItem>
                    <asp:ListItem Value="Accounting Officer">Accounting Officer </asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>SAP Vendor Number
            </td>
            <td>
                <asp:TextBox runat="server" ID="RFQ_SAPVendorNumber"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Name of Vendor
            </td>
            <td>
                <asp:TextBox runat="server" ID="RFQ_NameOfVendor"></asp:TextBox></td>
        </tr>

        <tr>
            <td>Purchase Order Number
            </td>
            <td>
                <asp:TextBox runat="server" ID="RFQ_PurchaseOrderNumber"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Outline agreement Number
            </td>
            <td>
                <asp:TextBox runat="server" ID="RFQ_OutlineAgreementNumber"></asp:TextBox>

            </td>
        </tr>
         <tr>
            <td>Award Reported to National Treasury
            </td>
            <td>
                <asp:RadioButtonList runat="server" ID="RFQ_AwardReportedtoNationalTreasury" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>

        <tr>
            <td>Signed contract by SARS and Supplier
            </td>
            <td>
                <asp:RadioButtonList runat="server" ID="RFQ_ContractBySARSAndSupplier" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td colspan="2">Notes
                <br />
                <asp:TextBox runat="server" ID="RFQ_Notes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
            </td>
        </tr>
    </table>
</fieldset>

