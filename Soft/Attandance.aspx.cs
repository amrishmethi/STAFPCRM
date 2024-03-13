using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_Attandance : System.Web.UI.Page
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

            Session["AccessRigthsSet"] = master.AccessRights("Attandance.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
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
            if (HttpContext.Current.Session["latt"] != null && HttpContext.Current.Session["lngg"] != null)
            {
                lits.Text = "<script type='text/javascript' language='javascript'>$(document).ready(function() {getcityname('" + HttpContext.Current.Session["latt"].ToString()
                    + "', '" + HttpContext.Current.Session["lngg"].ToString() + "');  });</script>";
            }
            else
            {
                lits.Text = "<script type='text/javascript' language='javascript'>$(document).ready(function() { getLocation();  });</script>";
            }
        }
    }

    private void FillRecords()
    {
        ViewState["IsHoliday"] = data.getDataSet("Select Count(*) FROM [STM_Tadkeshwar].[dbo].[tbl_Holiday] where delid=0 and Cast('" + data.ConvertToDateTime(txtDate.Text) + "' as date) between Cast(DateFrom as date) and Cast(Dateto as date)").Tables[0].Rows[0][0].ToString();

        lblDate.Text = txtDate.Text;
        DataSet ds = data.getDataSet("PROC_EMPHIERARCHY '" + drpProjectManager.SelectedValue + "','" + drpDepartment.SelectedValue + "'");
        rep.DataSource = ds;
        rep.DataBind();
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        TextBox txtWorkingTimeFRom = (TextBox)e.Item.FindControl("txtWorkingTimeFRom");
        string _Action = e.CommandName; 
        string _EmpId = e.CommandArgument.ToString();
        string Lat = hddLnL.Value.Split(',')[0].Replace("(", "");
        string Lang = hddLnL.Value.Split(',')[1].Replace(")", "");
        dsResult = master.GetAttandance(_Action, _EmpId, Soft["UserName"], Lat, Lang, data.ConvertToDateTimeNew(txtDate.Text, txtWorkingTimeFRom.Text).ToString());
        FillRecords();
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.FillUser(drpProjectManager, drpDepartment.SelectedValue);
        FillRecords();
    }

    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lnkIN = (LinkButton)e.Item.FindControl("lnkIN");
        LinkButton lnkOut = (LinkButton)e.Item.FindControl("lnkOut");
        LinkButton lnkLeave = (LinkButton)e.Item.FindControl("lnkLeave");
        HiddenField HddEmpId = (HiddenField)e.Item.FindControl("HddEmpId");
        HiddenField HddCrmUserId = (HiddenField)e.Item.FindControl("HddCrmUserId");
        Label lblAttandance = (Label)e.Item.FindControl("lblAttandance");
        TextBox txtWorkingTimeFRom = (TextBox)e.Item.FindControl("txtWorkingTimeFRom");

        txtWorkingTimeFRom.Text = DateTime.Now.ToString("HH:mm");
        DataSet dss = data.getDataSet("Select FORMAT(ATTENDANCEDATEIN,'hh:mm tt')ATTENDANCEDATEIN,FORMAT(ATTENDANCEDATEOUT,'hh:mm tt')ATTENDANCEDATEOUT  ,IsAttendanceOUT  from Attendance where IsDeleted=0 And IIF(" + HddCrmUserId.Value + "=0,EMPID,UserId)=IIF(" + HddCrmUserId.Value + "=0," + HddEmpId.Value + "," + HddCrmUserId.Value + ")  and Cast(AttendanceDateIN as date)=Cast('" + data.YYYYMMDD(txtDate.Text) + "' as date)");

        lnkOut.Visible = ViewState["IsHoliday"].ToString() == "1" ? false : (dss.Tables[0].Rows.Count > 0) ? Convert.ToBoolean(dss.Tables[0].Rows[0]["IsAttendanceOUT"]) ? false : true : false;
        lnkLeave.Visible = ViewState["IsHoliday"].ToString() == "1" ? false : (dss.Tables[0].Rows.Count > 0) ? Convert.ToBoolean(dss.Tables[0].Rows[0]["IsAttendanceOUT"]) ? true : true : false;
        lnkIN.Visible = ViewState["IsHoliday"].ToString() == "1" ? false : (dss.Tables[0].Rows.Count > 0) ? false : true;
        if (dss.Tables[0].Rows.Count > 0)
        {
            string AttandanceIn = "In: " + dss.Tables[0].Rows[0]["ATTENDANCEDATEIN"].ToString();
            string AttandanceOut = Convert.ToBoolean(dss.Tables[0].Rows[0]["IsAttendanceOUT"]) ? "   <br>  Out: " + dss.Tables[0].Rows[0]["ATTENDANCEDATEOUT"].ToString() : "";

            lblAttandance.Text = AttandanceIn + "" + AttandanceOut + "";
        }
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
}