<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SecondarySalesReport.aspx.cs" Inherits="Admin_SecondarySalesReport" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Secondary Sales Report(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Secondary Sales Report</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/SecondarySalesReport.aspx" class="active">Secondary Sales Report</a></li>
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
                                <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpIsCheck_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Station</label>
                                <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpIsCheck_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Primary Party</label>
                                <asp:DropDownList ID="drpParty" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpIsCheck_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Date From</label>
                                <asp:TextBox ID="dpFrom" runat="server" CssClass="form-control datepicker" OnTextChanged="drpIsCheck_SelectedIndexChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>Date To</label>
                                <asp:TextBox ID="dpTo" runat="server" CssClass="form-control datepicker" OnTextChanged="drpIsCheck_SelectedIndexChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label>Department</label>
                                <asp:DropDownList ID="drpDept" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpIsCheck_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>IsSales</label>
                                <asp:DropDownList ID="drpIsCheck" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpIsCheck_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="All"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
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
                                <table id="ExportTbl" class="table table-bordered display table-striped">
                                    <thead>
                                        <tr>
                                            <th rowspan="2">Sr. No.</th>
                                            <th rowspan="2">Date<br />
                                                Time</th>
                                            <th rowspan="2">Employee</th>
                                            <th rowspan="2">Secondary Sale Total</th>
                                            <th rowspan="2">Target Visits</th>
                                            <th rowspan="2">Target Amount</th>
                                            <th rowspan="2">No of Visits</th>
                                            <th colspan="3" style="text-align: center;">Primary</th>
                                            <th rowspan="2">View</th>
                                        </tr>
                                        <tr>
                                            <th>Party</th>
                                            <th>Station</th>
                                            <th>Mobile No</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>

                                                    <td style="text-align: left;"><%#Eval("CheckDate") %><br />
                                                        <%#Eval("CheckTime") %></td>
                                                    <td style="text-align: left;"><%#Eval("Employees") %></td>
                                                    <td style="text-align: left;"><%#Eval("TotalSale") %></td>
                                                    <td style="text-align: left;"><%#Eval("TargetVisit") %></td>
                                                    <td style="text-align: left;"><%#Eval("TargetAmount") %></td>
                                                    <td style="text-align: left;"><%#Eval("Visited") %></td>
                                                    <td style="text-align: left;"><%#Eval("PrimaryParty") %></td>
                                                    <td style="text-align: left;"><%#Eval("PrimaryStation") %></td>
                                                    <td style="text-align: left;"><%#Eval("MobileNo") %></td>
                                                    <td>
                                                        <a href="SalesItemReport.aspx?id=<%#Eval("ID") %>" style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-info  rolese" aria-label="View"><i class="fa fa-eye"></i></a>
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
                url: 'SecondarySalesReport.aspx/ControlAccess',
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

