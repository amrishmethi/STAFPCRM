using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_AdminPolicy : System.Web.UI.Page
{
    Master master = new Master();
    Data data = new Data();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dpDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            if (Request.QueryString["ID"] != null)
                BindData(Request.QueryString["ID"].ToString());

            FillData();
        }
    }

    private void BindData(string policy_Id)
    {
        ds = master.GetAdminPolicy("Select", policy_Id, "", "", "");
        txtPolicyname.Text = ds.Tables[0].Rows[0]["POLICY_NAME"].ToString();
        dpDate.Text = ds.Tables[0].Rows[0]["PDate"].ToString();
        btnSaveExit.Text = "Update";
    }

    public void FillData()
    {
        ds = master.GetAdminPolicy("Select", "0", "", "", "");
        repAdminPolicy.DataSource = ds;
        repAdminPolicy.DataBind();
    }

    protected void btnSaveExit_Click(object sender, EventArgs e)
    {

        string _Id = (Request.QueryString["ID"] != null) ? Request.QueryString["ID"].ToString() : "0";
        string _Action = (Request.QueryString["ID"] != null) ? "UPDATE" : "SAVE";

        ds = master.GetAdminPolicy(_Action, _Id, txtPolicyname.Text, "0", dpDate.Text);
        string _Path = (ds.Tables[0].Rows[0]["RESULT"].ToString() == "0") ? "window.location ='AdminPolicy.aspx'" : "";
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + ds.Tables[0].Rows[0]["RESULTMSG"].ToString() + "');" + _Path, true);
    }

    protected void repAdminPolicy_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            ds = master.GetAdminPolicy("Delete", e.CommandArgument.ToString(), "", "", "");
            string _Path = (ds.Tables[0].Rows[0]["RESULT"].ToString() == "0") ? "window.location ='AdminPolicy.aspx'" : "";
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + ds.Tables[0].Rows[0]["RESULTMSG"].ToString() + "');" + _Path, true);
        }
    }
}