using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SecondarySalesStation : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    Data data = new Data();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

           // Soft = Request.Cookies["STFP"];

            //Session["AccessRigthsSet"] = getdata.AccessRights("SecondarySalesStation.aspx", Soft["Type"] == "Soft" ? "0" : Soft["UserId"]).Tables[0];
           getdata.FillStation(drpStation);
            fillData();
        }
    }

    public void fillData()
    {
        ds = getdata.getSecondarySalesStation("SELECT", drpStation.SelectedValue,"");
       rep.DataSource = ds.Tables[0];
        rep.DataBind();
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("SecondarySalesStationMaster.aspx?id=" + e.CommandArgument + "");
        }
        if (e.CommandName == "Delete")
        {
            ds = getdata.getSecondarySalesStation("DELETE", e.CommandArgument.ToString(), "");
            if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Deleted Successfully......')", true);
                fillData();
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }

   
}