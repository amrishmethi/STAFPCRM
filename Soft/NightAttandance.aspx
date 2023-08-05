<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="NightAttandance.aspx.cs" Inherits="Soft_NightAttandance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="refresh" content="60">
    <title>Night Attandance (STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment.min.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBm1CyOx9FugNOb9K6F349bd6mDWjsuB3k"
        type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>Night Attandance 
            <asp:Label ID="lblDate" ClientIDMode="Static" runat="server" Style="float: right"></asp:Label>
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
                                <label>Reporting Manager</label>
                                <asp:DropDownList ID="drpProjectManager" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpProjectManager_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-3 isEditVisible1">
                                <label>Date</label>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datepicker " placeholder="dd/MM/yyyy" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
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
                                            <th style="text-align: left;">Attandance </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server" OnItemCommand="rep_ItemCommand" OnItemDataBound="rep_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("DEPT_NAME") %></td>
                                                    <td style="text-align: left;"><%#Eval("Emp_Name") %></td>

                                                    <td style="text-align: left;">
                                                        <div class="isEditVisible" style="display: inline;">
                                                            <asp:LinkButton ID="lnkIN" runat="server" Style="padding: 1px 6px; font-size: 14px;" CommandName="AttandanceIn" CssClass="btn btn-small btn-primary" CommandArgument='<%#Eval("EmpId") %>'> In</asp:LinkButton>
                                                            &nbsp;&nbsp;
                                                            <asp:HiddenField ID="HddEmpId" runat="server" Value='<%#Eval("EmpId") %>' />
                                                            <asp:HiddenField ID="HddCrmUserId" runat="server" Value='<%#Eval("CrmUserId") %>' />
                                                        </div>
                                                        <div class="isDelVisible" style="display: inline;">
                                                            <asp:LinkButton ID="lnkOut" runat="server" Style="padding: 1px 6px; font-size: 11px;" CommandName="AttandanceOut" CssClass="btn btn-small btn-danger" CommandArgument='<%#Eval("EmpId") %>'>  Out</asp:LinkButton>
                                                            <asp:HiddenField ID="HddID" runat="server" Value="0" />
                                                        </div>
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
    <script type="text/javascript">
        $(document).ready(function () {

            $.ajax({
                url: 'NightAttandance.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    let text = data.d;
                    const myArray = text.split(",");

                    //document.getElementById("Body_lnkAdd").style.display = myArray[0] == "False" ? "none" : "";

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

