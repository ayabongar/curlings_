<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TextGrid.ascx.cs" Inherits="SurveyWizard_TextGrid" %>
<%@ Register TagPrefix="SCS" Namespace="SCS.Web.UI.WebControls" Assembly="SCS.Web.UI.WebControls.Toolbar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2c7b5da5d964392f" %>

<link href="../Styles/toolBars.css" rel="stylesheet" type="text/css" />

<style type="text/css">
    .style1
    {
        width: 100%;
    }
</style>

<link href="../Styles/survey.css" rel="stylesheet" type="text/css" />

<table style="width:100%;">
    <tr>
        <td>
             <fieldset>
                 <scs:toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"  EnableClientApi="False" CssClass="toolbar" Width="99%">
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Rows" Text="Add Row Headings" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Columns" Text="Add Column Headings" />                        
                    </Items>
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
                </scs:toolbar>
            </fieldset>
        </td>
    </tr>
    <tr id="row_add_rows" runat="server" visible="true">
        <td><fieldset>
            <table width="100%">
                <tr>
                    <td style="width:350px;">
                        <asp:TextBox ID="txtRowDescription" runat="server" Width="350px" MaxLength="150"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnAddRow" runat="server" Text="Add" CssClass="buttons" 
                            onclick="btnAddRow_Click" />
                        <asp:Button ID="btnRemoveRow" runat="server" Text="Remove" CssClass="buttons" 
                            onclick="btnRemoveRow_Click" />
                        <asp:Button ID="btnSaveRows" runat="server" Text="Save" 
                            CssClass="buttons" onclick="btnSaveRows_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ListBox ID="lbRows" runat="server" Width="100%"></asp:ListBox>
                    </td>
                </tr>
            </table></fieldset>
        </td>
    </tr>
    <tr id="row_add_columns" runat="server" visible="false">
        <td>
        
        <fieldset>
        <table width="100%">
                <tr>
                    <td style="width:350px;">
                        <asp:TextBox ID="txtColumnDescription" runat="server" Width="350px" MaxLength="150"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnAddColumn" runat="server" Text="Add" CssClass="buttons" 
                            onclick="btnAddColumn_Click" />
                        <asp:Button ID="btnRemoveColumn" runat="server" Text="Remove" 
                            CssClass="buttons" onclick="btnRemoveColumn_Click" />
                        <asp:Button ID="btnSaveColumns" runat="server" Text="Save" 
                            CssClass="buttons" onclick="btnSaveColumns_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ListBox ID="lbColumns" runat="server" Width="100%"></asp:ListBox>
                    </td>
                </tr>
            </table>
        
        </fieldset>
        
        </td>
    </tr>
</table>

