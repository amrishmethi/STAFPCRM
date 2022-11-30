using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_Department : System.Web.UI.Page
{
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
        }
    }

    private void BindData(string Dept_Id)
    {
        ds = master.GetDepartment("Select", Dept_Id, "", "", "");
        txtDepartmentname.Text = ds.Tables[0].Rows[0]["DEPT_NAME"].ToString();
        txtCode.Text = ds.Tables[0].Rows[0]["DEPT_CODE"].ToString();
        btnSaveExit.Text = "Update";
    }

    public void FillData()
    {
        ds = master.GetDepartment("Select", "0", "", "", "");
        repDepartment.DataSource = ds;
        repDepartment.DataBind();
    }

    protected void btnSaveExit_Click(object sender, EventArgs e)
    {

        string _Id = (Request.QueryString["ID"] != null) ? Request.QueryString["ID"].ToString() : "0";
        string _Action = (Request.QueryString["ID"] != null) ? "UPDATE" : "SAVE";

        ds = master.GetDepartment(_Action, _Id, txtDepartmentname.Text, "0", txtCode.Text);
        string _Path = (ds.Tables[0].Rows[0]["RESULT"].ToString() == "0") ? "window.location ='Department.aspx'" : "";
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + ds.Tables[0].Rows[0]["RESULTMSG"].ToString() + "');" + _Path, true);
    }

    protected void repDepartment_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            ds = master.GetDepartment("Delete", e.CommandArgument.ToString(), "", "", "");
            string _Path = (ds.Tables[0].Rows[0]["RESULT"].ToString() == "0") ? "window.location ='Department.aspx'" : "";
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + ds.Tables[0].Rows[0]["RESULTMSG"].ToString() + "');" + _Path, true);
        }
    }
}