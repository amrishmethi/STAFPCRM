using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Admin_UserAssignReport : System.Web.UI.Page
{
    SyncData syncData = new SyncData();
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData Gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("UserAssignReport.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];

            Gd.FillUser1(drpUser);
            Gd.fillDepartment(drpDept);
            fillData();
        }
    }

    public void fillData()
    {
        ds = getdata.getUserDetails(drpUser.SelectedValue,drpDept.SelectedValue);
        rep.DataSource = ds.Tables[0];
        rep.DataBind();
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    fillData();
    //}

    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString() + "," + tbl1.Rows[0]["AssignStatus"].ToString() + "," + tbl1.Rows[0]["LoginStatus"].ToString() + "," + tbl1.Rows[0]["HrStatus"].ToString();
    }

    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink lnkAssbtn = (e.Item.FindControl("lnkAssbtn") as HyperLink);
            HyperLink lnkHrbtn = (e.Item.FindControl("lnkHrbtn") as HyperLink);
            HiddenField hddUid = (HiddenField)e.Item.FindControl("hddUid");
            HiddenField hddUserType = (e.Item.FindControl("hddUserType") as HiddenField);
            lnkAssbtn.Visible = hddUserType.Value == "admin" ? false : true;
            lnkHrbtn.Visible = hddUserType.Value == "admin" ? false : true;
        }
    }

    protected void IsChkLogin_CheckedChanged(object sender, EventArgs e)
    {
        int ItemC = 0;
        string[] ClientID = ((CheckBox)sender).ClientID.Split('_');
        if (ClientID.Length == 4)
        {
            ItemC = Convert.ToInt32(ClientID[3]);
        }

        CheckBox chk = (CheckBox)rep.Items[ItemC].FindControl("IsChkLogin");
        HiddenField hddUid = (HiddenField)rep.Items[ItemC].FindControl("hddUid");
        if (hddUid.Value != "")
        {
            data.getDataSet("Update [CSInfo].[dbo].[MobileAppUser]  set isCrmLogin = (case when isCrmLogin=1 then 0 else 1 end)  where id = " + hddUid.Value);
            fillData();
        }


    }

    protected void IsChkLoginApp_CheckedChanged(object sender, EventArgs e)
    {
        int ItemC = 0;
        string[] ClientID = ((CheckBox)sender).ClientID.Split('_');
        if (ClientID.Length == 4)
        {
            ItemC = Convert.ToInt32(ClientID[3]);
        }

        CheckBox chk = (CheckBox)rep.Items[ItemC].FindControl("IsChkLoginApp");
        HiddenField hddUid = (HiddenField)rep.Items[ItemC].FindControl("hddUid");
        if (hddUid.Value != "")
        {
            data.getDataSet("Update [CSInfo].[dbo].[MobileAppUser]  set Deactivate = (case when Deactivate=1 then 0 else 1 end)  where id = " + hddUid.Value);
            fillData();
        }
    }

    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillData();
    }

    protected void isCHkWitelist_CheckedChanged(object sender, EventArgs e)
    {
        int ItemC = 0;
        string[] ClientID = ((CheckBox)sender).ClientID.Split('_');
        if (ClientID.Length == 4)
        {
            ItemC = Convert.ToInt32(ClientID[3]);
        }

        CheckBox chk = (CheckBox)rep.Items[ItemC].FindControl("isCHkWitelist");
        HiddenField hddUid = (HiddenField)rep.Items[ItemC].FindControl("hddUid");
        if (hddUid.Value != "")
        {
            data.getDataSet("Update [CSInfo].[dbo].[MobileAppUser]  set isWhiteList = (case when isWhiteList=1 then 0 else 1 end)  where id = " + hddUid.Value);
            fillData();
        }
    }

    protected void btnSync_Click(object sender, EventArgs e)
    {
        GetUserData();
    }

    private void GetUserData()
    {
        string QBind = " INSERT INTO [csinfo].[dbo].[MobileAppUser] ([id],[Name],[MobileNo],[Password],[ExpiryDate],[Deactivate],[RegNo],[AppSoftCode],[UserType],[CreateDate],[ModifiedDate],[isCrmLogin])";
        string _QBind = "";
        DataSet dsUser = syncData.getDataSet("select * FROM [CSinfo].[dbo].[MobileAppUser]");
        foreach (DataRow drr in dsUser.Tables[0].Rows)
        {
            if (!data.Exist("select * FROM [CSinfo].[dbo].[MobileAppUser] WHERE ID=" + drr["ID"]))
            {
                _QBind = " Select '" + drr["id"] + "','" + drr["Name"] + "','" + drr["MobileNo"] + "','" + drr["Password"] + "','" + drr["ExpiryDate"] + "','" + drr["Deactivate"] + "','" + drr["RegNo"] + "','" + drr["AppSoftCode"] + "','" + drr["UserType"] + "','" + drr["CreateDate"] + "','" + drr["ModifiedDate"] + "','" + drr["isCrmLogin"] + "' ";
                string NQBind = QBind + _QBind;
                data.executeCommand(NQBind);
            }
        }


        DataSet dsUser1 = syncData.getDataSet("select * FROM [CSinfo].[dbo].[MobileAppUser] where Cast([ModifiedDate] as date)=Cast(getdate() as date)");
        foreach (DataRow drr in dsUser1.Tables[0].Rows)
        {
            _QBind = " Update  [CSinfo].[dbo].[MobileAppUser] SET [Password]='" + drr["Password"] + "' WHERE id='" + drr["id"] + "' ";
            data.executeCommand(_QBind);
        }
    }
}