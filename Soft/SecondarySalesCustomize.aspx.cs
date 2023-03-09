using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SecondarySalesCustomize : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData Gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];
            Session["AccessRigthsSet"] = getdata.AccessRights("SecondarySalesCustomize.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            
            fillData();
        }
    }

    public void fillData()
    {
        string str = "1=1";
        ds = getdata.getUserDetails("0", "2");
        DataView dv = ds.Tables[0].DefaultView;
        if (drpStatus.SelectedValue == "Active") { str += " and Status = 'Active'"; }
        else if (drpStatus.SelectedValue == "Non-Active") { str += " and Status = 'Non-Active'"; }
        dv.RowFilter = str;
        rep.DataSource = dv.ToTable();
        rep.DataBind();
    }

    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }

    protected void drpIsCheck_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillData();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        CheckBox chk;
        HiddenField hddEmpID;
        string OrderId = "";
        for (int i = 0; i < rep.Items.Count; i++)
        {
            chk = (CheckBox)rep.Items[i].FindControl("chk");
            hddEmpID = (HiddenField)rep.Items[i].FindControl("hddEmpID");
            if (chk.Checked == true)
            {
                if (OrderId == "")
                    OrderId = hddEmpID.Value;
                else
                    OrderId += "," + hddEmpID.Value;
            }
        }
        
        GenratePrint(OrderId, ((Button)sender).ID == "btnPrintSummary" ? true:false);
       
    }

    private void GenratePrint(string orderId, bool v)
    {
       
        DataTable datatable = new DataTable();
        datatable.Clear();

        datatable.Columns.Add("Sr. No.");
        datatable.Columns.Add("DateTime");
        datatable.Columns.Add("Employee");
        datatable.Columns.Add("Target Amount");
        datatable.Columns.Add("Secondary Sale Total");
        datatable.Columns.Add("Target Visits");
        datatable.Columns.Add("No of Visits");
        datatable.Columns.Add("Productive Visits");
        datatable.Columns.Add("Non-Productive Visits");
        datatable.Columns.Add("Achievement of %");
        datatable.Columns.Add("Primary Party");
        datatable.Columns.Add("Primary Station");
        datatable.Columns.Add("Mobile No");
        if (orderId != "")
        {
            string _OrderId = orderId;
            StringBuilder sb = new StringBuilder();
            string[] SplitValue = _OrderId.Split(',');

            for (int _Count = 0; _Count < SplitValue.Length; _Count++)
            {
                int i = 1;
                DataSet datasale = getdata.getSeconarySalesDetails(SplitValue[_Count],"0","Select", dpFrom.Text.Trim(), dpTo.Text.Trim(), "2");
                foreach (DataRow dr in datasale.Tables[0].Rows)
                {
                    DataRow _row = datatable.NewRow();
                    _row["Sr. No."] = i++;
                    _row["DateTime"] = dr["CheckDate"] + " " + dr["CheckTime"];
                    _row["Employee"] = dr["Employees"];
                    _row["Secondary Sale Total"] = dr["TotalSale"];
                    _row["Target Visits"] = dr["TargetVisit"];
                    _row["Target Amount"] = dr["TargetAmount"];
                    _row["No of Visits"] = dr["Visited"];
                    _row["Primary Party"] = dr["PrimaryParty"];
                    _row["Primary Station"] = dr["PrimaryStation"];
                    _row["Mobile No"] = dr["MobileNo"];
                    _row["Productive Visits"] = dr["Productive"];
                    _row["Non-Productive Visits"] = Convert.ToInt32(dr["Visited"]) - Convert.ToInt32(dr["Productive"]);
                    _row["Achievement of %"] = ((Convert.ToDecimal(dr["TotalSale"]) / (Convert.ToDecimal(dr["TargetAmount"])==0?1: Convert.ToDecimal(dr["TargetAmount"]))) * 100).ToString("#0.00") + "%";

                    datatable.Rows.Add(_row);
                }
            }

            Session["GridData"] = datatable;
            Session["Title"] = "Secondary Sales";
            Session["DateRange"] = "(Date as on " + dpFrom.Text + " - " + dpTo.Text + ")";
            if(v)
            Response.Write("<script>window.open ('Print.aspx','_blank');</script>");
            else
            Response.Write("<script>window.open ('Print.aspx?EmpWise=1','_blank');</script>");
        }
    }
}