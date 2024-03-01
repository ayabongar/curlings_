<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddNotes.ascx.cs" Inherits="Controls_AddNotes" %>
<table style="width: 100%;">
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnNew" Text="New" OnClick="NewNote" />
                                    <asp:Button runat="server" ID="btnAddNote" Text="Save Work Info" OnClick="AddNote" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView runat="server" CssClass="documents" ID="gvWorkInfo" GridLines="Horizontal"
                                        AutoGenerateColumns="False" OnRowDataBound="RowDataBound" DataKeyNames="WorkInfoId">
                                        <Columns>
                                            <asp:BoundField DataField="CreatedBy" HeaderText="Added By" />
                                            <asp:BoundField DataField="Timestamp" HeaderText="Date Created" DataFormatString="{0:yyyy-MM-dd hh:mm}" />
                                            <asp:TemplateField HeaderText="Notes" SortExpression="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNotes" runat="server" Text='<%# Utils.getshortString(Eval("Notes").ToString()) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                          
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>