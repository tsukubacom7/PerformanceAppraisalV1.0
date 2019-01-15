using PerformanceAppraisalProj.BL.Manage;
using PerformanceAppraisalProj.DAL;
using PerformanceAppraisalProj.Utility;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceAppraisalProj
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SiteMaster));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Inf_UserLoginData userData = new Inf_UserLoginData();
                if (Session["UserLoginData"] == null)
                {
                    Response.BufferOutput = true;
                    Response.Redirect("~/SSO.aspx");
                }
                else
                {
                    userData = (Inf_UserLoginData)Session["UserLoginData"];
                    if (userData != null)
                    {
                        string _EmployeeID = userData.EmployeeID.Trim();
                        string _displayName = userData.DisplayName;
                        lblLoginName.Text = _displayName;

                        HREmployee_Manage manage = new HREmployee_Manage();
                        HREmployee hrData = new HREmployee();
                        hrData = manage.GetHREmployeeData(_EmployeeID);
                        if (hrData == null || string.IsNullOrEmpty(hrData.EmployeeID))
                        {
                            MenuItem parent2 = NavigationMenu.FindItem("2");
                            NavigationMenu.Items.Remove(parent2);

                            MenuItem parent3 = NavigationMenu.FindItem("3");
                            NavigationMenu.Items.Remove(parent3);

                            MenuItem parent5 = NavigationMenu.FindItem("5");
                            NavigationMenu.Items.Remove(parent5);
                        }
                    }
                    else
                    {
                        Response.BufferOutput = true;
                        Response.Redirect("~/Unauthorized.aspx");
                    }
                }                               
            }
        }
    }
}
