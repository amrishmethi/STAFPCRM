using System;
using System.Web;
using System.Web.UI;

public partial class Admin_UserControls_TopMenu : System.Web.UI.UserControl
{
    HttpCookie Soft;
    Data data = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["STFP"] != null)
            {
                if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }
               
                Soft = Request.Cookies["STFP"];
                lblName.Text = Soft.Values["UserName"].ToString();
                //  profileuser.HRef = "../EmployeeMaster.aspx?id=" + Soft.Values["UserID"] +"&view=true";

                lblUpdate.InnerText = data.getDataSet("select Format(max(webdate),'dd-MMM-yyyy hh:mm tt')webdate from glos  ").Tables[0].Rows[0]["webdate"].ToString();
            }
        }
    }
}