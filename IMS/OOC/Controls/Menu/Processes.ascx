<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Processes.ascx.cs" Inherits="Controls_Menu_Processes" %>
<style>
     
.menu{
  width: 100%!important;
  padding: 0;
  position: relative;
  float: left;
  background: #000;
  list-style: none;
  font-family: Century Gothic, sans-serif;
}
.menu li {
  display: inline!important;
  font-size: 10px;
  margin: 0;
  padding: 0;
  float: left;
  line-height: 20px;
  position: relative;
  border-right:1px #000 solid;
}
.menu li a {
  padding: 15px 20px 15px;
  color: #FFF;
  text-decoration: none;
  display: inline-block;
  -o-transition: color .3s linear, background .3s linear;
  -webkit-transition: color .3s linear, background .3s linear;
  -moz-transition: color .3s linear, background .3s linear;
  transition: color .3s linear, background .3s linear;  
}
.menu li:hover > a{
  color: #333;
  background:rgb(229,228,226);
}
.menu li.active > a{
  background:rgb(229,228,226);
  color:#000;
}
.menu > li > a {
  text-transform: uppercase;
}

/* DROPDOWN */
.menu ul, 
.menu ul li ul {
  list-style: none;
  margin: 0;
  padding: 0;    
  display: none;
  position: absolute;
  z-index: 999;
  width: 100%!important;
  background: #454545;
}
.menu ul{
   top: 50px;
   left: 0;
}
.menu ul li ul{
    top: 0;
    left: 150px;
}
.menu ul li{
  clear:both;
  width:100%!important;
  font-size:14px;
}
.menu ul li a {
  width:100%;
  padding:12px 12px;
  display:inline-block;
  float:left;
  clear:both;
  box-sizing:border-box;
  -moz-box-sizing:border-box; 
  -webkit-box-sizing:border-box;
  text-transform:uppercase;
}
.menu ul li:hover > a{
  background:rgb(229,228,226);
  color:#000;
}
@media only screen and (max-width: 767px) {
  .menu{
    margin: 0;
  }
  .menu li{
    display: block;
    width: 100%;
  }
  .menu > li > a{
    width: 100%;
    padding: 16px 70px 16px 18px;
    text-align: left;
    border-top: solid 1px rgba(255, 255, 255, 0.05);
    box-sizing:border-box;
    -moz-box-sizing:border-box; 
    -webkit-box-sizing:border-box;
  }
  .menu ul, 
  .menu ul li ul{
    width: 100%;
    left: 0;
    padding: 0 20px;
    position: static;
    box-sizing:border-box;
    -moz-box-sizing:border-box; 
    -webkit-box-sizing:border-box; 
  }

}
.menu li:hover ul, .menu li:hover ul li ul {
  display:block;
}

</style>
<div id="menu" runat="server"></div>