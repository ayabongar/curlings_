<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReqInfoProposal.ascx.cs" Inherits="PrPcr_ReqInfoProposal" %>


    <table class="inc-details" width="100%">
    <tr>
        <td  style="width: 50%" valign="top" >Approved Procurement Plan  </td>
        <td>
            <asp:RadioButtonList runat="server" ID="ApprovedProcurementPlan" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td valign="top">Date of Kick-off meeting </td>
        <td>
            <asp:TextBox type="text" runat="server" ID="DateOfKickOffMmeeting" Width="150px" />

            <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="DateOfKickOffMmeeting"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender>

        </td>
    </tr>
    <tr>
        <td valign="top">Procurement Bid Request Form signedoff by Project Sponsor and Finance</td>
        <td>
            <asp:RadioButtonList runat="server" ID="ProcurementBidRequestFormSignedoffByProjectSponsorAndFinance" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td valign="top">Request for Corporate Legal Assistance (provide the CLS Reference Number) </td>
        <td>
            <asp:RadioButtonList runat="server" ID="RequestForCorporateLegalAssistanceProvideTheCLSReferenceNumber" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td valign="top">Confidentiality Agreement signed by all BEC members </td>
        <td>
            <asp:RadioButtonList runat="server" ID="ConfidentialityAgreementSignedByAllBECMembers" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td valign="top">BEC appointment letter signed by Project Sponsor </td>
        <td>
            <asp:RadioButtonList runat="server" ID="BECAppointmentLetterSignedByProjectSponsor" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td valign="top">Project plan signed/approved by a senior official from Business </td>
        <td>
            <asp:RadioButtonList runat="server" ID="ProjectPlanSignedApprovedByASeniorOfficialFromBusiness" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td valign="top">Completion and signedoff of the BEC kickoff checklist </td>
        <td>
            <asp:RadioButtonList runat="server" ID="CompletionAndSignedOffOfTheBecKickoffChecklist" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
   
</table>

<fieldset>
    <legend>The following SBD documents:</legend>
    <table class="inc-details" width="100%">
        <tr>
            <td style="width: 50%">SBD 1: Invitation to bid </td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="SBD1InvitationToBid" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td>SBD 2: Tax clearance Certificate Requirements</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="SBD2TaxclearanceCertificateRequirements" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td>SBD 3.1 / 3.2 / 3.3/ Pricing Schedule</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="SBD313233PricingSchedule" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td>SBD 4: Declaration of Interest</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="SBD4DeclarationofInterest" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td>SBD 5: National Industrial Participation Programme </td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="SBD5NationalIndustrialParticipationProgramme" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td>SBD 6.1: Preference Point Claim Form </td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="SBD61PreferencePointClaimForm" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td>SBD 6.2: Declaration Certificate for Local Production and Content for designated Sectors</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="SBD62DeclarationCertificateforLocalProductionandContentfordesignatedSectors" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td>SBD 8: Declaration of Bidders past Supply Chain Management Practices</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="SBD8DeclarationofBidderspastSupplyChainManagementPractices" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td>SBD 9: Certificate of independent Bid determination</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="SBD9CertificateofindependentBiddetermination" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td>Government Procurement General Conditions of Contract (GCC) or Special terms and conditions</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="GovernmentProcurementGeneralConditionsofContractGCCorSpecialtermsandconditions" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td>Oaths of secrecy</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="OathsOfSecrecy" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td>Draft SLA</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="DraftSLA" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="SBDDocumentNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
    </table>
</fieldset>
    <fieldset>
        <legend>Bid Specification/Scope of service which specify:</legend>
        <table class="inc-details" width="100%">
            <tr>
            <td style="width: 50%">Mandatory Requirements signed by business</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="MandatoryRequirementssignedbybusiness" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Technical evaluation criteria signed by business</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Technicalevaluationcriteriasignedbybusiness" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Pricing scheduled signed by evaluators</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Pricingscheduledsignedbyevaluators" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Relevant preference point system selected - Price and BEE i.e. 80/20 or 90/10</td>
            <td>
                 <asp:TextBox type="text" runat="server" ID="RelevantPreferencePointSystemSelectedPriceandBEE"  />
            
            </td>
             </tr>
             <tr>
            <td>Validity period of a tender clearly indicated in the SBD 3.</td>
            <td>
                 <asp:TextBox type="text" runat="server" ID="ValidityperiodofatenderclearlyindicatedintheSBD3"  />
            </td>
        </tr>
             <tr>
            <td>Whether or not site visit will be conducted</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Whetherornotsitevisitwillbeconducted" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Clearly indicate that the technical response and Price BEE must be submitted in two seperate envelopes</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="ClearlyindicatethatthetechnicalresponseandPriceBEEmustbesubmittedintwoseperateenvelopes" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="BidSpecificationNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
             </table>
    </fieldset>
    <fieldset>
        <legend>Tender Advertisement adequately contains:</legend>
        <table width="100%">
            <tr>
            <td style="width: 50%">Tender advertised for the minimum period of 21 working days before closing date.</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Tenderadvertisedfortheminimumperiodof21workingdaysbeforeclosingdate" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Is there a briefing session and if so, is it compulsory or not?</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Isthereabriefingsessionandifsoisitcompulsoryornot" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Are the bid documents being sold, and if so what is the selling price.</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Arethebiddocumentsbeingsoldandifsowhatisthesellingprice" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Is the Draft Advert been signed by Commodity Leader, Business and Support</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="IstheDraftAdvertbeensignedbyCommodityLeaderBusinessandSupport" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Are all the documents stored on secured folder or sharepoint</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Areallthedocumentsstoredonsecuredfolderorsharepoint" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="TenderAdvertisingAdequatelyNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
             </table>
    </fieldset>
    <fieldset>
        <legend>Invitation letter:</legend>
        <table width="100%">
            <tr>
            <td style="width: 50%">Is the invitation letter from SARS prepared and attached to tender pack?</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="IstheinvitationletterfromSARSpreparedandattachedtotenderpack" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Is the content of the invitation letter agrees to RFP document and draft advert?</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="IsthecontentoftheinvitationletteragreestoRFPdocumentanddraftadvert" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="InvitationLetterNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
             </table>
    </fieldset>
    <fieldset>
        <legend>Tender advertisement in/on:</legend>
        <table  class="inc-details">
            <tr>
            <td width="50%" >Advertise Date</td>
            <td >
                <asp:TextBox type="text" runat="server" ID="AdvertiseDate" Width="150px" />
           <asp:CalendarExtender ID="CalendarExtender2" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="AdvertiseDate"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender></td>
        </tr>
             <tr>
            <td>Closing Date</td>
            <td class="" valign="top">
                <asp:TextBox type="text" runat="server" ID="ClosingDate" Width="150px" />
           <asp:CalendarExtender ID="CalendarExtender3" runat="server" ClearTime="True"
                Enabled="True" Format="yyyy-MM-dd" TargetControlID="ClosingDate"
                TodaysDateFormat="yyyy-MM-dd">
            </asp:CalendarExtender></td>
        </tr>
             <tr>
            <td>Number of working days issued to Market</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="NumberofworkingdaysissuedtoMarket" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>E-tender (NT website)</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="EtenderNTwebsite" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Tender Government Bulletin</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="TenderGovernmentBulletin" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Public Media(Specify)</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="PublicMedia" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList><asp:TextBox type="text" runat="server" ID="PublicMediaspecify" Width="150px" />
               
                 
            </td>
        </tr>
            
             <tr>
            <td>CIDB Website</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="CIDBWebsite" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>SARS Website</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="SARSWebsite" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Briefing session agenda and briefing presentation in file</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Briefingsessionagendaandbriefingpresentationinfile" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Compulsory briefing attendance register signed by attendees</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Compulsorybriefingattendanceregistersignedbyattendees" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
           
             <tr>
            <td>Questions and answers for bidders were printed, placed in the file and uploaded on SARS website</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="QuestionsandanswersforbidderswereprintedplacedinthefileanduploadedonSARSwebsite" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="TenderAdvertisingInOnNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Tender closing</legend>
        <table width="100%" class="inc-details">
            <tr>
            <td style="width: 50%">Total number of bids received by closing date and time </td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Totalnumberofbidsreceivedbyclosingdateandtime" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Signed Bid Opening Register by tender office and Governance member</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="SignedBidOpeningRegisterbytenderofficeandGovernancemember" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Late tenders</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Latetenders" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>List of bidders published on SARS website</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="ListofbidderspublishedonSARSwebsite" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="TenderClosingNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
             </table>
    </fieldset>
    <fieldset>
        <legend>Pre-qualification</legend>
        <table width="100%" class="inc-details">
            <tr>
            <td style="width: 50%">Pre-qualification signed by tender office</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Prequalificationsignedbytenderoffice" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Mandatory evaluation signed off by evaluators</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Mandatoryevaluationsignedoffbyevaluators" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Declaration of interest was signed by all BEC members</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="DeclarationofinterestwassignedbyallBECmembers" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="PrequalificationNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
             </table>
    </fieldset>
    <fieldset>
        <legend>Technical Evaluation</legend>
        <table width="100%" class="inc-details">
            <tr>
            <td style="width: 50%">Technical evaluation occurred in Procurement</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="TechnicalevaluationoccurredinProcurement" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Evaluation was conducted under surveillance camera </td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Evaluationwasconductedundersurveillancecamera" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Technical evaluators scored all bids independently</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Technicalevaluatorsscoredallbidsindependently" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Score sheets were signed by all evaluators</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Scoresheetsweresignedbyallevaluators" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Anomolies resolved</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Anomoliesresolved" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Technical points applied as was published in bid document</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Technicalpointsappliedaswaspublishedinbiddocument" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Bidders who did not attain minimum score on technical were eliminated</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Bidderswhodidnotattainminimumscoreontechnicalwereeliminated" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Governance reviewed technical scores</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Governancereviewedtechnicalscores" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="TechnicalEvalutationNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
             </table>
    </fieldset>
    <fieldset>
        <legend>Pricing Evaluation:</legend>
        <table width="100%" class="inc-details">
            <tr>
            <td style="width: 50%">Pricing signed off by evaluators</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Pricingsignedoffbyevaluators" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Pricing points applied as was published in bid document</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Pricingpointsappliedaswaspublishedinbiddocument" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Bidders prices reviewed by Governance</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="BidderspricesreviewedbyGovernance" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="PricingEvalutationNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
             </table>
    </fieldset>
    <fieldset>
        <legend>BEE Evaluation:</legend>
        <table width="100%" class="inc-details">
            <tr>
            <td style="width: 50%">BEE points Signed off by evaluators</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="BEEpointsSignedoffbyevaluators" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>BEE scores reviewed by Governance</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="BEEscoresreviewedbyGovernance" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="BEEEvaluationNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
             </table>
    </fieldset>
    <fieldset>
        <legend>Financial Statement Analysis:</legend>
        <table width="100%" class="inc-details">
            <tr>
            <td style="width: 50%">Obtain the sign-off Financial Analysis Report</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="ObtainthesignoffFinancialAnalysisReport" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="FinancialStatementNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
             </table>
    </fieldset>
    <fieldset>
        <legend>Consolidation of Pricing and BEE Scores:</legend>
        <table width="100%" class="inc-details">
            <tr>
            <td style="width: 50%">Pricing and BEE scores consolidated</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="PricingandBEEscoresconsolidated" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Ranking of score was done correctly</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Rankingofscorewasdonecorrectly" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Top scorer was correctly ranked</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Topscorerwascorrectlyranked" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="ConsolidationOfPricingNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
             </table>
    </fieldset>
    <fieldset >
        <legend>Conduct final due deligence:</legend>
        <table width="100%"  class="inc-details">
            <tr>
            <td style="width: 50%">Checked against the list of restricted suppliers</td>
            <td  class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Checkedagainstthelistofrestrictedsuppliers" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Checked against the list of defaulters</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Checkedagainstthelistofdefaulters" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>All Barn-Owl Action plans resolved</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="AllBarnOwlActionplansresolved" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Obtain Procurecheck report, attach and consider</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="ObtainProcurecheckreportattachandconsider" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Recommended bidders tax affairs were checked</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Recommendedbidderstaxaffairswerechecked" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="ConductFinalDueNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
             </table>
    </fieldset>
    <fieldset>
        <legend>NBAC Report:</legend>
        <table  width="100%" class="inc-details">
            <tr>
            <td style="width: 50%">Reflects tender advertisement and closing date</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Reflectstenderadvertisementandclosingdate" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Responses received correctly captured</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Responsesreceivedcorrectlycaptured" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Evaluation criteria correctly reflected </td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Evaluationcriteriacorrectlyreflected" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Ranking of bidders was accurate</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Rankingofbidderswasaccurate" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Financial analysis report attached and considered</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Financialanalysisreportattachedandconsidered" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>GRC report attached </td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="GRCreportattached" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Recommendation of successful bidder was properly motivated</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Recommendationofsuccessfulbidderwasproperlymotivated" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Financial implication was correctly reflected inclusive of VAT</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="FinancialimplicationwascorrectlyreflectedinclusiveofVAT" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
             <tr>
            <td>Legal implication was correctly reflected</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="Legalimplicationwascorrectlyreflected" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
              <tr>
            <td>All BEC member signed the NBAC report</td>
            <td class="" valign="top">
                <asp:RadioButtonList runat="server" ID="AllBECmembersignedtheNBACreport" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
            <tr>
        <td colspan="2">
            Comment<br/>
            <asp:TextBox runat="server" ID="NBACReportNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>
        </table>
    </fieldset>




