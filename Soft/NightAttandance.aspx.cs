using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_NightAttandance : System.Web.UI.Page
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
            Session["AccessRigthsSet"] = master.AccessRights("NightAttandance.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];

            Gd.fillDepartment(drpDepartment);
            Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue);
            Gd.FillUser(drpProjectManager);
            ViewState["_Date"] = DateTime.Now.ToString("dd/MM/yyyy");
            txtDate.Text = ViewState["_Date"].ToString();
            if (Soft["Type"].ToUpper() != "ADMIN")
            {
                drpProjectManager.SelectedValue = Soft["EMP_ID"];
                drpProjectManager.Enabled = false;
            }
            FillRecords(); 
        }
    }

    private void FillRecords()
    {
        lblDate.Text = txtDate.Text;
        DataSet ds = data.getDataSet("PROC_EMPHIERARCHY '" + drpProjectManager.SelectedValue + "','" + drpDepartment.SelectedValue + "'");
        rep.DataSource = ds;
        rep.DataBind();
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        HiddenField HddID = (HiddenField)e.Item.FindControl("HddID");
        LinkButton lnkIN = (LinkButton)e.Item.FindControl("lnkIN");
        LinkButton lnkOut = (LinkButton)e.Item.FindControl("lnkOut");

        lnkIN.Visible = lnkOut.Visible = false;
        string _Action = HddID.Value == "0" ? "Save" : "Delete";
        string _EmpId = e.CommandArgument.ToString();
        DataSet ds = master.IU_NIGHTATTENDANCE(_Action, HddID.Value, _EmpId, drpDepartment.SelectedValue, data.ConvertToDateTime(txtDate.Text).ToString(), data.ConvertToDateTime(txtDate.Text).ToString(), "");

        FillRecords();
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.FillUser(drpProjectManager, drpDepartment.SelectedValue);
        FillRecords();
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        FillRecords();
    }

    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString() + "";
    }

    protected void drpProjectManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRecords();
    }

    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lnkIN = (LinkButton)e.Item.FindControl("lnkIN");
        LinkButton lnkOut = (LinkButton)e.Item.FindControl("lnkOut");
        HiddenField HddEmpId = (HiddenField)e.Item.FindControl("HddEmpId");
        HiddenField HddCrmUserId = (HiddenField)e.Item.FindControl("HddCrmUserId");
        HiddenField HddID = (HiddenField)e.Item.FindControl("HddID");

        DataSet dss = data.getDataSet("Select * from tbl_NightAttendance where IsDeleted=0 And FK_EmpId =" + HddEmpId.Value + "  and Cast(AttendanceDate as date)=Cast('" + data.YYYYMMDD(txtDate.Text) + "' as date)");

        HddID.Value = (dss.Tables[0].Rows.Count > 0) ? dss.Tables[0].Rows[0][0].ToString() : "0";
        lnkOut.Visible = (dss.Tables[0].Rows.Count > 0) ? true : false;
        lnkIN.Visible = (dss.Tables[0].Rows.Count > 0) ? false : true;
    }
}