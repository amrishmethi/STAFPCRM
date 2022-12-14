using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Admin_ClientMeet : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    Data data = new Data();
    GetData Gd = new GetData();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];
            Session["AccessRigthsSet"] = getdata.AccessRights("ClientMeet.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            
            Gd.FillUser(drpEmp);
            Gd.fillDepartment(drpDept);

            txtDateFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            txtDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');

            bindDrp(true,true);
            fillData();
        }
    }

    private void bindDrp(bool isuser,bool ishqtr)
    {
        DataSet dsusr = getdata.getHqtrUser();
        DataView dv = dsusr.Tables[0].DefaultView;
        if (isuser)
        {
            if (drpHqtr.SelectedIndex > 0)
                dv.RowFilter = "HeadQtr='" + drpHqtr.SelectedItem.Text + "'";
            dv.Sort = "Name";
            drpEmp.DataSource = dv.ToTable(true, "Name", "MId");
            drpEmp.DataTextField = "Name";
            drpEmp.DataValueField = "MId";
            drpEmp.DataBind();
            drpEmp.Items.Insert(0, new ListItem("Select", "0"));
        }
        if (ishqtr) { 
        if (drpEmp.SelectedIndex > 0)
            dv.RowFilter = "Name='" + drpEmp.SelectedItem.Text + "'";
        dv.Sort = "HeadQtr";
        drpHqtr.DataSource = dv.ToTable(true, "HeadQtr");
        drpHqtr.DataTextField = "HeadQtr";
        drpHqtr.DataValueField = "HeadQtr";
        drpHqtr.DataBind();
        drpHqtr.Items.Insert(0, new ListItem("Select", "0"));
    }
    }

    public void fillData()
    {
        ds = getdata.getClientMeet(drpEmp.SelectedValue, drpHqtr.SelectedValue, drpType.SelectedValue, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), drpDept.SelectedValue);
        DataView dv = ds.Tables[0].DefaultView;
        if (drpIsMeet.SelectedValue == "0") { dv.RowFilter = " AddedDate is null"; }
        else if (drpIsMeet.SelectedValue == "1") { dv.RowFilter = "AddedDate <> ''"; }

        rep.DataSource = dv.ToTable();
        rep.DataBind();
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    fillData();
    //}

    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }

    protected void drpEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillData();
        
        DropDownList ddl = sender as DropDownList;
        if (ddl == drpEmp)
        {
            bindDrp(false,true);
        }
        if (ddl == drpHqtr)
        {
            bindDrp(true,false);
        }

    }
}