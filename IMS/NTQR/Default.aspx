<%@ Page Title="" Language="C#" MasterPageFile="~/Cms.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="NTQR_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <style>
        td{
            text-align:left!important;
        }
        input[type=text] {
            width: 200px !important;
            height: 30px !important;
            border: 1px solid #ccc;
            border-radius: 5px;
            color: #336699 !important;
            text-align: left !important;
        }
        input[type=checkbox]{
   text-align:left!important;
}
       input[type=image]{
   vertical-align:text-top!important;
}

        select {
            width: 200px !important;
            height: 30px !important;
            border: 1px solid #ccc;
            border-radius: 5px;
            color: #336699 !important;
            text-align: left !important;
        }
       
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading" >
            WELCOME TO NATIONAL TREASURY QUARTERLY REPORTING</div>
        

        <div class="panel-body" style="text-align: center; height:auto; overflow-y:auto">
            <%--<img src="Images/sars_survey_logo.gif" />--%>
            <asp:Panel ID="pnlReport"  runat="server" >
        
            </asp:Panel>
        </div>
    </div>
</asp:Content>


