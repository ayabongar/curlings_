<%@ Page Title="IMS REPORTS" Language="C#" MasterPageFile="~/ReportsMaster.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports_IMSReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>

    <div class="panel panel-primary">
        <div class="panel-heading">
            <%=Heading%>
        </div>
        <div class="panel-body">
             <div class="pageBody" style="min-height:800px">
            <table>
                <tr>
                    <td>Start Date:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtDate" enableediting="false"></asp:TextBox>
                        <asp:CalendarExtender ID="txtIncidentDueDate_CalendarExtender" runat="server" ClearTime="True"
                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtDate"
                            TodaysDateFormat="yyyy-MM-dd">
                        </asp:CalendarExtender>

                    </td>
                    <td>End Date:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEndDate" enableediting="false"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True"
                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtEndDate"
                            TodaysDateFormat="yyyy-MM-dd">
                        </asp:CalendarExtender>

                    </td>
                    <td><asp:Button runat="server"  ID="btnSearch" Text="Search" OnClick="btnSearch_Click" /> <asp:Button runat="server" ID="btnExport" Text="Export To Excel" OnClick="ExportData" Visible="True" /></td>
                </tr>
            </table>
            <table style="width: 100%">
                

                <tr>
                    <td>
                        <asp:Panel runat="server" Width="1300px" ScrollBars="Both">
                            <asp:GridView runat="server" ID="gvReports" CssClass="documents" AllowPaging="True" OnPageIndexChanging="gvReports_PageIndexChanging" EmptyDataText="NO INCIDENTS " AutoGenerateColumns="true">
                        </asp:GridView>
                        </asp:Panel>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                       
                    </td>
                </tr>
            </table>
        </div>
    </div> </div>
</asp:Content>

