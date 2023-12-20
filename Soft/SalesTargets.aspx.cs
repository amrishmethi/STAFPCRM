using CuteEditor.Convertor;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            gd.FillPartyCategory(drpCatg);
            gd.FillAccounts(drpParty, drpCatg.SelectedValue, "29");
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

        //string qq = "Select distinct S.*,EMP.Emp_Name  from tbl_SalesTarge_new S LEFT JOIN tbl_EmpMaster EMP on EMP.CRMUserId=S.UserID and EMP.Delid=0 LEFT JOIN ACCOUNT A on A.Id in (select item from SplitString(S.partyId,',')) LEFT JOIN station ST on ST.StationNo=A.StationNo LEFT JOIN HeadQtrDistrict HDQ on HDQ.DistrictNo=ST.DistrictNo LEFT JOIN mastbyno mm on mm.MsNo=HDQ.HeadQtrNo and mm.MsSR='HDQ'"; 
        
        string qq = "Select distinct S.*  from tbl_SalesTarge_new S LEFT JOIN ACCOUNT A on A.Id in (select item from SplitString(S.partyId,',')) LEFT JOIN station ST on ST.StationNo=A.StationNo LEFT JOIN HeadQtrDistrict HDQ on HDQ.DistrictNo=ST.DistrictNo LEFT JOIN mastbyno mm on mm.MsNo=HDQ.HeadQtrNo and mm.MsSR='HDQ'";
        if (drpheadQtr.SelectedIndex > 0)
            qq += " and HDQ.HeadQtrNo=" + drpheadQtr.SelectedValue;

        //qq += " where 0=0   ";
        //if (drpDepartment.SelectedIndex > 0)
        //    qq += " and EMP.Dept_Id=" + drpDepartment.SelectedValue;
        //if (drpStatus.SelectedIndex > 0)
        //    qq += " and EMP.Status='" + drpStatus.SelectedValue + "'";
        DataSet dsTarget = data.getDataSet(qq);
        DataTable dtt = dsTarget.Tables[0];
        DataRow dr = dtt.NewRow();
        dr["PARTYID"] = "Total";
        dtt.Rows.Add(dr);
        ViewState["TargetData"] = dtt;

        FooterSession();
        SqlCommand cmd = new SqlCommand("PROC_GETPARTYLIST");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@USERID", drpUser.SelectedValue);
        cmd.Parameters.AddWithValue("@PARTYID", drpParty.SelectedValue);
        cmd.Parameters.AddWithValue("@CATID", drpCatg.SelectedValue);
        cmd.Parameters.AddWithValue("@AcScIsno", "29");
        DataSet dss = data.getDataSet(cmd);
        DataView dv = dss.Tables[0].DefaultView;
        repData.DataSource = dv.ToTable();
        repData.DataBind();

        DataTable dtTotal = GetFooter();
        RepFoot.DataSource = dtTotal;
        RepFoot.DataBind();
    }

    public static DataTable GetFooter()
    {
        DataTable dtTotal = (DataTable)HttpContext.Current.Session["Total"];
        foreach (DataRow drr in dtTotal.Rows)
        {
            DataTable dssw = (DataTable)HttpContext.Current.Session["dtMain"];
            var result = dssw.AsEnumerable()
                    .Sum(x => Convert.ToInt32(x[drr["sText"].ToString()]));
            drr["Qty"] = result;
        }
        dtTotal.AcceptChanges();
        return dtTotal;
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
        DataTable Dt = (DataTable)Session["dtMain"];
        foreach (DataRow drr in Dt.Rows)
        {
            if (drr["Whatsappno"].ToString() == "9982160414")
            {
                string a = "";
            }
            string q = "", _ColumnName = "", _ColumnValue = "";
            if (data.Exist("select * from tbl_SalesTarge_new where PartyId='" + drr["PartyId"] + "' "))
            {
                q = "update tbl_SalesTarge_new SET ModifyDate=Getdate() ";
                foreach (DataRow drCol in dtColumn.Rows)
                {
                    string ss = drCol["SS"].ToString().Replace(' ', '_').ToString();
                    q += ", [" + ss + "] = " + drr[drCol["SS"].ToString()].ToString() + "";
                }
                //q += " where PartyId='" + drr["PartyId"] + "' and Whatsappno='" + drr["Whatsappno"] + "' and PTCMSNO='" + drr["PTCMSNO"] + "' ";
                q += " where PartyId='" + drr["PartyId"] + "' and Whatsappno='" + drr["Whatsappno"] + "'  ";
            }
            else
            {
                foreach (DataRow drCol in dtColumn.Rows)
                {
                    string ss = drCol["SS"].ToString().Replace(' ', '_').ToString();
                    _ColumnName += ", [" + ss + "]";
                    _ColumnValue += ", '" + drr[drCol["SS"].ToString()].ToString() + "'";
                }
                q = " insert into tbl_SalesTarge_new (USERID,PartyId,Whatsappno,PTCMSNO,TargetMonth" + _ColumnName + ")";
                q += " Values('" + drpUser.SelectedValue + "','" + drr["PartyId"] + "','" + drr["Whatsappno"] + "','" + drr["PTCMSNO"] + "','" + _DD + "'" + _ColumnValue + ")";
            }
            data.executeCommand(q);
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Sales Target Saved Successfully');window.location ='SalesTargets.aspx'", true);

    }


    public void createTable()
    {
        dtGrp.Columns.Add("sText");
        dtGrp.Columns.Add("Qty");

        DataSet dss = (DataSet)ViewState["Columns"];
        foreach (DataRow drr in dss.Tables[0].Rows)
        {
            DataRow dr = dtGrp.NewRow();
            dr["sText"] = drr["SS"];
            dr["Qty"] = "0";
            dtGrp.Rows.Add(dr);
        }
        Session["Total"] = dtGrp;
        repHead.DataSource = dtGrp;
        repHead.DataBind();

        FooterSession();
    }

    private void FooterSession()
    {
        if (Session["dtMain"] == null)
        {
            Dt.Columns.Add("PartyId", typeof(string));
            Dt.Columns.Add("Whatsappno", typeof(string));
            Dt.Columns.Add("PTCMSNO", typeof(string));
            for (int k = 0; k < dtGrp.Rows.Count; k++)
            {
                string ff = dtGrp.Rows[k]["sText"].ToString();
                Dt.Columns.Add(ff, typeof(string));
            }

            Session["dtMain"] = Dt;
        }
        else
        {
            ((DataTable)Session["dtMain"]).Rows.Clear();
        }
    }

    protected void repData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater repd = (Repeater)e.Item.FindControl("repd");
                DataSet dss = (DataSet)ViewState["Columns"];
                foreach (DataRow dd in dss.Tables[0].Rows)
                {
                    dd["Qty"] = "0";
                }

                DataView dvvTo = ((DataTable)Session["Total"]).DefaultView;
                HiddenField HddHeadQtrNo = (HiddenField)e.Item.FindControl("HddHeadQtrNo");
                HiddenField HddNo = (HiddenField)e.Item.FindControl("HddNo");
                HiddenField hddMID = (HiddenField)e.Item.FindControl("hddMID");
                if (HddNo.Value == "8003869991")
                {
                }
                DataView dvv = ((DataTable)ViewState["TargetData"]).DefaultView;
                dvv.RowFilter = "PARTYID='" + hddMID.Value.Trim() + "'";
                DataTable dtt = dvv.ToTable();
                if (dtt.Rows.Count > 0)
                {
                    foreach (DataRow drr in dss.Tables[0].Rows)
                        drr["Qty"] = dtt.Rows[0][drr["Name"].ToString()];

                }
                dss.Tables[0].AcceptChanges();
                DataTable DtMain = ((DataTable)Session["dtMain"]);
                DataRow drrMian = DtMain.NewRow();
                drrMian["PartyId"] = hddMID.Value;
                drrMian["Whatsappno"] = HddNo.Value;
                
                drrMian["PTCMSNO"] = HddHeadQtrNo.Value;

                foreach (DataRow drr in dss.Tables[0].Rows)
                    if (dtt.Rows.Count > 0)
                        drrMian[drr["SS"].ToString()] = dtt.Rows[0][drr["Name"].ToString()];
                    else
                        drrMian[drr["SS"].ToString()] = "0";
                DtMain.Rows.Add(drrMian);
                Session["dtMain"] = DtMain;
                repd.DataSource = dss.Tables[0];
                repd.DataBind();
            }
        }
        catch (Exception ex)
        { }
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
                DataTable Dt = (DataTable)Session["dtMain"];
                if (Dt.Rows.Count == 0)
                {
                    DataRow drr = Dt.NewRow();
                    drr["PartyId"] = hddMID.Value;
                    drr[hddSizeVal.Value] = Convert.ToInt32(txtSize1);
                    Dt.Rows.Add(drr);
                }
                else
                {
                    int numberOfRecords = Dt.AsEnumerable()
                        .Where(x => x["PartyId"].ToString() == hddMID.Value)
                        .ToList().Count;
                    if (numberOfRecords == 0)
                    {
                        DataRow drr = Dt.NewRow();
                        drr["PartyId"] = hddMID.Value;
                        drr[hddSizeVal.Value] = Convert.ToInt32(txtSize1);
                        Dt.Rows.Add(drr);
                    }
                    else
                    {
                        DataRow drow = Dt.Select(("PartyId='" + hddMID.Value + "'"))[0];
                        int tempIndex = Dt.Rows.IndexOf(drow);
                        Dt.Rows[tempIndex][hddSizeVal.Value] = Convert.ToInt32(txtSize1);
                    }
                }
                Session["dtMain"] = Dt;
            }
        }
    }

    protected void drpCatg_SelectedIndexChanged(object sender, EventArgs e)
    {
        gd.FillAccounts(drpParty, drpCatg.SelectedValue, "29");
    }


    [WebMethod]
    public static string txtRep_TextChanged(string Id, string Val)
    {
        DataTable Dt = (DataTable)HttpContext.Current.Session["dtMain"];
        int _RowID = Convert.ToInt32(Id.Split('_')[3]);
        int _ColumnID = Convert.ToInt32(Id.Split('_')[5]) + 3;
        DataRow drr = Dt.Rows[_RowID];
        drr[_ColumnID] = Val;
        Dt.AcceptChanges();
        HttpContext.Current.Session["dtMain"] = Dt;
        int j = Dt.Rows.Count - 1;
        int _total = Convert.ToInt32(Dt.AsEnumerable().Sum(x => Convert.ToInt32(x[_ColumnID])));
        return Convert.ToInt32(Id.Split('_')[5]) + "," + _total;

    }
}