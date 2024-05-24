<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddFields.aspx.cs" Inherits="Admin_AddFields" %>
<%@ Register TagPrefix="cc2" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

 <div class="panel panel-primary">
        <div class="panel-heading"><%= currentProcess.Description %> v<%=currentProcess.Version %> - Add Fields - </div>
        <div class="panel-body">
        <table width="100%">
            <tr>
                <td>

                    <table width="100%">
                        <tr>
                            <td valign="top" class="style2">Field Name:
                            </td>
                            <td>

                                <asp:TextBox ID="txtFieldName" runat="server" Width="98%" MaxLength="450"></asp:TextBox>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style2">Label:</td>
                            <td>

                                <asp:TextBox ID="txtDisplayName" runat="server" Width="98%" MaxLength="450"></asp:TextBox>
                            </td>
                        </tr>


                        <tr>
                            <td class="style2">Select Field Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFieldTypeId" runat="server" Width="213px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlQuestionType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="row_scaletypes" runat="server" visible="false">
                            <td class="style2">Select Scale Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlScaleType" runat="server" Width="213px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlScaleType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="row_matrix_dimension" runat="server" visible="false">
                            <td class="style2">Select Dimension:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDimension" runat="server" Width="213px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="row_validation_types" runat="server" visible="false">
                            <td>Select Input Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlInputType" runat="server" Width="213px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="row_lookups" runat="server" visible="false">
                            <td>Select A Lookup:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLookup" runat="server" Width="213px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="row_hierarchy_lookups" runat="server" visible="false">
                            <td>Select A Lookup:</td>
                            <td>
                                <asp:DropDownList ID="ddlHierarchyLookUp" runat="server" Width="213px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr visible="false" runat="server" id="rowMultichoiceOptions">
                            <td></td>
                            <td>
                                <fieldset>
                                    <legend>Add Options</legend>
                                    <table width="40%">
                                        <tr>
                                            <td width="228px">
                                                <table style="border: 0px; padding: 0px;">
                                                    <tr>
                                                        <td>
                                                            <b>Option</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtMultiChoiceQuestionAnswer" runat="server" Width="300px" MaxLength="450"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="margin-left: 40px" width="491px">
                                                <asp:Button ID="btnAddOption" runat="server" Text="Add" CssClass="add-btn" OnClick="btnAddOption_Click"
                                                    Width="60px" />
                                                <asp:Button ID="btnRemove" runat="server" OnClick="btnRemove_Click" CssClass="add-btn"
                                                    Text="Remove" />
                                            </td>
                                            <td valign="top">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="margin-left: 40px" width="491px">
                                                <asp:ListBox ID="lbOptions" runat="server" Width="490px" Height="200px"></asp:ListBox>
                                            </td>
                                            <td valign="top">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2" align="right">Activate/Deactivate:
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIsActive" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td class="style2" align="right">Mandatory Field:
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIsRequired" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style2" align="right">Must Show On Screen:</td>
                            <td>

                                <asp:CheckBox ID="chkShowOnScreen" runat="server" />
                            </td>
                        </tr>
                          <tr runat="server" Visible="true" id="tr1" >
                            <td valign="top" class="style2" align="right">Show only if Incident is Assigned?</td>
                            <td>
                                <asp:CheckBox ID="chkShowOnAssigned" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style2" align="right">Must Show On Reports:</td>
                            <td>

                                <asp:CheckBox ID="chkShowOnReport" runat="server" />
                            </td>
                        </tr>
                        <tr runat="server" Visible="False">
                            <td valign="top" class="style2" align="right">Can Search With:</td>
                            <td>

                                <asp:CheckBox ID="chkShowOnSearch" runat="server" />
                            </td>
                        </tr>
                         <tr runat="server" Visible="true">
                            <td valign="top" class="style2" align="right">Add To Cover Page:</td>
                            <td>

                                <asp:CheckBox ID="chkAddToCoverPage" runat="server" />
                            </td>
                        </tr>
                          <tr runat="server" Visible="False" id="trCreateEmail" >
                            <td valign="top" class="style2" align="right">Send Email ?</td>
                            <td>
                                <asp:CheckBox ID="chkEmailContent" runat="server" AutoPostBack="True" OnCheckedChanged="chkEmailContent_CheckedChanged" />
                            </td>
                        </tr>
                        <tr runat="server" id="trEmailContent" Visible="false">
                                                <td colspan="2">
                                                    <b>Email Body(Excluding header and footer)</b><br/>
                                                    <cc2:HtmlEditor ID="editorEmailContent" runat="server" Width="1200px" Height="500px"
                                                        ToggleMode="None" />
                                                </td>
                                            </tr>
                        <tr>
                        <tr>
                            <td class="style2">&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnAddQuestion" runat="server" Text="Submit" OnClick="btnAddQuestion_Click"
                                    Width="126px" ClientIDMode="AutoID" CssClass="buttons" />
                            </td>
                        </tr>
                    </table>
                    

                </td>
            </tr>
            <tr>
                <td>
                    <fieldset runat="server" ID="fsFields">
                        <asp:GridView ID="gvProcessFields" runat="server" GridLines="Horizontal" Width="100%"
                            AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="FieldId" OnPageIndexChanging="gvQuestions_PageIndexChanging"
                            CssClass="documents" OnRowDataBound="gvQuestions_RowDataBound" EmptyDataText="THERE ARE NO FIELDS FOR THIS PROCESS">
                            <Columns>
                                <asp:TemplateField HeaderText="Field Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFieldName" runat="server" Text='<%# Utils.getshortString(Eval("FieldName").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="SortOrder" HeaderText="Sort Order">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FieldType" HeaderText="Field Type">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:CheckBoxField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Red" />
                        </asp:GridView>
                    </fieldset>
                </td>
            </tr>
        </table>
        </div>
    </div>
</asp:Content>

