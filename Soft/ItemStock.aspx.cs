using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_ItemStock : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("UserWiseParty.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0]; 
            gd.FillGroup1(drpGrp);

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string grp = "0";
        foreach (ListItem item in drpGrp.Items)
        {
            if (item.Selected)
            {
                grp += "," + item.Value;
            }
        }

        ds = getdata.ItemStock(grp, drpReport.SelectedValue);

        rep.DataSource = ds.Tables[0];
        rep.DataBind();

        lblTotalBag.Text = ds.Tables[0].Compute("sum(ItMinQty)", "").ToString();
        lblTotalQty.Text = ds.Tables[0].Compute("sum(tBalQty)", "").ToString();
        lblTotalAmt.Text = ds.Tables[0].Compute("sum(REORDERQTY)", "").ToString();
        lblBalBag.Text = ds.Tables[0].Compute("sum(TBALBAG)", "").ToString();
    }
}