<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="MonthlySallaryRep.aspx.cs" Inherits="Soft_MonthlySallaryRep" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Monthly Salary Report(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Monthly Salary Sheet
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/MonthlySallaryRep.aspx" class="active">Monthly Salary Report </a></li>
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
                                <asp:DropDownList ID="drpDesignation" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Employee</label>
                                <asp:DropDownList ID="drpProjectManager" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <label class="control-label">Month<span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpMonth"
                                    ErrorMessage="Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label">PF/ ESIC </label>
                                <asp:DropDownList ID="drpPf" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
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
                                            <th style="text-align: left;">S
                                                <br />
                                                No.</th>
                                            <th style="text-align: left;">Employee
                                                <br />
                                                Name</th>
                                            <th style="text-align: left;">Basic Salary</th>
                                            <th style="text-align: left;">Provident Fund</th>
                                            <th style="text-align: left;">Employer
                                                <br />
                                                PF</th>
                                            <th style="text-align: left;">ESIC</th>
                                            <th style="text-align: left;">Employer
                                                <br />
                                                ESIC</th>
                                            <th style="text-align: left;">House Rent Allowance</th>
                                            <th style="text-align: left;">Other Allowance</th>
                                            <th style="text-align: left;">Travel Allowance</th>
                                            <th style="text-align: left;">Daily Allowance Local</th>
                                            <th style="text-align: left;">Daily Allowance Ex</th>
                                            <th style="text-align: left;">Night Stay Allowance</th>
                                            <th style="text-align: left;">TDS</th>
                                            <th style="text-align: left;">Other Deduction</th>
                                            <th style="text-align: left;">Leave Deduction</th>
                                            <th style="text-align: left;">Loan</th>
                                            <th style="text-align: left;">Advance</th>
                                            <th style="text-align: left;">Over Time</th>
                                            <th style="text-align: left;">Gross Salary </th>
                                            <th style="text-align: left;">Net<br />
                                                Salary</th>
                                            <th style="text-align: left;">Total Days</th>
                                            <th style="text-align: left;">Sunday Off</th>
                                            <th style="text-align: left;">Sunday Work</th>
                                            <th style="text-align: left;">Attandance</th>
                                            <th style="text-align: left;">Leave</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("Emp_Name") %></td>
                                                    <td style="text-align: left;"><%#Eval("BASIC_SALARYVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("PF_EMPLOYEEVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("PF_EMPLOYERVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("ESIC_EMPLOYEEVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("ESIC_EMPLOYERVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("HRAVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("OAVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("CAVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("DAL1") %></td>
                                                    <td style="text-align: left;"><%#Eval("DAEX1") %></td>
                                                    <td style="text-align: left;"><%#Eval("NSA") %></td>
                                                    <td style="text-align: left;"><%#Eval("TDSVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("ODVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("LeaveDeduction") %></td>
                                                    <td style="text-align: left;"><%#Eval("LOANAMOUNT") %></td>
                                                    <td style="text-align: left;"><%#Eval("Advance") %></td>
                                                    <td style="text-align: left;"><%#Eval("OverTime") %></td>
                                                    <td style="text-align: left;"><%#Eval("Net_Salary") %></td>
                                                    <td style="text-align: left;"><%#Eval("NETSALARY") %></td>
                                                    <td style="text-align: left;"><%#Eval("NOOFWORKINGDAY") %></td>
                                                    <td style="text-align: left;"><%#Eval("SundayOFF") %></td>
                                                    <td style="text-align: left;"><%#Eval("NOOFSUNDAYWork") %></td>
                                                    <td style="text-align: left;"><%#Eval("NOOFATTANDANCE") %></td>
                                                    <td style="text-align: left;"><%#Eval("TOTALLEAVE") %></td>
                                                </tr>
                                            </ItemTemplate>

                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
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
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

