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
        drpEmployee.DataSource = dsusr.Tables[0].DefaultView.ToTable(true, "Name", "id");
        drpEmployee.DataTextField = "Name";
        drpEmployee.DataValueField = "id";
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
                dtRecord.Columns.Add("Delid");
            }
            else
            {
                dtRecord = (DataTable)ViewState["tbl"];
                SNO = dtRecord.Rows.Count + 1;
            }
            DataRow dtrow = dtRecord.NewRow();
            dtrow["SNO"] = SNO;
            dtrow["Id"] = HddRowID.Value;
            dtrow["ItemGroupId"] = drpItemGrup.SelectedValue;
            dtrow["ItemGroup"] = drpItemGrup.Text;
            dtrow["Qty"] = txtQty.Text;
            dtrow["Delid"] = "0";
            dtRecord.Rows.Add(dtrow);
            ViewState["tbl"] = dtRecord;
            repData.DataSource = dtRecord;
            repData.DataBind();
        }
        else
        {
            dtRecord = (DataTable)ViewState["tbl"];
            int rowind = Convert.ToInt32(ViewState["rowid"].ToString());
            dtRecord.Rows[rowind]["Id"] = HddRowID.Value;
            dtRecord.Rows[rowind]["ItemGroupId"] = drpItemGrup.SelectedValue;
            dtRecord.Rows[rowind]["ItemGroup"] = drpItemGrup.Text;
            dtRecord.Rows[rowind]["Qty"] = txtQty.Text;
            dtRecord.Rows[rowind]["Delid"] = "0";
            repData.DataSource = dtRecord;
            repData.DataBind();
        }
        clear();
    }

    private void clear()
    {
        HddRowID.Value = "0";
        drpItemGrup.SelectedIndex = 0;
        txtQty.Text = "0";
        btnAdd.Text = "Add";
    }

    protected void repData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Label lblQty = (Label)e.Item.FindControl("lblQty");
            HiddenField hddID = (HiddenField)e.Item.FindControl("hddID");
            HiddenField HddItemGroup = (HiddenField)e.Item.FindControl("HddItemGroup");

            drpItemGrup.SelectedValue = HddItemGroup.Value;
            txtQty.Text = lblQty.Text;
            HddRowID.Value = hddID.Value;
            btnAdd.Text = "Update";
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

            DataSet dssTargetMain = master.GetSecondarySaleTargetMain(drpEmployee.SelectedValue.ToString(), data.ConvertToDateTime(txtDate.Text).ToString(), _TotalQty.ToString(), txtMinVisit.Text, hddMainId.Value);
            if (dssTargetMain.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drr in dtRecord.Rows)
                {
                    dsResult = master.GetSecondarySaleTargetDetails(dssTargetMain.Tables[0].Rows[0]["RESULTT"].ToString(), drr["ItemGroup"].ToString(), drr["Qty"].ToString(), drr["Id"].ToString(), drr["Delid"].ToString());
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
}