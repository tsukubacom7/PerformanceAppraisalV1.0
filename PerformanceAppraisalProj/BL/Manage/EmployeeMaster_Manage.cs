using PerformanceAppraisalProj.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.BL.Manage
{
    public class EmployeeMaster_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(EmployeeMaster_Manage));
        public EmployeeMaster GetData(string EmployeeID)
        {
            EmployeeMaster retDet = new EmployeeMaster();
            try
            {      
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var getData = (from r in entity.EmployeeMasters
                                   select r).Where(t => t.EmployeeID == EmployeeID).FirstOrDefault();
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

        public List<EmployeeMaster> ListEmployeeByName(string _EmployeeName)
        {
            List<EmployeeMaster> lRet = new List<EmployeeMaster>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    lRet = (from r in entity.EmployeeMasters
                            select r).Where(t => t.EmployeeName.Contains(_EmployeeName) &&
                            t.AppraisalYear != DateTime.Now.Year.ToString() &&
                            t.HRStaff == null).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return lRet;
        }

        public List<EmployeeMaster> ListEmployeeData()
        {
            List<EmployeeMaster> lResult = new List<EmployeeMaster>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {                   
                    var listData = (from r in entity.EmployeeMasters
                                    select r).ToList();

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

        public bool UpdateEmployeeMaster(List<EmployeeMaster> lUpdData)
        {
            bool insRet = false;
            using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
            {
                using (var dbTrans = entity.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in lUpdData)
                        {
                            var getData = (from t in entity.EmployeeMasters
                                           where t.EmployeeID == item.EmployeeID
                                           select t).FirstOrDefault();

                            getData.HRStaff = item.HRStaff;
                            getData.CreatedBy = item.CreatedBy;
                            getData.CreatedDate = item.CreatedDate;
                            getData.AppraisalYear = item.AppraisalYear;

                            entity.SaveChanges();

                            if (!string.IsNullOrEmpty(item.HRStaff))
                            {
                                ActionHistory data = new ActionHistory();
                                data.EmployeeID = item.EmployeeID;
                                data.Status = "New Document";
                                data.Action = "Create";
                                data.AppraisalYear = item.CreatedDate.Value.Year;
                                data.CreatedBy = item.HRStaff;
                                data.CreatedDate = item.CreatedDate;
                                data.Responsibility = "Creator";

                                entity.ActionHistories.Add(data);
                                entity.SaveChanges();
                            }

                            if (!string.IsNullOrEmpty(item.FirstManager))
                            {
                                ActionHistory data = new ActionHistory();
                                data.EmployeeID = item.EmployeeID;
                                data.Action = "Approve";
                                data.AppraisalYear = item.CreatedDate.Value.Year;
                                data.CreatedBy = item.FirstManager;
                                data.CreatedDate = item.CreatedDate;
                                data.Responsibility = "FirstManager";

                                entity.ActionHistories.Add(data);
                                entity.SaveChanges();
                            }
                        }

                        dbTrans.Commit();

                        insRet = true;
                    }
                    catch (Exception ex)
                    {
                        insRet = false;
                        dbTrans.Rollback();

                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                    }
                }
            }

            return insRet;
        }

        public List<Inf_TaskListData> ListTaskEmployeeData(string UserName)
        {
            List<Inf_TaskListData> lResult = new List<Inf_TaskListData>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var listData = (from e in entity.EmployeeMasters
                                    join a in entity.ActionHistories on e.EmployeeID equals a.EmployeeID
                                    join h in entity.AppraisalDocHeaders on e.EmployeeID equals h.EmployeeID
                                    into gj
                                    from h in gj.Where(f => f.AppraisalYear == DateTime.Now.Year).DefaultIfEmpty()
                                    where a.CreatedBy == UserName
                                           && a.Status == null
                                           && a.AppraisalYear == DateTime.Now.Year
                                    select new
                                    {
                                        EmployeeID = e.EmployeeID,
                                        EmployeeName = e.EmployeeName,
                                        DepartmentName = e.DepartmentName,
                                        CompanyID = e.CompanyID,
                                        Position = e.Position,
                                        FirstManager = e.FirstManager,
                                        SecondManager = e.SecondManager,
                                        HRStaff = e.HRStaff,
                                        CreatedBy = e.CreatedBy,
                                        CreatedDate = e.CreatedDate,
                                        Status = a.Status,
                                        AppraisalDocNo = a.AppraisalDocNo,
                                        TaskOfUser = a.CreatedBy,
                                        AppraisalYear = a.AppraisalYear,
                                        Responsibility = a.Responsibility,
                                        AppraisalStatus = h.AppraisalStatus

                                    }).ToList();

                    foreach (var item in listData)
                    {
                        Inf_TaskListData data = new Inf_TaskListData();
                        data.AppraisalDocNo = item.AppraisalDocNo;
                        data.AppraisalYear = item.AppraisalYear;
                        data.CompanyID = item.CompanyID;
                        data.CreatedBy = item.CreatedBy;
                        data.CreatedDate = item.CreatedDate;
                        data.DepartmentName = item.DepartmentName;
                        data.EmployeeID = item.EmployeeID;
                        data.EmployeeName = item.EmployeeName;
                        data.FirstManager = item.FirstManager;
                        data.HRStaff = item.HRStaff;
                        data.Position = item.Position;
                        data.SecondManager = item.SecondManager;
                        data.Status = item.Status;
                        data.TaskOfUser = item.TaskOfUser;
                        data.Responsibility = item.Responsibility;
                        data.AppraisalStatus = item.AppraisalStatus;
                        lResult.Add(data);
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



        public List<Inf_TaskListData> ListCompleteEmployeeData()
        {
            List<Inf_TaskListData> lResult = new List<Inf_TaskListData>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var listData = (from e in entity.EmployeeMasters
                                    join h in entity.AppraisalDocHeaders on e.EmployeeID equals h.EmployeeID 
                                    where h.AppraisalStatus == "Completed"
                                           && h.AppraisalYear == DateTime.Now.Year
                                    select new
                                    {
                                        EmployeeID = e.EmployeeID,
                                        EmployeeName = e.EmployeeName,
                                        DepartmentName = e.DepartmentName,
                                        CompanyID = e.CompanyID,
                                        Position = e.Position,
                                        FirstManager = e.FirstManager,
                                        SecondManager = e.SecondManager,
                                        HRStaff = e.HRStaff,
                                        CreatedBy = e.CreatedBy,
                                        CreatedDate = e.CreatedDate,
                                        AppraisalDocNo = h.AppraisalDocNo,
                                        TaskOfUser = h.CreatedBy,
                                        AppraisalYear = h.AppraisalYear,
                                        AppraisalStatus = h.AppraisalStatus

                                    }).ToList();

                    foreach (var item in listData)
                    {
                        Inf_TaskListData data = new Inf_TaskListData();
                        data.AppraisalDocNo = item.AppraisalDocNo;
                        data.AppraisalYear = item.AppraisalYear;
                        data.CompanyID = item.CompanyID;
                        data.CreatedBy = item.CreatedBy;
                        data.CreatedDate = item.CreatedDate;
                        data.DepartmentName = item.DepartmentName;
                        data.EmployeeID = item.EmployeeID;
                        data.EmployeeName = item.EmployeeName;
                        data.FirstManager = item.FirstManager;
                        data.HRStaff = item.HRStaff;
                        data.Position = item.Position;
                        data.SecondManager = item.SecondManager;                       
                        data.TaskOfUser = item.TaskOfUser;
                        data.AppraisalStatus = item.AppraisalStatus;
                        lResult.Add(data);
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
        public List<Inf_TaskListData> ListAppraisalHistory(string DomainName)
        {
            List<Inf_TaskListData> lResult = new List<Inf_TaskListData>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    SqlParameter paraComcode = new SqlParameter("@DomainName", DomainName);
                    var result = entity.Database.SqlQuery<Inf_TaskListData>("exec spListAppraisalCompleteTask @DomainName", paraComcode).ToList();
                    lResult = result;

                    #region #### Old Code ####
                    //    var listData = (from e in entity.EmployeeMasters
                    //                    join h in entity.AppraisalDocHeaders on e.EmployeeID equals h.EmployeeID
                    //                    where h.AppraisalYear == DateTime.Now.Year
                    //                    select new
                    //                    {
                    //                        EmployeeID = e.EmployeeID,
                    //                        EmployeeName = e.EmployeeName,
                    //                        DepartmentName = e.DepartmentName,
                    //                        CompanyID = e.CompanyID,
                    //                        Position = e.Position,
                    //                        FirstManager = e.FirstManager,
                    //                        SecondManager = e.SecondManager,
                    //                        HRStaff = e.HRStaff,
                    //                        CreatedBy = e.CreatedBy,
                    //                        CreatedDate = e.CreatedDate,
                    //                        AppraisalDocNo = h.AppraisalDocNo,
                    //                        TaskOfUser = h.CreatedBy,
                    //                        AppraisalYear = h.AppraisalYear,
                    //                        AppraisalStatus = h.AppraisalStatus

                    //                    }).ToList();

                    //    foreach (var item in listData)
                    //    {
                    //        Inf_TaskListData data = new Inf_TaskListData();
                    //        data.AppraisalDocNo = item.AppraisalDocNo;
                    //        data.AppraisalYear = item.AppraisalYear;
                    //        data.CompanyID = item.CompanyID;
                    //        data.CreatedBy = item.CreatedBy;
                    //        data.CreatedDate = item.CreatedDate;
                    //        data.DepartmentName = item.DepartmentName;
                    //        data.EmployeeID = item.EmployeeID;
                    //        data.EmployeeName = item.EmployeeName;
                    //        data.FirstManager = item.FirstManager;
                    //        data.HRStaff = item.HRStaff;
                    //        data.Position = item.Position;
                    //        data.SecondManager = item.SecondManager;
                    //        data.TaskOfUser = item.TaskOfUser;
                    //        data.AppraisalStatus = item.AppraisalStatus;
                    //        lResult.Add(data);
                    //    }
                    #endregion

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return lResult;
        }
        public bool UpdateEmployeeData(EmployeeMaster EmpData)
        {
            bool insRet = false;
            using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
            {
                using (var dbTrans = entity.Database.BeginTransaction())
                {
                    try
                    {
                        var getData = (from t in entity.EmployeeMasters
                                       where t.EmployeeID == EmpData.EmployeeID
                                       select t).FirstOrDefault();

                        getData.FirstManager = EmpData.FirstManager;
                        getData.FirstManagerMail = EmpData.FirstManagerMail;
                        getData.SecondManager = EmpData.SecondManager;
                        getData.SecondManagerMail = EmpData.SecondManagerMail;

                        getData.CreatedBy = EmpData.CreatedBy;
                        getData.CreatedDate = EmpData.CreatedDate;

                        entity.SaveChanges();

                        dbTrans.Commit();

                        insRet = true;
                    }
                    catch (Exception ex)
                    {
                        insRet = false;
                        dbTrans.Rollback();

                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                    }
                }
            }

            return insRet;
        }

        public List<Attachment> listAttachfile(string EmployeeID)
        {
            List<Attachment> lRet = new List<Attachment>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    lRet = (from r in entity.Attachments select r).Where(t => t.EmployeeID == EmployeeID).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return lRet;
        }
    }
}