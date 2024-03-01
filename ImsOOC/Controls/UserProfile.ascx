<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserProfile.ascx.cs" Inherits="Controls_UserProfile" %>

<style>
    td {
        color: black;
    }

    .ace-nav > li {
        line-height: 20px !important;
    }
    .dropdown-menu.dropdown-close.dropdown-menu-right {
        left: auto;
        right: 10px!important;
    }
    .bold{
        font-weight:bolder;
        background-color:whitesmoke;
        width:40%;
    }
</style>
<ul class="user-menu dropdown-menu-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close">


    <li>

        <table style="width: 100%;" runat="server" id="tblUser" border="0">
            <tr>
                <th colspan="2" style="background-color: #555555; text-align: central; color: white">My Profile</th>
            </tr>
            <tr>
                <td class="bold">SID:</td>
                <td>
                    <asp:Label runat="server" ID="txtSID" ></asp:Label>


                </td>
            </tr>
            <tr>
                <td class="bold">Full Name:</td>
                <td>

                    <asp:Label runat="server"  ID="txtFirstName"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="bold">EmailAddress:</td>
                <td>
                    <asp:Label runat="server"  ID="txtEmailAddress"></asp:Label>

                </td>
            </tr>
            <tr>
                <td class="bold">Telephone:</td>
                <td>

                    <asp:Label runat="server"  ID="txtTelephone"></asp:Label>


                </td>
            </tr>
           
            <tr>
                <td class="bold">Role</td>
                <td>
                    <asp:Label runat="server" ID="ddlRoles" />
                </td>
            </tr>

           
            

        </table>

    </li>


</ul>
