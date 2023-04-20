using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        lblHeading.Text = Session["Title"].ToString();
        lblDateRng.Text = Session["DateRange"].ToString();
        CreateTableHTML((DataTable)Session["GridData"]);


    }



    private void CreateTableHTML(DataTable tbl)
    {
        string strbreak = ""; string brkStr = "";
        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<table  border='1' class='table display table-striped' style='border-collapse: collapse; font-size:10px;width: 100%;'>");
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
        //strHTML.Append("<tr style='height:2px;background-color:silver;'>");
        //strHTML.Append("<td style='text-align: left;' colspan='" + tbl.Columns.Count + "'></td>");
        //strHTML.Append("</tr>");
        decimal totSecSales = 0, totTrgtAmt = 0,totAchivePer = 0;
        int totVisits = 0, totTrgtVisit = 0, totProdVisit = 0, totNonProdVisit = 0;
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
                totAchivePer += Convert.ToDecimal(tbl.Rows[r][6].ToString() == "" ? "0" : tbl.Rows[r][9].ToString().Replace('%',' ').Trim());
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

            strHTML.Append("<tr>");

            for (int c = 0; c < tbl.Columns.Count; c++)
            {
                strHTML.Append("<td style='text-align: left;'>" + tbl.Rows[r][c] + "</td>");
            }
            strHTML.Append("</tr>");
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
                    totSecSales = totTrgtVisit = 0; totTrgtAmt = totVisits = 0; totProdVisit = 0; totNonProdVisit = 0; totAchivePer=0;  
                }
            }
        }

        strHTML.Append("</tbody>");
        strHTML.Append("</table>");
        bindTable.InnerHtml = strHTML.ToString();
    }
}