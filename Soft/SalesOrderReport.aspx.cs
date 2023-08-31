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
using System.Data.SqlClient;
using System.Net;

public partial class Soft_SalesOrder_Report : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    Data data = new Data();
    string query;
    SqlCommand cmd = new SqlCommand();
    Master getdata = new Master();
    GetData Gd = new GetData();
    private HttpCookie Soft;
    double _TotalAMt = 0;
    double _TotalBags = 0;
    double _TotalWght = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

            Soft = Request.Cookies["STFP"];

            Session["AccessRigthsSet"] = getdata.AccessRights("SalesOrderReport.aspx", Soft["Type"] == "admin" ? "0" : Soft["UserId"]).Tables[0];
            dpFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            dpTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            Gd.fillDepartment(drpDepartment);
            Gd.FillUser(drpUser, drpDepartment.SelectedValue);
            bindDrp(true, true);
            Gd.FillPrimaryParty(drpParty);
            Gd.FillGroup(drpGrp);
            foreach (ListItem size in drpGrp.Items)
            {//
                //if (size.Value.ToString() == "DISHWAS" || size.Value.ToString() == "POWDER" || size.Value.ToString() == "ARTICLE" || size.Value.ToString() == "KLEAN B")
                if (size.Value.ToString() == "ALL IN")
                {
                    size.Selected = true;
                }
            }

            Filldata();
        }
    }

    private void bindDrp(bool isuser, bool ishqtr)
    {
        DataSet dsusr = getdata.getHqtrUserDpt(drpDepartment.SelectedValue);
        DataView dv = dsusr.Tables[0].DefaultView;
        if (isuser)
        {
            if (drpHeadQtr.SelectedIndex > 0)
                dv.RowFilter = "HeadQtrNO='" + drpHeadQtr.SelectedValue + "'";
            dv.Sort = "Name";
            drpUser.DataSource = dv.ToTable(true, "Name", "MId");
            drpUser.DataTextField = "Name";
            drpUser.DataValueField = "MId";
            drpUser.DataBind();
            drpUser.Items.Insert(0, new ListItem("Select", "0"));
        }
        if (ishqtr)
        {
            if (drpUser.SelectedIndex > 0)
                dv.RowFilter = "MId='" + drpUser.SelectedValue + "'";
            dv.Sort = "HeadQtr";
            drpHeadQtr.DataSource = dv.ToTable(true, "HeadQtr", "HeadQtrNo");
            drpHeadQtr.DataTextField = "HeadQtr";
            drpHeadQtr.DataValueField = "HeadQtrNo";
            drpHeadQtr.DataBind();
            drpHeadQtr.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    private void Filldata()
    {

        //string str = "1=1";
        ds = getdata.getSalesOrder("SELECT", "", drpUser.SelectedValue, drpHeadQtr.SelectedValue, drpParty.SelectedValue, dpFrom.Text.Trim(), dpTo.Text.Trim(), "", "", drpGrp.SelectedValue);
        DataView dv = ds.Tables[0].DefaultView;
        // if (drpStatus.SelectedValue == "Active") { str += " and Status = 'Active'"; }
        // else if (drpStatus.SelectedValue == "Non-Active") { str += " and Status = 'Non-Active'"; }
        // dv.RowFilter = str;
        rep.DataSource = ds.Tables[0];
        rep.DataBind();

        txtGrandTot.Text = String.Format("{0:0.00}", _TotalAMt);
        txtTotalBags.Text = String.Format("{0:0.00}", _TotalBags);
        txtTotalWeight.Text = String.Format("{0:0.00}", _TotalWght);
    }




    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hddid = (HiddenField)e.Item.FindControl("hddid");
            Repeater rep1 = (Repeater)e.Item.FindControl("rep1");
            Label lblTotal = (Label)e.Item.FindControl("lblTotal");
            Label lblQty = (Label)e.Item.FindControl("lblQty");
            //  Label lblPacking = (Label)e.Item.FindControl("lblPacking");
            Label lblWeight = (Label)e.Item.FindControl("lblWeight");

            string grp = "0";
            foreach (ListItem item in drpGrp.Items)
            {
                if (item.Selected)
                {
                    grp += "," + item.Value;
                }
            }

            DataSet dsrep1 = getdata.getSalesOrder("SELECT", hddid.Value, "", "", "", "", "", "", "", grp);
            if (dsrep1.Tables[1].Rows.Count > 0)
            {
                _TotalAMt += (Convert.ToDouble(dsrep1.Tables[1].Compute("Sum(Amount)", "")));
                _TotalBags += (Convert.ToDouble(dsrep1.Tables[1].Compute("Sum(OrdQty)", "")));
                _TotalWght += (Convert.ToDouble(dsrep1.Tables[1].Compute("Sum(Weight)", "")));

                lblTotal.Text = (Convert.ToDecimal(dsrep1.Tables[1].Compute("Sum(Amount)", ""))).ToString("#0.00");
                lblQty.Text = (Convert.ToDecimal(dsrep1.Tables[1].Compute("Sum(OrdQty)", ""))).ToString("#0");
                //    lblPacking.Text = (Convert.ToDecimal(dsrep1.Tables[1].Compute("Sum(Packing)", ""))).ToString("#0.00");
                lblWeight.Text = (Convert.ToDecimal(dsrep1.Tables[1].Compute("sum(Weight)", ""))).ToString("#0.00");

                rep1.DataSource = dsrep1.Tables[1];
                rep1.DataBind();
            }
        }
          
    }



    //protected void btnPDF_Click(object sender, EventArgs e)
    //{
    //    string sb = tblBlock.InnerHtml;
    //    StringReader sr = new StringReader(sb);
    //    Session["InvPrint"] = sb;
    //    Response.Write("<script>window.open('SalesOrder.aspx','_blank');</script>");
    //}

    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ResizeImg")
        {
            Response.Redirect("ResizeImage.aspx?imgurl=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "Delete")
        {
            ds = getdata.getSalesOrder("DELETE", e.CommandArgument.ToString(), "0", "0", "0", "", "", "", "", "0");
            Filldata();
        }
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsusr = getdata.getHqtrUserDpt(drpDepartment.SelectedValue);
        DataView dv = dsusr.Tables[0].DefaultView;
        if (drpStatus.SelectedIndex > 0)
            dv.RowFilter = " Status='" + drpStatus.SelectedValue + "'";
        dv.Sort = "Name";
        drpUser.DataSource = dv.ToTable(true, "Name", "MID");
        drpUser.DataTextField = "Name";
        drpUser.DataValueField = "MID";
        drpUser.DataBind();
        drpUser.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Filldata();
        DropDownList ddl = sender as DropDownList;
        if (ddl == drpUser)
        {
            bindDrp(false, true);
        }
        if (ddl == drpHeadQtr)
        {
            bindDrp(true, false);
        }
        if (ddl == drpDepartment)
        {
            bindDrp(true, false);
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
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
        string grp = "0";
        foreach (ListItem item in drpGrp.Items)
        {
            if (item.Selected)
            {
                grp += "," + item.Value;
            }
        }

        if (SaleId != "")
        {
            //  CreateTable();
            string _OrderId = SaleId;
            StringBuilder sb = new StringBuilder();
            string[] SplitValue = _OrderId.Split(',');
            string emp = "";
            int row_num = 1; int totQty = 0; Decimal totAmt = 0; decimal totwat = 0;
            for (int _Count = 0; _Count < SplitValue.Length; _Count++)
            {

                sb.Append("<style>");
                sb.Append("\n @media print {");
                sb.Append("\n footer {page-break-after: always;}");
                sb.Append("\n }</style>");

                DataSet dset = getdata.getSalesOrder("SELECT", SplitValue[_Count], drpUser.SelectedValue, drpHeadQtr.SelectedValue, drpParty.SelectedValue, dpFrom.Text.Trim(), dpTo.Text.Trim(), "", "", grp);


                DataTable dsGet = dset.Tables[0];
                //  string ss = "Sp_OrderPrint " + SplitValue[_Count];
                //    DataRow drHqtr = master.getHqtrUser().Tables[0].Select("MId = " + dt.Rows[0]["CRMUserId"]).FirstOrDefault();

                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        if (emp != dsGet.Rows[0]["Employee"].ToString())
                        {


                            if (emp != "")
                            {
                                sb.Append("<table width='990' border='0' bordercolor='#CCC' cellspacing='0' cellpadding='5' style='border-top:2px  solid #CCC;border-bottom:2px  solid #CCC; border-collapse:collapse;'>");
                                sb.Append("<tr style='background-color:ghostwhite;'>");
                                sb.Append("<th width='10%'>&nbsp;</th>");
                                sb.Append("<th colspan='3' width='50%'>Total</th>");
                                sb.Append("<th width='10%'>" + totQty + "</th>");
                                sb.Append("<th width='15%' style='text-align:right;'>" + totwat + "</th>");
                                sb.Append("<th width='15%' style='text-align:right;'>" + totAmt + "</th>");
                                sb.Append("</tr>");
                                sb.Append("</table>");
                                sb.Append("<footer></footer>");
                                row_num = 1; totQty = 0; totAmt = 0; totwat = 0;
                            }

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
                            sb.Append("<td width='990' align='center'><strong> Sales Item Report</strong></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td align='center'>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td align='center'>" +
                                "<table width='98%' border='0px' bordercolor='#CCC' cellspacing='0' cellpadding='5'>");
                            //style='border:0px solid #CCC; border-collapsecollapse;'
                            sb.Append("<tr>");
                            sb.Append("<td>Employee Name: " + dsGet.Rows[0]["Employee"].ToString() + "</td>");
                            sb.Append("<td>Date: " + dsGet.Rows[0]["ODATE"].ToString() + " " + dsGet.Rows[0]["OTIME"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                            emp = dsGet.Rows[0]["Employee"].ToString();
                        }

                        sb.Append("<table width='990' border='0' cellspacing='0' cellpadding='0'>");
                        foreach (DataRow dr in dsGet.Rows)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td align='center' colspan='2'>" +
                                "<table width='100%' border='1' bordercolor='#CCC' cellspacing='0' cellpadding='5' style='border:1px solid #CCC; border-collapse:collapse;'>");//style='border:0px solid #CCC; border-collapsecollapse;'
                            sb.Append("<tr>");
                            sb.Append("<th rowspan='2'>" + (row_num++) + "</th>");
                            sb.Append("<th colspan='2'>Primary Party: " + dsGet.Rows[0]["Party"].ToString() + "</td>");
                            sb.Append("<th colspan='2'>Station: " + dsGet.Rows[0]["Station"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<th colspan='2'>Date & Time:  " + dsGet.Rows[0]["ODATE"].ToString() + " " + dsGet.Rows[0]["OTIME"].ToString() + " </th>");
                            sb.Append("<th colspan='2'>DELIVERY MODE: " + dsGet.Rows[0]["DeliveryMode"].ToString() + " Payment MODE: " + dsGet.Rows[0]["PaymentMode"].ToString() + "</th>");


                            sb.Append("</tr>");
                            sb.Append("<tr style='background-color:whitesmoke;'>");
                            sb.Append("<th width='10%'>&nbsp;</th>");
                            sb.Append("<th  width='50%'>Item</th>");
                            sb.Append("<th width='10%'>	Qty.</th>");
                            // sb.Append("<th width='10%'>Packing</th>");
                            // sb.Append("<th width='10%'>Weight</th>");
                            sb.Append("<th width='15%'>RATE </th>");
                            sb.Append("<th width='15%'>Amount</th>");
                            sb.Append("</tr>");
                            DataTable ITbl = dset.Tables[1];
                            int ino = 1; int sumQty = 0; decimal sumAmt = 0; decimal sumwat = 0;
                            foreach (DataRow rw in ITbl.Rows)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td style='text-align:center;'>" + (ino++) + "</td>");
                                sb.Append("<td style='text-align:left;colspan='2'>" + rw["ITName"] + "</td>");
                                sb.Append("<td style='text-align:center;'>" + Convert.ToInt32(rw["OrdQty"]).ToString("#0") + "</td>");
                                //  sb.Append("<td style='text-align:center;'>" + rw["Packing"] + "</td>");
                                //  sb.Append("<td style='text-align:center;'>" + rw["Weight"] + "</td>");
                                sb.Append("<td style='text-align:right;'>" + rw["OrdStpRate"] + "</td>");
                                sb.Append("<td style='text-align:right;'>" + rw["Amount"] + "</td>");
                                sb.Append("</tr>");
                                sumAmt += Convert.ToDecimal(rw["Amount"]);
                                sumQty += Convert.ToInt32(rw["OrdQty"]);
                                sumwat += Convert.ToInt32(rw["Weight"]);
                            }
                            totAmt += sumAmt; totQty += sumQty;
                            //totwat += sumwat;
                            sb.Append("<tr style='background-color:floralwhite;'>");

                            sb.Append("<td style='text-align:left;' colspan='2'>Remark: " + dr["Remark"] + "</td>");
                            sb.Append("<td style='text-align:center;'>" + sumQty + "</td>");
                            sb.Append("<td>&nbsp;</td>");

                            // sb.Append("<td style='text-align:center;'>" + sumwat + "</td>");

                            sb.Append("<td style='text-align:right;'>" + sumAmt + "</td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                        sb.Append("</table></td>");
                        sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td align='center'>");
                        //sb.Append("</td>");
                        //sb.Append("</tr>");

                        sb.Append("</table></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        if (_Count == SplitValue.Length - 1)
                        {
                            sb.Append("<table width='990' border='0' bordercolor='#CCC' cellspacing='0' cellpadding='5' style='border-top:2px  solid #CCC;border-bottom:2px  solid #CCC; border-collapse:collapse;'>");
                            sb.Append("<tr style='background-color:ghostwhite;'>");
                            sb.Append("<th width='10%'>&nbsp;</th>");
                            sb.Append("<th colspan='3' width='50%'>Total</th>");
                            sb.Append("<th width='10%'>" + totQty + "</th>");

                            sb.Append("<th width='15%' style='text-align:right;'>" + totwat + "</th>");

                            sb.Append("<th width='15%' style='text-align:right;'>" + totAmt + "</th>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                        }
                        //if (emp != dsGet.Rows[0]["Employee"].ToString())
                        // {

                        // }
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


