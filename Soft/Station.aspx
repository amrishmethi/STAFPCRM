<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="Station.aspx.cs" Inherits="Soft_Station" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Station (STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header">
        <h1>Station
        </h1>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="col-md-3 hidden ">
                            <label>Headqtr</label>
                            <asp:DropDownList ID="drpHeadqtr" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpHeadqtr_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-3 ">
                            <label>District</label>
                            <asp:ListBox ID="drpDistrict" runat="server" CssClass="form-control select2" SelectionMode="Multiple" OnSelectedIndexChanged="drpDistrict_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox> 
                        </div>
                        <div class="col-md-3 ">
                            <label>Status</label>
                            <asp:DropDownList ID="drpstatus" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpDistrict_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Deactivate" Value="0"></asp:ListItem>
                            </asp:DropDownList>
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
                                            <th style="text-align: left;">S No
                                            </th>
                                            <%--<th style="text-align: left;">HeadQtr
                                            </th>--%>
                                            <th style="text-align: left;">District
                                            </th>
                                            <th style="text-align: left;">Station
                                            </th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repDepartment" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td style="text-align: left;"><%# Container.ItemIndex+1 %>
                                                    </td>
                                                    <%--<td style="text-align: left;">
                                                        <%#Eval("HeadQtr") %> 
                                                    </td>--%>
                                                    <td style="text-align: left;">
                                                        <%#Eval("District") %> 
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%#Eval("Station") %> 
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:CheckBox ID="IsChkLoginApp" runat="server" Checked='<%# Convert.ToBoolean(Eval("isActive"))? true:false%>' AutoPostBack="true" OnCheckedChanged="IsChkLoginApp_CheckedChanged" />

                                                        <asp:HiddenField ID="hddUid" Value='<%#Eval("StationID") %>' runat="server" />
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
