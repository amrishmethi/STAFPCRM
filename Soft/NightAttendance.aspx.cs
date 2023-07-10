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

public partial class Soft_NightAttendance : System.Web.UI.Page
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
            Gd.FillUser(drpEmployee, drpdepartment.SelectedValue);
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (Request.QueryString["id"] != null)
                FillRecord(Request.QueryString["id"]);
        }
    }

    private void FillRecord(string Id)
    {
        DataSet dss = getdata.IU_NIGHTATTENDANCE("SELECT", Id, "0", "", "", "", "");
        drpdepartment.SelectedValue = dss.Tables[0].Rows[0]["Dept_Id"].ToString();
        Gd.FillUser(drpEmployee, drpdepartment.SelectedValue);
        drpEmployee.SelectedValue = dss.Tables[0].Rows[0]["FK_EmpId"].ToString();
        txtFromDate.Text = dss.Tables[0].Rows[0]["AttendanceDate"].ToString();
        txtReason.Text = dss.Tables[0].Rows[0]["Remarks"].ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _Action = Request.QueryString["Id"] == null ? "SAVE" : "UPDATE";
        string _Id = Request.QueryString["Id"] == null ? "0" : Request.QueryString["Id"];
        string _UserId = Soft["UserId"];

        DataSet ds = getdata.IU_NIGHTATTENDANCE(_Action, _Id, drpEmployee.SelectedValue, drpdepartment.SelectedValue, data.ConvertToDateTime(txtFromDate.Text).ToString(), data.ConvertToDateTime(txtFromDate.Text).ToString(), txtReason.Text);
        if (ds.Tables[0].Rows[0]["Result"].ToString() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record " + _Action + " Successfully');window.location ='NightAttendanceRep.aspx';", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Not Save Successfully');window.location ='NightAttendanceRep.aspx'", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("NightAttendanceRep.aspx");
    }

    protected void drpdepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.FillUser(drpEmployee, drpdepartment.SelectedValue);
    }
}