using ImsService.BO;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ImsService.BO
{
   public static class EmailTemplates
    {
       public static string AssignedVdpToYou()
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("   <html> ");
           sb.Append(" <body> ");
           sb.Append("  <center> ");
           sb.Append("   <table style='margin: 0px 10px; border-top-left-radius: 10px; border-top-right-radius: 10px; ");
           sb.Append("     width: 747px;'> ");
           sb.Append("    <tr> ");
           sb.Append(
               "     <td style='border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: silver; ");
           sb.Append("      width: 640px; text-align: center; height: 100px; font-size: 30pt; color: #fff; ");
           sb.Append("    font-family: Arial, Verdana, Comic Sans MS'> ");
           sb.Append("    <b>Incident Tracking System Notification</b> ");
           sb.Append("  </td> ");
           sb.Append("    </tr> ");
           sb.Append("   <tr> ");
           sb.Append("   <td class='w640' bgcolor='#ffffff' height='30' width='640'> ");
           sb.Append("   </td> ");
           sb.Append("   </tr> ");
           sb.Append("   <tr id='simple-content-row'> ");
           sb.Append("   <td class='w640' bgcolor='#ffffff' width='640'> ");
           sb.Append("    <table class='w640' border='0' cellspacing='0' cellpadding='0' width='640'> ");
           sb.Append("  <tbody> ");
           sb.Append("       <tr> ");
           sb.Append("     <td class='w30' width='30'> ");
           sb.Append("    </td> ");
           sb.Append("    <td class='w580' width='580'> ");
           sb.Append("  <repeater> ");

           sb.Append("   <layout label='Text only'> ");
           sb.Append("  <table class='w580' border='0' cellspacing='0' cellpadding='0'> ");
           sb.Append("  <tbody><tr> ");
           sb.Append("  <td class='w580' width='580'> ");
           sb.Append("    <div class='article-content' align='left'> ");
           sb.Append("    <table style='font-family: Verdana;  width: 700px; color: white; padding: 10px'> ");
           sb.Append("   <tr> ");
           sb.Append("        <td class='header' align='left' style='font-size: 10pt; color: Gray;' > ");
           sb.Append("       Good day <strong>{0}</strong><br /> ");
           sb.Append("      <br /> ");
           sb.Append("    <br /> ");
           sb.Append("    The following incident with reference number <a href='{5}'>{2}</a>  has been assigned to  ");
           sb.Append("   you. You  ");
           sb.Append("  are required to action and respond to it on or before the date : {3}<br /> ");
           sb.Append("   <br /> ");
           sb.Append("   Summary : {1}<br /> ");
           sb.Append("   <br /> ");
           sb.Append("     <br /> ");
           sb.Append("    </td> ");
           sb.Append("   </tr> ");
           sb.Append("  <tr> ");
           sb.Append("  <td style='font-size: 10pt; color: Gray;'> ");

           sb.Append("   <b>Incident Details</b> ");
           sb.Append("   <table width='100%' border='0' cellpadding='5px' style='font-size: 10pt; color: Gray;'> ");
           sb.Append("   <tr> ");
           sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;'> ");
           sb.Append("     Reference Number: ");
           sb.Append("     </td> ");
           sb.Append("    <td> ");
           sb.Append("    <a href='{5}'>{2}</a> ");
           sb.Append("   </td> ");

           sb.Append("   </tr> ");
           sb.Append("    <tr> ");
           sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;' > ");
           sb.Append("       Incident Status: ");
           sb.Append("    </td> ");
           sb.Append("     <td> ");
           sb.Append("      {4} ");
           sb.Append("     </td> ");

           sb.Append("   </tr> ");
           sb.Append("   <tr> ");
           sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;' > ");
           sb.Append("       Due Date: ");
           sb.Append("   </td> ");
           sb.Append("    <td> ");
           sb.Append("       {3} ");
           sb.Append("    </td> ");

           sb.Append("  </tr> ");

           sb.Append("   <tr> ");
           sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;' > ");
           sb.Append("       Application Name: ");
           sb.Append("   </td> ");
           sb.Append("    <td> ");
           sb.Append("       {6} ");
           sb.Append("    </td> ");

           sb.Append("  </tr> ");
           sb.Append("  </table> ");

           sb.Append("   </td> ");
           sb.Append("  </tr> ");


           sb.Append("   <tr> ");
           sb.Append("  <td style='font-size: 10pt; color: Gray;'> ");
           sb.Append("   <br /> ");
           sb.Append("   <b>Kind Regards<br/>Incident Resolution System ");

           sb.Append("    </b><br /><br /> ");
           sb.Append("    </td> ");
           sb.Append("   </tr> ");
           sb.Append("    </table> ");
           sb.Append("   </div> ");
           sb.Append(" </td> ");
           sb.Append("  </tr> ");

           sb.Append("  </tbody></table> ");
           sb.Append("    </layout> ");
           sb.Append("   </td> ");
           sb.Append("    <td class='w30' width='30'> ");
           sb.Append("    </td> ");
           sb.Append("   </tr> ");
           sb.Append("   </tbody> ");
           sb.Append("  </table> ");
           sb.Append("  </td> ");
           sb.Append("   </tr> ");
           sb.Append("   <tr> ");
           sb.Append(
               "     <td style='border-bottom-left-radius: 5px; border-bottom-right-radius: 5px; background-color: silver; ");
           sb.Append(
               "     width: 640px; text-align: left; height: 20px; color: #fff; font-family: Arial, Verdana, Comic Sans MS; ");
           sb.Append("     padding: 2px'> ");
           sb.Append("     <b>NB: This is an automated email, do not reply. </b> ");
           sb.Append("   </td> ");
           sb.Append("  </tr> ");
           sb.Append("  </table> ");
           sb.Append("  </center> ");
           sb.Append(" </body> ");
           sb.Append(" </html> ");


           return sb.ToString();

       }

       public static string AssignedToYou()
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("   <html> ");
           sb.Append(" <body> ");
           sb.Append("  <center> ");
           sb.Append("   <table style='margin: 0px 10px; border-top-left-radius: 10px; border-top-right-radius: 10px; ");
           sb.Append("     width: 747px;'> ");
           sb.Append("    <tr> ");
           sb.Append(
               "     <td style='border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: silver; ");
           sb.Append("      width: 640px; text-align: center; height: 100px; font-size: 30pt; color: #fff; ");
           sb.Append("    font-family: Arial, Verdana, Comic Sans MS'> ");
           sb.Append("    <b>Incident Tracking System Notification</b> ");
           sb.Append("  </td> ");
           sb.Append("    </tr> ");
           sb.Append("   <tr> ");
           sb.Append("   <td class='w640' bgcolor='#ffffff' height='30' width='640'> ");
           sb.Append("   </td> ");
           sb.Append("   </tr> ");
           sb.Append("   <tr id='simple-content-row'> ");
           sb.Append("   <td class='w640' bgcolor='#ffffff' width='640'> ");
           sb.Append("    <table class='w640' border='0' cellspacing='0' cellpadding='0' width='640'> ");
           sb.Append("  <tbody> ");
           sb.Append("       <tr> ");
           sb.Append("     <td class='w30' width='30'> ");
           sb.Append("    </td> ");
           sb.Append("    <td class='w580' width='580'> ");
           sb.Append("  <repeater> ");

           sb.Append("   <layout label='Text only'> ");
           sb.Append("  <table class='w580' border='0' cellspacing='0' cellpadding='0'> ");
           sb.Append("  <tbody><tr> ");
           sb.Append("  <td class='w580' width='580'> ");
           sb.Append("    <div class='article-content' align='left'> ");
           sb.Append("    <table style='font-family: Verdana;  width: 700px; color: white; padding: 10px'> ");
           sb.Append("   <tr> ");
           sb.Append("        <td class='header' align='left' style='font-size: 10pt; color: Gray;' > ");
           sb.Append("       Good day <strong>{0}</strong><br /> ");
           sb.Append("      <br /> ");
           sb.Append("    <br /> ");
           sb.Append("    The following incident with reference number <a href='{5}'>{2}</a>  has been assigned to  ");
           sb.Append("   you. You  ");
           sb.Append("  are required to action and respond to it on or before the date : {3}<br /> ");
           sb.Append("   <br /> ");
           sb.Append("   Summary : {1}<br /> ");
           sb.Append("   <br /> ");
           sb.Append("     <br /> ");
           sb.Append("    </td> ");
           sb.Append("   </tr> ");
           sb.Append("  <tr> ");
           sb.Append("  <td style='font-size: 10pt; color: Gray;'> ");

           sb.Append("   <b>Incident Details</b> ");
           sb.Append("   <table width='100%' border='0' cellpadding='5px' style='font-size: 10pt; color: Gray;'> ");
           sb.Append("   <tr> ");
           sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;'> ");
           sb.Append("     Reference Number: ");
           sb.Append("     </td> ");
           sb.Append("    <td> ");
           sb.Append("    <a href='{5}'>{2}</a> ");
           sb.Append("   </td> ");

           sb.Append("   </tr> ");
           sb.Append("    <tr> ");
           sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;' > ");
           sb.Append("       Incident Status: ");
           sb.Append("    </td> ");
           sb.Append("     <td> ");
           sb.Append("      {4} ");
           sb.Append("     </td> ");

           sb.Append("   </tr> ");
           sb.Append("   <tr> ");
           sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;' > ");
           sb.Append("       Due Date: ");
           sb.Append("   </td> ");
           sb.Append("    <td> ");
           sb.Append("       {3} ");
           sb.Append("    </td> ");

           sb.Append("  </tr> ");

           sb.Append("  </table> ");

           sb.Append("   </td> ");
           sb.Append("  </tr> ");


           sb.Append("   <tr> ");
           sb.Append("  <td style='font-size: 10pt; color: Gray;'> ");
           sb.Append("   <br /> ");
           sb.Append("   <b>Kind Regards<br/>Incident Resolution System ");

           sb.Append("    </b><br /><br /> ");
           sb.Append("    </td> ");
           sb.Append("   </tr> ");
           sb.Append("    </table> ");
           sb.Append("   </div> ");
           sb.Append(" </td> ");
           sb.Append("  </tr> ");

           sb.Append("  </tbody></table> ");
           sb.Append("    </layout> ");
           sb.Append("   </td> ");
           sb.Append("    <td class='w30' width='30'> ");
           sb.Append("    </td> ");
           sb.Append("   </tr> ");
           sb.Append("   </tbody> ");
           sb.Append("  </table> ");
           sb.Append("  </td> ");
           sb.Append("   </tr> ");
           sb.Append("   <tr> ");
           sb.Append(
               "     <td style='border-bottom-left-radius: 5px; border-bottom-right-radius: 5px; background-color: silver; ");
           sb.Append(
               "     width: 640px; text-align: left; height: 20px; color: #fff; font-family: Arial, Verdana, Comic Sans MS; ");
           sb.Append("     padding: 2px'> ");
           sb.Append("     <b>NB: This is an automated email, do not reply. </b> ");
           sb.Append("   </td> ");
           sb.Append("  </tr> ");
           sb.Append("  </table> ");
           sb.Append("  </center> ");
           sb.Append(" </body> ");
           sb.Append(" </html> ");


           return sb.ToString();

       }

       public static string EscalateToManagers()
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("   <html> ");
           sb.Append(" <body> ");
           sb.Append("  <center> ");
           sb.Append("   <table style='margin: 0px 10px; border-top-left-radius: 10px; border-top-right-radius: 10px; ");
           sb.Append("     width: 747px;'> ");
           sb.Append("    <tr> ");
           sb.Append(
               "     <td style='border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: silver; ");
           sb.Append("      width: 640px; text-align: center; height: 100px; font-size: 30pt; color: #fff; ");
           sb.Append("    font-family: Arial, Verdana, Comic Sans MS'> ");
           sb.Append("    <b>Incident Tracking System Notification</b> ");
           sb.Append("  </td> ");
           sb.Append("    </tr> ");
           sb.Append("   <tr> ");
           sb.Append("   <td class='w640' bgcolor='#ffffff' height='30' width='640'> ");
           sb.Append("   </td> ");
           sb.Append("   </tr> ");
           sb.Append("   <tr id='simple-content-row'> ");
           sb.Append("   <td class='w640' bgcolor='#ffffff' width='640'> ");
           sb.Append("    <table class='w640' border='0' cellspacing='0' cellpadding='0' width='640'> ");
           sb.Append("  <tbody> ");
           sb.Append("       <tr> ");
           sb.Append("     <td class='w30' width='30'> ");
           sb.Append("    </td> ");
           sb.Append("    <td class='w580' width='580'> ");
           sb.Append("  <repeater> ");

           sb.Append("   <layout label='Text only'> ");
           sb.Append("  <table class='w580' border='0' cellspacing='0' cellpadding='0'> ");
           sb.Append("  <tbody><tr> ");
           sb.Append("  <td class='w580' width='580'> ");
           sb.Append("    <div class='article-content' align='left'> ");
           sb.Append("    <table style='font-family: Verdana;  width: 700px; color: white; padding: 10px'> ");
           sb.Append("   <tr> ");
           sb.Append("        <td class='header' align='left' style='font-size: 10pt; color: Gray;' > ");
           sb.Append("       Good day <strong>{0}</strong><br /> ");
           sb.Append("      <br /> ");
           sb.Append("    <br /> ");
           sb.Append("    The following incident with reference number <a href='{5}'>{2}</a> has been assigned to <b>{6}</b> who was required to action and respond before the due date :<span style='color:red'>{3}</span>.  ");
           sb.Append("   <br /> ");
           sb.Append("   Summary : {1}<br /> ");
           sb.Append("   <br /> ");
           sb.Append("     <br /> ");
           sb.Append("    </td> ");
           sb.Append("   </tr> ");
           sb.Append("  <tr> ");
           sb.Append("  <td style='font-size: 10pt; color: Gray;'> ");

           sb.Append("   <b>Incident Details</b> ");
           sb.Append("   <table width='100%' border='0' cellpadding='5px' style='font-size: 10pt; color: Gray;'> ");
           sb.Append("   <tr> ");
           sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;'> ");
           sb.Append("     Reference Number: ");
           sb.Append("     </td> ");
           sb.Append("    <td> ");
           sb.Append("    <a href='{5}'>{2}</a> ");
           sb.Append("   </td> ");

           sb.Append("   </tr> ");
           sb.Append("    <tr> ");
           sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;' > ");
           sb.Append("       Incident Status: ");
           sb.Append("    </td> ");
           sb.Append("     <td> ");
           sb.Append("      {4} ");
           sb.Append("     </td> ");

           sb.Append("   </tr> ");
           sb.Append("   <tr> ");
           sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;' > ");
           sb.Append("       Due Date: ");
           sb.Append("   </td> ");
           sb.Append("    <td style='font-size: 10pt; color: red;' > ");
           sb.Append("       {3} ");
           sb.Append("    </td> ");

           sb.Append("  </tr> ");

           sb.Append("  </table> ");

           sb.Append("   </td> ");
           sb.Append("  </tr> ");


           sb.Append("   <tr> ");
           sb.Append("  <td style='font-size: 10pt; color: Gray;'> ");
           sb.Append("   <br /> ");
           sb.Append("   <b>Kind Regards<br/>Incident Resolution System ");

           sb.Append("    </b><br /><br /> ");
           sb.Append("    </td> ");
           sb.Append("   </tr> ");
           sb.Append("    </table> ");
           sb.Append("   </div> ");
           sb.Append(" </td> ");
           sb.Append("  </tr> ");

           sb.Append("  </tbody></table> ");
           sb.Append("    </layout> ");
           sb.Append("   </td> ");
           sb.Append("    <td class='w30' width='30'> ");
           sb.Append("    </td> ");
           sb.Append("   </tr> ");
           sb.Append("   </tbody> ");
           sb.Append("  </table> ");
           sb.Append("  </td> ");
           sb.Append("   </tr> ");
           sb.Append("   <tr> ");
           sb.Append(
               "     <td style='border-bottom-left-radius: 5px; border-bottom-right-radius: 5px; background-color: silver; ");
           sb.Append(
               "     width: 640px; text-align: left; height: 20px; color: #fff; font-family: Arial, Verdana, Comic Sans MS; ");
           sb.Append("     padding: 2px'> ");
           sb.Append("     <b>NB: This is an automated email, do not reply. </b> ");
           sb.Append("   </td> ");
           sb.Append("  </tr> ");
           sb.Append("  </table> ");
           sb.Append("  </center> ");
           sb.Append(" </body> ");
           sb.Append(" </html> ");


           return sb.ToString();

       }

        public static string ServiceStartStopped()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   <html> ");
            sb.Append(" <body> ");
            sb.Append("  <center> ");
            sb.Append("   <table style='margin: 0px 10px; border-top-left-radius: 10px; border-top-right-radius: 10px; ");
            sb.Append("     width: 747px;'> ");
            sb.Append("    <tr> ");
            sb.Append(
                "     <td style='border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: silver; ");
            sb.Append("      width: 640px; text-align: center; height: 100px; font-size: 30pt; color: #fff; ");
            sb.Append("    font-family: Arial, Verdana, Comic Sans MS'> ");
            sb.Append("    <b>Incident Tracking System Notification</b> ");
            sb.Append("  </td> ");
            sb.Append("    </tr> ");
            sb.Append("   <tr> ");
            sb.Append("   <td class='w640' bgcolor='#ffffff' height='30' width='640'> ");
            sb.Append("   </td> ");
            sb.Append("   </tr> ");
            sb.Append("   <tr id='simple-content-row'> ");
            sb.Append("   <td class='w640' bgcolor='#ffffff' width='640'> ");
            sb.Append("    <table class='w640' border='0' cellspacing='0' cellpadding='0' width='640'> ");
            sb.Append("  <tbody> ");
            sb.Append("       <tr> ");
            sb.Append("     <td class='w30' width='30'> ");
            sb.Append("    </td> ");
            sb.Append("    <td class='w580' width='580'> ");
            sb.Append("  <repeater> ");

            sb.Append("   <layout label='Text only'> ");
            sb.Append("  <table class='w580' border='0' cellspacing='0' cellpadding='0'> ");
            sb.Append("  <tbody><tr> ");
            sb.Append("  <td class='w580' width='580'> ");
            sb.Append("    <div class='article-content' align='left'> ");
            sb.Append("    <table style='font-family: Verdana;  width: 700px; color: white; padding: 10px'> ");
            sb.Append("   <tr> ");
            sb.Append("        <td class='header' align='left' style='font-size: 10pt; color: Gray;' > ");
            sb.Append("       Good day <strong>Mzwakhe Makhubu</strong><br /> ");
            sb.Append("      <br /> ");
            sb.Append("    <br /> ");
            sb.Append("   {1}");
   
           
            sb.Append("   <br /> ");
            sb.Append("     <br /> ");
            sb.Append("    </td> ");
            sb.Append("   </tr> ");
            sb.Append("  <tr> ");
            sb.Append("  <td style='font-size: 10pt; color: Gray;'> ");

            sb.Append("   <b>Incident Details</b> ");
            sb.Append("   <table width='100%' border='0' cellpadding='5px' style='font-size: 10pt; color: Gray;'> ");
            sb.Append("   <tr> ");
            sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;'> ");
            sb.Append("     Service Status: ");
            sb.Append("     </td> ");
            sb.Append("    <td> ");
            sb.Append("    {2}");
            sb.Append("   </td> ");
           
            sb.Append("   </tr> ");
            sb.Append("   <tr> ");
            sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;'> ");
            sb.Append("     Service {2} Date: ");
            sb.Append("     </td> ");
            sb.Append("    <td> ");
            sb.Append("    {0}");
            sb.Append("   </td> ");

            sb.Append("   </tr> ");
            sb.Append("  </table> ");

            sb.Append("   </td> ");
            sb.Append("  </tr> ");


            sb.Append("   <tr> ");
            sb.Append("  <td style='font-size: 10pt; color: Gray;'> ");
            sb.Append("   <br /> ");
            sb.Append("   <b>Kind Regards<br/>Incident Resolution System ");

            sb.Append("    </b><br /><br /> ");
            sb.Append("    </td> ");
            sb.Append("   </tr> ");
            sb.Append("    </table> ");
            sb.Append("   </div> ");
            sb.Append(" </td> ");
            sb.Append("  </tr> ");

            sb.Append("  </tbody></table> ");
            sb.Append("    </layout> ");
            sb.Append("   </td> ");
            sb.Append("    <td class='w30' width='30'> ");
            sb.Append("    </td> ");
            sb.Append("   </tr> ");
            sb.Append("   </tbody> ");
            sb.Append("  </table> ");
            sb.Append("  </td> ");
            sb.Append("   </tr> ");
            sb.Append("   <tr> ");
            sb.Append(
                "     <td style='border-bottom-left-radius: 5px; border-bottom-right-radius: 5px; background-color: silver; ");
            sb.Append(
                "     width: 640px; text-align: left; height: 20px; color: #fff; font-family: Arial, Verdana, Comic Sans MS; ");
            sb.Append("     padding: 2px'> ");
            sb.Append("     <b>NB: This is an automated email, do not reply. </b> ");
            sb.Append("   </td> ");
            sb.Append("  </tr> ");
            sb.Append("  </table> ");
            sb.Append("  </center> ");
            sb.Append(" </body> ");
            sb.Append(" </html> ");


            return sb.ToString();

        }

        #region NTQ
        public static string NQT_DueTodayYou()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   <html> ");
            sb.Append(" <body> ");
            sb.Append("  <center> ");
            sb.Append("   <table style='margin: 0px 10px; border-top-left-radius: 10px; border-top-right-radius: 10px; ");
            sb.Append("     width: 747px;'> ");
            sb.Append("    <tr> ");
            sb.Append(
                "     <td style='border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: silver; ");
            sb.Append("      width: 640px; text-align: center; height: 100px; font-size: 30pt; color: #fff; ");
            sb.Append("    font-family: Arial, Verdana, Comic Sans MS'> ");
            sb.Append("    <b>NATIONAL TREASURY REPORT SIGN-OFF SHEET Notification</b> ");
            sb.Append("  </td> ");
            sb.Append("    </tr> ");
            sb.Append("   <tr> ");
            sb.Append("   <td class='w640' bgcolor='#ffffff' height='30' width='640'> ");
            sb.Append("   </td> ");
            sb.Append("   </tr> ");
            sb.Append("   <tr id='simple-content-row'> ");
            sb.Append("   <td class='w640' bgcolor='#ffffff' width='640'> ");
            sb.Append("    <table class='w640' border='0' cellspacing='0' cellpadding='0' width='640'> ");
            sb.Append("  <tbody> ");
            sb.Append("       <tr> ");
            sb.Append("     <td class='w30' width='30'> ");
            sb.Append("    </td> ");
            sb.Append("    <td class='w580' width='580'> ");
            sb.Append("  <repeater> ");

            sb.Append("   <layout label='Text only'> ");
            sb.Append("  <table class='w580' border='0' cellspacing='0' cellpadding='0'> ");
            sb.Append("  <tbody><tr> ");
            sb.Append("  <td class='w580' width='580'> ");
            sb.Append("    <div class='article-content' align='left'> ");
            sb.Append("    <table style='font-family: Verdana;  width: 700px; color: white; padding: 10px'> ");
            sb.Append("   <tr> ");
            sb.Append("        <td class='header' align='left' style='font-size: 10pt; color: Gray;' > ");
            sb.Append("       Good day <strong>{0}</strong><br /> ");
            sb.Append("      <br /> ");
            sb.Append("    <br /> ");
            sb.Append("The report signoff sheet with the reference number <a href='{5}'>{2}</a>  is due today and you are required to submit the Quarterly National Treasury report as well as evidence by the due date: {3}<br />.");

            sb.Append("   <br /> ");          
            sb.Append("     <br /> ");
            sb.Append("    </td> ");
            sb.Append("   </tr> ");
            sb.Append("  <tr> ");
            sb.Append("  <td style='font-size: 10pt; color: Gray;'> ");

            sb.Append("   <b>Incident Details</b> ");
            sb.Append("   <table width='100%' border='0' cellpadding='5px' style='font-size: 10pt; color: Gray;'> ");
            sb.Append("   <tr> ");
            sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;'> ");
            sb.Append("     Summary : ");
            sb.Append("     </td> ");
            sb.Append("    <td> ");
            sb.Append("   {1} ");
            sb.Append("   </tr> ");
            sb.Append("   <tr> ");
            sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;'> ");
            sb.Append("     Reference Number: ");
            sb.Append("     </td> ");
            sb.Append("    <td> ");
            sb.Append("    <a href='{5}'>{2}</a> ");
            sb.Append("   </td> ");

            sb.Append("   </tr> ");
            sb.Append("    <tr> ");
            sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;' > ");
            sb.Append("       Incident Status: ");
            sb.Append("    </td> ");
            sb.Append("     <td> ");
            sb.Append("      {4} ");
            sb.Append("     </td> ");

            sb.Append("   </tr> ");
            sb.Append("   <tr> ");
            sb.Append("    <td width='50%' align='left' style='font-size: 10pt; color: Gray;' > ");
            sb.Append("       Due Date: ");
            sb.Append("   </td> ");
            sb.Append("    <td> ");
            sb.Append("       {3} ");
            sb.Append("    </td> ");

            sb.Append("  </tr> ");

            sb.Append("  </table> ");

            sb.Append("   </td> ");
            sb.Append("  </tr> ");


            sb.Append("   <tr> ");
            sb.Append("  <td style='font-size: 10pt; color: Gray;'> ");
            sb.Append("   <br /> ");
            sb.Append("   <b>Kind Regards<br/>NATIONAL TREASURY REPORT  System ");

            sb.Append("    </b><br /><br /> ");
            sb.Append("    </td> ");
            sb.Append("   </tr> ");
            sb.Append("    </table> ");
            sb.Append("   </div> ");
            sb.Append(" </td> ");
            sb.Append("  </tr> ");

            sb.Append("  </tbody></table> ");
            sb.Append("    </layout> ");
            sb.Append("   </td> ");
            sb.Append("    <td class='w30' width='30'> ");
            sb.Append("    </td> ");
            sb.Append("   </tr> ");
            sb.Append("   </tbody> ");
            sb.Append("  </table> ");
            sb.Append("  </td> ");
            sb.Append("   </tr> ");
            sb.Append("   <tr> ");
            sb.Append(
                "     <td style='border-bottom-left-radius: 5px; border-bottom-right-radius: 5px; background-color: silver; ");
            sb.Append(
                "     width: 640px; text-align: left; height: 20px; color: #fff; font-family: Arial, Verdana, Comic Sans MS; ");
            sb.Append("     padding: 2px'> ");
            sb.Append("     <b>NB: This is an automated email, do not reply. </b> ");
            sb.Append("   </td> ");
            sb.Append("  </tr> ");
            sb.Append("  </table> ");
            sb.Append("  </center> ");
            sb.Append(" </body> ");
            sb.Append(" </html> ");


            return sb.ToString();

        }
        #endregion

        #region OOC
        public static string OocAssignedToYou()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  <html>                                                                                                                                                                             ");
            sb.Append("  <body>																																												");
            sb.Append("      <center>																																										");
            sb.Append("          <table style='margin: 0px 10px; border-top-left-radius: 10px; border-top-right-radius: 10px;																				");
            sb.Append("              width: 747px;'>																																						");
            sb.Append("              <tr>																																									");
            sb.Append("                  <td style='border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: silver;																	");
            sb.Append("                      width: 640px; text-align: center; height: 100px; font-size: 30pt; color: #fff;																					");
            sb.Append("                      font-family: Arial, Verdana, 'Comic Sans MS''>																													");
            sb.Append("                      <b>Incident Tracking System Notification<br />Follow Up </b>																						");
            sb.Append("                  </td>																																								");
            sb.Append("              </tr>																																									");
            sb.Append("              <tr>																																									");
            sb.Append("                  <td class='w640' bgcolor='#ffffff' height='30' width='640'>																										");
            sb.Append("                  </td>																																								");
            sb.Append("              </tr>																																									");
            sb.Append("              <tr id='simple-content-row'>																																			");
            sb.Append("                  <td class='w640' bgcolor='#ffffff' width='640'>																													");
            sb.Append("                      <table class='w640' border='0' cellspacing='0' cellpadding='0' width='640'>																					");
            sb.Append("                          <tbody>																																					");
            sb.Append("                              <tr>																																					");
            sb.Append("                                  <td class='w30' width='30'>																														");
            sb.Append("                                  </td>																																				");
            sb.Append("                                  <td class='w580' width='580'>																														");
            sb.Append("                                      <repeater>																																		");
            sb.Append("                      																																								");
            sb.Append("                                      <layout label='Text only'>																														");
            sb.Append("                                          <table class='w580' border='0' cellspacing='0' cellpadding='0'>																			");
            sb.Append("                                              <tbody><tr>																															");
            sb.Append("                                                         <td class='w580' width='580'>																								");
            sb.Append("                                                             <div class='article-content' align='left'>																				");
            sb.Append("                                                                 <table style='font-family: Verdana;  width: 700px; color: white; padding: 10px'>									");
            sb.Append("                                                                     <tr>																											");
            sb.Append("                                                                         <td class='header' align='left' style='font-size: 10pt; color: Gray;'>										");
            sb.Append("                                                                             Good day <strong>{0}</strong><br />																		");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             <b>{1}</b>																								");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             The following incident with reference number <a href='{5}'>{2}</a>  was referred to you on				");
            sb.Append("                    {3}, and is yet to be resolved. Please provide urgent feedback or engage the OOC to discuss and set a more appropriate service level agreement/ completion date.	");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             Summary : {4}<br />																						");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                         </td>																										");
            sb.Append("                                                                     </tr>																											");
            sb.Append("                                                                     <tr>																											");
            sb.Append("                                                                         <td style='font-size: 10pt; color: Gray;'>																	");
            sb.Append("  																																													");
            sb.Append("                                                                             <b>Incident Details</b>																					");
            sb.Append("                                                                             <table width='100%' border='0' cellpadding='5px' style='font-size: 10pt; color: Gray;'>					");
            sb.Append("                                                                                 <tr>																								");
            sb.Append("                                                                                     <td width='50%' align='left' style='font-size: 10pt; color: Gray;'>								");
            sb.Append("                                                                                         Reference Number:																			");
            sb.Append("                                                                                     </td>																							");
            sb.Append("                                                                                     <td>																							");
            sb.Append("                                                                                         <a href='{5}'>{2}</a>																		");
            sb.Append("                                                                                     </td>																							");
            sb.Append("  																																													");
            sb.Append("                                                                                 </tr>																								");
            sb.Append("                                                                                 <tr>																								");
            sb.Append("                                                                                     <td width='50%' align='left' style='font-size: 10pt; color: Gray;' >							");
            sb.Append("                                                                                         Incident Status:																			");
            sb.Append("                                                                                     </td>																							");
            sb.Append("                                                                                     <td>																							");
            sb.Append("                                                                                         {6}																							");
            sb.Append("                                                                                     </td>																							");
            sb.Append("  																																													");
            sb.Append("                                                                                 </tr>																								");
            sb.Append("                                                                                 <tr>																								");
            sb.Append("                                                                                     <td width='50%' align='left' style='font-size: 10pt; color: Gray;' >							");
            sb.Append("                                                                                         Due Date:																					");
            sb.Append("                                                                                     </td>																							");
            sb.Append("                                                                                     <td>																							");
            sb.Append("                                                                                         {7}																							");
            sb.Append("                                                                                     </td>																							");
            sb.Append("  																																													");
            sb.Append("                                                                                 </tr>																								");
            sb.Append("                                                                                 <tr>																								");
            sb.Append("                                                                                     <td width='50%' align='left' style='font-size: 10pt; color: Gray;'>								");
            sb.Append("                                                                                         Created by:																					");
            sb.Append("                                                                                     </td>																							");
            sb.Append("                                                                                     <td>																							");
            sb.Append("                                                                                         {8}																							");
            sb.Append("                                                                                     </td>																							");
            sb.Append("  																																													");
            sb.Append("                                                                                 </tr>																								");
            sb.Append("  																																													");
            sb.Append("     																																												");
            sb.Append("                                                                             </table>																								");
            sb.Append("  																																													");
            sb.Append("                                                                         </td>																										");
            sb.Append("                                                                     </tr>																											");
            sb.Append("  																																													");
            sb.Append("  																																													");
            sb.Append("                                                                     <tr>																											");
            sb.Append("                                                                         <td style='font-size: 10pt; color: Gray;'>																	");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             <b>Kind Regards<br/>Incident Resolution System															");
            sb.Append("      																																												");
            sb.Append("                                                                             </b><br /><br />																						");
            sb.Append("                                                                         </td>																										");
            sb.Append("                                                                     </tr>																											");
            sb.Append("                                                                 </table>																											");
            sb.Append("                                                             </div>																													");
            sb.Append("                                                         </td>																														");
            sb.Append("                                                     </tr>																															");
            sb.Append("                              																																						");
            sb.Append("                                              </tbody></table>																														");
            sb.Append("                                      </layout>																																		");
            sb.Append("                                  </td>																																				");
            sb.Append("                                  <td class='w30' width='30'>																														");
            sb.Append("                                  </td>																																				");
            sb.Append("                              </tr>																																					");
            sb.Append("                          </tbody>																																					");
            sb.Append("                      </table>																																						");
            sb.Append("                  </td>																																								");
            sb.Append("              </tr>																																									");
            sb.Append("              <tr>																																									");
            sb.Append("                  <td style='border-bottom-left-radius: 5px; border-bottom-right-radius: 5px; background-color: silver;																");
            sb.Append("                      width: 640px; text-align: left; height: 20px; color: #fff; font-family: Arial, Verdana, 'Comic Sans MS';														");
            sb.Append("                      padding: 2px'>																																					");
            sb.Append("                      <b>NB: This is an automated email, do not reply. </b>																											");
            sb.Append("                  </td>																																								");
            sb.Append("              </tr>																																									");
            sb.Append("          </table>																																									");
            sb.Append("      </center>																																										");
            sb.Append("  </body>																																											");
            sb.Append("  </html>																																											");
            sb.Append("  																																													");

            return sb.ToString();
        }
        public static string OocEscalateToManager()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  <html>                                                                                                                                                                             ");
            sb.Append("  <body>																																												");
            sb.Append("      <center>																																										");
            sb.Append("          <table style='margin: 0px 10px; border-top-left-radius: 10px; border-top-right-radius: 10px;																				");
            sb.Append("              width: 747px;'>																																						");
            sb.Append("              <tr>																																									");
            sb.Append("                  <td style='border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: silver;																	");
            sb.Append("                      width: 640px; text-align: center; height: 100px; font-size: 30pt; color: #fff;																					");
            sb.Append("                      font-family: Arial, Verdana, 'Comic Sans MS''>																													");
            sb.Append("                      <b>Incident Tracking System Notification<br />Escalation </b>																						");
            sb.Append("                  </td>																																								");
            sb.Append("              </tr>																																									");
            sb.Append("              <tr>																																									");
            sb.Append("                  <td class='w640' bgcolor='#ffffff' height='30' width='640'>																										");
            sb.Append("                  </td>																																								");
            sb.Append("              </tr>																																									");
            sb.Append("              <tr id='simple-content-row'>																																			");
            sb.Append("                  <td class='w640' bgcolor='#ffffff' width='640'>																													");
            sb.Append("                      <table class='w640' border='0' cellspacing='0' cellpadding='0' width='640'>																					");
            sb.Append("                          <tbody>																																					");
            sb.Append("                              <tr>																																					");
            sb.Append("                                  <td class='w30' width='30'>																														");
            sb.Append("                                  </td>																																				");
            sb.Append("                                  <td class='w580' width='580'>																														");
            sb.Append("                                      <repeater>																																		");
            sb.Append("                      																																								");
            sb.Append("                                      <layout label='Text only'>																														");
            sb.Append("                                          <table class='w580' border='0' cellspacing='0' cellpadding='0'>																			");
            sb.Append("                                              <tbody><tr>																															");
            sb.Append("                                                         <td class='w580' width='580'>																								");
            sb.Append("                                                             <div class='article-content' align='left'>																				");
            sb.Append("                                                                 <table style='font-family: Verdana;  width: 700px; color: white; padding: 10px'>									");
            sb.Append("                                                                     <tr>																											");
            sb.Append("                                                                         <td class='header' align='left' style='font-size: 10pt; color: Gray;'>										");
            sb.Append("                                                                             Good day <strong>{0}</strong><br />																		");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             <b>{1}</b>																								");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             <br />																									");
            sb.Append(" The following incident with reference number <a href='{5}'>{2}</a>  was referred to, and followed up " +
                        " with <b>{3}</b> on several occasions respectively and is yet to be resolved. Please assist your team in providing urgent feedback " +
                        "or ensure engagement with the OOC to discuss and set a more appropriate service level agreement/ completion date.			");
            
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             Summary : {4}<br />																						");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                         </td>																										");
            sb.Append("                                                                     </tr>																											");
            sb.Append("                                                                     <tr>																											");
            sb.Append("                                                                         <td style='font-size: 10pt; color: Gray;'>																	");
            sb.Append("  																																													");
            sb.Append("                                                                             <b>Incident Details</b>																					");
            sb.Append("                                                                             <table width='100%' border='0' cellpadding='5px' style='font-size: 10pt; color: Gray;'>					");
            sb.Append("                                                                                 <tr>																								");
            sb.Append("                                                                                     <td width='50%' align='left' style='font-size: 10pt; color: Gray;'>								");
            sb.Append("                                                                                         Reference Number:																			");
            sb.Append("                                                                                     </td>																							");
            sb.Append("                                                                                     <td>																							");
            sb.Append("                                                                                         <a href='{5}'>{2}</a>																		");
            sb.Append("                                                                                     </td>																							");
            sb.Append("  																																													");
            sb.Append("                                                                                 </tr>																								");
            sb.Append("                                                                                 <tr>																								");
            sb.Append("                                                                                     <td width='50%' align='left' style='font-size: 10pt; color: Gray;' >							");
            sb.Append("                                                                                         Incident Status:																			");
            sb.Append("                                                                                     </td>																							");
            sb.Append("                                                                                     <td>																							");
            sb.Append("                                                                                         {6}																							");
            sb.Append("                                                                                     </td>																							");
            sb.Append("  																																													");
            sb.Append("                                                                                 </tr>																								");

            sb.Append("                                                                                 <tr>																								");
            sb.Append("                                                                                     <td width='50%' align='left' style='font-size: 10pt; color: Gray;' >							");
            sb.Append("                                                                                         Assigned Date:																			");
            sb.Append("                                                                                     </td>																							");
            sb.Append("                                                                                     <td>																							");
            sb.Append("                                                                                         {9}																							");
            sb.Append("                                                                                     </td>																							");
            sb.Append("  																																													");
            sb.Append("                                                                                 </tr>");

            sb.Append("                                                                                 <tr>																								");
            sb.Append("                                                                                     <td width='50%' align='left' style='font-size: 10pt; color: Gray;' >							");
            sb.Append("                                                                                         Follow Up Date:																			");
            sb.Append("                                                                                     </td>																							");
            sb.Append("                                                                                     <td>																							");
            sb.Append("                                                                                         {10}																							");
            sb.Append("                                                                                     </td>																							");
            sb.Append("  																																													");
            sb.Append("                                                                                 </tr>");




            sb.Append("                                                                                 <tr>																								");
            sb.Append("                                                                                     <td width='50%' align='left' style='font-size: 10pt; color: Gray;' >							");
            sb.Append("                                                                                         Due Date:																					");
            sb.Append("                                                                                     </td>																							");
            sb.Append("                                                                                     <td>																							");
            sb.Append("                                                                                         {7}																							");
            sb.Append("                                                                                     </td>																							");
            sb.Append("  																																													");
            sb.Append("                                                                                 </tr>																								");
            sb.Append("                                                                                 <tr>																								");
            sb.Append("                                                                                     <td width='50%' align='left' style='font-size: 10pt; color: Gray;'>								");
            sb.Append("                                                                                         Created by:																					");
            sb.Append("                                                                                     </td>																							");
            sb.Append("                                                                                     <td>																							");
            sb.Append("                                                                                         {8}																							");
            sb.Append("                                                                                     </td>																							");
            sb.Append("  																																													");
            sb.Append("                                                                                 </tr>																								");
            sb.Append("  																																													");
            sb.Append("     																																												");
            sb.Append("                                                                             </table>																								");
            sb.Append("  																																													");
            sb.Append("                                                                         </td>																										");
            sb.Append("                                                                     </tr>																											");
            sb.Append("  																																													");
            sb.Append("  																																													");
            sb.Append("                                                                     <tr>																											");
            sb.Append("                                                                         <td style='font-size: 10pt; color: Gray;'>																	");
            sb.Append("                                                                             <br />																									");
            sb.Append("                                                                             <b>Kind Regards<br/>Incident Resolution System															");
            sb.Append("      																																												");
            sb.Append("                                                                             </b><br /><br />																						");
            sb.Append("                                                                         </td>																										");
            sb.Append("                                                                     </tr>																											");
            sb.Append("                                                                 </table>																											");
            sb.Append("                                                             </div>																													");
            sb.Append("                                                         </td>																														");
            sb.Append("                                                     </tr>																															");
            sb.Append("                              																																						");
            sb.Append("                                              </tbody></table>																														");
            sb.Append("                                      </layout>																																		");
            sb.Append("                                  </td>																																				");
            sb.Append("                                  <td class='w30' width='30'>																														");
            sb.Append("                                  </td>																																				");
            sb.Append("                              </tr>																																					");
            sb.Append("                          </tbody>																																					");
            sb.Append("                      </table>																																						");
            sb.Append("                  </td>																																								");
            sb.Append("              </tr>																																									");
            sb.Append("              <tr>																																									");
            sb.Append("                  <td style='border-bottom-left-radius: 5px; border-bottom-right-radius: 5px; background-color: silver;																");
            sb.Append("                      width: 640px; text-align: left; height: 20px; color: #fff; font-family: Arial, Verdana, 'Comic Sans MS';														");
            sb.Append("                      padding: 2px'>																																					");
            sb.Append("                      <b>NB: This is an automated email, do not reply. </b>																											");
            sb.Append("                  </td>																																								");
            sb.Append("              </tr>																																									");
            sb.Append("          </table>																																									");
            sb.Append("      </center>																																										");
            sb.Append("  </body>																																											");
            sb.Append("  </html>																																											");
            sb.Append("  																																													");

            return sb.ToString();
        }
        #endregion
    }
}
