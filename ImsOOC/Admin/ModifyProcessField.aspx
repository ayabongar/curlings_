<%@ Page Title="::MODIFY PROCESS::" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ModifyProcessField.aspx.cs" Inherits="Admin_ModifyProcessField" %>
<%@ Register TagPrefix="cc2" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>
<%@ Register src="~/Admin/UserSelector.ascx" tagname="UserSelector" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   
        <div class="panel panel-primary">
        <div class="panel-heading"><%= currentProcess.Description %> v<%=currentProcess.Version %> - Modify Fields -</div>
        <div class="panel-body">

        <table width="100%">
            <tr>
                <td>

                    <table width="100%">
                        <tr>
                            <td valign="top" class="style2">Field Name:
                            </td>
                            <td>

                                <asp:TextBox ID="txtFieldName" runat="server" Width="799px" MaxLength="450"></asp:TextBox>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style2">Label:</td>
                            <td>

                                <asp:TextBox ID="txtDisplayName" runat="server" Width="799px"></asp:TextBox>
                            </td>
                        </tr>


                        <tr>
                            <td class="style2">Select Question Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFieldType" runat="server" Width="213px" AutoPostBack="True"
                                    OnSelectedIndexChanged="SelectQuestionsType">
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
                        <tr id="row_User" runat="server" visible="false">
                            <td>Default User:</td>
                            <td> 
                                <uc1:UserSelector ID="UserSelector1" runat="server" />
                              </td>
                        </tr>
                        <tr visible="false" runat="server" id="rowMultichoiceOptions">
                            <td></td>
                            <td>
                                <div class="page-header"style="width: 800px;">Add Options</div>
                                <div style="width: 800px;" class="roundeddiv">
                                    <table width="100%">
                                        <tr>
                                            <td width="228px">
                                                <table style="border: 0px; padding: 0px;">
                                                    <tr>
                                                        <td><b>Option</b></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtMultiChoiceQuestionAnswer" runat="server" Width="300px"
                                                                MaxLength="450"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnAddOption" runat="server" Text="Add" CssClass="add-btn"
                                                                OnClick="AddOption" EnableViewState="False" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="margin-left: 40px">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:ListBox ID="lbOptions" runat="server" Width="100%" Visible="False"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="gvOptions" runat="server" Width="100%" CssClass="documents" HorizontalAlign="Left" AutoGenerateColumns="False" GridLines="Horizontal">
                                                                <Columns>
                                                                    <asp:BoundField DataField="OptionDescription" HeaderText="Option">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMultichoiceOptionId" runat="server" Text='<%# Eval("MultichoiceOptionId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkUpdate" runat="server" Text="Modify" OnClick="lnkUpdate_Click"
                                                                                CssClass="buttons"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" OnClick="lnkRemove_Click"
                                                                                CssClass="buttons"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr id="row_modifyOption" runat="server" visible="false">
                                                        <td>
                                                            <table style="border: 0px; padding: 0px;">
                                                                <tr>
                                                                    <td><b>Option</b></td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOption" runat="server" Width="300px"
                                                                            MaxLength="450"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnModifyOption" runat="server" Text="Save" CssClass="add-btn"
                                                                            OnClick="ModifyOption" EnableViewState="False" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
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
                        <tr runat="server" Visible="true">
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
                      
                        <tr runat="server" id="trEmailContent" Visible="true">
                                                <td colspan="2">
                                                    <b>Email Body(Excluding header and footer)</b><br/>
                                                    <cc2:HtmlEditor ID="editorEmailContent" runat="server" Width="1200px" Height="500px"
                                                        ToggleMode="None" />
                                                </td>
                                            </tr>
                        <tr>
                            <td class="style2">&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnAddQuestion" runat="server" Text="Submit" OnClick="SaveField"
                                    Width="126px" ClientIDMode="AutoID" CssClass="buttons" Height="30px" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"
                                    Width="126px" ClientIDMode="AutoID" CssClass="buttons" Height="30px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
         </div>
    </div>
</asp:Content>

