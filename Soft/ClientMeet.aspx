<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="ClientMeet.aspx.cs" Inherits="Admin_ClientMeet" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Client Meet(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
    <style>
        #Body_myImg {
            border-radius: 5px;
            cursor: pointer;
            transition: 0.3s;
        }

            #Body_myImg:hover {
                opacity: 0.7;
            }

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.9); /* Black w/ opacity */
        }

        /* Modal Content (image) */
        .modal-content {
            margin: auto;
            display: block;
            width: 80%;
            max-width: 700px;
        }

        /* Caption of Modal Image */
        #caption {
            margin: auto;
            display: block;
            width: 80%;
            max-width: 700px;
            text-align: center;
            color: #ccc;
            padding: 10px 0;
            height: 150px;
        }

        /* Add Animation */
        .modal-content, #caption {
            -webkit-animation-name: zoom;
            -webkit-animation-duration: 0.6s;
            animation-name: zoom;
            animation-duration: 0.6s;
        }

        @-webkit-keyframes zoom {
            from {
                -webkit-transform: scale(0)
            }

            to {
                -webkit-transform: scale(1)
            }
        }

        @keyframes zoom {
            from {
                transform: scale(0)
            }

            to {
                transform: scale(1)
            }
        }

        /* The Close Button */
        .close {
            position: absolute;
            top: 100px;
            right: 35px;
            color: #FFF;
            font-size: 40px;
            font-weight: bold;
            transition: 0.3s;
        }

            .close:hover,
            .close:focus {
                color: #bbb;
                text-decoration: none;
                cursor: pointer;
            }

        /* 100% Image Width on Smaller Screens */
        @media only screen and (max-width: 700px) {
            .modal-content {
                width: 100%;
            }
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Client Meet
        <asp:LinkButton Style="display: inline;" ID="lnkDownloadPDF" runat="server" CssClass="btn btn-sm btn-success" OnClick="lnkDownloadPDF_Click">Print</asp:LinkButton></h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/ClientMeet.aspx" class="active">Client Meet</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-4">
                                <label>Employee</label>
                                <asp:DropDownList ID="drpEmp" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpEmp_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Department</label>
                                <asp:DropDownList ID="drpDept" runat="server" CssClass="form-control select2" >
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Head Quarter</label>
                                <asp:DropDownList ID="drpHqtr" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="drpEmp_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Client Meet Type</label>
                                <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2" >
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="New" Text="New"></asp:ListItem>
                                    <asp:ListItem Value="Exist" Text="Exist"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <label>Date From</label>
                                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control datepicker" >
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>Date To</label>
                                <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control datepicker" >
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>IsMeet</label>
                                <asp:DropDownList ID="drpIsMeet" runat="server" CssClass="form-control" >
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-2">
                                <label>Status</label>
                                <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control" >
                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="Active" Text="Active" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Non-Active" Text="Non-Active"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Show Image</label>
                                <asp:DropDownList ID="drpImg" runat="server" CssClass="form-control" >
                                   <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Get Report" OnClick="btnSubmit_Click" />

                              

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
                                <table id="ExportTbl" border="1" class="table display table-striped">
                                    <thead>
                                        <tr>
                                            <th style="text-align: left;">Sr. No.</th>
                                            <th style="text-align: left;">Date<br />
                                                Time</th>
                                            <th style="text-align: left;">Employee</th>
                                            <th style="text-align: left;">Party</th>
                                            <th style="text-align: left;">Head Quarter</th>
                                            <th style="text-align: left;">District</th>
                                            <th style="text-align: left;">Station</th>
                                            <th style="text-align: left;">Mobile No</th>
                                            <th style="text-align: left;">WhatsApp No</th>
                                            <th style="text-align: left;">Description</th>
                                            <th style="text-align: left;">Location</th>
                                            <th style="text-align: left;" id="isShowImg" runat="server">Image</th>
                                            <th style="text-align: left;">Meet Type</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rep"  runat="server" OnItemDataBound="rep_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("AddedDate") %><br />
                                                        <%#Eval("AddedTime") %></td>
                                                    <td style="text-align: left;"><%#Eval("Name") %></td>
                                                    <td style="text-align: left;"><%#Eval("PartyName") %></td>
                                                    <td style="text-align: left;"><%#Eval("HeadQtr") %></td>
                                                    <td style="text-align: left;"><%#Eval("District") %></td>
                                                    <td style="text-align: left;"><%#Eval("Station") %></td>
                                                    <td style="text-align: left;"><%#Eval("MobileNo") %></td>
                                                    <td style="text-align: left;"><%#Eval("WhatsAppNo") %></td>
                                                    <td style="text-align: left;"><%#Eval("Description") %></td>
                                                    <td style="text-align: left;"><%#Eval("Place") %></td>
           

                 <td id="isShowImgData" runat="server" >
                                                        <img id="myImg" runat="server" src='<%# "https://app.tadkeshwarfoods.com/AreaDevelop/" + Eval("Image") %>' style="width: 80px; height: 80px; padding: 10px; cursor: zoom-in" visible='<%# (Eval("Image").ToString()=="")?false:true %>' onclick="imgclick(this)" />

                                                        <!-- The Modal -->
                                                        <div id="myModal" class="modal">
                                                            <span class="close">&times;</span>
                                                            <img class="modal-content" id="img01">
                                                            <div id="caption"></div>
                                                        </div>

                                                        <script>
                                                            // Get the modal
                                                            function imgclick(e) {
                                                                debugger
                                                                var modal = document.getElementById("myModal");

                                                                // Get the image and insert it inside the modal - use its "alt" text as a caption
                                                                var ele = e.id;
                                                                var img = document.getElementById(ele);
                                                                var modalImg = document.getElementById("img01");
                                                                var captionText = document.getElementById("caption");
                                                                //img.onclick = function () {
                                                                modal.style.display = "block";
                                                                modalImg.src = img.src;
                                                                captionText.innerHTML = this.alt;
                                                                //}
                                                                // Get the <span> element that closes the modal
                                                                var span = document.getElementsByClassName("close")[0];

                                                                // When the user clicks on <span> (x), close the modal
                                                                span.onclick = function () {
                                                                    modal.style.display = "none";
                                                                }
                                                            }

                                                        </script>
                                                    </td>

                                                    <td style="text-align: left;"><%#Eval("ClientMeetType") %></td>
                                                    
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


            <!--model to show enlarged image -->
            <div class="modal fade" id="enlargeImageModal" tabindex="-1" role="dialog" aria-labelledby="enlargeImageModal" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-body">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <img src="" class="enlargeImageModalSource" style="width: 100%;">
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
            debugger
            $.ajax({
                url: 'ClientMeet.aspx/ControlAccess',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger
                    let text = data.d;
                    const myArray = text.split(",");

                    //document.getElementById("Body_lnkAdd").style.display = myArray[0] == "False" ? "none" : "";

                    //var elements = document.getElementsByClassName("isEditVisible");
                    //Array.prototype.forEach.call(elements, function (element) {
                    //    element.style.display = myArray[1] == "False" ? "none" : "inline";
                    //});
                    //var elements1 = document.getElementsByClassName("isDelVisible");
                    //Array.prototype.forEach.call(elements1, function (element) {
                    //    element.style.display = myArray[2] == "False" ? "none" : "inline";
                    //});
                    var elements2 = document.getElementsByClassName("isAssVisible");
                    Array.prototype.forEach.call(elements2, function (element) {
                        element.style.display = myArray[4] == "False" ? "none" : "";
                    });
                    var elements3 = document.getElementsByClassName("isLoginVisible");
                    Array.prototype.forEach.call(elements3, function (element) {
                        element.style.display = myArray[5] == "False" ? "none" : "";
                    });

                    //if (myArray[1] == 'False' && myArray[2] == 'False') {
                    //    document.getElementById("lblAction").innerHTML = "";

                    //}
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
</asp:Content>

