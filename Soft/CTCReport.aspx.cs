using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Ocsp;
using Spire.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_CTCReport : System.Web.UI.Page
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
            mnth.Text = DateTime.Now.ToString("MM-yyyy");

            fillDepartment(drpDepartment);
            Gd.FillUser(drpUser, drpDepartment.SelectedValue, drpStatus.SelectedValue);
            Gd.FillGroup(drpGrp);
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int month = Convert.ToInt32(mnth.Text.Split('-')[0]);
        int year = Convert.ToInt32(mnth.Text.Split('-')[1]);
        string _DD = year + "-" + month + "-01";

        string grp1 = "0";
        foreach (ListItem item in drpGrp.Items)
        {
            if (item.Selected)
            {
                grp1 += "," + item.Value;
            }
        }
        cmd.CommandText = "PROC_CTCREPORT";
        cmd.CommandType = CommandType.StoredProcedure; 
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@DEPT_ID", drpDepartment.SelectedValue);
        cmd.Parameters.AddWithValue("@STATUS", drpStatus.SelectedValue);
        cmd.Parameters.AddWithValue("@EMPID", drpUser.SelectedValue);
        cmd.Parameters.AddWithValue("@DT", mnth.Text);
        cmd.Parameters.AddWithValue("@GROUP", grp1);
        cmd.Parameters.AddWithValue("@DT1", _DD);
        DataSet ds = data.SP_getDataSet(cmd);

        if (ds.Tables[2].Rows.Count > 0)
        {
            DataTable dt = ds.Tables[2];
            DataView dvv = dt.DefaultView;
            DataTable dtN = dvv.ToTable(true, "HEADQTR", "SALE", "DUEAMT", "Emp_Name", "SALEQTY", "PENDINGQTY");

            decimal Target_Visit = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Target_Visit"]));
            decimal Total_Visit = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Total_Visit"]));
            decimal Productive_Visit = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Productive_Visit"]));
            decimal Sales_Amount = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Sales_Amount"])); 
            decimal Salary = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Salary"]));
            decimal Sale = dtN.AsEnumerable().Where(r => r.Field<string>("Emp_Name") == drpUser.SelectedItem.Text).Sum(row => Convert.ToDecimal(row["SALE"]));
            decimal Expenses = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Expenses"])); 
            decimal Total_Expenses = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Total_Expenses"]));
            decimal Create_Dealer = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Create_Dealer"]));
            decimal Client_MEET = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Client_MEET"]));
            decimal DUEAMT = Math.Abs(dtN.AsEnumerable().Where(r => r.Field<string>("Emp_Name") == drpUser.SelectedItem.Text).Sum(row => Convert.ToDecimal(row["DUEAMT"])));
            decimal SALEQTY = Math.Abs(dtN.AsEnumerable().Where(r => r.Field<string>("Emp_Name") == drpUser.SelectedItem.Text).Sum(row => Convert.ToInt32(row["SALEQTY"]))); 
            decimal PENDINGQTY = Math.Abs(dtN.AsEnumerable().Where(r => r.Field<string>("Emp_Name") == drpUser.SelectedItem.Text).Sum(row => Convert.ToInt32(row["PENDINGQTY"])));

            var groupedData = from row in dt.AsEnumerable()
                              group row by new { Emp_Name = row.Field<string>("Emp_Name"), Rep_Manager = row.Field<string>("Rep_Manager") }
                          into grp
                              select new
                              {
                                  Emp_Name = grp.Key.Emp_Name,
                                  Rep_Manager = grp.Key.Rep_Manager,
                                  rows = grp.CopyToDataTable(),
                                  Target_Visit = grp.Sum(row => Convert.ToDecimal(row["Target_Visit"])),
                                  Total_Visit = grp.Sum(row => Convert.ToDecimal(row["Total_Visit"])),
                                  Productive_Visit = grp.Sum(row => Convert.ToDecimal(row["Productive_Visit"])),
                                  Sales_Amount = grp.Sum(row => Convert.ToDecimal(row["Sales_Amount"])),
                                  Sale = grp.Sum(row => Convert.ToDecimal(row["Sale"])),
                                  Salary = grp.Sum(row => Convert.ToDecimal(row["Salary"])),
                                  Expenses = grp.Sum(row => Convert.ToDecimal(row["Expenses"])),
                                  CTC = grp.Sum(row => Convert.ToDecimal(row["CTC"])),
                                  Total_Expenses = grp.Sum(row => Convert.ToDecimal(row["Total_Expenses"])),
                                  Create_Dealer = grp.Sum(row => Convert.ToDecimal(row["Create_Dealer"])),
                                  Client_MEET = grp.Sum(row => Convert.ToDecimal(row["Client_MEET"])),
                                  DUEAMT = Math.Abs(grp.Sum(row => Convert.ToDecimal(row["DUEAMT"]))),
                                  PENDINGQTY = Math.Abs(grp.Sum(row => Convert.ToInt32(row["PENDINGQTY"]))),
                                  SALEQTY = Math.Abs(grp.Sum(row => Convert.ToInt32(row["SALEQTY"])))
                              };
            groupedData.ToList();

            DataTable mergedTable = new DataTable();
            mergedTable.Columns.Add("Emp_Name", typeof(string));
            mergedTable.Columns.Add("Month", typeof(string));
            mergedTable.Columns.Add("Target_Visit", typeof(decimal));
            mergedTable.Columns.Add("Total_Visit", typeof(decimal));
            mergedTable.Columns.Add("Productive_Visit", typeof(decimal));
            mergedTable.Columns.Add("Sales_Amount", typeof(decimal));
            mergedTable.Columns.Add("Sale", typeof(decimal));
            mergedTable.Columns.Add("Salary", typeof(decimal));
            mergedTable.Columns.Add("Expenses", typeof(decimal));
            mergedTable.Columns.Add("CTC", typeof(decimal));
            mergedTable.Columns.Add("Create_Dealer", typeof(decimal));
            mergedTable.Columns.Add("TOTAL_EXPENSES", typeof(decimal));
            mergedTable.Columns.Add("Client_MEET", typeof(decimal));
            mergedTable.Columns.Add("DUEAMT", typeof(decimal));
            mergedTable.Columns.Add("SALEQTY", typeof(Int32));
            mergedTable.Columns.Add("PENDINGQTY", typeof(Int32));
            mergedTable.Columns.Add("Rep_Manager", typeof(string));


            foreach (var item in groupedData)
            { 
                foreach (DataRow row in item.rows.Rows)
                {
                    mergedTable.ImportRow(row);
                } 
            }

            DataRow footerRow = mergedTable.NewRow();
            footerRow["Emp_Name"] = "Total";
            footerRow["Target_Visit"] = Target_Visit;
            footerRow["Total_Visit"] = Total_Visit;
            footerRow["Productive_Visit"] = Productive_Visit;
            footerRow["Sales_Amount"] = Sales_Amount;
            footerRow["Sale"] = Sale;
            footerRow["Salary"] = Salary;
            footerRow["Expenses"] = Expenses;
            footerRow["CTC"] = (Sale == 0 ? "0" : ((Total_Expenses * 100) / Sale).ToString("0.00"));
            footerRow["TOTAL_EXPENSES"] = Total_Expenses;
            footerRow["Create_Dealer"] = Create_Dealer;
            footerRow["Client_MEET"] = Client_MEET;
            footerRow["DUEAMT"] = Math.Abs(DUEAMT);
            footerRow["SALEQTY"] = Math.Abs(SALEQTY);
            footerRow["PENDINGQTY"] = Math.Abs(PENDINGQTY);
            mergedTable.Rows.Add(footerRow);


            rep.DataSource = mergedTable;
            rep.DataBind();
        }
    }


    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.FillUser(drpUser, drpDepartment.SelectedValue, drpStatus.SelectedValue);
    }

}