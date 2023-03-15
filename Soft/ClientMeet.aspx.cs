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
        string str = "1=1";
        ds = getdata.getClientMeet(drpEmp.SelectedValue, drpHqtr.SelectedValue, drpType.SelectedValue, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), drpDept.SelectedValue);
        DataView dv = ds.Tables[0].DefaultView;
        if (drpIsMeet.SelectedValue == "0") { str += " and AddedDate is null"; }
        else if (drpIsMeet.SelectedValue == "1") { str += " and AddedDate <> ''"; }
        if (drpStatus.SelectedValue == "Active") { str += " and Status = 'Active'"; }
        else if (drpStatus.SelectedValue == "Non-Active") { str += " and Status = 'Non-Active'"; }
        dv.RowFilter = str;
        ViewState["tbl"] = dv.ToTable();
        Session["ClientMeet"] = dv.ToString();
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
        //fillData();

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
            PdfTable.LockedWidth = true;
            PdfTable.SpacingBefore = 20f;
            PdfTable.SpacingAfter = 20f;
            iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(Path.GetDirectoryName(Server.MapPath(".")) + "../img/logo.jpg");
            myImage.ScaleAbsoluteHeight(70);
            myImage.ScaleAbsoluteWidth(140);
            myImage.Alignment = Element.ALIGN_LEFT;
            PdfPCell cell = new PdfPCell(myImage);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            PdfTable.AddCell(cell);

            PdfPCell header = new PdfPCell(new Paragraph("Shree Tadkeshwar Agro Food Product \n  H- 1-37- A, Sarna Doongar Industrial Area, Jhotwara Extension, \n Jaipur - 302012 Rajasthan, India \n (Mob) : + 91 - 98290 - 32422, (Tel) : 0141 - 3540250 \n (Email) : sales@tadkeshwarfoods.com, (url) : www.tadkeshwarfoods.com"));

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
                if (i == 0)
                    cellCols.SetWidth("2%");
                pdfTable.AddCell(cellCols);
            }
            //  creating table data(actual result)

            pdfTable.SetWidths(new int[] { 10, 10, 20, 20, 20, 20, 20, 20, 20 });

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
                    if (k == 0)
                        cellRows.SetWidth("2%");
                    pdfTable.AddCell(cellRows);
                    //  }
                }
            }

            pdfDoc.Add(pdfTable);

            //PdfPCell Footer = new PdfPCell(new Paragraph("Shree Tadkeshwar Agro Food Product \n  H- 1-37- A, Sarna Doongar Industrial Area, Jhotwara Extension, \n Jaipur - 302012 Rajasthan, India \n (Mob) : + 91 - 98290 - 32422, (Tel) : 0141 - 3540250 \n (Email) : sales@tadkeshwarfoods.com, (url) : www.tadkeshwarfoods.com"));

            ////  Remove the border that is set by default
            //Footer.Border = iTextSharp.text.Rectangle.TITLE;
            ////  Align the text: 0 is left, 1 center and 2 right.
            //Footer.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            //Footer.Border = Rectangle.NO_BORDER;

            //PdfTable.AddCell(Footer);
            //pdfDoc.Add(PdfTable);
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

        foreach (DataRow dr in ((DataTable)ViewState["tbl"]).Rows)
        {
            DataRow _row = datatable.NewRow();
            _row["Sr. No."] = datatable.Rows.Count + 1;
            _row["DateTime"] = dr["AddedDate"] + " " + dr["AddedTime"];
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
        //GeneratePDF(datatable, "ClientMeet");
        Session["GridData"] = datatable;
        Session["Title"] = "Client Meet";
        Session["DateRange"] = "(Date as on "+ txtDateFrom.Text + " - "+ txtDateTo.Text + ")";
        //Response.Redirect("Print.aspx");
        Response.Write("<script>window.open ('Print.aspx','_blank');</script>");
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillData();
    }

    protected void btnToggle_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Panel panel = (Panel)btn.NamingContainer.FindControl("Panel1");
        panel.Visible = !panel.Visible;
    }


    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (drpImg.SelectedValue == "0")
        {
            isShowImg.Visible = false;
            HtmlTableCell tdValue2 = (HtmlTableCell)e.Item.FindControl("isShowImgData");
            tdValue2.Visible = false;
        }
        else {
            isShowImg.Visible = true;
            HtmlTableCell tdValue2 = (HtmlTableCell)e.Item.FindControl("isShowImgData");
            tdValue2.Visible = true;
        }
    }
}