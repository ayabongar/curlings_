<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageRoles.aspx.cs" Inherits="SystemAdmins_ManageRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style3
        {
            width: 100%;
            background-color: #dde4ec;
        }
        .style4
        {
            width: 193px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            MANAGE USER ACCESS</div>
        <div class="panel-body">
            <fieldset>
        
        <table style="width: 100%;">
          
            <tr>
                <td class="style4">
                    User SID:
                </td>
                <td>
                    <table>
                        <tr>
                            <td><asp:TextBox ID="txtUserSID" runat="server" Height="28px"></asp:TextBox></td>
                            <td><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="buttons" Height="30px" OnClick="Search" /></td>
                        </tr>
                    </table>
                    
                    
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Select Role(s):
                </td>
                <td>
                    <asp:CheckBoxList ID="chklRoles" runat="server" Width="206px">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnAssignRoles" runat="server" Text="Add user" Enabled="false" CssClass="buttons"
                        OnClick="AddUserToRole" />
                    <asp:Button ID="btnRemoveUser" runat="server" Text="Remove user" 
                        Enabled="False" CssClass="buttons"
                        OnClick="RemoveUserFromRole" />
                </td>
            </tr>
        </table>
    </fieldset>
        </div>
    </div>

    

    
</asp:Content>
