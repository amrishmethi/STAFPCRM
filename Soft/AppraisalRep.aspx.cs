using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_AppraisalRep : System.Web.UI.Page
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
            Gd.FillUser(drpProjectManager);
            FillRecords();
            //if (Soft["Type"].ToUpper() != "ADMIN")
            //{
            //    drpProjectManager.SelectedValue = Soft["EMP_ID"];
            //    drpProjectManager.Enabled = false;
            //} 
        }
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRecords();
    }

    private void FillRecords()
    {
        DataSet dss = data.getDataSet("APPRAISAL_PROC");

        string query = " 0=0";
        if (drpDepartment.SelectedIndex > 0)
            query += " and DEPT_ID=" + drpDepartment.SelectedValue;
        if (drpProjectManager.SelectedIndex > 0)
            query += " and EMP_ID=" + drpProjectManager.SelectedValue;
        if (drpAppraisalStatus.SelectedIndex > 0)
            query += " and APPRAISAL_STATUS=" + drpAppraisalStatus.SelectedValue;
        if (drpStatus.SelectedIndex > 0)
            query += " and EMP_STATUS='" + drpStatus.SelectedValue + "'";

        DataView dv = dss.Tables[0].DefaultView;
        dv.RowFilter = query;
        rep.DataSource = dv;
        rep.DataBind();

    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            Soft = Request.Cookies["STFP"];
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hddEMpId = (e.Item.FindControl("hddEMpId") as HiddenField);

                master.AppraisalD(hddEMpId.Value, e.CommandArgument.ToString(), Soft["UserId"]);
                FillRecords();
            }
        }
    }
}