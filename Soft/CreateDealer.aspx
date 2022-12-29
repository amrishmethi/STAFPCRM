<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="CreateDealer.aspx.cs" Inherits="Admin_CreateDealer" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Create Dealer(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Create Dealer</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/CreateDealer.aspx" class="active">Create Dealer</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <%-- <div class="col-md-4">
                              <label>Employee</label>
                                <asp:DropDownList ID="drpEmp" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpEmp_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Department</label>
                                <asp:DropDownList ID="drpDept" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpEmp_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Head Quarter</label>
                                <asp:DropDownList ID="drpHqtr" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpEmp_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>--%>
                            <div class="col-md-2">
                                <label>Party Type</label>
                                <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="Channel Sales Party" Text="Channel Sales Party"></asp:ListItem>
                                    <asp:ListItem Value="Direct Sales Party" Text="Direct Sales Party"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <label>Date From</label>
                                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control datepicker" OnTextChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                               <div class="col-md-2">
                                <label>Date To</label>
                                <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control datepicker" OnTextChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <%-- <div class="col-md-2">
                                <label>IsMeet</label>
                                <asp:DropDownList ID="drpIsMeet" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpEmp_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                </asp:DropDownList>
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
                                            <th style="text-align: left;">Edit</th>
                                            <th style="text-align: left;">Docu</th>
                                            <th style="text-align: left;">Date<br />
                                                Time</th>
                                            <th style="text-align: left;">Name</th>
                                            <th style="text-align: left;">Station</th>
                                            <th style="text-align: left;">Address</th>
                                            <th style="text-align: left;">District</th>
                                            <th style="text-align: left;">Zip</th>
                                            <th style="text-align: left;">State</th>
                                            <th style="text-align: left;">Contact Person</th>
                                            <th style="text-align: left;">Gst No</th>
                                            <th style="text-align: left;">Gst Type</th>
                                            <th style="text-align: left;">Mobile</th>
                                            <th style="text-align: left;">WhatsApp</th>
                                            <th style="text-align: left;">Transport</th>
                                            <th style="text-align: left;">Party Type</th>
                                            <th style="text-align: left;">Party Category</th>
                                            <%--<th style="text-align: left;">Document</th>
                                            <th style="text-align: left;">Image</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <a href='UpdateCreateDealer.aspx?id=<%#Eval("ID") %>' style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese abc" aria-label="Edit" rel="lightbox"><i class="fa fa-pencil"></i></a>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <a href='createDealer_Document.aspx?id=<%#Eval("ID") %>' style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese " aria-label="Edit" rel="lightbox"><i class="fa fa-file"></i></a>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("cdDATE") %><br />
                                                        <%#Eval("cdTIME") %></td>
                                                    <td style="text-align: left;"><%#Eval("Name") %></td>
                                                    <td style="text-align: left;"><%#Eval("Station") %></td>
                                                    <td style="text-align: left;"><%#Eval("Address") %></td>
                                                    <td style="text-align: left;"><%#Eval("District") %></td>
                                                    <td style="text-align: left;"><%#Eval("PinCode") %></td>
                                                    <td style="text-align: left;"><%#Eval("State") %></td>
                                                    <td style="text-align: left;"><%#Eval("ContPer") %></td>
                                                    <td style="text-align: left;"><%#Eval("GSTNo") %></td>
                                                    <td style="text-align: left;"><%#Eval("GSTRegType") %></td>
                                                    <td style="text-align: left;"><%#Eval("SMSMobile") %></td>
                                                    <td style="text-align: left;"><%#Eval("WhatsAppNo") %></td>
                                                    <td style="text-align: left;"><%#Eval("Transport") %></td>
                                                    <td style="text-align: left;"><%#Eval("PartyType") %></td>
                                                    <td style="text-align: left;"><%#Eval("PartyCatg") %></td>
                                                    <%--   <td style="text-align: left;"><%#Eval("DocName") %></td>
                                                    <td><a href="ResizeImage.aspx?imgurl=<%# "https://app.tadkeshwarfoods.com/PartyDocument/" + Eval("DocFileName") %>" class="abc1">
                                                        <asp:Image runat="server" ImageUrl='<%# "https://app.tadkeshwarfoods.com/PartyDocument/" + Eval("DocFileName") %>' Width="50" Height="50" Visible='<%# (Eval("DocFileName").ToString()=="")?false:true %>' /></a></td>--%>
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
                url: 'CreateDealer.aspx/ControlAccess',
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

