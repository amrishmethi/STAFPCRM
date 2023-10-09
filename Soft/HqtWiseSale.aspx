<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="HqtWiseSale.aspx.cs" Inherits="Soft_HqtWiseSale" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>HeadQtr Wise Sale Report (S/T)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="scpt1" runat="server"></asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>HeadQtr Wise Sale Report (S/T)</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/HqtWiseSale.aspx" class="active">HeadQtr Wise Sale Report (S/T)</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">

                            <%--   <asp:UpdatePanel ID="upd" runat="server">
                    <ContentTemplate>--%>
                            <div class="col-md-3">
                                <label>Employee</label>

                                <asp:DropDownList ID="DrpEmployee" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="DrpEmployee_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                               <label>HeadQuarter <span style="color: #ff0000">*</span></label>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Select" InitialValue="0"
     Font-Bod="true" ForeColor="Red"  ControlToValidate="drpHeadQtr"></asp:RequiredFieldValidator>

                                <asp:DropDownList ID="drpHeadQtr" runat="server" OnSelectedIndexChanged="drpHeadQtr_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Distict</label>
                                <asp:ListBox ID="drpDistict" runat="server" OnSelectedIndexChanged="drpDistict_SelectedIndexChanged1" CssClass="form-control select2" AutoPostBack="true" SelectionMode="Multiple"></asp:ListBox>



                            </div>
                            <div class="col-md-2">
                                <label>Station</label>
                                <asp:DropDownList ID="Drpstation" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <label>Rate</label>
                                <asp:DropDownList ID="Drprate" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="With Tax" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Without Tax" Value="0"></asp:ListItem>

                                </asp:DropDownList>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-3 hidden">
                                <label>Report</label>
                                <asp:DropDownList ID="drpReport" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="Detail" Value="Detail"></asp:ListItem> 
                                    <asp:ListItem Text="Summary" Value="Summary"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <label>Party</label>
                                <asp:DropDownList ID="drpParty" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Group</label>
                                <asp:ListBox ID="drpGrp" runat="server" CssClass="form-control select2" SelectionMode="Multiple"></asp:ListBox>
                            </div>


                            <div class="col-md-2">
                                <label>Date From</label>
                                <asp:TextBox ID="dpFrom" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>Date To</label>
                                <asp:TextBox ID="dpTo" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" Text="Search" />
                            </div>
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
                                                <th>Date</th>
                                                <th>HeadQuarter</th>
                                                <th>District</th>
                                                <th>Station</th>
                                                <th>Party </th>
                                                <th>Party Group</th>
                                                <th>Item Name </th>
                                                <th>Order Bag</th>
                                                <th>packing</th>
                                                <th>Qty</th>
                                                <th>Rate</th>
                                                <th>Amount</th> 
                                            </tr> 
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rep" runat="server">
                                                <ItemTemplate>
                                                    <tr class="gradeA">
                                                        <td>
                                                            <%#Container.ItemIndex+1 %>
                                                        </td>
                                                        <td style="text-align: left;"><%# Eval("acname").ToString() =="Total"?"" : Eval("orddate")  %></td>
                                                        <td style="text-align: left;"><%#Eval("HeadQtr") %></td>
                                                        <td style="text-align: left;"><%#Eval("District") %></td>
                                                        <td style="text-align: left;"><%#Eval("Station") %></td>
                                                        <td style="text-align: left;"><%#Eval("acname") %></td>
                                                        <td style="text-align: left;"><%#Eval("cmsname") %></td>
                                                        <td style="text-align: left;"><%#Eval("itname") %></td>
                                                        <td style="text-align: left;"><%#Eval("ordbag") %></td>
                                                        <td style="text-align: left;"><%#Eval("CWeight") %></td>
                                                        <td style="text-align: left;"><%#Eval("qty") %></td>
                                                        <td style="text-align: left;"><%#Eval("ordstprate") %></td>
                                                        <td style="text-align: left;"><%#Eval("amount") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="8">Total</td>
                                                <td>
                                                    <asp:Label ID="lblTotalBag" runat="server"></asp:Label></td>
                                                <td></td>
                                                <td>
                                                    <asp:Label ID="lblTotalQty" runat="server"></asp:Label></td>
                                                <td></td>
                                                <td>
                                                    <asp:Label ID="lblTotalAmt" runat="server"></asp:Label></td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
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
                url: 'UserWiseParty.aspx/ControlAccess',
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
                    //var elements2 = document.getElementsByClassName("isAssVisible");
                    //Array.prototype.forEach.call(elements2, function (element) {
                    //    element.style.display = myArray[4] == "False" ? "none" : "";
                    //});
                    //var elements3 = document.getElementsByClassName("isLoginVisible");
                    //Array.prototype.forEach.call(elements3, function (element) {
                    //    element.style.display = myArray[5] == "False" ? "none" : "";
                    //});

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

