<%@ Page Title="Night Stay (STAFP)" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="NightStayA.aspx.cs" Inherits="Soft_NightStayA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Night Stay &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger" Text="Back To List"
            CausesValidation="false" OnClick="btnBack_Click"/>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/NightStay.aspx" class="active">Night Stay </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12" style="border-bottom: .5px solid lightgrey;">
                    <h4 class="box-title">
                        <label>Night Stay </label>
                    </h4>
                    <div class="col-md-3 hidden">
                        <label class="control-label">Department  </label>

                        <asp:DropDownList ID="drpDepartment" runat="server" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Employee <span style="color: #ff0000">*</span></label>
                        <asp:DropDownList ID="drpEmployee" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpEmployee_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:HiddenField ID="hddCrmUserId" runat="server" Value="0" />
                    </div>
                    <div class="col-md-2">
                        <label class="control-label">Date From</label>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                    </div>

                    <div class="col-md-3">
                        <label class="control-label">Entry Type</label>
                        <br />
                        <asp:RadioButton ID="rbNightStay" runat="server" Text=" Night Stay" GroupName="A" Checked="true" OnCheckedChanged="drpEmployee_SelectedIndexChanged" AutoPostBack="true" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rbDA" runat="server" Text="Daily Allowance" GroupName="A" OnCheckedChanged="drpEmployee_SelectedIndexChanged" AutoPostBack="true" />
                    </div>

                    <div class="col-md-2">
                        <label class="control-label">Charges </label>
                        <asp:Label ID="txtCharges" runat="server" CssClass="form-control"></asp:Label>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />

                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" CausesValidation="false"
                                Text="Cancel" OnClick="btnBack_Click"/>
                        </div>
                    </div><div class="clearfix">&nbsp;</div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
</asp:Content>

