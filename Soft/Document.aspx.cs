using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_Document : System.Web.UI.Page
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
        ds = master.GetDocument("Select", Dept_Id, "", "");
        txtDocumentname.Text = ds.Tables[0].Rows[0]["Docu_Name"].ToString();
        btnSaveExit.Text = "Update";
    }

    public void FillData()
    {
        ds = master.GetDocument("Select", "0", "", "");
        repDocument.DataSource = ds;
        repDocument.DataBind();
    }

    protected void btnSaveExit_Click(object sender, EventArgs e)
    {
        
        string _Id = (Request.QueryString["ID"] != null) ? Request.QueryString["ID"].ToString() : "0";
        string _Action = (Request.QueryString["ID"] != null) ? "UPDATE" : "SAVE";

        ds = master.GetDocument(_Action, _Id, txtDocumentname.Text, "0");
        string _Path = (ds.Tables[0].Rows[0]["RESULT"].ToString() == "0") ? "window.location ='Document.aspx'" : "";
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + ds.Tables[0].Rows[0]["RESULTMSG"].ToString() + "');" + _Path, true);
    }

    protected void repDocument_ItemCommand(object source, RepeaterCommandEventArgs e)
    { 
        if (e.CommandName == "Delete")
        {
            ds = master.GetDocument("Delete", e.CommandArgument.ToString(), "", "");
            string _Path = (ds.Tables[0].Rows[0]["RESULT"].ToString() == "0") ? "window.location ='Document.aspx'" : "";
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + ds.Tables[0].Rows[0]["RESULTMSG"].ToString() + "');" + _Path, true);
        }
    }
}