using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_HQWiseOutstanding : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;
    DataTable dtCustom = new DataTable();
    Workbook workbook1 = new Workbook();
    bool a = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("UserWiseParty.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');

            DataSet dsusr = getdata.getHqtrUserDpt("0");
            ViewState["tbl"] = dsusr.Tables[0];
            DataView dv = dsusr.Tables[0].DefaultView;
            //dv.RowFilter = " Status='Active'";
            dv.Sort = "Name";
            DrpEmployee.DataSource = dv.ToTable(true, "Name", "MId");
            DrpEmployee.DataTextField = "Name";
            DrpEmployee.DataValueField = "MId";
            DrpEmployee.DataBind();
            DrpEmployee.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));

            dv.Sort = "HeadQtr";
            drpHeadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
            drpHeadQtr.DataTextField = "HeadQtr";
            drpHeadQtr.DataValueField = "HeadQtrNo";
            drpHeadQtr.DataBind();
            drpHeadQtr.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));

            dv.Sort = "district";
            drpDistict.DataSource = dv.ToTable(true, "district", "districtNo");
            drpDistict.DataTextField = "district";
            drpDistict.DataValueField = "districtNo";
            drpDistict.DataBind();
            drpDistict.Items.Insert(0, new ListItem("Select", "0"));

            gd.FillPrimaryParty(drpParty);
            gd.FillPrimaryStation(Drpstation);
            gd.FillPartyCategory(drpPartyCategory);
            gd.FillBookType(drpBookType);
            gd.FillAccountGroup(drpAccountGrp);
        }
    }

    protected void drpHeadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataView dv = ((DataTable)ViewState["tbl"]).DefaultView;
        // Bind data to district dropdown list based on selected HeadQtr
        if (drpHeadQtr.SelectedIndex > 0)
        {
            string selectedHeadQtr = drpHeadQtr.SelectedValue;
            dv.RowFilter = "HeadQtrNo = '" + selectedHeadQtr + "'";
        }

        dv.Sort = "district";
        drpDistict.DataSource = dv.ToTable(true, "district", "districtNo");
        drpDistict.DataTextField = "district";
        drpDistict.DataValueField = "districtNo";
        drpDistict.DataBind();
        drpDistict.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void DrpEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataView dv = ((DataTable)ViewState["tbl"]).DefaultView;
        // Bind data to district dropdown list based on selected HeadQtr
        if (DrpEmployee.SelectedIndex > 0)
        {
            string selectedName = DrpEmployee.SelectedValue;
            dv.RowFilter = "MID = '" + selectedName + "'";
        }

        dv.Sort = "HeadQtr";
        drpHeadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
        drpHeadQtr.DataTextField = "HeadQtr";
        drpHeadQtr.DataValueField = "HeadQtrNo";
        drpHeadQtr.DataBind();
        drpHeadQtr.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void drpDistict_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DataView dv = ((DataTable)ViewState["tbl"]).DefaultView;
        // Bind data to district dropdown list based on selected HeadQtr
        if (drpDistict.SelectedIndex > 0)
        {
            string selecteddistrict = drpDistict.SelectedValue;
            dv.RowFilter = "districtNo = '" + selecteddistrict + "'";
        }
        dv.Sort = "Station";
        Drpstation.DataSource = dv.ToTable(true, "Station", "StationNO");
        Drpstation.DataTextField = "Station";
        Drpstation.DataValueField = "StationNO";
        Drpstation.DataBind();
        Drpstation.Items.Insert(0, new ListItem("Select", "0"));
    }





    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("PROC_HEADQTRWISEOUTSTANDING");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@HEADQTR", drpHeadQtr.SelectedValue);
        cmd.Parameters.AddWithValue("@DISTRICTNO", drpDistict.SelectedValue);
        cmd.Parameters.AddWithValue("@STATIONNO", Drpstation.SelectedValue);
        cmd.Parameters.AddWithValue("@ACCODE", drpParty.SelectedValue);
        cmd.Parameters.AddWithValue("@BOOKTYPE", drpBookType.SelectedValue);
        cmd.Parameters.AddWithValue("@PTCMSNO", drpPartyCategory.SelectedValue);
        cmd.Parameters.AddWithValue("@DATEFROM", data.YYYYMMDD(dpFrom.Text));
        cmd.Parameters.AddWithValue("@DATETO", data.YYYYMMDD(dpTo.Text));
        cmd.Parameters.AddWithValue("@ReportType", drpReport.SelectedValue);
        cmd.Parameters.AddWithValue("@ACGrp", drpAccountGrp.SelectedValue);
        DataSet dss = data.getDataSet(cmd);
        if (dss.Tables[0].Rows.Count > 0)
        {

            DataTable dt = dss.Tables[0];

            decimal totalPrice = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["GLAMOUNT"]));
            decimal totalPrice1 = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["GLOSAMT"]));
             
            var groupedData = from row in dt.AsEnumerable()
                              group row by new { acname = row.Field<string>("acname"), ACCREDITDY = row.Field<string>("ACCREDITDY"), Station = row.Field<string>("Station") }
                          into grp
                              select new
                              {
                                  ACCREDITDY = grp.Key.ACCREDITDY,
                                  acname = grp.Key.acname,
                                  Station = grp.Key.Station,
                                  rows = grp.CopyToDataTable(),
                                  totalPrice = grp.Sum(row => Convert.ToDecimal(row["GLAMOUNT"])),
                                  totalPrice1 = grp.Sum(row => Convert.ToDecimal(row["GLOSAMT"]))
                              };
            groupedData.ToList();

            DataTable mergedTable = new DataTable();
            mergedTable.Columns.Add("HEADQTRNO", typeof(string));
            mergedTable.Columns.Add("District", typeof(string));
            mergedTable.Columns.Add("Station", typeof(string));
            mergedTable.Columns.Add("acname", typeof(string));
            mergedTable.Columns.Add("VOCDATE", typeof(string));
            mergedTable.Columns.Add("VOCID", typeof(string));
            mergedTable.Columns.Add("GLAMOUNT", typeof(decimal));
            mergedTable.Columns.Add("GLOSAMT", typeof(decimal));
            mergedTable.Columns.Add("BILLAMT", typeof(decimal));
            mergedTable.Columns.Add("DUEAMT", typeof(decimal));
            mergedTable.Columns.Add("DUEDATE", typeof(string));
            mergedTable.Columns.Add("DUEDAY", typeof(string));
            mergedTable.Columns.Add("COLOR", typeof(string));
            mergedTable.Columns.Add("MM", typeof(string));


            foreach (var item in groupedData)
            {
                //mergedTable.Rows.Add(DBNull.Value, DBNull.Value, item.Station, item.acname + " As On " + dpFrom.Text + " (" + item.ACCREDITDY + ")", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);

                foreach (DataRow row in item.rows.Rows)
                {
                    mergedTable.ImportRow(row);
                }

                DataRow groupFooterRow = mergedTable.NewRow();
                groupFooterRow["Station"] = item.Station;
                groupFooterRow["acname"] = item.acname + " As On " + dpFrom.Text + " (" + item.ACCREDITDY + ")";
                groupFooterRow["VOCID"] = "Total";
                groupFooterRow["BILLAMT"] = item.totalPrice > 0 ? item.totalPrice.ToString("0.00") : (item.totalPrice * -1).ToString("0.00");
                groupFooterRow["DUEAMT"] = item.totalPrice1 > 0 ? item.totalPrice1.ToString("0.00") : (item.totalPrice1 * -1).ToString("0.00");
                groupFooterRow["MM"] = item.totalPrice1 > 0 ? "CR" : "DR";
                mergedTable.Rows.Add(groupFooterRow);
            }

            DataRow footerRow = mergedTable.NewRow(); 
            footerRow["VOCID"] = "Grand Total";
            footerRow["BILLAMT"] = totalPrice > 0 ? totalPrice.ToString("0.00") : (totalPrice * -1).ToString("0.00");
            footerRow["DUEAMT"] = totalPrice1 > 0 ? totalPrice1.ToString("0.00") : (totalPrice1 * -1).ToString("0.00");
            footerRow["MM"] = totalPrice1 > 0 ? "CR" : "DR";
            mergedTable.Rows.Add(footerRow);

            DataView dvv = mergedTable.DefaultView;
            dvv.Sort = "Station,acname";
            rep.DataSource = dvv.ToTable();
            rep.DataBind();
        }
    }
}