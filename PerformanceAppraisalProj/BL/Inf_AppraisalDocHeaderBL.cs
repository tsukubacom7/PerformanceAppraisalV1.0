using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using PerformanceAppraisalProj.DAO;

namespace PerformanceAppraisalProj.BL
{  
    public class Inf_AppraisalDocHeaderBL
    {
        private SqlConnection _conn;
        private SqlTransaction _tran;

        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Inf_AppraisalDocHeaderBL));

        public Inf_AppraisalDocHeaderBL(IDbConnection conn, IDbTransaction tran = null)
        {
            this._conn = (SqlConnection)conn;
            this._tran = (SqlTransaction)tran;
        }



        public string InsertAppraisalDocHeader(INF_APPRAISALDOCHEADER data)
        {
            string bRet = string.Empty;
            try
            {
                string strQuery = "INSERT INTO [dbo].[AppraisalDocHeader] " +
                                           "([AppraisalDocNo] " +
                                           ",[AppraisalYear] " +
                                           ",[AppraisalPeriodFrom] " +
                                           ",[AppraisalPeriodTo] " +
                                           ",[AppraisalDate] " +
                                           ",[EmployeeName] " +
                                           ",[Position] " +
                                           ",[DepartmentName] " +
                                           ",[CompanyID] " +
                                           ",[EmployeeStrength] " +
                                           ",[EmployeeImprovement] " +
                                           ",[CreatedDate] " +
                                           ",[CreatedBy] " +
                                           ",[AppraisalStatus] " +
                                           ",[AppraisalGrade] " +
                                           ",[AppraisalTotalScore] " +
                                           ",[StartDate]) " +
                                     "VALUES " +
                                           "(@AppraisalDocNo " +
                                           ", @AppraisalYear " +
                                           ", @AppraisalPeriodFrom " +
                                           ", @AppraisalPeriodTo " +
                                           ", @AppraisalDate " +
                                           ", @EmployeeName " +
                                           ", @Position " +
                                           ", @DepartmentName " +
                                           ", @CompanyID " +
                                           ", @EmployeeStrength " +
                                           ", @EmployeeImprovement " +
                                           ", @CreatedDate " +
                                           ", @CreatedBy " +
                                           ", @AppraisalStatus " +
                                           ", @AppraisalGrade " +
                                           ", @AppraisalTotalScore " +
                                           ", @StartDate); " +
                                           " SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                command.Parameters.AddWithValue("@AppraisalDocNo", string.IsNullOrWhiteSpace(data.AppraisalDocNo) ? "" : data.AppraisalDocNo);
                command.Parameters.AddWithValue("@AppraisalYear", data.AppraisalYear == null ? null : data.AppraisalYear);
                command.Parameters.AddWithValue("@EmployeeName", string.IsNullOrWhiteSpace(data.EmployeeName) ? "" : data.EmployeeName);
                command.Parameters.AddWithValue("@Position", string.IsNullOrWhiteSpace(data.Position) ? "" : data.Position);
                command.Parameters.AddWithValue("@DepartmentName", string.IsNullOrWhiteSpace(data.DepartmentName) ? "" : data.DepartmentName);
                command.Parameters.AddWithValue("@CompanyID", string.IsNullOrWhiteSpace(data.CompanyID) ? "" : data.CompanyID);
                command.Parameters.AddWithValue("@EmployeeStrength", string.IsNullOrWhiteSpace(data.EmployeeStrength) ? "" : data.EmployeeStrength);
                command.Parameters.AddWithValue("@EmployeeImprovement", string.IsNullOrWhiteSpace(data.EmployeeImprovement) ? "" : data.EmployeeImprovement);
                command.Parameters.AddWithValue("@CreatedBy", string.IsNullOrWhiteSpace(data.CreatedBy) ? "" : data.CreatedBy);
                command.Parameters.AddWithValue("@AppraisalStatus", string.IsNullOrWhiteSpace(data.AppraisalStatus) ? "" : data.AppraisalStatus);
                command.Parameters.AddWithValue("@AppraisalGrade", string.IsNullOrWhiteSpace(data.AppraisalGrade) ? "" : data.AppraisalGrade);
                command.Parameters.AddWithValue("@AppraisalTotalScore", data.AppraisalTotalScore == null ? null : data.AppraisalTotalScore);

                if (!string.IsNullOrEmpty(data.CreatedDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.CreatedDate;
                    string dateString = dtNew.ToString("dd-MMM-yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@CreateDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                }

                if (!string.IsNullOrEmpty(data.AppraisalPeriodFrom.ToString()))
                {
                    DateTime dtNew = (DateTime)data.AppraisalPeriodFrom;
                    string dateString = dtNew.ToString("dd-MMM-yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@AppraisalPeriodFrom", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@AppraisalPeriodFrom", DateTime.Now);
                }

                if (!string.IsNullOrEmpty(data.AppraisalPeriodTo.ToString()))
                {
                    DateTime dtNew = (DateTime)data.AppraisalPeriodTo;
                    string dateString = dtNew.ToString("dd-MMM-yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@AppraisalPeriodTo", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@AppraisalPeriodTo", DateTime.Now);
                }

                if (!string.IsNullOrEmpty(data.AppraisalDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.AppraisalDate;
                    string dateString = dtNew.ToString("dd-MMM-yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@AppraisalDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@AppraisalDate", DateTime.Now);
                }

                if (!string.IsNullOrEmpty(data.StartDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.StartDate;
                    string dateString = dtNew.ToString("dd-MMM-yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@StartDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                }

                object _CompanyNo = command.ExecuteScalar();
                if (_CompanyNo != null)
                {
                    bRet = _CompanyNo.ToString();
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

        public bool UpdateAppraisalDocHeader(INF_APPRAISALDOCHEADER data)
        {
            bool bRet = false;
            try
            {
                string strQuery = "UPDATE [dbo].[AppraisalDocHeader] " +
                                  "SET [AppraisalYear] = @AppraisalYear " +
                                        ",[AppraisalPeriodFrom] = @AppraisalPeriodFrom " +
                                        ",[AppraisalPeriodTo] = @AppraisalPeriodTo " +
                                        ",[AppraisalDate] = @AppraisalDate " +
                                        ",[EmployeeName] = @EmployeeName " +
                                        ",[Position] = @Position " +
                                        ",[DepartmentName] = @DepartmentName " +
                                        ",[CompanyID] = @CompanyID " +
                                        ",[EmployeeStrength] = @EmployeeStrength " +
                                        ",[EmployeeImprovement] = @EmployeeImprovement " +
                                        ",[CreatedDate] = @CreatedDate " +
                                        ",[CreatedBy] = @CreatedBy " +
                                        ",[AppraisalStatus] = @AppraisalStatus " +
                                        ",[AppraisalGrade] = @AppraisalGrade " +
                                        ",[AppraisalTotalScore] = @AppraisalTotalScore " +
                                        ",[StartDate] = @StartDate " +
                                   "WHERE [AppraisalDocNo] = @AppraisalDocNo ";

                SqlCommand command = new SqlCommand(strQuery, _conn);             
                command.Parameters.AddWithValue("@AppraisalYear", data.AppraisalYear == null ? null : data.AppraisalYear);
                command.Parameters.AddWithValue("@EmployeeName", string.IsNullOrWhiteSpace(data.EmployeeName) ? "" : data.EmployeeName);
                command.Parameters.AddWithValue("@Position", string.IsNullOrWhiteSpace(data.Position) ? "" : data.Position);
                command.Parameters.AddWithValue("@DepartmentName", string.IsNullOrWhiteSpace(data.DepartmentName) ? "" : data.DepartmentName);
                command.Parameters.AddWithValue("@CompanyID", string.IsNullOrWhiteSpace(data.CompanyID) ? "" : data.CompanyID);
                command.Parameters.AddWithValue("@EmployeeStrength", string.IsNullOrWhiteSpace(data.EmployeeStrength) ? "" : data.EmployeeStrength);
                command.Parameters.AddWithValue("@EmployeeImprovement", string.IsNullOrWhiteSpace(data.EmployeeImprovement) ? "" : data.EmployeeImprovement);
                command.Parameters.AddWithValue("@CreatedBy", string.IsNullOrWhiteSpace(data.CreatedBy) ? "" : data.CreatedBy);
                command.Parameters.AddWithValue("@AppraisalStatus", string.IsNullOrWhiteSpace(data.AppraisalStatus) ? "" : data.AppraisalStatus);
                command.Parameters.AddWithValue("@AppraisalGrade", string.IsNullOrWhiteSpace(data.AppraisalGrade) ? "" : data.AppraisalGrade);
                command.Parameters.AddWithValue("@AppraisalTotalScore", data.AppraisalTotalScore == null ? null : data.AppraisalTotalScore);

                if (!string.IsNullOrEmpty(data.CreatedDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.CreatedDate;
                    string dateString = dtNew.ToString("dd-MMM-yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@CreateDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                }

                if (!string.IsNullOrEmpty(data.AppraisalPeriodFrom.ToString()))
                {
                    DateTime dtNew = (DateTime)data.AppraisalPeriodFrom;
                    string dateString = dtNew.ToString("dd-MMM-yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@AppraisalPeriodFrom", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@AppraisalPeriodFrom", DateTime.Now);
                }

                if (!string.IsNullOrEmpty(data.AppraisalPeriodTo.ToString()))
                {
                    DateTime dtNew = (DateTime)data.AppraisalPeriodTo;
                    string dateString = dtNew.ToString("dd-MMM-yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@AppraisalPeriodTo", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@AppraisalPeriodTo", DateTime.Now);
                }

                if (!string.IsNullOrEmpty(data.AppraisalDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.AppraisalDate;
                    string dateString = dtNew.ToString("dd-MMM-yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@AppraisalDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@AppraisalDate", DateTime.Now);
                }

                if (!string.IsNullOrEmpty(data.StartDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.StartDate;
                    string dateString = dtNew.ToString("dd-MMM-yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@StartDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                }

                command.Parameters.AddWithValue("@AppraisalDocNo", data.AppraisalDocNo);

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

        public bool DeleteAppraisalDocHeader(INF_APPRAISALDOCHEADER data)
        {
            bool bRet = false;
            try
            {
                string strQuery = "DELETE FROM [AppraisalDocHeader] WHERE [AppraisalDocNo] = @AppraisalDocNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@AppraisalDocNo", data.AppraisalDocNo);

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