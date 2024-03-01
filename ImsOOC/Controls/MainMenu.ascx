<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainMenu.ascx.cs" Inherits="Controls_MainMenu" %>
<style>
    #MainMenu_navigation 
{
	top: 5em;
	left: 1em;
	position: static;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size:1em;
}

#MainMenu_navigation ul 
{
    list-style: none;
	margin: 0;
	padding: 0;
}

#MainMenu_navigation li 
{
	border-bottom: 1px solid #FFFFFF;
}

.home-item
{
	display: block;
	padding: 5px 5px 5px 0.5em;
	border-left: 12px solid #336699;
	border-right: 1px solid #6495ED;
	background-color: #6495ED;
	color: #FFFFFF;
	text-decoration: none;	
}

.aheading
{
	font-size:11px;
	 background: linear-gradient(to bottom, #428bca, #ffffff);
        color:#000;
}
.asubmenu
{
	font-size:11px;
	   color:black!important;
	display: block;
	padding: 5px 5px 5px 0.5em;
	border-left: 12px solid #336699;
	border-right: 1px solid #6495ED;
	background-color: #6495ED;
	color: #FFFFFF;
	text-decoration: none;	
	 background: linear-gradient(to right, #428bca, #ffffff);
        color:black!important;
}

#MainMenu_navigation .submenu_block
{
	margin-left: 12px;
}

#MainMenu_navigation ul.submenu_block li 
{
	border-bottom: 1px solid #336699;
	margin:0;
	background-color: #6495ED;
	color: #711515;

}

#MainMenu_navigation a.aheading:link, #MainMenu_navigation a.aheading:visited, #MainMenu_navigation ul li a.aheading:link,
a.aheading:link
{
	background-color: #4682B4;
	color: #000080;
	
}

#MainMenu_navigation a.aheading:hover 
{
	background-color: #336699;
	color: #FFFFFF;
}
.heading-clicked
{
    background-color: #336699;
	color: #FFFFFF;
}

#MainMenu_navigation li a:hover 
{
	background-color: #336699;
	color: #FFFFFF;
}
.error
{
    color: #FFFF00;
    font-family: verdana;
    font-size: large;    
}

.heading-clicked
{
    background-color: #336699;
	color: #FFFFFF;
}
#MainMenu_navigation ul li {
    border-radius: 6px 0 6px 0 !important;

}

#MainMenu_navigation ul li a{
    border-radius : 6px  0 6px 0!important;
    color:white;
    padding:10px!important;
}

</style>
<span id="SideNav3" style="display: inline-block; width: 280px;">
  
    <div id="navigation" runat="server" ></div>
</span>