using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerformanceAppraisalProj.DAO;
using System.Data;

namespace PerformanceAppraisalProj.BL
{
    public class Inf_AppraisalDocHeaderManage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Inf_AppraisalDocHeaderManage));

        public bool InsertAppraisalDocHeaderForm(INF_APPRAISALDOCHEADER dataHeader, INF_APPRAISALDOCLINE dataLine)
        {
            IDbConnection conn = null;
            IDbTransaction tran = null;

            bool ret = false;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                tran = conn.BeginTransaction(IsolationLevel.Serializable);

                Inf_AppraisalDocHeaderBL bl = new Inf_AppraisalDocHeaderBL(conn, tran);
                Inf_AppraisalDocLineBL lineBL = new Inf_AppraisalDocLineBL(conn, tran);

                /* Step 1. Insert table No.1 */
                string retPKHeader = bl.InsertAppraisalDocHeader(dataHeader);
                
                /* Step 2. Insert table No.2 */
                if (!string.IsNullOrWhiteSpace(retPKHeader))
                {
                    dataLine.AppraisalDocNo = retPKHeader;
                    lineBL.InsertAppraisalDocLine(dataLine);
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }

                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

        /* Exapmle insert list data */
        public bool ListAppraisalDocline(List<INF_APPRAISALDOCLINE> data)
        {
            IDbConnection conn = null;
            bool ret = false;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Inf_AppraisalDocLineBL bl = new Inf_AppraisalDocLineBL(conn);
                foreach (var itemRowData in data)
                {
                    bl.InsertAppraisalDocLine(itemRowData);
                }

                ret = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

        public bool UpdateAppraisalDocHeaderForm(INF_APPRAISALDOCHEADER data)
        {
            IDbConnection conn = null;
            bool ret = false;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Inf_AppraisalDocHeaderBL bl = new Inf_AppraisalDocHeaderBL(conn);
                ret = bl.UpdateAppraisalDocHeader(data);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

        public bool DeleteAppraisalDocHeaderForm(INF_APPRAISALDOCHEADER data)
        {
            IDbConnection conn = null;
            bool ret = false;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Inf_AppraisalDocHeaderBL bl = new Inf_AppraisalDocHeaderBL(conn);
                ret = bl.DeleteAppraisalDocHeader(data);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

    }
}