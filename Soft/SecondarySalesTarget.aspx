<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SecondarySalesTarget.aspx.cs" Inherits="Soft_SecondarySalesTarget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Secondary Sales Target(STAFP)</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="eee" runat="server"></asp:ScriptManager>
    <section class="content-header">
        <h1>ADD Secondary Sales Target
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/SecondarySalesPartyMaster.aspx" class="active">Secondary Sales Target</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="clearfix">&nbsp;</div>
                        <div class="col-md-4">
                            <label>Employee</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" InitialValue="0" Font-Bold="true" ForeColor="Red" Font-Size="Large" ControlToValidate="drpEmployee"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="drpEmployee" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpEmployee_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <label>Apply Date</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Font-Bold="true" ForeColor="Red" Font-Size="Large" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datepicker2"></asp:TextBox>
                            <asp:HiddenField ID="hddMainId" runat="server" Value="0" />
                        </div>

                        <div class="clearfix">&nbsp;</div>
                        <div class="clearfix">&nbsp;</div>
                        <asp:UpdatePanel ID="updd" runat="server">
                            <ContentTemplate>
                                <div class="col-md-4">
                                    <label>Item Group</label>
                                    <asp:DropDownList ID="drpItemGrup" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" InitialValue="0" Font-Bold="true" ForeColor="Red" Font-Size="Large" ControlToValidate="drpItemGrup" ValidationGroup="txtt"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label>Min Qty</label>
                                    <span id="erro1r" style="color: Red; display: none" class="numerror">* Input digits (0 - 9)</span>

                                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" onkeypress="return IsNumeric(event,0);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" Font-Bold="true" ForeColor="Red" Font-Size="Large" ControlToValidate="txtQty" ValidationGroup="txtt"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label>Incentive Per Bag/Case</label><span id="error1" style="color: Red; display: none" class="numerror">* Input digits (0 - 9)</span>
                                    <asp:TextBox ID="txtIncentive" runat="server" CssClass="form-control" onkeypress="return IsNumeric(event,1);"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:HiddenField ID="HddRowID" runat="server" Value="0" />
                                    <br />
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success" ValidationGroup="txtt" OnClick="btnAdd_Click" Style="margin-top: 5px;" />
                                    <span id="errmsg" runat="server" style="color: Red; display: none"></span>
                                </div>
                                <div class="clearfix">&nbsp;</div>
                                <div class="col-md-12">

                                    <div class="table-responsive">
                                        <table id="ExportTbl" class="table table-bordered display" style="width: 100%">
                                            <thead>
                                                <tr class="gradeA" style="background-color: lightslategrey; color: white;">
                                                    <th>S No</th>
                                                    <th>Item Group</th>
                                                    <th>Min Qty</th>
                                                    <th>Incentive Per Bag/Case</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repData" runat="server" OnItemCommand="repData_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr runat="server" visible='<%#Eval("Delid").ToString()=="0"?true:false %>'>
                                                            <td><%#Container.ItemIndex+1 %>
                                                                <asp:HiddenField ID="hddID" runat="server" Value='<%#Eval("Id") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblItemGroup" runat="server" Text='<%#Eval("ItemGroup") %>'></asp:Label>
                                                                <asp:HiddenField ID="HddItemGroup" runat="server" Value=' <%#Eval("ItemGroupId") %>'></asp:HiddenField>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblQty" runat="server" Text=' <%#Eval("Qty") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblIncentive" runat="server" Text=' <%#Eval("Incentive") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="lnkRemove" runat="server" CommandName="Remove" Text="Remove" CommandArgument='<%#Eval("SNo") %>'></asp:LinkButton></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <div class="col-md-3">
                            <label>Min Visit</label>
                            <span id="error" style="color: Red; display: none" class="numerror">* Input digits (0 - 9)</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                Font-Bold="true" ForeColor="Red" Font-Size="Large" ControlToValidate="txtMinVisit" ValidationGroup="txttq"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtMinVisit" runat="server" CssClass="form-control" onkeypress="return IsNumeric(event,2);"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label>Amount</label>
                            <span id="error3" style="color: Red; display: none" class="error">* Input digits (0 - 9)</span>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                Font-Bold="true" ForeColor="Red" Font-Size="Large" ControlToValidate="txtMinVisit" ValidationGroup="txttq"></asp:RequiredFieldValidator>--%>
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" onkeypress="return IsDecimal(event,0);"></asp:TextBox>
                        </div>
                        <div class="col-md-6" style="text-align: center;">
                            <div class="box-footer">
                                <br />
                                <asp:Button ID="btnSaveExit" runat="server" CssClass="btn btn-primary" Text="Save & Exit" ValidationGroup="txttq"
                                    OnClick="btnSaveExit_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success" CausesValidation="false"
                                    Text="Back To List" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div> 
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
</asp:Content>
