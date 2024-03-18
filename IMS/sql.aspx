<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sql.aspx.cs" Inherits="sql" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:TextBox runat="server" id="txtUpdate" TextMode="MultiLine" Height="588px" Width="1079px"></asp:TextBox><br/>
        <asp:Button runat="server"  ID="btnSave" Text="Run" OnClick="btnSave_Click" />
    </div>
    </form>
</body>
</html>
