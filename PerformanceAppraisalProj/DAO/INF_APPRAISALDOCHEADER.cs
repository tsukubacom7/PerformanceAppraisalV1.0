using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.DAO 
{
    [Serializable()]
    public class INF_APPRAISALDOCHEADER
    {
      public string AppraisalDocNo { get; set; }
      public int? AppraisalYear { get; set; }
      public DateTime AppraisalPeriodFrom { get; set; }
      public DateTime AppraisalPeriodTo { get; set; }
      public DateTime AppraisalDate { get; set; }
      public string EmployeeName { get; set; }
      public string Position { get; set; }
      public string DepartmentName { get; set; }
      public string CompanyID { get; set; }
      public string EmployeeStrength { get; set; }
      public string EmployeeImprovement { get; set; }
      public DateTime CreatedDate { get; set; }
      public string CreatedBy { get; set; }
      public string AppraisalStatus { get; set; }
      public string AppraisalGrade { get; set; }
      public decimal? AppraisalTotalScore { get; set; }
      public DateTime StartDate { get; set; }
    }
}