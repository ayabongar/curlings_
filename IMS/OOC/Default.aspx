<%@ Page Title="INCIDENT TRACKING SYSTEM" Language="C#" MasterPageFile="~/OOC/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <div class="panel panel-primary">
        <div class="panel-heading" >WELCOME TO OUR  OOC INCIDENT TRACKING</div>
        <div class="panel-body" style="min-height:400px;background-color:white">
         <img alt="SARS LOGO" src="Images/IncidentProcess.PNG"  style="width: 100%;" runat="server" id="imgWorkFlow" class="roundeddiv" /><br />
            <span id="spnLogo" visible="false" style="width: 100%;background-color:white; height: 800px;" runat="server">Executive Document Management System (EDMS) ensures that the documents flow from the moment of creation or receipt until completion or dispatch, as well as document preparation, execution, coordination, execution control by structural divisions.</span>
        </div>
    </div>
</asp:Content>
