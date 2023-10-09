using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
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
            Gd.FillGroup1(drpGrp);
            //FillRecords();
        }
    }
     

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.FillUser(drpProjectManager, drpDepartment.SelectedValue, drpStatus.SelectedValue, drpDesignation.SelectedValue);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    { 
        DataSet dsusr = getdata.GetEmployeList(drpDepartment.SelectedValue, drpDesignation.SelectedValue, drpProjectManager.SelectedValue, drpStatus.SelectedValue);

        DataTable dt = new DataTable();
        foreach (ListItem item in drpGrp.Items)
        {
            if (item.Selected)
            {
                DataView dttt = dsusr.Tables[0].DefaultView;
                dttt.RowFilter = " ItemGroup like '%" + item.Value + "%'";
                dt.Merge(dttt.ToTable());
            }
        }

        if (dt.Rows.Count == 0)
        {
            dt = dsusr.Tables[0];
        }


        ViewState["PartyList"] = dt;
        ViewState["GroupList"] = dsusr.Tables[1];
        rep.DataSource = dt;
        rep.DataBind();
    }

    protected void lstGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtable = ((DataTable)ViewState["GroupList"]);
        DataView dv = dtable.DefaultView;
        if (lstGrp.SelectedIndex > 0)
            dv.RowFilter = "MCMsCode='" + lstGrp.SelectedValue + "'";
        repsku.DataSource = dv.ToTable(); ;
        repsku.DataBind();
    }

    protected void btnConfirmY_Click(object sender, EventArgs e)
    {
        Gd.FillMainGroup(lstGrp);

        RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;
        string id = (item.FindControl("hddEnqID") as HiddenField).Value;
        string lblParty = (item.FindControl("lblParty") as Label).Text;
        string hddGroups = (item.FindControl("hddGroups") as HiddenField).Value;

        lblHead.Text = lblParty;

        DataTable dtable = ((DataTable)ViewState["GroupList"]);
        foreach (DataRow drr in dtable.Rows)
        {
            drr["PartyID"] = id;
            drr["chk"] = (hddGroups.Split(',').Contains(drr["CMsCode"])) ? "1" : "0";
        }
        dtable.AcceptChanges();
        ViewState["GroupList"] = dtable;
        repsku.DataSource = dtable;
        repsku.DataBind();
        mpe.Show();
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        string CMSCode = "", CMSName = "";
        HiddenField hddAssID = new HiddenField();


        DataTable dtable1 = ((DataTable)ViewState["GroupList"]);
        foreach (DataRow drr in dtable1.Rows)
        {
            if (drr["Chk"].ToString() == "1")
            {
                CMSCode += drr["CMSCode"].ToString() + ",";
                CMSName += drr["CMSName"].ToString() + ",";
            }
        }

        if (CMSCode.Length == 0)
        {
            CMSCode = "";
            CMSName = "";
        }
        else
        {
            CMSCode = CMSCode.Substring(0, CMSCode.Length - 1);
            CMSName = CMSName.Substring(0, CMSName.Length - 1);
        }
        hddAssID.Value = dtable1.Rows[0]["PartyID"].ToString();
        data.executeCommand("Update tbl_EMPMaster set ItemGroup='" + CMSCode + "' where EMPID='" + hddAssID.Value.ToString() + "'");

        DataTable dtable = ((DataTable)ViewState["PartyList"]);
        foreach (DataRow drr in dtable.Rows)
        {
            if (drr["EMPId"].ToString() == hddAssID.Value.ToString())
            {
                drr["ITEMGROUP"] = CMSCode;
                drr["ITEMGROUPS"] = CMSName;
            }
        }
        dtable.AcceptChanges();
        ViewState["PartyList"] = dtable;
        rep.DataSource = dtable;
        rep.DataBind(); 
    }

    protected void chkItems_CheckedChanged(object sender, EventArgs e)
    {
        DataTable dtable = ((DataTable)ViewState["GroupList"]);
        foreach (RepeaterItem item in repsku.Items)
        {
            HiddenField hddCmsCode = (HiddenField)item.FindControl("hddCmsCode");
            CheckBox chkItems = item.FindControl("chkItems") as CheckBox;
            foreach (DataRow drr in dtable.Rows)
            {
                if (drr["CMSCode"].ToString() == hddCmsCode.Value)
                    drr["chk"] = chkItems.Checked;
            }
        }
        dtable.AcceptChanges();
        ViewState["GroupList"] = dtable;
    }
}