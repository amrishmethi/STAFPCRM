using System;
using System.Web;
using System.Web.UI;

public partial class Admin_UserControls_TopMenu : System.Web.UI.UserControl
{
    HttpCookie Soft;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["STFP"] != null)
            {
                if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }
               
                //Soft = Request.Cookies["STFP"];
                //lblName.Text = Soft.Values["UserName"].ToString();
                //  profileuser.HRef = "../EmployeeMaster.aspx?id=" + Soft.Values["UserID"] +"&view=true";
            }
        }
    }
}