using Mailjet.Client.Resources;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_Employeegroupassign : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData Gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];
            Gd.fillDepartment(drpDepartment);
            Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue);
            Gd.FillUser(drpProjectManager);
            //FillRecords();
        }
    }

    private void FillRecords()
    {
        string grp = "0";
        foreach (ListItem item in drpGrp.Items)
        {
            if (item.Selected)
            {
                grp += "," + item.Value;
            }
        }
        DataSet dsusr = getdata.GetEmployeList(drpDepartment.SelectedValue, drpDesignation.SelectedValue, drpProjectManager.SelectedValue, drpStatus.SelectedValue);
        ViewState["PartyList"] = dsusr;
        rep.DataSource = dsusr;
        rep.DataBind();
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string grp = "0";
        foreach (ListItem item in drpGrp.Items)
        {
            if (item.Selected)
            {
                grp += "," + item.Value;
            }
        }
        FillRecords(); 
    }


    protected void btnConfirmY_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;
        string id = (item.FindControl("hddEnqID") as HiddenField).Value;
        string hddGroups = (item.FindControl("hddGroups") as HiddenField).Value;



        DataTable dtable = ((DataSet)ViewState["PartyList"]).Tables[1];
        foreach (DataRow drr in dtable.Rows)
        {
            drr["PartyID"] = id;
            drr["chk"] = (hddGroups.Split(',').Contains(drr["CMsCode"])) ? "1" : "0";
        }
        dtable.AcceptChanges();
        repsku.DataSource = dtable;
        repsku.DataBind();
        mpe.Show();
    }



    protected void btnApply_Click(object sender, EventArgs e)
    {
        string value = "";
        HiddenField hddAssID = new HiddenField();
        foreach (RepeaterItem item in repsku.Items)
        {
            HiddenField hddCmsCode = (HiddenField)item.FindControl("hddCmsCode");
            hddAssID = (HiddenField)item.FindControl("hddPartyId");
            CheckBox chkItems = item.FindControl("chkItems") as CheckBox;
            if (chkItems.Checked)
            {
                value += hddCmsCode.Value + ",";
            }
        }

        data.executeCommand("Update tbl_EMPMaster set ItemGroup='" + value.Substring(0, value.Length - 1) + "' where EMPID='" + hddAssID.Value.ToString() + "'");



        DataTable dtable = ((DataSet)ViewState["PartyList"]).Tables[0];
        foreach (DataRow drr in dtable.Rows)
        {
            if (drr["EMPId"].ToString() == hddAssID.Value.ToString())
                drr["ITEMGROUP"] = value.Substring(0, value.Length - 1);
        }
        dtable.AcceptChanges();
        rep.DataSource = dtable;
        rep.DataBind();
    }
}