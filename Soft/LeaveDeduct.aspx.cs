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
using System.Web.Services;

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
            Gd.fillDepartment(drpdepartment);
            Gd.FillUser(drpEmployee,drpdepartment.SelectedValue);
            txtFromDate.Text = txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TxtDay.Value = "1";
            if (Request.QueryString["id"] != null)
                FillRecord(Request.QueryString["id"]);
        }
    }

    private void FillRecord(string Id)
    {
        DataSet dss = getdata.IU_LEAVE("SELECT", Id, "0", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
        drpdepartment.SelectedValue = dss.Tables[0].Rows[0]["Dept_Id"].ToString();
        Gd.FillUser(drpEmployee, drpdepartment.SelectedValue);
        drpEmployee.SelectedValue = dss.Tables[0].Rows[0]["FK_EmpId"].ToString();
        txtFromDate.Text = dss.Tables[0].Rows[0]["Leave_Date"].ToString();
        txtToDate.Text = dss.Tables[0].Rows[0]["LeaveTo_Date"].ToString();
        drpLeaveType.SelectedValue = dss.Tables[0].Rows[0]["Leave_Type"].ToString();
        txtReason.Text = dss.Tables[0].Rows[0]["Reason"].ToString();
        GetRemainig();
       TxtDay.Value = dss.Tables[0].Rows[0]["Approved_Leave"].ToString();

        txtRemaininig.Text = (Convert.ToInt16(txtRemaininig.Text) + Convert.ToInt16(TxtDay.Value)).ToString();
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


        if (Convert.ToDouble(txtRemaininig.Text) <= Convert.ToDouble(TxtDay.Value))
        {
            DataSet ds = getdata.IU_LEAVE(_Action, _Id, drpEmployee.SelectedValue, drpLeaveType.SelectedItem.Text.ToString(), TxtDay.Value, data.ConvertToDateTime(txtFromDate.Text).ToString(), data.ConvertToDateTime(txtToDate.Text).ToString(), TxtDay.Value, data.ConvertToDateTime(txtToDate.Text).ToString(), data.ConvertToDateTime(txtFromDate.Text).ToString(), txtReason.Text, DateTime.Now.ToString(), "True", "True", DateTime.Now.ToString(), _UserId, "AdminDetect");
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

    protected void drpLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRemainig();
    }

    private void GetRemainig()
    {
        if (drpLeaveType.SelectedIndex > 0)
        {
            DataSet dss = data.getDataSet("GetPaindingLeave '" + data.ConvertToDateTime(txtToDate.Text) + "','" + drpLeaveType.SelectedItem.Text + "','" + drpEmployee.SelectedValue + "'");
            if (dss.Tables[0].Rows.Count > 0)
                txtRemaininig.Text = dss.Tables[0].Rows[0]["Remain"].ToString();
            else
                txtRemaininig.Text = "0";
        }
        else
            txtRemaininig.Text = "0";
    }

    [WebMethod]
    public static string GetDays(string From, string To)
    {
        Data data = new Data();
        DateTime _From = data.ConvertToDateTime(From);
        DateTime _To = data.ConvertToDateTime(To).AddDays(1);
        var _totalDay = (_To - _From).TotalDays;


        string ApproveLeave = _totalDay.ToString();
        return ApproveLeave;


    }

    protected void drpdepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

        Gd.FillUser(drpEmployee, drpdepartment.SelectedValue);
    }
}