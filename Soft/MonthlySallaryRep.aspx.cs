using System;
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
            Gd.FillUser(drpProjectManager);
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
        Gd.FillUser(drpProjectManager, drpDepartment.SelectedValue);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int month = Convert.ToInt32(mnth.Text.Split('-')[0]);
        int year = Convert.ToInt32(mnth.Text.Split('-')[1]);
        string _DD = year + "-" + month + "-01";


        string _FromDate = DateTime.Now.ToString("yyyy") + "-" + drpMonth.SelectedValue + "-01";
        DataSet dss = master.GetSallary(_DD, drpDepartment.SelectedValue, drpDesignation.SelectedValue, drpProjectManager.SelectedValue, drpPf.SelectedValue, drpStatus.SelectedValue);
        rep.DataSource = dss.Tables[0];
        rep.DataBind();

        Repeater1.DataSource = dss.Tables[1];
        Repeater1.DataBind(); 
    }
}