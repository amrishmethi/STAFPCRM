using iTextSharp.text;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_Station : System.Web.UI.Page
{
    GetData Gd = new GetData();
    Master master = new Master();
    Data data = new Data();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["HeadQtrDistrict"] = Gd.FillHeadQtrDistrict();
            ViewState["station"] = Gd.FillStation(); 
            BindData();
            FillData();

        }
    }

    private void BindData()
    {
        ds = (DataSet)ViewState["HeadQtrDistrict"];
        DataView view = new DataView(ds.Tables[0]);
        view.Sort = "HeadQtr";
        DataTable HeadQtr = view.ToTable(true, "HeadQtr", "HeadQtrNo");
        Gd.FillData(drpHeadqtr, HeadQtr, "HeadQtr", "HeadQtrNo");

        view.Sort = "District";
        DataTable District = view.ToTable(true, "District", "DistrictNo");
        Gd.FillData(drpDistrict, District, "District", "DistrictNo");
    }
    protected void IsChkLoginApp_CheckedChanged(object sender, EventArgs e)
    {
        int ItemC = 0;
        string[] ClientID = ((CheckBox)sender).ClientID.Split('_');
        if (ClientID.Length == 4)
        {
            ItemC = Convert.ToInt32(ClientID[3]);
        }

        CheckBox chk = (CheckBox)repDepartment.Items[ItemC].FindControl("IsChkLoginApp");
        HiddenField hddUid = (HiddenField)repDepartment.Items[ItemC].FindControl("hddUid");
        if (hddUid.Value != "")
        {
            data.getDataSet("Update  [station]  set isActive = (case when isActive=1 then 0 else 1 end)  where StationNo = " + hddUid.Value);
            FillData();
        }


    }
    public void FillData()
    {
        string grp = "0=0";
        foreach (System.Web.UI.WebControls.ListItem item in drpDistrict.Items)
        {
            if (item.Selected)
            {
                grp += "  or DistrictNO='" + item.Value + "'";
            }
        }

        ds = Gd.FillStation();
        DataView view = new DataView(ds.Tables[0]);
        string _RowFilter = "0=0";
        if (drpHeadqtr.SelectedIndex > 0)
            _RowFilter += " and HeadQtrNo='" + drpHeadqtr.SelectedValue + "'";
        if (grp != "0=0")
            _RowFilter += " and (" + grp.Replace("0=0  or", "") + ")";
        if (drpstatus.SelectedIndex > 0)
            _RowFilter += " and IsActive=" + drpstatus.SelectedValue + "";
        view.RowFilter = _RowFilter;
        view.Sort = "station";
        repDepartment.DataSource = view.ToTable();
        repDepartment.DataBind();
    }



    protected void drpHeadqtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        ds = (DataSet)ViewState["HeadQtrDistrict"];
        DataView view = new DataView(ds.Tables[0]);
        view.RowFilter = "HeadQtr='" + drpHeadqtr.SelectedValue + "'";
        view.Sort = "District";
        DataTable District = view.ToTable(true, "District");
        Gd.FillData(drpDistrict, District, "District", "District");

        FillData();
    }

    protected void drpDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillData();
    }
}