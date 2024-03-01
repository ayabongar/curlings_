<%@ Page Title="" Language="C#" MasterPageFile="~/NoAccess.master" AutoEventWireup="true" CodeFile="NoAccessForm.aspx.cs" Inherits="NoAccessForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            font-size: large;
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <strong><span class="auto-style1">ACCES DENIED</span></strong> 
</asp:Content>

