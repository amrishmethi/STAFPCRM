<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="PRODUCTS.aspx.cs" Inherits="Soft_PRODUCTS" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Product List(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="ss" runat="server"></asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>Product List </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/PRODUCTS.aspx" class="active">Product List </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3">
                                <label>Group</label>
                                <asp:DropDownList ID="drpGroup" runat="server" CssClass="form-control select2" Width="100%"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Image Base Path</label>
                                <asp:TextBox ID="txtbasepath" runat="server" Text=""
                                    Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label>Group Path</label>
                                <asp:TextBox ID="txtgrouppath" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                <a id="grpImage" href='<%#Eval("imageurl") %>' target="_blank" style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese" aria-label="Edit" rel="lightbox" runat="server" visible="false"><i class="fa fa-eye"></i></a>
                            </div>
                            <div class="col-md-3" style="padding-top: 3px;">
                                <div class="clearfix">&nbsp;</div>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />

                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
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
                                            <th style="text-align: left; width: 10%;">Sr. No.</th>
                                            <th style="text-align: left; width: 30%;">Item Name</th>
                                            <th style="text-align: left; width: 60%;">URL</th>
                                            <th style="text-align: left; width: 10%;">View</th>
                                        </tr>
                                    </thead>
                                    <tbody>

                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                        <asp:HiddenField ID="hddItemID" runat="server" Value='<%# Eval("ITCODE") %>' />
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("itname") %></td>
                                                    <td style="text-align: left;">
                                                        <asp:UpdatePanel ID="uu" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtUrl" runat="server" Text='<%#Eval("ITEMIMAGE") %>' CssClass="form-control" OnTextChanged="txtUrl_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="txtUrl" EventName="TextChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td><a href='<%#Eval("imageurl") %>' target="_blank" style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese" aria-label="Edit" rel="lightbox" runat="server" visible='<%#!Convert.ToBoolean(Eval("ISEDIT")) %>'><i class="fa fa-eye"></i></a></td>
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

