using PerformanceAppraisalProj.DAL;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace PerformanceAppraisalProj.Utility
{
    public class AuthenticateUser
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AuthenticateUser));
        public Inf_UserLoginData AuthenUserLogin(string username)
        {
            Inf_UserLoginData userData = new Inf_UserLoginData();
            try
            {
                string adName = @"LDAP://gulf";
                System.DirectoryServices.DirectoryEntry ldap = new System.DirectoryServices.DirectoryEntry(adName);
                DirectorySearcher searchUser = new DirectorySearcher(ldap);
                searchUser.Filter = "(&(objectClass=user)(SAMAccountName=" + username.Trim() + "))";

                SearchResult result = searchUser.FindOne();

                if (result != null)
                {
                    DirectoryEntry drEntry = result.GetDirectoryEntry();
                    drEntry.AuthenticationType = AuthenticationTypes.Secure;

                    userData.EmployeeID = drEntry.Properties["employeeID"].Value == null ? "" : drEntry.Properties["employeeID"].Value.ToString();
                    userData.DomainName = drEntry.Properties["SAMAccountName"].Value == null ? "" : drEntry.Properties["SAMAccountName"].Value.ToString();
                    userData.DisplayName = drEntry.Properties["displayName"].Value == null ? "" : drEntry.Properties["displayName"].Value.ToString();
                    userData.Email = drEntry.Properties["mail"].Value == null ? "" : drEntry.Properties["mail"].Value.ToString();
                    userData.Company = drEntry.Properties["company"].Value == null ? "" : drEntry.Properties["company"].Value.ToString();
                    userData.Department = drEntry.Properties["department"].Value == null ? "" : drEntry.Properties["department"].Value.ToString();
                    userData.Title = drEntry.Properties["title"].Value == null ? "" : drEntry.Properties["title"].Value.ToString();

                    if (drEntry.Properties["manager"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(drEntry.Properties["manager"].Value.ToString()))
                        {
                            string mgr = "";
                            mgr = drEntry.Properties["manager"].Value.ToString().Remove(0, 3).Replace("\\", " ");
                            int r = mgr.IndexOf(",");

                            userData.Manager = mgr.Remove(r);
                        }
                    }

                    drEntry.Dispose();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return userData;
        }

    }
}