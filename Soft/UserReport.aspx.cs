using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Admin_UserReport : System.Web.UI.Page
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
            Session["AccessRigthsSet"] = getdata.AccessRights("UserReport.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            //Gd.FillUser(drpUser);
            Gd.fillDepartment(drpDept);
            Gd.fillDesignation(drpDesg, "0");
            Gd.FillUser(drpUser, drpDept.SelectedValue, drpStatus.SelectedValue, drpDesg.SelectedValue);
            //fillData();
        }
    }


    public void fillData()
    {
        ds = getdata.getUserReport(drpUser.SelectedValue, txtMobile.Text.Trim(), drpDept.SelectedValue, drpDesg.SelectedValue, drpStatus.SelectedValue);

        rep.DataSource = ds.Tables[0];
        rep.DataBind();
    }



    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString() + "," + tbl1.Rows[0]["AssignStatus"].ToString() + "," + tbl1.Rows[0]["LoginStatus"].ToString();
    }


    protected void drpDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillData();
        Gd.fillDesignation(drpDesg, drpDept.SelectedValue);
        Gd.FillUser(drpUser, drpDept.SelectedValue, drpStatus.SelectedValue, drpDesg.SelectedValue);

    }

    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.FillUser(drpUser, drpDept.SelectedValue, drpStatus.SelectedValue, drpDesg.SelectedValue);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }

    protected void drpDesg_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.FillUser(drpUser, drpDept.SelectedValue, drpStatus.SelectedValue, drpDesg.SelectedValue);
    }
}