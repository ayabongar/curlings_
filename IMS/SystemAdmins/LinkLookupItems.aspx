<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LinkLookupItems.aspx.cs" Inherits="SystemAdmins_LinkLookupItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            CREATE NEW LOOKUPS</div>
        <div class="panel-body">
            <fieldset>
        <table cellpadding="0" class="style4">
          
            <tr>
                <td>
                    Parent Lookup Item:
                </td>
                <td><fieldset><asp:TextBox ID="txtLookupdataDescription" runat="server" Width="280px" Enabled="False" Font-Bold="True"></asp:TextBox></fieldset>
                    
                </td>
            </tr>
            <tr>
                <td valign="top">
                    &nbsp;</td>
                <td>
                    <fieldset>
                        <legend>Add lookup items</legend>
                        <table width="40%">
                            <tr>
                                <td width="228px">
                                    <asp:TextBox ID="txtLKPOption" runat="server" Width="280px" MaxLength="50" Height="24px"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 40px" width="491px">
                                    <asp:Button ID="btnAddOption" runat="server" Text="Add" CssClass="buttons" OnClick="btnAddOption_Click"
                                        Width="60px" />
                                    <asp:Button ID="btnRemove" runat="server" OnClick="btnRemove_Click" CssClass="buttons"
                                        Text="Remove" />
                                </td>
                                <td valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 40px" width="491px">
                                    <asp:ListBox ID="lbOptions" runat="server" Width="405px" Height="115px"></asp:ListBox>
                                </td>
                                <td valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="buttons" Text="Submit" OnClick="btnSubmit_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </fieldset>
        </div>
    </div>
    
</asp:Content>

