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
    
    public partial class AppraisalTemplate
    {
        public long RowID { get; set; }
        public Nullable<int> AppraisalYear { get; set; }
        public Nullable<int> QuestionLineNo { get; set; }
        public string QuestionDesc { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> QuestionType { get; set; }
        public Nullable<decimal> QuestionWeight { get; set; }
    }
}
