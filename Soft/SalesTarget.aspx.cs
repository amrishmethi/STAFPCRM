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
            chk.Checked = true;
            drpCatg.SelectedValue = "1115";
            mnth.Text = DateTime.Now.ToString("MM-yyyy");
            gd.FillPartyCategory(drpCatg);


            DataSet dsusr = getdata.getHqtrUserDpt(drpDepartment.SelectedValue);
            ViewState["tbl1"] = dsusr;
            DataView dv = dsusr.Tables[0].DefaultView;
            if (drpStatus.SelectedIndex > 0)
                dv.RowFilter = " Status='" + drpStatus.SelectedValue + "'";
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

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = (DataSet)ViewState["tbl1"];
        DataView dv = dsusr.Tables[0].DefaultView;
        if (drpStatus.SelectedIndex > 0)
            dv.RowFilter = " Status='" + drpStatus.SelectedValue + "'";
        dv.Sort = "Name";
        drpUser.DataSource = dv.ToTable(true, "Name", "MId");
        drpUser.DataTextField = "Name";
        drpUser.DataValueField = "MId";
        drpUser.DataBind();
        drpUser.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void fillData()
    {
        int month = Convert.ToInt32(mnth.Text.Split('-')[0]);
        int year = Convert.ToInt32(mnth.Text.Split('-')[1]);
        string _DD = year + "-" + month + "-01";

        DataView dv = data.getDataSet("Proc_SalesTarget_New '" + drpheadQtr.SelectedValue + "','" + _DD + "','" + drpCatg.SelectedValue + "'").Tables[1].DefaultView;
        string rowFilter = "0=0";
        string grp = "";
        foreach (ListItem item in drpDistrict.Items)
        {
            if (item.Selected)
            {
                grp += " or DistrictNO ='" + item.Value + "'";
            }
        }
        string _grp = grp.Count() > 0 ? grp.Remove(0, 4) : grp;
        if (!string.IsNullOrEmpty(_grp))
        {
            rowFilter += "AND (" + _grp + ")";
        }

        if (drpStation.SelectedIndex > 0)
        {
            rowFilter += " and StationNO = '" + drpStation.SelectedValue + "'";
        }
        if (drpCatg.SelectedIndex > 0)
        {
            if (chk.Checked)
                rowFilter += "  and PTCMsNo = '" + drpCatg.SelectedValue + "'  ";
            else
                rowFilter += " and (PTCMsNo = '" + drpCatg.SelectedValue + "' or PTCMsNo is null) ";
        }
        dv.RowFilter = rowFilter;
        dv.Sort = "District,PartyCategory,Party,Station";
        rep.DataSource = dv.ToTable();
        rep.DataBind();

        lblSale_POWDERl.Value = dv.ToTable().Compute("sum(Sale_POWDER)", "").ToString();
        lblSale_BAR_AND_TUB.Value = dv.ToTable().Compute("sum(Sale_BAR_AND_TUB)", "").ToString();
        lblKLEAN_POWDER.Value = dv.ToTable().Compute("sum(KLEAN_POWDER)", "").ToString();
        lblPowderTotal.Value = dv.ToTable().Compute("sum(Powder)", "").ToString();
        lblBar_Tub.Value = dv.ToTable().Compute("sum(Bar_Tub)", "").ToString();
        lblclean.Value = dv.ToTable().Compute("sum(KLEAN_BOLD_POWDER)", "").ToString();
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
        DataSet dsusr = (DataSet)ViewState["tbl1"];
        DataView dv = dsusr.Tables[0].DefaultView;
        dv.RowFilter = "Mid=" + drpUser.SelectedValue;
        dv.Sort = "HeadQtr";
        drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNO");
        drpheadQtr.DataTextField = "HeadQtr";
        drpheadQtr.DataValueField = "HeadQtrNO";
        drpheadQtr.DataBind();
        drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));

        dv.Sort = "District";
        drpDistrict.DataSource = dv.ToTable(true, "District", "DistrictNo");
        drpDistrict.DataTextField = "District";
        drpDistrict.DataValueField = "DistrictNo";
        drpDistrict.DataBind();
        drpDistrict.Items.Insert(0, new ListItem("Select", "0"));

        dv.Sort = "Station";
        drpStation.DataSource = dv.ToTable(true, "Station", "StationNO");
        drpStation.DataTextField = "Station";
        drpStation.DataValueField = "StationNO";
        drpStation.DataBind();
        drpStation.Items.Insert(0, new ListItem("Select", "0"));

    }

    protected void drpheadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = (DataSet)ViewState["tbl1"];
        DataView dv = dsusr.Tables[0].DefaultView;
        dv.RowFilter = "HeadQtrNO = '" + drpheadQtr.SelectedValue + "'";
        dv.Sort = "District";
        drpDistrict.DataSource = dv.ToTable(true, "District", "DistrictNo");
        drpDistrict.DataTextField = "District";
        drpDistrict.DataValueField = "DistrictNo";
        drpDistrict.DataBind();
        drpDistrict.Items.Insert(0, new ListItem("Select", "0"));

        dv.Sort = "Station";
        drpStation.DataSource = dv.ToTable(true, "Station", "StationNo");
        drpStation.DataTextField = "Station";
        drpStation.DataValueField = "StationNo";
        drpStation.DataBind();
        drpStation.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void drpDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = (DataSet)ViewState["tbl1"];
        DataView dv = dsusr.Tables[0].DefaultView;

        string grp = "";
        foreach (ListItem item in drpDistrict.Items)
        {
            if (item.Selected)
            {
                grp += " or DistrictNo ='" + item.Value + "'";
            }
        }
        string _grp = grp.Count() > 0 ? grp.Remove(0, 4) : grp;
        if (!string.IsNullOrEmpty(_grp))
        {
            dv.RowFilter = "0=0 AND (" + _grp + ")";
        }
        dv.Sort = "Station";
        drpStation.DataSource = dv.ToTable(true, "Station", "StationNo");
        drpStation.DataTextField = "Station";
        drpStation.DataValueField = "StationNo";
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
            TextBox txtclean = (TextBox)rep.Items[i].FindControl("txtclean");
            if (txtPowder.Text != "0" || txtBar_Tub.Text != "0")
            {
                data.executeCommand("Update tbl_SalesTarget SET IsUpdate=1 ,BAR_TUB='" + txtBar_Tub.Text + "',POWDER='" + txtPowder.Text + "',KLEAN_BOLD_POWDER='" + txtclean.Text + "',Modify_Date=Getdate()  WHERE TargetId=" + hddId.Value);
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Sales Target Saved Successfully');window.location ='SalesTarget.aspx'", true);
    }
}