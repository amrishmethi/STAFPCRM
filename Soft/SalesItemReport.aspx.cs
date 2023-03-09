using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_SalesItem_Report : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    Data data = new Data();
    string query;
    SqlCommand cmd = new SqlCommand();
    Master getdata = new Master();
    GetData Gd = new GetData();
    private HttpCookie Soft;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("SalesItemReport.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            //dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            //dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            //Gd.FillUser(drpUser);
            //Gd.FillPrimaryParty(drpParty);
            //Gd.FillPrimaryStation(drpStation);
            if (Request.QueryString["id"] != null)
            {
                Session["CheckID"] = Request.QueryString["id"];
                Filldata();
            }
        }
    }


    private void Filldata()
    {
        ds = getdata.getSecondarySalesItem(Request.QueryString["id"].ToString());

        rep.DataSource = ds.Tables[0];
        rep.DataBind();
    }




    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hddid = (HiddenField)e.Item.FindControl("hddid");
            Repeater rep1 = (Repeater)e.Item.FindControl("rep1");
            Label lblTotal = (Label)e.Item.FindControl("lblTotal");
            DataSet dsrep1 = data.getDataSet("PROC_SECONDARYITEMSDETAILS '" + hddid.Value + "'");
            if (dsrep1.Tables[0].Rows.Count > 0)
            {
                lblTotal.Text = Convert.ToDecimal(dsrep1.Tables[0].Compute("Sum(Amount)", "")) + "";
                txtGrandTot.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtGrandTot.Text) + Convert.ToDecimal(lblTotal.Text));
                rep1.DataSource = dsrep1;
                rep1.DataBind();
            }
        }
    }



    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string sb = tblBlock.InnerHtml;

        StringReader sr = new StringReader(sb);
        Session["InvPrint"] = sb;
        Response.Write("<script>window.open('SecondarySalePrint.aspx','_blank');</script>");
    }

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ResizeImg")
        {
            Response.Redirect("ResizeImage.aspx?imgurl=" + e.CommandArgument.ToString());
        }
    }


    protected void rep1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int Id = Convert.ToInt32(e.CommandArgument.ToString());
            data.executeCommand("Update [tbl_CheckOutItem] Set ISdeleted=1 where Id=" + Id);
            Filldata();
        }
    }

    protected void print_Click(object sender, EventArgs e)
    {
        GeneratePDF(Request.QueryString["id"]);
    }

    public void GeneratePDF(string SaleId)
    {
        if (SaleId != "")
        {
            //  CreateTable();
           // string _OrderId = SaleId;
            StringBuilder sb = new StringBuilder();
            
            sb.Append("<style>");
            sb.Append("\n @media print {");
            sb.Append("\n footer {page-break-after: always;}");
            sb.Append("\n }</style>");
           
            DataTable dsTbl = getdata.getSecondarySalesItem(Request.QueryString["id"].ToString()).Tables[0];
            DataView dv = dsTbl.DefaultView;
            dv.RowFilter = "IsProd<>0";
            DataTable dsGet = dv.ToTable();
            //  string ss = "Sp_OrderPrint " + SplitValue[_Count];



            //    DataRow drHqtr = master.getHqtrUser().Tables[0].Select("MId = " + dt.Rows[0]["CRMUserId"]).FirstOrDefault();

            using (StringWriter sw = new StringWriter())
                    {
                        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                        {
                            sb.Append("<table style='width: 100%; padding-right: 15px; border-spacing: 0px;'>");
                            sb.Append("<tr style='padding: 0px; margin-top: -10px; margin-bottom: -10px;'>");
                            sb.Append("<td style='text-align: left;padding: 0px;'>");
                            sb.Append("<img src='../../img/logo.jpg' height='80px'><br />");
                            sb.Append("</td>");
                            sb.Append("<td style='text-align: right;padding: 0px;'>");
                            sb.Append("<p style='font-size: 12px;'>");
                            sb.Append("<strong style='font-size: 18px;'>Shree Tadkeshwar Agro Food Product</strong>");
                            sb.Append("<br />");
                            sb.Append("H- 1-37- A, Sarna Doongar Industrial Area, Jhotwara Extension,");
                            sb.Append("<br />");
                            sb.Append("Jaipur - 302012 Rajasthan, India");
                            sb.Append("<br />");
                            sb.Append("(Mob) : + 91 - 98290 - 32422, (Tel) : 0141 - 3540250");
                            sb.Append("<br />");
                            sb.Append("(Email) : sales@tadkeshwarfoods.com, (url) : www.tadkeshwarfoods.com");
                            sb.Append("</p>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr style='padding: 0px; margin-top: -10px; margin-bottom: -10px;'>");
                            sb.Append("<td colspan='2' style='padding: 0px;' >");
                            sb.Append("<asp:Label ID='lblHeading' runat='server' Style='font-size: 20px; color: firebrick;'></asp:Label>");
                            sb.Append("<asp:Label ID='lblDateRng' runat='server'></asp:Label></td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                    sb.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                    sb.Append("<tr>");
                    sb.Append("<td>&nbsp;</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td align='center'>");
                    sb.Append("<table width='990' border='0' cellspacing='0' cellpadding='0'>");
                    sb.Append("<tr>");
                    sb.Append("<td width='990' align='center'><strong>Secondary Sales Report</strong></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td align='center'>&nbsp;</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td align='center'>" +
                        "<table width='98%' border='0px' bordercolor='#CCC' cellspacing='0' cellpadding='5'>");//style='border:0px solid #CCC; border-collapsecollapse;'
                    sb.Append("<tr>");
                    sb.Append("<td>Employee Name: " + dsGet.Rows[0]["Employees"].ToString() + "</td>");
                    sb.Append("<td>Date: " + dsGet.Rows[0]["CHECKDATE"].ToString() +"</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>Primary Party: " + dsGet.Rows[0]["PrimaryParty"].ToString() + "</td>");
                    sb.Append("<td>Station: " + dsGet.Rows[0]["PrimaryStation"].ToString() + "</td>");
                    sb.Append("</tr>"); 
                    sb.Append("<td>Total Visits: " + dsTbl.Rows.Count.ToString() + "</td>");
                    
                    sb.Append("<td>Productive: " + dsGet.Rows.Count + "&nbsp;&nbsp;&nbsp;");
                    sb.Append("Non-Productive: " + (dsTbl.Rows.Count - dsGet.Rows.Count) + "</td>");
                    sb.Append("</tr>");
                    int row_num = 1; int totQty=0; Decimal totAmt = 0; 
                    
                    foreach (DataRow dr in dsGet.Rows)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td align='center' colspan='2'>" +
                            "<table width='100%' border='1' bordercolor='#CCC' cellspacing='0' cellpadding='5' style='border:1px solid #CCC; border-collapse:collapse;'>");//style='border:0px solid #CCC; border-collapsecollapse;'
                        sb.Append("<tr>");
                        sb.Append("<th rowspan='2'>"+(row_num++)+"</th>");
                        sb.Append("<th colspan='3'>Secondary Party: " + dr["SECONDARYPARTY"].ToString() + "</th>");
                        sb.Append("<th colspan='2'>Secondary Station: " + dr["SECONDARYSTATION"].ToString() + "</th>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<th colspan='3'>Date & Time: " + dr["CHECKDATE"].ToString() + " " + dsGet.Rows[0]["CHECKTIME"].ToString() + "</th>");
                        sb.Append("<th colspan='2'>Mobile No: " + dr["MobileNo"].ToString() + "</th>");
                        sb.Append("</tr>");
                        sb.Append("<tr style='background-color:whitesmoke;'>");
                        sb.Append("<th width='10%'>&nbsp;</th>");
                        sb.Append("<th colspan='2' width='50%'>Item</th>");
                        sb.Append("<th width='10%'>Qty</th>");
                        sb.Append("<th width='15%'>Rate</th>");
                        sb.Append("<th width='15%'>Amount</th>");
                        sb.Append("</tr>");
                        DataTable ITbl = data.getDataSet("PROC_SECONDARYITEMSDETAILS '" + dr["ChkOutID"].ToString() + "'").Tables[0];
                        int ino = 1; int sumQty = 0; decimal sumAmt = 0;
                        foreach(DataRow rw in ITbl.Rows)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style='text-align:center;'>" + (ino++) + "</td>");
                            sb.Append("<td style='text-align:left;' colspan='2'>" + rw["ITName"] + "</td>");
                            sb.Append("<td style='text-align:center;'>" + Convert.ToInt32(rw["OrdQty"]).ToString("#0") + "</td>");
                            sb.Append("<td style='text-align:right;'>" + rw["OrdStpRate"] + "</td>");
                            sb.Append("<td style='text-align:right;'>" + rw["Amount"] + "</td>");
                            sb.Append("</tr>");
                            sumAmt += Convert.ToDecimal(rw["Amount"]);
                            sumQty += Convert.ToInt32(rw["OrdQty"]);
                        }
                        totAmt += sumAmt; totQty += sumQty;
                        sb.Append("<tr style='background-color:floralwhite;'>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("<td style='text-align:left;' colspan='2'>Remark: " + dr["Remark"] + "</td>");
                        sb.Append("<td style='text-align:center;'>" + sumQty + "</td>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("<td style='text-align:right;'>" + sumAmt + "</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td align='center'>");
                    sb.Append("<table width='98%' border='0' bordercolor='#CCC' cellspacing='0' cellpadding='5' style='border-top:2px  solid #CCC;border-bottom:2px  solid #CCC; border-collapse:collapse;'>");
                    sb.Append("<tr style='background-color:ghostwhite;'>");
                    sb.Append("<th width='10%'>&nbsp;</th>");
                    sb.Append("<th colspan='2' width='50%'>Total</th>");
                    sb.Append("<th width='10%'>"+ totQty + "</th>");
                    sb.Append("<th width='15%'>&nbsp;</th>");
                    sb.Append("<th width='15%' style='text-align:right;'>" + totAmt+"</th>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                   
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>&nbsp;</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<footer></footer>");
                        }
                    }
                
                //if (isPdf)
                //{
                //    string  pdfname = ExporttoPDF(sb);
                //    Response.Write("<script>window.open('../PDF/"+ pdfname + ".pdf','_blank');</script>");
                //}
                //else
                //{
                //Export HTML String as PDF.
                StringReader sr = new StringReader(sb.ToString());
               Session["InvPrint"] = sb.ToString();
                Response.Write("<script>window.open('UserRptPrint.aspx','_blank');</script>");
                //}
            
        }

    }
}