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

            Gd.FillGroup(drpGroup);

            //   ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert(" + ds.Tables[0].Rows[0][0].ToString() + ")", true);
            //   ViewState["Tbl"] = ds;
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
}