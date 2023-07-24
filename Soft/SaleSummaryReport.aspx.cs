using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Soft_SaleSummaryReport : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("UserWiseParty.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            bindDrp();
            gd.FillPrimaryParty(drpParty);
            gd.FillPrimaryStation(Drpstation);
            gd.FillGroup(drpGrp);
        }
    } 

    private void bindDrp()
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        dv.Sort = "Name";
        DrpEmployee.DataSource = dv.ToTable(true, "Name", "MID");
        DrpEmployee.DataTextField = "Name";
        DrpEmployee.DataValueField = "MID";
        DrpEmployee.DataBind();
        DrpEmployee.Items.Insert(0, new ListItem("Select", "0"));
        dv.Sort = "HeadQtr";
        drpHeadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNO");
        drpHeadQtr.DataTextField = "HeadQtr";
        drpHeadQtr.DataValueField = "HeadQtrNO";
        drpHeadQtr.DataBind();
        drpHeadQtr.Items.Insert(0, new ListItem("Select", "0"));
        dv.Sort = "district";
        drpDistict.DataSource = dv.ToTable(true, "district", "districtNo");
        drpDistict.DataTextField = "district";
        drpDistict.DataValueField = "districtNo";
        drpDistict.DataBind();
        drpDistict.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void fillData()
    { 
        string head = drpHeadQtr.SelectedValue;
        string station = Drpstation.SelectedValue;
        string party = drpParty.SelectedValue;
        string rate = Drprate.SelectedValue;
        string grp = "0";
        foreach (ListItem item in drpGrp.Items)
        {
            if (item.Selected)
            {
                grp += "," + item.Value;
            }
        }

        string district = "0";
        foreach (ListItem item in drpDistict.Items)
        {
            if (item.Selected)
            {
                district += "," + item.Value;
            } 
        } 

        ds = getdata.getSaleSummaryReportST(head, district, drpReport.SelectedValue, station, dpFrom.Text, dpTo.Text, rate, party, grp);

        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];

            decimal totalPrice = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["ordbag"]));
            decimal totalPrice1 = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["CWeight"]));
            decimal totalPrice2 = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Qty"]));
            decimal totalPrice3 = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["amount"]));

            var groupedData = dt.AsEnumerable()
                .GroupBy(row => row.Field<string>("cmsname"))
                .Select(group => new
                {
                    cmscname = group.Key,
                    rows = group.CopyToDataTable(),
                    totalPrice = group.Sum(row => Convert.ToDecimal(row["ordbag"])),
                    totalPrice1 = group.Sum(row => Convert.ToDecimal(row["CWeight"])),
                    totalPrice2 = group.Sum(row => Convert.ToDecimal(row["Qty"])),
                    totalPrice3 = group.Sum(row => Convert.ToDecimal(row["amount"]))

                })
                .ToList();

            DataTable mergedTable = new DataTable();
            mergedTable.Columns.Add("row_num", typeof(string));
            mergedTable.Columns.Add("HeadQtr", typeof(string));
            mergedTable.Columns.Add("District", typeof(string));
            mergedTable.Columns.Add("Station", typeof(string));
            mergedTable.Columns.Add("acname", typeof(string));
            mergedTable.Columns.Add("ordbag", typeof(decimal));
            mergedTable.Columns.Add("CWeight", typeof(decimal));
            mergedTable.Columns.Add("Qty", typeof(decimal));
            mergedTable.Columns.Add("amount", typeof(decimal));

            foreach (var item in groupedData)
            {
                mergedTable.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, item.cmscname + "(" + dpFrom.Text + "-" + dpTo.Text + ")", DBNull.Value, DBNull.Value, DBNull.Value);

                foreach (DataRow row in item.rows.Rows)
                {
                    mergedTable.ImportRow(row);
                }

                DataRow groupFooterRow = mergedTable.NewRow();
                groupFooterRow["acname"] = "Total";
                groupFooterRow["ordbag"] = item.totalPrice;
                groupFooterRow["CWeight"] = item.totalPrice1;
                groupFooterRow["Qty"] = item.totalPrice2;
                groupFooterRow["amount"] = item.totalPrice3;
                mergedTable.Rows.Add(groupFooterRow);
            }

            DataRow footerRow = mergedTable.NewRow();
            footerRow["acname"] = "Grand Total";
            footerRow["ordbag"] = totalPrice;
            footerRow["CWeight"] = totalPrice1;
            footerRow["Qty"] = totalPrice2;
            footerRow["amount"] = totalPrice3;
            mergedTable.Rows.Add(footerRow);

            rep.DataSource = mergedTable;
            rep.DataBind(); 
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }

    protected void drpHeadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        // Bind data to district dropdown list based on selected HeadQtr
        string selectedHeadQtr = drpHeadQtr.SelectedValue;
        dv.RowFilter = "HeadQtrNo = '" + selectedHeadQtr + "'";
        dv.Sort = "district";
        drpDistict.DataSource = dv.ToTable(true, "district", "districtNo");
        drpDistict.DataTextField = "district";
        drpDistict.DataValueField = "districtNo";
        drpDistict.DataBind();
        drpDistict.Items.Insert(0, new ListItem("Select", "0"));
    } 

    protected void DrpEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        // Bind data to district dropdown list based on selected HeadQtr
        string selectedName = DrpEmployee.SelectedValue;
        dv.RowFilter = "Mid = '" + selectedName + "'";
        dv.Sort = "HeadQtr";
        drpHeadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNO");
        drpHeadQtr.DataTextField = "HeadQtr";
        drpHeadQtr.DataValueField = "HeadQtrNO";
        drpHeadQtr.DataBind();
        drpHeadQtr.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void drpDistict_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        // Bind data to district dropdown list based on selected HeadQtr
        string selecteddistrict = drpDistict.SelectedValue;
        dv.RowFilter = "districtNo = '" + selecteddistrict + "'";
        dv.Sort = "Station";
        Drpstation.DataSource = dv.ToTable(true, "Station", "StationNo");
        Drpstation.DataTextField = "Station";
        Drpstation.DataValueField = "StationNo";
        Drpstation.DataBind();
        Drpstation.Items.Insert(0, new ListItem("Select", "0"));
    } 
}