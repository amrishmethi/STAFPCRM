<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="EMPLOYEESALES.aspx.cs" Inherits="Soft_EMPLOYEESALES" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Employee Wise Sale(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="scpt1" runat="server"></asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>Employee Wise Sale</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/EMPLOYEESALES.aspx" class="active">Employee Wise Sale</a></li>
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
                                <label>Year</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="mnth"
                                    ErrorMessage="Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:TextBox ID="mnth" runat="server" type="text" class="form-control YearPicker" autocomplete="off" />
                            </div>
                            <div class="col-md-2">
                                <label>Report Type </label>
                                <asp:DropDownList ID="drpReportType" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="Bag Wise" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Weight Wise" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Amount Wise" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label>Group</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpGrp"
                                    ErrorMessage="Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:ListBox ID="drpGrp" runat="server" CssClass="form-control select2" SelectionMode="Multiple"></asp:ListBox>
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
                                <table id="ExportTbl" class="table table-bordered display table-striped">
                                    <thead>
                                        <tr>
                                            <th>Sr. No.</th>
                                            <th>Employee</th>
                                            <th>HeadQuarter</th>
                                            <th>Apr</th>
                                            <th>May</th>
                                            <th>Jun</th>
                                            <th>Jul</th>
                                            <th>Aug</th>
                                            <th>Sep</th>
                                            <th>Oct</th>
                                            <th>Nov</th>
                                            <th>Dec</th>
                                            <th>Jan</th>
                                            <th>Feb</th>
                                            <th>Mar</th>
                                            <th>Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repDetail" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex+1%></td>
                                                    <td><%#Eval("EMP_NAME") %></td>
                                                    <td><%#Eval("HeadQtr") %></td>
                                                    <td><%#Eval("Apr") %></td>
                                                    <td><%#Eval("May") %></td>
                                                    <td><%#Eval("Jun") %></td>
                                                    <td><%#Eval("Jul") %></td>
                                                    <td><%#Eval("Aug") %></td>
                                                    <td><%#Eval("Sep") %></td>
                                                    <td><%#Eval("Oct") %></td>
                                                    <td><%#Eval("Nov") %></td>
                                                    <td><%#Eval("Dec") %></td>
                                                    <td><%#Eval("Jan") %></td>
                                                    <td><%#Eval("Feb") %></td>
                                                    <td><%#Eval("Mar") %></td>
                                                    <td><%#Eval("Total") %></td>
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
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

