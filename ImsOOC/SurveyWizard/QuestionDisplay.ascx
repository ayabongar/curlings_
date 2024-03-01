<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionDisplay.ascx.cs" Inherits="SurveyWizard_QuestionDisplay" %>
<%@ Register Src="MatrixQuestion.ascx" TagName="MatrixQuestion" TagPrefix="uc1" %>
<%@ Register Src="HierarchicalLookup.ascx" TagName="HierarchicalLookup" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.51116.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="Sars.Systems.Controls" Namespace="Sars.Systems.Controls" TagPrefix="sars" %>
<%@ Register src="../Admin/UserSelector.ascx" tagname="UserSelector" tagprefix="uc2" %>

<style type="text/css">
    .ques-display {
        width: 50%;
        vertical-align: top;
        margin: 5px;
        text-transform: capitalize;
    }

    .ques-choices {
        width: 50%;
        vertical-align: top;
        margin: 10px;
    }

    .tbl-question {
        width: 100%;
    }
    .auto-style3 {
        width: 50%;
        vertical-align: top;
        margin: 10px;
        height: 30px;
    }
</style>
<script type="text/javascript">
    function CountChars(field, maxlimit) {
        if (field.value.length > maxlimit) {
            field.value = field.value.substring(0, maxlimit);
        }
    }
</script>
<script src="../Scripts/_validation.js"></script>
<table width="100%">
    <tr id="row_ddl_other" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display">
                        <%=QuesDisplay%>
                    </td>
                    <td class="ques-choices"></td>
                </tr>

            </table>

        </td>
    </tr>





    <tr id="row_check_box_list_other" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display">
                        <%=QuesDisplay%>
                    </td>
                    <td class="auto-style3">
                        <sars:CheckBoxListWithOther ID="CheckBoxListWithOther1" runat="server" CellPadding="1" CellSpacing="1" OtherFieldWidth="250" />
                    </td>
                </tr>

            </table>
        </td>
    </tr>

    <tr id="row_radio_button_list_other" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display">
                        <%=QuesDisplay%>
                    </td>
                    <td class="ques-choices">
                        <sars:RadioButtonListWithOther AutoPostBack="True" ID="RadioButtonListWithOther1" runat="server" CellPadding="1" CellSpacing="1" OtherFieldWidth="250" />
                    </td>
                </tr>

            </table>
        </td>
    </tr>


    <tr id="row_scale" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display">
                        <%=QuesDisplay%>
                    </td>
                    <td class="ques-choices">
                        <asp:RadioButtonList ID="rbtnQuestionAnswers" runat="server" CellPadding="0"
                            CellSpacing="1"
                            ViewStateMode="Enabled" Width="100%" ForeColor="Gray"
                            CssClass="question-block" AutoPostBack="True" 
                            onselectedindexchanged="rbtnQuestionAnswers_SelectedIndexChanged">
                        </asp:RadioButtonList>
                    </td>
                </tr>

            </table>
        </td>
    </tr>
    <tr id="row_multi_choice" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display">
                        <%=QuesDisplay%>
                            
                    </td>
                    <td class="ques-choices">
                        <asp:CheckBoxList ID="chkbQuestionAnswers" runat="server" CellPadding="0" CellSpacing="1"
                            RepeatDirection="Vertical" Width="100%" CssClass="question-block"
                            ForeColor="Gray" AutoPostBack="True" 
                            onselectedindexchanged="chkbQuestionAnswers_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="row_free_text" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display"><%=QuesDisplay%>
                       
                            
                             
                    </td>
                    <td class="ques-choices">
                        <p>
                            <asp:TextBox ID="txtQuestionAnswer" runat="server" Width="300px" MaxLength="500" CssClass="question-block" Height="30px" />
                        </p>


                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="row_dropdown_choice" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display">
                        <%=QuesDisplay%>
                    </td>
                    <td class="ques-choices">
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlQuestionAnswers" runat="server" Width="315px" 
                                        CssClass="question-block" ForeColor="black" AutoPostBack="True" Height="30px" 
                                        onselectedindexchanged="ddlQuestionAnswers_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>

            </table>
        </td>
    </tr>
    <tr id="row_matrix" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display">
                        <%=QuesDisplay%>
                    </td>
                    <td class="ques-choices">
                        <uc1:MatrixQuestion ID="matrix" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="row_hierarchy_lookup" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display">
                        <%=QuesDisplay%>
                    </td>
                    <td class="ques-choices">
                        <uc1:HierarchicalLookup ID="HierarchicalLookup1" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="row_comment" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display">
                        <%=QuesDisplay%> <span style="font-style: italic; font-size: small; color: green;">(500 characters)</span> 
                    </td>
                    <td class="ques-choices">
                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="99%"
                            onkeyup="CountChars(this, 500)" Height="200px" CssClass="question-block"
                            ForeColor="black"></asp:TextBox>
                    </td>
                </tr>
            </table>

        </td>
    </tr>



    <tr id="row_date" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display">
                        <%=QuesDisplay%>
                    </td>
                    <td class="ques-choices">
                        <asp:TextBox ID="txtDate" runat="server" Width="150px" MaxLength="10"
                            ForeColor="black" Height="30px" />
                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="yyyy-MM-dd hh:mm" 
                            Enabled="True" TargetControlID="txtDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="row_lists" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display">
                        <%=QuesDisplay%>
                    </td>
                    <td class="ques-choices">
                        <table cellpadding="0" style="width: 100%;" runat="server" id="tblLists">
                            <tr>
                                <td style="width: 200px;">
                                    <asp:TextBox ID="txtListOption" runat="server" Width="270px" MaxLength="200"
                                        CssClass="question-block" ForeColor="black" Height="30px" />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Button CssClass="buttons" ID="btnAddListOption" runat="server" Text="Add"
                                        OnClick="btnAddListOption_Click" Height="30px" Width="35px" />
                                </td>
                            </tr>
                             <tr>
        <td colspan="2" id="userContainer" runat="server"></td>
    </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvListItems" runat="server" GridLines="Horizontal"
                                        CssClass="documents" Width="100%" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="85%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnRemoveListItem" OnClick="RemoveListItem" runat="server" Text="Remove">                                                        
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListItemId" runat="server" Text='<%# Eval("ListItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblQuestionId" runat="server" Text='<%# Eval("FieldId") %>'></asp:Label>
                                                    <asp:Label ID="lblResponseId" runat="server" Text='<%# Eval("IncidentId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
        </td>
    </tr>
    
     <tr id="row_AD_user" runat="server" visible="false">
        <td>
            <table width="100%" class="tbl-question">
                <tr>
                    <td class="ques-display">
                        <%=QuesDisplay%>
                    </td>
                    <td class="ques-choices">
                        <uc2:UserSelector  ID="UserSelector1" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>

</table>
