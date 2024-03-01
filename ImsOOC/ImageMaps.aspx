<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageMaps.aspx.cs" Inherits="ImageMaps" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#Shop").mousemove(function (e) {
                $('#coordinates').html('x: ' + e.pageX + ' y : ' + e.pageY);
            });
        })
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <h3>
            ImageMap Class Navigate Example</h3>
        <h4>
            Shopping Choices:</h4>
            <center>
        <asp:ImageMap ID="Shop" ImageUrl="Images/IncidentProcess.PNG" Width="100%" Height="360" BorderColor="red" BorderStyle="Solid" BorderWidth="1"
            AlternateText="Shopping choices" runat="Server">
            <asp:CircleHotSpot NavigateUrl="http://www.tailspintoys.com" X="156" Y="245" Radius="75"
                HotSpotMode="Navigate" AlternateText="Shop for toys"></asp:CircleHotSpot>
            <asp:CircleHotSpot NavigateUrl="http://www.cohowinery.com" X="783" Y="239" Radius="75"
                HotSpotMode="Navigate" AlternateText="Shop for wine"></asp:CircleHotSpot>
        </asp:ImageMap>
        </center>
    </div>
    <center>
    <div id="coordinates" style="height: 100px; width: 500px; color: #ffffff; background-color: black;"></div> </center>
    </form>
</body>
</html>
