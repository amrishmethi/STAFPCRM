<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="LoanEntry.aspx.cs" Inherits="Soft_LoanEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Loan Entry &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger" Text="Back To List"
            CausesValidation="false" OnClick="btnCancel_Click" />
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/LoanRep.aspx" class="active">Loan Entry </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12" style="border-bottom: .5px solid lightgrey;">
                    <h4 class="box-title">
                        <label>Loan Entry</label>
                    </h4>
                    <div class="col-md-4">
                        <label class="control-label">Employee <span style="color: #ff0000">*</span></label>
                        <asp:DropDownList ID="drpEmployee" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpEmployee_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Loan Amount</label>
                        <asp:TextBox ID="txtLoanAmount" runat="server" Enabled="false"  ClientIDMode="Static"  CssClass="form-control" onkeypress="return IsNumericKey(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label class="control-label">No Of Installment</label>
                        <asp:TextBox ID="txtNoOfInstallment" runat="server" CssClass="form-control" ClientIDMode="Static" onkeypress="return IsNumericKey(event);" onchange="noofinstallment();"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label class="control-label">Rate Per Anum</label>
                        <asp:TextBox ID="txtIntRate" runat="server" CssClass="form-control" onkeypress="return IsNumericKey(event);"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">Installment Amount</label>
                        <input id="txtInstallmentAmount" runat="server" class="form-control" clientidmode="Static"  onkeypress="return IsNumericKey(event);" readonly  />
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Loan Date</label>
                        <asp:TextBox ID="txtLoanDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Loan Deduct Date</label>
                        <asp:TextBox ID="txtLoanDeductDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />

                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" CausesValidation="false"
                                Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
    <script>
        function noofinstallment() {
            
            var NoOfInstallment = document.getElementById("txtNoOfInstallment").value ;
            var LoanAmount = document.getElementById("txtLoanAmount").value;
            
            var insAmt = parseInt(LoanAmount) / parseInt(NoOfInstallment);
            
            document.getElementById("txtInstallmentAmount").value = insAmt.toFixed(2);
        }
    </script>
</asp:Content>

