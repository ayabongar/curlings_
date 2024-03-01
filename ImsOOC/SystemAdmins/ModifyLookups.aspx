<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ModifyLookups.aspx.cs" Inherits="SystemAdmins_ModifyLookups" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
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
           MAINTAIN SURVEY LOOKUPS</div>
        <div class="panel-body">
            
    <table cellpadding="0" width="100%">
        <tr>
            <td>
                <fieldset>
                    <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="ToolBarClicked"
                        EnableClientApi="False" CssClass="toolbar" Width="99%">
                        <Items>
                            <SCS:ToolbarButton CausesValidation="True" CommandName="Modify" Text="Modify" />
                            <SCS:ToolbarButton CausesValidation="True" CommandName="AddLookupItem" Text="Add Lookup Item" />
                            <SCS:ToolbarButton CausesValidation="True" CommandName="ViewLookupItems" Text="View Lookup Items" />
                            <SCS:ToolbarButton CausesValidation="True" CommandName="ViewLookupItems" Text="Modify Lookup Items"
                                Visible="false" />
                        </Items>
                        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                            CssClassDisabled="button_disabled"></ButtonCssClasses>
                    </SCS:Toolbar>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <fieldset>
                    <asp:GridView ID="gvLookpus" runat="server" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" CssClass="documents" OnRowDataBound="RowDataBound" 
                        OnPageIndexChanging="PageIndexChanging" 
                        onselectedindexchanged="SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:CheckBoxField>
                            <asp:BoundField DataField="Timestamp" HeaderText="Date Created" SortExpression="Timestamp">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label Visible="false" runat="server" ID="lblLookupDataId" Text='<%# Eval("LookupDataId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Left" />
                        <SelectedRowStyle CssClass="selectedRow" />
                    </asp:GridView>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr runat="server" id="row_modify" visible="false">
            <td> <fieldset>
                <table cellpadding="0" width="100%">
                    <tr>
                        <td>
                            Lookup Name/Description:
                        </td>
                        <td>
                            <fieldset>
                                <asp:TextBox ID="txtLookupdataDescription" runat="server" Width="280px" 
                                    MaxLength="100"></asp:TextBox></fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Activate/Deactivate:
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsActive" runat="server" />
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
            </td>
        </tr>
        <tr runat="server" visible="false" id="row_addItems">
            <td colspan="2">
                <fieldset>
                    <legend>Add lookup items</legend>
                    <table width="50%">
                        <tr>
                            <td width="228px">
                                <asp:TextBox ID="txtLKPOption" runat="server" Height="24px" MaxLength="150" 
                                    Width="280px"></asp:TextBox>
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 40px" width="491px">
                                <asp:Button ID="btnAddOption" runat="server" CssClass="buttons" OnClick="AddOption"
                                    Text="Add" Width="60px" />
                                <asp:Button ID="btnRemove" runat="server" CssClass="buttons" OnClick="Remove"
                                    Text="Remove" />
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 40px" width="491px">
                                <asp:ListBox ID="lbOptions" runat="server" Height="115px" Width="405px"></asp:ListBox>
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 40px" width="491px">
                                <asp:Button ID="btnAddItem" runat="server" CssClass="buttons" Text="Submit" OnClick="AddItem" />
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>


        <tr runat="server" visible="false" id="row_view_items">
            <td colspan="2">
            <fieldset>
                    <asp:GridView ID="gvItems" runat="server" Width="100%" 
                        AutoGenerateColumns="False" CssClass="grid" 
                    OnRowDataBound="ItemsRowDataBound" 
                        onselectedindexchanged="ItemsSelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:CheckBoxField>
                            <asp:BoundField DataField="Timestamp" HeaderText="Date Created" SortExpression="Timestamp">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label Visible="false" runat="server" ID="lblLookupItemId" 
                                        Text='<%# Eval("LookupItemId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gvheader" />
                        <PagerStyle HorizontalAlign="Left" />
                        <SelectedRowStyle CssClass="selectedRow" />
                    </asp:GridView>
            </fieldset>
            </td>
            </tr>
            <tr id="row_modify_item" runat="server" visible="false">
                <td>
                    <fieldset>
                        <table cellpadding="0" class="style5">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Description:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLookupItemDescription" runat="server" Width="280px" 
                                        Height="24px" MaxLength="150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Activate/Deactivate:
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsActiveItem" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnModifyItem" runat="server" CssClass="buttons" Text="Submit" OnClick="Modify" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>

    </table>
  
        </div>
    </div>
    

    
</asp:Content>
