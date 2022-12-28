using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            Gd.fillDepartment(drpDepartment);
            Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue);
            Gd.FillUser(drpProjectManager);

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
        //string query = "select * from [EMP_VIEW] Where 0=0";
        //if (drpDepartment.SelectedIndex > 0)
        //    query += " and DEPT_ID=" + drpDepartment.SelectedValue;
        //if (drpDesignation.SelectedIndex > 0)
        //    query += " and DESIG_ID='" + drpDesignation.SelectedValue + "'";
        //if (drpProjectManager.SelectedIndex > 0)
        //    query += " and Rep_Manager='" + drpProjectManager.SelectedValue + "'";

        //dsResult = data.getDataSet(query);
        //dtEmp= dsResult.Tables[0];
        //DataTable dttEMp = new DataTable();
        //if (drpProjectManager.SelectedIndex > 0)
        //{
        //    foreach (DataRow drr in dtEmp.Rows)
        //    {
        //        DataSet dsEmp = data.getDataSet("select * from EMP_VIEW where Rep_Manager=" + drr["EMPID"]);
        //        dttEMp.Merge(dsEmp.Tables[0]);
        //    }
        //}
        //dtEmp.Merge(dttEMp); 
        //rep.DataSource = dtEmp;
        //rep.DataBind();


        DataSet ds = data.getDataSet("PROC_EMPHIERARCHY '" + drpProjectManager.SelectedValue + "','" + drpDepartment.SelectedValue + "'");
        rep.DataSource = ds;
        rep.DataBind();
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string _Action = e.CommandName;
        string _EmpId = e.CommandArgument.ToString();
        string Lat = hddLnL.Value.Split(',')[0].Replace("(", "");
        string Lang = hddLnL.Value.Split(',')[1].Replace(")", "");
        dsResult = master.GetAttandance(_Action, _EmpId, Soft["UserName"], Lat, Lang);
        FillRecords();
        //Response.Redirect("Attandance.aspx");
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
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

        DataSet dss = data.getDataSet("Select FORMAT(ATTENDANCEDATEIN,'hh:mm tt')ATTENDANCEDATEIN,FORMAT(ATTENDANCEDATEOUT,'hh:mm tt')ATTENDANCEDATEOUT  ,IsAttendanceOUT  from Attendance where IsDeleted=0 And IIF(" + HddCrmUserId.Value + "=0,EMPID,UserId)=IIF(" + HddCrmUserId.Value + "=0," + HddEmpId.Value + "," + HddCrmUserId.Value + ")  and Cast(AttendanceDateIN as date)=Cast(getdate() as date)");

        lnkOut.Visible = (dss.Tables[0].Rows.Count > 0) ? Convert.ToBoolean(dss.Tables[0].Rows[0]["IsAttendanceOUT"]) ? false : true : false;
        lnkLeave.Visible = (dss.Tables[0].Rows.Count > 0) ? Convert.ToBoolean(dss.Tables[0].Rows[0]["IsAttendanceOUT"]) ? false : true : false;
        lnkIN.Visible = (dss.Tables[0].Rows.Count > 0) ? false : true;
        if (dss.Tables[0].Rows.Count > 0)
        {
            string AttandanceIn = "In: " + dss.Tables[0].Rows[0]["ATTENDANCEDATEIN"].ToString();
            string AttandanceOut = Convert.ToBoolean(dss.Tables[0].Rows[0]["IsAttendanceOUT"]) ? "   <br>  Out: " + dss.Tables[0].Rows[0]["ATTENDANCEDATEOUT"].ToString() : "";

            lblAttandance.Text = AttandanceIn + "" + AttandanceOut + "";
        }
    }
}