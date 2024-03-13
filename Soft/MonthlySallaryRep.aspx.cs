using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_MonthlySallaryRep : System.Web.UI.Page
{
    private HttpCookie Soft;
    Data data = new Data();
    DataSet dsResult = new DataSet();
    GetData Gd = new GetData();
    Master master = new Master();
    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            mnth.Text = DateTime.Now.ToString("MM-yyyy");
            Gd.fillDepartment(drpDepartment);
            Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue);
            Gd.FillUser(drpProjectManager, drpDepartment.SelectedValue, drpStatus.SelectedValue);
            FillMonth();
        }
    }

    private void FillMonth()
    {
        DataSet dsMOnth = data.getDataSet("SELECT RIGHT('0'+(cast(number as nvarchar(30))),2)NUmber, DATENAME(MONTH, '1900-' + CAST(number as varchar(2)) + '-1') monthname FROM master..spt_values WHERE Type = 'P' and number between 1 and 12 ORDER BY Number");
        drpMonth.DataSource = dsMOnth;
        drpMonth.DataTextField = "monthname";
        drpMonth.DataValueField = "number";
        drpMonth.DataBind();
        drpMonth.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue);
        Gd.FillUser(drpProjectManager, drpDepartment.SelectedValue, drpStatus.SelectedValue);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int month = Convert.ToInt32(mnth.Text.Split('-')[0]);
        int year = Convert.ToInt32(mnth.Text.Split('-')[1]);
        string _DD = year + "-" + month + "-01";

        string _FromDate = DateTime.Now.ToString("yyyy") + "-" + drpMonth.SelectedValue + "-01";
        DataSet dss = master.GetSallary(_DD, drpDepartment.SelectedValue, drpDesignation.SelectedValue, drpProjectManager.SelectedValue, drpPf.SelectedValue, drpStatus.SelectedValue);

        DataTable mergedTable = new DataTable();
        mergedTable.Merge(dss.Tables[0]);
        mergedTable.Merge(dss.Tables[1]);

        rep.DataSource = mergedTable;
        rep.DataBind();

        Session["Salary"] = dss.Tables[0];
    }

    protected void btnSalary_Click(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        for (int i = 0; i < rep.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)rep.Items[i].FindControl("chk");
            HiddenField HddSalaryId = (HiddenField)rep.Items[i].FindControl("HddSalaryId");
            if (chk.Checked == true)
            {
                data.executeCommand("Update tbl_EMPSalary SET IsApprove=1, ApprovedBy='" + Soft["UserId"] + "' WHERE Id=" + HddSalaryId.Value);
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Salary Saved Successfully');window.location ='MonthlySallaryRep.aspx'", true);
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            data.executeCommand("Update TBL_EMPSALARY SET ISDELETE=1,ModifyTime=Getdate() WHERE ID= " + e.CommandArgument.ToString() + "");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Delete Successfully');window.location ='MonthlySallaryRep.aspx'", true);
        }
    }

    protected void btnSalarySlip_Click(object sender, EventArgs e)
    {
        string str = "Select STUFF((Select ',' + Cast(ID as nvarchar(50)) from [GETSALARYSLIP]  WHERE  Format(SALARYMONTH,'MM-yyyy')='" + mnth.Text + "'";
        if (drpDepartment.SelectedIndex > 0)
            str += " and Dept_Id=" + drpDepartment.SelectedValue;
        if (drpProjectManager.SelectedIndex > 0)
            str += " and EMP_ID=" + drpProjectManager.SelectedValue;
        if (drpStatus.SelectedIndex > 0)
            str += " and Status='" + drpStatus.SelectedValue + "'";
        if (drpPf.SelectedIndex > 0)
            str += " and Is_PF=" + drpPf.SelectedValue;

        str += " FOR XML PATH('') ), 1, 1, '')";
        DataSet dss = data.getDataSet(str);
        Session["SalaryId"] = dss.Tables[0].Rows[0][0].ToString();
        Response.Write("<script>window.open ('SalarySlip.aspx','_blank');</script>");
    }

    protected void btnPF_Click(object sender, EventArgs e)
    {
        string str = "SELECT PF_ACNO,EMP_NAME, GROSSSALARY,EPF,EPSBASIC,EDLI,EE,EPS,ER,  [NCP DAYS],  [A/C.02 (RS.)],[A/C.21 (RS.)] FROM PFREPORT_VIEW WHERE  Format(SALARYMONTH,'MM-yyyy')='" + mnth.Text + "'";
        if (drpDepartment.SelectedIndex > 0)
            str += " and Dept_Id=" + drpDepartment.SelectedValue;
        if (drpProjectManager.SelectedIndex > 0)
            str += " and EMP_ID=" + drpProjectManager.SelectedValue;
        if (drpStatus.SelectedIndex > 0)
            str += " and Status='" + drpStatus.SelectedValue + "'";
        if (drpPf.SelectedIndex > 0)
            str += " and Is_PF=" + drpPf.SelectedValue;
        DataSet dss = data.getDataSet(str);

        DataGrid grdreport = new DataGrid();
        grdreport.DataSource = dss;
        grdreport.DataBind();

        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "PF REPORT OF " + mnth.Text + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        grdreport.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
        //Response.Write("<script>window.open ('SalarySlip.aspx','_blank');</script>");
    }

    protected void btnPDFPrint_Click(object sender, EventArgs e)
    {
        Session["DateRange"] = " AS ON " + mnth.Text;
        DataTable dataTable = (DataTable)Session["Salary"];


        DataTable dtS = new DataTable();

        dtS.Columns.Add("S NO");
        dtS.Columns.Add("Department Name");
        dtS.Columns.Add("Employee Name");
        dtS.Columns.Add("PF AC / No");
        dtS.Columns.Add("ESIC AC / No");
        dtS.Columns.Add("Net Salary");
        dtS.Columns.Add("No Of Working Days");
        dtS.Columns.Add("OT Days");
        dtS.Columns.Add("Over Time");
        dtS.Columns.Add("Basic Salary");
        dtS.Columns.Add("House Rent Allowance");
        dtS.Columns.Add("Other Allowance");
        dtS.Columns.Add("Gross Salary");
        dtS.Columns.Add("Provident Fund");
        dtS.Columns.Add("ESIC");
        dtS.Columns.Add("TDS");
        dtS.Columns.Add("Salary Payble");

        foreach (DataRow dti in dataTable.Rows)
        {
            if (dti["_URL"].ToString() != "#")
            {
                DataRow drr = dtS.NewRow();
                drr["S NO"] = dtS.Rows.Count + 1;
                drr["Department Name"] = dti["Dept_Name"];
                drr["Employee Name"] = dti["Emp_Name"];
                drr["PF AC / No"] = dti["PF_AcNo"];
                drr["ESIC AC / No"] = dti["ESI_AcNO"];
                drr["Net Salary"] = dti["Net_Salary"];
                drr["No Of Working Days"] = dti["NOOFWORKINGDAY"] + "/" + dti["TotalWorkingDay"];
                drr["OT Days"] = dti["TotalOT"];
                drr["Over Time"] = dti["OverTime"];
                drr["Basic Salary"] = dti["BASIC_SALARYVALUE"];
                drr["House Rent Allowance"] = dti["HRAVALUE"];
                drr["Other Allowance"] = dti["OAVALUE"];
                drr["Gross Salary"] = dti["GrossSalary"];
                drr["Provident Fund"] = dti["PF_EMPLOYEEVALUE"];
                drr["ESIC"] = dti["ESIC_EMPLOYEEVALUE"];
                drr["TDS"] = dti["TDSVALUE"];
                drr["Salary Payble"] = dti["SALARYPAYBLE"];
                dtS.Rows.Add(drr);
            }
        }
        DataRow drr1 = dtS.NewRow();
        drr1["S NO"] = dtS.Rows.Count + 1;
        drr1["Net Salary"] = dataTable.Compute("sum(Net_Salary)", "_URL<>'#'");
        drr1["OT Days"] = dataTable.Compute("sum(TotalOT)", "_URL<>'#'");
        drr1["Over Time"] = dataTable.Compute("sum(OverTime)", "_URL<>'#'");
        drr1["Basic Salary"] = dataTable.Compute("sum(BASIC_SALARYVALUE)", "_URL<>'#'");
        drr1["House Rent Allowance"] = dataTable.Compute("sum(HRAVALUE)", "_URL<>'#'");
        drr1["Other Allowance"] = dataTable.Compute("sum(OAVALUE)", "_URL<>'#'");
        drr1["Gross Salary"] = dataTable.Compute("sum(GrossSalary)", "_URL<>'#'");
        drr1["Provident Fund"] = dataTable.Compute("sum(PF_EMPLOYEEVALUE)", "_URL<>'#'");
        drr1["ESIC"] = dataTable.Compute("sum(ESIC_EMPLOYEEVALUE)", "_URL<>'#'");
        drr1["TDS"] = dataTable.Compute("sum(TDSVALUE)", "_URL<>'#'");
        drr1["Salary Payble"] = dataTable.Compute("sum(SALARYPAYBLE)", "_URL<>'#'");
        dtS.Rows.Add(drr1);


        DataView dv = dtS.DefaultView;
        //dv.Sort = "Party";
        Session["GridData"] = dv.ToTable();
        Session["TermsId"] = "";

        Session["Title"] = "Salary Sheet ";

        Response.Write("<script>window.open ('Print.aspx','_blank');</script>");
    }
}