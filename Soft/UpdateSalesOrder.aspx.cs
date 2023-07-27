using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_UpdateSalesOrder : System.Web.UI.Page
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
            gd.FillPrimaryParty(drpParty);
            gd.FillGroup(drpGroup);
            if (Request.QueryString["id"] != null)
            {
                fillData(Request.QueryString["id"].ToString());
            }
        }
    }


    public void fillData(string id)
    {
        ds = master.getSalesOrder("SELECT", id, "0", "0", "0", "", "", "", "","0");
        txtEmp.Text = ds.Tables[0].Rows[0]["Employee"].ToString();
        txtDate.Text = ds.Tables[0].Rows[0]["ODate"].ToString();
        drpParty.SelectedValue = ds.Tables[0].Rows[0]["PTAcCode"].ToString();
        drpDelvMode.SelectedValue = ds.Tables[0].Rows[0]["DelvMode"].ToString();
        drpPymtMode.SelectedValue = ds.Tables[0].Rows[0]["PymtMode"].ToString();
        ViewState["tbl"] = ds.Tables[1];
        rep.DataSource = ViewState["tbl"];
        rep.DataBind();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        save();
    }

    private void save()
    {
        ds = master.getSalesOrder("UPDATE", Request.QueryString["id"].ToString(), "0", "0", drpParty.SelectedValue, "", "", drpDelvMode.SelectedValue, drpPymtMode.SelectedValue,"");
        DataTable dt = (DataTable)ViewState["tbl"];
        SqlCommand sqlcom = new SqlCommand("IU_SalesOrderDetail");
        sqlcom.Parameters.Clear();
        sqlcom.CommandType = CommandType.StoredProcedure;
        sqlcom.Parameters.AddWithValue("@tabledetail", dt);
        data.executeCommand(sqlcom);
        Page.ClientScript.RegisterStartupScript(typeof(Page), "close", string.Format("<script type='text/javascript'>{0}</script>", "parent.location.href='SalesOrderReport.aspx'; parent.$.fancybox.close() ;"));
    }



    protected void drpGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemDrop();
    }

    protected void btnplus_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["tbl"];

        if (hddid.Value != "")
        {
            DataRow dr = dt.Rows[Convert.ToInt32(hddid.Value)];
            dr["GrpCode"] = drpGroup.SelectedValue;
            dr["Grp"] = drpGroup.SelectedItem.Text;
            dr["ITCode"] = drpItem.SelectedValue;
            dr["ITName"] = drpItem.SelectedItem.Text;
            dr["OrdQty"] = txtQty.Text;
            dr["OrdStpRate"] = txtRate.Text;

        }
        else
        {
            DataRow dr = dt.NewRow();
            dr["OrderID"] = Request.QueryString["id"].ToString();
            dr["GrpCode"] = drpGroup.SelectedValue;
            dr["Grp"] = drpGroup.SelectedItem.Text;
            dr["ITName"] = drpItem.SelectedItem;
            dr["ITCode"] = drpItem.SelectedValue;
            dr["OrdQty"] = txtQty.Text;
            dr["OrdStpRate"] = txtRate.Text;
            dt.Rows.Add(dr);
        }
        ViewState["tbl"] = dt;
        rep.DataSource = ViewState["tbl"];
        rep.DataBind();
        clearFields();
    }

    private void clearFields()
    {
        btnplus.CssClass = "btn btn-primary fa fa-plus";
        hddid.Value = "";
        drpGroup.SelectedValue = "0";
        drpItem.SelectedValue = "0";
        txtQty.Text = "0";
        txtRate.Text = "0";
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            fillFields(Convert.ToInt32(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "Delete")
        {
            DataTable dt = (DataTable)ViewState["tbl"];
            DataRow dr = dt.Rows[Convert.ToInt32(e.CommandArgument.ToString()) - 1];
            dt.Rows.Remove(dr);
            ViewState["tbl"] = dt;
            rep.DataSource = ViewState["tbl"];
            rep.DataBind();
        }
    }

    private void fillFields(int v)
    {
        btnplus.CssClass = "btn btn-primary fa fa-recycle";
        hddid.Value = (v - 1).ToString();
        DataTable dt = (DataTable)ViewState["tbl"];
        DataRow dr = dt.Rows[v - 1];
        drpGroup.SelectedValue = dr["GrpCode"].ToString();
        fillItemDrop();
        drpItem.SelectedValue = dr["ITCode"].ToString();
        txtQty.Text = Convert.ToDecimal(dr["OrdQty"]).ToString("#0");
        txtRate.Text = Convert.ToDecimal(dr["OrdStpRate"]).ToString("#0.00");
    }


    private void fillItemDrop()
    {
        DataSet ds = master.getPriceList(drpGroup.SelectedValue, "0");
        drpItem.DataSource = ds;
        drpItem.DataTextField = "ITName";
        drpItem.DataValueField = "itcode";
        drpItem.DataBind();
        drpItem.Items.Insert(0, new ListItem("Select", "0"));
    }
}