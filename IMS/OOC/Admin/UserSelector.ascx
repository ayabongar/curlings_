<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserSelector.ascx.cs" Inherits="Admin_UserSelector" %>

<script type="text/javascript">
 

</script>



<table style="width: 100%; text-align: left;">

    <tr>
        <td>
              <span class="block input-icon input-icon-right">
    <asp:TextBox ID="txtSearchName" runat="server"  CssClass="width-100" MaxLength="200" ></asp:TextBox>
               <i class="ace-icon fa fa-user"></i>       
        </span>
            
        </td>
    </tr>

    <tr>
        <td id="userContainer" runat="server"></td>
    </tr>

</table>


