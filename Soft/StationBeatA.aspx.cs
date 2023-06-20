using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_StationBeatA : System.Web.UI.Page
{

    GetData Gd = new GetData();
    Master master = new Master();
    Data data = new Data();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["HeadQtrDistrict"] = Gd.FillHeadQtrDistrict();
            ViewState["station"] = Gd.FillStation();
            BindData();

            if (Request.QueryString["Id"] != null)
            {
                FillData(Request.QueryString["Id"].ToString());
            }
        }
    }

    private void FillData(string Id)
    {
        DataSet dss = master.StationBeat("SELECT", "", "", "", "", Id);
        drpHeadqtr.SelectedValue = dss.Tables[0].Rows[0]["HeadQtr"].ToString();
        drpDistrict.SelectedValue = dss.Tables[0].Rows[0]["District"].ToString();
        drpStation.SelectedValue = dss.Tables[0].Rows[0]["Station"].ToString();
        txtBeat.Text = dss.Tables[0].Rows[0]["Beat"].ToString();
    }


    private void BindData()
    {
        ds = (DataSet)ViewState["HeadQtrDistrict"];
        DataView view = new DataView(ds.Tables[0]);
        view.Sort = "HeadQtr";
        DataTable HeadQtr = view.ToTable(true, "HeadQtr");
        Gd.FillData(drpHeadqtr, HeadQtr, "HeadQtr", "HeadQtr");

        view.Sort = "District";
        DataTable District = view.ToTable(true, "District");
        Gd.FillData(drpDistrict, District, "District", "District");

        Gd.FillPrimaryStation(drpStation);
    }
     

    protected void drpHeadqtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        ds = (DataSet)ViewState["HeadQtrDistrict"];
        DataView view = new DataView(ds.Tables[0]);
        view.RowFilter = "HeadQtr='" + drpHeadqtr.SelectedValue + "'";
        view.Sort = "District";
        DataTable District = view.ToTable(true, "District");
        Gd.FillData(drpDistrict, District, "District", "District");

    }

    protected void drpDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ds = (DataSet)ViewState["station"];
        DataView view = new DataView(ds.Tables[0]);
        string _RowFilter = "0=0";
        if (drpHeadqtr.SelectedIndex > 0)
            _RowFilter += " and HeadQtr='" + drpHeadqtr.SelectedValue + "'";
        if (drpDistrict.SelectedIndex > 0)
            _RowFilter += " and District='" + drpDistrict.SelectedValue + "'";

        view.RowFilter = _RowFilter;
        view.Sort = "Station";
        DataTable District = view.ToTable(true, "Station");
        Gd.FillData(drpStation, District, "Station", "Station");

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _Action = Request.QueryString["Id"] == null ? "SAVE" : "UPDATE";
        string _Id = Request.QueryString["Id"] == null ? "0" : Request.QueryString["Id"];

        DataSet dss = master.StationBeat(_Action, drpStation.SelectedValue, drpDistrict.SelectedValue, drpHeadqtr.SelectedValue, txtBeat.Text.Trim(), _Id);

        if (dss.Tables[0].Rows[0]["Result"].ToString() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record " + _Action + " Successfully');window.location ='StationBeatA.aspx'", true);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("StationBeat.aspx");
    }
}