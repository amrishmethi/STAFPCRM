using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AttendanceReport : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    Data data = new Data();
    GetData Gd = new GetData();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("AttendanceReport.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            //dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            Gd.FillUser(drpUser);
            Gd.fillDepartment(drpDept);
            fillData();
        }
    }

    public void fillData()
    {
        ds = getdata.getAttendanceList(drpUser.SelectedValue, drpDept.SelectedValue, txtDate.Text.Trim());
        DataView dv = ds.Tables[0].DefaultView;
        if (drpIsAttend.SelectedValue == "0") { dv.RowFilter = " DateIN is null"; }
        else if (drpIsAttend.SelectedValue == "1") { dv.RowFilter = "DateIN <> ''"; }

        rep.DataSource = dv.ToTable();
        rep.DataBind();
    }


    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    fillData();
    //}

    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }
    //private string ShrinkURL(string strURL)
    //{
    //    string URL;
    //    URL = "http://tinyurl.com/api-create.php?url=" +
    //       strURL.ToLower();

    //    System.Net.HttpWebRequest objWebRequest;
    //    System.Net.HttpWebResponse objWebResponse;

    //    System.IO.StreamReader srReader;

    //    string strHTML;

    //    objWebRequest = (System.Net.HttpWebRequest)System.Net
    //       .WebRequest.Create(URL);
    //    objWebRequest.Method = "GET";

    //    objWebResponse = (System.Net.HttpWebResponse)objWebRequest
    //       .GetResponse();
    //    srReader = new System.IO.StreamReader(objWebResponse
    //       .GetResponseStream());

    //    strHTML = srReader.ReadToEnd();

    //    srReader.Close();
    //    objWebResponse.Close();
    //    objWebRequest.Abort();

    //    return (strHTML);

    //}



    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillData();
    }
}