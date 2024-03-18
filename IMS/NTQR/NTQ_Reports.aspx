<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" CodeFile="NTQ_Reports.aspx.cs" Inherits="NTQR_NTQ_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading"><%= String.Format("{0} v{1}", CurrentProc.Description, CurrentProc.Version) %> - MY INCIDENTS </div>
        <div class="panel-body">
            <div class="pageBody">
                <table style="width: 100%;">
                    <tr>
                        <td>

                            <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="False" OnButtonClicked="Toolbar1_ButtonClicked" Width="99.5%">
                                <Items>
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="AddNewIncident" Text="Register New Quarter Report" Visible="false" />

                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Reports" Text="View Reports" Visible="false" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="AddNewObjective" Text="Register New Objectives" Visible="false"/>


                                </Items>
                                <ButtonCssClasses CssClass="button" CssClassDisabled="button_disabled" CssClassEnabled="button_enabled" CssClassSelected="" />
                            </SCS:Toolbar>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="form-group has-info" runat="server">
                                            <asp:HiddenField runat="server" ID="hdnId" />
                                    </div>

                                </div>

                                <div class="form-group has-info" runat="server">
                                    <label for="inputWarning" style="font-weight:bold" class="col-xs-12 col-sm-2 control-label no-padding-right">Strategic Objective: <span style="color: red">*</span></label>

                                    <div class="col-xs-12 col-sm-8">
                                         <asp:DropDownList ID="drpStrategicObjective" CssClass="font:bold" Enabled="false" class="width-100" runat="server" ValidationGroup="popup" AutoPostBack="True" >
                                            </asp:DropDownList>


                                    </div>

                                </div>

                                <div class="form-group has-info" runat="server">
                                    <label for="inputWarning" style="font-weight:bold" class="col-xs-12 col-sm-2 control-label no-padding-right">Key Result: <span style="color: red">*</span></label>

                                    <div class="col-xs-12 col-sm-8">
                                         <asp:DropDownList ID="drpKeyResult" Enabled="false"  class="width-100" ValidationGroup="popup" runat="server">
                                            </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="form-group has-info" runat="server">
                                    <label for="inputWarning" style="font-weight:bold" class="col-xs-12 col-sm-2 control-label no-padding-right">Key Result Indicator: <span style="color: red">*</span></label>

                                    <div class="col-xs-12 col-sm-8">
                                           <asp:DropDownList ID="drpKeyResultIndicator" Enabled="false" class="width-100" ValidationGroup="popup" runat="server">
                                            </asp:DropDownList>

                                    </div>
                                </div>
                                
                                    <div class="form-group has-info" runat="server">
                                        <label for="inputWarning" style="font-weight:bold" class="col-xs-2 col-sm-2 control-label no-padding-right">TID: <span style="color: red">*</span></label>

                                        <div class="col-xs-12 col-sm-8">
                                            
                                                    <asp:DropDownList ID="drpTID" runat="server" Enabled="false"  ValidationGroup="popup">
                                                    </asp:DropDownList>
                                                    
                                        </div>

                                    </div>
                            </div>



                            <asp:TabContainer runat="server" ID="tabIncidents" CssClass="Tab">
                                <asp:TabPanel runat="server" ID="tabMyIncidents">
                                    <HeaderTemplate>NTQ Report</HeaderTemplate>
                                    <ContentTemplate>

                                        <asp:GridView runat="server" ID="gvIncidents" CssClass="documents" Width="100%"
                                            DataKeyNames="Id,IncidentStatusId,StatusType" AutoGenerateColumns="False"
                                            OnRowDataBound="RowDataBound" GridLines="Horizontal" PageSize="15"
                                            OnPageIndexChanging="PageChanging" AllowPaging="True"
                                            EmptyDataText="YOU HAVE NO INCIDENTS FOR THIS SYSTEM" OnRowCommand="gvIncidents_RowCommand">
                                            <Columns>

                                                <asp:BoundField DataField="Quarter" HeaderText="Quarter">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="StatusType" HeaderText="StatusType">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CompilerName" HeaderText="CompilerName">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="KeyResultOwnerName" HeaderText="KeyResultOwnerName">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AnchorName" HeaderText="AnchorName">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>                                              


                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div>
                                                            <div style="float: left">
                                                                
                                                                <asp:Button ID="btnView" runat="server" Text="View Reports" Width="100px" CommandArgument='<%#Eval("Id") %>' CommandName="View_Report" Height="30px"></asp:Button>

                                                            </div>

                                                        </div>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Red" />
                                            <PagerStyle HorizontalAlign="Left" />
                                            <SelectedRowStyle CssClass="selectedRow" />
                                        </asp:GridView>
                                        <asp:Label runat="server" ID="Label1" Visible="False" Font-Bold="True"
                                            ForeColor="Red" Width="100%">THERE ARE NO INCIDENTS ASSIGNED TO YOU</asp:Label>
                                    </ContentTemplate>
                                </asp:TabPanel>

                            </asp:TabContainer>




                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>





</asp:Content>

