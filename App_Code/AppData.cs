using Mailjet.Client.Resources;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Protocols.WSTrust;
using System.Web;
using System.Web.Services.Description;
 
public class AppData
{
    DataSet ds = new DataSet();
    Data data = new Data();
    SqlCommand cmd = new SqlCommand();
    string query = ""; 
    int res = 0;
    HttpCookie Soft;
    string userid = "";
    public AppData()
    { 
    } 

    public DataSet GetPrimaryPartyLocUpdate(string PartyId, string PartyName, string MobileNo)
    {
        cmd = new SqlCommand("usp_API_PrimaryPartyLocUpdate");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@PartyId", PartyId);
        cmd.Parameters.AddWithValue("@PartyName", PartyName);
        cmd.Parameters.AddWithValue("@MobileNo", MobileNo); 
        ds = data.getDataSet(cmd);
        return ds;
    }
}