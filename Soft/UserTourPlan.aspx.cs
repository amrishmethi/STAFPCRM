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
    GetData Gd = new GetData();
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


            Gd.fillDepartment(drpDepartment);
            bindDrp(true, true, true);
            //fillData();
        }
    }

    private void bindDrp(bool isuser, bool ishqtr, bool isdstrct)
    {
        DataSet dsusr = getdata.getHqtrUserDpt(drpDepartment.SelectedValue);
        DataView dv = dsusr.Tables[0].DefaultView;
        if (isuser)
        {
            if (drpheadQtr.SelectedIndex > 0)
                dv.RowFilter = "HeadQtr='" + drpheadQtr.SelectedItem.Text + "'";
            dv.Sort = "Name";
            drpUser.DataSource = dv.ToTable(true, "Name", "MId");
            drpUser.DataTextField = "Name";
            drpUser.DataValueField = "MId";
            drpUser.DataBind();
            drpUser.Items.Insert(0, new ListItem("Select", "0"));
        }
        if (ishqtr)
        {
            if (drpUser.SelectedIndex > 0)
                dv.RowFilter = "Name='" + drpUser.SelectedItem.Text + "'";
            dv.Sort = "HeadQtr";
            drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr");
            drpheadQtr.DataTextField = "HeadQtr";
            drpheadQtr.DataValueField = "HeadQtr";
            drpheadQtr.DataBind();
            drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
        }
        if (isdstrct)
        {
            if (drpUser.SelectedIndex > 0)
                dv.RowFilter = "Name='" + drpUser.SelectedItem.Text + "'";
            dv.Sort = "HeadQtr";
            drpDistrict.DataSource = dv.ToTable(true, "District");
            drpDistrict.DataTextField = "District";
            drpDistrict.DataValueField = "District";
            drpDistrict.DataBind();
            drpDistrict.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind(sender);
    }
    public void fillData()
    {
        ds = getdata.getUserTourPlan(drpUser.SelectedValue, "");
        DataView dv = ds.Tables[1].DefaultView;
        string _filter = "0=0 ";
        if (drpheadQtr.SelectedIndex > 0)
        {
            _filter += " and HeadQtr = '" + drpheadQtr.SelectedValue + "'";
        }
        if (drpDepartment.SelectedIndex > 0)
        {
            _filter += " and Dept_Id = '" + drpDepartment.SelectedValue + "'";
        }
        if (drpDistrict.SelectedIndex > 0)
        {
            _filter += " and District = '" + drpDistrict.SelectedValue + "'";
        }
        if (dpFrom.Text != "")
            _filter += " and TDate1>='" + data.ConvertToDateTime(dpFrom.Text) + "'";
        if (dpTo.Text != "")
            _filter += " and  TDate1<='" + data.ConvertToDateTime(dpTo.Text) + "'";
        dv.RowFilter = _filter;
        rep.DataSource = dv.ToTable();
        rep.DataBind();

    }


    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }

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

                ds = getdata.saveUserTourPlan(e.CommandArgument.ToString(), drpUser.SelectedValue, lblHqtr.Text, lblDist.Text, lblStat.Text, txtDate.Text == "" ? "" : data.YYYYMMDD(txtDate.Text.Trim()));
            }
            fillData();
        }
    }

    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind(sender);
    }
    protected void drpheadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind(sender);
    }
    protected void drpDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }


    private void Bind(object sender)
    {
        DropDownList ddl = sender as DropDownList;
        if (ddl == drpUser)
        {
            bindDrp(false, true, false);
        }
        if (ddl == drpheadQtr)
        {
            bindDrp(true, false, true);
        }
        if (ddl == drpDepartment)
        {
            bindDrp(true, false, false);
        }
        if (ddl == drpDistrict)
        {
            bindDrp(true, false, true);
        }
    }
}