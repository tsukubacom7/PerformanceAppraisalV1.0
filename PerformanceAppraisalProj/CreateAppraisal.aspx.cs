using PerformanceAppraisalProj.BL;
using PerformanceAppraisalProj.BL.Manage;
using PerformanceAppraisalProj.DAL;
using PerformanceAppraisalProj.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceAppraisalProj
{
    public partial class CreateAppraisal : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CreateAppraisal));
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

                List<EmployeeMaster> lEmployee = new List<EmployeeMaster>();
                EmployeeMaster_Manage manager = new EmployeeMaster_Manage();

                lEmployee = manager.ListEmployeeData();

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<EmployeeMaster> lData = new List<EmployeeMaster>();
                foreach (GridViewRow row in gvEmployee.Rows)
                {
                    CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;
                    if (chkSelect.Checked)
                    {
                        EmployeeMaster data = new EmployeeMaster();

                        HiddenField hdfFirstManager = row.FindControl("hdfFirstManager") as HiddenField;
                        HiddenField hdfSecondManager = row.FindControl("hdfSecondManager") as HiddenField;
                        HiddenField hdfEmployeeID = row.FindControl("hdfEmployeeID") as HiddenField;

                        HiddenField hdfFirstManagerMail = row.FindControl("hdfFirstManagerMail") as HiddenField;
                        HiddenField hdfSecondManagerMail = row.FindControl("hdfSecondManagerMail") as HiddenField;
                        HiddenField hdfEmployeeName = row.FindControl("hdfEmployeeName") as HiddenField;
                        HiddenField hdfPosition = row.FindControl("hdfPosition") as HiddenField;
                      
                        data.EmployeeID = hdfEmployeeID.Value;
                        data.FirstManager = hdfFirstManager.Value;
                        data.SecondManager = hdfSecondManager.Value;
                        data.HRStaff = hdfUserLogin.Value;
                        data.CreatedBy = hdfUserLogin.Value;
                        data.CreatedDate = DateTime.Now;

                        data.FirstManagerMail = hdfFirstManagerMail.Value.Trim();
                        data.SecondManagerMail = hdfSecondManagerMail.Value.Trim();
                        data.EmployeeName = hdfEmployeeName.Value;
                        data.AppraisalYear = DateTime.Now.Year.ToString();
                        data.Position = hdfPosition.Value;

                        lData.Add(data);
                    }
                }

                if (lData != null && lData.Count > 0)
                {
                    EmployeeMaster_Manage manage = new EmployeeMaster_Manage();
                    bool ret = manage.UpdateEmployeeMaster(lData);
                    if (ret)
                    {
                        lblMsgResult.Text = "บันทึกข้อมูลเรียบร้อย";
                        lbtnPopup_ModalPopupExtender.Show();

                        SendEamilTo1stManager(lData);
                    }
                    else
                    {
                        lblMsgResult.Text = "ไม่สามารถบันทึกข้อมูลได้";
                        btnOK.Visible = false;
                        btnCancel.Visible = true;

                        lbtnPopup_ModalPopupExtender.Show();
                    }
                }
                else
                {
                    lblMsgResult.Text = "กรุณาเลือกพนักงานที่ต้องการประเมิน";
                    btnOK.Visible = false;
                    btnCancel.Visible = true;
                    lbtnPopup_ModalPopupExtender.Show();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
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
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdfAppraisalDate = (HiddenField)e.Row.FindControl("hdfAppraisalDate");
                    HiddenField hdfHRStaff = (HiddenField)e.Row.FindControl("hdfHRStaff");
                    HiddenField hdfCreatedDate = (HiddenField)e.Row.FindControl("hdfCreatedDate");

                    if (!string.IsNullOrEmpty(hdfCreatedDate.Value))
                    {
                        DateTime dtcraete = new DateTime();
                        dtcraete = Convert.ToDateTime(hdfCreatedDate.Value);
                        if (dtcraete.Year.Equals(DateTime.Now.Year) && !string.IsNullOrEmpty(hdfHRStaff.Value.Trim()))
                        {
                            var chkSelect = e.Row.FindControl("chkSelect") as CheckBox;
                            chkSelect.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            lbtnPopup_ModalPopupExtender.Hide();
            Response.Redirect("~/Default.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnOK.Visible = true;
            btnCancel.Visible = false;
            lbtnPopup_ModalPopupExtender.Hide();
        }

        #region #### For Send Email ####

        private void SendEamilTo1stManager(List<EmployeeMaster> lData)
        {
            string sSubjectMail = ConfigurationManager.GetConfiguration().SubjectMailApprove;
            string sEmailFrom = ConfigurationManager.GetConfiguration().EmailFrom;
            try
            {
                if (lData != null && lData.Count > 0)
                {
                    foreach (var item in lData)
                    {
                        string _EmailFirst = item.FirstManagerMail.Trim();
                        string reqDate = item.CreatedDate.Value.ToString(@"dd\/MM\/yyyy");
                        string _EmployeeName = item.EmployeeName;
                        if (!string.IsNullOrEmpty(_EmailFirst))
                        {
                            string _EmailBody = GenEmailBody(item.EmployeeID, item.FirstManager, item.HRStaff, reqDate, _EmployeeName,
                                item.Position, "Waiting 1st Manager Approve", item.SecondManager);

                            SendMail(sSubjectMail, _EmailBody, _EmailFirst, sEmailFrom);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }


        private string GenEmailBody(string EmpID, string ManagerName, string ReqName, string ReqDate, string EmployeeName,
            string Position, string Status, string SecManagerName)
        {
           
            string _website = ConfigurationManager.GetConfiguration().AppraisalWebsite;
            string _EmpID = EmpID;
           
            string _HRRequestName = ReqName;
            string _RequestDate = ReqDate;
            string _url = _website + "/Form/PerformanceAppraisal.aspx?EmployeeID=" + _EmpID;

            string _status = Status;
            string _1stManagerName = ManagerName;
            string _2ndManagerName = SecManagerName;
            string _employeeName = EmployeeName;
            string _position = Position;

            #region #### Old Version ####
            //string strEmailBody = "<HTML><BODY>" +
            //    "<table style='font-size: 11.0pt; font-family: Arial,Helvetica,san-serif; color: black;'>" +
            //    "<tr><td> Dear K. " + _1stManagerName + ", </td></tr><br />" +
            //    "<tr><td> Please process this request as detail below: </td></tr><br />" +
            //    "<tr><td> Requested by: " + _HRRequestName + "</td></tr>" +
            //    "<tr><td> Employee ID: " + _EmpID + "</td></tr>" +
            //    "<tr><td> Employee Name: " + EmployeeName + "</td></tr>" +
            //    "<tr><td></td></tr>" +
            //    "<tr><td> Objective:[Approve] Performance Appraisal</td></tr>" +
            //    "<tr><td> Request Date: " + _RequestDate + "</td></tr>" +
            //    "<tr><td> You can action by: " + _url + "</td></tr><br />" +
            //    "<tr><td></td></tr>" +
            //    "<tr><td height='50px'></td></tr>" +
            //    "<tr><td>Best regards,</td></tr>" +
            //    "</table></BODY></HTML>";
            #endregion

            string strEmailBody = "<HTML><BODY><table style='font-size: 11.0pt; font-family: Arial,Helvetica,san-serif; color: black;'>" +
                                "<tr><td>Dear K." + _1stManagerName + ",</td></tr>" +
                                "<tr><td height = '20px'></td></tr>" +
                                "<tr><td> You have an approval request as below details.</td></tr>" +
                                "<tr><td height='20px'></td></tr>" +
                                "<tr><td>" +
                                "<table width='100 %' border='1' cellspacing='0' cellpadding='3'>" +
                                "<tr><td> Status </td><td> " + _status + " </td></tr>" +
                                "<tr><td> Employee Name </td><td>" + _employeeName + "</td></tr>" +
                                "<tr><td> Position </td><td>" + _position + "</td></tr>" +
                                "<tr><td> 1st Manager Name </td><td>" + _1stManagerName + "</td></tr>" +
                                "<tr><td> 2nd Manager Name </td><td>" + _2ndManagerName + "</td></tr></table>" +
                                "</td></tr>" +
                                "<tr><td height='20px'></td></tr>" +
                                "<tr><td></td></tr>" +
                                "<tr><td>You can take action by following link: " + _url + "</td></tr>" +
                                "<tr><td></td></tr>" +
                                "<tr><td></td></tr>" +
                                "<tr><td></td></tr>" +
                                "<tr><td height='50px'></td></tr>" +
                                "<tr><td>Best regards,</ td ></tr></table></BODY></HTML>";

            return strEmailBody;
        }

        private void SendMail(string Subject, string BodyMail, string EmailTo, string EmailFrom)
        {
            try
            {
                logger.Info("SendMailUserPassword: Start");

                string sSubjectMail = Subject;
                string sBodyMail = BodyMail;

                EmailNotification email = new EmailNotification();
                email.EmailSubject = sSubjectMail;
                email.EmailForm = EmailFrom;
                email.EmailSMTP = ConfigurationManager.GetConfiguration().EmailSMTP;
                email.EmailPort = int.Parse(ConfigurationManager.GetConfiguration().EmailPort);
                //**************************************//
                //ArrayList aEmailTo = new ArrayList();
                //aEmailTo.Add(EmailTo);  
                ArrayList aEmailTo = new ArrayList();
                string[] _EmailTo = null;
                _EmailTo = EmailTo.Split(';');
                foreach (string sEmailTo in _EmailTo)
                {
                    aEmailTo.Add(sEmailTo);
                }
                //**************************************//
                email.EmailTo = aEmailTo;
                email.EmailBody = sBodyMail;

                email.SendEmail();

                logger.Info("SendMailUserPassword: Complete");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        #endregion
    }
}
