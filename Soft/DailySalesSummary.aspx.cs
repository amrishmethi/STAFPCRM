 
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Soft_DailySalesSummary : System.Web.UI.Page
{

    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    private HttpCookie Soft;
    bool a = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("UserWiseParty.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');

            gd.FillGroup(drpGrp);
            fillData();
        }
    }

    public void fillData()
    {

        string grp = "0";
        foreach (System.Web.UI.WebControls.ListItem item in drpGrp.Items)
        {
            if (item.Selected)
            {
                grp += "," + item.Value;
            }
        }
        ds = getdata.getDailySaleSummaryReportSTHQ(dpFrom.Text, dpTo.Text, grp, drpGroupType.SelectedValue, drpReportType.SelectedValue);
        DataTable dtCustom = ds.Tables[0];
        DataRow drrr = ds.Tables[0].NewRow();
        if (dtCustom.Rows.Count > 0)
        {
            foreach (DataRow cc in ds.Tables[1].Rows)
            {
                if (cc[0].ToString() != "")
                {
                    drrr[cc[0].ToString()] = ds.Tables[0].AsEnumerable()
                    .Sum(myRow => myRow.Field<Int32?>(cc[0].ToString()));
                }
            }
            ds.Tables[0].Rows.Add(drrr);
        }
        ViewState["SaleSUmmary"] = dtCustom;
        grdReport.DataSource = dtCustom;
        grdReport.DataBind();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportExcel(grdReport);
    }

    public void ExportExcel(DataGrid grd)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "DAILY SALES REPORT.xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        grd.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
}