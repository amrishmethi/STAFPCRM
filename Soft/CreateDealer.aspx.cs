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

public partial class Admin_CreateDealer : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData Gd = new GetData();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];
            Session["AccessRigthsSet"] = getdata.AccessRights("CreateDealer.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];

            Gd.FillUser(drpEmp);
            bindDrp(true, true);
            txtDateFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            txtDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');

            //fillData();
        }
    }

    private void bindDrp(bool isuser, bool ishqtr)
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        if (isuser)
        {
            if (drpHqtr.SelectedIndex > 0)
                dv.RowFilter = "HeadQtrNo='" + drpHqtr.SelectedValue + "'";
            dv.Sort = "Name";
            drpEmp.DataSource = dv.ToTable(true, "Name", "MId");
            drpEmp.DataTextField = "Name";
            drpEmp.DataValueField = "MId";
            drpEmp.DataBind();
            drpEmp.Items.Insert(0, new ListItem("Select", "0"));
        }
        if (ishqtr)
        {
            if (drpEmp.SelectedIndex > 0)
                dv.RowFilter = "MId='" + drpEmp.SelectedValue + "'";
            dv.Sort = "HeadQtr";
            drpHqtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
            drpHqtr.DataTextField = "HeadQtr";
            drpHqtr.DataValueField = "HeadQtrNo";
            drpHqtr.DataBind();
            drpHqtr.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    public void fillData()
    {
        ds = getdata.getCreateDealer("SELECT", "0", "", "", "", "", "", "", "", "", "", "", "", "", drpType.SelectedValue, "", txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), drpEmp.SelectedValue, drpHqtr.SelectedValue);
        DataView dv = ds.Tables[0].DefaultView;
        //if (drpIsMeet.SelectedValue == "0") { dv.RowFilter = " AddedDate is null"; }
        //else if (drpIsMeet.SelectedValue == "1") { dv.RowFilter = "AddedDate <> ''"; }



        ViewState["tbl"] = dv.ToTable();
        Session["ClientMeet"] = dv.ToString();

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



    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillData();
        DropDownList ddl = sender as DropDownList;
        if (ddl == drpEmp)
        {
            bindDrp(false, true);
        }
        if (ddl == drpHqtr)
        {
            bindDrp(true, false);
        }
    }

    protected void lnkDownloadPDF_Click(object sender, EventArgs e)
    {

        DataTable datatable = new DataTable();
        datatable.Clear();

        datatable.Columns.Add("Sr. No.");
        datatable.Columns.Add("DateTime");
        datatable.Columns.Add("Employee");
        datatable.Columns.Add("Head Quarter");
        datatable.Columns.Add("Name");
        datatable.Columns.Add("Station");
        datatable.Columns.Add("Address");
        datatable.Columns.Add("District");
        datatable.Columns.Add("Zip");
        datatable.Columns.Add("State");
        datatable.Columns.Add("Contact Person");
        datatable.Columns.Add("GST No");
        datatable.Columns.Add("Gst Type");
        datatable.Columns.Add("Mobile");
        // datatable.Columns.Add("Mobile No");
        datatable.Columns.Add("WhatsApp No");
        datatable.Columns.Add("Transport");
        datatable.Columns.Add("Party Type");
        datatable.Columns.Add("Party Category");
        // datatable.Columns.Add("Meet Type");
        // datatable.Columns.Add("Image");

        foreach (DataRow dr in ((DataTable)ViewState["tbl"]).Rows)
        {
            DataRow _row = datatable.NewRow();
            _row["Sr. No."] = datatable.Rows.Count + 1;
            _row["DateTime"] = dr["cdDATE"];
            _row["Employee"] = dr["Employee"];
            _row["Head Quarter"] = dr["HeadQtr"];
            _row["Name"] = dr["Name"];
            _row["Station"] = dr["Station"];
            _row["Address"] = dr["Address"];
            _row["District"] = dr["District"];
            _row["Zip"] = dr["PinCode"];
            _row["State"] = dr["State"];
            _row["Contact Person"] = dr["ContPer"];
            _row["GST No"] = dr["GSTNo"];
            _row["Gst Type"] = dr["GstType"];
            _row["Mobile"] = dr["SMSMobile"];
            //   _row["Mobile No"] = dr["MobileNo"];
            _row["WhatsApp No"] = dr["WhatsAppNo"];
            _row["Party Type"] = dr["Transport"];
            _row["Party Category"] = dr["PartyCatg"];
            // _row["Meet Type"] = dr["ClientMeetType"];
            //    _row["Image"] = dr["Image"];
            datatable.Rows.Add(_row);
        }
        //GeneratePDF(datatable, "ClientMeet");
        Session["GridData"] = datatable;
        Session["Title"] = "Create Dealer";
        Session["DateRange"] = "(Date as on " + txtDateFrom.Text + " - " + txtDateTo.Text + ")";
        Session["TermsId"] = "";
        //Response.Redirect("Print.aspx");
        Response.Write("<script>window.open ('Print.aspx','_blank');</script>");

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillData();
    }
}