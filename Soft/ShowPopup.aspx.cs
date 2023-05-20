using Org.BouncyCastle.Ocsp;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_ShowPopup : System.Web.UI.Page
{
    Master master = new Master();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["EMP_ID"] != null)
            {
                DataTable dt = (DataTable)Session["Salary"];
                DataView dvv = dt.DefaultView;
                dvv.RowFilter = "EMP_ID=" + Request.QueryString["EMP_ID"];

                DataTable dtNew = dvv.ToTable();
                rptUserViewAttendance.DataSource = dtNew;
                rptUserViewAttendance.DataBind();

                rep.DataSource = dtNew;
                rep.DataBind();

                lblName.InnerHtml = dtNew.Rows[0]["Emp_Name"].ToString();
            }
        }
    }
}