<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" CodeFile="KeyResultOwner.aspx.cs" Inherits="NTQR_KeyResultOwner" %>
<%@ Register Src="../Admin/UserSelector.ascx" TagName="UserSelector" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/webservices.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel-body">



        <div class="panel panel-primary">
            <div class="panel-heading">
                Add Or Update Key Owner Name lookup
            </div>
            <div class="panel-body">
                <div class="pageBody">
                    <table style="padding: 15px; width: 100%;">
                        <tr runat="server" >
                             <td>
                                <fieldset>
                                    <legend>Existing KRO</legend>
                                 
                                    <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CssClass="documents" DataKeyNames="Id" EmptyDataText="NO KRO YET"
                                        GridLines="Horizontal" OnPageIndexChanging="gvUsers_PageIndexChanging" PageSize="100"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Id">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="KRO Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnRemove" runat="server" OnClick="RemoveUser" Text="Remove User"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle ForeColor="Red" />
                                    </asp:GridView>
                                </fieldset>
                            </td>
                            <td style="vertical-align: top;">
                                <fieldset>
                                    <legend>Add New KRO</legend>
                                    <table style="width:100%">

                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <strong>Search User (SID/Name/Surname):</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px;">
                                                <uc1:UserSelector ID="UserSelector1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td  style="padding-left: 15px;">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="buttons" OnClick="Save" Text="Add New KRO"
                                                    Width="141px"  Height="30px" />
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                           
                        </tr>
                    </table>

                </div>
            </div>
        </div>
    </div>

            
</asp:Content>

