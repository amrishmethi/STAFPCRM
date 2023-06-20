using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SecondarySalesParty : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    Data data = new Data();
    GetData Gd = new GetData();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("SecondarySalesParty.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            Gd.FillParty(drpParty);
            Gd.FillPrimaryStation(drpStation);
            ViewState["station"] = Gd.FillStation();
            DataSet dsusr = getdata.getHqtrUser();
            DataView dv = dsusr.Tables[0].DefaultView;
            dv.Sort = "District";
            drpheadQtr.DataSource = dv.ToTable(true, "District");
            drpheadQtr.DataTextField = "District";
            drpheadQtr.DataValueField = "District";
            drpheadQtr.DataBind();
            drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
            //fillData();
        }
    }

    public void fillData()
    {
        ds = getdata.getSecondarySalesParty("SELECT", drpParty.SelectedValue, drpStation.SelectedValue, drpStation.SelectedItem.Text, "", "", "", drpheadQtr.SelectedValue, drpBeat1.SelectedValue);
        rep.DataSource = ds.Tables[0];
        rep.DataBind();
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("SecondarySalesPartyMaster.aspx?id=" + e.CommandArgument + "");
        }
        if (e.CommandName == "Delete")
        {
            ds = getdata.getSecondarySalesParty("DELETE", e.CommandArgument.ToString(), "", "", "", "", "", "", "");
            if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Deleted Successfully......')", true);
                fillData();
            }
        }
    }


    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }


    protected void drpStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        ds = (DataSet)ViewState["station"];
        DataView view = new DataView(ds.Tables[0]);
        string _RowFilter = "0=0 and IsActive=1";
        if (drpheadQtr.SelectedIndex > 0)
            _RowFilter += " and District='" + drpheadQtr.SelectedValue + "'";

        view.RowFilter = _RowFilter;
        view.Sort = "Station";
        DataTable District = view.ToTable(true, "Station");
        Gd.FillData(drpStation, District, "Station", "Station");
    }

    protected void tnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }

    protected void drpStation_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ds = Gd.FillStationBeat();
        DataView view = new DataView(ds.Tables[0]);
        string _RowFilter = "0=0";
        if (drpheadQtr.SelectedIndex > 0)
            _RowFilter += " and District='" + drpheadQtr.SelectedValue + "'";
        if (drpStation.SelectedIndex > 0)
            _RowFilter += " and Station='" + drpStation.SelectedValue + "'";
        view.RowFilter = _RowFilter;
        view.Sort = "station";

        DataTable District = view.ToTable(true, "Beat", "StationId");
        Gd.FillData(drpBeat, District, "Beat", "StationId");
        Gd.FillData(drpBeat1, District, "Beat", "StationId");
    }

    protected void btnUpdateBeat_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < rep.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)rep.Items[i].FindControl("chk");
            HiddenField hddPartyId = (HiddenField)rep.Items[i].FindControl("hddPartyId");
            if (chk.Checked == true)
            {
                data.executeCommand("UPDATE TBL_SECONDARYSALESPARTY SET BeatId='" + drpBeat.SelectedValue + "'  WHERE Id=" + hddPartyId.Value);
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Beat Update Successfully');window.location ='SecondarySalesParty.aspx'", true);

    }
}