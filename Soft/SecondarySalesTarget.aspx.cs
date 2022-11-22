using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_SecondarySalesTarget : System.Web.UI.Page
{
    Master getdata = new Master();
    int SNO;
    Data data = new Data();
    DataTable dtRecord = new DataTable();
    Master master = new Master();
    DataSet dsResult = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillEmployee();
            FillItemGroup();
            if (Request.QueryString["DID"] != null)
                FillData(Request.QueryString["DID"]);
        }
    }

    private void FillData(string Detail_ID)
    {
        string query = "select EMPID from [GETSECONDARYSALESTARGET_VIEW] Where DETAILID=" + Detail_ID;
        dsResult = data.getDataSet(query);

        BinDatat(dsResult.Tables[0].Rows[0]["EMPID"].ToString());
    }

    private void BinDatat(string EmpID)
    {
        DataSet dss = data.getDataSet("select * from [GETSECONDARYSALESTARGET_VIEW] where EMPID=" + EmpID);
        if (dss.Tables[0].Rows.Count > 0)
        {
            hddMainId.Value = dss.Tables[0].Rows[0]["MAINID"].ToString();
            drpEmployee.SelectedValue = dss.Tables[0].Rows[0]["EMPID"].ToString();
            drpEmployee.Enabled = false;
            txtDate.Text = dss.Tables[0].Rows[0]["TARGET_DATE"].ToString();
            txtMinVisit.Text = dss.Tables[0].Rows[0]["MINVISIT"].ToString();
            txtAmount.Text = dss.Tables[0].Rows[0]["AMOUNT"].ToString();
            btnSaveExit.Text = "Update";

            if (ViewState["tbl"] == null)
            {
                dtRecord.Columns.Add("sno");
                dtRecord.Columns.Add("Id");
                dtRecord.Columns.Add("ItemGroup");
                dtRecord.Columns.Add("ItemGroupId");
                dtRecord.Columns.Add("Qty", typeof(int));
                dtRecord.Columns.Add("Incentive", typeof(int));
                dtRecord.Columns.Add("Delid");
            }
            foreach (DataRow drr in dss.Tables[0].Rows)
            {
                SNO++;
                DataRow dtrow = dtRecord.NewRow();
                dtrow["SNO"] = SNO;
                dtrow["Id"] = drr["DETAILID"];
                dtrow["ItemGroupId"] = drr["ITEMGROUP"];
                dtrow["ItemGroup"] = drr["ITEMGROUP"];
                dtrow["Qty"] = drr["QTY"];
                dtrow["Incentive"] = drr["Incentive"];
                dtrow["Delid"] = "0";
                dtRecord.Rows.Add(dtrow);
                ViewState["tbl"] = dtRecord;
            }
            repData.DataSource = dtRecord;
            repData.DataBind();
        }
    }

    private void FillItemGroup()
    {
        DataSet dsusr = data.getDataSet("usp_API_ITEMGROUP 930185018");
        drpItemGrup.DataSource = dsusr;
        drpItemGrup.DataTextField = "CmsName";
        drpItemGrup.DataValueField = "CmsName";
        drpItemGrup.DataBind();
        drpItemGrup.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillEmployee()
    {
        DataSet dsusr = getdata.getHqtrUser();
        drpEmployee.DataSource = dsusr.Tables[0].DefaultView.ToTable(true, "Name", "Mid");
        drpEmployee.DataTextField = "Name";
        drpEmployee.DataValueField = "Mid";
        drpEmployee.DataBind();
        drpEmployee.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (btnAdd.Text == "Add")
        {
            if (ViewState["tbl"] == null)
            {
                SNO = 1;
                dtRecord.Columns.Add("sno");
                dtRecord.Columns.Add("Id");
                dtRecord.Columns.Add("ItemGroup");
                dtRecord.Columns.Add("ItemGroupId");
                dtRecord.Columns.Add("Qty", typeof(int));
                dtRecord.Columns.Add("Incentive", typeof(int));
                dtRecord.Columns.Add("Delid");
            }
            else
            {
                dtRecord = (DataTable)ViewState["tbl"];
                SNO = dtRecord.Rows.Count + 1;
            }

            DataRow[] foundAuthors = dtRecord.Select("ItemGroup = '" + drpItemGrup.Text + "'");
            if (foundAuthors.Length == 0)
            {
                DataRow dtrow = dtRecord.NewRow();
                dtrow["SNO"] = SNO;
                dtrow["Id"] = HddRowID.Value;
                dtrow["ItemGroupId"] = drpItemGrup.SelectedValue;
                dtrow["ItemGroup"] = drpItemGrup.Text;
                dtrow["Qty"] = txtQty.Text;
                dtrow["Incentive"] = txtIncentive.Text;
                dtrow["Delid"] = "0";
                dtRecord.Rows.Add(dtrow);
                ViewState["tbl"] = dtRecord;
                repData.DataSource = dtRecord;
                repData.DataBind();
            }
            else
                errmsg.InnerText = "* " + drpItemGrup.Text + " Already Exists";
        }
        else
        {
            dtRecord = (DataTable)ViewState["tbl"];
            DataRow[] foundAuthors = dtRecord.Select("ItemGroup = '" + drpItemGrup.Text + "' and Id<>" + HddRowID.Value);
            if (foundAuthors.Length == 0)
            {
                int rowind = Convert.ToInt32(ViewState["rowid"].ToString());
                dtRecord.Rows[rowind]["Id"] = HddRowID.Value;
                dtRecord.Rows[rowind]["ItemGroupId"] = drpItemGrup.SelectedValue;
                dtRecord.Rows[rowind]["ItemGroup"] = drpItemGrup.Text;
                dtRecord.Rows[rowind]["Incentive"] = txtIncentive.Text;
                dtRecord.Rows[rowind]["Qty"] = txtQty.Text;
                dtRecord.Rows[rowind]["Delid"] = "0";
                repData.DataSource = dtRecord;
                repData.DataBind();
            }
        }
        clear();
    }

    private void clear()
    {
        HddRowID.Value = "0";
        drpItemGrup.SelectedIndex = 0;
        txtQty.Text = "0";
        txtIncentive.Text = "0";
        btnAdd.Text = "Add";
    }

    protected void repData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Label lblQty = (Label)e.Item.FindControl("lblQty");
            Label lblIncentive = (Label)e.Item.FindControl("lblIncentive");
            HiddenField hddID = (HiddenField)e.Item.FindControl("hddID");
            HiddenField HddItemGroup = (HiddenField)e.Item.FindControl("HddItemGroup");

            drpItemGrup.SelectedValue = HddItemGroup.Value;
            txtQty.Text = lblQty.Text;
            txtIncentive.Text = lblIncentive.Text;
            HddRowID.Value = hddID.Value;
            btnAdd.Text = "Update";

            ViewState["rowid"] = e.Item.ItemIndex;
        }

        if (e.CommandName == "Remove")
        {
            string FiD = e.CommandArgument.ToString();
            string Srnoo = "", _ID = "";
            DataTable tbl = (DataTable)ViewState["tbl"];
            for (int k = 0; k < tbl.Rows.Count; k++)
            {
                Srnoo = tbl.Rows[k]["SNO"].ToString();
                _ID = tbl.Rows[k]["ID"].ToString();
                if (Srnoo == FiD)
                {
                    if (_ID == "0")
                        tbl.Rows[k].Delete();
                    else
                        tbl.Rows[k]["Delid"] = "1";
                }
            }
            ViewState["tbl"] = tbl;
            repData.DataSource = tbl;
            repData.DataBind();
        }
    }

    protected void btnSaveExit_Click(object sender, EventArgs e)
    {
        Save_Modify();
    }

    private void Save_Modify()
    {
        if (ViewState["tbl"] != null)
        {
            dtRecord = (DataTable)ViewState["tbl"];
            int _TotalQty = Convert.ToInt32(dtRecord.Compute("sum(Qty)", ""));

            DataSet dssTargetMain = master.GetSecondarySaleTargetMain(drpEmployee.SelectedValue.ToString(), data.ConvertToDateTime(txtDate.Text).ToString(), txtMinVisit.Text, _TotalQty.ToString(), hddMainId.Value, txtAmount.Text);
            if (dssTargetMain.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drr in dtRecord.Rows)
                {
                    dsResult = master.GetSecondarySaleTargetDetails(dssTargetMain.Tables[0].Rows[0]["RESULTT"].ToString(), drr["ItemGroup"].ToString(), drr["Qty"].ToString(), drr["Id"].ToString(), drr["Delid"].ToString(), drr["Incentive"].ToString());
                }
            }
            if (dsResult.Tables[0].Rows[0]["RESULTT"].ToString() != "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Save Successfully');window.location ='SecondarySalesTargetReport.aspx'", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Not Save Successfully')'", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Insert Atleast 1 Record')", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("SecondarySalesTargetReport.aspx");
    }

    protected void drpEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        BinDatat(drpEmployee.SelectedValue.ToString());
    }
}