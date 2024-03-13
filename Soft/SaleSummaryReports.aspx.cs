﻿using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Soft_SaleSummaryReports : System.Web.UI.Page
{
    DataTable dtCustom = new DataTable();
    Workbook workbook1 = new Workbook();
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    bool a = false;
    private HttpCookie Soft;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("UserWiseParty.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            //dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            bindDrp();
            gd.FillPrimaryParty(drpParty);
            gd.FillPrimaryStation(Drpstation);
            gd.FillGroup(drpGrp);
        }
    }

    private void bindDrp()
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        dv.Sort = "Name";
        DrpEmployee.DataSource = dv.ToTable(true, "Name", "MID");
        DrpEmployee.DataTextField = "Name";
        DrpEmployee.DataValueField = "MID";
        DrpEmployee.DataBind();
        DrpEmployee.Items.Insert(0, new ListItem("Select", "0"));
        dv.Sort = "HeadQtr";
        drpHeadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNO");
        drpHeadQtr.DataTextField = "HeadQtr";
        drpHeadQtr.DataValueField = "HeadQtrNO";
        drpHeadQtr.DataBind();
        drpHeadQtr.Items.Insert(0, new ListItem("Select", "0"));
        dv.Sort = "district";
        drpDistict.DataSource = dv.ToTable(true, "district", "districtNo");
        drpDistict.DataTextField = "district";
        drpDistict.DataValueField = "districtNo";
        drpDistict.DataBind();
        drpDistict.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void fillData()
    {
        string head = drpHeadQtr.SelectedValue;
        string station = Drpstation.SelectedValue;
        string party = drpParty.SelectedValue;
        string rate = Drprate.SelectedValue;
        string grp = "0";
        foreach (ListItem item in drpGrp.Items)
        {
            if (item.Selected)
            {
                grp += "," + item.Value;
            }
        }

        string district = "0";
        foreach (ListItem item in drpDistict.Items)
        {
            if (item.Selected)
            {
                district += "," + item.Value;
            }
        }

        ds = getdata.getSaleSummaryReportSTDynamic(head, district, drpReportType.SelectedValue, station, dpFrom.Text, dpTo.Text, rate, party, grp);
        if (ds.Tables[0].Rows.Count > 0)
        {
            CreateTable(ds.Tables[0].Columns);
            DataView dvv = ds.Tables[0].DefaultView;

            DataTable dvvr = dvv.ToTable(true, "HEADQTR", "DISTRICT", "Station", "AcName");
            int SNo = 1;

            foreach (DataRow row in dvvr.Rows)
            {
                int _Total = 0;
                DataRow drrr = dtCustom.NewRow();
                drrr["SNo"] = SNo++;
                drrr["HEADQTR"] = row["HEADQTR"];
                drrr["DISTRICT"] = row["DISTRICT"];
                drrr["AcName"] = row["AcName"];
                drrr["Station"] = row["Station"];
                drrr["AMOUNT"] = ds.Tables[0].AsEnumerable()
                    .Where(myRow => myRow.Field<string>("HEADQTR") == row["HEADQTR"].ToString())
                    .Where(myRow => myRow.Field<string>("DISTRICT") == row["DISTRICT"].ToString())
                    .Where(myRow => myRow.Field<string>("Station") == row["Station"].ToString())
                    .Where(myRow => myRow.Field<string>("AcName") == row["AcName"].ToString())
                    .Sum(myRow => myRow.Field<decimal>("AMOUNT"));

                foreach (DataRow cc in ds.Tables[1].Rows)
                {
                    drrr[cc[0].ToString()] = ds.Tables[0].AsEnumerable()
                    .Where(myRow => myRow.Field<string>("HEADQTR") == row["HEADQTR"].ToString())
                    .Where(myRow => myRow.Field<string>("DISTRICT") == row["DISTRICT"].ToString())
                    .Where(myRow => myRow.Field<string>("Station") == row["Station"].ToString())
                    .Where(myRow => myRow.Field<string>("AcName") == row["AcName"].ToString())
                    .Sum(myRow => myRow.Field<Int32?>(cc[0].ToString()));

                    _Total += Convert.ToInt32(ds.Tables[0].AsEnumerable()
                    .Where(myRow => myRow.Field<string>("HEADQTR") == row["HEADQTR"].ToString())
                    .Where(myRow => myRow.Field<string>("DISTRICT") == row["DISTRICT"].ToString())
                    .Where(myRow => myRow.Field<string>("Station") == row["Station"].ToString())
                    .Where(myRow => myRow.Field<string>("AcName") == row["AcName"].ToString())
                    .Sum(myRow => myRow.Field<Int32?>(cc[0].ToString())));


                    cc[1] = ds.Tables[0].AsEnumerable()
                   .Sum(myRow => myRow.Field<Int32?>(cc[0].ToString()));
                    ds.Tables[1].AcceptChanges();
                }
                drrr["Total"] = _Total;
                dtCustom.Rows.Add(drrr);
            }

            DataRow drrr1 = dtCustom.NewRow();
            drrr1["AcName"] = "Total";
            drrr1["AMOUNT"] = ds.Tables[0].AsEnumerable()
                   .Sum(myRow => myRow.Field<decimal>("AMOUNT"));
            ds.Tables[1].AcceptChanges();
            foreach (DataRow cc in ds.Tables[1].Rows)
            {
                drrr1[cc[0].ToString()] = cc[1].ToString();
            }
            drrr1["Total"] = ds.Tables[1].Compute("sum(Qty)", "");
            dtCustom.Rows.Add(drrr1);

            ViewState["HQPendingOrderSummary"] = dtCustom;
            grdReport.DataSource = dtCustom;
            grdReport.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('No Record Found');", true);
        }
    }

    private void CreateTable(DataColumnCollection columns)
    {
        foreach (DataColumn row in columns)
        {
            dtCustom.Columns.Add(row.ColumnName);
        }
        dtCustom.Columns.Add("Total");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillData();
    }

    protected void drpHeadQtr_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        // Bind data to district dropdown list based on selected HeadQtr
        string selectedHeadQtr = drpHeadQtr.SelectedValue;
        dv.RowFilter = "HeadQtrNo = '" + selectedHeadQtr + "'";
        dv.Sort = "district";
        drpDistict.DataSource = dv.ToTable(true, "district", "districtNo");
        drpDistict.DataTextField = "district";
        drpDistict.DataValueField = "districtNo";
        drpDistict.DataBind();
        drpDistict.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void DrpEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        // Bind data to district dropdown list based on selected HeadQtr
        string selectedName = DrpEmployee.SelectedValue;
        dv.RowFilter = "Mid = '" + selectedName + "'";
        dv.Sort = "HeadQtr";
        drpHeadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNO");
        drpHeadQtr.DataTextField = "HeadQtr";
        drpHeadQtr.DataValueField = "HeadQtrNO";
        drpHeadQtr.DataBind();
        drpHeadQtr.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void drpDistict_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
        DataView dv = dsusr.Tables[0].DefaultView;
        // Bind data to district dropdown list based on selected HeadQtr
        string selecteddistrict = drpDistict.SelectedValue;
        dv.RowFilter = "districtNo = '" + selecteddistrict + "'";
        dv.Sort = "Station";
        Drpstation.DataSource = dv.ToTable(true, "Station", "StationNo");
        Drpstation.DataTextField = "Station";
        Drpstation.DataValueField = "StationNo";
        Drpstation.DataBind();
        Drpstation.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void DownLoadFile()
    {
        DataTable dtt = (DataTable)ViewState["HQPendingOrderSummary"];
        string Dpath = "";
        EXcelDownload(dtt);
        string url1 = HttpContext.Current.Request.Url.AbsoluteUri;
        if (url1.Contains("localhost"))
            Dpath = Server.MapPath("//ExcelDownload//Format.xlsx");
        else
            Dpath = "E:\\SSCOMP\\STAFPCRM\\ExcelDownload\\Format.xlsx";
        workbook1.SaveToFile(Dpath);
        string ff = "HQPendingOrderSummary.xlsx";
        string filePath = "ExcelDownload/Format.xlsx";
        Response.ContentType = "application/excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + ff + "\"");
        Response.TransmitFile(Dpath);
        Response.End();

    }

    private void EXcelDownload(DataTable dtt)
    {

        string path = "";
        string url1 = HttpContext.Current.Request.Url.AbsoluteUri;
        if (url1.Contains("localhost"))
            path = Server.MapPath("//Excel//Format.xlsx");
        else
            path = "E:\\SSCOMP\\STAFPCRM\\Excel\\Format.xlsx";
        int iRowCnt = dtt.Columns.Count;
        workbook1.LoadFromFile(path);
        a = true;
        Worksheet worksheet = workbook1.Worksheets[0];
        worksheet.Range[1, 1].Text = "HQ Pending Order Summary Report of " + drpHeadQtr.SelectedItem.Text;
        worksheet.Range[2, 1].Text = "Date As On " + dpFrom.Text + " To " + dpTo.Text;

        worksheet.Range[1, 1, 1, iRowCnt].Merge(true);
        worksheet.Range[1, 1].Style.Font.FontName = "Calibri";
        worksheet.Range[1, 1].Style.Font.IsBold = true;
        worksheet.Range[1, 1].Style.Font.Size = 20;
        worksheet.Range[1, 1].HorizontalAlignment = HorizontalAlignType.Center;

        worksheet.Range[2, 1, 2, iRowCnt].Merge(true);
        worksheet.Range[2, 1].Style.Font.FontName = "Calibri";
        worksheet.Range[2, 1].Style.Font.IsBold = true;
        worksheet.Range[2, 1].Style.Font.Size = 16;
        worksheet.Range[2, 1].HorizontalAlignment = HorizontalAlignType.Center;
        worksheet.Range[1, 1].AutoFitColumns();
        worksheet.Range[1, 1].AutoFitRows();
        worksheet.Range[2, 1].AutoFitColumns();
        worksheet.Range[2, 1].AutoFitRows();

        worksheet.Range[3, 1, 3, iRowCnt].BorderAround(LineStyleType.Thin);
        worksheet.Range[3, 1, 3, iRowCnt].BorderInside(LineStyleType.Thin);
        for (int i = 1; i < dtt.Columns.Count + 1; i++)
        {
            worksheet.Range[3, i].Text = dtt.Columns[i - 1].ColumnName.ToUpper();
            worksheet.Range[3, i].Style.Font.FontName = "Calibri";
            worksheet.Range[3, i].Style.Font.IsBold = true;
            worksheet.Range[3, i].Style.Font.Size = 12;
        }

        int _cnt;
        for (int i = 0; i < dtt.Rows.Count; i++)
        {
            _cnt = 0;
            for (int j = 0; j < dtt.Columns.Count; j++)
            {
                _cnt++;
                try
                {
                    worksheet.Range[i + 4, _cnt].Text = dtt.Rows[i][j].ToString().Trim();
                }
                catch { }
            }
        }
        worksheet.AllocatedRange.BorderAround(LineStyleType.Thin);
        worksheet.AllocatedRange.BorderInside(LineStyleType.Thin);
        worksheet.AutoFitColumn(iRowCnt);
        worksheet.AutoFitRow(dtt.Rows.Count);
        worksheet.AllocatedRange.AutoFitColumns();
        worksheet.AllocatedRange.AutoFitRows();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DownLoadFile();
    }
}