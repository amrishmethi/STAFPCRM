using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_primarypartylocation : System.Web.UI.Page
{
    AppData appData = new AppData();
    Data data = new Data();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            FillData("0", "", "");
        }
    } 
    public void FillData(string PartyId, string PartyName, string MobileNo)
    {
        ds = appData.GetPrimaryPartyLocUpdate(PartyId, PartyName, MobileNo);
        repPrimaryParty.DataSource = ds;
        repPrimaryParty.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string PartyName = string.IsNullOrEmpty(txtPartyName.Text.Trim()) ? "" : txtPartyName.Text.Trim();
        string MobileNo = string.IsNullOrEmpty(txtMobileNo.Text.Trim()) ? "" : txtMobileNo.Text.Trim();
        FillData("0", PartyName, MobileNo);
    }

    [WebMethod]
    public static int UpdateLocationStatus(string type, string PartyId)
    {
        Data data = new Data();
        int status = data.executeCommand("UPDATE PrimaryPartyLocation SET IsEditAble='1'  WHERE Id=" + PartyId);
        return status;
    }
}