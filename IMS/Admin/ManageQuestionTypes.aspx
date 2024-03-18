<%@ Page Title="MODIFY QUESTION TYPES" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="ManageQuestionTypes.aspx.cs" Inherits="Admin_ManageQuestionTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style3
        {
            width: 100%;
            background-color: #dde4ec;
        }
        .style4
        {
            width: 357px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            ATTACH DOCUMENTS</div>
        <div class="panel-body">
            <table width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="dgQuestionTypes" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" DataKeyNames="FieldTypeId" CssClass="documents" OnRowDataBound="GridView1_RowDataBound"
                            GridLines="Horizontal" 
                            OnPageIndexChanging="dgQuestionTypes_PageIndexChanging" PageSize="5">
                            <Columns>
                                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:CheckBoxField>
                                <asp:BoundField DataField="Timestamp" HeaderText="Date Created" SortExpression="Timestamp">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QuestionTypeId" HeaderText="QuestionTypeId" InsertVisible="False"
                                    ReadOnly="True" SortExpression="QuestionTypeId" Visible="False" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button Height="30px" runat="server" CssClass="buttons" ID="btnModify" Text="Modify"
                                            OnClick="Modify" Width="100%" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label Visible="false" runat="server" ID="lblQuestionTypeId" Text='<%# Eval("FieldTypeId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="selectedRow" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr id="row_modify" runat="server" visible="false">
                    <td>
                        <fieldset>
                            <table cellpadding="0" width="100%">
                                <tr>
                                    <td class="style4">
                                        <strong>Question Type Description:</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuesTypeDescr" runat="server" Width="243px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Activate:</strong>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsActive" runat="server" />
                                    </td>
                                </tr>
                                <tr id="row_scale_types" runat="server" visible="false">
                                    <td class="style4" valign="top">
                                        Available scale options
                                    </td>
                                    <td>
                                        <asp:GridView ID="gvAvailableScaleOptions" runat="server" Width="100%" CssClass="documents"
                                            HorizontalAlign="Left" HeaderStyle-CssClass="gvheader" AutoGenerateColumns="False"
                                            GridLines="Horizontal">
                                            <Columns>
                                                <asp:BoundField DataField="Description" HeaderText="Scale Type/Option">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="buttons" OnClick="btnSubmit_Click"
                                            Text="Submit" />
                                        <asp:Button Height="30px" ID="btnAddMoreScales" runat="server" CssClass="buttons"
                                            Text="Add New Scale Options" Visible="False" OnClick="btnAddMoreScales_Click1"
                                            Width="185px" />
                                        <asp:Button Height="30px" ID="btnAddMoreMatrices" runat="server" CssClass="buttons"
                                            Text="Add New Matrix Options" Visible="False" OnClick="btnAddMoreMatrices_Click"
                                            Width="185px" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr visible="false" runat="server" id="row_addnew_button">
                    <td>
                        <asp:LinkButton ID="lnkAddNew" Text="Add New" runat="server" OnClick="AddNewQuestionType"></asp:LinkButton>
                    </td>
                </tr>
                <tr id="row_addnew" runat="server" visible="false">
                    <td>
                        <fieldset>
                            <legend>Add New Question type</legend>
                            <table cellpadding="0" width="100%">
                                <tr>
                                    <td class="style4">
                                        <strong>Question Type Description:</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" Width="243px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Activate:</strong>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" CssClass="buttons" OnClick="Button1_Click"
                                            Text="Submit" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="buttons" OnClick="btnCancel_Click"
                                            Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr id="row_scaleoptions" runat="server" visible="false">
                    <td>
                        <fieldset>
                            <legend>add more scales</legend>
                            <table cellpadding="0" width="100%">
                                <tr>
                                    <td class="style4">
                                        <strong>&nbsp;Description:</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtScaleDescription" runat="server" Width="243px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Type in option and click add:</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtScaleOption" runat="server" Width="142px" MaxLength="30"></asp:TextBox>
                                        <asp:Button ID="btnAddOption" runat="server" CssClass="buttons" Text="Add" OnClick="btnAddOption_Click" />
                                        <asp:Button ID="btnRemoveScale" runat="server" CssClass="buttons" OnClick="btnRemoveScale_Click"
                                            Text="Remove" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4" valign="top">
                                        <strong>Add Scale Options:</strong>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lbScaleOptions" runat="server" Width="244px"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmitNewScaleType" runat="server" CssClass="buttons" Text="Submit"
                                            OnClick="btnSubmitNewScaleType_Click" />
                                        <asp:Button ID="btnCancelAddingMatrix" runat="server" CssClass="buttons" Text="Cancel"
                                            OnClick="btnCancelAddingMatrix_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr id="row_matrix" runat="server" visible="false">
                    <td>
                        <fieldset>
                            <legend>add more two column question types - matix</legend>
                            <table cellpadding="0" width="100%">
                                <tr>
                                    <td class="style4">
                                        <strong>Matrix Description:</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMatrixDescription" runat="server" Width="415px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Column 1 Heading:</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMatrixColumn1Heading" runat="server" Width="415px" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Column 2 Heading:</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMatrixColumn2Heading" runat="server" Width="415px" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4" valign="top">
                                        <strong>Type in option and click add:</strong>
                                    </td>
                                    <td>
                                        <table cellpadding="0">
                                            <tr>
                                                <td>
                                                    <strong>First Column</strong>
                                                </td>
                                                <td>
                                                    <strong>Second Column</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtMatrixOption1" runat="server" Width="142px" MaxLength="30" Height="25px"></asp:TextBox>
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtMatrixOption2" runat="server" Width="142px" MaxLength="30" Height="25px"></asp:TextBox>
                                                </td>
                                                <td valign="top">
                                                    <asp:Button ID="btnAddMatrixOption" runat="server" CssClass="buttons" Text="Add"
                                                        OnClick="btnAddMatrixOption_Click" Height="32px" />
                                                    <asp:Button ID="btnRemoveMatrixOption" runat="server" CssClass="buttons" OnClick="btnRemoveMatrixOption_Click"
                                                        Text="Remove" Width="64px" Height="32px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4" valign="top">
                                        <strong>AddMatrix Options:</strong>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lbMatrixOptions" runat="server" Width="417px"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmitMatrix" runat="server" CssClass="buttons" Text="Submit"
                                            OnClick="btnSubmitMatrix_Click" />
                                        <asp:Button ID="btnCancelMatrix" runat="server" CssClass="buttons" Text="Cancel"
                                            OnClick="btnCancelMatrix_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
