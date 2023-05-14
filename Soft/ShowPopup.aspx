<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowPopup.aspx.cs" Inherits="Soft_ShowPopup" %>

<!DOCTYPE html>

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
            <h1>Monthly Salary Sheet
            </h1>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="form-group">
                                <div class="col-md-3">
                                    <label>Net Salary</label>
                                    <asp:Label ID="lblNetSalary" runat="server" Text="00000"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <label>Basic Salary</label>
                                    <asp:Label ID="lblBasicSalary" runat="server" Text="00000"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <label>Total Days In Month</label>
                                    <asp:Label ID="lblTotalDay" runat="server" Text="00000"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <label>Working Days </label>
                                    <asp:Label ID="lblWorkingDay" runat="server" Text="00000"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
