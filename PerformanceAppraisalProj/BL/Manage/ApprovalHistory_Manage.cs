using PerformanceAppraisalProj.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.BL.Manage
{
    public class ApprovalHistory_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ApprovalHistory_Manage));
        public ApprovalHistory GetApprovalHistoryData(string AppraisalDocNo)
        {
            ApprovalHistory retDet = new ApprovalHistory();
            try
            {      
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var getData = (from r in entity.ApprovalHistories select r).Where(t => t.AppraisalDocNo == AppraisalDocNo).FirstOrDefault();
                    retDet = getData;                  
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return retDet;
        }

        public List<ApprovalHistory> ListApprovalHistory(string AppraisalDocNo)
        {
            List<ApprovalHistory> lResult = new List<ApprovalHistory>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var listData = (from r in entity.ApprovalHistories select r).Where(c => c.AppraisalDocNo == AppraisalDocNo).ToList();
                    foreach (var item in listData)
                    {
                        lResult.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return lResult;
        }
    }
}