<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddWorkInfo.aspx.cs" Inherits="SurveyWizard_AddWorkInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/_validation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            ADD/VIEW WORK INFO FOR INCIDENT :
            <%=CurrentIncident.IncidentNumber %></div>
        <div class="panel-body">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <fieldset>
                            <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"
                                EnableClientApi="False" CssClass="toolbar" Width="99%">
                                <Items>
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Add" Text="Add" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Back" Text="Back" />
                                </Items>
                                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                                    CssClassDisabled="button_disabled"></ButtonCssClasses>
                            </SCS:Toolbar>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset>
                            <asp:TextBox runat="server" ID="txtNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset>
                            <asp:GridView runat="server" CssClass="documents" ID="gvWorkInfo" GridLines="Horizontal"
                                AutoGenerateColumns="False" OnRowDataBound="RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="CreatedBy" HeaderText="Added By" />
                                    <asp:BoundField DataField="Timestamp" HeaderText="Date Created" DataFormatString="{0:yyyy-MM-dd hh:mm}" />
                                    <asp:TemplateField HeaderText="Notes" SortExpression="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNotes" runat="server" Text='<%# Utils.getshortString(Eval("Notes").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
