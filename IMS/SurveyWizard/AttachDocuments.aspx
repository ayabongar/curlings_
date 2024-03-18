<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUserNoUpdatePanel.master" AutoEventWireup="true" CodeFile="AttachDocuments.aspx.cs" Inherits="SurveyWizard_AttachDocuments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">ATTACH DOCUMENTS - <%=cIncidentProcess.IncidentNumber %></div>
        <div class="panel-body">
            
             <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked" EnableClientApi="False" CssClass="toolbar" Width="99%">
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Submit" Text="Upload Document" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Cancel" Text="Close" />
                    </Items>
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
                </SCS:Toolbar>
            
               <table style="width: 100%;"><tr><td>&nbsp;</td></tr></table>
           
        <table style="width: 80%;">
            <tr>
                <td>Browse for file :</td>
                <td><asp:FileUpload runat="server" ID="flDoc" width="425px"/></td>
            </tr>
            <tr>
                <td></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2"><asp:GridView DataKeyNames="DocId" CssClass="documents" runat="server" ID="gvDocs" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="NO DOCUMENTS FOR THIS WORK INFO" OnPageIndexChanging="gvDocs_PageIndexChanging" OnRowCommand="gvDocs_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="File Name" DataField="DocumentName"/>
                            <asp:BoundField HeaderText="File Type" DataField="Extension"/>
                            <asp:BoundField HeaderText="Uploaded By" DataField="FullName"/>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkOpenFile" runat="server" CommandArgument='<%#Eval("DocId") %>' CommandName="OpenFile" Text="Open File"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Red" />
                    </asp:GridView></td>
            </tr>
        </table>
        </div>
    </div>
</asp:Content>

