using PerformanceAppraisalProj.BL.Manage;
using PerformanceAppraisalProj.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceAppraisalProj
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserLoginData"] == null)
                {
                    Response.BufferOutput = true;
                    Response.Redirect("~/SSO.aspx");
                }
                else
                {
                    Inf_UserLoginData userData = new Inf_UserLoginData();
                    userData = (Inf_UserLoginData)Session["UserLoginData"];
                    userData = (Inf_UserLoginData)Session["UserLoginData"];
                    if (userData != null)
                    {
                        string _EmployeeID = userData.EmployeeID.Trim();
                       
                        HREmployee_Manage manage = new HREmployee_Manage();
                        HREmployee hrData = new HREmployee();
                        hrData = manage.GetHREmployeeData(_EmployeeID);
                        if (hrData == null || string.IsNullOrEmpty(hrData.EmployeeID))
                        {
                            lbtnSelEmployee.Visible = false;
                            lbtnPrintForm.Visible = false;
                            lbtnReportSummary.Visible = false;
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
        protected void lbtnTaskList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AppraisalTask.aspx");
        }
        protected void lbtnSelEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateAppraisal.aspx");
        }
        //protected void lbtnComplete_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/CompleteAppraisal.aspx");
        //}
        protected void lblEmployeeMgt_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AppraisalHistory.aspx");
        }
        protected void lbtnPrintForm_Click(object sender, EventArgs e)
        {
            string url = @"http://gulfmax.gulfelectric.co.th/rassamples/en/asp/rPortfolio/HTMLViewers/interactiveViewer_HR.asp?ReportName=C%3A%5CProgram+Files%5CCrystal+Decisions%5CReport+Application+Server+9%5CReports%5CPA_Form%2Erpt";
            //Response.Redirect(url);
            Response.Write("<script>window.open ('" + url + "','_blank');</script>");
        }

        protected void lbtnReportSummary_Click(object sender, EventArgs e)
        {
            string url = @"http://gulfmax.gulfelectric.co.th/rassamples/en/asp/rPortfolio/HTMLViewers/interactiveViewer_HR.asp?ReportName=C%3A%5CProgram+Files%5CCrystal+Decisions%5CReport+Application+Server+9%5CReports%5CPA_Summary%2Erpt";
            //Response.Redirect(url);
            Response.Write("<script>window.open ('" + url + "','_blank');</script>");
        }
    }
}
