using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_SalesItem_Report : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    Data data = new Data();
    string query;
    SqlCommand cmd = new SqlCommand();
    Master getdata = new Master();
    GetData Gd = new GetData();
    private HttpCookie Soft;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("SalesItemReport.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            //dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            //dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            //Gd.FillUser(drpUser);
            //Gd.FillPrimaryParty(drpParty);
            //Gd.FillPrimaryStation(drpStation);
            if (Request.QueryString["id"] != null)
            {
                Session["CheckID"] = Request.QueryString["id"];
                Filldata();
            }
        }
    }


    private void Filldata()
    {
        ds = getdata.getSecondarySalesItem(Request.QueryString["id"].ToString());

        rep.DataSource = ds.Tables[0];
        rep.DataBind();
    }


  

    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hddid = (HiddenField)e.Item.FindControl("hddid");
            Repeater rep1 = (Repeater)e.Item.FindControl("rep1");
            Label lblTotal = (Label)e.Item.FindControl("lblTotal");
            DataSet dsrep1 = data.getDataSet("PROC_SECONDARYITEMSDETAILS '" + hddid.Value + "'");
            if (dsrep1.Tables[0].Rows.Count > 0) { 
            lblTotal.Text = Convert.ToDecimal(dsrep1.Tables[0].Compute("Sum(Amount)", ""))+"";
                txtGrandTot.Text = String.Format("{0:0.00}",Convert.ToDecimal(txtGrandTot.Text)+Convert.ToDecimal(lblTotal.Text));
            rep1.DataSource = dsrep1;
            rep1.DataBind();
            }
        }
    }



    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string sb = tblBlock.InnerHtml;
       
        StringReader sr = new StringReader(sb);
        Session["InvPrint"] = sb;
        Response.Write("<script>window.open('SecondarySalePrint.aspx','_blank');</script>");
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName == "ResizeImg")
        {
            Response.Redirect("ResizeImage.aspx?imgurl=" + e.CommandArgument.ToString());
        }
    }

}