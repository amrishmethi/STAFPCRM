using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_Payroll : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData Gd = new GetData();
    Data data = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            foreach (ListItem size in drpWorkingDay.Items)
            {
                if (size.Value.ToString() != "Sunday")
                {
                    size.Selected = true;
                }
            }

            Gd.FillCompany(DrpCompanies);
            Gd.fillDepartment(drpDepartment);
            Gd.FillUser(drpProjectManager);
        }
    }

    protected void drpGender_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ChksameAddr_CheckedChanged(object sender, EventArgs e)
    {
        txtparmentaddress.Text = (ChksameAddr.Checked) ? txtpresentaddress.Text : "";
        txtparmentaddress.Enabled = (ChksameAddr.Checked) ? false : true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }

    protected void btnSaveNext_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void chkDOL_CheckedChanged(object sender, EventArgs e)
    {
        txtDateOfLeaving.Enabled = (chkDOL.Checked);
        txtDateOfLeaving.Text = (chkDOL.Checked) ? txtDateOfLeaving.Text : "";
    }

    protected void ChkEsi_CheckedChanged(object sender, EventArgs e)
    {
        txtESICode.Enabled = (ChkEsi.Checked);
        txtESICode.Text = (ChkEsi.Checked) ? txtESICode.Text : "";
    }

    protected void chkBasic_CheckedChanged(object sender, EventArgs e)
    {
        txtPFCode.Enabled = (chkBasic.Checked);
        txtPFCode.Text = (chkBasic.Checked) ? txtPFCode.Text : "";
    }

    protected void chkHRA_CheckedChanged(object sender, EventArgs e)
    {
        txtHRA.Enabled = (chkHRA.Checked);
        txtHRA.Text = (chkHRA.Checked) ? txtHRA.Text : "";

    }

    protected void ChkWs_CheckedChanged(object sender, EventArgs e)
    {
        TxtwashingAllowance.Enabled = (ChkWs.Checked);
        TxtwashingAllowance.Text = (ChkWs.Checked) ? TxtwashingAllowance.Text : "";
    }

    protected void chkMA_CheckedChanged(object sender, EventArgs e)
    {
        txtMediacl.Enabled = (chkMA.Checked);
        txtMediacl.Text = (chkMA.Checked) ? txtMediacl.Text : "";
    }

    protected void chkConv_CheckedChanged(object sender, EventArgs e)
    {
        txtConveyance.Enabled = (chkConv.Checked);
        txtConveyance.Text = (chkConv.Checked) ? txtConveyance.Text : "";
    }

    protected void chkDP_CheckedChanged(object sender, EventArgs e)
    {
        txtDPay.Enabled = (chkDP.Checked);
        txtDPay.Text = (chkDP.Checked) ? txtDPay.Text : "";
    }

    protected void drpMarriedStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDOM.ReadOnly = (drpMarriedStatus.SelectedValue.ToString() == "Married") ? false : true;
    }

    protected void chkPF_CheckedChanged(object sender, EventArgs e)
    {
        txtPFComp.Enabled = txtPFSelf.Enabled = (chkPF.Checked);
        txtPFComp.Text = (chkPF.Checked) ? txtPFComp.Text : "";
        txtPFSelf.Text = (chkPF.Checked) ? txtPFSelf.Text : "";

    }

    protected void ChkESIC_CheckedChanged(object sender, EventArgs e)
    {
        txtESICSelf.Enabled = txtESICComp.Enabled = ChkESIC.Checked;
        txtESICSelf.Text = (ChkESIC.Checked) ? txtESICSelf.Text : "";
        txtESICComp.Text = (ChkESIC.Checked) ? txtESICComp.Text : "";
    }

    protected void chkDALocal_CheckedChanged(object sender, EventArgs e)
    {
        txtDALocal.Enabled = chkDALocal.Checked;
        txtDALocal.Text = (chkDALocal.Checked) ? txtDALocal.Text : "";
    }

    protected void chkDAEx_CheckedChanged(object sender, EventArgs e)
    {
        txtDAEx.Enabled = chkDAEx.Checked;
        txtDAEx.Text = (chkDAEx.Checked) ? txtDAEx.Text : "";
    }

    protected void chkFoodAll_CheckedChanged(object sender, EventArgs e)
    {
        txtFoodAll.Enabled = chkFoodAll.Checked;
        txtFoodAll.Text = (chkFoodAll.Checked) ? txtFoodAll.Text : "";
    }

    protected void chkOthers_CheckedChanged(object sender, EventArgs e)
    {
        txtOthers.Enabled = chkOthers.Checked;
        txtOthers.Text = (chkOthers.Checked) ? txtOthers.Text : "";
    }

    protected void chkNightAll_CheckedChanged(object sender, EventArgs e)
    {
        txtNightAll.Enabled = chkNightAll.Checked;
        txtNightAll.Text = (chkOthers.Checked) ? txtNightAll.Text : "";
    }

    protected void chktdsapply_CheckedChanged(object sender, EventArgs e)
    {
        txtTDS.Enabled = chktdsapply.Checked;
        txtTDS.Text = (chktdsapply.Checked) ? txtTDS.Text : "";
    }

    protected void chkDeductOther_CheckedChanged(object sender, EventArgs e)
    {
        txtDeductOther.Enabled = chkDeductOther.Checked;
        txtDeductOther.Text = (chkDeductOther.Checked) ? txtDeductOther.Text : "";
    }

    protected void chkPaidLeave_CheckedChanged(object sender, EventArgs e)
    {
        txtNoOfPaidLeave.Enabled = chkPaidLeave.Checked;
        txtNoOfPaidLeave.Text = (chkPaidLeave.Checked) ? txtNoOfPaidLeave.Text : "";
    }

    protected void ChkCL_CheckedChanged(object sender, EventArgs e)
    {
        txtCL.Enabled = ChkCL.Checked;
        txtCL.Text = (ChkCL.Checked) ? txtCL.Text : "";
    }

    protected void chkLateCheckIn_CheckedChanged(object sender, EventArgs e)
    {
        txtLateCheckIn.Enabled = chkLateCheckIn.Checked;
        txtLateCheckIn.Text = (chkLateCheckIn.Checked) ? txtLateCheckIn.Text : "";
    }

    protected void chkEarlyCheckOut_CheckedChanged(object sender, EventArgs e)
    {
        txtEarlyCheckOut.Enabled = chkEarlyCheckOut.Checked;
        txtEarlyCheckOut.Text = (chkEarlyCheckOut.Checked) ? txtEarlyCheckOut.Text : "";
    }

    protected void chkBonus_CheckedChanged(object sender, EventArgs e)
    {
        txtBonus.Enabled = chkBonus.Checked;
        txtBonus.Text = (chkBonus.Checked) ? txtBonus.Text : "";
    }

    protected void chkMinHour_CheckedChanged(object sender, EventArgs e)
    {
        txtWorkingHour.Enabled = chkMinHour.Checked;
        txtWorkingHour.Text = (chkMinHour.Checked) ? txtWorkingHour.Text : "";
    }

    protected void chkOverTime_CheckedChanged(object sender, EventArgs e)
    {
        txtOverTime.Enabled = chkOverTime.Checked;
        txtOverTime.Text = (chkOverTime.Checked) ? txtOverTime.Text : "";
    }

    protected void chkCHeckInTime_CheckedChanged(object sender, EventArgs e)
    {
        txtCheckIn.Enabled = chkCHeckInTime.Checked;
        txtCheckIn.Text = (chkCHeckInTime.Checked) ? txtCheckIn.Text : "";
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue.ToString());
    }

    protected void chkBS_CheckedChanged(object sender, EventArgs e)
    {

        txtBasicsalary.Enabled = chkBS.Checked;
        txtBasicsalary.Text = (chkBS.Checked) ? txtBasicsalary.Text : "";
    }
}