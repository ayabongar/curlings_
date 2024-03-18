<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AnswerAgreeDisagree.ascx.cs"
    Inherits="SurveyWizard_AnswerAgreeDisagree" %>
<style type="text/css">
    .style1
    {
        width: 93%;
        background-color: #dde4ec;
        padding: 2px;
        table-layout: fixed;
        border: 1px solid #000000;
    }
    .style3
    {
        width: 54%;
    }
</style>
<div>
    <asp:GridView ID="gvQuestions" runat="server" BorderStyle="Solid"
        HeaderStyle-HorizontalAlign="Left" HorizontalAlign="Left" AutoGenerateColumns="False"
        CaptionAlign="Left" CellPadding="0" Width="100%" 
        onrowdatabound="gvQuestions_RowDataBound">
        <RowStyle HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblquestionType" runat="server" Text='<%# Eval("QuestionTypeId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblQuestionsId" runat="server" Text='<%# Eval("QuestionsId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblSectionId" runat="server" Text='<%# Eval("SectionId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblSubsectionId" runat="server" Text='<%# Eval("SubsectionId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

                <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblQuestionnaireId" runat="server" Text='<%# Eval("QuestionnaireId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="Description" HeaderText="QUESTION" HeaderStyle-HorizontalAlign="Left"
                ItemStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Strongly Agree" HeaderStyle-HorizontalAlign="Left"
                ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:RadioButton ID="rdbtnStrongAgree" value="1" runat="server" GroupName="agree-disagree" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Agree" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:RadioButton ID="rdbtnAgree" runat="server" GroupName="agree-disagree" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Neither Agree nor Disagree" HeaderStyle-HorizontalAlign="Left"
                ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:RadioButton ID="rdbtnNeither" runat="server" GroupName="agree-disagree" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Disagree" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:RadioButton ID="rdbtnDisagree" runat="server" GroupName="agree-disagree" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Strongly Disagree" HeaderStyle-HorizontalAlign="Left"
                ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:RadioButton ID="rdbtnStrongDisagree" runat="server" GroupName="agree-disagree" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Left" Font-Size="Small"></HeaderStyle>
        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPrevious" NextPageText="Next"
            PreviousPageText="Previous" />
        <PagerStyle BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True" ForeColor="#666699" />
        <RowStyle HorizontalAlign="Left" Font-Size="Smaller" />
    </asp:GridView>
</div>
