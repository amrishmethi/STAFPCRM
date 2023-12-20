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
                                <label>Employee</label>
                                <asp:DropDownList ID="drpProjectManager" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <label>Month</label>
                                <asp:TextBox ID="mnth" runat="server" type="text" class="form-control MnthPicker" autocomplete="off" />
                            </div>
                            <div class="col-md-2 hidden">
                                <label class="control-label">Month<span style="color: #ff0000">*</span></label>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpMonth"
                                    ErrorMessage="Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
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
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-6">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Get Report"
                                    ValidationGroup="aa" OnClick="btnSubmit_Click" />
                                &nbsp;
                                &nbsp;
                                &nbsp;
                                <asp:Button ID="btnSalary" runat="server" CssClass="btn btn-primary" Text="Salary Generate"
                                    ValidationGroup="aa" OnClick="btnSalary_Click" />

                                &nbsp;
                                &nbsp;
                                &nbsp;
                                <asp:Button ID="btnSalarySlip" runat="server" CssClass="btn btn-primary" Text="Salary Slip"
                                    ValidationGroup="aa" OnClick="btnSalarySlip_Click" />

                                &nbsp;
  &nbsp;
  &nbsp;
  <asp:Button ID="btnPF" runat="server" CssClass="btn btn-primary" Text="PF REPORT"
      ValidationGroup="aa" OnClick="btnPF_Click" />
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
                                <table id="ExportTbl1" class="table table-bordered display table-striped">
                                    <thead>
                                        <%-- <tr>
                                            <th colspan="5"></th>
                                            <th style="text-align: center; background-color: green; color: white" colspan="8">Earning</th>
                                            <th></th>
                                            <th style="text-align: center; background-color: red; color: white" colspan="5">Deduction</th>
                                            <th colspan="4"></th>
                                        </tr>--%>
                                        <tr>
                                            <th style="text-align: left;">S No.</th>
                                            <th style="text-align: left;">
                                                <input type='checkbox' id='chkAll' runat='server' onclick='javascript: SelectAllCheckboxes(this);' /></th>
                                            <th style="text-align: left;">Month</th>
                                            <th style="text-align: left;">Department Name</th>
                                            <th style="text-align: left;">Employee Name</th>
                                            <th style="text-align: left;">PF AC/No</th>
                                            <th style="text-align: left;">ESIC AC/No</th>
                                            <th style="text-align: left;">Net Salary</th>
                                            <th style="text-align: left;">No Of Working Days</th>
                                            <th style="text-align: left;">OT Days</th>
                                            <th style="text-align: left;">Over Time</th>
                                            <th style="text-align: left;">Basic Salary</th>
                                            <th style="text-align: left;">House Rent Allowance</th>
                                            <th style="text-align: left;">Other Allowance</th>
                                            <%-- <th style="text-align: left;">Travel Allowance</th>
                                            <th style="text-align: left;">Daily Allowance Local</th>
                                            <th style="text-align: left;">Night Stay Allowance</th>
                                            <th style="text-align: left;">Extra Claim</th>--%>

                                            <th style="text-align: left;">Gross Salary</th>
                                            <th style="text-align: left;">Provident Fund</th>
                                            <th style="text-align: left;">ESIC</th>
                                            <th style="text-align: left;">TDS</th>
                                            <th style="text-align: left;">Salary Payble</th>
                                            <th style="text-align: left;">Loan EMI</th>
                                            <th style="text-align: left;">Advance</th>
                                            <th style="text-align: left;">Net Salary</th>
                                            <th style="text-align: left;">Employer PF</th>
                                            <th style="text-align: left;">Employer ESIC</th>
                                            <th style="text-align: left;">CTC</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server" OnItemCommand="rep_ItemCommand">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                        <asp:HiddenField ID="HddSalaryId" runat="server" Value='<%#Eval("Id") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chk" runat="server" Visible='<%#Eval("IsApprove").ToString()=="True"?false:true %>' />
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Style="padding: 1px 6px; font-size: 11px;" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" CommandName="Delete" CssClass="btn btn-small btn-danger" CommandArgument='<%#Eval("Id") %>' Visible='<%#Eval("IsApprove").ToString()=="True"?true:false%>'><i class="fa fa-trash-o"></i></asp:LinkButton>

                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("SALARYMONTH1") %></td>
                                                    <td style="text-align: left;"><%#Eval("Dept_Name") %></td>
                                                    <td style="text-align: left;"><%#Eval("Emp_Name") %></td>
                                                    <td style="text-align: left;"><%#Eval("PF_AcNo") %></td>
                                                    <td style="text-align: left;"><%#Eval("ESI_AcNO") %></td>
                                                    <td style="text-align: left;"><%#Eval("Net_Salary") %></td>
                                                    <td style="text-align: left;"><%#Eval("NOOFWORKINGDAY") %>/<%#Eval("TotalWorkingDay") %></td>
                                                    <td style="text-align: left;"><%#Eval("TotalOT") %></td>
                                                    <td style="text-align: left;"><%#Eval("OverTime") %></td>
                                                    <td style="text-align: left;"><a href='<%# "ShowPopup.aspx?EMP_ID="+Eval("EMP_ID") %>' runat="server" class="abc1"><%#Eval("BASIC_SALARYVALUE") %></a></td>
                                                    <td style="text-align: left;"><%#Eval("HRAVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("OAVALUE") %></td>
                                                    <%-- <td style="text-align: left;"><%#Eval("CAVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("DAL") %></td>
                                                    <td style="text-align: left;"><%#Eval("NSA") %></td>
                                                    <td style="text-align: left;"><%#Eval("Other") %></td>--%>

                                                    <td style="text-align: left;"><%#Eval("GrossSalary") %></td>
                                                    <td style="text-align: left;"><%#Eval("PF_EMPLOYEEVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("ESIC_EMPLOYEEVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("TDSVALUE") %></td>
                                                    <td style="text-align: left;"><a href='<%#Eval("_URL") %>' runat="server" target="_blank"><%#Eval("SALARYPAYBLE") %></a></td>
                                                    <td style="text-align: left;"><%#Eval("LOANAMOUNT") %></td>
                                                    <td style="text-align: left;"><%#Eval("Advance") %></td>
                                                    <td style="text-align: left;"><%#Eval("NETSALARY") %></td>
                                                    <td style="text-align: left;"><%#Eval("PF_EMPLOYERVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("ESIC_EMPLOYERVALUE") %></td>
                                                    <td style="text-align: left;"><%#Eval("CTC") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <th colspan="3"><%#Eval("Emp_Name") %></th>
                                                    <th colspan="2"></th>
                                                    <th style="text-align: left;"><%#Eval("Net_Salary") %></th>
                                                    <th style="text-align: left;" colspan="1"></th>
                                                    <th style="text-align: left;"><%#Eval("OverTime") %></th>
                                                    <th style="text-align: left;"><%#Eval("BASIC_SALARYVALUE") %></th>
                                                    <th style="text-align: left;"><%#Eval("HRAVALUE") %></th>
                                                    <th style="text-align: left;"><%#Eval("OAVALUE") %></th>
                                                    <%--  <th style="text-align: left;"><%#Eval("CAVALUE") %></th>
                                                    <th style="text-align: left;"><%#Eval("DAL") %></th>
                                                    <th style="text-align: left;"><%#Eval("NSA") %></th>
                                                    <th style="text-align: left;"><%#Eval("Other") %></th>--%>

                                                    <th style="text-align: left;"><%#Eval("GrossSalary") %></th>
                                                    <th style="text-align: left;"><%#Eval("PF_EMPLOYEEVALUE") %></th>
                                                    <th style="text-align: left;"><%#Eval("ESIC_EMPLOYEEVALUE") %></th>
                                                    <th style="text-align: left;"><%#Eval("LOANAMOUNT") %></th>
                                                    <th style="text-align: left;"><%#Eval("Advance") %></th>
                                                    <th style="text-align: left;"><%#Eval("TDSVALUE") %></th>
                                                    <th style="text-align: left;"><%#Eval("NETSALARY") %></th>
                                                    <th style="text-align: left;"><%#Eval("PF_EMPLOYERVALUE") %></th>
                                                    <th style="text-align: left;"><%#Eval("ESIC_EMPLOYERVALUE") %></th>
                                                    <th style="text-align: left;"><%#Eval("CTC") %></th>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
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
    <link href="../css/CalenderView.css" rel="stylesheet" />
    <script src="js/jquery-ui.js"></script>
    <link href="js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ui-datepicker-calendar {
            /*display: tr;*/
        }
    </style>
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

    <script type="text/javascript">
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox

            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
                spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
                    elm[i].id != theBox.id) {
                    //elm[i].click();

                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;

                }
        }
    </script>
</asp:Content>

