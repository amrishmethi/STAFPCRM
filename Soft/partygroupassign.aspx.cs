using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_PartyAssign : System.Web.UI.Page
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

            gd.FillPartyCategory(drpCatg);
            gd.FillMainGroup(lstGrp);
            gd.FillGroup1(drpGrp);


            DataSet dsusr = getdata.getHqtrUserDpt("0");
            ViewState["tbl1"] = dsusr;
            DataView dv = dsusr.Tables[0].DefaultView;
            dv.Sort = "HeadQtr";
            drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
            drpheadQtr.DataTextField = "HeadQtr";
            drpheadQtr.DataValueField = "HeadQtrNo";
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
    }
    protected void drpheadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataView dv = ((DataSet)ViewState["tbl1"]).Tables[0].DefaultView;
        dv.RowFilter = "HeadQtrNo=" + drpheadQtr.SelectedValue;
        drpDistrict.DataSource = dv.ToTable(true, "District", "DistrictNo");
        drpDistrict.DataTextField = "District";
        drpDistrict.DataValueField = "DistrictNo";
        drpDistrict.DataBind();
        drpDistrict.Items.Insert(0, new ListItem("Select", "0"));
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
        string grp = "0";
        foreach (ListItem item in drpGrp.Items)
        {
            if (item.Selected)
            {
                grp += "," + item.Value;
            }
        }
        DataSet dsusr = getdata.GetPartyList(drpDistrict.SelectedValue, drpStation.SelectedValue, drpCatg.SelectedValue, grp);

        ViewState["PartyList"] = dsusr.Tables[0];
        ViewState["GroupList"] = dsusr.Tables[1];

        rep.DataSource = dsusr;
        rep.DataBind();
    }


    protected void btnConfirmY_Click(object sender, EventArgs e)
    {
        gd.FillMainGroup(lstGrp);



        RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;
        string id = (item.FindControl("hddEnqID") as HiddenField).Value;
        string lblParty = (item.FindControl("lblParty") as Label).Text;
        string hddGroups = (item.FindControl("hddGroups") as HiddenField).Value;

        lblHead.Text = lblParty;

        DataTable dtable = ((DataTable)ViewState["GroupList"]);
        foreach (DataRow drr in dtable.Rows)
        {
            drr["PartyID"] = id;
            drr["chk"] = (hddGroups.Split(',').Contains(drr["CMsCode"])) ? "1" : "0";
        }
        dtable.AcceptChanges();
        ViewState["GroupList"] = dtable;
        repsku.DataSource = dtable;
        repsku.DataBind();
        mpe.Show();
    }



    protected void btnApply_Click(object sender, EventArgs e)
    {
        string CMSCode = "", CMSName = "";
        HiddenField hddAssID = new HiddenField();


        DataTable dtable1 = ((DataTable)ViewState["GroupList"]);
        foreach (DataRow drr in dtable1.Rows)
        {
            if (drr["Chk"].ToString() == "1")
            {
                CMSCode += drr["CMSCode"].ToString() + ",";
                CMSName += drr["CMSName"].ToString() + ",";
            }
        }
        hddAssID.Value = dtable1.Rows[0]["PartyID"].ToString();
        data.executeCommand("Update ACCOUNT set ItemGroup='" + CMSCode.Substring(0, CMSCode.Length - 1) + "' where id='" + hddAssID.Value.ToString() + "'");

        DataTable dtable = ((DataTable)ViewState["PartyList"]);
        foreach (DataRow drr in dtable.Rows)
        {
            if (drr["Id"].ToString() == hddAssID.Value.ToString())
            {
                drr["ITEMGROUP"] = CMSCode.Substring(0, CMSCode.Length - 1);
                drr["ITEMGROUPS"] = CMSName.Substring(0, CMSName.Length - 1);
            }
        }
        dtable.AcceptChanges();
        ViewState["PartyList"] = dtable;
        rep.DataSource = dtable;
        rep.DataBind();
    }

    protected void lstGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtable = ((DataTable)ViewState["GroupList"]);
        DataView dv = dtable.DefaultView;
        if (lstGrp.SelectedIndex > 0)
            dv.RowFilter = "MCMsCode='" + lstGrp.SelectedValue + "'";
        repsku.DataSource = dv.ToTable(); ;
        repsku.DataBind();
    }

    protected void chkItems_CheckedChanged(object sender, EventArgs e)
    {
        DataTable dtable = ((DataTable)ViewState["GroupList"]);
        foreach (RepeaterItem item in repsku.Items)
        {
            HiddenField hddCmsCode = (HiddenField)item.FindControl("hddCmsCode");
            CheckBox chkItems = item.FindControl("chkItems") as CheckBox;
            foreach (DataRow drr in dtable.Rows)
            {
                if (drr["CMSCode"].ToString() == hddCmsCode.Value)
                    drr["chk"] = chkItems.Checked;
            }
        }
        dtable.AcceptChanges();
        ViewState["GroupList"] = dtable;
    }
}