<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CheckParameters.aspx.cs" Inherits="CheckParameters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server" id="dvNames"></div>
    <asp:Repeater runat="server" ID="rptValues">
        <HeaderTemplate>
            <table class="documents">
                <tr>
                    <th>HTTP KEY</th>
                    <th>CURRENT HTTP REQIEST VALUE</th>
                </tr>
           
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("Key") %></td>
               <td><%# Eval("Value")%></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

