﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="UserAssignReport.aspx.cs" Inherits="Admin_UserAssignReport" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Check-In Out Report(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>User Assign Report</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/UserAssignReport.aspx" class="active">User Assign Report </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">

                            <div class="col-md-3">
                                <label>User</label>
                                <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <%--<div class="col-md-2">
                                <label>Date From</label>
                                <asp:TextBox ID="dpFrom" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>Date To</label>
                                <asp:TextBox ID="dpTo" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>--%>


                            <div class="col-md-1" style="padding-top: 3px;">
                                <div class="clearfix">&nbsp;</div>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
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
                                            <th style="text-align: left;">Sr. No.</th>
                                            <th style="text-align: center;" class="isLoginVisible">IsLogin CRM</th>
                                            <th style="text-align: left;">User Name</th>
                                            <th>Mobile</th>
                                            <th style="text-align: center;" class="isAssVisible">Assign</th>
                                            <%--<th style="text-align: center;">Reset Password</th>--%>

                                            <%--<th style="text-align: left;" rowspan="2">WhatsApp No</th>--%>
                                            <%--<th>--%>
                                            <%--<label id="lblAction">Action</label></th>--%>
                                        </tr>

                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server" OnItemDataBound="rep_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                        <asp:HiddenField ID="hddUserType" Value='<%#Eval("UserType") %>' runat="server" />
                                                        <asp:HiddenField ID="hddUid" Value='<%#Eval("id") %>' runat="server" />
                                                    </td>
                                                    <td style="text-align: center;" class="isLoginVisible">
                                                        <asp:CheckBox ID="IsChkLogin" runat="server" Checked='<%# Convert.ToBoolean(Eval("Deactivate"))? false:true %>' AutoPostBack="true" OnCheckedChanged="IsChkLogin_CheckedChanged" /></td>
                                                    <td style="text-align: left;"><%#Eval("Name") %></td>

                                                    <td style="text-align: left;"><%#Eval("MobileNo") %></td>
                                                    <td style="text-align: center;" class="isAssVisible">
                                                        <asp:HyperLink ID="lnkAssbtn" runat="server" NavigateUrl='<%# "AddUserRoles.aspx?id=" + (string)Eval("Id").ToString() %>' class="btn btn-small btn-primary">Assign Roles</asp:HyperLink>

                                                        <%--<asp:HyperLink ID="lnkHrbtn" runat="server" NavigateUrl='<%# "Payroll.aspx?id=" + (string)Eval("Id").ToString() %>' class="btn btn-small btn-primary">HR</asp:HyperLink>--%>
                                                    </td>
                                                    <%--<td style="text-align: center;" class="isAssVisible">
                                                        <a href="ResetPassword.aspx" class="btn btn-small btn-success"><i class="fa fa-key" aria-hidden="true"></i></a>
                                                    </td>--%>
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
            debugger
            $.ajax({
                url: 'UserAssignReport.aspx/ControlAccess',
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
                    var elements2 = document.getElementsByClassName("isAssVisible");
                    Array.prototype.forEach.call(elements2, function (element) {
                        element.style.display = myArray[4] == "False" ? "none" : "";
                    });
                    var elements3 = document.getElementsByClassName("isLoginVisible");
                    Array.prototype.forEach.call(elements3, function (element) {
                        element.style.display = myArray[5] == "False" ? "none" : "";
                    });
               
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

