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

            DataSet dss = data.getDataSet("SELECT * from SaleTargetGroupList_View  ");
            ViewState["Columns"] = dss;

            createTable();

        }
    }

    public void createTable()
    {
        dtGrp.Columns.Add("sText");

        DataSet dss = (DataSet)ViewState["Columns"];

        DataView dvv = dss.Tables[0].DefaultView;
        dvv.RowFilter = "Display=1";
        dvv.Sort = "SS";


        foreach (DataRow drr in dvv.ToTable().Rows)
        {
            DataRow dr = dtGrp.NewRow();
            dr["sText"] = drr["SS"];
            dtGrp.Rows.Add(dr);
        }
        repHead.DataSource = dtGrp;
        repHead.DataBind();

        repHead1.DataSource = dtGrp;
        repHead1.DataBind();

        Dt1.Columns.Add("Grpp");

        Dt.Columns.Add("HeadQtr", typeof(string));
        Dt.Columns.Add("Employee", typeof(string));
        for (int k = 0; k < dtGrp.Rows.Count; k++)
        {
            string ff = dtGrp.Rows[k]["sText"].ToString();
            Dt.Columns.Add(ff + "_T", typeof(string));
            Dt.Columns.Add(ff + "_S", typeof(string));
            Dt.Columns.Add(ff + "_B", typeof(string));

            DataRow drrrr = Dt1.NewRow();
            drrrr["Grpp"] = ff + "_T";
            Dt1.Rows.Add(drrrr);
            DataRow drrrr1 = Dt1.NewRow();
            drrrr1["Grpp"] = ff + "_S";
            Dt1.Rows.Add(drrrr1);
            DataRow drrrr2 = Dt1.NewRow();
            drrrr2["Grpp"] = ff + "_B";
            Dt1.Rows.Add(drrrr2); 
        } 

        ViewState["dtMain"] = Dt;
        ViewState["dtColumns"] = Dt1;
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
        DataSet ds = getdata.GetSaleTargetReport(drpDepartment.SelectedValue, drpStatus.SelectedValue, drpUser.SelectedValue, drpheadQtr.SelectedValue, mnth.Text);
        ViewState["Saletarget"] = ds;
        repDetail.DataSource = ds.Tables[0];
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

    protected void repDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater repd = (Repeater)e.Item.FindControl("repDetail1");
            DataSet dss = (DataSet)ViewState["Columns"];

            HiddenField HddHeadQtrNo = (HiddenField)e.Item.FindControl("HddHeadQtrNo");
            DataSet dsdata = ((DataSet)ViewState["Saletarget"]);
            DataTable dtTarget = dsdata.Tables[0];
            DataTable dtSale = dsdata.Tables[1];

            DataView dvTarget = dtTarget.DefaultView;
            dvTarget.RowFilter = "HEADQTR='" + HddHeadQtrNo.Value + "'";
            DataTable dtTarget1 = dvTarget.ToTable();

            DataView dvSale = dtSale.DefaultView;
            dvSale.RowFilter = "HEADQTRNO='" + HddHeadQtrNo.Value + "'";
            DataTable dtSale1 = dvSale.ToTable();

            DataView dvvv = dss.Tables[0].DefaultView;
            dvvv.Sort = "NAME";
            DataTable dttNew = dvvv.ToTable();
            int _Sale = 0, _Target = 0;
            foreach (DataRow drr in dttNew.Rows)
            {
                if (drr["EntryType"].ToString() == "Target")
                {
                    _Target = Convert.ToInt32(dtTarget1.Rows[0][drr["Name"].ToString()]);
                    drr["Qty"] = _Target;
                }
                if (drr["EntryType"].ToString() == "Sale")
                {  
                    _Sale = Convert.ToInt32(dtSale1.AsEnumerable()
                        .Where(myRow => myRow.Field<string>("CMSNAME") == drr["Name"].ToString().Replace('_', ' '))
                        .Sum(myRow => myRow.Field<Int32?>("ORDBAG")));
                    drr["Qty"] = _Sale;
                }
                if (drr["EntryType"].ToString() == "Bal")
                    drr["Qty"] = _Target - _Sale;
            } 

            dttNew.AcceptChanges();
            repd.DataSource = dttNew;
            repd.DataBind();
        }
    }
}