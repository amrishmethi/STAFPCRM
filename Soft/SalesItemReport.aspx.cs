using System;
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
    HttpCookie Admin;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Request.Cookies["BuiltIn"] == null) { Response.Redirect("../Login.aspx"); }
            //Admin = Request.Cookies["BuiltIn"];

            getdata.FillStation(drpStation);
            getdata.FillParty(drpParty);
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            Filldata();
        }
    }


    private void Filldata()
    {
        SqlCommand cmd = new SqlCommand("PROC_SECONDARYITEMS");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", Request.QueryString["id"].ToString());
        cmd.Parameters.AddWithValue("@PARTY", drpParty.SelectedValue);
        cmd.Parameters.AddWithValue("@STATION", drpStation.SelectedValue);
        cmd.Parameters.AddWithValue("@DATEFROM", data.YYYYMMDD(dpFrom.Text.Trim()));
        cmd.Parameters.AddWithValue("@DATETO", data.YYYYMMDD(dpTo.Text.Trim()));
        ds = data.getDataSet(cmd);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblParty.Text = ds.Tables[0].Rows[0]["PrimaryParty"].ToString();
            lblStation.Text = ds.Tables[0].Rows[0]["PrimaryStation"].ToString();
        }
        rep.DataSource = ds;
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