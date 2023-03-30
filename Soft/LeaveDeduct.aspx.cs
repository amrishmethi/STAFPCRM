using CuteEditor.Impl;
using CuteEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_LeaveDeduct : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    Data data = new Data();
    GetData Gd = new GetData();
    private HttpCookie Soft;
    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            Gd.FillUser(drpEmployee);
            if (Request.QueryString["id"] != null)
                FillRecord(Request.QueryString["id"]);
        }
    }

    private void FillRecord(string Id)
    {
        DataSet dss = getdata.IU_LEAVE("SELECT", Id, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
        drpEmployee.SelectedValue = dss.Tables[0].Rows[0]["FK_EmpId"].ToString();
        txtFromDate.Text = dss.Tables[0].Rows[0]["Leave_Date"].ToString();
        txtToDate.Text = dss.Tables[0].Rows[0]["LeaveTo_Date"].ToString();
        drpLeaveType.SelectedItem.Text = dss.Tables[0].Rows[0]["Leave_Type"].ToString();
        txtReason.Text = dss.Tables[0].Rows[0]["Reason"].ToString();
        if (dss.Tables[0].Rows[0]["LeaveValue"].ToString() == "HALF DAY")
            rbHalfDay.Checked = true;
        else
            rbFullDay.Checked = true;
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _Action = Request.QueryString["Id"] == null ? "SAVE" : "UPDATE";
        string _Id = Request.QueryString["Id"] == null ? "0" : Request.QueryString["Id"];
        string _UserId = Soft["UserId"];

        string ApproveLeave = "0.00";
        if (rbFullDay.Checked)
            ApproveLeave = "1";
        if (rbHalfDay.Checked)
            ApproveLeave = "0.5";
        //DataSet dsApproveLeave = data.getDataSet("select PL+CL as ApprovedLeave From tbl_EMPSalaryDetails WHERE Emp_Id=" + drpEmployee.SelectedValue);
        //double ApprovedLeave = Convert.ToDouble(dsApproveLeave.Tables[0].Rows[0]["ApprovedLeave"]);
        double ApprovedLeave = 100;
        DataSet dss = data.getDataSet("select isnull(sum(Approved_Leave),0)ApproveLeave from tbl_Leave WHERE FK_EmpId=" + drpEmployee.SelectedValue);
        double TotalLeave = Convert.ToDouble(dss.Tables[0].Rows[0]["ApproveLeave"]);

        double RemainingLeave = ApprovedLeave - TotalLeave;

        if (RemainingLeave >= Convert.ToDouble(ApproveLeave))
        {
            DataSet ds = getdata.IU_LEAVE(_Action, _Id, drpEmployee.SelectedValue, drpLeaveType.SelectedItem.Text.ToString(), ApproveLeave, data.ConvertToDateTime(txtFromDate.Text).ToString(), data.ConvertToDateTime(txtToDate.Text).ToString(), ApproveLeave, data.ConvertToDateTime(txtToDate.Text).ToString(), data.ConvertToDateTime(txtFromDate.Text).ToString(), txtReason.Text, DateTime.Now.ToString(), "True", "True", DateTime.Now.ToString(), _UserId, "AdminDetect");
            if (ds.Tables[0].Rows[0]["Result"].ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record " + _Action + " Successfully');window.location ='LeaveDeductRep.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Not Save Successfully');window.location ='LeaveDeductRep.aspx'", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "alert('No More Leaves Remain'); ", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("LeaveDeductRep.aspx");
    }
}