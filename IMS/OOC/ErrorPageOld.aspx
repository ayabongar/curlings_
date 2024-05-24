<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPageOld.aspx.cs" Inherits="ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en-ZA">
<head id="Head1" runat="server">
    <title></title>
      <meta http-equiv="x-ua-compatible" content="IE=edge" />
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/survey.css" rel="stylesheet" type="text/css" />
   
    <style type="text/css">
        .style1 {
            width: 8px;
            height: 8px;
        }

        .style2 {
            height: 8px;
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
    </style>
    


</head>
<body>
    <form id="Form1" runat="server" autocomplete="off">
      
        <table class="page">
            <tr>
                <td>
                    <div class="header">
                        <table style="width: 100%; text-align: right; font-size: 12px; color: #FFFFFF; vertical-align: top;">
                            <tr>
                                <td style="text-align: left;">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">
                                        <asp:Image runat="server" ID="logo" ImageUrl="~/Images/logo-sars.png" BorderStyle="None"
                                            ImageAlign="Left" />
                                    </asp:HyperLink>
                                </td>
                                <td style="padding-right: 10px; text-align: right;">
                                    <table style="width: 100%; float: right;">
                                        <tr>
                                            <td><span style="float: right; font-size: x-large; font-family: Century; color: #ffffff;">
                                                <%=ConfigurationManager.AppSettings["version"] %></span></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span runat="server" id="welcome" style="font-size: small; float: right; padding-top: 0; text-align: right; color: #ffffff;"></span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                      


                            <table style="width: 100%;">
                                <tr>
                                    <td valign="top" style="width: 280px">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                MAIN MENU
                                            </div>
                                            <div class="panel-body">
                                              
                                            </div>
                                        </div>
                                    </td>
                                    <td valign="top" style="text-align: left;">

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
                                                <td style="color: #bcbabc; text-align: center"><b>Application Error
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

                                    </td>
                                </tr>
                            </table>
                          
               
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
