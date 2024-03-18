<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" CodeFile="UserUnits.aspx.cs" Inherits="NTQR_UserUnits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">Add Or Update User Units</div>
        <div class="pageBody">
             <SCS:Toolbar ID="Toolbar1" ClientIDMode="Static" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"
                        EnableClientApi="False" CssClass="toolbar" Width="100%">
                        <Items>
                            <SCS:ToolbarButton CausesValidation="True" CommandName="Open" Text="Add" />
                           
                        </Items>
                        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                            CssClassDisabled="button_disabled"></ButtonCssClasses>
                    </SCS:Toolbar>
            <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CssClass="documents" EmptyDataText="NO RECORD FOUND"
                GridLines="Horizontal"  PageSize="100" DataKeyNames="Id"
                Width="100%" DataSourceID="SqlDataSource1">
                <EmptyDataRowStyle ForeColor="Red" />
                <Columns>
                                            <asp:BoundField DataField="Unit" HeaderText="Unit" ItemStyle-Font-Size="Large">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Objective" HeaderText="Objective">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                      <asp:BoundField DataField="KeyResult" HeaderText="KeyResult">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnRemove" runat="server" OnClick="Remove" Text="Remove"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db %>" OldValuesParameterFormatString="original_{0}"
                SelectCommand="SELECT uk.Id, u.Name AS Unit, o.Name AS Objective, k.Name AS KeyResult
FROM     NTQ_UserKeyResults AS uk LEFT OUTER JOIN
                  NTQ_Lookup_StrategicObjective AS o ON uk.fk_ObjectiveId = o.Id LEFT OUTER JOIN
                  NTQ_User_Units AS u ON uk.fk_NTQ_User_Unit_Id = u.Id RIGHT OUTER JOIN
                  NTQ_Lookup_KeyResult AS k ON uk.fk_KeyResultId = k.Id
                 where not k.Name is null
ORDER BY o.Id, u.Id, k.Id"
                >
               
                
            </asp:SqlDataSource>

           

            <asp:SqlDataSource ID="dsUnits" runat="server" ConnectionString="<%$ ConnectionStrings:db %>" SelectCommand="SELECT [Id], [Name] FROM [NTQ_User_Units]"></asp:SqlDataSource>

           

        </div>

    </div>
      <asp:Panel runat="server" ID="pnlObjectives" Width="40%" ScrollBars="Both" BackColor="White">
        <table style="width: 100%">
            <tr>
                <td>
                    <SCS:Toolbar ID="Toolbar3" ClientIDMode="Static" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"
                        EnableClientApi="False" CssClass="toolbar" Width="100%">
                        <Items>
                            <SCS:ToolbarButton CausesValidation="True" CommandName="Save" Text="Save" />
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

                                    <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Select  a Unit: <span style="color: red">*</span></label>

                                    <div class="col-xs-12 col-sm-4">
                                        <span class="block input-icon input-icon-right">
                                             <asp:DropDownList ID="drpUnits" class="width-100" DataSourceID="dsUnits" ValidationGroup="popup" runat="server"  DataTextField="Name" DataValueField="Id"  >
                                        </asp:DropDownList>

                                            <asp:RequiredFieldValidator ErrorMessage="Units is a required field!" ValidationGroup="popup" Text="*" ForeColor="Red" ControlToValidate="drpUnits" runat="server" />

                                            
                                        </span>
                                    </div>

                                </div>
                                <div class="form-group has-info" runat="server">
                                    <label for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Strategic Objective: <span style="color: red">*</span></label>

                                    <div class="col-xs-12 col-sm-4">


                                        <asp:DropDownList ID="drpStrategicObjective" class="width-100" runat="server" ValidationGroup="popup" AutoPostBack="True" OnSelectedIndexChanged="drpStrategicObjective_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ErrorMessage="Quarter is a required field!" Text="*" ValidationGroup="popup" ForeColor="Red" ControlToValidate="drpStrategicObjective" runat="server" />

                                    </div>
                                 

                                </div>

                                <div class="form-group has-info" runat="server">
                                    <label for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Key Result: <span style="color: red">*</span></label>

                                    <div class="col-xs-12 col-sm-4">


                                        <asp:DropDownList ID="drpKeyResult" class="width-100" ValidationGroup="popup" runat="server"  >
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ErrorMessage="Quarter is a required field!" Text="*" ValidationGroup="popup" ForeColor="Red" ControlToValidate="drpKeyResult" runat="server" />

                                    </div>
                                
                                </div>
                                



                        <div style="visibility: hidden">
                                        <asp:Button runat="server" ID="btnView" Text="View Case Details" />
                                        <asp:Button runat="server" ID="btnCancel" Text="View Case Details" />
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
</asp:Content>

