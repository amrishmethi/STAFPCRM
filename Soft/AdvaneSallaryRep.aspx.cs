using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Soft_AdvaneSallaryRep : System.Web.UI.Page
{
    private HttpCookie Soft;
    Data data = new Data();
    DataSet dsResult = new DataSet();
    GetData Gd = new GetData();
    Master master = new Master();
    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            Gd.FillUser(drpEmployee);
            GetReport();
        }
    }

    private void GetReport()
    {
        DataSet dss = master.IUD_AdvanceSalary("Select", "0", "", DateTime.Now.ToString("dd/MM/yyyy"), "", "", "", "", "");
        rep.DataSource = dss;
        rep.DataBind();
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            DataSet dss = master.IUD_AdvanceSalary("DELETE", e.CommandArgument.ToString(), "", "", "", "", "", "1", "");
            if (dss.Tables[0].Rows[0]["Result"].ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record DELETE Successfully');window.location ='AdvaneSallaryRep.aspx'", true);
            }
        }
    }
}