﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SalesTargetsView.aspx.cs" Inherits="Admin_SalesTargetsView" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Sales Target View(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="scpt1" runat="server"></asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>Sales Target View</h1>
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
                            <div class="col-md-3">
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
                                <asp:DropDownList ID="drpheadQtr" runat="server" CssClass="form-control select2">
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
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Get Report"
                                    ValidationGroup="aa" OnClick="btnSubmit_Click" />
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
                                            <th rowspan="2">Sr. No.</th>
                                            <th rowspan="2">Employee</th>
                                            <th rowspan="2">HeadQuarter</th>
                                            <asp:Repeater ID="repHead" runat="server">
                                                <ItemTemplate>
                                                    <th style="text-align: center" colspan="3"><%#Eval("sText") %></th>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                        <tr>
                                            <asp:Repeater ID="repHead1" runat="server">
                                                <ItemTemplate>
                                                    <th>Target</th>
                                                    <th>Sale</th>
                                                    <th>Balance</th>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repDetail" runat="server" OnItemDataBound="repDetail_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex+1%></td>
                                                    <td><%#Eval("EMP_NAME") %></td>
                                                    <td><%#Eval("MSNAME") %>
                                                        <asp:HiddenField ID="HddHeadQtrNo" runat="server" Value='<%#Eval("HEADQTR") %>' />
                                                    </td>
                                                    <asp:repeater ID="repDetail1" runat="server">
                                                        <ItemTemplate>
                                                            <td><%#Eval("Qty")%></td>
                                                        </ItemTemplate>
                                                    </asp:repeater>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>

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

