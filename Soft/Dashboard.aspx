<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Admin_Dashboard" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <script type="text/javascript"
src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false">
</script>--%>

    <%--<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD7DfAXDji_OMVHi0-vwlWTI5csEXXwCgE"
        type="text/javascript"></script>--%>
    <title>Dashboard(STAFP)</title>
    <style>
        #Body_linkloc {
            color: whitesmoke;
        }

            #Body_linkloc:hover {
                color: white;
            }
    </style>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Dashboard
            <small>Over Views</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active"><a href="#">Dashboard</a></li>
        </ol>
        <%--   <div class="small-box bg-aqua">
            &nbsp; <i class="fa fa-map-marker"></i>&nbsp;
                  <p id="demo" style="display: inline;"></p>
            <p class="small-box-footer">
                <a id="linkloc" runat="server" href="javascript:;" class="abc">CheckIN</a> <i class="fa fa-arrow-circle-up"></i>
            </p>
        </div>--%>
    </section>




    <asp:Literal ID="lits" runat="server"></asp:Literal>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-lg-4 col-xs-6">
                <!-- small box -->
                <div class="small-box bg-aqua">
                    <div class="inner">
                        <h4>Total Employees (SAles): <b id="TotUsr" runat="server">0</b></h4>
                        <p>
                            Today's CheckIn : <b id="chkInusr" runat="server">0</b>
                            <br />
                            Today's CheckOut : <b id="chkOutusr" runat="server">0</b>
                        </p>
                    </div>
                    <div class="icon">
                        <i class="ion ion-person-add"></i>
                    </div>
                    <a href="CheckInReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                </div>
            </div>
            <!-- ./col -->
            <div class="col-lg-8 col-xs-6">
                <!-- small box -->
                <div class="small-box bg-green">
                    <div class="inner">
                        <h4>Total Employees : <b id="TotUsr1" runat="server">0</b></h4>
                        <div id="deptBlock" runat="server" class="col-md-12" style="font-weight:bold;text-align:center;"></div>
                        <div id="AttnInBlock" runat="server" class="col-md-12" style="font-weight:bold;text-align:center;"></div>
                        <div id="AttnOutBlock" runat="server" class="col-md-12" style="font-weight:bold;text-align:center;"></div>
                    </div>
                    <div class="icon">
                        <i class="ion ion-stats-bars"></i>
                    </div>
                    <a href="AttendanceReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                </div>
            </div>
            <!-- ./col -->
        </div>
        <div class="row">
            <%--<div class="col-lg-3 col-xs-6">--%>
            <!-- small box -->
            <%--<div class="small-box bg-yellow">
                    <div class="inner">
                        <h4>Total : <b id="B1" runat="server">14</b></h4>

                        <p>New : <b id="B2" runat="server">15</b></p>
                    </div>
                    <div class="icon">
                        <i class="ion ion-bag"></i>
                    </div>
                    <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                </div>--%>
            <%--</div>--%>
            <!-- ./col -->
            <div class="col-lg-12 col-xs-6">
                <!-- smalls box -->
                <div class="bg-red" style="padding:10px;">
                    <div class="inner">
                        <h4>Item Summary : <b id="todayDate" runat="server"></b></h4>
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="widget-content">
                                    <%--<div class="table-responsive">--%>
                                    <table id="ExportTbl" class="table table-bordered display table-striped" style="color: black;">
                                        <thead>
                                            <tr>
                                                <th>Sr. No.</th>
                                                <th>Employee</th>
                                                <th>Group</th>
                                                <th>Qty</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rep" runat="server">
                                                <ItemTemplate>
                                                    <tr class="gradeA">
                                                        <td>
                                                            <%#Container.ItemIndex+1 %>
                                                        </td>
                                                        <td style="text-align: left;"><%#Eval("UserName") %></td>
                                                        <td style="text-align: left;"><%#Eval("GroupName") %></td>
                                                        <td style="text-align: left;"><%#Eval("Qty") %></td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    <%--</div>--%>
                                </div>
                            </div>
                        </div>
                        <%--<p>New  : <b id="B4" runat="server">17</b></p>--%>
                    </div>
                    <%--   <div class="icon">
                        <i class="ion ion-pie-graph"></i>
                    </div>--%>
                    <%--  <a href="SecondarySalesReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>--%>
                </div>
            </div>
            <!-- ./col -->
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
    <script type="text/javascript">  
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
            }
            else { document.getElementById("demo").innerHTML = "Geolocation is not supported by this browser."; }
        }


        function showPosition(position) { 
            var latvalue = position.coords.latitude;
            var longvalue = position.coords.longitude;

            getcityname(latvalue, longvalue);

        }
    </script>
    <script type="text/javascript">  
        function getcityname(latvalue, longvalue) {
          
            var geocoder;
            geocoder = new google.maps.Geocoder();
            var latlng = new google.maps.LatLng(latvalue, longvalue);

            //alert(latlng);
            geocoder.geocode(
                { 'latLng': latlng },
                function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        if (results[0]) {
                            var add = results[0].formatted_address;
                            var value = add.split(",");

                            count = value.length;
                            var places = "";
                            for (var i = 0; i < count; i++) {
                                places = places + value[i];
                            }
                            //alert(places);


                            document.getElementById('demo').innerHTML = places;
                            country = value[count - 1];
                            state = value[count - 2];
                            city = value[count - 3];
                            // alert("city name is: " + add);
                        }
                        else {
                            //alert("address not found");
                        }
                    }
                    else {
                        //alert("Geocoder failed due to: " + status);
                    }
                }
            );
        }
    </script>
    <script type="text/javascript"> 
        $("#Body_linkloc").click(function () {
          
            this.href = 'checkInOut.aspx?loc=' + document.getElementById('demo').innerHTML
        });
    </script>
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>


