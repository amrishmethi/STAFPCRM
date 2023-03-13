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
        if (drpReport.SelectedIndex == 0)
        {
            summary.Visible = true;
            detail.Visible = false;
            DataSet dss = data.getDataSet("usp_DistanceTravelReort_Summary " + drpEmp.SelectedValue + ",'" + data.ConvertToDateTime(txtDateFrom.Text) + "','" + data.ConvertToDateTime(txtDateTo.Text) + "'");
            Repeater1.DataSource = dss;
            Repeater1.DataBind();

            lblTotalKM.Text = dss.Tables[0].Compute("sum(Distance)", "").ToString() + "";
            if (dss.Tables[0].Rows.Count > 0)
            {
                lblRate.Text = dss.Tables[0].Rows[0]["Rate"].ToString();
                lblAMount.Text = dss.Tables[0].Rows[0]["TotalAmt"].ToString();
            }
        }
        else
        {
            summary.Visible = false;
            detail.Visible = true;
            DataSet dss = data.getDataSet("usp_DistanceTravelReort " + drpEmp.SelectedValue + ",'" + data.ConvertToDateTime(txtDateFrom.Text) + "','" + data.ConvertToDateTime(txtDateTo.Text) + "'");
            rep.DataSource = dss;
            rep.DataBind();

            txtTotal.Text = dss.Tables[0].Compute("sum(Distance)", "").ToString() + "";
            if (dss.Tables[0].Rows.Count > 0)
            {
                lblRatee.Text = dss.Tables[0].Rows[0]["Rate"].ToString();
                lblAmountt.Text = dss.Tables[0].Rows[0]["TotalAmt"].ToString();
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetReport();
    }
}