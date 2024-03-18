<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ViewFields.aspx.cs" Inherits="Admin_ViewFields" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/boxover.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            VIEW PROCESS FIELDS</div>
        <div class="panel-body">
            <fieldset>
                <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"
                    EnableClientApi="False" CssClass="toolbar" Width="99%">
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Back" Text="Back" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Modify" Text="Modify Field" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="ConfigureChildFields" Text="Add Dependant Fields" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Delete" Text="Delete Field" />
                         <SCS:ToolbarButton CausesValidation="True" CommandName="UpdateFieldOrder" Text="Update Field Order" Visible="false" />
                    </Items>
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                        CssClassDisabled="button_disabled"></ButtonCssClasses>
                </SCS:Toolbar>
            </fieldset>
            <fieldset>
                <asp:GridView ID="gvProcessFields" runat="server" GridLines="Horizontal" Width="100%"
                    AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="FieldId,FieldTypeId" OnPageIndexChanging="gvProcessFields_PageIndexChanging"
                    CssClass="documents" OnRowDataBound="gvProcessFields_RowDataBound" EmptyDataText="THERE ARE NO FIELDS FOR THIS PROCESS"
                    OnSelectedIndexChanged="gvProcessFields_SelectedIndexChanged" PageSize="50">
                    <Columns>
                        <asp:TemplateField HeaderText="Field Name">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdFieldId" Value='<%# Eval("FieldId") %>' />
                                <asp:Label ID="lblFieldName" runat="server" Text='<%# Utils.getshortString(Eval("FieldName").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Field Order">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlOrder" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectedOrderChanges"
                                    Width="100px">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="FieldType" HeaderText="Field Type">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:CheckBoxField>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <EmptyDataRowStyle ForeColor="Red" />
                    <PagerStyle HorizontalAlign="Left" />
                    <SelectedRowStyle CssClass="selectedRow" />
                </asp:GridView>
            </fieldset>
        </div>
    </div>
</asp:Content>
