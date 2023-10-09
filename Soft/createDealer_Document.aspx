<%@ Page Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="createDealer_Document.aspx.cs" Inherits="Admin_createDealer_Document" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Create Dealer Document(STAFP)</title>
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
    <section class="content-header">
        <h1>Document
        </h1> <ol class="breadcrumb">
            <li>
               <button onclick="history.go(-1);
        return false;"
                    class="btn btn-sm btn-success">
                    Go Back</button></li></ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">

                <div class="widget-content">
                    <table id="ExportTbl" border="1" class="table display table-striped">
                        <thead>
                            <tr>
                                <th style="text-align: left;">Sr. No.</th>
                                <th style="text-align: left;">Document</th>
                                <th style="text-align: left;">Image</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rep" runat="server">
                                <ItemTemplate>
                                    <tr class="gradeA">
                                        <td>
                                            <%#Container.ItemIndex+1 %>
                                        </td>
                                        <td style="text-align: left;"><%#Eval("DocName") %></td>
                                        <td><%--<a href="ResizeImage.aspx?imgurl=<%# "https://app.tadkeshwarfoods.com/PartyDocument/" + Eval("DocFileName") %>" class="abc1">
                                            <asp:Image runat="server" ImageUrl='<%# "https://app.tadkeshwarfoods.com/PartyDocument/" + Eval("DocFileName") %>' Width="50" Height="50" Visible='<%# (Eval("DocFileName").ToString()=="")?false:true %>' /></a>--%>
                                            <img id="myImg" runat="server" src='<%# "https://app.tadkeshwarfoods.com/PartyDocument/" + Eval("DocFileName") %>' style="width:80px; height:80px; padding: 10px; cursor: zoom-in" visible='<%# (Eval("DocFileName").ToString()=="")?false:true %>' onclick="imgclick(this)"/>

                                    <!-- The Modal -->
                                    <div id="myModal" class="modal">
                                        <span class="close">&times;</span>
                                        <img class="modal-content" id="img01">
                                        <div id="caption"></div>
                                    </div>

                                    <script>
                                        // Get the modal
                                        function imgclick(e) {
                                          
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
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>

                </div>
            </div>

        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>
