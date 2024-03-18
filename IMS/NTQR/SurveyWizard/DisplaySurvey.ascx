<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DisplaySurvey.ascx.cs" Inherits="ntqr_SurveyWizard_DisplaySurvey" %>
<%@ Register Src="QuestionDisplay.ascx" TagName="QuestionDisplay" TagPrefix="uc1" %>
<%@ Register Src="MatrixQuestion.ascx" TagName="MatrixQuestion" TagPrefix="uc2" %>
<%@ Register Src="HierarchicalLookup.ascx" TagName="HierarchicalLookup" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/UserSelector.ascx" TagPrefix="uc1" TagName="UserSelector" %>




    <table style="padding: 1px; width: 100%;">
        <tr>
            <td>
                <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="False" GridLines="None" DataKeyNames="FieldId, IsChild,ParentId, IsParent"
                    ShowHeader="False" Width="100%" OnRowDataBound="RowDataBound">
                    <Columns>
                        <asp:TemplateField Visible="true">
                            <ItemTemplate>
                                <uc1:QuestionDisplay ID="QuestionDisplay1" runat="server" OnOptionChanged="OptionChanged"   />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>

    </table>
    

