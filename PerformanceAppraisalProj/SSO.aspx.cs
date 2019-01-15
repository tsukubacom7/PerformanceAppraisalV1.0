using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.DirectoryServices;
using System.Data;
using PerformanceAppraisalProj.DAL;
using PerformanceAppraisalProj.Utility;

namespace PerformanceAppraisalProj
{
    public partial class SSO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region #### Old Version ####

            //string username = Thread.CurrentPrincipal.Identity.Name.Split('\\')[1];
            //string adName = @"LDAP://gulf";
            //System.DirectoryServices.DirectoryEntry ldap = new System.DirectoryServices.DirectoryEntry(adName);
            //DirectorySearcher searchUser = new DirectorySearcher(ldap);
            //searchUser.Filter = "(&(objectClass=user)(SAMAccountName=" + username.Trim() + "))";

            //SearchResult result = searchUser.FindOne();

            //if (result != null)
            //{

            //    DirectoryEntry drEntry = result.GetDirectoryEntry();
            //    drEntry.AuthenticationType = AuthenticationTypes.Secure;

            //    //HttpCookie _ASPXEMPID = new HttpCookie("ASPXEMPID");
            //    //HttpCookie _ASPXAUTH = new HttpCookie("ASPXAUTH");
            //    //HttpCookie _ASPXDISPNAME = new HttpCookie("ASPXDISPNAME");
            //    //HttpCookie _ASPXEMPMAIL = new HttpCookie("ASPXEMPMAIL");
            //    //HttpCookie _ASPXCOMPANY = new HttpCookie("ASPXCOMPANY");
            //    //HttpCookie _ASPXDEPT = new HttpCookie("ASPXDEPT");
            //    //HttpCookie _ASPXTITLE = new HttpCookie("ASPXTITLE");
            //    //HttpCookie _ASPXADMGRNAME = new HttpCookie("ASPXMGRNAME");

            //    Inf_UserLoginData userData = new Inf_UserLoginData();

            //    userData.EmployeeID = drEntry.Properties["employeeID"].Value.ToString();
            //    userData.DomainName = drEntry.Properties["SAMAccountName"].Value.ToString();
            //    userData.DisplayName = drEntry.Properties["displayName"].Value.ToString();
            //    userData.Email = drEntry.Properties["mail"].Value.ToString();
            //    userData.Company = drEntry.Properties["company"].Value.ToString();
            //    userData.Department = drEntry.Properties["department"].Value.ToString();
            //    userData.Title = drEntry.Properties["title"].Value.ToString();

            //    if (drEntry.Properties["manager"].Value != null)
            //    {
            //        if (!string.IsNullOrEmpty(drEntry.Properties["manager"].Value.ToString()))
            //        {
            //            string mgr = "";
            //            mgr = drEntry.Properties["manager"].Value.ToString().Remove(0, 3).Replace("\\", " ");
            //            int r = mgr.IndexOf(",");

            //            userData.Manager = mgr.Remove(r);
            //        }
            //    }

            //    Session["UserLoginData"] = userData;

            //    //Response.Cookies.Add(_ASPXEMPID);
            //    //Response.Cookies.Add(_ASPXAUTH);
            //    //Response.Cookies.Add(_ASPXDISPNAME);
            //    //Response.Cookies.Add(_ASPXEMPMAIL);              
            //    //Response.Cookies.Add(_ASPXDEPT);
            //    //Response.Cookies.Add(_ASPXTITLE);
            //    //Response.Cookies.Add(_ASPXCOMPANY);
            //    //ViewState["AUTH"] = _ASPXAUTH.Value;
            //    //ViewState["ADMGRNAME"] = _ASPXADMGRNAME.Value;
            //    //ViewState["ADMGRNAMETRIM"] = mgr.Remove(r).Replace(" ", "");                          

            //    Response.Redirect("~/Default.aspx");
            //    drEntry.Dispose();
            //}
            //else
            //{
            //    Response.BufferOutput = true;
            //    Response.Redirect("~/UnAuthorize.aspx");
            //}

            #endregion
            
            string sCurrURL = (string)Session["CURRURL"];
            string url = string.Empty;
            if (string.IsNullOrEmpty(sCurrURL))
            {
                url = "~/Default.aspx";
            }
            else
            {
                url = sCurrURL;
            }
            Session["CURRURL"] = null;
                     

            string username = string.Empty;
            /********** Local Version ******************/
            //username = Thread.CurrentPrincipal.Identity.Name.Split('\\')[1];
            //username = "preeeecha.ja";
            /******************************************/

            /**********  Production version ***********/
            string _username = HttpContext.Current.User.Identity.Name;
            username = GetUserLogin(_username);
            /******************************************/

            Inf_UserLoginData userData = new Inf_UserLoginData();
            AuthenticateUser autUser = new AuthenticateUser();
            userData = autUser.AuthenUserLogin(username);
            if (userData == null || string.IsNullOrEmpty(userData.DomainName))
            {
                Response.BufferOutput = true;
                Response.Redirect("~/Unauthorized.aspx");
            }
            else
            {
                Session["UserLoginData"] = userData;
                //Response.Redirect("~/Default.aspx");
                Response.Redirect(url);
            }
        }

        private static string GetUserLogin(string s)
        {
            int stop = s.IndexOf("\\");
            return (stop > -1) ? s.Substring(stop + 1, s.Length - stop - 1) : null;
        }
    }   
}