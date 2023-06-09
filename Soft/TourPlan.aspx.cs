using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_TourPlan : System.Web.UI.Page
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
            if (Request.QueryString["ID"] != null)
            {
                FillData(Request.QueryString["ID"]);
            }
        }
    }

    private void FillData(string Id)
    {
        ds = getdata.getUserTourPlan(drpUser.SelectedValue, "");
        DataView dv = ds.Tables[1].DefaultView;
        string _filter = "0=0 ";
        _filter += " and TID= '" + Id + "'";
        dv.RowFilter = _filter;

        DataTable dtt = dv.ToTable();
        hddid.Value = dtt.Rows[0]["Dept_Id"].ToString();
        bindDrp(true, false, false);
        drpUser.SelectedValue = dtt.Rows[0]["CRMUserId"].ToString();
        bindDrp(false, true, false);
        drpheadQtr.SelectedValue = dtt.Rows[0]["HeadQtr"].ToString();
        bindDrp(true, false, true);
        drpDistrict.SelectedValue = dtt.Rows[0]["District"].ToString();
        Gd.FillStation(drpStation);
        drpStation.SelectedItem.Text = dtt.Rows[0]["Station"].ToString();
        dpFrom.Text = dtt.Rows[0]["TDate"].ToString();
        txtPurpose.Text = dtt.Rows[0]["Purpose"].ToString();
    }

    private void bindDrp(bool isuser, bool ishqtr, bool isdstrct)
    {
        DataSet dsusr = getdata.getHqtrUserDpt(hddid.Value);
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

    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind(sender);
    }
    protected void drpheadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind(sender);
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
        if (ddl == drpDistrict)
        {
            bindDrp(true, false, true);
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string sqlcom = "Update [TourPlan] set District='" + drpDistrict.SelectedItem.Text + "', Station='" + drpStation.SelectedItem.Text + "', TDate='" + data.YYYYMMDD(dpFrom.Text.Trim()) + "' , Purpose='" + txtPurpose.Text.Trim() + "' where Id=" + Request.QueryString["Id"].ToString() + "";
        data.executeCommand(sqlcom);
         
        Page.ClientScript.RegisterStartupScript(typeof(Page), "close", string.Format("<script type='text/javascript'>{0}</script>", "parent.location.href='UserTourPlan.aspx'; parent.$.fancybox.close() ;")); 
    }
}