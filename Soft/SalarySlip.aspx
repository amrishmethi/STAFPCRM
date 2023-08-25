<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalarySlip.aspx.cs" Inherits="Soft_SalarySlip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Salary Slip</title>
    <style>
        .page {
            page-break-after: always;
        }
    </style>
</head>
<body onload="window.print()">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <asp:Repeater ID="repData" runat="server">
            <ItemTemplate>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <table width="750" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="990" align="center">
                                    <table width="100%" border="1px" bordercolor="#202020" cellspacing="0" cellpadding="5" style="border: 1px solid #202020; border-collapse: collapse;">
                                        <tr>
                                            <td style="border-right: hidden">
                                                <img src="../../img/logo.jpg" height="80px">
                                            </td>
                                            <td colspan="3" align="center">
                                                <h2 style="margin-bottom: 0px;">Shree Tadkeshwar Agro Food Product </h2>
                                                H-1-37-A, Sarna Doongar Industrial Area, Jhotwara Extension,
                                                <br />
                                                Jaipur - 302012 Rajasthan, India
                                                <br />
                                                (Mob) : + 91 - 97857 - 57775, (Tel) : 0141 - 3540250
                                                <br />
                                                (Email) : hr@tadkeshwarfoods.com, (url) : www.tadkeshwarfoods.com
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4"><strong>Salary Slip for the Month of:-
                                         <%#Eval("SM") %>   </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Date of Joining </td>
                                            <td><%#Eval("DOJ") %></td>
                                            <td>PF No</td>
                                            <td><%#Eval("PF_AcNo") %></td>
                                        </tr>
                                        <tr>
                                            <td>Employee Name</td>
                                            <td><%#Eval("EMP_NAME") %> </td>
                                            <td>Department</td>
                                            <td><%#Eval("Dept_Name") %></td>
                                        </tr>
                                        <tr>
                                            <td>Employee Code</td>
                                            <td><%#Eval("Emp_Code") %></td>
                                            <td>Designation</td>
                                            <td><%#Eval("Desg_Name") %></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center"><strong>Pay &amp; Allowances</strong></td>
                                            <td align="center"><strong>Amounts</strong></td>
                                            <td align="center"><strong>Deductions</strong></td>
                                            <td align="center"><strong>Amounts</strong></td>
                                        </tr>
                                        <tr>
                                            <td>Basic Salary</td>
                                            <td><%#Eval("BASIC_SALARYVALUE") %></td>
                                            <td>PF- Counter</td>
                                            <td><%#Eval("PF_EMPLOYEEVALUE") %></td>
                                        </tr>
                                        <tr>
                                            <td>HRA</td>
                                            <td><%#Eval("HRAVALUE") %></td>
                                            <td>ESIC</td>
                                            <td><%#Eval("ESIC_EMPLOYEEVALUE") %></td>
                                        </tr>
                                        <tr>
                                            <td>Other Allowance</td>
                                            <td><%#Eval("OAVALUE") %></td>
                                            <td>Loan EMI</td>
                                            <td><%#Eval("LOANAMOUNT") %></td>
                                        </tr>
                                        <tr>
                                            <td>Over Time</td>
                                            <td><%#Eval("OverTime") %></td>
                                            <td>Salary Advance</td>
                                            <td><%#Eval("Advance") %></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>TDS if any</td>
                                            <td><%#Eval("TDSVALUE") %></td>
                                        </tr>
                                        <tr>
                                            <td>Total Gross Salary</td>
                                            <td><%#Eval("GrossSalary") %></td>
                                            <td>Total Deductions </td>
                                            <td><%#Eval("DEDUCTION") %></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center"><strong>Net Salary Pay</strong> <%#Eval("NETSALARY") %></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <br />
                                                <br />
                                                <strong>HR Manager<strong>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;              
                                       
                                            <strong style="float: right">Employee Name</strong></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">&nbsp;</td>
                            </tr>
                        </table>
                        <table width="750" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="center" colspan="4"><strong>Attendance Details </strong>
                                </td>
                            </tr>
                            <tr>
                                <td width="990" align="center">
                                    <table width="100%" border="1px" bordercolor="#202020" cellspacing="0" cellpadding="5" style="border: 1px solid #202020; border-collapse: collapse;">
                                        <tr>
                                            <th style="text-align: left;">Attendance</th>
                                            <th style="text-align: left;">Sunday Off</th>
                                            <th style="text-align: left;">Holiday Off</th>
                                            <th style="text-align: left;">Sunday Work</th>
                                            <th style="text-align: left;">Night OT</th>
                                            <th style="text-align: left;">Holiday Work</th>
                                            <th style="text-align: left;">PL</th>
                                            <th style="text-align: left;">NO Of Working Days</th>
                                            <th style="text-align: left;">Leave</th>
                                        </tr>
                                        <tbody>
                                            <tr class="gradeA">
                                                <td style="text-align: left; border: 1px solid #202020;"><%#Eval("NOOFATTANDANCE") %></td>
                                                <td style="text-align: left;"><%#Eval("ALLOWSUNDAY") %></td>
                                                <td style="text-align: left;"><%#Eval("NoOfHoliday") %></td>
                                                <td style="text-align: left;"><%#Eval("NOOFSUNDAYWork") %></td>
                                                <td style="text-align: left;"><%#Eval("NIghtOT") %></td>
                                                <td style="text-align: left;"><%#Eval("NOOFHolidayWork") %></td>
                                                <td style="text-align: left;"><%#Eval("PL1") %></td>
                                                <td style="text-align: left;"><%#Eval("TOTALWORKINGDAY") %></td>
                                                <td style="text-align: left;"><%#Eval("TOTALLEAVE") %></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td colspan="4" align="center">Computer Generated Pay Slip</td>
                </tr>
                <tr class="page"></tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</body>
</html>
