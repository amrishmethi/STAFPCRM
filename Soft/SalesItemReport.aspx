<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="SalesItemReport.aspx.cs" Inherits="Soft_SalesItem_Report" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Sales Item Report(STAFP)</title>
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
    <uc1:DTCSS runat="server" ID="DTCSS" />
      
</head>
<body style="background-color: whitesmoke;">
    <form id="form1" runat="server">
        <section class="content-header">
            <div class="col-sm-4">
                <h4>Primary Party:<asp:Label ID="lblParty" runat="server"></asp:Label>
                </h4>
            </div>
            <div class="col-sm-4">
                <h4>Station:<asp:Label ID="lblStation" runat="server"></asp:Label>
                </h4>
            </div>
            <div class="col-sm-4" align="right">
                 <a class="btn btn-success" href="SecondarySalesReport.aspx">Back
                 </a>
            </div>
            <div class="clearfix">&nbsp;</div>
            <div class="box box-primary">
                <div class="box-body">
                    <div class="form-group">

                        <div class="col-md-2">
                            <label>Station</label>
                            <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label> Party</label>
                            <asp:DropDownList ID="drpParty" runat="server" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label>Date From</label>
                            <asp:TextBox ID="dpFrom" runat="server" Height="28px" CssClass="form-control datepicker">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Date To</label>
                            <asp:TextBox ID="dpTo" runat="server" Height="28px" CssClass="form-control datepicker">
                            </asp:TextBox>
                        </div>


                        <div class="col-md-1" style="padding-top: 3px;">
                            <div class="clearfix">&nbsp;</div>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                        <div class="clearfix">&nbsp;</div>

                    </div>
                </div>
            </div>
        </section>
        <section class="content">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="widget-content nopadding" style="overflow: auto;">
                        <div class="table-responsive">
                            <table id="ExportTbl" class="table table-bordered display" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>Sr. No.</th>
                                        <th>User</th>
                                        <th>Date</th>
                                        <th>Time</th>
                                        <th>Party</th>
                                        <th>Station</th>
                                        <th>Mobile No</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rep" runat="server" OnItemDataBound="rep_ItemDataBound">
                                        <ItemTemplate>
                                            <tr class="gradeA" style="background-color: lightslategrey; color: white;">
                                                <td>
                                                    <%#Container.ItemIndex+1 %>
                                                    <asp:HiddenField ID="hddid" runat="server" Value='<%#Eval("ChkOutID") %>' />
                                                </td>
                                                <td><%#Eval("UserName") %></td>
                                                <td><%#Eval("CheckDate") %></td>
                                                <td><%#Eval("CheckTime") %></td>
                                                <td><%#Eval("SecondaryParty") %></td>
                                                <td><%#Eval("SecondaryStation") %></td>
                                                <td><%#Eval("MobileNo") %></td>

                                            </tr>
                                            <tr>
                                                <td colspan="9" style="background-color: lightgray;">
                                                    <table id="example" class="table table-bordered display table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th>Sr. No.</th>
                                                                <th>Group</th>
                                                                <th>Item</th>
                                                                <th>Qty</th>
                                                                <th>Rate</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rep1" runat="server">
                                                                <ItemTemplate>
                                                                    <tr class="gradeA">
                                                                        <td>
                                                                            <%#Container.ItemIndex+1 %>
                                                                        </td>
                                                                        <td style="text-align: left;"><%#Eval("GroupName") %></td>
                                                                        <td style="text-align: left;"><%#Eval("ITName") %></td>
                                                                        <td style="text-align: left;"><%#Eval("OrdQty") %></td>
                                                                        <td style="text-align: left;"><%#Eval("OrdStpRate") %></td>
                                                                    </tr>
                                                                </ItemTemplate>

                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
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
        <uc1:DTJS runat="server" ID="DTJS" />
    </form>
</body>
</html>
