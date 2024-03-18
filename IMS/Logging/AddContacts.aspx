<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddContacts.aspx.cs" Inherits="Logging_AddContacts" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="x-ua-compatible" content="IE=9">
    <title>::CONTACTS::</title>
    <link href="logger.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="page">
                <table style="width: 100%; padding: 0px; border-collapse: collapse;">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <div class="page-header">
                                            Add/Remove Contact
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset>
                                <table style="width:100%;">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label Text="" runat="server"  ID="lblError" ForeColor="Red" Font-Bold="true"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label-column">
                                            Contact Full Name:
                                        </td>
                                        <td class="field-column">
                                            <asp:TextBox runat="server" ID="txtName" CssClass="text-field-long" />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="label-column">Email Address:</td>
                                        <td class="field-column"><asp:TextBox runat="server" ID="txtEmailAddress" CssClass="text-field-long" /></td>
                                    </tr>
                                     <tr>
                                        <td class="label-column">&nbsp;</td>
                                        <td class="field-column">
                                            <asp:Button ID="btnAddContact" runat="server" CssClass="buttons" Text="Add Contact" OnClick="btnAddContact_Click" />
                                            <asp:Button ID="btnRemove" runat="server" CssClass="buttons" Text="Remove" OnClick="RemoveContact" />
                                         </td>
                                    </tr>
                                     <tr>
                                        <td class="label-column">&nbsp;</td>
                                        <td class="field-column">
                                          <asp:ListBox ID="lbDetails" runat="server" CssClass="rounded" Width="300px" Height="119px"></asp:ListBox>
                                         </td>
                                    </tr>
                                     <tr>
                                        <td class="label-column">&nbsp;</td>
                                        <td class="field-column">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <div class="page-footer">
                                        </div>
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
