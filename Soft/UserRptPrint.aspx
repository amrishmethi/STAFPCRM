<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRptPrint.aspx.cs" Inherits="UserRptPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
   <title>STAFP Report</title>
    <link href="../bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" rel="stylesheet" />
    <script src="../bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
    <script>
        $(function () {
            $(".textarea").wysihtml5();
        });
    </script>
    <script>
        window.print();
    </script>
</head>
<body>
    <form id="form2" runat="server">
        <br/><br/>
        <div id="hdd" runat="server">
        </div>
    </form>
</body>


</html>
