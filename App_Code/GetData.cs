using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using System.Data.SqlClient;

/// <summary>
/// Summary description for GetData
/// </summary>
public class GetData
{
    Data data = new Data();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    string query = "";
    Master getdata = new Master();

    public GetData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void FillTypeOfConstruction(ListBox drptypeConst)
    {
        ds = data.getDataSet("select * from Tbl_TypeConst where IsDeleted=0 Order by TypeName");
        drptypeConst.DataSource = ds;
        drptypeConst.DataTextField = "TypeName";
        drptypeConst.DataValueField = "ID";
        drptypeConst.DataBind();
    } 

    public void FillRoomType(ListBox drptypeConst)
    {
        ds = data.getDataSet("select * from Tbl_RoomType where IsDeleted=0 Order by Name");
        drptypeConst.DataSource = ds;
        drptypeConst.DataTextField = "Name";
        drptypeConst.DataValueField = "ID";
        drptypeConst.DataBind();
    }

    public void FillCustomer(DropDownList drptypeConst,string uid)
    {
        ds = data.getDataSet("select * from tbl_Enquiry where IsDeleted=0 and UserId = iif("+uid+"=1,UserId,"+uid+") Order by Name");
        drptypeConst.DataSource = ds;
        drptypeConst.DataTextField = "Name";
        drptypeConst.DataValueField = "ID";
        drptypeConst.DataBind();
        
    }

    public void FillProduct(DropDownList drpName,string mid)
    {
        ds = data.getDataSet("select * from tbl_Product where IsDeleted=0 and MaterialID = '"+mid +"' Order by Name");
        drpName.DataSource = ds;
        drpName.DataTextField = "Name";
        drpName.DataValueField = "ID";
        drpName.DataBind();
        drpName.Items.Insert(0, new ListItem("Select Name", "0")); 
        
    }

    public void FillFinish(DropDownList drpFinish)
    {
        ds = data.getDataSet("select * from Tbl_MaterialType where IsDeleted=0  Order by Name");
        drpFinish.DataSource = ds;
        drpFinish.DataTextField = "Name";
        drpFinish.DataValueField = "ID";
        drpFinish.DataBind();
        drpFinish.Items.Insert(0, new ListItem("Select Finish", "0"));
    }

    public void FillUnit(DropDownList drp)
    {
        ds = data.getDataSet("select * from tbl_Unit where IsDeleted=0 Order by Name");
        drp.DataSource = ds;
        drp.DataTextField = "Name";
        drp.DataValueField = "ID";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select Unit", "0"));
    }

    public void FillColor(DropDownList drp)
    {
        ds = data.getDataSet("select * from tbl_MaterialColor where IsDeleted=0 Order by Name");
        drp.DataSource = ds;
        drp.DataTextField = "Name";
        drp.DataValueField = "ID";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select Color", "0"));
    }

    public void FillCompany(DropDownList DrpCompanies)
    {
        string query = "Select *   FROM [stm_company].[dbo].[Company]";
        ds = data.getDataSet(query);
        DrpCompanies.DataSource = ds;
        DrpCompanies.DataTextField = "COname";
        DrpCompanies.DataValueField = "COCode";
        DrpCompanies.DataBind();
        DrpCompanies.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillEmployee(DropDownList drpEmployee)
    {
        DataSet dsusr = getdata.getHqtrUser();
        drpEmployee.DataSource = dsusr.Tables[0].DefaultView.ToTable(true, "Name", "Mid");
        drpEmployee.DataTextField = "Name";
        drpEmployee.DataValueField = "Mid";
        drpEmployee.DataBind();
        drpEmployee.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillItemGroup(DropDownList drpItemGrup)
    {
        DataSet dsusr = data.getDataSet("usp_API_ITEMGROUP 930185018");
        drpItemGrup.DataSource = dsusr;
        drpItemGrup.DataTextField = "CmsName";
        drpItemGrup.DataValueField = "CmsName";
        drpItemGrup.DataBind();
        drpItemGrup.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillParty(DropDownList drp)
    {
        query = "select * from tbl_SecondarySalesParty where IsActive=1 order by Name ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Name";
        drp.DataValueField = "ID";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillPrimaryStation(DropDownList drp)
    {
        query = "select distinct Station from [STM_AcMast].[dbo].[Station] order by Station ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Station";
        drp.DataValueField = "Station";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillPrimaryParty(DropDownList drp)
    {
        query = "select * from STM_ACMAST.DBO.ACCOUNT order by ACNAME ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "ACNAME";
        drp.DataValueField = "ACCOde";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillStation(DropDownList drp)
    {
        query = "select * from [STM_ACMAST].[DBO].[STATION] order by Station ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Station";
        drp.DataValueField = "Station";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillUser(DropDownList drp)
    {
        query = "select * from [CSInfo].[dbo].[MobileAppUser] order by Name ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Name";
        drp.DataValueField = "ID";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillDrop(DropDownList drp, String tbl)
    {
        query = "select * from " + tbl + " where IsDeleted=0 order by Name ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Name";
        drp.DataValueField = "ID";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }
}