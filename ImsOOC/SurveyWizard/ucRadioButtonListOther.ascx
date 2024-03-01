<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRadioButtonListOther.ascx.cs"
    Inherits="RadioButtonListOther" %>
<table width="100%">
    <tr>
        <td>
            <fieldset>
               
                <table>
                    <tr>
                        <td valign="top" style="padding-left: 20px;">
                            <asp:RadioButtonList ID="rbtnQuestionAnswers" runat="server" CellPadding="0" 
                                CellSpacing="1"
                                ViewStateMode="Enabled" Width="100%" ForeColor="Gray" 
                                CssClass="question-block" 
                                onselectedindexchanged="SelectedIndexChanged" AutoPostBack="True">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td valign="top" style="padding-left: 20px;">
                        <asp:TextBox ID="txtOther" runat="server" Width="300px" 
                            MaxLength="500" CssClass="question-block" ForeColor="Gray" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
