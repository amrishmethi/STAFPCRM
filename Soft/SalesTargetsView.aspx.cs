using Org.BouncyCastle.Ocsp;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Admin_SalesTargetsView : System.Web.UI.Page
{
    DataTable dtGrp = new DataTable();
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;
    DataTable Dt = new DataTable();
    DataTable Dt1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            gd.fillDepartment(drpDepartment);
            gd.FillGroup1(drpGrp);
            gd.FillPartyCategory(drpCatg);
            mnth.Text = DateTime.Now.ToString("MM-yyyy");


            DataSet dsusr = getdata.getHqtrUserDpt(drpDepartment.SelectedValue);
            ViewState["tbl1"] = dsusr;
            DataView dv = dsusr.Tables[0].DefaultView;
            if (drpStatus.SelectedIndex > 0)
                dv.RowFilter = " Status='" + drpStatus.SelectedValue + "'";
            dv.Sort = "Name";
            drpUser.DataSource = dv.ToTable(true, "Name", "MId");
            drpUser.DataTextField = "Name";
            drpUser.DataValueField = "MId";
            drpUser.DataBind();
            drpUser.Items.Insert(0, new ListItem("Select", "0"));

            dv.Sort = "HeadQtr";
            drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
            drpheadQtr.DataTextField = "HeadQtr";
            drpheadQtr.DataValueField = "HeadQtrNo";
            drpheadQtr.DataBind();
            drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));

            CreateTable();

        }
    }

    public void CreateTable()
    {
        if (ViewState["Target"] == null)
        {
            Dt.Columns.Add("EMP_NAME");
            Dt.Columns.Add("HeadQtr");
            Dt.Columns.Add("Target", typeof(int));
            Dt.Columns.Add("Sale", typeof(int));
            Dt.Columns.Add("Balance", typeof(int));

            ViewState["Target"] = Dt;
        }
    }
    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = (DataSet)ViewState["tbl1"];
        DataView dv = dsusr.Tables[0].DefaultView;
        if (drpStatus.SelectedIndex > 0)
            dv.RowFilter = " Status='" + drpStatus.SelectedValue + "'";
        dv.Sort = "Name";
        drpUser.DataSource = dv.ToTable(true, "Name", "MId");
        drpUser.DataTextField = "Name";
        drpUser.DataValueField = "MId";
        drpUser.DataBind();
        drpUser.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void fillData()
    {
        DataSet ds = getdata.GetSaleTargetReport(drpDepartment.SelectedValue, drpStatus.SelectedValue, drpUser.SelectedValue, drpheadQtr.SelectedValue, mnth.Text, drpCatg.SelectedValue);

        DataView dvv = ds.Tables[0].DefaultView;
        DataTable dtt = dvv.ToTable(true, "EMP_NAME", "HQNAME");


        DataTable dtt2 = (DataTable)ViewState["Target"];
        dtt2.Rows.Clear();
        foreach (DataRow drr in dtt.Rows)
        {
            DataRow dr = dtt2.NewRow();
            dr["EMP_NAME"] = drr["EMP_NAME"];
            dr["HeadQtr"] = drr["HQNAME"];
            int _Target = 0, _Sale = 0;
            foreach (ListItem item in drpGrp.Items)
            {
                if (item.Selected)
                {
                    _Target += Convert.ToInt32(ds.Tables[0].AsEnumerable().Where(x => x["HQNAME"].ToString() == drr["HQNAME"].ToString()).Where(x => x["EMP_NAME"].ToString() == drr["EMP_NAME"].ToString()).Sum(r => r.Field<Int64?>("" + item.ToString().Replace(" ", "_") + "") ?? 0));
                    _Sale += Convert.ToInt32(ds.Tables[1].AsEnumerable().Where(x => x["HEADQTR"].ToString() == drr["HQNAME"].ToString()).Where(x => x["CMSNAME"].ToString() == item.ToString()).Sum(x => x.Field<int>("ORDBAG")));
                }
            }
            dr["Target"] = _Target;
            dr["Sale"] = _Sale;
            dr["Balance"] = _Target - _Sale;
            dtt2.Rows.Add(dr);
        }
        if (dtt2.Rows.Count > 0)
        {
            DataRow dr = dtt2.NewRow();
            dr["EMP_NAME"] = "Total";
            dr["HeadQtr"] = "";
            dr["Target"] = dtt2.Compute("sum(Target)", "");
            dr["Sale"] = dtt2.Compute("sum(Sale)", "");
            dr["Balance"] = dtt2.Compute("sum(Balance)", "");
            dtt2.Rows.Add(dr);
        }
        dtt2.AcceptChanges();
        repDetail.DataSource = dtt2;
        repDetail.DataBind();
    }

    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = (DataSet)ViewState["tbl1"];
        DataView dv = dsusr.Tables[0].DefaultView;
        dv.RowFilter = "Mid=" + drpUser.SelectedValue;
        dv.Sort = "HeadQtr";
        drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNO");
        drpheadQtr.DataTextField = "HeadQtr";
        drpheadQtr.DataValueField = "HeadQtrNO";
        drpheadQtr.DataBind();
        drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillData();
    }


}