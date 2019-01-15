using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformanceAppraisalProj.BL
{
    public class ConfigurationManager
    {
        private static ConfigurationItem _item;
        public static ConfigurationItem GetConfiguration()
        {
            if (_item == null)
            {
                _item = new ConfigurationItem();
            }
            return _item;
        }
    }

    public class ConfigurationItem
    {
        private string _dbcs;
        private string _dbProvider;
        private string _encryptionKey;
        private string _emailSMTP;
        private string _emailPort;
        private string _appraisalwebsite;
        private string _subjectmailAppr;
        private string _subjectmailInform;
        private string _EmailFrom;

        private string _attachFilePath;
        private string _uploadFolder;

        public ConfigurationItem()
        {
            _dbcs = System.Configuration.ConfigurationManager.AppSettings["ConnString"];
            _dbProvider = System.Configuration.ConfigurationManager.AppSettings["DBProvider"];
            _encryptionKey = System.Configuration.ConfigurationManager.AppSettings["EncryptionKey"];
            _emailSMTP = System.Configuration.ConfigurationManager.AppSettings["EmailSMTP"];
            _emailPort = System.Configuration.ConfigurationManager.AppSettings["EmailPort"];
            _appraisalwebsite = System.Configuration.ConfigurationManager.AppSettings["AppraisalWebsite"];
            _subjectmailAppr = System.Configuration.ConfigurationManager.AppSettings["SubjectMailApprove"];
            _subjectmailInform = System.Configuration.ConfigurationManager.AppSettings["SubjectMailInform"];
            _EmailFrom = System.Configuration.ConfigurationManager.AppSettings["EmailFrom"];

            _attachFilePath = System.Configuration.ConfigurationManager.AppSettings["AttachFilePath"];
            _uploadFolder = System.Configuration.ConfigurationManager.AppSettings["UploadFolder"];
        }

        public string DbConnectionString
        {
            get { return _dbcs; }
        }
        public string DbProviderName
        {
            get { return _dbProvider; }
        }
        public string EncryptionKey
        {
            get { return _encryptionKey; }
        }
        public string EmailSMTP
        {
            get { return _emailSMTP; }
        }
        public string EmailPort
        {
            get { return _emailPort; }
        }
        public string AppraisalWebsite
        {
            get { return _appraisalwebsite; }
        }
        public string SubjectMailApprove
        {
            get { return _subjectmailAppr; }
        }
        public string SubjectMailInform
        {
            get { return _subjectmailInform; }
        }
        public string EmailFrom
        {
            get { return _EmailFrom; }
        }
        public string AttachFilePath
        {
            get { return _attachFilePath; }
        }
        public string UploadFolder
        {
            get { return _uploadFolder; }
        }

    }
}
