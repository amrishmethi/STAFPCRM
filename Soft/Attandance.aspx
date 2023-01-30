<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="Attandance.aspx.cs" Inherits="Soft_Attandance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"> 
    <title>Attandance (STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment.min.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBm1CyOx9FugNOb9K6F349bd6mDWjsuB3k"
        type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="content-header" style="height: 2.5em;">
        <h1>Attandance 
            <asp:Label ID="lblDate" ClientIDMode="Static" runat="server" Style="float: right"></asp:Label>
        </h1>

        <asp:HiddenField ID="hddLnL" runat="server" />
        <asp:Literal ID="lits" runat="server"></asp:Literal>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-3">
                                <label>Department</label>
                                <asp:DropDownList ID="drpDepartment" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-3 hidden">
                                <label>Designation</label>
                                <asp:DropDownList ID="drpDesignation" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Reporting Manager</label>
                                <asp:DropDownList ID="drpProjectManager" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-3 isEditVisible1">
                                <label>Date</label>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datepicker " placeholder="dd/MM/yyyy" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="clearfix">&nbsp;</div>

                        <div class="clearfix">&nbsp;</div>
                    </div>
                </div>
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="widget-content">
                            <div class="table-responsive">
                                <table id="ExportTbl" class="table table-bordered display table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">Sr. No.</th>
                                            <th style="text-align: left;">Department</th>
                                            <th style="text-align: left;">Emp Name</th>

                                            <th>
                                                <label id="lblAction">Attandance</label></th>
                                            <th  style="text-align: left;">&nbsp;</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep" runat="server" OnItemCommand="rep_ItemCommand" OnItemDataBound="rep_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("DEPT_NAME") %></td>
                                                    <td style="text-align: left;"><%#Eval("Emp_Name") %></td>

                                                    <td style="text-align: left;">

                                                        <div class="isEditVisible" style="display: inline;">
                                                            <asp:LinkButton ID="lnkIN" runat="server" Style="padding: 1px 6px; font-size: 14px;" CommandName="AttandanceIn" CssClass="btn btn-small btn-primary" CommandArgument='<%#Eval("EmpId") %>'> In</asp:LinkButton>
                                                            <asp:Label ID="lblAttandance" runat="server" Style="font-size: large; color: Highlight;"></asp:Label>
                                                            &nbsp;&nbsp;
                                                            <asp:HiddenField ID="HddEmpId" runat="server" Value='<%#Eval("EmpId") %>' />
                                                            <asp:HiddenField ID="HddCrmUserId" runat="server" Value='<%#Eval("CrmUserId") %>' />
                                                        </div>
                                                        <div class="isDelVisible" style="display: inline;">
                                                            <asp:LinkButton ID="lnkOut" runat="server" Style="padding: 1px 6px; font-size: 11px;" CommandName="AttandanceOut" CssClass="btn btn-small btn-danger" CommandArgument='<%#Eval("EmpId") %>'>  Out</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkLeave" runat="server" Style="padding: 1px 6px; font-size: 11px;" CommandName="Leave" CssClass="btn btn-small btn-success" CommandArgument='<%#Eval("EmpId") %>'>  Leave</asp:LinkButton>
                                                        </div>

                                                    </td>
                                                    <td >
                                                        <asp:TextBox ID="txtWorkingTimeFRom" placeholder="Select Time" runat="server" CssClass="demo form-control timepicker "></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            $.ajax({
                url: 'Attandance.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger
                    let text = data.d;
                    const myArray = text.split(",");

                    //document.getElementById("Body_lnkAdd").style.display = myArray[0] == "False" ? "none" : "";

                    var elements = document.getElementsByClassName("isEditVisible1");
                    Array.prototype.forEach.call(elements, function (element) {
                        element.style.display = myArray[1] == "False" ? "none" : "inline";
                    });
                },
                error: function (response) {
                },
                failure: function (response) {
                }
            });
        })
    </script> 

       <script>
           // Set the date we're counting down to 
           $(document).ready(function () {
               // Update the count down every 1 second
               var x = setInterval(function () {
                   debugger
                   // Get today's date and time


                   var date = new Date();
                   var current_date = date.getHours() + ":" + (date.getMinutes()) + ":" + date.getSeconds();
                   document.getElementById("lblDate").innerText = current_date.toString();

                   var elements = document.getElementsByClassName("demo");
                   Array.prototype.forEach.call(elements, function (element) {
                       element.innerText = current_date;
                   });

               }, 1000);
           })
       </script>


     
    <script type="text/javascript">
        $(document).ready(function () {

            $.ajax({
                url: 'PayrollRep.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger
                    let text = data.d;
                    const myArray = text.split(",");

                    document.getElementById("Body_lnkAdd").style.display = myArray[0] == "False" ? "none" : "";

                    var elements = document.getElementsByClassName("isEditVisible");
                    Array.prototype.forEach.call(elements, function (element) {
                        element.style.display = myArray[1] == "False" ? "none" : "inline";
                    });
                    var elements1 = document.getElementsByClassName("isDelVisible");
                    Array.prototype.forEach.call(elements1, function (element) {
                        element.style.display = myArray[2] == "False" ? "none" : "inline";
                    });

                    if (myArray[1] == 'False' && myArray[2] == 'False') {
                        document.getElementById("lblAction").innerHTML = "";

                    }
                    document.getElementsByClassName("buttons-excel")[0].style.display = myArray[3] == "False" ? "none" : "";
                    document.getElementsByClassName("buttons-pdf")[0].style.display = myArray[3] == "False" ? "none" : "";
                },
                error: function (response) {
                },
                failure: function (response) {
                }
            });
        })

    </script>
    <uc1:DTJS runat="server" ID="DTJS" />

    <script type="text/javascript">  
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
            }

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
            document.getElementById("Body_hddLnL").value = latlng
            //  alert(latlng);
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

            if (document.getElementById('demo').innerHTML == "") {
                alert("This site has been blocked from accessing your location \n Please Turn on!!!");
                this.removeAttribute("class");
            } else {

                this.href = 'Attendance.aspx?loc=' + document.getElementById('demo').innerHTML + '&lnl=' + document.getElementById("Body_hddLnL").value

            }
        });
    </script>
    <script> 
        $('.timepicker').datetimepicker({
            format: 'hh mm a'
        });
    </script>
     
</asp:Content>

