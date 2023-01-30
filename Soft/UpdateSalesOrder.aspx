<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateSalesOrder.aspx.cs" Inherits="Admin_UpdateSalesOrder" %>

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
            <h1>Update Sales Order
            </h1>

        </section>
        <section class="content">
            <div class="box box-primary">
                <div class="box-body">

                    <div class="clearfix">&nbsp;</div>
                    <asp:ScriptManager ID="scrptmgr" runat="server"></asp:ScriptManager>
                    <div class="col-sm-1">
                        <label>Date</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-sm-1">
                        <label>Employee</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtEmp" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>

                    <div class="col-sm-1">
                        <label>Party</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:DropDownList ID="drpParty" runat="server" CssClass="form-control select2"></asp:DropDownList>
                    </div>

                    <div class="col-sm-1">
                        <label>Delivery Mode</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:DropDownList ID="drpDelvMode" runat="server" CssClass="form-control">
                            <asp:ListItem Value="BUYER">Ex-Buyer PLANT</asp:ListItem>
                            <asp:ListItem Value="SELER">Ex-Seller PLANT</asp:ListItem>
                            <asp:ListItem Value="PLANT">Ex-PLANT</asp:ListItem>
                            <asp:ListItem Value="LOCAL">Ex-LOCAL</asp:ListItem>
                            <asp:ListItem Value="ONSIT">Ex-ONSITE</asp:ListItem>
                            <asp:ListItem Value="DEPO">Ex-Depo</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="clearfix">&nbsp;</div>

                    <div class="col-sm-1">
                        <label>Payment Mode</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:DropDownList ID="drpPymtMode" runat="server" CssClass="form-control select2">
                            <asp:ListItem Value="CRD" Text="Credit"></asp:ListItem>
                            <asp:ListItem Value="ADV" Text="Advance"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
            </div>
            <div class="box box-primary">
                <div class="box-body">
                    <div class="clearfix">&nbsp;</div>
                    <asp:HiddenField ID="hddid" runat="server"/>
                    <asp:UpdatePanel ID="updt1" runat="server">
                    <ContentTemplate>
                        <div class="col-sm-4">
                        <label>Group</label>
                        <asp:DropDownList ID="drpGroup" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpGroup_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <label>Item</label>
                        <asp:DropDownList ID="drpItem" runat="server" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                        
                        </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="drpGroup" EventName="SelectedIndexChanged"/></Triggers>    
                    </asp:UpdatePanel>
                    <div class="col-sm-1">
                        <label>Bag/Case</label>
                        <asp:TextBox ID="txtQty" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-2">
                        <label>Rate Per Kg</label>
                        <asp:TextBox ID="txtRate" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-1" style="padding-top: 3px;" >
                        <div class="clearfix">&nbsp;</div>
                        <asp:LinkButton ID="btnplus" runat="server" CssClass="btn btn-primary fa fa-plus" OnClick="btnplus_Click"></asp:LinkButton>
                    </div>
                </div>
                <div class="clearfix">&nbsp;</div>
                <div class="widget-content">
                    <div class="table-responsive">
                        <table border="1" class="table display table-striped">
                            <thead>
                                <tr>
                                    <td>#</td>
                                    <td>Group</td>
                                    <td>Item</td>
                                    <td>Bag/Case</td>
                                    <td>Rate/Kg</td>
                                    <td>Action</td>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rep" runat="server" OnItemCommand="rep_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Container.ItemIndex+1 %></td>
                                            <td><%#Eval("Grp") %></td>
                                            <td><%#Eval("ITName") %></td>
                                            <td><%#Eval("OrdQty","{0:0}") %></td>
                                            <td><%#Eval("OrdStpRate") %></td>
                                            <td style="text-align: left;">
                                                <div class="isEditVisible" style="display: inline;">
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese" aria-label="Edit" rel="lightbox" CommandName="Edit" CommandArgument='<%#Container.ItemIndex+1 %>'><i class="fa fa-pencil"></i></asp:LinkButton>
                                                </div>
                                                <div class="isDelVisible" style="display: inline;">
                                                    <asp:LinkButton ID="lnkDelete" runat="server" Style="padding: 1px 6px; font-size: 11px;" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" CommandName="Delete" CssClass="btn btn-small btn-danger"
                                                        CommandArgument='<%#Container.ItemIndex+1 %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                </div>

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
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
