<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="CTCReport.aspx.cs" Inherits="Soft_CTCReport" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>CTC Report(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="scpt1" runat="server"></asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>CTC Report(S/T)</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/orderreportsp.aspx" class="active">CTC Report(S/T)</a></li>
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
                            <div class="col-md-3">
                                <label>Employee <span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select" InitialValue="0"
                                    Font-Bod="true" ForeColor="Red" ControlToValidate="drpUser" ValidationGroup="aa"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <label>Month</label>
                                <asp:TextBox ID="mnth" runat="server" type="text" class="form-control MnthPicker" autocomplete="off" />
                            </div>
                            <div class="col-md-3">
                                <label>Group <span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Select" InitialValue=""
                                    Font-Bod="true" ForeColor="Red" ControlToValidate="drpGrp" ValidationGroup="aa"></asp:RequiredFieldValidator>
                                <asp:ListBox ID="drpGrp" runat="server" CssClass="form-control select2" SelectionMode="Multiple"></asp:ListBox>
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
                                            <th>Sr. No.</th>
                                            <th>Employee</th>
                                            <th>Rep Manager</th>
                                            <th>Month</th>
                                            <th>Secondary Sales Target Visit</th>
                                            <th>Secondary Sales Total Visit</th>
                                            <th>Secondary Sales Productive Visit </th>
                                            <th>Secondary Sales Amount</th>
                                            <th>Sale Without Tax</th>
                                            <th>Sale Target</th>
                                            <th>Sale Qty</th>
                                            <th>Balance</th>
                                            <th>Target %</th>
                                            <%--As Per HQ Party Group Wse--%>
                                            <th>Salary</th>
                                            <th>Expenses</th>
                                            <th>Total Expenses</th>
                                            <th>CTC %</th>
                                            <th>Create Dealer</th>
                                            <th>Client Meet</th>
                                            <th>Due Outstanding Amount</th>
                                            <th>Pending Order</th>
                                            <%--As Per Pending Order Report--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Emp_Name")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Rep_Manager")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# mnth.Text %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Target_Visit")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Total_Visit")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Productive_Visit")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Sales_Amount")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Sale")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("TargetQty")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("SALEQTY")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Balance")  %></td>

                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'>


                                                        <%# Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Eval("SALEQTY")) * 100) /Convert.ToDouble(Eval("TargetQty"))).ToString("0.00")  %>



                                                    </td>

                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Salary")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Expenses")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Total_Expenses")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("CTC")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Create_Dealer")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("Client_MEET")  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Math.Abs(Convert.ToDouble(Eval("DUEAMT")))  %></td>
                                                    <td style='<%# Eval("Emp_Name").ToString()=="Total" ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%#  Eval("PENDINGQTY") %> </td>
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
