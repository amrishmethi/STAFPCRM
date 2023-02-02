using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        rep.DataSource = Session["ClientMeet"];
        rep.DataBind();
        print();
    }

    private void print()
    {
       
        Document pdfDoc = new Document();
        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
        pdfDoc.Open();
        string strHTML = htmlabc.InnerHtml.ToString();
        HTMLWorker htmlWorker = new HTMLWorker(pdfDoc);
        htmlWorker.Parse(new StringReader(strHTML));
        pdfWriter.CloseStream = false;
        pdfDoc.Close();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Test.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(pdfDoc);
        Response.Flush();
        Response.End();
    }
}