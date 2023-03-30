<%@ Page Title="" Language="C#" MasterPageFile="~/Soft/AdminMaster.master" AutoEventWireup="true" CodeFile="AttandanceCalander.aspx.cs" Inherits="Soft_AttandanceCalander" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        div > p {
            background: white;
        }
    </style>
    <title>Attandance Calander</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <section class="content-header" style="height: 2.5em;">
        <h1>Attandance Calander
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Soft/Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="/Soft/AttandanceCalander.aspx" class="active">Attandance Calander</a></li>
        </ol>
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
                            <div class="col-md-3">
                                <label>
                                    Employee 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="drpEmployee" ForeColor="Red" ValidationGroup="Save" InitialValue="0"></asp:RequiredFieldValidator></label>
                                <asp:DropDownList ID="drpEmployee" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <label>Month</label>
                                <asp:TextBox ID="mnth" runat="server" type="text" class="form-control MnthPicker" autocomplete="off" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="mnth" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="Search" runat="server" ValidationGroup="Save" CssClass="btn btn-lg btn-success" Text="Search" OnClick="Search_Click" />
                            </div>
                            <div class="col-md-4">
                                <br />
                                <asp:Label ID="Attandance" Text="Attandance" ForeColor="Green" runat="server" Enabled="false" Font-Size="X-Large" Style="text-align: left" />
                                &nbsp;
                                &nbsp;
                                &nbsp;
                                &nbsp;
                                &nbsp;
                                <asp:Label ID="Leave" Text="Leave" ForeColor="Red" runat="server" Enabled="false" Font-Size="X-Large" Style="text-align: center" />
                                &nbsp;
                                 &nbsp;
                                 &nbsp;
                                &nbsp;
                                &nbsp;
                                <asp:Label ID="Holiday" Text="Holiday" ForeColor="Orange" runat="server" Enabled="false" Font-Size="X-Large" Style="text-align: right" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="faqs-top-grids" style="height: 100%;">

            <div id="calendar" class="cal-context" style="width: 100%;">
                <div class="cal-row-fluid cal-row-head">
                    <div class="cal-cell1">Sunday</div>
                    <div class="cal-cell1">Monday</div>
                    <div class="cal-cell1">Tuesday</div>
                    <div class="cal-cell1">Wednesday</div>
                    <div class="cal-cell1">Thursday</div>
                    <div class="cal-cell1">Friday</div>
                    <div class="cal-cell1">Saturday</div>


                </div>
                <div class="cal-month-box">
                    <%
                        int mnt = DateTime.Today.Month;
                        if (mnta != "")
                        {
                            mnt = Convert.ToInt16(mnta);
                        }
                        int year = DateTime.Today.Year;
                        if (yr != "")
                        {
                            year = Convert.ToInt16(yr);
                        }
                        int noofday = DateTime.DaysInMonth(year, mnt);
                        int dy = 1;
                        DateTime firstday = Convert.ToDateTime(mnt + "/01/" + year);
                        int dayofweek = (int)firstday.DayOfWeek;
                        int wiker = 5;
                        if (dayofweek > 4)
                        {
                            if (noofday == 28 || noofday == 29)
                            {
                                wiker = 5;
                            }
                            else
                            {
                                wiker = 6;
                            }
                        }
                        for (int i = 1; i <= wiker; i++)
                        {%>
                    <div class="cal-row-fluid cal-before-eventlist">
                        <%for (int j = 0; j <= 6; j++)
                            {
                        %>
                        <div class="cal-cell1 cal-cell">
                            <div class="cal-month-day cal-day-inmonth" style="">
                                <%if (j == dayofweek || dy > 1)
                                    {
                                        if (dy <= noofday)
                                        {
                                %> <span class="pull-right" style="color: white; font-size: xx-large;"><%=dy %></span>
                                <% if (FData1.Rows.Count > 0)
                                    {
                                        System.Data.DataView dvv = FData1.DefaultView;
                                        dvv.RowFilter = "ss='" + Convert.ToDateTime(mnt + "/" + dy + "/" + year).ToString("dd/MM/yyyy") + "'";
                                        if (dvv.ToTable().Rows.Count > 0)
                                        {
                                            foreach (System.Data.DataRow item in dvv.ToTable().Rows)
                                            {
                                                if (Convert.ToDateTime(item["DATEFROM"]).ToString("dd/MM/yyyy") == Convert.ToDateTime(mnt + "/" + dy + "/" + year).ToString("dd/MM/yyyy"))
                                                {%>
                                <p style="background-color: orange; color: #fff; min-height: 90px;" align='center'>
                                    <%= item["HOLIDAY_NAME"] %>
                                </p>
                                <%}

                                            }
                                        }
                                    }
                                %>
                                <% if (FData.Rows.Count > 0)
                                    {
                                        System.Data.DataView dvv = FData.DefaultView;
                                        dvv.RowFilter = "ss='" + Convert.ToDateTime(mnt + "/" + dy + "/" + year).ToString("dd/MM/yyyy") + "'";
                                        if (dvv.ToTable().Rows.Count > 0)
                                        {
                                            foreach (System.Data.DataRow item in dvv.ToTable().Rows)
                                            {
                                                if (Convert.ToDateTime(item["AttendanceDateIn"]).ToString("dd/MM/yyyy") == Convert.ToDateTime(mnt + "/" + dy + "/" + year).ToString("dd/MM/yyyy"))
                                                {%>
                                <p style="background-color: green; color: #fff; min-height: 90px;" align='center'>
                                    IN: <%=Convert.ToDateTime(item["AttendanceDateIn"]).ToString("hh:mm tt")%>
                                    <br />
                                    <%if (item["IsAttendanceOUT"].ToString() == "True")
                                        { %>
                                    Out: <%= Convert.ToDateTime(item["AttendanceDateOut"]).ToString("hh:mm tt")%>
                                    <%} %>
                                </p>
                                <%}

                                        }
                                    }
                                    else
                                    {%>
                                <p style="background-color: Red; color: #fff; min-height: 90px; vertical-align: middle;" align='center'>
                                    LEAVE
                                </p>
                                <% }
                                    }
                                %>

                                <%  dy++;
                                        }
                                    }

                                %>
                            </div>
                        </div>
                        <% 
                            } %>
                    </div>

                    <% } %>
                </div>
            </div>
            <div>
                <div class="row">
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="Server">
    <link href="../css/CalenderView.css" rel="stylesheet" />
    <script src="js/jquery-ui.js"></script>
    <link href="js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ui-datepicker-calendar {
            /*display: tr;*/
        }
    </style>
    <script type="text/javascript">
        $('.MnthPicker').datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            format: "mm-yyyy",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true
        });
    </script>
</asp:Content>

