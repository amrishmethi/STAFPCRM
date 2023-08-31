<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="AddAdminPolicy.aspx.cs" Inherits="Soft_Add_AdminPolicy" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Admin Policy (STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header">
        <h1>Add Admin Policy
        </h1>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="col-md-2 ">
                            <label>Date</label>
                            <asp:TextBox ID="dpDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                Font-Bold="true" ForeColor="Red"  ValidationGroup="MM" ControlToValidate="dpDate"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6 ">
                            <label>Policy Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                Font-Bold="true" ForeColor="Red"  ValidationGroup="MM" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-8 ">
                            <label>Policy Description</label>
                            <%--<asp:TextBox ID="txtPolicyname" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>--%>
                          <CKEditor:CKEditorControl ID="txtPolicyDesc" BasePath="../ckeditor/" runat="server"></CKEditor:CKEditorControl>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                Font-Bold="true" ForeColor="Red"  ValidationGroup="MM" ControlToValidate="txtPolicyDesc"></asp:RequiredFieldValidator>
                        </div>
                        <div class="clearfix">&nbsp;</div>
                        <div class="col-md-1 ">
                            <br />
                            <asp:Button ID="btnSaveExit" runat="server" CssClass="btn btn-primary" ValidationGroup="MM" Text="Save"
                                OnClick="btnSaveExit_Click" />
                              <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success" CausesValidation="false"
                                        Text="Back To List" OnClick="btnCancel_Click" />
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
