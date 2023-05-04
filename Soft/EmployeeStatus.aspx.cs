using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Soft_EmployeeStatus : System.Web.UI.Page
{
    GetData gd = new GetData();
    Master master = new Master();
    Data data = new Data();
    private DataTable dtEmp;
    private SqlCommand cmd;
    DataTable dt_Employee;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }
            txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            gd.fillDepartment(drpDept);
            drpDept.SelectedValue = "2";
            fillEmp();

        }
    }

    private void CreateTemplate()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class='widget-content'>");
        sb.Append("<div class='table-responsive'>");


        sb.Append("<table id ='Example1' border='1' runat='server' class='table display table-responsive table-striped'>");
        sb.Append("<thead>");
        //**************Check Box********************

        dt_Employee = dtEmp.DefaultView.ToTable(true, "Emp_Name", "ID");
        DataView dv = dtEmp.DefaultView;
        sb.Append("<tr>");
        sb.Append("<th colspan='3'>Check All<input type='checkbox'  id='chkAll' runat='server' onclick='javascript: SelectAllCheckboxes(this);' /></th>");

        foreach (DataRow dr in dt_Employee.Rows)
        {
            sb.Append("<th style='text-align:center;' colspan='2'>  <input type='checkbox' id='chk_" + dr["ID"] + "' runat='server' onclick='javascript: SelectCheckID();'/></th>");
        }
        sb.Append("</tr>");
        //**************SR No********************
        sb.Append("<tr>");
        sb.Append("<th colspan='3'>S. No.</th>");

        for (int i = 1; i <= dt_Employee.Rows.Count; i++)
        {
            sb.Append("<th style='text-align:center;' colspan='2'>" + i + "</th>");
        }
        sb.Append("</tr>");
        //**************Employee NAME********************
        sb.Append("<tr style='height:90px;'>");
        sb.Append("<th colspan='3'>Name</th>");
        foreach (DataRow dr in dt_Employee.Rows)
        {
            sb.Append("<th style='text-align:center;' colspan='2'>" + dr["Emp_Name"] + "</th>");
        }
        sb.Append("</tr>");
        //**************Particulars********************
        sb.Append("<tr style='height:60px;'>");
        sb.Append("<th colspan='3'>Particulars</th>");

        for (int i = 1; i <= dt_Employee.Rows.Count; i++)
        {
            sb.Append("<td style='text-align:center;'>Date & Time</td>");
            sb.Append("<td style='text-align:center;'>Party/Station Name</td>");
        }
        sb.Append("</tr>");

        sb.Append("</thead>");
        sb.Append("<tbody>");
        //***********ATTENDANCE IN******************
        sb.Append("<tr style='height:50px;'>");
        sb.Append("<th colspan='3'>Attendance In</th>");
        DataTable dt_attIN = dtEmp.DefaultView.ToTable(true, "Emp_Name", "Att_DATEIN", "Att_TIMEIN", "ATT_STATIONIN");
        foreach (DataRow dr in dt_attIN.Rows)
        {
            sb.Append("<td>" + dr["Att_DATEIN"] + "<br/>" + dr["Att_TIMEIN"] + "</td>");
            sb.Append("<td>" + dr["ATT_STATIONIN"] + "</td>");
        }
        sb.Append("</tr>");

        //***********TOUR DATE******************
        DataTable dt_tour = dtEmp.DefaultView.ToTable(true, "Emp_Name", "Tour_Date", "Tour_Station");
        sb.Append("<tr style='height:60px;'>");
        sb.Append("<th colspan='3'>Employee Tour Plan Day Wise</th>");
        foreach (DataRow dr in dt_tour.Rows)
        {
            sb.Append("<td>" + dr["Tour_Date"] + "</td>");
            sb.Append("<td>" + dr["Tour_Station"] + "</td>"); 
            //sb.Append("<td>&nbsp;</td>");
        }
        sb.Append("</tr>");
        //***********CHECK IN******************

        int a = Convert.ToInt32(dtEmp.Compute("MAX(CHECK_COUNT)", "1 = 1"));
        for (int i = 0; i < a; i++)
        {
            sb.Append("<tr style='height:80px;'>");
            if (i == 0)
            {
                sb.Append("<th colspan='2' rowspan='" + a + "'>Check In</th>");
            }
            sb.Append("<th>" + (i + 1) + "</th>");
            foreach (DataRow dr in dt_Employee.Rows)
            {
                dv.RowFilter = " ID=" + dr["ID"];
                DataTable dt_chkin = dv.ToTable(true, "CHECKIN_DATE", "CHECKIN_TIME", "CHECKIN_PARTY");
                if (i < Convert.ToInt32(dt_chkin.Rows.Count))
                {
                    sb.Append("<td>" + dt_chkin.Rows[i]["CHECKIN_DATE"] + "<br/>" + dt_chkin.Rows[i]["CHECKIN_TIME"] + "</td>");
                    sb.Append("<td>" + dt_chkin.Rows[i]["CHECKIN_PARTY"] + "</td>");

                }
                else
                {
                    sb.Append("<td>&nbsp;</td>");
                    sb.Append("<td>&nbsp;</td>");
                }
            }
            sb.Append("</tr>");
        }

        //**************CLIENT MEET********************
        int b = Convert.ToInt32(dtEmp.Compute("MAX(MEET_COUNT)", "1 = 1"));
        dv = dtEmp.DefaultView;
        for (int i = 0; i < b; i++)
        {
            sb.Append("<tr style='height:80px;'>");
            if (i == 0)
            {
                sb.Append("<th rowspan ='" + (b) + "' colspan='2'>Client Meet</th>");
            }
            sb.Append("<th>" + (i + 1) + "</th>");
            foreach (DataRow dr in dt_Employee.Rows)
            {
                dv.RowFilter = " ID=" + dr["ID"];
                DataTable dt_meet = dv.ToTable(true, "Emp_Name", "MEET_DATE", "MEET_TIME", "MEET_PARTY", "MEET_STATION");
                if (i < Convert.ToInt32(dt_meet.Rows.Count))
                {

                    sb.Append("<td>" + dt_meet.Rows[i]["MEET_DATE"] + "<br/>" + dt_meet.Rows[i]["MEET_TIME"] + "</td>");
                    if (dt_meet.Rows[0]["MEET_PARTY"].ToString() != "")
                        sb.Append("<td>" + dt_meet.Rows[i]["MEET_PARTY"] + "(" + dt_meet.Rows[i]["MEET_STATION"] + ")</td>");
                    else sb.Append("<td>&nbsp;</td>");
                }
                else
                {
                    sb.Append("<td>&nbsp;</td>");
                    sb.Append("<td>&nbsp;</td>");
                }
            }
            sb.Append("</tr>");
        }

        //**************CREATE DEALER********************
        int d = Convert.ToInt32(dtEmp.Compute("MAX(CD_COUNT)", "1 = 1"));
        for (int i = 0; i < d; i++)
        {
            sb.Append("<tr style='height:80px;'>");
            if (i == 0)
            {
                sb.Append("<th rowspan ='" + d + "' colspan='2'>Create Dealer</th>");
            }
            sb.Append("<th>" + (i + 1) + "</th>");
            foreach (DataRow dr in dt_Employee.Rows)
            {
                dv.RowFilter = " ID=" + dr["ID"];
                DataTable dt_cd = dv.ToTable(true, "Emp_Name", "CD_DATE", "CD_TIME", "CD_PARTY", "CD_STATION");
                if (i < Convert.ToInt32(dt_cd.Rows.Count))
                {

                    sb.Append("<td>" + dt_cd.Rows[i]["CD_DATE"] + "<br/>" + dt_cd.Rows[i]["CD_TIME"] + "</td>");
                    if (dt_cd.Rows[0]["CD_PARTY"].ToString() != "")
                    {
                        sb.Append("<td>" + dt_cd.Rows[i]["CD_PARTY"] + "(" + dt_cd.Rows[i]["CD_STATION"] + ")</td>");
                    }
                    else
                    {
                        sb.Append("<td>&nbsp;</td>");
                    }
                }
                else
                {
                    sb.Append("<td>&nbsp;</td>");
                    sb.Append("<td>&nbsp;</td>");
                }
            }
            sb.Append("</tr>");
        }
        //**************SALES ORDER********************
        int e = Convert.ToInt32(dtEmp.Compute("MAX(ORDER_COUNT)", "1 = 1"));
        for (int i = 0; i < e; i++)
        {
            sb.Append("<tr style='height:80px;'>");
            if (i == 0)
            {
                sb.Append("<th rowspan ='" + e + "' colspan='2'>Sales Order</th>");
            }
            sb.Append("<th>" + (i + 1) + "</th>");
            foreach (DataRow dr in dt_Employee.Rows)
            {
                dv.RowFilter = " ID=" + dr["ID"];
                DataTable dt_ord = dv.ToTable(true, "Emp_Name", "ORDER_DATE", "ORDER_TIME", "ORDER_PARTY", "ORDER_AMT", "ORDER_STATION");
                if (i < Convert.ToInt32(dt_ord.Rows.Count))
                {
                    if (dt_ord.Rows[i]["ORDER_DATE"].ToString() != "")
                    {
                        sb.Append("<td>" + dt_ord.Rows[i]["ORDER_DATE"] + "<br/>" + dt_ord.Rows[i]["ORDER_TIME"] + " <br/>(Amount : " + dt_ord.Rows[0]["ORDER_AMT"] + ")</td>");
                        sb.Append("<td>" + dt_ord.Rows[i]["ORDER_PARTY"] + " (" + dt_ord.Rows[i]["ORDER_STATION"] + ")</td>");
                    }
                    else
                    {
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("<td>&nbsp;</td>");
                    }
                }
                else
                {
                    sb.Append("<td>&nbsp;</td>");
                    sb.Append("<td>&nbsp;</td>");
                }
            }
            sb.Append("</tr>");
        }
        //**************SECONDARY SALES********************
        //      int f = Convert.ToInt32(dtEmp.Compute("MAX(SALES_COUNT)", "1 = 1"));
        //for (int i = 0; i < f; i++)
        //{
        sb.Append("<tr style='height:80px;'>");
        //if (i == 0)
        //{
        sb.Append("<th colspan='3'>Secondary Sales</th>");
        //}
        //sb.Append("<th>" + (i + 1) + "</th>");
        foreach (DataRow dr in dt_Employee.Rows)
        {
            dv.RowFilter = " ID=" + dr["ID"];
            DataTable dt_sales = dv.ToTable(true, "Emp_Name", "SALES_VISITED", "SALES_AMT", "CHECKIN_PARTY");
            if (dt_sales.Rows[0]["CHECKIN_PARTY"].ToString() != "")
            {
                sb.Append("<td>" + txtdate.Text.Trim() + "<br/>(Visits : " + dt_sales.Rows[0]["SALES_VISITED"] + ") <br/>(Amount : " + dt_sales.Rows[0]["SALES_AMT"] + ")</td>");
                sb.Append("<td>" + dt_sales.Rows[0]["CHECKIN_PARTY"] + " (Primary)</td>");
            }
            else
            {
                sb.Append("<td>&nbsp;</td>");
                sb.Append("<td>&nbsp;</td>");
            }
        }
        sb.Append("</tr>");
        //        }
        //***********CHECK OUT******************
        dv = dtEmp.DefaultView;
        for (int i = 0; i < a; i++)
        {
            sb.Append("<tr style='height:80px;'>");
            if (i == 0)
            {
                sb.Append("<th colspan='2' rowspan='" + a + "'>Check Out</th>");
            }
            sb.Append("<th>" + (i + 1) + "</th>");
            foreach (DataRow dr in dt_Employee.Rows)
            {
                dv.RowFilter = " ID=" + dr["ID"];
                DataTable dt_chkOut = dv.ToTable();
                if (i < Convert.ToInt32(dt_chkOut.Rows.Count))
                {
                    sb.Append("<td>" + dt_chkOut.Rows[i]["CHECKOUT_DATE"] + "<br/>" + dt_chkOut.Rows[i]["CHECKOUT_TIME"] + "</td>");
                    sb.Append("<td>" + dt_chkOut.Rows[i]["CHECKOUT_NEXTPARTY"] + "</td>");

                }
                else
                {
                    sb.Append("<td>&nbsp;</td>");
                    sb.Append("<td>&nbsp;</td>");
                }
            }
            sb.Append("</tr>");
        }



        //***********ATTENDANCE OUT******************

        sb.Append("<tr>");

        //  DataTable dt_attOUT = dtEmp.DefaultView.ToTable(true, "Emp_Name", "Att_DATEOUT", "Att_TIMEOUT");
        dv = dtEmp.DefaultView;
        sb.Append("<th colspan='3'>Attendance Out</th>");
        foreach (DataRow dr in dt_Employee.Rows)
        {
            dv.RowFilter = " ID=" + dr["ID"];
            DataTable dt_AttOut = dv.ToTable();
            sb.Append("<td>" + dt_AttOut.Rows[0]["Att_DATEOUT"] + "<br/>" + dt_AttOut.Rows[0]["Att_TIMEOUT"] + "</td>");
            sb.Append("<td>" + dt_AttOut.Rows[0]["ATT_STATIONOUT"] + "</td>");
        }
        sb.Append("</tr>");
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</div>");
        sb.Append("</div>");
        Contentblock.InnerHtml = sb.ToString();
    }

    private void fillEmp()
    {
        //string query = "select * from [EMP_VIEW] Where [Status]='Active' and DelId = 0 ";
        //    if (drpDept.SelectedIndex > 0)
        //        query += " and DEPT_ID=" + drpDept.SelectedValue;
        //     dsEmp = data.getDataSet(query).Tables[0];
        cmd = new SqlCommand("PROC_EmployeeTracking");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@DeptID", drpDept.SelectedValue);
        cmd.Parameters.AddWithValue("@Dt", data.YYYYMMDD(txtdate.Text));
        dtEmp = data.getDataSet(cmd).Tables[0];
        ViewState["Emp"] = dtEmp;
        CreateTemplate();
    }

    protected void drpDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillEmp();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        dt_Employee = ((DataTable)ViewState["Emp"]).DefaultView.ToTable(true, "Emp_Name", "ID");

        string OrderId = HDDID.Value;

        GenratePDF(OrderId, false);
    }
    public void GenratePDF(string OrderId, Boolean isPdf)
    {
        if (OrderId != "")
        {
            //  CreateTable();
            string _OrderId = OrderId;
            StringBuilder sb = new StringBuilder();
            string[] SplitValue = _OrderId.Split(',');
            sb.Append("<style>");
            sb.Append("\n @media print {");
            sb.Append("\n footer {page-break-after: always;}");
            sb.Append("\n }</style>");
            if (SplitValue.Length > 0)
            {
                DataTable dsGet = (DataTable)ViewState["Emp"];
                for (int _Count = 0; _Count < SplitValue.Length; _Count++)
                {
                    //  string ss = "Sp_OrderPrint " + SplitValue[_Count];
                    DataView dv = dsGet.DefaultView;

                    dv.RowFilter = "ID=" + SplitValue[_Count];
                    DataTable dt = dv.ToTable();

                    DataRow drHqtr = master.getHqtrUser().Tables[0].Select("MId = " + dt.Rows[0]["CRMUserId"]).FirstOrDefault();

                    using (StringWriter sw = new StringWriter())
                    {
                        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                        {
                            sb.Append("<table style='width: 100%; padding-right: 15px; border-spacing: 0px;'>");
                            sb.Append("<tr style='padding: 0px; margin-top: -10px; margin-bottom: -10px;'>");
                            sb.Append("<td style='text-align: left;padding: 0px;'>");
                            sb.Append("<img src='../../img/logo.jpg' height='80px'><br />");
                            sb.Append("</td>");
                            sb.Append("<td style='text-align: right;padding: 0px;'>");
                            sb.Append("<p style='font-size: 12px;'>");
                            sb.Append("<strong style='font-size: 18px;'>Shree Tadkeshwar Agro Food Product</strong>");
                            sb.Append("<br />");
                            sb.Append("H- 1-37- A, Sarna Doongar Industrial Area, Jhotwara Extension,");
                            sb.Append("<br />");
                            sb.Append("Jaipur - 302012 Rajasthan, India");
                            sb.Append("<br />");
                            sb.Append("(Mob) : + 91 - 98290 - 32422, (Tel) : 0141 - 3540250");
                            sb.Append("<br />");
                            sb.Append("(Email) : sales@tadkeshwarfoods.com, (url) : www.tadkeshwarfoods.com");
                            sb.Append("</p>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr style='padding: 0px; margin-top: -10px; margin-bottom: -10px;'>");
                            sb.Append("<td colspan='2' style='padding: 0px;' >");
                            sb.Append("<asp:Label ID='lblHeading' runat='server' Style='font-size: 20px; color: firebrick;'></asp:Label>");
                            sb.Append("<asp:Label ID='lblDateRng' runat='server'></asp:Label></td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                            sb.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                            sb.Append("<tr>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td align='center'><table width='990' border='0' cellspacing='0' cellpadding='0'>");
                            sb.Append("<tr>");
                            sb.Append("<td width='990' align='center'><strong>Employee Daily Report</strong></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td align='center'>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td align='center'><table width='100%' border='1px' bordercolor='#CCC' cellspacing='0' cellpadding='5' style='border:1px solid #CCC; border-collapsecollapse;'>");
                            sb.Append("<tr>");
                            sb.Append("<td>Employee Name: " + dt.Rows[0]["Emp_Name"].ToString() + "</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("<td>Department Name: " + master.GetDepartment("Select", dt.Rows[0]["Dept_Id"].ToString(), "", "", "").Tables[0].Rows[0]["DEPT_NAME"].ToString() + "</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>Headquarter: " + drHqtr["HeadQtr"] + "</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("<td>Reporting Officer : " + dt.Rows[0]["Rep_Manager"] + " </td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("<td>date : " +txtdate.Text.Trim().ToString() + "</td>");
                            sb.Append("<td></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>Attandance in</td>");
                            sb.Append("<td colspan='3' align='center'>" + dt.Rows[0]["Att_DATEIN"].ToString() + " " + dt.Rows[0]["Att_TIMEIN"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>Check in : "+ dt.Rows[0]["CHECKIN_PARTY"].ToString() + " </td>");
                            sb.Append("<td colspan='3' align='center'>" + dt.Rows[0]["CHECKIN_DATE"].ToString() + " " + dt.Rows[0]["CHECKIN_TIME"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>Secondary sale(Amount)</td>");
                            sb.Append("<td colspan='3' align='center'>" + dt.Rows[0]["SALES_AMT"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            DataSet dt_summary = getUserSummary(SplitValue[_Count], txtdate.Text.Trim());
                            sb.Append("<td rowspan='" + (dt_summary.Tables[0].Rows.Count + 1) + "'>Secondary sale (group qty)</td>");
                            sb.Append("<td>Group</td>");
                            sb.Append("<td>Qty</td>");
                            sb.Append("<td>Amount</td>");
                            sb.Append("</tr>");
                            foreach (DataRow rd in dt_summary.Tables[0].Rows)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td>" + rd["GroupName"] + "</td>");
                                sb.Append("<td>" + rd["Qty"] + "</td>");
                                sb.Append("<td>" + rd["Amount"] + "</td>");
                                sb.Append("</tr>");
                            }
                            sb.Append("<tr>");
                            sb.Append("<td>Total Visit</td>");
                            sb.Append("<td colspan='3' align='center'>" + dt.Rows[0]["SALES_VISITED"].ToString() + "</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<td>Total Visit Productive</td>");
                            sb.Append("<td colspan='3' align='center'>" + dt.Rows[0]["Productive"].ToString() + "</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<td>Total Visit Non-Productive</td>");
                            sb.Append("<td colspan='3' align='center'>" + dt.Rows[0]["NonProductive"].ToString() + "</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<td>Sale order(in Amount)</td>");
                            sb.Append("<td colspan='3' align='center'>" + dt.Rows[0]["ORDER_AMT"].ToString() + "</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<td rowspan='" + (dt_summary.Tables[1].Rows.Count + 1) + "'>Sale order group wise summary</td>");
                            sb.Append("<td>Group</td>");
                            sb.Append("<td>Qty</td>");
                            sb.Append("<td>Amount</td>");
                            sb.Append("</tr>");
                            foreach (DataRow rd in dt_summary.Tables[1].Rows)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td>" + rd["GroupName"] + "</td>");
                                sb.Append("<td>" + rd["Qty"] + "</td>");
                                sb.Append("<td>" + rd["Amount"] + "</td>");
                                sb.Append("</tr>");
                            }
                            DataView dv_usr = dt.DefaultView;
                            DataTable tbl_CD = dv_usr.ToTable(true, "CD_PARTY", "CD_STATION");
                            sb.Append("<tr>");
                            sb.Append("<td rowspan='" + (tbl_CD.Rows.Count + 1) + "'>New party add (create dealer)</td>");
                            sb.Append("<td>Station</td>");
                            sb.Append("<td>Party Name</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("</tr>");
                            foreach (DataRow rd in tbl_CD.Rows)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td>" + rd["CD_STATION"] + "</td>");
                                sb.Append("<td>" + rd["CD_PARTY"] + "</td>");
                                sb.Append("<td>&nbsp;</td>");
                                sb.Append("</tr>");
                            }

                            DataTable tbl_CM = dv_usr.ToTable(true, "MEET_PARTY", "MEET_STATION", "MEET_REMARK");
                            sb.Append("<td rowspan='" + (tbl_CM.Rows.Count + 1) + "'>Client Meeting</td>");
                            sb.Append("<td>Station</td>");
                            sb.Append("<td>Party Name</td>");
                            sb.Append("<td>Remark</td>");
                            sb.Append("</tr>");

                            foreach (DataRow rd in tbl_CM.Rows)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td>" + rd["MEET_STATION"] + "</td>");
                                sb.Append("<td>" + rd["MEET_PARTY"] + "</td>");
                                sb.Append("<td>" + rd["MEET_REMARK"] + "</td>");
                                sb.Append("</tr>");
                            }



                            sb.Append("<tr>");
                            sb.Append("<td>Total Travel(in km) today</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>Employee travel expenses</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>Check out</td>");
                            sb.Append("<td colspan='3' align='center'>" + dt.Rows[0]["CHECKOUT_DATE"].ToString() + " " + dt.Rows[0]["CHECKOUT_TIME"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>Attandence out</td>");
                            sb.Append("<td colspan='3' align='center'>" + dt.Rows[0]["Att_DATEOUT"].ToString() + " " + dt.Rows[0]["Att_TIMEOUT"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("</table></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td align='center'>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("</table></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                            sb.Append("<footer></footer>");
                        }
                    }
                }
                //if (isPdf)
                //{
                //    string  pdfname = ExporttoPDF(sb);
                //    Response.Write("<script>window.open('../PDF/"+ pdfname + ".pdf','_blank');</script>");
                //}
                //else
                //{
                //Export HTML String as PDF.
                StringReader sr = new StringReader(sb.ToString());
                Session["InvPrint"] = sb.ToString();
                Response.Write("<script>window.open('UserRptPrint.aspx','_blank');</script>");
                //}
            }
        }

    }

    private DataSet getUserSummary(string id, string date1)
    {
        DataSet dsSummary = new DataSet();
        cmd = new SqlCommand("PROC_USERSUMMARY");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@dt", date1);
        cmd.Parameters.AddWithValue("@id", id);
        dsSummary = data.getDataSet(cmd);


        return dsSummary;
    }
}
