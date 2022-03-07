namespace DbdStoredProcedures.API.Models;

public class Department
{
    public string DName { get; set; }
    public int DNumber { get; set; }
    public int MgrSSN { get; set; }
    public DateTime MgrStartDate { get; set; }
    public int EmpCount { get; set; }

    public Department(string dName, int dNumber, int mgrSsn, DateTime mgrStartDate, int empCount)
    {
        DName = dName;
        DNumber = dNumber;
        MgrSSN = mgrSsn;
        MgrStartDate = mgrStartDate;
        EmpCount = empCount;
    }

    public Department(string dName, int dNumber, int mgrSsn, DateTime mgrStartDate)
    {
        DName = dName;
        DNumber = dNumber;
        MgrSSN = mgrSsn;
        MgrStartDate = mgrStartDate;
    }

    public Department(string dName, int mgrSsn)
    {
        DName = dName;
        MgrSSN = mgrSsn;
    }
}