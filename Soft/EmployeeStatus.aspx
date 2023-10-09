<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeStatus.aspx.cs" Inherits="Soft_EmployeeStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Work Status(STFP)</title>
    <link href="../content/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- Theme style -->
    <link href="../content/dist/css/AdminLTE.css" rel="stylesheet" />
    <link href="../content/dist/css/skins/skin-purple.css" rel="stylesheet" />
    <link href="../content/plugins/select2/select2.css" rel="stylesheet" />
    <link href="../content/plugins/highslide/highslide.css" rel="stylesheet" />
    <link href="../content/plugins/highslide/highslide-ie6.css" rel="stylesheet" />
    <link href="../content/plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <link href="../content/plugins/jQueryUI/jquery-ui.css" rel="stylesheet" />
    <link href="../content/plugins/Toster/jquery.toast.css" rel="stylesheet" />
    <link href="../content/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" />
    <script src="../content/plugins/jQuery/jquery.js"></script>
    <script src="../content/plugins/jQueryUI/jquery-ui.min.js"></script>
    <style>
        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 0px;
            line-height: 1.428571;
            vertical-align: top;
            border-top: 1px solid #ddd;
        }
    </style>
    <script type="text/javascript">
        //  const { each } = require("jquery");

        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox

            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
                spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
                    elm[i].id != theBox.id) {
                    //elm[i].click();

                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;

                }
            SelectCheckID();
        }

    </script>
    <script type="text/javascript">
        function SelectCheckID() {
          

            var str = "";
            var aa = document.querySelectorAll("input[type=checkbox]");
            for (var i = 1; i < aa.length; i++) {

                if (aa[i].checked) {
                    var OrderId = aa[i].id.substring(aa[i].id.indexOf('_') + 1);
                    if (str == "") { str = OrderId; }
                    else { str += "," + OrderId; }

                }
            }
            document.getElementById("HDDID").value = str;

        }
    </script>
</head>
<body runat="server">
    <form id="form1" runat="server">
        <section class="content-header" style="height: 2.5em;">
            <div class="col-sm-1">Date</div>
            <div class="col-sm-1">
                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control datepicker" OnTextChanged="drpDept_SelectedIndexChanged" AutoPostBack="true" Height="28px"></asp:TextBox>
            </div>
            <div class="col-sm-1">Department</div>
            <div class="col-sm-3">
                <asp:DropDownList ID="drpDept" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDept_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>


            </div>

            <ol class="breadcrumb">
                <li>

                    <button onclick="history.go(-1); return false;" class="btn btn-sm btn-success">Go Back</button></li>
                <li>
                    <button onclick="location.href='../Soft/Dashboard.aspx'; return false;" class="btn btn-sm btn-success">Go Home</button></li>
                <li>
                    <asp:Button runat="server" ID="btnPrint" Text="Print" class="btn btn-sm btn-success" OnClick="btnPrint_Click" />
                </li>
            </ol>
        </section>
        <section class="content">
            <div class="box box-primary">
                <%--<div class="box-body" > <input type="text" id="checkid"  runat="server" value=""  /></div>--%>
                <asp:HiddenField ID="HDDID" runat="server" Value="0" />
                <div class="box-body" id="Contentblock" runat="server">
                </div>
            </div>
        </section>
        <script src="../content/bootstrap/js/bootstrap.min.js"></script>
        <script src="../content/plugins/datatables/jquery.dataTables.min.js"></script>
        <script src="../content/plugins/datatables/dataTables.bootstrap.min.js"></script>
        <!-- SlimScroll -->
        <script src="../content/plugins/slimScroll/jquery.slimscroll.js"></script>
        <!-- FastClick -->
        <script src="../content/plugins/fastclick/fastclick.js"></script>
        <script src="../content/plugins/select2/select2.full.js"></script>
        <script src="../content/dist/MenuScript.js"></script>
        <script src="../content/bootstrap/sweetalert.js"></script>
        <script src="../content/plugins/highslide/highslide-full.js"></script>
        <!-- AdminLTE App -->
        <script src="../content/dist/js/app.js"></script>
        <script src="../content/plugins/datepicker/bootstrap-datepicker.js"></script>
        <script src="../content/plugins/Toster/jquery.toaster.js"></script>
        <script type="text/javascript">
            $(window).load(function () {
                $(".se-pre-con").fadeOut("slow");;
            });
            $(window).scroll(function () {
                if ($(window).scrollTop() > 200) {
                    $("#back-top").fadeIn(200)
                } else {
                    $("#back-top").fadeOut(200)
                }
            });
            $("#back-top").click(function () {
                $("html, body").stop().animate({
                    scrollTop: 0
                }, "easeInOutExpo")
            });
            $('.select2').select2();
            hs.graphicsDir = '/Admin/plugins/highslide/graphics/';
            hs.wrapperClassName = 'draggable-header';
            hs.outlineType = 'rounded-white';
            hs.align = 'center';
            hs.dimmingOpacity = 0.75;
            hs.Expander.prototype.onAfterClose = function () {
                window.location.reload();
            };
            $('.Hs').click(function () {
                return hs.htmlExpand(this, { objectType: 'iframe' })
            });
        </script>
        <script type="text/javascript"> 
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy',
                timePicker: true,
                todayHighlight: true,
                autoclose: true,
                endDate: new Date(),
            });
        </script>
        <link href="../css/CalenderView.css" rel="stylesheet" />
        <script src="js/jquery-ui.js"></script>
        <link href="js/jquery-ui.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            .ui-datepicker-calendar {
                /*display: tr;*/
            }
        </style>
    </form>
</body>
</html>
