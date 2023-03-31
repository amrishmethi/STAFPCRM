<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="Holiday.aspx.cs" Inherits="Soft_Holiday" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Holiday (STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header">
        <h1>Add Holiday
        </h1>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="col-md-3 ">
                            <label>Holiday Name</label>
                            <asp:TextBox ID="txtHolidayName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                Font-Bold="true" ForeColor="Red" Font-Size="Large" ValidationGroup="MM" ControlToValidate="txtHolidayName"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label>Date From</label>
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass=" form-control datepicker1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                Font-Bold="true" ForeColor="Red" Font-Size="Large" ValidationGroup="MM" ControlToValidate="txtDateFrom"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label>Date To</label>
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass=" form-control datepicker1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                Font-Bold="true" ForeColor="Red" Font-Size="Large" ValidationGroup="MM" ControlToValidate="txtDateTo"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3 ">
                            <br />
                            <asp:Button ID="btnSaveExit" runat="server" CssClass="btn btn-primary" ValidationGroup="MM" Text="Save"
                                OnClick="btnSaveExit_Click" />
                        </div>
                    </div>
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="widget-content nopadding" style="overflow: auto;">
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">S No</th>
                                            <th style="text-align: left;">Holiday Code</th>
                                            <th style="text-align: left;">Date From</th>
                                            <th style="text-align: left;">Date To</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repHoliday" runat="server" OnItemCommand="repHoliday_ItemCommand">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td style="text-align: left;"><%# Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%#Eval("HOLIDAY_NAME") %> 
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%#Eval("DATEFROM") %> 
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%#Eval("DATETo") %> 
                                                    </td>
                                                    <td style="text-align: left;">

                                                        <a href="Holiday.aspx?id=<%#Eval("HOLIDAY_ID") %>" style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese" aria-label="Edit" rel="lightbox"><i class="fa fa-pencil"></i></a>

                                                        <asp:LinkButton ID="lnkDelete" runat="server" Style="padding: 1px 6px; font-size: 11px;" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" CommandName="Delete" CssClass="btn btn-small btn-danger" CommandArgument='<%#Eval("HOLIDAY_ID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    </td>
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
