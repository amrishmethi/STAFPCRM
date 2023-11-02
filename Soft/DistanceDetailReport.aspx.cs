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
            Gd.fillDepartment(drpDepartment);

            DataSet dsusr = getdata.getHqtrUserDpt(drpDepartment.SelectedValue);
            ViewState["tbl1"] = dsusr;
            DataView dv = dsusr.Tables[0].DefaultView;
            if (drpStatus.SelectedIndex > 0)
                dv.RowFilter = " Status='" + drpStatus.SelectedValue + "'";
            dv.Sort = "Name";
            drpEmp.DataSource = dv.ToTable(true, "Name", "MId");
            drpEmp.DataTextField = "Name";
            drpEmp.DataValueField = "MId";
            drpEmp.DataBind();
            drpEmp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));

            txtDateFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = (DataSet)ViewState["tbl1"];
        DataView dv = dsusr.Tables[0].DefaultView;
        if (drpStatus.SelectedIndex > 0)
            dv.RowFilter = " Status='" + drpStatus.SelectedValue + "'";

        dv.Sort = "Name";
        drpEmp.DataSource = dv.ToTable(true, "Name", "MId");
        drpEmp.DataTextField = "Name";
        drpEmp.DataValueField = "MId";
        drpEmp.DataBind();
        drpEmp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
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
            DataSet dss = data.getDataSet("usp_DistanceTravelReort_Summary '" + drpEmp.SelectedValue + "','" + data.YYYYMMDD(txtDateFrom.Text) + "','" + data.YYYYMMDD(txtDateTo.Text) + "','" + drpDepartment.SelectedValue + "','" + drpStatus.SelectedValue + "'");
            summary.Visible = true;
            detail.Visible = false;
            all.Visible = false;

            Repeater1.DataSource = dss;
            Repeater1.DataBind();
            if (dss.Tables[0].Rows.Count > 0)
            {
                lblTotalKM.Text = dss.Tables[0].Compute("sum(Distance)", "").ToString() + "";
                lblAmount.Text = dss.Tables[0].Rows[0]["TotalAmt"].ToString();
                lblTotNS.Text = dss.Tables[0].Compute("sum(NightStay)", "").ToString();
                lblTotDA.Text = dss.Tables[0].Compute("sum(DAL1)", "").ToString();
                lblTotal.Text = dss.Tables[0].Compute("sum(Total)", "").ToString();
                lblTotOth.Text = dss.Tables[0].Compute("sum(Other)", "").ToString();
            }
        }
        else if (drpReport.SelectedIndex == 1)
        {
            DataSet dss = data.getDataSet("usp_DistanceTravelReort '" + drpEmp.SelectedValue + "','" + data.YYYYMMDD(txtDateFrom.Text) + "','" + data.YYYYMMDD(txtDateTo.Text) + "','" + drpDepartment.SelectedValue + "','" + drpStatus.SelectedValue + "'");
            summary.Visible = false;
            detail.Visible = true;
            all.Visible = false;
            rep.DataSource = dss;
            rep.DataBind();

            txtTotal.Text = dss.Tables[0].Compute("sum(Distance)", "").ToString() + "";
            if (dss.Tables[0].Rows.Count > 0)
            {
                lblAmountt.Text = dss.Tables[0].Compute("sum(Amount)", "").ToString();
                lblTotalq.Text = dss.Tables[0].Compute("sum(Total)", "").ToString();
                lblNight.Text = dss.Tables[0].Compute("sum(NightStay)", "").ToString();
                lblDal.Text = dss.Tables[0].Compute("sum(DAL)", "").ToString();
                lblOther.Text = dss.Tables[0].Compute("sum(Other)", "").ToString();
            }
        }
        else if (drpReport.SelectedIndex == 2)
        {
            DataSet dss = data.getDataSet("usp_DistanceTravelReort_Summary '" + drpEmp.SelectedValue + "','" + data.YYYYMMDD(txtDateFrom.Text) + "','" + data.YYYYMMDD(txtDateTo.Text) + "','" + drpDepartment.SelectedValue + "','" + drpStatus.SelectedValue + "'");
            summary.Visible = false;
            detail.Visible = false;
            all.Visible = true;
            if (dss.Tables[0].Rows.Count > 0)
            {
                DataTable recDt = dss.Tables[0];


                var result = from rows in recDt.AsEnumerable()
                             group rows by new { Emp_Name = rows["Emp_Name"], ADVANCE = rows["ADVANCE"] } into grp
                             select new
                             {
                                 Emp_Name = grp.Key.Emp_Name,
                                 ADVANCE = grp.Key.ADVANCE,
                                 Distance = grp.Sum(r => r.Field<decimal>("Distance")),
                                 TotalAmt = grp.Sum(r => r.Field<decimal>("Amount")),
                                 NightStay = grp.Sum(r => r.Field<decimal>("NightStay")),
                                 DAL1 = grp.Sum(r => r.Field<decimal>("DAL1")),
                                 Other = grp.Sum(r => r.Field<decimal>("Other")),
                                 Total = grp.Sum(r => r.Field<decimal>("Total")),
                             };

                result.ToList();


                DataTable boundTable = new DataTable();
                boundTable.Columns.Add("Emp_Name", typeof(string));
                boundTable.Columns.Add("Distance", typeof(decimal));
                boundTable.Columns.Add("TotalAmt", typeof(decimal));
                boundTable.Columns.Add("NightStay", typeof(decimal));
                boundTable.Columns.Add("DAL1", typeof(decimal));
                boundTable.Columns.Add("Other", typeof(decimal));
                boundTable.Columns.Add("ADVANCE", typeof(decimal));
                boundTable.Columns.Add("Total", typeof(decimal));
                boundTable.Columns.Add("Rate", typeof(decimal));
                boundTable.Columns.Add("Amount", typeof(decimal));
                boundTable.Columns.Add("NetTotal", typeof(decimal));

                foreach (var g in result)
                {
                    var row = boundTable.NewRow();
                    row["Emp_Name"] = g.Emp_Name;
                    row["Distance"] = g.Distance;
                    row["TotalAmt"] = g.TotalAmt;
                    row["NightStay"] = g.NightStay;
                    row["DAL1"] = g.DAL1;
                    row["Other"] = g.Other;
                    row["ADVANCE"] = g.ADVANCE;
                    row["NetTotal"] = Convert.ToDecimal(g.Total) - Convert.ToDecimal(g.ADVANCE);
                    row["Total"] = Convert.ToDecimal(g.Total);
                    boundTable.Rows.Add(row);
                }

                 
                foreach (DataRow dr in boundTable.Rows)
                {
                    decimal rate = Convert.ToDecimal(recDt.Select("Emp_Name = '" + dr["Emp_Name"] + "'").FirstOrDefault()["Rate"]);
                    dr["Rate"] = rate;
                    dr["Amount"] = (rate * Convert.ToDecimal(dr["Distance"])).ToString("0.00");
                }
                Repeater2.DataSource = boundTable;
                Repeater2.DataBind();
                if (boundTable.Rows.Count > 0)
                {
                    Label1.Text = boundTable.Compute("sum(Distance)", "").ToString() + "";
                    Label2.Text = boundTable.Compute("sum(Amount)", "").ToString();
                    Label3.Text = boundTable.Compute("sum(NightStay)", "").ToString();
                    Label4.Text = boundTable.Compute("sum(DAL1)", "").ToString();
                    Label6.Text = boundTable.Compute("sum(Other)", "").ToString();
                    Label5.Text = boundTable.Compute("sum(Total)", "").ToString();
                    LabelADVANCE.Text = boundTable.Compute("sum(ADVANCE)", "").ToString();
                    Label7.Text = boundTable.Compute("sum(NetTotal)", "").ToString();
                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetReport();
    }
}