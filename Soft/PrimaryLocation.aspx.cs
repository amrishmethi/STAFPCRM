using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_PrimaryLocation : System.Web.UI.Page
{
    Master master = new Master();
    private HttpCookie Soft;
    Data data = new Data();
    DataSet dsResult = new DataSet();
    GetData Gd = new GetData();
    DataTable dtEmp = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            Session["AccessRigthsSet"] = master.AccessRights("PrimaryLocation.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            Gd.fillDepartment(drpDepartment);
            Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue);
            Gd.FillUser(drpProjectManager); 
            FillRecords();

        }
    }


    private void FillRecords()
    {
        string query = "Select * from PrimaryStation_VIEW WHERE 0=0";
        if (drpDepartment.SelectedIndex > 0)
            query += " and DEPT_ID=" + drpDepartment.SelectedValue;
        if (drpProjectManager.SelectedIndex > 0)
            query += " and EMPID=" + drpProjectManager.SelectedValue;
        if (drpStatus.SelectedIndex > 0)
            query += " and Status='" + drpStatus.SelectedValue + "'";

        DataSet ds = data.getDataSet(query);
        rep.DataSource = ds;
        rep.DataBind();
    }
    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRecords();
    }
     

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string _ID = e.CommandArgument.ToString(); 
        dsResult = data.getDataSet("update UserPrimaryStationDetail SET IsEditable= IIF(IsEditable=0,1,0) WHERE ID='" + _ID + "'");
        FillRecords();
    }

    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString() + "";
    }
}