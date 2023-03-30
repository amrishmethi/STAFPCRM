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
        if (drpEmp.SelectedIndex == 0 && drpReport.SelectedIndex != 2)
        {
            lblerror.Text = "Please Select";
            return;
        }
        else { lblerror.Text = ""; }
        if (drpReport.SelectedIndex == 0)
        {
            summary.Visible = true;
            detail.Visible = false;
            all.Visible = false;
            DataSet dss = data.getDataSet("usp_DistanceTravelReort_Summary " + drpEmp.SelectedValue + ",'" + data.YYYYMMDD(txtDateFrom.Text) + "','" + data.YYYYMMDD(txtDateTo.Text) + "'");
            Repeater1.DataSource = dss;
            Repeater1.DataBind();
            if (dss.Tables[0].Rows.Count > 0)
            {
                lblTotalKM.Text = dss.Tables[0].Compute("sum(Distance)", "").ToString() + "";
                lblAmount.Text = dss.Tables[0].Rows[0]["TotalAmt"].ToString();
                lblTotNS.Text = dss.Tables[0].Compute("sum(NightStay)", "").ToString();
                lblTotDA.Text = dss.Tables[0].Compute("sum(DAL1)", "").ToString();
                lblTotal.Text = dss.Tables[0].Compute("sum(Total)", "").ToString();
            }
        }
        else if(drpReport.SelectedIndex == 1)
        {
            summary.Visible = false;
            detail.Visible = true;
            all.Visible = false;
            DataSet dss = data.getDataSet("usp_DistanceTravelReort " + drpEmp.SelectedValue + ",'" + data.YYYYMMDD(txtDateFrom.Text) + "','" + data.YYYYMMDD(txtDateTo.Text) + "'");
            rep.DataSource = dss;
            rep.DataBind();

            txtTotal.Text = dss.Tables[0].Compute("sum(Distance)", "").ToString() + "";
            if (dss.Tables[0].Rows.Count > 0)
            {
                lblRatee.Text = dss.Tables[0].Rows[0]["Rate"].ToString();
                lblAmountt.Text = dss.Tables[0].Rows[0]["TotalAmt"].ToString();
            }
        }
        else if (drpReport.SelectedIndex == 2)
        {
            summary.Visible = false;
            detail.Visible = false;
            all.Visible = true;
            DataSet dss = data.getDataSet("usp_DistanceTravelReort_Summary " + drpEmp.SelectedValue + ",'" + data.YYYYMMDD(txtDateFrom.Text) + "','" + data.YYYYMMDD(txtDateTo.Text) + "'");
            DataTable recDt = dss.Tables[0];
            DataTable boundTable =

           recDt.AsEnumerable()
             .GroupBy(r => r.Field<string>("Emp_Name"))
             .Select(g =>
             {
             var row = recDt.NewRow();
             row["Emp_Name"] = g.Key;
             row["Distance"] = g.Sum(r => r.Field<decimal>("Distance"));
             row["TotalAmt"] = g.Sum(r => r.Field<decimal>("Amount"));
             row["NightStay"] = g.Sum(r => r.Field<decimal>("NightStay"));
             row["DAL1"] = g.Sum(r => r.Field<decimal>("DAL1"));
             row["Total"] = g.Sum(r => r.Field<decimal>("Total"));
              return row;
             }).OrderBy(r => r.Field<string>("Emp_Name")).CopyToDataTable<DataRow>();
            foreach(DataRow dr in boundTable.Rows)
            {
                decimal rate = Convert.ToDecimal(recDt.Select("Emp_Name = '"+dr["Emp_Name"]+"'").FirstOrDefault()["Rate"]);
                dr["Rate"] = rate;
                dr["Amount"] = rate * Convert.ToDecimal(dr["Distance"]);
            }
                        // Create a table from the query.
            //DataTable boundTable = query.CopyToDataTable<DataRow>();


            Repeater2.DataSource = boundTable;
            Repeater2.DataBind();
            if (dss.Tables[0].Rows.Count > 0)
            {
                lblTotalKM.Text = dss.Tables[0].Compute("sum(Distance)", "").ToString() + "";
                lblAmount.Text = dss.Tables[0].Rows[0]["TotalAmt"].ToString();
                lblTotNS.Text = dss.Tables[0].Compute("sum(NightStay)", "").ToString();
                lblTotDA.Text = dss.Tables[0].Compute("sum(DAL1)", "").ToString();
                lblTotal.Text = dss.Tables[0].Compute("sum(Total)", "").ToString();
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetReport();
    }

   
}