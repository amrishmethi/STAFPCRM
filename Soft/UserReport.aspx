<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="UserReport.aspx.cs" Inherits="Admin_UserReport" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Employees Info(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Employee Info</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/UserReport.aspx" class="active">Employees Info </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <asp:ScriptManager ID="scriptMgr" runat="server"></asp:ScriptManager>
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">

                            <%--<asp:UpdatePanel ID="updt1" runat="server">
                                <ContentTemplate>--%>
                            <div class="col-md-3">
                                <label>Department</label>
                                <asp:DropDownList ID="drpDept" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpDept_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Designation</label>
                                <asp:DropDownList ID="drpDesg" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDesg_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    Employee Status
                                </label>
                                <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="Active" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Non-Active" Value="Non-Active"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Employee</label>
                                <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <%--</ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="drpDept" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                            <div class="col-md-2">
                                <label>Mobile No</label>
                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control">
                                </asp:TextBox>
                            </div>

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
                                            <th style="text-align: left;">Employee Name</th>
                                            <th style="text-align: left;">Department</th>
                                            <th style="text-align: left;">Mobile No</th>
                                            <th style="text-align: left;">Salary Account</th>
                                            <th style="text-align: left;">Loan Account</th>
                                            <th style="text-align: left;">Travel Account</th>
                                            <%--     <th style="text-align: left;">CUG Mobile No</th>
                                            <th style="text-align: left;">Official Email</th>--%>
                                            <%-- <th style="text-align: left;">Password</th>--%>
                                            <%--<th style="text-align: left;">Designation</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("Name") %></td>
                                                    <td style="text-align: left;"><%#Eval("Department") %></td>
                                                    <td style="text-align: left;"><%#Eval("MobileNo") %></td>
                                                    <td style="text-align: left;"><%#Eval("Salary_Account") %></td>
                                                    <td style="text-align: left;"><%#Eval("Loan_Account") %></td>
                                                    <td style="text-align: left;"><%#Eval("Travel_Account") %></td>
                                                    <%--<td style="text-align: left;"><%#Eval("CUG_MobileNO") %></td>--%>
                                                    <%--<%--<td style="text-align: left;"><%#Eval("O_mail") %></td>--%> 
                                                    <%--<td style="text-align: left;"><%#Eval("Password") %></td>--%>
                                                    <%--<td style="text-align: left;"><%#Eval("Designation") %></td>--%>
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
                url: 'UserReport.aspx/ControlAccess',
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

