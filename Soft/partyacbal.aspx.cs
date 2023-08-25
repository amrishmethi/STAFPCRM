using Spire.Pdf.Tables;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_partyacbal : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;
    DataTable dtCustom = new DataTable();
    Workbook workbook1 = new Workbook();
    bool a = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            gd.FillAccount(DrpParty);
            gd.FillPrimaryStation(DrpStation);
        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string query = "Select * from GetPartyAccountBalance_View WHERE 0=0";
        if (DrpStation.SelectedIndex > 0)
            query += " and stationNo=" + DrpStation.SelectedValue;
        if (DrpParty.SelectedIndex > 0)
            query += " and Id=" + DrpParty.SelectedValue;

        query += " order by ACName";
        DataSet dss = data.getDataSet(query);
        rep.DataSource = dss;
        rep.DataBind();

        lblCrTotal.Value = dss.Tables[0].Compute("sum(CR1)","").ToString();
        lblDrTotal.Value = dss.Tables[0].Compute("sum(DR1)","").ToString();
    }
}