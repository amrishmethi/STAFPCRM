<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PDFPrint.aspx.cs" Inherits="soft_PDFPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <style type="text/css" media="print">
        @page {
            size: auto;
            margin-top: 0mm;
            margin-bottom: 0mm;
        }

        .navbar .navbar-nav > li > a.btn.btn-primary, .btn-primary {
            border-color: #7A9E9F;
            color: #7A9E9F;
        }

        .btn, .navbar .navbar-nav > li > a.btn {
            border-radius: 20px;
            box-sizing: border-box;
            border-width: 2px;
            background-color: transparent;
            font-size: 14px;
            font-weight: 600;
            padding: 7px 18px;
            transition: all 150ms linear;
        }

        .btn {
            display: inline-block;
            text-align: center;
            vertical-align: middle;
            cursor: pointer;
            user-select: none;
            border: 1px solid transparent;
            line-height: 1.5;
        }

        h1, .h1, h2, .h2, h3, .h3, h4, .h4, h5, .h5, h6, .h6, p, .navbar, .brand, a, .td-name, td {
            font-family: 'Muli', 'Helvetica', Arial, sans-serif;
        }

        a {
            text-decoration: none;
        }

        * {
            margin: 0;
        }
    </style>
    <style type="text/css">
        .navbar .navbar-nav > li > a.btn.btn-primary, .btn-primary {
            border-color: #7A9E9F;
            color: #7A9E9F;
        }

        .btn, .navbar .navbar-nav > li > a.btn {
            border-radius: 20px;
            box-sizing: border-box;
            border-width: 2px;
            background-color: transparent;
            font-size: 14px;
            font-weight: 600;
            padding: 7px 18px;
            transition: all 150ms linear;
        }

        .btn {
            display: inline-block;
            text-align: center;
            vertical-align: middle;
            cursor: pointer;
            user-select: none;
            /*border: 1px solid transparent;*/
            line-height: 1.5;
        }

        h1, .h1, h2, .h2, h3, .h3, h4, .h4, h5, .h5, h6, .h6, p, .navbar, .brand, a, .td-name, td {
            font-family: 'Muli', 'Helvetica', Arial, sans-serif;
        }

        * {
            margin: 0;
        }

        .btn.btn-primary, .btn-primary:hover {
            background-color: #7A9E9F;
            color: rgba(255, 255, 255, 0.85);
            border-color: #7A9E9F;
        }

        table.hrtbl {
            width: 100%;
        }
    </style>

  
</head>
<body onload="printContent('divPrint')">
    <form id="form1" runat="server">
        <div class="col-md-12" style="padding: 0px 10px;">
            <table class="hrtbl">
                <tbody>
                    <tr>
                        <td>
                            
                        </td>
                    </tr>
                </tbody>
            </table>
            <table class="hrtbl">
                <!-- START MAIN CONTENT AREA -->
                <tbody>
                    <tr>
                       
                        <td style="padding: 15px;" align="left">
                            <%--<a class="btn btn-primary" onclick="printContent('divPrint')">Print</a>--%>
                        </td>
                    </tr>
                </tbody>
                <!-- END MAIN CONTENT AREA -->
            </table>
            <table class="hrtbl">
                <tbody>
                    <tr>
                        <td>
                           
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="divPrint" class="col-md-12" style="padding: 0px 10px;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <table width="900" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                       
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                                    <tr>
                                                        <td>
                                                            <img id="imgLogo" src="~/img/logo.jpg" runat="server" style="height: 100px;" /></td>
                                                        <td align="right"><strong style="color: #4d919d;">Shree Tadkeshwar Agro Food Product</strong>
                                                            <br />
                                                              H- 1-37- A, Sarna Doongar Industrial Area, Jhotwara Extension,
                                                            <br />
                                                            Jaipur - 302012 Rajasthan, India
                                                            <br />
                                                           </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td style="border-top: 2px solid #09F; padding: 5px 5px;"><strong style="color: #000;">&nbsp;
                                            </strong></td>
                                        </tr>
                                        
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>

            <div id="hdd" runat="server">
        </div>
        </div>
    </form> 
    <script src="../JsP/jquery-1.10.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
          // window.print(); 
            //var restorepage = $('body').html();
            //var printcontent = $('#divPrint').clone();
            //$('body').empty().html(printcontent);
            //window.print();
            //$('body').html(restorepage);
        });

        function printContent(el) {
            var restorepage = $('body').html();
            var printcontent = $('#' + el).clone();
            $('body').empty().html(printcontent);
            window.print();
            $('body').html(restorepage);
        };
        //function showAll() {
        //    $('.AgencyInfo').show();
        //    $('.AmountInfo').show();
        //};
        //function showAge() {
        //    $('.AgencyInfo').show();
        //    $('.AmountInfo').hide();
        //};
        //function showPrice() {
        //    $('.AgencyInfo').hide();
        //    $('.AmountInfo').show();
        //};
        //function HideBoth() {
        //    debugger;
        //    $('.AgencyInfo').hide();
        //    $('.AmountInfo').hide();
        //}
    </script>
</body>
</html>
