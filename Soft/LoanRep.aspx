<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="LoanRep.aspx.cs" Inherits="Soft_LoanRep" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Loan Report</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1><a id="lnkAdd" runat="server" href="/Soft/LoanEntry.aspx" class="btn btn-primary">Add Loan</a>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/LoanRep.aspx" class="active">LoanList </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-4">
                                <label>Employee</label>
                                <asp:DropDownList ID="drpEmployee" runat="server" CssClass="form-control select2" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Date From</label>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>Date To</label>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
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
                                <table id="ExportTbl" border="1" class="table display table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">Sr. No.</th>
                                            <th style="text-align: left;">Loan Date</th> 
                                            <th style="text-align: left;">Employee</th>
                                            <th style="text-align: left;">Loan Amount</th>
                                            <th style="text-align: left;">No Of Installment </th>
                                            <th style="text-align: left;">Interest Rate</th>
                                            <th style="text-align: left;">Loan Deduct From</th>
                                            <th>
                                                <label id="lblAction">Action</label></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server" OnItemCommand="rep_ItemCommand">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("LoanDate1") %></td> 
                                                    <td style="text-align: left;"><%#Eval("EMP_NAME") %></td>
                                                    <td style="text-align: left;"><%#Eval("amount") %></td>
                                                    <td style="text-align: left;"><%#Eval("Installments") %></td>
                                                    <td style="text-align: left;"><%#Eval("InsRate") %></td>
                                                    <td style="text-align: left;"><%#Eval("LoanDeductDate1") %></td>
                                                    <td style="text-align: left;">
                                                        <a href="LoanEntry.aspx?id=<%#Eval("Id") %>" style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese" aria-label="Edit" rel="lightbox"><i class="fa fa-pencil"></i></a>

                                                        <asp:LinkButton ID="lnkDelete" runat="server" Style="padding: 1px 6px; font-size: 11px;" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" CommandName="Delete" CssClass="btn btn-small btn-danger"
                                                            CommandArgument='<%#Eval("Id") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
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

