using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.DAO 
{
    [Serializable()]
    public class INF_APPROVALHISTORY
    {
      public Int64? RowID { get; set; }
      public DateTime TransactionDate { get; set; }
      public string UserID { get; set; }
      public string Position { get; set; }
      public string Action { get; set; }
      public string Comment { get; set; }
      public string AppraisalDocNo { get; set; }
    }
}