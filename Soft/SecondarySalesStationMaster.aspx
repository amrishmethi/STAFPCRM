<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="SecondarySalesStationMaster.aspx.cs" Inherits="Admin_SecondarySalesStation_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Secondary Sales Station(STAFP)</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header">
        <h1>ADD Secondary Sales Station
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/SecondarySalesStationMaster.aspx" class="active">Secondary Sales Station</a></li>
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
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                             Font-Bold="true" ForeColor="Red"  ControlToValidate="txtStation"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtStation" runat="server" CssClass="form-control"></asp:TextBox>
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

