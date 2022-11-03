using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SecondarySalesStation_Master : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    HttpCookie Soft;
    Data data = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                fillData(Request.QueryString["id"].ToString());
                btnSaveExit.Text = "Update";
            }
        }
    }


    public void fillData(string id)
    {
        ds = getdata.getSecondarySalesStation("SELECT", id, "");
        txtStation.Text = ds.Tables[0].Rows[0]["Name"].ToString();
    }

    public void Save()
    {
        string action = "SAVE", SecondarySalesStationid = "";
        if (Request.QueryString["id"] != null)
        {
            action = "UPDATE";
            SecondarySalesStationid = Request.QueryString["id"].ToString();
        }
        getdata.getSecondarySalesStation(action, SecondarySalesStationid, txtStation.Text.Trim());
        Response.Redirect("SecondarySalesStation.aspx");
    }


    protected void btnSaveExit_Click(object sender, EventArgs e)
    {
        Save();
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("SecondarySalesStation.aspx");
    }


}