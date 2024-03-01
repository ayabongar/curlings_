<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SortQuestions.aspx.cs" Inherits="Admin_SortQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        

                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="../Images/icon_inprogress.gif" style="height: 39px; width: 51px" /> Processing ....
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        

                    </td>
                </tr>
                
                
                <tr>
                    <td>
                        
                         <asp:Button runat="server" Text="Re-Order Questions" ID="btnReOrder" 
                onclick="btnReOrder_Click" />
                    </td>
                </tr>
            </table>
           

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

