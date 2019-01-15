using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PerformanceAppraisalProj.BL
{
    public class ConnectionFactory
    {
        private static DbProviderFactory _factory;
        public static IDbConnection GetConnection()
        {
            if (_factory == null)
            {
                _factory = DbProviderFactories.GetFactory(ConfigurationManager.GetConfiguration().DbProviderName);
            }

            return _factory.CreateConnection();
        }
    }
}