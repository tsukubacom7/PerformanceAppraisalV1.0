using PerformanceAppraisalProj.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.BL.Manage
{
    public class AppraisalTemplate_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AppraisalTemplate_Manage));
        public List<AppraisalTemplate> ListData()
        {
            List<AppraisalTemplate> lResult = new List<AppraisalTemplate>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var listData = (from c in entity.AppraisalTemplates select c).ToList();
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