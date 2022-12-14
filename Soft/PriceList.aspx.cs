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
            ds = getdata.getPriceList("");
         //   ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert(" + ds.Tables[0].Rows[0][0].ToString() + ")", true);
            ViewState["Tbl"] = ds;
            Gd.FillGroup(drpGroup);
            fillData();
        }
    }

   

    public void fillData()
    {
        
        DataView dv = ((DataSet)ViewState["Tbl"]).Tables[0].DefaultView;
        StringBuilder str = new StringBuilder();
        if (drpState.SelectedIndex == 0)
        {
            if (str.Length>0)
            {
                str.Append("and");
            }
            str.Append(" OutOfState = 0 ");
        }else
        {
            if (str.Length>0)
            {
                str.Append("and");
            }
            str.Append(" OutOfState <> 0 ");
        }
        dv.RowFilter = str.ToString();
        rep.DataSource = dv.ToTable();
        rep.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strrr = "";
        if (drpGroup.SelectedIndex > 0)
        {
           DataSet dss = getdata.getSubGrpString(drpGroup.SelectedValue);
            
            if (dss.Tables[0].Rows.Count > 0)
            {   
                foreach(DataRow dr in dss.Tables[0].Rows)
                {
                  strrr += "'"+ dr["CMsCode"] + "',";
                }
            }
        }
        if (strrr.Length > 0)
        {
            ds = getdata.getPriceList(strrr.Substring(0, strrr.Length - 1));
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert("+ds.Tables[0].Rows[0][0].ToString()+")", true);
            ViewState["Tbl"] = ds;
        }
        fillData();
    }

    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }


}