using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Dashboard : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Data data = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }
            //    if (HttpContext.Current.Session["latt"] != null && HttpContext.Current.Session["lngg"] != null)
            //    {

            //        lits.Text = "<script type='text/javascript' language='javascript'>$(document).ready(function() {getcityname('" + HttpContext.Current.Session["latt"].ToString()
            //            + "', '" + HttpContext.Current.Session["lngg"].ToString() + "');  });</script>";
            //    }
            //    else
            //    {
            //        lits.Text = "<script type='text/javascript' language='javascript'>$(document).ready(function() { getLocation();  });</script>";
            //    }
            //    if (Session["CheckInId"] != null)
            //    {

            //            linkloc.InnerText = "CheckOUT";
            //    }
            //    else linkloc.InnerText = "CheckIN";

            FillData();
        }
    }

    private void FillData()
    {
        todayDate.InnerHtml = DateTime.Now.ToString("MMM dd,yyyy");
        SqlCommand cmd = new SqlCommand("PROC_HomePage");
        cmd.CommandType = CommandType.StoredProcedure;
        ds = data.getDataSet(cmd);
        if (ds.Tables[0].Rows.Count > 0)
        {
            chkInusr.InnerHtml = ds.Tables[0].Rows[0]["CheckInUsers"].ToString();
            chkOutusr.InnerHtml = ds.Tables[0].Rows[0]["CheckOutUsers"].ToString();
            salesItem.InnerHtml = ds.Tables[0].Rows[0]["Items"].ToString();
            salesGroup.InnerHtml = ds.Tables[0].Rows[0]["Groups"].ToString();
            SalesUsr.InnerHtml = ds.Tables[0].Rows[0]["Users"].ToString();
            rep.DataSource = ds.Tables[1];
            rep.DataBind();
        }
    }
}