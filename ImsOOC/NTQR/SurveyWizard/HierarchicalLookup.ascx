<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HierarchicalLookup.ascx.cs" Inherits="SurveyWizard_HierarchicalLookup" %>
<table>
    <tr runat="server" ID="row_level1" Visible="True">
        <td>
            <asp:DropDownList runat="server" ID="ddlLevel1" Width="315px" CssClass="question-block" ForeColor="Gray" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel1_SelectedIndexChanged" Height="30px"/>
        </td>
    </tr>
    <tr runat="server" ID="row_level2" Visible="False">
        <td>
            <asp:DropDownList runat="server" ID="ddlLevel2" Width="315px" CssClass="question-block" ForeColor="Gray"  AutoPostBack="True" OnSelectedIndexChanged="ddlLevel2_SelectedIndexChanged" Height="30px"/>
        </td>
    </tr>
    <tr runat="server" ID="row_level3" Visible="False">
        <td>
            <asp:DropDownList runat="server" ID="ddlLevel3" Width="315px" CssClass="question-block" ForeColor="Gray"  AutoPostBack="True" Height="30px"/>
        </td>
    </tr>
</table>