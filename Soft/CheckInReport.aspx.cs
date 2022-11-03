using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CheckInReport : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    Data data = new Data();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //  if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            // Soft = Request.Cookies["STFP"];

            //Session["AccessRigthsSet"] = getdata.AccessRights("SecondarySalesParty.aspx", Soft["Type"] == "Soft" ? "0" : Soft["UserId"]).Tables[0];
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            getdata.FillUser(drpUser);
            fillData();
        }
    }

    public void fillData()
    {
        ds = getdata.getCheckInDetails(drpUser.SelectedValue, dpFrom.Text.Trim(), dpTo.Text.Trim());
        //DataView dv = new DataView(ds.Tables[0]);
        //StringBuilder filter = new StringBuilder();
        //filter.Append(" 1=1 ");
        //if (drpUser.SelectedIndex > 0)
        //{ filter.Append(" and UserId = " + drpUser.SelectedValue); }

        //filter.Append(" and CheckDate >='" + dpFrom.Text + "' and CheckDate <='" + dpTo.Text + "'");
        //dv.RowFilter = filter.ToString();

        ////string _Filter = " 0=0";
        ////if (drpUser.SelectedIndex > 0)
        ////    _Filter += " and UserId = " + drpUser.SelectedValue;

        ////_Filter += " and CheckDate >='" + dpFrom.Text + "' and CheckDate <='" + dpTo.Text + "'";


        ////dv.RowFilter = _Filter;
        //dv.Sort = "AddedDate Desc";
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


}