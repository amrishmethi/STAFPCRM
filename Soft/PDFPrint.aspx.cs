using System;


public partial class soft_PDFPrint : System.Web.UI.Page
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        hdd.InnerHtml = Session["InvPrint"].ToString();
    } 
}