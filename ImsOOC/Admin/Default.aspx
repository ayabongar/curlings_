<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/NormalUser.master" CodeFile="Default.aspx.cs" Inherits="SurveyWizard_Default" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

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
            width: 150px!important;
        }
        .wrapper{
              z-index:inherit;
              max-width: 1200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <table style="width: 100%;">
        <tr>
            <td style="vertical-align: top; width: 20%; border-right: 1px solid silver;">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                            MY PROCESSES
                    </div>
                    <div class="panel-body">
                        <asp:GridView ID="gvProcesses" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CssClass="documents" DataKeyNames="ProcessId,StatusId,Description" GridLines="None" OnPageIndexChanging="PageIndexChanging"
                            OnRowDataBound="RowDataBound" PageSize="20" Width="100%"
                            OnSelectedIndexChanged="gvProcesses_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderText="MY SYSTEMS" SortExpression="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcessDescription" runat="server" Text='<%# String.Format("{0} v{1}", Utils.getshortString(Eval("Description").ToString()), Eval("Version")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="selectedRow" />
                        </asp:GridView>
                    </div>
                </div>


                

            </td>
            <td>

                
     <div class="panel panel-primary">
        <div class="panel-heading" >WELCOME TO OUR INCIDENT TRACKING</div>
        <div class="panel-body" style="min-height:400px;background-color:white">
       
            <span id="spnLogo" visible="true" style="width: 100%;background-color:white; height: 800px;" runat="server">
				</span>
        </div>
    </div>


            </td>
        </tr>
    </table>



    
</asp:Content>

