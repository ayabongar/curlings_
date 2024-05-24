<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUser.master" AutoEventWireup="true" CodeFile="UserLandingPage.aspx.cs" Inherits="CustomPages_UserLandingPage" %>

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
                        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="False" OnButtonClicked="Toolbar1_ButtonClicked" Width="99%">
                            <Items>
                                
                                <SCS:ToolbarButton CausesValidation="True" CommandName="Search" Text="Search Incidents" />
                                <SCS:ToolbarButton CausesValidation="True" CommandName="Back" Text="Home" />
                                 
                            </Items>
                            <ButtonCssClasses CssClass="button" CssClassDisabled="button_disabled" CssClassEnabled="button_enabled" CssClassSelected="" />
                        </SCS:Toolbar>
                    </fieldset>
                </td>
            </tr>
                <tr>
                    <td>
                        <fieldset>
                            <table style="width: 100%; padding: 2px; border-collapse: separate;">
                                <tr>
                                    <td style="vertical-align: top; padding: 15px;">
                                        <asp:GridView ID="gvIncidents" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CssClass="documents" DataKeyNames="IncidentID,IncidentNumber,ProcessId" GridLines="Horizontal"
                                            OnPageIndexChanging="IncidentPageChanging"
                                            OnRowDataBound="IncidentRowDataBound" Width="100%" PageSize="20" OnRowCommand="gvIncidents_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="ProcessName" HeaderText="Process Name">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IncidentNumber" HeaderText="Incident Number">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Timestamp" DataFormatString="{0:yyyy-MM-dd hh:mm}" HeaderText="Registered">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                          
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button runat="server"  ID="btnCoverPage" Text="CoverPage" CommandArgument='<%#Eval("IncidentID") %>' CommandName="btnCoverPage"/>
                                                        <asp:Button runat="server"  ID="btnAcknowledgement" Text="Acknowledgement Letter" CommandArgument='<%#Eval("IncidentID") %>' CommandName="btnAcknowledgement"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                          
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

