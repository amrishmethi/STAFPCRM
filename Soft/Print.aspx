<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Soft_Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .page-header, .page-header-space {
            height: 125px;
        }

        .page-footer, .page-footer-space {
            height: 15px;
        }

        .page-footer {
            position: fixed;
            bottom: 0;
            width: 100%;
            /*border-top: 1px solid black; /* for demo */
            background: white;
        }

        .page-header {
            /*margin-top:-10px;*/
            position: fixed;
            top: 0mm;
            width: 100%;
            /*border-bottom: 1px solid black; /* for demo */
            background: white;
        }

        .page {
            page-break-after: always;
        }
    </style>
</head>
<body onload="window.print()">
    <form id="form1" runat="server">
        <div class="page-header" style="text-align: center">
            <table style="width: 100%; padding-right: 15px; border-spacing: 0px;">

                <tr style="padding: 0px; margin-top: -10px; margin-bottom: -10px;">
                    <td style="text-align: left;padding: 0px;">
                        <img src="../../img/logo.jpg" height="80px"><br />

                    </td>
                    <td style="text-align: right;padding: 0px;">
                        <p style="font-size: 12px;">
                            <strong style="font-size: 18px;">Shree Tadkeshwar Agro Food Product</strong>
                            <br />
                            H- 1-37- A, Sarna Doongar Industrial Area, Jhotwara Extension,
            <br />
                            Jaipur - 302012 Rajasthan, India
                        <br />
                            (Mob) : + 91 - 98290 - 32422, (Tel) : 0141 - 3540250
                            <br />
                            (Email) : sales@tadkeshwarfoods.com, (url) : www.tadkeshwarfoods.com
                        </p>
                    </td>
                </tr>
                <tr style="padding: 0px; margin-top: -10px; margin-bottom: -10px;">
                    <td colspan="2" style="padding: 0px;" >
                        <asp:Label ID="lblHeading" runat="server" Style="font-size: 20px; color: firebrick;"></asp:Label>
                        <asp:Label ID="lblDateRng" runat="server"></asp:Label></td>
                </tr>
            </table>
        </div>

        <div class="page-footer">
            <span style="font-size: 8px; margin-top:-10px;"><%= System.DateTime.Now.ToString() %></span>
        </div>


        <table style="width:100%;">

            <thead>
                <tr>
                    <td>
                        <!--place holder for the fixed-position header-->
                        <div class="page-header-space"></div>
                    </td>
                </tr>
            </thead>

            <tbody>
                <tr style="align:center;">
                    <td>
                        <div id="bindTable" runat="server"></div>
                    </td>
                </tr>
            </tbody>

            <tfoot>
                <tr>
                    <td>
                        <!--place holder for the fixed-position footer-->
                        <div class="page-footer-space"></div>
                    </td>
                </tr>
            </tfoot>
        </table>
    </form>
</body>
</html>
