using PerformanceAppraisalProj.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.BL.Manage
{
    public class HREmployee_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HREmployee_Manage));
        public HREmployee GetHREmployeeData(string EmployeeID)
        {
            HREmployee retDet = new HREmployee();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var getData = (from r in entity.HREmployees
                                   select r).Where(t => t.EmployeeID == EmployeeID).FirstOrDefault();
                    retDet = getData;
                }
            }
            catch (Exception ex)
            {
                retDet = null;
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return retDet;
        }
    }
}