using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AttendanceReport : System.Web.UI.Page
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

            Session["AccessRigthsSet"] = getdata.AccessRights("AttendanceReport.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            txtDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            Gd.FillUser(drpUser);
            Gd.fillDepartment(drpDept);
            fillData();
        }
    }

    public void fillData()
    {
        string str = "1=1";
        ds = getdata.getAttendanceList(drpUser.SelectedValue, drpDept.SelectedValue, txtDate.Text.Trim(), txtDateTo.Text.Trim());
        DataView dv = ds.Tables[0].DefaultView;
        if (drpIsAttend.SelectedValue == "0") { str += " and  DateIN is null"; }
        else if (drpIsAttend.SelectedValue == "1") { str += " and DateIN <> ''"; }
        if (drpStatus.SelectedValue == "Active") { str += " and Status = 'Active'"; }
        else if (drpStatus.SelectedValue == "Non-Active") { str += " and Status = 'Non-Active'"; }
        dv.RowFilter = str;
        rep.DataSource = dv.ToTable();
        rep.DataBind();
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }

    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }


    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.FillUser(drpUser, drpDept.SelectedValue, drpStatus.SelectedValue);
        //fillData();
    }
}