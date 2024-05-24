<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCAttachDocuments.ascx.cs" Inherits="AttachDocuments" %>

<div>
    <table style="width: 100%;">
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    <table style="width: 80%;">
        <tr>
            <td>Browse for file :</td>
            <td>
                <asp:FileUpload runat="server" ID="flDoc" Width="425px" /><asp:Button runat="server" ID="btnAddFile" Text="Upload File" OnClick="UploadFile" /></td>
        </tr>
        <tr>
            <td></td>
            <td>
                </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView DataKeyNames="DocId" CssClass="documents" runat="server" ID="gvDocs" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="NO DOCUMENTS FOR THIS WORK INFO" OnPageIndexChanging="gvDocs_PageIndexChanging" OnRowCommand="gvDocs_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="File Name" DataField="DocumentName" />
                        <asp:BoundField HeaderText="File Type" DataField="Extension" />
                        <asp:BoundField HeaderText="Uploaded By" DataField="FullName" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkOpenFile" runat="server" CommandArgument='<%#Eval("DocId") %>' CommandName="OpenFile" Text="Open File"></asp:LinkButton>
                            </ItemTemplate>
                       
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                                 <ItemTemplate>
                                <span onclick="javascript:return confirm('Are you sure you want to delete this document');">
                                  
                                  <asp:LinkButton runat="server" ID="lnkDelete"  CommandArgument='<%#Eval("DocId") %>' CommandName="DeleteFile" >Delete</asp:LinkButton>
                                </span>

                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle ForeColor="Red" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>


