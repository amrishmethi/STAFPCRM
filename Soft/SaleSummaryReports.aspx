<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SaleSummaryReports.aspx.cs" Inherits="Soft_SaleSummaryReports" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>HQ PENDING ORDER SUMMARY(S/T)(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="scpt1" runat="server"></asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>HQ PENDING ORDER SUMMARY (S/T)</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/SaleSummaryReports.aspx" class="active">HQ PENDING ORDER SUMMARY(S/T)</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">

                            <div class="col-md-3">
                                <label>Employee</label>

                                <asp:DropDownList ID="DrpEmployee" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="DrpEmployee_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <label>HeadQuarter</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" InitialValue="0"
                                    Font-Bod="true" ForeColor="Red" Font-Size="Large" ControlToValidate="drpHeadQtr"></asp:RequiredFieldValidator>

                                <asp:DropDownList ID="drpHeadQtr" runat="server" OnSelectedIndexChanged="drpHeadQtr_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Distict</label>
                                <asp:ListBox ID="drpDistict" runat="server" OnSelectedIndexChanged="drpDistict_SelectedIndexChanged1" CssClass="form-control select2" AutoPostBack="true" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-md-2">
                                <label>Station</label>
                                <asp:DropDownList ID="Drpstation" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2 hidden">
                                <label>Rate</label>
                                <asp:DropDownList ID="Drprate" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="With Tax" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Without Tax" Value="0"></asp:ListItem>

                                </asp:DropDownList>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-3 hidden">
                                <label>Report</label>
                                <asp:DropDownList ID="drpReport" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                    <asp:ListItem Text="Complete" Value="Complete"></asp:ListItem>
                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Report Type</label>
                                <asp:DropDownList ID="drpReportType" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Main Group" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Sub Group" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Party</label>
                                <asp:DropDownList ID="drpParty" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Group</label>
                                <asp:ListBox ID="drpGrp" runat="server" CssClass="form-control select2" SelectionMode="Multiple"></asp:ListBox>
                            </div>


                            <div class="col-md-2">
                                <label>Date From</label>
                                <asp:TextBox ID="dpFrom" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>Date To</label>
                                <asp:TextBox ID="dpTo" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" Text="Search" />
                                &nbsp;
 &nbsp;
 &nbsp;
 &nbsp;
 <asp:Button ID="btnExport" runat="server" CssClass="btn btn-success" OnClick="btnExport_Click" Text="Export To Excel" />
                            </div>
                            <div class="clearfix">&nbsp;</div>
                        </div>
                    </div>
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="widget-content">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdReport" ClientIDMode="Static" runat="server" CssClass="table table-bordered display table-striped ">
                                    </asp:GridView>
                                </div>
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
                url: 'UserWiseParty.aspx/ControlAccess',
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
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

