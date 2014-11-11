using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace KPIEmailService
{
    public class EmailJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            List<int> dates = ReminderDates;

            if (dates.Contains(DateTime.Now.Day))
            {
                //TODO : Decide it is reminder or escallation -> get imcomplete KPI list.
                if(dates.Last() == DateTime.Now.Day)
                {
                }
            }
        }

        public string RemidersEmailGroup 
        {
            get
            {
                return ConfigurationManager.AppSettings["RemidersEmailGroup"];
            }
        }

        public string ReminderEmailGroup
        {
            get
            {
                return ConfigurationManager.AppSettings["EscallationEmailGroup"];
            }
        }

        public List<int> ReminderDates
        {
            get
            {
                List<int> dates = new List<int>();

                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["ReminderDates"]))
                {
                    string[] dateStr = ConfigurationManager.AppSettings["ReminderDates"].Split(',');

                    int date;
                    int count = 0;

                    foreach (var item in dateStr)
                    {
                        if (count == 3)
                            break;

                        if(int.TryParse(item,out date) && date > 0 && date < 32)
                        {
                            dates.Add(date);

                            date = 0;
                            count++;
                        }
                    }
                }

                dates.Sort();

                return dates;
            }
        }

        private void SendEmail(string emailTo, string subject, string body)
        {
            int port = 0;

            int.TryParse(ConfigurationManager.AppSettings["Port"], out port);

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Port = port;
            client.Host = ConfigurationManager.AppSettings["Host"];
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(
                ConfigurationManager.AppSettings["NetworkCredentialUserName"],
                ConfigurationManager.AppSettings["NetworkCredentialPassword"]);

            MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["EmailFrom"], emailTo, subject, body);
            mailMessage.IsBodyHtml = true;

            mailMessage.BodyEncoding = System.Text.UTF8Encoding.UTF8;
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mailMessage);
        }
    }
}
