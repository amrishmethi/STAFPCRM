using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_SecondarySalesTargetReport : System.Web.UI.Page
{
    Master getdata = new Master();
    int SNO;
    Data data = new Data();
    DataTable dtRecord = new DataTable();
    Master master = new Master();
    DataSet dsResult = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillItemGroup();
            FillEmployee();
            FillRecords();
        }
    }

    private void FillItemGroup()
    {
        DataSet dsusr = data.getDataSet("usp_API_ITEMGROUP 930185018");
        drpItemGrup.DataSource = dsusr;
        drpItemGrup.DataTextField = "CmsName";
        drpItemGrup.DataValueField = "CmsName";
        drpItemGrup.DataBind();
        drpItemGrup.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillEmployee()
    {
        DataSet dsusr = getdata.getHqtrUser();
        drpEmployee.DataSource = dsusr.Tables[0].DefaultView.ToTable(true, "Name", "Mid");
        drpEmployee.DataTextField = "Name";
        drpEmployee.DataValueField = "Mid";
        drpEmployee.DataBind();
        drpEmployee.Items.Insert(0, new ListItem("Select", "0"));
    }

    private void FillRecords()
    {
        string query = "select * from [GETSECONDARYSALESTARGET_VIEW] Where 0=0";
        if (drpEmployee.SelectedIndex > 0)
            query += " and EMPID=" + drpEmployee.SelectedValue;
        if (drpItemGrup.SelectedIndex > 0)
            query += " and ITEMGROUP='" + drpItemGrup.SelectedValue + "'";
        dsResult = data.getDataSet(query);
        rep.DataSource = dsResult;
        rep.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillRecords();
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            data.executeCommand("Update TBL_SECONDARYSALESTARGETDETAILS Set Delid=1 where DETAILID=" + e.CommandArgument.ToString());
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Delete Successfully');window.location ='SecondarySalesTargetReport.aspx'", true);
        }
    }
}