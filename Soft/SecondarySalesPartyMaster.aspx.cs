using System;
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
        ds = getdata.getSecondarySalesParty("SELECT",id,"0","0","","","","0");
        if (ds.Tables[0].Rows.Count > 0) { 
        drpStation.SelectedValue = ds.Tables[0].Rows[0]["StationName"].ToString();
        txtParty.Text = ds.Tables[0].Rows[0]["Name"].ToString();
        txtMobile.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
        txtWhatsApp.Text = ds.Tables[0].Rows[0]["WhatsUpMobileNo"].ToString();
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
        getdata.getSecondarySalesParty(action, SecondarySalesPartyid,"0",drpStation.SelectedItem.Text,txtParty.Text.Trim(),txtMobile.Text.Trim(),txtWhatsApp.Text.Trim(),"");
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
   

}