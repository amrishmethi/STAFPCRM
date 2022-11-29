<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SecondarySalesPartyMaster.aspx.cs" Inherits="Admin_SecondarySalesParty_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Secondary Sales Party(STAFP)</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header">
        <h1>ADD Secondary Sales Party
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/SecondarySalesPartyMaster.aspx" class="active">Secondary Sales Party</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="clearfix">&nbsp;</div>
                        <div class="col-md-4">
                            <label>Sales Station</label>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            InitialValue="0" Font-Bold="true" ForeColor="Red" Font-Size="Large" ControlToValidate="drpStation"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="drpStation" runat="server" CssClass="form-control select2"></asp:DropDownList>
                          </div>
                         
                        <div class="col-md-4">
                            <label>Sales Party</label>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                             Font-Bold="true" ForeColor="Red" Font-Size="Large" ControlToValidate="txtParty"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtParty" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>

                        <div class="clearfix">&nbsp;</div>     
                        <div class="clearfix">&nbsp;</div>     
                        <div class="col-md-4">
                            <label>Mobile No</label>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                             Font-Bold="true" ForeColor="Red" Font-Size="Large" ControlToValidate="txtMobile"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>    
                        
                      
                        <div class="col-md-4">
                            <label>WhatsApp No</label>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                             Font-Bold="true" ForeColor="Red" Font-Size="Large" ControlToValidate="txtWhatsApp"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtWhatsApp" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                        
                        <div class="clearfix">&nbsp;</div>
                        <div class="clearfix">&nbsp;</div>

                        <div class="box-body">

                            <div class="clearfix">&nbsp;</div>

                            <div class="col-md-12" style="text-align: center;">
                                <div class="box-footer">

                                    <asp:Button ID="btnSaveExit" runat="server" CssClass="btn btn-primary" Text="Save & Exit"
                                        OnClick="btnSaveExit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success" CausesValidation="false"
                                        Text="Back To List" OnClick="btnCancel_Click" />

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <div class="col-md-2 col-xs-4">
        <asp:Image ID="imagE1" runat="server" Height="40px" Width="40px" Visible="false" />
        <asp:Image ID="imagAdhar" runat="server" Height="40px" Width="40px" Visible="false" />
    </div>
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

