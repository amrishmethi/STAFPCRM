<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SecondarySalesCustomize.aspx.cs" Inherits="Admin_SecondarySalesCustomize" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Customize Secondary Print Report(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Customize Sales Report Print</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/SecondarySalesCustomize.aspx" class="active">Customize Sales Report Print</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-6">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-12">
                                <label>Date From</label>
                                <asp:TextBox ID="dpFrom" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-12">
                                <label>Date To</label>
                                <asp:TextBox ID="dpTo" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                        </div>
                    </div>
                    
                </div>
                 <asp:Button ID="btnPrint" runat="server" Text="Employee Wise Print" OnClick="btnPrint_Click" CssClass="btn btn-success" />
                 <asp:Button ID="btnPrintSummary" runat="server" Text="Print Summary" OnClick="btnPrint_Click" CssClass="btn btn-success" />
            </div>
            <div class="col-md-6">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="widget-content">
                            <%--<div class="table-responsive">--%>
                                <table id="example" class="table table-bordered display table-striped" >
                                    <thead>
                                        <tr>
                                            <th colspan="2" ><input type='checkbox' id='chkAll' runat='server' onclick='javascript: SelectAllCheckboxes(this);' />&nbsp;Select Employee</th>
                                         </tr>
                                        </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <asp:CheckBox ID="chk" runat="server" />
                                                    </td>
                                                    <asp:HiddenField ID="hddEmpID" runat="server" Value='<%#Eval("EmpId") %>'/>
                                                    <td style="text-align: left;"><%#Eval("Name") %></td>
                                                </tr>


                                            </ItemTemplate>

                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            <%--</div>--%>
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
                url: 'SecondarySalesCustomize.aspx/ControlAccess',
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
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

