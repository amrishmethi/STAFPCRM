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
            mnth.Text = DateTime.Now.ToString("MM-yyyy");
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("UserWiseParty.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            gd.fillDepartment(drpDepartment);
            gd.FillPartyCategory(drpCatg);


            DataSet dsusr = getdata.getHqtrUserDpt("0");
            ViewState["tbl1"] = dsusr;
            DataView dv = dsusr.Tables[0].DefaultView;
            string sr = "0=0";
            if (drpDepartment.SelectedIndex > 0)
                sr += " and Dept_Id=" + drpDepartment.SelectedValue + "";
            if (drpStatus.SelectedIndex > 0)
                sr += " and Status='" + drpStatus.SelectedValue + "'";
            dv.RowFilter = sr;
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
        DataView dv = ((DataSet)ViewState["tbl1"]).Tables[0].DefaultView;
        string sr = "0=0";
        if (drpDepartment.SelectedIndex > 0)
            sr += " and Dept_Id=" + drpDepartment.SelectedValue + "";
        if (drpStatus.SelectedIndex > 0)
            sr += " and Status='" + drpStatus.SelectedValue + "'";
        dv.RowFilter = sr;
        dv.Sort = "Name";
        drpUser.DataSource = dv.ToTable(true, "Name", "MId");
        drpUser.DataTextField = "Name";
        drpUser.DataValueField = "MId";
        drpUser.DataBind();
        drpUser.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void fillData()
    {
        ds = getdata.getUserwiseParty(drpUser.SelectedValue, drpType.SelectedValue);

        ViewState["tbl"] = ds.Tables[0];
        DataView dv = ((DataTable)ViewState["tbl"]).DefaultView;

        string rowFilter = "0=0";
        if (drpheadQtr.SelectedIndex > 0)
        {
            rowFilter += " and HeadQtrNo = '" + drpheadQtr.SelectedValue + "'";
        }

        if (drpDistrict.SelectedIndex > 0)
        {
            rowFilter += " and DistrictNo = '" + drpDistrict.SelectedValue + "'";
        }
        if (drpStation.SelectedIndex > 0)
        {
            rowFilter += " and StationNo = '" + drpStation.SelectedValue + "'";
        }
        if (drpCatg.SelectedIndex > 0)
        {
            if (drpReportType.SelectedIndex == 2)
                rowFilter += "  and PTCMsNo = '" + drpCatg.SelectedValue + "'  ";
            if (drpReportType.SelectedIndex == 0)
                rowFilter += " and (PTCMsNo = '" + drpCatg.SelectedValue + "' or PTCMsNo is null) ";
            if (drpReportType.SelectedIndex == 1)
                rowFilter += "  and PTCMsNo is null  ";
        }
        if (drpGst.SelectedIndex > 0)
        {
            if (drpGst.SelectedIndex == 1)
                rowFilter += "and GSTNo=''";
            if (drpGst.SelectedIndex == 2)
                rowFilter += "and GSTNo<>''";
        }

        dv.RowFilter = rowFilter;
        DataTable dtt = dv.ToTable();
        dtt.Columns.Add("IsCheckIn");
        dtt.Columns.Add("SecondarySale");

        DataSet dsCheckIn = data.getDataSet("select Ci.MobileNo, ISNULL((Select SUM( CO.OrdQty * CO.OrdStpRate * I.CWeight)   FROM [STM_Tadkeshwar].[dbo].[tbl_CheckOutItem] CO     \r\n  left join [STM_Tadkeshwar].[dbo].[ITEM] I on I.ITName = CO.ITName  where FID in (Select ID from [STM_Tadkeshwar].[DBO].[TBL_CHECKOUT]  where CheckInID = CI.Id)),'') AMOunt  From tbl_CheckIn CI WHERE  Format(ADDEDDATE,'MM-yyyy')='" + mnth.Text + "'");
        foreach (DataRow dr in dtt.Rows)
        {
            dr["IsCheckIn"] = dsCheckIn.Tables[0].AsEnumerable().Where(x => x["MOBILENO"].ToString() == dr["MOBILE"].ToString()).Count() > 0 ? "Yes" : "No";
            dr["SecondarySale"] = dsCheckIn.Tables[0].Compute("Sum(AMOunt)", "MOBILENO='" + dr["MOBILE"].ToString() + "'");
        }
        dtt.AcceptChanges();
        rep.DataSource = dtt;
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
        DataView dv = ((DataSet)ViewState["tbl1"]).Tables[0].DefaultView;
        dv.RowFilter = "MId=" + drpUser.SelectedValue;
        dv.Sort = "District";
        drpDistrict.DataSource = dv.ToTable(true, "District", "DistrictNo");
        drpDistrict.DataTextField = "District";
        drpDistrict.DataValueField = "DistrictNo";
        drpDistrict.DataBind();
        drpDistrict.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void drpheadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");

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
        DataView dv = ((DataSet)ViewState["tbl1"]).Tables[0].DefaultView;
        dv.RowFilter = "DistrictNo=" + drpDistrict.SelectedValue;
        dv.Sort = "Station";
        drpStation.DataSource = dv.ToTable(true, "Station", "StationNO");
        drpStation.DataTextField = "Station";
        drpStation.DataValueField = "StationNO";
        drpStation.DataBind();
        drpStation.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void drpStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillData();
    }

    protected void drpCatg_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drpUser.SelectedIndex > 0)
        //{
        //    fillData();
        //}
    }

    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drpType.SelectedValue == "1")
        //    drpCatg.Enabled = false;
        //else
        //    drpCatg.Enabled = true;
        //ds = getdata.getUserTourPlan(drpUser.SelectedValue, drpType.SelectedValue);
        //ViewState["tbl"] = ds.Tables[0];
        //fillData();
    }



    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        //if (drpUser.SelectedIndex > 0)
        //{
        //    fillData();
        //}
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }
}