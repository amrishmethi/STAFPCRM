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

            DataSet dsusr = getdata.getHqtrUser();
            if (RadioButton1.Checked)
            {
                DataView dv = dsusr.Tables[0].DefaultView;
                dv.Sort = "Name";
                drpUser.DataSource = dv.ToTable(true, "Name", "MId");
                drpUser.DataTextField = "Name";
                drpUser.DataValueField = "MId";
                drpUser.DataBind();
                drpUser.Items.Insert(0, new ListItem("Select", "0"));

                ////dv.Sort = "HeadQtr";
                ////drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr");
                ////drpheadQtr.DataTextField = "HeadQtr";
                ////drpheadQtr.DataValueField = "HeadQtr";
                ////drpheadQtr.DataBind();
                drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
            }
            ds = getdata.getUserTourPlan(drpUser.SelectedValue,"");
            ViewState["tbl"] = ds.Tables[2];
            fillData();
        }
    }

    public void fillData()
    {
        //ds = getdata.getUserTourPlan(drpUser.SelectedValue);
        string str = "";
        DataView dv = ((DataTable)ViewState["tbl"]).DefaultView;
        if (drpUser.SelectedIndex > 0)
        {
            str += "id = '" + drpUser.SelectedValue + "'";
        }
        if (str != "") str += " and ";
        if (drpheadQtr.SelectedIndex > 0)
        {    str += " HeadQtr = '" + drpheadQtr.SelectedValue + "'";
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

    //protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        HyperLink lnkAssbtn = (e.Item.FindControl("lnkAssbtn") as HyperLink);
    //        HiddenField hddUid = (HiddenField)e.Item.FindControl("hddUid");
    //        HiddenField hddUserType = (e.Item.FindControl("hddUserType") as HiddenField);
    //        lnkAssbtn.Visible = hddUserType.Value == "admin"?false:true;
    //    }
    //}




    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButton1.Checked)
        {
            ds = getdata.getUserTourPlan(drpUser.SelectedValue,"");
            ViewState["tbl"] = ds.Tables[2];
            DataView dv = ds.Tables[2].DefaultView;
            dv.RowFilter = "id = "+ drpUser.SelectedValue ;
            drpheadQtr.DataSource =dv.ToTable(true, "HeadQtr");
            drpheadQtr.DataTextField = "HeadQtr";
            drpheadQtr.DataValueField = "HeadQtr";
            drpheadQtr.DataBind();
            drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
            if (ds.Tables[2].Rows.Count > 0)
            {
                drpheadQtr.SelectedIndex = 1;
            }
        }
        fillData();
        //rep.DataSource = ds.Tables[2];
        //rep.DataBind();
    }

    protected void drpheadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButton2.Checked)
        {
            DataSet dsusr = getdata.getHqtrUser();
            DataView dv = dsusr.Tables[0].DefaultView;
            dv.RowFilter = "HeadQtr = '" + drpheadQtr.SelectedValue + "'";

            drpUser.DataSource = dv.ToTable(true, "Name", "MId");
            drpUser.DataTextField = "Name";
            drpUser.DataValueField = "MId";
            drpUser.DataBind();
            drpUser.Items.Insert(0, new ListItem("Select", "0"));

        }
        fillData();
    }

    //protected void drpDistrict_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillData();
    //}

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUser();
        DataView dv = dsusr.Tables[0].DefaultView;
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
            drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr");
            drpheadQtr.DataTextField = "HeadQtr";
            drpheadQtr.DataValueField = "HeadQtr";
            drpheadQtr.DataBind();
            drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
            drpUser.Items.Clear();
            drpUser.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}