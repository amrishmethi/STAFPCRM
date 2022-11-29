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



    public DataSet getSecondarySalesParty(string action, string id, string stationid, string station, string name, string mobile, string whatsapp)
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
    public DataSet getCheckInDetails(string userid, string indate, string intime)
    {
        cmd = new SqlCommand("PROC_CHECKINOUT");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", userid);
        cmd.Parameters.AddWithValue("@CheckinDateFrom", data.YYYYMMDD(indate));
        cmd.Parameters.AddWithValue("@CheckinDateTo", data.YYYYMMDD(intime));
        ds = data.getDataSet(cmd);
        return ds;
    }
    public DataSet getAttendanceList(string userid, string date)
    {
        cmd = new SqlCommand("PROC_ATTENDANCE");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", userid);
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
    public DataSet getUserDetails(string userid)
    {
        cmd = new SqlCommand("PROC_USERDETAILS");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", userid);
        ds = data.getDataSet(cmd);
        return ds;
    } public DataSet getUserReport(string userid,string mobile)
    {
        cmd = new SqlCommand("PROC_USERDETAILSREPORT");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", userid);
        cmd.Parameters.AddWithValue("@Mobno", mobile);
        ds = data.getDataSet(cmd);
        return ds;
    }
    public DataSet getUserTourPlan(string userid)
    {
        cmd = new SqlCommand("PROC_USERTOURPLAN");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", userid);
        ds = data.getDataSet(cmd);
        return ds;
    }
    public DataSet getSeconarySalesDetails(string userid, string party, string station, string indate, string intime)
    {
        cmd = new SqlCommand("PROC_SECONDARYSALES");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@USERID", userid);
        cmd.Parameters.AddWithValue("@PARTY", party);
        cmd.Parameters.AddWithValue("@STATION", station);
        cmd.Parameters.AddWithValue("@DATEFROM", data.YYYYMMDD(indate));
        cmd.Parameters.AddWithValue("@DATETO", data.YYYYMMDD(intime));
        ds = data.getDataSet(cmd);
        return ds;
    }

    public void FillParty(DropDownList drp)
    {
        query = "select * from tbl_SecondarySalesParty where IsActive=1 order by Name ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Name";
        drp.DataValueField = "ID";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    

    public void FillPrimaryStation(DropDownList drp)
    {
        query = "select distinct Station from [STM_AcMast].[dbo].[Station] order by Station ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Station";
        drp.DataValueField = "Station";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillPrimaryParty(DropDownList drp)
    {
        query = "select * from STM_ACMAST.DBO.ACCOUNT order by ACNAME ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "ACNAME";
        drp.DataValueField = "ACCOde";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void FillStation(DropDownList drp)
    {
        query = "select * from [STM_ACMAST].[DBO].[STATION] order by Station ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Station";
        drp.DataValueField = "Station";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void FillUser(DropDownList drp)
    {
        query = "select * from [CSInfo].[dbo].[MobileAppUser] order by Name ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Name";
        drp.DataValueField = "ID";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }


    public void FillDrop(DropDownList drp, String tbl)
    {
        query = "select * from " + tbl + " where IsDeleted=0 order by Name ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Name";
        drp.DataValueField = "ID";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public DataSet getHqtrUser()
    {
        query = "select HQD.HeadQtr,MU.Name, MU.id as MId from[STM_STMAST].[DBO].[HEADQTRDISTRICT]  HQD " +
"LEFT JOIN[STM_STMAST].[DBO].[MRMAST] M ON M.HEADQTR = HQD.HEADQTR " +
"LEFT JOIN[CSINFO].[DBO].[MOBILEAPPUSER] MU ON MU.MOBILENO = M.MOBILENO ";
        ds = data.getDataSet(query);
        return ds;
    }
    public string ConvertDDMMYYYY(string d)
    {
        string dat = d;
        string[] aa = dat.Split('/');
        dat = aa[1] + "/" + aa[0] + "/" + aa[2];
        return dat;
    }
//    @
//    @
//    @
//@
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

    public DataSet GetSecondarySaleTargetDetails(string MAINID, string ITEMGROUP, string QTY, string DETAILID, string Delid, string Incentive, string AMOUNTFROM, string AMOUNTTO)
    {
        cmd = new SqlCommand("IU_SECONDARYSALESTARGET_DETAILS");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@MAINID", MAINID);
        cmd.Parameters.AddWithValue("@ITEMGROUP", ITEMGROUP);
        cmd.Parameters.AddWithValue("@QTY", QTY);
        cmd.Parameters.AddWithValue("@DETAILID", DETAILID);
        cmd.Parameters.AddWithValue("@Delid", Delid);
        cmd.Parameters.AddWithValue("@Incentive", Incentive);
        cmd.Parameters.AddWithValue("@AMOUNTFROM", AMOUNTFROM);
        cmd.Parameters.AddWithValue("@AMOUNTTO", AMOUNTTO);
        ds = data.getDataSet(cmd);
        return ds;
    }
}
