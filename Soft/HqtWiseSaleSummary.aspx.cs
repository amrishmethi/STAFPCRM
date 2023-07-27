using Spire.Xls;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;  
 

public partial class Soft_HqtWiseSaleSummary : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;
    DataTable dtCustom = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("UserWiseParty.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
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
        ds = getdata.getSaleSummaryReportSTHQ(head, district, drpReport.SelectedValue, station, dpFrom.Text, dpTo.Text, rate, party, grp);

        CreateTable(ds.Tables[0].Columns);
        DataView dvv = ds.Tables[0].DefaultView;
        DataTable dvvr = dvv.ToTable(true, "HeadQtr", "District", "Station", "AcName");
        int SNo = 1;

        foreach (DataRow row in dvvr.Rows)
        {
            DataRow drrr = dtCustom.NewRow();
            drrr["SNo"] = SNo++;
            drrr["HeadQtr"] = row["HeadQtr"];
            drrr["District"] = row["District"];
            drrr["Station"] = row["Station"];
            drrr["AcName"] = row["AcName"];
            drrr["AMOUNT"] = ds.Tables[0].AsEnumerable()
                .Where(myRow => myRow.Field<string>("HeadQtr") == row["HeadQtr"].ToString())
                .Where(myRow => myRow.Field<string>("District") == row["District"].ToString())
                .Where(myRow => myRow.Field<string>("Station") == row["Station"].ToString())
                .Where(myRow => myRow.Field<string>("AcName") == row["AcName"].ToString())
                .Sum(myRow => myRow.Field<decimal>("AMOUNT"));

            foreach (DataRow cc in ds.Tables[1].Rows)
            {
                drrr[cc[0].ToString()] = ds.Tables[0].AsEnumerable()
                .Where(myRow => myRow.Field<string>("HeadQtr") == row["HeadQtr"].ToString())
                .Where(myRow => myRow.Field<string>("District") == row["District"].ToString())
                .Where(myRow => myRow.Field<string>("Station") == row["Station"].ToString())
                .Where(myRow => myRow.Field<string>("AcName") == row["AcName"].ToString())
                .Sum(myRow => myRow.Field<Int32?>(cc[0].ToString()));


                cc[1] = ds.Tables[0].AsEnumerable()
               .Sum(myRow => myRow.Field<Int32?>(cc[0].ToString()));
                ds.Tables[1].AcceptChanges();
            }
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
        dtCustom.Rows.Add(drrr1);
        ViewState["SaleSUmmary"] = dtCustom;
        grdReport.DataSource = dtCustom;
        grdReport.DataBind();
    }

    private void CreateTable(DataColumnCollection columns)
    {
        foreach (DataColumn row in columns)
        {
            dtCustom.Columns.Add(row.ColumnName);
        }
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
        dv.RowFilter = "MID = '" + selectedName + "'";
        dv.Sort = "HeadQtr";
        drpHeadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
        drpHeadQtr.DataTextField = "HeadQtr";
        drpHeadQtr.DataValueField = "HeadQtrNo";
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
        Drpstation.DataSource = dv.ToTable(true, "Station", "StationNO");
        Drpstation.DataTextField = "Station";
        Drpstation.DataValueField = "StationNO";
        Drpstation.DataBind();
        Drpstation.Items.Insert(0, new ListItem("Select", "0"));
    }



    public void DownLoadFile()
    {
        DataTable dtt = (DataTable)ViewState["SaleSUmmary"];

        string path = Server.MapPath("");

        if (!Directory.Exists(path))   // CHECK IF THE FOLDER EXISTS. IF NOT, CREATE A NEW FOLDER.
        {
            Directory.CreateDirectory(path);
        }
        int iRowCnt = dtt.Columns.Count;

        File.Delete(path + "SaleSummary.xlsx");

        Workbook workbook = new Workbook();
        workbook.LoadFromFile(path + "\\Format.xlsx");
        Worksheet worksheet = workbook.Worksheets[0];
        worksheet.Range[1, 1].Text = "Sale Summary Report of " + drpHeadQtr.SelectedItem.Text;
        worksheet.Range[2, 1].Text = "Date As On " + dpFrom.Text + " To " + dpTo.Text;
        worksheet.Range[1, 1, 1, iRowCnt].Merge(true);
        worksheet.Range[2, 1, 2, iRowCnt].Merge(true);
        worksheet.Range[1, 1].Style.Font.FontName = "Calibri";
        worksheet.Range[1, 1].Style.Font.IsBold = true;
        worksheet.Range[1, 1].Style.Font.Size = 20;
        worksheet.Range[1, 1].HorizontalAlignment = HorizontalAlignType.Center;

        worksheet.Range[2, 1].Style.Font.FontName = "Calibri";
        worksheet.Range[2, 1].Style.Font.IsBold = true;
        worksheet.Range[2, 1].Style.Font.Size = 20;
        worksheet.Range[2, 1].HorizontalAlignment = HorizontalAlignType.Center;


        // ADD A WORKSHEET.


        // ROW ID FROM WHERE THE DATA STARTS SHOWING.


        //// SHOW THE HEADER.
        //worksheet.Cells[1, 1] = "Sale Summary Report of " + drpHeadQtr.SelectedItem.Text;


        //range.Font.Name = "Calibri";
        //range.Font.Bold = true;
        //range.Font.Size = 20;
        //range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        //xlWorkSheetToExport.Range[xlWorkSheetToExport.Cells[1, 1], xlWorkSheetToExport.Cells[1, iRowCnt]].Merge();


        //xlWorkSheetToExport.Cells[2, 1] = "Date As On " + dpFrom.Text + " To " + dpTo.Text;
        //Excel.Range range2 = xlWorkSheetToExport.Cells[2, 1] as Excel.Range;
        //range2.Font.Name = "Calibri";
        //range2.Font.Bold = true;
        //range2.Font.Size = 16;
        //range2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        //xlWorkSheetToExport.Range[xlWorkSheetToExport.Cells[2, 1], xlWorkSheetToExport.Cells[2, iRowCnt]].Merge();




        //int i;
        //int _cnt = 0;
        //for (i = 1; i < dtt.Columns.Count + 1; i++)
        //{
        //    xlWorkSheetToExport.Cells[3, i] = dtt.Columns[i - 1].ColumnName.ToUpper();
        //    Excel.Range rng = xlWorkSheetToExport.Cells[3, i] as Excel.Range;
        //    rng.EntireRow.Font.Name = "Calibri";
        //    rng.EntireRow.Font.Bold = true;
        //    rng.EntireRow.Font.Size = 16;
        //}
        //for (i = 0; i < dtt.Rows.Count; i++)
        //{
        //    _cnt = 0;
        //    for (int j = 0; j < dtt.Columns.Count; j++)
        //    {
        //        _cnt++;
        //        try
        //        {
        //            xlWorkSheetToExport.Cells[i + 4, _cnt] = "'" + dtt.Rows[i][j].ToString().Trim();
        //        }
        //        catch { }
        //    }
        //}

        //Excel.Range range1 = xlAppToExport.ActiveCell.Worksheet.Cells[4, 1] as Excel.Range;
        ////range1.AutoFormat(ExcelAutoFormat.xlRangeAutoFormatList3);

        //// SAVE THE FILE IN A FOLDER.
        //xlWorkSheetToExport.SaveAs(path + "SaleSummary.xlsx");

        //// CLEAR.
        //xlAppToExport.Workbooks.Close();
        //xlAppToExport.Quit();
        //xlAppToExport = null;
        //xlWorkSheetToExport = null; 

        string name = Server.MapPath("\\") + "Upload\\SaleSummary.xlsx";


        workbook.SaveToFile(name);

        string ff = "SaleSummary.xlsx";
        string filePath = "SaleSummary.xlsx";
        Response.ContentType = "application/excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + ff + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DownLoadFile();
    }

    private void ExcelDocViewer(string fileName)
    {
        try
        {
            System.Diagnostics.Process.Start(fileName);
        }
        catch { }
    }
}