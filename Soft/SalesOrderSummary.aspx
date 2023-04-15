<%@ Page Title="Sales Order Summary(STAFP)" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SalesOrderSummary.aspx.cs" Inherits="Soft_SalesOrderSummary" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc1:DTCSS runat="server" ID="DTCSS" />
    <style type="text/css">
        #slateGrey {
            background-color: lightslategrey;
            color: white;
        }

        #backcolor {
            background-color: lightgray;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Sales Order Summary</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/SalesOrderSummary.aspx" class="active">Sales Order Summary</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">

                            <div class="col-md-3">
                                <label>Employee</label>
                                <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>HeadQuarter</label>
                                <asp:DropDownList ID="drpHeadQtr" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Party</label>
                                <asp:DropDownList ID="drpParty" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Month</label>
                                <asp:TextBox ID="mnth" runat="server" type="text" class="form-control MnthPicker" autocomplete="off" />
                            </div>
                             <div class="col-md-2">
                                <label class="control-label">Report Type<span style="color: #ff0000">*</span></label>
                                <asp:DropDownList ID="drpReport" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="EMPLOYEE WISE" Value="EMPLOYEEWISE" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="PARTY WISE" Value="PARTYWISE"></asp:ListItem>
                                    <asp:ListItem Text="DATE WISE" Value="DATEWISE"></asp:ListItem>
                                </asp:DropDownList>
                            </div><div class="clearfix">&nbsp;</div>
                            <div class="col-md-2"> 
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

                        <div class="widget-content nopadding" style="overflow: auto;">
                            <div id="tblBlock" runat="server" class="table-responsive">
                                <table id="ExportTbl" class="table table-bordered display table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center;" rowspan="2">Sr. No.</th>
                                            <th style="text-align: center;" rowspan="2">Date</th>
                                            <th style="text-align: center;" rowspan="2">Employee Name</th>
                                            <th style="text-align: center;" rowspan="2">Party Name</th>
                                            <th style="text-align: center;" colspan="2">Total Sale (Month)</th>
                                            <th style="text-align: center;" rowspan="2">Total Amount<br />
                                                (Month)</th>
                                            <th style="text-align: center;" rowspan="2">Total Expenses<br />
                                                (Month)</th>
                                            <th style="text-align: center;" rowspan="2">CTC%
                                                <br />
                                                (Month)</th>
                                        </tr>
                                        <tr>
                                            <th>Powder</th>
                                            <th>Bar/Tub</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repData" runat="server" OnItemDataBound="repData_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td><%#Container.ItemIndex+1 %></td>
                                                    <td><%#Eval("ORDDATE") %></td>
                                                    <td><%#Eval("NAME") %>
                                                        <asp:HiddenField ID="hddcrmId" runat="server" Value='<%#Eval("Id") %>' />
                                                    </td>
                                                    <td><%#Eval("PARTY") %></td>
                                                    <td><%#Eval("POWDER") %></td>
                                                    <td><%#Eval("BAR_AND_TUB") %></td>
                                                    <td>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("AMOUNT") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblExpense" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblCTC" runat="server"></asp:Label></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td colspan="4"> Total</td>
                                            <td><asp:Label ID="lblPowder" runat="server" Text="0"></asp:Label></td>
                                            <td><asp:Label ID="lblBarTub" runat="server" Text="0"></asp:Label></td>
                                            <td><asp:Label ID="lblTotalAmount" runat="server" Text="0"></asp:Label></td>
                                            <td><asp:Label ID="lblTotalExp" runat="server" Text="0"></asp:Label></td>
                                            <td><asp:Label ID="lblTotalCTC" runat="server" Text="0"></asp:Label></td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                            <div class="col-sm-12 hidden" style="text-align: right;">
                                <label>Grand Total</label>
                                <asp:TextBox ID="txtGrandTot" runat="server" Style="font-weight: bold; text-align: right;" ReadOnly="true" Text="0.00"></asp:TextBox>
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
</asp:Content>

