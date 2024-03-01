﻿<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUser.master" AutoEventWireup="true"
    CodeFile="CompleteIncident.aspx.cs" Inherits="Admin_CompleteIncident" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="UserSelector" Src="~/Admin/UserSelector.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <style type="text/css">
        .inc-details
        {
            width: 100%;
            padding: 15px;
        }
        
        .inc-details input[type="text"]
        {
            width: 300px;
            height: 30px;
            padding-left: 2px;
        }
        
        .inc-details-label
        {
            width: 50%;
            vertical-align: top;
            margin: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
    <div class="panel panel-primary">
        <div class="panel-heading">
          <%=CurrentProc.Description.ToUpper()%> - COMPLETE THIS INCIDENT</div>
        <div class="panel-body">
           
        <table style="width: 100%;">
            <tr>
                <td>
                    
                        <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"
                            EnableClientApi="False" CssClass="toolbar" Width="99%">
                            <Items>
                                <SCS:ToolbarButton CausesValidation="True" CommandName="Submit" Text="Submit" />
                                <SCS:ToolbarButton CausesValidation="True" CommandName="Cancel" Text="Cancel" />
                            </Items>
                            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                                CssClassDisabled="button_disabled"></ButtonCssClasses>
                        </SCS:Toolbar>
                    <br /><br />
                <div class="pageBody">
                        <asp:TabContainer runat="server" ID="tbContainer" ActiveTabIndex="0" Width="100%">
                            <asp:TabPanel runat="server" ID="tbDetails">
                                <HeaderTemplate>
                                    Incident Details
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table class="inc-details">
                                        <tr>
                                            <td class="inc-details-label">
                                                Incident Number:
                                            </td>
                                            <td style="padding-left: 4px;">
                                                <input type="text" value="<%=CurrentIncidentDetails.ReferenceNumber %>" disabled="disabled" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="inc-details-label">
                                                Incident Registration Date:
                                            </td>
                                            <td style="padding-left: 4px;">
                                                <input type="text" value="<%=CurrentIncidentDetails.Timestamp.ToString("yyyy-MM-dd HH:mm") %>"
                                                    disabled="disabled" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="inc-details-label">
                                                Incident Status:
                                            </td>
                                            <td style="padding-left: 4px;">
                                                <input type="text" value="<%=CurrentIncidentDetails.IncidentStatus %>" disabled="disabled" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="inc-details-label">
                                                Assigned To:
                                            </td>
                                            <td>
                                                <uc1:userselector id="UserSelector1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="inc-details-label">
                                                Incident Summary
                                            </td>
                                            <td style="padding-left: 4px;">
                                                <asp:TextBox runat="server" ID="txtSummary" Width="400px" MaxLength="250" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="inc-details-label">
                                                Incident Due Date:
                                            </td>
                                            <td style="padding-left: 4px;">
                                                <asp:TextBox type="text" runat="server" ID="txtIncidentDueDate" Width="150px" ReadOnly="True" />
                                                <asp:CalendarExtender ID="txtIncidentDueDate_CalendarExtender" runat="server" ClearTime="True"
                                                    Enabled="True" Format="yyyy-MM-dd hh:mm" TargetControlID="txtIncidentDueDate"
                                                    TodaysDateFormat="yyyy-MM-dd hh:mm">
                                                </asp:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                </div>
                </td>
            </tr>
            
            <tr>
                <td>
                  <div class="pageBody">
                        <table width="100%">
                          
                            <tr>
                                <td class="inc-details-label">Note:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNotes" Width="500px" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
        </div>
  
    
</asp:Content>
