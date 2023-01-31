using System.Data;
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class Soft_Payroll : System.Web.UI.Page
{
    Master getdata = new Master();
    GetData Gd = new GetData();
    Data data = new Data();
    Payrol payroll = new Payrol();
    private HttpCookie Soft;
    int SNO;
    DataTable dtRecord = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            Gd.FillCompany(DrpCompanies);
            Gd.fillDepartment(drpDepartment);
            Gd.FillUser(drpProjectManager);
            Gd.fillDocument(drpDocument);
            if (Request.QueryString["EmpId"] != null)
            {
                FillEmp_Data(Request.QueryString["EmpId"]);
            }
            else if (Request.QueryString["uid"] != null)
            {
                DataSet dsemp = data.getDataSet("select * from tbl_EMpmaster where CRMUserId=" + Request.QueryString["uid"]);
                if (dsemp.Tables[0].Rows.Count > 0)
                {
                    FillEmp_Data(dsemp.Tables[0].Rows[0]["EmpId"].ToString());
                }
                else
                {
                    DataSet CrmUser = getdata.getUserDetails(Request.QueryString["uid"], "0");
                    if (CrmUser.Tables[0].Rows.Count > 0)
                    {
                        txtemployeename.Text = CrmUser.Tables[0].Rows[0]["Name"].ToString();
                        txtCUG.Text = CrmUser.Tables[0].Rows[0]["MobileNo"].ToString();
                        hddCrmUserId.Value = CrmUser.Tables[0].Rows[0]["Id"].ToString();
                    }

                    foreach (ListItem size in drpWorkingDay.Items)
                    {
                        if (size.Value.ToString() != "Sunday")
                        {
                            size.Selected = true;
                        }
                    }
                }
            }
            else
            {
                foreach (ListItem size in drpWorkingDay.Items)
                {
                    if (size.Value.ToString() != "Sunday")
                    {
                        size.Selected = true;
                    }
                }
            }
        }
    }

    public void FillEmp_Data(string Emp_id)
    {
        DataSet dsGet = data.getDataSet("GETEMPDETAIL " + Emp_id);
        #region Employee Basic
        if (dsGet.Tables[0].Rows.Count > 0)
        {
            chkAttandance.Checked = Convert.ToBoolean(dsGet.Tables[0].Rows[0]["ATTANDANCEBY"]);
            DrpCompanies.SelectedValue = dsGet.Tables[0].Rows[0]["Comp_Id"].ToString();
            txtEmpCode.Text = dsGet.Tables[0].Rows[0]["Emp_Code"].ToString();
            txtemployeename.Text = dsGet.Tables[0].Rows[0]["Emp_Name"].ToString();
            drpDepartment.SelectedValue = dsGet.Tables[0].Rows[0]["Dept_Id"].ToString();
            Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue);
            drpDesignation.SelectedValue = dsGet.Tables[0].Rows[0]["Desig_Id"].ToString();
            drpProjectManager.SelectedValue = dsGet.Tables[0].Rows[0]["Rep_Manager"].ToString();
            txtDOJ.Text = dsGet.Tables[0].Rows[0]["DOJ1"].ToString();
            txtDateOfLeaving.Text = dsGet.Tables[0].Rows[0]["DOL1"].ToString();
            txtpanno.Text = dsGet.Tables[0].Rows[0]["PanNo"].ToString();
            txtPFCode.Text = dsGet.Tables[0].Rows[0]["PF_AcNo"].ToString();
            txtESICode.Text = dsGet.Tables[0].Rows[0]["ESI_AcNO"].ToString();
            drpStatus.SelectedValue = dsGet.Tables[0].Rows[0]["Status"].ToString();
            hddEmpNo.Value = dsGet.Tables[0].Rows[0]["EmpNo"].ToString();
            hddCrmUserId.Value = dsGet.Tables[0].Rows[0]["CRMUserId"].ToString();
        }
        #endregion

        #region Bank Details
        if (dsGet.Tables[1].Rows.Count > 0)
        {
            txtBankAccno.Text = dsGet.Tables[1].Rows[0]["AcNO"].ToString();
            txtbankaddress.Text = dsGet.Tables[1].Rows[0]["Branch"].ToString();
            txtBankIFSC.Text = dsGet.Tables[1].Rows[0]["IFSC"].ToString();
            txtBankName.Text = dsGet.Tables[1].Rows[0]["Bank_Name"].ToString();
            txtBankAccno2.Text = dsGet.Tables[1].Rows[0]["AcNO2"].ToString();
            txtbankaddress2.Text = dsGet.Tables[1].Rows[0]["Branch2"].ToString();
            txtBankIFSC2.Text = dsGet.Tables[1].Rows[0]["IFSC2"].ToString();
            txtBankName2.Text = dsGet.Tables[1].Rows[0]["Bank_Name2"].ToString();
        }
        #endregion

        #region Salary Details
        if (dsGet.Tables[2].Rows.Count > 0)
        {
            txtNetSalary.Text = dsGet.Tables[2].Rows[0]["Net_Salary"].ToString();
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_BasicSalary"]))
            {
                chkBS.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_BasicSalary"]);
                RBBSFixed.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_BasicFixed"]);
                RBBSPer.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_BasicPer"]);
                txtBasicsalary.Text = dsGet.Tables[2].Rows[0]["Basic_Salary"].ToString();
                txtBasicsalary.Enabled = true;
                txtBasicsalaryValue.Value = dsGet.Tables[2].Rows[0]["Basic_SalaryValue"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_PF"]))
            {
                chkPF.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_PF"]);
                rbPFFixed.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_PFFixed"]);
                rbPFPer.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_PFPer"]);
                txtPFSelf.Text = dsGet.Tables[2].Rows[0]["PF_Employee"].ToString();
                txtPFSelfValue.Value = dsGet.Tables[2].Rows[0]["PF_EmployeeValue"].ToString();
                txtPFComp.Text = dsGet.Tables[2].Rows[0]["PF_Employer"].ToString();
                txtPFSelf.Enabled = true;
                txtPFComp.Enabled = true;
                txtPFCompValue.Value = dsGet.Tables[2].Rows[0]["PF_EmployerValue"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_ESIC"]))
            {
                ChkESIC.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_ESIC"]);
                rbESICFixed.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_ESICFixed"]);
                rbESICPer.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_ESICPPer"]);
                txtESICSelf.Text = dsGet.Tables[2].Rows[0]["ESIC_Employee"].ToString();
                txtESICSelf.Enabled = true;
                txtESICSelfValue.Value = dsGet.Tables[2].Rows[0]["ESIC_EmployeeValue"].ToString();
                txtESICComp.Text = dsGet.Tables[2].Rows[0]["ESIC_Employer"].ToString();
                txtESICComp.Enabled = true;
                txtESICCompValue.Value = dsGet.Tables[2].Rows[0]["ESIC_EmployerValue"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_HRA"]))
            {
                chkHRA.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_HRA"]);
                rbHRAFixed.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_HRAFixed"]);
                rbHRAPer.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_HRAPer"]);
                txtHRA.Text = dsGet.Tables[2].Rows[0]["HRA"].ToString();
                txtHRA.Enabled = true;
                txtHRAValue.Value = dsGet.Tables[2].Rows[0]["HRAValue"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_WA"]))
            {
                ChkWA.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_WA"]);
                RbWAFixed.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_WAFixed"]);
                RbWAPer.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_WAPer"]);
                TxtwashingAllowance.Text = dsGet.Tables[2].Rows[0]["WA"].ToString();
                TxtwashingAllowance.Enabled = true;
                TxtwashingAllowanceValue.Value = dsGet.Tables[2].Rows[0]["WAValue"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_MA"]))
            {
                chkMA.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_MA"]);
                rbMAFixed.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_MAFixed"]);
                rbMAPer.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_MAPer"]);
                txtMediacl.Text = dsGet.Tables[2].Rows[0]["MA"].ToString();
                txtMediacl.Enabled = true;
                txtMediaclValue.Value = dsGet.Tables[2].Rows[0]["MAValue"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_CA"]))
            {
                chkConv.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_CA"]);
                RbTAFixed.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_CAFixed"]);
                RbTAPer.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_CAPer"]);
                txtConveyance.Text = dsGet.Tables[2].Rows[0]["CA"].ToString();
                txtConveyance.Enabled = true;
                txtConveyanceValue.Value = dsGet.Tables[2].Rows[0]["CAValue"].ToString();
            }

            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_LA"]))
            {
                chkLA.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_LA"]);
                rbLAFixed.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_LAFixed"]);
                rbLAPer.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_LAPer"]);
                txtLAPay.Text = dsGet.Tables[2].Rows[0]["LA"].ToString();
                txtLAPay.Enabled = true;
                txtLAPayValue.Value = dsGet.Tables[2].Rows[0]["LAValue"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_FA"]))
            {
                chkFoodAll.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_FA"]);
                rbFAFixed.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_FAFixed"]);
                rbFAPer.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_FAPer"]);
                txtFoodAll.Text = dsGet.Tables[2].Rows[0]["FA"].ToString();
                txtFoodAll.Enabled = true;
                txtFoodAllValue.Value = dsGet.Tables[2].Rows[0]["FAValue"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_OA"]))
            {
                chkOthers.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_OA"]);
                RBOAFixed.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_OAFixed"]);
                RBOAPer.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_OAPer"]);
                txtOthers.Text = dsGet.Tables[2].Rows[0]["OA"].ToString();
                txtOthers.Enabled = true;
                txtOthersValue.Value = dsGet.Tables[2].Rows[0]["OAValue"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_TDS"]))
            {
                chktdsapply.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_TDS"]);
                rbTDSFixed.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_TDSFixed"]);
                rbTDSPer.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_TDSPer"]);
                txtTDS.Text = dsGet.Tables[2].Rows[0]["TDS"].ToString();
                txtTDS.Enabled = true;
                txtTDSValue.Value = dsGet.Tables[2].Rows[0]["TDSValue"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_OD"]))
            {
                chkDeductOther.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_OD"]);
                rbDFixed.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_ODFixed"]);
                rbDPer.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["Is_ODPer"]);
                txtDeductOther.Text = dsGet.Tables[2].Rows[0]["OD"].ToString();
                txtDeductOther.Enabled = true;
                txtDeductOtherValue.Value = dsGet.Tables[2].Rows[0]["ODValue"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_DAL"]))
            {
                chkDALocal.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_DAL"]);
                txtDALocal.Text = dsGet.Tables[2].Rows[0]["DAL"].ToString();
                txtDALocal.Enabled = true;
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_DAEX"]))
            {
                chkDAEx.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_DAEX"]);
                txtDAEx.Text = dsGet.Tables[2].Rows[0]["DAEX"].ToString();
                txtDAEx.Enabled = true;
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_NSA"]))
            {
                chkNightAll.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_NSA"]);
                txtNightAll.Text = dsGet.Tables[2].Rows[0]["NSA"].ToString();
                txtNightAll.Enabled = true;
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_PL"]))
            {
                chkPaidLeave.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_PL"]);
                txtNoOfPaidLeave.Text = dsGet.Tables[2].Rows[0]["PL"].ToString();
                txtNoOfPaidLeave.Enabled = true;
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_CL"]))
            {
                ChkCL.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_CL"]);
                txtCL.Text = dsGet.Tables[2].Rows[0]["CL"].ToString();
                txtCL.Enabled = true;
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_LCI"]))
            {
                chkLateCheckIn.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_LCI"]);
                txtLateCheckIn.Text = dsGet.Tables[2].Rows[0]["LCI"].ToString();
                txtLateCheckIn.Enabled = true;
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_ECO"]))
            {
                chkEarlyCheckOut.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_ECO"]);
                txtEarlyCheckOut.Text = dsGet.Tables[2].Rows[0]["ECO"].ToString();
                txtEarlyCheckOut.Enabled = true;
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_YB"]))
            {
                chkBonus.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_YB"]);
                txtBonus.Enabled = true;
                txtBonus.Text = dsGet.Tables[2].Rows[0]["YB"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_WH"]))
            {
                chkMinHour.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_WH"]);
                txtWorkingHour.Text = dsGet.Tables[2].Rows[0]["WH"].ToString();
                txtWorkingHour.Enabled = true;
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_OTPD"]))
            {
                chkOverTime.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_OTPD"]);
                txtOverTime.Enabled = true;
                txtOverTime.Text = dsGet.Tables[2].Rows[0]["OTPD"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_OTPH"]))
            {
                chkOverTimePH.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_OTPH"]);
                txtOverTimePH.Enabled = true;
                txtOverTimePH.Text = dsGet.Tables[2].Rows[0]["OTPH"].ToString();
            }
            if (Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_CIT"]))
            {
                chkCHeckInTime.Checked = Convert.ToBoolean(dsGet.Tables[2].Rows[0]["IS_CIT"]);
                txtCheckIn.Enabled = true;
                txtCheckIn.Text = dsGet.Tables[2].Rows[0]["CIT"].ToString();
            }
            txtWorkingTimeFRom.Text = dsGet.Tables[2].Rows[0]["WTF"].ToString();
            txtWorkingTimeTo.Text = dsGet.Tables[2].Rows[0]["WTT"].ToString();
            string[] Size = dsGet.Tables[2].Rows[0]["WD"].ToString().Split(',');
            foreach (ListItem size in drpWorkingDay.Items)
            {
                for (int i = 0; i < Size.Length; i++)
                {
                    if (size.Value.ToString() == Size[i].Trim().ToString())
                        size.Selected = true;
                }
            }

            txtCTC.Value = dsGet.Tables[2].Rows[0]["CTC"].ToString();
            txtInHand.Value = dsGet.Tables[2].Rows[0]["In_Hand"].ToString();
        }
        #endregion

        #region Family Details
        if (dsGet.Tables[3].Rows.Count > 0)
        {
            txtfathername.Text = dsGet.Tables[3].Rows[0]["Father_Name"].ToString();
            txtMotherName.Text = dsGet.Tables[3].Rows[0]["Mother_name"].ToString();
            drpSpouse.SelectedValue = dsGet.Tables[3].Rows[0]["Spouse"].ToString();
            txtSpouse.Text = dsGet.Tables[3].Rows[0]["Spouse_Name"].ToString();
            TxtChild.Text = dsGet.Tables[3].Rows[0]["NoOfChild"].ToString();
            txtFatherCntctNo.Text = dsGet.Tables[3].Rows[0]["ContactNo"].ToString();
        }
        #endregion

        #region Personal Details
        if (dsGet.Tables[4].Rows.Count > 0)
        {
            drpGender.SelectedValue = dsGet.Tables[4].Rows[0]["Gender"].ToString();
            txtDOB.Text = dsGet.Tables[4].Rows[0]["DOB1"].ToString();
            drpMarriedStatus.SelectedValue = dsGet.Tables[4].Rows[0]["Marrital_Status"].ToString();
            txtEducation.Text = dsGet.Tables[4].Rows[0]["Qualification"].ToString();
            txtDOM.Text = dsGet.Tables[4].Rows[0]["DOM1"].ToString();
            TxtMaternityLeave.Text = dsGet.Tables[4].Rows[0]["Metarnitiy_Leave"].ToString();
            txtphoneno.Text = dsGet.Tables[4].Rows[0]["Phone_No"].ToString();
            txtmobno.Text = dsGet.Tables[4].Rows[0]["Mobile_No"].ToString();
            txtCUG.Text = dsGet.Tables[4].Rows[0]["CUG_MobileNO"].ToString();
            txtemail.Text = dsGet.Tables[4].Rows[0]["Personal_Mail"].ToString();
            txtOfficEmail.Text = dsGet.Tables[4].Rows[0]["Office_Mail"].ToString();
            txtpresentaddress.Text = dsGet.Tables[4].Rows[0]["Present_Add"].ToString();
            txtparmentaddress.Text = dsGet.Tables[4].Rows[0]["Permanent_add"].ToString();
            ChksameAddr.Checked = Convert.ToBoolean(dsGet.Tables[4].Rows[0]["IsAddSame"]);
        }
        #endregion

        #region Personal Details
        if (dsGet.Tables[5].Rows.Count > 0)
        {
            txtLastCompanyName.Text = dsGet.Tables[5].Rows[0]["Comp_Name"].ToString();
            TxtLastCompanyAdd.Text = dsGet.Tables[5].Rows[0]["Comp_Add"].ToString();
            TxtLastCtc.Text = dsGet.Tables[5].Rows[0]["last_CTC"].ToString();
            TxtLastPfNo.Text = dsGet.Tables[5].Rows[0]["PFAcNo"].ToString();
            txtrefname.Text = dsGet.Tables[5].Rows[0]["Ref_Name"].ToString();
            txtrefcontactno.Text = dsGet.Tables[5].Rows[0]["Ref_contact"].ToString();
            TxtSecRefName.Text = dsGet.Tables[5].Rows[0]["Ref_Name2"].ToString();
            TxtSecRefContactNo.Text = dsGet.Tables[5].Rows[0]["Ref_Contact2"].ToString();
        }
        #endregion

        #region Documents
        foreach (DataRow drDoc in dsGet.Tables[0].Rows)
        {
            if (ViewState["tbl"] == null)
            {
                SNO = 1;
                dtRecord.Columns.Add("sno");
                dtRecord.Columns.Add("Docu_Name");
                dtRecord.Columns.Add("Docu_Id");
                dtRecord.Columns.Add("File_Name");
                dtRecord.Columns.Add("Delid");
                dtRecord.Columns.Add("Id");
            }
            else
            {
                dtRecord = (DataTable)ViewState["tbl"];
                SNO = dtRecord.Rows.Count + 1;
            }
            DataRow dtrow = dtRecord.NewRow();
            dtrow["SNO"] = SNO;
            dtrow["Id"] = HddRowID.Value;
            dtrow["Docu_Id"] = drpDocument.SelectedValue;
            dtrow["Docu_Name"] = drpDocument.SelectedItem.Text;
            //dtrow["File_Name"] = _FileName;
            dtrow["Delid"] = "0";
            dtRecord.Rows.Add(dtrow);
            ViewState["tbl"] = dtRecord;
            rep.DataSource = dtRecord;
            rep.DataBind();
        }
        #endregion
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveEmpMain();
    }

    private void SaveEmpMain()
    {
        string _Action = Request.QueryString["EmpId"] == null ? "SAVE" : "UPDATE";
        string _EmpId = Request.QueryString["EmpId"] == null ? "0" : Request.QueryString["EmpId"];

        string _UserId = Soft["UserId"];
        DataSet DsMain = payroll.Emp_Main(_Action, _EmpId, DrpCompanies.SelectedValue.ToString(), txtEmpCode.Text, txtemployeename.Text, drpDepartment.SelectedValue.ToString(), drpDesignation.SelectedValue.ToString(), drpProjectManager.SelectedValue.ToString(), txtDOJ.Text, txtDateOfLeaving.Text, txtpanno.Text, txtPFCode.Text, txtESICode.Text, drpStatus.SelectedItem.Text, hddEmpNo.Value, hddCrmUserId.Value, _UserId, chkAttandance.Checked.ToString());
        if (DsMain.Tables[0].Rows.Count > 0)
        {
            if (DsMain.Tables[0].Rows[0]["Result"].ToString() == "")
            {
                //Bank Details
                payroll.Emp_Bank(_Action, DsMain.Tables[0].Rows[0]["EMPID"].ToString(), _UserId, txtBankName.Text, txtBankAccno.Text, txtbankaddress.Text, txtBankIFSC.Text, txtBankName2.Text, txtBankAccno2.Text, txtbankaddress2.Text, txtBankIFSC2.Text);

                //Personal Details
                payroll.Emp_PERSONAL(_Action, DsMain.Tables[0].Rows[0]["EMPID"].ToString(), _UserId, drpGender.SelectedItem.Text, txtDOB.Text, drpMarriedStatus.SelectedValue, txtEducation.Text, txtDOM.Text, TxtMaternityLeave.Text, txtphoneno.Text, txtmobno.Text, txtCUG.Text, txtemail.Text, txtOfficEmail.Text, txtpresentaddress.Text, txtparmentaddress.Text, ChksameAddr.Checked.ToString());

                //Family Details
                payroll.Emp_Family(_Action, DsMain.Tables[0].Rows[0]["EMPID"].ToString(), _UserId, txtfathername.Text, txtMotherName.Text, drpSpouse.SelectedValue.ToString(), txtSpouse.Text, TxtChild.Text, txtFatherCntctNo.Text);

                //Previous Company Details
                payroll.Emp_PrevComp(_Action, DsMain.Tables[0].Rows[0]["EMPID"].ToString(), _UserId, txtLastCompanyName.Text, TxtLastCompanyAdd.Text, TxtLastCtc.Text, TxtLastPfNo.Text, txtrefname.Text, txtrefcontactno.Text, TxtSecRefName.Text, TxtSecRefContactNo.Text);

                //Salary Details
                string _WorkingDays = "";
                foreach (ListItem item in drpWorkingDay.Items)
                {
                    if (item.Selected)
                    {
                        if (_WorkingDays != "")
                        {
                            _WorkingDays += " ," + item.Value;
                        }
                        else
                        {
                            _WorkingDays = item.Value;
                        }
                    }
                }

                payroll.Emp_Salary(_Action, DsMain.Tables[0].Rows[0]["EMPID"].ToString(), _UserId, txtNetSalary.Text, chkBS.Checked.ToString(), RBBSFixed.Checked.ToString(), RBBSPer.Checked.ToString(), txtBasicsalary.Text, txtBasicsalaryValue.Value, chkPF.Checked.ToString(), rbPFFixed.Checked.ToString(), rbPFPer.Checked.ToString(), txtPFSelf.Text, txtPFSelfValue.Value, txtPFComp.Text, txtPFCompValue.Value, ChkESIC.Checked.ToString(), rbESICFixed.Checked.ToString(), rbESICPer.Checked.ToString(), txtESICSelf.Text, txtESICSelfValue.Value, txtESICComp.Text, txtESICCompValue.Value, chkHRA.Checked.ToString(), rbHRAFixed.Checked.ToString(), rbHRAPer.Checked.ToString(), txtHRA.Text, txtHRAValue.Value, ChkWA.Checked.ToString(), RbWAFixed.Checked.ToString(), RbWAPer.Checked.ToString(), TxtwashingAllowance.Text, TxtwashingAllowanceValue.Value, chkMA.Checked.ToString(), rbMAFixed.Checked.ToString(), rbMAPer.Checked.ToString(), txtMediacl.Text, txtMediaclValue.Value, chkConv.Checked.ToString(), RbTAFixed.Checked.ToString(), RbTAPer.Checked.ToString(), txtConveyance.Text, txtConveyanceValue.Value, chkDALocal.Checked.ToString(), txtDALocal.Text, chkDAEx.Checked.ToString(), txtDAEx.Text, chkLA.Checked.ToString(), rbLAFixed.Checked.ToString(), rbLAPer.Checked.ToString(), txtLAPay.Text, txtLAPayValue.Value, chkFoodAll.Checked.ToString(), rbFAFixed.Checked.ToString(), rbFAPer.Checked.ToString(), txtFoodAll.Text, txtFoodAllValue.Value, chkOthers.Checked.ToString(), RBOAFixed.Checked.ToString(), RBOAPer.Checked.ToString(), txtOthers.Text, txtOthersValue.Value, chkNightAll.Checked.ToString(), txtNightAll.Text, chktdsapply.Checked.ToString(), rbTDSFixed.Checked.ToString(), rbTDSPer.Checked.ToString(), txtTDS.Text, txtTDSValue.Value, chkDeductOther.Checked.ToString(), rbDFixed.Checked.ToString(), rbDPer.Checked.ToString(), txtDeductOther.Text, txtDeductOtherValue.Value, chkPaidLeave.Checked.ToString(), txtNoOfPaidLeave.Text, ChkCL.Checked.ToString(), txtCL.Text, chkLateCheckIn.Checked.ToString(), txtLateCheckIn.Text, chkEarlyCheckOut.Checked.ToString(), txtEarlyCheckOut.Text, chkBonus.Checked.ToString(), txtBonus.Text, chkMinHour.Checked.ToString(), txtWorkingHour.Text, chkOverTime.Checked.ToString(), txtOverTime.Text, chkOverTimePH.Checked.ToString(), txtOverTimePH.Text, chkCHeckInTime.Checked.ToString(), txtCheckIn.Text, txtWorkingTimeFRom.Text, txtWorkingTimeTo.Text, _WorkingDays, txtCTC.Value, txtInHand.Value);

                //Document Details
                if (ViewState["tbl"] != null)
                {
                    dtRecord = (DataTable)ViewState["tbl"];
                    foreach (DataRow drr in dtRecord.Rows)
                    {
                        payroll.Emp_Document(DsMain.Tables[0].Rows[0]["EMPID"].ToString(), _UserId, drr["Docu_Id"].ToString(), drr["File_Name"].ToString(), drr["Id"].ToString(), drr["Delid"].ToString());
                    }
                }
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record " + _Action + " Successfully');window.location ='PayrollRep.aspx'", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + DsMain.Tables[0].Rows[0]["Result"].ToString() + "');window.location ='PayrollRep.aspx'", true);

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PayrollRep.aspx");
    }

    protected void drpMarriedStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDOM.ReadOnly = (drpMarriedStatus.SelectedValue.ToString() == "Married") ? false : true;
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        string _EmpCode = "";
        if (drpDepartment.SelectedIndex > 0)
        {
            DataSet dss = getdata.GetDepartment("Select", drpDepartment.SelectedValue, "", "", "");
            _EmpCode = dss.Tables[0].Rows[0]["Dept_Code"].ToString();
        }
        Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue.ToString());
        GetEmpCode(drpDepartment.SelectedValue.ToString());
    }

    public void GetEmpCode(string DepT_ID)
    {
        string query = "select (select Dept_Code from tbl_Department where dept_Id=" + DepT_ID + ") +'/'+ cast(isnull(max(EmpNo),0)+1 as nvarchar(50)) as EMp_Code,isnull(max(EmpNo),0)+1 as EMpNO from tbl_EMpMaster where Dept_Id=" + DepT_ID + "";
        DataSet ds = data.getDataSet(query);
        txtEmpCode.Text = ds.Tables[0].Rows[0]["EMp_Code"].ToString();
        hddEmpNo.Value = ds.Tables[0].Rows[0]["EMpNO"].ToString();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string strFolder = Server.MapPath("~/upload/Document/");
        string _FileName = "";

        string path = @"" + strFolder;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        if (ViewState["tbl"] != null)
        {
            dtRecord = (DataTable)ViewState["tbl"];
            if (dtRecord.Rows.Count > 0)
            {
                DataRow[] filteredRows = dtRecord.Select("Docu_Name = '" + drpDocument.SelectedItem.Text + "'");
                if (filteredRows.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Document Already Exists')", true);
                }
            }
        }

        if (imageupload.HasFile)
        {
            _FileName = txtEmpCode.Text.Replace("/", "_") + "_" + drpDocument.SelectedItem.Text + "" + Path.GetExtension(imageupload.FileName);
            imageupload.SaveAs(strFolder + _FileName);
        }
        if (btnAdd.Text == "Add")
        {
            if (ViewState["tbl"] == null)
            {
                SNO = 1;
                dtRecord.Columns.Add("sno");
                dtRecord.Columns.Add("Docu_Name");
                dtRecord.Columns.Add("Docu_Id");
                dtRecord.Columns.Add("File_Name");
                dtRecord.Columns.Add("Delid");
                dtRecord.Columns.Add("Id");
            }
            else
            {
                dtRecord = (DataTable)ViewState["tbl"];
                SNO = dtRecord.Rows.Count + 1;
            }
            DataRow dtrow = dtRecord.NewRow();
            dtrow["SNO"] = SNO;
            dtrow["Id"] = HddRowID.Value;
            dtrow["Docu_Id"] = drpDocument.SelectedValue;
            dtrow["Docu_Name"] = drpDocument.SelectedItem.Text;
            dtrow["File_Name"] = _FileName;
            dtrow["Delid"] = "0";
            dtRecord.Rows.Add(dtrow);
            ViewState["tbl"] = dtRecord;
            rep.DataSource = dtRecord;
            rep.DataBind();
        }
        else
        {
            dtRecord = (DataTable)ViewState["tbl"];
            int rowind = Convert.ToInt32(ViewState["rowid"].ToString());

            dtRecord.Rows[rowind]["Id"] = HddRowID.Value;
            dtRecord.Rows[rowind]["Docu_Id"] = drpDocument.SelectedValue;
            dtRecord.Rows[rowind]["Docu_Name"] = drpDocument.SelectedItem.Text;
            dtRecord.Rows[rowind]["File_Name"] = _FileName;
            dtRecord.Rows[rowind]["Delid"] = "0";
            rep.DataSource = dtRecord;
            rep.DataBind();
        }
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string _Action = e.CommandName;
        int _EmpId = Convert.ToInt32(e.CommandArgument)-1;
        dtRecord = (DataTable)ViewState["tbl"];
        dtRecord.Rows.RemoveAt(_EmpId);
        dtRecord.AcceptChanges();
        ViewState["tbl"] = dtRecord;
        rep.DataSource = dtRecord;
        rep.DataBind();
    }
}