<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ListMyProcesses.aspx.cs" Inherits="Admin_ListMyProcesses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">LIST MY PROCESSES</div>
        <div class="panel-body">

        <table style="width: 100%;">
             <tr>
                <td>
                    <fieldset>
                        <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"
                            EnableClientApi="False" CssClass="toolbar" Width="99%">
                            <Items>
                                <SCS:ToolbarButton CausesValidation="True" CommandName="AddNewIncident" Text="Add New Incident" />
                                <SCS:ToolbarButton CausesValidation="True" CommandName="ViewIncidents" Text="View Incidents" />
                            </Items>
                            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
                        </SCS:Toolbar>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <asp:GridView ID="gvProcesses" runat="server" CssClass="documents" AllowPaging="True" Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="PageIndexChanging"
                            GridLines="None" OnRowDataBound="RowDataBound" DataKeyNames="ProcessId,StatusId" PageSize="7">
                            <Columns>
                                <asp:TemplateField HeaderText="Process Name" SortExpression="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcessDescription" runat="server" Text='<%# Utils.getshortString(Eval("Description").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Prefix" HeaderText="Incident Prefix">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Timestamp" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Date Create" SortExpression="Timestamp">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="selectedRow" />
                        </asp:GridView>
                        <asp:Label runat="server" ID="lblMessage" Visible="False" Font-Bold="True"
                            ForeColor="Red" Width="100%">THERE ARE NO PROCESSES</asp:Label>
                    </fieldset>
                </td>
            </tr>
        </table>
      </div>
    </div>
</asp:Content>

