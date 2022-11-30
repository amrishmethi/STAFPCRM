using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_Designation : System.Web.UI.Page
{
    GetData Gd = new GetData();
    Master master = new Master();
    Data data = new Data();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
                BindData(Request.QueryString["ID"].ToString());

            FillData();
            Gd.fillDepartment(drpDepartment);
        }
    }

    private void BindData(string Dept_Id)
    {
        ds = master.GetDesignation("Select", Dept_Id, "0", "", "");
        drpDepartment.SelectedValue = ds.Tables[0].Rows[0]["Dept_Id"].ToString();
        txtDepartmentname.Text = ds.Tables[0].Rows[0]["Desg_Name"].ToString();
        btnSaveExit.Text = "Update";
    }

    public void FillData()
    {
        ds = master.GetDesignation("Select", "0", "0", "", "");
        repDepartment.DataSource = ds;
        repDepartment.DataBind();
    }

    protected void btnSaveExit_Click(object sender, EventArgs e)
    {
        string _Id = (Request.QueryString["ID"] != null) ? Request.QueryString["ID"].ToString() : "0";
        string _Action = (Request.QueryString["ID"] != null) ? "UPDATE" : "SAVE";

        ds = master.GetDesignation(_Action, _Id, drpDepartment.SelectedValue, txtDepartmentname.Text, "0");
        string _Path = (ds.Tables[0].Rows[0]["RESULT"].ToString() == "0") ? "window.location ='Designation.aspx'" : "";
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + ds.Tables[0].Rows[0]["RESULTMSG"].ToString() + "');" + _Path, true);
    }

    protected void repDepartment_ItemCommand(object source, RepeaterCommandEventArgs e)
    { 
        if (e.CommandName == "Delete")
        {
            ds = master.GetDesignation("Delete", e.CommandArgument.ToString(), "", "", "");
            string _Path = (ds.Tables[0].Rows[0]["RESULT"].ToString() == "0") ? "window.location ='Designation.aspx'" : "";
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + ds.Tables[0].Rows[0]["RESULTMSG"].ToString() + "');" + _Path, true);
        }
    }
}