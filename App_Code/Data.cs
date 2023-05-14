using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.IO;
/// <summary>
/// Summary description for Data
/// </summary>
public sealed class Data
{
    public string conString;
    public string storeid;
    SqlConnection sqlCon;
    SqlDataAdapter sqlAdp;
    SqlDataReader dr;
    SqlCommand cmd = new SqlCommand();
    DataSet ds;
    protected string QuickCost;
    public Data()
    {
        this.conString = System.Configuration.ConfigurationManager.ConnectionStrings["My_Con"].ToString();
        this.sqlCon = new SqlConnection(conString);
    }

    public SqlDataReader getDataReader(string commandString)
    {
        cmd.CommandText = commandString;
        cmd.Connection = sqlCon;
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return dr;
    }

    public bool Exist(string commandString)
    {
        bool flag;
        cmd.CommandText = commandString;
        cmd.Connection = sqlCon;
        sqlCon.Open();
        dr = cmd.ExecuteReader();
        dr.Read();

        if (dr.HasRows)
            flag = true;
        else
            flag = false;

        dr.Close();
        sqlCon.Close();
        return flag;
    }

    public DataSet getDataSet(string commandString)
    {
        ds = new DataSet();
        sqlAdp = new SqlDataAdapter(commandString, sqlCon);
        sqlAdp.Fill(ds);
        return ds;
    }

    public DataSet getDataSet(SqlCommand command)
    {
        command.Connection = sqlCon;
        ds = new DataSet();
        sqlAdp = new SqlDataAdapter(command);
        sqlAdp.Fill(ds);
        return ds;
    }

    public DataSet SP_getDataSet(SqlCommand command)
    {
        command.Connection = sqlCon;
        ds = new DataSet();
        sqlAdp = new SqlDataAdapter(command);
        sqlAdp.Fill(ds);
        return ds;
    }

    public int executeCommand(string commandString)
    {
        cmd.CommandText = commandString;
        cmd.Connection = sqlCon;
        int errStatus = 0;
        try
        {
            sqlCon.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            //File.WriteAllText(HttpContext.Current.Server.MapPath("~/uploadsimg/Employee/" + System.Guid.NewGuid().ToString() + ".txt"),
            //    ex.Message + ex.InnerException + ex.Source);
            errStatus = 1;

        }
        finally
        {
            sqlCon.Close();
        }
        return errStatus;
    }

    public int executeCommand(string commandString, out string str)
    {
        cmd.CommandText = commandString;
        cmd.Connection = sqlCon;
        str = "";
        int errStatus = 0;
        try
        {
            sqlCon.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            //File.WriteAllText(HttpContext.Current.Server.MapPath("~/uploadsimg/Employee/" + System.Guid.NewGuid().ToString() + ".txt"),
            //    ex.Message + ex.InnerException + ex.Source);
            errStatus = 1;
            str = ex.Message;
        }
        finally
        {
            sqlCon.Close();
        }
        return errStatus;
    }

    public int executeCommand(SqlCommand sqlCommand)
    {
        sqlCommand.Connection = sqlCon;
        int errStatus = 0;
        try
        {
            sqlCon.Open();
            sqlCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            string aa = ex.ToString();
            errStatus = 1;
        }
        finally
        {
            sqlCon.Close();
        }
        return errStatus;
    }

    public int executeCommand(SqlCommand sqlCommand, string PageName, string MethodName, string LoginId)
    {
        sqlCommand.Connection = sqlCon;
        int errStatus = 0;
        try
        {
            sqlCon.Open();
            sqlCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            LogError(ex.Message.ToString(), MethodName, PageName, ex.StackTrace.ToString(), LoginId);
            string aa = ex.ToString();
            errStatus = 1;
        }
        finally
        {
            sqlCon.Close();
        }
        return errStatus;
    }



    public int executeCommand(SqlCommand sqlCommand, out string str)
    {
        sqlCommand.Connection = sqlCon;
        int errStatus = 0;
        str = "";
        try
        {
            sqlCon.Open();
            sqlCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            errStatus = 1;
            str = ex.Message;
        }
        finally
        {
            sqlCon.Close();
        }
        return errStatus;
    }

    public DateTime ConvertToDateTime(string strDateTime)
    {
        DateTime dtFinaldate;
        string sDateTime;

        string[] sDate = strDateTime.Split('/');
        sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2];
        dtFinaldate = Convert.ToDateTime(sDateTime);

        return dtFinaldate;
    }

    public DateTime ConvertToDateTimeNew(string strDateTime, string strTime)
    {
        DateTime dtFinaldate;
        string sDateTime;
        string sTime;


        string[] sDate = strDateTime.Replace('-', '/').Split('/');
        string[] sTimee = strTime.Replace(' ', ':').Split(':');
        sTime = sTimee[0] + ':' + sTimee[1] + ' ' + sTimee[2];
        sTime = DateTime.Parse(sTime).ToString("HH:mm");

        sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2] + " " + sTime;
        dtFinaldate = Convert.ToDateTime(sDateTime);

        return dtFinaldate;
    }

    public string ConvertDDMMYYYY(string d)
    {
        string dat = d;
        string[] aa = dat.Split('/');
        dat = aa[1] + "/" + aa[0] + "/" + aa[2];
        return dat;
    }

    public string YYYYMMDD(string strDateTime)
    {
        if (strDateTime != "")
        {
            string sDateTime;
            string[] sDate = strDateTime.Split('/');
            sDateTime = sDate[2] + '-' + sDate[1] + '-' + sDate[0];
            return sDateTime;
        }
        return "";
    }

    public DataSet GetDataSetSP(SqlParameter[] arrParam, string strSPName)
    {
        cmd.Connection = sqlCon;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = strSPName;
        cmd.Parameters.Clear();
        cmd.CommandTimeout = 18000;
        for (int i = 0; i < arrParam.Length; i++)
        {
            cmd.Parameters.Add(arrParam[i]);
        }
        sqlAdp = new SqlDataAdapter();
        sqlAdp.SelectCommand = cmd;
        ds = new DataSet();
        sqlAdp.Fill(ds);
        return ds;
    }

    public void LogError(string ErrorName, string ErrorLocation, string Form, string ErrorDetails, string LoginId)
    {
        string q = "INSERT INTO [_TblLogError]([ErrorName],[ErrorLocation],[CreateUser],[FormName],[ErrorDetails]) VALUES (@ErrorName,@ErrorLocation,@CreateUser,@FormName,@ErrorDetails)";
        cmd.CommandText = q;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@ErrorName", ErrorName);
        cmd.Parameters.AddWithValue("@ErrorLocation", ErrorLocation);
        cmd.Parameters.AddWithValue("@CreateUser", LoginId);
        cmd.Parameters.AddWithValue("@FormName", Form);
        cmd.Parameters.AddWithValue("@ErrorDetails", ErrorDetails);
        executeCommand(cmd);
    }
}
