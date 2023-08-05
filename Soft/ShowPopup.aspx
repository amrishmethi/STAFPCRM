<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowPopup.aspx.cs" Inherits="Soft_ShowPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <%--<title>Soft</title>--%>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no"
        name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link href="../content/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- Theme style -->
    <link href="../content/dist/css/AdminLTE.css" rel="stylesheet" />
    <link href="../content/dist/css/skins/skin-purple.css" rel="stylesheet" />
    <link href="../content/plugins/select2/select2.css" rel="stylesheet" />
    <link href="../content/plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <link href="../content/plugins/jQueryUI/jquery-ui.css" rel="stylesheet" />
    <link href="../content/plugins/Toster/jquery.toast.css" rel="stylesheet" />
    <link href="../content/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" />
    <script src="../content/plugins/jQuery/jquery.js"></script>
    <script src="../content/plugins/jQueryUI/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="../css/data-table/bootstrap-table.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.0/css/jquery.dataTables.css">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.37/css/bootstrap-datetimepicker.min.css" rel="stylesheet">
    <link rel="stylesheet" href="../css/data-table/bootstrap-editable.css" />
    <link href="../colorbox/colorbox.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            var getd = sessionStorage.getItem("menu");
            if (getd == "set") {
                $('body').addClass('sidebar-collapse');
            }
        });
    </script>
    <style>
        .no-js #loader {
            display: none;
        }

        .js #loader {
            display: block;
            position: absolute;
            left: 100px;
            top: 0;
        }

        .se-pre-con {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url(../content/dist/img/Preloader_10.gif) center no-repeat #fff;
        }

        #back-top {
            bottom: 20px;
            position: fixed;
            right: 25px;
            z-index: 9;
            background: #ebebeb;
            padding: 0px 3px;
            box-shadow: 0px 1px 5px 0.5px #888;
            color: #000;
        }
    </style>
    <style type="text/css">
        /*td {
    font-size: 1px;
  }*/
        /* th {
    font-size: 12px;
  }*/
        .page_enabled, .page_disabled {
            display: inline-block;
            /* height: 20px;*/
            min-width: 20px;
            line-height: 20px;
            text-align: center;
            text-decoration: none;
            border: 1px solid #ccc;
        }

        .page_enabled {
            background-color: #337ab7;
            color: #fff;
        }

        .page_disabled {
            background-color: #00aff0 !important;
            color: #ffffff !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="content-header" style="height: 2.5em;">
            <h1>Salary Calculation of <span id="lblName" runat="server"></span>
            </h1>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="widget-content">
                                <table class="table table-bordered display table-striped">
                                    <asp:Repeater ID="rep" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>Net Salary (A)</td>
                                                <td><%#Eval("Net_Salary") %> </td>
                                            </tr>
                                            <tr>
                                                <td>Total Days In Month (B) </td>
                                                <td><%#Eval("NOOFWORKINGDAY") %></td>
                                            </tr>
                                            <tr>
                                                <td>Salary Working Days (C)</td>
                                                <td><%#Eval("SALARYDAY") %></td>
                                            </tr>
                                            <tr>
                                                <td>Basic Salary [(A*C)/B] </td>
                                                <td><%#Eval("BASIC_SALARYVALUE") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>

                                <table class="table table-bordered display table-striped">
                                    <asp:Repeater ID="rptUserViewAttendance" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>Month Days</td>
                                                <td><%#Eval("NOOFWORKINGDAY") %> </td>
                                            </tr>
                                            <tr>
                                                <td>Attendance </td>
                                                <td><%#Eval("NOOFATTANDANCE") %></td>
                                            </tr>
                                            <tr>
                                                <td>Sunday Off</td>
                                                <td><%#Eval("ALLOWSUNDAY") %></td>
                                            </tr>
                                            <tr>
                                                <td>Holiday Off</td>
                                                <td><%#Eval("NoOfHoliday") %></td>
                                            </tr>
                                            <tr>
                                                <td>PL</td>
                                                <td><%#Eval("PL") %></td>
                                            </tr>
                                            <tr style="background-color: green; color: white;">
                                                <td>Basic Salary </td>
                                                <td><%#Eval("TOTALWORKINGDAY") %></td>
                                            </tr>
                                            <tr>
                                                <td>Sunday Work</td>
                                                <td><%#Eval("NOOFSUNDAYWork") %></td>
                                            </tr>
                                            <tr>
                                                <td>Holiday Work </td>
                                                <td><%#Eval("NOOFHolidayWork") %></td>
                                            </tr>

                                            <tr style="background-color: green; color: white;">
                                                <td>Over Time</td>
                                                <td><%#Eval("TotalOT") %></td>
                                            </tr>
                                            <tr style="background-color: Red; color: white;">
                                                <td>Leave</td>
                                                <td><%#Eval("TOTALLEAVE") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
    <!-- Bootstrap 3.3.6 -->
    <script src="../JsP/data-table/bootstrap-table.js"></script>
    <script src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.js"></script>
    <script src="../JsP/data-table/tableExport.js"></script>
    <script src="../JsP/data-table/data-table-active.js"></script>
    <script src="../jsp/data-table/bootstrap-table-editable.js"></script>
    <%--<script src="../jsp/data-table/bootstrap-editable.js"></script>--%>
    <script src="../JsP/data-table/bootstrap-table-resizable.js"></script>
    <script src="../JsP/data-table/colResizable-1.5.source.js"></script>
    <script src="../JsP/data-table/bootstrap-table-export.js"></script>


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
    <!-- SoftLTE App -->
    <script src="../content/dist/js/app.js"></script>
    <script src="../content/plugins/datepicker/bootstrap-datepicker.js"></script>
    <script src="../content/plugins/Toster/jquery.toaster.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.37/js/bootstrap-datetimepicker.min.js"></script>
</body>
</html>
