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
                            Current Status</label><span style="color: #ff0000">*</span>
                        <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control select2">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Active</asp:ListItem>
                            <asp:ListItem Value="2">Non-Active</asp:ListItem>
                        </asp:DropDownList>
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
                <asp:UpdatePanel ID="updt" runat="server">
                    <ContentTemplate>

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
                                <asp:CheckBox ID="chkHRA" runat="server" OnCheckedChanged="chkHRA_CheckedChanged" AutoPostBack="true" />
                                &nbsp; &nbsp;<label class="control-label">House Rent Allowance</label>
                                &nbsp; &nbsp;<asp:RadioButton ID="rbHRAFixed" runat="server" Text="Fixed" GroupName="HRA" Checked="true" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbHRAPer" runat="server" Text="Per(%)" GroupName="HRA" />
                                <asp:TextBox ID="txtHRA" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="ChkWs" runat="server" OnCheckedChanged="ChkWs_CheckedChanged" AutoPostBack="true" />
                                &nbsp; &nbsp;<label class="control-label">Washing Allowance</label>
                                &nbsp; &nbsp;<asp:RadioButton ID="RbWAFixed" runat="server" Text="Fixed" GroupName="WA" Checked="true" />
                                &nbsp; &nbsp;<asp:RadioButton ID="RbWAPer" runat="server" Text="Per(%)" GroupName="WA" />
                                <asp:TextBox ID="TxtwashingAllowance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkMA" runat="server" AutoPostBack="true" OnCheckedChanged="chkMA_CheckedChanged" />
                                &nbsp; &nbsp;<label class="control-label">Medical Allowance</label>
                                &nbsp; &nbsp;<asp:RadioButton ID="rbMAFixed" runat="server" Text="Fixed" GroupName="MA" Checked="true" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbMAPer" runat="server" Text="Per(%)" GroupName="MA" />
                                <asp:TextBox ID="txtMediacl" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkConv" runat="server" AutoPostBack="true" OnCheckedChanged="chkConv_CheckedChanged" />
                                &nbsp; &nbsp;<label class="control-label">Conveyance Allowance</label>
                                &nbsp; &nbsp;<asp:RadioButton ID="RbTAFixed" runat="server" Text="Fixed" GroupName="TA" Checked="true" />
                                &nbsp; &nbsp;<asp:RadioButton ID="RbTAPerKM" runat="server" Text="Per KM" GroupName="TA" />

                                <asp:TextBox ID="txtConveyance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkDP" runat="server" AutoPostBack="true" OnCheckedChanged="chkDP_CheckedChanged" />
                                &nbsp; &nbsp;<label class="control-label">Laptop Allowance</label>
                                &nbsp; &nbsp;<asp:RadioButton ID="rbDPFixed" runat="server" Text="Fixed" GroupName="DP" Checked="true" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbDPPer" runat="server" Text="Per(%)" GroupName="DP" />
                                <asp:TextBox ID="txtDPay" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkFoodAll" runat="server" AutoPostBack="true" />
                                &nbsp; &nbsp;<label class="control-label">Food Allowance</label>
                                &nbsp; &nbsp;<asp:RadioButton ID="rbFAFixed" runat="server" Text="Fixed" GroupName="FA" Checked="true" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbFAPer" runat="server" Text="Per(%)" GroupName="FA" />
                                <asp:TextBox ID="txtFoodAll" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkOthers" runat="server" AutoPostBack="true" />
                                &nbsp; &nbsp;<label class="control-label">Other Allowance</label>
                                &nbsp; &nbsp;<asp:RadioButton ID="RBOAFixed" runat="server" Text="Fixed" GroupName="OA" Checked="true" />
                                &nbsp; &nbsp;<asp:RadioButton ID="RBOArPer" runat="server" Text="Per(%)" GroupName="OA" />
                                <asp:TextBox ID="txtOthers" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chktdsapply" runat="server" AutoPostBack="true" />
                                &nbsp; &nbsp;<label class="control-label">T.D.S.</label>
                                &nbsp; &nbsp;<asp:RadioButton ID="rbTDSFixed" runat="server" Text="Fixed" GroupName="TDS" Checked="true" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbTDSPer" runat="server" Text="Per(%)" GroupName="TDS" />
                                <asp:TextBox ID="txtTDS" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkDeductOther" runat="server" AutoPostBack="true" />
                                &nbsp; &nbsp;<label class="control-label">Other Deductions</label>
                                &nbsp; &nbsp;<asp:RadioButton ID="rbDFixed" runat="server" Text="Fixed" GroupName="OD" Checked="true" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbODPer" runat="server" Text="Per(%)" GroupName="OD" />
                                <asp:TextBox ID="txtDeductOther" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkPaidLeave" runat="server" AutoPostBack="true" />
                                &nbsp; &nbsp;<label class="control-label">Paid Leave (In Months)</label>
                                <asp:TextBox ID="txtNoOfPaidLeave" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkLateCheckIn" runat="server" AutoPostBack="true" />
                                &nbsp; &nbsp;<label class="control-label">Late Check-In (In Minutes)</label>
                                <asp:TextBox ID="txtLateCheckIn" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkEarlyCheckOut" runat="server" AutoPostBack="true" />
                                &nbsp; &nbsp;<label class="control-label">Early Check-Out (In Minutes)</label>
                                <asp:TextBox ID="txtEarlyCheckOut" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkBonus" runat="server" AutoPostBack="true" />
                                &nbsp; &nbsp;<label class="control-label">Yearly Bonus (%)</label>
                                <asp:TextBox ID="txtBonus" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkMinHour" runat="server" AutoPostBack="true" />
                                &nbsp; &nbsp;<label class="control-label">Working Hours</label>
                                <asp:TextBox ID="txtWorkingHour" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-4">
                                <label class="control-label">Working Days</label>
                                <br />
                                <asp:ListBox ID="drpWorkingDay" runat="server" SelectionMode="Multiple" CssClass="form-control select2">
                                    <asp:ListItem Text="Sunday" Value="Sunday"></asp:ListItem>
                                    <asp:ListItem Text="Monday" Value="Monday"></asp:ListItem>
                                    <asp:ListItem Text="Tuesday" Value="Tuesday"></asp:ListItem>
                                    <asp:ListItem Text="Wednesday" Value="Wednesday"></asp:ListItem>
                                    <asp:ListItem Text="Thursday" Value="Thursday"></asp:ListItem>
                                    <asp:ListItem Text="Friday" Value="Friday"></asp:ListItem>
                                    <asp:ListItem Text="Saturday" Value="Saturday"></asp:ListItem>
                                </asp:ListBox>
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkOverTime" runat="server" AutoPostBack="true" />
                                &nbsp; &nbsp;<label class="control-label">Over Time</label>
                                &nbsp; &nbsp;<asp:RadioButton ID="rbOTFixed" runat="server" Text="Fixed" GroupName="OT" Checked="true" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbOTPer" runat="server" Text="Per(%)" GroupName="OT" />
                                <asp:TextBox ID="txtOverTime" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="clearfix">&nbsp;</div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkHRA" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chkConv" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ChkWs" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chkMA" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chkDP" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chkFoodAll" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chkOthers" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chktdsapply" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chkDeductOther" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chkPaidLeave" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chkLateCheckIn" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chkEarlyCheckOut" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
        <asp:UpdatePanel ID="updtAddress" runat="server">
            <ContentTemplate>
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="col-md-12" style="border-bottom: .5px solid lightgrey;">
                            <h4 class="box-title">
                                <label>PESONAL DETAILS</label>
                            </h4>
                            <div class="col-md-3">
                                <label class="control-label">
                                    Gender <span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpGender"
                                    ErrorMessage="Select Gender" ValidationGroup="aa" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpGender" runat="server" CssClass="form-control select2" AutoPostBack="true">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">
                                    Date of Birth <span style="color: #ff0000">*</span>
                                </label>  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDOB"
                                    ErrorMessage="Enter DOB" ValidationGroup="aa"  ></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">
                                    Status <span style="color: #ff0000">*</span>
                                </label>  <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="drpMarriedStatus"
                                    ErrorMessage="Select Status" ValidationGroup="aa" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpMarriedStatus" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpMarriedStatus_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Single">Single</asp:ListItem>
                                    <asp:ListItem Value="Married">Married</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">
                                    Education
                                </label>
                                <asp:TextBox ID="txtEducation" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                             <div class="clearfix">&nbsp;</div>
                            <div class="col-md-3">
                                <label class="control-label">
                                    Date of Marriage
                                </label>
                                <asp:TextBox ID="txtDOM" runat="server" CssClass="form-control datepicker" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div id="ShowMaternity" runat="server"  class="col-md-3">
                                <label class="control-label">
                                    Maternity Leave</label>
                                <asp:TextBox ID="TxtMaternityLeave" runat="server" CssClass="form-control" Text="0"  ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="col-md-3">
                                <label class="control-label">
                                    Phone Number
                                </label>
                                <span id="erro1r" style="color: Red; display: none" class="numerror">* Input digits (0 - 9)</span>
                                <asp:TextBox ID="txtphoneno" runat="server" CssClass="form-control" onkeypress="return IsNumeric(event,0);"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">
                                    Mobile Number
                                </label>
                                <span style="color: #ff0000">*</span>
                                <span style="color: Red; display: none" class="numerror">* Input digits (0 - 9)</span>
                                <asp:TextBox ID="txtmobno" runat="server" CssClass="form-control" onkeypress="return IsNumeric(event,1);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtmobno"
                                    ErrorMessage="Enter Mobile Number" ValidationGroup="aa"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">
                                    CUG Mobile Number  <span style="color: Red; display: none" class="numerror">* Input digits (0 - 9)</span>
                                </label> 
                                <asp:TextBox ID="txtCUG" runat="server" CssClass="form-control" onkeypress="return IsNumeric(event,2);"></asp:TextBox>
                            </div>  
                            <div class="col-md-3">
                                <label class="control-label">
                                    Personal Email ID <span style="color: #ff0000">*</span>
                                </label>                                
                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                                &nbsp;<asp:RegularExpressionValidator ID="Rev1" runat="server" ErrorMessage="Valid Email Id"
                                    ControlToValidate="txtemail" style="color: #ff0000" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>                                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtemail"
                                    ErrorMessage="Enter Email ID" ValidationGroup="aa" style="color: #ff0000" ></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">
                                    Office Email ID  <span style="color: #ff0000">*</span>
                                </label>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                    ErrorMessage="Valid Email Id" ControlToValidate="txtOfficEmail"  style="color: #ff0000"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtOfficEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-6">
                                <label class="control-label">
                                    Present Address  <span style="color: #ff0000">*</span>
                                </label>
                                <asp:CheckBox ID="ChksameAddr" runat="server" AutoPostBack="True" OnCheckedChanged="ChksameAddr_CheckedChanged" Text="If Permanent Address Is Same" Style="font-family: Calibri; font-size: x-small;" />
                                <asp:TextBox ID="txtpresentaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtpresentaddress"
                                    ErrorMessage="Enter Present Address" ValidationGroup="aa" style="color: #ff0000"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-6">
                                <label class="control-label">
                                    Permanent Address <span style="color: #ff0000">*</span>
                                </label>
                                <asp:TextBox ID="txtparmentaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtparmentaddress"
                                    ErrorMessage="Enter Permanent Address" ValidationGroup="aa"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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

        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-12" style="border-bottom: .5px solid lightgrey;">
                    <h4 class="box-title">
                        <label>DOCUMENTS</label>
                    </h4>

                    <div class="col-md-4">
                        <label class="control-label">
                            Upload Photo</label>
                        <asp:FileUpload ID="imageupload" runat="server" />
                        <asp:HiddenField ID="hddimg1" runat="server" />
                        <asp:Image ID="Image1" runat="server" Width="70px" />
                    </div>

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

