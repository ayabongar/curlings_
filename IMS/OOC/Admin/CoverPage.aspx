<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUserNoUpdatePanel.master" AutoEventWireup="true" CodeFile="CoverPage.aspx.cs" Inherits="Admin_CoverPage" %>

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

        .pageBody {
            width: 90%;
            -webkit-box-shadow: 1px 0px 14px 6px #CCC;
            -moz-box-shadow: 1px 0px 14px 6px #CCC;
            box-shadow: 1px 0px 10px 4px #CCC;
            margin-top: 5px;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            border-radius: 10px;
            padding: 10px;
            margin: auto !important;
        }

        input[type=text] {
              background-color:white!important;
              -webkit-transition: all 0.30s ease-in-out!important;
              -moz-transition: all 0.30s ease-in-out!important;
              -ms-transition: all 0.30s ease-in-out!important;
              -o-transition: all 0.30s ease-in-out!important;
              outline: none!important;
              padding: 3px 0px 3px 3px!important;
              margin: 5px 1px 3px 0px;
              border: 1px solid #DDDDDD!important;

}
 
          textarea {
              background-color:white!important;
  -webkit-transition: all 0.30s ease-in-out!important;
  -moz-transition: all 0.30s ease-in-out!important;
  -ms-transition: all 0.30s ease-in-out!important;
  -o-transition: all 0.30s ease-in-out!important;
  outline: none!important;
  padding: 15px 0px 15px 15px!important;
  margin: 5px 5px 5px 0px;
  border: 1px solid #DDDDDD!important;

}
 
input[type=text]:focus, textarea:focus {
      background-color:white!important;
  box-shadow: 0 0 5px rgba(81, 203, 238, 1)!important;
  padding: 3px 0px 3px 3px!important;
  margin: 5px 1px 3px 0px!important;
  border: 1px solid rgba(81, 203, 238, 1)!important;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <%= CurrentProcess != null ? string.Format("{0} v {1} ","OOC Office", CurrentProcess.Version)   : string.Empty %>-
            <%= CurrentProcess != null ? string.Format("{0} v {1} ","OOC Office", CurrentProcess.Version)   : string.Empty %>
        </div>
        <div class="panel-body">
            <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked" EnableClientApi="False" CssClass="toolbar" Width="99.3%">
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Close" Text="Back" Visible="True" />
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Print" Text="Download" />

                </Items>
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
            </SCS:Toolbar>
            <table width="100%" border="0" bgcolor="white">


                <tr>
                    <th>Document Type 
            <asp:DropDownList runat="server" ID="drpDocumentType">
                <asp:ListItem>Select One..</asp:ListItem>
                <asp:ListItem Value="Blue">Information</asp:ListItem>
                <asp:ListItem Value="Green">Discussion</asp:ListItem>
                <asp:ListItem Value="Red">Urgent</asp:ListItem>
                <asp:ListItem Value="Yellow">Signature</asp:ListItem>
                <asp:ListItem Value="Orange">Correspondence</asp:ListItem>


            </asp:DropDownList>
                    </th>
                    <th>Pages to Print
                    <asp:DropDownList runat="server" ID="drpPageToPrint">
                        <asp:ListItem Value="0">All..</asp:ListItem>
                        <asp:ListItem Value="1">1 </asp:ListItem>
                        <asp:ListItem Value="2">2 </asp:ListItem>
                        <asp:ListItem Value="3">3  </asp:ListItem>
                         <asp:ListItem Value="4">4  </asp:ListItem>


                    </asp:DropDownList></th>
                </tr>
                <tr>
                    <td colspan="2" style="background-color: white">
                        <div id="dvMain" runat="server" style="width: 100%">
                            <div class="pageBody">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">

                                            <div id="dvheader" runat="server" style="width: 100%; background-color: #4B6C9E; height: 100px">
                                                <div style="float: left">
                                                    <div class="panel-heading">
                                                        <strong style="font-size: large; color: white"><%= CurrentProcess != null ? string.Format("{0} v {1} ","OOC Office", CurrentProcess.Version)   : string.Empty %>
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
                                        <td width="50%">Reference Number</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblReferenceNumber" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Date Received</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblDateRecieved" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Subject</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblSubject"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Method Received</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblMethodRecieved" Enabled="false"></asp:TextBox>
                                    </tr>

                                    <tr>
                                        <td>Back Office / OOC Comments</td>

                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:TextBox runat="server"  ID="txtDocContent" TextMode="MultiLine" Height="300px" MaxLength="600" Width="100%"></asp:TextBox></td>
                                    </tr>
                                    
                                    <tr>
                                        <td colspan='2'>
                                            <hr />
                                            <div style="float: right">

                                                <br />
                                                Page 1
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                            <br />
                            <br />

                            <div id="dvSecond" runat="server">
                                <div class="pageBody">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">

                                                <div id="Div3" runat="server" style="width: 100%; background-color: #4B6C9E; height: 100px">
                                                    <div style="float: left">
                                                        <div class="panel-heading">
                                                            <strong style="font-size: large; color: white"><%= CurrentProcess != null ? string.Format("{0} v {1} ","OOC Office", CurrentProcess.Version)   : string.Empty %>
                                                                <br />
                                                                Cover Page  </strong>
                                                        </div>
                                                    </div>
                                                    <div style="float: right">
                                                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Default.aspx">
                                                            <asp:Image runat="server" Width="250px" ID="Image3" ImageUrl="~/Images/logo-sars.png" BorderStyle="None"
                                                                ImageAlign="Left" />
                                                        </asp:HyperLink>
                                                    </div>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%">Reference Number</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="lblSecReferenceNumber" Enabled="false"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Date Received</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="lblSecDateReceived" Enabled="false"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Subject</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="lblSecSubject" Enabled="true"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Method Received</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="lblSecMethodReceived" Enabled="false"></asp:TextBox>
                                        </tr>
                                        <tr>
                                            <td>Responsible Administrator</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="lblActionPerson" Enabled="false"></asp:TextBox></td>
                                        </tr>
                                        <tr style="visibility: hidden">
                                            <td>
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="chkDelivered" Text=""></asp:CheckBox>&nbsp;Delivered</td>

                                        </tr>

                                        <tr style="visibility: hidden">

                                            <td colspan="2">
                                                <asp:CheckBox runat="server" CssClass="checkbox" Text="" ID="chkSigneOff"></asp:CheckBox>&nbsp;All SARS Numbers have been reviewed and signed off</td>
                                        </tr>
                                        <tr>

                                            <td>Document has been checked for grammar and spelling errors</td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="chkGrammer" >
                                                    <asp:ListItem Value="1" Text="Yes" />
                                                    <asp:ListItem Value="0" Text="No" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>Document has been checked for conflicting messages</td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="chkConflictMessages">
                                                    <asp:ListItem Value="1" Text="Yes" />
                                                    <asp:ListItem Value="0" Text="No" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>Document addresses the information requested</td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="chkInfoRequested">
                                                    <asp:ListItem Value="1" Text="Yes" />
                                                    <asp:ListItem Value="0" Text="No" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>

                                        <tr>

                                            <td>Template and format requirements met acknowledge Receipt</td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="chkDocSarsSTD">
                                                    <asp:ListItem Value="1" Text="Yes" />
                                                    <asp:ListItem Value="0" Text="No" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="visibility: hidden">

                                            <td colspan="2">
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="chkDisclose" Text=""></asp:CheckBox>&nbsp;Document does not disclose inappropriate / unsubstantiated facts</td>
                                        </tr>
                                        <tr>
                                            <td colspan='2'>
                                                <hr />
                                                <div style="float: right">

                                                    <br />
                                                    Page 2
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </div>
                            <br />
                            <br />

                            <div id="dvThird" runat="server">
                                <div class="pageBody">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <div id="Div1" runat="server" style="width: 100%; background-color: #4B6C9E; height: 100px">
                                                    <div style="float: left">
                                                        <div class="panel-heading">
                                                            <strong style="font-size: large; color: white"><%= CurrentProcess != null ? string.Format("{0} v {1} ","OOC Office", CurrentProcess.Version)   : string.Empty %>
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
                                            <td style="width: 50%">Reference Number</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="lblComReferenceNo" Enabled="false"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Subject</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="lblComSubject" Enabled="false"></asp:TextBox></td>
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
                                                <asp:TextBox runat="server" ID="lblPriority" Enabled="false"></asp:TextBox></td>
                                        </tr>




                                        <tr style="visibility: hidden">
                                            <td>
                                                <asp:CheckBox runat="server" CssClass="checkbox" ID="lblSignOff" Text=""></asp:CheckBox>Sign-off for release</td>
                                        </tr>
                                        <tr>
                                            <td> Comment 1</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox runat="server"  ID="txtComment"  Height="300px" MaxLength="600" Width="100%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                         <tr>
                                        <td>Comment 2</td>

                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:TextBox runat="server"  ID="txtDocContent2" TextMode="MultiLine"  Height="300px" MaxLength="600" Width="100%"></asp:TextBox></td>
                                    </tr>
                                        <tr>
                                            <td colspan="2" style="font-size: large"><strong>Commissioner's Comments</strong></td>

                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox runat="server" CssClass="checkbox" ID="txtCommissionersComment" TextMode="MultiLine"  Height="300px" MaxLength="600" Width="100%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan='2'>
                                                <hr />
                                                <div style="float: right">

                                                    <br />
                                                    Page 3
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <br />
                            <br />

                            <div id="dvForth" runat="server">
                                <div class="pageBody">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <div id="Div2" runat="server" style="width: 100%; background-color: #4B6C9E; height: 100px">
                                                    <div style="float: left">
                                                        <div class="panel-heading">
                                                            <strong style="font-size: large; color: white"><%= CurrentProcess != null ? string.Format("{0} v {1} ","OOC Office", CurrentProcess.Version)   : string.Empty %>
                                                                <br />
                                                                Cover Page  </strong>
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
                                            <td width="50%">Reference Number</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblMinReferenceNo"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Subject</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblMinSubject"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td >
                                                Approval Required from DG: NT</td>
                                            <td>
                                                 <asp:DropDownList runat="server" ID="chkApprovalFromDG">
                                                    <asp:ListItem Value="1" Text="Yes" />
                                                    <asp:ListItem Value="0" Text="No" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Approval Required from Deputy Minister</td>
                                             <td>
                                                 <asp:DropDownList runat="server" ID="ckApprovalFromDeputyMinister">
                                                    <asp:ListItem Value="1" Text="Yes" />
                                                    <asp:ListItem Value="0" Text="No" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Approval Required from Minister</td>
                                            <td>
                                                 <asp:DropDownList runat="server" ID="chkApprovalFromMinister">
                                                    <asp:ListItem Value="1" Text="Yes" />
                                                    <asp:ListItem Value="0" Text="No" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="visibility:hidden">
                                            <td >
                                             Additional Information</td>
                                            <td>
                                                 <asp:DropDownList runat="server" ID="chkAdditionalInformation">
                                                    <asp:ListItem Value="1" Text="Yes" />
                                                    <asp:ListItem Value="0" Text="No" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan='2'>
                                                <hr />
                                                <div style="float: right">

                                                    <br />
                                                    Page 4
                                                </div>
                                            </td>
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

