<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="Admin_ResetPassword" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Check-In Out Report(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Reset User Password</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/CheckInReport.aspx" class="active">Reset Password </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-sm-12" align="CENTER">
                                <h4>Enter a new Password for <asp:Label ID ="lblUser" runat="server"></asp:Label></h4>
</div>                            <div class="clearfix">&nbsp;</div>
                            <div class="col-sm-5" align="right">
                                <label>New Password:</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtPwd" TextMode="Password" runat="server" CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-sm-5" align="right">
                                <label>Confirm Password:</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtCnfPwd" TextMode="Password" runat="server" CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="clearfix">&nbsp;</div>

                           <div class="col-sm-12" align="center">
                               <div class="clearfix">&nbsp;</div>
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset Password" OnClick="btnReset_Click" />
                            </div>

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
    <%--<script type="text/javascript">
        $(document).ready(function () {

            $.ajax({
                url: 'SecondarySalesParty.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger
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

    </script>--%>
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

