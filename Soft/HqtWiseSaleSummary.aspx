<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="HqtWiseSaleSummary.aspx.cs" Inherits="Soft_HqtWiseSaleSummary" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>HQ GROUP /PARTY WISE SALE</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">

    <section class="content-header" style="height: 2.5em;">
        <h1>HQ GROUP /party WISE SALE</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/HqtWiseSale.aspx" class="active">HQ GROUP /party WISE SALE</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">

                            <div class="col-md-3">
                                <label>Employee</label>

                                <asp:DropDownList ID="DrpEmployee" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="DrpEmployee_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <label>HeadQuarter <span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Select" InitialValue="0"
                                    Font-Bod="true" ForeColor="Red" ControlToValidate="drpHeadQtr"></asp:RequiredFieldValidator>

                                <asp:DropDownList ID="drpHeadQtr" runat="server" OnSelectedIndexChanged="drpHeadQtr_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Distict</label>
                                <asp:ListBox ID="drpDistict" runat="server" OnSelectedIndexChanged="drpDistict_SelectedIndexChanged1" CssClass="form-control select2" AutoPostBack="true" SelectionMode="Multiple"></asp:ListBox>



                            </div>
                            <div class="col-md-2">
                                <label>Station</label>
                                <asp:DropDownList ID="Drpstation" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2 hidden">
                                <label>Rate</label>
                                <asp:DropDownList ID="Drprate" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="With Tax" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Without Tax" Value="0"></asp:ListItem>

                                </asp:DropDownList>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-3 hidden">
                                <label>Report</label>
                                <asp:DropDownList ID="drpReport" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="Detail" Value="Detail"></asp:ListItem>
                                    <asp:ListItem Text="Summary" Value="Summary"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Report Type</label>
                                <asp:DropDownList ID="drpReportType" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Main Group" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Sub Group" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Party</label>
                                <asp:DropDownList ID="drpParty" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Group</label>
                                <asp:ListBox ID="drpGrp" runat="server" CssClass="form-control select2" SelectionMode="Multiple"></asp:ListBox>
                            </div>


                            <div class="col-md-2">
                                <label>Date From</label>
                                <asp:TextBox ID="dpFrom" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>Date To</label>
                                <asp:TextBox ID="dpTo" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" Text="Search" />
                                &nbsp;
                                &nbsp;
                                &nbsp;
                                &nbsp;
                                <asp:Button ID="btnExport" runat="server" CssClass="btn btn-success" OnClick="btnExport_Click" Text="Export To Excel" />
                                  &nbsp;
  &nbsp;
  &nbsp;
  &nbsp;
                                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success" Text="Print Report"
    ValidationGroup="aa" OnClick="btnPrint_Click" />
                            </div>
                            <div class="clearfix">&nbsp;</div>
                        </div>
                    </div>
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="widget-content">
                                <div class="table-responsive">
                                    <asp:DataGrid ID="grdReport" ClientIDMode="Static" runat="server" CssClass="table table-bordered display table-striped ">
                                    </asp:DataGrid>

                                </div>
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
                url: 'HqtWiseSaleSummary.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                  
                    let text = data.d;
                    const myArray = text.split(",");

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

