<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="EmployeeCategoryAssign.aspx.cs" Inherits="Soft_EmployeeCategoryAssign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Employee Category Assign (STAFP)</title>
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
        <h1>Category Assign (Employee)</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/EmployeeCategoryassign.aspx" class="active">Category Assign (Employee)</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">

                            <div class="form-group">

                                <div class="col-md-3">
                                    <label>Department</label>
                                    <asp:DropDownList ID="drpDepartment" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label>Designation</label>
                                    <asp:DropDownList ID="drpDesignation" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label>Status</label>
                                    <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="Active" Text="Active" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="Non-Active" Text="Non-Active"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label>Employee</label>
                                    <asp:DropDownList ID="drpProjectManager" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div> 
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-4">
                                <label>Category</label>
                                <asp:ListBox ID="drpCategory" runat="server" CssClass="form-control select2" SelectionMode="Multiple"></asp:ListBox>
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
                                                <th style="text-align: left;">Sr. No.</th>
                                                <th style="text-align: left;">Department</th>
                                                <th style="text-align: left;">Designation</th>
                                                <th style="text-align: left;">Emp Code</th>
                                                <th style="text-align: left;">Emp Name</th>
                                                <th style="text-align: left;">Rep Manager</th>
                                                <th style="text-align: left;">Catgory</th>
                                                <th style="text-align: left;">Assign</th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="gradeA">
                                            <td>
                                                <%#Container.ItemIndex+1 %>
                                                <asp:HiddenField ID="hddEnqID" Value='<%# Eval("EmpId")  %>' runat="server" />
                                                <asp:HiddenField ID="hddGroups" Value='<%# Eval("PARTYCATEGORYS")  %>' runat="server" />
                                            </td>
                                            <td style="text-align: left;"><%#Eval("DEPT_NAME") %></td>
                                            <td style="text-align: left;"><%#Eval("DESG_NAME") %></td>
                                            <td style="text-align: left;"><%#Eval("Emp_Code") %></td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblParty" runat="server" Text='<%#Eval("Emp_Name") %>'></asp:Label></td>
                                            <td style="text-align: left;"><%#Eval("REP_MANAGERNAME") %></td>
                                            <td style="text-align: left;"><%#Eval("PARTYCATEGORYS") %></td>
                                            <td style="text-align: left;">
                                                <asp:LinkButton ID="btnConfirmY" CommandArgument='<%# Eval("EmpId")  %>' CommandName="Confirm" runat="server" Style="padding: 1px 6px; font-size: 11px;" CssClass="btn btn-small btn-success fa fa-check" aria-label="View" OnClick="btnConfirmY_Click"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:LinkButton Text="" ID="lnkFake" runat="server" />
                                <asp:LinkButton Text="" ID="LinkButton1" runat="server" />
                                <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake"
                                    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <div class="header">
                                        <asp:Label ID="lblHead" runat="server"></asp:Label>
                                    </div>

                                    <div class="body">
                                        <asp:UpdatePanel ID="updd" runat="server">
                                            <ContentTemplate>
                                                <div class="row hidden">
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="lstGrp" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="lstGrp_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:CheckBox ID="ChkIte" runat="server" Text="Select All" onclick='javascript: SelectAllCheckboxes(this);' AutoPostBack="true" />
                                                    </div>
                                                </div>
                                                <asp:Repeater ID="repsku" runat="server">
                                                    <HeaderTemplate>
                                                        <table style="width: 100%; height: 500px; overflow-y: scroll; display: block; overflow-x: auto;">
                                                            <tr>
                                                                <th style="width: 10%;">Sr. No.</th>
                                                                <th style="vertical-align: middle; text-align: center; text-transform: none !important; width: 20%;" class="headerchk"></th>
                                                                <th style="width: 50%;">Category Name</th>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#Container.ItemIndex+1 %> 
                                                            </td>
                                                            <td style="vertical-align: middle; text-align: center; text-transform: none !important;" class="itemchk">
                                                                <asp:CheckBox ID="chkItems" runat="server" Checked='<%#Convert.ToBoolean(Eval("chk")) %>' OnCheckedChanged="chkItems_CheckedChanged" AutoPostBack="true" /></td>
                                                            <asp:HiddenField ID="hddMSNO" runat="server" Value='<%#Eval("MSNO") %>' />
                                                            <asp:HiddenField ID="hddPartyId" runat="server" Value='<%#Eval("PartyId") %>' />
                                                            <td>
                                                                <asp:Label ID="lblMSNAME" runat="server" Text='<%# Eval("MSNAME") %>' />
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

                                                <asp:AsyncPostBackTrigger ControlID="ChkIte" EventName="CheckedChanged" />
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
 <script type="text/javascript">
     //  const { each } = require("jquery");

     function SelectAllCheckboxes(spanChk) {
       
         // Added as ASPX uses SPAN for checkbox

         var oItem = spanChk.children;
         var theBox = (spanChk.type == "checkbox") ?
             spanChk : spanChk.children.item[0];
         xState = theBox.checked;
         elm = theBox.form.elements;
         var n = 0;
         for (i = 0; i < elm.length; i++)
             if (elm[i].type == "checkbox" &&
                 elm[i].id != theBox.id) {
                 //elm[i].click();

                 if (elm[i].checked != xState)
                     /*elm[i].click();*/
                     elm[i].checked = xState;

                 n++;
             }
         //document.getElementById("Body_lblchkno").innerHTML = n;
     }

 </script>



    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

