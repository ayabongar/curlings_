<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LookupdataManager.aspx.cs" Inherits="SystemAdmins_LookupdataManager" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            CREATE NEW LOOKUPS</div>
        <div class="panel-body">
        <table cellpadding="0" class="style4">
          
            <tr>
                <td>
                    Lookup Name/Description:
                </td>
                <td><fieldset><asp:TextBox ID="txtLookupdataDescription" runat="server" Width="280px"></asp:TextBox></fieldset>
                    
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
                                    <table>
                                        <tr>
                                            <td>
                                                 <asp:TextBox ID="txtLKPOption" runat="server" Width="280px" MaxLength="50" Height="24px"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                   
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
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
        </table>
        </div>
    </div>

    
</asp:Content>
