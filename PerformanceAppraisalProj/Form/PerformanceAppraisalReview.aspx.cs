using PerformanceAppraisalProj.BL;
using PerformanceAppraisalProj.BL.Manage;
using PerformanceAppraisalProj.DAL;
using PerformanceAppraisalProj.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceAppraisalProj.Form
{
    public partial class PerformanceAppraisalReview : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PerformanceAppraisalReview));
       
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

                    hdfEmployeeID.Value = Request.QueryString["EmployeeID"];

                    InitialEmpDetail();
                    GetAppraisalHeader();

                    BindGridView1();
                    BindGridView2();

                    BindGVApprovalHist();
                    BindGVAppraisalHistory();

                    lblTotalPart1.Text = "50 คะแนน";
                    lblTotalPart2.Text = "50 คะแนน";
                    lblTotalPoint.Text = "100 คะแนน";

                    if (!string.IsNullOrEmpty(lblActualPart1.Text.Trim()) && 
                        !string.IsNullOrEmpty(lblActualPart2.Text.Trim()))
                    {
                        lblActualPoint.Text = (Convert.ToDecimal(lblActualPart1.Text.Trim()) + Convert.ToDecimal(lblActualPart2.Text.Trim())).ToString();
                    }

                    Session["tbAttachFile"] = null;

                    InitialGVAttachFile();
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

                hdfFirstManagerMail.Value = empData.FirstManagerMail;
                hdfFirstManager.Value = empData.FirstManager;

                hdfSecondManager.Value = empData.SecondManager;

                if (empData.StartDate != null)
                {
                    lblJoinDate.Text = empData.StartDate.Value.ToString(@"dd\/MM\/yyyy");
                }
            }
            else
            {
                btnSubmit.Visible = false;
                btnReject.Visible = false;
                lbtnPopupErr_ModalPopupExtender.Show();
            }          
        }

        private void InitialGVAttachFile()
        {
            try
            {
                string _EmployeeID = hdfEmployeeID.Value.Trim();

                EmployeeMaster_Manage manage = new EmployeeMaster_Manage();
                List<Attachment> lAttachment = new List<Attachment>();
                lAttachment = manage.listAttachfile(_EmployeeID);

                DataTable dtAttachment = (DataTable)Session["tbAttachFile"];

                if (lAttachment!= null && lAttachment.Count > 0)
                {
                    dtAttachment = new DataTable();
                    DataRow dr;

                    dtAttachment.Clear();

                    dtAttachment.Columns.Add("FileName", typeof(string));
                    dtAttachment.Columns.Add("Description", typeof(string));
                    dtAttachment.Columns.Add("AttachFilePath", typeof(string));
                    dtAttachment.Columns.Add("EmployeeID", typeof(string));                   

                    foreach (var item in lAttachment)
                    {
                        dr = dtAttachment.NewRow();

                        dr["FileName"] = item.FileName;
                        dr["Description"] = string.Empty;
                        dr["AttachFilePath"] = item.Attachment1;
                        dr["EmployeeID"] = item.EmployeeID;

                        dtAttachment.Rows.Add(dr);
                    }
                }

                Session["tbAttachFile"] = dtAttachment;

                gvAttachFile.DataSource = dtAttachment;
                gvAttachFile.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void GetAppraisalHeader()
        {
            AppraisalDocHeader_Manage manage = new AppraisalDocHeader_Manage();
            AppraisalDocHeader docHead = new AppraisalDocHeader();
            docHead = manage.GetData(hdfEmployeeID.Value.Trim(), DateTime.Now.Year);
            if (docHead != null)
            {
                //**** For Check Duplicate Job ****//
                if (docHead.AppraisalStatus.Trim().ToLower().Equals("completed"))
                {
                    btnSubmit.Visible = false;
                    btnReject.Visible = false;
                    lbtnPopupErr_ModalPopupExtender.Show();
                }
                else
                {
                    if (docHead.AppraisalStatus.Trim().Equals("Rejected"))
                    {
                        ActionHistory_Manage hm = new ActionHistory_Manage();
                        string _staus = hm.GetActionHistoryStatus(hdfAppraisalDocNo.Value.Trim(), "SecondManager");
                        if (string.IsNullOrEmpty(_staus))
                        {
                            btnSubmit.Visible = false;
                            btnReject.Visible = false;
                            lbtnPopupErr_ModalPopupExtender.Show();
                        }
                        else
                        {
                            btnSubmit.Visible = true;
                            btnReject.Visible = true;
                        }
                    }
                    else
                    {
                        btnSubmit.Visible = true;
                        btnReject.Visible = true;
                    }
                }
                //*******************************//

                lblAppraisalDocNo.Text = docHead.AppraisalDocNo;
                hdfAppraisalDocNo.Value = docHead.AppraisalDocNo;

                txtEmpStrength.Text = docHead.EmployeeStrength.Trim();
                txtEmpImpovement.Text = docHead.EmployeeImprovement.Trim();              

                if (docHead.AppraisalPeriodFrom != null)
                {
                    lblDateFrom.Text = docHead.AppraisalPeriodFrom.Value.ToString(@"dd\/MM\/yyyy");
                }
                if (docHead.AppraisalPeriodTo != null)
                {
                    lblDateTo.Text = docHead.AppraisalPeriodTo.Value.ToString(@"dd\/MM\/yyyy");
                }

                lblResultScore.Text = string.IsNullOrEmpty(docHead.AppraisalGrade) ? "" : "Band " + docHead.AppraisalGrade.Trim();
            }
            else
            {
                lblAppraisalDocNo.Text = "(Creator)";
                //********** Check case click link from old email ***// 
                btnSubmit.Visible = false;
                btnReject.Visible = false;
                lbtnPopupErr_ModalPopupExtender.Show();
                //***************************************************//
            }
        }

        private void BindGridView1()
        {
            try
            {
                List<AppraisalDocLine> lUser = new List<AppraisalDocLine>();
                AppraisalDocLine_Manage manager = new AppraisalDocLine_Manage();

                lUser = manager.ListAppraisalDocLine(hdfAppraisalDocNo.Value.Trim());
                lUser = lUser.Where(t => t.QuestionType == 1).ToList();

                gvAppraisalPart1.DataSource = lUser;
                gvAppraisalPart1.DataBind();


                if(lUser!= null && lUser.Count > 0)
                {
                    decimal scoreType1 = 0;
                    foreach (var item in lUser)
                    {
                        //decimal dScore = Convert.ToDecimal(item.CalculatedScore * 10);
                        decimal dScore = Convert.ToDecimal(item.CalculatedScore);
                        scoreType1 += dScore;
                    }
                    
                    lblActualPart1.Text = scoreType1.ToString();
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void BindGridView2()
        {
            try
            {
                List<AppraisalDocLine> lUser = new List<AppraisalDocLine>();
                AppraisalDocLine_Manage manager = new AppraisalDocLine_Manage();

                lUser = manager.ListAppraisalDocLine(hdfAppraisalDocNo.Value.Trim());
                lUser = lUser.Where(t => t.QuestionType == 2).ToList();

                gvAppraisalPart2.DataSource = lUser;
                gvAppraisalPart2.DataBind();


                if (lUser != null && lUser.Count > 0)
                {
                    decimal scoreType2 = 0;
                    foreach (var item in lUser)
                    {
                        //decimal dScore = Convert.ToDecimal(item.CalculatedScore * 10);
                        decimal dScore = Convert.ToDecimal(item.CalculatedScore);
                        scoreType2 += dScore;
                    }
                    
                    lblActualPart2.Text = scoreType2.ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void BindGVApprovalHist()
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

        protected void gvAppraisalPart1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void gvAppraisalPart1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
                }
            }
        }       

        protected void ddlPart1Result_SelectedIndexChanged1(object sender, EventArgs e)
        {
        }

        protected void ddlPart2Result_SelectedIndexChanged(object sender, EventArgs e)
        {          
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                /************* Creaete Object Appraisal Doc Line *********/
                AppraisalDocHeader dataHeader = new AppraisalDocHeader();
                dataHeader.EmployeeID = hdfEmployeeID.Value.Trim();
                dataHeader.AppraisalStatus = "Completed";
                dataHeader.AppraisalDocNo = hdfAppraisalDocNo.Value.Trim();
                dataHeader.CreatedDate = DateTime.Now;
                dataHeader.CreatedBy = hdfUserLogin.Value.Trim();
                /*********************************************************/

                //***************** List Attach File for insert to Database *********
                List<Attachment> lAttachFile = new List<Attachment>();
                DataTable dtUpload = (DataTable)Session["tbAttachFile"];
                if (dtUpload != null && dtUpload.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpload.Rows.Count; i++)
                    {
                        Attachment attachData = new Attachment();

                        attachData.FileName = dtUpload.Rows[i]["FileName"].ToString();
                        attachData.EmployeeID = dtUpload.Rows[i]["EmployeeID"].ToString();
                        attachData.FileDescription = dtUpload.Rows[i]["Description"].ToString();
                        attachData.Attachment1 = dtUpload.Rows[i]["AttachFilePath"].ToString();
                        attachData.CreatedDate = DateTime.Now;
                        attachData.CreatedBy = hdfUserLogin.Value.Trim();

                        lAttachFile.Add(attachData);
                    }
                }
                //**********************************************************************

                /********************** Insert to DataBase ***************/
                AppraisalDocHeader_Manage manage = new AppraisalDocHeader_Manage();
                bool insResult = manage.Upd2ndManagerAppr(dataHeader, txtRemark.Text, lAttachFile);
                if (insResult)
                {
                    lblMsgResult.Text = "บันทึกข้อมูลเรียบร้อย";
                    lbtnPopup_ModalPopupExtender.Show();                 
                }
                else
                {
                    lblMsgResult.Text = "ไม่สามารถบันทึกข้อมูลได้";

                    btnOK.Visible = false;
                    btnCancel.Visible = true;

                    lbtnPopup_ModalPopupExtender.Show();
                }
                /********************************************************/
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                //*** Show Popup When Error (Exception) *****
                lblMsgResult.Text = "ไม่สามารถบันทึกข้อมูลได้ กรุณาติดต่อผู้ดูแลระบบ";

                btnOK.Visible = false;
                btnCancel.Visible = true;

                lbtnPopup_ModalPopupExtender.Show();
                //*********************************
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                /************* Creaete Object Appraisal Doc Line *********/
                AppraisalDocHeader dataHeader = new AppraisalDocHeader();
                dataHeader.EmployeeID = hdfEmployeeID.Value.Trim();
                dataHeader.AppraisalStatus = "Rejected";
                dataHeader.AppraisalDocNo = hdfAppraisalDocNo.Value.Trim();
                dataHeader.CreatedDate = DateTime.Now;
                dataHeader.CreatedBy = hdfUserLogin.Value.Trim();
                /*********************************************************/

                //***************** List Attach File for insert to Database *********
                List<Attachment> lAttachFile = new List<Attachment>();
                DataTable dtUpload = (DataTable)Session["tbAttachFile"];
                if (dtUpload != null && dtUpload.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpload.Rows.Count; i++)
                    {
                        Attachment attachData = new Attachment();

                        attachData.FileName = dtUpload.Rows[i]["FileName"].ToString();
                        attachData.EmployeeID = dtUpload.Rows[i]["EmployeeID"].ToString();
                        attachData.FileDescription = dtUpload.Rows[i]["Description"].ToString();
                        attachData.Attachment1 = dtUpload.Rows[i]["AttachFilePath"].ToString();
                        attachData.CreatedDate = DateTime.Now;
                        attachData.CreatedBy = hdfUserLogin.Value.Trim();

                        lAttachFile.Add(attachData);
                    }
                }
                //**********************************************************************

                /********************** Insert to DataBase ***************/
                AppraisalDocHeader_Manage manage = new AppraisalDocHeader_Manage();
                bool insResult = manage.Upd2ndManagerReject(dataHeader, txtRemark.Text, lAttachFile);
                if (insResult)
                {
                    lblMsgResult.Text = "บันทึกข้อมูลเรียบร้อย";
                    lbtnPopup_ModalPopupExtender.Show();

                    string sSubjectMail = ConfigurationManager.GetConfiguration().SubjectMailApprove;
                    string sEmailFrom = ConfigurationManager.GetConfiguration().EmailFrom;
                    string reqDate = DateTime.Now.ToString(@"dd\/MM\/yyyy");

                    string Email1stManager = hdfFirstManagerMail.Value.Trim();
                    if (!string.IsNullOrEmpty(Email1stManager))
                    {
                        string _emaployeeName = lblEmployeeName.Text;
                        string emaiBody = GenEmailBody(hdfEmployeeID.Value.Trim(), hdfFirstManager.Value.Trim(), hdfUserLogin.Value.Trim(), reqDate, _emaployeeName);
                        EmailNotification mailFunc = new EmailNotification();
                        mailFunc.SendMailNotice(sSubjectMail, emaiBody, Email1stManager, sEmailFrom);
                    }
                }
                else
                {
                    lblMsgResult.Text = "ไม่สามารถบันทึกข้อมูลได้";
                    lbtnPopup_ModalPopupExtender.Show();
                }
                /********************************************************/
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
            BindGVApprovalHist();
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

        #region #### For Sent Email ####
        private string GenEmailBody(string EmpID, string ManagerName, string ReqName, string ReqDate, string EmployeeName)
        {
            string _website = ConfigurationManager.GetConfiguration().AppraisalWebsite;
            string _EmpID = EmpID;           
            string _HRRequestName = ReqName;
            string _RequestDate = ReqDate;
            string _url = _website + "/Form/PerformanceAppraisalReject.aspx?EmployeeID=" + _EmpID;

            string _1ndManagerName = hdfFirstManager.Value.Trim();
            string _2ndManagerName = hdfSecondManager.Value.Trim();
            string _position = lblPosition.Text.Trim();

            string strEmailBody = "<HTML><BODY>" +
                                 "<table style='font-size: 11.0pt; font-family: Arial,Helvetica,san-serif; color: black;'>" +
                                 "<tr><td>Dear K." + ManagerName + ",</td></tr>" +
                                 "<tr><td height = '20px'></td></tr>" +
                                 "<tr><td> You have an approval request as below details.</td></tr>" +
                                 "<tr><td height='20px'></td></tr>" +
                                 "<tr><td>" +
                                 "<table width='100 %' border='1' cellspacing='0' cellpadding='3'>" +
                                 "<tr><td> Status </td><td> Waiting 2nd Manager Approve </td></tr>" +
                                 "<tr><td> Employee Name </td><td>" + EmployeeName + "</td></tr>" +
                                 "<tr><td> Position </td><td>" + _position + "</td></tr>" +
                                 "<tr><td> 1st Manager Name </td><td>" + _1ndManagerName + "</td></tr>" +
                                 "<tr><td> 2nd Manager Name </td><td>" + _2ndManagerName + "</td></tr></table>" +
                                 "</td></tr>" +
                                 "<tr><td height='20px'></td></tr>" +
                                 "<tr><td></td></tr>" +
                                 "<tr><td>You can take action by following link: " + _url + "</td></tr>" +
                                 "<tr><td></td></tr>" +
                                 "<tr><td height='50px'></td></tr>" +
                                 "<tr><td></td></tr>" +
                                 "<tr><td></td></tr>" +
                                 "<tr><td>Best regards,</ td ></tr></table>" +
                                 "</BODY></HTML>";

            return strEmailBody;
        }
        #endregion

        private void BindGVAppraisalHistory()
        {
            try
            {
                /********* Find User from AD  *************/
                List<AppraisalDocHeader> lUser = new List<AppraisalDocHeader>();
                AppraisalDocHeader_Manage manager = new AppraisalDocHeader_Manage();
                lUser = manager.ListDocHeaderByID(hdfEmployeeID.Value.Trim());

                gvEmployee.DataSource = lUser;
                gvEmployee.DataBind();

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmployee.PageIndex = e.NewPageIndex;
            BindGVAppraisalHistory();
        }

        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HyperLink hplClick = (HyperLink)e.Row.FindControl("hplClick");
                    HiddenField hdfAppraisalDate = (HiddenField)e.Row.FindControl("hdfAppraisalDate");
                    HiddenField hdfEmployeeID = (HiddenField)e.Row.FindControl("hdfEmployeeID");

                    if (hplClick != null)
                    {

                        if (!string.IsNullOrEmpty(hdfEmployeeID.Value.Trim()))
                        {
                            string _empID = hdfEmployeeID.Value.Trim();
                            string _year = Convert.ToDateTime(hdfAppraisalDate.Value).Year.ToString();
                            string url = string.Format("~/Form/PerformanceAppraisalHistory.aspx?EmployeeID={0}&AppraisalYear={1}", _empID, _year);

                            hplClick.NavigateUrl = url;
                            hplClick.Text = "Click";
                        }
                    }

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AppraisalTask.aspx");
        }

        protected void btnCloseMsgErr_Click(object sender, EventArgs e)
        {
            lbtnPopupErr_ModalPopupExtender.Hide();
            Response.Redirect("~/AppraisalTask.aspx");
        }

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

        protected void gvAppraisalPart2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuAttachment.HasFile)
                {
                    if (fuAttachment.FileBytes.Length >= 5242880)
                    {
                        lbtnPopupFuErr_ModalPopupExtender.Show();
                        lblFuWarning.Text = "Could not upload. File exceeds maximum allowed size of 5MB.";
                        return;
                    }

                    DataTable dtAttachFile = null;
                    DataRow dr;

                    string fileName = string.Empty;

                    fileName = fuAttachment.FileName;

                    string strEmpID = hdfEmployeeID.Value.Trim();
                    string strPathFile = ConfigurationManager.GetConfiguration().AttachFilePath;
                    string strBDUploadFolder = ConfigurationManager.GetConfiguration().UploadFolder;
                    //string strPathDate = DateTime.Now.ToString("ddMMyyyy") + "/";

                    string pathUpload = strPathFile + "/" + strEmpID + "/";  //  Path/EmpID/UploadFile.txt
                    string ServerMapPath = Server.MapPath(pathUpload);

                    /**************** Upload File To Server ***********************/
                    if (!System.IO.Directory.Exists(Server.MapPath(pathUpload)))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(pathUpload));
                    }

                    fuAttachment.PostedFile.SaveAs(ServerMapPath + fuAttachment.FileName);
                    /**************************************************************/

                    if (Session["tbAttachFile"] == null)
                    {
                        dtAttachFile = new DataTable();
                        dtAttachFile.Clear();

                        dtAttachFile.Columns.Add("FileName", typeof(string));
                        dtAttachFile.Columns.Add("Description", typeof(string));
                        dtAttachFile.Columns.Add("AttachFilePath", typeof(string));
                        dtAttachFile.Columns.Add("EmployeeID", typeof(string));

                        dr = dtAttachFile.NewRow();

                        dr["FileName"] = fileName;
                        dr["Description"] = string.Empty;
                        dr["AttachFilePath"] = pathUpload + fuAttachment.FileName;
                        dr["EmployeeID"] = strEmpID;
                    }
                    else
                    {
                        dtAttachFile = (DataTable)Session["tbAttachFile"];
                        if (dtAttachFile.Rows.Count >= 5)
                        {
                            lbtnPopupFuErr_ModalPopupExtender.Show();
                            lblFuWarning.Text = "You can only upload 5 files.";
                            return;
                        }

                        dr = dtAttachFile.NewRow();

                        dr["FileName"] = fileName;
                        dr["Description"] = string.Empty;
                        dr["AttachFilePath"] = pathUpload + fuAttachment.FileName;
                        dr["EmployeeID"] = strEmpID;
                    }

                    dtAttachFile.Rows.Add(dr);

                    Session["tbAttachFile"] = dtAttachFile;


                    fuAttachment.Attributes.Clear();
                    fuAttachment.Focus();

                    BindGvAttachFile();
                }
                else
                {
                    lbtnPopupFuErr_ModalPopupExtender.Show();
                    lblFuWarning.Text = "Please specify a file to upload.";
                    return;
                }
            }
            catch (Exception ex)
            {
                fuAttachment.Attributes.Clear();

                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }
        protected void BindGvAttachFile()
        {
            DataTable dt = (DataTable)Session["tbAttachFile"];

            gvAttachFile.DataSource = dt;
            gvAttachFile.DataBind();
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnDownload = (LinkButton)(sender);
                string filePath = lbtnDownload.CommandArgument;
                string fileName = Path.GetFileName(filePath);

                string ext = Path.GetExtension(filePath);
                string _type = "";
                switch (ext.ToLower())
                {
                    case ".htm":
                    case ".html":
                        _type = "text/HTML";
                        break;

                    case ".txt":
                        _type = "text/plain";
                        break;

                    case ".doc":
                    case ".rtf":
                        _type = "Application/msword";
                        break;

                    case ".xls":
                    case "xlsx":
                        _type = "Application/vnd.ms-excel";
                        break;

                    case ".pdf":
                        _type = "Application/pdf";
                        break;

                    case ".gif":
                        _type = "image/gif";
                        break;

                    case ".png":
                        _type = "image/png";
                        break;

                    case ".jpg":
                        _type = "image/jpeg";
                        break;
                }

                string rootPath = Server.MapPath("~/");
                string outputPath = filePath.Replace(rootPath, "/").Replace("\\", "//");

                HttpResponse response = HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = _type + "; Charset=Windows-874";
                response.AppendHeader("Content-Disposition", "attachment;filename= " + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                response.TransmitFile(Server.MapPath(outputPath));
                response.Flush();
                response.End();

            }
            catch (ThreadAbortException exf)
            {
                logger.Error(exf.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void gvAttachFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lb = e.Row.FindControl("lnkDownload") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lb);

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
                    }
                }               
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void gvAttachFile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Delete"))
                {
                    if (e.CommandName.Equals("Delete"))
                    {
                        string filePath = string.Empty;
                        string fileName = string.Empty;
                        string empID = string.Empty;

                        string[] arg = new string[2];
                        arg = e.CommandArgument.ToString().Split(';');
                        filePath = arg[0];
                        empID = arg[1];
                        fileName = Path.GetFileName(filePath);

                        if (!string.IsNullOrWhiteSpace(filePath))
                        {
                            if (System.IO.File.Exists(Server.MapPath(filePath)))
                            {
                                System.IO.File.Delete(Server.MapPath(filePath));
                            }
                        }

                        if (!string.IsNullOrEmpty(fileName))
                        {
                            ActionHistory_Manage manage = new ActionHistory_Manage();
                            bool result = manage.DeleteFileAttachment(empID, fileName);
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

        protected void gvAttachFile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tbAttachFile"];
                int pageIndex = gvAttachFile.PageIndex;
                int pageSize = gvAttachFile.PageSize;
                int rowIndex = Convert.ToInt32(e.RowIndex);

                if (pageIndex > 0)
                {
                    rowIndex = (pageIndex * pageSize) + rowIndex;
                }

                dt.Rows[rowIndex].Delete();
                dt.AcceptChanges();

                Session["tbAttachFile"] = dt;

                BindGvAttachFile();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void gvAttachFile_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAttachFile.PageIndex = e.NewPageIndex;
            BindGvAttachFile();
        }

        protected void btnFuClose_Click(object sender, EventArgs e)
        {
            lbtnPopupFuErr_ModalPopupExtender.Hide();
            fuAttachment.Focus();
        }
    }
}