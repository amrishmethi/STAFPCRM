using CuteEditor.Convertor;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Admin_SalesTargets : System.Web.UI.Page
{
    DataTable dtGrp = new DataTable();
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;
    DataTable Dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];
             
            gd.fillDepartment(drpDepartment);

            mnth.Text = DateTime.Now.ToString("MM-yyyy");


            DataSet dsusr = getdata.getHqtrUserDpt(drpDepartment.SelectedValue);
            ViewState["tbl1"] = dsusr;
            DataView dv = dsusr.Tables[0].DefaultView;
            if (drpStatus.SelectedIndex > 0)
                dv.RowFilter = " Status='" + drpStatus.SelectedValue + "'";
            dv.Sort = "Name";
            drpUser.DataSource = dv.ToTable(true, "Name", "MId");
            drpUser.DataTextField = "Name";
            drpUser.DataValueField = "MId";
            drpUser.DataBind();
            drpUser.Items.Insert(0, new ListItem("Select", "0"));

            dv.Sort = "HeadQtr";
            drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
            drpheadQtr.DataTextField = "HeadQtr";
            drpheadQtr.DataValueField = "HeadQtrNo";
            drpheadQtr.DataBind();
            drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));

            DataSet dss = data.getDataSet("SELECT * from SaleTargetGroupList_View where Display=1");
            ViewState["Columns"] = dss;

            createTable();

        }
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = (DataSet)ViewState["tbl1"];
        DataView dv = dsusr.Tables[0].DefaultView;
        if (drpStatus.SelectedIndex > 0)
            dv.RowFilter = " Status='" + drpStatus.SelectedValue + "'";
        dv.Sort = "Name";
        drpUser.DataSource = dv.ToTable(true, "Name", "MId");
        drpUser.DataTextField = "Name";
        drpUser.DataValueField = "MId";
        drpUser.DataBind();
        drpUser.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void fillData()
    {
        int month = Convert.ToInt32(mnth.Text.Split('-')[0]);
        int year = Convert.ToInt32(mnth.Text.Split('-')[1]);
        string _DD = year + "-" + month + "-01";

        string qq = "Select distinct S.*,EMP.Emp_Name,mm.MsName from tbl_SalesTarge_new S LEFT JOIN tbl_EmpMaster EMP on EMP.CRMUserId=S.UserID and EMP.Delid=0 LEFT JOIN mastbyno mm on mm.MsNo=S.HeadQtr and mm.MsSR='HDQ' where 0=0 ";
        if (drpDepartment.SelectedIndex > 0)
            qq += " and EMP.Dept_Id=" + drpDepartment.SelectedValue;
        if (drpStatus.SelectedIndex > 0)
            qq += " and EMP.Status='" + drpStatus.SelectedValue + "'";
        if (drpUser.SelectedIndex > 0)
            qq += " and S.UserID=" + drpUser.SelectedValue;
        if (drpheadQtr.SelectedIndex > 0)
            qq += " and S.HeadQtr=" + drpheadQtr.SelectedValue;
        if (mnth.Text != "")
            qq += " and Format(S.TargetMonth, 'MM-yyyy')='" + mnth.Text + "'";
        DataSet dsTarget = data.getDataSet(qq);
        ViewState["TargetData"] = dsTarget;


        string query = "select distinct HEADQTR,Name,MID,HeadQtrNo from GETHEADQUARTER  where 0=0";
        if (drpDepartment.SelectedIndex > 0)
            query += " and Dept_Id=" + drpDepartment.SelectedValue;
        if (drpStatus.SelectedIndex > 0)
            query += " and Status='" + drpStatus.SelectedValue + "'";
        if (drpUser.SelectedIndex > 0)
            query += " and MID=" + drpUser.SelectedValue;
        if (drpheadQtr.SelectedIndex > 0)
            query += " and HeadQtrNo=" + drpheadQtr.SelectedValue;
        DataView dv = data.getDataSet(query).Tables[0].DefaultView;
        repData.DataSource = dv.ToTable();
        repData.DataBind();

    }

    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = (DataSet)ViewState["tbl1"];
        DataView dv = dsusr.Tables[0].DefaultView;
        dv.RowFilter = "Mid=" + drpUser.SelectedValue;
        dv.Sort = "HeadQtr";
        drpheadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNO");
        drpheadQtr.DataTextField = "HeadQtr";
        drpheadQtr.DataValueField = "HeadQtrNO";
        drpheadQtr.DataBind();
        drpheadQtr.Items.Insert(0, new ListItem("Select", "0"));
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillData();
    }

    protected void btnSalary_Click(object sender, EventArgs e)
    {
        int month = Convert.ToInt32(mnth.Text.Split('-')[0]);
        int year = Convert.ToInt32(mnth.Text.Split('-')[1]);
        string _DD = year + "-" + month + "-01";
        DataTable dtColumn = ((DataSet)ViewState["Columns"]).Tables[0];
        DataTable Dt = (DataTable)ViewState["dtMain"];
        foreach (DataRow drr in Dt.Rows)
        {
            string q = "", _ColumnName = "", _ColumnValue = "";
            if (data.Exist("select * from tbl_SalesTarge_new where USERID='" + drr["Mid"] + "' and HeadQtr='" + drr["HeadQtrNo"] + "' and Format(TargetMonth,'MM-yyyy')='" + mnth.Text + "'"))
            {
                q = "update tbl_SalesTarge_new SET ModifyDate=Getdate() ";
                foreach (DataRow drCol in dtColumn.Rows)
                {
                    string ss = drCol["SS"].ToString().Replace(' ', '_').ToString();
                    q += ", [" + ss + "] = " + drr[drCol["SS"].ToString()].ToString() + "";
                }
                q += " where USERID='" + drr["Mid"] + "' and HeadQtr='" + drr["HeadQtrNo"] + "' and Format(TargetMonth,'MM-yyyy')='" + mnth.Text + "'";
            }
            else
            {
                foreach (DataRow drCol in dtColumn.Rows)
                {
                    string ss = drCol["SS"].ToString().Replace(' ', '_').ToString();
                    _ColumnName += ", [" + ss + "]";
                    _ColumnValue += ", '" + drr[drCol["SS"].ToString()].ToString() + "'";
                }
                q = " insert into tbl_SalesTarge_new (USERID,HeadQtr,TargetMonth" + _ColumnName + ")";
                q += " Values('" + drr["Mid"] + "','" + drr["HeadQtrNo"] + "','" + _DD + "'" + _ColumnValue + ")";
            }
            data.executeCommand(q);
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Sales Target Saved Successfully');window.location ='SalesTargets.aspx'", true);

    }


    public void createTable()
    {
        dtGrp.Columns.Add("sText");

        DataSet dss = (DataSet)ViewState["Columns"];
        foreach (DataRow drr in dss.Tables[0].Rows)
        {
            DataRow dr = dtGrp.NewRow();
            dr["sText"] = drr["SS"];
            dtGrp.Rows.Add(dr);
        }
        repHead.DataSource = dtGrp;
        repHead.DataBind();




        Dt.Columns.Add("HeadQtrNo", typeof(string));
        Dt.Columns.Add("MID", typeof(string));
        for (int k = 0; k < dtGrp.Rows.Count; k++)
        {
            string ff = dtGrp.Rows[k]["sText"].ToString();
            Dt.Columns.Add(ff, typeof(string));
        }
        ViewState["dtMain"] = Dt;
    }

    protected void repData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater repd = (Repeater)e.Item.FindControl("repd");
            DataSet dss = (DataSet)ViewState["Columns"];

            HiddenField HddHeadQtrNo = (HiddenField)e.Item.FindControl("HddHeadQtrNo");
            HiddenField hddMID = (HiddenField)e.Item.FindControl("hddMID");
            DataView dvv = ((DataSet)ViewState["TargetData"]).Tables[0].DefaultView;
            dvv.RowFilter = "UserId='" + hddMID.Value + "' and HeadQtr='" + HddHeadQtrNo.Value + "'";
            DataTable dtt = dvv.ToTable();
            
            foreach (DataRow drr in dss.Tables[0].Rows)
                drr["Qty"] = dtt.Rows[0][drr["Name"].ToString()];


            dss.Tables[0].AcceptChanges();
            repd.DataSource = dss;
            repd.DataBind();
        }
    }

    protected void txtRep_TextChanged(object sender, EventArgs e)
    {
        GetDataaa();
    }

    private void GetDataaa()
    {
        for (int i = 0; i < repData.Items.Count; i++)
        {
            Repeater repd = (Repeater)repData.Items[i].FindControl("repd");
            HiddenField HddHeadQtrNo = (HiddenField)repData.Items[i].FindControl("HddHeadQtrNo");
            HiddenField hddMID = (HiddenField)repData.Items[i].FindControl("hddMID");
            for (int ii = 0; ii < repd.Items.Count; ii++)
            {
                TextBox txtRep = (TextBox)repd.Items[ii].FindControl("txtRep");
                HiddenField hddSizeVal = (HiddenField)repd.Items[ii].FindControl("hddSizeVal");
                string txtSize1 = txtRep.Text.Trim();
                if (String.IsNullOrEmpty(txtSize1))
                    txtSize1 = "0";
                DataTable Dt = (DataTable)ViewState["dtMain"];
                if (Dt.Rows.Count == 0)
                {
                    DataRow drr = Dt.NewRow();
                    drr["HeadQtrNo"] = HddHeadQtrNo.Value;
                    drr["MID"] = hddMID.Value;
                    drr[hddSizeVal.Value] = Convert.ToInt32(txtSize1);
                    Dt.Rows.Add(drr);
                }
                else
                {
                    int numberOfRecords = Dt.AsEnumerable()
                        .Where(x => x["HeadQtrNo"].ToString() == HddHeadQtrNo.Value)
                        .Where(x => x["MID"].ToString() == hddMID.Value)
                        .ToList().Count;
                    if (numberOfRecords == 0)
                    {
                        DataRow drr = Dt.NewRow();
                        drr["HeadQtrNo"] = HddHeadQtrNo.Value;
                        drr["MID"] = hddMID.Value;
                        drr[hddSizeVal.Value] = Convert.ToInt32(txtSize1);
                        Dt.Rows.Add(drr);
                    }
                    else
                    {
                        DataRow drow = Dt.Select(("HeadQtrNo='" + HddHeadQtrNo.Value + "' and MID='" + hddMID.Value + "'"))[0];
                        int tempIndex = Dt.Rows.IndexOf(drow);
                        Dt.Rows[tempIndex][hddSizeVal.Value] = Convert.ToInt32(txtSize1);
                    }
                }
                ViewState["dtMain"] = Dt;
            }
        }
    }
}