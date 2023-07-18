using System;
using System.Activities.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_MonthlyAttandance : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataTable dt1 = new DataTable();
    protected int month;
    protected int year;
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
    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gd.FillUser(drpEmployee, drpDepartment.SelectedValue);
    }

    public void fillAttendance()
    {
        DataColumn c1 = new DataColumn();
        c1.ColumnName = "SNo";
        DataColumn c2 = new DataColumn();
        c2.ColumnName = "EmployeeName";
        DataColumn c3 = new DataColumn();
        c3.ColumnName = "Dat1";
        DataColumn c4 = new DataColumn();
        c4.ColumnName = "Dat2";
        DataColumn c5 = new DataColumn();
        c5.ColumnName = "Dat3";
        DataColumn c6 = new DataColumn();
        c6.ColumnName = "Dat4";
        DataColumn c7 = new DataColumn();
        c7.ColumnName = "Dat5";
        DataColumn c8 = new DataColumn();
        c8.ColumnName = "Dat6";
        DataColumn c9 = new DataColumn();
        c9.ColumnName = "Dat7";
        DataColumn c10 = new DataColumn();
        c10.ColumnName = "Dat8";
        DataColumn c11 = new DataColumn();
        c11.ColumnName = "Dat9";
        DataColumn c12 = new DataColumn();
        c12.ColumnName = "Dat10";
        DataColumn c13 = new DataColumn();
        c13.ColumnName = "Dat11";
        DataColumn c14 = new DataColumn();
        c14.ColumnName = "Dat12";
        DataColumn c15 = new DataColumn();
        c15.ColumnName = "Dat13";
        DataColumn c16 = new DataColumn();
        c16.ColumnName = "Dat14";
        DataColumn c17 = new DataColumn();
        c17.ColumnName = "Dat15";
        DataColumn c18 = new DataColumn();
        c18.ColumnName = "Dat16";
        DataColumn c19 = new DataColumn();
        c19.ColumnName = "Dat17";
        DataColumn c20 = new DataColumn();
        c20.ColumnName = "Dat18";
        DataColumn c21 = new DataColumn();
        c21.ColumnName = "Dat19";
        DataColumn c22 = new DataColumn();
        c22.ColumnName = "Dat20";
        DataColumn c23 = new DataColumn();
        c23.ColumnName = "Dat21";
        DataColumn c24 = new DataColumn();
        c24.ColumnName = "Dat22";
        DataColumn c25 = new DataColumn();
        c25.ColumnName = "Dat23";
        DataColumn c26 = new DataColumn();
        c26.ColumnName = "Dat24";
        DataColumn c27 = new DataColumn();
        c27.ColumnName = "Dat25";
        DataColumn c28 = new DataColumn();
        c28.ColumnName = "Dat26";
        DataColumn c29 = new DataColumn();
        c29.ColumnName = "Dat27";
        DataColumn c30 = new DataColumn();
        c30.ColumnName = "Dat28";
        DataColumn c31 = new DataColumn();
        c31.ColumnName = "Dat29";
        DataColumn c32 = new DataColumn();
        c32.ColumnName = "Dat30";
        DataColumn c33 = new DataColumn();
        c33.ColumnName = "Dat31";
        DataColumn c34 = new DataColumn();
        c34.ColumnName = "TotalDays";
        DataColumn c35 = new DataColumn();
        c35.ColumnName = "SundayOff";
        DataColumn c36 = new DataColumn();
        c36.ColumnName = "EmpId";
        DataColumn c37 = new DataColumn();
        c37.ColumnName = "HolidayOff";
        DataColumn c38 = new DataColumn();
        c38.ColumnName = "SundayWork";
        DataColumn c39 = new DataColumn();
        c39.ColumnName = "HolidayWork";
        DataColumn c40 = new DataColumn();
        c40.ColumnName = "PL";
        DataColumn c41 = new DataColumn();
        c41.ColumnName = "Attandance";
        DataColumn c42 = new DataColumn();
        c42.ColumnName = "Leave";
        DataColumn c43 = new DataColumn();
        c43.ColumnName = "NoOfWorkingDays";
        DataColumn c44 = new DataColumn();
        c44.ColumnName = "NIghtOT";
        dt1.Columns.Add(c1);
        dt1.Columns.Add(c2);
        dt1.Columns.Add(c3);
        dt1.Columns.Add(c4);
        dt1.Columns.Add(c5);
        dt1.Columns.Add(c6);
        dt1.Columns.Add(c7);
        dt1.Columns.Add(c8);
        dt1.Columns.Add(c9);
        dt1.Columns.Add(c10);
        dt1.Columns.Add(c11);
        dt1.Columns.Add(c12);
        dt1.Columns.Add(c13);
        dt1.Columns.Add(c14);
        dt1.Columns.Add(c15);
        dt1.Columns.Add(c16);
        dt1.Columns.Add(c17);
        dt1.Columns.Add(c18);
        dt1.Columns.Add(c19);
        dt1.Columns.Add(c20);
        dt1.Columns.Add(c21);
        dt1.Columns.Add(c22);
        dt1.Columns.Add(c23);
        dt1.Columns.Add(c24);
        dt1.Columns.Add(c25);
        dt1.Columns.Add(c26);
        dt1.Columns.Add(c27);
        dt1.Columns.Add(c28);
        dt1.Columns.Add(c29);
        dt1.Columns.Add(c30);
        dt1.Columns.Add(c31);
        dt1.Columns.Add(c32);
        dt1.Columns.Add(c33);
        dt1.Columns.Add(c34);
        dt1.Columns.Add(c35);
        dt1.Columns.Add(c36);
        dt1.Columns.Add(c37);
        dt1.Columns.Add(c38);
        dt1.Columns.Add(c39);
        dt1.Columns.Add(c40);
        dt1.Columns.Add(c41);
        dt1.Columns.Add(c42);
        dt1.Columns.Add(c43);
        dt1.Columns.Add(c44);

        mntyr = (mnth.Text == "") ? DateTime.Now.ToString("MM-yyyy") : mnth.Text;
        month = Convert.ToInt32(mntyr.Split('-')[0]);
        year = Convert.ToInt32(mntyr.Split('-')[1]);
        string _DD = year + "-" + month + "-01";

        if (mnth.Text != "")
        {
            int totdays = DateTime.DaysInMonth(year, month);
            DateTime firstdate = Convert.ToDateTime(month + "/1/" + year);
            DateTime lastdate = Convert.ToDateTime(month + "/" + totdays + "/" + year);
            DateTime nextdate = firstdate;

            string query = "select EM.EmpId,Emp_Name,ESD.PL,ESD.CL,EM.CRMUserId from tbl_EMpMaster EM inner join tbl_EMPSalaryDetails ESD on ESD.Emp_Id=EM.EmpId and ESD.Delid=EM.Delid WHERE EM.Delid=0  ";
            if (drpStatus.SelectedIndex > 0)
                query += " and EM.Status='" + drpStatus.SelectedValue + "'";
            if (drpEmployee.SelectedIndex > 0)
            {
                query += " and (EmpID=" + drpEmployee.SelectedValue + " )";
            }
            if (drpDepartment.SelectedIndex > 0)
            {
                query += " and Dept_Id=" + drpDepartment.SelectedValue;
            }
            ds = data.getDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string CRMUSERID = ds.Tables[0].Rows[i]["CRMUserId"].ToString();
                    string EmpId = ds.Tables[0].Rows[i]["EmpId"].ToString();
                    double totattend = 0;
                    int cleave = Convert.ToInt32(ds.Tables[0].Rows[0]["CL"]);
                    int sleave = Convert.ToInt32(ds.Tables[0].Rows[0]["PL"]);
                    double totalleave = 0;
                    nextdate = firstdate;
                    dt1.Rows.Add();
                    dt1.Rows[i]["SNo"] = i + 1;
                    dt1.Rows[i]["EmpId"] = ds.Tables[0].Rows[i]["EmpId"].ToString();
                    dt1.Rows[i]["EmployeeName"] = ds.Tables[0].Rows[i]["Emp_Name"].ToString();
                    for (int j = 2; j < dt1.Columns.Count - 3; j++)
                    {
                        DataSet dsnext = new DataSet();
                        if (drpAttendance.SelectedIndex == 0)
                        {
                            if (nextdate <= Convert.ToDateTime(month + "/" + totdays + "/" + year))
                            {
                                if (nextdate.DayOfWeek.ToString() == "Sunday")
                                {
                                    #region if mark attendance on sunday then show 
                                    dt1.Rows[i][j] = "S";
                                    //if mark attendance on sunday then show 
                                    string query2 = "select Format(AttendancedateIn, 'hh:mm tt') AttendancedateIn,Format(Attendancedateout, 'hh:mm tt') Attendancedateout, IsAttendanceOUT from Attendance   where IsDeleted=0 ANd  Cast(AttendanceDateIN as date)='" + nextdate + "'   ";

                                    if (CRMUSERID == "0")
                                        query2 += " and (EmpID=" + EmpId + " )";
                                    else
                                        query2 += " and (EmpID=" + EmpId + " or USERID=" + CRMUSERID + ")";


                                    dsnext = data.getDataSet(query2);
                                    if (dsnext.Tables[0].Rows.Count > 0)
                                    {
                                        string Mtime = ""; string Etime = ""; TimeSpan ts;
                                        double hrs = 0; double minutes = 0;
                                        string duration = "";

                                        Mtime = dsnext.Tables[0].Rows[0]["AttendanceDateIN"].ToString();
                                        if (dsnext.Tables[0].Rows[0]["IsAttendanceOUT"].ToString().ToUpper() == "TRUE")
                                            Etime = dsnext.Tables[0].Rows[0]["AttendanceDateOut"].ToString();

                                        if (Etime == "" || Etime == null)
                                        {
                                            duration = "";
                                        }
                                        else
                                        {
                                            DateTime dat1 = Convert.ToDateTime(nextdate.ToString("MM/dd/yyyy") + " " + Mtime);
                                            DateTime dat2 = Convert.ToDateTime(nextdate.ToString("MM/dd/yyyy") + " " + Etime);
                                            ts = dat2 - dat1;

                                            hrs = dat2.Subtract(dat1).Hours;
                                            minutes = dat2.Subtract(dat1).Minutes;

                                            //duration = "(" + hrs + ":" + minutes + "Hours)";
                                        }
                                        //dt1.Rows[i][j] = Mtime + "<br />" + Etime + "<br />" + duration;
                                        dt1.Rows[i][j] = "P";
                                    }
                                    totattend = totattend + 1;
                                    #endregion
                                }
                                else
                                {
                                    string query2 = "select Format(AttendancedateIn, 'hh:mm tt') AttendancedateIn,Format(Attendancedateout, 'hh:mm tt') Attendancedateout, IsAttendanceOUT from Attendance   where IsDeleted=0 ANd  Cast(AttendanceDateIN as date)='" + nextdate + "'   ";

                                    if (CRMUSERID == "0")
                                        query2 += " and (EmpID=" + EmpId + " )";
                                    else
                                        query2 += " and (EmpID=" + EmpId + " or USERID=" + CRMUSERID + ")";
                                    dsnext = data.getDataSet(query2);
                                    if (dsnext.Tables[0].Rows.Count > 0)
                                    {
                                        string Mtime = ""; string Etime = ""; TimeSpan ts;
                                        double hrs = 0; double minutes = 0;
                                        string duration = "";

                                        Mtime = dsnext.Tables[0].Rows[0]["AttendanceDateIN"].ToString();
                                        if (dsnext.Tables[0].Rows[0]["IsAttendanceOUT"].ToString().ToUpper() == "TRUE")
                                            Etime = dsnext.Tables[0].Rows[0]["AttendanceDateOut"].ToString();

                                        if (Etime == "" || Etime == null)
                                        {
                                            duration = "";
                                        }
                                        else
                                        {
                                            DateTime dat1 = Convert.ToDateTime(nextdate.ToString("MM/dd/yyyy") + " " + Mtime);
                                            DateTime dat2 = Convert.ToDateTime(nextdate.ToString("MM/dd/yyyy") + " " + Etime);
                                            ts = dat2 - dat1;

                                            hrs = dat2.Subtract(dat1).Hours;
                                            minutes = dat2.Subtract(dat1).Minutes;

                                            //duration = "(" + hrs + ":" + minutes + "Hours)";
                                        }
                                        totattend = totattend + 1;
                                        //dt1.Rows[i][j] = Mtime + "<br />" + Etime + "<br />" + duration;
                                        dt1.Rows[i][j] = "P";
                                    }
                                    else
                                    {
                                        if (nextdate.ToString("MM/dd/yyyy") == "03/06/2023")
                                        {
                                            int a = 0;
                                        }
                                        string query1 = "GETHOLIDAYLIST_Proc '" + nextdate.ToString("MM/dd/yyyy") + "'  ";
                                        DataSet dsholiday = data.getDataSet(query1);
                                        if (dsholiday.Tables[0].Rows.Count > 0)
                                        {
                                            DataView dvv = dsholiday.Tables[0].DefaultView;
                                            dvv.RowFilter = "DATEFROM ='" + nextdate.ToString("MM/dd/yyyy") + "'";
                                            if (dvv.ToTable().Rows.Count > 0)
                                            {
                                                if (dt1.Rows[i][j].ToString() != "P")
                                                    dt1.Rows[i][j] = "HD";
                                            }
                                            else
                                            {
                                                dt1.Rows[i][j] = "L";
                                                totalleave = totalleave + 1;
                                            }
                                        }
                                        else
                                        {
                                            DataSet dssLeave = data.getDataSet("GETLEAVEOFEMPLOYEE  '" + ds.Tables[0].Rows[i]["EmpId"].ToString() + "','" + nextdate.ToString("MM/dd/yyyy") + "'");
                                            if (dssLeave.Tables[0].Rows.Count > 0)
                                            {
                                                totattend = totattend + 1;
                                                dt1.Rows[i][j] = "PL";
                                            }
                                            else
                                            {
                                                dt1.Rows[i][j] = "L";
                                                totalleave = totalleave + 1;
                                            }
                                        }
                                    }
                                }
                                nextdate = nextdate.AddDays(1); ;

                            }
                            dsnext.Clear();
                        }
                        else
                        {
                            if (nextdate <= Convert.ToDateTime(month + "/" + totdays + "/" + year))
                            {
                                string query2 = "select AttendanceDate from TBL_NIGHTATTENDANCE   where IsDeleted=0 ANd  Cast(AttendanceDate as date)='" + nextdate + "'   ";

                                query2 += " and (FK_EMPID=" + EmpId + " )";

                                dsnext = data.getDataSet(query2);
                                if (dsnext.Tables[0].Rows.Count > 0)
                                {
                                    dt1.Rows[i][j] = "P";
                                }
                                else
                                    dt1.Rows[i][j] = "L";

                                nextdate = nextdate.AddDays(1); ;
                            }
                        }
                    }

                    //DataSet dss = master.GetSallary(_DD, drpDepartment.SelectedValue, "0", EmpId, "2", drpStatus.SelectedValue);
                    DataSet dss = data.getDataSet("GETATTENDANCECALCULATION '" + EmpId + "','" + _DD + "'");

                    dt1.Rows[i]["TotalDays"] = dss.Tables[0].Rows[0]["NOOFWORKINGDAY"].ToString();
                    dt1.Rows[i]["SundayOFF"] = dss.Tables[0].Rows[0]["ALLOWSUNDAY"].ToString();
                    dt1.Rows[i]["HolidayOff"] = dss.Tables[0].Rows[0]["NoOfHoliday"].ToString();
                    dt1.Rows[i]["SundayWork"] = dss.Tables[0].Rows[0]["NOOFSUNDAYWork"].ToString();
                    dt1.Rows[i]["HolidayWork"] = dss.Tables[0].Rows[0]["NOOFHolidayWork"].ToString();
                    dt1.Rows[i]["Attandance"] = dss.Tables[0].Rows[0]["NOOFATTANDANCE"].ToString();
                    dt1.Rows[i]["NoOfWorkingDays"] = dss.Tables[0].Rows[0]["TOTALWORKINGDAY"].ToString();
                    dt1.Rows[i]["NIghtOT"] = dss.Tables[0].Rows[0]["NIghtOT"].ToString();
                    dt1.Rows[i]["PL"] = dss.Tables[0].Rows[0]["PL1"].ToString();
                    dt1.Rows[i]["Leave"] = dss.Tables[0].Rows[0]["TOTALLEAVE"].ToString();
                }
            }
        }
        rptUserViewAttendance.DataSource = dt1;
        rptUserViewAttendance.DataBind();
    }


    protected void Search_Click(object sender, EventArgs e)
    {
        fillAttendance();
    }
}