using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_UpdateCreateDealer : System.Web.UI.Page
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
            gd.FillStation(drpStation);
            gd.FillPartyCategory(drpPartyCatg);
            ViewState["Station"] = drpStation.DataSource;
            if (Request.QueryString["id"] != null)
            {
                fillData(Request.QueryString["id"].ToString());
            }
        }
    }


    public void fillData(string id)
    {
        ds = master.getCreateDealer("SELECT", id, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "","0","0");
        txtname.Text = ds.Tables[0].Rows[0]["Name"].ToString();
        drpStation.SelectedValue = ds.Tables[0].Rows[0]["Station"].ToString();
        BindDistrict();
        txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
        drpDistrict.SelectedValue = ds.Tables[0].Rows[0]["District"].ToString();
        txtZip.Text = ds.Tables[0].Rows[0]["PinCode"].ToString();
        txtState.Text = ds.Tables[0].Rows[0]["State"].ToString();
        txtContPer.Text = ds.Tables[0].Rows[0]["ContPer"].ToString();
        txtGst.Text = ds.Tables[0].Rows[0]["GSTNo"].ToString();
        drpGstRegType.SelectedValue = ds.Tables[0].Rows[0]["GSTRegType"].ToString();
        txtMobile.Text = ds.Tables[0].Rows[0]["SMSMobile"].ToString();
        txtWhtsApp.Text = ds.Tables[0].Rows[0]["WhatsAppNo"].ToString();
        txtTransport.Text = ds.Tables[0].Rows[0]["Transport"].ToString();
        drpType.SelectedValue = ds.Tables[0].Rows[0]["PartyType"].ToString();
        drpPartyCatg.SelectedValue = ds.Tables[0].Rows[0]["PartyCategory"].ToString();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        save();
    }

    private void save()
    {
        ds = master.getCreateDealer("UPDATE", Request.QueryString["id"].ToString(),txtname.Text.Trim(), txtContPer.Text.Trim(), txtAddress.Text.Trim(), txtZip.Text.Trim(), drpStation.SelectedValue, txtState.Text.Trim(), txtGst.Text.Trim(), drpGstRegType.SelectedValue, txtMobile.Text.Trim(), txtWhtsApp.Text.Trim(), drpDistrict.SelectedValue, txtTransport.Text.Trim(), drpType.SelectedValue, drpPartyCatg.SelectedValue, "", "", "", ""); Page.ClientScript.RegisterStartupScript(typeof(Page), "close", string.Format("<script type='text/javascript'>{0}</script>", "parent.location.href='CreateDealer.aspx'; parent.$.fancybox.close() ;"));
    }


    protected void drpStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDistrict();
    }

    private void BindDistrict()
    {
        DataSet dss = (DataSet)ViewState["Station"];
        DataView dv = dss.Tables[0].DefaultView;
        dv.RowFilter = "Station = '"+ drpStation.SelectedValue +"'";
        drpDistrict.DataSource = dv;
        drpDistrict.DataTextField = "District";
        drpDistrict.DataValueField = "District";
        drpDistrict.DataBind();
        drpDistrict.SelectedIndex = 0;
    }
}