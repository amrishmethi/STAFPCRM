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

public partial class Admin_UserTourPlan : System.Web.UI.Page
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

            Session["AccessRigthsSet"] = getdata.AccessRights("EmployeeTourPlan.aspx", Soft["Type"] == "admin" ? "0" : Soft["EmployeeId"]).Tables[0];

            DataSet dsusr = getdata.getHqtrUser();

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
            fillData();
        }
    }

    public void fillData()
    {
        ds = getdata.getUserTourPlan(drpUser.SelectedValue);
               DataView dv = ds.Tables[1].DefaultView;
        if (drpheadQtr.SelectedIndex > 0)
        {
            dv.RowFilter = "HeadQtr = '" + drpheadQtr.SelectedValue + "'";
        }

        if (drpDistrict.SelectedIndex > 0)
        {
            dv.RowFilter = "District = '" + drpDistrict.SelectedValue + "'";
        }
        rep.DataSource = dv.ToTable();
        rep.DataBind();
    
    }

    //protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    if (e.CommandName == "Edit")
    //    {
    //        Response.Redirect("SecondarySalesPartyMaster.aspx?id=" + e.CommandArgument + "");
    //    }
    //    if (e.CommandName == "Delete")
    //    {
    //        string query = "update tbl_SecondarySalesParty set IsActive = 0  where ID=" + e.CommandArgument + "";
    //        data.executeCommand(query);
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Deleted Successfully......')", true);
    //        fillData();
    //    }
    //}


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
    //        HiddenField hddEmployeeType = (e.Item.FindControl("hddEmployeeType") as HiddenField);
    //        lnkAssbtn.Visible = hddEmployeeType.Value == "admin"?false:true;
    //    }
    //}



    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Save")
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox txtDate = (e.Item.FindControl("txtDate") as TextBox);
                Label lblHqtr = (e.Item.FindControl("lblHqtr") as Label);
                Label lblDist = (e.Item.FindControl("lblDist") as Label);
                Label lblStat = (e.Item.FindControl("lblStat") as Label);

                ds = getdata.saveUserTourPlan(e.CommandArgument.ToString(), drpUser.SelectedValue, lblHqtr.Text, lblDist.Text, lblStat.Text, txtDate.Text == ""?"":data.YYYYMMDD(txtDate.Text.Trim()));
            }
            fillData();
        }
    }

    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillData();
        drpheadQtr.DataSource = ds.Tables[1].DefaultView.ToTable(true, "HeadQtr");
        drpheadQtr.DataTextField = "HeadQtr";
        drpheadQtr.DataValueField = "HeadQtr";
        drpheadQtr.DataBind();
       // drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
        drpDistrict.DataSource = ds.Tables[1].DefaultView.ToTable(true, "District");
        drpDistrict.DataTextField = "District";
        drpDistrict.DataValueField = "District";
        drpDistrict.DataBind();
        drpDistrict.Items.Insert(0, new ListItem("Select", "0"));
        rep.DataSource = ds.Tables[1];
        rep.DataBind();
    }
    protected void drpheadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUser();
        DataView dv = dsusr.Tables[0].DefaultView;
        dv.RowFilter = "HeadQtr = '" + drpheadQtr.SelectedValue + "'";

        drpUser.DataSource = dv.ToTable(true, "Name", "MId");
        drpUser.DataTextField = "Name";
        drpUser.DataValueField = "MId";
        drpUser.DataBind();
        drpUser.SelectedIndex = 0;
        ds = getdata.getUserTourPlan(drpUser.SelectedValue);
        drpDistrict.DataSource = ds.Tables[1].DefaultView.ToTable(true, "District");
        drpDistrict.DataTextField = "District";
        drpDistrict.DataValueField = "District";
        drpDistrict.DataBind();
        drpDistrict.Items.Insert(0, new ListItem("Select", "0"));
        rep.DataSource = ds.Tables[1];
        rep.DataBind();
    }
    protected void drpDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillData();
    }
}