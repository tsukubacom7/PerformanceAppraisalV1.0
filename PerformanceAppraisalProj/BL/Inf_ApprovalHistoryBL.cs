using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using PerformanceAppraisalProj.DAO;

namespace PerformanceAppraisalProj.BL
{  
    public class Inf_ApprovalHistoryBL
    {
        private SqlConnection _conn;
        private SqlTransaction _tran;

        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Inf_ApprovalHistoryBL));

        public Inf_ApprovalHistoryBL(IDbConnection conn, IDbTransaction tran = null)
        {
            this._conn = (SqlConnection)conn;
            this._tran = (SqlTransaction)tran;
        }



        public string InsertInf_ApprovalHistoryBL(INF_APPROVALHISTORY data)
        {
            string bRet = string.Empty;
            try
            {
                string strQuery = "INSERT INTO [dbo].[ApprovalHistory] " +
                                           "([TransactionDate] " +
                                           ",[UserID] " +
                                           ",[Action] " +
                                           ",[Comment] " +
                                           ",[AppraisalDocNo]) " +
                                     "VALUES " +
                                           "(@TransactionDate " +
                                           ", @UserID " +
                                           ", @Action " +
                                           ", @Comment) " +
                                           ", @AppraisalDocNo); " +
                                           " SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                if (!string.IsNullOrEmpty(data.TransactionDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.TransactionDate;
                    string dateString = dtNew.ToString("dd-MMM-yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@TransactionDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                }

                command.Parameters.AddWithValue("@UserID", string.IsNullOrWhiteSpace(data.UserID) ? "" : data.UserID);
                command.Parameters.AddWithValue("@Action", string.IsNullOrWhiteSpace(data.Action) ? "" : data.Action);
                command.Parameters.AddWithValue("@Comment", string.IsNullOrWhiteSpace(data.Comment) ? "" : data.Comment);
                command.Parameters.AddWithValue("@AppraisalDocNo", string.IsNullOrWhiteSpace(data.Comment) ? "" : data.AppraisalDocNo);

            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return bRet;
        }

        public bool UpdateApprovalHistory(INF_APPROVALHISTORY data)
        {
            bool bRet = false;
            try
            {
                string strQuery = "UPDATE [dbo].[ApprovalHistory] " +
                                  "SET [TransactionDate] = @TransactionDate " +
                                        ",[UserID] = @UserID " +
                                        ",[Action] = @Action " +
                                        ",[Comment] = @Comment " +
                                        ",[AppraisalDocNo] = @AppraisalDocNo " +
                                   "WHERE [RowID] = @RowID ";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                if (!string.IsNullOrEmpty(data.TransactionDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.TransactionDate;
                    string dateString = dtNew.ToString("dd-MMM-yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@TransactionDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                }

                command.Parameters.AddWithValue("@UserID", string.IsNullOrWhiteSpace(data.UserID) ? "" : data.UserID);
                command.Parameters.AddWithValue("@Action", string.IsNullOrWhiteSpace(data.Action) ? "" : data.Action);
                command.Parameters.AddWithValue("@Comment", string.IsNullOrWhiteSpace(data.Comment) ? "" : data.Comment);
                command.Parameters.AddWithValue("@AppraisalDocNo", string.IsNullOrWhiteSpace(data.AppraisalDocNo) ? "" : data.AppraisalDocNo);

                if (command.ExecuteNonQuery() == 1)
                {
                    bRet = true;
                }

            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw ex;
            }

            return bRet;
        }

        public bool DeleteApprovalHistory(INF_APPROVALHISTORY data)
        {
            bool bRet = false;
            try
            {
                string strQuery = "DELETE FROM [ApprovalHistory] WHERE [RowID] = @RowID";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@RowID", data.RowID);

                command.ExecuteNonQuery();

                bRet = true;
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw ex;
            }

            return bRet;
        }

    }
}