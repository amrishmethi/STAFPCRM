<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="Payroll.aspx.cs" Inherits="Soft_Payroll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" />
    <section class="content-header" style="height: 2.5em;">
        <h1>HR Details &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger" Text="Back To List"
            OnClick="btnCancel_Click" CausesValidation="false" /></h1>
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
                        <label class="control-label">Company<span style="color: #ff0000">*</span></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DrpCompanies"
                            ErrorMessage=" Please Select Company" ValidationGroup="aa" InitialValue="0"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="DrpCompanies" runat="server" CssClass="form-control select2">
                        </asp:DropDownList>

                        <asp:HiddenField ID="hddCrmUserId" runat="server" Value="0" />
                    </div>
                    <asp:UpdatePanel ID="updept" runat="server">
                        <ContentTemplate>
                            <div class="col-md-4">
                                <label class="control-label">Department<span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpDepartment"
                                    ErrorMessage="Please Select" ValidationGroup="aa" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpDepartment" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged"></asp:DropDownList>
                                <asp:HiddenField ID="hddEmpNo" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label class="control-label">Designation<span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpDesignation"
                                    ErrorMessage="Please Select" ValidationGroup="aa" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpDesignation" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="drpDepartment" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">Emp Code</label>
                        &nbsp; &nbsp; &nbsp; &nbsp;<asp:CheckBox ID="chkAttandance" runat="server" ClientIDMode="Static" Text="Attandance By Self" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpCode"
                            ErrorMessage=" Please Enter" ValidationGroup="aa"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Employee Name <span style="color: #ff0000">*</span></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                            ErrorMessage=" Please Enter" ValidationGroup="aa" ControlToValidate="txtemployeename"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtemployeename" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Reporting Manager<span style="color: #ff0000">*</span></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="drpProjectManager"
                            ErrorMessage="Please Select" ValidationGroup="aa" InitialValue="0" Style="color: #ff0000"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="drpProjectManager" runat="server" CssClass="form-control select2">
                        </asp:DropDownList>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">Date of Joining    <span style="color: #ff0000">*</span></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ForeColor="Red"
                            ErrorMessage=" Please Enter" ControlToValidate="txtDOJ" ValidationGroup="aa"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control datepicker1">
                        </asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Date of Leaving</label>
                        &nbsp;<asp:CheckBox ID="chkDOL" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtDateOfLeaving','')" />
                        <asp:TextBox ID="txtDateOfLeaving" runat="server" ClientIDMode="Static" CssClass="form-control datepicker" Enabled="false">
                        </asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">PAN No</label>
                        <asp:TextBox ID="txtpanno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>

                    <div class="col-md-4">
                        <label class="control-label">PF A/c No.</label>
                        <asp:CheckBox ID="chkBasic" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtPFCode','')" />
                        <asp:TextBox ID="txtPFCode" runat="server" CssClass="form-control" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">ESI A/c No.</label>
                        <asp:CheckBox ID="ChkEsi" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtESICode','')" />
                        <asp:TextBox ID="txtESICode" runat="server" ClientIDMode="Static" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Current Status</label>
                        <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control select2">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="Active" Selected="True">Active</asp:ListItem>
                            <asp:ListItem Value="Non-Active">Non-Active</asp:ListItem>
                        </asp:DropDownList>
                    </div>
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
                        <label class="control-label">Bank Name</label>
                        <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Bank Account No</label>
                        <asp:TextBox ID="txtBankAccno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Branch Name & Address</label>
                        <asp:TextBox ID="txtbankaddress" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">IFSC Code</label>
                        <asp:TextBox ID="txtBankIFSC" runat="server" CssClass=" form-control"></asp:TextBox>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-3">
                        <label class="control-label">Bank Name</label>
                        <asp:TextBox ID="txtBankName2" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Bank Account No</label>
                        <asp:TextBox ID="txtBankAccno2" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Branch Name & Address</label>
                        <asp:TextBox ID="txtbankaddress2" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">IFSC Code</label>
                        <asp:TextBox ID="txtBankIFSC2" runat="server" CssClass="form-control"></asp:TextBox>
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

                    <table width="100%" border="0" cellspacing="0" cellpadding="5px" class="table table-bordered table-striped">
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <label class="control-label">
                                    NET SALARY<span style="color: #ff0000">*</span>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ForeColor="Red"
                                    ErrorMessage=" Please Enter" ValidationGroup="aa" ControlToValidate="txtNetSalary" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtNetSalary" runat="server" ClientIDMode="Static" CssClass="form-control" onchange="AllCal()" Text="0"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkBS" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtBasicsalary','')" /></td>
                            <td>
                                <label class="control-label">
                                    Basic Salary   <span style="color: #ff0000">*</span>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ForeColor="Red"
                                    ErrorMessage=" Please Enter" ValidationGroup="aa" ControlToValidate="txtBasicsalaryValue" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:RadioButton ID="RBBSFixed" runat="server" Text="Fixed" GroupName="BS" Checked="true" ClientIDMode="Static" onchange="getValue('RBBS','txtBasicsalary','','txtNetSalary','')" />
                                &nbsp; &nbsp;<asp:RadioButton ID="RBBSPer" runat="server" Text="Per(%)" GroupName="BS" ClientIDMode="Static" onchange="getValue('RBBS','txtBasicsalary','','txtNetSalary','')" /></td>
                            <td>
                                <asp:TextBox ID="txtBasicsalary" runat="server" CssClass="form-control" Enabled="false" onchange="getValue('RBBS','txtBasicsalary','','txtNetSalary','')" onkeypress="return IsDecimalKey(event)" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>
                                <input id="txtBasicsalaryValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly />
                                <%--<asp:TextBox ID="txtBasicsalaryValue" runat="server" CssClass="form-control" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkPF" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtPFSelf','txtPFComp')" /></td>
                            <td>
                                <label class="control-label">PF</label></td>
                            <td>
                                <asp:RadioButton ID="rbPFFixed" runat="server" Text="Fixed" GroupName="PF" Checked="true" onchange="getValue('rbPF','txtPFSelf','txtPFComp','txtBasicsalaryValue','')" ClientIDMode="Static" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbPFPer" runat="server" Text="Per(%)" GroupName="PF" onchange="getValue('rbPF','txtPFSelf','txtPFComp','txtBasicsalaryValue','')" ClientIDMode="Static" /></td>
                            <td>
                                <asp:TextBox ID="txtPFSelf" runat="server" CssClass="form-control" placeholder="Employee" Enabled="false" onchange="getValue('rbPF','txtPFSelf','txtPFComp','txtBasicsalaryValue','')" ClientIDMode="Static"></asp:TextBox>
                                <br />
                                <asp:TextBox ID="txtPFComp" runat="server" CssClass="form-control" placeholder="Employer" Enabled="false" onchange="getValue('rbPF','txtPFSelf','txtPFComp','txtBasicsalaryValue','')" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>
                                <input id="txtPFSelfValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly placeholder="Employee" />
                                <br />
                                <input id="txtPFCompValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly placeholder="Employer" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="ChkESIC" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtESICSelf','txtESICComp')" /></td>
                            <td>
                                <label class="control-label">ESIC (Applicable Amount)</label>
                                <asp:TextBox ID="txtESICApplicable" runat="server" Text="21000" CssClass="form-control" ClientIDMode="Static" onchange="AllCal()"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RadioButton ID="rbESICFixed" runat="server" Text="Fixed" GroupName="ESIC" Checked="true" ClientIDMode="Static" onchange="getValue('rbESIC','txtESICSelf','txtESICComp','txtNetSalary','txtESICApplicable')" />
                                &nbsp; &nbsp;
                                        <asp:RadioButton ID="rbESICPer" runat="server" Text="Per(%)" GroupName="ESIC" ClientIDMode="Static" onchange="getValue('rbESIC','txtESICSelf','txtESICComp','txtNetSalary','txtESICApplicable)" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtESICSelf" runat="server" CssClass="form-control" placeholder="Employee" Enabled="false" onchange="getValue('rbESIC','txtESICSelf','txtESICComp','txtNetSalary','txtESICApplicable')" ClientIDMode="Static"></asp:TextBox>
                                <br />
                                <asp:TextBox ID="txtESICComp" runat="server" CssClass="form-control" placeholder="Employer" Enabled="false" onchange="getValue('rbESIC','txtESICSelf','txtESICComp','txtNetSalary','txtESICApplicable')" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>
                                <input id="txtESICSelfValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly placeholder="Employee" />
                                <br />
                                <input id="txtESICCompValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly placeholder="Employer" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkHRA" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtHRA','')" /></td>
                            <td>
                                <label class="control-label">House Rent Allowance</label></td>
                            <td>
                                <asp:RadioButton ID="rbHRAFixed" runat="server" Text="Fixed" GroupName="HRA" Checked="true" onchange="getValue('rbHRA','txtHRA','','txtNetSalary','')" ClientIDMode="Static" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbHRAPer" runat="server" Text="Per(%)" GroupName="HRA" onchange="getValue('rbHRA','txtHRA','','txtNetSalary','')" ClientIDMode="Static" /></td>
                            <td>
                                <asp:TextBox ID="txtHRA" runat="server" CssClass="form-control" Enabled="false" onchange="getValue('rbHRA','txtHRA','','txtNetSalary','')" ClientIDMode="Static"></asp:TextBox></td>
                            <td>
                                <input id="txtHRAValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="ChkWA" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'TxtwashingAllowance','')" /></td>
                            <td>
                                <label class="control-label">Washing Allowance</label></td>
                            <td>
                                <asp:RadioButton ID="RbWAFixed" runat="server" Text="Fixed" GroupName="WA" Checked="true" onchange="getValue('RbWA','TxtwashingAllowance','','txtNetSalary','')" ClientIDMode="Static" />
                                &nbsp; &nbsp;<asp:RadioButton ID="RbWAPer" runat="server" Text="Per(%)" GroupName="WA" onchange="getValue('RbWA','TxtwashingAllowance','','txtNetSalary','')" ClientIDMode="Static" /></td>
                            <td>
                                <asp:TextBox ID="TxtwashingAllowance" runat="server" CssClass="form-control" Enabled="false" onchange="getValue('RbWA','TxtwashingAllowance','','txtNetSalary','')" ClientIDMode="Static"></asp:TextBox></td>
                            <td>
                                <input id="TxtwashingAllowanceValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkMA" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtMediacl','')" /></td>
                            <td>
                                <label class="control-label">Medical Allowance</label></td>
                            <td>
                                <asp:RadioButton ID="rbMAFixed" runat="server" Text="Fixed" GroupName="MA" Checked="true" onchange="getValue('rbMA','txtMediacl','','txtNetSalary','')" ClientIDMode="Static" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbMAPer" runat="server" Text="Per(%)" GroupName="MA" onchange="getValue('rbMA','txtMediacl','','txtNetSalary','')" ClientIDMode="Static" /></td>
                            <td>
                                <asp:TextBox ID="txtMediacl" runat="server" CssClass="form-control" Enabled="false" onchange="getValue('rbMA','txtMediacl','','txtNetSalary','')" ClientIDMode="Static"></asp:TextBox></td>
                            <td>
                                <input id="txtMediaclValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:CheckBox ID="chkLA" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtLAPay','')" /></td>
                            <td>
                                <label class="control-label">Laptop Allowance</label></td>
                            <td>
                                <asp:RadioButton ID="rbLAFixed" runat="server" Text="Fixed" GroupName="DP" Checked="true" onchange="getValue('rbLA','txtLAPay','','txtNetSalary','')" ClientIDMode="Static" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbLAPer" runat="server" Text="Per(%)" GroupName="DP" onchange="getValue('rbLA','txtLAPay','','txtNetSalary','')" ClientIDMode="Static" /></td>
                            <td>
                                <asp:TextBox ID="txtLAPay" runat="server" CssClass="form-control" Enabled="false" onchange="getValue('rbLA','txtLAPay','','txtNetSalary','')" ClientIDMode="Static"></asp:TextBox></td>
                            <td>
                                <input id="txtLAPayValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkFoodAll" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtFoodAll','')" /></td>
                            <td>
                                <label class="control-label">Food Allowance</label></td>
                            <td>
                                <asp:RadioButton ID="rbFAFixed" runat="server" Text="Fixed" GroupName="FA" Checked="true" onchange="getValue('rbFA','txtFoodAll','','txtNetSalary','')" ClientIDMode="Static" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbFAPer" runat="server" Text="Per(%)" GroupName="FA" onchange="getValue('rbFA','txtFoodAll','','txtNetSalary','')" ClientIDMode="Static" /></td>
                            <td>
                                <asp:TextBox ID="txtFoodAll" runat="server" CssClass="form-control" Enabled="false" onchange="getValue('rbFA','txtFoodAll','','txtNetSalary','')" ClientIDMode="Static"></asp:TextBox></td>
                            <td>
                                <input id="txtFoodAllValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkOthers" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtOthers','')" /></td>
                            <td>
                                <label class="control-label">Other Allowance</label></td>
                            <td>
                                <asp:RadioButton ID="RBOAFixed" runat="server" Text="Fixed" GroupName="OA" Checked="true" onchange="getValue('RBOA','txtOthers','','txtNetSalary','')" ClientIDMode="Static" />
                                &nbsp; &nbsp;<asp:RadioButton ID="RBOAPer" runat="server" Text="Per(%)" GroupName="OA" onchange="getValue('RBOA','txtOthers','','txtNetSalary','')" ClientIDMode="Static" /></td>
                            <td>
                                <asp:TextBox ID="txtOthers" runat="server" CssClass="form-control" Enabled="false" onchange="getValue('RBOA','txtOthers','','txtNetSalary','')" ClientIDMode="Static"></asp:TextBox></td>
                            <td>
                                <input id="txtOthersValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chktdsapply" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtTDS','')" /></td>
                            <td>
                                <label class="control-label">T.D.S.</label></td>
                            <td>
                                <asp:RadioButton ID="rbTDSFixed" runat="server" Text="Fixed" GroupName="TDS" Checked="true" onchange="getValue('rbTDS','txtTDS','','txtNetSalary','')" ClientIDMode="Static" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbTDSPer" runat="server" Text="Per(%)" GroupName="TDS" onchange="getValue('rbTDS','txtTDS','','txtNetSalary','')" ClientIDMode="Static" /></td>
                            <td>
                                <asp:TextBox ID="txtTDS" runat="server" CssClass="form-control" Enabled="false" onchange="getValue('rbTDS','txtTDS','','txtNetSalary','')" ClientIDMode="Static"></asp:TextBox></td>
                            <td>
                                <input id="txtTDSValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkDeductOther" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtDeductOther','')" /></td>
                            <td>
                                <label class="control-label">Other Deductions</label></td>
                            <td>
                                <asp:RadioButton ID="rbDFixed" runat="server" Text="Fixed" GroupName="OD" Checked="true" onchange="getValue('rbD','txtDeductOther','','txtNetSalary','')" ClientIDMode="Static" />
                                &nbsp; &nbsp;<asp:RadioButton ID="rbDPer" runat="server" Text="Per(%)" GroupName="OD" onchange="getValue('rbD','txtDeductOther','','txtNetSalary','')" ClientIDMode="Static" /></td>
                            <td>
                                <asp:TextBox ID="txtDeductOther" runat="server" CssClass="form-control" Enabled="false" onchange="getValue('rbD','txtDeductOther','','txtNetSalary','')" ClientIDMode="Static"></asp:TextBox></td>
                            <td>
                                <input id="txtDeductOtherValue" runat="server" type="text" clientidmode="Static" class="form-control" readonly />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkConv" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtConveyance','')" /></td>
                            <td>
                                <label class="control-label">Conveyance Allowance</label></td>
                            <td>
                                <asp:RadioButton ID="RbTAFixed" runat="server" Text="Fixed" GroupName="TA" Checked="true" ClientIDMode="Static" />
                                &nbsp; &nbsp;<asp:RadioButton ID="RbTAPer" runat="server" Text="Per KM" GroupName="TA" ClientIDMode="Static" /></td>
                            <td>
                                <asp:TextBox ID="txtConveyance" runat="server" CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:TextBox></td>
                            <td>
                                <input id="txtConveyanceValue" runat="server" type="text" clientidmode="Static" class="form-control hidden" readonly />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkDALocal" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtDALocal','')" /></td>
                            <td>
                                <label class="control-label">Daily Allowance Local</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtDALocal" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkDAEx" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtDAEx','')" /></td>
                            <td>
                                <label class="control-label">Daily Allowance Ex</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtDAEx" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkNightAll" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtNightAll','')" /></td>
                            <td>
                                <label class="control-label">Night Stay Allowance</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtNightAll" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkPaidLeave" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtNoOfPaidLeave','')" /></td>
                            <td>
                                <label class="control-label">Paid Leave (In Months)</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtNoOfPaidLeave" ClientIDMode="Static" runat="server" onkeypress="return IsNumericKey(event);" CssClass="form-control" Enabled="false"></asp:TextBox></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="ChkCL" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtCL','')" /></td>
                            <td>
                                <label class="control-label">CL (In Yearly)</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtCL" ClientIDMode="Static" runat="server" CssClass="form-control" onkeypress="return IsNumericKey(event);" Enabled="false"></asp:TextBox></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkLateCheckIn" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtLateCheckIn','')" /></td>
                            <td>
                                <label class="control-label">Late Check-In (In Minutes)</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtLateCheckIn" ClientIDMode="Static" runat="server" CssClass="form-control" onkeypress="return IsNumericKey(event);" Enabled="false"></asp:TextBox></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkEarlyCheckOut" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtEarlyCheckOut','')" /></td>
                            <td>
                                <label class="control-label">Early Check-Out (In Minutes)</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtEarlyCheckOut" ClientIDMode="Static" runat="server" CssClass="form-control" onkeypress="return IsNumericKey(event);" Enabled="false"></asp:TextBox></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkBonus" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtBonus','')" /></td>
                            <td>
                                <label class="control-label">Yearly Bonus (%)</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtBonus" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkMinHour" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtWorkingHour','')" /></td>
                            <td>
                                <label class="control-label">Working Hours</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtWorkingHour" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkOverTime" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtOverTime','')" /></td>
                            <td>
                                <label class="control-label">Over Time ( % Per Day Salary)</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtOverTime" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkOverTimePH" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtOverTimePH','')" /></td>
                            <td>
                                <label class="control-label">Over Time ( % Per Hour Salary)</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtOverTimePH" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkCHeckInTime" runat="server" ClientIDMode="Static" onclick="return OptionsSelected(this,'txtCheckIn','')" /></td>
                            <td>
                                <label class="control-label">Check In Time</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtCheckIn" ClientIDMode="Static" runat="server" Enabled="false" CssClass="form-control timepicker"></asp:TextBox></td>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <label class="control-label">Working Time</label></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtWorkingTimeFRom" runat="server" CssClass="form-control timepicker"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtWorkingTimeTo" runat="server" CssClass="form-control timepicker"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <label class="control-label">Working Days</label></td>
                            <td>&nbsp;</td>
                            <td colspan="2">
                                <asp:ListBox ID="drpWorkingDay" runat="server" SelectionMode="Multiple" CssClass="form-control select2">
                                    <asp:ListItem Text="Sunday" Value="Sunday"></asp:ListItem>
                                    <asp:ListItem Text="Monday" Value="Monday"></asp:ListItem>
                                    <asp:ListItem Text="Tuesday" Value="Tuesday"></asp:ListItem>
                                    <asp:ListItem Text="Wednesday" Value="Wednesday"></asp:ListItem>
                                    <asp:ListItem Text="Thursday" Value="Thursday"></asp:ListItem>
                                    <asp:ListItem Text="Friday" Value="Friday"></asp:ListItem>
                                    <asp:ListItem Text="Saturday" Value="Saturday"></asp:ListItem>
                                </asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <label class="control-label">Cost To Company (CTC) </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ForeColor="Red"
                                    ErrorMessage=" Please Enter" ValidationGroup="aa" ControlToValidate="txtCTC" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ForeColor="Red"
                                    ErrorMessage=" Please Enter" ValidationGroup="aa" ControlToValidate="txtCTC" InitialValue="NaN"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <input id="txtCTC" runat="server" type="text" clientidmode="Static" class="form-control" readonly />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <label class="control-label">In-Hand Salary</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Red"
                                    ErrorMessage=" Please Enter" ValidationGroup="aa" ControlToValidate="txtInHand" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ForeColor="Red"
                                    ErrorMessage=" Please Enter" ValidationGroup="aa" ControlToValidate="txtInHand" InitialValue="NaN"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <input id="txtInHand" runat="server" type="text" clientidmode="Static" class="form-control" readonly />
                            </td>
                        </tr>
                    </table>
                </div>
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
                                <label class="control-label">Gender <span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpGender"
                                    ErrorMessage="Select Gender" ValidationGroup="aa" Style="color: #ff0000" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpGender" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">Date of Birth <span style="color: #ff0000">*</span></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDOB"
                                    ErrorMessage="Enter DOB" ValidationGroup="aa" Style="color: #ff0000"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">
                                    Marital Status <span style="color: #ff0000">*</span>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="drpMarriedStatus"
                                    ErrorMessage="Select Status" ValidationGroup="aa" Style="color: #ff0000" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpMarriedStatus" runat="server" CssClass="form-control select2" ClientIDMode="Static" onchange="return  OnSelected(this,'txtDOM','TxtMaternityLeave')">
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
                                <asp:TextBox ID="txtDOM" runat="server" ClientIDMode="Static" CssClass="form-control datepicker" Enabled="false"></asp:TextBox>
                            </div>
                            <div id="ShowMaternity" runat="server" class="col-md-3">
                                <label class="control-label">
                                    Maternity Leave</label>
                                <asp:TextBox ID="TxtMaternityLeave" ClientIDMode="Static" runat="server" CssClass="form-control" Text="0" onkeypress="return IsNumericKey(event);" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="col-md-3">
                                <label class="control-label">
                                    Phone Number
                                </label>
                                <asp:TextBox ID="txtphoneno" runat="server" CssClass="form-control" onkeypress="return IsNumericKey(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">
                                    Mobile Number <span style="color: #ff0000">*</span>
                                </label>

                                <asp:TextBox ID="txtmobno" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return IsNumericKey(event);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtmobno"
                                    ErrorMessage="Enter Mobile Number" Style="color: #ff0000" ValidationGroup="aa"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">
                                    CUG Mobile Number   <span style="color: #ff0000">*</span>
                                </label>
                                <asp:TextBox ID="txtCUG" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return IsNumericKey(event);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtCUG"
                                    ErrorMessage="Enter Mobile Number" ValidationGroup="aa" Style="color: #ff0000"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">
                                    Personal Email ID <span style="color: #ff0000">*</span>
                                </label>
                                <asp:RegularExpressionValidator ID="Rev1" runat="server" ErrorMessage="Valid Email Id"
                                    ControlToValidate="txtemail" Style="color: #ff0000" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                                &nbsp;
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtemail"
                                    ErrorMessage="Enter Email ID" ValidationGroup="aa" Style="color: #ff0000"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">
                                    Office Email ID  <span style="color: #ff0000">*</span>
                                </label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                    ErrorMessage="Valid Email Id" ControlToValidate="txtOfficEmail" Style="color: #ff0000" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtOfficEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="col-md-6">
                                <label class="control-label">
                                    Present Address  <span style="color: #ff0000">*</span>
                                </label>
                                <asp:CheckBox ID="ChksameAddr" runat="server" ClientIDMode="Static" onclick="return IfAddressSame(this,'txtpresentaddress','txtparmentaddress')" Text="If Permanent Address Is Same" Style="font-family: Calibri; font-size: x-small;" />
                                <asp:TextBox ID="txtpresentaddress" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtpresentaddress"
                                    ErrorMessage="Enter Present Address" ValidationGroup="aa" Style="color: #ff0000"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-6">
                                <label class="control-label">
                                    Permanent Address <span style="color: #ff0000">*</span>
                                </label>
                                <asp:TextBox ID="txtparmentaddress" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtparmentaddress"
                                    ErrorMessage="Enter Permanent Address" ValidationGroup="aa" Style="color: #ff0000"></asp:RequiredFieldValidator>
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
                        <label class="control-label">Father's Name <span style="color: #ff0000">*</span></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtfathername"
                            ErrorMessage="Enter Father's Name" ValidationGroup="aa" Style="color: #ff0000"></asp:RequiredFieldValidator>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="txtfathername" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Mother's Name</label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="txtMotherName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Spouse</label>
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
                        <label class="control-label">Spouse Name</label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="txtSpouse" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">No.Of Children</label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="TxtChild" runat="server" CssClass="form-control" onkeypress="return IsNumericKey(event);"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Contact No.Of Family<span style="color: #ff0000">*</span></label>
                        <asp:TextBox ID="txtFatherCntctNo" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtFatherCntctNo"
                            ErrorMessage="Enter Family's Contact Detail" Style="color: #ff0000" ValidationGroup="aa"></asp:RequiredFieldValidator>
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
                            <asp:TextBox ID="txtLastCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Company Address
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="TxtLastCompanyAdd" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">
                            Last Drawn CTC
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="TxtLastCtc" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">&nbsp;</div>
                    <div class="col-md-4">
                        <label class="control-label">
                            PF Account No.
                        </label>
                        <div class="controls  controls-row">
                            <asp:TextBox ID="TxtLastPfNo" runat="server" CssClass="form-control"></asp:TextBox>
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                        <ContentTemplate>
                            <div class="col-md-4">
                                <label class="control-label">
                                    Document</label>
                                <asp:DropDownList ID="drpDocument" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ForeColor="Red"
                                    ErrorMessage=" Please Select" ValidationGroup="aa1" ControlToValidate="drpDocument" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <asp:FileUpload ID="imageupload" runat="server" ClientIDMode="Static" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ForeColor="Red"
                                    ErrorMessage=" Please Select" ValidationGroup="aa1" ControlToValidate="imageupload" InitialValue=""></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4">
                                <asp:HiddenField ID="HddRowID" runat="server" Value="0" />
                                <br />
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add" ValidationGroup="aa1" OnClick="btnAdd_Click" />
                            </div>
                            <div class="clearfix">&nbsp;</div>

                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <table class="table table-bordered display table-striped" style="background-color: gainsboro;">
                                        <thead>
                                            <tr>
                                                <th style="text-align: left;">Sr. No.</th>
                                                <th style="text-align: left;">Document</th>
                                                <th style="text-align: left;">File</th>
                                                <th>
                                                    <label id="lblAction">Action</label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rep" runat="server" OnItemCommand="rep_ItemCommand">
                                                <ItemTemplate>
                                                    <tr runat="server" visible='<%#Eval("Delid").ToString()=="0"?true:false %>'>
                                                        <td>
                                                            <%#Container.ItemIndex+1 %>
                                                            <asp:HiddenField ID="hddID" runat="server" Value='<%#Eval("Id") %>' />
                                                        </td>
                                                        <td style="text-align: left;"><%#Eval("Docu_Name") %>
                                                            <asp:HiddenField ID="HddDcoumentId" runat="server" Value='<%#Eval("Docu_Id") %>' />
                                                        </td>
                                                        <td style="text-align: left;"><a href="../../upload/Document/<%#Eval("File_Name") %>" target="_blank" class=""><%#Eval("File_Name") %></a>

                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkRemove" runat="server" CommandName="Remove" Text="Remove" CommandArgument='<%#Eval("SNo") %>'></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnAdd" />
                        </Triggers>
                    </asp:UpdatePanel>
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
    <script> 
        $('.timepicker').datetimepicker({
            format: 'hh:mm a'
        });
    </script>

    <script> 
        function OptionsSelected(me, valuee, valuee2) {
            if (document.getElementById(me.id).checked) {
                document.getElementById(valuee).disabled = false;
                if (valuee2 != '')
                    document.getElementById(valuee2).disabled = false;
            }
            else {
                document.getElementById(valuee).disabled = true;
                if (valuee2 != '')
                    document.getElementById(valuee2).disabled = true;
            }
            document.getElementById(valuee).value = "";
            if (valuee2 != '')
                document.getElementById(valuee2).value = "";
        }

        function IfAddressSame(me, valuee, valuee2) {
            if (document.getElementById(me.id).checked) {
                document.getElementById(valuee2).value = document.getElementById(valuee).value;
            }
        }

        function OnSelected(me, valuee1, valuee2) {
            debugger
            var value = me.selectedIndex;
            if (value > 1) {
                document.getElementById(valuee1).disabled = false;
                if (valuee2 != '')
                    document.getElementById(valuee2).disabled = false;
            }
            else {
                document.getElementById(valuee1).disabled = true;
                if (valuee2 != '')
                    document.getElementById(valuee2).disabled = true;
            }
        }
    </script>


    <script>
        function SalaryCal() {
            var txtNetSalary = document.getElementById("txtNetSalary").value == '' ? '0' : document.getElementById("txtNetSalary").value;
            var txtBasicsalaryValue = document.getElementById("txtBasicsalaryValue").value == '' ? '0' : document.getElementById("txtBasicsalaryValue").value;
            var txtPFSelfValue = document.getElementById("txtPFSelfValue").value == '' ? '0' : document.getElementById("txtPFSelfValue").value;
            var txtPFCompValue = document.getElementById("txtPFCompValue").value == '' ? '0' : document.getElementById("txtPFCompValue").value;
            var txtESICSelfValue = document.getElementById("txtESICSelfValue").value == '' ? '0' : document.getElementById("txtESICSelfValue").value;
            var txtESICCompValue = document.getElementById("txtESICCompValue").value == '' ? '0' : document.getElementById("txtESICCompValue").value;
            var txtHRAValue = document.getElementById("txtHRAValue").value == '' ? '0' : document.getElementById("txtHRAValue").value;
            var TxtwashingAllowanceValue = document.getElementById("TxtwashingAllowanceValue").value == '' ? '0' : document.getElementById("TxtwashingAllowanceValue").value;
            var txtMediaclValue = document.getElementById("txtMediaclValue").value == '' ? '0' : document.getElementById("txtMediaclValue").value;
            var txtLAPayValue = document.getElementById("txtLAPayValue").value == '' ? '0' : document.getElementById("txtLAPayValue").value;
            var txtFoodAllValue = document.getElementById("txtFoodAllValue").value == '' ? '0' : document.getElementById("txtFoodAllValue").value;
            var txtOthersValue = document.getElementById("txtOthersValue").value == '' ? '0' : document.getElementById("txtOthersValue").value;
            var txtTDSValue = document.getElementById("txtTDSValue").value == '' ? '0' : document.getElementById("txtTDSValue").value;
            var txtDeductOtherValue = document.getElementById("txtDeductOtherValue").value == '' ? '0' : document.getElementById("txtDeductOtherValue").value;

            var total = parseFloat(txtBasicsalaryValue) + parseFloat(txtPFCompValue) + parseFloat(txtESICCompValue) + parseFloat(txtHRAValue) + parseFloat(TxtwashingAllowanceValue) + parseFloat(txtMediaclValue) + parseFloat(txtLAPayValue) + parseFloat(txtFoodAllValue) + parseFloat(txtOthersValue) + parseFloat(txtTDSValue) + parseFloat(txtDeductOtherValue);


            var Ntotal = parseFloat(txtBasicsalaryValue) + parseFloat(txtHRAValue) + parseFloat(TxtwashingAllowanceValue) + parseFloat(txtMediaclValue) + parseFloat(txtLAPayValue) + parseFloat(txtFoodAllValue) + parseFloat(txtOthersValue) + parseFloat(txtTDSValue) + parseFloat(txtDeductOtherValue) - parseFloat(txtPFSelfValue) - parseFloat(txtESICSelfValue);

            document.getElementById("txtCTC").value = total;
            document.getElementById("txtInHand").value = Ntotal;

            //if (total > txtNetSalary) alert('Value Check');
        }
    </script>

    <script>
        function AllCal() {
            debugger
            getValue('RBBS', 'txtBasicsalary', '', 'txtNetSalary', '');
            getValue('rbPF', 'txtPFSelf', 'txtPFComp', 'txtBasicsalaryValue', '');
            getValue('rbESIC', 'txtESICSelf', 'txtESICComp', 'txtNetSalary', 'txtESICApplicable');
            getValue('rbHRA', 'txtHRA', '', 'txtNetSalary', '');
            getValue('RbWA', 'TxtwashingAllowance', '', 'txtNetSalary', '');
            getValue('rbMA', 'txtMediacl', '', 'txtNetSalary', '');
            getValue('rbLA', 'txtLAPay', '', 'txtNetSalary', '');
            getValue('rbFA', 'txtFoodAll', '', 'txtNetSalary', '');
            getValue('RBOA', 'txtOthers', '', 'txtNetSalary', '');
            getValue('rbTDS', 'txtTDS', '', 'txtNetSalary', '');
            getValue('rbD', 'txtDeductOther', '', 'txtNetSalary', '');
        }
    </script>

    <script type="text/javascript">  
        $(document).ready(function () {
            $("form").bind("keypress", function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
        });
    </script>

    <script>
        function getValue(rd, BSValue, BSValue1, BSValue2, BSValue3) {
            debugger
            var str = rd + "Fixed";
            var str1 = rd + "Per";
            var BSFixed = document.getElementById(str).checked;
            var BSPer = document.getElementById(str1).checked;

            var strval = BSValue + "Value";
            var strval1, strval2, _STR;

            if (BSValue1 != '')
                strval1 = BSValue1 + "Value";

            if (BSValue3 != '') {
                strval2 = BSValue3;
                _STR = document.getElementById(BSValue2).value < document.getElementById(strval2).value ? document.getElementById(BSValue2).value : document.getElementById(strval2).value;
            }
            else
                _STR = document.getElementById(BSValue2).value;

            if (BSFixed) {
                document.getElementById(strval).value = document.getElementById(BSValue).value;
                if (BSValue1 != '')
                    document.getElementById(strval1).value = document.getElementById(BSValue1).value;
            }
            if (BSPer) {
                document.getElementById(strval).value = (parseFloat(_STR) * parseFloat(document.getElementById(BSValue).value)) / 100;
                document.getElementById(strval).text = document.getElementById(strval).value;
                if (BSValue1 != '')
                    document.getElementById(strval1).value = (parseFloat(_STR) * parseFloat(document.getElementById(BSValue1).value)) / 100;
            }
            SalaryCal();
        }
    </script>
</asp:Content>

