<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" CodeFile="html2pdfReport.aspx.cs" Inherits="NTQR_html2pdfReport" %>
<%@ Register Src="~/Admin/UCAttachDocuments.ascx" TagPrefix="asp" TagName="UCAttachDocuments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="dist/html2pdf.bundle.min.js"></script>

    <script>
        var html2pdf;
        var _window;
        function loadPdf() {
            var element = document.getElementById('contentReport');
            var opt = {
                margin: 1,
                filename: 'QuarteryReport.pdf',
                image: { type: 'jpeg', quality: 0.98 },
                html2canvas: { scale: 2 },
                jsPDF: { unit: 'in', format: 'A4', orientation: 'landscape' },
                pagebreak: { before: '.beforeClass', after: ['#signature', '#after2'], avoid: 'img' }
            };

            // html2pdf().from(element).set(opt).save();



            html2pdf().from(element).set(opt).toPdf().get('pdf').then(function (pdf) {
                // Your code to alter the pdf object.
                var totalPages = pdf.internal.getNumberOfPages();


                for (i = 1; i <= totalPages; i++) {
                    pdf.setPage(i);
                    pdf.setFontSize(10);
                    pdf.setTextColor(150);
                    pdf.text('Page ' + i + ' of ' + totalPages, pdf.internal.pageSize.getWidth() / 2, pdf.internal.pageSize.getHeight() - 0.3);
                    //pdf.text('' + i, (pdf.internal.pageSize.getWidth() / 2), (pdf.internal.pageSize.getHeight() - 0.3));
                }

            }).save();

        }

        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            var style = "<style type='text/css' media='print'> @page { size: auto; margin: 0;} .td1 { "
            " background-color: #154c79; "
            " color: white!important; "
            " font - weight: bold; "
            " font - size: 10pt; "
            " vertical - align: top; "
            " border: 1px solid black; "

            " }  </style>";



            document.head.innerHTML += `
                 <style type='text/css' media='print'>

body{    font-family:Calibri, 10pt, Default, Default, Default!important; margin: 0!important;}

.pagebreak { page-break-before: always; } /* page-break-after works, as well */

@page {
     
     size: auto;
    margin: 10%;
@bottom-left {
            content: counter(page) "/" counter(pages);
        }

}
@page :right :footer  {
    content: last(section), , "Page " decimal(pageno)!important;
    font-variant: small-caps!important;
}

.td1 {
                    background-color: #154c79!important;
                color: white!important;
                font-weight: bold!important;
                font-size: 10pt!important;
                vertical-align: top!important;
                border: 1px solid black!important;
    font-family:Calibri, 10pt, Default, Default, Default!important;
        }

                .tdActualAchievement {
                    background-color: #154c79!important;
                color: white!important;
                font-weight: bold!important;
                font-size: 10pt!important;
                vertical-align: top!important;
                border: 1px solid black!important;
                width: 10%;
    font-family:Calibri, 10pt, Default, Default, Default!important;
        }

                .tdTable2 {
                    background-color: #154c79!important;
                color: white!important;
                font-weight: bold!important;
                font-size: 10pt!important;
                vertical-align: top!important;
                border: 1px solid black!important;
                width: 10%;
    font-family:Calibri, 10pt, Default, Default, Default!important;
        }

                .td1a {
                    background-color: #154c79!important;
                color: white!important;
                font-weight: bold!important;
                font-size: 10pt!important;
                vertical-align: top!important;
                border: 1px solid black!important;
        }

                .td1b {
                    font-size: 10pt;
                vertical-align: top;
                border: 1px solid black;
        }

                .tdObjective {
                    font-size: 10pt!important;
                vertical-align: top!important;
                vertical-align: top!important;
                width: 25%!important;
        }

                .td2 {
                    font-size: 10pt!important;
                vertical-align: top!important;
                vertical-align: top!important;
        }

                .td1BottomBorder {
                    background-color: #154c79!important;
                color: white!important;
                font-weight: bold!important;
                font-size: 10pt!important;
                vertical-align: top!important;
                width: 25%!important;
                border: 1px solid black!important;
        }

                .td2BottomBorder {
                    font-size: 10pt!important;
                vertical-align: top!important;
                width: 25%!important;
                border: 1px solid black!important;
        }

                .BorderBottom {
                    border-bottom: 1px solid black!important;
        }

                .sig {
                    font-weight: bold!important;
                vertical-align: top!important;
                max-width: 10%!important;
        }

              

                #dvVariance table {
                    table-layout: fixed!important;
        }

                #dvVariance table, dvVariance table tr tdbody td {
                    width: auto !important;
                word-wrap: break-word;
        }

                table {
                     width: auto !important;
                    table-layout: fixed!important;
                    border-collapse: collapse!important;

        }

td{
 min-height:0.39583in!important;
  width: auto !important;
                word-wrap: break-word!important;
 padding:3px!important;
}
tr{
 min-height:0.39583in!important;
  width: auto !important;
                word-wrap: break-word!important;
}
           </style>`




            //document.head = "";
            // document.head.innerHTML = style2;
            document.head.title = "";
            //document.write('<html><head><title>" "</title>');
            //document.write('</head>');
            document.body.innerHTML = printContents;
            //document.write('</html>');






            window.print();


            document.body.innerHTML = originalContents;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <style type="text/css" media="print">
        @media print {
            @page {
                size: auto;
                margin: 0;
            }

            body {
                padding-top: 72px;
                padding-bottom: 72px;
            }
        }
    </style>
    <style>
        .td1 {
            background-color: #154c79;
            color: white;
            font-weight: bold;
            font-size: 10pt;
            vertical-align: top;
            border: 1px solid black;
            font-family: Calibri, 10pt, Default, Default, Default !important;
        }

        .tdActualAchievement {
            background-color: #154c79;
            color: white;
            font-weight: bold;
            font-size: 10pt;
            vertical-align: top;
            border: 1px solid black;
            width: 10%;
            padding: 3px !important;
        }

        .tdTable2 {
            background-color: #154c79;
            color: white;
            font-weight: bold;
            font-size: 10pt;
            vertical-align: top;
            border: 1px solid black;
            width: 10%;
        }

        .td1a {
            background-color: #154c79;
            color: white;
            font-weight: bold;
            font-size: 10pt;
            vertical-align: top;
            border: 1px solid black;
        }

        .td1b {
            font-size: 10pt;
            vertical-align: top;
            border: 1px solid black;
        }

        .tdObjective {
            font-size: 10pt;
            vertical-align: top;
            vertical-align: top;
            width: 25%;
        }

        .td2 {
            font-size: 10pt;
            vertical-align: top;
            vertical-align: top;
        }

        .td1BottomBorder {
            background-color: #154c79;
            color: white;
            font-weight: bold;
            font-size: 10pt;
            vertical-align: top;
            width: 25%;
            border: 1px solid black;
        }

        .td2BottomBorder {
            font-size: 10pt;
            vertical-align: top;
            width: 25%;
            border: 1px solid black;
        }

        .BorderBottom {
            border-bottom: 1px solid black;
        }

        .sig {
            font-weight: bold;
            vertical-align: top;
            max-width: 10%;
        }

        .btn1 {
            background-color: rgb(66, 139, 202);
            color: white;
            cursor: pointer;
            min-height: 30px;
            min-width: 100px;
            width: auto !important;
            font-size: 100% !important;
            border-width: initial;
            border-style: none;
            border-color: initial;
            border-image: initial;
            padding: 10px;
            text-decoration: none;
            margin: 4px 2px;
            border-radius: 5px;
        }

        #dvVariance table {
            table-layout: fixed !important;
        }

        #dvVariance table, dvVariance table tr tdbody td {
            width: auto !important;
            word-wrap: break-word;
        }

        table {
            width: auto !important;
            table-layout: fixed !important;
        }
    </style>

    <button type="button" class="btn1" onclick="printDiv('contentReport')">Print</button>
    <asp:Button class="btn1" Text="Rework" ID="btnRework" runat="server" Visible ="false" OnClick="btnRework_Click" />
    <asp:Button class="btn1" Text="Back" ID="btnBck" runat="server" OnClick="btnBck_Click" />
    <div id="contentReport" style="width: 895px;">
        <table id="tbheader" style="width: 100%!important; border: 0px solid black;">
            <thead>
                <tr valign="right">
                    <td style="text-align: right!important; color: gray; font: bold 10pt 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif">
                        <asp:Label Text="text" ID="lblQuarter1" runat="server" />
                        &nbsp;NATIONAL TREASURY REPORT: Q1 - Q4</td>
                </tr>
                <tr>
                    <td style="color: black; font: bold 10pt 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; width: 100%!important">
                        <asp:Label Text="text" ID="lblQuarter2" runat="server" />
                        &nbsp;NATIONAL TREASURY REPORT INPUT: SIGN-OFF SHEET</td>
                </tr>
            </thead>
        </table>
        <table id="tbheader1" style="width: 100%; border: 1px solid black">
            <tbody>
                <tr>
                    <td class="td1" rowspan="2">STRATEGIC OBJECTIVE:</td>
                    <td class="tdObjective" rowspan="2">
                        <asp:Literal ID="lblStrategicObjective" runat="server" /></td>

                    <td class="td1" colspan="2">Anchor:</td>

                    <td class="td2" colspan="6">
                        <asp:Literal ID="lblAnchor" runat="server" /></td>

                </tr>
                <tr>
                    <td class="td1" colspan="2">Key Result Owner:</td>

                    <td class="td2" colspan="6">
                        <asp:Literal ID="lblKRO" runat="server" /></td>

                </tr>

                <tr>
                    <td class="td1" colspan="2">&nbsp;</td>

                    <td class="td1a" colspan="1">ANNUAL TARGET</td>

                    <td class="td1" colspan="2">Q1 TARGET</td>

                    <td class="td1" colspan="2">Q2 TARGET</td>

                    <td class="td1">Q3 TARGET</td>

                    <td class="td1" colspan="2">Q4 TARGET</td>

                </tr>

                <tr>
                    <td class="td1">KEY RESULT:<br />
                        <asp:Literal ID="lblTID" runat="server" />
                        <br />
                    </td>
                    <td class="td1b">
                        <asp:Literal ID="lblKeyResult" runat="server" /></td>

                    <td class="td1b" rowspan="2" colspan="1">
                        <asp:Literal ID="lblAnnualTarget" runat="server" /></td>

                    <td class="td1b" rowspan="2" colspan="2">
                        <asp:Literal ID="lblQ1" runat="server" /></td>

                    <td class="td1b" rowspan="2" colspan="2">
                        <asp:Literal ID="lblQ2" runat="server" /></td>

                    <td class="td1b" rowspan="2">
                        <asp:Literal ID="lblQ3" runat="server" /></td>

                    <td class="td1b" rowspan="2" colspan="2">
                        <asp:Literal ID="lblQ4" runat="server" /></td>

                </tr>

                <tr>
                    <td class="td1">KEY RESULT INDICATOR:</td>
                    <td class="td1b">
                        <asp:Literal ID="lblKeyResultIndicator" runat="server" /></td>

                </tr>






            </tbody>
        </table>
        <br />
        <table style="width: 100%!important; border: 1px solid black">
            <tbody>
                <tr>
                    <td class="td1" colspan="7" style="text-align: center">
                        <asp:Literal ID="lblQuarterHeader" runat="server" /></td>

                </tr>
                <tr>
                    <td class="td1">
                        <asp:Literal ID="lblQuarterName" runat="server" /></td>
                    <td colspan="2" class="tdActualAchievement">Actual Achievement</td>

                    <td class="tdActualAchievement">Variance</td>

                    <td class="tdActualAchievement">Target Met</td>

                    <td colspan="2" class="tdActualAchievement">Data Valid And Correct</td>

                </tr>

                <tr>
                    <td class="tdTable2">
                        <asp:Literal ID="lblQTarget" runat="server" /></td>
                    <td class="td1b" colspan="2">
                        <asp:Literal ID="lblActualAchievement" runat="server" /></td>

                    <td class="td1b">
                        <asp:Literal ID="lblVariance" runat="server" /></td>

                    <td class="td1b">
                        <asp:Literal ID="lblTargetMet" runat="server" /></td>

                    <td class="td1b" colspan="2">
                        <asp:Literal ID="lblDataValidAndCorrect" runat="server" /></td>

                </tr>

                <tr>
                    <td class="td1a">REASON FOR VARIANCE</td>
                    <td class="td1b" colspan="6">
                        <div id="dvVariance">
                            <asp:Literal ID="lblReasonForVairance" runat="server" />

                        </div>
                    </td>

                </tr>

                <tr>
                    <td class="td1a">MITIGATION FOR UNDERPERFORMANCE (Progress, actions and activities completed during the quarter)</td>
                    <td class="td1b" colspan="6">
                        <asp:Literal ID="lblMitigation" runat="server" /></td>

                </tr>

                <tr>
                    <td class="td1a">COMMENT ON PERFORMANCE (Progress, actions and activities completed during the quarter)</td>
                    <td class="td1b" colspan="6">
                        <asp:Literal ID="lblCommentOnPeformance" runat="server" /></td>

                </tr>

                <tr>
                    <td class="td1a">CALCULATED ACCORDING TO THE TECHNICAL INDICATOR DESCRIPTIONS ( APP, Part D)</td>
                    <td class="td1b">
                        <asp:Literal ID="lblCalAccordingTID" runat="server" /></td>

                    <td class="td1">If not Calculated according to the TID, please state reason why and the calculation used</td>

                    <td class="td1b" colspan="4">
                        <asp:Literal ID="lblIfCalAccordingTID" runat="server" /></td>

                </tr>

                <tr>
                    <td class="td1a">DATA SOURCE</td>
                    <td class="td1b" colspan="6">
                        <asp:Literal ID="lblDataSource" runat="server" /></td>

                </tr>

                <tr>
                    <td class="td1a">EVIDENCE (Provide a list)</td>
                    <td class="td1b" colspan="6">
                        <asp:Literal ID="lblEvidence" runat="server" /></td>

                </tr>

                <tr>
                    <td class="td1b" colspan="7">
                        <span style="font-family: Wingdings; color: #FF0000; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none;">Ø</span><span style="font-family: Times New Roman; color: #FF0000; font-size: 7pt; font-weight: 400; font-style: normal; text-decoration: none;">&nbsp; </span><span style="font-family: Calibri; color: #FF0000; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none;">Please ensure that information provided is complete, accurate and valid and has been reviewed prior to submission.</span><p style="text-align: left; textindent: -110pt; padding-left: -110pt; padding-right: 0pt; padding-top: 0pt; padding-bottom: 0pt;">
                            <span style="font-family: Wingdings; color: #FF0000; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none;">Ø</span><span style="font-family: Times New Roman; color: #FF0000; font-size: 7pt; font-weight: 400; font-style: normal; text-decoration: none;">&nbsp; </span><span style="font-family: Calibri; color: #FF0000; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none;">The information you provide is subject to audit by the Auditor-General and Internal Audit.</span>
                        <p style="text-align: left; textindent: -110pt; padding-left: -110pt; padding-right: 0pt; padding-top: 0pt; padding-bottom: 0pt;">
                            <span style="font-family: Wingdings; color: #FF0000; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none;">Ø</span><span style="font-family: Times New Roman; color: #FF0000; font-size: 7pt; font-weight: 400; font-style: normal; text-decoration: none;">&nbsp; </span><span style="font-family: Calibri; color: #FF0000; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none;">It must be emphasised that line managers remain responsible for establishing and running performance information systems within their sections, and for using performance information to make decisions.</span>
                    </td>

                </tr>

            </tbody>
        </table>
        <br id="signature" />
        <div class="pagebreak"></div>
        <table style="width: 100%!important; border: 0px solid black">

            <tr>
                <td class="td1a" rowspan="2" style="border-top: none!important" colspan="2">COMPILER OF THIS REPORT:<br />
                    <asp:Literal ID="lblCompilerName" runat="server" /></td>
                <td class="sig" style="vertical-align: top">SIGN: </td>

                <td style="vertical-align: top">
                    <asp:Image ID="imgCompilerSignature" runat="server" Height="57px" Width="195px" />
                </td>

            </tr>

            <tr>
                <td class="BorderBottom"><strong>DATE: </strong>&nbsp;</td>

                <td colspan="9" class="BorderBottom">
                    <asp:Literal ID="lblCompilerDate" runat="server" /></td>

            </tr>
            <tr>
                <td class="td1a" rowspan="2" colspan="2">KEY RESULT OWNER:<br />
                    <asp:Literal ID="lblKRO1Name" runat="server" /></td>
                <td class="sig" style="vertical-align: top">SIGN:</td>

                <td>
                    <asp:Image ID="imgKRO1Signature" runat="server" Height="57px" Width="195px" />
                </td>

            </tr>

            <tr>
                <td class="BorderBottom"><strong>DATE: </strong>&nbsp;</td>

                <td colspan="9" class="BorderBottom">
                    <asp:Literal ID="lblKRO1Date" runat="server" /></td>

            </tr>
            <tr id="trKRO2Name" runat="server" visible="false">
                <td class="td1a" rowspan="2" colspan="2">KEY RESULT OWNER:<br />
                    <asp:Literal ID="lblKRO2Name" runat="server" /></td>
                <td class="sig">SIGN: 
                </td>

                <td>
                    <asp:Image ID="imgKRO2Signature" runat="server" Height="57px" Width="195px" />
                </td>

            </tr>

            <tr id="trKRO2Date" runat="server" visible="false">
                <td class="BorderBottom"><strong>DATE:</strong> </td>

                <td colspan="9" class="BorderBottom">
                    <asp:Literal ID="lblKRO2Date" runat="server" />
                </td>

            </tr>


            <tr>
                <td class="td1a" rowspan="2" colspan="2">ANCHOR:<br />
                    <asp:Literal ID="lblAnchor1Name" runat="server" /></td>
                <td class="sig">SIGN: 
                </td>

                <td>
                    <asp:Image ID="imgAnchor1Signature" runat="server" Height="57px" Width="195px" />
                </td>

            </tr>

            <tr>
                <td class="BorderBottom"><strong>DATE: </strong></td>

                <td colspan="9" class="BorderBottom">
                    <asp:Literal ID="lblAnchor1Date" runat="server" /></td>

            </tr>
            <tr id="trAnchor2Name" runat="server" visible="false">
                <td class="td1a" rowspan="2" colspan="2">ANCHOR:<br />
                    <asp:Literal ID="lblAnchor2Name" runat="server" /></td>
                <td class="sig">SIGN: 
                </td>

                <td>
                    <asp:Image ID="imgAnchor2Signature" runat="server" Height="57px" Width="195px" />
                </td>

            </tr>

            <tr id="trAnchor2Date" runat="server" visible="false">
                <td class="BorderBottom"><strong>DATE: </strong>&nbsp;</td>

                <td colspan="9" class="BorderBottom">
                    <asp:Literal ID="lblAnchor2Date" runat="server" /></td>

            </tr>

            <tr>
                <td class="noBorder" colspan="7">&nbsp;</td>

            </tr>

            <tr>
                <td class="td1a" colspan="12" style="text-align: center"><strong>TECHNICAL INDICATOR DESCRIPTION iro KEY RESULT (APP
                    <asp:Literal ID="lblPartD" runat="server" />
                    – PART D)</strong></td>

            </tr>


        </table>  
        

        <asp:Image ID="imgTID" runat="server" Height="600px" Width="800px" />
        <table style="min-width:100%!important">
              <tr>
                <td class="td1a" colspan="12" style="text-align: center"><strong>UPLOADED DOCUMENTS<asp:Literal ID="Literal1" runat="server" />
                  </strong></td>

            </tr>
            <tr>
                <td>
                    <asp:GridView DataKeyNames="DocId" CssClass="documents" runat="server" ID="gvDocs" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="NO DOCUMENTS FOR THIS WORK INFO" OnPageIndexChanging="gvDocs_PageIndexChanging" OnRowCommand="gvDocs_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="File Name" DataField="DocumentName" />                       
                        <asp:BoundField HeaderText="Uploaded By" DataField="FullName" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkOpenFile" runat="server" CommandArgument='<%#Eval("DocId") %>' CommandName="OpenFile" Text="Open File"></asp:LinkButton>
                            </ItemTemplate>
                       
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        
                    </Columns>
                    <EmptyDataRowStyle ForeColor="Red" />
                </asp:GridView>
                </td>
            </tr>
        </table>

    </div>
</asp:Content>

