<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divContent" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                Application Error
            </div>
            <table style="border: 0px; background-color: #fff; background: #fff; width: 100%;" cellspacing="0">
                                            <tr cellspacing="0">
                                                <td cellspacing="0">
                                                    <table style="border: 0px; width: 100%" align="center" cellspacing="0">
                                                        <tr>
                                                            <td class="heading"><strong>Sorry!</strong></td>

                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="color: #bcbabc; text-align: center"><b>
                                                </b></td>
                                            </tr>
                                            <tr style="height: auto; width: 100%">
                                                <td valign="top">
                                                    <table>
                                                        <tr>
                                                            <td valign="middle" style="text-align: center; color: #bcbabc">We're so sorry there is a problem with the IMS at the moment, try again if persist please contact the system administrator.
                                                    
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
        </div>
    </div>
</asp:Content>

