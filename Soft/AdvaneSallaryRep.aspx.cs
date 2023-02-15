using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Soft_AdvaneSallaryRep : System.Web.UI.Page
{
    private HttpCookie Soft;
    Data data = new Data();
    DataSet dsResult = new DataSet();
    GetData Gd = new GetData();

    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            Gd.FillUser(drpEmployee);
            GetReport();
        }
    }

    private void GetReport()
    {
        DataSet dss = IUD_AdvanceSalary("Select", "", "", "", "", "", "", "");
        rep.DataSource = dss;
        rep.DataBind();
    }

    private DataSet IUD_AdvanceSalary(string ACTION, string ID, string VOC_NO, string VOC_DATE, string EMP_ID, string REMARKS, string AMOUNT, string DELID)
    {
        string _UserId = Soft["UserId"];
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "IU_ADVANCESALARY";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@ID", ID);
        cmd.Parameters.AddWithValue("@VOC_NO", VOC_NO);
        cmd.Parameters.AddWithValue("@VOC_DATE", VOC_DATE);
        cmd.Parameters.AddWithValue("@EMP_ID", EMP_ID);
        cmd.Parameters.AddWithValue("@REMARKS", REMARKS);
        cmd.Parameters.AddWithValue("@AMOUNT", AMOUNT);
        cmd.Parameters.AddWithValue("@DELID", DELID);
        cmd.Parameters.AddWithValue("@USERID", _UserId);
        DataSet ds = data.getDataSet(cmd);
        return ds;
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            DataSet dss = IUD_AdvanceSalary("DELETE", e.CommandArgument.ToString(), "", "", "", "", "", "1");
            if (dss.Tables[0].Rows[0]["Result"].ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record DELETE Successfully');window.location ='AdvaneSallaryRep.aspx'", true);
            }
        }
    }
}