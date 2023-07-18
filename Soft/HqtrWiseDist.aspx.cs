using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Admin_HqtrWiseDist : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    Data data = new Data();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("UserWiseParty.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];

            DataSet dsusr = getdata.getHqtrUserDpt("0");
            ViewState["tbl"] = dsusr.Tables[0];
            if (RadioButton1.Checked)
            {
                DataView dv = ((DataTable)ViewState["tbl"]).DefaultView;
                dv.Sort = "Name";
                drpUser.DataSource = dv.ToTable(true, "Name", "MId");
                drpUser.DataTextField = "Name";
                drpUser.DataValueField = "MId";
                drpUser.DataBind();
                drpUser.Items.Insert(0, new ListItem("Select", "0"));

                dv.Sort = "HeadQtr";
                drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
                drpheadQtr.DataTextField = "HeadQtr";
                drpheadQtr.DataValueField = "HeadQtrNo";
                drpheadQtr.DataBind();
                drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
    }

    public void fillData()
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        ViewState["tbl"] = dsusr.Tables[0];
        string str = "0=0  ";
        DataView dv = ((DataTable)ViewState["tbl"]).DefaultView;

        if (drpUser.SelectedIndex > 0)
        {
            str += "and MId = '" + drpUser.SelectedValue + "'";
        } 
        if (drpheadQtr.SelectedIndex > 0)
        {
            str += " and HeadQtrNo = '" + drpheadQtr.SelectedValue + "'";
        }
        dv.RowFilter = str;
        //if (drpDistrict.SelectedIndex > 0)
        //{
        //    dv.RowFilter = "District = '" + drpDistrict.SelectedValue + "'";
        //}
        rep.DataSource = dv.ToTable();
        rep.DataBind();
    }



    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }


    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButton1.Checked)
        {
            DataView dv = ((DataTable)ViewState["tbl"]).DefaultView;
            dv.RowFilter = "MId = " + drpUser.SelectedValue;
            drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
            drpheadQtr.DataTextField = "HeadQtr";
            drpheadQtr.DataValueField = "HeadQtrNo";
            drpheadQtr.DataBind();
            drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void drpheadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButton2.Checked)
        {
            DataView dv = ((DataTable)ViewState["tbl"]).DefaultView;
            dv.RowFilter = "HeadQtr = '" + drpheadQtr.SelectedValue + "'";
            drpUser.DataSource = dv.ToTable(true, "Name", "MId");
            drpUser.DataTextField = "Name";
            drpUser.DataValueField = "MId";
            drpUser.DataBind();
            drpUser.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        DataView dv = ((DataTable)ViewState["tbl"]).DefaultView;
        if (RadioButton1.Checked)
        {
            dv.Sort = "Name";
            drpUser.DataSource = dv.ToTable(true, "Name", "MId");
            drpUser.DataTextField = "Name";
            drpUser.DataValueField = "MId";
            drpUser.DataBind();
            drpUser.Items.Insert(0, new ListItem("Select", "0"));
            drpheadQtr.Items.Clear();
            drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
        }
        else if (RadioButton2.Checked)
        {
            dv.Sort = "HeadQtr";
            drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
            drpheadQtr.DataTextField = "HeadQtr";
            drpheadQtr.DataValueField = "HeadQtrNo";
            drpheadQtr.DataBind();
            drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
            drpUser.Items.Clear();
            drpUser.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        fillData();
    }
}