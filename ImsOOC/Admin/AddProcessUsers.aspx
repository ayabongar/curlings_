<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddProcessUsers.aspx.cs" Inherits="Admin_AddProcessUsers" %>

<%@ Register Src="UserSelector.ascx" TagName="UserSelector" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/webservices.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            ADD/REMOVE USERS</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 15px; width: 100%;">
                        <tr>
                            <td>
                                <fieldset>   <table style="width: 100%" border="0">
                                            <tr>
                                                <td align="right"><b>Search By </b>
                                                    <asp:TextBox runat="server" ID="txtTeamSearch"  CssClass="search_textbox" Width="100px" />
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                        Enabled="True" TargetControlID="txtTeamSearch" WatermarkCssClass="watermarked"
                                                        WatermarkText="SID">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:Button Text="Search" ID="Button3" runat="server" OnClick="btnTeamSearch_Click" />
                                                </td>

                                            </tr>
                                        </table>
                                    <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CssClass="documents" DataKeyNames="UserProcessId" EmptyDataText="NO USERS ASSIGNED YET"
                                        GridLines="Horizontal" OnPageIndexChanging="gvUsers_PageIndexChanging" PageSize="4"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="FullName" HeaderText="Full Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Role" HeaderText="Role">
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
                            <td style="text-align: center; vertical-align: top;">
                                <fieldset>
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Process Name:</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <input type="text" style="height: 30px; width: 300px;" value="<%= CurrentProcess != null ? CurrentProcess.Description : string.Empty %>"></input>
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
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
