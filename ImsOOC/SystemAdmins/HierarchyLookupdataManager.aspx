<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="HierarchyLookupdataManager.aspx.cs" Inherits="SystemAdmins_HierarchyLookupdataManager" %>

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
            CREATE NEW HIERARCHICAL LOOKUPS</div>
        <div class="panel-body">
      
       
        <table cellpadding="0" class="style4">
          
            <tr runat="server" ID="row_parent_lookup" Visible="False">
                <td>
                    <strong>Parent Look up details:</strong></td>
                <td>
                    <asp:Label runat="server" ID="lblParentLookUpDetails" Font-Bold="True" Font-Size="Medium" ForeColor="#0066FF"></asp:Label>
                    
                </td>
            </tr>
          
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
                                <td >
                                     <table width="100%">
                                        <tr runat="server" ID="row_parent_drop_downs" Visible="False"><td>Select Parent</td>
                                            <td>
                                                <asp:DropDownList ID="ddlParentLookUps" runat="server" Width="280px" Height="24px"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr><td>Option:</td>
                                            <td><asp:TextBox ID="txtLKPOption" runat="server" Width="280px" MaxLength="50" Height="24px"></asp:TextBox></td>
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
                    <asp:Button ID="btnSubmit" runat="server" CssClass="buttons" Text="Submit" OnClick="SubmitLookUps" />
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
 
        </div>
    </div>
    
</asp:Content>
