using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Soft_HqtWiseSale : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("UserWiseParty.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            //Gd.fillDepartment(drpDepartment);
            //gd.FillUser(DrpEmployee);
            bindDrp();
            gd.FillPrimaryParty(drpParty);
            gd.FillPrimaryStation(Drpstation);
            gd.FillGroup1(drpGrp);

        }
    }



    private void bindDrp()
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        dv.Sort = "Name";
        DrpEmployee.DataSource = dv.ToTable(true, "Name", "MID");
        DrpEmployee.DataTextField = "Name";
        DrpEmployee.DataValueField = "MID";
        DrpEmployee.DataBind();
        DrpEmployee.Items.Insert(0, new ListItem("Select", "0"));
        dv.Sort = "HeadQtr";
        drpHeadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNO");
        drpHeadQtr.DataTextField = "HeadQtr";
        drpHeadQtr.DataValueField = "HeadQtrNO";
        drpHeadQtr.DataBind();
        drpHeadQtr.Items.Insert(0, new ListItem("Select", "0"));
        dv.Sort = "district";
        drpDistict.DataSource = dv.ToTable(true, "district", "districtNo");
        drpDistict.DataTextField = "district";
        drpDistict.DataValueField = "districtNo";
        drpDistict.DataBind();
        drpDistict.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void fillData()
    {
        string head = drpHeadQtr.SelectedValue;
        string station = Drpstation.SelectedValue;
        string party = drpParty.SelectedValue;
        string rate = Drprate.SelectedValue;
        string grp = "0";
        foreach (ListItem item in drpGrp.Items)
        {
            if (item.Selected)
            {
                grp += "," + item.Value;
            }
        }

        string district = "0";
        foreach (ListItem item in drpDistict.Items)
        {
            if (item.Selected)
            {
                district += "," + item.Value;
            }


        }
        ds = getdata.getSaleReportST(head, district, drpReport.SelectedValue, station, dpFrom.Text, dpTo.Text, rate, party, grp);

        rep.DataSource = ds.Tables[0];
        rep.DataBind();

        lblTotalBag.Text = ds.Tables[0].Compute("sum(ordbag)", "").ToString();
        lblTotalQty.Text = ds.Tables[0].Compute("sum(qty)", "").ToString();
        lblTotalAmt.Text = ds.Tables[0].Compute("sum(amount)", "").ToString();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }

    protected void drpHeadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        // Bind data to district dropdown list based on selected HeadQtr
        string selectedHeadQtr = drpHeadQtr.SelectedValue;
        dv.RowFilter = "HeadQtrNo = '" + selectedHeadQtr + "'";
        dv.Sort = "district";
        drpDistict.DataSource = dv.ToTable(true, "district", "districtNo");
        drpDistict.DataTextField = "district";
        drpDistict.DataValueField = "districtNo";
        drpDistict.DataBind();
        drpDistict.Items.Insert(0, new ListItem("Select", "0"));
    }




    protected void DrpEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        // Bind data to district dropdown list based on selected HeadQtr
        string selectedName = DrpEmployee.SelectedValue;
        dv.RowFilter = "MID = '" + selectedName + "'";
        dv.Sort = "HeadQtr";
        drpHeadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
        drpHeadQtr.DataTextField = "HeadQtr";
        drpHeadQtr.DataValueField = "HeadQtrNo";
        drpHeadQtr.DataBind();
        drpHeadQtr.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void drpDistict_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        // Bind data to district dropdown list based on selected HeadQtr
        string selecteddistrict = drpDistict.SelectedValue;
        dv.RowFilter = "districtNo = '" + selecteddistrict + "'";
        dv.Sort = "Station";
        Drpstation.DataSource = dv.ToTable(true, "Station", "StationNO");
        Drpstation.DataTextField = "Station";
        Drpstation.DataValueField = "StationNO";
        Drpstation.DataBind();
        Drpstation.Items.Insert(0, new ListItem("Select", "0"));
    }

}