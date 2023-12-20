using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_PartySales : System.Web.UI.Page
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
            gd.FillPartyCategory(drpCatg);

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
        string _PartyId = "0";
        if (drpParty.SelectedIndex > 0)
            _PartyId = drpParty.SelectedValue;
        DataSet ds = getdata.GetSaleTargetReport(drpDepartment.SelectedValue, drpStatus.SelectedValue, drpUser.SelectedValue, drpheadQtr.SelectedValue, mnth.Text, "PROC_EMPSALEVIEW", drpCatg.SelectedValue, _PartyId);

        DataView dvv = ds.Tables[0].DefaultView;
        DataTable dtt = dvv.ToTable(true, "WPNO", "CID", "HQNAME", "STATION");


        DataTable dtt2 = (DataTable)ViewState["EMPSale"];
        dtt2.Rows.Clear();
        foreach (DataRow drr in dtt.Rows)
        {
            DataRow dr = dtt2.NewRow();
            dr["Party"] = ds.Tables[0].AsEnumerable().Where(x => x["WPNO"].ToString() == drr["WPNO"].ToString()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["CID"].ToString().Trim() == drr["CID"].ToString().Trim()).Select(r => r["PARTY"]).ToArray()[0];
            dr["Mobile"] = drr["WPNO"];
            dr["STATION"] = ds.Tables[0].AsEnumerable().Where(x => x["WPNO"].ToString() == drr["WPNO"].ToString()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["CID"].ToString().Trim() == drr["CID"].ToString().Trim()).Select(r => r["STATION"]).ToArray()[0];
            dr["CATEGORY"] = ds.Tables[0].AsEnumerable().Where(x => x["WPNO"].ToString() == drr["WPNO"].ToString()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["CID"].ToString().Trim() == drr["CID"].ToString().Trim()).Select(r => r["PARTYCATEGORY"]).ToArray()[0];
            decimal _Target = 0;
            bool _IsSHow = false;
            for (int _Col = 4; _Col < 16; _Col++)
            {
                if (drr["wpno"].ToString()== "9352020139")
                {

                }
                decimal _Sale = 0;
                foreach (ListItem item in drpGrp.Items)
                {
                    if (item.Selected)
                    {
                        if (drpReportType.SelectedIndex == 0)
                            _Sale += Convert.ToDecimal(ds.Tables[2].AsEnumerable().Where(x => x["WHATSAPPNO"].ToString().Trim() == drr["WPNO"].ToString().Trim()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["PTCMSNO"].ToString().Trim() == drr["CID"].ToString().Trim()).Where(x => x["STDATE"].ToString() == dtt2.Columns[_Col].ColumnName).Where(x => x["CMSNAME"].ToString() == item.ToString()).Sum(x => x.Field<int>("ORDBAG")));
                        if (drpReportType.SelectedIndex == 1)
                            _Sale += Convert.ToDecimal(ds.Tables[2].AsEnumerable().Where(x => x["WHATSAPPNO"].ToString().Trim() == drr["WPNO"].ToString().Trim()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["PTCMSNO"].ToString().Trim() == drr["CID"].ToString().Trim()).Where(x => x["STDATE"].ToString() == dtt2.Columns[_Col].ColumnName).Where(x => x["CMSNAME"].ToString() == item.ToString()).Sum(x => x.Field<decimal>("STCWEIGHT")));
                        if (drpReportType.SelectedIndex == 2)
                            _Sale += Convert.ToDecimal(ds.Tables[2].AsEnumerable().Where(x => x["WHATSAPPNO"].ToString().Trim() == drr["WPNO"].ToString().Trim()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["PTCMSNO"].ToString().Trim() == drr["CID"].ToString().Trim()).Where(x => x["STDATE"].ToString() == dtt2.Columns[_Col].ColumnName).Where(x => x["CMSNAME"].ToString() == item.ToString()).Sum(x => x.Field<decimal>("Amount")));
                        if (_Sale != 0)
                            _IsSHow = true;
                    }
                }
                dr[dtt2.Columns[_Col].ColumnName] = _Sale;
                _Target += _Sale;
            }
            dr["Total"] = _Target;
            if (_IsSHow)
                dtt2.Rows.Add(dr);
        }
        if (dtt2.Rows.Count > 0)
        {
            DataRow dr = dtt2.NewRow();
            dr["Party"] = "Total";
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
            Dt.Columns.Add("Party");
            Dt.Columns.Add("Mobile");
            Dt.Columns.Add("STATION");
            Dt.Columns.Add("CATEGORY");
            Dt.Columns.Add("Jan", typeof(decimal));
            Dt.Columns.Add("Feb", typeof(decimal));
            Dt.Columns.Add("Mar", typeof(decimal));
            Dt.Columns.Add("Apr", typeof(decimal));
            Dt.Columns.Add("May", typeof(decimal));
            Dt.Columns.Add("Jun", typeof(decimal));
            Dt.Columns.Add("Jul", typeof(decimal));
            Dt.Columns.Add("Aug", typeof(decimal));
            Dt.Columns.Add("Sep", typeof(decimal));
            Dt.Columns.Add("Oct", typeof(decimal));
            Dt.Columns.Add("Nov", typeof(decimal));
            Dt.Columns.Add("Dec", typeof(decimal));
            Dt.Columns.Add("Total", typeof(decimal));
            ViewState["EMPSale"] = Dt;
        }
    }

    protected void drpCatg_SelectedIndexChanged(object sender, EventArgs e)
    {
        gd.FillAccount(drpParty, drpCatg.SelectedValue);
    }
}