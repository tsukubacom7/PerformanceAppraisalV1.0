using PerformanceAppraisalProj.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.BL.Manage
{
    public class AppraisalDocLine_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AppraisalDocLine_Manage));
        public bool InsertData(List<AppraisalDocLine> lData)
        {
            bool ret = false;
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    using (var dbTrans = entity.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var item in lData)
                            {
                                entity.AppraisalDocLines.Add(item);
                                entity.SaveChanges();
                            }                        

                            dbTrans.Commit();

                            ret = true;
                        }
                        catch (Exception ex)
                        {
                            ret = false;
                            dbTrans.Rollback();

                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return ret;
        }


        public List<AppraisalDocLine> ListAppraisalDocLine(string AppraisalDocNo)
        {
            List<AppraisalDocLine> lResult = new List<AppraisalDocLine>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var listData = (from r in entity.AppraisalDocLines select r).Where(c => c.AppraisalDocNo == AppraisalDocNo).ToList();
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