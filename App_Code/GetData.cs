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
using Org.BouncyCastle.Asn1.X509;

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

    public void FillCompany(DropDownList DrpCompanies)
    {
        string query = "Select *   FROM  [Company]";
        ds = data.getDataSet(query);
        DrpCompanies.DataSource = ds;
        DrpCompanies.DataTextField = "COname";
        DrpCompanies.DataValueField = "COCode";
        DrpCompanies.DataBind();
        DrpCompanies.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillEmployee(DropDownList drpEmployee)
    {
        DataSet dsusr = getdata.getHqtrUserDpt("0");
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
    public void FillPartyCategory(DropDownList drp)
    {
        query = "Select MsNo,MsName from MastByNo Where MsSr='PTC' Order By Msname ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "MsName";
        drp.DataValueField = "MsNo";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillPrimaryStation(DropDownList drp, string DistrictNo = "0")
    {
        query = "select  Station,StationNo from  [Station] WHERE IsActive=1 ";
        if (DistrictNo != "0")
            query += " and DistrictNo=" + DistrictNo;
        query += " order by Station ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Station";
        drp.DataValueField = "StationNo";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillPrimaryParty(DropDownList drp)
    {


        query = "select AcName + '(' + AcStation + ')' AS AcName, AcCode from  ACCOUNT order by ACNAME ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "ACNAME";
        drp.DataValueField = "ACCOde";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillDistrict(DropDownList drpdistrict, string HeadQtr)
    {
        query = "select distinct District,DistrictNO from  [HeadQtrWiseDistrict]  WHERE 0=0";
        if (HeadQtr != "0")
            query += " and HeadQtrNo=" + HeadQtr;
        query += " order by District ";
        ds = data.getDataSet(query);
        drpdistrict.DataSource = ds;
        drpdistrict.DataTextField = "District";
        drpdistrict.DataValueField = "DistrictNO";
        drpdistrict.DataBind();
        drpdistrict.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillHeadQtr(DropDownList drpHEADQTR)
    {
        query = "select distinct HeadQtrNo,HEADQTR from  [HeadQtrWiseDistrict]  order by HEADQTR ";
        ds = data.getDataSet(query);
        drpHEADQTR.DataSource = ds;
        drpHEADQTR.DataTextField = "HEADQTR";
        drpHEADQTR.DataValueField = "HeadQtrNo";
        drpHEADQTR.DataBind();
        drpHEADQTR.Items.Insert(0, new ListItem("Select", "0"));
    }



    public void FillStation(DropDownList drp)
    {
        query = "select * from [STATION] order by Station ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Station";
        drp.DataValueField = "StationNo";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void FillUser1(DropDownList drp)
    {
        query = "select * from [CSInfo].[dbo].[MobileAppUser] where Deactivate=0 order by Name ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Name";
        drp.DataValueField = "ID";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }


    public void FillUser(DropDownList drp)
    {
        query = "select * from [tbl_EmpMaster] where Delid=0 order by Emp_Name ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Emp_Name";
        drp.DataValueField = "EmpId";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void FillGroup(DropDownList drp)
    {
        query = "SELECT [CMsCode], [CMsName]   FROM [CMaster] Where CMsSr='I'   Order By CMSName";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "CMsName";
        drp.DataValueField = "CMsCode";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void FillGroup(ListBox drp)
    {
        query = "SELECT [CMsCode], [CMsName]   FROM  [CMaster] Where CMsSr='I'   Order By CMSName";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "CMsName";
        drp.DataValueField = "CMsCode";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillGroup1(ListBox drp)
    {
        query = "SELECT [CMsCode], [CMsName]   FROM  [CMaster] Where CMsSr='I'  AND CMsValue1='y'   Order By CMSName";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "CMsName";
        drp.DataValueField = "CMsCode";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillMainGroup(ListBox drp)
    {
        query = "SELECT [CMsCode], [CMsName]   FROM  [CMaster] Where CMsSr='I'  AND MCMsCode='' and CMsValue1='y'    Order By CMSName";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "CMsName";
        drp.DataValueField = "CMsCode";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillMainGroup(DropDownList drp)
    {
        query = "SELECT [CMsCode], [CMsName]   FROM  [CMaster] Where CMsSr='I'  AND MCMsCode='' and CMsValue1='y'    Order By CMSName";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "CMsName";
        drp.DataValueField = "CMsCode";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void FillItem(DropDownList drp)
    {
        query = "SELECT ITName  FROM  [ITEM] Order By ITName";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "ITName";
        drp.DataValueField = "ITName";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillHeadQt(DropDownList drp)
    {
        query = "SELECT [CMsCode], [CMsName]   FROM  Where CMsSr='I'   Order By CMSName";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "CMsName";
        drp.DataValueField = "CMsCode";
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

    public void fillDepartment(DropDownList drpDepartment)
    {
        ds = getdata.GetDepartment("Select", "0", "", "", "");
        drpDepartment.DataSource = ds;
        drpDepartment.DataTextField = "Dept_Name";
        drpDepartment.DataValueField = "dept_Id";
        drpDepartment.DataBind();
        drpDepartment.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void fillDocument(DropDownList drpDocument)
    {
        ds = getdata.GetDocument("Select", "0", "", "");
        drpDocument.DataSource = ds;
        drpDocument.DataTextField = "Docu_Name";
        drpDocument.DataValueField = "Docu_Id";
        drpDocument.DataBind();
        drpDocument.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void fillDesignation(DropDownList drpDesignation, string Dept_Id)
    {
        ds = getdata.GetDesignation("Select", "0", Dept_Id, "", "");
        drpDesignation.DataSource = ds;
        drpDesignation.DataTextField = "DESG_NAME";
        drpDesignation.DataValueField = "DESG_ID";
        drpDesignation.DataBind();
        drpDesignation.Items.Insert(0, new ListItem("Select", "0"));
    }


    public void FillUser(DropDownList drp, string De_Id, string status = "Active", string Desg_Id = "0")
    {
        query = "select * from [tbl_EmpMaster] where Delid=0   ";
        if (De_Id != "0")
        {
            query += " and Dept_Id=" + De_Id;
        }
        if (status != "ALL")
        {
            query += " and Status='" + status + "'";
        }
        if (Desg_Id != "0")
        {
            query += " and Desig_Id='" + Desg_Id + "'";
        }
        query += " order by Emp_Name ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Emp_Name";
        drp.DataValueField = "EmpId";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void FillCRMUser(DropDownList drp, string De_Id, string status)
    {
        query = "select * from [tbl_EmpMaster] where Delid=0 ";
        if (status != "ALL")
        {
            query += " and Status='" + status + "'";
        }
        if (De_Id != "0")
        {
            query += " and Dept_Id=" + De_Id;
        }
        query += " order by Emp_Name ";
        ds = data.getDataSet(query);
        drp.DataSource = ds;
        drp.DataTextField = "Emp_Name";
        drp.DataValueField = "crmuserid";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }



    public DataSet FillHeadQtrDistrict()
    {
        string query = "SELECT distinct [HeadQtr],[District],DistrictNo,HeadQtrNo FROM [HeadQtrWiseDistrict] order by HeadQtr";
        return data.getDataSet(query);
    }

    public DataSet FillStation()
    {
        string query = "SELECT * FROM [GETSTATIONLIST] order by station";
        return data.getDataSet(query);
    }

    public DataSet FillStationBeat()
    {
        string query = "SELECT * FROM [stationBeat] WHere isDelete=0 order by station";
        return data.getDataSet(query);
    }

    public void FillData(DropDownList drp, DataTable ds, string TextField, string ValueField)
    {
        drp.DataSource = ds;
        drp.DataTextField = TextField;
        drp.DataValueField = ValueField;
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void FillData(ListBox drp, DataTable ds, string TextField, string ValueField)
    {
        drp.DataSource = ds;
        drp.DataTextField = TextField;
        drp.DataValueField = ValueField;
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("Select", "0"));
    }
}