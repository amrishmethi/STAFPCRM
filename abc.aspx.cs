using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using Spire.Pdf.Lists;

public partial class abc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //GenerateTktPDF("10002");
        SendSMSWithPDF("7742870061", "Test msg");
    }

    private string tktPDF(string tKTID)
    {
        string str = "";
        str = "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
        str += " <tr>";
        str += "    <td>&nbsp;</td>";
        str += " </tr>";
        str += " <tr>";
        str += "<td>";
        str += "<table width='990' border='0' align='center' cellpadding='0' cellspacing='0'>";
        str += " <tr>";
        str += "    <td>";
        str += "         <div style='display: block; position: relative; '>";
        str += "           <div style='width: 100 %; height: auto; '>";
        str += "               <img src='https://jitoconnect.indiahostbiz.com/img/jito-pass.jpg'/>";
        str += "         </div>";
        str += "        <div style='position: absolute; z - index: 99; top: 46 %; left: 7 %; '>";
        str += "          <img src='https://jitoconnect.indiahostbiz.com/img/bar-code.jpg'/>";
        str += "        </div>";

        str += "     < div style='position: absolute; z - index: 99; top: 34 %; right: 36 %; '>" + tKTID + "</div>";

        str += "      <div style='position: absolute; z - index: 99; bottom: 14 %; right: 8 %;'><span style='border: 1px solid #999; padding: 5px 10px;'>" + tKTID + "</span></div>";

        str += "  </div>";
        str += "  </td>";
        str += "</tr>";
        str += " </table>";
        str += " </td>";
        str += " </tr>";
        str += " <tr>";
        str += "   <td>&nbsp;</td>";
        str += "</tr>";
        str += "</table>";
        return str;
    }
    public string GenerateTktPDF(string TKTID)
    {

        string sb = tktPDF(TKTID);


        StringReader sr = new StringReader(sb.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        using (MemoryStream memoryStream = new MemoryStream())
        {
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            string folder = "~/PDF/";
            #region For Password Protected File
            using (MemoryStream inputData = new MemoryStream(bytes))
            {
                using (MemoryStream outputData = new MemoryStream())
                {

                }
            }
            #endregion
            File.WriteAllBytes(HttpContext.Current.Server.MapPath(folder) + TKTID + ".pdf", bytes);
        }

        return TKTID;
    }

    public void SendSMSWithPDF(string Mobile, string Msg)
    {
        try
        {
            string PdfLink = "https://test.aruwa.in/Pdf/";
            string PDFNAME = "10002.pdf";
            //string mess = "https://botmasterapi.in/api/send?number=91" + Mobile + "&type=media&message=" + Msg + "&media_url=" + PdfLink + "&filename=" + PDFNAME + "&instance_id=650004A3AAC6F&access_token=6500043bb462f";
            string mess = "https://botmasterapi.in/api/send?number=91" + Mobile + "&type=text&message=" + Msg + "&instance_id=650004A3AAC6F&access_token=6500043bb462f";
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(mess);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams and the response.
            reader.Close();
            response.Close();
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "TTT", true);
        }

        catch (Exception exx)
        {
            //ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, exx.ToString(), true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('" + exx.ToString() + "');", true);
        }

    }
}