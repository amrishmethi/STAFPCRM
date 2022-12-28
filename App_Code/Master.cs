using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;


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

    public DataSet getSecondarySalesParty(string action, string id, string stationid, string station, string name, string mobile, string whatsapp, string hqtr)
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
        cmd = new SqlCommand("Select CMsCode from [stm_stmast].[dbo].[CMaster] Where CMsSr='I' and MCMSCode='" + selectedValuee + "'");
        cmd.CommandType = CommandType.Text;
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

    public DataSet getUserDetails(string userid, string dptid)
    {
        cmd = new SqlCommand("PROC_USERDETAILS");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", userid);
        cmd.Parameters.AddWithValue("@DeptID", dptid);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet getUserReport(string userid, string mobile, string dept, string desg)
    {
        cmd = new SqlCommand("PROC_USERDETAILSREPORT");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", userid);
        cmd.Parameters.AddWithValue("@Mobno", mobile);
        cmd.Parameters.AddWithValue("@deptid", dept);
        cmd.Parameters.AddWithValue("@desgid", desg);
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
    public DataSet getCreateDealer(string action, string id, string name, string contper, string address,string zip, string station, string state, string gst, string gstRegType, string smsMob, string whatsapp, string district, string transport, string partyType, string partyCatg, string webdate)
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

    public DataSet getHqtrUser()
    {
        return ds = data.getDataSet("select * from [stm_acmast].[dbo].GETHEADQUARTER order by Name,HEADQTR");
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

    public DataSet GetAttandance(string ACTION, string EMPID, string USERID, string LATITUDEIN, string LONGITUDEIN)
    {
        cmd = new SqlCommand("IU_HRATTANDANCE");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@EMPID", EMPID);
        cmd.Parameters.AddWithValue("@USERID", USERID);
        cmd.Parameters.AddWithValue("@LATITUDEIN", LATITUDEIN);
        cmd.Parameters.AddWithValue("@LONGITUDEIN", LONGITUDEIN);
        ds = data.getDataSet(cmd);
        return ds;
    }
}
