using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_AttandanceCalander : System.Web.UI.Page
{
    protected string mnta = "";
    protected string yr = "";
    string mntyr = "";
    protected DataTable FData = new DataTable();
    protected DataTable FData1 = new DataTable();
    private HttpCookie Soft;
    Data data = new Data();
    DataSet dsResult = new DataSet();
    GetData Gd = new GetData();
    Master master = new Master();
    protected void Page_Load(object sender, EventArgs e)
    {
        Soft = Request.Cookies["STFP"];
        if (!IsPostBack)
        {
            mnth.Text = DateTime.Now.ToString("MM-yyyy");
            Gd.fillDepartment(drpDepartment);
            Gd.FillUser(drpEmployee, drpDepartment.SelectedValue);
        }
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        mntyr = (mnth.Text == "") ? DateTime.Now.ToString("MM-yyyy") : mnth.Text;
        mnta = mntyr.Split('-')[0];
        yr = mntyr.Split('-')[1];

        string CRMUSERID = data.getDataSet("select CRMUserId from tbl_EMpMaster WHERE EmpId=" + drpEmployee.SelectedValue).Tables[0].Rows[0]["CRMUserId"].ToString();

        string query = "select convert(nvarchar(11),AttendancedateIn,103)ss,* from Attendance where IsDeleted=0 ANd Month(AttendanceDateIn)=" + mnta + " and Year(AttendanceDateIn)=" + yr + " ";
        if (CRMUSERID == "0")
            query += " and (EmpID=" + drpEmployee.SelectedValue + " )";
        else
            query += " and (EmpID=" + drpEmployee.SelectedValue + " or USERID=" + CRMUSERID + ")";

        query += " order by AttendancedateIn";
        DataSet dss = data.getDataSet(query);
        FData = dss.Tables[0];


        DataTable dsHoliday = data.getDataSet("select * from GETHOLIDAYLIST_VIEW where Month(DATEFROM)=" + mnta + " and Year(DATEFROM)=" + yr + " order by DATEFROM").Tables[0];
        DataTable dtSunday = data.getDataSet("PROC_GETSUNDAYOFMONTH '" + mnta + "', '" + yr + "'").Tables[0];
        FData1 = dsHoliday;
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.FillUser(drpEmployee, drpDepartment.SelectedValue);
    }
}