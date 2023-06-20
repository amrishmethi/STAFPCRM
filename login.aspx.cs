using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Net.Sockets;
using System.Net;

public partial class login : System.Web.UI.Page
{ 
    DataSet ds = new DataSet();
    Data data = new Data();
    SyncData syncData = new SyncData();
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + GetIp() + "')", true);
             
            HttpContext.Current.Response.Cookies["STFP"].Expires = DateTime.Now.AddDays(-1d);
            if (Request.Params["logout"] != null)
            {
                HttpContext.Current.Response.Cookies["STFP"].Expires = DateTime.Now.AddDays(-1d);
            }
        }
    }
    public string GetIp()
    {
        string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ip))
        {
            ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return ip;
    }

    public static string GetLocalIPAddress()
    {
        string hostName = Dns.GetHostName(); // Retrive the Name of HOST

        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

        return myIP;
        //var host = Dns.GetHostEntry(Dns.GetHostName());
        //foreach (var ip in host.AddressList)
        //{
        //    if (ip.AddressFamily == AddressFamily.InterNetwork)
        //    {
        //        return ip.ToString();
        //    }
        //}
        //throw new Exception("No network adapters with an IPv4 address in the system!");
    }
  

    protected void LogBtn_Click(object sender, EventArgs e)
    { 
        //if (txtuser.Value != "9829032422")
        //{
        //    if (!data.Exist("select * from tbl_IpAddress WHere Isdelete=0 and IPAdrs='" + GetIp() + "'"))
        //    {
        //        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('unauthorized access please share the ip address (" + GetIp() + ") to the administrator');window.location ='Login.aspx'", true);
        //        return;
        //    }
        //}
        //    string url = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"].ToString();
        if (txtuser.Value.Trim() != "" && txtpass.Value.Trim() != "")
        {
            cmd = new SqlCommand("Sp_Login");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UserName", txtuser.Value);
            cmd.Parameters.AddWithValue("@Password", txtpass.Value);
            ds = data.getDataSet(cmd);
            if (ds.Tables[0].Rows.Count > 0 )
            {
                if (ds.Tables[0].Rows[0]["isWhiteList"].ToString() == "False")
                {
                    if (!data.Exist("select * from tbl_IpAddress WHere Isdelete=0 and IPAdrs='" + GetIp() + "'"))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('unauthorized access please share the ip address (" + GetIp() + ") to the administrator');window.location ='Login.aspx'", true);
                        return;
                    }
                }
                HttpCookie Admin = new HttpCookie("STFP");
                Admin.Expires = DateTime.Now.AddDays(1d);
                Admin.Values.Add("MobileNo", txtuser.Value.Trim());
                Admin.Values.Add("UserName", ds.Tables[0].Rows[0]["Name"].ToString());
                Admin.Values.Add("UserId", ds.Tables[0].Rows[0]["ID"].ToString());
                Admin.Values.Add("EMP_ID", ds.Tables[0].Rows[0]["EMP_ID"].ToString());
                Admin.Values.Add("Type", ds.Tables[0].Rows[0]["UserType"].ToString());
                Response.Cookies.Add(Admin);

                Response.Redirect("~/Soft/Dashboard.aspx");
            }
            else
            {
                
                lblMes.Visible = true;
            }
        }
        else
        {
            lblMes.Visible = true;
        }
    }

    public void Login()
    {
        string url = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"].ToString();
        if (IsValid)
        {
            Response.Redirect(url + "/Soft/");
        }
    }
    public void ForgorPwdMail(string Name, string Email, string UserName, string Password)
    {
        string str = "";
        #region  Header 
        str = " <table width='100%' border='0' cellspacing='0' cellpadding='0'> ";
        str += " <tr>";
        str += " <td> ";
        str += " <table width='650' border='0' cellspacing='0' cellpadding='0' align='center'> ";
        str += " <tr> ";
        str += " <td style='border:1px solid #f4f5f7; border-radius:3px;'> ";
        str += " <table width='650' border='0' cellspacing='0' cellpadding='0'> ";
        str += "  <tr> ";
        str += " <td bgcolor='#6CD1BB' style='padding:10px;'>";
        str += " <a href='https://crm.tadkeshwarfoods.com/' target='_blank'> ";
        // str += "  <img src='https://crm.tadkeshwarfoods.com/images/logo.png' height='60px' width='auto' />";
        str += "  </a>";
        str += " <td>";
        str += " </tr>";

        #endregion

        #region Content Region
        str += "  <tr> ";
        str += " <td> ";
        str += " <table width='98%' border='0' cellspacing='0' cellpadding='5' align='center'> ";

        str += "  <tr> ";
        str += " <td align='center' style='border:1px solid #d6d4d5; background-color:#f8f8f8; color:#666;'> ";
        str += " <table width='100%' border='0' cellspacing='0' cellpadding='5'>";
        str += " <tr> <td> </td> </tr>";
        str += " <tr> <td><strong>Dear </strong> " + Name + " </td> </tr>";
        str += " <tr> <td><strong> Your login credentials are as follows, User Name : " + UserName + " and Password : " + Password + " for airblr.in . </td> </tr>";

        str += " <tr> <td>&nbsp;</td> </tr>";
        str += " <tr> <td>&nbsp;</td> </tr>";

        str += " <tr> <td>&nbsp;</td> </tr>";
        str += " </table> ";
        str += " </td> ";
        str += " </tr> ";


        str += " <tr> <td></td> </tr>";
        str += " <tr> <td>&nbsp;</td> </tr>";
        str += " <tr> <td></td> </tr>";

        str += " </table> ";
        str += " </td> ";
        str += " </tr> ";
        #endregion

        #region Footer
        str += " </table> ";
        str += " </td> ";
        str += " </tr> ";
        str += " </table> ";
        str += " </td> ";
        str += " </tr>";
        str += " </table> ";
        #endregion 
        MailJet MC = new MailJet();
        MC.sendmailtoclient(Email.Trim(), str, "Credentials Detail ");
    }

    protected void lnkFgetPwd_Click(object sender, EventArgs e)
    {
        //  ForgorPwdMail("", "", "", "");
    }
}