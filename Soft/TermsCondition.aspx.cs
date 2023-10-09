using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_TermsCondition : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    Data data = new Data();
    GetData Gd = new GetData();
    private HttpCookie Soft;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];
            fillData();
            if (Request.QueryString["Id"] != null)
                BindData(Request.QueryString["Id"]);
        }
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("SecondarySalesPartyMaster.aspx?id=" + e.CommandArgument + "");
        }
        if (e.CommandName == "Delete")
        {
            ds = getdata.TermsCondition("DELETE", e.CommandArgument.ToString(), "", "");
            if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Deleted Successfully......')", true);
                fillData();
            }
        }
    }
    public void BindData(string ID)
    {
        ds = getdata.TermsCondition("SELECT", ID, txtHeading.Text, txtDiscription.Text);
        txtDiscription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
        txtHeading.Text = ds.Tables[0].Rows[0]["HEADING"].ToString();
    }

    public void fillData()
    {
        ds = getdata.TermsCondition("SELECT", "0", txtHeading.Text, txtDiscription.Text);
        rep.DataSource = ds.Tables[0];
        rep.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        string _Action = Request.QueryString["Id"] == null ? "SAVE" : "UPDATE";
        string _Id = Request.QueryString["Id"] == null ? "0" : Request.QueryString["Id"];

        DataSet dss = getdata.TermsCondition(_Action, _Id, txtHeading.Text, txtDiscription.Text);

        if (dss.Tables[0].Rows[0]["Result"].ToString() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "alert", "alert('Record " + _Action + " Successfully');window.location ='TermsCondition.aspx'", true);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TermsCondition.aspx");
    }
}