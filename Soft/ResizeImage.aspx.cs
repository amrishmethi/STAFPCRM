using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Soft_ResizeImage : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    GetData gd = new GetData();
    Master getdata = new Master();
    HttpCookie Admin;
    string uploadthumburl;
    Data data = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Admin = Request.Cookies["STFP"];
           
        }
    }

}