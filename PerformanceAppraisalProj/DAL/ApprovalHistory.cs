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
    
    public partial class ApprovalHistory
    {
        public long RowID { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string UserID { get; set; }
        public string Action { get; set; }
        public string Comment { get; set; }
        public string AppraisalDocNo { get; set; }
    }
}