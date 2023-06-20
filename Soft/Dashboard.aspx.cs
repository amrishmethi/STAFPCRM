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
    Master master = new Master();
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
        string str = "",str1="",str2="";
        cmd.CommandType = CommandType.StoredProcedure;
        ds = data.getDataSet(cmd);
        DataSet ds1 = master.GetDepartment("Select", "0", "", "", "");
        str += "<div class='col-md-2'>";
        str += "Attendance";
        str += "</div>";
        str1 += "<div class='col-md-2'>";
        str1 += "In";
        str1 += "</div>";
        str2 += "<div class='col-md-2'>";
        str2 += "Out";
        str2 += "</div>";
        foreach (DataRow rw in ds.Tables[2].Rows)
        {
            str += "<div class='col-md-2'>";
            str += rw["Dept"]+"("+rw["TotAttendant"] +")";
            str += "</div>";
            str1 += "<div class='col-md-2'>";
            str1 += rw["AttendIn"];
            str1 += "</div>"; 
            str2 += "<div class='col-md-2'>";
            str2 += rw["AttendOut"];
            str2 += "</div>";
        }
       
        if (ds.Tables[0].Rows.Count > 0)
        {
            chkInusr.InnerHtml = ds.Tables[0].Rows[0]["CheckInUsers"].ToString();
            chkOutusr.InnerHtml = ds.Tables[0].Rows[0]["CheckOutUsers"].ToString();
            deptBlock.InnerHtml = str;
            AttnInBlock.InnerHtml = str1;
            AttnOutBlock.InnerHtml = str2;
            TotUsr.InnerHtml = ds.Tables[0].Rows[0]["SalesUsers"].ToString(); 
            TotUsr1.InnerHtml = ds.Tables[0].Rows[0]["Users"].ToString();
        //    AttnIn.InnerHtml = ds.Tables[0].Rows[0]["AttendIn"].ToString();
       //     AttnOut.InnerHtml = ds.Tables[0].Rows[0]["AttendOut"].ToString();
            rep.DataSource = ds.Tables[1];
            rep.DataBind();
        }
    }
}