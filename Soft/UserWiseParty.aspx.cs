using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Admin_UserWiseParty : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("UserWiseParty.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            gd.fillDepartment(drpDepartment);
            DataSet dsusr = getdata.getHqtrUser();
            gd.FillPartyCategory(drpCatg);
            DataView dv = dsusr.Tables[0].DefaultView;
            dv.Sort = "Name";
            drpUser.DataSource = dv.ToTable(true, "Name", "MId");
            drpUser.DataTextField = "Name";
            drpUser.DataValueField = "MId";
            drpUser.DataBind();
            drpUser.Items.Insert(0, new ListItem("Select", "0"));

            dv.Sort = "HeadQtr";
            drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr");
            drpheadQtr.DataTextField = "HeadQtr";
            drpheadQtr.DataValueField = "HeadQtr";
            drpheadQtr.DataBind();
            drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));

        }
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        gd.FillCRMUser(drpUser, drpDepartment.SelectedValue, drpStatus.SelectedValue);
    }
    public void fillData()
    {
        DataView dv = ((DataTable)ViewState["tbl"]).DefaultView;

        string rowFilter = "0=0";
        if (drpheadQtr.SelectedIndex > 0)
        {
            rowFilter += " and HeadQtr = '" + drpheadQtr.SelectedValue + "'";
        }

        if (drpDistrict.SelectedIndex > 0)
        {
            rowFilter += " and District = '" + drpDistrict.SelectedValue + "'";
        }
        if (drpStation.SelectedIndex > 0)
        {
            rowFilter += " and Station = '" + drpStation.SelectedValue + "'";
        }
        if (drpCatg.SelectedIndex > 0)
        {
            if (chk.Checked)
                rowFilter += "  and PTCMsNo = '" + drpCatg.SelectedValue + "'  ";
            else
                rowFilter += " and (PTCMsNo = '" + drpCatg.SelectedValue + "' or PTCMsNo is null) ";
        }


        dv.RowFilter = rowFilter;
        rep.DataSource = dv.ToTable();
        rep.DataBind();
    }



    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        if (tbl1.Rows.Count > 0)
            return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
        else
            return "";
    }


    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        ds = getdata.getUserTourPlan(drpUser.SelectedValue, drpType.SelectedValue);
        ViewState["tbl"] = ds.Tables[0];
        drpheadQtr.DataSource = ds.Tables[0].DefaultView.ToTable(true, "HeadQtr");
        drpheadQtr.DataTextField = "HeadQtr";
        drpheadQtr.DataValueField = "HeadQtr";
        drpheadQtr.DataBind();
        drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
        drpDistrict.DataSource = ds.Tables[0].DefaultView.ToTable(true, "District");
        drpDistrict.DataTextField = "District";
        drpDistrict.DataValueField = "District";
        drpDistrict.DataBind();
        drpDistrict.Items.Insert(0, new ListItem("Select", "0"));
        drpStation.DataSource = ds.Tables[0].DefaultView.ToTable(true, "Station");
        drpStation.DataTextField = "Station";
        drpStation.DataValueField = "Station";
        drpStation.DataBind();
        drpStation.Items.Insert(0, new ListItem("Select", "0"));
        fillData();

    }

    protected void drpheadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUser();

        DataView dv = dsusr.Tables[0].DefaultView;
        dv.RowFilter = "HeadQtr = '" + drpheadQtr.SelectedValue + "'";

        //drpUser.DataSource = dv.ToTable(true, "Name", "MId");
        //drpUser.DataTextField = "Name";
        //drpUser.DataValueField = "MId";
        //drpUser.DataBind();
        //drpUser.Items.Insert(0, new ListItem("Select", "0"));
        //drpUser.SelectedIndex = 0;
        ds = getdata.getUserTourPlan(drpUser.SelectedValue, drpType.SelectedValue);
        ViewState["tbl"] = ds.Tables[0];
        drpDistrict.DataSource = ds.Tables[0].DefaultView.ToTable(true, "District");
        drpDistrict.DataTextField = "District";
        drpDistrict.DataValueField = "District";
        drpDistrict.DataBind();
        drpDistrict.Items.Insert(0, new ListItem("Select", "0"));
        drpStation.DataSource = ds.Tables[0].DefaultView.ToTable(true, "Station");
        drpStation.DataTextField = "Station";
        drpStation.DataValueField = "Station";
        drpStation.DataBind();
        drpStation.Items.Insert(0, new ListItem("Select", "0"));
        rep.DataSource = ds.Tables[0];
        rep.DataBind();
    }

    protected void drpDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillData();
        DataTable dt = (DataTable)ViewState["tbl"];
        DataView dv = dt.DefaultView;
        dv.RowFilter = "District = '" + drpDistrict.SelectedValue + "'";
        drpStation.DataSource = dv.ToTable(true, "Station");
        drpStation.DataTextField = "Station";
        drpStation.DataValueField = "Station";
        drpStation.DataBind();
        drpStation.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void drpStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillData();
    }

    protected void drpCatg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpUser.SelectedIndex > 0)
        {
            fillData();
        }
    }

    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpType.SelectedValue == "1")
            drpCatg.Enabled = false;
        else
            drpCatg.Enabled = true;
        ds = getdata.getUserTourPlan(drpUser.SelectedValue, drpType.SelectedValue);
        ViewState["tbl"] = ds.Tables[0];
        fillData();
    }



    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        if (drpUser.SelectedIndex > 0)
        {
            fillData();
        }
    }
}