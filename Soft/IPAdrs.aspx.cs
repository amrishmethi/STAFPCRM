using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_IPAdrs : System.Web.UI.Page
{
    private HttpCookie Soft;
    Data data = new Data();
    DataSet dsResult = new DataSet();
    GetData Gd = new GetData();
    Master getdata = new Master();
    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        { 
            if (Request.QueryString["Id"] != null)
            { 
                BinData(Request.QueryString["Id"].ToString());
            }
            GetReport();
        }
    }

    private void GetReport()
    {
        DataSet dss = getdata.IpAddress("SELECT", "","", "0");
        rep.DataSource = dss;
        rep.DataBind();
    }

    private void BinData(string Id)
    {
        DataSet dss = getdata.IpAddress("SELECT", "","", Id);
        txtIpAdrs.Text = dss.Tables[0].Rows[0]["IPAdrs"].ToString();
        txtRemark.Text = dss.Tables[0].Rows[0]["Remark"].ToString();
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            DataSet dss = getdata.IpAddress("DELETE", "","", e.CommandArgument.ToString());
            if (dss.Tables[0].Rows[0]["Result"].ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record DELETE Successfully');window.location ='IPAdrs.aspx'", true);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _Action = Request.QueryString["Id"] == null ? "SAVE" : "UPDATE";
        string _Id = Request.QueryString["Id"] == null ? "0" : Request.QueryString["Id"];

        DataSet dss = getdata.IpAddress(_Action, txtIpAdrs.Text,txtRemark.Text.Trim(), _Id);

        if (dss.Tables[0].Rows[0]["Result"].ToString() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record " + _Action + " Successfully');window.location ='IPAdrs.aspx'", true);
        }
    }
}