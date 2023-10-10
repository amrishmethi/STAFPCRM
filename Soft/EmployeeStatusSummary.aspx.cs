using Spire.Pdf.Tables;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Soft_EmployeeStatusSummary : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    Data data = new Data();
    string query;
    SqlCommand cmd = new SqlCommand();
    Master getdata = new Master();
    GetData Gd = new GetData();
    private HttpCookie Soft;
    double _TotalAmount = 0;
    double _TotalExp = 0;
    double _TotalCTC = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }
            Soft = Request.Cookies["STFP"];
            dtFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dtTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            fillDepartment(drpDepartment);
            Gd.FillUser(drpUser, drpDepartment.SelectedValue, drpStatus.SelectedValue);
        }
    }
    public void fillDepartment(DropDownList drpDepartment)
    {
        ds = getdata.GetDepartment("Select", "0", "", "", "");
        DataView dvv = ds.Tables[0].DefaultView;
        dvv.RowFilter = "dept_Id in (2,3,8)";
        drpDepartment.DataSource = dvv.ToTable();
        drpDepartment.DataTextField = "Dept_Name";
        drpDepartment.DataValueField = "dept_Id";
        drpDepartment.DataBind();
        drpDepartment.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.FillUser(drpUser, drpDepartment.SelectedValue, drpStatus.SelectedValue);
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        cmd.CommandText = "PROC_EMPLOYEEWORKINGSTATUSSUMMARY";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@DEPT_ID", drpDepartment.SelectedValue);
        cmd.Parameters.AddWithValue("@STATUS", drpStatus.SelectedValue);
        cmd.Parameters.AddWithValue("@EMPID", drpUser.SelectedValue);
        cmd.Parameters.AddWithValue("@DT", data.ConvertToDateTime(dtFrom.Text));
        cmd.Parameters.AddWithValue("@DTTO", data.ConvertToDateTime(dtTo.Text));
        DataSet ds = data.SP_getDataSet(cmd); 
        grdReport.DataSource = ds;
        grdReport.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //Response.Clear();
        //Response.Buffer = true;
        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.Charset = "";
        //string FileName = "HQ_GROUP_PARTY_WISE_SALES_REPORT.xls";
        //StringWriter strwritter = new StringWriter();
        //HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.ContentType = "application/vnd.ms-excel";
        //Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        //grdReport.RenderControl(htmltextwrtter);
        //Response.Write(strwritter.ToString());
        //Response.End();
        
        HtmlForm htmlForm = new HtmlForm();
        string fileName = "attachment; filename=EmployeeStatusSummary.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", fileName);
        Response.ContentType = "application/ms-excel";
        StringWriter strw = new StringWriter();
        HtmlTextWriter htxtw = new HtmlTextWriter(strw);  
        htmlForm.Controls.Add(grdReport);
        this.Controls.Add(htmlForm);
        grdReport.RenderControl(htxtw);
        Response.Write(strw.ToString());
        Response.End();
    }
}