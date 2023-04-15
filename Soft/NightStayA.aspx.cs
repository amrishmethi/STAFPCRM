using Mailjet.Client.Resources;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_NightStayA : System.Web.UI.Page
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
            //Session["AccessRigthsSet"] = master.AccessRights("NightStay.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            Gd.fillDepartment(drpDepartment);
            drpDepartment.SelectedValue = drpDepartment.Items.FindByText("SALES").Value;
            Gd.FillUser(drpEmployee, drpDepartment.SelectedValue);
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    protected void drpEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dss = data.getDataSet("select EMP.CRMUserId,ESD.NSA,ESD.DAL from tbl_EmpMaster EMP inner join tbl_EMPSalaryDetails ESD on ESD.Emp_Id=EMP.EMPID where ESD.Delid=0 and ESD.EMP_Id=" + drpEmployee.SelectedValue);
        if (dss != null)
        {
            hddCrmUserId.Value = dss.Tables[0].Rows[0]["CRMUserId"].ToString();
            txtCharges.Text = (rbNightStay.Checked) ? dss.Tables[0].Rows[0]["NSA"].ToString() : dss.Tables[0].Rows[0]["DAL"].ToString();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("NightStay.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (drpEmployee.SelectedIndex > 0)
        {
            Soft = Request.Cookies["STFP"];
            string _EntryBy = "By CRM (" + Soft["UserName"] + ")";
            string _ChargesType = rbDA.Checked ? "DAL" : "NIGHT";
            string q = "  insert into NightStayCharges(UserId, Charges,   CRMUSERID, ChargesType, AttandanceBy,FID,AttendanceDate)";
            q += " Values( '" + hddCrmUserId.Value + "','" + txtCharges.Text + "','" + drpEmployee.SelectedValue + "','" + _ChargesType + "','" + _EntryBy + "',0,'" + data.ConvertToDateTime(txtFromDate.Text) + "')";
            if (data.executeCommand(q) == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Save Successfully');window.location ='NightStay.aspx'", true);
            }
        }
    }
}