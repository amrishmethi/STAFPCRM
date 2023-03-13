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
                                <label class="control-label">Employee<span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpEmp"
                                    ErrorMessage="Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
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
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" ValidationGroup="aa" Text="Get Report" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                        <div class="clearfix">&nbsp;</div>
                        <div class="clearfix">&nbsp;</div>
                    </div>
                </div>

                <div class="box box-primary"  id="detail" runat="server" visible="false">
                    <div class="box-body">
                        <div class="widget-content">
                            <div class="table-responsive">
                                <table id="ExportTbl" class="table table-bordered display table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">S No.</th>
                                            <th style="text-align: left;">Station</th>
                                            <th style="text-align: left;">Travel Date</th>
                                            <th style="text-align: left;">Working</th>
                                            <th style="text-align: left;">Distance in KM</th>
                                            <th style="text-align: left;">Place</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("Station") %></td>
                                                    <td style="text-align: left;"><%#Eval("TravelDate") %></td>
                                                    <td style="text-align: left;"><%#Eval("EntryType") %></td>
                                                    <td style="text-align: left;"><%#Eval("Distance") %></td>
                                                    <td style="text-align: left;"><%#Eval("Place") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th colspan="4" style="text-align: right;">Total Disance</th>
                                            <td>
                                                <asp:Label ID="txtTotal" runat="server" CssClass="form-control"></asp:Label></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <th colspan="4" style="text-align: right;">Rate</th>
                                            <td>
                                                <asp:Label ID="lblRatee" runat="server" CssClass="form-control"></asp:Label></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <th colspan="4" style="text-align: right;">Amount</th>
                                            <td>
                                                <asp:Label ID="lblAmountt" runat="server" CssClass="form-control"></asp:Label></td>
                                            <td>&nbsp;</td>
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
                                            <th style="text-align: left;">Travel Date</th>
                                            <th style="text-align: left;">Distance in KM</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("TravelDate") %></td>
                                                    <td style="text-align: left;"><%#Eval("Distance") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th colspan="2" style="text-align: right;">Total Disance</th>
                                            <td>
                                                <asp:Label ID="lblTotalKM" runat="server" CssClass="form-control"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <th colspan="2" style="text-align: right;">Rate</th>
                                            <td>
                                                <asp:Label ID="lblRate" runat="server" CssClass="form-control"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <th colspan="2" style="text-align: right;">Total Amount</th>
                                            <td>
                                                <asp:Label ID="lblAMount" runat="server" CssClass="form-control"></asp:Label></td>
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
</asp:Content>
