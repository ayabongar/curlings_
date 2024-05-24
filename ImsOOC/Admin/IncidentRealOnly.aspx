<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUserNoUpdatePanel.master" AutoEventWireup="true"
    CodeFile="IncidentRealOnly.aspx.cs" Inherits="Admin_IncidentRealOnly" %>

<%@ Register TagPrefix="uc1" TagName="UserSelector_1" Src="~/Admin/UserSelector.ascx" %>
<%@ Register Src="~/Admin/UserSelector.ascx" TagPrefix="uc1" TagName="UserSelector" %>
<%@ Register Src="~/SurveyWizard/HierarchicalLookup.ascx" TagPrefix="uc1" TagName="HierarchicalLookup" %>
<%@ Register Src="~/SurveyWizard/MatrixQuestion.ascx" TagPrefix="uc1" TagName="MatrixQuestion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/SurveyWizard/DisplaySurvey.ascx" TagPrefix="uc1" TagName="DisplaySurvey" %>
<%@ Register TagPrefix="asp" Src="~/SurveyWizard/DisplaySurvey.ascx" TagName="DisplaySurvey" %>
<%@ Register TagPrefix="asp" TagName="UCAttachDocuments" Src="~/Admin/UCAttachDocuments.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../Scripts/boxover.js"></script>
    <script type="text/javascript" src="../Scripts/_validation.js"></script>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.3.js" type="text/javascript"></script>
    <script src="../Scripts/webservices.js" type="text/javascript"></script>
    <style type="text/css">
        .inc-details {
            width: 100%;
            padding: 15px;
        }

            .inc-details input[type="text"] {
                width: 300px;
                height: 30px;
                padding-left: 2px;
            }

        .inc-details-label {
            width: 50%;
            vertical-align: top;
            margin: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divContent" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <%= CurrentProcess != null ? CurrentProcess.Description : string.Empty %>
            -
            <%= CurrentIncidentDetails.IncidentNumber %>
            </div>
            <div class="panel-body">

                <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked" EnableClientApi="False" CssClass="toolbar" Width="99.5%">
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Cancel" Text="Back" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Notes" Text="Work Info" Visible="False" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Print" Text="Cover Page" Visible="False" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="AcknowledgementLetter" Text="Acknowledgement Letter" Visible="False" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="PrintScreen" Text="Print" Visible="False" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="ReAssign" Text="Re-Assign" Visible="true" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Finalise" Text="Finalise" Visible="false" />
                    </Items>
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
                </SCS:Toolbar>
                <br />
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="pageBody">

                            <table class="inc-details" border="0" style="width: 100%">
                                <tr>
                                    <td class="inc-details-label" style="width: 50%">Created By:
                                    </td>
                                    <td style="padding-left: 6px;">
                                        <input type="text" id="txtCreatedBy" runat="server" disabled="True" />
                                    </td>
                                </tr>

                                <tr>
                                    <td class="inc-details-label">Incident Registration Date: </td>
                                    <td style="padding-left: 4px;">
                                        <input type="text" value="<%=CurrentIncidentDetails.Timestamp.ToString("yyyy-MM-dd") %>"
                                            disabled="disabled" /></td>
                                </tr>
                                <tr>
                                    <td class="inc-details-label">Incident Reference Number:
                                    </td>
                                    <td style="padding-left: 4px;">
                                        <input type="text" value="<%=CurrentIncidentDetails.ReferenceNumber %>" disabled="disabled" />
                                    </td>
                                </tr>

                                <tr>
                                    <td class="inc-details-label">Incident Status: </td>
                                    <td style="padding-left: 4px;">
                                        <input type="text" value="<%=CurrentIncidentDetails.IncidentStatus %>" disabled="disabled" /></td>
                                </tr>
                                <tr>
                                    <td class="inc-details-label">Incident Type:
                                    </td>
                                    <td style="padding-left: 4px;">

                                        <asp:DropDownList runat="server" ID="drpRoles" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="inc-details-label">Primary Actioned Person: </td>
                                    <td>
                                        <uc1:UserSelector ID="UserSelector1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="inc-details-label">Secondary Actioned Person:
                                    </td>
                                    <td>
                                        <uc1:UserSelector ID="SecAssignedToSID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="inc-details-label">Incident Due Date: </td>
                                    <td style="padding-left: 4px;">
                                        <asp:TextBox type="text" runat="server" ID="txtIncidentDueDate" Width="150px" /><asp:CalendarExtender ID="txtIncidentDueDate_CalendarExtender" runat="server" ClearTime="True"
                                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtIncidentDueDate"
                                            TodaysDateFormat="yyyy-MM-dd"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr id="trSLAUpdate" runat="server">
                                    <td class="inc-details-label" style="font-weight: bold">SLA Date 
                                    </td>
                                    <td style="padding-left: 4px;">
                                        <asp:TextBox type="text" runat="server" ID="txtSLAUpdated" Width="150px" Enabled="false" />

                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" ClearTime="True"
                                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtSLAUpdated"
                                            TodaysDateFormat="yyyy-MM-dd"></asp:CalendarExtender>

                                    </td>
                                </tr>
                                <tr id="trSLADateReason" runat="server">
                                    <td class="inc-details-label" style="font-weight: bold">Reason For Updating Of SLA Date:
                                    </td>
                                    <td style="padding-left: 4px;">

                                        <asp:TextBox type="text" runat="server" ID="txtSLAUpdateReason" Height="80px" TextMode="MultiLine" Enabled="false" />

                                    </td>
                                </tr>
                                <tr id="tr1" runat="server">
                                    <td class="inc-details-label" style="font-weight: bold">Date Actioned 
                                    </td>
                                    <td style="padding-left: 4px;">
                                        <asp:TextBox type="text" runat="server" ID="txtDateActioned" Width="150px" Enabled="false" />


                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="pageBody">
                            <table class="inc-details" border="0" style="width: 100%">
                                <tr>
                                    <td class="inc-details-label" style="padding-left: 4px;">
                                        <asp:DisplaySurvey ID="DisplaySurvey2" runat="server" />
                                    </td>
                                </tr>

                            </table>
                        </div>
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
                              <asp:ImageButton runat="server" ImageUrl="~/Images/Close_Box_Red.png" ID="btnClose" CausesValidation="false" /></td>
                      </tr>
                  </table>
              </td>
          </tr>
          <tr>
              <td>
                  <asp:Panel runat="server" ID="Panel1" Enabled="true">

                      <table style="width: 100%;">
                          <tr>
                              <td>

                                  <SCS:Toolbar ID="Toolbar2" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"
                                      EnableClientApi="False" CssClass="toolbar" Width="99%">
                                      <Items>
                                          <SCS:ToolbarButton CausesValidation="True" CommandName="ReAssignUser" Text="Submit" />

                                      </Items>
                                      <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                                          CssClassDisabled="button_disabled"></ButtonCssClasses>
                                  </SCS:Toolbar>
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
                                                  <asp:TextBox runat="server" ID="txtNotes2" Width="500px" Rows="4" TextMode="MultiLine"></asp:TextBox>
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

  <asp:ModalPopupExtender ID="mpAllocate" runat="server" PopupControlID="pnlAllocate" TargetControlID="btnViewCase" RepositionMode="RepositionOnWindowResizeAndScroll"
      Drag="True"
      CancelControlID="btnClose" BackgroundCssClass="modalBackground">
  </asp:ModalPopupExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="pageBody">
                    <table style="width: 100%;">
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="txtNotes" Width="100%" Rows="4" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="btnNew" Text="New" OnClick="btnNew_Click" Visible="False" /><asp:Button runat="server" ID="btnAddNote" Text="Save" OnClick="btnAddNote_Click" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TreeView runat="server" ID="treeNotes" AutoGenerateDataBindings="False" BorderColor="Gray" BorderStyle="None" BorderWidth="1px"
                                    OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ExpandImageUrl="../Images/Collapse.png" CollapseImageUrl="../Images/Minus.png"
                                    ExpandDepth="0" Font-Bold="True" Font-Size="Medium" ForeColor="Black" NodeWrap="True">
                                    <HoverNodeStyle BorderColor="Black" />
                                    <LeafNodeStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="ChildTreeNode" Font-Bold="False" />
                                    <NodeStyle BorderColor="Silver" BorderStyle="None" BorderWidth="1px" Font-Bold="False" />
                                    <ParentNodeStyle BorderColor="Black" BorderStyle="Solid" Font-Bold="False" Font-Size="Medium" ForeColor="Black" />
                                    <RootNodeStyle BorderColor="Black" ChildNodesPadding="5px" CssClass="TreeNode" Font-Bold="True" Font-Overline="False" Font-Size="Small" NodeSpacing="5px" />
                                    <SelectedNodeStyle BorderColor="Black" Font-Bold="False" />
                                </asp:TreeView>
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
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAttach" runat="server" Text="Documents" OnClick="ViewDocuments"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="pageBody">
                    <asp:UCAttachDocuments runat="server" ID="UCAttachDocuments" />
                </div>


            </div>

        </div>
    </div>


  
</asp:Content>
