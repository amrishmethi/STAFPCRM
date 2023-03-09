<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="PriceList.aspx.cs" Inherits="Admin_PriceList" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Price List(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Price List&nbsp;<asp:LinkButton style="display:inline;" ID="lnkDownloadPDF" runat="server" CssClass="btn btn-sm btn-success" OnClick="lnkDownloadPDF_Click">Print</asp:LinkButton></h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/PriceList.aspx" class="active">Price List </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-8">
                                <label>Group</label>
                                <%--<asp:DropDownList ID="drpGroup" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpGroup_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>--%>
                                <asp:ListBox ID="drpGroup" runat="server" CssClass="form-control select2" SelectionMode="Multiple" Width="100%" OnSelectedIndexChanged="drpGroup_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                            </div>
                            <div class="col-md-2">
                                <label>State</label>
                                <asp:DropDownList ID="drpState" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpGroup_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Local State" Value="0"/>
                                    <asp:ListItem Text="Out of State" Value="1"/>
                                </asp:DropDownList>
                            </div>
                            <%--    <div class="col-md-2">
                                <label>Date To</label>
                                <asp:TextBox ID="dpTo" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>--%>


<%--                            <div class="col-md-1" style="padding-top: 3px;">
                                <div class="clearfix">&nbsp;</div>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                            </div>--%>

                        </div>
                        <div class="clearfix">&nbsp;</div>
                        <div class="clearfix">&nbsp;</div>
                    </div>
                </div>
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="widget-content">
                            <div class="table-responsive">
                                <table id="ExportTbl" border="1" class="table display table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">Sr. No.</th>
                                            <th style="text-align: left;">Item Name</th>
                                           <th style="text-align: left;">Rate Per Kg</th>
                                            <th style="text-align: left;">Rate Per Pc</th>
                                            <th style="text-align: left;">Carton Pack(Per Pc)</th>
                                            <th style="text-align: left;">Amount (Per Bag/Case)</th>
                                            <th style="text-align: left;">MRP(Per Pc)</th>
                                         </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("itname") %></td>
                                                    <td style="text-align: left;"><%#Eval("SalesOrderRate") %></td>
                                                    <td style="text-align: left;"><%#Eval("tRate") %></td>
                                                     <td style="text-align: left;"><%#Eval("itpacking") %></td>
                                                    <td style="text-align: left;"><%#Eval("BagRate") %></td>
                                                    
                                                    <td style="text-align: left;"><%#Eval("MRP") %></td>
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
    <script type="text/javascript">
        $(document).ready(function () {
            debugger
            $.ajax({
                url: 'PriceList.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger
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
                    var elements2 = document.getElementsByClassName("isAssVisible");
                    Array.prototype.forEach.call(elements2, function (element) {
                        element.style.display = myArray[4] == "False" ? "none" : "";
                    });
                    var elements3 = document.getElementsByClassName("isLoginVisible");
                    Array.prototype.forEach.call(elements3, function (element) {
                        element.style.display = myArray[5] == "False" ? "none" : "";
                    });

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

