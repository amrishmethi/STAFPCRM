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
    DataSet ds = new DataSet();
    Master getdata = new Master();
    Data data = new Data();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("UserAssignReport.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];

            getdata.FillUser(drpUser);
            fillData();
        }
    }

    public void fillData()
    {
        ds = getdata.getUserDetails(drpUser.SelectedValue);
    
        rep.DataSource = ds.Tables[0];
        rep.DataBind();
    }

    //protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    if (e.CommandName == "Edit")
    //    {
    //        Response.Redirect("SecondarySalesPartyMaster.aspx?id=" + e.CommandArgument + "");
    //    }
    //    if (e.CommandName == "Delete")
    //    {
    //        string query = "update tbl_SecondarySalesParty set IsActive = 0  where ID=" + e.CommandArgument + "";
    //        data.executeCommand(query);
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Record Deleted Successfully......')", true);
    //        fillData();
    //    }
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }

    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString() + "," + tbl1.Rows[0]["AssignStatus"].ToString() + "," + tbl1.Rows[0]["LoginStatus"].ToString();
    }

    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink lnkAssbtn = (e.Item.FindControl("lnkAssbtn") as HyperLink);
            HiddenField hddUid = (HiddenField)e.Item.FindControl("hddUid");
            HiddenField hddUserType = (e.Item.FindControl("hddUserType") as HiddenField);
            lnkAssbtn.Visible = hddUserType.Value == "admin"?false:true;
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
        { data.getDataSet("Update [CSInfo].[dbo].[MobileAppUser]  set Deactivate = (case when Deactivate=1 then 0 else 1 end)  where id = " + hddUid.Value);
            fillData();
        }
        
        
    }
}