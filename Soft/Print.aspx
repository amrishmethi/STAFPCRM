<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Soft_Print" %>

<!DOCTYPE html>

<html id="htmlabc" runat="server" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="float: left">
                    <img src="../../img/logo.jpg" height="50px">
                </td>
                <td style="float: right">
                    <p>
                        <strong>Shree Tadkeshwar Agro Food Product</strong>
                        <br />
                        H- 1-37- A, Sarna Doongar Industrial Area, Jhotwara Extension,
            <br />
                        Jaipur - 302012 Rajasthan, India
                    </p>
                </td>
            </tr>
        </table>

        <table id="ExportTbl" border="1" class="table display table-striped">
            <thead>
                <tr>
                    <th style="text-align: left;">Sr. No.</th>
                    <th style="text-align: left;">Date<br />
                        Time</th>
                    <th style="text-align: left;">Employee</th>
                    <th style="text-align: left;">Party</th>
                    <th style="text-align: left;">District</th>
                    <th style="text-align: left;">Station</th>
                    <th style="text-align: left;">WhatsApp No</th>
                    <th style="text-align: left;">Description</th>
                    <th style="text-align: left;">Location</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rep" runat="server">
                    <ItemTemplate>
                        <tr class="gradeA">
                            <td>
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td style="text-align: left;"><%#Eval("DateTime") %> </td>
                            <td style="text-align: left;"><%#Eval("Employee") %></td>
                            <td style="text-align: left;"><%#Eval("Party") %></td>
                            <td style="text-align: left;"><%#Eval("District") %></td>
                            <td style="text-align: left;"><%#Eval("Station") %></td>
                            <td style="text-align: left;"><%#Eval("WhatsApp No") %></td>
                            <td style="text-align: left;"><%#Eval("Description") %></td>
                            <td style="text-align: left;"><%#Eval("Location") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </form>
</body>
</html>
