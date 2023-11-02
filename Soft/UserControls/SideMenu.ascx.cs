using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

public partial class Admin_UserControls_SideMenu : System.Web.UI.UserControl
{
    DataSet ds = new DataSet();
    Data data = new Data();
    HttpCookie Admin;
    string query = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Request.Params["BuiltIn"] != null)
            //{
            Admin = Request.Cookies["STFP"];

            if (!(Admin.Values["Type"].ToString().ToLower().Equals("admin"))) { menu(); }
            else { fillData(); }
            // }
        }
    }
    public void menu()
    {
        bool s = HttpContext.Current.Request.Browser.IsMobileDevice;
        string str = "";
        //str += "  <li class='active'><a href='Dashboard.aspx'><i class='fa fa-book'></i><span>";
        //str += "Dashboard</span></a></li>";
        //if (Admin.Values["Type"].ToString() != "Admin1")
        //{
        query = "select tbl_Menu.*,(select count(*) from tbl_Menu as p inner join tbl_EmpRoles as r on p.MenuId = r.PageID where p.ParentId = tbl_Menu.MenuId and r.UserId = " + Admin.Values["UserId"].ToString() + ") as cc " +
           " from tbl_Menu inner join tbl_EmpRoles on tbl_menu.MenuId = tbl_EmpRoles.PageID where tbl_Menu.ParentId = 0 and tbl_EmpRoles.UserId=" + Admin.Values["UserId"].ToString() + "";
        query += " and tbl_menu.isDelete=0 and tbl_Menu.ISactive=1";
        //   if (s == true)
        //       query += " and IsMobile=1";
        query += " order by tbl_menu.Position";
        //}
        //else
        //{
        //    query = "select tbl_Pages.*,(select count(*) from tbl_Pages as p  where p.Parent=tbl_Pages.id) as cc  " +
        //       " from tbl_Pages  where tbl_Pages.Parent=0  and tbl_Pages.Show=1  order by tbl_Pages.DisplayOrder";
        //}
        ds = data.getDataSet(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                str += "<li class=\"treeview\">";
                str += "<a href=\"" + ds.Tables[0].Rows[i]["MenuLink"].ToString() + "\"><i class=\"fa fa-angle-right\"></i>";
                str += "<span>" + ds.Tables[0].Rows[i]["MenuName"].ToString() + "</span>";
                if (Convert.ToInt32(ds.Tables[0].Rows[i]["cc"]) != 0)
                {
                    str += "<span class='label label-important'>" + ds.Tables[0].Rows[i]["cc"].ToString() + "</span>";
                }
                str += "</a>";
                //if (Admin.Values["Type"].ToString() != "Admin1")
                //{
                query = "select tbl_Menu.* from tbl_Menu inner join tbl_EmpRoles on tbl_Menu.MenuId=tbl_EmpRoles.PageID where tbl_Menu.ParentId=" + ds.Tables[0].Rows[i]["MenuId"].ToString() + "";
                query += " and tbl_EmpRoles.UserId=" + Admin.Values["UserId"].ToString() + " and tbl_Menu.isDelete=0 ";
                //if (s == true)
                //    query += " and IsMobile=1";
                query += " order by tbl_Menu.Position";
                //}
                //else
                //{
                //    query = "select tbl_Pages.* from tbl_Pages  where tbl_Pages.Parent=" + ds.Tables[0].Rows[i]["id"].ToString() + "  and tbl_Pages.Show=1 order by tbl_Pages.DisplayOrder";
                //}

                DataSet ds1 = data.getDataSet(query);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    str += "<ul class=\"treeview-menu\">";
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        str += "<li class=\"treeview\">";
                        str += "<a href=\"" + ds1.Tables[0].Rows[j]["MenuLink"].ToString() + "\" >";
                        str += "<i class=\"fa fa-pie-chart\"></i><span>" + ds1.Tables[0].Rows[j]["MenuName"].ToString() + "</span></a></li>";
                    }

                    str += "</ul>";

                }
                str += "</li>";

            }
        }


        str += "<ul style='margin:0;'><li >&nbsp;</li></ul>";
        str += "<ul style='margin:0;'><li >&nbsp;</li></ul>";
        str += "<ul style='margin:0;'><li>&nbsp;</li></ul>";
        str += "<ul style='margin:0;'><li>&nbsp;</li></ul>";
        SideMenu_ulMenu.InnerHtml = str;
    }
    public void fillData()
    {
        SqlCommand cmd = new SqlCommand("sp_GetMenuList");
        cmd.CommandType = CommandType.StoredProcedure;
        ds = data.getDataSet(cmd);
        DataTable dt = ds.Tables[0];
        DataView dv = dt.DefaultView;
        dv.RowFilter = "ParentId=0";
        dv.Sort = "Position";
        //repMenu.DataSource = dv;
        //repMenu.DataBind();

        //< li class="active"><a href = "dashboard.aspx" >< i class="fa fa-book"></i><span>dashboard</span></a></li>
        //   <asp:repeater id = "repmenu" runat="server" onitemdatabound="repmenu_itemdatabound">
        //       <itemtemplate> <asp:hiddenfield id = "hddmenuid" runat="server" value='<%#eval("menuid") %>' />
        //           <li class="treeview"><a href = '<%#eval("menulink") %>' >< i class="fa c"></i><span><%#eval("menuname") %></span><span class="label label-important" style="font-size:13px"><%#eval("menucount") %></span></a>  
        //               <ul class="treeview-menu"> 
        //                   <asp:repeater id = "repmenuitem" runat="server">
        //                       <itemtemplate>
        //                           <li class="treeview"><a href = '<%#eval("menulink") %>' >< i class="fa fa-pie-chart"></i><span><%#eval("menuname") %></span></a></li>
        //                       </itemtemplate>
        //                   </asp:repeater>
        //               </ul>
        //           </li>

        string str = "";
        foreach (DataRow dr in dv.ToTable().Rows)
        {
            str = str + "<li class='treeview'><a href = '" + dr["MenuLink"] + "'><i class='fa fa-angle-right'></i><span>" + dr["MenuName"] + "</span>";
            if (dr["MenuCount"].ToString() != "")
            {
                str = str + "<span class='label label-important' style='font-size:13px'>" + dr["MenuCount"] + "</span>";
                str = str + "</a>";
                str = str + "<ul class='treeview-menu'>";
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = "ParentId=" + dr["MenuId"];
                dv1.Sort = "Position";
                foreach (DataRow dr1 in dv1.ToTable().Rows)
                {
                    str = str + "<li class='treeview'><a href = '" + dr1["MenuLink"] + "'><i class='fa fa-pie-chart'></i><span>" + dr1["MenuName"] + "</span></a></li>";
                }
                str = str + "</ul>";
            }
            else
            {
                str = str + "</a>";
            }
            str = str + "</li>";
        }


        SideMenu_ulMenu.InnerHtml = str;

    }


    protected void repMenu_itemdatabound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int dd = e.Item.ItemIndex;
            Repeater repMenuItem = (Repeater)e.Item.FindControl("repMenuItem");
            HiddenField hddmenuid = (HiddenField)e.Item.FindControl("hddmenuid");
            DataTable dt = ds.Tables[0];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "ParentId=" + hddmenuid.Value;
            dv.Sort = "Position";
            repMenuItem.DataSource = dv;
            repMenuItem.DataBind();
        }
    }
}