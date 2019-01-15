using PerformanceAppraisalProj.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.BL.Manage
{
    public class DepartmentMaster_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(DepartmentMaster_Manage));
        public DepartmentMaster GetDepartmentData(string DepartmentName)
        {
            DepartmentMaster retDet = new DepartmentMaster();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var getData = (from r in entity.DepartmentMasters select r).
                        Where(t => t.DepartmentName == DepartmentName).FirstOrDefault();
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


        public List<DepartmentMaster> ListDepartmentData()
        {
            List<DepartmentMaster> lResult = new List<DepartmentMaster>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var listData = (from r in entity.DepartmentMasters select r).ToList();
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