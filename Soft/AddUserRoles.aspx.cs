﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_AddUserRoles : System.Web.UI.Page
{
    Data data = new Data();
    DataSet ds = new DataSet();
    HttpCookie AdminCookie;
    GetData gdate = new GetData();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["STFP"] == null) { Response.Redirect("../Login.aspx"); }

        else
        {
            AdminCookie = Request.Cookies["STFP"];
            if (!IsPostBack)
            {
                FillPages();
                if (Request.QueryString["id"] != null)
                {
                    lbluser.Text = data.getDataSet("select Name from [CSinfo].[dbo].[MobileAppUser] where id =" + Request.QueryString["id"]).Tables[0].Rows[0][0].ToString();
                    UpdateRoles(Request.QueryString["id"]);
                }
            }
        }
    }
    public void FillPages()
    {
        ds = data.getDataSet("select * from tbl_Menu where ParentId=0 and isdelete = 0 order by Position");
        GRD.DataSource = ds;
        GRD.DataBind();
    }
    protected void GRD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPID = (Label)e.Row.FindControl("lblPID");
            GridView GRDInner = (GridView)e.Row.FindControl("GRDInner");
            ds = data.getDataSet("select * from tbl_Menu where ParentId=" + lblPID.Text + " and isdelete = 0  order by MenuId");

            GRDInner.DataSource = ds;
            GRDInner.DataBind();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataSet ds1 = data.getDataSet("select * from tbl_Menu where ParentId=" + ds.Tables[0].Rows[i]["MenuId"].ToString() + " and isdelete = 0  order by Menuid");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    GridView GRDPages = (GridView)GRDInner.Rows[i].FindControl("GRDPages");
                    GRDPages.DataSource = ds1;
                    GRDPages.DataBind();
                }
            }

        }
    }
    public void UpdateRoles(string userid)
    {
        if (GRD.Rows.Count > 0)
        {
            for (int i = 0; i < GRD.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GRD.Rows[i].FindControl("chk");
                Label lblPID = (Label)GRD.Rows[i].FindControl("lblPID");

                if (!data.Exist("Select PageID from tbl_EmpRoles where UserId=" + userid + " and PageID=" + lblPID.Text + ""))
                {
                    chk.Checked = false;
                }
                else
                {
                    chk.Checked = true;
                }
                GridView GRDInner = (GridView)GRD.Rows[i].FindControl("GRDInner");
                if (GRDInner.Rows.Count > 0)
                {
                    for (int j = 0; j < GRDInner.Rows.Count; j++)
                    {
                        Label lblPID1 = (Label)GRDInner.Rows[j].FindControl("lblPID1");
                        CheckBox chk1 = (CheckBox)GRDInner.Rows[j].FindControl("chk1");
                        CheckBox chkAdd = (CheckBox)GRDInner.Rows[j].FindControl("chkAdd");
                        CheckBox chkView = (CheckBox)GRDInner.Rows[j].FindControl("chkView");
                        CheckBox chkEdit = (CheckBox)GRDInner.Rows[j].FindControl("chkEdit");
                        CheckBox chkDelete = (CheckBox)GRDInner.Rows[j].FindControl("chkDelete");
                        CheckBox chkAssign = (CheckBox)GRDInner.Rows[j].FindControl("chkAssign");
                        CheckBox chkLogin = (CheckBox)GRDInner.Rows[j].FindControl("chkLogin");
                        CheckBox chkHr = (CheckBox)GRDInner.Rows[j].FindControl("chkHr");
                        if (!data.Exist("Select PageID from tbl_EmpRoles where UserId=" + userid + " and PageID=" + lblPID1.Text + ""))
                        {
                            chk1.Checked = false;
                        }
                        else
                        {
                            chk1.Checked = true;
                        }
                        CheckBoxList ChkPages = (CheckBoxList)GRDInner.Rows[j].FindControl("ChkPages");
                        ds = data.getDataSet("select * from tbl_EmpRoles where UserId=" + userid + " and PageID=" + lblPID1.Text + " ");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            chkHr.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["HrStatus"].ToString());
                            chkAdd.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AddStatus"].ToString());
                            chkView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ViewP"].ToString());
                            chkEdit.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["EditStatus"].ToString());
                            chkDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DeleteStatus"].ToString());
                            chkAssign.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AssignStatus"].ToString());
                            chkLogin.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["LoginStatus"].ToString());
                        }
                        else
                        {
                            chkHr.Checked = false;
                            chkAdd.Checked = false;
                            chkView.Checked = false;
                            chkEdit.Checked = false;
                            chkDelete.Checked = false;
                            chkAssign.Checked = false;
                            chkLogin.Checked = false;
                        }
                    }

                }
            }
        }
    }
    public void AssignAdmin(string userid)
    {
        if (GRD.Rows.Count > 0)
        {
            for (int i = 0; i < GRD.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GRD.Rows[i].FindControl("chk");
                Label lblPID = (Label)GRD.Rows[i].FindControl("lblPID");
                chk.Checked = true;
                GridView GRDInner = (GridView)GRD.Rows[i].FindControl("GRDInner");
                if (GRDInner.Rows.Count > 0)
                {
                    for (int j = 0; j < GRDInner.Rows.Count; j++)
                    {
                        Label lblPID1 = (Label)GRDInner.Rows[j].FindControl("lblPID1");
                        CheckBox chk1 = (CheckBox)GRDInner.Rows[j].FindControl("chk1");
                        //CheckBox chkAdd = (CheckBox)GRDInner.Rows[j].FindControl("chkAdd");
                        //CheckBox chkView = (CheckBox)GRDInner.Rows[j].FindControl("chkView");
                        //CheckBox chkEdit = (CheckBox)GRDInner.Rows[j].FindControl("chkEdit");
                        //CheckBox chkDelete = (CheckBox)GRDInner.Rows[j].FindControl("chkDelete");
                        chk1.Checked = true;
                        CheckBoxList ChkPages = (CheckBoxList)GRDInner.Rows[j].FindControl("ChkPages");
                        //chkAdd.Checked = true;
                        //chkView.Checked = true;
                        //chkEdit.Checked = true;
                        //chkDelete.Checked = true;
                    }

                }
            }
        }
    }
    bool p = false; bool a = false; bool e = false; bool d = false; bool v = false; bool r = false; bool l = false,h=false;
    public void insertRoles(string userid)
    {
        data.executeCommand("delete from tbl_EmpRoles where UserId=" + userid + "");
        if (GRD.Rows.Count > 0)
        {
            for (int i = 0; i < GRD.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GRD.Rows[i].FindControl("chk");
                if (chk.Checked == true)
                {
                    Label lblPID = (Label)GRD.Rows[i].FindControl("lblPID");
                    data.executeCommand("insert into tbl_EmpRoles (UserId, PageID, AddStatus, EditStatus, DeleteStatus,ViewP,AssignStatus,LoginStatus,HrStatus) values(" + userid + "," + lblPID.Text + ",'1','1','1','1','1','1','1')");
                    GridView GRDInner = (GridView)GRD.Rows[i].FindControl("GRDInner");
                    if (GRDInner.Rows.Count > 0)
                    {
                        for (int j = 0; j < GRDInner.Rows.Count; j++)
                        {
                            CheckBox chk1 = (CheckBox)GRDInner.Rows[j].FindControl("chk1");
                            CheckBox chkAdd = (CheckBox)GRDInner.Rows[j].FindControl("chkAdd");
                            CheckBox chkView = (CheckBox)GRDInner.Rows[j].FindControl("chkView");
                            CheckBox chkEdit = (CheckBox)GRDInner.Rows[j].FindControl("chkEdit");
                            CheckBox chkDelete = (CheckBox)GRDInner.Rows[j].FindControl("chkDelete");
                            CheckBox chkAssign = (CheckBox)GRDInner.Rows[j].FindControl("chkAssign");
                            CheckBox chkLogin = (CheckBox)GRDInner.Rows[j].FindControl("chkLogin");
                            CheckBox chkHr = (CheckBox)GRDInner.Rows[j].FindControl("chkHr");
                            p = Convert.ToBoolean(chk1.Checked);
                            a = Convert.ToBoolean(chkAdd.Checked);
                            e = Convert.ToBoolean(chkEdit.Checked);
                            d = Convert.ToBoolean(chkDelete.Checked);
                            v = Convert.ToBoolean(chkView.Checked);
                            r = Convert.ToBoolean(chkAssign.Checked);
                            l = Convert.ToBoolean(chkLogin.Checked);
                            h = Convert.ToBoolean(chkHr.Checked);
                            Label lblPID1 = (Label)GRDInner.Rows[j].FindControl("lblPID1");
                            if (p == true)
                            {
                                data.executeCommand("insert into tbl_EmpRoles (UserId, PageID, AddStatus, EditStatus, DeleteStatus,ViewP,AssignStatus,LoginStatus,hrStatus) values(" + userid + "," + lblPID1.Text + ",'" + a + "','" + e + "','" + d + "','" + v + "','" + r + "','" + l + "','" + h + "')");
                            }

                        }
                    }
                }
            }
        }
    }

    protected void btnSaveExit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            insertRoles(Request.QueryString["id"].ToString());
        }

        Response.Redirect("UserAssignReport.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserAssignReport.aspx");
    }

    protected void GRDInner_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HiddenField hddmenupage = (HiddenField)e.Row.FindControl("hddmenupage");
        CheckBox chkAssign = (CheckBox)e.Row.FindControl("chkAssign");
        Label lblPIDAss = (Label)e.Row.FindControl("lblPIDAss");
        CheckBox chkLogin = (CheckBox)e.Row.FindControl("chkLogin");
        Label lblPIDLgn = (Label)e.Row.FindControl("lblPIDLgn");
        Label lblPIDAdd = (Label)e.Row.FindControl("lblPIDAdd");
        CheckBox chkAdd = (CheckBox)e.Row.FindControl("chkAdd");
        Label lblPIDView = (Label)e.Row.FindControl("lblPIDView");
        CheckBox chkView = (CheckBox)e.Row.FindControl("chkView");
        Label lblPIDEdit = (Label)e.Row.FindControl("lblPIDEdit");
        CheckBox chkEdit = (CheckBox)e.Row.FindControl("chkEdit");
        Label lblPIDDel = (Label)e.Row.FindControl("lblPIDDel");
        CheckBox chkDelete = (CheckBox)e.Row.FindControl("chkDelete");
        Label lblPIHr = (Label)e.Row.FindControl("lblPIHr");
        CheckBox chkHr = (CheckBox)e.Row.FindControl("chkHr");

        if (hddmenupage != null)
        {
            DataTable dtble = data.getDataSet("select * from tbl_Menu where MenuName='" + hddmenupage.Value + "' and isdelete = 0  order by Menuid").Tables[0];

            chkAssign.Visible = Convert.ToBoolean(dtble.Rows[0]["IsRights"]);
            lblPIDAss.Visible = Convert.ToBoolean(dtble.Rows[0]["IsRights"]);
            chkLogin.Visible = Convert.ToBoolean(dtble.Rows[0]["IsLogin"]);
            lblPIDLgn.Visible = Convert.ToBoolean(dtble.Rows[0]["IsLogin"]);
            chkAdd.Visible = Convert.ToBoolean(dtble.Rows[0]["IsAdd"]);
            lblPIDAdd.Visible = Convert.ToBoolean(dtble.Rows[0]["IsAdd"]);
            chkView.Visible = Convert.ToBoolean(dtble.Rows[0]["IsPrint"]);
            lblPIDView.Visible = Convert.ToBoolean(dtble.Rows[0]["IsPrint"]);
            chkEdit.Visible = Convert.ToBoolean(dtble.Rows[0]["IsEdit"]);
            lblPIDEdit.Visible = Convert.ToBoolean(dtble.Rows[0]["IsEdit"]);
            chkDelete.Visible = Convert.ToBoolean(dtble.Rows[0]["IsDel"]);
            lblPIDDel.Visible = Convert.ToBoolean(dtble.Rows[0]["IsDel"]);
            chkHr.Visible = Convert.ToBoolean(dtble.Rows[0]["IsHr"]);
            lblPIHr.Visible = Convert.ToBoolean(dtble.Rows[0]["IsHr"]); 
        }
    }
}