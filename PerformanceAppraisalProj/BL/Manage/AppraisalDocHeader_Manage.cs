using PerformanceAppraisalProj.DAL;
using PerformanceAppraisalProj.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.BL.Manage
{
    public class AppraisalDocHeader_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AppraisalDocHeader_Manage));

        public bool InsertDocHeaderData(AppraisalDocHeader AppraisalData, List<Attachment> lAttachFile,
            List<AppraisalDocLine> lAppraisalDocLine, ActionHistory updActHisData, ApprovalHistory insApprovalHis, ActionHistory ins2NdActHisData)
        {
            bool result = false;
            Int64 retPK = 0;
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    using (var dbTrans = entity.Database.BeginTransaction())
                    {
                        try
                        {
                            //** Insert table AppraisalDocHeaders ****/
                            entity.AppraisalDocHeaders.Add(AppraisalData);
                            entity.SaveChanges();

                            retPK = AppraisalData.RowID;
                            //***************************************/

                            //**** Delete rows before insert******************//
                            if (lAttachFile != null && lAttachFile.Count > 0)
                            {
                                foreach (var item in lAttachFile)
                                {
                                    var lRetAttach = entity.Attachments.Where(c => c.EmployeeID == AppraisalData.EmployeeID &&
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

                            //************* Step 2 ********************//
                            if (retPK > 0)
                            {
                                string AppraisalDocNo = string.Empty;
                                AppraisalDocNo = GenerateDocNo(retPK.ToString());
                                if (!string.IsNullOrEmpty(AppraisalDocNo))
                                {
                                    //---------- Update "DocNo" to AppraisalDocHeader -------//
                                    var getData = (from t in entity.AppraisalDocHeaders
                                                   where t.RowID == retPK
                                                   select t).FirstOrDefault();

                                    getData.AppraisalDocNo = AppraisalDocNo;
                                    entity.SaveChanges();
                                    //-------------------------------------------------------//

                                    //-------------------- Insert AppraisalDocLine ----------//
                                    foreach (var itemIns in lAppraisalDocLine)
                                    {
                                        itemIns.AppraisalDocNo = AppraisalDocNo;

                                        entity.AppraisalDocLines.Add(itemIns);
                                        entity.SaveChanges();
                                    }
                                    //-------------------------------------------------------//

                                    //---------------- Insert & Update History -------------//
                                    /*****   Update table Action History *****/
                                    var getActHistory = (from t in entity.ActionHistories
                                                         where t.EmployeeID == updActHisData.EmployeeID
                                                                && t.CreatedBy == updActHisData.CreatedBy
                                                                && t.Status != "New Document"
                                                                && t.AppraisalYear == updActHisData.AppraisalYear
                                                         select t).FirstOrDefault();

                                    getActHistory.Status = updActHisData.Status;
                                    getActHistory.AppraisalDocNo = AppraisalDocNo;
                                    getActHistory.Comments = updActHisData.Comments;
                                    entity.SaveChanges();
                                    /*****************************************/

                                    /******* Insert Approval History *********/
                                    insApprovalHis.AppraisalDocNo = AppraisalDocNo;
                                    entity.ApprovalHistories.Add(insApprovalHis);
                                    entity.SaveChanges();
                                    /*****************************************/

                                    /* Insert table Action History For Seccound Manager */
                                    if (!string.IsNullOrEmpty(ins2NdActHisData.EmployeeID))
                                    {
                                        ins2NdActHisData.AppraisalDocNo = AppraisalDocNo;
                                        entity.ActionHistories.Add(ins2NdActHisData);
                                        entity.SaveChanges();
                                    }
                                    /*****************************************/

                                    /*****   Update table Employee Master *****/
                                    var getEmpMasData = (from t in entity.EmployeeMasters
                                                         where t.EmployeeID == updActHisData.EmployeeID
                                                         select t).FirstOrDefault();
                                    getEmpMasData.AppraisalDate = insApprovalHis.TransactionDate;
                                    entity.SaveChanges();
                                    /*****************************************/
                                }
                            }

                            dbTrans.Commit();

                            result = true;
                        }
                        catch (Exception ex)
                        {
                            result = false;

                            dbTrans.Rollback();

                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;

                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return result;
        }

        private string GenerateDocNo(string DocHeaderRunNo)
        {
            string strBDCode = string.Empty;
            try
            {
                /************************** A000001 ****************************/
                string strPrefix = "A";
                string strCodeRunNo = "00000" + DocHeaderRunNo;

                GlobalFunction func = new GlobalFunction();
                string runnNo = func.RightFunction(strCodeRunNo, 6);

                strBDCode = strPrefix + runnNo;
                /****************************************************************/
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return strBDCode;
        }

        public bool InsertData(AppraisalDocHeader data, List<AppraisalDocLine> lData)
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
                            /**** Insert AppraisalDocHeader ******/
                            entity.AppraisalDocHeaders.Add(data);
                            entity.SaveChanges();
                            /*************************************/

                            /**** Insert AppraisalDocLine ******/
                            foreach (var item in lData)
                            {
                                entity.AppraisalDocLines.Add(item);
                                entity.SaveChanges();
                            }
                            /*************************************/

                            /*****   Update table Action History *****/
                            var getData = (from t in entity.ActionHistories
                                           where t.EmployeeID == data.EmployeeID && t.CreatedBy == data.CreatedBy && t.Status != "New Document"
                                           select t).FirstOrDefault();

                            getData.Status = "Approved";
                            getData.AppraisalDocNo = data.AppraisalDocNo;
                            getData.Comments = "";
                            entity.SaveChanges();
                            /*****************************************/

                            /******* Insert ApprovalHistory *********/
                            ApprovalHistory dataAppHis = new ApprovalHistory();
                            dataAppHis.Action = "Approve";
                            dataAppHis.TransactionDate = data.CreatedDate;
                            dataAppHis.UserID = data.CreatedBy;
                            dataAppHis.Comment = "";
                            dataAppHis.AppraisalDocNo = data.AppraisalDocNo;
                            entity.ApprovalHistories.Add(dataAppHis);
                            entity.SaveChanges();
                            /****************************************/

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
           
        public AppraisalDocHeader GetData(string EmployeeID, int Year)
        {
            AppraisalDocHeader retDet = new AppraisalDocHeader();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var getData = (from r in entity.AppraisalDocHeaders select r)
                    .Where(t => t.EmployeeID.Equals(EmployeeID.Trim()) && t.AppraisalYear == Year).FirstOrDefault();
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
        public AppraisalDocHeader GetDocHeaderCompleteData(string EmployeeID, string DocNo, int Year)
        {
            AppraisalDocHeader retDet = new AppraisalDocHeader();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var getData = (from r in entity.AppraisalDocHeaders select r)
                    .Where(t => t.EmployeeID.Equals(EmployeeID.Trim()) &&
                                t.AppraisalDocNo == DocNo &&
                                t.AppraisalYear == Year).FirstOrDefault();
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

        public bool UpdateAppraisalDocHeader(AppraisalDocHeader updData)
        {
            bool updRet = false;
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var getData = (from t in entity.AppraisalDocHeaders
                                   where t.RowID == updData.RowID
                                   select t).FirstOrDefault();

                    getData.AppraisalDocNo = updData.AppraisalDocNo;                   
                    entity.SaveChanges();

                    updRet = true;
                }
            }
            catch (Exception ex)
            {
                updRet = false;

                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return updRet;
        }
        public bool UpdateRejectAppraisalDocHeader(AppraisalDocHeader updData)
        {
            bool updRet = false;
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var getData = (from t in entity.AppraisalDocHeaders
                                   where t.AppraisalDocNo == updData.AppraisalDocNo
                                   select t).FirstOrDefault();

                    getData.AppraisalDate = updData.AppraisalDate;
                    getData.AppraisalPeriodFrom = updData.AppraisalPeriodFrom;
                    getData.AppraisalPeriodTo = updData.AppraisalPeriodTo;
                    getData.AppraisalStatus = updData.AppraisalStatus;
                    getData.AppraisalTotalScore = updData.AppraisalTotalScore;
                    getData.AppraisalGrade = updData.AppraisalGrade;

                    entity.SaveChanges();

                    updRet = true;
                }
            }
            catch (Exception ex)
            {
                updRet = false;

                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return updRet;
        }
        public bool Upd2ndManagerAppr(AppraisalDocHeader data, string Remark, List<Attachment> lAttachFile)
        {
            bool ret = false;

            using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
            {
                using (var dbTrans = entity.Database.BeginTransaction())
                {
                    try
                    {
                        /**** Update Approve AppraisalDocHeader ******/
                        var getData = (from t in entity.AppraisalDocHeaders
                                       where t.AppraisalDocNo == data.AppraisalDocNo
                                       select t).FirstOrDefault();
                        getData.AppraisalStatus = data.AppraisalStatus;
                        getData.CreatedDate = data.CreatedDate;
                        getData.CreatedBy = data.CreatedBy;

                        entity.SaveChanges();
                        /*****************************************/

                        /******* Update ActionHistory ******/
                        var getActionData = (from t in entity.ActionHistories
                                             where t.EmployeeID == data.EmployeeID
                                             && (t.Status == null || t.Status.Trim().Equals("Rejected"))
                                             && t.AppraisalYear == DateTime.Now.Year
                                             select t).FirstOrDefault();

                        getActionData.AppraisalDocNo = data.AppraisalDocNo;
                        getActionData.CreatedDate = data.CreatedDate;
                        getActionData.CreatedBy = data.CreatedBy;
                        getActionData.Comments = Remark;
                        getActionData.Status = "Approved";

                        entity.SaveChanges();
                        /****************************************/

                        /******* Insert ApprovalHistory *********/
                        ApprovalHistory dataAppHis = new ApprovalHistory();
                        dataAppHis.Action = "Approve";
                        dataAppHis.TransactionDate = data.CreatedDate;
                        dataAppHis.UserID = data.CreatedBy;
                        dataAppHis.Comment = Remark;
                        dataAppHis.AppraisalDocNo = data.AppraisalDocNo;

                        entity.ApprovalHistories.Add(dataAppHis);
                        entity.SaveChanges();
                        /****************************************/

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

            return ret;
        }
        public bool Upd2ndManagerReject(AppraisalDocHeader data, string Remark, List<Attachment> lAttachFile)
        {
            bool ret = false;

            using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
            {
                using (var dbTrans = entity.Database.BeginTransaction())
                {
                    try
                    {
                        /**** Update Approve AppraisalDocHeader ******/
                        var getData = (from t in entity.AppraisalDocHeaders
                                       where t.AppraisalDocNo == data.AppraisalDocNo
                                       select t).FirstOrDefault();
                        getData.AppraisalStatus = data.AppraisalStatus;
                        getData.CreatedDate = data.CreatedDate;
                        getData.CreatedBy = data.CreatedBy;

                        entity.SaveChanges();
                        /*****************************************/
                      
                        /******* Update ActionHistory First Manager ******/
                        var getAct1stData = (from t in entity.ActionHistories
                                             where t.EmployeeID == data.EmployeeID
                                             && t.Responsibility == "FirstManager"
                                             && t.AppraisalYear == DateTime.Now.Year
                                             && t.AppraisalDocNo == data.AppraisalDocNo
                                             select t).FirstOrDefault();

                        //getAct1stData.AppraisalDocNo = data.AppraisalDocNo;
                        //getAct1stData.CreatedDate = data.CreatedDate;
                        //getAct1stData.CreatedBy = data.CreatedBy;
                        //getAct1stData.Comments = Remark;
                        getAct1stData.Status = null;
                        entity.SaveChanges();
                        /****************************************/

                        /******* Update ActionHistory Seccound Manager ******/
                        var getAct1ndData = (from t in entity.ActionHistories
                                             where t.EmployeeID == data.EmployeeID
                                             && (t.Status == null || t.Status.Trim().Equals("Rejected"))
                                             && t.AppraisalYear == DateTime.Now.Year
                                             && t.Responsibility == "SecondManager"
                                             && t.AppraisalDocNo == data.AppraisalDocNo
                                             select t).FirstOrDefault();

                        //getActionData.AppraisalDocNo = data.AppraisalDocNo;
                        //getActionData.CreatedDate = data.CreatedDate;
                        //getActionData.CreatedBy = data.CreatedBy;
                        getAct1ndData.Comments = Remark;
                        getAct1ndData.Status = "Rejected";
                        entity.SaveChanges();
                        /****************************************/                                              
                       

                        /******* Insert ApprovalHistory *********/
                        ApprovalHistory dataAppHis = new ApprovalHistory();
                        dataAppHis.Action = "Reject";
                        dataAppHis.TransactionDate = data.CreatedDate;
                        dataAppHis.UserID = data.CreatedBy;
                        dataAppHis.Comment = Remark;
                        dataAppHis.AppraisalDocNo = data.AppraisalDocNo;

                        entity.ApprovalHistories.Add(dataAppHis);
                        entity.SaveChanges();
                        /****************************************/

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

            return ret;
        }


        public bool UpdateAppraisalData(AppraisalDocHeader data, List<AppraisalDocLine> lData)
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
                            /**** Insert AppraisalDocHeader ******/
                            entity.AppraisalDocHeaders.Add(data);
                            entity.SaveChanges();
                            /*************************************/

                            /**** Insert AppraisalDocLine ******/
                            foreach (var item in lData)
                            {
                                entity.AppraisalDocLines.Add(item);
                                entity.SaveChanges();
                            }
                            /*************************************/

                            /*****   Update table Action History *****/
                            var getData = (from t in entity.ActionHistories
                                           where t.EmployeeID == data.EmployeeID && t.CreatedBy == data.CreatedBy && t.Status != "New Document"
                                           select t).FirstOrDefault();

                            getData.Status = "Approved";
                            getData.AppraisalDocNo = data.AppraisalDocNo;
                            getData.Comments = "";
                            entity.SaveChanges();
                            /*****************************************/

                            /******* Insert ApprovalHistory *********/
                            ApprovalHistory dataAppHis = new ApprovalHistory();
                            dataAppHis.Action = "Approve";
                            dataAppHis.TransactionDate = data.CreatedDate;
                            dataAppHis.UserID = data.CreatedBy;
                            dataAppHis.Comment = "";
                            dataAppHis.AppraisalDocNo = data.AppraisalDocNo;
                            entity.ApprovalHistories.Add(dataAppHis);
                            entity.SaveChanges();
                            /****************************************/

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

        public List<AppraisalDocHeader> ListDocHeaderByID(string EmployeeID)
        {
            List<AppraisalDocHeader> lRet = new List<AppraisalDocHeader>();
            try
            {
                using (PerformanceAppraisalEntities entity = new PerformanceAppraisalEntities())
                {
                    var getData = (from r in entity.AppraisalDocHeaders select r)
                    .Where(t => t.EmployeeID.Equals(EmployeeID.Trim()) && t.AppraisalStatus == "Completed").ToList();
                    lRet = getData;
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
 