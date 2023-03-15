using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_MonthlySallaryRep : System.Web.UI.Page
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
            FillMonth();
        }
    }

    private void FillMonth()
    {
        DataSet dsMOnth = data.getDataSet("SELECT RIGHT('0'+(cast(number as nvarchar(30))),2)NUmber, DATENAME(MONTH, '1900-' + CAST(number as varchar(2)) + '-1') monthname FROM master..spt_values WHERE Type = 'P' and number between 1 and 12 ORDER BY Number");
        drpMonth.DataSource = dsMOnth;
        drpMonth.DataTextField = "monthname";
        drpMonth.DataValueField = "number";
        drpMonth.DataBind();
        drpMonth.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue);
        Gd.FillUser(drpProjectManager, drpDepartment.SelectedValue);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string _FromDate = DateTime.Now.ToString("yyyy") + "-" + drpMonth.SelectedValue + "-01";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GETSALLARYDATA_PROC";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@MONTH", _FromDate);
        cmd.Parameters.AddWithValue("@Dept_Id", drpDepartment.SelectedValue);
        cmd.Parameters.AddWithValue("@Desig_Id", drpDesignation.SelectedValue);
        cmd.Parameters.AddWithValue("@Rep_Manager", drpProjectManager.SelectedValue);
        cmd.Parameters.AddWithValue("@PF", drpPf.SelectedValue);
        DataSet dss = data.getDataSet(cmd);
        rep.DataSource = dss;
        rep.DataBind();
    }
}