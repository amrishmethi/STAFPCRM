﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="HQWiseOutstanding.aspx.cs" Inherits="Soft_HQWiseOutstanding" %>


<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>HQ WISE OUTSTANDING</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>HQ WISE OUTSTANDING</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/HQWiseOutstanding.aspx" class="active">HQ WISE OUTSTANDING</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-2">
                                <label>Employee Status</label>
                                <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="Active" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Non-Active" Value="Non-Active"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Employee</label>
                                <asp:DropDownList ID="DrpEmployee" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="DrpEmployee_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>HeadQuarter <span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Select" InitialValue="0"
                                    Font-Bod="true" ForeColor="Red" ControlToValidate="drpHeadQtr"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpHeadQtr" runat="server" OnSelectedIndexChanged="drpHeadQtr_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Distict</label>
                                <asp:DropDownList ID="drpDistict" runat="server" OnSelectedIndexChanged="drpDistict_SelectedIndexChanged1" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Station</label>
                                <asp:DropDownList ID="Drpstation" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Party</label>
                                <asp:DropDownList ID="drpParty" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Account Group</label>
                                <asp:DropDownList ID="drpAccountGrp" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Book Type</label>
                                <asp:DropDownList ID="drpBookType" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Category</label>
                                <asp:DropDownList ID="drpPartyCategory" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Report Type</label>
                                <asp:DropDownList ID="drpReport" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Due" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Date As On</label>
                                <asp:TextBox ID="dpFrom" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2 hidden">
                                <label>Date To</label>
                                <asp:TextBox ID="dpTo" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" Text="Get Data" />

                                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success" Text="Print Report"
                                    ValidationGroup="aa" OnClick="btnPrint_Click" />
                            </div>
                            <div class="clearfix">&nbsp;</div>
                        </div>
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
                                            <th>
                                                <input type='checkbox' id='chkAll' runat='server' onclick='javascript: chkChange(this);' /></th>
                                            <th>DISTRICT</th>
                                            <th>STATION</th>
                                            <th>Party </th>
                                            <th>Bill Date</th>
                                            <th>Bill No</th>
                                            <th>Bill Amount</th>
                                            <th>Due Amount</th>
                                            <th>Due Date</th>
                                            <th>Due Day</th>
                                        </tr>

                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA" style="color: <%#Eval("color") %>">
                                                    <td>
                                                        <%#Container.ItemIndex+1  %>
                                                    </td>
                                                    <td>
                                                        <input id="chk" runat="server" type="checkbox" onclick="javascript: chkChange(this);" visible='<%#Eval("VOCID").ToString().Contains("Total") ?false:true%>' />
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("District") %></td>
                                                    <td style="text-align: left;"><%#Eval("Station") %></td>
                                                    <td style='<%#Eval("acname").ToString().Contains("As On")==true ? "text-align: left;font-weight: bold;": "text-align: center;" %>'><%# Eval("acname") %></td>

                                                    <td style="text-align: left;"><%#Eval("VOCDATE") %></td>
                                                    <td style="text-align: left;"><%#Eval("VOCID") %></td>
                                                    <td style='<%# Eval("acname").ToString().Contains("Total")==true ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("BILLAMT") +" "+Eval("MM") %>   </td>
                                                    <td style='<%# Eval("acname").ToString().Contains("Total")==true ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("DUEAMT") +" "+Eval("MM") %>   </td>
                                                    <td style="text-align: left;"><%#Eval("DUEDATE") %></td>
                                                    <td style="text-align: left;"><%# Eval("DUEDAY") %></td>
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
    <uc1:DTJS runat="server" ID="DTJS" />

    var cksku =""; 
    <script type="text/javascript"> 
        function SelectAllCheckboxes(spanChk) {

            var theBox = (spanChk.type == "checkbox") ?
                spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            var n = 0;
            for (i = 0; i < elm.length; i++)

                if (elm[i].type == "checkbox" &&
                    elm[i].id != theBox.id) {

                    if (elm[i].checked != xState) {

                        elm[i].click();
                        chkChange(elm[i]);
                    }
                    n++;
                }
        }

    </script>


    <script>
        function chkChange(e) {

            var id = e.id;
            $.ajax({
                type: 'POST',
                url: "HQWiseOutstanding.aspx/txtRep_TextChanged",
                data: '{Id: "' + id + '"  }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {

                    if (id == "Body_chkAll") {
                        var theBox = (e.type == "checkbox") ?
                            e : e.children.item[0];
                        xState = theBox.checked;
                        elm = theBox.form.elements;
                        var n = 0;
                        for (i = 0; i < elm.length; i++) {
                            if (elm[i].type == "checkbox" &&
                                elm[i].id != theBox.id) {

                                if (elm[i].checked != xState) {

                                    elm[i].checked = xState;
                                }
                                n++;
                            }
                        }
                    }
                }
            });

        }
    </script>
</asp:Content>