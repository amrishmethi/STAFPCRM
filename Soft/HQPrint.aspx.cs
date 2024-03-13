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

public partial class Soft_HQPrint : System.Web.UI.Page
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
        string _PartyName = "";
        for (int r = 0; r < tbl.Rows.Count; r++)
        {

            if (_PartyName != tbl.Rows[r]["Party"].ToString())
            {
                if (r != 0)
                {
                    strHTML.Append("<tr>");
                    strHTML.Append("<td></td>");
                    strHTML.Append("<td></td>");
                    strHTML.Append("<td style='text-align:center'><b>" + _PartyName + "  " + Session["DateRange"] + " </b></td>");
                    strHTML.Append("<td></td>");
                    strHTML.Append("<td>Total</td>");
                    strHTML.Append("<td style='text-align:center'>" + tbl.Compute("sum([Bill Amount])", "Party='" + _PartyName + "'") + " DR</td>");
                    strHTML.Append("<td style='text-align:center'>" + tbl.Compute("sum([Due Amount])", "Party='" + _PartyName + "'") + " DR</td>");
                    strHTML.Append("<td></td>");
                    strHTML.Append("<td></td>");
                    strHTML.Append("</tr>");
                }
                _PartyName = tbl.Rows[r]["Party"].ToString();
            }
            strHTML.Append("<tr>");

            for (int c = 0; c < tbl.Columns.Count; c++)
            {
                if (tbl.Rows[r][c].ToString().Contains("https"))
                    strHTML.Append("<td style='text-align: center;'><img src='" + tbl.Rows[r][c] + "' height='50px' width='50px' /></td>");
                else
                    strHTML.Append("<td style='text-align: center;'>" + tbl.Rows[r][c] + "</td>");
            }
            strHTML.Append("</tr>");

            if (r + 1 == tbl.Rows.Count)
            {
                strHTML.Append("<tr>");
                strHTML.Append("<td></td>");
                strHTML.Append("<td></td>");
                strHTML.Append("<td style='text-align:center'><b>" + _PartyName + " " + Session["DateRange"] + " </b></td>");
                strHTML.Append("<td></td>");
                strHTML.Append("<td>Total</td>");
                strHTML.Append("<td style='text-align:center'>" + tbl.Compute("sum([Bill Amount])", "Party='" + _PartyName + "'") + " DR</td>");
                strHTML.Append("<td style='text-align:center'>" + tbl.Compute("sum([Due Amount])", "Party='" + _PartyName + "'") + " DR</td>");
                strHTML.Append("<td></td>");
                strHTML.Append("<td></td>");
                strHTML.Append("</tr>");

                strHTML.Append("<tr>");
                strHTML.Append("<td style='text-align:right' colspan='4'></td>");
                strHTML.Append("<td><b>Grand Total</b> </td>");
                strHTML.Append("<td style='text-align:center'>" + tbl.Compute("sum([Bill Amount])", "") + " DR</td>");
                strHTML.Append("<td style='text-align:center'>" + tbl.Compute("sum([Due Amount])", "") + " DR</td>");
                strHTML.Append("<td colspan='2'></td>");
                strHTML.Append("</tr>");
            } 
        }

        strHTML.Append("</tbody>");
        strHTML.Append("</table>");
        bindTable.InnerHtml = strHTML.ToString();
    }
}