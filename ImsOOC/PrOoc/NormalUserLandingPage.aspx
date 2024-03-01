﻿<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUserNoUpdatePanel.master" AutoEventWireup="true" CodeFile="NormalUserLandingPage.aspx.cs" Inherits="PrOoc_NormalUserLandingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .slaabouttobeviolated {
            color: orange;
        }

        .slaviolated {
            color: red;
        }

        .slakept {
            color: black;
        }
    </style>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading"><%= String.Format("{0} v{1}", CurrentProc.Description, CurrentProc.Version) %> - MY INCIDENTS </div>
        <div class="panel-body">
             <div class="pageBody">
            <table style="width: 100%;">
                <tr>
                    <td>
                        
                            <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="False" OnButtonClicked="Toolbar1_ButtonClicked" Width="99%">
                                <Items>
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="AddNewIncident" Text="Register New Incident" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Search" Text="Search Incidents" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Reports" Text="View Reports" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Back" Text="Home" />

                                </Items>
                                <ButtonCssClasses CssClass="button" CssClassDisabled="button_disabled" CssClassEnabled="button_enabled" CssClassSelected="" />
                            </SCS:Toolbar>
                       
                    </td>
                </tr>
                <tr>
                    <td>


                        <asp:TabContainer runat="server" ID="tabIncidents">
                            <asp:TabPanel runat="server" ID="tabMyIncidents">
                                <HeaderTemplate>My Incidents</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:GridView runat="server" ID="gvIncidents" CssClass="documents" Width="100%"
                                        DataKeyNames="IncidentID,IncidentNumber,ProcessId,IncidentStatusId" AutoGenerateColumns="False"
                                        OnRowDataBound="RowDataBound" GridLines="Horizontal"
                                        OnPageIndexChanging="PageChanging" AllowPaging="True"
                                        EmptyDataText="YOU HAVE NO INCIDENTS FOR THIS SYSTEM" OnRowCommand="gvIncidents_RowCommand">
                                        <Columns>

                                            <asp:BoundField DataField="ReferenceNumber" HeaderText="Incident Number">
                                                <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Subject" HeaderText="Subject">
                                                <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Timestamp" HeaderText="Registered" DataFormatString="{0:yyyy-MM-dd hh:mm}">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd hh:mm}">

                                                <HeaderStyle VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IncidentStatus" HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AssignedToFullName" HeaderText="AssignedTo">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div >
                                                    <div style="float:left"> <asp:Button ID="btnView" runat="server" Text="View" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="View_Incident" Height="30px"></asp:Button></div>
                                                       <div style="float:left" runat="server" id="tdReCopy" visible="false">
                                                             <asp:Button ID="btnCopy" runat="server" Text="Copy Incident" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Copy_Incident" Height="30px"></asp:Button>
                                                       </div>
                                                       <div style="float:left" runat="server" id="tdReAssign">
                                                            <asp:Button ID="bntReAssign" runat="server" Text="Re-Assign" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Reassigne_Incident" Height="30px"></asp:Button>
                                                       </div>
                                                       <div style="float:left" runat="server" id="tdComplete">
                                                            <asp:Button ID="btnComplete" runat="server" Text="Complete" CommandArgument='<%#Eval("IncidentID") %>' Width="100px" CommandName="Complete_Incident" Height="30px"></asp:Button>
                                                       </div>
                                                       <div style="float:left" runat="server" id="tdClose" >
                                                           <asp:Button ID="btnClose" runat="server" visible="false" Text="Close" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Close_Incident" Width="100px" Height="30px"></asp:Button>
                                                       </div>
                                                       <div runat="server" id="tdReOpen"  style="padding-left:5px;float:left">
                                                             <asp:Button ID="btnReOpen" runat="server" visible="False" Text="Re-Open" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Reopen_Incident" Height="30px"></asp:Button> 
                                                       </div>
                                                     </div>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle ForeColor="Red" />
                                        <PagerStyle HorizontalAlign="Left" />
                                        <SelectedRowStyle CssClass="selectedRow" />
                                    </asp:GridView>
                                    <asp:Label runat="server" ID="Label1" Visible="False" Font-Bold="True"
                                        ForeColor="Red" Width="100%">THERE ARE NO INCIDENTS ASSIGNED TO YOU</asp:Label>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel runat="server" ID="tabTeamIncidents" Visible="False">
                                <HeaderTemplate>Team Incidents</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:GridView runat="server" ID="grdTeamIncidents" CssClass="documents" Width="100%"
                                        DataKeyNames="IncidentID,IncidentNumber,ProcessId,IncidentStatusId" AutoGenerateColumns="False"
                                        OnRowDataBound="RowDataBound" GridLines="Horizontal"
                                        OnPageIndexChanging="PageChanging" AllowPaging="True"
                                        EmptyDataText="YOU HAVE NO INCIDENTS FOR THIS SYSTEM" OnRowCommand="gvIncidents_RowCommand">
                                        <Columns>

                                            <asp:BoundField DataField="ReferenceNumber" HeaderText="Incident Number">
                                                <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Subject" HeaderText="Subject">
                                                <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Timestamp" HeaderText="Registered" DataFormatString="{0:yyyy-MM-dd hh:mm}">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd hh:mm}">

                                                <HeaderStyle VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IncidentStatus" HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AssignedToFullName" HeaderText="AssignedTo">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                   <div >
                                                    <div style="float:left"> <asp:Button ID="btnView" runat="server" Text="View" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="View_Incident" Height="30px"></asp:Button></div>
                                                       <div style="float:left" runat="server" id="tdReCopy" visible="False">
                                                             <asp:Button ID="btnCopy" runat="server" Text="Copy Incident" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Copy_Incident" Height="30px"></asp:Button>
                                                       </div>
                                                       <div style="float:left" runat="server" id="tdReAssign">
                                                            <asp:Button ID="bntReAssign" runat="server" Text="Re-Assign" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Reassigne_Incident" Height="30px"></asp:Button>
                                                       </div>
                                                       <div style="float:left" runat="server" id="tdComplete">
                                                            <asp:Button ID="btnComplete" runat="server" Text="Complete" CommandArgument='<%#Eval("IncidentID") %>' Width="100px" CommandName="Complete_Incident" Height="30px"></asp:Button>
                                                       </div>
                                                       <div style="float:left" runat="server" id="tdClose" >
                                                           <asp:Button ID="btnClose" visible="False" runat="server" Text="Close" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Close_Incident" Width="100px" Height="30px"></asp:Button>
                                                       </div>
                                                       <div runat="server" id="tdReOpen" visible="False" style="padding-left:5px;float:left">
                                                             <asp:Button ID="Button1" runat="server" Text="Re-Open" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Reopen_Incident" Height="30px"></asp:Button> 
                                                       </div>
                                                     </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle ForeColor="Red" />
                                        <PagerStyle HorizontalAlign="Left" />
                                        <SelectedRowStyle CssClass="selectedRow" />
                                    </asp:GridView>
                                    <asp:Label runat="server" ID="Label2" Visible="False" Font-Bold="True"
                                        ForeColor="Red" Width="100%">THERE ARE NO INCIDENTS ASSIGNED TO YOU</asp:Label>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>

                    </td>
                </tr>
            </table>
                 </div>
        </div>
    </div>
</asp:Content>



