<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="UserWiseParty.aspx.cs" Inherits="Admin_UserWiseParty" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Employee Wise Party(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="scpt1" runat="server"></asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>Employee Wise Party</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/UserWiseParty.aspx" class="active">Employee Wise Party</a></li>
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
                                <label>
                                    Employee Status
                                </label>
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
                            <div class="col-md-2 hidden">
                                <label>HeadQuarter</label>
                                <asp:DropDownList ID="drpheadQtr" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpheadQtr_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>District</label>
                                <asp:DropDownList ID="drpDistrict" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDistrict_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Station</label>
                                <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpStation_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox ID="chk" runat="server" ToolTip="Without Party" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" Visible="false" />
                                <label>
                                    Party Category 
                                </label>
                                <asp:DropDownList ID="drpCatg" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpCatg_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Party Type</label>
                                <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Value="0" Selected="True">Primary</asp:ListItem>
                                    <asp:ListItem Value="1">Secondary</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Report Type</label>
                                <asp:DropDownList ID="drpReportType" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0" Selected="True">ALL</asp:ListItem>
                                    <asp:ListItem Value="1">Without Category</asp:ListItem>
                                    <asp:ListItem Value="2">With Category</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>GST Type</label>
                                <asp:DropDownList ID="drpGst" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Value="0" Selected="True">ALL</asp:ListItem>
                                    <asp:ListItem Value="1">Without GST</asp:ListItem>
                                    <asp:ListItem Value="2">With GST</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Month</label>
                                <asp:TextBox ID="mnth" runat="server" type="text" class="form-control MnthPicker" autocomplete="off" />
                            </div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Get Data" OnClick="btnSearch_Click" />
                            </div>
                            <%--<div class="col-md-2">
                                <label>Status</label>
                                <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="Active" Text="Active"></asp:ListItem>
                                    <asp:ListItem Value="Non-Active" Text="Non-Active"></asp:ListItem>
                                </asp:DropDownList>
                            </div>--%>
                            <%-- </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drpUser" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="drpheadQtr" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="drpDistrict" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>--%>
                        </div>

                        <div class="clearfix">&nbsp;</div>
                        <div class="clearfix">&nbsp;</div>
                    </div>
                </div>
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="widget-content">
                            <div class="table-responsive">
                                <table id="ExportTbl" class="table table-bordered display table-striped">
                                    <thead>
                                        <tr>
                                            <th>Sr. No.</th>
                                            <th>HeadQuarter</th>
                                            <th>District</th>
                                            <th>Station</th>
                                            <th>Party Category</th>
                                            <th>Party</th>
                                            <th>GSTNo</th>
                                            <th>Mobile</th>
                                            <th>WhatsApp</th>
                                            <th>isCheckIn</th>
                                        </tr>

                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("HeadQtr") %></td>
                                                    <td style="text-align: left;"><%#Eval("District") %></td>
                                                    <td style="text-align: left;"><%#Eval("Station") %></td>
                                                    <td style="text-align: left;"><%#Eval("PartyCategory") %></td>
                                                    <td style="text-align: left;"><%#Eval("Party") %></td>
                                                    <td style="text-align: left;"><%#Eval("GSTNo") %></td>
                                                    <td style="text-align: left;"><%#Eval("Mobile") %></td>
                                                    <td style="text-align: left;"><%#Eval("WhatsAppNo") %></td>
                                                    <td style="text-align: left;"><%#Eval("isCheckIn") %></td>
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
                url: 'UserWiseParty.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

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
    <script type="text/javascript">
        $('.MnthPicker').datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            format: "mm-yyyy",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true
        });
    </script>
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

