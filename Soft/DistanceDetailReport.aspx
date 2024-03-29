﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="DistanceDetailReport.aspx.cs" Inherits="Soft_DistanceDetailReport" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Distance Detail Report(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Distance Detail Report</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/DistanceDetailReport.aspx" class="active">Distance Detail Report</a></li>
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
                                <label class="control-label">Employee<span style="color: #ff0000">*</span></label>
                                <asp:Label ID="lblerror" ForeColor="Red" runat="server"></asp:Label>
                                <asp:DropDownList ID="drpEmp" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <label class="control-label">Date From <span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateFrom"
                                    ErrorMessage="Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label">Date To <span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDateTo"
                                    ErrorMessage="Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">Report Type<span style="color: #ff0000">*</span></label>
                                <asp:DropDownList ID="drpReport" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="Summary" Value="Summary"></asp:ListItem>
                                    <asp:ListItem Text="Detail" Value="Detail"></asp:ListItem>
                                    <asp:ListItem Text="Employee Wise" Value="All"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" ValidationGroup="aa" Text="Get Report" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success" Text="Print Report"
                                    ValidationGroup="aa" OnClick="btnPrint_Click" />
                            </div>
                        </div>
                        <div class="clearfix">&nbsp;</div>
                        <div class="clearfix">&nbsp;</div>
                    </div>
                </div>

                <div class="box box-primary" id="detail" runat="server" visible="false">
                    <div class="box-body">
                        <div class="widget-content">
                            <div class="table-responsive">
                                <table id="ExportTbl" class="table table-bordered display table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">S No.</th>
                                            <th style="text-align: left;">Emp Name</th>
                                            <th style="text-align: left;">Station</th>
                                            <th style="text-align: left;">Travel Date</th>
                                            <th style="text-align: left;">Working</th>
                                            <th style="text-align: left;">Place</th>
                                            <th style="text-align: left;">Distance in KM</th>
                                            <th style="text-align: left;">Rate</th>
                                            <th style="text-align: left;">Amount</th>
                                            <th style="text-align: left;">Night Stay</th>
                                            <th style="text-align: left;">Daily Allowance</th>
                                            <th style="text-align: left;">Other</th>
                                            <th style="text-align: left;">Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;" ><%#Eval("EMP_NAME") %></td>
                                                    <td style="text-align: left;"><%#Eval("Station") %></td>
                                                    <td style="text-align: left;"><%#Eval("TravelDate") %></td>
                                                    <td style="text-align: left;"><%#Eval("EntryType") %></td>
                                                    <td style="text-align: left;"><%#Eval("Place") %></td>
                                                    <td style="text-align: left;"><%#Eval("Distance") %></td>
                                                    <td style="text-align: left;"><%#Eval("Rate") %></td>
                                                    <td style="text-align: left;"><%#Eval("Amount") %></td>
                                                    <td style="text-align: left;"><%#Eval("NightStay") %></td>
                                                    <td style="text-align: left;"><%#Eval("DAL") %></td>
                                                    <td style="text-align: left;"><%#Eval("Other") %></td>
                                                    <td style="text-align: left;"><%#Eval("Total") %></td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th colspan="6" style="text-align: right;">Total Disance</th>
                                            <td>
                                                <asp:Label ID="txtTotal" runat="server"></asp:Label></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label ID="lblAmountt" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblNight" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblDal" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblOther" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblTotalq" runat="server"></asp:Label></td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="box box-primary" id="summary" runat="server" visible="false">
                    <div class="box-body">
                        <div class="widget-content">
                            <div class="table-responsive">
                                <table id="ExportTbl" class="table table-bordered display table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">S No.</th>
                                            <th>
                                                <input type='checkbox' id='chkAll' runat='server' onclick='javascript: chkChange(this);' /></th>
                                            <th style="text-align: left;">Employee Name</th>
                                            <th style="text-align: left;">Primary Location</th>
                                            <th style="text-align: left;">Travel Date</th>
                                            <th style="text-align: left;">Place Att. IN</th>
                                            <th style="text-align: left;">Place Att. Out</th>
                                            <th style="text-align: left;">Distance in KM</th>
                                            <th style="text-align: left;">Rate Per KM</th>
                                            <th style="text-align: left;">Amount</th>
                                            <th style="text-align: left;">Night Stay</th>
                                            <th style="text-align: left;">Daily Allowance</th>
                                            <th style="text-align: left;">Other Claim</th>
                                            <th style="text-align: left;">Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA" style='<%# Eval("IsConfirm").ToString().ToUpper()!="FALSE" ?"background-color: steelblue; color:white": "" %>'>
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td>
                                                        <input id="chk" runat="server" type="checkbox" onclick="javascript: chkChange(this);" />
                                                        <td style="text-align: left;"><%#Eval("Emp_NAME") %></td>
                                                        <td style="text-align: left;"><%#Eval("Location") %></td>
                                                        <td style="text-align: left;"><%#Eval("TravelDate") %></td>
                                                        <td style="text-align: left;"><%#Eval("PlaceIn") %></td>
                                                        <td style="text-align: left;"><%#Eval("PlaceOut") %></td>
                                                        <td style="text-align: left;"><%#Eval("Distance") %></td>
                                                        <td style="text-align: left;"><%#Eval("Rate") %></td>
                                                        <td style="text-align: left;"><%#Eval("Amount") %></td>
                                                        <td style="text-align: left;"><%#Eval("NightStay") %></td>
                                                        <td style="text-align: left;"><%#Eval("DAL1") %></td>
                                                        <td style="text-align: left;"><%#Eval("Other") %></td>
                                                        <td style="text-align: left;"><%#Eval("Total") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr class="gradeA">
                                            <th colspan="7" style="text-align: right;">Total Distance</th>
                                            <td>
                                                <asp:Label ID="lblTotalKM" runat="server"></asp:Label></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label ID="lblAmount" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lblTotNS" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblTotDA" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblTotOth" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblTotal" runat="server"></asp:Label></td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="box box-primary" id="all" runat="server" visible="false">
                    <div class="box-body">
                        <div class="widget-content">
                            <div class="table-responsive">
                                <table id="ExportTbl" class="table table-bordered display table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">S No.</th>
                                            <th style="text-align: left;">Employee</th>
                                            <th style="text-align: left;">Distance in KM</th>
                                            <th style="text-align: left;">Rate Per KM</th>
                                            <th style="text-align: left;">Amount</th>
                                            <th style="text-align: left;">Night Stay</th>
                                            <th style="text-align: left;">Daily Allowance</th>
                                            <th style="text-align: left;">Other Claim</th>
                                            <th style="text-align: left;">Total</th>
                                            <th style="text-align: left;">Advance</th>
                                            <th style="text-align: left;">Balance</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater2" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("Emp_Name") %></td>
                                                    <td style="text-align: left;"><%#Eval("Distance") %></td>
                                                    <td style="text-align: left;"><%#Eval("Rate") %></td>
                                                    <td style="text-align: left;"><%#Eval("Amount") %></td>
                                                    <td style="text-align: left;"><%#Eval("NightStay") %></td>
                                                    <td style="text-align: left;"><%#Eval("DAL1") %></td>
                                                    <td style="text-align: left;"><%#Eval("Other") %></td>
                                                    <td style="text-align: left;"><%#Eval("Total") %></td>
                                                    <td style="text-align: left;"><%#Eval("ADVANCE") %></td>
                                                    <td style="text-align: left;"><%#Eval("NetTotal") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr class="gradeA">
                                            <th colspan="2" style="text-align: right;">Total</th>
                                            <td>
                                                <asp:Label ID="Label1" runat="server"></asp:Label></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="Label3" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="Label5" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="LabelADVANCE" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server"></asp:Label></td>

                                        </tr>
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
    var cksku =""; 
    <script type="text/javascript"> 
        function SelectAllCheckboxes(spanChk) {

            var theBox = (spanChk.type == "checkbox") ?
                spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            var n = 0;
            for (i = 0; i < elm.length; i++)

                if (elm[i].type == "checkbox" &&
                    elm[i].id != theBox.id) {

                    if (elm[i].checked != xState) {

                        elm[i].click();
                        chkChange(elm[i]);
                    }
                    n++;
                }
        }

    </script>
    <script>
        function chkChange(e) {

            var id = e.id;
            $.ajax({
                type: 'POST',
                url: "DistanceDetailReport.aspx/txtRep_TextChanged",
                data: '{Id: "' + id + '"  }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {

                    if (id == "Body_chkAll") {
                        var theBox = (e.type == "checkbox") ?
                            e : e.children.item[0];
                        xState = theBox.checked;
                        elm = theBox.form.elements;
                        var n = 0;
                        for (i = 0; i < elm.length; i++) {
                            if (elm[i].type == "checkbox" &&
                                elm[i].id != theBox.id) {

                                if (elm[i].checked != xState) {

                                    elm[i].checked = xState;
                                }
                                n++;
                            }
                        }
                    }
                }
            });
        }
    </script>
</asp:Content>

