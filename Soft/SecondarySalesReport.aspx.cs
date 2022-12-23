using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SecondarySalesReport : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData Gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];
            Session["AccessRigthsSet"] = getdata.AccessRights("SecondarySalesReport.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            Gd.FillUser(drpUser);
            Gd.FillPrimaryParty(drpParty);
            Gd.FillPrimaryStation(drpStation);
            Gd.fillDepartment(drpDept);
            fillData();
        }
    }

    public void fillData()
    {
        ds = getdata.getSeconarySalesDetails(drpUser.SelectedValue, drpParty.SelectedValue, drpStation.SelectedItem.Text, dpFrom.Text.Trim(), dpTo.Text.Trim(),drpDept.SelectedValue);
        DataView dv = ds.Tables[0].DefaultView;
        if (drpIsCheck.SelectedValue == "0") { dv.RowFilter = " AddedDate is null"; }
        else if (drpIsCheck.SelectedValue == "1") { dv.RowFilter = " AddedDate is not null"; }
        rep.DataSource = dv.ToTable();
        rep.DataBind();
    }




    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }

    protected void drpIsCheck_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillData();
    }
}