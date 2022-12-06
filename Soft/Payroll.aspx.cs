using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_Payroll : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData Gd = new GetData();
    Data data = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            foreach (ListItem size in drpWorkingDay.Items)
            {
                if (size.Value.ToString() != "Sunday")
                {
                    size.Selected = true;
                }
            }

            Gd.FillCompany(DrpCompanies);
            Gd.fillDepartment(drpDepartment);
            Gd.FillUser(drpProjectManager);
        }
    }
      
     

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }

    protected void btnSaveNext_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
     
    protected void drpMarriedStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDOM.ReadOnly = (drpMarriedStatus.SelectedValue.ToString() == "Married") ? false : true;
    }
     
    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.fillDesignation(drpDesignation, drpDepartment.SelectedValue.ToString());
    }
     

}