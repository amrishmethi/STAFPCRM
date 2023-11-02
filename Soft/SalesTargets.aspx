<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SalesTargets.aspx.cs" Inherits="Admin_SalesTargets" %>

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
                            <div class="col-md-3 ">
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
                                <asp:DropDownList ID="drpheadQtr" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 hidden">
                                <label>Month</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="mnth"
                                    ErrorMessage="Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:TextBox ID="mnth" runat="server" type="text" class="form-control MnthPicker" autocomplete="off" />
                            </div>
                            <div class="col-md-2">
                                <label>Party Category</label>
                                <asp:DropDownList ID="drpCatg" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpCatg_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Party</label>
                                <asp:DropDownList ID="drpParty" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
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
                                            <th>Party</th>
                                            <th>Station</th>
                                            <th>Category</th>
                                            <th>Whatsapp NO</th>
                                            <asp:Repeater ID="repHead" runat="server">
                                                <ItemTemplate>
                                                    <th><%#Eval("sText") %></th>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repData" runat="server" OnItemDataBound="repData_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex+1 %></td>
                                                    <td> 
                                                        <input id="txtParty" style="width:250px;" readonly value='<%#Eval("PARTY")%>' />
                                                        <asp:HiddenField ID="hddMID" runat="server" Value='<%# Eval("PARTYID") %>' />
                                                    </td>
                                                    <td><%# Eval("STATION") %></td>
                                                    <td> 
                                                        <input id="txtCat" style="width:250px;"  readonly value='<%#Eval("PARTYCATEGORY")%>' />
                                                        <asp:HiddenField ID="HddHeadQtrNo" runat="server" Value='<%# Eval("PTCMSNO") %>' />
                                                        
                                                    </td>
                                                    <td><%# Eval("Whatsappno") %>
                                                        <asp:HiddenField ID="HddNo" runat="server" Value='<%# Eval("Whatsappno") %>' />
                                                    </td>
                                                    <asp:Repeater ID="repd" runat="server">
                                                        <ItemTemplate>
                                                            <td>
                                                                <input id="txtRep" runat="server" class="form-control" onkeypress="return IsNumericKey(event);" value='<%#Eval("Qty")%>' onchange="txtRep_TextChanged(this);" />
                                                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>--%>
                                                                <%--<asp:TextBox ID='txtRep' ClientIDMode="Static" runat="server" CssClass="form-control" onkeypress="return IsNumericKey(event);" Text='<%#Eval("Qty")%>' OnTextChanged="txtRep_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                                                <%--<asp:HiddenField ID="hddSizeVal" runat="server" Value='<%#Eval("SS")%>'></asp:HiddenField>--%>
                                                                <%--</ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="txtRep" EventName="TextChanged" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>--%>
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td colspan="5"></td>
                                            <asp:Repeater ID="RepFoot" runat="server">
                                                <ItemTemplate>
                                                    <td>
                                                        <input id="txtTotal" runat="server" class="form-control" readonly value='<%#Eval("Qty")%>' />
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
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

    <script>
        function txtRep_TextChanged(e) {

            var id = e.id;
            var id1 = e.value;

            $.ajax({
                type: 'POST',
                url: "SalesTargets.aspx/txtRep_TextChanged",
                data: '{Id: "' + id + '" ,Val: "' + id1 + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    document.getElementById("Body_RepFoot_txtTotal_" + response.d.split(',')[0]).value = response.d.split(',')[1];
                }
            });

        }
    </script>
</asp:Content>

