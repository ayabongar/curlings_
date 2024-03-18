<%@ Page Title="INCIDENT SEARCH" Language="C#" MasterPageFile="~/NormalUser.master" AutoEventWireup="true" CodeFile="SearchHome.aspx.cs" Inherits="Admin_SearchHome" %>

<%@ Register Src="~/Admin/SearchFilterTypes.ascx" TagPrefix="uc1" TagName="SearchFilterTypes" %>
<%@ Register TagPrefix="uc1" TagName="UserSelector_1_1" Src="~/Admin/UserSelector.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/webservices.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading"><%= String.Format("{0} v{1}", CurrentProc.Description, CurrentProc.Version) %> - Search - </div>
        <div class="panel-body">
            <div class="pageBody">
                <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="False" OnButtonClicked="Toolbar1_ButtonClicked" Width="99%">
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Back" Text="Home" />
                    </Items>
                    <ButtonCssClasses CssClass="button" CssClassDisabled="button_disabled" CssClassEnabled="button_enabled" CssClassSelected="" />
                </SCS:Toolbar>
           
            <fieldset>
                <legend><b>Search by filter type:</b></legend>
                <table width="100%" border="0">
                  
                    <tr>
                        <td  style="vertical-align: top;width:40%">Or Search by </td>
                        <td style="padding-left: 1px;">
                            <asp:DropDownList runat="server" ID="ddlFilterType" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="FilterTypeChanged">
                                <asp:ListItem Value="0">Select Filter Type</asp:ListItem>
                                <asp:ListItem Value="1">Date Registered</asp:ListItem>
                                <%--<asp:ListItem Value="2">Date Assigned</asp:ListItem>--%>
                                <asp:ListItem Value="3">Registered By</asp:ListItem>
                                <asp:ListItem Value="4">Assigned To</asp:ListItem>
                                <asp:ListItem Value="5">Incident Number</asp:ListItem>
                                <asp:ListItem Value="6">Reference Number</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>


                </table>
               <table width="100%" border="0">
                        <tr>
                            <td>
                                <uc1:SearchFilterTypes runat="server" ID="SearchFilterTypes1" />

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset runat="server" id="fsSearchButton" visible="False" style="border-color: white">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="vertical-align: top;width:40%"></td>
                                            <td style="padding-left: 1px;">
                                                <asp:Button runat="server" ID="btnSearch" Text="SEARCH" Width="200px" OnClick="SearchIncidents" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>

                    </table>
            </fieldset>
            <br/>
            <fieldset>
                <legend><b>Or Search by Status Type:</b></legend>
                <table style="width: 100%;" border="0">
                      <tr>
                        <td style="vertical-align: top;width:40%">Incidents Statuses</td>
                        <td><asp:RadioButtonList runat="server" ID="radStatuses" AutoPostBack="True"  OnSelectedIndexChanged="radStatuses_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Assigned">Open</asp:ListItem>
                                <asp:ListItem Value="Complete">Closed</asp:ListItem>
                                <asp:ListItem Value="All">All</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                </table>
            </fieldset>


            <fieldset runat="server" id="fsRecords" visible="False">
                
                
            </fieldset>
                <asp:GridView runat="server"  ID="gvSearchIncidents"  AllowPaging="True" AutoGenerateColumns="False" CssClass="documents" OnRowCommand="gvSearchIncidents_RowCommand" OnRowDataBound="RowDataBound"
                    OnPageIndexChanging="PageChanging" >
                     <Columns>
                        <asp:BoundField DataField="System Name" HeaderText="System Name" />
                        <asp:BoundField DataField="IncidentNumber" HeaderText="Reference Number" />
                        <asp:BoundField DataField="ReferenceNumber" HeaderText="Incident Number" />
                        <asp:BoundField DataField="Date Registered" HeaderText="Date Registered" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="Registered By" HeaderText="Registered By" />
                        <asp:BoundField DataField="Assigned To" HeaderText="Assigned To" />
                        <asp:BoundField DataField="Incident Status" HeaderText="Incident Status" />
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:TemplateField>
                            <ItemTemplate>
                             
                                <div  runat="server" id="tdReAssign">
                                      <asp:Button ID="btnView" runat="server" Text="View" Width="100px"  CommandArgument='<%#Eval("IncidentID") %>' CommandName="View_Incident" Height="30px"></asp:Button>
                                            <asp:Button ID="bntReAssign" runat="server" Text="Re-Assign"  Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Reassigne_Incident" Height="30px"></asp:Button>

                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </div>
    </div></div>
    
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
                                                            <td class="inc-details-label">Incident Registration Date:
                                                            </td>
                                                            <td style="padding-left: 4px;">
                                                                <asp:TextBox runat="server" ID="Timestamp" Enabled="False"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="inc-details-label">Incident Status:
                                                            </td>
                                                            <td style="padding-left: 4px;">
                                                                <asp:TextBox runat="server" ID="IncidentStatus1" Enabled="False"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="inc-details-label">Assigned To:
                                                            </td>
                                                            <td>
                                                                <uc1:UserSelector_1_1 ID="UserSelector1" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="inc-details-label">Incident Summary
                                                            </td>
                                                            <td style="padding-left: 4px;">
                                                                <asp:TextBox runat="server" ID="txtSummary" Width="400px" MaxLength="250" ReadOnly="True"></asp:TextBox>





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
                                                    <uc1:UserSelector_1_1 ID="UserSelector2" runat="server" />
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

