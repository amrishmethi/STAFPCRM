using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_StationBeatV : System.Web.UI.Page
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
            FillData();
        }
    }

    private void BindData()
    {
        ds = (DataSet)ViewState["HeadQtrDistrict"];
        DataView view = new DataView(ds.Tables[0]);
        view.Sort = "HeadQtr";
        DataTable HeadQtr = view.ToTable(true, "HeadQtr", "HeadQtrNo");
        Gd.FillData(drpHeadqtr, HeadQtr, "HeadQtr", "HeadQtrNo");

        view.Sort = "District";
        DataTable District = view.ToTable(true, "District", "DistrictNo");
        Gd.FillData(drpDistrict, District, "District", "DistrictNo");

        Gd.FillPrimaryStation(drpStation);
    }

    public void FillData()
    { 
        DataSet dss = master.StationBeat("SELECTV", drpStation.SelectedValue, drpDistrict.SelectedValue, drpHeadqtr.SelectedValue, "", drpStation.SelectedValue, "0");

        DataView view = new DataView(dss.Tables[0]);
        string _RowFilter = "0=0";
        if (drpHeadqtr.SelectedIndex > 0)
            _RowFilter += " and HeadQtrNo='" + drpHeadqtr.SelectedValue + "'";
        if (drpDistrict.SelectedIndex > 0)
            _RowFilter += " and DistrictNo='" + drpDistrict.SelectedValue + "'";
        if (drpStation.SelectedIndex > 0)
            _RowFilter += " and StationId='" + drpStation.SelectedValue + "'"; 
        view.RowFilter = _RowFilter;
        view.Sort = "station";
        repDepartment.DataSource = view.ToTable();
        repDepartment.DataBind();
    }



    protected void drpHeadqtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        ds = (DataSet)ViewState["HeadQtrDistrict"];
        DataView view = new DataView(ds.Tables[0]);
        view.RowFilter = "HeadQtrNo='" + drpHeadqtr.SelectedValue + "'";
        view.Sort = "District";
        DataTable District = view.ToTable(true, "District", "DistrictNo");
        Gd.FillData(drpDistrict, District, "District", "DistrictNo");

        FillData();
    }

    protected void drpDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ds = (DataSet)ViewState["station"];
        DataView view = new DataView(ds.Tables[0]);
        string _RowFilter = "0=0";
        if (drpHeadqtr.SelectedIndex > 0)
            _RowFilter += " and HeadQtrNo='" + drpHeadqtr.SelectedValue + "'";
        if (drpDistrict.SelectedIndex > 0)
            _RowFilter += " and DistrictNo='" + drpDistrict.SelectedValue + "'";

        view.RowFilter = _RowFilter;
        view.Sort = "Station";
        DataTable District = view.ToTable(true, "Station", "StationNo");
        Gd.FillData(drpStation, District, "Station", "StationNo");


        FillData();
    }

    protected void drpStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = master.StationBeat("select", drpStation.SelectedValue, drpDistrict.SelectedValue, drpHeadqtr.SelectedValue, "", drpStation.SelectedValue, "0");

        DataView view = new DataView(ds.Tables[0]);
        string _RowFilter = "0=0";
        if (drpDistrict.SelectedIndex > 0)
            _RowFilter += " and DistrictNo='" + drpDistrict.SelectedValue + "'";
        if (drpStation.SelectedIndex > 0)
            _RowFilter += " and StationId='" + drpStation.SelectedValue + "'";
        view.RowFilter = _RowFilter;
        view.Sort = "station";
         

        FillData();
    } 
}