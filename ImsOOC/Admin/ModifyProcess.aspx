<%@ Page Title="SARS SURVEY - MODIFY SURVEYS" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ModifyProcess.aspx.cs" Inherits="ModifyProcessPage" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.51116.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
    
    </style>
    <script src="../Scripts/boxover.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading">- Modify Process -</div>
        <div class="panel-body">


            <table style="width: 100%">
                <tr>
                    <td>
                        <fieldset>
                            <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"
                                EnableClientApi="False" CssClass="toolbar" Width="99%">
                                <Items>

                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Modify" Text="Modify Process" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="AddFields" Text="Add Fields" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="ViewFields" Text="View Fields" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Review" Text="Review" Visible="false" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Publish" Text="Publish" Visible="false" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="AddUsers" Text="Add Users" Visible="false" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="ViewOwners" Text="Process Owners" />
                                    <SCS:ToolbarButton CausesValidation="True" CommandName="Reminders" Text="Incidents Reminders" />
                                </Items>
                                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                                    CssClassDisabled="button_disabled"></ButtonCssClasses>
                            </SCS:Toolbar>
                        </fieldset>
                    </td>
                </tr>

                <tr>
                    <td>
                        <fieldset>
                            <asp:GridView ID="gvProcesses" runat="server" CssClass="documents" AllowPaging="True" Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="PageIndexChanging"
                                GridLines="None" OnRowDataBound="RowDataBound" DataKeyNames="ProcessId,StatusId"
                                OnSelectedIndexChanging="gvProcesses_SelectedIndexChanging"
                                OnSelectedIndexChanged="SelectedIndexChanged" PageSize="7">
                                <Columns>

                                    <asp:TemplateField HeaderText="Process Name" SortExpression="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcessDescription" runat="server" Text='<%# Utils.getshortString(Eval("Description").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Timestamp" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Date Create" SortExpression="Timestamp">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="ProcessStatus" HeaderText="Status" SortExpression="ProcessStatus">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:CheckBoxField DataField="IsActive" HeaderText="Active">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:CheckBoxField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Left" />
                                <SelectedRowStyle CssClass="selectedRow" />
                            </asp:GridView>
                            <asp:Label runat="server" ID="lblMessage" Visible="False" Font-Bold="True"
                                ForeColor="Red" Width="100%">THERE ARE NO PROCESSES</asp:Label>
                        </fieldset>
                    </td>
                </tr>
                <tr runat="server" id="row_modfy_process" visible="false">
                    <td>
                        <fieldset>
                            <legend>Modify Process Details</legend>


                            <table style="width: 100%;">
                                <tr>
                                    <td class="2" style="text-align: center;">
                                        <table style="width: 100%; text-align: left;">
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <strong>Process Name:</strong></td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:TextBox ID="txtProcessName" runat="server" Width="630px"
                                                        onkeyup="CountChars(this, 250)"
                                                        meta:resourcekey="txtsurveyNameResource1" MaxLength="250"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <strong>Process Administrators</strong></td>
                                            </tr>

                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:ListBox ID="lbOwners" runat="server" Width="300px" Enabled="False" Height="168px"></asp:ListBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <strong>Reference Number Prefix:</strong></td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:TextBox runat="server" ID="txtPrefix" Width="300px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">Muximum Upload File Size(Mega Bytes)</td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:TextBox ID="txtFileSize" runat="server" Height="30px" MaxLength="5" meta:resourcekey="txtsurveyNameResource1" onkeyup="CountChars(this, 250)" Width="62px" Font-Italic="False" Font-Strikeout="False" Style="text-align: center;" ForeColor="#333333">2.5</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">Working Folder URL</td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:TextBox ID="txtWorkingFolderUrl" runat="server" Height="30px"  Width="62px" Font-Italic="False" Font-Strikeout="False"  ForeColor="#333333"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <b>Active:</b></td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:CheckBox ID="chkActive" runat="server" />


                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <b>Add Cover Page:</b></td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:CheckBox ID="ckAddCoverPage" runat="server" />


                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <b>On Complete, Incidents gets finilised by the Process Owner?</b></td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:CheckBox ID="chkCreater" runat="server" />


                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <b>Can access colleague incidents?</b></td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:CheckBox ID="ckCanShareIncidents" runat="server" />


                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px; padding-top: 15px;">
                                                    <asp:Button ID="btnsubmitNext" runat="server" CssClass="buttons"
                                                        Text="Save Process" Width="200px"
                                                        meta:resourcekey="btnsubmitNextResource1" OnClick="SubmitProcessData"
                                                        Height="30px" />
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr id="row_review_process" runat="server" visible="false">
                    <td>
                        <fieldset>
                            <legend>Please complete the check list below</legend>&nbsp;<table cellpadding="0" width="100%">
                                <tr>
                                    <td class="style5">&nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="chkPublishCheckList" runat="server" Width="100%"
                                            CellPadding="4" CellSpacing="2">
                                            <asp:ListItem Value="0">Check if every required field has been populated!</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style5">&nbsp;
                                    </td>
                                    <td>&nbsp;&nbsp;
                                <asp:Button ID="btnReview" runat="server" CssClass="buttons" OnClick="ReviewSurvey_Click"
                                    Text="Complete Review" Width="200px" Height="30px" />
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
