<%@ Page Title="" Language="C#" MasterPageFile="~/OOC/Site.master" AutoEventWireup="true" CodeFile="OocReportViewer.aspx.cs" Inherits="PrOoc_OocReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="uc1" TagName="UserSelector_1" Src="~/Admin/UserSelector.ascx" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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

        .TextBoxes {
            width: 150px !important;
        }

        .wrapper {
            z-index: inherit;
            max-width: 1200px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


    <table style="width: 100%;">
        <tr>

            <td>

                <div class="panel panel-primary" style="min-height: 600px">
                    <div class="panel-heading">
                        <asp:Label ID="lblProcessDescription" runat="server" Text='<%# String.Format("{0} Report", Request["reportname"].ToString()) %>'></asp:Label>

                    </div>
                    <div class="panel-body">
                        <div class="pageBody" style="min-height: 600px">

                            <table>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>

                                <tr>
                                    <td>Process Name:</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpProcess">
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td>Select a Report Type:</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpReports" AutoPostBack="True" OnSelectedIndexChanged="drpReports_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="Select One.." />
                                            <%--<asp:ListItem Value="96" Text="OOC Internal Report Statistics" />
                                            <asp:ListItem Value="97" Text="OOC External Report Statistics" />
                                            <asp:ListItem Value="120" Text="OOC Tax Escalation Statistics" />--%>
                                            <asp:ListItem Value="irq01" Text="Cases due today" />
                                            <asp:ListItem Value="irq02" Text="Cases due the following day" />
                                            <asp:ListItem Value="irq03" Text="Escalated Cases still Open " />
                                            <asp:ListItem Value="irq04" Text="Comments made by the Commissioner/ Minister/ DCs/ CoS" />
                                            <asp:ListItem Value="irq05" Text="Submissions per divisions/units" />
                                            <asp:ListItem Value="irq06" Text="Feedback provided to business areas / regions. " />
                                            <asp:ListItem Value="irq07" Text="Referred requests as per business area / DCs" />
                                            <asp:ListItem Value="irq08" Text="Referred matters i.e., enterprise committee" />
                                            <asp:ListItem Value="irq09" Text="Query and Tax types" />
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                                <tr id="trStartDate" runat="server" visible="False">
                                    <td runat="server">
                                        <asp:Label ID="txtStartDate" Text="Start Date:" runat="server" />
                                    </td>
                                    <td runat="server">
                                        <asp:TextBox runat="server" ID="txtDate" enableediting="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtIncidentDueDate_CalendarExtender" runat="server" ClearTime="True" Format="yyyy-MM-dd" TargetControlID="txtDate"
                                            TodaysDateFormat="yyyy-MM-dd" BehaviorID="_content_txtIncidentDueDate_CalendarExtender"></asp:CalendarExtender>

                                    </td>
                                </tr>
                                <tr id="trEndDate" runat="server" visible="False">
                                    <td runat="server">End Date:</td>
                                    <td runat="server">
                                        <asp:TextBox runat="server" ID="txtEndDate" enableediting="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" ClearTime="True" Format="yyyy-MM-dd" TargetControlID="txtEndDate"
                                            TodaysDateFormat="yyyy-MM-dd" BehaviorID="_content_CalendarExtender3"></asp:CalendarExtender>

                                    </td>
                                </tr>
                                <tr id="trFreeText" runat="server" visible="False">
                                    <td runat="server">
                                        <asp:Label ID="lblFreeText" Text="text" runat="server" />
                                    </td>
                                    <td runat="server">
                                        <asp:TextBox runat="server" ID="txtFreeText"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True" Format="yyyy-MM-dd" TargetControlID="txtEndDate"
                                            TodaysDateFormat="yyyy-MM-dd" BehaviorID="_content_CalendarExtender1"></asp:CalendarExtender>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" /></td>
                                </tr>
                            </table>
                            <asp:TabContainer runat="server" ID="tabIncidents" ActiveTabIndex="0">

                                <asp:TabPanel runat="server" ID="TabPanel1">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblReportName" runat="server" />
                                    </HeaderTemplate>

                                    <ContentTemplate>


                                        <asp:Panel ID="pnlgvReport" runat="server" Style="width: 1200px" ScrollBars="Both">
                                            <asp:Label ID="lblTotal" Font-Bold="True" runat="server" />
                                            <asp:GridView runat="server" ID="gvReports" CssClass="documents" Width="100%"
                                                DataKeyNames="IncidentID,ProcessId" AutoGenerateColumns="False"
                                                OnRowDataBound="RowDataBound" GridLines="Horizontal" PageSize="100"
                                                OnPageIndexChanging="PageChanging" AllowPaging="True"
                                                EmptyDataText="YOU HAVE NO INCIDENTS FOR THIS SYSTEM" OnRowCommand="gvIncidents_RowCommand" ShowFooter="True">
                                                <Columns>
                                                   
                                                    <asp:BoundField DataField="DateRegistered" HeaderText="Registered" DataFormatString="{0:yyyy-MM-dd hh:mm}">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IncidentNumber" HeaderText="Incident Number">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Subject" HeaderText="Subject">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd hh:mm}">

                                                        <HeaderStyle VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IncidentStatus" HeaderText="Status">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PrimaryActionedPerson" HeaderText="Assign To">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RegisteredBy" HeaderText="Registered By">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <div>
                                                                <div style="float: left">
                                                                    <asp:Button ID="btnView" runat="server" Text="View" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="View_Incident" Height="30px"></asp:Button>
                                                                </div>
                                                                <div style="float: left" runat="server" id="tdReAssign">
                                                                    <asp:Button ID="bntReAssign" runat="server" Text="Re-Assign" Width="100px" CommandArgument='<%#Eval("IncidentID") %>' CommandName="Reassigne_Incident" Height="30px"></asp:Button>
                                                                </div>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="Black" />
                                                <EmptyDataRowStyle ForeColor="Red" />
                                                <PagerStyle HorizontalAlign="Left" />
                                                <SelectedRowStyle CssClass="selectedRow" />
                                            </asp:GridView>


                                            <asp:Panel runat="server" ID="pnlAllocate">
                                                <table>
                                                    <tr>
                                                        <td style="visibility: hidden">
                                                            <asp:Button runat="server" ID="btnViewCase" Text="View Case Details" />
                                                            <asp:Button runat="server" ID="btnExit" Text="View Case Details" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" style="background-color: white">
                                                    <tr style="background-color: grey">
                                                        <td style="max-height: 20px">
                                                            <table width="100%" border="0" class="panel-heading">
                                                                <tr>
                                                                    <td style="color: white">Re-Assign Incidents</td>
                                                                    <td align="right">
                                                                        <asp:ImageButton runat="server" ImageUrl="~/Images/Close_Box_Red.png" ID="btnClose" CausesValidation="False" /></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel runat="server" ID="Panel1">

                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button runat="server" ID="btnReAssign" Text="Submit" OnClick="btnReAssign_Click" />

                                                                            <br />
                                                                            <br />
                                                                            <div class="pageBody">
                                                                                <asp:TabContainer runat="server" ID="tbContainer" ActiveTabIndex="0" Width="100%">
                                                                                    <asp:TabPanel runat="server" ID="tbDetails">
                                                                                        <HeaderTemplate>
                                                                                            Incident Details
                                                                                        </HeaderTemplate>





                                                                                        <ContentTemplate>
                                                                                            <table class="inc-details">
                                                                                                <tr>
                                                                                                    <td class="inc-details-label">Incident Number:
                                                                                                    </td>
                                                                                                    <td style="padding-left: 4px;">
                                                                                                        <asp:TextBox runat="server" ID="IncidentNumber"></asp:TextBox>

                                                                                                    </td>
                                                                                                </tr>

                                                                                                <tr>
                                                                                                    <td class="inc-details-label">Assigned To:
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <uc1:UserSelector_1 ID="UserSelector1" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="inc-details-label">Incident Due Date:
                                                                                                    </td>
                                                                                                    <td style="padding-left: 4px;">
                                                                                                        <asp:TextBox type="text" runat="server" ID="txtIncidentDueDate" Width="150px" ReadOnly="True" />




                                                                                                        <asp:CalendarExtender
                                                                                                            ID="CalendarExtender2" runat="server" ClearTime="True"
                                                                                                            Format="yyyy-MM-dd hh:mm" TargetControlID="txtIncidentDueDate" TodaysDateFormat="yyyy-MM-dd hh:mm" Enabled="True"></asp:CalendarExtender>





                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>

                                                                                        </ContentTemplate>





                                                                                    </asp:TabPanel>
                                                                                </asp:TabContainer>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div class="pageBody">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td class="inc-details-label">New Assignee:
                                                                                        </td>
                                                                                        <td>
                                                                                            <uc1:UserSelector_1 ID="UserSelector2" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="inc-details-label">Note:
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox runat="server" ID="txtNotes" Width="500px" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>

                                            <asp:ModalPopupExtender ID="mpAllocate" runat="server" PopupControlID="pnlAllocate" TargetControlID="btnViewCase"
                                                Drag="True"
                                                CancelControlID="btnClose" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True">
                                            </asp:ModalPopupExtender>


                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </div>

                    </div>
                </div>

            </td>
        </tr>
    </table>

</asp:Content>
