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

            gd.FillGroup(drpGroup);
            if (Request.QueryString["id"] != null)
            {
                fillData(Request.QueryString["id"].ToString());
            }
        }
    }


    public void fillData(string id)
    {
        ds = master.get_UpdateSecondaryItems("Select", id, "", "", "", "");
        DataTable dt = ((DataSet)drpGroup.DataSource).Tables[0];
        DataRow[] dr = dt.Select(" CMsName = '"+ ds.Tables[0].Rows[0]["GroupName"].ToString() + "'");
        drpGroup.SelectedValue = dr[0]["CMsCode"].ToString();
        fillItemDrop();
        drpItem.SelectedValue = ds.Tables[0].Rows[0]["ITName"].ToString();
        txtQty.Text = ds.Tables[0].Rows[0]["OrdQty"].ToString();
        txtRate.Text = ds.Tables[0].Rows[0]["OrdStpRate"].ToString();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        save();
    }

    private void save()
    {
        ds = master.get_UpdateSecondaryItems("Update", Request.QueryString["id"].ToString(), drpGroup.SelectedItem.Text, drpItem.SelectedValue, txtQty.Text.Trim(), txtRate.Text.Trim());
        Page.ClientScript.RegisterStartupScript(typeof(Page), "close", string.Format("<script type='text/javascript'>{0}</script>", "parent.location.href='SalesItemReport.aspx?id=" + Session["CheckID"] + "'; parent.$.fancybox.close() ;"));
    }

    protected void drpGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemDrop();
    }

    private void fillItemDrop()
    {
        DataSet ds = master.getPriceList(drpGroup.SelectedValue, "0");
        drpItem.DataSource = ds;
        drpItem.DataTextField = "ITName";
        drpItem.DataValueField = "ITName";
        drpItem.DataBind();
        drpItem.Items.Insert(0, new ListItem("Select", "0"));
    }
}