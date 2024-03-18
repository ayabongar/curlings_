<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUser.master" AutoEventWireup="true" CodeFile="SelectNormalUserProcess.aspx.cs" Inherits="PrPcr_SelectNormalUserProcess" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/boxover.js"></script>
    <style type="text/css">
        .slaabouttobeviolated {
            color: orange;
        }

        .slaviolated {
            color: red;
        }

        .slakept {
            color: green;
        }


        .dvhdr1 {
            background: #CCCCCC;
            font-family: verdana;
            font-size: 9px;
            font-weight: bold;
            border-top: 1px solid #000000;
            border-left: 1px solid #000000;
            border-right: 1px solid #000000;
            border-bottom: 1px solid #000000;
            padding: 2px;
            width: 400px;
        }

        .dvbdy1 {
            background: #FFFFFF;
            font-family: verdana;
            font-size: 10px;
            border-left: 1px solid #000000;
            border-right: 1px solid #000000;
            border-bottom: 1px solid #000000;
            padding: 2px;
            width: 400px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading">SELECT A SYSTEM</div>
        <div class="panel-body">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <fieldset>
                            <table style="width: 100%; padding: 2px; border-collapse: separate;">
                                <tr>
                                    <td style="vertical-align: top; width: 30%; border-right: 1px solid silver; padding: 15px;">
                                        <asp:GridView ID="gvProcesses" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CssClass="documents" DataKeyNames="ProcessId,StatusId" GridLines="None" OnPageIndexChanging="PageIndexChanging"
                                            OnRowDataBound="RowDataBound" PageSize="20" Width="100%"
                                            OnSelectedIndexChanged="gvProcesses_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="System  Name" SortExpression="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProcessDescription" runat="server" Text='<%# String.Format("{0} v{1}", Utils.getshortString(Eval("Description").ToString()), Eval("Version")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                            <SelectedRowStyle CssClass="selectedRow" />
                                        </asp:GridView>
                                    </td>
                                    <td style="vertical-align: top; padding: 15px;">
                                        <asp:GridView ID="gvIncidents" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CssClass="documents" DataKeyNames="IncidentID,IncidentNumber,ProcessId" GridLines="Horizontal"
                                            OnPageIndexChanging="IncidentPageChanging"
                                            OnRowDataBound="IncidentRowDataBound" Width="100%" PageSize="20">
                                            <Columns>
                                                <asp:BoundField DataField="ProcessName" HeaderText="Process Name">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ReferenceNumber" HeaderText="Incident Number">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Timestamp" DataFormatString="{0:yyyy-MM-dd hh:mm}" HeaderText="Registered">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                          
                                                <asp:BoundField DataField="DueDate" DataFormatString="{0:yyyy-MM-dd hh:mm}" HeaderText="Due Date">
                                                    <HeaderStyle VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IncidentStatus" HeaderText="Status">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                            <SelectedRowStyle CssClass="selectedRow" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>


