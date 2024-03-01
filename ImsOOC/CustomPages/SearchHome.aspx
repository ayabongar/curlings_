<%@ Page Title="INCIDENT SEARCH" Language="C#" MasterPageFile="~/NormalUser.master" AutoEventWireup="true" CodeFile="SearchHome.aspx.cs" Inherits="CustomPages_SearchHome" %>

<%@ Register Src="~/Admin/SearchFilterTypes.ascx" TagPrefix="uc1" TagName="SearchFilterTypes" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/webservices.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading"> - Search - </div>
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
                        </td>
                    </tr>
                </table>
            </fieldset>

            <fieldset runat="server" id="fsRecords" visible="False">
                <asp:GridView DataKeyNames="IncidentID,Incident Status" runat="server" ID="gvIncidents"
                    CssClass="documents" AutoGenerateColumns="False"
                    OnPageIndexChanging="PageChanging" OnRowDataBound="RowDataBound"
                    OnSelectedIndexChanged="gvIncidents_SelectedIndexChanged" AllowPaging="True" OnRowCommand="gvIncidents_RowCommand">
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
                                <asp:Button runat="server" ID="btnCoverPage" Text="CoverPage" CommandArgument='<%#Eval("IncidentID") %>' CommandName="btnCoverPage" Width="200px" />
                                <asp:Button runat="server" ID="btnAcknowledgement" Text="Acknowledgement Letter" CommandArgument='<%#Eval("IncidentID") %>' CommandName="btnAcknowledgement" Width="200px"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>
    </div>
</asp:Content>

