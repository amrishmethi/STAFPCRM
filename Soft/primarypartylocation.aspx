<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="primarypartylocation.aspx.cs" Inherits="Soft_primarypartylocation" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Primary Party Location (STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header">
        <h1>Primary Party Location</h1>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <label>Party Name</label>
                                <asp:TextBox ID="txtPartyName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label>Mobile No</label>
                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" ValidationGroup="MM" Text="Search"
                                    OnClick="btnSearch_Click" />
                            </div>
                        </div>
                        <div class="clearfix">&nbsp;</div>
                    </div>
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="widget-content nopadding" style="overflow: auto;">
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">SR No</th>
                                            <th style="text-align: left;">Party Name</th>
                                            <th style="text-align: left;">Station</th>
                                            <th style="text-align: left;">Mobile No</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repPrimaryParty" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td style="text-align: left;"><%# Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%#Eval("AcName") %> 
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%#Eval("AcStation") %> 
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%#Eval("MobileNo") %> 
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <i class="fa fa-map-marker" aria-hidden="true" title="<%# !string.IsNullOrEmpty(Eval("Place") as string) ? Eval("Place") : "Location not available" %>"></i>
                                                        <a id="<%# "btn" + Eval("Id") %>" onclick="EditAble('<%#Eval("EditAble") %>', <%#Eval("Id") %>)" style="padding: 1px 6px; font-size: 11px;" class="btnEditAble btn btn-small <%#  Eval("EditAble") as string == "No" ? "btn-success" : "btn-danger" %>"><%#Eval("EditAble") %></a>
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
    <script type="text/javascript">
        function EditAble(type, PartyId) {
            var Id = '#btn' + PartyId;
            if (type == "Yes") {
                alert("For this party location already edit able");
                return false;
            }
            else {
                if ($(Id).hasClass('btn-danger')) {
                    alert("For this party location already edit able");
                }
            }
            var Data = {
                type: type,
                PartyId: PartyId
            };
            $.ajax({
                url: 'primarypartylocation.aspx/UpdateLocationStatus',
                dataType: "json",
                type: "Post",
                data: JSON.stringify(Data),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d == 0) {
                        var Id = '#btn' + PartyId;
                        $(Id).removeClass('btn-success').addClass('btn-danger').text('Yes');
                    }
                },
                error: function (response) {
                },
                failure: function (response) {
                }
            });
        }
    </script>
</asp:Content>

