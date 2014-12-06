using NHSKPIBusinessControllers;
using NHSKPIDataService.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Mail;

namespace KPIEmailService
{
    public class EmailJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            EmailNotificationController controller = new EmailNotificationController();

            List<EmailNotification> noticationConfigs = controller.GetAllEmailNotifications();

            foreach (var item in noticationConfigs)
            {
                if (DateTime.Now.Day == item.Reminder1) 
                {
                    DataSet ds = controller.GetIncompleteKPI(item.HospitalId);
                    string file = ExportDataSetToExcel(ds, DateTime.Now.AddMonths(-1).Month + "_" + item.HospitalId + "_R1.xlsx");
                    SendEmail(item.ReminderEmail, file);
                }
                else if (DateTime.Now.Day == item.Reminder2)
                {
                    DataSet ds = controller.GetIncompleteKPI(item.HospitalId);
                    string file = ExportDataSetToExcel(ds, DateTime.Now.AddMonths(-1).Month + "_" + item.HospitalId + "_R2.xlsx");
                    SendEmail(item.ReminderEmail, file);
                }
                else if (DateTime.Now.Day == item.ManagerEscalation)
                {
                    DataSet ds = controller.GetIncompleteKPI(item.HospitalId);
                    string file = ExportDataSetToExcel(ds, DateTime.Now.AddMonths(-1).Month + "_" + item.HospitalId + "_E.xlsx");
                    SendEmail(item.EscalationEmail, file);
                }
            }

        }

        private void SendEmail(string emailTo, string filePath)
        {
            int port = 0;

            int.TryParse(ConfigurationManager.AppSettings["Port"], out port);
            string subject = ConfigurationManager.AppSettings["ReminderSubject"];
            string body = ConfigurationManager.AppSettings["ReminderBody"];

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
            mailMessage.Attachments.Add(new Attachment(filePath));
            mailMessage.BodyEncoding = System.Text.UTF8Encoding.UTF8;
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mailMessage);
        }

        private string ExportDataSetToExcel(DataSet ds,string fileName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["ReportFilePath"]))
                {
                    fileName = Path.Combine(ConfigurationManager.AppSettings["ReportFilePath"], fileName);
                }

                if (File.Exists(fileName))
                    File.Delete(fileName);

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                object misValue = System.Reflection.Missing.Value;

                Microsoft.Office.Interop.Excel.Workbook excelWorkBook = excelApp.Workbooks.Add(misValue); //excelApp.Workbooks.Open(fileName);

                foreach (DataTable table in ds.Tables)
                {
                    //Add a new worksheet to workbook with the Datatable name
                    Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                    excelWorkSheet.Name = table.TableName;

                    for (int i = 1; i < table.Columns.Count + 1; i++)
                    {
                        excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                    }

                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        for (int k = 0; k < table.Columns.Count; k++)
                        {
                            excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                        }
                    }
                }

                excelWorkBook.SaveAs(fileName);
                excelWorkBook.Close();
                excelApp.Quit();

                return fileName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
