using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        //Repeater1.DataSource = dss.Tables[1];
        //Repeater1.DataBind();

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
}