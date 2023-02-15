using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_PayrollRep : System.Web.UI.Page
{
    private HttpCookie Soft;
    Data data = new Data();
    DataSet dsResult = new DataSet();
    GetData Gd = new GetData();

    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            Gd.fillDepartment(drpDepartment);
            Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue);
            Gd.FillUser(drpProjectManager);
            FillRecords();
        }
    }

    private void FillRecords()
    {
        string query = "select * from [EMP_VIEW] Where 0=0";
        if (drpDepartment.SelectedIndex > 0)
            query += " and DEPT_ID=" + drpDepartment.SelectedValue;
        if (drpDesignation.SelectedIndex > 0)
            query += " and DESIG_ID='" + drpDesignation.SelectedValue + "'";
        if (drpProjectManager.SelectedIndex > 0)
            query += " and Rep_Manager='" + drpProjectManager.SelectedValue + "'";
        if (drpStatus.SelectedIndex > 0)
            query += " and Status='" + drpStatus.SelectedValue + "'";
            dsResult = data.getDataSet(query);
        rep.DataSource = dsResult;
        rep.DataBind();
    }


    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            data.executeCommand("EMPDETAIL_DELETE " + e.CommandArgument.ToString() + "," + Soft["UserId"]);
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Delete Successfully');window.location ='PayrollRep.aspx'", true);
        }
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRecords();
    }

    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataSet dsrep = (DataSet)rep.DataSource;
        if (dsrep.Tables[0].Rows.Count > 0)
        {
            lblTotInHand.Text = (Convert.ToDecimal(dsrep.Tables[0].Compute("Sum(In_Hand)", ""))).ToString("#0.00");
            lblTotNetSal.Text = (Convert.ToDecimal(dsrep.Tables[0].Compute("Sum(Net_Salary)", ""))).ToString("#0.00");
        }
    }
}