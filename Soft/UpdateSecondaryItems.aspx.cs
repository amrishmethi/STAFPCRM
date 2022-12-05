using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_UpdateSecondaryItems : System.Web.UI.Page
{
    DataSet ds = new DataSet();

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
        ds = master.get_UpdateSecondaryItems("Select", id, "", "", "", "");
        txtGroup.Text = ds.Tables[0].Rows[0]["GroupName"].ToString();
        txtItem.Text = ds.Tables[0].Rows[0]["ITName"].ToString();
        txtQty.Text = ds.Tables[0].Rows[0]["OrdQty"].ToString();
        txtRate.Text = ds.Tables[0].Rows[0]["OrdStpRate"].ToString();

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        save();
    }

    private void save()
    {
        ds = master.get_UpdateSecondaryItems("Update", Request.QueryString["id"].ToString(), txtGroup.Text.Trim(), txtItem.Text.Trim(), txtQty.Text.Trim(), txtRate.Text.Trim());
        Page.ClientScript.RegisterStartupScript(typeof(Page), "close", string.Format("<script type='text/javascript'>{0}</script>", "parent.location.href='SalesItemReport.aspx?id=" + Session["CheckID"] + "'; parent.$.fancybox.close() ;"));
    }
}