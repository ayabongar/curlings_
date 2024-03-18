<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageMimeTypes.aspx.cs" Inherits="SystemAdmins_ManageMimeTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="panel panel-primary">
        <div class="panel-heading">
           MANAGE FILE TYPES</div>
        <div class="panel-body">
            <table style="width: 100%;">
                <tr>
                    <td>
                        File Extension:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtExtension" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        File Type (image/bmp):
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMimeType" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                        
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnSubmit" MaxLength="50" Text="Add File Type" OnClick="SaveFileType"></asp:Button>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="2">
                        <asp:GridView runat="server" ID="gvFileTypes" AutoGenerateColumns="False" CssClass="documents" GridLines="Horizontal" EmptyDataText="THERE ARE NO FILE TYPES" AllowPaging="True" OnPageIndexChanging="PageChnaged">
                            <Columns>
                                <asp:BoundField HeaderText="File Extension" DataField="Extension"/>
                                <asp:BoundField HeaderText="File Type" DataField="FileType"/>
                                <asp:BoundField HeaderText="Date Added" DataField="Timestamp" />
                                    

                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" />
                            <RowStyle HorizontalAlign="Left"></RowStyle>
                            <EmptyDataRowStyle ForeColor="red"></EmptyDataRowStyle>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
     </div>
</asp:Content>

