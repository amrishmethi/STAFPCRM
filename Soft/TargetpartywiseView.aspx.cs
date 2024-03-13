using Org.BouncyCastle.Ocsp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Admin_TargetpartywiseView : System.Web.UI.Page
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
            gd.FillPartyCategory(drpCatg);
            gd.FillGroup1(lstGroup);
            //gd.FillAccount(drpParty, drpCatg.SelectedValue);
            mnth.Text = DateTime.Now.ToString("MM-yyyy");


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


    public void CreateTable()
    {
        if (ViewState["Target"] == null)
        {
            Dt.Columns.Add("Srno");
            Dt.Columns.Add("Party");
            Dt.Columns.Add("Chk");
            Dt.Columns.Add("Mobile");
            Dt.Columns.Add("STATION");
            Dt.Columns.Add("CATEGORY");
            Dt.Columns.Add("Target", typeof(int));
            Dt.Columns.Add("Sale", typeof(int));
            Dt.Columns.Add("Balance", typeof(int));

            ViewState["Target"] = Dt;
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

    public void fillData()
    {
        chkAll.Checked = false;
        string _PartyId = "0";
        if (drpParty.SelectedIndex > 0)
            _PartyId = drpParty.SelectedValue;

        DataSet ds = getdata.GetSaleTargetReport(drpDepartment.SelectedValue, drpStatus.SelectedValue, drpUser.SelectedValue, drpheadQtr.SelectedValue, mnth.Text, "PROC_SALETARGETVIEW", drpCatg.SelectedValue, _PartyId);

        DataView dvv = ds.Tables[0].DefaultView;
        DataTable dtt = dvv.ToTable(true, "WPNO", "CID", "HQNAME", "STATION");


        DataTable dtt2 = (DataTable)ViewState["Target"];
        dtt2.Rows.Clear();
        int i = 1;
        foreach (DataRow drr in dtt.Rows)
        {
            DataRow dr = dtt2.NewRow();
            dr["Srno"] = i++;
            dr["Party"] = ds.Tables[0].AsEnumerable().Where(x => x["WPNO"].ToString() == drr["WPNO"].ToString()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["CID"].ToString().Trim() == drr["CID"].ToString().Trim()).Select(r => r["PARTY"]).ToArray()[0];
            dr["Mobile"] = drr["WPNO"];
            dr["STATION"] = ds.Tables[0].AsEnumerable().Where(x => x["WPNO"].ToString() == drr["WPNO"].ToString()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["CID"].ToString().Trim() == drr["CID"].ToString().Trim()).Select(r => r["STATION"]).ToArray()[0];
            dr["CATEGORY"] = ds.Tables[0].AsEnumerable().Where(x => x["WPNO"].ToString() == drr["WPNO"].ToString()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["CID"].ToString().Trim() == drr["CID"].ToString().Trim()).Select(r => r["PARTYCATEGORY"]).ToArray()[0];

            int _Target = 0, _Sale = 0;
            foreach (ListItem item in lstGroup.Items)
            {
                if (item.Selected)
                {
                    _Target += Convert.ToInt32(ds.Tables[0].AsEnumerable().Where(x => x["HQNAME"].ToString() == drr["HQNAME"].ToString()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["WPNO"].ToString() == drr["WPNO"].ToString()).Where(x => x["CID"].ToString().Trim() == drr["CID"].ToString().Trim()).Sum(r => r.Field<Int64?>("" + item.ToString().Replace(" ", "_") + "") ?? 0));

                    _Sale += Convert.ToInt32(ds.Tables[2].AsEnumerable().Where(x => x["WHATSAPPNO"].ToString().Trim() == drr["WPNO"].ToString().Trim()).Where(x => x["STATION"].ToString() == drr["STATION"].ToString()).Where(x => x["PTCMSNO"].ToString().Trim() == drr["CID"].ToString().Trim()).Where(x => x["CMSNAME"].ToString() == item.ToString()).Sum(x => x.Field<int>("ORDBAG")));
                }
            }
            dr["Chk"] = "0";
            dr["Target"] = _Target;
            dr["Sale"] = _Sale;
            dr["Balance"] = _Target - _Sale;
            if (_Target != 0 || _Sale != 0)
                dtt2.Rows.Add(dr);
        }
        dtt2.AcceptChanges();
        DataView dvv1 = dtt2.DefaultView;
        dvv.Sort = "Party";
        DataTable dtN = dvv1.ToTable();
        dtN.AcceptChanges();
        if (dtN.Rows.Count > 0)
        {
            DataRow dr = dtN.NewRow();
            dr["Srno"] = dtN.Rows.Count + 1;
            dr["Party"] = "Total";
            dr["Mobile"] = "";
            dr["STATION"] = "";
            dr["CATEGORY"] = "";
            dr["Target"] = dtt2.Compute("sum(Target)", "");
            dr["Sale"] = dtt2.Compute("sum(Sale)", "");
            dr["Balance"] = dtt2.Compute("sum(Balance)", "");
            dtN.Rows.Add(dr);
        }

        dtN.AcceptChanges();
        Session["TargetpartywiseView"] = dtN;
        repDetail.DataSource = dtN;
        repDetail.DataBind();
    }
    protected void drpDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataView dv = ((DataSet)ViewState["tbl1"]).Tables[0].DefaultView;
        dv.RowFilter = "DistrictNo=" + drpDistrict.SelectedValue;
        dv.Sort = "Station";
        drpStation.DataSource = dv.ToTable(true, "Station", "StationNO");
        drpStation.DataTextField = "Station";
        drpStation.DataValueField = "StationNO";
        drpStation.DataBind();
        drpStation.Items.Insert(0, new ListItem("Select", "0"));
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
    protected void drpCatg_SelectedIndexChanged(object sender, EventArgs e)
    {
        gd.FillAccount(drpParty, drpCatg.SelectedValue);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillData();
    }

    [WebMethod]
    public static string txtRep_TextChanged(string Id)
    {
        DataTable Dt = (DataTable)HttpContext.Current.Session["TargetpartywiseView"];
        if (Id == "Body_chkAll")
        {
            foreach (DataRow drw in Dt.Rows) { drw["Chk"] = drw["Chk"].ToString() == "1" ? "0" : "1"; }
        }
        else
        {
            int _RowID = Convert.ToInt32(Id.Split('_')[3]);
            DataRow drr = Dt.Rows[_RowID];
            drr["Chk"] = drr["Chk"].ToString() == "1" ? "0" : "1";
        }
        Dt.AcceptChanges();
        HttpContext.Current.Session["TargetpartywiseView"] = Dt;
        int j = Dt.Rows.Count - 1;

        return j.ToString();

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        
        Session["DateRange"] = " AS ON " + mnth.Text;
        DataTable dataTable = (DataTable)Session["TargetpartywiseView"];
        DataTable dtPrint = ((DataTable)Session["TargetpartywiseView"]).Select("chk=1").CopyToDataTable();
        int _CID = dtPrint.Rows.Count - 1;
        dtPrint.Rows[_CID]["Balance"] = dtPrint.Compute("sum(Balance)", "Party<>'TOTAL'");
        dtPrint.Rows[_CID]["Target"] = dtPrint.Compute("sum(Target)", "Party<>'TOTAL'");
        dtPrint.Rows[_CID]["Sale"] = dtPrint.Compute("sum(Sale)", "Party<>'TOTAL'");

        dtPrint.Columns.Remove("Chk"); 
        int i = 1;
        foreach(DataRow drr in dtPrint.Rows)
        {
            drr["Srno"] = i++;
        }
        dtPrint.AcceptChanges();
        Session["GridData"] = dtPrint;
        Session["TermsId"] = "";
        string _ITemgroup = "";
        foreach (ListItem item in lstGroup.Items)
        {
            if (item.Selected)
            {
                _ITemgroup += item.Text + ",";
            }
        }
        Session["Title"] = "TARGET PARTY WISE </br>" + drpUser.SelectedItem.Text + " (" + drpheadQtr.SelectedItem.Text + ") Of " + _ITemgroup; 
     
        Response.Write("<script>window.open ('Print.aspx','_blank');</script>");
    }
}