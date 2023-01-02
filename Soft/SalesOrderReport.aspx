<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SalesOrderReport.aspx.cs" Inherits="Soft_SalesOrder_Report" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Sales Order Report(STAFP)</title>
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
        <h1>Sales Order Report</h1>
        <ol class="breadcrumb">
            <li>
               <%-- <button onclick="history.go(-1);
        return false;"
                    class="btn btn-sm btn-success">
                    Go Back</button>--%>
                <button id="print" onclick="Print_Div()" class="btn btn-sm btn-info">PDF</button>
            </li>
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/SalesItemReport.aspx" class="active">Sales Order Report</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">

                <div class="box box-primary">
                    <div class="box-body">

                        <div class="widget-content nopadding" style="overflow: auto;">
                            <div id="tblBlock" runat="server" class="table-responsive">
                                <table id="example" class="table table-bordered display" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">Sr. No.</th>
                                            <th style="text-align: left;">Order Date<br />
                                                Time</th>
                                            <th style="text-align: left;">Party</th>
                                            <th style="text-align: left;">Delivery Mode</th>
                                            <th style="text-align: left;">Payment Mode</th>
                                            <th style="text-align: left;">Remark</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server" OnItemDataBound="rep_ItemDataBound" OnItemCommand="rep_ItemCommand" >
                                            <ItemTemplate>
                                                <tr id="slateGrey" class="gradeA" style="">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                        <asp:HiddenField ID="hddid" runat="server" Value='<%#Eval("ID") %>' />
                                                    </td>
                                                    <td><%#Eval("ODATE") %><br />
                                                        <%#Eval("OTIME") %></td>
                                                    <td><%#Eval("Party") %></td>
                                                  
                                                    <td><%#Eval("DelvMode") %></td>
                                                    <td><%#Eval("PymtMode") %></td>
                                                    <td><%#Eval("Remark") %></td>
                                                    </tr>
                                                <tr>
                                                    <td id="backcolor" colspan="13" style="">

                                                        <table class="table table-bordered display table-striped">
                                                            <thead>
                                                                <tr>
                                                                    <th>Sr. No.</th>
                                                                    <th>Item</th>
                                                                    <th>Qty</th>
                                                                    <th>RATE</th>
                                                                    <th>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rep1" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr class="gradeA">
                                                                            <td>
                                                                                <%#Container.ItemIndex+1 %>
                                                                            </td>
                                                                            <td style="text-align: left;"><%#Eval("ITName") %></td>
                                                                            <td style="text-align: left;"><%#Eval("OrdQty") %></td>
                                                                            <td style="text-align: left;"><%#Eval("OrdStpRate") %></td>
                                                                           <td style="text-align: center;">
                                                                                <a href="#" style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese abc" aria-label="Edit" rel="lightbox"><i class="fa fa-pencil"></i></a>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>

                                                                </asp:Repeater>
                                                            </tbody>
                                                        <%--    <tfoot style="background-color: floralwhite;">
                                                                <tr>
                                                                    <td colspan="6" runat="server">Remark : </td>
                                                                    <td><strong>
                                                                        <asp:Label ID="lblTotal" runat="server"></asp:Label></strong></td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </tfoot>--%>
                                                        </table>

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                          <%--  <div class="col-sm-12" style="text-align: right;">
                                <label>Grand Total</label>
                                <asp:TextBox ID="txtGrandTot" runat="server" Style="font-weight: bold; text-align: right;" ReadOnly="true" Text="0.00"></asp:TextBox>
                            </div>--%>
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
                url: 'SalesOrderReport.aspx/ControlAccess',
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
    <script>
        function Print_Div() {
            debugger
            var restorepage = $('body').html();
            var divContents = document.getElementById("Body_tblBlock").innerHTML;
            var a = window.open('', '', 'height=500, width=500');
            a.document.write('<html>');
            a.document.write('<head> <style>');
            a.document.write('@media print {#slateGrey{ background-color: lightslategrey; color: white; }');
            a.document.write('#backcolor{ background-color: lightgray; }}');
            a.document.write('</style></head>');
            a.document.write('<body>');
            a.document.write(divContents);
            a.document.write('</body></html>');
            a.document.close();
            a.print();
            $('body').html(restorepage);
        }
        //function Print_Div() {
        //    debugger
        //    var restorepage = $('body').html();
        //    var printcontent = $('#Body_tblBlock').clone();
        //    $('body').empty().html(printcontent);
        //    window.print();
        //    $('body').html(restorepage);
        //};
    
    </script>
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>


