<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDropDownOther.ascx.cs"
    Inherits="SurveyWizard_ucDropDownOther" %>
<table width="100%">
    <tr>
        <td>
            <fieldset>
               
                <table width="100%">
                    <tr>
                        <td style="width: 40%; padding-top: 4px;" valign="top">
                            <asp:Label ID="lblQuestionDescriptionDropDown" runat="server" Font-Bold="False" 
                                CssClass="question-block" ForeColor="Gray"></asp:Label>
                        </td>
                        <td valign="top" style="padding-left: 20px;">
                            <asp:DropDownList ID="ddlQuestionAnswers" runat="server" Width="315px"
                                OnSelectedIndexChanged="ddlQuestionAnswers_SelectedIndexChanged" 
                                CssClass="question-block" ForeColor="Gray">
                            </asp:DropDownList>
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
                    <td style="width: 40%;" valign="top">
                        Other, Specify</td>
                    <td valign="top" style="padding-left: 20px;">
                        <asp:TextBox ID="txtOther" runat="server" Width="300px" 
                            MaxLength="500" CssClass="question-block" ForeColor="Gray" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
