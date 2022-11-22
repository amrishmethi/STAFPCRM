<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SalesItemReport.aspx.cs" Inherits="Soft_SalesItem_Report" %>

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
            <li><a href="/Soft/SalesItemReport.aspx" class="active">Secondary Sales Report</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="clearfix">&nbsp;</div>

                            <div class="col-md-2">
                                <label>Employee</label>
                                <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Station</label>
                                <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Primary Party</label>
                                <asp:DropDownList ID="drpParty" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
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

                            <div class="col-md-1" style="padding-top: 3px;">
                                <div class="clearfix">&nbsp;</div>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                            <div class="clearfix">&nbsp;</div>
                        </div>

                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-body">
                        <div class="widget-content nopadding" style="overflow: auto;">
                            <div class="table-responsive">
                                <table id="ExportTbl" class="table table-bordered display" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;" rowspan="2">Sr. No.</th>
                                            <th style="text-align: left;" rowspan="2">Employee</th>
                                            <th style="text-align: center;" colspan="6">Primary</th>
                                            <th style="text-align: center;" colspan="5">Secondary</th>
                                        </tr>
                                        <tr>
                                            <th>Date</th>
                                            <th>Time</th>
                                            <th>Party</th>
                                            <th>Station</th>
                                            <th>Mobile No</th>
                                            <th>WhatsApp No</th>
                                            <th>Date</th>
                                            <th>Time</th>
                                            <th>Party</th>
                                            <th>Station</th>
                                            <th>MobileNo</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server" OnItemDataBound="rep_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="gradeA" style="background-color: lightslategrey; color: white;">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                        <asp:HiddenField ID="hddid" runat="server" Value='<%#Eval("ChkOutID") %>' />
                                                    </td>
                                                    <td><%#Eval("UserName") %></td>
                                                    <td><%#Eval("CheckInDate") %></td>
                                                    <td><%#Eval("CheckInTime") %></td>
                                                    <td><%#Eval("PRIMARYPARTY") %></td>
                                                    <td><%#Eval("PRIMARYSTATION") %></td>
                                                    <td><%#Eval("MobileNoIN") %></td>
                                                    <td><%#Eval("WHATSAPPNO") %></td>
                                                    <td><%#Eval("CheckOutDate") %></td>
                                                    <td><%#Eval("CheckOutTime") %></td>
                                                    <td><%#Eval("SecondaryParty") %></td>
                                                    <td><%#Eval("SecondaryStation") %></td>
                                                    <td><%#Eval("MobileNoIN") %></td>

                                                </tr>
                                                <tr>
                                                    <td colspan="13" style="background-color: lightgray;">
                                                        <table class="table table-bordered display table-striped">
                                                            <thead>
                                                                <tr>
                                                                    <th>Sr. No.</th>
                                                                    <th>Group</th>
                                                                    <th>Item</th>
                                                                    <th>Qty</th>
                                                                    <th>Rate</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rep1" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr class="gradeA">
                                                                            <td>
                                                                                <%#Container.ItemIndex+1 %>
                                                                            </td>
                                                                            <td style="text-align: left;"><%#Eval("GroupName") %></td>
                                                                            <td style="text-align: left;"><%#Eval("ITName") %></td>
                                                                            <td style="text-align: left;"><%#Eval("OrdQty") %></td>
                                                                            <td style="text-align: left;"><%#Eval("OrdStpRate") %></td>
                                                                        </tr>
                                                                    </ItemTemplate>

                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
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
                url: 'SalesItemReport.aspx/ControlAccess',
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


