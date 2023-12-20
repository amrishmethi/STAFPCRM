using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MobileMenu : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    GetData getdata = new GetData();
    Master emp = new Master();
    HttpCookie Admin;
    string uploadthumburl;
    Data data = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //    Admin = Request.Cookies["BuiltIn"];
            BindTag();
            fillData();

            if (Request.QueryString["id"] != null)
            {
                FillForm(Request.QueryString["id"].ToString());
                btnSaveExit.Text = "Update";
                // btnSaveAnother.Visible = false;
            }
        }

    }

    private void FillForm(string id)
    {
        DataView dataView = ds.Tables[0].DefaultView;
        dataView.RowFilter = "MenuId = " + id;
        txtMenuName.Text = dataView[0]["MenuName"].ToString();
        drpLevel.SelectedValue = dataView[0]["ParentId"].ToString();
        txtLink.Text = dataView[0]["MenuLink"].ToString();
        txtPosition.Text = dataView[0]["Position"].ToString();
    }

    public void BindTag()
    {
        ds.Clear();
        String query = "select * from tbl_MenuMobile where IsDelete=0 and ParentId=0";
        ds = data.getDataSet(query);
        drpLevel.DataSource = ds;
        drpLevel.DataValueField = "MenuId";
        drpLevel.DataTextField = "MenuName";
        drpLevel.DataBind();
        drpLevel.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void fillData()
    {
        SqlCommand cmd = new SqlCommand("SP_GETMENULISTMOBILE");
        cmd.CommandType = CommandType.StoredProcedure;
        ds = data.getDataSet(cmd);
        repMenuList.DataSource = ds;
        repMenuList.DataBind();
    }

    public void Save()
    {
        SqlCommand cmd = new SqlCommand("SP_MENUMOBILE");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@MenuName", txtMenuName.Text);
        cmd.Parameters.AddWithValue("@ParentId", drpLevel.Text);
        cmd.Parameters.AddWithValue("@MenuLink", txtLink.Text.ToLower());
        cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
        cmd.Parameters.AddWithValue("@MenuId", Request.QueryString["id"]);
        ds = data.getDataSet(cmd);
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + ds.Tables[0].Rows[0]["Result"].ToString() + "')", true);
        if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
            Response.Redirect("MobileMenu.aspx");
    }


    protected void btnSaveExit_Click(object sender, EventArgs e)
    {
        Save();
    }

    protected void repMenuList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string query = "update tbl_MenuMobile set IsDelete = 1  where MenuId=" + e.CommandArgument + "";
            data.executeCommand(query);
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Deleted Successfully......')", true);
            Response.Redirect("MobileMenu.aspx");
        }
    }


}