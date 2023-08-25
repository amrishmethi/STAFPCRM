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

public partial class Admin_PriceList : System.Web.UI.Page
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
            Session["AccessRigthsSet"] = getdata.AccessRights("PriceList.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
       

            Gd.FillGroup(drpGroup);
            Gd.FillTerms(drpTerms); 
            fillData();
        }
    }



    public void fillData()
    {
        DataTable dtt = new DataTable();
        foreach (ListItem item in drpGroup.Items)
        {
            if (item.Selected)
            {
                ds = getdata.getPriceList(item.Value, drpState.SelectedValue);
                dtt.Merge(ds.Tables[0]);
            }
        }
        ViewState["tbl"] = dtt;
        rep.DataSource = dtt;
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



    protected void drpGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillData();
    }

    protected void lnkDownloadPDF_Click(object sender, EventArgs e)
    {
        DataTable datatable = new DataTable();
        datatable.Clear();


        datatable.Columns.Add("Sr. No.");
        datatable.Columns.Add("Item Name");
        datatable.Columns.Add("Rate Per Kg");
        datatable.Columns.Add("Rate Per Pc");
        datatable.Columns.Add("Carton Pack Per Pc");
        datatable.Columns.Add("Amount Per Bag/Case");
        datatable.Columns.Add("MRP Per Pc");


        foreach (DataRow dr in ((DataTable)ViewState["tbl"]).Rows)
        {
            DataRow _row = datatable.NewRow();
            _row["Sr. No."] = datatable.Rows.Count + 1;
            _row["Item Name"] = dr["itname"];
            _row["Rate Per Kg"] = dr["SalesOrderRate"];
            _row["Rate Per Pc"] = dr["tRate"];
            _row["Carton Pack Per Pc"] = dr["itpacking"];
            _row["Amount Per Bag/Case"] = dr["BagRate"];
            _row["MRP Per Pc"] = dr["MRP"];

            datatable.Rows.Add(_row);
        }

        Session["GridData"] = datatable;
        Session["Title"] = "Price List";
        string strGrp = "";
        foreach (ListItem item in drpGroup.Items)
        {
            if (item.Selected)
            {
                if (strGrp == "")
                    strGrp = item.Text;
                else
                    strGrp += ", " + item.Text;
            }
        }
        Session["DateRange"] = "(" + strGrp + ")";
        Session["TermsId"] = drpTerms.SelectedValue;
        //Response.Redirect("Print.aspx");
        Response.Write("<script>window.open ('Print.aspx','_blank');</script>");
    }
}