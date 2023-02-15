<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="CheckInReport.aspx.cs" Inherits="Admin_CheckInReport" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Check-In Out Report(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Check-In Report</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/CheckInReport.aspx" class="active">Check-In Report </a></li>
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
                                <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Department</label>
                                <asp:DropDownList ID="drpDept" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Date From</label>
                                <asp:TextBox ID="dpFrom" runat="server" CssClass="form-control datepicker" OnTextChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>Date To</label>
                                <asp:TextBox ID="dpTo" runat="server" CssClass="form-control datepicker" OnTextChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>IsCheck</label>
                                <asp:DropDownList ID="drpIsCheck" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Status</label>
                                <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="Active" Text="Active" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Non-Active" Text="Non-Active"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <%--<div class="col-md-1" style="padding-top: 3px;">
                                <div class="clearfix">&nbsp;</div>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                            </div>--%>
                            <div class="clearfix">&nbsp;</div>
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
                                            <th style="text-align: left;" rowspan="2">Sr. No.</th>
                                            <th style="text-align: left;" rowspan="2">Employee</th>
                                            <th style="text-align: center;" colspan="6">CheckIn</th>
                                            <th style="text-align: center;" colspan="5">CheckOut</th>

                                            <%--<th style="text-align: left;" rowspan="2">WhatsApp No</th>--%>
                                            <%--<th>--%>
                                            <%--<label id="lblAction">Action</label></th>--%>
                                        </tr>

                                        <tr>
                                            <th style="text-align: left;">Image</th>
                                            <th style="text-align: left;">Primary party</th>
                                            <th style="text-align: left;">Station</th>
                                            <th style="text-align: left;">Mobile No</th>
                                            <th style="text-align: left;">Date<br />Time</th>
                                            <th style="text-align: left;">Location</th>
                                            <th style="text-align: left;">Image</th>
                                            <th style="text-align: left;">Date<br />Time</th>
                                            <th style="text-align: left;">Location</th>
                                            <th style="text-align: left;">Next Party</th>
                                            <th style="text-align: left;">Next Station</th>
                                            <%--<th style="text-align: left;">Next Station</th>--%>
                                            <%--<label id="lblAction">Action</label>--%>
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
                                                           <td><a href="ResizeImage.aspx?imgurl=<%# "https://app.tadkeshwarfoods.com/CameraPhotos/CheckInOut/" + Eval("InImage") %>" class="abc1">
                                                        <asp:Image runat="server" ImageUrl='<%# "https://app.tadkeshwarfoods.com/CameraPhotos/CheckInOut/" + Eval("InImage") %>' Width="50" Height="50" Visible='<%# (Eval("InImage").ToString()=="")?false:true %>' /></a></td>
                                                    <td style="text-align: left;"><%#Eval("Party") %></td>
                                                    <td style="text-align: left;"><%#Eval("Station") %></td>
                                                    <td style="text-align: left;"><%#Eval("LoginMobile") %></td>
                                                    <td style="text-align: left;"><%#Eval("CheckDate") %><br /><%#Eval("CheckTime") %></td>
                                                    <td style="text-align: left;"><%#Eval("Place") %></td>
                                                    
                                                           <td><a href="ResizeImage.aspx?imgurl=<%# "https://app.tadkeshwarfoods.com/CameraPhotos/CheckInOut/" + Eval("OutImage") %>" class="abc1">
                                                        <asp:Image runat="server" ImageUrl='<%# "https://app.tadkeshwarfoods.com/CameraPhotos/CheckInOut/" + Eval("OutImage") %>' Width="50" Height="50" Visible='<%# (Eval("OutImage").ToString()=="")?false:true %>' /></a></td>
                                                    <td style="text-align: left;"><%#Eval("OutDate") %><br /><%#Eval("OutTime") %></td>
                                                    <td style="text-align: left;"><%#Eval("ChkOutPlace") %></td>
                                                    <td style="text-align: left;"><%#Eval("NextParty") %></td>
                                                    <td style="text-align: left;"><%#Eval("NextStation") %></td>
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
                url: 'CheckInReport.aspx/ControlAccess',
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

