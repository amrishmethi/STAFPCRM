using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        ViewState["tbl"] = dv.ToTable();
        rep.DataSource = ViewState["tbl"];
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
    private void GeneratePDF(DataTable dataTable, string Name)
    {
        try
        {
            string[] columnNames = (from dc in dataTable.Columns.Cast<DataColumn>()
                                    select dc.ColumnName).ToArray();
            int Cell = 0;
            int count = columnNames.Length;
            object[] array = new object[count];

            dataTable.Rows.Add(array);

            Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, mStream);
            int cols = dataTable.Columns.Count;
            int rows = dataTable.Rows.Count;
            pdfDoc.Open();
            PdfPTable PdfTable = new PdfPTable(2);
            PdfTable.TotalWidth = 1000f;
            PdfTable.SpacingBefore = 20f;
            PdfTable.SpacingAfter = 20f;
            iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(Path.GetDirectoryName(Server.MapPath("."))+"../img/logo.jpg");
                myImage.ScaleAbsoluteHeight(70);
                myImage.ScaleAbsoluteWidth(140);
                myImage.Alignment = Element.ALIGN_LEFT;
                PdfPCell cell = new PdfPCell(myImage);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                PdfTable.AddCell(cell);
            
            PdfPCell header= new PdfPCell(new Paragraph("Shree Tadkeshwar Agro Food Product \n  H- 1-37- A, Sarna Doongar Industrial Area, Jhotwara Extension, \n Jaipur - 302012 Rajasthan, India \n (Mob) : + 91 - 98290 - 32422, (Tel) : 0141 - 3540250 \n (Email) : sales@tadkeshwarfoods.com, (url) : www.tadkeshwarfoods.com"));

          //  Remove the border that is set by default
            header.Border = iTextSharp.text.Rectangle.TITLE;
          //  Align the text: 0 is left, 1 center and 2 right.
           header.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            header.Border = Rectangle.NO_BORDER;
            
            PdfTable.AddCell(header);
            pdfDoc.Add(PdfTable);


            //  Header.
            //  pdfDoc.Open();
            iTextSharp.text.Table pdfTable = new iTextSharp.text.Table(cols, rows);
            pdfTable.BorderWidth = 1; pdfTable.Width = 100;
            pdfTable.Padding = 1; pdfTable.Spacing = 4;

           // creating table headers
            for (int i = 0; i < cols; i++)
            {
                Cell cellCols = new Cell();
                Chunk chunkCols = new Chunk();
                cellCols.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#548B54"));
                iTextSharp.text.Font ColFont = FontFactory.GetFont(FontFactory.HELVETICA, 14, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.WHITE);

                chunkCols = new Chunk(dataTable.Columns[i].ColumnName, ColFont);

                cellCols.Add(chunkCols);
                pdfTable.AddCell(cellCols);
            }
          //  creating table data(actual result)

            for (int k = 0; k < rows; k++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Cell cellRows = new Cell();
                    if (k % 2 == 0)
                    {
                        cellRows.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#cccccc")); ;
                    }
                    else { cellRows.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#ffffff")); }
                    //if (j == cols - 1)
                    //{
                    //    iTextSharp.text.Image dataImage = iTextSharp.text.Image.GetInstance("https://app.tadkeshwarfoods.com/AreaDevelop/"+ dataTable.Rows[k][j].ToString());
                    //    dataImage.ScaleAbsoluteHeight(60);
                    //    dataImage.ScaleAbsoluteWidth(60);
                    //    dataImage.Alignment = Element.ALIGN_LEFT;
                    //    PdfPCell cell1 = new PdfPCell(dataImage);
                    //    cell1.Border = Rectangle.NO_BORDER;
                    //    cell1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    //    cell1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    //    PdfTable.AddCell(cell);
                    //}
                    //else { 
                    iTextSharp.text.Font RowFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                    Chunk chunkRows = new Chunk(dataTable.Rows[k][j].ToString(), RowFont);
                    cellRows.Add(chunkRows);

                    pdfTable.AddCell(cellRows);
                  //  }
                }
            }

            pdfDoc.Add(pdfTable);
            pdfDoc.Close();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Name + "_" + DateTime.Now.ToString() + ".pdf");
            Response.Clear();
            Response.BinaryWrite(mStream.ToArray());
            Response.End();

        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkDownloadPDF_Click(object sender, EventArgs e)
    {
        DataTable datatable = new DataTable();
        datatable.Clear();

        datatable.Columns.Add("Sr. No.");
        datatable.Columns.Add("DateTime");
        datatable.Columns.Add("Employee");
        datatable.Columns.Add("Party");
        //datatable.Columns.Add("Head Quarter");
        datatable.Columns.Add("District");
        datatable.Columns.Add("Station");
       // datatable.Columns.Add("Mobile No");
        datatable.Columns.Add("WhatsApp No");
        datatable.Columns.Add("Description");
        datatable.Columns.Add("Location");
       // datatable.Columns.Add("Meet Type");
       // datatable.Columns.Add("Image");

        foreach (DataRow dr in ((DataTable)ViewState["tbl"]).Rows) { 
        DataRow _row = datatable.NewRow();
            _row["Sr. No."] = datatable.Rows.Count+1;
            _row["DateTime"] = dr["AddedDate"] +""+ dr["AddedTime"];
            _row["Employee"] = dr["Name"];
            _row["Party"] = dr["PartyName"];
           // _row["Head Quarter"] = dr["HeadQtr"];
            _row["District"] = dr["District"];
            _row["Station"] = dr["Station"];
         //   _row["Mobile No"] = dr["MobileNo"];
            _row["WhatsApp No"] = dr["WhatsAppNo"];
            _row["Description"] = dr["Description"];
            _row["Location"] = dr["Place"];
           // _row["Meet Type"] = dr["ClientMeetType"];
        //    _row["Image"] = dr["Image"];
            datatable.Rows.Add(_row);
        }
        GenerateReport(datatable, "ClientMeet");

    }

    protected void GenerateReport(DataTable dataTable, string Name)
    {
        DataRow dr = dataTable.Rows[0]; ;
        Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
        Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            Phrase phrase = null;
            PdfPCell cell = null;
            PdfPTable table = null;
            Color color = null;

            document.Open();

            //Header Table
            table = new PdfPTable(2);
            table.TotalWidth = 500f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 0.3f, 0.7f });

            //Company Logo
            cell = ImageCell("~/img/user-img.jpg", 30f, PdfPCell.ALIGN_CENTER);
            table.AddCell(cell);
           
            //Company Name and Address
            phrase = new Phrase();
            phrase.Add(new Chunk("Shree Tadkeshwar Agro Food Product \n\n", FontFactory.GetFont("Arial", 16, Font.BOLD, Color.RED)));
            phrase.Add(new Chunk("H- 1-37- A, Sarna Doongar Industrial Area, Jhotwara Extension,\n", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            phrase.Add(new Chunk("Jaipur - 302012 Rajasthan, India ,\n", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            phrase.Add(new Chunk("(Mob) : + 91 - 98290 - 32422, (Tel) : 0141 - 3540250 \n", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            phrase.Add(new Chunk("(Email) : sales@tadkeshwarfoods.com, (url) : www.tadkeshwarfoods.com", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            table.AddCell(cell);

            //Separater Line
            color = new Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
            DrawLine(writer, 25f, document.Top - 79f, document.PageSize.Width - 25f, document.Top - 79f, color);
            DrawLine(writer, 25f, document.Top - 80f, document.PageSize.Width - 25f, document.Top - 80f, color);
            document.Add(table);

            table = new PdfPTable(2);
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            table.SetWidths(new float[] { 0.3f, 1f });
            table.SpacingBefore = 20f;

            int cols = dataTable.Columns.Count;
            int rows = dataTable.Rows.Count;
            iTextSharp.text.Table pdfTable = new iTextSharp.text.Table(cols, rows);
            pdfTable.BorderWidth = 1; pdfTable.Width = 100;
            pdfTable.Padding = 1; pdfTable.Spacing = 4;
            for (int i = 0; i < cols; i++)
            {
                Cell cellCols = new Cell();
                Chunk chunkCols = new Chunk();
                cellCols.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#548B54"));
                iTextSharp.text.Font ColFont = FontFactory.GetFont(FontFactory.HELVETICA, 14, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.WHITE);

                chunkCols = new Chunk(dataTable.Columns[i].ColumnName, ColFont);

                cellCols.Add(chunkCols);
                pdfTable.AddCell(cellCols);
            }

            for (int k = 0; k < rows; k++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Cell cellRows = new Cell();
                    if (k % 2 == 0)
                    {
                        cellRows.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#cccccc")); ;
                    }
                    else { cellRows.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#ffffff")); }
                     
                    iTextSharp.text.Font RowFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                    Chunk chunkRows = new Chunk(dataTable.Rows[k][j].ToString(), RowFont);
                    cellRows.Add(chunkRows);

                    pdfTable.AddCell(cellRows); 
                }
            }

             

            DrawLine(writer, 160f, 80f, 160f, 690f, Color.BLACK);
            DrawLine(writer, 115f, document.Top - 200f, document.PageSize.Width - 100f, document.Top - 200f, Color.BLACK);

              
            document.Add(pdfTable);
            document.Close();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=Employee.pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }
    }


    private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
    {
        PdfContentByte contentByte = writer.DirectContent;
        contentByte.SetColorStroke(color);
        contentByte.MoveTo(x1, y1);
        contentByte.LineTo(x2, y2);
        contentByte.Stroke();
    }
    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }
    private static PdfPCell ImageCell(string path, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
        image.ScalePercent(scale);
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 0f;
        return cell;
    }
}