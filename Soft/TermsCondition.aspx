<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="TermsCondition.aspx.cs" Inherits="Soft_TermsCondition" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Terms Condition(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Terms Condition 
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/TermsCondition.aspx" class="active">Terms Condition</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-9">
                                <label>Terms Condition</label>
                                <CKEditor:CKEditorControl ID="txtDiscription" BasePath="../ckeditor/" runat="server"></CKEditor:CKEditorControl>
                            </div>
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>Heading<span style="color: #ff0000">*</span></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHeading"
                                            ErrorMessage="Must Be Enter" ValidationGroup="A" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtHeading" runat="server" CssClass="form-control">
                                        </asp:TextBox>
                                    </div>
                                    <div class="clearfix">&nbsp;</div>
                                    <div class="col-md-12">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="A" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger" Text="Back To List" OnClick="btnBack_Click" />
                                    </div>
                                </div>

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
                                            <th style="text-align: left;">Heading</th>
                                            <th style="text-align: left;">Description</th>
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
                                                    <td style="text-align: left;"><%#Eval("Heading") %></td>
                                                    <td style="text-align: left;"><%#Eval("Description") %></td>

                                                    <td style="text-align: left;">
                                                        <div class="isEditVisible" style="display: inline;">
                                                            <a href="TermsCondition.aspx?id=<%#Eval("Id") %>" style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese" aria-label="Edit" rel="lightbox"><i class="fa fa-pencil"></i></a>
                                                        </div>
                                                        <div class="isDelVisible" style="display: inline;">
                                                            <asp:LinkButton ID="lnkDelete" runat="server" Style="padding: 1px 6px; font-size: 11px;" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" CommandName="Delete" CssClass="btn btn-small btn-danger"
                                                                CommandArgument='<%#Eval("Id") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>
