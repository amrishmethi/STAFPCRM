<%@ Page Title="Appraisal" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="AppraisalAdd.aspx.cs" Inherits="Soft_AppraisalAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1><a id="lnkAdd" runat="server" href="/Soft/AppraisalRep.aspx" class="btn btn-danger">Back To List</a>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/AppraisalAdd.aspx" class="active">Appraisal Add </a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3">
                                <label>Department</label>
                                <asp:DropDownList ID="drpDepartment" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Employee</label>
                                <asp:DropDownList ID="drpProjectManager" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Affective Date</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate"
                                    ErrorMessage="*" ValidationGroup="aa" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datepicker " placeholder="dd/MM/yyyy"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" ValidationGroup="aa" Text="Submit" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                        <div class="clearfix">&nbsp;</div>

                        <div class="clearfix">&nbsp;</div>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-body">
                        <div class="widget-content">
                            <div class="table-responsive">
                                <table id="ExportTbl" class="table table-bordered display table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">Sr. No.</th>
                                            <th style="text-align: left;">Department</th>
                                            <th style="text-align: left;">Emp Name</th>
                                            <th style="text-align: left;">Previous Salary</th>
                                            <th style="text-align: left;">Appraisal</th>
                                            <th style="text-align: left;">Current Salary</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">

                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("DEPT_NAME") %></td>
                                                    <td style="text-align: left;"><%#Eval("EMP_NAME") %></td>
                                                    
                                                            <td style="text-align: left;">
                                                                <asp:HiddenField ID="hddDept_Id" runat="server" Value='<%#Eval("DEPT_ID") %>' />
                                                                <asp:HiddenField ID="hddEmp_Id" runat="server" Value='<%#Eval("EMP_ID") %>' />
                                                                <asp:TextBox ID="lblPre" runat="server" CssClass="form-control" Text=' <%#Eval("PREV_NETSALARY") %>' ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: left;">

                                                                <asp:TextBox ID="txtApp" runat="server" CssClass="form-control" onkeypress="return IsNumericKey(event);" Text=' <%#Eval("APPRAISAL") %>' AutoPostBack="true" MaxLength="4" OnTextChanged="txtApp_TextChanged"></asp:TextBox></td>

                                                            <td style="text-align: left;">
                                                                <asp:TextBox ID="lblNext" runat="server" CssClass="form-control" Text=' <%#Eval("Cur_NETSALARY") %>' ReadOnly="true"></asp:TextBox>
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
    <script>
        function getValue(th) {
            debugger
            var _STR = th.id;

            const myArray = _STR.split("_");
            var _rowId = myArray[3];
            var _PrevSal = parseFloat(document.getElementById('Body_rep_lblPre_' + _rowId).value);
            var _NextSal = parseFloat(document.getElementById('Body_rep_lblNext_' + _rowId).value);
            var _Appr = parseFloat(document.getElementById('Body_rep_txtApp_' + _rowId).value);


            document.getElementById('Body_rep_lblNext_' + _rowId).value = parseFloat(_PrevSal) + (parseFloat(_PrevSal) * parseFloat(_Appr) / 100);
        }
    </script>
</asp:Content>

