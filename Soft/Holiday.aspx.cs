using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_Holiday : System.Web.UI.Page
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
        ds = master.GetHoliday("Select", Dept_Id, "", "", "", "");
        txtHolidayName.Text = ds.Tables[0].Rows[0]["HOLIDAY_NAME"].ToString();
        txtDateFrom.Text = ds.Tables[0].Rows[0]["DATEFROM"].ToString();
        txtDateTo.Text = ds.Tables[0].Rows[0]["DATETo"].ToString();
        btnSaveExit.Text = "Update";
    }

    public void FillData()
    {
        ds = master.GetHoliday("Select", "0", "", "", "", "");
        repHoliday.DataSource = ds;
        repHoliday.DataBind();
    }

    protected void btnSaveExit_Click(object sender, EventArgs e)
    {
        string _Id = (Request.QueryString["ID"] != null) ? Request.QueryString["ID"].ToString() : "0";
        string _Action = (Request.QueryString["ID"] != null) ? "UPDATE" : "SAVE";
        string DATEFROM = data.ConvertToDateTime(txtDateFrom.Text).ToString();
        string DATETo = data.ConvertToDateTime(txtDateTo.Text).ToString();
        ds = master.GetHoliday(_Action, _Id, txtHolidayName.Text, "0", DATEFROM, DATETo);
        string _Path = (ds.Tables[0].Rows[0]["RESULT"].ToString() == "0") ? "window.location ='Holiday.aspx'" : "";
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + ds.Tables[0].Rows[0]["RESULTMSG"].ToString() + "');" + _Path, true);
    }

    protected void repHoliday_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            ds = master.GetHoliday("Delete", e.CommandArgument.ToString(), "", "", "", "");
            string _Path = (ds.Tables[0].Rows[0]["RESULT"].ToString() == "0") ? "window.location ='Holiday.aspx'" : "";
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + ds.Tables[0].Rows[0]["RESULTMSG"].ToString() + "');" + _Path, true);
        }
    }
}