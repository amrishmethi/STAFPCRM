﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_NightAttendanceRep : System.Web.UI.Page
{
    private HttpCookie Soft;
    Data data = new Data();
    DataSet dsResult = new DataSet();
    GetData Gd = new GetData();
    Master getdata = new Master();
    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            Gd.fillDepartment(drpdepartment);
            Gd.FillUser(drpEmployee, drpdepartment.SelectedValue);
            txtFromDate.Text = txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            GetReport();
        }
    }

    private void GetReport()
    {
        string _FROMDate = txtFromDate.Text == "" ? "" : data.ConvertToDateTime(txtFromDate.Text).ToString();
        string _ToDate = txtToDate.Text == "" ? "" : data.ConvertToDateTime(txtToDate.Text).ToString();
        DataSet dss = getdata.IU_NIGHTATTENDANCE("SELECT", "0", drpEmployee.SelectedValue, drpdepartment.SelectedValue, _FROMDate, _ToDate, "");
        rep.DataSource = dss;
        rep.DataBind();
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            DataSet dss = getdata.IU_NIGHTATTENDANCE("DELETE", e.CommandArgument.ToString(), "0", "0", "", "", "");
            if (dss.Tables[0].Rows[0]["Result"].ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record DELETE Successfully');window.location ='NightAttendanceRep.aspx'", true);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetReport();
    }

    protected void drpdepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

        Gd.FillUser(drpEmployee, drpdepartment.SelectedValue);
    }
}