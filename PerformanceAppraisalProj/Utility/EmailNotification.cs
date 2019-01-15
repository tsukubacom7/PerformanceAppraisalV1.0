using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Collections;
using PerformanceAppraisalProj.BL;

namespace PerformanceAppraisalProj.Utility
{
    public class EmailNotification
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(EmailNotification));

        #region #### Private Variables ####

        private string m_EmailSubject = string.Empty;
        private string m_EmailForm = string.Empty;
        private ArrayList m_EmailTo;
        private string m_EmailFormName = string.Empty;
        private int m_EmailPort;
        private string m_EmailSMTP = string.Empty;
        private string m_EmailBody = string.Empty;
        private string m_EmailUser = string.Empty;
        private string m_EmailPassword = string.Empty;

        #endregion

        #region #### Public Property ####
        public string EmailSubject
        {
            get { return m_EmailSubject; }
            set { m_EmailSubject = value; }
        }
        public string EmailForm
        {
            get { return m_EmailForm; }
            set { m_EmailForm = value; }
        }
        public ArrayList EmailTo
        {
            get { return m_EmailTo; }
            set { m_EmailTo = value; }
        }
        public string EmailFormName
        {
            get { return m_EmailFormName; }
            set { m_EmailFormName = value; }
        }
        public int EmailPort
        {
            get { return m_EmailPort; }
            set { m_EmailPort = value; }
        }
        public string EmailSMTP
        {
            get { return m_EmailSMTP; }
            set { m_EmailSMTP = value; }
        }
        public string EmailBody
        {
            get { return m_EmailBody; }
            set { m_EmailBody = value; }
        }
        public string EmailUser
        {
            get { return m_EmailUser; }
            set { m_EmailUser = value; }
        }
        public string EmailPassword
        {
            get { return m_EmailPassword; }
            set { m_EmailPassword = value; }
        }

        #endregion

        public void SendEmail()
        {
            SmtpClient client = default(SmtpClient);
            MailMessage oEmail = new MailMessage();
            System.Net.Mail.MailAddress addressObj = default(System.Net.Mail.MailAddress);
            try
            {
                client = new SmtpClient(m_EmailSMTP, this.m_EmailPort);

                //verify user/password
                if (((m_EmailUser != null)) & ((m_EmailPassword != null)))
                {
                    if (!string.IsNullOrWhiteSpace(m_EmailUser) && !string.IsNullOrWhiteSpace(m_EmailPassword))
                    {
                        client.Credentials = new System.Net.NetworkCredential(m_EmailUser, m_EmailPassword);
                    }
                }

                //set mail attributes
                var _with1 = oEmail;
                _with1.From = new MailAddress(m_EmailForm, m_EmailFormName);
                _with1.IsBodyHtml = true;
                _with1.Subject = m_EmailSubject;
                _with1.Body = m_EmailBody;
                _with1.Priority = MailPriority.Normal;

                //list email TO list
                if ((m_EmailTo != null))
                {
                    for (int iIndex = 0; iIndex < m_EmailTo.Count; iIndex++)
                    {
                        addressObj = new System.Net.Mail.MailAddress(m_EmailTo[iIndex].ToString());
                        if ((addressObj != null))
                        {
                            _with1.To.Add(addressObj);
                        }
                    }
                }

                //send mail
                client.Send(oEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendMailNotice(string Subject, string BodyMail, string EmailTo, string EmailFrom)
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
    }
}