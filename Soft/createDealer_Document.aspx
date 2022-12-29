<%@ Page Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="createDealer_Document.aspx.cs" Inherits="Admin_createDealer_Document" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Create Dealer Document(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
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
                                        <td><a href="ResizeImage.aspx?imgurl=<%# "https://app.tadkeshwarfoods.com/PartyDocument/" + Eval("DocFileName") %>" class="abc1">
                                            <asp:Image runat="server" ImageUrl='<%# "https://app.tadkeshwarfoods.com/PartyDocument/" + Eval("DocFileName") %>' Width="50" Height="50" Visible='<%# (Eval("DocFileName").ToString()=="")?false:true %>' /></a></td>
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
