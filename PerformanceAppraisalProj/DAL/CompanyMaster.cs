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
    
    public partial class CompanyMaster
    {
        public long CompanyNo { get; set; }
        public string CompanyAD { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyFullName { get; set; }
        public string CompanyGroup { get; set; }
        public string CompanyType { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
