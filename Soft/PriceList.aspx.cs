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
            //   Gd.FillUser(drpUser);
            ds = getdata.getPriceList();
            ViewState["Tbl"] = ds;
            BindDrp();
            fillData(ds);
        }
    }

    private void BindDrp()
    {
        DataView dv = ((DataSet)ViewState["Tbl"]).Tables[0].DefaultView;
        dv.Sort = "GPCode1";
        drpGroup.DataSource = dv.ToTable(true, "GPCode1");
        drpGroup.DataTextField = "GPCode1";
        drpGroup.DataValueField = "GPCode1";
        drpGroup.DataBind();
        drpGroup.Items.Insert(0, new ListItem("Select", "0"));

        dv.Sort = "OutOfState";
        drpStateOut.DataSource = dv.ToTable(true, "OutOfState");
        drpStateOut.DataTextField = "OutOfState";
        drpStateOut.DataValueField = "OutOfState";
        drpStateOut.DataBind();
        drpStateOut.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void fillData(DataSet dss)
    {
        DataView dv = ((DataSet)ViewState["Tbl"]).Tables[0].DefaultView;
        StringBuilder str = new StringBuilder();

        if (drpGroup.SelectedIndex > 0)
        {
            str.Append(" GPCode1 = '" + drpGroup.SelectedValue + "' ");
        }
        if (drpStateOut.SelectedIndex > 0)
        {
            if (str != null)
            {
                str.Append("and");
            }
            str.Append(" OutOfState = '" + drpStateOut.SelectedValue + "' ");
        }
        dv.RowFilter = str.ToString();
        rep.DataSource = dv.ToTable();
        rep.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData((DataSet)ViewState["Tbl"]);
    }



    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }

}