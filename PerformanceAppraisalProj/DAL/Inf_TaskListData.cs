using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.DAL
{
    public class Inf_TaskListData
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string CompanyID { get; set; }
        public string Position { get; set; }
        public string FirstManager { get; set; }
        public string SecondManager { get; set; }
        public string HRStaff { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public string AppraisalDocNo { get; set; }
        public string TaskOfUser { get; set; }
        public int? AppraisalYear { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Responsibility { get; set; }
        public string AppraisalStatus { get; set; }
    }
}