//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PerformanceAppraisalProj.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeMaster
    {
        public long RowID { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyID { get; set; }
        public Nullable<System.DateTime> ContractStart { get; set; }
        public Nullable<System.DateTime> ContractEnd { get; set; }
        public Nullable<System.DateTime> AppraisalDate { get; set; }
        public string FirstManager { get; set; }
        public string SecondManager { get; set; }
        public string HRStaff { get; set; }
        public string DepartmentName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Position { get; set; }
        public string EmployeeGroup { get; set; }
        public string EmployeeSubGroup { get; set; }
        public Nullable<int> ContractYear { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public string Remark { get; set; }
        public string FirstManagerMail { get; set; }
        public string SecondManagerMail { get; set; }
        public string AppraisalYear { get; set; }
        public string EmployeeType { get; set; }
    }
}
