using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_createDealer_Document : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    GetData gd = new GetData();
    Master master = new Master();
    HttpCookie Admin;
    string uploadthumburl;
    Data data = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Admin = Request.Cookies["STFP"];

            if (Request.QueryString["id"] != null)
            {
                fillData(Request.QueryString["id"].ToString());
            }
        }
    }


    public void fillData(string id)
    {
        ds = master.getCreateDealer("SELECT", id, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "","");
        rep.DataSource = ds.Tables[1];
        rep.DataBind();
    }

 }