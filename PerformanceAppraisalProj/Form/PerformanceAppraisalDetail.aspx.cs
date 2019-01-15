using PerformanceAppraisalProj.BL;
using PerformanceAppraisalProj.BL.Manage;
using PerformanceAppraisalProj.DAL;
using PerformanceAppraisalProj.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceAppraisalProj.Form
{
    public partial class PerformanceAppraisalDetail : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PerformanceAppraisalDetail));       
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
                    hdfEmployeeID.Value = Request.QueryString["EmployeeID"];

                    Inf_UserLoginData userData = new Inf_UserLoginData();
                    userData = (Inf_UserLoginData)Session["UserLoginData"];
                    hdfUserLogin.Value = userData.DisplayName;                  

                    InitialEmpDetail();
                    GetAppraisalHeader();
                    BindGridView();

                    CheckStateActionHistory();

                    DisableButton();
                }
            }
        }

        private void InitialEmpDetail()
        {
            EmployeeMaster_Manage manage = new EmployeeMaster_Manage();
            EmployeeMaster empData = new EmployeeMaster();
            empData = manage.GetData(hdfEmployeeID.Value.Trim());
            if (empData != null)
            {
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

                lblFirstManager.Text = string.IsNullOrWhiteSpace(empData.FirstManager) ?
                    " (First Manager)" : empData.FirstManager.Trim() + " (First Manager)";
                lblSecondManager.Text = string.IsNullOrWhiteSpace(empData.SecondManager) ?
                    " (Second Manager)" : empData.SecondManager.Trim() + " (Second Manager)";

                hdfFirstManager.Value = empData.FirstManager;
                hdfSecondManager.Value = empData.SecondManager;
                hdfFirstManagerMail.Value = empData.FirstManagerMail;
            }
        }

        private void GetAppraisalHeader()
        {
            AppraisalDocHeader_Manage manage = new AppraisalDocHeader_Manage();
            AppraisalDocHeader docHead = new AppraisalDocHeader();
            docHead = manage.GetData(hdfEmployeeID.Value.Trim(), DateTime.Now.Year);
            if (docHead != null)
            {
                lblDocNo.Text = docHead.AppraisalDocNo;
                hdfAppraisalDocNo.Value = docHead.AppraisalDocNo;

                hdfAppraisalStatus.Value = string.IsNullOrEmpty(docHead.AppraisalStatus) ? "" : docHead.AppraisalStatus.Trim();

                lblApprovalStatus.Text = hdfAppraisalStatus.Value.Trim();

                if (docHead.AppraisalPeriodFrom != null)
                {
                    lblDateFrom.Text = docHead.AppraisalPeriodFrom.Value.ToString(@"dd\/MM\/yyyy");
                }
                if (docHead.AppraisalPeriodTo != null)
                {
                    lblDateTo.Text = docHead.AppraisalPeriodTo.Value.ToString(@"dd\/MM\/yyyy");
                }
            }
            else
            {              
                lblDocNo.Text = string.Empty;              
            }           
        }    
        
        private void DisableButton()
        {
            ActionHistory_Manage manage = new ActionHistory_Manage();
            List<ActionHistory> lData = new List<ActionHistory>();
            lData = manage.ListActionHistory(hdfEmployeeID.Value.Trim(), DateTime.Now.Year);
            if (lData != null && lData.Count > 0)
            {
                ActionHistory data = lData.Where(c => c.Responsibility == "Creator").FirstOrDefault();
                lblCreator.Text = string.IsNullOrEmpty(data.CreatedBy) ? "(Creator)" : data.CreatedBy + " (Creator)";
                btnSubmit.Visible = false;
            }
            else
            {
                lblCreator.Text = "(Creator)";
                btnSubmit.Visible = true;
            }
        }   

        private void BindGridView()
        {
            try
            {
                ApprovalHistory_Manage manage = new ApprovalHistory_Manage();
                List<ApprovalHistory> lApproval = new List<ApprovalHistory>();
                lApproval = manage.ListApprovalHistory(hdfAppraisalDocNo.Value.Trim());

                gvActionHistory.DataSource = lApproval;
                gvActionHistory.DataBind();

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }


        private void CheckStateActionHistory()
        {
            try
            {
                if (string.IsNullOrEmpty(hdfAppraisalStatus.Value.Trim()) 
                    || !hdfAppraisalStatus.Value.Trim().Equals("Rejected"))
                {
                    ActionHistory_Manage hisManage = new ActionHistory_Manage();
                    List<ActionHistory> lResult = new List<ActionHistory>();
                    lResult = hisManage.ListActionHistory(hdfEmployeeID.Value.Trim(), DateTime.Now.Year);
                    if (lResult != null && lResult.Count > 0)
                    {
                        string FirstManager = (string.IsNullOrEmpty(hdfFirstManager.Value.Trim()) ? "" : hdfFirstManager.Value.Trim());
                        string SecondManager = (string.IsNullOrEmpty(hdfSecondManager.Value.Trim()) ? "" : hdfSecondManager.Value.Trim());

                        foreach (var item in lResult)
                        {
                            string _status = string.IsNullOrWhiteSpace(item.Status) ? "" : item.Status.Trim();

                            /*   Check status of First Manager */
                            if (item.Responsibility.Trim().Equals("FirstManager") && (_status == ""))
                            {
                                lblFirstManager.ForeColor = System.Drawing.Color.Green;
                            }

                            /* Check status of Seccound Manager */
                            if (item.Responsibility.Trim().Equals("SecondManager") && (_status == ""))
                            {
                                lblSecondManager.ForeColor = System.Drawing.Color.Green;
                            }
                        }
                    }
                }
                else
                {
                    if (hdfAppraisalStatus.Value.Trim().Equals("Rejected"))
                    {
                        lblFirstManager.ForeColor = System.Drawing.Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void gvActionHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvActionHistory.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateAppraisal.aspx", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<EmployeeMaster> lData = new List<EmployeeMaster>();
                EmployeeMaster data = new EmployeeMaster();
                data.EmployeeID = hdfEmployeeID.Value;
                data.FirstManager = hdfFirstManager.Value.Trim(); 
                data.HRStaff = hdfUserLogin.Value;
                data.CreatedBy = hdfUserLogin.Value;
                data.CreatedDate = DateTime.Now;

                data.AppraisalYear = DateTime.Now.Year.ToString();

                lData.Add(data);

                if (lData != null && lData.Count > 0)
                {
                    EmployeeMaster_Manage manage = new EmployeeMaster_Manage();
                    bool ret = manage.UpdateEmployeeMaster(lData);
                    if (ret)
                    {
                        lblMsgResult.Text = "บันทึกข้อมูลเรียบร้อย";
                        lbtnPopup_ModalPopupExtender.Show();

                        string sSubjectMail = ConfigurationManager.GetConfiguration().SubjectMailApprove;
                        string sEmailFrom = ConfigurationManager.GetConfiguration().EmailFrom;
                        string reqDate = data.CreatedDate.Value.ToString(@"dd\/MM\/yyyy");

                        string Email1stManager = hdfFirstManagerMail.Value.Trim();

                        string emaiBody = GenEmailBody(data.EmployeeID, data.FirstManager, data.HRStaff, reqDate);

                        SendMail(sSubjectMail, emaiBody, Email1stManager, sEmailFrom);
                    }
                    else
                    {
                        lblMsgResult.Text = "ไม่สามารถบันทึกข้อมูลได้";
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

        protected void btnOK_Click(object sender, EventArgs e)
        {
            lbtnPopup_ModalPopupExtender.Show();
            Response.Redirect("~/Default.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnOK.Visible = true;
            btnCancel.Visible = false;

            lbtnPopup_ModalPopupExtender.Hide();
        }


        #region #### For Sent Email ####

        private string GenEmailBody(string EmpID, string ManagerName, string ReqName, string ReqDate)
        {
            string _website = ConfigurationManager.GetConfiguration().AppraisalWebsite;
            string _EmpID = EmpID;
            string _1stManagerName = ManagerName;
            string _HRRequestName = ReqName;
            string _RequestDate = ReqDate;
            string _url = _website + "/Form/PerformanceAppraisal.aspx?EmployeeID=" + _EmpID;
            string _status = "Waiting 1st Manager Approve";
            
            string strEmailBody = "<HTML><BODY><table style='font-size: 11.0pt; font-family: Arial,Helvetica,san-serif; color: black;'>" +
                               "<tr><td>Dear K." + _1stManagerName + ",</td></tr>" +
                               "<tr><td height = '20px'></td></tr>" +
                               "<tr><td> You have an approval request as below details.</td></tr>" +
                               "<tr><td height='20px'></td></tr>" +
                               "<tr><td>" +
                               "<table width='100 %' border='1' cellspacing='0' cellpadding='3'>" +
                               "<tr><td> Status </td><td> " + _status + " </td></tr>" +
                               "<tr><td> Employee Name </td><td>" + lblEmployeeName.Text.Trim() + "</td></tr>" +
                               "<tr><td> Position </td><td>" + lblPosition.Text.Trim() + "</td></tr>" +
                               "<tr><td> 1st Manager Name </td><td>" + _1stManagerName + "</td></tr>" +
                               "<tr><td> 2nd Manager Name </td><td>" + lblSecondManager.Text.Trim() + "</td></tr></table>" +
                               "</td></tr>" +
                               "<tr><td height='20px'></td></tr>" +
                               "<tr><td></td></tr>" +
                               "<tr><td>You can take action by following link: " + _url + "</td></tr>" +
                               "<tr><td></td></tr>" +
                               "<tr><td height='50px'></td></tr>" +
                               "<tr><td></td></tr>" +
                               "<tr><td></td></tr>" +
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

        protected void gvActionHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtComments = (TextBox)e.Row.FindControl("txtComments");
                txtComments.ReadOnly = true;
                txtComments.Attributes.Add("style", "width:98%");
                txtComments.Attributes.Add("CssClass", "TextArea");

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
                }
            }
        }
    }
}