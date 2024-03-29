﻿using CuteEditor.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_LoanEntry : System.Web.UI.Page
{
    private HttpCookie Soft;
    Data data = new Data();
    DataSet dsResult = new DataSet();
    GetData Gd = new GetData();
    Master Master = new Master();
    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            Gd.FillLOanUser(drpEmployee);
            if (Request.QueryString["Id"] != null)
                FillData(Request.QueryString["Id"].ToString());
        }
    }


    private void FillData(string Id)
    {
        DataSet ds = Master.IUD_Loan("SELECT", Id, "", "", "", "", "", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
        drpEmployee.SelectedValue = ds.Tables[0].Rows[0]["EMPID"].ToString();
        txtLoanAmount.Text = ds.Tables[0].Rows[0]["amount"].ToString();
        txtNoOfInstallment.Text = ds.Tables[0].Rows[0]["Installments"].ToString();
        txtInstallmentAmount.Value = ds.Tables[0].Rows[0]["InstAmount"].ToString();
        txtIntRate.Text = ds.Tables[0].Rows[0]["InsRate"].ToString();
        txtLoanDate.Text = ds.Tables[0].Rows[0]["LoanDate1"].ToString();
        txtLoanDeductDate.Text = ds.Tables[0].Rows[0]["LoanDeductDate1"].ToString();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("LoanRep.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _Action = Request.QueryString["Id"] == null ? "SAVE" : "UPDATE";
        string _Id = Request.QueryString["Id"] == null ? "0" : Request.QueryString["Id"];

        DataSet ds = Master.IUD_Loan(_Action, _Id, drpEmployee.SelectedValue, txtLoanAmount.Text, txtNoOfInstallment.Text, txtInstallmentAmount.Value, txtIntRate.Text, txtLoanDate.Text, txtLoanDeductDate.Text);
        if (ds.Tables[0].Rows[0]["Result"].ToString() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "alert('Record " + _Action + " Successfully'); window.location='LoanRep.aspx';", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "alert('No More Leaves Remain'); ", true);
        }
    }

    protected void drpEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpEmployee.SelectedIndex > 0)
        {
            string query = "SELECT ISNULL((SELECT ISNULL(ABS(B.ACCURBAL),0)ACCURBAL FROM TBL_EMPMASTER EMP LEFT JOIN ACCOUNT A ON A.ID=EMP.ACCOUNTLOAN LEFT JOIN ACBAL B ON B.ACCODE=A.ACCODE AND B.ACPSCISNO=EMP.ACCOUNTGROUPLOAN WHERE EMP.DELID=0 AND EMP.EmpId=" + drpEmployee.SelectedValue + "),0)ACCURBAL";
            DataSet ds = data.getDataSet(query);
            txtLoanAmount.Text = ds.Tables[0].Rows[0]["ACCURBAL"].ToString();
        }
        else
        {
            txtLoanAmount.Text = "0";
        }
    }
}