using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_EMPLOYEESALES : System.Web.UI.Page
{
    DataTable dtGrp = new DataTable();
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;
    DataTable Dt = new DataTable();
    DataTable Dt1 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            gd.fillDepartment(drpDepartment);
            gd.FillGroup1(drpGrp);
            mnth.Text = DateTime.Now.ToString("yyyy");


            DataSet dsusr = getdata.getHqtrUserDpt(drpDepartment.SelectedValue);
            ViewState["tbl1"] = dsusr;
            DataView dv = dsusr.Tables[0].DefaultView;
            if (drpStatus.SelectedIndex > 0)
                dv.RowFilter = " Status='" + drpStatus.SelectedValue + "'";
            dv.Sort = "Name";
            drpUser.DataSource = dv.ToTable(true, "Name", "MId");
            drpUser.DataTextField = "Name";
            drpUser.DataValueField = "MId";
            drpUser.DataBind();
            drpUser.Items.Insert(0, new ListItem("Select", "0"));

            dv.Sort = "HeadQtr";
            drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
            drpheadQtr.DataTextField = "HeadQtr";
            drpheadQtr.DataValueField = "HeadQtrNo";
            drpheadQtr.DataBind();
            drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));

            CreateTable();
        }
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = (DataSet)ViewState["tbl1"];
        DataView dv = dsusr.Tables[0].DefaultView;
        if (drpStatus.SelectedIndex > 0)
            dv.RowFilter = " Status='" + drpStatus.SelectedValue + "'";
        dv.Sort = "Name";
        drpUser.DataSource = dv.ToTable(true, "Name", "MId");
        drpUser.DataTextField = "Name";
        drpUser.DataValueField = "MId";
        drpUser.DataBind();
        drpUser.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = (DataSet)ViewState["tbl1"];
        DataView dv = dsusr.Tables[0].DefaultView;
        dv.RowFilter = "Mid=" + drpUser.SelectedValue;
        dv.Sort = "HeadQtr";
        drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNO");
        drpheadQtr.DataTextField = "HeadQtr";
        drpheadQtr.DataValueField = "HeadQtrNO";
        drpheadQtr.DataBind();
        drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillData();
    }
    public void fillData()
    {
        DataSet ds = getdata.GetSaleTargetReport(drpDepartment.SelectedValue, drpStatus.SelectedValue, drpUser.SelectedValue, drpheadQtr.SelectedValue, mnth.Text, "PROC_EMPSALEVIEW", "0", "0");

        DataView dvv = ds.Tables[0].DefaultView;
        DataTable dtt = dvv.ToTable(true, "EMP_NAME", "HQNAME", "DOJ", "DOL");


        DataTable dtt2 = (DataTable)ViewState["EMPSale"];
        dtt2.Rows.Clear();
        string _TT = "0";
        foreach (DataRow drr in dtt.Rows)
        {
            DataRow dr = dtt2.NewRow();
            dr["EMP_NAME"] = drr["EMP_NAME"];
            dr["HeadQtr"] = drr["HQNAME"];
            decimal _Target = 0;
            for (int _Col = 2; _Col < 14; _Col++)
            {
                int _Sale = 0;
                foreach (ListItem item in drpGrp.Items)
                {
                    if (item.Selected)
                    {
                        
                        string _FiedName = (drpReportType.SelectedIndex == 0) ? "ORDBAG":(drpReportType.SelectedIndex == 1) ? "STCWEIGHT" : "AMOUNT"; 
                        if (drr["DOL"].ToString() == "")
                        {
                            if(drpReportType.SelectedIndex == 0)
                            _Sale += Convert.ToInt32(ds.Tables[1].AsEnumerable()
                                .Where(x => x["HEADQTR"].ToString() == drr["HQNAME"].ToString())
                                .Where(x => x["CMSNAME"].ToString() == item.ToString())
                                .Where(x => x["STDATE1"].ToString() == dtt2.Columns[_Col].ColumnName)
                                .Where(x => Convert.ToDateTime(x["STDATE"]) >= Convert.ToDateTime(drr["DOJ"]))
                                .Sum(x => x.Field<int>(_FiedName)));
                            else 
                                _Sale += Convert.ToInt32(ds.Tables[1].AsEnumerable()
                                .Where(x => x["HEADQTR"].ToString() == drr["HQNAME"].ToString())
                                .Where(x => x["CMSNAME"].ToString() == item.ToString())
                                .Where(x => x["STDATE1"].ToString() == dtt2.Columns[_Col].ColumnName)
                                .Where(x => Convert.ToDateTime(x["STDATE"]) >= Convert.ToDateTime(drr["DOJ"]))
                                .Sum(x => x.Field<decimal>(_FiedName)));
                        }
                        else
                        {
                            if (drpReportType.SelectedIndex == 0)
                                _Sale += Convert.ToInt32(ds.Tables[1].AsEnumerable()
                                .Where(x => x["HEADQTR"].ToString() == drr["HQNAME"].ToString())
                                .Where(x => x["CMSNAME"].ToString() == item.ToString())
                                .Where(x => x["STDATE1"].ToString() == dtt2.Columns[_Col].ColumnName)
                                .Where(x => Convert.ToDateTime(x["STDATE"]) >= Convert.ToDateTime(drr["DOJ"]))
                                .Where(x => Convert.ToDateTime(x["STDATE"]) <= Convert.ToDateTime(drr["DOL"]))
                                .Sum(x => x.Field<int>(_FiedName)));
                            else
                                _Sale += Convert.ToInt32(ds.Tables[1].AsEnumerable()
                                .Where(x => x["HEADQTR"].ToString() == drr["HQNAME"].ToString())
                                .Where(x => x["CMSNAME"].ToString() == item.ToString())
                                .Where(x => x["STDATE1"].ToString() == dtt2.Columns[_Col].ColumnName)
                                .Where(x => Convert.ToDateTime(x["STDATE"]) >= Convert.ToDateTime(drr["DOJ"]))
                                .Where(x => Convert.ToDateTime(x["STDATE"]) <= Convert.ToDateTime(drr["DOL"]))
                                .Sum(x => x.Field<decimal>(_FiedName)));
                        }



                        //if (drpReportType.SelectedIndex == 0)
                        //    _Sale += Convert.ToDecimal(ds.Tables[2].AsEnumerable().Where(x => x["WHATSAPPNO"].ToString().Trim() == drr["WPNO"].ToString().Trim()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["PTCMSNO"].ToString().Trim() == drr["CID"].ToString().Trim()).Where(x => x["STDATE"].ToString() == dtt2.Columns[_Col].ColumnName).Where(x => x["CMSNAME"].ToString() == item.ToString()).Sum(x => x.Field<int>("ORDBAG")));
                        //if (drpReportType.SelectedIndex == 1)
                        //    _Sale += Convert.ToDecimal(ds.Tables[2].AsEnumerable().Where(x => x["WHATSAPPNO"].ToString().Trim() == drr["WPNO"].ToString().Trim()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["PTCMSNO"].ToString().Trim() == drr["CID"].ToString().Trim()).Where(x => x["STDATE"].ToString() == dtt2.Columns[_Col].ColumnName).Where(x => x["CMSNAME"].ToString() == item.ToString()).Sum(x => x.Field<decimal>("STCWEIGHT")));
                        //if (drpReportType.SelectedIndex == 2)
                        //    _Sale += Convert.ToDecimal(ds.Tables[2].AsEnumerable().Where(x => x["WHATSAPPNO"].ToString().Trim() == drr["WPNO"].ToString().Trim()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["PTCMSNO"].ToString().Trim() == drr["CID"].ToString().Trim()).Where(x => x["STDATE"].ToString() == dtt2.Columns[_Col].ColumnName).Where(x => x["CMSNAME"].ToString() == item.ToString()).Sum(x => x.Field<decimal>("Amount")));

                    }
                }
                dr[dtt2.Columns[_Col].ColumnName] = _Sale;
                _Target += _Sale;
            }
            dr["Total"] = _Target;
            if (_Target > 0)
                dtt2.Rows.Add(dr);
        }

        if (_TT != "0")
        { }
        if (dtt2.Rows.Count > 0)
        {
            DataRow dr = dtt2.NewRow();
            dr["EMP_NAME"] = "Total";
            dr["HeadQtr"] = "";
            dr["Jan"] = dtt2.Compute("sum(Jan)", "");
            dr["Feb"] = dtt2.Compute("sum(Feb)", "");
            dr["Mar"] = dtt2.Compute("sum(Mar)", "");
            dr["Apr"] = dtt2.Compute("sum(Apr)", "");
            dr["May"] = dtt2.Compute("sum(May)", "");
            dr["Jun"] = dtt2.Compute("sum(Jun)", "");
            dr["Jul"] = dtt2.Compute("sum(Jul)", "");
            dr["Aug"] = dtt2.Compute("sum(Aug)", "");
            dr["Sep"] = dtt2.Compute("sum(Sep)", "");
            dr["Oct"] = dtt2.Compute("sum(Oct)", "");
            dr["Nov"] = dtt2.Compute("sum(Nov)", "");
            dr["Dec"] = dtt2.Compute("sum(Dec)", "");
            dr["Total"] = dtt2.Compute("sum(Total)", "");
            dtt2.Rows.Add(dr);
        }
        dtt2.AcceptChanges();
        repDetail.DataSource = dtt2;
        repDetail.DataBind();
    }

    public void CreateTable()
    {
        if (ViewState["EMPSale"] == null)
        {
            Dt.Columns.Add("EMP_NAME");
            Dt.Columns.Add("HeadQtr");
            Dt.Columns.Add("Jan", typeof(int));
            Dt.Columns.Add("Feb", typeof(int));
            Dt.Columns.Add("Mar", typeof(int));
            Dt.Columns.Add("Apr", typeof(int));
            Dt.Columns.Add("May", typeof(int));
            Dt.Columns.Add("Jun", typeof(int));
            Dt.Columns.Add("Jul", typeof(int));
            Dt.Columns.Add("Aug", typeof(int));
            Dt.Columns.Add("Sep", typeof(int));
            Dt.Columns.Add("Oct", typeof(int));
            Dt.Columns.Add("Nov", typeof(int));
            Dt.Columns.Add("Dec", typeof(int));
            Dt.Columns.Add("Total", typeof(int));
            ViewState["EMPSale"] = Dt;
        }
    }
}