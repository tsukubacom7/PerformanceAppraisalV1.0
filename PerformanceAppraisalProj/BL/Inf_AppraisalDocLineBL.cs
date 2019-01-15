using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using PerformanceAppraisalProj.DAO;

namespace PerformanceAppraisalProj.BL
{  
    public class Inf_AppraisalDocLineBL
    {
        private SqlConnection _conn;
        private SqlTransaction _tran;

        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Inf_AppraisalDocLineBL));

        public Inf_AppraisalDocLineBL(IDbConnection conn, IDbTransaction tran = null)
        {
            this._conn = (SqlConnection)conn;
            this._tran = (SqlTransaction)tran;
        }


        public string InsertAppraisalDocLine(INF_APPRAISALDOCLINE data)
        {
            string bRet = string.Empty;
            try
            {
                string strQuery = "INSERT INTO [dbo].[AppraisalDocLine] " +
                                       "([AppraisalDocNo] " +
                                       ",[AppraisalDocLineNo] " +
                                       ",[QuestionLineNo] " +
                                       ",[QuestionDesc] " +
                                       ",[Score] " +
                                       ",[CalculatedScore] " +
                                       ",[QuestionWeight] " +
                                       ",[Remark]) " +
                                       ",[QuestionType]) " +
                                    "VALUES " +
                                       "(@AppraisalDocNo " +
                                       ",@AppraisalDocLineNo " +
                                       ",@QuestionLineNo " +
                                       ",@QuestionDesc " +
                                       ",@Score " +
                                       ",@CalculatedScore " +
                                       ",@QuestionWeight " +
                                       ",@Remark " +
                                       ",@QuestionType)";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                command.Parameters.AddWithValue("@AppraisalDocNo", string.IsNullOrWhiteSpace(data.AppraisalDocNo) ? "" : data.AppraisalDocNo);
                command.Parameters.AddWithValue("@AppraisalDocLineNo", data.AppraisalDocLineNo == null ? null : data.AppraisalDocLineNo);
                command.Parameters.AddWithValue("@QuestionLineNo", data.QuestionLineNo == null ? null : data.QuestionLineNo);
                command.Parameters.AddWithValue("@QuestionDesc", string.IsNullOrWhiteSpace(data.QuestionDesc) ? "" : data.QuestionDesc);
                command.Parameters.AddWithValue("@Score", data.Score == null ? null : data.Score);
                command.Parameters.AddWithValue("@CalculatedScore", data.CalculatedScore == null ? null : data.CalculatedScore);
                command.Parameters.AddWithValue("@QuestionWeight", data.QuestionWeight == null ? null : data.QuestionWeight);
                command.Parameters.AddWithValue("@Remark", string.IsNullOrWhiteSpace(data.Remark) ? "" : data.Remark);
                command.Parameters.AddWithValue("@QuestionType", data.Score == null ? null : data.QuestionType);

                command.ExecuteNonQuery();             

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

        public bool UpdateAppraisalDocLine(INF_APPRAISALDOCLINE data)
        {
            bool bRet = false;
            try
            {
                string strQuery = "UPDATE [dbo].[AppraisalDocLine] " +
                                   "SET [AppraisalDocNo] = @AppraisalDocNo " +
                                      ",[AppraisalDocLineNo] = @AppraisalDocLineNo " +
                                      ",[QuestionLineNo] = @QuestionLineNo " +
                                      ",[QuestionDesc] = @QuestionDesc " +
                                      ",[Score] = @Score " +
                                      ",[CalculatedScore] = @CalculatedScore " +
                                      ",[QuestionWeight] = @QuestionWeight " +
                                      ",[Remark] = @Remark " +
                                      ",[QuestionType] = @QuestionType " +
                                  "WHERE [RowID] = @RowID ";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                command.Parameters.AddWithValue("@AppraisalDocNo", string.IsNullOrWhiteSpace(data.AppraisalDocNo) ? "" : data.AppraisalDocNo);
                command.Parameters.AddWithValue("@AppraisalDocLineNo", data.AppraisalDocLineNo == null ? null : data.AppraisalDocLineNo);
                command.Parameters.AddWithValue("@QuestionLineNo", data.QuestionLineNo == null ? null : data.QuestionLineNo);
                command.Parameters.AddWithValue("@QuestionDesc", string.IsNullOrWhiteSpace(data.QuestionDesc) ? "" : data.QuestionDesc);
                command.Parameters.AddWithValue("@Score", data.Score == null ? null : data.Score);
                command.Parameters.AddWithValue("@CalculatedScore", data.CalculatedScore == null ? null : data.CalculatedScore);
                command.Parameters.AddWithValue("@QuestionWeight", data.QuestionWeight == null ? null : data.QuestionWeight);
                command.Parameters.AddWithValue("@Remark", string.IsNullOrWhiteSpace(data.Remark) ? "" : data.Remark);
                command.Parameters.AddWithValue("@QuestionType", data.Score == null ? null : data.QuestionType);
                command.Parameters.AddWithValue("@RowID", data.RowID == null ? null : data.RowID);                

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

        public bool DeleteAppraisalDocLine(INF_APPRAISALDOCLINE data)
        {
            bool bRet = false;
            try
            {
                string strQuery = "DELETE FROM [AppraisalDocLine] WHERE [RowID] = @RowID";

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