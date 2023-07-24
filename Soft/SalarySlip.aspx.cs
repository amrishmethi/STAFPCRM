using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_SalarySlip : System.Web.UI.Page
{
    Data data = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
                GetSalarySlip(Request.QueryString["Id"]);
            else if (Session["SalaryId"] != null)
                GetSalarySlip(Session["SalaryId"].ToString());
        }
    }

    private void GetSalarySlip(string SalaryId)
    {
        SalaryId = SalaryId == "" ? "0" : SalaryId;
        DataSet dss = data.getDataSet("select * FROM GETSALARYSLIP where  id in (" + SalaryId + ")");
        repData.DataSource = dss;
        repData.DataBind();
    }
}