<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ConfigureReminders.aspx.cs" Inherits="Admin_ConfigureReminders" %>


<%@ Register Src="UserSelector.ascx" TagName="UserSelector" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/webservices.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            ADD/REMOVE USERS</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 15px; width: 100%;">
                        <tr>
                           
                            <td style="text-align: center; vertical-align: top;">
                                <fieldset>
                                    <table style="width: 100%; text-align: left;">
                                         <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Process Name:</strong>
                                            </td>
                                             <td style="padding-left: 15px;">
                                                <input type="text" style="height: 30px; width: 300px;" value="<%= CurrentProcess != null ? CurrentProcess.Description : string.Empty %>"></input>
                                            </td>
                                        </tr>
                                       
                                         <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Remind Users:</strong>
                                            </td>
                                              <td style="padding-left: 15px;">
                                                <asp:DropDownList ID="drpNotifyUsers" runat="server">                                                   
                                                    <asp:ListItem Text="Yes" Value="1" />
                                                    <asp:ListItem Text="No" Value="0" Selected="True" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                         <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Total No Of Notifications User should Recieve :</strong>
                                            </td>
                                             <td style="padding-left: 15px;">
                                                <asp:DropDownList ID="Users_NoOfNotification" runat="server">                                                   
                                                    <asp:ListItem Text="0" Value="0" Selected="True" />
                                                    <asp:ListItem Text="1" Value="1"  />
                                                     <asp:ListItem Text="2" Value="2"  />
                                                     <asp:ListItem Text="3" Value="3"  />
                                                     <asp:ListItem Text="4" Value="4"  />
                                                     <asp:ListItem Text="5" Value="5"  />
                                                     <asp:ListItem Text="6" Value="6"  />
                                                     <asp:ListItem Text="7" Value="7"  />
                                                     <asp:ListItem Text="8" Value="8"  />
                                                     <asp:ListItem Text="9" Value="9"  />
                                                     <asp:ListItem Text="10" Value="10"  />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                       
                                          <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Escalate To Process Owner:</strong>
                                            </td>
                                              <td style="padding-left: 15px;">
                                                <asp:DropDownList ID="EscalateToManagers" runat="server">                                                   
                                                    <asp:ListItem Text="Yes" Value="1" />
                                                    <asp:ListItem Text="No" Value="0" Selected="True" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                          <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Total No Of Notifications Process Owner should Recieve :</strong>
                                            </td>
                                              <td style="padding-left: 15px;">
                                                <asp:DropDownList ID="MngNoOfNotifications" runat="server">                                                   
                                                    <asp:ListItem Text="0" Value="0" Selected="True" />
                                                    <asp:ListItem Text="1" Value="1"  />
                                                     <asp:ListItem Text="2" Value="2"  />
                                                     <asp:ListItem Text="3" Value="3"  />
                                                     <asp:ListItem Text="4" Value="4"  />
                                                     <asp:ListItem Text="5" Value="5"  />
                                                     <asp:ListItem Text="6" Value="6"  />
                                                     <asp:ListItem Text="7" Value="7"  />
                                                     <asp:ListItem Text="8" Value="8"  />
                                                     <asp:ListItem Text="9" Value="9"  />
                                                     <asp:ListItem Text="10" Value="10"  />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                      
                                       
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Escalation Process Owner SID:</strong>
                                            </td>
                                              <td style="padding-left: 15px;">
                                                <uc1:UserSelector ID="UserSelector1" runat="server" />
                                            </td>
                                        </tr>
                                       
                                       
                                        <tr>
                                            <td style="padding-left: 15px; padding-top: 15px;">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="buttons" OnClick="Save" Text="Save"
                                                    Width="141px" meta:resourcekey="btnsubmitNextResource1" Height="30px" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>


