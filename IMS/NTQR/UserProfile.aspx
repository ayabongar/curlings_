<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="NTQR_UserProfile" %>

<%@ Register Src="../Admin/UserSelector.ascx" TagName="UserSelector" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/webservices.js"></script>
    <script>
        function confirmDelete(user) {
            return confirm('Are you that you want to delete ' + user);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel-body">



        <div class="panel panel-primary">
            <div class="panel-heading">
                ADD/REMOVE USERS
            </div>
            <div class="panel-body">
                <div class="pageBody">
                    <table style="padding: 15px; width: 100%;">
                        <tr runat="server" visible="false">

                            <td style="text-align: center; vertical-align: top;">
                                <fieldset>
                                    <legend>Add New User</legend>
                                    <table style="width: 100%; text-align: left;">

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
                                            <td style="padding-left: 15px;">
                                                <strong>Upload Signature:</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:FileUpload runat="server" ID="fileUpload" Width="425px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Select User Role:</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <asp:DropDownList ID="ddlRole" runat="server" Height="30px" MaxLength="20" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px; padding-top: 15px;">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="buttons" OnClick="Save" Text="Add Process User"
                                                    Width="141px" meta:resourcekey="btnsubmitNextResource1" Height="30px" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>Existing Users</legend>
                                    <table style="width: 100%" border="0">

                                        <tr>
                                            <td>
                                                <asp:Button Text="Add User" ID="AddUser" runat="server" OnClick="AddUser_Click" /></td>
                                            <td align="right"><b>Search By </b>
                                                <asp:TextBox runat="server" ID="txtTeamSearch" CssClass="search_textbox" Width="100px" />
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                    Enabled="True" TargetControlID="txtTeamSearch" WatermarkCssClass="watermarked"
                                                    WatermarkText="SID">
                                                </asp:TextBoxWatermarkExtender>
                                                <asp:Button Text="Search" ID="Button3" runat="server" OnClick="btnTeamSearch_Click" />
                                            </td>

                                        </tr>
                                    </table>
                                    <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CssClass="documents" DataKeyNames="UserId,ID" EmptyDataText="NO USERS ASSIGNED YET"
                                        GridLines="Horizontal" OnPageIndexChanging="gvUsers_PageIndexChanging" PageSize="100"
                                        Width="100%">
                                        <Columns>
                                         
                                            <asp:BoundField DataField="UserFullName" HeaderText="Full Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                         
                                            <asp:BoundField DataField="RoleName" HeaderText="Role">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                             <asp:TemplateField>
                                                 <HeaderTemplate >Unit</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="units" Text='<%# GetUserUnits(Eval("UserId") ) %>' runat="server" />
                                                    
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnModify" runat="server" OnClick="ModifyUser" Text="Modify User"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                                  <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnRemove" runat="server" OnClientClick='<%# "return confirm(\"Do you want to delete " + Eval("UserFullName") + "\")" %>'  OnClick="RemoveUser" Text="Remove User Role"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle ForeColor="Red" />
                                    </asp:GridView>
                                </fieldset>
                            </td>
                        </tr>
                    </table>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

