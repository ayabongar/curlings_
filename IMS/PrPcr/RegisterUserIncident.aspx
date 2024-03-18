<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUserNoUpdatePanel.master" AutoEventWireup="true" CodeFile="RegisterUserIncident.aspx.cs" Inherits="PrPcr_RegisterUserIncident" %>

<%@ Register TagPrefix="uc1" TagName="UserSelector" Src="~/Admin/UserSelector.ascx" %>
<%@ Register TagPrefix="asp" TagName="UCAttachDocuments" Src="~/Admin/UCAttachDocuments.ascx" %>

<%@ Register Src="ReqInfoProposal.ascx" TagName="ReqInfoProposal" TagPrefix="uc2" %>

<%@ Register Src="ReqInfoProposalStage2.ascx" TagName="ReqInfoProposalStage2" TagPrefix="uc3" %>

<%@ Register Src="Condonation.ascx" TagName="Condonation" TagPrefix="uc4" %>
<%@ Register Src="ExpansionVariation.ascx" TagName="ExpansionVariation" TagPrefix="uc5" %>
<%@ Register Src="Transversal.ascx" TagName="Transversal" TagPrefix="uc6" %>
<%@ Register Src="Deviation.ascx" TagName="Deviation" TagPrefix="uc7" %>

<%@ Register Src="RFQ.ascx" TagName="RFQ" TagPrefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../Scripts/boxover.js"></script>
    <script type="text/javascript" src="../Scripts/_validation.js"></script>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.3.js" type="text/javascript"></script>
    <script src="../Scripts/webservices.js" type="text/javascript"></script>
    <style type="text/css">
        .inc-details {
            width: 100%;
            padding: 15px;
        }

            .inc-details input[type="text"] {
                width: 300px;
                height: 30px;
                padding-left: 2px;
            }

            .inc-details select {
                width: 300px;
                height: 30px;
                padding-left: 2px;
            }

        .inc-details-label {
            width: 50%;
            vertical-align: top;
            margin: 5px;
            height: 35px;
        }

        fieldset {
            border: 1px solid #428BCA;
            margin: 15px;
        }

        .inc-details input[type=radio] {
            border: 1px solid black;
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

        input[type="textarea"] {
            border: 1px solid black;
        }

        .ajax__tab_xp .ajax__tab_header {
            text-transform: uppercase;
        }
    </style>

</asp:Content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading">
             <%= CurrentProcess != null ? string.Format("{0} v{1} ",CurrentProcess.Description, CurrentProcess.Version)   : string.Empty %> 
            -
            <%= CurrentIncidentDetails.ReferenceNumber %>
        </div>
        <div class="panel-body">

            <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked" EnableClientApi="False" CssClass="toolbar" Width="99%">
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Submit" Text="Save Incident Details" />
                    <SCS:ToolbarButton CausesValidation="True" CommandName="AddNotes" Text="Add Work Info" Visible="False" />
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Cancel" Text="Cancel" />
                    


                </Items>
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
            </SCS:Toolbar>


            <p>
                <table style="width: 100%;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </p>

            <asp:TabContainer runat="server" ID="tbContainer" ActiveTabIndex="7"
                Width="100%">
                <asp:TabPanel runat="server" ID="tbDetails">
                    <HeaderTemplate>
                        Admin
                        
                    </HeaderTemplate>

                    <ContentTemplate>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="ProcurementCategory" />
                               
                                
                            </Triggers>
                            <ContentTemplate>
                                <table class="inc-details">
                                    <tr>
                                        <td class="inc-details-label">System Reference Number:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox runat="server" ID="SystemReferenceNo"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Cross Reference Number:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox runat="server" ID="CrossReferenceNo" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td class="inc-details-label">Type of Request:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:DropDownList runat="server" ID="TypeofRequest" Width="300px">
                                                 <asp:ListItem Value="">Select One..</asp:ListItem>
                                                <asp:ListItem Value="Internal">Internal</asp:ListItem>
                                                 <asp:ListItem Value="External">External</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="inc-details-label">Subject:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox runat="server" ID="Subject" Width="300px" MaxLength="255" TextMode="MultiLine" Height="100px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="inc-details-label">Priority:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:DropDownList runat="server" ID="Priority" Width="300px">
                                                 <asp:ListItem Value="">Select One..</asp:ListItem>
                                                <asp:ListItem Value="Low">Low</asp:ListItem>
                                                 <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                                <asp:ListItem Value="High">High</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Division:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox runat="server" ID="Division" Width="300px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Sub Division:
                                        </td>
                                        <td style="padding-left: 4px;">
                                          
                                             <asp:TextBox runat="server" ID="SubDivision" Width="300px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Requestor Name:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox runat="server" ID="RequestorName" Width="300px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">SARS Financial Year:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox type="text" runat="server" ID="SARSFinancialYear" Width="150px" />

                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True"
                                                Enabled="True" Format="yyyy-MM-dd" TargetControlID="SARSFinancialYear"
                                                TodaysDateFormat="yyyy-MM-dd">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="inc-details-label">Procurement Category:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:DropDownList runat="server" ID="ProcurementCategory" AutoPostBack="True" OnSelectedIndexChanged="ProcurementCategory_SelectedIndexChanged">
                                                <asp:ListItem Value="">Select One..</asp:ListItem>
                                                <asp:ListItem Value="Request for Quotation (RFQ)">Request for Quotation (RFQ)</asp:ListItem>
                                                <asp:ListItem Value="Request for Information (RFI)">Request for Information (RFI)</asp:ListItem>
                                                <asp:ListItem Value="Request for Proposal (RFP)">Request for Proposal (RFP)</asp:ListItem>
                                                <asp:ListItem Value="Deviation">Deviation</asp:ListItem>
                                                <asp:ListItem Value="Expansion and Variation">Expansion and Variation</asp:ListItem>
                                                <asp:ListItem Value="Condonation">Condonation</asp:ListItem>
                                                <asp:ListItem Value="Transversal">Transversal</asp:ListItem>
                                            </asp:DropDownList>
                                       
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Senior Manager:
                                        </td>
                                        <td>
                                            <uc1:UserSelector ID="SeniorManager" runat="server" />
                                        </td>
                                    </tr>
                                  
                                    <tr>
                                        <td class="inc-details-label">Action Person:
                                        </td>
                                        <td>
                                            <uc1:UserSelector ID="ActionPerson" runat="server" />
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td class="inc-details-label">Due Date:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox type="text" runat="server" ID="DueDate" Width="150px" />

                                            <asp:CalendarExtender ID="txtIncidentDueDate_CalendarExtender" runat="server" ClearTime="True"
                                                Enabled="True" Format="yyyy-MM-dd" TargetControlID="DueDate"
                                                TodaysDateFormat="yyyy-MM-dd">
                                            </asp:CalendarExtender>

                                        </td>
                                    </tr>
                                      <tr>
        <td colspan="2">
            Comment <br/>
            <asp:TextBox runat="server" ID="AdminNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
        </td>
    </tr>

                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </ContentTemplate>

                </asp:TabPanel>
                  <asp:TabPanel runat="server" ID="TenderStage1Preparation" Visible="False">
                    <HeaderTemplate>
                        Tender STAGE 1 Preapration
                    </HeaderTemplate>
                      <ContentTemplate>

            <uc2:ReqInfoProposal ID="ReqInfoProposal1" runat="server" />
                        </ContentTemplate>
                      </asp:TabPanel>
                <asp:TabPanel runat="server" ID="TenderStage2Preparation"  Visible="False">
                    <HeaderTemplate>
                      Tender Stage 2 Tender Process
                        
                    </HeaderTemplate>

                    <ContentTemplate>

           <uc3:ReqInfoProposalStage2 ID="ReqInfoProposalStage21" runat="server" />
                        </ContentTemplate>
                      </asp:TabPanel>
                
                  <asp:TabPanel runat="server" ID="Condonation"  Visible="False">
                    <HeaderTemplate>
                        Condonation
                        
                    </HeaderTemplate>

                    <ContentTemplate>

         
                        <uc4:Condonation ID="Condonation1" runat="server"></uc4:Condonation>

         
                        </ContentTemplate>
                      </asp:TabPanel>
                 <asp:TabPanel runat="server" ID="Deviation"  Visible="False">
                    <HeaderTemplate>
                        Deviation
                        
                    </HeaderTemplate>

                    <ContentTemplate>

         
                        <uc7:Deviation ID="Deviation1" runat="server" />

         
                        </ContentTemplate>
                      </asp:TabPanel>
                 <asp:TabPanel runat="server" ID="ExpansionVariation"  Visible="False">
                    <HeaderTemplate>
                        Expansion & Variation
                        
                    </HeaderTemplate>

                    <ContentTemplate>

         
                        <uc5:ExpansionVariation ID="ExpansionVariation1" runat="server"></uc5:ExpansionVariation>

         
                        </ContentTemplate>
                      </asp:TabPanel>
                 <asp:TabPanel runat="server" ID="Transversal"  Visible="False">
                    <HeaderTemplate>
                        Transversal
                        
                    </HeaderTemplate>

                    <ContentTemplate>

         
                        <uc6:Transversal ID="Transversal1" runat="server" />

         
                        </ContentTemplate>
                      </asp:TabPanel>
                <asp:TabPanel runat="server" ID="RFQ"  Visible="False">
                    <HeaderTemplate>
                        RFQ
                        
                    </HeaderTemplate>

                    <ContentTemplate>

         
                        

         
                        <uc8:RFQ ID="RFQ1" runat="server" />

         
                        

         
                        </ContentTemplate>
                      </asp:TabPanel>
            </asp:TabContainer>

           


        </div>
    </div>
</asp:content>

