<%@ Page Title="Night Stay (STAFP)" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="NIghtStay.aspx.cs" Inherits="Soft_NIghtStay" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">

        <h1><a id="lnkAdd" runat="server" href="/Soft/NightStayA.aspx" class="btn btn-primary">Add Night Stay</a>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/NIghtStay.aspx" class="active">Night Stay</a></li>
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
                            <div class="col-md-3 hidden">
                                <label>Designation</label>
                                <asp:DropDownList ID="drpDesignation" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Employee</label>
                                <asp:DropDownList ID="drpProjectManager" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpProjectManager_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 isEditVisible1">
                                <label>Date From</label>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datepicker " placeholder="dd/MM/yyyy" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-md-2 isEditVisible1">
                                <label>Date To</label>
                                <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control datepicker " placeholder="dd/MM/yyyy" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>Status</label>
                                <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="Active" Text="Active" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Non-Active" Text="Non-Active"></asp:ListItem>
                                </asp:DropDownList>
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
                                <table id="ExportTbl" class="table table-bordered display table-striped table-responsive" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">Sr. No.</th>
                                            <th style="text-align: left;">Date</th>
                                            <th style="text-align: left;">Department</th>
                                            <th style="text-align: left;">Emp Name</th>
                                            <th style="text-align: left;">Charges </th>
                                            <th style="text-align: left;">Charges Type </th>
                                            <th style="text-align: left;">Attandance By</th>
                                            <th style="text-align: left;">&nbsp;</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server" OnItemCommand="rep_ItemCommand">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("ATTENDANCEDATE1") %></td>
                                                    <td style="text-align: left;"><%#Eval("DEPT_NAME") %></td>
                                                    <td style="text-align: left;"><%#Eval("Emp_Name") %></td>
                                                    <td style="text-align: left;"><%#Eval("CHARGES") %> </td>
                                                    <td style="text-align: left;"><%#Eval("CHARGESTYPE") %> </td>
                                                    <td style="text-align: left;"><%#Eval("AttandanceBy") %> </td>
                                                    <td style="text-align: left;">
                                                        <asp:LinkButton ID="lnkOut" runat="server" Style="padding: 1px 6px; font-size: 11px;" CommandName='<%#Eval("CHARGESTYPE1") %>' CssClass="btn btn-small btn-success" CommandArgument='<%#Eval("ID") %>'><%#Eval("CHARGESTYPE1") %></asp:LinkButton>

                                                        <asp:LinkButton ID="lnkDelete" runat="server" Style="padding: 1px 6px; font-size: 11px;" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" CommandName="Delete" CssClass="btn btn-small btn-danger" CommandArgument='<%#Eval("ID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
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
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                url: 'NIghtStay.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    let text = data.d;
                    const myArray = text.split(",");
                    var elements = document.getElementsByClassName("isEditVisible1");
                    Array.prototype.forEach.call(elements, function (element) {
                        element.style.display = myArray[1] == "False" ? "none" : "inline";
                    });
                },
                error: function (response) {
                },
                failure: function (response) {
                }
            });
        })
    </script>
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>
