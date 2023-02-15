using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mailjet.Client.Resources.SMS;
using Mailjet.Client.Resources;

public partial class Soft_AdvanceSallary : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    Data data = new Data();
    GetData Gd = new GetData();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            Gd.FillUser(drpEmployee);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdvaneSallaryRep.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdvaneSallaryRep.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string _Action = Request.QueryString["Id"] == null ? "SAVE" : "UPDATE";
        string _Id = Request.QueryString["Id"] == null ? "0" : Request.QueryString["Id"];
        string _UserId = Soft["UserId"];

        DataSet dss = IUD_AdvanceSalary(_Action, _Id, txtVocNo.Text, txtDate.Text, drpEmployee.SelectedValue.ToString(), txtRemarks.Text, txtAmount.Text, "0", _UserId);

        if (dss.Tables[0].Rows[0]["Result"].ToString() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record " + _Action + " Successfully');window.location ='AdvaneSallaryRep.aspx'", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Not Save Successfully');window.location ='AdvaneSallaryRep.aspx'", true);
        }
    }


    private DataSet IUD_AdvanceSalary(string ACTION, string ID, string VOC_NO, string VOC_DATE, string EMP_ID, string REMARKS, string AMOUNT, string DELID, string USERID)
    {
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
        cmd.Parameters.AddWithValue("@USERID", USERID);
        DataSet ds = data.getDataSet(cmd);
        return ds;
    }
}