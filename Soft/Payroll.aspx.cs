using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_Payroll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void drpSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

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

    }



    protected void drpMarriedStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDOM.ReadOnly = (drpMarriedStatus.SelectedValue.ToString() == "Married") ? false : true;
    }
}