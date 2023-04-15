using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_NIghtStay : System.Web.UI.Page
{
    Master master = new Master();
    private HttpCookie Soft;
    Data data = new Data();
    DataSet dsResult = new DataSet();
    GetData Gd = new GetData();
    DataTable dtEmp = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            Session["AccessRigthsSet"] = master.AccessRights("NightStay.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            Gd.fillDepartment(drpDepartment);
            Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue);
            Gd.FillUser(drpProjectManager);
            ViewState["_Date"] = DateTime.Now.ToString("dd/MM/yyyy");
            txtDate.Text = ViewState["_Date"].ToString();
            txtDateTo.Text = ViewState["_Date"].ToString();
            FillRecords();

        }
    }

    private void FillRecords()
    {
        string query = "Select * from NIGHTSTAY_VIEW WHERE ATTENDANCEDATE2 between '" + data.ConvertToDateTime(txtDate.Text) + "' and '" + data.ConvertToDateTime(txtDateTo.Text) + "'";
        if (drpDepartment.SelectedIndex > 0)
            query += " and DEPT_ID=" + drpDepartment.SelectedValue;
        if (drpProjectManager.SelectedIndex > 0)
            query += " and EMPID=" + drpProjectManager.SelectedValue;
        if (drpStatus.SelectedIndex > 0)
            query += " and Status='" + drpStatus.SelectedValue + "'";
        //lblDate.Text = txtDate.Text;
        query += " order by ATTENDANCEDATE2";
        DataSet ds = data.getDataSet(query);
        rep.DataSource = ds;
        rep.DataBind();
    }
    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.FillUser(drpProjectManager, drpDepartment.SelectedValue);
        FillRecords();
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        FillRecords();
    }


    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string _ID = e.CommandArgument.ToString();
        string _CHARGESTYPE1 = e.CommandName.ToString();
        if (e.CommandName == "Delete")
        {
            string _query = "ALLOWANCE_UPDATE '" + _ID + "','" + _CHARGESTYPE1.ToUpper() + "'";

            if (data.executeCommand(_query) == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record DELETE Successfully');window.location ='NightStay.aspx'", true);
            }
        }
        else
        {
            dsResult = data.getDataSet("ALLOWANCE_UPDATE '" + _ID + "','" + _CHARGESTYPE1 + "'");
        }
        FillRecords();
    }

    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString() + "";
    }

    protected void drpProjectManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRecords();
    }
}