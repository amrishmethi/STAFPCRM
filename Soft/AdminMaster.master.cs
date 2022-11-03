using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] != null)
            {
                string sRet = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                Session["Page"] = sRet;
            }
            else
            {
                Response.Redirect("../login.aspx");
            }
        }
    }
}
