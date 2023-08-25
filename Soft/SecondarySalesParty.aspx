<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SecondarySalesParty.aspx.cs" Inherits="Admin_SecondarySalesParty" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Secondary Sales Party Report(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1><a id="lnkAdd" runat="server" href="/Soft/SecondarySalesPartyMaster.aspx" class="btn btn-primary">Add Secondary Sales Party</a>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/SecondarySalesParty.aspx" class="active">Secondary Sales Party List </a></li>
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpUser"
                                    ErrorMessage="Must Be Select" ValidationGroup="A11" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpUser" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>HeadQtr<span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpHeadqtr1"
                                    ErrorMessage="Must Be Select" ValidationGroup="A1" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpHeadqtr1" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpHeadqtr1_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>District<span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpheadQtr"
                                    ErrorMessage="Must Be Select" ValidationGroup="A1" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpheadQtr" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpStation_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Station</label>
                                <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpStation_SelectedIndexChanged1" AutoPostBack="true">
                                </asp:DropDownList>
                            </div> 
                            <div class="col-md-2">
                                <label>Beat</label>
                                <asp:DropDownList ID="drpBeat1" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <label>Party</label>
                                <asp:DropDownList ID="drpParty" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="tnSearch" runat="server" CssClass="btn btn-success" Text="Get Data" ValidationGroup="A1" OnClick="tnSearch_Click" />
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-3">
                                <label>Beat</label>
                                <asp:DropDownList ID="drpBeat" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="cold-md-3">
                                <br />
                                <asp:Button ID="btnUpdateBeat" runat="server" CssClass="btn btn-success" Text="Update Beat" OnClick="btnUpdateBeat_Click" />
                            </div>
                        </div>

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
                                        <th style="text-align: left;">Sr. No.</th>
                                        <th style="text-align: left;">
                                            <input type='checkbox' id='chkAll' runat='server' onclick='javascript: SelectAllCheckboxes(this);' /></th>
                                        <th style="text-align: left;">District</th>
                                        <th style="text-align: left;">Station</th>
                                        <th style="text-align: left;">Beat</th>
                                        <th style="text-align: left;">Party</th>
                                        <th style="text-align: left;">Mobile No</th>
                                        <th style="text-align: left;">WhatsApp No</th>
                                        <th>
                                            <label id="lblAction">Action</label></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rep" runat="server" OnItemCommand="rep_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="gradeA">
                                                <td>
                                                    <%#Container.ItemIndex+1 %>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk" runat="server" />
                                                    <asp:HiddenField ID="hddPartyId" runat="server" Value='<%#Eval("ID") %>' />
                                                </td>
                                                <%--     <asp:Label ID="lblItem" runat="server" Text='<%#Eval("StationName") %>'></asp:Label>--%>

                                                <td style="text-align: left;"><%#Eval("District") %></td>
                                                <td style="text-align: left;"><%#Eval("StationName") %></td>
                                                <td style="text-align: left;"><%#Eval("Beat") %></td>
                                                <td style="text-align: left;"><%#Eval("Name") %></td>
                                                <td style="text-align: left;"><%#Eval("MobileNo") %></td>
                                                <td style="text-align: left;"><%#Eval("WhatsUpMobileNo") %></td>

                                                <td style="text-align: left;">
                                                    <div class="isEditVisible" style="display: inline;">
                                                        <a href="SecondarySalesPartyMaster.aspx?id=<%#Eval("ID") %>" style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese" aria-label="Edit" rel="lightbox"><i class="fa fa-pencil"></i></a>
                                                    </div>
                                                    <div class="isDelVisible" style="display: inline;">
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Style="padding: 1px 6px; font-size: 11px;" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" CommandName="Delete" CssClass="btn btn-small btn-danger"
                                                            CommandArgument='<%#Eval("ID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
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



    </script>

    <script type="text/javascript">
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox

            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
                spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
                    elm[i].id != theBox.id) {
                    //elm[i].click();

                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;

                }
        }
    </script>
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

