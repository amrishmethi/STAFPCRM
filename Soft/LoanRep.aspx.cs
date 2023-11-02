using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

public partial class Soft_LoanRep : System.Web.UI.Page
{
    private HttpCookie Soft;
    Data data = new Data();
    DataSet dsResult = new DataSet();
    GetData Gd = new GetData();
    Master Master = new Master();
    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            Gd.FillLOanUser(drpEmployee);
            GetReport();
        }
    }

    private void GetReport()
    {
        DataSet ds = Master.IUD_Loan("SELECT", "", "", "", "", "", "", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
        rep.DataSource = ds;
        rep.DataBind();
    }
     

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {            
            DataSet dss = Master.IUD_Loan("DELETE", e.CommandArgument.ToString(), "", "", "", "", "", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
            if (dss.Tables[0].Rows[0]["Result"].ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record DELETE Successfully');window.location ='LeaveDeductRep.aspx'", true);
            }
        }
    }
}