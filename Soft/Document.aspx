<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="Document.aspx.cs" Inherits="Soft_Document" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Document (STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header">
        <h1>Add Document
        </h1>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="col-md-3 ">
                            <label>Document</label>
                            <asp:TextBox ID="txtDocumentname" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                Font-Bold="true" ForeColor="Red" Font-Size="Large" ValidationGroup="MM" ControlToValidate="txtDocumentname"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4 ">
                            <br />
                            <asp:Button ID="btnSaveExit" runat="server" CssClass="btn btn-primary" ValidationGroup="MM" Text="Save"
                                OnClick="btnSaveExit_Click" />
                        </div>
                    </div>
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="widget-content nopadding" style="overflow: auto;">
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">S No
                                            </th>
                                            <th style="text-align: left;">Document
                                            </th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repDocument" runat="server" OnItemCommand="repDocument_ItemCommand">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td style="text-align: left;"><%# Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("Docu_Name") %>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">

                                                        <a href="Document.aspx?id=<%#Eval("DOCU_ID") %>" style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese" aria-label="Edit" rel="lightbox"><i class="fa fa-pencil"></i></a>

                                                        <asp:LinkButton ID="lnkDelete" runat="server" Style="padding: 1px 6px; font-size: 11px;" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" CommandName="Delete" CssClass="btn btn-small btn-danger"
                                                            CommandArgument='<%#Eval("DOCU_ID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>
