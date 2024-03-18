<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionDisplay.ascx.cs" Inherits="NTQR_SurveyWizard_QuestionDisplay" %>
<%@ Register Src="MatrixQuestion.ascx" TagName="MatrixQuestion" TagPrefix="uc1" %>
<%@ Register Src="HierarchicalLookup.ascx" TagName="HierarchicalLookup" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.51116.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="Sars.Systems.Controls" Namespace="Sars.Systems.Controls" TagPrefix="sars" %>
<%@ Register Src="../../Admin/UserSelector.ascx" TagName="UserSelector" TagPrefix="uc2" %>

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
<div class="form-group has-info" id="row_ddl_other" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%></label>

    <div class="col-xs-12 col-sm-5">
        <span class="block input-icon input-icon-right"></span>
    </div>

</div>
<div class="form-group has-info" id="row_check_box_list_other" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%></label>

    <div class="col-xs-12 col-sm-5">
        <span class="block input-icon input-icon-right">

            <sars:CheckBoxListWithOther ID="CheckBoxListWithOther1" runat="server" CellPadding="1" CellSpacing="1" OtherFieldWidth="250" />

        </span>
    </div>

</div>
<div class="form-group has-info" id="row_radio_button_list_other" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%></label>

    <div class="col-xs-12 col-sm-5">
        <span class="block input-icon input-icon-right">
            <sars:RadioButtonListWithOther AutoPostBack="True" ID="RadioButtonListWithOther1" runat="server" CellPadding="1" CellSpacing="1" OtherFieldWidth="250" />

        </span>
    </div>

</div>

<div class="form-group has-info" id="row_scale" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%></label>

    <div class="col-xs-12 col-sm-5">
        <span class="block input-icon input-icon-right">
            <asp:RadioButtonList ID="rbtnQuestionAnswers" runat="server" CellPadding="0"
                CellSpacing="1"
                ViewStateMode="Enabled" ForeColor="Gray"
                CssClass="ace" AutoPostBack="True"
                OnSelectedIndexChanged="rbtnQuestionAnswers_SelectedIndexChanged">
            </asp:RadioButtonList>
        </span>
    </div>

</div>
<div class="form-group has-info" id="row_multi_choice" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%></label>

    <div class="col-xs-12 col-sm-5">
        <span class="block input-icon input-icon-right">
            <asp:CheckBoxList ID="chkbQuestionAnswers" runat="server" CellPadding="0" CellSpacing="1"
                RepeatDirection="Vertical" Width="100%" CssClass="ace"
                ForeColor="Gray" AutoPostBack="True"
                OnSelectedIndexChanged="chkbQuestionAnswers_SelectedIndexChanged">
            </asp:CheckBoxList>
        </span>
    </div>

</div>
<div class="form-group has-info" id="row_free_text" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%></label>

    <div class="col-xs-12 col-sm-5">
        <span class="block input-icon input-icon-right">
            <asp:TextBox ID="txtQuestionAnswer" runat="server" MaxLength="500" CssClass="width-100" />

        </span>
    </div>

</div>
<div class="form-group has-info" id="row_dropdown_choice" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%></label>

    <div class="col-xs-12 col-sm-5">
        <span class="block input-icon input-icon-right">
            <asp:DropDownList ID="ddlQuestionAnswers" runat="server"
                 CssClass="width-100" ForeColor="black" AutoPostBack="True"
                OnSelectedIndexChanged="ddlQuestionAnswers_SelectedIndexChanged">
            </asp:DropDownList>

        </span>
    </div>

</div>
<div class="form-group has-info" id="row_matrix" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%></label>

    <div class="col-xs-12 col-sm-5">
        <span class="block input-icon input-icon-right">
            <uc1:MatrixQuestion ID="matrix" runat="server" />

        </span>
    </div>

</div>
<div class="form-group has-info" id="row_hierarchy_lookup" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%></label>

    <div class="col-xs-12 col-sm-5">
        <span class="block input-icon input-icon-right">
            <uc1:HierarchicalLookup ID="HierarchicalLookup1" runat="server" />

        </span>
    </div>

</div>
<div class="form-group has-info" id="row_comment" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%><span style="font-style: italic; font-size: small; color: green;">(500 characters)</span></label>

    <div class="col-xs-12 col-sm-5">
        <span class="block input-icon input-icon-right">
            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine"
                onkeyup="CountChars(this, 500)" Height="100px" CssClass="width-100"
                ForeColor="black"></asp:TextBox>

        </span>
    </div>

</div>
<div class="form-group has-info" id="row_date" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%></label>

    <div class="col-xs-12 col-sm-5">

        <div class="input-group">
            <asp:TextBox ID="txtDate" runat="server" MaxLength="10"
                ForeColor="black" CssClass="form-control date-picker" />
            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                Enabled="True" TargetControlID="txtDate">
            </cc1:CalendarExtender>
           <%-- <span class="input-group-addon">
                <i class="fa fa-calendar bigger-110"></i>
            </span>--%>

        </div>

    </div>

</div>
<div class="form-group has-info" id="row_lists" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%></label>

    <div class="col-xs-12 col-sm-5">

        <table cellpadding="0" style="width: 100%;" runat="server" id="tblLists">
            <tr>
                <td style="width: 200px;">
                    <asp:TextBox ID="txtListOption" runat="server" Width="270px" MaxLength="200"
                        CssClass="width-100" ForeColor="black" Height="30px" />
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


    </div>

</div>
<div class="form-group has-info" id="row_AD_user" runat="server" visible="false">
    <label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right"><%=QuesDisplay%></label>

    <div class="col-xs-12 col-sm-5">

        <uc2:UserSelector ID="UserSelector1" runat="server" />



    </div>

</div>
