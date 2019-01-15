using PerformanceAppraisalProj.BL.Manage;
using PerformanceAppraisalProj.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceAppraisalProj.Form
{
    public partial class PerformanceAppraisalHistory : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PerformanceAppraisalHistory));
       
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
                    hdfPerformanceYear.Value = Request.QueryString["AppraisalYear"];

                    InitialEmpDetail();
                    GetAppraisalHeader();

                    BindGridView1();
                    BindGridView2();

                    BindGVApprovalHist();

                    lblTotalPart1.Text = "50 คะแนน";
                    lblTotalPart2.Text = "50 คะแนน";
                    lblTotalPoint.Text = "100 คะแนน";
                    lblActualPoint.Text = (Convert.ToDecimal(lblActualPart1.Text.Trim()) + Convert.ToDecimal(lblActualPart2.Text.Trim())).ToString();
                    
                    InitialGridAttachfile();
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
                if (empData.AppraisalDate != null)
                {
                    lblAppraisalDate.Text = empData.AppraisalDate.Value.ToString(@"dd\/MM\/yyyy");
                }
            }
        }

        private void GetAppraisalHeader()
        {
            if (!string.IsNullOrEmpty(hdfPerformanceYear.Value.Trim()))
            {
                int _year = int.Parse(hdfPerformanceYear.Value.Trim());

                AppraisalDocHeader_Manage manage = new AppraisalDocHeader_Manage();
                AppraisalDocHeader docHead = new AppraisalDocHeader();

                docHead = manage.GetData(hdfEmployeeID.Value.Trim(), _year);
                if (docHead != null)
                {
                    lblAppraisalDocNo.Text = docHead.AppraisalDocNo;
                    hdfAppraisalDocNo.Value = docHead.AppraisalDocNo;

                    txtEmpStrength.Text = docHead.EmployeeStrength.Trim();
                    txtEmpImpovement.Text = docHead.EmployeeImprovement.Trim();
                }
                else
                {
                    lblAppraisalDocNo.Text = "(Creator)";
                }

                lblResultScore.Text = string.IsNullOrEmpty(docHead.AppraisalGrade) ? "" : "Band " + docHead.AppraisalGrade;
                if (docHead.AppraisalPeriodFrom != null)
                {
                    lblDateFrom.Text = docHead.AppraisalPeriodFrom.Value.ToString(@"dd\/MM\/yyyy");
                }
                if (docHead.AppraisalPeriodTo != null)
                {
                    lblDateTo.Text = docHead.AppraisalPeriodTo.Value.ToString(@"dd\/MM\/yyyy");
                }
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


                if (lUser != null && lUser.Count > 0)
                {
                    decimal scoreType1 = 0;
                    foreach (var item in lUser)
                    {
                        decimal dScore = Convert.ToDecimal(item.CalculatedScore * 10);
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
                        decimal dScore = Convert.ToDecimal(item.CalculatedScore * 10);
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
       
        protected void gvActionHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvActionHistory.PageIndex = e.NewPageIndex;
            BindGVApprovalHist();
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

        protected void gvAttachFile_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvAttachFile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvAttachFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ScriptManager scriptMan = ScriptManager.GetCurrent(this);
                LinkButton lnkDownload = e.Row.FindControl("lnkDownload") as LinkButton;
                if (lnkDownload != null)
                {
                    scriptMan.RegisterPostBackControl(lnkDownload);
                }

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
                }
            }
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

        private void InitialGridAttachfile()
        {
            try
            {
                string _EmployeeID = hdfEmployeeID.Value.Trim();

                EmployeeMaster_Manage manage = new EmployeeMaster_Manage();
                List<Attachment> lAttachment = new List<Attachment>();
                lAttachment = manage.listAttachfile(_EmployeeID);

                gvAttachFile.DataSource = lAttachment;
                gvAttachFile.DataBind();
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
            InitialGridAttachfile();
        }
    }
}