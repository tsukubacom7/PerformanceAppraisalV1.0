using PerformanceAppraisalProj.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.BL.Manage
{
    public class ActionHistory_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ActionHistory_Manage));
        public ActionHistory GetActionHistoryData(string EmployeeID)
        {
            ActionHistory retDet = new ActionHistory();
            try
            {      
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var getData = (from r in entity.ActionHistories select r).Where(t => t.EmployeeID == EmployeeID).FirstOrDefault();
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


        public string GetActionHistoryStatus(string DocNo, string Responsibility)
        {
            string sStatus = string.Empty;
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    sStatus = entity.ActionHistories.Where(t => t.AppraisalDocNo == DocNo &&
                             t.Responsibility == Responsibility).Select(c => c.Status).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return sStatus;
        }

        public List<ActionHistory> ListActionHistory(string EmployeeID, int AppraisalYear)
        {
            List<ActionHistory> lResult = new List<ActionHistory>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var listData = (from r in entity.ActionHistories select r)
                                    .Where(c => c.EmployeeID == EmployeeID
                                     && c.AppraisalYear == AppraisalYear).ToList();

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
        

        public bool InsertAndUpdHistory(ActionHistory updActionHis, ApprovalHistory insApprovalHis, ActionHistory insActionHis)
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
                            /*****   Update table Action History *****/
                            var getData = (from t in entity.ActionHistories
                                           where t.EmployeeID == updActionHis.EmployeeID
                                                  && t.CreatedBy == updActionHis.CreatedBy
                                                  && t.Status != "New Document" 
                                                  && t.AppraisalYear == updActionHis.AppraisalYear
                                           select t).FirstOrDefault();

                            getData.Status = updActionHis.Status;
                            getData.AppraisalDocNo = updActionHis.AppraisalDocNo;
                            getData.Comments = updActionHis.Comments;
                            entity.SaveChanges();
                            /*****************************************/

                            /******* Insert Approval History *********/
                            entity.ApprovalHistories.Add(insApprovalHis);
                            entity.SaveChanges();
                            /*****************************************/

                            /* Insert table Action History For Seccound Manager */
                            if (!string.IsNullOrEmpty(insActionHis.EmployeeID))
                            {
                                entity.ActionHistories.Add(insActionHis);
                                entity.SaveChanges();
                            }
                            /*****************************************/


                            /*****   Update table Employee Master *****/
                            var getEmpMasData = (from t in entity.EmployeeMasters
                                                 where t.EmployeeID == updActionHis.EmployeeID
                                                 select t).FirstOrDefault();
                            getEmpMasData.AppraisalDate = insApprovalHis.TransactionDate;
                            entity.SaveChanges();
                            /*****************************************/

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

        public bool InsertAndUpdHistoryReject(AppraisalDocHeader headerData, ActionHistory updActionHis, 
                                              ApprovalHistory insApprovalHis, List<AppraisalDocLine> lDoclineData, 
                                              List<Attachment> lAttachFile)
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

                            /*****   Update table  AppraisalDocHeader *****/
                            var getHeaderData = (from t in entity.AppraisalDocHeaders
                                                 where t.AppraisalDocNo == headerData.AppraisalDocNo
                                                 select t).FirstOrDefault();
                            getHeaderData.AppraisalDate = headerData.AppraisalDate;
                            getHeaderData.AppraisalPeriodFrom = headerData.AppraisalPeriodFrom;
                            getHeaderData.AppraisalPeriodTo = headerData.AppraisalPeriodTo;
                            getHeaderData.AppraisalStatus = headerData.AppraisalStatus;
                            getHeaderData.AppraisalTotalScore = headerData.AppraisalTotalScore;
                            getHeaderData.AppraisalGrade = headerData.AppraisalGrade;
                            getHeaderData.EmployeeStrength = headerData.EmployeeStrength;
                            getHeaderData.EmployeeImprovement = headerData.EmployeeImprovement;
                            entity.SaveChanges();
                            /***********************************************/

                            /*****   Update table ActionHistory 1st Manager Status *****/
                            var getActHis1stData = (from t in entity.ActionHistories
                                                 where t.EmployeeID == updActionHis.EmployeeID
                                                        && t.Responsibility == "FirstManager"
                                                        && t.AppraisalYear == updActionHis.AppraisalYear
                                                        && t.AppraisalDocNo == updActionHis.AppraisalDocNo
                                                 select t).FirstOrDefault();

                            getActHis1stData.Status = updActionHis.Status;
                            getActHis1stData.Comments = updActionHis.Comments;
                            entity.SaveChanges();
                            /*****************************************/

                            /*****   Update table ActionHistory 2nd Manager Status *****/
                            var getActHis2ndData = (from t in entity.ActionHistories
                                                 where t.EmployeeID == updActionHis.EmployeeID
                                                        && t.Responsibility == "SecondManager"
                                                        && t.AppraisalYear == updActionHis.AppraisalYear
                                                        && t.AppraisalDocNo == updActionHis.AppraisalDocNo
                                                 select t).FirstOrDefault();

                            getActHis2ndData.Status = null;
                            getActHis2ndData.Comments = null;
                            entity.SaveChanges();
                            /*****************************************/

                            /******* Insert Approval History *********/
                            entity.ApprovalHistories.Add(insApprovalHis);
                            entity.SaveChanges();
                            /*****************************************/

                            /*****   Update table Employee Master *****/
                            var getEmpMasData = (from t in entity.EmployeeMasters
                                                 where t.EmployeeID == updActionHis.EmployeeID
                                                 select t).FirstOrDefault();
                            getEmpMasData.AppraisalDate = insApprovalHis.TransactionDate;
                            entity.SaveChanges();
                            /*****************************************/

                            /****  Delete AppraisalDocLine Where "AppraisalDocNo" bofore Insert *******/
                            entity.AppraisalDocLines.RemoveRange(entity.AppraisalDocLines.Where(c => c.AppraisalDocNo == headerData.AppraisalDocNo));
                            entity.SaveChanges();
                            /***********************************************************/

                            /****  Insert AppraisalDocLine *******/
                            foreach (var item in lDoclineData)
                            {
                                entity.AppraisalDocLines.Add(item);
                                entity.SaveChanges();
                            }
                            /*************************************/

                            //**** Delete rows before insert******************//
                            if (lAttachFile != null && lAttachFile.Count > 0)
                            {
                                foreach (var item in lAttachFile)
                                {
                                    var lRetAttach = entity.Attachments.Where(c => c.EmployeeID == item.EmployeeID &&
                                    c.FileName == item.FileName).ToList();
                                    if (lRetAttach.Count() > 0)
                                    {
                                        foreach (var iAttach in lRetAttach)
                                        {
                                            entity.Attachments.Remove(iAttach);
                                            entity.SaveChanges();
                                        }
                                    }
                                }
                            }
                            //************************************************//

                            //***** Insert table Attachments   ****************/
                            if (lAttachFile != null && lAttachFile.Count > 0)
                            {
                                foreach (var item in lAttachFile)
                                {
                                    Attachment insAttachment = new Attachment();
                                    insAttachment.FileName = item.FileName;
                                    insAttachment.EmployeeID = item.EmployeeID;
                                    insAttachment.FileDescription = item.FileDescription;
                                    insAttachment.Attachment1 = item.Attachment1;
                                    insAttachment.CreatedBy = item.CreatedBy;
                                    insAttachment.CreatedDate = item.CreatedDate;
                                    insAttachment.UpdatedBy = item.UpdatedBy;
                                    insAttachment.UpdatedDate = item.UpdatedDate;

                                    entity.Attachments.Add(insAttachment);
                                    entity.SaveChanges();
                                }
                            }
                            //*****************************************/

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

        public bool DeleteFileAttachment(string EmployeeID, string FileName)
        {
            bool ret = false;
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var lRetAttach = entity.Attachments.Where(c => c.EmployeeID == EmployeeID && c.FileName == FileName).ToList();
                    if (lRetAttach.Count() > 0)
                    {
                        foreach (var iAttach in lRetAttach)
                        {
                            entity.Attachments.Remove(iAttach);
                            entity.SaveChanges();
                        }
                    }
                }

                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return ret;
        }
    }
}