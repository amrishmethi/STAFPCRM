using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_SalesOrder_Report : System.Web.UI.Page
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

            Session["AccessRigthsSet"] = getdata.AccessRights("SalesOrderReport.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            Gd.FillUser(drpUser);
            bindDrp(true, true);
            Gd.FillPrimaryParty(drpParty);
            Gd.FillGroup(drpGrp);
            //Gd.FillPrimaryStation(drpStation);
            Filldata();
        }
    }

    private void bindDrp(bool isuser, bool ishqtr)
    {
        DataSet dsusr = getdata.getHqtrUser();
        DataView dv = dsusr.Tables[0].DefaultView;
        if (isuser)
        {
            if (drpHeadQtr.SelectedIndex > 0)
                dv.RowFilter = "HeadQtr='" + drpHeadQtr.SelectedItem.Text + "'";
            dv.Sort = "Name";
            drpUser.DataSource = dv.ToTable(true, "Name", "MId");
            drpUser.DataTextField = "Name";
            drpUser.DataValueField = "MId";
            drpUser.DataBind();
            drpUser.Items.Insert(0, new ListItem("Select", "0"));
        }
        if (ishqtr)
        {
            if (drpUser.SelectedIndex > 0)
                dv.RowFilter = "Name='" + drpUser.SelectedItem.Text + "'";
            dv.Sort = "HeadQtr";
            drpHeadQtr.DataSource = dv.ToTable(true, "HeadQtr");
            drpHeadQtr.DataTextField = "HeadQtr";
            drpHeadQtr.DataValueField = "HeadQtr";
            drpHeadQtr.DataBind();
            drpHeadQtr.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    private void Filldata()
    {
        string str = "1=1";
        ds = getdata.getSalesOrder("SELECT","",drpUser.SelectedValue,drpHeadQtr.SelectedValue,drpParty.SelectedValue,dpFrom.Text.Trim(),dpTo.Text.Trim(),"","",drpGrp.SelectedValue);
        DataView dv = ds.Tables[0].DefaultView;
        if (drpStatus.SelectedValue == "Active") { str += " and Status = 'Active'"; }
        else if (drpStatus.SelectedValue == "Non-Active") { str += " and Status = 'Non-Active'"; }
        dv.RowFilter = str;
        rep.DataSource = dv.ToTable();
        rep.DataBind();
    }


  

    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hddid = (HiddenField)e.Item.FindControl("hddid");
            Repeater rep1 = (Repeater)e.Item.FindControl("rep1");
            Label lblTotal = (Label)e.Item.FindControl("lblTotal");
            Label lblQty = (Label)e.Item.FindControl("lblQty");
          //  Label lblPacking = (Label)e.Item.FindControl("lblPacking");
            Label lblWeight = (Label)e.Item.FindControl("lblWeight");
            DataSet dsrep1 = getdata.getSalesOrder("SELECT",hddid.Value,"","","","","","","",drpGrp.SelectedValue);
            if (dsrep1.Tables[1].Rows.Count > 0) {
                lblTotal.Text = (Convert.ToDecimal(dsrep1.Tables[1].Compute("Sum(Amount)", ""))).ToString("#0.00");
                lblQty.Text = (Convert.ToDecimal(dsrep1.Tables[1].Compute("Sum(OrdQty)", ""))).ToString("#0");
                //    lblPacking.Text = (Convert.ToDecimal(dsrep1.Tables[1].Compute("Sum(Packing)", ""))).ToString("#0.00");
  

                lblWeight.Text = (Convert.ToDecimal(dsrep1.Tables[1].Compute("sum(Weight)", ""))).ToString("#0.00");
                txtGrandTot.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtGrandTot.Text) + Convert.ToDecimal(lblTotal.Text));
                rep1.DataSource = dsrep1.Tables[1];
            rep1.DataBind();
            }
        }
    }



    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string sb = tblBlock.InnerHtml;

        StringReader sr = new StringReader(sb);
        Session["InvPrint"] = sb;
        Response.Write("<script>window.open('SalesOrder.aspx','_blank');</script>");
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName == "ResizeImg")
        {
            Response.Redirect("ResizeImage.aspx?imgurl=" + e.CommandArgument.ToString());
        }
    }


    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Filldata();
        DropDownList ddl = sender as DropDownList;
        if (ddl == drpUser)
        {
            bindDrp(false, true);
        }
        if (ddl == drpHeadQtr)
        {
            bindDrp(true, false);
        }
    }
}