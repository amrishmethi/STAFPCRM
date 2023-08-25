<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="partyacbal.aspx.cs" Inherits="Soft_partyacbal" %>

<%@ Register Src="~/Soft/UserControls/DTCSS.ascx" TagPrefix="uc1" TagName="DTCSS" %>
<%@ Register Src="~/Soft/UserControls/DTJS.ascx" TagPrefix="uc1" TagName="DTJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Party Balance Report(STAFP)</title>
    <uc1:DTCSS runat="server" ID="DTCSS" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Party Balance Report
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/partyacbal.aspx" class="active">Party Balance Report</a></li>
        </ol>
    </section> 
   <section class="content">
       <div class="row">
           <div class="col-md-12">
               <div class="box box-primary">
                   <div class="box-body">
                       <div class="form-group">
                           <div class="col-md-3">
                               <label>Station</label>
                               <asp:DropDownList ID="DrpStation" runat="server" CssClass="form-control select2"></asp:DropDownList>
                           </div>
                           <div class="col-md-3">
                               <label>Party</label>
                               <asp:DropDownList ID="DrpParty" runat="server" CssClass="form-control select2"></asp:DropDownList>
                           </div>
                           <div class="col-md-3">
                               <br />
                               <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" Text="Search" />
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
                                           <th>Sr. No.</th>
                                           <th>Party</th>
                                           <th>Station</th>
                                           <th>Debit</th>
                                           <th>Credit</th>
                                       </tr>
                                   </thead>
                                   <tbody>
                                       <asp:Repeater ID="rep" runat="server">
                                           <ItemTemplate>
                                               <tr class="gradeA">
                                                   <td>
                                                       <%#Container.ItemIndex+1 %> 
                                                   </td>
                                                   <td style="text-align: left;"><%#Eval("ACName") %></td>
                                                   <td style="text-align: left;"><%#Eval("Station") %></td>
                                                   <td style="text-align: left;"><%#Eval("DR") %></td>
                                                   <td style="text-align: left;"><%#Eval("CR") %></td>
                                               </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                   </tbody>
                                   <tfoot>
                                       <tr class="gradeA">
                                           <td></td>
                                           <td></td>
                                           <td>Total</td>
                                           <td><input id="lblDrTotal" runat="server" value="0" readonly class="form-control" /></td>
                                           <td><input id="lblCrTotal" runat="server" value="0" readonly class="form-control" /></td>
                                       </tr>
                                   </tfoot>
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
    <uc1:DTJS runat="server" ID="DTJS" />
</asp:Content>

