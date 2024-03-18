<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TestUserControls.aspx.cs" Inherits="TestUserControls" %>

<%@ Register src="Admin/UserSelector.ascx" tagname="UserSelector" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table>
        <tr>
            <td><uc1:UserSelector ID="UserSelector1" runat="server" /></td>
        </tr>
        
        <tr>
            <td style="padding-left: 30px;">
                <asp:Button runat="server" ID="ShowFullName" Text="Show Full Name" Width="200px" OnClick="ShowFullName_Click"/>
            </td>
        </tr>
         <tr>
            <td style="padding-left: 30px;">
                <asp:Button runat="server" ID="ShowSID" Text="Show Full SID" Width="200px" OnClick="ShowSID_Click"/>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 30px;">
                <asp:Button runat="server" ID="ShowAll" Text="Show Full SID" Width="200px" OnClick="ShowAll_Click"/>
            </td>
        </tr>
    </table>
    
</asp:Content>

