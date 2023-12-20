﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="MonthlyAttandance.aspx.cs" Inherits="Soft_MonthlyAttandance" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        div > p {
            background: white;
        }
    </style>
    <title>Monthly Attandance Calander</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Monthly Attandance Calander
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/MonthlyAttandance.aspx" class="active">Monthly Attandance Calander</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-2">
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
                            <div class="col-md-2">
                                <label>
                                    Employee 
                                </label>
                                <asp:DropDownList ID="drpEmployee" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <label>Month</label>
                                <asp:TextBox ID="mnth" runat="server" type="text" class="form-control MnthPicker" autocomplete="off" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="mnth" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    Attendance Type
                                </label>
                                <asp:DropDownList ID="drpAttendance" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="Day" Value="Day" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Night" Value="Night"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="Search" runat="server" ValidationGroup="Save" CssClass="btn btn-success" Text="Search" OnClick="Search_Click" />
                            </div>

                        </div>
                    </div>
                </div>
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="widget-content">
                            <div class="table-responsive">

                                <table id="ExportTbl" class="table table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Sr.<br />
                                                No.
                                            </th>
                                            <th>Employee<br />
                                                Name
                                            </th>
                                            <th>01
                                            </th>
                                            <th>02
                                            </th>
                                            <th>03
                                            </th>
                                            <th>04
                                            </th>
                                            <th>05
                                            </th>
                                            <th>06
                                            </th>
                                            <th>07
                                            </th>
                                            <th>08
                                            </th>
                                            <th>09
                                            </th>
                                            <th>10
                                            </th>
                                            <th>11
                                            </th>
                                            <th>12
                                            </th>
                                            <th>13
                                            </th>
                                            <th>14
                                            </th>
                                            <th>15
                                            </th>
                                            <th>16
                                            </th>
                                            <th>17
                                            </th>
                                            <th>18
                                            </th>
                                            <th>19
                                            </th>
                                            <th>20
                                            </th>
                                            <th>21
                                            </th>
                                            <th>22
                                            </th>
                                            <th>23
                                            </th>
                                            <th>24
                                            </th>
                                            <th>25
                                            </th>
                                            <th>26
                                            </th>
                                            <th>27
                                            </th>
                                            <th>28
                                            </th>
                                            <th>29
                                            </th>
                                            <th>30
                                            </th>
                                            <th>31
                                            </th>
                                            <th style="text-align: left;">Month Days </th>
                                            <th style="text-align: left;">Basic Attendance</th>
                                            <th style="text-align: left;">Sunday Off</th>
                                            <th style="text-align: left;">Holiday Off</th>
                                            <th style="text-align: left;">PL</th>
                                            <th style="text-align: left;">Basic Salary</th>
                                            <th style="text-align: left;">Sunday Work</th>
                                            <th style="text-align: left;">Night OT</th>
                                            <th style="text-align: left;">Holiday Work</th>
                                            <th style="text-align: left;">Over Time</th>
                                            <th style="text-align: left;">Leave</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptUserViewAttendance" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <asp:Label ID="lblEmpId" runat="server" Visible="false" Text='<%#Eval("EmpId")%>'></asp:Label>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td>
                                                       <%#Eval("EmployeeName")%>
                                                    </td>
                                                    <td style='<%# Eval("Dat1")=="S"||Eval("Dat1")=="HD"||Eval("Dat1")=="PL"?"background-color: Orange; color:white": Eval("Dat1")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                         <%#Eval("Dat1") %> 
                                                    </td>
                                                    <td style='<%# Eval("Dat2")=="S"||Eval("Dat2")=="HD"||Eval("Dat2")=="PL"?"background-color: Orange; color:white": Eval("Dat2")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                         <%#Eval("Dat2") %> 
                                                    </td>
                                                    <td style='<%# Eval("Dat3")=="S"||Eval("Dat3")=="HD"||Eval("Dat3")=="PL"?"background-color: Orange; color:white": Eval("Dat3")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                       <%#Eval("Dat3") %> 
                                                    </td>
                                                    <td style='<%# Eval("Dat4")=="S"||Eval("Dat4")=="HD"||Eval("Dat4")=="PL"?"background-color: Orange; color:white": Eval("Dat4")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                         <%#Eval("Dat4") %> 
                                                    </td>
                                                    <td style='<%# Eval("Dat5")=="S"||Eval("Dat5")=="HD"||Eval("Dat5")=="PL"?"background-color: Orange; color:white": Eval("Dat5")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat5") %>                                                            
                                                    </td>
                                                    <td style='<%# Eval("Dat6")=="S"||Eval("Dat6")=="HD"||Eval("Dat6")=="PL"?"background-color: Orange; color:white": Eval("Dat6")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat6") %>                                                            
                                                    </td>
                                                    <td style='<%# Eval("Dat7")=="S"||Eval("Dat7")=="HD"||Eval("Dat7")=="PL"?"background-color: Orange; color:white": Eval("Dat7")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'><%#Eval("Dat7") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat8")=="S"||Eval("Dat8")=="HD"||Eval("Dat8")=="PL"?"background-color: Orange; color:white": Eval("Dat8")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat8") %>  
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat9")=="S"||Eval("Dat9")=="HD"||Eval("Dat9")=="PL"?"background-color: Orange; color:white": Eval("Dat9")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat9") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat10")=="S"||Eval("Dat10")=="HD"||Eval("Dat10")=="PL"?"background-color: Orange; color:white": Eval("Dat10")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat10") %>  
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat11")=="S"||Eval("Dat11")=="HD"||Eval("Dat11")=="PL"?"background-color: Orange; color:white": Eval("Dat11")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat11") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat12")=="S"||Eval("Dat12")=="HD"||Eval("Dat12")=="PL"?"background-color: Orange; color:white": Eval("Dat12")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat12") %>  
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat13")=="S"||Eval("Dat13")=="HD"||Eval("Dat13")=="PL"?"background-color: Orange; color:white": Eval("Dat13")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat13") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat14")=="S" ||Eval("Dat14")=="HD"||Eval("Dat14")=="PL"  ?"background-color: Orange; color:white": Eval("Dat14")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat14") %>  
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat15")=="S"||Eval("Dat15")=="HD"||Eval("Dat15")=="PL"?"background-color: Orange; color:white": Eval("Dat15")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat15") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat16")=="S"||Eval("Dat16")=="HD"||Eval("Dat16")=="PL"?"background-color: Orange; color:white": Eval("Dat16")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat16") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat17")=="S"||Eval("Dat17")=="HD"||Eval("Dat17")=="PL"?"background-color: Orange; color:white": Eval("Dat17")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat17") %>  
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat18")=="S"||Eval("Dat18")=="HD"||Eval("Dat18")=="PL"?"background-color: Orange; color:white": Eval("Dat18")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat18") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat19")=="S"||Eval("Dat19")=="HD"||Eval("Dat19")=="PL"?"background-color: Orange; color:white": Eval("Dat19")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat19") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat20")=="S"||Eval("Dat20")=="HD"||Eval("Dat20")=="PL"?"background-color: Orange; color:white": Eval("Dat20")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat20") %>                                                            
                                                    </td>
                                                    <td style='<%# Eval("Dat21")=="S"||Eval("Dat21")=="HD"||Eval("Dat21")=="PL"?"background-color: Orange; color:white": Eval("Dat21")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat21") %>
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat22")=="S"||Eval("Dat22")=="HD"||Eval("Dat22")=="PL"?"background-color: Orange; color:white": Eval("Dat22")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat22") %>  
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat23")=="S"||Eval("Dat23")=="HD"||Eval("Dat23")=="PL"?"background-color: Orange; color:white": Eval("Dat23")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat23") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat24")=="S"||Eval("Dat24")=="HD"||Eval("Dat24")=="PL"?"background-color: Orange; color:white": Eval("Dat24")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat24") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat25")=="S"||Eval("Dat25")=="HD"||Eval("Dat25")=="PL"?"background-color: Orange; color:white": Eval("Dat25")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat25") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat26")=="S"||Eval("Dat26")=="HD"||Eval("Dat26")=="PL"?"background-color: Orange; color:white": Eval("Dat26")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat26") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat27")=="S"||Eval("Dat27")=="HD"||Eval("Dat27")=="PL"?"background-color: Orange; color:white": Eval("Dat27")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat27") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat28")=="S"||Eval("Dat28")=="HD"||Eval("Dat28")=="PL"?"background-color: Orange; color:white": Eval("Dat28")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat28") %>  
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat29")=="S"||Eval("Dat29")=="HD"||Eval("Dat29")=="PL"?"background-color: Orange; color:white": Eval("Dat29")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat29") %>
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat30")=="S"||Eval("Dat30")=="HD"||Eval("Dat30")=="PL"?"background-color: Orange; color:white": Eval("Dat30")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat30") %> 
                                                           
                                                    </td>
                                                    <td style='<%# Eval("Dat31")=="S"||Eval("Dat31")=="HD"||Eval("Dat31")=="PL"?"background-color: Orange; color:white": Eval("Dat31")=="L"?"background-color: red; color:white" : "background-color: Green; color:white"%>'>
                                                        <%#Eval("Dat31") %> 
                                                           
                                                    </td>

                                                    <td style="text-align: left;"><%#Eval("TotalDays") %></td>
                                                    <td style="text-align: left;"><%#Eval("Attandance") %></td>
                                                    <td style="text-align: left;"><%#Eval("SundayOFF") %></td>
                                                    <td style="text-align: left;"><%#Eval("HolidayOff") %></td>
                                                    <td style="text-align: left;"><%#Eval("PL") %></td>
                                                    <td style="text-align: left; background-color: Green; color: white"><%#Eval("NoOfWorkingDays") %></td>
                                                    <td style="text-align: left;"><%#Eval("SundayWork") %></td>
                                                    <td style="text-align: left;"><%#Eval("NIghtOT") %></td>
                                                    <td style="text-align: left;"><%#Eval("HolidayWork") %></td>
                                                    <td style="text-align: left; background-color: Green; color: white"><%#Eval("TotalOT") %></td>
                                                    <td style="text-align: left;"><%#Eval("Leave") %></td>
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
        <div class="col-md-12">
            <h3 style="color: Red;">Note :   &nbsp;&nbsp;&nbsp;&nbsp;  </h3>
            <h4 style="color: black;">Basic Salary :&nbsp;&nbsp;   Attendance&nbsp;+&nbsp;Sunday Off &nbsp;+&nbsp;Holiday Off&nbsp;+&nbsp; PL</h4>
            <h4 style="color: black;">Over Time :&nbsp;&nbsp;   Sunday Work &nbsp;+&nbsp;Holiday Work &nbsp;+&nbsp;Night Attandance</h4>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
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
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

