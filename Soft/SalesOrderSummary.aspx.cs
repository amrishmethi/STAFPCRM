﻿using System;
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
            drpDepartment.SelectedValue = drpDepartment.Items.FindByText("SALES").Value;
            //Gd.FillUser(drpUser, drpDepartment.SelectedValue);
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
            if (drpStatus.SelectedIndex > 0)
                dv.RowFilter = " Status='" + drpStatus.SelectedValue + "'";
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
            lblBKBP.Text = ds.Tables[0].Compute("Sum(KBP)", "").ToString();
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
        if (ddl == drpStatus)
        {
            bindDrp(true, false);
        }
    }

    protected void repData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hddcrmId = (HiddenField)e.Item.FindControl("hddcrmId");
            HiddenField hddempId = (HiddenField)e.Item.FindControl("hddempId");
            Label lblExpense = (Label)e.Item.FindControl("lblExpense");
            Label lblAmount = (Label)e.Item.FindControl("lblAmount");
            Label lblCTC = (Label)e.Item.FindControl("lblCTC");

            if (drpReport.SelectedIndex == 0)
            {
                int month = Convert.ToInt32(mnth.Text.Split('-')[0]);
                int year = Convert.ToInt32(mnth.Text.Split('-')[1]);
                string _DD = month + "/1/" + year;


                var startDate = new DateTime(year, month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);




                double _CTC = 0, _Travel = 0, _NSA = 0, _DAL = 0, _Other = 0;

                DataSet dss = getdata.GetSallary(_DD, "0", "0", hddcrmId.Value, "2", "ALL");
                DataView dvv = dss.Tables[0].DefaultView;
                dvv.RowFilter = "ISapprove=1";
                if (dvv.ToTable().Rows.Count > 0)
                    _CTC = Convert.ToDouble(dvv.ToTable().Rows[0]["CTC"]);

                DataSet dss1 = data.getDataSet("usp_DistanceTravelReort_Summary '" + hddempId.Value + "','" + startDate + "','" + endDate + "','0','ALL'");

                if (dss1.Tables[0].Rows.Count > 0)
                {
                    _NSA = Convert.ToDouble(dss1.Tables[0].Compute("sum(NightStay)", ""));
                    _DAL = Convert.ToDouble(dss1.Tables[0].Compute("sum(DAL1)", ""));
                    _Other = Convert.ToDouble(dss1.Tables[0].Compute("sum(Other)", ""));
                    //_Travel = Convert.ToDouble(dss1.Tables[0].Compute("sum(TotalAmt)", ""));
                    _Travel = Convert.ToDouble(dss1.Tables[0].Rows[0]["TotalAmt"]);
                }

                lblExpense.Text = (_CTC + _Travel + _NSA + _DAL + _Other).ToString();
                //lblExpense.Text = (Convert.ToDouble(dss.Tables[0].Rows[0]["CTC"]) + Convert.ToDouble(dss.Tables[0].Rows[0]["CAVALUE"]) + Convert.ToDouble(dss.Tables[0].Rows[0]["NSA"]) + Convert.ToDouble(dss.Tables[0].Rows[0]["Other"]) + Convert.ToDouble(dss.Tables[0].Rows[0]["DAL"])).ToString();

                double _CTC1 = Convert.ToDouble(lblExpense.Text) * 100 / Convert.ToDouble(lblAmount.Text);
                lblCTC.Text = _CTC1.ToString("0.00");

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