using iTextSharp.text;
using iTextSharp.text.pdf;
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

public partial class Admin_ClientMeet : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    Data data = new Data();
    GetData Gd = new GetData();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];
            Session["AccessRigthsSet"] = getdata.AccessRights("ClientMeet.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];

            Gd.FillUser(drpEmp);
            Gd.fillDepartment(drpDept);

            txtDateFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            txtDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');

            bindDrp(true, true);
            fillData();
        }
    }

    private void bindDrp(bool isuser, bool ishqtr)
    {
        DataSet dsusr = getdata.getHqtrUser();
        DataView dv = dsusr.Tables[0].DefaultView;
        if (isuser)
        {
            if (drpHqtr.SelectedIndex > 0)
                dv.RowFilter = "HeadQtr='" + drpHqtr.SelectedItem.Text + "'";
            dv.Sort = "Name";
            drpEmp.DataSource = dv.ToTable(true, "Name", "MId");
            drpEmp.DataTextField = "Name";
            drpEmp.DataValueField = "MId";
            drpEmp.DataBind();
            drpEmp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
        if (ishqtr)
        {
            if (drpEmp.SelectedIndex > 0)
                dv.RowFilter = "Name='" + drpEmp.SelectedItem.Text + "'";
            dv.Sort = "HeadQtr";
            drpHqtr.DataSource = dv.ToTable(true, "HeadQtr");
            drpHqtr.DataTextField = "HeadQtr";
            drpHqtr.DataValueField = "HeadQtr";
            drpHqtr.DataBind();
            drpHqtr.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
    }

    public void fillData()
    {
        ds = getdata.getClientMeet(drpEmp.SelectedValue, drpHqtr.SelectedValue, drpType.SelectedValue, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), drpDept.SelectedValue);
        DataView dv = ds.Tables[0].DefaultView;
        if (drpIsMeet.SelectedValue == "0") { dv.RowFilter = " AddedDate is null"; }
        else if (drpIsMeet.SelectedValue == "1") { dv.RowFilter = "AddedDate <> ''"; }

        rep.DataSource = dv.ToTable();
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
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }

    protected void drpEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillData();

        DropDownList ddl = sender as DropDownList;
        if (ddl == drpEmp)
        {
            bindDrp(false, true);
        }
        if (ddl == drpHqtr)
        {
            bindDrp(true, false);
        }

    }
    //private void GeneratePDF(DataTable dataTable, string Name)
    //{
    //    try
    //    {
    //        string[] columnNames = (from dc in dataTable.Columns.Cast<DataColumn>()
    //                                select dc.ColumnName).ToArray();
    //        int Cell = 0;
    //        int count = columnNames.Length;
    //        object[] array = new object[count];

    //        dataTable.Rows.Add(array);

    //        Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
    //        System.IO.MemoryStream mStream = new System.IO.MemoryStream();
    //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, mStream);
    //        int cols = dataTable.Columns.Count;
    //        int rows = dataTable.Rows.Count;


    //        HeaderFooter header = new HeaderFooter(new Phrase(Name), false);

    //        Remove the border that is set by default
    //        header.Border = iTextSharp.text.Rectangle.TITLE;
    //        Align the text: 0 is left, 1 center and 2 right.
    //       header.Alignment = Element.ALIGN_CENTER;
    //        pdfDoc.Header = header;
    //        Header.
    //       pdfDoc.Open();
    //        iTextSharp.text.Table pdfTable = new iTextSharp.text.Table(cols, rows);
    //        pdfTable.BorderWidth = 1; pdfTable.Width = 100;
    //        pdfTable.Padding = 1; pdfTable.Spacing = 4;

    //        creating table headers
    //        for (int i = 0; i < cols; i++)
    //        {
    //            Cell cellCols = new Cell();
    //            Chunk chunkCols = new Chunk();
    //            cellCols.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#548B54"));
    //            iTextSharp.text.Font ColFont = FontFactory.GetFont(FontFactory.HELVETICA, 14, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.WHITE);

    //            chunkCols = new Chunk(dataTable.Columns[i].ColumnName, ColFont);

    //            cellCols.Add(chunkCols);
    //            pdfTable.AddCell(cellCols);
    //        }
    //        creating table data(actual result)

    //        for (int k = 0; k < rows; k++)
    //        {
    //            for (int j = 0; j < cols; j++)
    //            {
    //                Cell cellRows = new Cell();
    //                if (k % 2 == 0)
    //                {
    //                    cellRows.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#cccccc")); ;
    //                }
    //                else { cellRows.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#ffffff")); }
    //                iTextSharp.text.Font RowFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
    //                Chunk chunkRows = new Chunk(dataTable.Rows[k][j].ToString(), RowFont);
    //                cellRows.Add(chunkRows);

    //                pdfTable.AddCell(cellRows);
    //            }
    //        }

    //        pdfDoc.Add(pdfTable);
    //        pdfDoc.Close();
    //        Response.ContentType = "application/octet-stream";
    //        Response.AddHeader("Content-Disposition", "attachment; filename=" + Name + "_" + DateTime.Now.ToString() + ".pdf");
    //        Response.Clear();
    //        Response.BinaryWrite(mStream.ToArray());
    //        Response.End();

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
}