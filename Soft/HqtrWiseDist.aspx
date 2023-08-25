<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="HqtrWiseDist.aspx.cs" Inherits="Admin_HqtrWiseDist" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>HeadQuarter Wise District(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="scpt1" runat="server"></asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>HeadQuarter Wise District</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/HqtrWiseDist.aspx" class="active">HeadQuarter Wise District</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-2">
                                <label>Status</label>
                                <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpDept_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="Active" Text="Active" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Non-Active" Text="Non-Active"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Employee</label>
                                <%--<asp:RadioButton ID="RadioButton1" runat="server" Text="&nbsp;Employee" GroupName="Filter" Checked="true" OnCheckedChanged="RadioButton1_CheckedChanged" AutoPostBack="true" />--%>
                                <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>HeadQuarter</label>
                                <%--<asp:RadioButton ID="RadioButton2" runat="server" Text="&nbsp;HeadQuarter" GroupName="Filter" OnCheckedChanged="RadioButton1_CheckedChanged" AutoPostBack="true" />--%>
                                <asp:DropDownList ID="drpheadQtr" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpheadQtr_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1" style="padding-top: 3px;">
                                <div class="clearfix">&nbsp;</div>
                                <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-success" Text="Get Data" OnClick="btnsearch_Click" />
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
                                                <th>Employee</th>
                                                <th>HeadQuarter</th>
                                                <th>District</th>
                                                <th>Station</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rep" runat="server">
                                                <ItemTemplate>
                                                    <tr class="gradeA">
                                                        <td>
                                                            <%#Container.ItemIndex+1 %>
                                                        </td>
                                                        <td style="text-align: left;"><%#Eval("NAME") %></td>
                                                        <td style="text-align: left;"><%#Eval("HeadQtr") %></td>
                                                        <td style="text-align: left;"><%#Eval("District") %></td>
                                                        <td style="text-align: left;"><%#Eval("Station") %></td>
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
