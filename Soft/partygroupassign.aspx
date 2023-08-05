<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="partygroupassign.aspx.cs" Inherits="Soft_PartyAssign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Party Assign (STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
    <style>
        .zoom:hover {
            -ms-transform: scale(4); /* IE 9 */
            -webkit-transform: scale(4); /* Safari 3-8 */
            transform: scale(4);
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border: 3px solid #0DA9D0;
            width: 600px;
            height: 600px;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                padding: 5px;
                height: 530px;
            }

            .modalPopup .footer {
                padding: 3px;
            }

            .modalPopup .button {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

            .modalPopup td {
                text-align: left;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="scpt1" runat="server"></asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>Group Assign (Party)</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/PartyAssign.aspx" class="active">Group Assign (Party)</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-2 hidden">
                                <label>HeadQuarter</label>
                                <asp:DropDownList ID="drpheadQtr" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpheadQtr_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>District</label>
                                <asp:DropDownList ID="drpDistrict" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDistrict_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Station</label>
                                <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Party Category </label>
                                <asp:DropDownList ID="drpCatg" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label>Group</label>
                                <asp:ListBox ID="drpGrp" runat="server" CssClass="form-control select2" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Get Data" OnClick="btnSearch_Click" />
                            </div>
                            <div class="clearfix">&nbsp;</div>
                        </div>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-body">
                        <div class="widget-content">
                            <div class="table-responsive">
                                <asp:Repeater ID="rep" runat="server">
                                    <HeaderTemplate>
                                        <table id="ExportTbl" class="table table-bordered display table-striped">
                                            <tr>
                                                <th>Sr. No.</th>
                                                <th>District</th>
                                                <th>Station</th>
                                                <th>Party Category</th>
                                                <th>Party</th>
                                                <th>Group</th>
                                                <th>Assign</th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="gradeA">
                                            <td>
                                                <%#Container.ItemIndex+1 %>
                                                <asp:HiddenField ID="hddEnqID" Value='<%# Eval("ID")  %>' runat="server" />
                                                <asp:HiddenField ID="hddGroups" Value='<%# Eval("ItemGroup")  %>' runat="server" />
                                            </td>
                                            <td style="text-align: left;"><%#Eval("District") %></td>
                                            <td style="text-align: left;"><%#Eval("Station") %></td>
                                            <td style="text-align: left;"><%#Eval("PartyCategory") %></td>
                                            <td style="text-align: left;"><%#Eval("Party") %></td>
                                            <td style="text-align: left;"><%#Eval("ItemGroups") %></td>
                                            <td style="text-align: left;">
                                                <asp:LinkButton ID="btnConfirmY" CommandArgument='<%# Eval("ID")  %>' CommandName="Confirm" runat="server" Style="padding: 1px 6px; font-size: 11px;" CssClass="btn btn-small btn-success fa fa-check" aria-label="View" OnClick="btnConfirmY_Click"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:LinkButton Text="" ID="lnkFake" runat="server" />
                                <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake"
                                    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <div class="header">
                                        Group
                                    </div>

                                    <div class="body">
                                        <asp:UpdatePanel ID="updd" runat="server">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <div class="col-md-6">
                                                        <label>Group</label>
                                                        <asp:DropDownList ID="lstGrp" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="lstGrp_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <asp:Repeater ID="repsku" runat="server">
                                                    <HeaderTemplate>
                                                        <table id="ExportTbl" style="width: 100%; height: 500px; overflow-y: scroll; display: block; overflow-x: auto;">
                                                            <tr>
                                                                <th style="width: 10%;">Sr. No.</th>
                                                                <th style="vertical-align: middle; text-align: center; text-transform: none !important; width: 15%;" class="headerchk"></th>
                                                                <th style="width: 20%;">Group Name</th>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#Container.ItemIndex+1 %> 
                                                            </td>
                                                            <td style="vertical-align: middle; text-align: center; text-transform: none !important;" class="itemchk">
                                                                <asp:CheckBox ID="chkItems" runat="server" Checked='<%#Convert.ToBoolean(Eval("chk")) %>' OnCheckedChanged="chkItems_CheckedChanged" /></td>
                                                            <asp:HiddenField ID="hddCmsCode" runat="server" Value='<%#Eval("CmsCode") %>' />
                                                            <asp:HiddenField ID="hddPartyId" runat="server" Value='<%#Eval("PartyId") %>' />
                                                            <td>
                                                                <asp:Label ID="lblLotNo" runat="server" Text='<%# Eval("CMSName") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="lstGrp" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="footer" align="right">
                                        <asp:Button ID="btnApply" runat="server" Text="Apply Changes" CssClass="btn btn-sm btn-primary" OnClick="btnApply_Click" />
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-sm btn-default" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
    var cksku =""; 
     

        
    <%--<script type="text/javascript">
        var controlid = "";
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);
        function prm_InitializeRequest(sender, args) {
            $('.select2').select2("destroy");
        }
        function prm_EndRequest(sender, args) {
            $('.select2').addClass('select2');
        }
    </script>--%>
    <script type="text/javascript">
        debugger
        var __count = 0;
        $(document).ready(function () {

            var chkAll = $('.headerchk :checkbox');
            var $checkboxes = $('.itemchk :checkbox');
            chkAll.change(function () {
                debugger
                //Check header and item's checboxes on click of header checkbox

                if (chkAll.is(':checked')) {
                    $checkboxes.attr('checked', 'checked');
                    __count = $checkboxes.length;
                }
                else {
                    $checkboxes.removeAttr('checked');
                }
                //chkItem.prop('checked', $(this).is(':checked'));
            });
            var chkItem = $(".itemchk").change(function () {
                debugger
                //If any of the item's checkbox is unchecked then also uncheck header's checkbox
                chkAll.prop('checked', chkItem.filter(':not(:checked)').length == 0);
                __count = 0;
                for (var i = 0; i < $checkboxes.length; i++) {
                    if ($checkboxes[i].checked) {
                        __count++;
                    }

                }
            });
        });
    </script>
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

