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
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceAppraisalProj.Form
{
    public partial class PerformanceAppraisal : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PerformanceAppraisal));
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

                    Session["tbAttachFile"] = null;
                    InitialGVAttachFile();


                    #region #### For Pop Up Employee ####

                    if (!string.IsNullOrEmpty(hdfEmployeeType.Value) && hdfEmployeeType.Value.Trim().Equals("2"))
                    {
                        InitialGVPopupEmployee();
                        lbtnPopupEmployee_ModalPopupExtender.Show();
                    }

                    #endregion #### For Pop Up Employee ####
                }
            }
        }

        private void InitialGVAttachFile()
        {
            DataTable dtAttachFile = null;            
            dtAttachFile = new DataTable();
            dtAttachFile.Clear();

            dtAttachFile.Columns.Add("FileName", typeof(string));
            dtAttachFile.Columns.Add("Description", typeof(string));
            dtAttachFile.Columns.Add("AttachFilePath", typeof(string));
            dtAttachFile.Columns.Add("EmployeeID", typeof(string));         

            gvAttachFile.DataSource = dtAttachFile;
            gvAttachFile.DataBind();

        }
        private void InitialEmpDetail()
        {
            EmployeeMaster_Manage manage = new EmployeeMaster_Manage();
            EmployeeMaster empData = new EmployeeMaster();
            empData = manage.GetData(hdfEmployeeID.Value.Trim());
            if (empData != null)
            {
                //============ For Check Employee Group for Contract =====//
                hdfEmployeeType.Value = empData.EmployeeType;
                //========================================================//

                lblEmployeeName.Text = empData.EmployeeName;
                lblDepartment.Text = empData.DepartmentName;
                lblPosition.Text = empData.Position;

                hdfCompanyID.Value = empData.CompanyID;
                hdfSecondManager.Value = empData.SecondManager;
                hdfFirstManager.Value = empData.FirstManager;

                hdfFirstManagerMail.Value = empData.FirstManagerMail;
                hdfSecondManagerMail.Value = empData.SecondManagerMail;

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

                if (!string.IsNullOrEmpty(empData.AppraisalYear))
                {
                    if (empData.EmployeeType.Trim().Equals("1"))
                    {
                        lblDateFrom.Text = "01/01/" + empData.AppraisalYear;
                        lblDateTo.Text = "31/12/" + empData.AppraisalYear;
                    }
                    else
                    {
                        if (empData.ContractStart != null)
                        {
                            lblDateFrom.Text = empData.ContractStart.Value.ToString(@"dd\/MM\/yyyy");
                        }
                        if (empData.ContractEnd != null)
                        {
                            lblDateTo.Text = empData.ContractEnd.Value.ToString(@"dd\/MM\/yyyy");
                        }
                    }

                    //**** Check for link appraisal no data (old llink)*******//
                    string _year = DateTime.Now.Year.ToString();
                    if (empData.AppraisalYear.Trim() != _year)
                    {
                        btnSubmit.Visible = false;
                        lbtnPopupErr_ModalPopupExtender.Show();
                    }
                    //********************************************************//
                }
                else
                {
                    //**** Check for link appraisal no data (old llink)*******//
                    if (string.IsNullOrEmpty(empData.AppraisalYear) || string.IsNullOrEmpty(empData.HRStaff))
                    {
                        btnSubmit.Visible = false;
                        lbtnPopupErr_ModalPopupExtender.Show();
                    }
                    //********************************************************//
                }
            }
            else
            {
                btnSubmit.Visible = false;
                lbtnPopupErr_ModalPopupExtender.Show();
            }
        }
        private void GetAppraisalHeader()
        {
            try
            {
                AppraisalDocHeader_Manage manage = new AppraisalDocHeader_Manage();
                AppraisalDocHeader docHead = new AppraisalDocHeader();
                docHead = manage.GetData(hdfEmployeeID.Value.Trim(), DateTime.Now.Year);
                if (docHead != null)
                {
                    string _DocNo = string.IsNullOrEmpty(docHead.AppraisalDocNo) ? "" : docHead.AppraisalDocNo.Trim();
                    hdfAppraisalDocNo.Value = _DocNo;

                    //**** For Check Duplicate Job ****//
                    if (!string.IsNullOrEmpty(_DocNo))
                    {
                        if (docHead.AppraisalStatus.ToLower().Trim().Equals("completed"))
                        {
                            btnSubmit.Visible = false;
                            lbtnPopupErr_ModalPopupExtender.Show();
                        }
                        else
                        {
                            if (docHead.AppraisalStatus.Trim().Equals("Waiting 2nd Manager Approve"))
                            {
                                ActionHistory_Manage hm = new ActionHistory_Manage();
                                string _staus = hm.GetActionHistoryStatus(hdfAppraisalDocNo.Value.Trim(), "FirstManager");

                                if (!string.IsNullOrEmpty(_staus) && _staus.Trim().Equals("Approved"))
                                {
                                    btnSubmit.Visible = false;
                                    lbtnPopupErr_ModalPopupExtender.Show();
                                }
                                else
                                {
                                    btnSubmit.Visible = true;
                                }
                            }
                            else if (docHead.AppraisalStatus.Trim().Equals("Rejected"))
                            {
                                ActionHistory_Manage hm = new ActionHistory_Manage();
                                string _staus = hm.GetActionHistoryStatus(hdfAppraisalDocNo.Value.Trim(), "SecondManager");
                                if (string.IsNullOrEmpty(_staus))
                                {
                                    btnSubmit.Visible = false;
                                    lbtnPopupErr_ModalPopupExtender.Show();
                                }
                                else
                                {
                                    btnSubmit.Visible = true;
                                }
                            }
                            else
                            {
                                btnSubmit.Visible = true;
                            }
                        }
                    }
                    //*******************************//
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }
        private void BindGridView1()
        {
            try
            {
                /********* Find User from AD  *************/
                List<AppraisalTemplate> lUser = new List<AppraisalTemplate>();
                AppraisalTemplate_Manage manager = new AppraisalTemplate_Manage();
                lUser = manager.ListData().Where(t => t.QuestionType == 1).ToList();

                gvAppraisalPart1.DataSource = lUser;
                gvAppraisalPart1.DataBind();

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
                /********* Find User from AD  *************/
                List<AppraisalTemplate> lUser = new List<AppraisalTemplate>();
                AppraisalTemplate_Manage manager = new AppraisalTemplate_Manage();
                lUser = manager.ListData().Where(t => t.QuestionType == 2).ToList();

                gvAppraisalPart2.DataSource = lUser;
                gvAppraisalPart2.DataBind();

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
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtPart1Comment = (TextBox)e.Row.FindControl("txtPart1Comment");
                    txtPart1Comment.Attributes.Add("style", "width:98%");
                    txtPart1Comment.MaxLength = 150;

                    txtPart1Comment.ToolTip = "Maximum limit 150 characters.";
                    txtPart1Comment.Attributes.Add("onkeyup", "return validateRemark(this, 150)");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void ddlPart1Result_SelectedIndexChanged1(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlPart1Result = (DropDownList)row.FindControl("ddlPart1Result");
            HiddenField hdfPart1QuestionWeight = (HiddenField)row.FindControl("hdfPart1QuestionWeight");
            Label lblPart1Score = (Label)row.FindControl("lblPart1Score");

            if (ddlPart1Result != null && !ddlPart1Result.SelectedValue.ToString().Equals("0"))
            {
                var calScore = int.Parse(ddlPart1Result.SelectedValue) * decimal.Parse(hdfPart1QuestionWeight.Value);
                lblPart1Score.Text = calScore.ToString("#,##0.00");
            }
        }

        protected void ddlPart2Result_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlPart2Result = (DropDownList)row.FindControl("ddlPart2Result");
            HiddenField hdfPart2QuestionWeight = (HiddenField)row.FindControl("hdfPart2QuestionWeight");
            Label lblPart2Score = (Label)row.FindControl("lblPart2Score");

            if (ddlPart2Result != null && !ddlPart2Result.SelectedValue.ToString().Equals("0"))
            {
                var calScore = int.Parse(ddlPart2Result.SelectedValue) * decimal.Parse(hdfPart2QuestionWeight.Value);
                lblPart2Score.Text = calScore.ToString("#,##0.00");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    /*********************************************************/
                    string str2ndManager = hdfSecondManager.Value;
                    /************* Creaete Object Appraisal Doc Line *********/

                    AppraisalDocHeader dataHeader = new AppraisalDocHeader();
                    dataHeader.AppraisalDate = DateTime.Now;
                    dataHeader.EmployeeID = hdfEmployeeID.Value.Trim();

                    // DateTime.ParseExact(this.Text, "dd/MM/yyyy", null);
                    if (!string.IsNullOrEmpty(lblDateFrom.Text.Trim()))
                    {
                        dataHeader.AppraisalPeriodFrom = DateTime.ParseExact(lblDateFrom.Text.Trim(), "dd/MM/yyyy", null);
                    }
                    if (!string.IsNullOrEmpty(lblDateTo.Text.Trim()))
                    {
                        dataHeader.AppraisalPeriodTo = DateTime.ParseExact(lblDateTo.Text.Trim(), "dd/MM/yyyy", null);
                    }

                    //**** Check case no 2nd Manager ****//
                    dataHeader.AppraisalStatus = string.IsNullOrEmpty(str2ndManager) ? "Completed" : "Waiting 2nd Manager Approve";
                    dataHeader.AppraisalYear = DateTime.Now.Year;
                    dataHeader.CompanyID = hdfCompanyID.Value.Trim();
                    dataHeader.CreatedBy = hdfUserLogin.Value.Trim();
                    dataHeader.CreatedDate = DateTime.Now;
                    dataHeader.DepartmentName = lblDepartment.Text;
                    dataHeader.EmployeeImprovement = txtEmpImpovement.Text;
                    dataHeader.EmployeeName = lblEmployeeName.Text;
                    dataHeader.EmployeeStrength = txtEmpStrength.Text;
                    dataHeader.Position = lblPosition.Text;

                    if (!string.IsNullOrEmpty(lblJoinDate.Text))
                    {
                        dataHeader.StartDate = DateTime.ParseExact(lblJoinDate.Text.Trim(), "dd/MM/yyyy", null);
                    }

                    /*********************************************************/
                    /************* Creaete Object Appraisal Doc Line *********/
                    /*********************************************************/
                    List<AppraisalDocLine> lAppraisalDocLine = new List<AppraisalDocLine>();
                    /****************** Gridview Part 1 **********************/

                    int iAppraisalDocLineNo = 1;
                    foreach (GridViewRow row in gvAppraisalPart1.Rows)
                    {
                        Label lblPart1Score = row.FindControl("lblPart1Score") as Label;
                        string sScore = lblPart1Score.Text.Trim();

                        DropDownList ddlPart1Result = row.FindControl("ddlPart1Result") as DropDownList;
                        string sAgreeReqult = ddlPart1Result.SelectedValue;

                        TextBox txtPart1Comment = row.FindControl("txtPart1Comment") as TextBox;
                        txtPart1Comment.MaxLength = 150;

                        string sComment = txtPart1Comment.Text;

                        HiddenField hdfPart1QuestionDesc = row.FindControl("hdfPart1QuestionDesc") as HiddenField;
                        HiddenField hdfPart1QuestionLineNo = row.FindControl("hdfPart1QuestionLineNo") as HiddenField;
                        HiddenField hdfPart1QuestionType = row.FindControl("hdfPart1QuestionType") as HiddenField;
                        HiddenField hdfPart1QuestionWeight = row.FindControl("hdfPart1QuestionWeight") as HiddenField;

                        AppraisalDocLine itemPart1 = new AppraisalDocLine();
                        itemPart1.AppraisalDocLineNo = iAppraisalDocLineNo;
                        itemPart1.Score = string.IsNullOrEmpty(sAgreeReqult) ? 0 : Convert.ToInt16(sAgreeReqult);
                        itemPart1.QuestionLineNo = Convert.ToInt16(hdfPart1QuestionLineNo.Value);
                        itemPart1.QuestionDesc = hdfPart1QuestionDesc.Value;
                        itemPart1.QuestionType = Convert.ToInt16(hdfPart1QuestionType.Value);
                        itemPart1.CalculatedScore = string.IsNullOrEmpty(sScore) ? 0 : Convert.ToDecimal(sScore);
                        itemPart1.QuestionWeight = string.IsNullOrEmpty(hdfPart1QuestionWeight.Value) ? 0 : Convert.ToDecimal(hdfPart1QuestionWeight.Value);
                        itemPart1.Remark = sComment;
                        lAppraisalDocLine.Add(itemPart1);

                        iAppraisalDocLineNo += 1;
                    }
                    /*********************************************************/

                    /***************** Gridview Part 2 ***********************/
                    foreach (GridViewRow row in gvAppraisalPart2.Rows)
                    {
                        Label lblPart1Score = row.FindControl("lblPart2Score") as Label;
                        string sScore = lblPart1Score.Text.Trim();

                        DropDownList ddlPart2Result = row.FindControl("ddlPart2Result") as DropDownList;
                        string sAgreeReqult = ddlPart2Result.SelectedValue;

                        TextBox txtPart2Comment = row.FindControl("txtPart2Comment") as TextBox;
                        txtPart2Comment.MaxLength = 150;
                        string sComment = txtPart2Comment.Text;

                        HiddenField hdfPart2QuestionDesc = row.FindControl("hdfPart2QuestionDesc") as HiddenField;
                        HiddenField hdfPart2QuestionLineNo = row.FindControl("hdfPart2QuestionLineNo") as HiddenField;
                        HiddenField hdfPart2QuestionType = row.FindControl("hdfPart2QuestionType") as HiddenField;
                        HiddenField hdfPart2QuestionWeight = row.FindControl("hdfPart2QuestionWeight") as HiddenField;

                        AppraisalDocLine itemPart2 = new AppraisalDocLine();
                        itemPart2.AppraisalDocLineNo = iAppraisalDocLineNo;
                        itemPart2.Score = string.IsNullOrEmpty(sAgreeReqult) ? 0 : Convert.ToInt16(sAgreeReqult);
                        itemPart2.QuestionLineNo = Convert.ToInt16(hdfPart2QuestionLineNo.Value);
                        itemPart2.QuestionDesc = hdfPart2QuestionDesc.Value;
                        itemPart2.QuestionType = Convert.ToInt16(hdfPart2QuestionType.Value);
                        itemPart2.CalculatedScore = string.IsNullOrEmpty(sScore) ? 0 : Convert.ToDecimal(sScore);
                        itemPart2.QuestionWeight = string.IsNullOrEmpty(hdfPart2QuestionWeight.Value) ? 0 : Convert.ToDecimal(hdfPart2QuestionWeight.Value);
                        itemPart2.Remark = sComment;

                        lAppraisalDocLine.Add(itemPart2);

                        iAppraisalDocLineNo += 1;
                    }
                    /*********************************************************/
                    /***************** Set Grade & Score ********************/
                    int TotalScore = 0;
                    string gradeResult = "";
                    gradeResult = CalculateGrade(lAppraisalDocLine, ref TotalScore);
                    dataHeader.AppraisalTotalScore = TotalScore;
                    dataHeader.AppraisalGrade = gradeResult;

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
                    ActionHistory updActHisData = new ActionHistory();
                    updActHisData.EmployeeID = hdfEmployeeID.Value.Trim();
                    updActHisData.CreatedBy = hdfUserLogin.Value.Trim();
                    updActHisData.Status = "Approved";
                    updActHisData.Comments = txtRemark.Text;
                    updActHisData.AppraisalYear = DateTime.Now.Year;

                    ApprovalHistory insAppHis = new ApprovalHistory();
                    insAppHis.Action = "Approve";
                    insAppHis.Comment = txtRemark.Text;
                    insAppHis.TransactionDate = DateTime.Now;
                    insAppHis.UserID = hdfUserLogin.Value.Trim();

                    ActionHistory ins2NdActHisData = new ActionHistory();
                    if (!string.IsNullOrEmpty(hdfSecondManager.Value))
                    {
                        ins2NdActHisData.EmployeeID = hdfEmployeeID.Value.Trim();
                        ins2NdActHisData.Action = "Approve";
                        ins2NdActHisData.AppraisalYear = DateTime.Now.Year;
                        ins2NdActHisData.CreatedBy = hdfSecondManager.Value.Trim();
                        ins2NdActHisData.CreatedDate = DateTime.Now;
                        ins2NdActHisData.Responsibility = "SecondManager";
                    }
                    //--------------------------------------------------------------//

                    /********************** Insert to DataBase ****************************/
                    AppraisalDocHeader_Manage manage = new AppraisalDocHeader_Manage();
                    bool insResult = manage.InsertDocHeaderData(dataHeader, lAttachFile, lAppraisalDocLine, updActHisData, insAppHis, ins2NdActHisData);
                    if (insResult)
                    {
                        lblMsgResult.Text = "บันทึกข้อมูลเรียบร้อย";
                        lbtnPopup_ModalPopupExtender.Show();

                        string sSubjectMail = ConfigurationManager.GetConfiguration().SubjectMailApprove;
                        string sEmailFrom = ConfigurationManager.GetConfiguration().EmailFrom;
                        string reqDate = DateTime.Now.ToString(@"dd\/MM\/yyyy");

                        string Email2ndManager = hdfSecondManagerMail.Value.Trim();
                        if (!string.IsNullOrEmpty(Email2ndManager))
                        {
                            string _employeeName = lblEmployeeName.Text;
                            string emaiBody = GenEmailBody(hdfEmployeeID.Value.Trim(), hdfSecondManager.Value.Trim(),
                                hdfUserLogin.Value.Trim(), reqDate, _employeeName);
                            SendMail2ndManager(sSubjectMail, emaiBody, Email2ndManager, sEmailFrom);
                        }
                    }
                    else
                    {
                        lblMsgResult.Text = "ไม่สามารถบันทึกข้อมูลได้";

                        btnOK.Visible = false;
                        btnCancel.Visible = true;

                        lbtnPopup_ModalPopupExtender.Show();
                    }
                    /*********************************************************/
                }
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

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            decimal scoreType1 = 0;
            foreach (GridViewRow row in gvAppraisalPart1.Rows)
            {
                Label lblPart1Score = row.FindControl("lblPart1Score") as Label;
                string sScore = lblPart1Score.Text.Trim();

                decimal dScore = string.IsNullOrEmpty(sScore) ? 0 : Convert.ToDecimal(sScore);
                //dScore = (dScore * 10);
                scoreType1 += dScore;
            }

            decimal scoreType2 = 0;
            foreach (GridViewRow row in gvAppraisalPart2.Rows)
            {
                Label lblPart1Score = row.FindControl("lblPart2Score") as Label;
                string sScore = lblPart1Score.Text.Trim();

                decimal dScore = string.IsNullOrEmpty(sScore) ? 0 : Convert.ToDecimal(sScore);
                //dScore = (dScore * 10);
                scoreType2 += dScore;
            }


            //**** Calculate Score ******//
            string _grade = "";
            Int32 SummaryPoint = Convert.ToInt32(scoreType1 + scoreType2);
            if (SummaryPoint <= 55)
            {
                _grade = "1";
            }
            else
            {
                if (SummaryPoint > 55 && SummaryPoint <= 70)
                {
                    _grade = "2";
                }
                else if (SummaryPoint > 70 && SummaryPoint <= 85)
                {
                    _grade = "3";
                }
                else if (SummaryPoint > 85)
                {
                    _grade = "4";
                }
            }
            //****************************//

            lblActualPart1.Text = scoreType1.ToString();
            lblActualPart2.Text = scoreType2.ToString();
            lblActualPoint.Text = (scoreType1 + scoreType2).ToString();

            lblResultScore.Text = "Band " + _grade;

            //*********************//
        }

        private string CalculateGrade(List<AppraisalDocLine> lDocLine, ref int TotalScore)
        {
            string retGrade = "";
            try
            {
                int SummaryPoint = 0;
                decimal? _point = 0;
                foreach (var item in lDocLine)
                {
                    _point += item.CalculatedScore;
                }

                string _grade = string.Empty;

                //SummaryPoint = Convert.ToInt16(_point * 10);
                SummaryPoint = Convert.ToInt16(_point);
                TotalScore = SummaryPoint;
                if (SummaryPoint <= 55)
                {
                    _grade = "1";
                }
                else
                {
                    if (SummaryPoint > 55 && SummaryPoint <= 70)
                    {
                        _grade = "2";
                    }
                    else if (SummaryPoint > 70 && SummaryPoint <= 85)
                    {
                        _grade = "3";
                    }
                    else if (SummaryPoint > 85)
                    {
                        _grade = "4";
                    }
                }

                retGrade = _grade;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return retGrade;
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
            string _2ndManagerName = ManagerName;
            string _HRRequestName = ReqName;
            string _RequestDate = ReqDate;
            string _url = _website + "/Form/PerformanceAppraisalReview.aspx?EmployeeID=" + _EmpID;

            string strEmailBody = "<HTML><BODY><table style='font-size: 11.0pt; font-family: Arial,Helvetica,san-serif; color: black;'>" +
                                 "<tr><td>Dear K." + _2ndManagerName + ",</td></tr>" +
                                 "<tr><td height = '20px'></td></tr>" +
                                 "<tr><td> You have an approval request as below details.</td></tr>" +
                                 "<tr><td height='20px'></td></tr>" +
                                 "<tr><td>" +
                                 "<table width='100 %' border='1' cellspacing='0' cellpadding='3'>" +
                                 "<tr><td> Status </td><td> Waiting 2nd Manager Approve </td></tr>" +
                                 "<tr><td> Employee Name </td><td>" + EmployeeName + "</td></tr>" +
                                 "<tr><td> Position </td><td>" + lblPosition.Text.Trim() + "</td></tr>" +
                                 "<tr><td> 1st Manager Name </td><td>" + hdfFirstManager.Value.Trim() + "</td></tr>" +
                                 "<tr><td> 2nd Manager Name </td><td>" + _2ndManagerName + "</td></tr></table>" +
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

        private void SendMail2ndManager(string Subject, string BodyMail, string EmailTo, string EmailFrom)
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Session["tbAttachFile"] = null;
            Response.Redirect("~/AppraisalTask.aspx");
        }

        #region #### For Gridview Appraisal History ####

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

        #endregion

        protected void ValidateTxt_ServerValidate(object source, ServerValidateEventArgs args)
        {
            foreach (GridViewRow row in gvAppraisalPart1.Rows)
            {
                DropDownList ddlPart1Result = row.FindControl("ddlPart1Result") as DropDownList;
                string sAgreeReqult1 = ddlPart1Result.SelectedValue;

                if (sAgreeReqult1.Trim().Equals("0"))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุคะแนน";
                    ddlPart1Result.Focus();
                    args.IsValid = false;
                    return;
                }
            }

            foreach (GridViewRow row in gvAppraisalPart2.Rows)
            {
                DropDownList ddlPart2Result = row.FindControl("ddlPart2Result") as DropDownList;
                string sAgreeReqult2 = ddlPart2Result.SelectedValue;

                if (sAgreeReqult2.Trim().Equals("0"))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุคะแนน";
                    ddlPart2Result.Focus();
                    args.IsValid = false;
                    return;
                }
            }
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
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtPart2Comment = (TextBox)e.Row.FindControl("txtPart2Comment");
                    txtPart2Comment.Attributes.Add("style", "width:98%");
                    txtPart2Comment.MaxLength = 150;
                    txtPart2Comment.ToolTip = "Maximum limit 150 characters.";
                    txtPart2Comment.Attributes.Add("onkeyup", "return validateRemark(this, 150)");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
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

        protected void gvAttachFile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                fuAttachment.Attributes.Clear();

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
        protected void gvAttachFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
                }
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

        #region #### For Pop Up Employee ####
        private void InitialGVPopupEmployee()
        {
            lblPUSubject.Text = "เรียนคุณ  " + hdfFirstManager.Value;

            DataTable dtPopupEmployee = null;
            DataRow dr;

            dtPopupEmployee = new DataTable();
            dtPopupEmployee.Clear();

            dtPopupEmployee.Columns.Add("EmployeeName", typeof(string));
            dtPopupEmployee.Columns.Add("Position", typeof(string));
            dtPopupEmployee.Columns.Add("Company", typeof(string));
            dtPopupEmployee.Columns.Add("DateHired", typeof(string));
            dtPopupEmployee.Columns.Add("DateEnded", typeof(string));

            dr = dtPopupEmployee.NewRow();

            dr["EmployeeName"] =lblEmployeeName.Text;
            dr["Position"] = lblPosition.Text;
            dr["Company"] = hdfCompanyID.Value;
            dr["DateHired"] = lblContractStart.Text;
            dr["DateEnded"] = lblContractEnd.Text;

            dtPopupEmployee.Rows.Add(dr);

            gvPopEmployee.DataSource = dtPopupEmployee;
            gvPopEmployee.DataBind();
        }
        protected void btnCloseEmp_Click(object sender, EventArgs e)
        {
            //Hide Pop up Notice 
            lbtnPopupEmployee_ModalPopupExtender.Hide();
        }
        protected void gvPopEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        #endregion #### For Pop Up Employee ####
    }
}