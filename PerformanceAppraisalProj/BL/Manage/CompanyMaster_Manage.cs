using PerformanceAppraisalProj.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.BL.Manage
{
    public class CompanyMaster_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CompanyMaster_Manage));
        public CompanyMaster GetCompanyData(string Company)
        {
            CompanyMaster retDet = new CompanyMaster();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var getData = (from r in entity.CompanyMasters select r).
                        Where(t => t.CompanyAD == Company && t.IsActive == true).FirstOrDefault();
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


        public List<CompanyMaster> ListCompanyData()
        {
            List<CompanyMaster> lResult = new List<CompanyMaster>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var listData = (from r in entity.CompanyMasters select r)
                        .Where(t => t.IsActive == true).ToList();

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