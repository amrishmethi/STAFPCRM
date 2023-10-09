<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SalesTargetView.aspx.cs" Inherits="Admin_SalesTargetView" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Sales Target View(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="scpt1" runat="server"></asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>Sales Target View</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/SalesTarget.aspx" class="active">Sales Target</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3 hidden">
                                <label>Department</label>
                                <asp:DropDownList ID="drpDepartment" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Employee Status</label>
                                <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="Active" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Non-Active" Value="Non-Active"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Employee</label>

                                <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>HeadQuarter</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpheadQtr"
                                    ErrorMessage="Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpheadQtr" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpheadQtr_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>District</label>
                                <asp:ListBox ID="drpDistrict" runat="server" SelectionMode="Multiple" CssClass="form-control select2" OnSelectedIndexChanged="drpDistrict_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                            </div>
                            <div class="col-md-2">
                                <label>Station</label>
                                <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpStation_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-2">
                                <asp:CheckBox ID="chk" runat="server" ToolTip="Without Party" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                                <label>Party Category</label>
                                <asp:DropDownList ID="drpCatg" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpCatg_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Party Type</label>
                                <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0" Selected="True">Primary</asp:ListItem>
                                    <asp:ListItem Value="1">Secondary</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Month</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="mnth"
                                    ErrorMessage="Please Select" ValidationGroup="aa" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:TextBox ID="mnth" runat="server" type="text" class="form-control MnthPicker" autocomplete="off" />
                            </div>

                            <div class="col-md-4">
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Get Target"
                                    ValidationGroup="aa" OnClick="btnSubmit_Click" /> 
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
                                <table id="ExportTbl" class="table table-bordered display table-striped">
                                    <thead>
                                        <tr>
                                            <th>Sr. No.</th> 
                                            <th>District</th>
                                            <th>Party Category</th>
                                            <th>Party</th>
                                            <th>Station</th>
                                            <th>Powder</th>
                                            <th>Sale (Powder)</th>
                                            <th>Remaining (Powder)</th>
                                            <th>BAR/ TUB</th>
                                            <th>Sale (BAR/ TUB)</th>
                                            <th>Remaining (BAR/ TUB)</th>
                                            <th>Klean Bold Powder</th>
                                            <th>Sale (KBP)</th>
                                            <th>Remaining (KBP)</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                        <%--<asp:HiddenField ID="hddId" runat="server" Value='<%#Eval("TargetId") %>' />--%>
                                                    </td> 
                                                    <td style="text-align: left;"><%#Eval("District") %></td>
                                                    <td style="text-align: left;"><%#Eval("PartyCategory") %></td>
                                                    <td style="text-align: left;"><%#Eval("Party") %></td>
                                                    <td style="text-align: left;"><%#Eval("Station") %></td>
                                                    <td style="text-align: left;"><%#Eval("POWDER") %> </td>
                                                    <td style="text-align: left;"><%#Eval("SALE_POWDER") %></td>
                                                    <td style="text-align: left;"><%#Eval("Balance_POwder") %></td>
                                                    <td style="text-align: left;"><%#Eval("BAR_TUB") %> </td>
                                                    <td style="text-align: left;"><%#Eval("Sale_BAR_AND_TUB") %></td>
                                                    <td style="text-align: left;"><%#Eval("Balance_BT") %></td>
                                                    <td style="text-align: left;"><%#Eval("KLEAN_BOLD_POWDER") %> </td>
                                                    <td style="text-align: left;"><%#Eval("KLEAN_POWDER") %></td>
                                                    <td style="text-align: left;"><%#Eval("Balance_KBP") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr class="gradeA">
                                            <td></td> 
                                            <td></td>
                                            <td>Total</td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <input id="lblPowderTotal" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblSale_POWDER" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblBalance_POWDER" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblBar_Tub" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblSale_BAR_AND_TUB" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblBalance_BAR_AND_TUB" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblclean" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblKLEAN_POWDER" runat="server" value="0" readonly class="form-control" /></td>
                                            <td>
                                                <input id="lblBalanceKLEAN_POWDER" runat="server" value="0" readonly class="form-control" /></td>
                                        </tr>
                                    </tfoot>
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
                url: 'salestarget.aspx/ControlAccess',
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

