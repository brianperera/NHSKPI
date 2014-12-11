using System;
using System.Collections.Generic;
using System.Text;
using NHSKPIDataService.Models;
using System.Data;
using System.Web;
using NHSKPIDataService.Services;
using System.Net.Mail;
using System.Configuration;


namespace NHSKPIBusinessControllers
{
    public class UtilController
    {
        #region Private Variable

        UtilService _UtilService = null;
                
        #endregion

        #region Properties

        public UtilService UtilService
        {
            get 
            {
                if (_UtilService == null)
                {
                    _UtilService = new UtilService();
                }
                return _UtilService; 
            }
            set 
            { 
                _UtilService = value; 
            }
        }

        #endregion

        #region Get Specialty Level Target Initial Data
        /// <summary>
        /// Get Specialty Level Target Initial Data
        /// </summary>
        /// <param name="hospitalId"></param>
        /// <returns></returns>
        public DataSet GetSpecialtyLevelTargetInitialData(int hospitalId)
        {
            try
            {
                DataSet dsSpecialtyLevelTargetInitialData = UtilService.GetSpecialtyLevelTargetInitialData(hospitalId);
                return dsSpecialtyLevelTargetInitialData;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Get Specialty Level YTD Target Initial Data
        /// <summary>
        /// Get Specialty Level YTD Target Initial Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpecialtyLevelYTDTargetInitialData()
        {
            try
            {
                DataSet dsSpecialtyLevelYTDTargetInitialData = UtilService.GetSpecialtyLevelYTDTargetInitialData();
                return dsSpecialtyLevelYTDTargetInitialData;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Send Email Notification

        public void SendEmailNotification(EmailMessage email)
        {
            // Command line argument must the the SMTP host.

            EmailConfigurations emailConfigurations = new EmailConfigurations
            {
                Port = int.Parse(ConfigurationManager.AppSettings["Port"].ToString()),
                Host = ConfigurationManager.AppSettings["Host"].ToString(),
                NetworkCredentialUserName = ConfigurationManager.AppSettings["NetworkCredentialUserName"].ToString(),
                NetworkCredentialPassword = ConfigurationManager.AppSettings["NetworkCredentialPassword"].ToString(),
                EmailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString()
            };

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Port = emailConfigurations.Port;
            client.Host = emailConfigurations.Host;
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(
                emailConfigurations.NetworkCredentialUserName, emailConfigurations.NetworkCredentialPassword);

            MailMessage mailMessage = new MailMessage(emailConfigurations.EmailFrom, email.EmailTo, email.Subject, email.Body);
            mailMessage.IsBodyHtml = true;

            mailMessage.BodyEncoding = System.Text.UTF8Encoding.UTF8;
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mailMessage);
        }

        public List<Email> GetEmailList(int hospitalId)
        {
            try
            {
                return UtilService.GetEmailList(hospitalId);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }
        }

        public bool InsertEmailToBucket(Email email)
        {
            try
            {
                return UtilService.InsertEmailToBucket(email);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }
        }

        #endregion
    }
}
