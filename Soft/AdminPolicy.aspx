<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="AdminPolicy.aspx.cs" Inherits="Soft_AdminPolicy" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Admin Policy (STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1><a id="lnkAdd" runat="server" href="/Soft/AddAdminPolicy.aspx" class="btn btn-primary">Add Admin Policy</a>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#"><i class="fa fa-dashboard"></i>HR</a></li>
            <li><a href="/Soft/AdminPolicy.aspx" class="active">Admin Policy </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="widget-content nopadding" style="overflow: auto;">
                            <table id="ExportTbl" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th style="text-align: left;">S No
                                        </th>
                                        <th style="text-align: left;">Date
                                        </th>
                                        <th style="text-align: left;">Admin Policy
                                        </th>
                                        <th id="lblAction">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repAdminPolicy" runat="server" OnItemCommand="repAdminPolicy_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="gradeA">
                                                <td style="text-align: left;"><%# Container.ItemIndex+1 %>
                                                </td>
                                                <td style="text-align: left;">
                                                    <%#Eval("PDate") %>
                                                </td>
                                                <td style="text-align: left;">
                                                    <%#Eval("Policy_Head") %>
                                                </td>
                                                <td id="lblAction" style="text-align: left;">
                                                    <div class="isEditVisible" style="display: inline;">
                                                        <a href="AddAdminPolicy.aspx?id=<%#Eval("Policy_Id") %>" style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese" aria-label="Edit" rel="lightbox"><i class="fa fa-pencil"></i></a>
                                                    </div>
                                                    <div class="isDelVisible" style="display: inline;">
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Style="padding: 1px 6px; font-size: 11px;" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" CommandName="Delete" CssClass="btn btn-small btn-danger"
                                                            CommandArgument='<%#Eval("Policy_Id") %>'><i class="fa fa-trash-o"></i></asp:LinkButton> 
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
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                url: 'AdminPolicy.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    let text = data.d;
                    const myArray = text.split(",");

                    document.getElementById("Body_lnkAdd").style.display = myArray[0] == "False" ? "none" : "";

                    var elements = document.getElementsByClassName("isEditVisible");
                    Array.prototype.forEach.call(elements, function (element) {
                        element.style.display = myArray[1] == "False" ? "none" : "inline";
                    });
                    var elements1 = document.getElementsByClassName("isDelVisible");
                    Array.prototype.forEach.call(elements1, function (element) {
                        element.style.display = myArray[2] == "False" ? "none" : "inline";
                    });

                    if (myArray[1] == 'False' && myArray[2] == 'False') {
                        document.getElementById("lblAction").innerHTML = "";

                    }
                    document.getElementsByClassName("buttons-excel")[0].style.display = myArray[3] == "False" ? "none" : "";
                    document.getElementsByClassName("buttons-pdf")[0].style.display = myArray[3] == "False" ? "none" : "";
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
