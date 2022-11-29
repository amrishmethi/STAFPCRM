using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
public class MailJet
{
    public MailJet()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void sendmailtoclient(string Email, string Message, string Subject)
    {
        try
        {
            //MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"),); .Property(Send.TextPart, "Dear Test , welcome to Mailjet! May the delivery force be with you!")
            MailjetClient client = new MailjetClient("d0c45600b16454c8ff1d411d875ab0dd", "f5713cf5480dd2d8b895f6a745ee40c8");
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
               .Property(Send.FromEmail, "hr@tadkeshwarfoods.com")
               .Property(Send.FromName, "STAFP")
               .Property(Send.Subject, Subject)
               .Property(Send.HtmlPart, Message)
               .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", Email}
                 }
                   });
            client.PostAsync(request);
        }
        catch (Exception exxx)
        {
            
        }
    }
}