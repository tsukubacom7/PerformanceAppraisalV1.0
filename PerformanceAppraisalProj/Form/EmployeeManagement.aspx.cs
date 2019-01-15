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
    public partial class EmployeeManagement : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(EmployeeManagement));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string _CurrUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                Session["CURRURL"] = _CurrUrl;

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
                    
                    InitialGridviewSUser();
                    pnSearchResult.Visible = false;
                }
            }
        }         

        private void InitialEmpDetail()
        {
            EmployeeMaster_Manage manage = new EmployeeMaster_Manage();
            EmployeeMaster empData = new EmployeeMaster();
            empData = manage.GetData(hdfEmployeeID.Value.Trim());

            lblEmployeeName.Text = empData.EmployeeName;
            lblDepartment.Text = empData.DepartmentName;
            lblPosition.Text = empData.Position;
            if (empData.StartDate != null)
            {
                lblJoinDate.Text = empData.StartDate.Value.ToString(@"dd\/MM\/yyyy");
            }
            if (empData.ContractStart != null)
            {
                lblContractStart.Text = empData.ContractStart.Value.ToString(@"dd\/MM\/yyyy");
            }
            if (empData.ContractEnd != null)
            {
                lblContractEnd.Text = empData.ContractEnd.Value.ToString(@"dd\/MM\/yyyy");
            }

            //hdfCompanyID.Value = empData.CompanyID;
            txt2ndMgtName.Text = empData.SecondManager;
            txt2ndMgtEmail.Text = empData.SecondManagerMail;

            txt1stMgtName.Text = empData.FirstManager;
            txt1stMgtEmail.Text = empData.FirstManagerMail;
        }  

        private void ClearCriterai()
        {
            //txtEmpName.Text = string.Empty;
        }

        #region "Popup Search User Event"
        protected void ibtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            lbtnPopup_ModalPopupExtender.Show();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnSearchResult.Visible = false;

            lbtnPopup_ModalPopupExtender.Hide();
            InitialGridviewSUser();
            ClearCriterai();
        }       
        protected void ibtnFind_Click(object sender, ImageClickEventArgs e)
        {
            lbtnPopup_ModalPopupExtender.Show();
            BindGridViewUser();
        }
        protected void gvSearchUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            lbtnPopup_ModalPopupExtender.Show();
            gvSearchUser.PageIndex = e.NewPageIndex;

            BindGridViewUser();
        }
        protected void gvSearchUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                string _EmpName = e.CommandArgument.ToString().Trim().Split(',')[0];
                string _EmpID = e.CommandArgument.ToString().Trim().Split(',')[1];

                hdfEmployeeID.Value = _EmpID;
                //txtEmpName.Text = _EmpName;

                txtSearch.Text = string.Empty;

                InitialGridviewSUser();
                InitialEmpDetail();

                lbtnPopup_ModalPopupExtender.Hide();
                pnSearchResult.Visible = true;
            }
        }

        #endregion

        private void BindGridViewUser()
        {
            try
            {
                /********* Find User from TB EmployeeMaster  *************/
                EmployeeMaster_Manage manage = new EmployeeMaster_Manage();
                List<EmployeeMaster> lUser = new List<EmployeeMaster>();
                lUser = manage.ListEmployeeByName(txtSearch.Text.Trim());

                gvSearchUser.DataSource = lUser;
                gvSearchUser.DataBind();

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void InitialGridviewSUser()
        {
            List<EmployeeMaster> lUser = new List<EmployeeMaster>();
            gvSearchUser.DataSource = lUser;
            gvSearchUser.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool ret = false;
            try
            {
                EmployeeMaster_Manage manage = new EmployeeMaster_Manage();
                EmployeeMaster updData = new EmployeeMaster();

                updData.EmployeeID = hdfEmployeeID.Value.Trim();
                updData.FirstManager = txt1stMgtName.Text.Trim();
                updData.FirstManagerMail = txt1stMgtEmail.Text.Trim();
                updData.SecondManager = txt2ndMgtName.Text.Trim();
                updData.SecondManagerMail = txt2ndMgtEmail.Text.Trim();

                updData.CreatedBy = hdfUserLogin.Value.Trim();
                updData.CreatedDate = DateTime.Now;

                ret = manage.UpdateEmployeeData(updData);
                if (ret)
                {
                    lblResult.Text = "Update employee data completed.";
                }
                else
                {
                    lblResult.Text = "Can not update employee data.";
                }

                lbtnPopupApp_ModalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            lbtnPopupApp_ModalPopupExtender.Hide();
        }
    }
}
