using Org.BouncyCastle.Ocsp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Admin_TargetpartywiseView : System.Web.UI.Page
{
    DataTable dtGrp = new DataTable();
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;
    DataTable Dt = new DataTable();
    DataTable Dt1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            gd.fillDepartment(drpDepartment);
            gd.FillPartyCategory(drpCatg);
            gd.FillMainGroup(lstGroup);
            //gd.FillAccount(drpParty, drpCatg.SelectedValue);
            mnth.Text = DateTime.Now.ToString("MM-yyyy");


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
        string grp = "0";
        foreach (ListItem item in lstGroup.Items)
        {
            if (item.Selected)
            {
                grp += "," + item.Value;
            }
        }
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "PROC_TARGETVIEWPARTYWISE";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@HEADQTR", drpheadQtr.SelectedValue);
        cmd.Parameters.AddWithValue("@PARTYID", drpParty.SelectedValue);
        cmd.Parameters.AddWithValue("@DISTRICID", drpDistrict.SelectedValue);
        cmd.Parameters.AddWithValue("@STATIONID", drpStation.SelectedValue);
        cmd.Parameters.AddWithValue("@CATID", drpCatg.SelectedValue);
        cmd.Parameters.AddWithValue("@GROUP", grp);
        cmd.Parameters.AddWithValue("@MONTH", mnth.Text);
        DataSet dss = data.getDataSet(cmd);
        DataView dv = dss.Tables[0].DefaultView;
        dv.Sort = "ACNAME";


        DataTable dtt = dv.ToTable();
        DataRow drr = dtt.NewRow();
        drr["ACNAME"] = "Total";
        drr["TARGETQTY"] = dtt.Compute("Sum(TARGETQTY)", "");
        drr["ORDBAG"] = dtt.Compute("Sum(ORDBAG)", "");
        drr["PENDING"] = dtt.Compute("Sum(PENDING)", "");
        dtt.Rows.Add(drr);
        ViewState["tbl"] = dtt;
     
        repDetail.DataSource = dtt;
        repDetail.DataBind();
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
    }
    protected void drpCatg_SelectedIndexChanged(object sender, EventArgs e)
    {
        gd.FillAccount(drpParty, drpCatg.SelectedValue);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillData();
    }

}