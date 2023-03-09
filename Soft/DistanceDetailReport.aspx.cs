using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_DistanceDetailReport : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    Data data = new Data();
    GetData Gd = new GetData();
    private HttpCookie Soft;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];
            Gd.FillUser(drpEmp);
        }
    }

    private void GetReport()
    {
        DataSet dss = data.getDataSet("usp_DistanceTravelReort " + drpEmp.SelectedValue + ",'" + data.ConvertToDateTime(txtDateFrom.Text) + "','" + data.ConvertToDateTime(txtDateTo.Text) + "'");
        rep.DataSource = dss;
        rep.DataBind();

        txtTotal.Text = dss.Tables[0].Compute("sum(Distance)", "").ToString()+"";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetReport();
    }
}