<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="AdvanceSallary.aspx.cs" Inherits="Soft_AdvanceSallary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" />
    <section class="content-header" style="height: 2.5em;">
        <h1>Advance Salary &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger" Text="Back To List"
            OnClick="btnBack_Click" CausesValidation="false" /></h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/AdvaneSallaryRep.aspx" class="active">Advance Salary </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12" style="border-bottom: .5px solid lightgrey;">
                    <h4 class="box-title">
                        <label>EMPLOYEE DETAILS</label>
                    </h4>
                    <div class="col-md-4">
                        <label class="control-label">Employee<span style="color: #ff0000">*</span></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="drpEmployee"
                            ErrorMessage=" Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="drpEmployee" runat="server" CssClass="form-control select2">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="control-label">Date</label>
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datepicker" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">Voc No<span style="color: #ff0000">*</span></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                            ErrorMessage=" Please Enter" ValidationGroup="aa" ControlToValidate="txtVocNo"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtVocNo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Amount<span style="color: #ff0000">*</span></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                            ErrorMessage=" Please Enter" ValidationGroup="aa" ControlToValidate="txtAmount"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" onkeypress="return IsNumericKey(event);"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-8">
                        <label class="control-label">Remarks </label>
                        <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Save & Exit"
                                ValidationGroup="aa" OnClick="btnSubmit_Click" />

                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel"
                                OnClick="btnCancel_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
</asp:Content>

