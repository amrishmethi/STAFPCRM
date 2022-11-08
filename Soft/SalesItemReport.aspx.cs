﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_SalesItem_Report : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    Data data = new Data();
    string query;
    SqlCommand cmd = new SqlCommand();
    Master getdata = new Master();
    private HttpCookie Soft;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("SecondarySalesReport.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            getdata.FillUser(drpUser);
            getdata.FillPrimaryParty(drpParty);
            getdata.FillPrimaryStation(drpStation);
            Filldata();
        }
    }


    private void Filldata()
    {
        ds = getdata.getSeconarySalesDetails(drpUser.SelectedValue, drpParty.SelectedValue, drpStation.SelectedItem.Text, dpFrom.Text.Trim(), dpTo.Text.Trim());
      
        rep.DataSource = ds.Tables[0];
        rep.DataBind();
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Filldata();
    }

    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hddid = (HiddenField)e.Item.FindControl("hddid");
            Repeater rep1 = (Repeater)e.Item.FindControl("rep1");

            DataSet dsrep1 = data.getDataSet("PROC_SECONDARYITEMSDETAILS '" + hddid.Value + "'");
            rep1.DataSource = dsrep1;
            rep1.DataBind();

        }
    }
}