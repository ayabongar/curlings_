<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddProcessOwners.aspx.cs" Inherits="Admin_AddProcessOwners" %>

<%@ Register Src="UserSelector.ascx" TagName="UserSelector" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/webservices.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            ADD/REMOVE OWNERS FROM [<%=CurrentIncidentProcess.Description %>]</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 15px; width: 100%;">
                        <tr> 
                            <td style="width: 50%; vertical-align: top;">
                                <fieldset>
                                    <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="OwnerId"
                                        CssClass="documents" EmptyDataText="NO USERS ASSIGNED YET"
                                        GridLines="Horizontal" OnPageIndexChanging="gvUsers_PageIndexChanging" PageSize="4"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="FullName" HeaderText="Full Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnRemove" runat="server" OnClick="RemoveUser" Text="Remove User"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle ForeColor="Red" />
                                    </asp:GridView>
                                </fieldset>
                            </td>
                            <td style="vertical-align: top; width: 50%;">
                                <fieldset>
                                    <table style="text-align: left;">
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Process Name:</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <input type="text" style="height: 30px; width: 100%;" value="<%= CurrentProcess != null ? CurrentProcess.Description : string.Empty %>"></input>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Search User (SID/Name/Surname):</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <uc1:UserSelector ID="UserSelector1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px; padding-top: 15px;">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="buttons" Height="30px" meta:resourcekey="btnsubmitNextResource1" OnClick="Save" Text="Add Process Owner" Width="141px" />
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
