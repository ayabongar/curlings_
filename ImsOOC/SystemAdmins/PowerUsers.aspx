<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PowerUsers.aspx.cs" Inherits="SystemAdmins_PowerUsers" %>

<%@ Register src="../Admin/UserSelector.ascx" tagname="UserSelector" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/webservices.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">VIEW POWER USERS</div>
        <div class="panel-body">
            <table>
                <tr>
                    <td>SID:</td>
                    <td>
                        <uc1:UserSelector ID="UserSelector1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button runat="server" ID="btnAddPowerUser" Text="Add User" OnClick="AddUser"/></td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <asp:GridView runat="server" ID="gvUsers" CssClass="documents" AutoGenerateColumns="False" OnRowCommand="gvUsers_RowCommand" Width="613px">
                            <Columns>
                                <asp:BoundField HeaderText="SID" DataField="SID"/>
                                <asp:BoundField HeaderText="Name" DataField="FullName"/>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkOpenFile" runat="server" CommandArgument='<%#Eval("SID") %>' CommandName="Remove" Text="Remove User"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

