<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Admin_Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header">
        <h1>Add Menu
        </h1>
    </section>
    <section class="content">
        <div class="row">
            <div class="box box-primary">
                <div class="box-body">

                    <div class="col-md-3 ">
                        <label>Menu Name</label>
                        <asp:TextBox ID="txtMenuName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            Font-Bold="true" ForeColor="Red" Font-Size="Large" ValidationGroup="MM" ControlToValidate="txtMenuName"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-3">
                        <label>Parent Menu</label>
                        <asp:DropDownList ID="drpLevel" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="col-md-3">
                        <label>Link</label>
                        <asp:TextBox ID="txtLink" runat="server" CssClass="form-control" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                            Font-Bold="true" ForeColor="Red" Font-Size="Large" ValidationGroup="MM" ControlToValidate="txtLink"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-2">
                        <label>Position</label>
                        <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control" onkeypress="return IsNumeric(event,0);"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                            Font-Bold="true" ForeColor="Red" Font-Size="Large" ValidationGroup="MM" ControlToValidate="txtPosition"></asp:RequiredFieldValidator>
                        <span id="error" style="color: Red; display: none" class="numerror">* Input digits (0 - 9)</span>
                    </div>
                    <div class="box-body">
                        <div class="">
                            <div class="col-md-12" style="text-align: center;">
                                <asp:Button ID="btnSaveExit" runat="server" CssClass="btn btn-primary" ValidationGroup="MM" Text="Save"
                                    OnClick="btnSaveExit_Click" />
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-12" style="text-align: center;">
                                <div class="box-footer">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="widget-content nopadding" style="overflow: auto;">
                            <table id="example1" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th style="text-align: left;">Menu
                                        </th>
                                        <th style="text-align: left;">Parent Menu
                                        </th>
                                        <th style="text-align: left;">Link
                                        </th>
                                        <th style="text-align: left;">Position
                                        </th>

                                        <th>Action</th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repMenuList" runat="server" OnItemCommand="repMenuList_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="gradeA">
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblMenu" runat="server" Text='<%#Eval("MenuName") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label6" runat="server" Text='<%#Eval("Parent") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("MenuLink") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Position") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: left;">

                                                    <a href="Menu.aspx?id=<%#Eval("MenuId") %>" style="padding: 1px 6px; font-size: 11px;" class="btn btn-small btn-primary rolese" aria-label="Edit" rel="lightbox"><i class="fa fa-pencil"></i></a>

                                                    <asp:LinkButton ID="lnkDelete" runat="server" Style="padding: 1px 6px; font-size: 11px;" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" CommandName="Delete" CssClass="btn btn-small btn-danger"
                                                        CommandArgument='<%#Eval("MenuId") %>'><i class="fa fa-trash-o"></i></asp:LinkButton> 
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
    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
    <script>
        function myFunction() {
            /* Get the text field */
            var copyText = document.getElementById("Body_txtContactInfo");

            /* Select the text field */
            copyText.select();
            copyText.setSelectionRange(0, 99999); /* For mobile devices */

            /* Copy the text inside the text field */
            navigator.clipboard.writeText(copyText.value);

            /* Alert the copied text */
            // alert("Copied the text: " + copyText.value);
        }
        function RelativeContact() {
            /* Get the text field */
            var copyText = document.getElementById("Body_txtRelativeContactNo");

            /* Select the text field */
            copyText.select();
            copyText.setSelectionRange(0, 99999); /* For mobile devices */

            /* Copy the text inside the text field */
            navigator.clipboard.writeText(copyText.value);

            /* Alert the copied text */
            // alert("Copied the text: " + copyText.value);
        }
    </script>
</asp:Content>

