<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="HQWiseOutstanding.aspx.cs" Inherits="Soft_HQWiseOutstanding" %>


<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>HQ Wise Outstanding</title>
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
                            <div class="clearfix">&nbsp;</div>
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
                            <div class="col-md-3">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" Text="Search" />
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
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("District") %></td>
                                                    <td style="text-align: left;"><%#Eval("Station") %></td>
                                                    <td style='<%# Eval("acname").ToString().Contains("As On")==true ? "text-align: left;font-weight: bold;": "text-align: center;" %>'><%# Eval("acname") %></td>

                                                    <td style="text-align: left;"><%#Eval("VOCDATE") %></td>
                                                    <td style="text-align: left;"><%#Eval("VOCID") %></td>
                                                    <td style='<%# Eval("acname").ToString().Contains("Total")==true ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("BILLAMT") +" "+Eval("MM") %>   </td>
                                                    <td style='<%# Eval("acname").ToString().Contains("Total")==true ? "text-align: left;font-weight: bold;": "text-align: left;" %>'><%# Eval("DUEAMT") +" "+Eval("MM") %>   </td>
                                                    <td style="text-align: left;"><%#Eval("DUEDATE") %></td>
                                                    <td style="text-align: left;"><%#Eval("DUEDAY") %></td>
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
</asp:Content>
