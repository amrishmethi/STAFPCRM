using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Admin_TargetPartyWise : System.Web.UI.Page
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
            gd.FillPartyCategory(drpCatg);
            gd.FillMainGroup(lstGroup);
            //gd.FillAccount(drpParty, drpCatg.SelectedValue);
            DataSet dsusr = getdata.getHqtrUserDpt("0");
            ViewState["tbl1"] = dsusr;
            DataView dv = dsusr.Tables[0].DefaultView;
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
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "PROC_GETPARTYLIST";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@USERID", drpUser.SelectedValue);
        cmd.Parameters.AddWithValue("@PARTYID", drpParty.SelectedValue);
        cmd.Parameters.AddWithValue("@DISTRICID", drpDistrict.SelectedValue);
        cmd.Parameters.AddWithValue("@STATIONID", drpStation.SelectedValue);
        cmd.Parameters.AddWithValue("@CATID", drpCatg.SelectedValue);
        DataSet dss = data.getDataSet(cmd);
        DataTable dtt = dss.Tables[0]; ;


        DataRow drr = dtt.NewRow();
        drr["Party"] = "Total";
        drr["TARGETQTY"] = dtt.Compute("sum(TARGETQTY)", "");
        dtt.Rows.Add(drr);

        dtt.AcceptChanges();
        ViewState["tbl"] = dtt;
        DataView dv = ((DataTable)ViewState["tbl"]).DefaultView;
        dv.Sort = "Party";
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

        ds = getdata.getUserTourPlan(drpUser.SelectedValue, "1");
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



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }

    protected void btnTarget_Click(object sender, EventArgs e)
    {
        string grp = "0";
        foreach (ListItem item in lstGroup.Items)
        {
            if (item.Selected)
            {
                grp += "," + item.Value;
            }
        }
        DataTable Dt = (DataTable)ViewState["tbl"];
        DataView dv = Dt.DefaultView;
        //dv.RowFilter = "TARGETQTY>0 and TARGETID";

        foreach (DataRow drr in dv.ToTable().Rows)
        {
            string query = "";
            if (drr["Party"].ToString().ToUpper() != "TOTAL")
            {
                if (drr["TARGETID"].ToString() == "0")
                {
                    if (drr["TARGETQTY"].ToString() == "0")
                        query = "insert into tbl_SaleTargetPartyWise (PartyId,GroupId,Qty,MobileNo) values('" + drr["PartyId"] + "','" + grp + "','" + drr["TARGETQTY"] + "','" + drr["WHATSAPPNO"] + "')";
                }
                else
                {
                    query = "Update tbl_SaleTargetPartyWise SET GroupId='" + grp + "',Qty='" + drr["TARGETQTY"] + "' where TARGETID='" + drr["TARGETID"] + "'";
                }
                data.executeCommand(query);
            }
        }

        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Sales Target Party Wise Saved Successfully');window.location ='TargetPartyWise.aspx'", true);
    }

    protected void drpCatg_SelectedIndexChanged(object sender, EventArgs e)
    {
        gd.FillAccount(drpParty, drpCatg.SelectedValue);
    }

    protected void txtRep_TextChanged(object sender, EventArgs e)
    { 
        DataTable Dt = (DataTable)ViewState["tbl"];
        RepeaterItem item1 = ((TextBox)sender).NamingContainer as RepeaterItem; 
        HiddenField hddWHATSAPPNO = (HiddenField)item1.FindControl("hddWHATSAPPNO");
        TextBox txtRep = (TextBox)item1.FindControl("txtRep"); 

        DataRow drr = Dt.Select("PARTYID='" + hddWHATSAPPNO.Value.ToString() + "'").FirstOrDefault();
        drr["TARGETQTY"] = txtRep.Text;
        Dt.AcceptChanges();
        ViewState["tbl"] = Dt;
        int j = Dt.Rows.Count - 1;

        TextBox txtRep1 = (TextBox)rep.Items[j].FindControl("txtRep");

        txtRep1.Text = Dt.Compute("sum(TARGETQTY)", "Party<>'Total'").ToString();

    }
}