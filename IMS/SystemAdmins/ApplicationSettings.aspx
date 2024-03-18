<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ApplicationSettings.aspx.cs" Inherits="admin_ApplicationSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td {
            text-align: center;
        }

        th {
            text-align: center;
        }

        .txt {
            border: solid silver 1px;
            height: 40px;
            color: green;
            text-align: left;
            vert-align: middle;
            width: 100%;
        }

        .btn {
            border: solid silver 1px;
            height: 40px;
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading">
            VIEW AND UPDATE CONFIGURATION SETTINGS
        </div>
        <div class="panel-body">
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Key"
            CellPadding="4" CellSpacing="4" CssClass="documents" Width="100%"
            Caption="ONLY FOR DEVELOPERS">
            <Columns>
                <asp:BoundField DataField="Key" HeaderText="Key">
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Value">
                    <ItemTemplate>
                        <asp:TextBox runat="server" Width="100%" ID="txtValue" CssClass="txt" style="text-align: center;" 
                            Text='<%# Bind("Value") %>' Height="30px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="Update" CssClass="btn" CommandName="Update" CommandArgument='<%# Bind("Value") %>' Height="30px"></asp:Button>
                    </ItemTemplate>
                    <HeaderStyle Width="200px" />
                    <ItemStyle Width="200px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>

