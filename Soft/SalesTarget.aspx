<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SalesTarget.aspx.cs" Inherits="Admin_SalesTarget" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Sales Target(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="scpt1" runat="server"></asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>Sales Target</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/SalesTarget.aspx" class="active">Sales Target</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3 hidden">
                                <label>Department</label>
                                <asp:DropDownList ID="drpDepartment" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Employee Status</label>
                                <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="Active" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Non-Active" Value="Non-Active"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Employee</label>

                                <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>HeadQuarter</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpheadQtr"
                                    ErrorMessage="Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpheadQtr" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpheadQtr_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>District</label>
                                <asp:ListBox ID="drpDistrict" runat="server" SelectionMode="Multiple" CssClass="form-control select2" OnSelectedIndexChanged="drpDistrict_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                            </div>
                            <div class="col-md-2">
                                <label>Station</label>
                                <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpStation_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-2">
                                <asp:CheckBox ID="chk" runat="server" ToolTip="Without Party" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                                <label>Party Category</label>
                                <asp:DropDownList ID="drpCatg" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpCatg_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Party Type</label>
                                <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0" Selected="True">Primary</asp:ListItem>
                                    <asp:ListItem Value="1">Secondary</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Month</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="mnth"
                                    ErrorMessage="Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:TextBox ID="mnth" runat="server" type="text" class="form-control MnthPicker" autocomplete="off" />
                            </div>

                            <div class="col-md-4">
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Get Target"
                                    ValidationGroup="aa" OnClick="btnSubmit_Click" />
                                &nbsp;
                                &nbsp;
                                &nbsp;
                                <asp:Button ID="btnSalary" runat="server" CssClass="btn btn-primary" Text="Save Target"
                                    ValidationGroup="aa" OnClick="btnSalary_Click" />
                            </div> 
                        </div>

                        <div class="clearfix">&nbsp;</div>
                        <div class="clearfix">&nbsp;</div>
                    </div>
                </div>
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="widget-content">
                            <div class="table-responsive">
                                <table id="Tbl" class="table table-bordered display table-striped">
                                    <thead>
                                        <tr>
                                            <th>Sr. No.</th>
                                            <%--<th>Employee</th>--%>
                                            <%--<th>HeadQuarter</th>--%>
                                            <th>District</th>
                                            <th>Party Category</th>
                                            <th>Party</th>
                                            <th>Station</th>
                                            <th>Powder</th>
                                            <th>BAR/ TUB</th>
                                            <th>KLEAN BOLD POWDER</th>
                                            <th>Last Month Sale (Powder)</th>
                                            <th>Last Month Sale (BAR/ TUB)</th>
                                            <th>Last Month Sale (KBP)</th>
                                        </tr>

                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                        <asp:HiddenField ID="hddId" runat="server" Value='<%#Eval("TargetId") %>' />
                                                    </td>
                                                    <%--<td style="text-align: left;"><%#Eval("Employee") %></td>--%>
                                                    <%--<td style="text-align: left;"><%#Eval("HeadQtr") %></td>--%>
                                                    <td style="text-align: left;"><%#Eval("District") %></td>
                                                    <td style="text-align: left;"><%#Eval("PartyCategory") %></td>
                                                    <td style="text-align: left;"><%#Eval("Party") %></td>
                                                    <td style="text-align: left;"><%#Eval("Station") %></td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox type="text" ID="txtPowder" CssClass="form-control Powder" runat="server" value='<%#Eval("Powder") %>' onkeypress="return IsNumericKey(event);" /></td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtBar_Tub" CssClass="form-control Bar_Tub" runat="server" Text='<%#Eval("Bar_Tub") %>' onkeypress="return IsNumericKey(event);"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtclean" CssClass="form-control clean" runat="server" Text='<%#Eval("KLEAN_BOLD_POWDER") %>' onkeypress="return IsNumericKey(event);"></asp:TextBox>
                                                    </td>

                                                    <td style="text-align: left;"><%#Eval("Sale_POWDER") %></td>
                                                    <td style="text-align: left;"><%#Eval("Sale_BAR_AND_TUB") %></td>
                                                    <td style="text-align: left;"><%#Eval("KLEAN_POWDER") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr class="gradeA">
                                            <td></td>
                                            <td>Total</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <input id="lblPowderTotal" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblBar_Tub" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblclean" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblSale_POWDERl" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblSale_BAR_AND_TUB" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblKLEAN_POWDER" runat="server" value="0" readonly class="form-control" /></td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            debugger
            $.ajax({
                url: 'salestarget.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger
                    let text = data.d;
                    const myArray = text.split(",");

                    //document.getElementById("Body_lnkAdd").style.display = myArray[0] == "False" ? "none" : "";

                    //var elements = document.getElementsByClassName("isEditVisible");
                    //Array.prototype.forEach.call(elements, function (element) {
                    //    element.style.display = myArray[1] == "False" ? "none" : "inline";
                    //});
                    //var elements1 = document.getElementsByClassName("isDelVisible");
                    //Array.prototype.forEach.call(elements1, function (element) {
                    //    element.style.display = myArray[2] == "False" ? "none" : "inline";
                    //});
                    //var elements2 = document.getElementsByClassName("isAssVisible");
                    //Array.prototype.forEach.call(elements2, function (element) {
                    //    element.style.display = myArray[4] == "False" ? "none" : "";
                    //});
                    //var elements3 = document.getElementsByClassName("isLoginVisible");
                    //Array.prototype.forEach.call(elements3, function (element) {
                    //    element.style.display = myArray[5] == "False" ? "none" : "";
                    //});

                    //if (myArray[1] == 'False' && myArray[2] == 'False') {
                    //    document.getElementById("lblAction").innerHTML = "";

                    //}
                    document.getElementsByClassName("buttons-excel")[0].style.display = myArray[3] == "False" ? "none" : "";
                    document.getElementsByClassName("buttons-pdf")[0].style.display = myArray[3] == "False" ? "none" : "";
                },
                error: function (response) {
                },
                failure: function (response) {
                }
            });
        })

    </script>
    <script> 

        $(document).ready(function () {


            $('#Tbl').DataTable({
                dom: '<"dt-top-container"<l><"dt-center-in-div"B><f>r>t<ip>',
                "processing": true,
                //    "serverSide": true,
                // paging:false,
                rowReorder: {
                    selector: 'td:nth-child(1)'
                },

                buttons: [
                    {
                        extend: 'excelHtml5', footer: true,
                        exportOptions: {
                            columns: ':visible',
                            format: {
                                body: function (inner, rowidx, colidx, node) {
                                    if ($(node).children("input").length > 0) {
                                        return $(node).children("input").first().val();
                                    } else {
                                        return inner;
                                    }
                                }
                            }
                        }
                    },
                    'colvis'
                ],
                pageLength: -1,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]

            });

            $("#Tbl").on('input', '.Powder', function () {
                var calculated_total_sum = 0;

                $("#Tbl .Powder").each(function () {
                    var get_textbox_value = $(this).val();
                    if ($.isNumeric(get_textbox_value)) {
                        calculated_total_sum += parseFloat(get_textbox_value);
                    }
                });
                document.getElementById("Body_lblPowderTotal").value = calculated_total_sum;
            });

            $("#Tbl").on('input', '.Bar_Tub', function () {
                var calculated_total_sum = 0;

                $("#Tbl .Bar_Tub").each(function () {
                    var get_textbox_value = $(this).val();
                    if ($.isNumeric(get_textbox_value)) {
                        calculated_total_sum += parseFloat(get_textbox_value);
                    }
                });
                document.getElementById("Body_lblBar_Tub").value = calculated_total_sum;
            });

            $("#Tbl").on('input', '.clean', function () {
                var calculated_total_sum = 0;

                $("#Tbl .clean").each(function () {
                    var get_textbox_value = $(this).val();
                    if ($.isNumeric(get_textbox_value)) {
                        calculated_total_sum += parseFloat(get_textbox_value);
                    }
                });
                document.getElementById("Body_lblclean").value = calculated_total_sum;
            });
        });


    </script>
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

