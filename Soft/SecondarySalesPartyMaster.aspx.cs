﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SecondarySalesParty_Master : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData Gd = new GetData();
    HttpCookie Soft;
    Data data = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Gd.FillStation(drpStation);
            if (Request.QueryString["id"] != null)
            {
                fillData(Request.QueryString["id"].ToString());
                btnSaveExit.Text = "Update";
            }
        }
    }


    public void fillData(string id)
    {
        ds = getdata.getSecondarySalesParty("SELECT", id, "0", "SELECT", "", "", "", "0", "0", "0");
        if (ds.Tables[0].Rows.Count > 0)
        {

            drpStation.SelectedValue = ds.Tables[0].Rows[0]["StationId"].ToString();
            txtParty.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            txtMobile.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
            txtWhatsApp.Text = ds.Tables[0].Rows[0]["WhatsUpMobileNo"].ToString();
            Beat(drpStation.SelectedValue);
            drpBeat.SelectedValue = ds.Tables[0].Rows[0]["BeatId"].ToString();
        }
    }

    public void Save()
    {
        string action = "SAVE", SecondarySalesPartyid = "";
        if (Request.QueryString["id"] != null)
        {
            action = "UPDATE";
            SecondarySalesPartyid = Request.QueryString["id"].ToString();
        }
        getdata.getSecondarySalesParty(action, SecondarySalesPartyid, drpStation.SelectedValue, drpStation.SelectedItem.Text, txtParty.Text.Trim(), txtMobile.Text.Trim(), txtWhatsApp.Text.Trim(), "", drpBeat.SelectedValue, "0");
        Response.Redirect("SecondarySalesParty.aspx");
    }


    protected void btnSaveExit_Click(object sender, EventArgs e)
    {
        Save();
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("SecondarySalesParty.aspx");
    }



    protected void drpStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Beat(drpStation.SelectedValue);
    }

    private void Beat(string stationId)
    {

        DataSet ds1 = Gd.FillStationBeat1();
        DataView view = new DataView(ds1.Tables[0]);
        string _RowFilter = "0=0";
        if (drpStation.SelectedIndex > 0)
            _RowFilter += " and StationId='" + stationId + "'";
        view.RowFilter = _RowFilter;
        view.Sort = "station";

        DataTable District = view.ToTable(true, "Beat", "BeatId");
        Gd.FillData(drpBeat, District, "Beat", "BeatId");
    }
}