<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUser.master" AutoEventWireup="true" CodeFile="NormalUserLandingPage.aspx.cs" Inherits="Admin_NormalUserLandingPage" %>

<%@ Register Src="~/Admin/SearchFilterTypes.ascx" TagPrefix="uc1" TagName="SearchFilterTypes" %>
<%@ Register TagPrefix="uc1" TagName="UserSelector_1" Src="~/Admin/UserSelector.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .slaabouttobeviolated {
            color: black !important;
        }

        .slaviolated {
            color: red !important;
          
        }

        .slakept {
            color: black !important;
           
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading"><%= String.Format("{0} v{1}", CurrentProc.Description, CurrentProc.Version) %> - MY INCIDENTS </div>
        <div class="panel-body">
            <div class="pageBody">
                <table style="width: 100%;">
                    <tr>
                        <td>

                            <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="False" OnButtonClicked="Toolbar1_ButtonClicked" Width="99.5%">
                                <Items>
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="AddNewIncident" Text="Register New Incident" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Search" Text="Advance Search" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Reports" Text="View Reports" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Back" Text="Home" />

                                </Items>
                                <ButtonCssClasses CssClass="button" CssClassDisabled="button_disabled" CssClassEnabled="button_enabled" CssClassSelected="" />
                            </SCS:Toolbar>

                        </td>
                    </tr>
                    <tr>
                        <td>




                            <asp:TabContainer runat="server" ID="tabIncidents" >
                                <asp:TabPanel runat="server" ID="tabMyIncidents">
                                    <HeaderTemplate>My Incidents</HeaderTemplate>
                                    <ContentTemplate>
                                        <table style="width: 100%" border="0">
                                            <tr>
                                                <td align="right"><b>Search By </b>
                                                    <asp:TextBox runat="server" ID="txtMyIncidents" CssClass="search_textbox" Width="400px" />
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                                        Enabled="True" TargetControlID="txtMyIncidents" WatermarkCssClass="watermarked"
                                                        WatermarkText="IncidentNo,Summary,StatusType">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:Button Text="Search" ID="Button4" runat="server" OnClick="btnTeamSearch_Click" />
                                                </td>

                                            </tr>
                                        </table>
                                        <asp:GridView runat="server" ID="gvIncidents" CssClass="documents" Width="100%"
                                            DataKeyNames="IncidentID,IncidentNumber,ProcessId,IncidentStatusId,IncidentStatus" AutoGenerateColumns="False"
                                            OnRowDataBound="RowDataBound" GridLines="Horizontal" PageSize="30"
                                            OnPageIndexChanging="PageChanging" AllowPaging="True"
                                            EmptyDataText="YOU HAVE NO INCIDENTS FOR THIS SYSTEM" OnRowCommand="gvIncidents_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="AssignedToFullName" HeaderText="Assigned To">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ReferenceNumber" HeaderText="Incident Number">
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
                                               
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div>
                                                            <div style="float: left">
                                                                <asp:Button ID="btnView" runat="server" Text="View" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="View_Incident" Height="30px"></asp:Button>
                                                            </div>
                                                            <div style="float: left" runat="server" id="tdReCopy" visible="False">
                                                                <asp:Button ID="btnCopy" runat="server" Text="Copy Incident" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Copy_Incident" Height="30px"></asp:Button>
                                                            </div>
                                                            <div style="float: left" runat="server" id="tdReAssign">
                                                                <asp:Button ID="bntReAssign" runat="server" Text="Re-Assign" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Reassigne_Incident" Height="30px"></asp:Button>
                                                            </div>
                                                            <div style="float: left" runat="server" id="tdComplete">
                                                                <asp:Button ID="btnComplete" runat="server" Text="Complete" CommandArgument='<%#Eval("IncidentID") %>' Width="100px" CommandName="Complete_Incident" Height="30px"></asp:Button>
                                                            </div>
                                                            <div style="float: left" runat="server" id="tdClose">
                                                                <asp:Button ID="btnClose" runat="server" Text="Close" Visible="False" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Close_Incident" Width="100px" Height="30px"></asp:Button>
                                                            </div>
                                                            <div runat="server" id="tdReOpen" style="padding-left: 5px; float: left">
                                                                <asp:Button ID="btnReOpen" Visible="False" runat="server" Text="Re-Open" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Reopen_Incident" Height="30px"></asp:Button>
                                                            </div>
                                                        </div>
                                                        <%-- <table style="padding: 0px;">
                                             <tr>
                                                 <td>
                                                     <asp:Button ID="btnView" runat="server" Text="View" Width="100px"  CommandArgument='<%#Eval("IncidentID") %>' CommandName="View_Incident" Height="30px" ></asp:Button>
                                                 </td>
                                                  <td  runat="server" id="tdCopy">
                                                     <asp:Button ID="btnCopy" runat="server" Text="Copy Incident" Width="100px"  CommandArgument='<%#Eval("IncidentID") %>' CommandName="Copy_Incident" Height="30px" ></asp:Button>
                                                 </td>
                                                 <td runat="server" id="tdReAssign">
                                                     <asp:Button ID="bntReAssign" runat="server" Text="Re-Assign" Width="100px"  CommandArgument='<%#Eval("IncidentID") %>' CommandName="Reassigne_Incident"  Height="30px"></asp:Button>
                                                 </td>
                                                 <td runat="server" id="tdComplete">
                                                     <asp:Button ID="btnComplete" runat="server" Text="Complete" CommandArgument='<%#Eval("IncidentID") %>' Width="100px" CommandName="Complete_Incident" Height="30px"></asp:Button>
                                                 </td>
                                                 <td runat="server" id="tdClose" Visible="False">
                                                     <asp:Button ID="btnClose" runat="server" Text="Close"  CommandArgument='<%#Eval("IncidentID") %>' CommandName="Close_Incident" Width="100px"  Height="30px"></asp:Button>
                                                 </td>
                                                 
                                                  <td runat="server" id="tdReOpen" Visible="False">
                                                     <asp:Button ID="btnReOpen" runat="server" Text="Re-Open"  CommandArgument='<%#Eval("IncidentID") %>' CommandName="Reopen_Incident"  Height="30px"></asp:Button>
                                                 </td>
                                             </tr>
                                         </table>--%>
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
                                        <table style="width: 100%" border="0">
                                            <tr>
                                                <td align="right"><b>Search By </b>
                                                    <asp:TextBox runat="server" ID="txtTeamSearch" Text="Work In Progress" CssClass="search_textbox" Width="400px" />
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                        Enabled="True" TargetControlID="txtTeamSearch" WatermarkCssClass="watermarked"
                                                        WatermarkText="AssignedToSID,IncidentNo,Summary,StatusType">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:Button Text="Search" ID="Button3" runat="server" OnClick="btnTeamSearch_Click" />
                                                </td>

                                            </tr>
                                        </table>
                                        <asp:GridView runat="server" ID="grdTeamIncidents" CssClass="documents" Width="100%"
                                            DataKeyNames="IncidentID,IncidentNumber,ProcessId,IncidentStatusId" AutoGenerateColumns="False"
                                            OnRowDataBound="RowDataBound" GridLines="Horizontal"
                                            OnPageIndexChanging="PageChanging" AllowPaging="True" PageSize="30"
                                            EmptyDataText="YOU HAVE NO INCIDENTS FOR THIS SYSTEM" OnRowCommand="gvIncidents_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="AssignedToFullName" HeaderText="Assigned To">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ReferenceNumber" HeaderText="Incident Number">
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

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div>
                                                            <div style="float: left">
                                                                <asp:Button ID="btnView" runat="server" Text="View" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="View_Incident" Height="30px"></asp:Button>
                                                            </div>
                                                            <div style="float: left" runat="server" id="tdReCopy">
                                                                <asp:Button ID="btnCopy" runat="server" Visible="False" Text="Copy Incident" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Copy_Incident" Height="30px"></asp:Button>
                                                            </div>
                                                            <div style="float: left" runat="server" id="tdReAssign">
                                                                <asp:Button ID="bntReAssign" runat="server" Text="Re-Assign" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Reassigne_Incident" Height="30px"></asp:Button>
                                                            </div>

                                                            <div style="float: left" runat="server" id="tdClose">
                                                                <asp:Button ID="btnClose" runat="server" Text="Close" Visible="False" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Close_Incident" Width="100px" Height="30px"></asp:Button>
                                                            </div>
                                                            <div runat="server" id="tdReOpen" style="padding-left: 5px; float: left">
                                                                <asp:Button ID="btnReOpen" Visible="False" runat="server" Text="Re-Open" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Reopen_Incident" Height="30px"></asp:Button>
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

    <asp:Panel runat="server" ID="pnlAllocate">
        <table>
            <tr>
                <td style="visibility: hidden">
                    <asp:Button runat="server" ID="btnViewCase" Text="View Case Details" />
                    <asp:Button runat="server" ID="btnExit" Text="View Case Details" />
                </td>
            </tr>
        </table>
        <table width="100%" style="background-color: white">
            <tr style="background-color: grey">
                <td style="max-height: 20px">
                    <table width="100%" border="0" class="panel-heading">
                        <tr>
                            <td style="color: white">Re-Assign Incidents</td>
                            <td align="right">
                                <asp:ImageButton runat="server" ImageUrl="~/Images/Close_Box_Red.png" ID="btnClose" CausesValidation="false" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="Panel1" Enabled="true">

                        <table style="width: 100%;">
                            <tr>
                                <td>

                                    <SCS:Toolbar ID="Toolbar2" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"
                                        EnableClientApi="False" CssClass="toolbar" Width="99%">
                                        <Items>
                                            <SCS:ToolbarButton CausesValidation="True" CommandName="Submit" Text="Submit" />

                                        </Items>
                                        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                                            CssClassDisabled="button_disabled"></ButtonCssClasses>
                                    </SCS:Toolbar>
                                    <br />
                                    <br />
                                    <div class="pageBody">
                                        <asp:TabContainer runat="server" ID="tbContainer" ActiveTabIndex="0" Width="100%">
                                            <asp:TabPanel runat="server" ID="tbDetails">
                                                <HeaderTemplate>
                                                    Incident Details
                                                </HeaderTemplate>





                                                <ContentTemplate>
                                                    <table class="inc-details">
                                                        <tr>
                                                            <td class="inc-details-label">Incident Number:
                                                            </td>
                                                            <td style="padding-left: 4px;">
                                                                <asp:TextBox runat="server" ID="IncidentNumber"></asp:TextBox>

                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td class="inc-details-label">Assigned To:
                                                            </td>
                                                            <td>
                                                                <uc1:UserSelector_1 ID="UserSelector1" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="inc-details-label">Incident Due Date:
                                                            </td>
                                                            <td style="padding-left: 4px;">
                                                                <asp:TextBox type="text" runat="server" ID="txtIncidentDueDate" Width="150px" ReadOnly="True" />




                                                                <asp:CalendarExtender
                                                                    ID="txtIncidentDueDate_CalendarExtender" runat="server" ClearTime="True" Enabled="True"
                                                                    Format="yyyy-MM-dd hh:mm" TargetControlID="txtIncidentDueDate" TodaysDateFormat="yyyy-MM-dd hh:mm">
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
                                                <td class="inc-details-label">New Assignee:
                                                </td>
                                                <td>
                                                    <uc1:UserSelector_1 ID="UserSelector2" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="inc-details-label">Note:
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtNotes" Width="500px" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:ModalPopupExtender ID="mpAllocate" runat="server" PopupControlID="pnlAllocate" TargetControlID="btnViewCase" RepositionMode="RepositionOnWindowResizeAndScroll"
        Drag="True"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>



</asp:Content>

