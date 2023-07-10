<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="NightAttendance.aspx.cs" Inherits="Soft_NightAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Night Attendance &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger" Text="Back To List"
            CausesValidation="false" OnClick="btnCancel_Click" />
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/NightAttendanceRep.aspx" class="active">Night Attendance </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12" style="border-bottom: .5px solid lightgrey;">
                    <h4 class="box-title">
                        <label>Night Attendance </label>
                    </h4>
                    <div class="col-md-4">
                        <label class="control-label">Department <span style="color: #ff0000">*</span></label>

                        <asp:DropDownList ID="drpdepartment" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpdepartment_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Employee <span style="color: #ff0000">*</span></label>

                        <asp:DropDownList ID="drpEmployee" runat="server" CssClass="form-control select2">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                        <label class="control-label">Date </label>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Reason </label>
                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />

                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" CausesValidation="false" OnClick="btnCancel_Click"
                                Text="Cancel" />
                        </div>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">

    <script type="text/javascript">
        function getdays() {
            var dateI1 = document.getElementById("txtFromDate").value;
            var dateI2 = document.getElementById("txtToDate").value;
            debugger
            $.ajax({

                url: 'NightAttendance.aspx/GetDays',
                dataType: "json",
                data: '{From: "' + dateI1 + '",To: "' + dateI2 + '"}',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger
                    $('#Body_TxtDay').val(data.d);
                },
                error: function (response) {
                },
                failure: function (response) {
                }
            });
        }


        $(document).ready(function () {
            var dateI1 = document.getElementById("txtFromDate").value;
            var dateI2 = document.getElementById("txtToDate").value;
            debugger
            $.ajax({

                url: 'NightAttendance.aspx/GetDays',
                dataType: "json",
                data: '{From: "' + dateI1 + '",To: "' + dateI2 + '"}',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger
                    $('#Body_TxtDay').val(data.d);
                },
                error: function (response) {
                },
                failure: function (response) {
                }
            });
        });

    </script>
</asp:Content>

