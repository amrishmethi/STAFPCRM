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

public partial class Soft_EmployeeBookAssign : System.Web.UI.Page
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
            Gd.FillBookType(drpCategory);
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
        foreach (ListItem item in drpCategory.Items)
        {
            if (item.Selected)
            {
                DataView dttt = dsusr.Tables[0].DefaultView;
                dttt.RowFilter = " BKisno like '%" + item.Value + "%'";
                dt.Merge(dttt.ToTable());
            }
        }

        if (dt.Rows.Count == 0)
        {
            dt = dsusr.Tables[0];
        }

        ViewState["PartyList"] = dt;
        ViewState["CategoryList"] = dsusr.Tables[3];
        rep.DataSource = dt;
        rep.DataBind();
    }

    protected void lstGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtable = ((DataTable)ViewState["CategoryList"]);
        DataView dv = dtable.DefaultView;
        if (lstGrp.SelectedIndex > 0)
            dv.RowFilter = "BKisno='" + lstGrp.SelectedValue + "'";
        repsku.DataSource = dv.ToTable(); ;
        repsku.DataBind();
    }

    protected void btnConfirmY_Click(object sender, EventArgs e)
    {
        Gd.FillBookType(lstGrp);

        RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;
        string id = (item.FindControl("hddEnqID") as HiddenField).Value;
        string lblParty = (item.FindControl("lblParty") as Label).Text;
        string hddGroups = (item.FindControl("hddGroups") as HiddenField).Value;

        lblHead.Text = lblParty;

        DataTable dtable = ((DataTable)ViewState["CategoryList"]);
        foreach (DataRow drr in dtable.Rows)
        {
            drr["PartyID"] = id;
            drr["chk"] = (hddGroups.Split(',').Contains(drr["BKName"].ToString())) ? "1" : "0";
        }
        dtable.AcceptChanges();
        ViewState["CategoryList"] = dtable;
        repsku.DataSource = dtable;
        repsku.DataBind();
        mpe.Show();
    }



    protected void btnApply_Click(object sender, EventArgs e)
    {
        string MsName = "", MsNo = "";
        HiddenField hddAssID = new HiddenField();
        Soft = Request.Cookies["STFP"];

        DataTable dtable1 = ((DataTable)ViewState["CategoryList"]);
        foreach (DataRow drr in dtable1.Rows)
        {
            if (drr["Chk"].ToString() == "1")
            {
                MsName += drr["BKName"].ToString() + ",";
                MsNo += drr["BKisno"].ToString() + ",";
            }
        }

        if (MsName.Length == 0)
        {
            MsName = "";
            MsNo = "";
        }
        else
        {
            MsName = MsName.Substring(0, MsName.Length - 1);
            MsNo = MsNo.Substring(0, MsNo.Length - 1);
        }
        hddAssID.Value = dtable1.Rows[0]["PartyID"].ToString(); string _UserId = Soft["UserId"];
        data.executeCommand("Update tbl_EMPMaster set BOOK='" + MsNo + "',ModifyDate=GETDATE(),ModifyUser='" + _UserId + "' where EMPID='" + hddAssID.Value.ToString() + "'");

        DataTable dtable = ((DataTable)ViewState["PartyList"]);
        foreach (DataRow drr in dtable.Rows)
        {
            if (drr["EMPId"].ToString() == hddAssID.Value.ToString())
            {
                drr["PARTYCATEGORYS"] = MsName;
                drr["PARTYCATEGORY"] = MsNo;
            }
        }
        dtable.AcceptChanges();
        ViewState["PartyList"] = dtable;
        rep.DataSource = dtable;
        rep.DataBind();
    }

    protected void chkItems_CheckedChanged(object sender, EventArgs e)
    {
        DataTable dtable = ((DataTable)ViewState["CategoryList"]);
        foreach (RepeaterItem item in repsku.Items)
        {
            HiddenField hddMsName = (HiddenField)item.FindControl("hddMSNO");
            CheckBox chkItems = item.FindControl("chkItems") as CheckBox;
            foreach (DataRow drr in dtable.Rows)
            {
                if (drr["BKisno"].ToString() == hddMsName.Value)
                    drr["chk"] = chkItems.Checked;
            }
        }
        dtable.AcceptChanges();
        ViewState["CategoryList"] = dtable;
    }
}