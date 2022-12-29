<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateCreateDealer.aspx.cs" Inherits="Admin_UpdateCreateDealer" %>

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
        <section class="content-header">
            <h1>Update Create Dealer
            </h1>

        </section>
        <section class="content">
            <div class="box box-primary">
                <div class="box-body">
                    
                    <div class="clearfix">&nbsp;</div>
                    <asp:ScriptManager ID="scrptmgr" runat="server"></asp:ScriptManager>
                    <div class="col-sm-1">
                        <label>Name</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:UpdatePanel ID="updt" runat="server">
                        <ContentTemplate>
                    <div class="col-sm-1">
                        <label>Station</label>
                    </div>
                            <div class="col-sm-5">
                                <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpStation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                           <div class="clearfix">&nbsp;</div>
                            <div class="col-sm-1">
                                <label>Address</label>
                            </div>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <label>District</label>
                            </div>
                            <div class="col-sm-5">
                                <asp:DropDownList ID="drpDistrict" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="drpStation" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                 <div class="clearfix">&nbsp;</div>
                    <div class="col-sm-1">
                        <label>Pin Code</label>
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtZip" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-1" >
                        <label>State</label>
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtState" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    
                    <div class="col-sm-1">
                        <label>Contact Person</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtContPer" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                  <div class="clearfix">&nbsp;</div>
                    <div class="col-sm-1">
                        <label>Gst No</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtGst" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                   

                    <div class="col-sm-1">
                        <label>Gst Reg Type</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtGstRegType" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                  <div class="clearfix">&nbsp;</div>
                    <div class="col-sm-1">
                        <label>Mobile No</label>
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    
                    <div class="col-sm-1">
                        <label>WhatsApp No</label>
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtWhtsApp" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-1">
                        <label>Transport</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtTransport" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                  <div class="clearfix">&nbsp;</div>
                    <div class="col-sm-1">
                        <label>Party Type</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2">
                            <asp:ListItem Value="Channel Sales Party" Text="Channel Sales Party"></asp:ListItem>
                            <asp:ListItem Value="Direct Sales Party" Text="Direct Sales Party"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-1">
                        <label>Party Category</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:DropDownList ID="drpPartyCatg" runat="server" CssClass="form-control select2">
                        </asp:DropDownList>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                </div>
                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
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
    </form>
</body>
</html>
