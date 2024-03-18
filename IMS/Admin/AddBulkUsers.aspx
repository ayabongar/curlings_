<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddBulkUsers.aspx.cs" Inherits="Admin_AddBulkUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmit" />
                                </Triggers>
                                 <ContentTemplate>
    <div class="panel panel-primary">
        <div class="panel-heading">Add Bulk Users To a Role </div>
        <div class="panel-body">
            <table>
                <tr>
                    <td>My Role:</td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="lstRoles" /></td>
                </tr>
                <tr>
                    <td class="inc-details-label">Browse for file :</td>
                    <td style="padding-left: 4px;">
                        <asp:FileUpload runat="server" ID="flDoc" />
                        </td>
                </tr>
                <tr>
                    <td>ProcessId</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtProcessId"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView runat="server" ID="grdError"></asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
                                     </ContentTemplate>
                                </asp:UpdatePanel>
</asp:Content>

