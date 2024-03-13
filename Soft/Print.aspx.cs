using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Soft_Print : System.Web.UI.Page
{
    Master master = new Master();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblHeading.Text = Session["Title"].ToString();
        lblDateRng.Text = Session["DateRange"].ToString();
        CreateTableHTML((DataTable)Session["GridData"]);

        DataSet dss = master.TermsCondition("Select", Session["TermsId"].ToString(), "", "");
        if (dss.Tables[0].Rows.Count == 1)
        {
            lblTermsHeading.Text = "Terms & Condition";
            lblTerms.Text = dss.Tables[0].Rows[0]["Description"].ToString();
        }
    }

    private void CreateTableHTML(DataTable tbl)
    {
        string strbreak = ""; string brkStr = "";
        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<table  border='1' class='table display table-striped' style='border-collapse: collapse; font-size:16px;width: 100%;'>");
        strHTML.Append("<thead>");

        strHTML.Append("<tr>");
        for (int col = 0; col < tbl.Columns.Count; col++)
        {
            strHTML.Append("<th style='text-align: left;'>" + tbl.Columns[col].ColumnName + "</th>");
        }

        strHTML.Append("</tr>");
        strHTML.Append("</thead>");
        strHTML.Append("<tbody>");
        brkStr = tbl.Rows[0][2].ToString();
        decimal totSecSales = 0, totTrgtAmt = 0, totAchivePer = 0;
        int totVisits = 0, totTrgtVisit = 0, totProdVisit = 0, totNonProdVisit = 0;
        string _PartyName = "";
        for (int r = 0; r < tbl.Rows.Count; r++)
        {
            if (Session["Title"].ToString() == "Secondary Sales")
            {
                totSecSales += Convert.ToDecimal(tbl.Rows[r][4].ToString() == "" ? "0" : tbl.Rows[r][4].ToString());
                totTrgtVisit += Convert.ToInt32(tbl.Rows[r][5].ToString() == "" ? "0" : tbl.Rows[r][5].ToString());
                totTrgtAmt += Convert.ToDecimal(tbl.Rows[r][3].ToString() == "" ? "0" : tbl.Rows[r][3].ToString());
                totVisits += Convert.ToInt32(tbl.Rows[r][6].ToString() == "" ? "0" : tbl.Rows[r][6].ToString());
                totProdVisit += Convert.ToInt32(tbl.Rows[r][6].ToString() == "" ? "0" : tbl.Rows[r][7].ToString());
                totNonProdVisit += Convert.ToInt32(tbl.Rows[r][6].ToString() == "" ? "0" : tbl.Rows[r][8].ToString());
                totAchivePer += Convert.ToDecimal(tbl.Rows[r][6].ToString() == "" ? "0" : tbl.Rows[r][9].ToString().Replace('%', ' ').Trim());
                if (r < tbl.Rows.Count - 1)
                {
                    if (brkStr != tbl.Rows[r + 1][2].ToString())
                    {
                        if (Request.QueryString["EmpWise"] != null) { strbreak = "page"; }
                        else { strbreak = "nopage"; }
                        brkStr = tbl.Rows[r + 1][2].ToString();

                    }
                    else
                    {
                        strbreak = "";
                    }
                }
            }
            if (Session["Title"].ToString().Contains("HQ WISE OUTSTANDING"))
            {
                if (_PartyName != tbl.Rows[r]["Party"].ToString())
                {
                    if (r != 0)
                    {
                        DataTable dataTable = (DataTable)Session["HQ WISE OUTSTANDING"];
                        DataRow drr = dataTable.AsEnumerable().Where(x => x["acname"].ToString().Contains(_PartyName)).FirstOrDefault();

                        strHTML.Append("<tr>");
                        strHTML.Append("<td></td>");
                        strHTML.Append("<td></td>");
                        strHTML.Append("<td style='text-align:center'><b>" + _PartyName + "" + Session["DateRange"] + " (" + drr["ACCREDITDY"] + ") </b></td>");
                        strHTML.Append("<td></td>");
                        strHTML.Append("<td>Total</td>");
                        double _BillAMt = Convert.ToDouble(tbl.Compute("sum([Bill Amount])", "Party='" + _PartyName + "'"));
                        string _BillAMt1 = (_BillAMt > 0) ? _BillAMt + " CR" : _BillAMt * -1 + " DR";
                        double _DueAMt = Convert.ToDouble(tbl.Compute("sum([Due Amount])", "Party='" + _PartyName + "'"));
                        string _DueAMt1 = (_DueAMt > 0) ? _DueAMt + " CR" : _DueAMt * -1 + " DR";

                        strHTML.Append("<td style='text-align:center'>" + _BillAMt1 + "</td > ");
                        strHTML.Append("<td style='text-align:center'>" + _DueAMt1 + "</td>");
                        strHTML.Append("<td></td>");
                        strHTML.Append("<td></td>");
                        strHTML.Append("</tr>");
                    }
                    _PartyName = tbl.Rows[r]["Party"].ToString();
                }
            }
            strHTML.Append("<tr>");

            for (int c = 0; c < tbl.Columns.Count; c++)
            {
                if (tbl.Rows[r][c].ToString().Contains("https"))
                    strHTML.Append("<td style='text-align: center;'><img src='" + tbl.Rows[r][c] + "' height='50px' width='50px' /></td>");
                else if (Session["Title"].ToString().Contains("HQ WISE OUTSTANDING"))
                    if (tbl.Columns[c].ColumnName == "Bill Amount" || tbl.Columns[c].ColumnName == "Due Amount")
                    {
                        string _str = Convert.ToDouble(tbl.Rows[r][c]) > 0 ? Convert.ToDouble(tbl.Rows[r][c]).ToString() + " CR" : (Convert.ToDouble(tbl.Rows[r][c]) * -1).ToString() + " DR";
                        strHTML.Append("<td style='text-align: center;'>" + _str + "</td>");
                    }
                    else
                        strHTML.Append("<td style='text-align: center;'>" + tbl.Rows[r][c] + "</td>");

                else
                    strHTML.Append("<td style='text-align: center;'>" + tbl.Rows[r][c] + "</td>");
            }
            strHTML.Append("</tr>");
            if (Session["Title"].ToString().Contains("HQ WISE OUTSTANDING"))
            {
                if (r + 1 == tbl.Rows.Count)
                {
                    DataTable dataTable = (DataTable)Session["HQ WISE OUTSTANDING"];
                    DataRow drr = dataTable.AsEnumerable().Where(x => x["acname"].ToString().Contains(_PartyName)).FirstOrDefault();

                    strHTML.Append("<tr>");
                    strHTML.Append("<td></td>");
                    strHTML.Append("<td></td>");
                    strHTML.Append("<td style='text-align:center'><b>" + _PartyName + "" + Session["DateRange"] + " (" + drr["ACCREDITDY"] + ") </b></td>");
                    strHTML.Append("<td></td>");
                    strHTML.Append("<td>Total</td>");
                    double _BillAMt = Convert.ToDouble(tbl.Compute("sum([Bill Amount])", "Party='" + _PartyName + "'"));
                    string _BillAMt1 = (_BillAMt > 0) ? _BillAMt + " CR" : _BillAMt * -1 + " DR";
                    double _DueAMt = Convert.ToDouble(tbl.Compute("sum([Due Amount])", "Party='" + _PartyName + "'"));
                    string _DueAMt1 = (_DueAMt > 0) ? _DueAMt + " CR" : _DueAMt * -1 + " DR";

                    strHTML.Append("<td style='text-align:center'>" + _BillAMt1 + "</td > ");
                    strHTML.Append("<td style='text-align:center'>" + _DueAMt1 + "</td>");
                    strHTML.Append("<td></td>");
                    strHTML.Append("<td></td>");
                    strHTML.Append("</tr>");

                    strHTML.Append("<tr>");
                    strHTML.Append("<td style='text-align:right' colspan='4'></td>");
                    strHTML.Append("<td><b>Grand Total</b> </td>");
                    double _TBillAMt = Convert.ToDouble(tbl.Compute("sum([Bill Amount])", ""));
                    string _TBillAMt1 = (_TBillAMt > 0) ? _TBillAMt + " CR" : _TBillAMt * -1 + " DR";
                    double _TDueAMt = Convert.ToDouble(tbl.Compute("sum([Due Amount])", ""));
                    string _TDueAMt1 = (_TDueAMt > 0) ? _TDueAMt + " CR" : _TDueAMt * -1 + " DR";


                    strHTML.Append("<td style='text-align:center'>" + _TBillAMt1 + "</td > ");
                    strHTML.Append("<td style='text-align:center'>" + _TDueAMt1 + "</td>");
                    strHTML.Append("<td colspan='2'></td>");
                    strHTML.Append("</tr>");
                }
            }
            if (Session["Title"].ToString() == "Secondary Sales")
            {
                if (strbreak != "" || r == tbl.Rows.Count - 1)
                {
                    strHTML.Append("<tr style='font-weight:800;height:24px;background-color:whitesmoke;' class='" + strbreak + "'>");
                    strHTML.Append("<td colspan='3' style='text-align:left;'>Total</td>");
                    strHTML.Append("<td>" + totTrgtAmt + "</td>");
                    strHTML.Append("<td>" + totSecSales + "</td>");
                    strHTML.Append("<td>" + totTrgtVisit + "</td>");
                    strHTML.Append("<td>" + totVisits + "</td>");
                    strHTML.Append("<td>" + totProdVisit + "</td>");
                    strHTML.Append("<td>" + totNonProdVisit + "</td>");
                    strHTML.Append("<td>" + totAchivePer + "</td>");
                    strHTML.Append("<td>&nbsp;</td>");
                    strHTML.Append("<td>&nbsp;</td>");
                    strHTML.Append("<td>&nbsp;</td>");
                    strHTML.Append("</tr>");
                    totSecSales = totTrgtVisit = 0; totTrgtAmt = totVisits = 0; totProdVisit = 0; totNonProdVisit = 0; totAchivePer = 0;
                }
            }
        }

        strHTML.Append("</tbody>");
        strHTML.Append("</table>");
        bindTable.InnerHtml = strHTML.ToString();
    }
}