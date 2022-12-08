using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_AdminPolicy : System.Web.UI.Page
{
    Master master = new Master();
    Data data = new Data();
    DataSet ds = new DataSet();
    private HttpCookie Soft;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = master.AccessRights("AdminPolicy.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            FillData();
        }
    }

    public void FillData()
    {
        ds = master.GetAdminPolicy("Select", "0", "","", "", "");
        repAdminPolicy.DataSource = ds;
        repAdminPolicy.DataBind();
    }

    protected void repAdminPolicy_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            ds = master.GetAdminPolicy("Delete", e.CommandArgument.ToString(), "","", "", "");
            string _Path = (ds.Tables[0].Rows[0]["RESULT"].ToString() == "0") ? "window.location ='AdminPolicy.aspx'" : "";
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + ds.Tables[0].Rows[0]["RESULTMSG"].ToString() + "');" + _Path, true);
        }
    }
    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }
}