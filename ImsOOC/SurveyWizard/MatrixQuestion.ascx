<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MatrixQuestion.ascx.cs"
    Inherits="SurveyWizard_MatrixQuestion" %>
<table cellpadding="1" style="width: 100%" cellspacing="1">
    <tr>
        <td>
            <table style="border: 1px solid #4b6c9e; border-collapse: collapse; width: 100%;">
                <tr class="questionHeaders">                  
                    <th align="left">
                        <%= MatrixLeftHeader %>
                    </th>
                    <th align="left">
                        <%= MatrixRightHeader%>
                    </th>
                </tr>
                <tr>
                    
                    <td align="left">
                        <asp:RadioButtonList ID="rbtnLeft" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Width="100%" CellPadding="1" 
                            CellSpacing="1" CssClass="question-block" ForeColor="Gray">
                        </asp:RadioButtonList>
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rbtnRight" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Width="100%" CellPadding="1" 
                            CellSpacing="1" CssClass="question-block" Font-Overline="False" 
                            ForeColor="Gray">
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
