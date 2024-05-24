<%@ Page Title="INCIDENT SEARCH" Language="C#" MasterPageFile="~/OOC/NormalUser.master" AutoEventWireup="true" CodeFile="SearchHome.aspx.cs" Inherits="Admin_SearchHome" %>
<%@ Register Src="~/Admin/SearchFilterTypes.ascx" TagPrefix="uc1" TagName="SearchFilterTypes" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/webservices.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<%--    <asp:UpdatePanel runat="server" ID="pncontents">
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnExport"  />
             
        </Triggers>
        <ContentTemplate>--%>
            <div class="panel panel-primary">
        <div class="panel-heading"><%= String.Format("{0} v{1}", CurrentProc.Description, CurrentProc.Version) %> - Search - </div>
        <div class="panel-body">
            <fieldset>
                <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="False" OnButtonClicked="Toolbar1_ButtonClicked" Width="99%">
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Back" Text="Home" />
                    </Items>
                    <ButtonCssClasses CssClass="button" CssClassDisabled="button_disabled" CssClassEnabled="button_enabled" CssClassSelected="" />
                </SCS:Toolbar>
            </fieldset>
            <fieldset>
                <table width="80%">
                    <tr>
                        <td style="width: 450px;">Select your filter type:
                        </td>
                        <td style="padding-left: 7px;">
                            <asp:DropDownList runat="server" ID="ddlFilterType" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="FilterTypeChanged">
                                <asp:ListItem Value="0">Select Filter Type</asp:ListItem>
                                <asp:ListItem Value="DateRegistered">Date Registered</asp:ListItem>
                                <%--<asp:ListItem Value="2">Date Assigned</asp:ListItem>--%>
                                <asp:ListItem Value="RegisteredBy">Registered By</asp:ListItem>
                                <asp:ListItem Value="AssignedTo">Assigned To</asp:ListItem>
                                <asp:ListItem Value="Subject">Subject</asp:ListItem>
                                <asp:ListItem Value="IncidentNumber">Incident Number</asp:ListItem>
                                 <asp:ListItem Value="CompleteOrClosed">Complete Or Closed</asp:ListItem>
                                 <asp:ListItem Value="AssignedOrWorkInProgress">Assigned Or Work In Progress</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                </table>
            </fieldset>
            <fieldset>
                <table width="80%">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:SearchFilterTypes runat="server" ID="SearchFilterTypes1" />

                        </td>
                    </tr>
                </table>
            </fieldset>

            <fieldset runat="server" id="fsSearchButton" visible="False">
                <table style="width: 80%;">
                    <tr>
                        <td style="width: 450px;"></td>
                        <td style="padding-left: 7px;">
                            <asp:Button runat="server" ID="btnSearch" Text="SEARCH" Width="200px" OnClick="SearchIncidents" />
                             <asp:Button runat="server" ID="btnExport" Text="Export To Excel" Width="200px" OnClick="btnExport_Click"  />
                        </td>
                    </tr>
                </table>
            </fieldset>

            <fieldset runat="server" id="fsRecords" visible="False">
                <asp:GridView DataKeyNames="IncidentID,Incident Status" runat="server" ID="gvIncidents"
                    CssClass="documents" AutoGenerateColumns="False"
                    OnPageIndexChanging="PageChanging" OnRowDataBound="RowDataBound"
                    OnSelectedIndexChanged="gvIncidents_SelectedIndexChanged" AllowPaging="True" PageSize="30" OnRowCommand="gvIncidents_RowCommand" >
                    <Columns>
                        <asp:BoundField DataField="System Name" HeaderText="System Name"   />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" />
                        <asp:BoundField DataField="Incident Number" HeaderText="Incident Number" />
                        <asp:BoundField DataField="Date Registered" HeaderText="Date Registered" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="Registered By" HeaderText="Registered By" />
                        <asp:BoundField DataField="Assigned To" HeaderText="Assigned To" />
                        <asp:BoundField DataField="Incident Status" HeaderText="Incident Status" />
                        <asp:BoundField DataField="Due Date" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                         <td runat="server" id="tdReAssign">
                                              <asp:Button ID="bntReAssign" runat="server" Text="View" Width="100px" CommandArgument='<%#Eval("IncidentID")  +"-"+ Eval("ProcessID")%>' CommandName="bntReAssign" Height="30px"></asp:Button>
                                             </td>
                                    </tr>
                                </table>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>
    </div>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>
    --%>
</asp:Content>

