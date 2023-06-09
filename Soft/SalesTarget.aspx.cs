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

public partial class Admin_SalesTarget : System.Web.UI.Page
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
            drpDepartment.SelectedValue = "2";
            gd.FillCRMUser(drpUser, drpDepartment.SelectedValue, drpStatus.SelectedValue);
            chk.Checked = true;
            drpCatg.SelectedValue = "1115";
            mnth.Text = DateTime.Now.ToString("MM-yyyy");



            DataSet dsusr = getdata.getHqtrUser();
            gd.FillPartyCategory(drpCatg);
            DataView dv = dsusr.Tables[0].DefaultView;
            //dv.Sort = "Name";
            //drpUser.DataSource = dv.ToTable(true, "Name", "MId");
            //drpUser.DataTextField = "Name";
            //drpUser.DataValueField = "MId";
            //drpUser.DataBind();
            //drpUser.Items.Insert(0, new ListItem("Select", "0"));

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
        int month = Convert.ToInt32(mnth.Text.Split('-')[0]);
        int year = Convert.ToInt32(mnth.Text.Split('-')[1]);
        string _DD = year + "-" + month + "-01";

        DataView dv = data.getDataSet("Proc_SalesTarget '" + drpUser.SelectedValue + "','" + _DD + "'").Tables[1].DefaultView;
        string rowFilter = "0=0";
        string grp = "";
        foreach (ListItem item in drpDistrict.Items)
        {
            if (item.Selected)
            {
                grp += " or District ='" + item.Value + "'";
            }
        }
        string _grp = grp.Count() > 0 ? grp.Remove(0, 4) : grp;
        if (!string.IsNullOrEmpty(_grp))
        {
            rowFilter += "AND (" + _grp + ")";
        }


        if (drpheadQtr.SelectedIndex > 0)
        {
            rowFilter += " and HeadQtr = '" + drpheadQtr.SelectedValue + "'";
        }

        //if (drpDistrict.SelectedIndex > 0)
        //{
        //    rowFilter += " and District = '" + drpDistrict.SelectedValue + "'";
        //}
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
        //fillData();

    }

    protected void drpheadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUser();

        DataView dv = dsusr.Tables[0].DefaultView;
        dv.RowFilter = "HeadQtr = '" + drpheadQtr.SelectedValue + "'";
        ds = getdata.getUserTourPlan(drpUser.SelectedValue, drpType.SelectedValue);
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
    }

    protected void drpDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["tbl"];

        string grp = "";
        foreach (ListItem item in drpDistrict.Items)
        {
            if (item.Selected)
            {
                grp += " or District ='" + item.Value + "'";
            }
        }
        string _grp = grp.Count() > 0 ? grp.Remove(0, 4) : grp;
        DataView dv = dt.DefaultView;
        //dv.RowFilter = "District = '" + drpDistrict.SelectedValue + "'";
        if (!string.IsNullOrEmpty(_grp))
        {
            dv.RowFilter = "0=0 AND (" + _grp + ")";
            dv.Sort = "Station";
        }
        drpStation.DataSource = dv.ToTable(true, "Station", "District");
        drpStation.DataTextField = "Station";
        drpStation.DataValueField = "Station";
        drpStation.DataBind();
        drpStation.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void drpStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillData();
    }

    protected void drpCatg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpUser.SelectedIndex > 0)
        {
            //fillData();
        }
    }

    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpType.SelectedValue == "1")
            drpCatg.Enabled = false;
        else
            drpCatg.Enabled = true;
        ds = getdata.getUserTourPlan(drpUser.SelectedValue, drpType.SelectedValue);
        //ViewState["tbl"] = ds.Tables[0];
    }



    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        if (drpUser.SelectedIndex > 0)
        {
            //fillData();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillData();
    }

    protected void btnSalary_Click(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        for (int i = 0; i < rep.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)rep.Items[i].FindControl("chk");
            HiddenField hddId = (HiddenField)rep.Items[i].FindControl("hddId");
            TextBox txtPowder = (TextBox)rep.Items[i].FindControl("txtPowder");
            TextBox txtBar_Tub = (TextBox)rep.Items[i].FindControl("txtBar_Tub");
            if (txtPowder.Text != "0" || txtBar_Tub.Text != "0")
            {
                data.executeCommand("Update tbl_SalesTarget SET IsUpdate=1 ,BAR_TUB='" + txtBar_Tub.Text + "',POWDER='" + txtPowder.Text + "' WHERE TargetId=" + hddId.Value);
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Sales Target Saved Successfully');window.location ='SalesTarget.aspx'", true);
    }
}