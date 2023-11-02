using Spire.Pdf.Tables;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_PAYROLLBAL : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;
    DataTable dtCustom = new DataTable();
    Workbook workbook1 = new Workbook();
    bool a = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];
            gd.fillDepartment(drpDepartment);
            gd.FillAccountGroup(DrpParty);
        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string query = "select * from EMPLOYEECURRENTBALANCE_VIEW WHERE 0=0 ";

        if (DrpParty.SelectedIndex > 0)
            query += " and ACCOUNTGROUP=" + DrpParty.SelectedValue;
        if (drpDepartment.SelectedIndex > 0)
            query += " and Dept_Id=" + drpDepartment.SelectedValue;
        if (drpStatus.SelectedIndex > 0)
            query += " and Status='" + drpStatus.Text+"'";

        query += " order by emp_Name";
        DataSet dss = data.getDataSet(query);
        rep.DataSource = dss;
        rep.DataBind();

        lblDrTotal.Value = dss.Tables[0].Compute("sum(DR)", "").ToString();
        lblCrTotal.Value = dss.Tables[0].Compute("sum(CR)", "").ToString();
    }
}