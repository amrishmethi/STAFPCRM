using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_SalesOrderSummary : System.Web.UI.Page
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
            Session["AccessRigthsSet"] = getdata.AccessRights("SalesOrderSummary.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            Gd.fillDepartment(drpDepartment);
            Gd.FillUser(drpUser, drpDepartment.SelectedValue);
            bindDrp(true, true);
            Gd.FillPrimaryParty(drpParty);
            //Filldata();
        }
    }

    private void bindDrp(bool isuser, bool ishqtr)
    {
        DataSet dsusr = getdata.getHqtrUserDpt(drpDepartment.SelectedValue);
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
        ds = getdata.getSalesSummaryOrder(drpUser.SelectedValue, drpHeadQtr.SelectedValue, drpParty.SelectedValue, mnth.Text, drpReport.SelectedValue, drpDepartment.SelectedValue, drpStatus.SelectedValue);
        DataView dv = ds.Tables[0].DefaultView;
        if (drpReport.SelectedValue == "TARGETWISE")
        {
            All.Visible = false;
            TargetWise.Visible = true;
            Repeater1.DataSource = ds.Tables[0];
            Repeater1.DataBind();
        }
        else
        {
            All.Visible = true;
            TargetWise.Visible = false;
            repData.DataSource = ds.Tables[0];
            repData.DataBind();

            lblPowder.Text = ds.Tables[0].Compute("Sum(POWDER)", "").ToString();
            lblBarTub.Text = ds.Tables[0].Compute("Sum(BAR_AND_TUB)", "").ToString();
            lblTotalAmount.Text = ds.Tables[0].Compute("Sum(AMOUNT)", "").ToString();
            lblTotalExp.Text = _TotalExp.ToString("0.00");
            lblTotalCTC.Text = ((_TotalExp / _TotalAmount) * 100).ToString("0.00");
        }

        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Filldata();
    }
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind(sender);
    }

    private void Bind(object sender)
    {
        DropDownList ddl = sender as DropDownList;
        if (ddl == drpUser)
        {
            bindDrp(false, true);
        }
        if (ddl == drpHeadQtr)
        {
            bindDrp(true, false);
        }
        if (ddl == drpDepartment)
        {
            bindDrp(true, false);
        }
    }

    protected void repData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hddcrmId = (HiddenField)e.Item.FindControl("hddcrmId");
            Label lblExpense = (Label)e.Item.FindControl("lblExpense");
            Label lblAmount = (Label)e.Item.FindControl("lblAmount");
            Label lblCTC = (Label)e.Item.FindControl("lblCTC");

            if (drpReport.SelectedIndex == 0)
            {
                int month = Convert.ToInt32(mnth.Text.Split('-')[0]);
                int year = Convert.ToInt32(mnth.Text.Split('-')[1]);
                string _DD = month + "/1/" + year;

                DataSet dss = getdata.GetSallary(_DD, "0", "0", hddcrmId.Value, "2", "ALL");
                if (dss.Tables[0].Rows.Count > 0)
                    lblExpense.Text = dss.Tables[0].Rows[0]["CTC"].ToString();
                else
                    lblExpense.Text = "0";
                double _CTC = Convert.ToDouble(lblExpense.Text) * 100 / Convert.ToDouble(lblAmount.Text);
                lblCTC.Text = _CTC.ToString("0.00");

                _TotalAmount += Convert.ToDouble(lblAmount.Text);
                _TotalExp += Convert.ToDouble(lblExpense.Text);
            }
        }
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind(sender);
    }
}