<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" CodeFile="NormalUserLandingPage.aspx.cs" Inherits="NTQR_NormalUserLandingPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Admin/SearchFilterTypes.ascx" TagPrefix="uc1" TagName="SearchFilterTypes" %>
<%@ Register TagPrefix="uc1" TagName="UserSelector_1" Src="~/Admin/UserSelector.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .td {
            background-color: white !important;
        }
    </style>


    <div class="panel panel-primary">
        <div class="panel-heading"><%= String.Format("{0} v{1}", CurrentProc.Description, CurrentProc.Version) %> - MY INCIDENTS </div>
        <div class="panel-body">
            <div class="pageBody">
                <table style="width: 100%;">
                    <tr>
                        <td>

                            <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="False" OnButtonClicked="Toolbar1_ButtonClicked" Width="99.5%">
                                <Items>

                                    <SCS:ToolbarButton CausesValidation="True" CommandName="AddNewObjective" Text="Register New Objectives" />


                                </Items>
                                <ButtonCssClasses CssClass="button" CssClassDisabled="button_disabled" CssClassEnabled="button_enabled" CssClassSelected="" />
                            </SCS:Toolbar>

                        </td>
                    </tr>
                    <tr>
                        <td>

                         <%--   <asp:TabContainer ID="tbCFY" runat="server">
                                <asp:TabPanel ID="tbCurrent" runat="server">
                                    <HeaderTemplate> Current FY Report
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                    </ContentTemplate>

                                </asp:TabPanel>s
                                <asp:TabPanel ID="TabPrevFY" runat="server">
                                    <HeaderTemplate>Prevous FY Report
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                    </ContentTemplate>

                                </asp:TabPanel>
                            </asp:TabContainer>--%>

                            <table style="width: 100%" border="0">
                                <tr>
                                    <td align="right"><b>Search By Financial Year</b>

                                        <asp:DropDownList ID="drpSearch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpSearch_SelectedIndexChanged" Width="300px">
                                        </asp:DropDownList><br />
                                        <br />
                                        <asp:GridView ID="gvIncidents" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="KeyResultId,IncidentStatusId,StatusType" EmptyDataText="YOU HAVE NO INCIDENTS FOR THIS SYSTEM" GridLines="Horizontal" OnPageIndexChanging="PageChanging" OnRowCommand="gvIncidents_RowCommand" OnRowDataBound="RowDataBound" PageSize="50" Width="100%">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <table style="border: 1px solid black" width="100%">
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table border="1">
                                                            <tr valign="top" style="background-color: gray;">
                                                                <td style="width: 5%; background-color: gray; color: white!important; font-weight: bold">Financial Year</td>
                                                                <td style="width: 40%; color: white!important; font-weight: bold">Strategic Objective</td>
                                                                <td style="width: 20%; color: white!important; font-weight: bold">Key Result</td>
                                                                <td style="width: 20%; color: white!important; font-weight: bold">Key Result Indicator</td>
                                                                <td style="width: 10%; color: white!important; font-weight: bold">REPORTS </td>
                                                                <td></td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:HiddenField runat="server" ID="hdnID" Value='<% #Eval("KeyResultId") %>' />
                                                                    <asp:Label ID="Label2" runat="server" Text='<% #Eval("CFY") %>'></asp:Label>

                                                                </td>
                                                                <td style="font-weight: bold">
                                                                    <asp:Label ID="Label3" runat="server" Text='<% #Eval("StrategicObjective") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text='<% #Eval("KeyResult") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text='<% #Eval("KeyResultIndicator") %>'></asp:Label>
                                                                    <asp:Label ID="lblAnchor" runat="server" Text='<% #Eval("Anchor") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblKeyResult" runat="server" Text='<% #Eval("KeyResultOwner") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="AssignedToSID" runat="server" Text='<% #Eval("AssignedToSID") %>' Visible="false"></asp:Label>
                                                                </td>

                                                                <td colspan="4" class="td">
                                                                    <table>
                                                                        <tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:LinkButton Text="Search" ID="lnkCreateReport" runat="server" CssClass="btn btn-sm btn-primary" CommandArgument='<%#Eval("KeyResultId") %>' CommandName="Create_Report">
                                                                                                                <i class="ace-icon fa fa-pencil small-110"></i>	CREATE NEW REPORT
							
                                                                                    </asp:LinkButton></td>
                                                                                <td>
                                                                                    <asp:UpdatePanel runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:LinkButton Text="Search" ID="lnkEditObjectives" runat="server" CssClass="btn btn-sm btn-primary" CommandArgument='<%#Eval("KeyResultId") %>' CommandName="Edit_Object">
                                                                                                                <i class="ace-icon fa fa-pencil small-110"></i>	EDIT OBJECTIVES
							
                                                                                            </asp:LinkButton>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                        </tr>
                                                                    </table>
                                                                    <asp:GridView runat="server" ID="gvReport" Width="100%" CssClass="documents"
                                                                        DataKeyNames="Id,IncidentStatusId,StatusType" AutoGenerateColumns="False"
                                                                        OnRowDataBound="NextedRowDataBound" GridLines="Horizontal" PageSize="100"
                                                                        OnPageIndexChanging="PageChanging" AllowPaging="True"
                                                                        EmptyDataText="YOU HAVE NO REPORTS FOR THIS OBJECTIVE" OnRowCommand="gvReport_RowCommand">
                                                                        <Columns>

                                                                            <asp:BoundField DataField="Quarter" HeaderText="Quarter">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>

                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>STATUS TYPE</HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <span class="label label-sm label-grey arrowed aside">
                                                                                        <%-- <asp:Label ID="status" runat="server" Text='<%# Eval("StatusType") %>'></asp:Label>--%>
                                                                                        <asp:LinkButton ID="cfy" Text='<% #Eval("StatusType") %>' ForeColor="White" CommandArgument='<%#Eval("Id") + " | " + Eval("fk_ReportKeyResult_ID") %>'  CommandName="Print" runat="server" />
                                                                                    </span>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <div style="float: left">


                                                                                            <asp:LinkButton Text="Search" ID="lnkViewReport" runat="server" CssClass="btn btn-sm btn-primary" CommandArgument='<%#Eval("Id") + " | " + Eval("fk_ReportKeyResult_ID") %>' CommandName="View_Report">
                                                                                                                <i class="ace-icon fa fa-pencil small-110"></i>	VIEW REPORT
							
                                                                                            </asp:LinkButton>

                                                                                        </div>

                                                                                    </div>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                        <EmptyDataRowStyle ForeColor="Red" />
                                                                        <PagerStyle HorizontalAlign="Left" />
                                                                        <SelectedRowStyle CssClass="selectedRow" />
                                                                    </asp:GridView>
                                                                </td>
                                                                <td></td>
                                                            </tr>

                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="grdHeaderStyle" />
                                                    <ItemStyle CssClass="grdItemStyle" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Red" />
                                            <PagerStyle HorizontalAlign="Left" />
                                            <SelectedRowStyle CssClass="selectedRow" />
                                        </asp:GridView>
                                    </td>

                                </tr>
                            </table>


                            <table>
                                <tr>
                                    <td style="visibility: hidden">
                                        <asp:Button runat="server" ID="btnView" Text="View Case Details" />
                                        <asp:Button runat="server" ID="btnCancel" Text="View Case Details" />
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <asp:Panel runat="server" ID="pnlObjectives" Width="70%" ScrollBars="Both" BackColor="White">
        <table style="width: 100%">
            <tr>
                <td>
                    <SCS:Toolbar ID="Toolbar3" ClientIDMode="Static" runat="server" OnButtonClicked="Toolbar2_ButtonClicked"
                        EnableClientApi="False" CssClass="toolbar" Width="100%">
                        <Items>
                            <SCS:ToolbarButton CausesValidation="True" CommandName="SaveObjective" Text="Save Objective" />
                            <SCS:ToolbarButton CausesValidation="True" CommandName="Close" Text="Close" />
                        </Items>
                        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                            CssClassDisabled="button_disabled"></ButtonCssClasses>
                    </SCS:Toolbar>

                    <div class="widget-body">
                        <div class="widget-main">
                            <div class="form-group has-info" runat="server">
                                <asp:ValidationSummary runat="server" ID="vSummary" ValidationGroup="popup" ShowSummary="false" ShowMessageBox="true" />
                                <asp:HiddenField runat="server" ID="hdnId" />
                            </div>

                        </div>
                        <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>--%>
                        <div class="form-group has-info" runat="server">

                            <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Financial Year(yyyy/yy): <span style="color: red">*</span></label>

                            <div class="col-xs-12 col-sm-2">
                                <span class="block input-icon input-icon-right">
                                    <asp:TextBox class="width-100" runat="server" ID="txtCFY" ClientIDMode="Static" placeholder="FInancial Year" />


                                    <asp:RequiredFieldValidator ErrorMessage="CFY Target is a required field!" ValidationGroup="popup" Text="*" ForeColor="Red" ControlToValidate="txtCFY" runat="server" />

                                    <i class="ace-icon fa fa-info-circle"></i>
                                </span>
                            </div>

                        </div>
                        <div class="form-group has-info" runat="server">
                            <label for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Strategic Objective: <span style="color: red">*</span></label>

                            <div class="col-xs-12 col-sm-10">


                                <asp:DropDownList ID="drpStrategicObjective" class="width-100" runat="server" ValidationGroup="popup" AutoPostBack="True" OnSelectedIndexChanged="drpStrategicObjective_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ErrorMessage="Quarter is a required field!" Text="*" ValidationGroup="popup" ForeColor="Red" ControlToValidate="drpStrategicObjective" runat="server" />

                            </div>
                        </div>
                        <div class="form-group has-info" runat="server">
                            <label for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Key Result: <span style="color: red">*</span></label>

                            <div class="col-xs-12 col-sm-10">


                                <asp:DropDownList ID="drpKeyResult" class="width-100" ValidationGroup="popup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpKeyResult_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ErrorMessage="Quarter is a required field!" Text="*" ValidationGroup="popup" ForeColor="Red" ControlToValidate="drpKeyResult" runat="server" />

                            </div>

                        </div>
                        <div class="form-group has-info" runat="server">
                            <label for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Key Result Indicator: <span style="color: red">*</span></label>

                            <div class="col-xs-12 col-sm-10">


                                <asp:DropDownList ID="drpKeyResultIndicator" class="width-100" ValidationGroup="popup" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ErrorMessage="Quarter is a required field!" Text="*" ValidationGroup="popup" ForeColor="Red" ControlToValidate="drpKeyResultIndicator" runat="server" />

                            </div>
                        </div>
                        <div class="form-group has-info" runat="server">
                            <label for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right input-icon input-icon-right">
                                Key Result Owner: <span style="color: red">*</span>


                            </label>

                            <div class="col-xs-12 col-sm-4">


                                <asp:ListBox runat="server" ID="drpKeyResultOwner" class="width-100" ValidationGroup="popup" SelectionMode="Multiple"></asp:ListBox>
                                <asp:RequiredFieldValidator ErrorMessage="Key Result Owner Name is a required field!" ValidationGroup="popup" Text="*" ForeColor="Red" ControlToValidate="drpKeyResultOwner" runat="server" />
                            </div>
                            <span data-toggle="tooltip">
                                <label for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Anchor: <span style="color: red">*</span></label></span>

                            <div class="col-xs-12 col-sm-4">

                                <asp:ListBox runat="server" ID="drpAnchor" class="width-100" ValidationGroup="popup" SelectionMode="Multiple"></asp:ListBox>
                                <asp:RequiredFieldValidator ErrorMessage="Anchor Name is a required field!" ValidationGroup="popup" Text="*" ForeColor="Red" ControlToValidate="drpAnchor" runat="server" />
                            </div>

                        </div>


                        <div class="form-group has-info" runat="server">

                            <label for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Annual Target:</label>

                            <div class="col-xs-12 col-sm-4">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="drpAnnualTarget" runat="server" ValidationGroup="popup">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ErrorMessage="Annual Target is a required field!" ValidationGroup="popup" Text="*" ForeColor="Red" ControlToValidate="drpAnnualTarget" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <label for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">Quarter Three Target: <span style="color: red">*</span></label>

                            <div class="col-xs-12 col-sm-4">
                                <asp:DropDownList ID="drpQuarter3Target" runat="server" ValidationGroup="popup">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ErrorMessage="Quarter Target is a required field!" ValidationGroup="popup" Text="*" ForeColor="Red" ControlToValidate="drpQuarter3Target" runat="server" />
                            </div>
                        </div>





                        <div class="form-group has-info" runat="server">
                            <label for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">Quarter One Target: <span style="color: red">*</span></label>

                            <div class="col-xs-12 col-sm-4">
                                <asp:DropDownList ID="drpQuarterOneTarget" runat="server" ValidationGroup="popup">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ErrorMessage="Quarter Target is a required field!" ValidationGroup="popup" Text="*" ForeColor="Red" ControlToValidate="drpQuarterOneTarget" runat="server" />
                            </div>
                            <label for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">Quarter Four Target: <span style="color: red">*</span></label>

                            <div class="col-xs-12 col-sm-4">
                                <asp:DropDownList ID="drpQuarter4Target" runat="server" ValidationGroup="popup">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ErrorMessage="Quarter Target is a required field!" ValidationGroup="popup" Text="*" ForeColor="Red" ControlToValidate="drpQuarter4Target" runat="server" />
                            </div>
                        </div>
                        <div class="form-group has-info" runat="server">
                            <label for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">Quarter Two Target: <span style="color: red">*</span></label>

                            <div class="col-xs-12 col-sm-4">
                                <asp:DropDownList ID="drpQuarter2Target" runat="server" ValidationGroup="popup">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ErrorMessage="Quarter Target is a required field!" ValidationGroup="popup" Text="*" ForeColor="Red" ControlToValidate="drpQuarter2Target" runat="server" />
                            </div>
                            <label for="inputWarning" class="col-xs-2 col-sm-2 control-label no-padding-right">TID: <span style="color: red">*</span></label>

                            <div class="col-xs-12 col-sm-4">

                                <asp:DropDownList ID="drpTID" runat="server" ValidationGroup="popup">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ErrorMessage="TID is a required field!" ValidationGroup="popup" Text="*" ForeColor="Red" ControlToValidate="drpTID" runat="server" />

                            </div>
                        </div>




                        <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </td>
            </tr>

        </table>



    </asp:Panel>

    <asp:ModalPopupExtender ID="mdlObjetives" runat="server" PopupControlID="pnlObjectives" TargetControlID="btnView" RepositionMode="RepositionOnWindowResizeAndScroll"
        Drag="True"
        CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>



    <script src="../assets/js/jquery.maskedinput.min.js"></script>
    <script src="../assets/js/bootstrap-tag.min.js"></script>
    <script>      

        $('#Toolbar3_item_0').click(function () {

            if (Page_ClientValidate()) {
                $(this).hide();
            }
        });
        // $('#Toolbar3').prop('disabled', true);
        $(document).ready(function () {
            // $('#birth-date').mask('00/00/0000');
            // $('#phone-number').mask('0000-0000');
            $('#txtCFY').mask('9999/99');
        });
    </script>
</asp:Content>

