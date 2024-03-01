<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewMyIncidents.aspx.cs" Inherits="SurveyWizard_ViewMyIncidents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

  
        <div class="panel panel-primary">
        <div class="panel-heading">MY INCIDENTS</div>
        <div class="panel-body">

        
       <%-- <fieldset>
            <table style="width: auto;">
                <tr>
                    <td class="auto-style1">Status: </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStatuses" Width="300px" AutoPostBack="True" Height="25px">
                        </asp:DropDownList></td>
                    
                    <td>
                        <asp:Button Text="Filter" Height="26px" runat="server" ID="btnFilter" OnClick="btnFilter_Click" Width="83px" />
                    </td>
                </tr>
            </table>
        </fieldset>--%>
        <fieldset>
                        <asp:GridView runat="server" ID="gvIncidents" CssClass="documents" Width="100%" DataKeyNames="IncidentID,IncidentNumber,ProcessId" AutoGenerateColumns="False" OnRowDataBound="RowDataBound" GridLines="Horizontal" OnPageIndexChanging="PageChanging" AllowPaging="True">
                            <Columns>

                                <asp:BoundField DataField="IncidentNumber" HeaderText="Incident Number">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Timestamp" HeaderText="Registered" DataFormatString="{0:yyyy-MM-dd hh:mm}">
                                    <HeaderStyle HorizontalAlign="Left" />
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
                                
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                         <table>
                                             <tr>
                                                 <td>
                                                     <asp:Button ID="btnView" runat="server" Text="View" Width="100px" OnClick="OpenIncident"></asp:Button>
                                                 </td>
                                                 <td>
                                                     <asp:Button ID="bntReAssign" runat="server" Text="Re-Assign" 
                                            OnClick="ChangeAssignee" Width="100px"></asp:Button>
                                                 </td>
                                                 <td>
                                                     <asp:Button ID="btnComplete" runat="server" Text="Complete" 
                                            onclick="CompleteIncident" Width="100px"></asp:Button>
                                                 </td>
                                             </tr>
                                         </table>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="selectedRow" />
                        </asp:GridView>
              <asp:Label runat="server" ID="lblMessage" Visible="False" Font-Bold="True"
                            ForeColor="Red" Width="100%">THERE ARE NO INCIDENTS ASSIGNED TO YOU</asp:Label>
        </fieldset>
    
    
    </div>
    </div>
</asp:Content>

