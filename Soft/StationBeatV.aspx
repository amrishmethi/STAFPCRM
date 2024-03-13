<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="StationBeatV.aspx.cs" Inherits="Soft_StationBeatV" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Station Beat (STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">

        <h1>Station Beat View
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/StationBeatV.aspx" class="active">Station Beat View</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="col-md-3 hidden">
                            <label>Headqtr</label>
                            <asp:DropDownList ID="drpHeadqtr" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpHeadqtr_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-3 ">
                            <label>District</label>
                            <asp:DropDownList ID="drpDistrict" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpDistrict_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-3 ">
                            <label>Station</label>
                            <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpStation_SelectedIndexChanged"></asp:DropDownList>
                        </div> 
                        <div class="clearfix">&nbsp;</div>
                        <div class="clearfix">&nbsp;</div> 
                    </div>
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="widget-content nopadding" style="overflow: auto;">
                                <table id="ExportTbl" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">S No</th>
                                            <th style="text-align: left;">Station</th>
                                            <th style="text-align: left;">Beat</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repDepartment" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td style="text-align: left;"><%# Container.ItemIndex+1 %> </td>
                                                    <td style="text-align: left;"><%#Eval("Station") %> </td>
                                                    <td style="text-align: left;"><%#Eval("Beat") %> </td>
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
