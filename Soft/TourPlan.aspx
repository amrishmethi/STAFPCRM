<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TourPlan.aspx.cs" Inherits="Soft_TourPlan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="scrptmgr" runat="server"></asp:ScriptManager>
        <section class="content-header">
            <h1>Update Tour Plan
            </h1>

        </section>
        <section class="content">

            <div class="box box-primary">
                <div class="box-body">
                    <div class="clearfix">&nbsp;</div>
                    <asp:HiddenField ID="hddid" runat="server" />
                    <%-- <asp:UpdatePanel ID="updt1" runat="server">
                        <ContentTemplate>--%>
                    <div class="col-md-4">
                        <label class="control-label">Employee </label>
                        <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2" Enabled="false" OnSelectedIndexChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label>HeadQuarter</label>
                        <asp:DropDownList ID="drpheadQtr" runat="server" CssClass="form-control select2" Enabled="false" OnSelectedIndexChanged="drpheadQtr_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label>District</label>
                        <asp:DropDownList ID="drpDistrict" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDistrict_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label>Station</label>
                        <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>Tour Date</label>
                        <asp:TextBox ID="dpFrom" runat="server" CssClass="form-control datepicker1">
                        </asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Purpose</label>

                        <asp:DropDownList ID="txtPurpose" runat="server" CssClass="form-control select2">
                            <asp:ListItem Value="Secondary Sales">Secondary Sales</asp:ListItem>
                            <asp:ListItem Value="Client Meet">Client Meet</asp:ListItem>
                            <asp:ListItem Value="For Payment">For Payment</asp:ListItem>
                            <asp:ListItem Value="Week Off">Week Off</asp:ListItem>
                            <asp:ListItem Value="Leave">Leave</asp:ListItem>
                            <asp:ListItem Value="Other">Other</asp:ListItem>

                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <br />
                        <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnsave_Click" />
                    </div>
                    <div class="clearfix"></div>

                    <%-- </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="drpUser" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="drpheadQtr" EventName="SelectedIndexChanged" /> 
                        </Triggers>
                    </asp:UpdatePanel>--%>
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
            $('.datepicker1').datepicker({
                format: 'dd/mm/yyyy',
                timePicker: true,
                todayHighlight: true,
                autoclose: true,
            });
        </script>
    </form>
</body>
</html>
