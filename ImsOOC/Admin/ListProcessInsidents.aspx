<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ListProcessInsidents.aspx.cs" Inherits="Admin_ListProcessInsidents" %>

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
        <div class="panel-heading">Incident List For : <%= CurrentProcess.Description %></div>
        <div class="panel-body">
        <fieldset>
            <SCS:Toolbar ID="Toolbar1" runat="server" OnButtonClicked="Toolbar1_ButtonClicked"
                EnableClientApi="False" CssClass="toolbar" Width="99%">
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="ViewDetails" Text="View Details" />
                    <SCS:ToolbarButton CausesValidation="True" CommandName="AttachDocuments" Text="Attach Documents" Visible="False" />
                </Items>

                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected=""
                    CssClassDisabled="button_disabled"></ButtonCssClasses>
            </SCS:Toolbar>
        </fieldset>
       
        <fieldset>
            <asp:GridView runat="server" ID="gvIncidents" CssClass="documents"  Width="100%" DataKeyNames="IncidentID,IncidentNumber,ProcessId" AutoGenerateColumns="False" OnRowDataBound="RowDataBound" GridLines="Horizontal" OnPageIndexChanging="PageChanging" AllowPaging="True">
                <Columns>

                    <asp:BoundField DataField="IncidentNumber" HeaderText="Incident Number" >
                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Timestamp" HeaderText="Registered" DataFormatString="{0:yyyy-MM-dd hh:mm}" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="Summary" SortExpression="Summary">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcessDescription" runat="server" Text='<%# Utils.getshortString(Eval("Summary") != null ? Eval("Summary").ToString() : string.Empty) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd hh:mm}" >

                    <HeaderStyle VerticalAlign="Top" />
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IncidentStatus" HeaderText="Status">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

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



