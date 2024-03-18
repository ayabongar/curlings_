<%@ Page Title="" Language="C#" MasterPageFile="~/NormalUserNoUpdatePanel.master" AutoEventWireup="true" CodeFile="RegisterUserIncident.aspx.cs" Inherits="PrOoc_RegisterUserIncident" %>

<%@ Register TagPrefix="uc1" TagName="UserSelector" Src="~/Admin/UserSelector.ascx" %>
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

            .inc-details select {
                width: 300px;
                height: 30px;
                padding-left: 2px;
            }

        .inc-details-label {
            width: 50%;
            vertical-align: top;
            margin: 5px;
            height: 35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading">
             <%= CurrentProcess != null ? string.Format("{0} v{1} ",CurrentProcess.Description, CurrentProcess.Version)   : string.Empty %> 
            -
            <asp:Label runat="server" ID="lblReferenceNumber" ></asp:Label>
        </div>
        <div class="panel-body">
          
         <div class="pageBody">
            <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked" EnableClientApi="False" CssClass="toolbar" Width="99.5%">
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Cancel" Text="Home" />
                     <SCS:ToolbarButton CausesValidation="True" CommandName="AddNotes" Text="Add Work Info" Visible="False" /> 
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Submit" Text="Save Incident Details" />                                      
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Print" Text="Cover Page" Visible="False" />
                    <SCS:ToolbarButton CausesValidation="True" CommandName="AcknowledgementLetter" Text="Acknowledgement Letter" Visible="False" />
                     <SCS:ToolbarButton CausesValidation="True" CommandName="ReAssign" Text="Re-Assign" Visible="False" />
                     <SCS:ToolbarButton CausesValidation="True" CommandName="Complete" Text="Complete Incident" Visible="False" />
                    <SCS:ToolbarButton CausesValidation="True" CommandName="Close" Text="Close Incident" Visible="False" />
                </Items>
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="" CssClassDisabled="button_disabled"></ButtonCssClasses>
            </SCS:Toolbar>
             

            <asp:TabContainer runat="server" ID="tbContainer" ActiveTabIndex="1"
                Width="100%">
                <asp:TabPanel runat="server" ID="tbDetails">
                    <HeaderTemplate>
                        Incident Details
                        
                    </HeaderTemplate>

                    <ContentTemplate>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnAddFile" />
                                <asp:PostBackTrigger ControlID="gvDocs" />
                                
                                <asp:PostBackTrigger ControlID="btnAddNote" />
                                <asp:PostBackTrigger ControlID="gvWorkInfo" />
                                
                            </Triggers>
                            <ContentTemplate>
                                <table class="inc-details">
                                     <tr>
                                        <td class="inc-details-label">
                                            Incident Status Type :
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:Label Text="" ID="lblMessage" runat="server" CssClass="toolbar" Width="99.5%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">System Reference Number:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox runat="server" ID="SystemReferenceNo"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Cross Reference Number:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox runat="server" ID="CrossReferenceNo" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Subject:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox runat="server" ID="Subject" Width="300px" MaxLength="255" TextMode="MultiLine" Height="100px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Method Received:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:DropDownList runat="server" ID="MethodReceived" Width="300px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Ministerial Reference Number:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox runat="server" ID="MinisterialReferenceNumber" Width="300px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Commissioners Case:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:CheckBox runat="server" ID="CommissionersCase"></asp:CheckBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="inc-details-label">Date Received:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox type="text" runat="server" ID="DateReceived" Width="150px" />

                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" ClearTime="True"
                                                Enabled="True" Format="yyyy-MM-dd" TargetControlID="DateReceived"
                                                TodaysDateFormat="yyyy-MM-dd">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="inc-details-label">Responsible CO:
                                        </td>
                                        <td>
                                            <uc1:UserSelector ID="ResponsibleGM" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Correct Format Used:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:CheckBox runat="server" ID="CorrectFormatUsed"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Responsible Manager:
                                        </td>
                                        <td>
                                            <uc1:UserSelector ID="ResponsibleManager" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Sub-division:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox runat="server" ID="Subdivision" Width="300px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Author Name:
                                        </td>
                                        <td style="padding-left: 4px;">
                                               <uc1:UserSelector ID="AuthorName" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Type of Submission:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:DropDownList runat="server" ID="TypeOfSubmission" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="TypeOfSubmission_SelectedIndexChanged"></asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="tdSubmissionOther" runat="server" visible="False">Type of Submission Other:
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="TypeOfSubmissionOther" Width="300px" Visible="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Purpose of Document:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:DropDownList runat="server" ID="PurposeOfDocument" Width="300px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Document Sent to Ministry:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:CheckBox runat="server" ID="DocumentSentToMinistry"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Document Received from Ministry:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:CheckBox runat="server" ID="DocumentReceivedFromMinistry"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Comments made by Commissioner:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:CheckBox runat="server" ID="CommentsMadeByCommissioner"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Action Person:
                                        </td>
                                        <td>
                                            <uc1:UserSelector ID="ActionPerson" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Priority:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:DropDownList runat="server" ID="Priority" Width="300px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Responsible Administrator :
                                        </td>
                                        <td>
                                            <uc1:UserSelector ID="ResponsibleAdministrator" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inc-details-label">Due Date:
                                        </td>
                                        <td style="padding-left: 4px;">
                                            <asp:TextBox type="text" runat="server" ID="DueDate" Width="150px" />

                                            <asp:CalendarExtender ID="txtIncidentDueDate_CalendarExtender" runat="server" ClearTime="True"
                                                Enabled="True" Format="yyyy-MM-dd" TargetControlID="DueDate"
                                                TodaysDateFormat="yyyy-MM-dd">
                                            </asp:CalendarExtender>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="inc-details-label">Browse for file :</td>
                                                    <td style="padding-left: 4px;">
                                                        <asp:FileUpload runat="server" ID="flDoc" />
                                                        <asp:Button runat="server" ID="btnAddFile" Text="Upload File" OnClick="UploadFile" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView DataKeyNames="DocId" CssClass="documents" runat="server" ID="gvDocs" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="NO DOCUMENTS FOR THIS WORK INFO" OnPageIndexChanging="gvDocs_PageIndexChanging" OnRowCommand="gvDocs_RowCommand">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="File Name" DataField="DocumentName" />
                                                                <asp:BoundField HeaderText="File Type" DataField="Extension" />
                                                                <asp:BoundField HeaderText="Uploaded By" DataField="FullName" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkOpenFile" runat="server" CommandArgument='<%#Eval("DocId") %>' CommandName="OpenFile" Text="Open File"></asp:LinkButton>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <span onclick="javascript:return confirm('Are you sure you want to delete this document');">

                                                                            <asp:LinkButton runat="server" ID="lnkDelete" CommandArgument='<%#Eval("DocId") %>' CommandName="DeleteFile">Delete</asp:LinkButton>
                                                                        </span>

                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataRowStyle ForeColor="Red" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                        
                                    </tr>
                                    
                                   

                                    <tr>
                                       <td colspan="2">
                                           <table style="width: 100%;">
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                      Notes:<br/>
                                    <asp:TextBox runat="server" ID="txtNotes" Width="99%" Rows="4" TextMode="MultiLine" onkeyup="CountChars(this, 500)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnAddNote" Text="Save Notes" OnClick="AddNote" />
                                </td>
                            </tr>
                            <tr>
                                <td>
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
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                                       </td>
                                    </tr>



                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </ContentTemplate>

                </asp:TabPanel>

            </asp:TabContainer>


        </div>
    </div></div>
</asp:Content>

