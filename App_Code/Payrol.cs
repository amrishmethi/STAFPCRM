using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Payroll
/// </summary>
public class Payrol
{
    DataSet ds = new DataSet();
    Data data = new Data();
    SqlCommand cmd = new SqlCommand();
    public Payrol()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public DataSet Emp_Main(string ACTION, string EMPID, string COMP_ID, string EMP_CODE, string EMP_NAME, string DEPT_ID, string DESIG_ID, string REP_MANAGER, string DOJ, string DOL, string PANNO, string PF_ACNO, string ESI_ACNO, string STATUS, string EMPNO, string CRMUSERID, string CREATEUSER, string ATTANDANCEBY)
    {
        cmd = new SqlCommand("IU_EMPMASTER");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@EMPID", EMPID);
        cmd.Parameters.AddWithValue("@COMP_ID", COMP_ID);
        cmd.Parameters.AddWithValue("@EMP_CODE", EMP_CODE);
        cmd.Parameters.AddWithValue("@EMP_NAME", EMP_NAME);
        cmd.Parameters.AddWithValue("@DEPT_ID", DEPT_ID);
        cmd.Parameters.AddWithValue("@DESIG_ID", DESIG_ID);
        cmd.Parameters.AddWithValue("@REP_MANAGER", REP_MANAGER);
        cmd.Parameters.AddWithValue("@DOJ", (DOJ == "") ? DBNull.Value.ToString() : data.YYYYMMDD(DOJ));
        cmd.Parameters.AddWithValue("@DOL", (DOL == "") ? DBNull.Value.ToString() : data.YYYYMMDD(DOL));
        cmd.Parameters.AddWithValue("@PANNO", PANNO);
        cmd.Parameters.AddWithValue("@PF_ACNO", PF_ACNO);
        cmd.Parameters.AddWithValue("@ESI_ACNO", ESI_ACNO);
        cmd.Parameters.AddWithValue("@STATUS", STATUS);
        cmd.Parameters.AddWithValue("@EMPNO", EMPNO);
        cmd.Parameters.AddWithValue("@CRMUSERID", CRMUSERID);
        cmd.Parameters.AddWithValue("@CREATEUSER", CREATEUSER);
        cmd.Parameters.AddWithValue("@ATTANDANCEBY", ATTANDANCEBY);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet Emp_Bank(string ACTION, string EMP_ID, string CREATEUSER, string BANK_NAME, string ACNO, string BRANCH, string IFSC, string BANK_NAME2, string ACNO2, string BRANCH2, string IFSC2)
    {
        cmd = new SqlCommand("IU_EMPMASTER_BANK");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@EMP_ID", EMP_ID);
        cmd.Parameters.AddWithValue("@CREATEUSER", CREATEUSER);
        cmd.Parameters.AddWithValue("@BANK_NAME", BANK_NAME);
        cmd.Parameters.AddWithValue("@ACNO", ACNO);
        cmd.Parameters.AddWithValue("@BRANCH", BRANCH);
        cmd.Parameters.AddWithValue("@IFSC", IFSC);
        cmd.Parameters.AddWithValue("@BANK_NAME2", BANK_NAME2);
        cmd.Parameters.AddWithValue("@ACNO2", ACNO2);
        cmd.Parameters.AddWithValue("@BRANCH2", BRANCH2);
        cmd.Parameters.AddWithValue("@IFSC2", IFSC2);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet Emp_PERSONAL(string ACTION, string EMP_ID, string CREATEUSER, string GENDER, string DOB, string MARRITAL_STATUS, string QUALIFICATION, string DOM, string METARNITIY_LEAVE, string PHONE_NO, string MOBILE_NO, string CUG_MOBILENO, string PERSONAL_MAIL, string OFFICE_MAIL, string PRESENT_ADD, string PERMANENT_ADD, string ISADDSAME)
    {
        cmd = new SqlCommand("IU_EMPMASTER_PERSONAL");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@EMP_ID", EMP_ID);
        cmd.Parameters.AddWithValue("@CREATEUSER", CREATEUSER);
        cmd.Parameters.AddWithValue("@GENDER", GENDER);
        cmd.Parameters.AddWithValue("@DOB", (DOB == "") ? DBNull.Value.ToString() : data.YYYYMMDD(DOB));
        cmd.Parameters.AddWithValue("@MARRITAL_STATUS", MARRITAL_STATUS);
        cmd.Parameters.AddWithValue("@QUALIFICATION", QUALIFICATION);
        cmd.Parameters.AddWithValue("@DOM", (DOM == "") ? DBNull.Value.ToString() : data.YYYYMMDD(DOM));
        cmd.Parameters.AddWithValue("@METARNITIY_LEAVE", METARNITIY_LEAVE);
        cmd.Parameters.AddWithValue("@PHONE_NO", PHONE_NO);
        cmd.Parameters.AddWithValue("@MOBILE_NO", MOBILE_NO);
        cmd.Parameters.AddWithValue("@CUG_MOBILENO", CUG_MOBILENO);
        cmd.Parameters.AddWithValue("@PERSONAL_MAIL", PERSONAL_MAIL);
        cmd.Parameters.AddWithValue("@OFFICE_MAIL", OFFICE_MAIL);
        cmd.Parameters.AddWithValue("@PRESENT_ADD", PRESENT_ADD);
        cmd.Parameters.AddWithValue("@PERMANENT_ADD", PERMANENT_ADD);
        cmd.Parameters.AddWithValue("@ISADDSAME", ISADDSAME);
        ds = data.getDataSet(cmd);
        return ds;
    }


    public DataSet Emp_Family(string ACTION, string EMP_ID, string CREATEUSER, string FATHER_NAME, string MOTHER_NAME, string SPOUSE, string SPOUSE_NAME, string NOOFCHILD, string CONTACTNO)
    {
        cmd = new SqlCommand("IU_EMPMASTER_FAMILY");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@EMP_ID", EMP_ID);
        cmd.Parameters.AddWithValue("@CREATEUSER", CREATEUSER);
        cmd.Parameters.AddWithValue("@FATHER_NAME", FATHER_NAME);
        cmd.Parameters.AddWithValue("@MOTHER_NAME", MOTHER_NAME);
        cmd.Parameters.AddWithValue("@SPOUSE", SPOUSE);
        cmd.Parameters.AddWithValue("@SPOUSE_NAME", SPOUSE_NAME);
        cmd.Parameters.AddWithValue("@NOOFCHILD", NOOFCHILD);
        cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet Emp_PrevComp(string ACTION, string EMP_ID, string CREATEUSER, string COMP_NAME, string COMP_ADD, string LAST_CTC, string PFACNO, string REF_NAME, string REF_CONTACT, string REF_NAME2, string REF_CONTACT2)
    {
        cmd = new SqlCommand("IU_EMPMASTER_PREVCOMP");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@EMP_ID", EMP_ID);
        cmd.Parameters.AddWithValue("@CREATEUSER", CREATEUSER);
        cmd.Parameters.AddWithValue("@COMP_NAME", COMP_NAME);
        cmd.Parameters.AddWithValue("@COMP_ADD", COMP_ADD);
        cmd.Parameters.AddWithValue("@LAST_CTC", LAST_CTC);
        cmd.Parameters.AddWithValue("@PFACNO", PFACNO);
        cmd.Parameters.AddWithValue("@REF_NAME", REF_NAME);
        cmd.Parameters.AddWithValue("@REF_CONTACT", REF_CONTACT);
        cmd.Parameters.AddWithValue("@REF_NAME2", REF_NAME2);
        cmd.Parameters.AddWithValue("@REF_CONTACT2", REF_CONTACT2);
        ds = data.getDataSet(cmd);
        return ds;
    }

    public DataSet Emp_Salary(string ACTION, string EMP_ID, string CREATEUSER, string NET_SALARY, string IS_BASICSALARY, string IS_BASICFIXED, string IS_BASICPER, string BASIC_SALARY, string BASIC_SALARYVALUE, string IS_PF, string IS_PFFIXED, string IS_PFPER, string PF_EMPLOYEE, string PF_EMPLOYEEVALUE, string PF_EMPLOYER, string PF_EMPLOYERVALUE, string IS_ESIC, string IS_ESICFIXED, string IS_ESICPPER, string ESIC_EMPLOYEE, string ESIC_EMPLOYEEVALUE, string ESIC_EMPLOYER, string ESIC_EMPLOYERVALUE, string IS_HRA, string IS_HRAFIXED, string IS_HRAPER, string HRA, string HRAVALUE, string IS_WA, string IS_WAFIXED, string IS_WAPER, string WA, string WAVALUE, string IS_MA, string IS_MAFIXED, string IS_MAPER, string MA, string MAVALUE, string IS_CA, string IS_CAFIXED, string IS_CAPER, string CA, string CAVALUE, string IS_DAL, string DAL, string IS_DAEX, string DAEX, string IS_LA, string IS_LAFIXED, string IS_LAPER, string LA, string LAVALUE, string IS_FA, string IS_FAFIXED, string IS_FAPER, string FA, string FAVALUE, string IS_OA, string IS_OAFIXED, string IS_OAPER, string OA, string OAVALUE, string IS_NSA, string NSA, string IS_TDS, string IS_TDSFIXED, string IS_TDSPER, string TDS, string TDSVALUE, string IS_OD, string IS_ODFIXED, string IS_ODPER, string OD, string ODVALUE, string IS_PL, string PL, string IS_CL, string CL, string IS_LCI, string LCI, string IS_ECO, string ECO, string IS_YB, string YB, string IS_WH, string WH, string IS_OTPD, string OTPD, string IS_OTPH, string OTPH, string IS_CIT, string CIT, string WTF, string WTT, string WD, string CTC, string IN_HAND, string NSA_KM, string NoOfSunday, string ESICApplicable)
    {
        cmd = new SqlCommand("IU_EMPMASTER_SALARY");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        cmd.Parameters.AddWithValue("@EMP_ID", EMP_ID);
        cmd.Parameters.AddWithValue("@CREATEUSER", CREATEUSER);
        cmd.Parameters.AddWithValue("@NET_SALARY", (NET_SALARY == "") ? "0" : NET_SALARY);
        cmd.Parameters.AddWithValue("@IS_BASICSALARY", IS_BASICSALARY);
        cmd.Parameters.AddWithValue("@IS_BASICFIXED", IS_BASICFIXED);
        cmd.Parameters.AddWithValue("@IS_BASICPER", IS_BASICPER);
        cmd.Parameters.AddWithValue("@BASIC_SALARY", (BASIC_SALARY == "") ? "0" : BASIC_SALARY);
        cmd.Parameters.AddWithValue("@BASIC_SALARYVALUE", (BASIC_SALARYVALUE == "") ? "0" : BASIC_SALARYVALUE);
        cmd.Parameters.AddWithValue("@IS_PF", IS_PF);
        cmd.Parameters.AddWithValue("@IS_PFFIXED", IS_PFFIXED);
        cmd.Parameters.AddWithValue("@IS_PFPER", IS_PFPER);
        cmd.Parameters.AddWithValue("@PF_EMPLOYEE", (PF_EMPLOYEE == "") ? "0" : PF_EMPLOYEE);
        cmd.Parameters.AddWithValue("@PF_EMPLOYEEVALUE", (PF_EMPLOYEEVALUE == "") ? "0" : PF_EMPLOYEEVALUE);
        cmd.Parameters.AddWithValue("@PF_EMPLOYER", (PF_EMPLOYER == "") ? "0" : PF_EMPLOYER);
        cmd.Parameters.AddWithValue("@PF_EMPLOYERVALUE", (PF_EMPLOYERVALUE == "") ? "0" : PF_EMPLOYERVALUE);
        cmd.Parameters.AddWithValue("@IS_ESIC", IS_ESIC);
        cmd.Parameters.AddWithValue("@IS_ESICFIXED", IS_ESICFIXED);
        cmd.Parameters.AddWithValue("@IS_ESICPPER", IS_ESICPPER);
        cmd.Parameters.AddWithValue("@ESIC_EMPLOYEE", (ESIC_EMPLOYEE == "") ? "0" : ESIC_EMPLOYEE);
        cmd.Parameters.AddWithValue("@ESIC_EMPLOYEEVALUE", (ESIC_EMPLOYEEVALUE == "") ? "0" : ESIC_EMPLOYEEVALUE);
        cmd.Parameters.AddWithValue("@ESIC_EMPLOYER", (ESIC_EMPLOYER == "") ? "0" : ESIC_EMPLOYER);
        cmd.Parameters.AddWithValue("@ESIC_EMPLOYERVALUE", (ESIC_EMPLOYERVALUE == "") ? "0" : ESIC_EMPLOYERVALUE);
        cmd.Parameters.AddWithValue("@IS_HRA", IS_HRA);
        cmd.Parameters.AddWithValue("@IS_HRAFIXED", IS_HRAFIXED);
        cmd.Parameters.AddWithValue("@IS_HRAPER", IS_HRAPER);
        cmd.Parameters.AddWithValue("@HRA", (HRA == "") ? "0" : HRA);
        cmd.Parameters.AddWithValue("@HRAVALUE", (HRAVALUE == "") ? "0" : HRAVALUE);
        cmd.Parameters.AddWithValue("@IS_WA", IS_WA);
        cmd.Parameters.AddWithValue("@IS_WAFIXED", IS_WAFIXED);
        cmd.Parameters.AddWithValue("@IS_WAPER", IS_WAPER);
        cmd.Parameters.AddWithValue("@WA", (WA == "") ? "0" : WA);
        cmd.Parameters.AddWithValue("@WAVALUE", (WAVALUE == "") ? "0" : WAVALUE);
        cmd.Parameters.AddWithValue("@IS_MA", IS_MA);
        cmd.Parameters.AddWithValue("@IS_MAFIXED", IS_MAFIXED);
        cmd.Parameters.AddWithValue("@IS_MAPER", IS_MAPER);
        cmd.Parameters.AddWithValue("@MA", (MA == "") ? "0" : MA);
        cmd.Parameters.AddWithValue("@MAVALUE", (MAVALUE == "") ? "0" : MAVALUE);
        cmd.Parameters.AddWithValue("@IS_CA", IS_CA);
        cmd.Parameters.AddWithValue("@IS_CAFIXED", IS_CAFIXED);
        cmd.Parameters.AddWithValue("@IS_CAPER", IS_CAPER);
        cmd.Parameters.AddWithValue("@CA", (CA == "") ? "0" : CA);
        cmd.Parameters.AddWithValue("@CAVALUE", (CAVALUE == "") ? "0" : CAVALUE);
        cmd.Parameters.AddWithValue("@IS_DAL", IS_DAL);
        cmd.Parameters.AddWithValue("@DAL", (DAL == "") ? "0" : DAL);
        cmd.Parameters.AddWithValue("@IS_DAEX", IS_DAEX);
        cmd.Parameters.AddWithValue("@DAEX", (DAEX == "") ? "0" : DAEX);
        cmd.Parameters.AddWithValue("@IS_LA", IS_LA);
        cmd.Parameters.AddWithValue("@IS_LAFIXED", IS_LAFIXED);
        cmd.Parameters.AddWithValue("@IS_LAPER", IS_LAPER);
        cmd.Parameters.AddWithValue("@LA", (LA == "") ? "0" : LA);
        cmd.Parameters.AddWithValue("@LAVALUE", (LAVALUE == "") ? "0" : LAVALUE);
        cmd.Parameters.AddWithValue("@IS_FA", IS_FA);
        cmd.Parameters.AddWithValue("@IS_FAFIXED", IS_FAFIXED);
        cmd.Parameters.AddWithValue("@IS_FAPER", IS_FAPER);
        cmd.Parameters.AddWithValue("@FA", (FA == "") ? "0" : FA);
        cmd.Parameters.AddWithValue("@FAVALUE", (FAVALUE == "") ? "0" : FAVALUE);
        cmd.Parameters.AddWithValue("@IS_OA", IS_OA);
        cmd.Parameters.AddWithValue("@IS_OAFIXED", IS_OAFIXED);
        cmd.Parameters.AddWithValue("@IS_OAPER", IS_OAPER);
        cmd.Parameters.AddWithValue("@OA", (OA == "") ? "0" : OA);
        cmd.Parameters.AddWithValue("@OAVALUE", (OAVALUE == "") ? "0" : OAVALUE);
        cmd.Parameters.AddWithValue("@IS_NSA", IS_NSA);
        cmd.Parameters.AddWithValue("@NSA", (NSA == "") ? "0" : NSA);
        cmd.Parameters.AddWithValue("@IS_TDS", IS_TDS);
        cmd.Parameters.AddWithValue("@IS_TDSFIXED", IS_TDSFIXED);
        cmd.Parameters.AddWithValue("@IS_TDSPER", IS_TDSPER);
        cmd.Parameters.AddWithValue("@TDS", (TDS == "") ? "0" : TDS);
        cmd.Parameters.AddWithValue("@TDSVALUE", (TDSVALUE == "") ? "0" : TDSVALUE);
        cmd.Parameters.AddWithValue("@IS_OD", IS_OD);
        cmd.Parameters.AddWithValue("@IS_ODFIXED", IS_ODFIXED);
        cmd.Parameters.AddWithValue("@IS_ODPER", IS_ODPER);
        cmd.Parameters.AddWithValue("@OD", (OD == "") ? "0" : OD);
        cmd.Parameters.AddWithValue("@ODVALUE", (ODVALUE == "") ? "0" : ODVALUE);
        cmd.Parameters.AddWithValue("@IS_PL", IS_PL);
        cmd.Parameters.AddWithValue("@PL", (PL == "") ? "0" : PL);
        cmd.Parameters.AddWithValue("@IS_CL", IS_CL);
        cmd.Parameters.AddWithValue("@CL", (CL == "") ? "0" : CL);
        cmd.Parameters.AddWithValue("@IS_LCI", IS_LCI);
        cmd.Parameters.AddWithValue("@LCI", (LCI == "") ? "0" : LCI);
        cmd.Parameters.AddWithValue("@IS_ECO", IS_ECO);
        cmd.Parameters.AddWithValue("@ECO", (ECO == "") ? "0" : ECO);
        cmd.Parameters.AddWithValue("@IS_YB", IS_YB);
        cmd.Parameters.AddWithValue("@YB", (YB == "") ? "0" : YB);
        cmd.Parameters.AddWithValue("@IS_WH", IS_WH);
        cmd.Parameters.AddWithValue("@WH", (WH == "") ? "0" : WH);
        cmd.Parameters.AddWithValue("@IS_OTPD", IS_OTPD);
        cmd.Parameters.AddWithValue("@OTPD", (OTPD == "") ? "0" : OTPD);
        cmd.Parameters.AddWithValue("@IS_OTPH", IS_OTPH);
        cmd.Parameters.AddWithValue("@OTPH", (OTPH == "") ? "0" : OTPH);
        cmd.Parameters.AddWithValue("@IS_CIT", IS_CIT);
        cmd.Parameters.AddWithValue("@CIT", CIT);
        cmd.Parameters.AddWithValue("@WTF", (WTF == "") ? DBNull.Value.ToString() : WTF);
        cmd.Parameters.AddWithValue("@WTT", (WTT == "") ? DBNull.Value.ToString() : WTT);
        cmd.Parameters.AddWithValue("@WD", WD);
        cmd.Parameters.AddWithValue("@CTC", (CTC == "") ? "0" : CTC);
        cmd.Parameters.AddWithValue("@IN_HAND", (IN_HAND == "") ? "0" : IN_HAND);
        cmd.Parameters.AddWithValue("@NSA_KM", (NSA_KM == "") ? "0" : NSA_KM);
        cmd.Parameters.AddWithValue("@NoOfSunday", NoOfSunday);
        cmd.Parameters.AddWithValue("@ESICApplicable", ESICApplicable);
        ds = data.getDataSet(cmd);
        return ds;
    }


    public DataSet Emp_Document(string EMP_ID, string CREATEUSER, string DOC_ID, string DocFile_Name, string ID, string DELID)
    {
        cmd.CommandText = "IU_EMPMASTER_DOCUMENT";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@ID", ID);
        cmd.Parameters.AddWithValue("@EMP_ID", EMP_ID);
        cmd.Parameters.AddWithValue("@DOC_ID", DOC_ID);
        cmd.Parameters.AddWithValue("@DocFile_Name", DocFile_Name);
        cmd.Parameters.AddWithValue("@DELID", DELID);
        cmd.Parameters.AddWithValue("@CREATEUSER", CREATEUSER);
        ds = data.getDataSet(cmd);
        return ds;
    }
}