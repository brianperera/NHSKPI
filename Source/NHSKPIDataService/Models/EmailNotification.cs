using Microsoft.Practices.EnterpriseLibrary.Data;
using NHSKPIDataService.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace NHSKPIDataService.Models
{
    public class EmailNotification
    {
        private int id;
        private int hospitalId;
        private DateTime reminder1;
        private DateTime reminder2;
        private DateTime managerEscalation;
        private string reminderEmail;
        private string escalationEmail;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int HospitalId
        {
            get { return hospitalId; }
            set { hospitalId = value; }
        }

        public DateTime Reminder1
        {
            get { return reminder1; }
            set { reminder1 = value; }
        }

        public DateTime Reminder2
        {
            get { return reminder2; }
            set { reminder2 = value; }
        }

        public DateTime ManagerEscalation
        {
            get { return managerEscalation; }
            set { managerEscalation = value; }
        }

        public string ReminderEmail
        {
            get { return reminderEmail; }
            set { reminderEmail = value; }
        }

        public string EscalationEmail
        {
            get { return escalationEmail; }
            set { escalationEmail = value; }
        }       

        public void Add(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Insert_EmailNotification);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int16, this.HospitalId);
                db.AddInParameter(dbCommand, "@Reminder1", DbType.Date, this.Reminder1);
                db.AddInParameter(dbCommand, "@Reminder2", DbType.Date, this.Reminder2);
                db.AddInParameter(dbCommand, "@ManagerEscalation", DbType.Date, this.ManagerEscalation);
                db.AddInParameter(dbCommand, "@ReminderEmail", DbType.String, this.ReminderEmail);
                db.AddInParameter(dbCommand, "@ManagerEscalation", DbType.String, EscalationEmail);
                db.ExecuteNonQuery(dbCommand, transaction);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Update_EmailNotification);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int16, this.HospitalId);
                db.AddInParameter(dbCommand, "@Reminder1", DbType.Date, this.Reminder1);
                db.AddInParameter(dbCommand, "@Reminder2", DbType.Date, this.Reminder2);
                db.AddInParameter(dbCommand, "@ManagerEscalation", DbType.Date, this.ManagerEscalation);
                db.AddInParameter(dbCommand, "@ReminderEmail", DbType.String, this.ReminderEmail);
                db.AddInParameter(dbCommand, "@ManagerEscalation", DbType.String, EscalationEmail);
                db.ExecuteNonQuery(dbCommand, transaction);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Delete_EmailNotification);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int16, this.HospitalId);
                db.ExecuteNonQuery(dbCommand, transaction);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmailNotification Search(Database db, DbTransaction transaction, int hospitalId)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_EmailNotification);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
            var results = db.ExecuteReader(dbCommand);

            EmailNotification item = new EmailNotification();
            item.HospitalId = hospitalId;

            int intVal;
            DateTime dateVal;

            if (results != null)
            {
                while (results.Read())
                {
                    if (int.TryParse(results["Id"].ToString(), out intVal))
                        item.Id = intVal;

                    item.ReminderEmail = results["ReminderEmail"].ToString();
                    item.EscalationEmail = results["ManagerEscalationEmail"].ToString();

                    if (DateTime.TryParse(results["Reminder1"].ToString(), out dateVal))
                        item.Reminder1 = dateVal;
                    if (DateTime.TryParse(results["Reminder2"].ToString(), out dateVal))
                        item.Reminder2 = dateVal;
                    if (DateTime.TryParse(results["ManagerEscalation"].ToString(), out dateVal))
                        item.ManagerEscalation = dateVal;
                }
            }

            return item;
        }

        public List<EmailNotification> SearchAll(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_EmailNotification);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, -1);
            var results = db.ExecuteReader(dbCommand);

            List<EmailNotification> list=new List<EmailNotification>();
            EmailNotification item;
            int intVal;
            DateTime dateVal;

            if (results != null)
            {
                while (results.Read())
                {
                    item = new EmailNotification();

                    if (int.TryParse(results["Id"].ToString(), out intVal))
                        item.Id = intVal;

                    if (int.TryParse(results["HospitalId"].ToString(), out intVal))
                        item.Id = intVal;

                    item.ReminderEmail = results["ReminderEmail"].ToString();
                    item.EscalationEmail = results["ManagerEscalationEmail"].ToString();

                    if (DateTime.TryParse(results["Reminder1"].ToString(), out dateVal))
                        item.Reminder1 = dateVal;
                    if (DateTime.TryParse(results["Reminder2"].ToString(), out dateVal))
                        item.Reminder2 = dateVal;
                    if (DateTime.TryParse(results["ManagerEscalation"].ToString(), out dateVal))
                        item.ManagerEscalation = dateVal;

                    list.Add(item);
                }
            }

            return list;
        }
        
    }
}
