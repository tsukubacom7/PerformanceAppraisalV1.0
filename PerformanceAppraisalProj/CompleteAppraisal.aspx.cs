using PerformanceAppraisalProj.BL.Manage;
using PerformanceAppraisalProj.DAL;
using PerformanceAppraisalProj.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceAppraisalProj
{
    public partial class CompleteAppraisal : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CompleteAppraisal));
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
                    hdfUserLogin.Value = userData.DisplayName;

                    InitialDDlCompay();
                    InitialDDlDepartment();
                    BindingGrid();
                }
            }
        }

        private void InitialDDlCompay()
        {
            try
            {
                /********* Find User from AD  *************/
                List<CompanyMaster> lCompany = new List<CompanyMaster>();
                CompanyMaster_Manage manager = new CompanyMaster_Manage();
                lCompany = manager.ListCompanyData();

                ddlCompany.DataSource = lCompany;
                ddlCompany.DataBind();

                ddlCompany.Items.Insert(0, new ListItem("== Select ==", ""));

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void InitialDDlDepartment()
        {
            try
            {
                /********* Find User from AD  *************/
                List<DepartmentMaster> lDept = new List<DepartmentMaster>();
                DepartmentMaster_Manage manager = new DepartmentMaster_Manage();
                lDept = manager.ListDepartmentData();

                ddlDepartment.DataSource = lDept;
                ddlDepartment.DataBind();

                ddlDepartment.Items.Insert(0, new ListItem("== Select ==", ""));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void BindingGrid()
        {
            try
            {
                string sName = string.Empty;
                string sEmpID = string.Empty;
                string sCompany = string.Empty;
                string sDepartment = string.Empty;

                if (!string.IsNullOrEmpty(txtEmpName.Text.Trim()))
                {
                    sName = txtEmpName.Text.Trim();
                }
                if (!string.IsNullOrEmpty(txtEmpID.Text.Trim()))
                {
                    sEmpID = txtEmpID.Text.Trim();
                }
                if (ddlCompany.SelectedIndex > 0)
                {
                    sCompany = ddlCompany.SelectedValue.Trim();
                }
                if (ddlDepartment.SelectedIndex > 0)
                {
                    sDepartment = ddlDepartment.SelectedItem.ToString().Trim();
                }
               
                EmployeeMaster_Manage manager = new EmployeeMaster_Manage();

                List<Inf_TaskListData> lEmployee = new List<Inf_TaskListData>();
                lEmployee = manager.ListCompleteEmployeeData();

                if (!string.IsNullOrEmpty(sName))
                {
                    lEmployee = lEmployee.Where(c => c.EmployeeName.Contains(sName)).ToList();
                }

                if (!string.IsNullOrEmpty(sEmpID))
                {
                    lEmployee = lEmployee.Where(c => c.EmployeeID.Contains(sEmpID)).ToList();
                }

                if (!string.IsNullOrEmpty(sCompany))
                {
                    lEmployee = lEmployee.Where(c => c.CompanyID.Contains(sCompany)).ToList();
                }

                if (!string.IsNullOrEmpty(sDepartment))
                {
                    lEmployee = lEmployee.Where(c => c.DepartmentName.Contains(sDepartment)).ToList();
                }

                gvEmployee.DataSource = lEmployee;
                gvEmployee.DataBind();

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindingGrid();
        }

        protected void gvEmployee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearCriterai();
            BindingGrid();
        }

        private void ClearCriterai()
        {
            txtEmpID.Text = string.Empty;
            txtEmpName.Text = string.Empty;
            ddlCompany.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
        }

        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmployee.PageIndex = e.NewPageIndex;
            BindingGrid();
        }

        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
                        
        }
    }
}
