using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SecondarySalesReport : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Master getdata = new Master();
    GetData Gd = new GetData();
    Data data = new Data();
    private HttpCookie Soft;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];
            Session["AccessRigthsSet"] = getdata.AccessRights("SecondarySalesReport.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            Gd.FillUser(drpUser);
            Gd.FillPrimaryParty(drpParty);
            Gd.FillPrimaryStation(drpStation);
            Gd.fillDepartment(drpDept);
            fillData();
        }
    }

    public void fillData()
    {
        string str = "1=1";
        ds = getdata.getSeconarySalesDetails(drpUser.SelectedValue, drpParty.SelectedValue, drpStation.SelectedItem.Text, dpFrom.Text.Trim(), dpTo.Text.Trim(), drpDept.SelectedValue);
        DataView dv = ds.Tables[0].DefaultView;
        if (drpIsCheck.SelectedValue == "0") { str += " and AddedDate is null"; }
        else if (drpIsCheck.SelectedValue == "1") { str += " and AddedDate is not null"; }
        if (drpStatus.SelectedValue == "Active") { str += " and Status = 'Active'"; }
        else if (drpStatus.SelectedValue == "Non-Active") { str += " and Status = 'Non-Active'"; }
        dv.RowFilter = str;

        DataTable dtt = dv.ToTable();
        DataRow drr = dtt.NewRow();
        drr["Employees"] = "Total";
        drr["TargetAmount"] = dtt.Compute("sum(TargetAmount)", "");
        drr["TotalSale"] = dtt.Compute("sum(TotalSale)", "");
        drr["Achievement"] = dtt.Compute("sum(Achievement)", "");
        drr["TargetVisit"] = dtt.Compute("sum(TargetVisit)", "");
        drr["Productive"] = dtt.Compute("sum(Productive)", "");
        drr["NonProductive"] = dtt.Compute("sum(NonProductive)", "");
        drr["Visited"] = dtt.Compute("sum(Visited)", "");
        dtt.Rows.Add(drr);
        rep.DataSource = dtt;
        rep.DataBind();
    }




    [WebMethod]
    public static string ControlAccess()
    {
        DataTable tbl1 = (DataTable)HttpContext.Current.Session["AccessRigthsSet"];
        return tbl1.Rows[0]["AddStatus"].ToString() + "," + tbl1.Rows[0]["EditStatus"].ToString() + "," + tbl1.Rows[0]["DeleteStatus"].ToString() + "," + tbl1.Rows[0]["ViewP"].ToString();
    }

    protected void drpIsCheck_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillData();
    }

    protected void print_Click(object sender, EventArgs e)
    {
        CheckBox chk;
        HiddenField hddID;
        string OrderId = "";
        for (int i = 0; i < rep.Items.Count; i++)
        {
            chk = (CheckBox)rep.Items[i].FindControl("chk");
            hddID = (HiddenField)rep.Items[i].FindControl("hddID");
            if (chk.Checked == true)
            {
                if (OrderId == "")
                    OrderId = hddID.Value;
                else
                    OrderId += "," + hddID.Value;
            }
        }

        GenratePrint(OrderId);
    }

    public void GenratePrint(string SaleId)
    {
        if (SaleId != "")
        {
            //  CreateTable();
            string _OrderId = SaleId;
            StringBuilder sb = new StringBuilder();
            string[] SplitValue = _OrderId.Split(',');
            for (int _Count = 0; _Count < SplitValue.Length; _Count++)
            {
                sb.Append("<style>");
                sb.Append("\n @media print {");
                sb.Append("\n footer {page-break-after: always;}");
                sb.Append("\n }</style>");

                DataTable dstbl = getdata.getSecondarySalesItem(SplitValue[_Count]).Tables[0];
                DataView dv = dstbl.DefaultView;
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
                        sb.Append("<td>Date: " + dsGet.Rows[0]["CHECKDATE"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>Primary Party: " + dsGet.Rows[0]["PrimaryParty"].ToString() + "</td>");
                        sb.Append("<td>Station: " + dsGet.Rows[0]["PrimaryStation"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<td>Total Visits: " + dstbl.Rows.Count.ToString() + "</td>");
                        sb.Append("<td>Productive: " + dsGet.Rows.Count + "\t");
                        sb.Append("Non-Productive: " + (dstbl.Rows.Count - dsGet.Rows.Count) + "</td>");
                        sb.Append("</tr>");
                        int row_num = 1; int totQty = 0; Decimal totAmt = 0;

                        foreach (DataRow dr in dsGet.Rows)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td align='center' colspan='2'>" +
                                "<table width='100%' border='1' bordercolor='#CCC' cellspacing='0' cellpadding='5' style='border:1px solid #CCC; border-collapse:collapse;'>");//style='border:0px solid #CCC; border-collapsecollapse;'
                            sb.Append("<tr>");
                            sb.Append("<th rowspan='2'>" + (row_num++) + "</th>");
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
                            foreach (DataRow rw in ITbl.Rows)
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
                        sb.Append("<th width='10%'>" + totQty + "</th>");
                        sb.Append("<th width='15%'>&nbsp;</th>");
                        sb.Append("<th width='15%' style='text-align:right;'>" + totAmt + "</th>");
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