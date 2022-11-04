<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="AttendanceReport.aspx.cs" Inherits="Admin_AttendanceReport" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <title>Attendance Report(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Attendance Report</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/AttendanceReport.aspx" class="active">Attendance Report </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">

                            <div class="col-md-3">
                                <label>User</label>
                                <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Date</label>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>IsAttend</label>
                                <asp:DropDownList ID="drpIsAttend" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                            </div>


                            <div class="col-md-1" style="padding-top: 3px;">
                                <div class="clearfix">&nbsp;</div>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                            </div>

                        </div>
                        <div class="clearfix">&nbsp;</div>
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
                                            <th style="text-align: left;" rowspan="2">Sr. No.</th>
                                            <th style="text-align: left;" rowspan="2">User</th>
                                            <th style="text-align: left;" rowspan="2">MobileNo</th>
                                            <th style="text-align: center;" colspan="5">In</th>
                                            <th style="text-align: center;" colspan="5">Out</th>

                                            <%--<th style="text-align: left;" rowspan="2">WhatsApp No</th>--%>
                                            <%--<th>--%>
                                            <%--<label id="lblAction">Action</label></th>--%>
                                        </tr>

                                        <tr>
                                            <th style="text-align: left;">Station</th>
                                            <th style="text-align: left;">Desciption</th>
                                            <th style="text-align: left;">Date</th>
                                            <th style="text-align: left;">Time</th>
                                            <th style="text-align: left;">Location</th>
                                            <th style="text-align: left;">Station</th>
                                            <th style="text-align: left;">Desciption</th>
                                            <th style="text-align: left;">Date</th>
                                            <th style="text-align: left;">Time</th>
                                            <th style="text-align: left;">Location</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("UserName") %></td>

                                                    <td style="text-align: left;"><%#Eval("MobileNo") %></td>
                                                    <td style="text-align: left;"><%#Eval("StationIN") %></td>
                                                    <td style="text-align: left;"><%#Eval("DesscriptionIN") %></td>
                                                    <td style="text-align: left;"><%#Eval("DateIN") %></td>
                                                    <td style="text-align: left;"><%#Eval("TimeIN") %></td>
                                                    <td style="text-align: left;"><%#Eval("PlaceIN") %></td>
                                                   <td style="text-align: left;"><%#Eval("StationOUT") %></td>
                                                    <td style="text-align: left;"><%#Eval("DesscriptionOUT") %></td>
                                                    <td style="text-align: left;"><%#Eval("DateOUT") %></td>
                                                    <td style="text-align: left;"><%#Eval("TimeOUT") %></td>
                                                    <td style="text-align: left;"><%#Eval("PlaceOUT") %></td>
                                                    <%--<td style="text-align: left;"><%#Eval("WhatsAppNo") %></td>--%>
                                                    <%--<td style="text-align: left;">
                                                                                                               <div class="isEditVisible" style="display: inline;">
                                                            <a href="SecondarySalesPartyMaster.aspx?id=<%#Eval("ID") %>" style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese" aria-label="Edit" rel="lightbox"><i class="fa fa-pencil"></i></a>
                                                        </div>
                                                        <div class="isDelVisible" style="display: inline;">
                                                            <asp:LinkButton ID="lnkDelete" runat="server" Style="padding: 1px 6px; font-size: 11px;" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" CommandName="Delete" CssClass="btn btn-small btn-danger"
                                                                CommandArgument='<%#Eval("ID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        </div>

                                                    </td>--%>
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

            $.ajax({
                url: 'AttendanceReport.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger
                    let text = data.d;
                    const myArray = text.split(",");

                    /*document.getElementById("Body_lnkAdd").style.display = myArray[0] == "False" ? "none" : "";*/

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
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

