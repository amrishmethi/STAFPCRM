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
            Gd.FillStation(drpStation);
            DataSet dsusr = getdata.getHqtrUser();
            DataView dv = dsusr.Tables[0].DefaultView;
            dv.Sort = "HeadQtr";
            drpheadQtr.DataSource = dv.ToTable(true,"HeadQtr");
            drpheadQtr.DataTextField = "HeadQtr";
            drpheadQtr.DataValueField = "HeadQtr";
            drpheadQtr.DataBind();
            drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
            fillData();
        }
    }

    public void fillData()
    {
        ds = getdata.getSecondarySalesParty("SELECT", drpParty.SelectedValue,"", drpStation.SelectedValue, "","","",drpheadQtr.SelectedValue);
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
            ds = getdata.getSecondarySalesParty("DELETE", e.CommandArgument.ToString(), "", "", "", "", "","");
            if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "1") { 
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
        fillData();

    }
}