<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUserNoUpdatePanel.master" AutoEventWireup="true" CodeFile="CoverPage.aspx.cs" Inherits="CustomPages_CoverPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.min.js"> </script>
    <style>
        table {
            color: black;
        }

        td {
            min-height: 35px;
        }

        input {
            border: 1px solid black;
        }

        .checkbox {
            border: 1px solid black;
            height: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <%= CurrentIncidentDetails.ReferenceNumber %>-
            <%= CurrentProcess != null ? string.Format("{0} v {1} ","OOC Office", CurrentProcess.Version)   : string.Empty %>
        </div>
        <div class="panel-body">
            <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked" EnableClientApi="False" CssClass="toolbar" Width="99.3%">
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Close" Text="Back" Visible="True" />
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Print" Text="Print"   />

                </Items>
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
            </SCS:Toolbar>
            <table width="100%" border="0" bgcolor="white">


                <tr>
                    <th>Document Type 
            <asp:DropDownList runat="server" ID="drpDocumentType">
                <asp:ListItem>Select One..</asp:ListItem>
                <asp:ListItem Value="Blue">Information </asp:ListItem>
                <asp:ListItem Value="Green">Discussion </asp:ListItem>
                <asp:ListItem Value="Red">Urgent  </asp:ListItem>
                <asp:ListItem Value="Yellow">Signature  </asp:ListItem>
                <asp:ListItem Value="Orange">Correspondence  </asp:ListItem>
                 <asp:ListItem Value="Orange">Division  </asp:ListItem>

            </asp:DropDownList>
                    </th>
                    <th>Pages to Print
                    <asp:DropDownList runat="server" ID="drpPageToPrint">
                        <asp:ListItem Value="0">All..</asp:ListItem>
                        <asp:ListItem Value="1">1 </asp:ListItem>
                        <asp:ListItem Value="2">2 </asp:ListItem>
                        <asp:ListItem Value="3">3  </asp:ListItem>



                    </asp:DropDownList></th>
                </tr>
                <tr>
                    <td colspan="2" style="background-color: white">
                        <div id="dvMain" runat="server">
                            <div id="dvFirst" runat="server">
                                <div class="page">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">

                                                <div id="dvheader" runat="server" style="width: 100%; background-color: #4B6C9E; height: 100px">
                                                    <div style="float: left">
                                                        <div class="panel-heading">
                                                            <strong style="font-size: large; color: white">
                                                                <br />
                                                                Cover Page  </strong>
                                                        </div>
                                                    </div>
                                                    <div style="float: right">
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">
                                                            <asp:Image runat="server" Width="250px" ID="logo" ImageUrl="~/Images/logo-sars.png" BorderStyle="None"
                                                                ImageAlign="Left" />
                                                        </asp:HyperLink>
                                                    </div>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">Reference Number</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblReferenceNumber"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Date Received</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblDateRecieved"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Subject</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblSubject"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="chkDelivered" Text=""></asp:CheckBox>&nbsp;Delivered</td>

                                        </tr>
                                        <tr>
                                            <td>Document Content</td>

                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox runat="server" CssClass="checkbox" ID="txtDocContent" TextMode="MultiLine" Height="150px" Width="100%"></asp:TextBox></td>
                                        </tr>
                                        <tr>

                                            <td colspan="2">
                                                <asp:CheckBox runat="server" CssClass="checkbox" Text="" ID="chkSigneOff"></asp:CheckBox>&nbsp;All SARS Numbers have been reviewed and signed off</td>
                                        </tr>
                                        <tr>

                                            <td colspan="2">
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="chkGrammer" Text=""></asp:CheckBox>&nbsp;Document has been checked for grammar and spelling errors</td>
                                        </tr>
                                        <tr>

                                            <td colspan="2">
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="chkConflictMessages" Text=""></asp:CheckBox>&nbsp;Document has been checked for conflicting messages</td>
                                        </tr>
                                        <tr>

                                            <td colspan="2">
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="chkInfoRequested" Text=""></asp:CheckBox>&nbsp;Document addresses the information requested</td>
                                        </tr>
                                        <tr>

                                            <td colspan="2">
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="chkDisclose" Text=""></asp:CheckBox>&nbsp;Document does not disclose inappropriate / unsubstantiated facts</td>
                                        </tr>
                                        <tr>

                                            <td colspan="2">
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="chkDocSarsSTD" Text=""></asp:CheckBox>&nbsp;Document is presented in a SARS standard template being neat coherent and protraying the image SARS wants to present</td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                            <div id="dvSecond" runat="server">
                                <div class="page">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <div id="Div1" runat="server" style="width: 100%; background-color: #4B6C9E; height: 100px">
                                                    <div style="float: left">
                                                        <div class="panel-heading">
                                                            <strong style="font-size: large; color: white">
                                                                <br />
                                                                Cover Page  </strong>
                                                        </div>
                                                    </div>
                                                    <div style="float: right">
                                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Default.aspx">
                                                            <asp:Image runat="server" Width="250px" ID="Image1" ImageUrl="~/Images/logo-sars.png" BorderStyle="None"
                                                                ImageAlign="Left" />
                                                        </asp:HyperLink>
                                                    </div>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="font-size: large">
                                                <strong>Internal Memo to Commissioner</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Reference Number</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblComReferenceNo"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Subject</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblComSubject"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Nature</td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="drpNature">
                                                    <asp:ListItem Value="Travel - Local">Travel - Local</asp:ListItem>
                                                    <asp:ListItem Value="Travel - International">Travel - International</asp:ListItem>
                                                    <asp:ListItem Value="Appointment">Appointment</asp:ListItem>
                                                    <asp:ListItem Value="Dismissals">Dismissals</asp:ListItem>
                                                    <asp:ListItem Value="Request to use an external venue">Request to use an external venue</asp:ListItem>
                                                    <asp:ListItem Value="Information Only">Information Only</asp:ListItem>
                                                    <asp:ListItem Value="Decision required">Decision required</asp:ListItem>
                                                    <asp:ListItem Value="Commissioner approval required">Commissioner approval required</asp:ListItem>
                                                    <asp:ListItem Value="Request to attend">Request to attend</asp:ListItem>
                                                    <asp:ListItem Value="Deputy Minister approval required">Deputy Minister approval required</asp:ListItem>
                                                    <asp:ListItem Value="Briefing document">Briefing document</asp:ListItem>
                                                    <asp:ListItem Value="Minister approval required">Minister approval required</asp:ListItem>
                                                    <asp:ListItem Value="Rule amendments and legislation related">Rule amendments and legislation related</asp:ListItem>
                                                    <asp:ListItem Value="Comittee documentation">Comittee documentation</asp:ListItem>
                                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>

                                        <tr>
                                            <td>Priority</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblPriority"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Action Person</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblActionPerson"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="font-size: large"><strong>Commissioner's Comments</strong></td>

                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox runat="server" CssClass="checkbox" ID="txtCommissionersComment" TextMode="MultiLine" Height="150px" Width="100%"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td><strong>Comments</strong></td>
                                            <td>
                                                <asp:Label runat="server" ID="txtComment"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Sign-off for release</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblSignOff"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Notes</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblNotes"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div id="dvThird" runat="server">
                                <div class="page">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <div id="Div2" runat="server" style="width: 100%; background-color: #4B6C9E; height: 100px">
                                                    <div style="float: left">
                                                        <div class="panel-heading">
                                                         
                                                                <br />
                                                              <strong>  Cover Page  </strong>
                                                        </div>
                                                    </div>
                                                    <div style="float: right">
                                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Default.aspx">
                                                            <asp:Image runat="server" Width="250px" ID="Image2" ImageUrl="~/Images/logo-sars.png" BorderStyle="None"
                                                                ImageAlign="Left" />
                                                        </asp:HyperLink>
                                                    </div>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="font-size: large">
                                                <strong>Memo to Minister</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">Reference Number</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblMinReferenceNo"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Subject</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblMinSubject"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="chkApprovalFromDG" Text="" />&nbsp;Approval Required from DG: NT</td>

                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="ckApprovalFromDeputyMinister" Text="" />&nbsp;Approval Required from Deputy Minister</td>

                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="chkApprovalFromMinister" Text="" />&nbsp;Approval Required from Minister</td>

                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="chkAdditionalInformation" Text="" />&nbsp;Additional Information</td>

                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>





            </table>
        </div>
    </div>
</asp:Content>
