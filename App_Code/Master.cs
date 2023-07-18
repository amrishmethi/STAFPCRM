using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Protocols.WSTrust;
using System.Web;


public class Master
{
    DataSet ds = new DataSet();
    Data data = new Data();
    SqlCommand cmd = new SqlCommand();
    string query = "";
    string SessionID = "";
    int res = 0;
    HttpCookie Soft;
    string userid = "";

    public Master()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataSet getSecondarySalesParty(string action, string id, string stationid, string station, string name, string mobile, string whatsapp, string hqtr, string Beat)
    {
        cmd = new SqlCommand("PROC_SECONDARYPARTY");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", action);
        cmd.Parameters.AddWithValue("@ID", id);
        cmd.Parameters.AddWithValue("@STATIONID", stationid);
        cmd.Parameters.AddWithValue("@STATIONNAME", station);
        cmd.Parameters.AddWithValue("@NAME", name);
        cmd.Parameters.AddWithValue("@MOBILENO", mobile);
        cmd.Parameters.AddWithValue("@WHATSUPMOBILENO", whatsapp);
        cmd.Parameters.AddWithValue("@HQD", hqtr);
        cmd.Parameters.AddWithValue("@BeatId", Beat);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getSecondarySalesStation(string action, string id, string name)
    {
        cmd = new SqlCommand("PROC_SECONDARYSTATION");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", action);
        cmd.Parameters.AddWithValue("@ID", id);
        cmd.Parameters.AddWithValue("@NAME", name);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet saveUserTourPlan(string id, string uid, string hqtr, string dist, string stat, string tdate)
    {
        cmd = new SqlCommand("PROC_SaveUserTourPlan");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", id);
        cmd.Parameters.AddWithValue("@UsrID", uid);
        cmd.Parameters.AddWithValue("@Hqt", hqtr);
        cmd.Parameters.AddWithValue("@Dst", dist);
        cmd.Parameters.AddWithValue("@Sta", stat);
        cmd.Parameters.AddWithValue("@Dt", tdate);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getSubGrpString(string selectedValuee)
    {
        //cmd = new SqlCommand("Select CMsCode from [stm_stmast].[dbo].[CMaster] Where CMsSr='I' and MCMSCode='" + selectedValuee + "'");
        //cmd.CommandType = CommandType.Text;
        cmd = new SqlCommand("Proc_cmaster '" + selectedValuee + "' ");
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getCheckInDetails(string userid, string deptid, string indate, string intime)
    {
        cmd = new SqlCommand("PROC_CHECKINOUT");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", userid);
        cmd.Parameters.AddWithValue("@DeptID", deptid);
        cmd.Parameters.AddWithValue("@CheckinDateFrom", data.YYYYMMDD(indate));
        cmd.Parameters.AddWithValue("@CheckinDateTo", data.YYYYMMDD(intime));
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getAttendanceList(string userid, string deptid, string date)
    {
        cmd = new SqlCommand("PROC_ATTENDANCE");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", userid);
        cmd.Parameters.AddWithValue("@DeptID", deptid);
        cmd.Parameters.AddWithValue("@CheckinDateFrom", data.YYYYMMDD(date));

        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet AccessRights(string v, string userid)
    {
        cmd = new SqlCommand("Sp_UserAccess");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@pagelink", v);
        cmd.Parameters.AddWithValue("@uid", userid);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getUserDetails(string userid, string dptid, string Status = "ALL", string CRMUSERID = "0")
    {
        cmd = new SqlCommand("PROC_USERDETAILS");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", userid);
        cmd.Parameters.AddWithValue("@DeptID", dptid);
        cmd.Parameters.AddWithValue("@Status", Status);
        cmd.Parameters.AddWithValue("@CRMUSERID", CRMUSERID);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getUserReport(string userid, string mobile, string dept, string desg, string Status)
    {
        cmd = new SqlCommand("PROC_USERDETAILSREPORT");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", userid);
        cmd.Parameters.AddWithValue("@Mobno", mobile);
        cmd.Parameters.AddWithValue("@deptid", dept);
        cmd.Parameters.AddWithValue("@desgid", desg);
        cmd.Parameters.AddWithValue("@Status", Status);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getPriceList(string sgrp, string isout)
    {
        cmd = new SqlCommand("PROC_PriceList");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@Grp", sgrp);
        cmd.Parameters.AddWithValue("@Isout", isout);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getClientMeet(string emp, string party, string type, string dt_from, string dt_to, string deptid)
    {
        cmd = new SqlCommand("PROC_ClientMeet");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@empid", emp);
        cmd.Parameters.AddWithValue("@Hqtr", party);
        cmd.Parameters.AddWithValue("@type", type);
        cmd.Parameters.AddWithValue("@dt", data.YYYYMMDD(dt_from));
        cmd.Parameters.AddWithValue("@dt1", data.YYYYMMDD(dt_to));
        cmd.Parameters.AddWithValue("@dpt", deptid);
        ds = data.getDataSet(cmd);
        return ds;
    }
    public DataSet getCreateDealer(string action, string id, string name, string contper, string address, string zip, string station, string state, string gst, string gstRegType, string smsMob, string whatsapp, string district, string transport, string partyType, string partyCatg, string webdate, string webdate1, string empid, string headQtr)
    {
        cmd = new SqlCommand("PROC_CREATEDEALER");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@ACTION", action);
        cmd.Parameters.AddWithValue("@ID", id);
        cmd.Parameters.AddWithValue("@NAME", name);
        cmd.Parameters.AddWithValue("@CONTACTP", contper);
        cmd.Parameters.AddWithValue("@ADDRESS", address);
        cmd.Parameters.AddWithValue("@ZIP", zip);
        cmd.Parameters.AddWithValue("@STATION", station);
        cmd.Parameters.AddWithValue("@STATE", state);
        cmd.Parameters.AddWithValue("@GST", gst);
        cmd.Parameters.AddWithValue("@GSTTYPE", gstRegType);
        cmd.Parameters.AddWithValue("@MOBILE", smsMob);
        cmd.Parameters.AddWithValue("@WHATSAPP", whatsapp);
        cmd.Parameters.AddWithValue("@DISTRICT", district);
        cmd.Parameters.AddWithValue("@TRANSPORT", transport);
        cmd.Parameters.AddWithValue("@PTYPE", partyType);
        cmd.Parameters.AddWithValue("@PCATG", partyCatg);
        cmd.Parameters.AddWithValue("@WEBDATE", data.YYYYMMDD(webdate));
        cmd.Parameters.AddWithValue("@WEBDATE1", data.YYYYMMDD(webdate1));
        cmd.Parameters.AddWithValue("@EMPID", empid);
        cmd.Parameters.AddWithValue("@HQTR", headQtr);

        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getUserTourPlan(string userid, string type)
    {
        cmd = new SqlCommand("PROC_USERTOURPLAN");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", userid);
        cmd.Parameters.AddWithValue("@type", type);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getSeconarySalesDetails(string userid, string party, string station, string indate, string intime, string dept)
    {
        cmd = new SqlCommand("PROC_SecondarySalesReport");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@USERID", userid);
        cmd.Parameters.AddWithValue("@PARTY", party);
        cmd.Parameters.AddWithValue("@STATION", station);
        cmd.Parameters.AddWithValue("@DATEFROM", data.YYYYMMDD(indate));
        cmd.Parameters.AddWithValue("@DATETO", data.YYYYMMDD(intime));
        cmd.Parameters.AddWithValue("@Deptid", dept);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getSecondarySalesItem(string id)
    {
        cmd = new SqlCommand("PROC_SECONDARYITEMS1");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", id);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getSalesOrder(string action, string id, string emp, string hqtr, string party, string dt, string dt1, string delv, string pymt, string grp)
    {
        cmd = new SqlCommand("PROC_SalesOrder");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Action", action);
        cmd.Parameters.AddWithValue("@ID", id);
        cmd.Parameters.AddWithValue("@Empid", emp);
        cmd.Parameters.AddWithValue("@hqtr", hqtr);
        cmd.Parameters.AddWithValue("@party", party);
        cmd.Parameters.AddWithValue("@dtFrom", data.YYYYMMDD(dt));
        cmd.Parameters.AddWithValue("@dtTo", data.YYYYMMDD(dt1));
        cmd.Parameters.AddWithValue("@delv", delv);
        cmd.Parameters.AddWithValue("@pymtmode", pymt);
        cmd.Parameters.AddWithValue("@group", grp);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getSaleOrderReportST(string head, string district, string report, string station, string dt, string dt1, string rate, string party, string Group)
    {
        cmd = new SqlCommand("Proc_SaleOrderReportST");
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@head", head);
        cmd.Parameters.AddWithValue("@district", district);
        cmd.Parameters.AddWithValue("@report", report);
        cmd.Parameters.AddWithValue("@station", station);
        cmd.Parameters.AddWithValue("@dtFrom", data.YYYYMMDD(dt));
        cmd.Parameters.AddWithValue("@dtTo", data.YYYYMMDD(dt1));
        cmd.Parameters.AddWithValue("@rate", rate);
        cmd.Parameters.AddWithValue("@Party", party);
        cmd.Parameters.AddWithValue("@Group", Group);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getSaleSummaryReportST(string head, string district, string report, string station, string dt, string dt1, string rate, string party, string Group)
    {
        cmd = new SqlCommand("Proc_SaleSummaryReportST");
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@head", head);
        cmd.Parameters.AddWithValue("@district", district);
        cmd.Parameters.AddWithValue("@report", report);
        cmd.Parameters.AddWithValue("@station", station);
        cmd.Parameters.AddWithValue("@dtFrom", data.YYYYMMDD(dt));
        cmd.Parameters.AddWithValue("@dtTo", data.YYYYMMDD(dt1));
        cmd.Parameters.AddWithValue("@rate", rate);
        cmd.Parameters.AddWithValue("@Party", party);
        cmd.Parameters.AddWithValue("@Group", Group);
        ds = data.getDataSet(cmd);
        return ds;
    }
    //public DataSet getHqtrUser()
    //{
    //    return ds = data.getDataSet("select * from GETHEADQUARTER order by Name,HEADQTR");
    //}
    //public DataSet getHqtrUser1()
    //{
    //    return ds = data.getDataSet("select * from GETHEADQUARTER G inner Join  [Station] S on S.DistrictNo = G.districtNo inner join tbl_EmpMaster EMP on EMP.CRMUSerId=G.MID and Emp.Delid=0 order by Name,G.HEADQTR");
    //}


    public DataSet getHqtrUserDpt(string Dept_Id)
    {
        string query = "select * from  GETHEADQUARTER WHERE 0=0 ";
        if (Dept_Id != "0")
            query += "and  Dept_Id = " + Dept_Id + " ";
        query += "order by Name,HEADQTR";
        return ds = data.getDataSet(query);
    }

    public DataSet GetSecondarySaleTargetMain(string EMPID, string APP_DATE, string MINVISIT, string TOTALQTY, string ID, string Amount)
    {
        cmd = new SqlCommand("IU_SECONDARYSALESTARGET_MAIN");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EMPID", EMPID);
        cmd.Parameters.AddWithValue("@APP_DATE", APP_DATE);
        cmd.Parameters.AddWithValue("@MINVISIT", MINVISIT);
        cmd.Parameters.AddWithValue("@TOTALQTY", TOTALQTY);
        cmd.Parameters.AddWithValue("@ID", ID);
        cmd.Parameters.AddWithValue("@Amount", Amount);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet GetSecondarySaleTargetDetails(string MAINID, string ITEMGROUP, string QTY, string DETAILID, string Delid, string Incentive, string AmountFrom, string AmountTo)
    {
        cmd = new SqlCommand("IU_SECONDARYSALESTARGET_DETAILS");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@MAINID", MAINID);
        cmd.Parameters.AddWithValue("@ITEMGROUP", ITEMGROUP);
        cmd.Parameters.AddWithValue("@QTY", QTY);
        cmd.Parameters.AddWithValue("@DETAILID", DETAILID);
        cmd.Parameters.AddWithValue("@Delid", Delid);
        cmd.Parameters.AddWithValue("@Incentive", Incentive);
        cmd.Parameters.AddWithValue("@AmountFrom", AmountFrom);
        cmd.Parameters.AddWithValue("@AmountTo", AmountTo);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet GetDepartment(string ACTION, string DEPT_ID, string DEPT_NAME, string DELID, string DEPT_CODE)
    {
        cmd = new SqlCommand("GETDEPARTMENT");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@DEPT_ID", DEPT_ID);
        cmd.Parameters.AddWithValue("@DEPT_NAME", DEPT_NAME);
        cmd.Parameters.AddWithValue("@DELID", DELID);
        cmd.Parameters.AddWithValue("@DEPT_CODE", DEPT_CODE);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet GetAdminPolicy(string ACTION, string POLICY_ID, string Head, string POLICY, string DELID, string POLICY_DATE)
    {
        cmd = new SqlCommand("GETADMINPOLICY");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@POLICY_ID", POLICY_ID);
        cmd.Parameters.AddWithValue("@HEAD", Head);
        cmd.Parameters.AddWithValue("@POLICY", POLICY);
        cmd.Parameters.AddWithValue("@DELID", DELID);
        cmd.Parameters.AddWithValue("@POLICY_DATE", (POLICY_DATE != "") ? data.YYYYMMDD(POLICY_DATE) : POLICY_DATE);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet GetDocument(string ACTION, string DOCU_ID, string DOCU_NAME, string DELID)
    {
        cmd = new SqlCommand("GETDOCUMENT");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@DOCU_ID", DOCU_ID);
        cmd.Parameters.AddWithValue("@DOCU_NAME", DOCU_NAME);
        cmd.Parameters.AddWithValue("@DELID", DELID);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet GetDesignation(string ACTION, string DESG_ID, string DEPT_ID, string DESG_NAME, string DELID)
    {
        cmd = new SqlCommand("GETDESIGNATION");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@DESG_ID", DESG_ID);
        cmd.Parameters.AddWithValue("@DEPT_ID", DEPT_ID);
        cmd.Parameters.AddWithValue("@DESG_NAME", DESG_NAME);
        cmd.Parameters.AddWithValue("@DELID", DELID);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet get_UpdateSecondaryItems(string ACTION, string ID, string Group, string Itm, string Qty, string Rate)
    {
        cmd = new SqlCommand("PROC_UPDATESECONDARYITEMS");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@ID", ID);
        cmd.Parameters.AddWithValue("@grp", Group);
        cmd.Parameters.AddWithValue("@item", Itm);
        cmd.Parameters.AddWithValue("@qty", Qty);
        cmd.Parameters.AddWithValue("@Rate", Rate);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet GetHoliday(string ACTION, string HOLIDAY_ID, string HOLIDAY_NAME, string DELID, string DATEFROM, string DATETO)
    {
        cmd = new SqlCommand("GETHOLIDAY");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@HOLIDAY_ID", HOLIDAY_ID);
        cmd.Parameters.AddWithValue("@HOLIDAY_NAME", HOLIDAY_NAME);
        cmd.Parameters.AddWithValue("@DELID", DELID);
        cmd.Parameters.AddWithValue("@DATEFROM", DATEFROM);
        cmd.Parameters.AddWithValue("@DATETO", DATETO);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet GetAttandance(string ACTION, string EMPID, string USERID, string LATITUDEIN, string LONGITUDEIN, string ATTANDANCEDATE)
    {
        cmd = new SqlCommand("IU_HRATTANDANCE");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@EMPID", EMPID);
        cmd.Parameters.AddWithValue("@LATITUDEIN", LATITUDEIN);
        cmd.Parameters.AddWithValue("@LONGITUDEIN", LONGITUDEIN);
        cmd.Parameters.AddWithValue("@ATTANDANCEDATE", ATTANDANCEDATE);
        cmd.Parameters.AddWithValue("@USERID", USERID);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet IUD_Loan(string _Action, string _Id, string EMpId, string Loanamoaunt, string NoOfInstallment, string InstallmentAMount, string intrestRate, string loanDate, string LoanDeductDate)
    {
        cmd.CommandText = "SP_LOANDETAILINSERT";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@ACTION", _Action);
        cmd.Parameters.AddWithValue("@Id", _Id);
        cmd.Parameters.AddWithValue("@FK_EmpId", EMpId);
        cmd.Parameters.AddWithValue("@AMOUNT", Loanamoaunt);
        cmd.Parameters.AddWithValue("@INSTALLMENTS", NoOfInstallment);
        cmd.Parameters.AddWithValue("@INSTAMOUNT", InstallmentAMount);
        cmd.Parameters.AddWithValue("@INSRATE", intrestRate);
        cmd.Parameters.AddWithValue("@DAT", data.ConvertToDateTime(loanDate));
        cmd.Parameters.AddWithValue("@LOANDEDUCTDATE", data.ConvertToDateTime(LoanDeductDate));
        return data.getDataSet(cmd);
    }


    public DataSet IUD_AdvanceSalary(string ACTION, string ID, string VOC_NO, string VOC_DATE, string EMP_ID, string REMARKS, string AMOUNT, string DELID, string USERID)
    {
        cmd.CommandText = "IU_ADVANCESALARY";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@ID", ID);
        cmd.Parameters.AddWithValue("@VOC_NO", VOC_NO);
        cmd.Parameters.AddWithValue("@VOC_DATE", data.ConvertToDateTime(VOC_DATE));
        cmd.Parameters.AddWithValue("@EMP_ID", EMP_ID);
        cmd.Parameters.AddWithValue("@REMARKS", REMARKS);
        cmd.Parameters.AddWithValue("@AMOUNT", AMOUNT);
        cmd.Parameters.AddWithValue("@DELID", DELID);
        cmd.Parameters.AddWithValue("@USERID", USERID);
        DataSet ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet IU_LEAVE(string ACTION, string ID, string FK_EmpId, string Leave_Type, string Requested_leave, string From_date, string To_date, string Approved_Leave, string Approved_todate, string Approved_fromdate, string Reason, string Date, string IsConfirm, string Mgr_Approved, string confirmdate, string FK_AdminID, string LeaveDetect)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "IU_LEAVE";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@LEAVE_ID", ID);
        cmd.Parameters.AddWithValue("@FK_EmpId", FK_EmpId);
        cmd.Parameters.AddWithValue("@Leave_Type", Leave_Type);
        cmd.Parameters.AddWithValue("@Requested_leave", Requested_leave);
        cmd.Parameters.AddWithValue("@From_date", From_date);
        cmd.Parameters.AddWithValue("@To_date", To_date);
        cmd.Parameters.AddWithValue("@Approved_Leave", Approved_Leave);
        cmd.Parameters.AddWithValue("@Approved_todate", Approved_todate);
        cmd.Parameters.AddWithValue("@Approved_fromdate", Approved_fromdate);
        cmd.Parameters.AddWithValue("@Reason", Reason);
        cmd.Parameters.AddWithValue("@Date", Date);
        cmd.Parameters.AddWithValue("@IsConfirm", IsConfirm);
        cmd.Parameters.AddWithValue("@Mgr_Approved", Mgr_Approved);
        cmd.Parameters.AddWithValue("@confirmdate", confirmdate);
        cmd.Parameters.AddWithValue("@FK_AdminID", FK_AdminID);
        cmd.Parameters.AddWithValue("@LeaveDetect", LeaveDetect);
        DataSet ds = data.getDataSet(cmd);
        return ds;
    }


    public DataSet IU_NIGHTATTENDANCE(string ACTION, string NightAttendance_Id, string FK_EmpId, string Dept_Id, string AttendanceDate, string AttendanceDateTo, string Remarks)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "IU_NIGHTATTENDANCE";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@NightAttendance_Id", NightAttendance_Id);
        cmd.Parameters.AddWithValue("@FK_EmpId", FK_EmpId);
        cmd.Parameters.AddWithValue("@Dept_Id", Dept_Id);
        cmd.Parameters.AddWithValue("@AttendanceDate", AttendanceDate);
        cmd.Parameters.AddWithValue("@AttendanceDateTo", AttendanceDateTo);
        cmd.Parameters.AddWithValue("@Remarks", Remarks);
        DataSet ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getSalesSummaryOrder(string EMPID, string HQTR, string PARTY, string ORDDATE, string TYPE, string DEPT_ID, string EMP_STATUS)
    {
        cmd = new SqlCommand("GETSALESUMMARY_REPORT");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EMPID", EMPID);
        cmd.Parameters.AddWithValue("@HQTR", HQTR);
        cmd.Parameters.AddWithValue("@PARTY", PARTY);
        cmd.Parameters.AddWithValue("@ORDDATE", ORDDATE);
        cmd.Parameters.AddWithValue("@TYPE", TYPE);
        cmd.Parameters.AddWithValue("@DEPT_ID", DEPT_ID);
        cmd.Parameters.AddWithValue("@EMP_STATUS", EMP_STATUS);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet GetSallary(string _FromDate, string Dept_Id, string Desig_Id, string Rep_Manager, string PF, string STATUS)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GETSALLARYDATA_PROC1";
        //cmd.CommandText = "GETSALLARYDATA_PROC";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@MONTH", _FromDate);
        cmd.Parameters.AddWithValue("@Dept_Id", Dept_Id);
        cmd.Parameters.AddWithValue("@Desig_Id", Desig_Id);
        cmd.Parameters.AddWithValue("@Rep_Manager", Rep_Manager);
        cmd.Parameters.AddWithValue("@PF", PF);
        cmd.Parameters.AddWithValue("@STATUS", STATUS);
        DataSet dss = data.getDataSet(cmd);
        return dss;
    }


    public DataSet Appraisal(string EMP_ID, string PREV_NETSALARY, string APPRAISAL, string CUR_NETSALARY, string AFFECTIVE_DATE, string ID, string _UserId)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "IU_APPRAISAL";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@EMP_ID", EMP_ID);
        cmd.Parameters.AddWithValue("@PREV_NETSALARY", PREV_NETSALARY);
        cmd.Parameters.AddWithValue("@APPRAISAL", APPRAISAL);
        cmd.Parameters.AddWithValue("@CUR_NETSALARY", CUR_NETSALARY);
        cmd.Parameters.AddWithValue("@AFFECTIVE_DATE", AFFECTIVE_DATE);
        cmd.Parameters.AddWithValue("@CREATEUSER", _UserId);
        cmd.Parameters.AddWithValue("@ID", ID);
        DataSet dss = data.getDataSet(cmd);
        return dss;
    }

    public DataSet AppraisalD(string EMP_ID, string ID, string _UserId)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "IU_APPRAISALD";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@EMP_ID", EMP_ID);
        cmd.Parameters.AddWithValue("@CREATEUSER", _UserId);
        cmd.Parameters.AddWithValue("@ID", ID);
        DataSet dss = data.getDataSet(cmd);
        return dss;
    }


    public DataSet IpAddress(string Action, string Ip, string remark, string ID)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "PROC_IPADRS";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@ACTION", Action);
        cmd.Parameters.AddWithValue("@IPADRS", Ip);
        cmd.Parameters.AddWithValue("@RMK", remark);
        cmd.Parameters.AddWithValue("@ID", ID);
        DataSet dss = data.getDataSet(cmd);
        return dss;
    }

    public DataSet StationBeat(string Action, string STATION, string DISTRICT, string HEADQTR, string BEAT, string STATIONID, string BEATID)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "IU_STATIONBEAT";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@ACTION", Action);
        cmd.Parameters.AddWithValue("@STATION", STATION);
        cmd.Parameters.AddWithValue("@DISTRICT", DISTRICT);
        cmd.Parameters.AddWithValue("@HEADQTR", HEADQTR);
        cmd.Parameters.AddWithValue("@BEAT", BEAT);
        cmd.Parameters.AddWithValue("@STATIONID", STATIONID);
        cmd.Parameters.AddWithValue("@BEATID", BEATID);
        DataSet dss = data.getDataSet(cmd);
        return dss;
    }
}
