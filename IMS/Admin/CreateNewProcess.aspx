<%@ Page Title="SARS SURVEY - CREATE QUESTIONNAIRE" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CreateNewProcess.aspx.cs" Inherits="CreateNewProcess"
    Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" %>

<%@ Register Src="UserSelector.ascx" TagName="UserSelector" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/webservices.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">



    <div class="panel panel-primary">
        <div class="panel-heading">CREATE NEW PROCESS</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%" style="padding: 15px;">
                        <tr style="min-height: 20px;">
                            <td colspan="2" class="style3"></td>
                        </tr>
                        <tr>
                            <td class="2" style="text-align: center;">
                                <table style="width: 100%; text-align: left;">
                                    <tr>
                                        <td style="padding-left: 15px;">
                                            <strong>Process Name:</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px;">
                                            <asp:TextBox ID="txtsurveyName" runat="server" Width="630px" onkeyup="CountChars(this, 250)"
                                                meta:resourcekey="txtsurveyNameResource1" MaxLength="250" Height="30px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px;">
                                            <strong>Process Administrators(Search First Name/Surname/SID):</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px;">
                                            <uc1:UserSelector ID="UserSelector1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px;">
                                            <asp:Button runat="server" ID="btnAddOwner" Text="Add" CssClass="buttons" OnClick="btnAddOwner_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px;">
                                            <asp:ListBox runat="server" ID="lbProcessOwners" Width="300px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px;">
                                            <strong>Reference Number Prefix:</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px;">
                                            <asp:TextBox runat="server" ID="txtPrefix" Width="300px" MaxLength="5" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px;">Muximum Upload File Size(Mega Bytes)</td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px;">
                                            <asp:TextBox ID="txtFileSize" runat="server" Height="30px" MaxLength="5" meta:resourcekey="txtsurveyNameResource1" onkeyup="CountChars(this, 250)" Width="111px" Enabled="False" Font-Strikeout="True" ForeColor="Red" Text="5" Style="text-align: center;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px;">
                                            <b>Add Cover Page:</b></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px;">
                                            <asp:CheckBox ID="ckAddCoverPage" runat="server" />


                                        </td>
                                    </tr>
                                      <tr>
                                                <td style="padding-left: 15px;">
                                                    <b>On Complete Re-Allocate incident to the Creator?</b></td>
                                            </tr>
                                             <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:CheckBox ID="chkCreater" runat="server" />


                                                </td>
                                            </tr>
                                      </tr>
                                               <tr>
                                                <td style="padding-left: 15px;">
                                                    <b>Can access colleague incidents?</b></td>
                                            </tr>
                                             <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:CheckBox ID="ckCanShareIncidents" runat="server" />


                                                </td>
                                            </tr>
                                    <tr>
                                        <td style="padding-left: 15px; padding-top: 15px; padding-bottom: 15px;">
                                            <asp:Button ID="btnsubmitNext" runat="server" CssClass="buttons" OnClick="CreateProcess"
                                                Text="Save New Process" Width="141px" meta:resourcekey="btnsubmitNextResource1"
                                                 />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <p>
                                    &nbsp;
                                </p>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>




</asp:Content>
