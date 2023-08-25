using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_PRODUCTS : System.Web.UI.Page
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
            Gd.FillGroup(drpGroup);
        }
    }


    public void fillData()
    {
        DataTable dtt = new DataTable();
        foreach (ListItem item in drpGroup.Items)
        {
            if (item.Selected)
            {
                ds = getdata.getProductList(item.Value);
                dtt.Merge(ds.Tables[0]);
            }
        }
        ViewState["tbl"] = dtt;
        rep.DataSource = dtt;
        rep.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }

    protected void txtUrl_TextChanged(object sender, EventArgs e)
    {
        int _Id = Convert.ToInt32(((TextBox)sender).ClientID.Split('_')[3]);
        HiddenField ItemID = (HiddenField)rep.Items[_Id].FindControl("hddItemID");
        TextBox txtUrl = (TextBox)rep.Items[_Id].FindControl("txtUrl");


        DataTable dtt = (DataTable)ViewState["tbl"];
        foreach (DataRow drr in dtt.Rows)
        {
            if (drr["ITCODE"].ToString() == ItemID.Value.ToString())
            {
                drr["ITEMIMAGE"] = txtUrl.Text;
            }
        }
        dtt.AcceptChanges();
        ViewState["tbl"] = dtt;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dtt = (DataTable)ViewState["tbl"];
        DataView dvv = dtt.DefaultView;
        dvv.RowFilter = "ITEMIMAGE<>''";
        DataTable dd = dvv.ToTable();
        foreach (DataRow drr in dd.Rows)
        {
            data.executeCommand("update [ITEM] set ITEMIMAGE='" + drr["ITEMIMAGE"].ToString() + "' where ITCODE='" + drr["ITCODE"] + "'");
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Save Successfully');window.location ='PRODUCTS.aspx'", true);
    }
}