<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewIncidentEmailLog.aspx.cs" Inherits="Admin_ViewIncidentEmailLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .left-labels {
            width: 400px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading">- Email Log - </div>
        <div class="panel-body">
            <table style="width: 100%;">
                <tr>
                    <td class="left-labels">Incident Number:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtIncNo" MaxLength="15" Width="400px" Height="30px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="left-labels">Incident Number:</td>
                    <td>
                        <asp:Button runat="server" ID="btnSearch" MaxLength="15" Text="Search" OnClick="btnSearch_Click" Width="250px" Height="30px"></asp:Button>
                    </td>
                </tr>
                <tr>
                  
                    <td colspan="2">
                        <asp:GridView ID="gvEmailLog" runat="server" AutoGenerateColumns="False" CssClass="documents" OnRowCommand="GridView1_RowCommand" Width="804px" AllowPaging="True" OnPageIndexChanging="gvEmailLog_PageIndexChanging" >
                            <Columns>
                                <asp:BoundField DataField="Subject" HeaderText="Subject" />
                                <asp:BoundField DataField="SentTo" HeaderText="Sent To" />
                                <asp:BoundField DataField="Timestamp" HeaderText="Date Sent" DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" Text="View" ID="lnkView" CommandArgument='<%#Eval("EmailLogId") %>' CommandName="View"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>

            <table style="width: 100%" runat="server" id="tblEmail" Visible="False">
                
                <tr>
                    <td class="left-labels">Sent To Email Address:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmailAddress" Width="400px" Height="30px" />
                    </td>
                </tr>
                <tr>
                    <td class="left-labels">Subject :
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSubject" Width="400px" Height="30px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                       
                           
                            <div runat="server" id="dvBody"></div>
                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

