using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.DAO 
{
    [Serializable()]
    public class INF_APPRAISALDOCLINE
    {
        public Int64? RowID { get; set; }
        public string AppraisalDocNo { get; set; }
        public int? AppraisalDocLineNo { get; set; }
        public int? QuestionLineNo { get; set; }
        public string QuestionDesc { get; set; }
        public int? Score { get; set; }
        public decimal? CalculatedScore { get; set; }
        public decimal? QuestionWeight { get; set; }
        public string Remark { get; set; }
        public int? QuestionType { get; set; }

    }
}