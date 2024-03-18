<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReportErrors.aspx.cs" Inherits="SystemAdmins_ReportErrors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        .style4
        {
            width: 268px;
        }
         .surveyHeader
        {
            font-family: 'Courier New' , Courier, monospace;
            font-size: large;
            background-color: #4b6c9e;
            color: #FFFFFF;
            text-transform: uppercase;
            font-weight: bold;
        }
         .surveyStyle
        {
            width: 100%;
            border: 1px solid #4b6c9e;
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
    <table cellpadding="0" class="surveyStyle" width="100%">
        <tr class="surveyHeader">
            <td colspan="2"> Report your errors/bugs/problems</td>
        </tr>
        <tr>
            <td class="style4">
                Survey Name:</td>
            <td>
                <asp:TextBox ID="txtsurveyName" runat="server" Width="425px" MaxLength="150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4" valign="top">
                Description Of Your Error:</td>
            <td>
                <asp:TextBox ID="txtErrorDescription" runat="server" Height="136px" 
                    TextMode="MultiLine" Width="425px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4" valign="top">
                &nbsp;</td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="buttons" 
                    onclick="btnSubmit_Click" Text="Submit" Width="92px" />
            </td>
        </tr>
    </table>
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

