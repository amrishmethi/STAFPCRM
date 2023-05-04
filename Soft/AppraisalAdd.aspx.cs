using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Soft_AppraisalAdd : System.Web.UI.Page
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
        }
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRecords();
    }

    private void FillRecords()
    {
        string _Id = (Request.QueryString["ID"] != null) ? Request.QueryString["ID"] : "0";
        DataSet dss = data.getDataSet("APPRAISAL_PROC " + _Id);

        string query = " 0=0";
        if (drpDepartment.SelectedIndex > 0)
            query += " and DEPT_ID=" + drpDepartment.SelectedValue;
        if (drpProjectManager.SelectedIndex > 0)
            query += " and EMP_ID=" + drpProjectManager.SelectedValue;
        if (Request.QueryString["ID"] != null)
            query += " and Id=" + Request.QueryString["ID"].ToString();
        else
            query += " and EMP_STATUS='ACTIVE' And APPRAISAL_STATUS=1";

        DataView dv = dss.Tables[0].DefaultView;
        dv.RowFilter = query;
        rep.DataSource = dv;
        rep.DataBind();

        if (_Id != "0")
            txtDate.Text = dv.ToTable().Rows[0]["AFFECTIVE_DATE"].ToString();
        else
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < rep.Items.Count; i++)
        {
            HiddenField hddDept_Id = (HiddenField)rep.Items[i].FindControl("hddDept_Id");
            HiddenField hddEmp_Id = (HiddenField)rep.Items[i].FindControl("hddEmp_Id");
            TextBox lblPre = (TextBox)rep.Items[i].FindControl("lblPre");
            TextBox lblNext = (TextBox)rep.Items[i].FindControl("lblNext");
            TextBox txtApp = (TextBox)rep.Items[i].FindControl("txtApp");

            string _Id = Request.QueryString["ID"] != null ? Request.QueryString["ID"].ToString() : "0";
            string UserId = Soft["UserId"];
            master.Appraisal(hddEmp_Id.Value, lblPre.Text, txtApp.Text, lblNext.Text, data.ConvertDDMMYYYY(txtDate.Text), _Id, UserId);
        }

        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Save Successfully');window.location ='AppraisalRep.aspx'", true);
    }

    protected void txtApp_TextChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < rep.Items.Count; i++)
        {
            TextBox lblPre = (TextBox)rep.Items[i].FindControl("lblPre");
            TextBox lblNext = (TextBox)rep.Items[i].FindControl("lblNext");
            TextBox txtApp = (TextBox)rep.Items[i].FindControl("txtApp");

            decimal Pre = Convert.ToDecimal(lblPre.Text);
            decimal Appr = Convert.ToDecimal(txtApp.Text);
            decimal Cur = Pre + ((Pre * Appr) / 100);
            lblNext.Text = Cur.ToString("0.00");
        }
    }
}