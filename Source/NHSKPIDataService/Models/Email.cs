using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHSKPIDataService.Models
{
    public class Email
    {
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class EmailConfigurations
    {
        public string EmailFrom { get; set; }
        public string CommentType { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string NetworkCredentialUserName { get; set; }
        public string NetworkCredentialPassword { get; set; }
        public string SiteURL { get; set; }
    }
}
