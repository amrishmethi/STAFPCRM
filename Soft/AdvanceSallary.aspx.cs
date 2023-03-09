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
using System.Security.Cryptography;

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
            if (Request.QueryString["Id"] != null)
                FillData(Request.QueryString["Id"].ToString());
        }
    }

    private void FillData(string Id)
    {
        DataSet dss = getdata.IUD_AdvanceSalary("SELECT", Id, "", "", "", "", "", "", "");
        txtVocNo.Text = dss.Tables[0].Rows[0]["Voc_No"].ToString();
        drpEmployee.SelectedValue = dss.Tables[0].Rows[0]["EMP_ID"].ToString();
        txtDate.Text = dss.Tables[0].Rows[0]["Voc_Date1"].ToString();
        txtAmount.Text = dss.Tables[0].Rows[0]["Amount"].ToString();
        txtRemarks.Text = dss.Tables[0].Rows[0]["Remarks"].ToString();
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

        DataSet dss = getdata.IUD_AdvanceSalary(_Action, _Id, txtVocNo.Text, txtDate.Text, drpEmployee.SelectedValue.ToString(), txtRemarks.Text, txtAmount.Text, "0", _UserId);

        if (dss.Tables[0].Rows[0]["Result"].ToString() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record " + _Action + " Successfully');window.location ='AdvaneSallaryRep.aspx'", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Not Save Successfully');window.location ='AdvaneSallaryRep.aspx'", true);
        }
    }


   
}