<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="AddUserRoles.aspx.cs" Inherits="Admin_AddUserRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <div class="advanced-form-area mg-b-15">
        <div class="container-fluid">
            <div class="row">

                <div id="adminpro-register-form" class="adminpro-form">
                    <div class="col-lg-12">
                        <div class="sparkline13-list shadow-reset">
                            <div class="sparkline13-hd">
                                <div class="main-sparkline13-hd">
                                    <h1><span class="table-project-n">Assign Role to </span><asp:Label ID ="lbluser" runat="server"></asp:Label></h1>
                                </div>
                            </div>
                            <div class="login-bg">
                                <div class="sparkline11-graph">
                                    <div class="input-knob-dial-wrap">

                                        <asp:GridView ID="GRD" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                                            Width="100%" GridLines="Both" CellSpacing="0" EmptyDataText="No Record Found" CellPadding="3" OnRowDataBound="GRD_RowDataBound">
                                            <RowStyle CssClass="grdsubheading" />
                                            <AlternatingRowStyle CssClass="grdsubheadingalter" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" />
                                                        <asp:Label ID="lblPID" runat="server" Text='<%#Eval("MenuId") %>' Visible="false"></asp:Label>

                                                        <%#Eval("MenuName") %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:GridView ID="GRDInner" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                                                            Width="100%" GridLines="None" CellSpacing="0" EmptyDataText="No Record Found" CellPadding="3" OnRowDataBound="GRDInner_RowDataBound">
                                                            <RowStyle CssClass="grdsubheading" />
                                                            <AlternatingRowStyle CssClass="grdsubheadingalter" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chk1" runat="server" />
                                                                        <asp:Label ID="lblPID1" runat="server" Text='<%#Eval("MenuId") %>' Visible="false"></asp:Label>
                                                                        <asp:HiddenField ID="hddmenupage" runat="server" Value=' <%#Eval("MenuName") %>' />
                                                                        <%#Eval("MenuName") %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkAdd" runat="server" />
                                                                        <asp:Label ID="lblPIDAdd" runat="server" Text="Add"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkView" runat="server" />
                                                                        <asp:Label ID="lblPIDView" runat="server" Text="Print"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkEdit" runat="server" />
                                                                        <asp:Label ID="lblPIDEdit" runat="server" Text="Edit"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                                                        <asp:Label ID="lblPIDDel" runat="server" Text="Delete"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkAssign" runat="server" />
                                                                        <asp:Label ID="lblPIDAss" runat="server" Text="Assign"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkLogin" runat="server" />
                                                                        <asp:Label ID="lblPIDLgn" runat="server" Text="CRM Login"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="row">
                                            <div class="col-lg-4"></div>
                                            <div class="col-lg-8">
                                                <div class="login-button-pro">
                                                    <asp:Button ID="btnSaveExit" runat="server" CssClass="btn btn-primary" Text="Save"
                                                        OnClick="btnSaveExit_Click" />
                                                    <a href="UserAssignReport.aspx" class="btn btn-custon-four btn-warning">Back To List</a>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
</asp:Content>

