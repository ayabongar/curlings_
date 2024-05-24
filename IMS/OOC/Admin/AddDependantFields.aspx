<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddDependantFields.aspx.cs" Inherits="Admin_AddDependantFields" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            CONFIGURE CHILD FIELDS [<%= CurrentProcess.Description%>]</div>
        <div class="panel-body">
            <fieldset>
                <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"
                    EnableClientApi="False" CssClass="toolbar" Width="99%">
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="Add" Text="Add Child Field" />
                         <SCS:ToolbarButton CausesValidation="True" CommandName="Back" Text="Back" />
                    </Items>
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                        CssClassDisabled="button_disabled"></ButtonCssClasses>
                </SCS:Toolbar>
                <table style="width: 99%">
                    <tr>
                        <td colspan="2"><asp:TextBox ID="txtFieldName" style="padding-left: 5px; text-transform: uppercase; font-family: verdana;" runat="server" Enabled="False" Width="100%" Height="30px"/></td>
                    </tr>

                    <tr>
                        <td style="width: 400px; vertical-align: top;">
                            <fieldset><legend>Options</legend>
                            <asp:RadioButtonList runat="server" ID="lstOptions" Width="100%" AutoPostBack="True"
                                Font-Names="Verdana" Font-Size="X-Small" 
                                    onselectedindexchanged="lstOptions_SelectedIndexChanged"/>
                                </fieldset>
                        </td>
                        <td style="vertical-align: top;">
                            <fieldset><legend>Possible Child Fields</legend>
                            <asp:CheckBoxList runat="server" ID="lstChildFields" Width="100%" 
                                Font-Names="Verdana" Font-Size="X-Small"/>
                                </fieldset>
                        </td>
                    </tr>

                   <%-- <tr>
                        <td style="width: 400px">
                            <asp:Button ID="btnAddChild" runat="server" Text="Add Child Field" Height="30px" Width="300px" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>--%>
                </table>
                
            </fieldset>
            
        </div>
    </div>
</asp:Content>

