<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="PrimaryLocation.aspx.cs" Inherits="Soft_PrimaryLocation" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Primary Station(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>Primary Station  
        </h1>

    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3">
                                <label>Department</label>
                                <asp:DropDownList ID="drpDepartment" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-3 hidden">
                                <label>Designation</label>
                                <asp:DropDownList ID="drpDesignation" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Employee</label>
                                <asp:DropDownList ID="drpProjectManager" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Status</label>
                                <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="Active" Text="Active" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Non-Active" Text="Non-Active"></asp:ListItem>
                                </asp:DropDownList>
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
                                <table id="ExportTbl" class="table table-bordered display table-striped table-responsive" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">Sr. No.</th>
                                            <th style="text-align: left;">Department</th>
                                            <th style="text-align: left;">Emp Name</th>
                                            <th>Primary Station </th>
                                            <th style="text-align: left;">&nbsp;</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server" OnItemCommand="rep_ItemCommand">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("DEPT_NAME") %></td>
                                                    <td style="text-align: left;"><%#Eval("Emp_Name") %></td>
                                                    <td style="text-align: left;"><%#Eval("PLACE") %> </td>
                                                    <td style="text-align: left;">
                                                        <asp:LinkButton ID="lnkOut" runat="server" Style="padding: 1px 6px; font-size: 11px;" CommandName='<%#Eval("IsEditable") %>' Visible='<%#Eval("IsEditable").ToString()=="False"? true:false %>' CssClass="btn btn-small btn-success" CommandArgument='<%#Eval("ID") %>' Text='<%#Eval("IsEditable1") %>'>   </asp:LinkButton></td>
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
    <script type="text/javascript">
        $(document).ready(function () {

            $.ajax({
                url: 'PrimaryLocation.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    let text = data.d;
                    const myArray = text.split(",");

                    var elements = document.getElementsByClassName("isEditVisible1");
                    Array.prototype.forEach.call(elements, function (element) {
                        element.style.display = myArray[1] == "False" ? "none" : "inline";
                    });
                },
                error: function (response) {
                },
                failure: function (response) {
                }
            });
        })
    </script>
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>
