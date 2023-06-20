<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="StationBeatA.aspx.cs" Inherits="Soft_StationBeatA" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Station Beat (STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">

        <h1>Station Beat</a>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/StationBeatA.aspx" class="active">Station Beat</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="col-md-3 hidden">
                            <label class="control-label">Headqtr<span style="color: #ff0000">*</span></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpHeadqtr"
                                ErrorMessage="Must Be Select" ValidationGroup="AB" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                            <asp:DropDownList ID="drpHeadqtr" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpHeadqtr_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-3 ">
                            <label class="control-label">District<span style="color: #ff0000">*</span></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpDistrict"
                                ErrorMessage="Must Be Select" ValidationGroup="AB" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                            <asp:DropDownList ID="drpDistrict" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpDistrict_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-3 ">
                            <label class="control-label">Station<span style="color: #ff0000">*</span></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpStation"
                                ErrorMessage="Must Be Select" ValidationGroup="AB" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                            <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Station Beat<span style="color: #ff0000">*</span></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBeat"
                                ErrorMessage="Must Be Fill" ValidationGroup="A" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>

                            <asp:TextBox ID="txtBeat" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="clearfix">&nbsp;</div>
                        <div class="col-md-4">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="A" OnClick="btnSave_Click"/>
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger" Text="Back To List" OnClick="btnBack_Click" />

                        </div>
                        <div class="clearfix">&nbsp;</div>
                        <div class="clearfix">&nbsp;</div>

                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>
