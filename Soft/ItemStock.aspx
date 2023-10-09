<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="ItemStock.aspx.cs" Inherits="Soft_ItemStock" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Item Stock(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="scpt1" runat="server"></asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>Item Stock(STAFP)</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/ItemStock.aspx" class="active">Item Stock</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3">
                                <label>Group</label>
                                <asp:ListBox ID="drpGrp" runat="server" CssClass="form-control select2" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-md-3">
                                <label>Report</label>
                                <asp:DropDownList ID="drpReport" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                                    <asp:ListItem Text="Reorder" Value="Reorder"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" Text="Search" />
                            </div>
                            <div class="clearfix"></div>
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

                                                <th>Group</th> 
                                                <th>Item Name </th>
                                                <th>Min Qty</th>
                                                <th>Stock Bag Qty</th>
                                                <th>Stock Qty</th>
                                                <th>ReOrder Qty</th> 
                                            </tr> 
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rep" runat="server">
                                                <ItemTemplate>
                                                    <tr class="gradeA">
                                                        <td>
                                                            <%#Container.ItemIndex+1 %>
                                                        </td> 
                                                        <td style="text-align: left;"><%#Eval("CMSName") %></td>
                                                        <td style="text-align: left;"><%#Eval("Itname") %></td>
                                                        <td style="text-align: left;"><%#Eval("ItMinQty") %></td>
                                                        <td style="text-align: left;"><%#Eval("TBALBAG") %></td>
                                                        <td style="text-align: left;"><%#Eval("tBalQty") %></td>
                                                        <td style="text-align: left;"><%#Eval("REORDERQTY") %></td> 
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="3">Total</td>
                                                <td>
                                                    <asp:Label ID="lblTotalBag" runat="server"></asp:Label></td> 
                                                <td>
                                                    <asp:Label ID="lblBalBag" runat="server"></asp:Label></td> 
                                                <td>
                                                    <asp:Label ID="lblTotalQty" runat="server"></asp:Label></td> 
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

