<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="Payroll.aspx.cs" Inherits="Soft_Payroll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>HR Details</h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/AttendanceReport.aspx" class="active">HR Details </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12" style="border-bottom: .5px solid lightgrey;">
                    <h4 class="box-title">
                        <label>EMPLOYEE DETAILS</label>
                    </h4>

                    <div class="col-md-4">
                        <label class="control-label">
                            Company<span style="color: #ff0000">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DrpCompanies"
                            ErrorMessage=" Please Select Company" ValidationGroup="aa" InitialValue="0"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="DrpCompanies" runat="server" CssClass="form-control select2">
                        </asp:DropDownList>

                        <asp:HiddenField ID="hddprodid" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Associate Code
                                    <span style="color: #ff0000">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAssociatecode"
                            ErrorMessage=" Please Enter Associate code" ValidationGroup="aa"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtAssociatecode" runat="server" CssClass="form-control"></asp:TextBox>


                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Associate Name <span style="color: #ff0000">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                            ErrorMessage=" Enter Associate Name" ValidationGroup="aa" ControlToValidate="txtemployeename"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtemployeename" runat="server" CssClass="form-control"></asp:TextBox>


                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Department<span style="color: #ff0000">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpCategory"
                            ErrorMessage="Please Select Department" ValidationGroup="aa" InitialValue="0"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="drpCategory" runat="server" CssClass="form-control select2">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Designation<span style="color: #ff0000">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpSubCategory"
                            ErrorMessage="Please Select Designation" ValidationGroup="aa" InitialValue="0"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="drpSubCategory" runat="server" CssClass="form-control select2" AutoPostBack="true"
                            OnSelectedIndexChanged="drpSubCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Reporting Manager
                        </label>
                        <asp:DropDownList ID="drpProjectManager" runat="server" CssClass="form-control select2">
                        </asp:DropDownList>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Date of Joining    <span style="color: #ff0000">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ForeColor="Red"
                            ErrorMessage=" Enter Date of Joining" ControlToValidate="txtdat2" InitialValue="0"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtdat2" runat="server" CssClass="form-control datepicker">
                        </asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Date of Leaving
                        </label>
                        &nbsp;<asp:CheckBox ID="chkDOL" runat="server" OnCheckedChanged="chkDOL_CheckedChanged" AutoPostBack="true" />
                        <asp:TextBox ID="txtDateOfLeaving" runat="server" CssClass="form-control datepicker" Enabled="false">
                        </asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            PAN No
                        </label>
                        <asp:TextBox ID="txtpanno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">
                            PF Code No.
                        </label>
                        <asp:CheckBox ID="chkBasic" runat="server" OnCheckedChanged="chkBasic_CheckedChanged" AutoPostBack="true" />
                        <asp:TextBox ID="txtPFCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            ESI Code No.
                        </label>
                        <asp:CheckBox ID="ChkEsi" runat="server" OnCheckedChanged="ChkEsi_CheckedChanged" AutoPostBack="true" />
                        <asp:TextBox ID="txtESICode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">User Name<span style="color: #ff0000">*</span></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtusername"
                            ErrorMessage="Enter User Name" ValidationGroup="aa"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtusername" runat="server" CssClass="form-control"></asp:TextBox>

                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Password <span style="color: #ff0000">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtpassword"
                            ErrorMessage="Enter Password" ValidationGroup="aa" EnableClientScript="true"
                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtpassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>

                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Upload Photo</label>
                        <asp:FileUpload ID="imageupload" runat="server" />
                        <asp:HiddenField ID="hddimg1" runat="server" />
                        <asp:Image ID="Image1" runat="server" Width="70px" />
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Current Status</label><span style="color: #ff0000">*</span>
                        <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control select2">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Active</asp:ListItem>
                            <asp:ListItem Value="2">Non-Active</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12" style="border-bottom: .5px solid lightgrey;">
                    <h4 class="box-title">
                        <label>BANK DETAILS</label>
                    </h4>

                    <div class="col-md-3">
                        <label class="control-label">
                            Bank Name
                        </label>
                        <asp:TextBox ID="txtempbank" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">
                            Bank Account No
                        </label>

                        <asp:TextBox ID="txtaccountno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">
                            Branch Name & Address
                        </label>
                        <asp:TextBox ID="txtbankaddress" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">
                            IFSC Code</label>

                        <asp:TextBox ID="txtIFSC" runat="server" CssClass=" form-control"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-3">
                        <label class="control-label">
                            Bank Name</label>
                        <asp:TextBox ID="txtBank2Name" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">
                            Bank Account No
                        </label>
                        <asp:TextBox ID="txtBank2Accno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">
                            Branch Name & Address
                        </label>
                        <asp:TextBox ID="txtBank2Bankaddr" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">
                            IFSC Code</label>
                        <asp:TextBox ID="txtBank2IFSC" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                </div>
            </div>
        </div>

        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12" style="border-bottom: .5px solid lightgrey;">
                    <h4 class="box-title">
                        <label>SALARY  DETAILS</label>
                    </h4>
                    <div class="col-md-4">
                        <label class="control-label">
                            Basic Salary</label>
                        <asp:TextBox ID="txtsalary" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            House Rent Allowance</label>
                        &nbsp;<asp:CheckBox ID="chkHRA" runat="server" OnCheckedChanged="chkHRA_CheckedChanged" AutoPostBack="true" />
                        <asp:TextBox ID="txtHRA" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Washing Allowance
                        </label>
                        &nbsp;<asp:CheckBox ID="ChkWs" runat="server" OnCheckedChanged="ChkWs_CheckedChanged" AutoPostBack="true" />
                        <asp:TextBox ID="TxtwashingAllowance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Medical Allowance</label>
                        &nbsp;<asp:CheckBox ID="chkMA" runat="server" AutoPostBack="true" OnCheckedChanged="chkMA_CheckedChanged" />
                        <asp:TextBox ID="txtMediacl" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Conveyance Allowance</label>
                        &nbsp;<asp:CheckBox ID="chkConv" runat="server" AutoPostBack="true" OnCheckedChanged="chkConv_CheckedChanged" />
                        <asp:TextBox ID="txtConveyance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Laptop Allowance
                        </label>
                        &nbsp;&nbsp;<asp:CheckBox ID="chkDP" runat="server" AutoPostBack="true"  />
                        <asp:TextBox ID="txtDPay" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Food Allowance</label>
                        &nbsp;<asp:CheckBox ID="chkFoodAll" runat="server" AutoPostBack="true" />
                        <asp:TextBox ID="txtFoodAll" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Transport Allowance</label>
                        &nbsp;<asp:CheckBox ID="chkTransportAll" runat="server" AutoPostBack="true" />
                        <asp:TextBox ID="txtTransPortAll" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Other Allowance
                        </label>
                        &nbsp;<asp:CheckBox ID="chkOthers" runat="server" AutoPostBack="true" />
                        <asp:TextBox ID="txtOthers" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">
                            T.D.S.</label>
                        &nbsp;<asp:CheckBox ID="chktdsapply" runat="server" AutoPostBack="true" />
                        <asp:TextBox ID="txtTDS" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Other Deductions</label>
                        &nbsp;<asp:CheckBox ID="chkDeductOther" runat="server" AutoPostBack="true" />
                        <asp:TextBox ID="txtDeductOther" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                    </div>

                    <div class="clearfix">&nbsp;</div>
                    <div class="clearfix">&nbsp;</div>
                </div>
            </div>
        </div>

        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12" style="border-bottom: .5px solid lightgrey;">
                    <h4 class="box-title">
                        <label>PESONAL DETAILS</label>
                    </h4>
                    <div class="col-md-4">
                        <label class="control-label">
                            Gender <span style="color: #ff0000">*</span></label>
                        <asp:DropDownList ID="drpGender" runat="server" CssClass="form-control select2" AutoPostBack="true"
                            OnSelectedIndexChanged="drpGender_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="Male">Male</asp:ListItem>
                            <asp:ListItem Value="Female">Female</asp:ListItem>
                        </asp:DropDownList>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div id="ShowMaternity" runat="server" visible="false" class="control-group">
                                    <label class="control-label">
                                        Maternity Leave</label>
                                    <div class="controls  controls-row">
                                        <asp:TextBox ID="TxtMaternityLeave" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="drpGender" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Date of Birth <span style="color: #ff0000">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtdat1"
                            ErrorMessage="Enter Date of Birth" ValidationGroup="aa"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtdat1" runat="server" CssClass="form-control datepicker">
                        </asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Status
                        </label>
                        <asp:DropDownList ID="drpMarriedStatus" runat="server" CssClass="form-control select2">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="Single">Single</asp:ListItem>
                            <asp:ListItem Value="Married">Married</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Education
                        </label>
                        <asp:TextBox ID="txtEducation" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Date of Marriage
                        </label>
                        <asp:TextBox ID="txtdat4" runat="server" CssClass="form-control datepicker"></asp:TextBox>

                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Phone Number
                        </label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtphoneno"
                            ValidationExpression="^\d+$" ErrorMessage="Only numbers Allowed"></asp:RegularExpressionValidator>

                        <asp:TextBox ID="txtphoneno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>

                    <div class="col-md-4">
                        <label class="control-label">
                            Mobile Number
                        </label>
                        <span style="color: #ff0000">*</span>

                        <asp:TextBox ID="txtmobno" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtmobno"
                            ErrorMessage="Enter Mobile Number" ValidationGroup="aa"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="reg1" runat="server" ControlToValidate="txtmobno"
                            ValidationExpression="^\d+$" ErrorMessage="Only numbers Allowed"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            CUG Mobile Number <span style="color: #ff0000">&nbsp;&nbsp;</span>
                        </label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCUG"
                            ValidationExpression="^\d+$" ErrorMessage="Only numbers Allowed"></asp:RegularExpressionValidator>

                        <asp:TextBox ID="txtCUG" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>


                    <div class="col-md-4">
                        <label class="control-label">
                            Personal Email ID <span style="color: #ff0000">*</span>
                        </label>

                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtemail"
                            ErrorMessage="Enter Email ID" ValidationGroup="aa"></asp:RequiredFieldValidator>
                        &nbsp;<asp:RegularExpressionValidator ID="Rev1" runat="server" ErrorMessage="Enter Valid Email Id"
                            ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                    </div>

                    <div class="col-md-4">
                        <label class="control-label">
                            Office Email ID  <span style="color: #ff0000">*</span>
                        </label>
                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                            ErrorMessage="Enter Valid Email Id" ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtOfficEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <asp:UpdatePanel ID="updtAddress" runat="server">
                        <ContentTemplate>
                            <div class="col-md-4">
                                <label class="control-label">
                                    Present Address  <span style="color: #ff0000">*</span>
                                </label>
                                <asp:CheckBox ID="ChksameAddr" runat="server" AutoPostBack="True" OnCheckedChanged="ChksameAddr_CheckedChanged" Text="If Permanent Address Is Same" Style="font-family: Calibri; font-size: x-small;" />
                                <asp:TextBox ID="txtpresentaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtpresentaddress"
                                    ErrorMessage="Enter Present Address" ValidationGroup="aa"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <label class="control-label">
                                    Permanent Address <span style="color: #ff0000">*</span>
                                </label>
                                <asp:TextBox ID="txtparmentaddress" runat="server" CssClass="form-control"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtparmentaddress"
                                    ErrorMessage="Enter Permanent Address" ValidationGroup="aa"></asp:RequiredFieldValidator>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>


        <div class="clearfix">&nbsp;</div>
        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12" style="border-bottom: .5px solid lightgrey;">
                    <h4 class="box-title">
                        <label>Family Details</label>
                    </h4>

                    <div class="col-md-4">
                        <label class="control-label">
                            Father's Name <span style="color: #ff0000">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtfathername"
                            ErrorMessage="Enter Father's Name" ValidationGroup="aa"></asp:RequiredFieldValidator>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="txtfathername" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Mother's Name
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="TxtFemales" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Spouse
                        </label>
                        <div class="controls  controls-row">
                            <asp:DropDownList ID="drpSpouse" runat="server" CssClass="form-control select2">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="Husband">Husband</asp:ListItem>
                                <asp:ListItem Value="Wife">Wife</asp:ListItem>
                                <asp:ListItem Value="None">None</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Spouse Name
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="txtSpouse" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            No.Of Children
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="TxtChild" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Contact No.Of Family<span style="color: #ff0000">*</span>
                        </label>
                        <asp:TextBox ID="txtFatherCntctNo" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtFatherCntctNo"
                            ErrorMessage="Enter Family's Contact Detail" ValidationGroup="aa"></asp:RequiredFieldValidator>

                    </div>
                </div>
            </div>
        </div>
        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12" style="border-bottom: .5px solid lightgrey;">
                    <h4 class="box-title">
                        <label>Previous Company Details</label>
                    </h4>


                    <div class="col-md-4">
                        <label class="control-label">
                            Company Name
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Company Address
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="TxtCompanyAdd" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Last Drawn CTC
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="TxtCtc" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">
                            PF Account No.
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="TxtPfNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Reference Name(1)
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="txtrefname" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Reference Contact Number(1)
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="txtrefcontactno" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Reference Name(2)
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="TxtSecRefName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Reference Contact Number(2)
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="TxtSecRefContactNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="clearfix">&nbsp;</div>
                </div>
            </div>
        </div>

        <div class="box-body">
            <div class="col-md-12">
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Save & Exit"
                    ValidationGroup="aa" OnClick="btnSubmit_Click" />

                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel"
                    OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
</asp:Content>

